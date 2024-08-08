using PruebaConexionIntegracion.SoapServices.Models.Response;

namespace PruebaConexionIntegracion.SoapServices.Interfaces
{
    public interface IMethodF4101ProductoSoapService
    {
        Task<IEnumerable<F4101ProductoResponseSoapDto>> ObtenerFundasSoap();
    }
}