using PruebaConexionIntegracion.SoapServices.Models.Commons;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Interfaces
{
    public interface IBaseSoapService
    {
        string SoapEnv { get; }
        string SoapWss { get; }
        string TextXml { get; }
        string SoapAction { get; }
        string SoapXmlns { get; }
        Task<string> ObtenerTokenAutorizacionConsumoSoap();
        Task<XDocument> ExecuteSoapRequest(SoapMethodRequestDto requestDto);
    }
}