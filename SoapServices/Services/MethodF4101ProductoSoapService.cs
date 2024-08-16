using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices.Extensions;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Commons;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class MethodF4101ProductoSoapService : IMethodF4101ProductoSoapService
    {
        private const string TipoFundas = "V_Fundas";
        private const string VCampoFunda = "IMDSC1";
        private const string SoapRequest = "<soapenv:Envelope xmlns:soapenv=\"{0}\" xmlns:wss=\"{1}\"><soapenv:Header/><soapenv:Body><wss:getProductos><wss:requestBean><wss:token>{2}</wss:token><wss:vIdItem>{3}</wss:vIdItem><wss:vCampo>{4}</wss:vCampo><wss:vCrit>{5}</wss:vCrit></wss:requestBean></wss:getProductos></soapenv:Body></soapenv:Envelope>";

        private readonly IConfiguration configuration;
        private readonly IBaseSoapService baseSoapService;

        public MethodF4101ProductoSoapService(IConfiguration configuration, IBaseSoapService baseSoapService)
        {
            this.configuration = configuration;
            this.baseSoapService = baseSoapService;
        }

        public async Task<IEnumerable<F4101ProductoResponseSoapDto>> ObtenerFundasSoap()
        {
            return await EjecutarConsulta(TipoFundas, VCampoFunda);
        }

        private async Task<IEnumerable<F4101ProductoResponseSoapDto>> EjecutarConsulta(
            string vIdItem, string vCampo = "", string vCritic = "")
        {
            // Generamos el token
            string token = await baseSoapService.ObtenerTokenAutorizacionConsumoSoap();

            if (string.IsNullOrEmpty(token))
                throw new SoapServiceException(HandledErrorMessageType.ErrorGeneraTokenTitle, HandledErrorMessageType.ErrorGeneraTokenTitle);

            // Generamos el request
            string requestSoap = string.Format(SoapRequest, baseSoapService.SoapEnv,
                baseSoapService.SoapWss, token, vIdItem, vCampo, vCritic);

            var soapMethodRequetDto = new SoapMethodRequestDto(
                configuration["SoapServices:BaseUrl"]!, configuration["SoapServices:MetodoF4101Producto"]!,
                requestSoap, [new("xsi", "xmlns:xsi=\"xsi\"")]);

            // Parsear la respuesta SOAP
            var xDocument = await baseSoapService.ExecuteSoapRequest(soapMethodRequetDto);
            XNamespace nameSpaceXmlns = baseSoapService.SoapXmlns;

            List<F4101ProductoResponseSoapDto> resultado = [];
            var desencats = xDocument
                .Descendants(nameSpaceXmlns + "getProductosBean")
                .ToList();

            // Verificamos si el servicio nos devolvió un mensaje de error
            desencats.ForEach(bean =>
            {
                var mensaje = bean.Element(nameSpaceXmlns + "message")?.Value;
                if (!string.IsNullOrEmpty(mensaje)) throw new SoapServiceException(HandledErrorMessageType.ErrorErrorServicioTitle, HandledErrorMessageType.ErrorErrorServicioDetail);

                resultado.Add(new F4101ProductoResponseSoapDto()
                {
                    Id = Convert.ToDecimal(bean.Element(nameSpaceXmlns + "id")?.Value),
                    It = bean.Element(nameSpaceXmlns + "it")?.Value ?? string.Empty,
                    Lit = bean.Element(nameSpaceXmlns + "lit")?.Value ?? string.Empty,
                    Ct = bean.Element(nameSpaceXmlns + "ct")?.Value ?? string.Empty,
                    Jp = bean.Element(nameSpaceXmlns + "jp")?.Value ?? string.Empty,
                    Cod = bean.Element(nameSpaceXmlns + "cod")?.Value ?? string.Empty,
                });
            });

            return resultado;
        }
        private static string ProcesarEncabezadosFaltantes(string responseContent)
        {
            ICollection<NameSpaceXmlDto> encabezados = [
                new("xsi", "xmlns:xsi=\"xsi\"")
            ];

            string pattern = "^<([^>]*)>";
            var match = Regex.Match(responseContent, pattern);
            if (!match.Success) throw new SoapServiceException(HandledErrorMessageType.ErrorPrepararEncabezarTitle, HandledErrorMessageType.ErrorPrepararEncabezarDetail);

            var nuevoEncabezado = match.Groups[1].Value
                .Replace("<", string.Empty)
                .Replace(">", string.Empty);

            foreach (var encabezado in encabezados)
            {
                if (!nuevoEncabezado.Contains(encabezado.Prefijo) && responseContent.Contains(encabezado.Prefijo))
                    nuevoEncabezado += $" {encabezado.Encabezado}";
            }

            return Regex.Replace(responseContent, pattern, $"<{nuevoEncabezado}>");
        }
    }
}