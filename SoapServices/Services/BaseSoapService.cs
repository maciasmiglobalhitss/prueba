using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System.Buffers;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class BaseSoapService : IBaseSoapService
    {
        protected const string m_SoapEnv = "http://schemas.xmlsoap.org/soap/envelope/";
        protected const string m_SoapWss = "http://magicsoftware.com/wsdl/com/magicsoftware/magicxpi/favoritafruit/WSSinergia/";
        protected const string m_SoapXmlns = "http://magicsoftware.com/wsdl/com/magicsoftware/magicxpi/favoritafruit/WSSinergia/";
        protected const string m_TextXml = "text/xml";
        protected const string m_SoapAction = "SOAPAction";

        private const string SoapRequestGsa = "<soapenv:Envelope xmlns:soapenv=\"{0}\" xmlns:kb=\"KB_GW_Seguridad\"><soapenv:Header/><soapenv:Body><kb:gpsys_ws.CONSULTA_PRM_SEGURIDAD><kb:Scg_app_codigo>{1}</kb:Scg_app_codigo><kb:Gps_prm_rec_codigo>{2}</kb:Gps_prm_rec_codigo></kb:gpsys_ws.CONSULTA_PRM_SEGURIDAD></soapenv:Body></soapenv:Envelope>";

        private readonly IConfiguration configuration;

        public BaseSoapService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string SoapEnv => m_SoapEnv;

        public string SoapWss => m_SoapWss;

        public string TextXml => m_TextXml;

        public string SoapAction => m_SoapAction;

        public string SoapXmlns => m_SoapXmlns;

        public async Task<string> ObtenerTokenSoap()
        {
            #region Recuperación de credenciales soffi
            var credencialesGsa = await ObtenerCredencialesSoap();

            var audUserCods = configuration["Gsa:AudUserCod"]!.Split(";");
            var audPassCods = configuration["Gsa:AudPassCod"]!.Split(";");
            var tokenUserCods = configuration["Gsa:TokenUserCod"]!.Split(";");
            var tokenPassCods = configuration["Gsa:TokenPassCod"]!.Split(";");

            var aud_user = credencialesGsa
                .FirstOrDefault(e => audUserCods.Contains(e.Codigo))?
                .Valor;
            var aud_pass = credencialesGsa
                .FirstOrDefault(e => audPassCods.Contains(e.Codigo))?
                .Valor;
            var tokenuser = credencialesGsa
                .FirstOrDefault(e => tokenUserCods.Contains(e.Codigo))?
                .Valor;
            var tokenpass = credencialesGsa
                .FirstOrDefault(e => tokenPassCods.Contains(e.Codigo))?
                .Valor;

            if (string.IsNullOrEmpty(aud_user) || string.IsNullOrEmpty(aud_pass) ||
                string.IsNullOrEmpty(tokenuser) || string.IsNullOrEmpty(tokenpass))
                throw new SoapServiceException(HandledErrorMessageType.ErrorCredencialesVaciasTitle, HandledErrorMessageType.ErrorCredencialesVaciasDetail);

            #endregion Recuperación de credenciales soffi

            #region Generación del token

            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(configuration["UrlGeneraToken"]!),
                Authenticator = new HttpBasicAuthenticator(aud_user, Base64Decode(aud_pass))
            };
            var client = new RestClient(options);
            var request = new RestRequest()
            {
                Method = Method.Post,
            };

            request.AddParameter("username", tokenuser);
            request.AddParameter("password", Base64Decode(tokenpass));
            request.AddParameter("grant_type", "password");

            // execute the request
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaSolicitudTitle, HandledErrorMessageType.ErrorRespuestaSolicitudDetail);

            if (string.IsNullOrEmpty(response.Content))
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaVaciaTitle, HandledErrorMessageType.ErrorRespuestaVaciaDetail);

            var customer = JsonSerializer.Deserialize<UserTokenResponseSoapDto>(response.Content)
                ?? throw new SoapServiceException(HandledErrorMessageType.ErrorDeserializarTitle, HandledErrorMessageType.ErrorDeserializarDetail);

            #endregion Generación del token

            return customer.Id_token;
        }

        private async Task<IEnumerable<CredencialesGsaResponseSoapDto>> ObtenerCredencialesSoap()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
               (sender, certificate, chain, sslPolicyErrors) => true;

            // Generamos el request
            string requestSoap = string.Format(SoapRequestGsa,
                SoapEnv, configuration["Gsa:CodApplication"]!, configuration["Gsa:CodResource"]!);
            var soapAction = configuration["Gsa:MetodoCredenciales"]!;

            // Generamos la consulta
            var restClient = new RestClient(configuration["Gsa:BaseUrl"]!);
            var restRequest = new RestRequest()
            {
                Method = Method.Post,
            };

            restRequest.AddHeader("Content-Type", "text/xml;charset=UTF-8");
            restRequest.AddHeader("SOAPAction", soapAction);
            restRequest.AddParameter("text/xml", requestSoap, ParameterType.RequestBody);

            var response = await restClient.ExecuteAsync(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaSolicitudTitle, HandledErrorMessageType.ErrorRespuestaSolicitudDetail);

            var responseContent = response.Content;
            if (string.IsNullOrEmpty(responseContent))
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaVaciaTitle, HandledErrorMessageType.ErrorRespuestaVaciaDetail);

            // Parsear la respuesta SOAP
            var xDocument = XDocument.Parse(responseContent);
            XNamespace nameSpaceXmlns = SoapXmlns;

            var resultado = xDocument
                .Descendants(SoapXmlns + "gps_sdt_get_parametrosItem")
                .Select(bean => new CredencialesGsaResponseSoapDto()
                {
                    Codigo = (bean.Element(SoapXmlns + "codigo")?.Value ?? string.Empty).Trim(),
                    Descripcion = (bean.Element(SoapXmlns + "descripcion")?.Value ?? string.Empty).Trim(),
                    Valor = (bean.Element(SoapXmlns + "valor")?.Value ?? string.Empty).Trim(),
                });

            return resultado;
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}