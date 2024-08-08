using Microsoft.Extensions.Configuration;
using PruebaConexionIntegracion.Commons;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Models.Response;
using RestSharp;
using System.Xml.Linq;

namespace PruebaConexionIntegracion.SoapServices.Services
{
    public class MethodRangoValorSoapService : IMethodRangoValorSoapService
    {
        private const string SoapRequest = "<soapenv:Envelope xmlns:soapenv=\"{0}\" xmlns:wss=\"{1}\"><soapenv:Header/><soapenv:Body><wss:getRangos><wss:requestBean><wss:token>{2}</wss:token><wss:vIdTipo>{3}</wss:vIdTipo><wss:vGrupo>{4}</wss:vGrupo></wss:requestBean></wss:getRangos></soapenv:Body></soapenv:Envelope>";

        #region Constantes

        private const string VIdTipoVacio = "";
        private const string VIdTipoLargoDedoMinimo = "9";
        private const string VIdTipoLargoDedoMaximo = "9";
        private const string VIdTipoCintaBarrida = "11";

        private const string VGrupoLargoDedoMinimo = "1";
        private const string VGrupoLargoDedoMaximo = "1";
        private const string VGrupoCalibracionMinimo = "2";
        private const string VGrupoCalibracionMaximo = "3";
        private const string VGrupoDedoClusterMinimo = "4";
        private const string VGrupoDedoClusterMaximo = "4";
        private const string VGrupoClusterCajaMinimo = "5";
        private const string VGrupoClusterCajaMaximo = "5";
        private const string VGrupoCuña4DedosMinimo = "6";
        private const string VGrupoCuña4DedosMaximo = "6";
        private const string VGrupoCuña3DedosMinimo = "7";
        private const string VGrupoCuña3DedosMaximo = "7";
        private const string VGrupoSaneoMano = "8";
        private const string VGrupoSaneoCluster = "9";
        private const string VGrupoSaneoCaja = "10";
        private const string VGrupoClusterFunda = "11";
        private const string VGrupoTipoMinimo = "12";
        private const string VGrupoTipoMaximo = "12";
        private const string VGrupoManoCajaMinimo = "13";
        private const string VGrupoManoCajaMaximo = "13";
        private const string VGrupoDedoManoMinimo = "14";
        private const string VGrupoDedoManoMaximo = "14";
        private const string VGrupoParafilm = "15";
        private const string VGrupoEdadCintaBarrida = "1";
        private const string VGrupoCalibracionCintaMinimo = "2";
        private const string VGrupoCalibracionCintaMaximo = "2";

        #endregion Constantes

        private readonly IConfiguration configuration;
        private readonly IBaseSoapService baseSoapService;

        public MethodRangoValorSoapService(IConfiguration configuration, IBaseSoapService baseSoapService)
        {
            this.configuration = configuration;
            this.baseSoapService = baseSoapService;
        }

        #region Métodos de Largo dedo

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerLargoDedoMinimo()
        {
            return await EjecutarConsulta(VIdTipoLargoDedoMinimo, VGrupoLargoDedoMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerLargoDedoMaximo()
        {
            return await EjecutarConsulta(VIdTipoLargoDedoMaximo, VGrupoLargoDedoMaximo);
        }

        #endregion Métodos de Largo dedo

        #region Métodos de Calibracion

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCalibracionMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCalibracionMaximo);
        }

        #endregion Métodos de Calibracion

        #region Métodos de dedo cluster

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoClusterMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoDedoClusterMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoClusterMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoDedoClusterMaximo);
        }

        #endregion Métodos de dedo cluster

        #region Métodos de cluster caja

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerClusterCajaMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoClusterCajaMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerClusterCajaMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoClusterCajaMaximo);
        }

        #endregion Métodos de cluster caja

        #region Métodos cuña 4 dedos

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña4DedosMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCuña4DedosMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña4DedosMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCuña4DedosMaximo);
        }

        #endregion Métodos cuña 4 dedos

        #region Métodos cuña 3 dedos

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña3DedosMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCuña3DedosMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCuña3DedosMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCuña3DedosMaximo);
        }

        #endregion Métodos cuña 3 dedos

        #region Métodos de saneo mano

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerSaneoMano()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoSaneoMano);
        }

        #endregion Métodos de saneo mano

        #region Métodos de saneo cluster

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerSaneoCluster()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoSaneoCluster);
        }

        #endregion Métodos de saneo cluster

        #region Métodos de saneo caja

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerSaneoCaja()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoSaneoCaja);
        }

        #endregion Métodos de saneo caja

        #region Métodos de cluster funda

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerClusterFunda()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoClusterFunda);
        }

        #endregion Métodos de cluster funda

        #region Métodos de tipo

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerTipoMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoTipoMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerTipoMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoTipoMaximo);
        }

        #endregion Métodos de tipo

        #region Métodos de mano caja

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerManoCajaMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoManoCajaMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerManoCajaMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoManoCajaMaximo);
        }

        #endregion Métodos de mano caja

        #region Métodos de dedo mano

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoManoMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoDedoManoMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerDedoManoMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoDedoManoMaximo);
        }

        #endregion Métodos de dedo mano

        #region Métodos de parafilm

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerParafilm()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoParafilm);
        }

        #endregion Métodos de parafilm

        #region Métodos de edad cinta barrida

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerEdadCintaBarrida()
        {
            return await EjecutarConsulta(VIdTipoCintaBarrida, VGrupoEdadCintaBarrida);
        }

        #endregion Métodos de edad cinta barrida

        #region Métodos de calibracion cinta

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionCintaMinimo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCalibracionCintaMinimo);
        }

        public async Task<IEnumerable<RangoValorResponseSoapDto>> ObtenerCalibracionCintaMaximo()
        {
            return await EjecutarConsulta(VIdTipoVacio, VGrupoCalibracionCintaMaximo);
        }

        #endregion Métodos de calibracion cinta

        #region Métodos de ejecución de consulta

        private async Task<IEnumerable<RangoValorResponseSoapDto>> EjecutarConsulta(string vIdTipo, string vGrupo)
        {
            // Generamos el token
            string token = await baseSoapService.ObtenerTokenSoap();

            if (string.IsNullOrEmpty(token))
                throw new SoapServiceException(HandledErrorMessageType.ErrorGeneraTokenTitle, HandledErrorMessageType.ErrorGeneraTokenTitle);

            // Generamos el request
            string requestSoap = string.Format(SoapRequest, baseSoapService.SoapEnv, baseSoapService.SoapWss, token, vIdTipo, vGrupo);

            // Generamos la consulta
            var restClient = new RestClient(configuration["SoapServices:BaseUrl"]!);
            var restRequest = new RestRequest()
            {
                Method = Method.Post,
            };
            restRequest.AddHeader("Content-Type", baseSoapService.TextXml);
            restRequest.AddBody(requestSoap, baseSoapService.TextXml);
            restRequest.AddHeader(baseSoapService.SoapAction, configuration["SoapServices:MetodoRangoValor"]!);

            var response = await restClient.ExecuteAsync(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaSolicitudTitle, HandledErrorMessageType.ErrorRespuestaSolicitudDetail);

            var responseContent = response.Content;
            if (string.IsNullOrEmpty(responseContent))
                throw new SoapServiceException(HandledErrorMessageType.ErrorRespuestaVaciaTitle, HandledErrorMessageType.ErrorRespuestaVaciaDetail);

            // Parsear la respuesta SOAP
            var xDocument = XDocument.Parse(responseContent);
            XNamespace nameSpaceXmlns = baseSoapService.SoapXmlns;

            var resultado = xDocument
                .Descendants(baseSoapService.SoapXmlns + "getRangosBean")
                .Select(bean => new RangoValorResponseSoapDto()
                {
                    VValor = Convert.ToDecimal(bean.Element(baseSoapService.SoapXmlns + "vValor")?.Value),
                });

            return resultado;
        }

        #endregion Métodos de ejecución de consulta
    }
}