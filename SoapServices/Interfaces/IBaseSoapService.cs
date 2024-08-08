namespace PruebaConexionIntegracion.SoapServices.Interfaces
{
    public interface IBaseSoapService
    {
        string SoapEnv { get; }
        string SoapWss { get; }
        string TextXml { get; }
        string SoapAction { get; }
        string SoapXmlns { get; }

        Task<string> ObtenerTokenSoap();
    }
}