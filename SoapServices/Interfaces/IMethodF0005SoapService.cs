using PruebaConexionIntegracion.SoapServices.Models.Response;

namespace PruebaConexionIntegracion.SoapServices.Interfaces
{
    public interface IMethodF0005SoapService
    {
        Task<IEnumerable<F0005ResponseSoapDto>> ObtenerProductosSoap();

        Task<IEnumerable<F0005ResponseSoapDto>> ObtenerCalidadSoap();
    }
}