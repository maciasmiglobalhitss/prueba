using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices.Extensions;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Commons;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class MethodF0005SoapService : IMethodF0005SoapService
    {
        private const string TipoProducto = "V_PRODAGR";
        private const string TipoCalidad = "V_Calidad";
        private const string SoapRequest = "<soapenv:Envelope xmlns:soapenv=\"{0}\" xmlns:wss=\"{1}\"><soapenv:Header/><soapenv:Body><wss:getUdcs><wss:requestBean><wss:token>{2}</wss:token><wss:vIdUDC>{3}</wss:vIdUDC><wss:vCrit>{4}</wss:vCrit></wss:requestBean></wss:getUdcs></soapenv:Body></soapenv:Envelope>";

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

        private async Task<IEnumerable<F0005ResponseSoapDto>> EjecutarConsulta(
            string vIdUDC = "", string vCri = "")
        {
            // Generamos el token
            string token = await baseSoapService.ObtenerTokenAutorizacionConsumoSoap();

            if (string.IsNullOrEmpty(token))
                throw new SoapServiceException(HandledErrorMessageType.ErrorGeneraTokenTitle, HandledErrorMessageType.ErrorGeneraTokenTitle);

            // Generamos el request
            string requestSoap = string.Format(SoapRequest, baseSoapService.SoapEnv, baseSoapService.SoapWss, token, vIdUDC, vCri);

            var soapMethodRequetDto = new SoapMethodRequestDto(
                configuration["SoapServices:BaseUrl"]!, configuration["SoapServices:MetodoF0005"]!,
                requestSoap, []);

            // Parsear la respuesta SOAP
            var xDocument = await baseSoapService.ExecuteSoapRequest(soapMethodRequetDto);
            XNamespace nameSpaceXmlns = baseSoapService.SoapXmlns;

            List<F0005ResponseSoapDto> resultado = [];
            var desencats = xDocument
                .Descendants(nameSpaceXmlns + "getUdcsBean")
                .ToList();

            // Verificamos si el servicio nos devolvió un mensaje de error
            desencats.ForEach(bean =>
            {
                var mensaje = bean.Element(nameSpaceXmlns + "message")?.Value;
                if (!string.IsNullOrEmpty(mensaje)) throw new SoapServiceException(HandledErrorMessageType.ErrorErrorServicioTitle, HandledErrorMessageType.ErrorErrorServicioDetail);

                resultado.Add(new F0005ResponseSoapDto()
                {
                    Code = bean.Element(nameSpaceXmlns + "Code")?.Value ?? string.Empty,
                    Description = bean.Element(nameSpaceXmlns + "Description")?.Value ?? string.Empty,
                });
            });

            return resultado;
        }
    }
}