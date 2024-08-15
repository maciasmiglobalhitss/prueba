namespace PruebaConexionIntegracion.SoapServices.Interfaces
{
    public interface IBaseSoapService
    {
        string SoapEnv { get; }
        string SoapWss { get; }
        string TextXml { get; }
        string SoapAction { get; }
        string SoapXmlns { get; }
        bool IgnorarSSl { get; }

        Task<string> ObtenerTokenSoap();
    }
}