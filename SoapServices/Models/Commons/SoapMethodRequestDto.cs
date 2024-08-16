using System.ComponentModel.DataAnnotations;

namespace PruebaConexionIntegracion.SoapServices.Models.Commons
{
    public class SoapMethodRequestDto
    {
        [Required(ErrorMessage = "La url báse para el consumo del servicio es obligatoria")]
        public string BaseUrl { get; }

        [Required(ErrorMessage = "La url del método a consumir el servicio es obligatoria")]
        public string MethodUrl { get; }

        [Required(ErrorMessage = "El xml de la petición es obligatorio")]
        public string RequestXml { get; }

        public IList<NameSpaceXmlDto> NameSpaces { get; }

        public SoapMethodRequestDto(string baseUrl, string methodUrl,
            string requestXml, IList<NameSpaceXmlDto> nameSpaces)
        {
            BaseUrl = baseUrl;
            MethodUrl = methodUrl;
            RequestXml = requestXml;
            NameSpaces = nameSpaces;
        }
    }
}
