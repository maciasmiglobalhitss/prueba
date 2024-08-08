using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class MethodF4101UnidadMedidaSoapService : IMethodF4101UnidadMedidaSoapService
    {
        private const string TipoImitm = "Imitm";
        private const string SoapRequest = "<soapenv:Envelope xmlns:soapenv=\"{0}\" xmlns:wss=\"{1}\"><soapenv:Header/><soapenv:Body><wss:getUniMed><wss:requestBean><wss:token>{2}</wss:token><wss:vCampo>{3}</wss:vCampo><wss:vCrit>{4}</wss:vCrit></wss:requestBean></wss:getUniMed></soapenv:Body></soapenv:Envelope>";

        private readonly IConfiguration configuration;
        private readonly IBaseSoapService baseSoapService;

        public MethodF4101UnidadMedidaSoapService(IConfiguration configuration, IBaseSoapService baseSoapService)
        {
            this.configuration = configuration;
            this.baseSoapService = baseSoapService;
        }

        public async Task<IEnumerable<F4101UnidadMedidaResponseSoapDto>> ObtenerUnidadesMedida()
        {
            return await EjecutarConsulta(TipoImitm);
        }

        private async Task<IEnumerable<F4101UnidadMedidaResponseSoapDto>> EjecutarConsulta(string tipo)
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
            restRequest.AddHeader(baseSoapService.SoapAction, configuration["SoapServices:MetodoF4101UnidadMedida"]!);

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
                .Descendants(baseSoapService.SoapXmlns + "getUniMedBean")
                .Select(bean => new F4101UnidadMedidaResponseSoapDto()
                {
                    Code = bean.Element(baseSoapService.SoapXmlns + "Code")?.Value ?? string.Empty,
                    Description = bean.Element(baseSoapService.SoapXmlns + "Description")?.Value ?? string.Empty,
                });

            return resultado;
        }
    }
}