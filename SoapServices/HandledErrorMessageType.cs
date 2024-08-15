namespace PruebaConexionIntegracion.SoapServices
{
    public static class HandledErrorMessageType
    {
        #region Errores Controlados

        public const string ErrorGeneraTokenTitle = "Error en el sistema";
        public const string ErrorGeneraTokenDetail = "Error al generar el token";

        public const string ErrorRespuestaSolicitudTitle = "Error en el sistema";
        public const string ErrorRespuestaSolicitudDetail = "La solicitud devolvió con un código diferente de 200";

        public const string ErrorRespuestaVaciaTitle = "Error en el sistema";
        public const string ErrorRespuestaVaciaDetail = "La solicitud devolvió una respuesta vacía";

        public const string ErrorDeserializarTitle = "Error en el sistema";
        public const string ErrorDeserializarDetail = "Error al convertir respuesta a un objeto";

        public const string ErrorCredencialesVaciasTitle = "Error en el sistema";
        public const string ErrorCredencialesVaciasDetail = "Error al recuperar las credenciales desde gsa";

        public const string ErrorPrepararEncabezarTitle = "Error en el sistema";
        public const string ErrorPrepararEncabezarDetail = "Error al preparar encabezados del requerimiento";
        #endregion Errores Controlados
    }
}