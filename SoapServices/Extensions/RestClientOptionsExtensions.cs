using RestSharp;

namespace PruebaConexionIntegracion.SoapServices.Extensions
{
    public static class RestClientOptionsExtensions
    {
        public static void AplicarByPassSsl(this RestClientOptions options)
        {
            options.ConfigureMessageHandler = handler =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
                };
            };
        }
    }
}
