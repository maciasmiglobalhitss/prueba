using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class MethodF0005SoapService : IMethodF0005SoapService
    {
        private const string TipoProducto = "V_PRODAGR";
        private const string TipoCalidad = "V_Calidad";
        private const string SoapRequest = "<soapenv:Envelope xmlns:soapenv=\"{0}\" xmlns:wss=\"{1}\"><soapenv:Header/><soapenv:Body><wss:getUdcs><wss:requestBean><wss:token>{2}</wss:token><wss:vIdUDC>{3}</wss:vIdUDC><wss:vCrit></wss:vCrit></wss:requestBean></wss:getUdcs></soapenv:Body></soapenv:Envelope>";

        private readonly IConfiguration configuration;
        private readonly IBaseSoapService baseSoapService;

        public MethodF0005SoapService(IConfiguration configuration, IBaseSoapService baseSoapService)
        {
            this.configuration = configuration;
            this.baseSoapService = baseSoapService;
        }

        public async Task<IEnumerable<F0005ResponseSoapDto>> ObtenerCalidadSoap()
        {
            return await EjecutarConsulta(TipoCalidad);
        }

        public async Task<IEnumerable<F0005ResponseSoapDto>> ObtenerProductosSoap()
        {
            return await EjecutarConsulta(TipoProducto);
        }

        private async Task<IEnumerable<F0005ResponseSoapDto>> EjecutarConsulta(string tipoConsulta)
        {
            // Generamos el token
            string token = await baseSoapService.ObtenerTokenSoap();

            if (string.IsNullOrEmpty(token))
                throw new SoapServiceException(HandledErrorMessageType.ErrorGeneraTokenTitle, HandledErrorMessageType.ErrorGeneraTokenTitle);

            // Generamos el request
            string requestSoap = string.Format(SoapRequest, baseSoapService.SoapEnv, baseSoapService.SoapWss, token, tipoConsulta);

            // Generamos la consulta
            var restClient = new RestClient(configuration["SoapServices:BaseUrl"]!);
            var restRequest = new RestRequest()
            {
                Method = Method.Post,
            };
            restRequest.AddHeader("Content-Type", baseSoapService.TextXml);
            restRequest.AddBody(requestSoap, baseSoapService.TextXml);
            restRequest.AddHeader(baseSoapService.SoapAction, configuration["SoapServices:MetodoF0005"]!);

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
                .Descendants(baseSoapService.SoapXmlns + "getUdcsBean")
                .Select(bean => new F0005ResponseSoapDto()
                {
                    Code = bean.Element(baseSoapService.SoapXmlns + "Code")?.Value ?? string.Empty,
                    Description = bean.Element(baseSoapService.SoapXmlns + "Description")?.Value ?? string.Empty,
                });

            return resultado;
        }
    }
}