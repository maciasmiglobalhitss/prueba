using PruebaConexionIntegracion.SoapServices.Models.Response;

namespace PruebaConexionIntegracion.SoapServices.Interfaces
{
    public interface IMethodF4101UnidadMedidaSoapService
    {
        Task<IEnumerable<F4101UnidadMedidaResponseSoapDto>> ObtenerUnidadesMedida();
    }
}