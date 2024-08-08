using PruebaConexionIntegracion.SoapServices.Models.Response;

namespace PruebaConexionIntegracion.SoapServices.Interfaces
{
    public interface IMethodRangoValorSoapService
    {
        #region Métodos de largo dedo

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerLargoDedoMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerLargoDedoMaximo();

        #endregion Métodos de largo dedo

        #region Métodos de calibración

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionMaximo();

        #endregion Métodos de calibración

        #region Métodos de dedo cluster

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoClusterMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoClusterMaximo();

        #endregion Métodos de dedo cluster

        #region Métodos de cluster caja

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerClusterCajaMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerClusterCajaMaximo();

        #endregion Métodos de cluster caja

        #region Metodos cuña 4 dedos

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña4DedosMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña4DedosMaximo();

        #endregion Metodos cuña 4 dedos

        #region Métodos cuña 3 dedos

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña3DedosMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña3DedosMaximo();

        #endregion Métodos cuña 3 dedos

        #region Métodos saneo mano

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerSaneoMano();

        #endregion Métodos saneo mano

        #region Métodos saneo cluster

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerSaneoCluster();

        #endregion Métodos saneo cluster

        #region Métodos saneo caja

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerSaneoCaja();

        #endregion Métodos saneo caja

        #region Métodos clustar funda

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerClusterFunda();

        #endregion Métodos clustar funda

        #region Métodos de tipo

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerTipoMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerTipoMaximo();

        #endregion Métodos de tipo

        #region Métodos de mano caja

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerManoCajaMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerManoCajaMaximo();

        #endregion Métodos de mano caja

        #region Métodos de dedo mano

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoManoMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoManoMaximo();

        #endregion Métodos de dedo mano

        #region Métodos de parafilm

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerParafilm();

        #endregion Métodos de parafilm

        #region Métodos de edad cinta barrida

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerEdadCintaBarrida();

        #endregion Métodos de edad cinta barrida

        #region Métodos de calibración cinta

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionCintaMinimo();

        Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionCintaMaximo();

        #endregion Métodos de calibración cinta
    }
}