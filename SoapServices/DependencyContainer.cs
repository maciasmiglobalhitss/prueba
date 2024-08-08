using Microsoft.Extensions.DependencyInjection;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using PruebaConexionIntegracion.SoapServices.Services;

namespace PruebaConexionIntegracion.SoapServices
{
    public static class DependencyContainer
    {
        public static IServiceCollection ConfigurarServicioSoap(this IServiceCollection services)
        {
            services.AddTransient<IBaseSoapService, BaseSoapService>();
            services.AddTransient<IMethodF0005SoapService, MethodF0005SoapService>();
            services.AddTransient<IMethodF4101ProductoSoapService, MethodF4101ProductoSoapService>();
            services.AddTransient<IMethodF4101UnidadMedidaSoapService, MethodF4101UnidadMedidaSoapService>();
            services.AddTransient<IMethodRangoValorSoapService, MethodRangoValorSoapService>();

            return services;
        }
    }
}