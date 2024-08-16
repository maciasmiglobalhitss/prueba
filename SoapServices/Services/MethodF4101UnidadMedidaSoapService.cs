using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices.Extensions;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Commons;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using System.Net;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class MethodF4101UnidadMedidaSoapService : IMethodF4101UnidadMedidaSoapService
    {
        private const string TipoImitm = "Imitm";
        private const string VCritValue = "204036";
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
            return await EjecutarConsulta(TipoImitm, VCritValue);
        }

        private async Task<IEnumerable<F4101UnidadMedidaResponseSoapDto>> EjecutarConsulta(string vCampo = "", string vCritic = "")
        {
            // Generamos el token
            string token = await baseSoapService.ObtenerTokenAutorizacionConsumoSoap();

            if (string.IsNullOrEmpty(token))
                throw new SoapServiceException(HandledErrorMessageType.ErrorGeneraTokenTitle, HandledErrorMessageType.ErrorGeneraTokenTitle);

            // Generamos el request
            string requestSoap = string.Format(SoapRequest, baseSoapService.SoapEnv,
                baseSoapService.SoapWss, token, vCampo, vCritic);

            var soapMethodRequetDto = new SoapMethodRequestDto(
                configuration["SoapServices:BaseUrl"]!, configuration["SoapServices:MetodoF4101UnidadMedida"]!,
                requestSoap, []);

            var xDocument = await baseSoapService.ExecuteSoapRequest(soapMethodRequetDto);
            XNamespace nameSpaceXmlns = baseSoapService.SoapXmlns;

            List<F4101UnidadMedidaResponseSoapDto> resultado = [];
            var desencats = xDocument
                .Descendants(nameSpaceXmlns + "getUniMedBean")
                .ToList();

            // Verificamos si el servicio nos devolvió un mensaje de error
            desencats.ForEach(bean =>
            {
                var mensaje = bean.Element(nameSpaceXmlns + "message")?.Value;
                if (!string.IsNullOrEmpty(mensaje)) throw new SoapServiceException(HandledErrorMessageType.ErrorErrorServicioTitle, HandledErrorMessageType.ErrorErrorServicioDetail);

                resultado.Add(new F4101UnidadMedidaResponseSoapDto()
                {
                    Code = bean.Element(nameSpaceXmlns + "Code")?.Value ?? string.Empty,
                    Description = bean.Element(nameSpaceXmlns + "Description")?.Value ?? string.Empty,
                });
            });

            return resultado;
        }
    }
}