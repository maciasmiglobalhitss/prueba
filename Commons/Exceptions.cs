namespace PruebaConexionIntegracion.Commons
{
    public class SoapServiceException : Exception
    {
        public string Title { get; }
        public string Layer { get; }

        public SoapServiceException(string title, string message) : base(message)
        {
            Title = title;
            Layer = "Layers.CodeGateway";
            HResult = 0;
        }

        public SoapServiceException(string title, string message, Exception innerException)
            : base(message, innerException)
        {
            Title = title;
            Layer = "Layers.CodeGateway";
            HResult = 0;
        }
    }
}