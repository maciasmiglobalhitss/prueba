using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class MethodF4101ProductoSoapService : IMethodF4101ProductoSoapService
    {
        private const string TipoFundas = "V_Fundas";
        private const string SoapRequest = "<soapenv:Envelope xmlns:soapenv=\"{0}\" xmlns:wss=\"{1}\"><soapenv:Header/><soapenv:Body><wss:getProductos><wss:requestBean><wss:token>{2}</wss:token><wss:vIdItem>{3}</wss:vIdItem><wss:vCampo>{4}</wss:vCampo><wss:vCrit></wss:vCrit></wss:requestBean></wss:getProductos></soapenv:Body></soapenv:Envelope>";

        private readonly IConfiguration configuration;
        private readonly IBaseSoapService baseSoapService;

        public MethodF4101ProductoSoapService(IConfiguration configuration, IBaseSoapService baseSoapService)
        {
            this.configuration = configuration;
            this.baseSoapService = baseSoapService;
        }

        public async Task<IEnumerable<F4101ProductoResponseSoapDto>> ObtenerFundasSoap()
        {
            return await EjecutarConsulta(TipoFundas);
        }

        private async Task<IEnumerable<F4101ProductoResponseSoapDto>> EjecutarConsulta(string tipo)
        {
            // Generamos el token
            string token = await baseSoapService.ObtenerTokenSoap();

            if (string.IsNullOrEmpty(token))
                throw new SoapServiceException(HandledErrorMessageType.ErrorGeneraTokenTitle, HandledErrorMessageType.ErrorGeneraTokenTitle);

            // Generamos el request
            string requestSoap = string.Format(SoapRequest, baseSoapService.SoapEnv, baseSoapService.SoapWss, token, tipo, string.Empty);

            // Generamos la consulta
            var restClient = new RestClient(configuration["SoapServices:BaseUrl"]!);
            var restRequest = new RestRequest()
            {
                Method = Method.Post,
            };
            restRequest.AddHeader("Content-Type", baseSoapService.TextXml);
            restRequest.AddBody(requestSoap, baseSoapService.TextXml);
            restRequest.AddHeader(baseSoapService.SoapAction, configuration["SoapServices:MetodoF4101Producto"]!);

            var response = await restClient.ExecuteAsync(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaSolicitudTitle, HandledErrorMessageType.ErrorRespuestaSolicitudDetail);

            var responseContent = response.Content;
            if (string.IsNullOrEmpty(responseContent))
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaVaciaTitle, HandledErrorMessageType.ErrorRespuestaVaciaDetail);

            // Parsear la respuesta SOAP
            var xDocument = XDocument.Parse(responseContent);
            XNamespace nameSpaceXmlns = baseSoapService.SoapXmlns;

            var resultado = xDocument
                .Descendants(baseSoapService.SoapXmlns + "getProductosBean")
                .Select(bean => new F4101ProductoResponseSoapDto()
                {
                    Id = Convert.ToDecimal(bean.Element(baseSoapService.SoapXmlns + "id")?.Value),
                    It = bean.Element(baseSoapService.SoapXmlns + "it")?.Value ?? string.Empty,
                    Lit = bean.Element(baseSoapService.SoapXmlns + "lit")?.Value ?? string.Empty,
                    Ct = bean.Element(baseSoapService.SoapXmlns + "ct")?.Value ?? string.Empty,
                    Jp = Convert.ToBoolean(bean.Element(baseSoapService.SoapXmlns + "jp")?.Value),
                    Cod = Convert.ToBoolean(bean.Element(baseSoapService.SoapXmlns + "cod")?.Value),
                });

            return resultado;
        }
    }
}