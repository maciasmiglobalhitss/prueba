namespace PruebaConexionIntegracion.SoapServices.Models.Commons
{
    public class NameSpaceXmlDto
    {
        public string Prefijo { get; }
        public string Encabezado { get; }
        
        public NameSpaceXmlDto(string prefijo, string encabezado) {
            this.Prefijo = prefijo;
            this.Encabezado = encabezado;
        }
    }
}
