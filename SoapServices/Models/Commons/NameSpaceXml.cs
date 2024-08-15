namespace PruebaConexionIntegracion.SoapServices.Models.Commons
{
    public class NameSpaceXml
    {
        public string Prefijo { get; }
        public string Encabezado { get; }
        
        public NameSpaceXml(string prefijo, string encabezado) {
            this.Prefijo = prefijo;
            this.Encabezado = encabezado;
        }
    }
}
