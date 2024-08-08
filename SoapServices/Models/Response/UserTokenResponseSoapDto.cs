namespace PruebaConexionIntegracion.SoapServices.Models.Response
{
    public class UserTokenResponseSoapDto
    {
        public string Access_token { get; set; } = string.Empty!;
        public string Refresh_token { get; set; } = string.Empty!;
        public string Id_token { get; set; } = string.Empty!;
        public string Token_type { get; set; } = string.Empty!;
        public int Expires_in { get; set; }
    }
}