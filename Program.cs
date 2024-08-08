using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebaConexionIntegracion.SoapServices;
using PruebaConexionIntegracion.SoapServices.Interfaces;

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigurarServicioSoap();
    });

var host = builder.Build();
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var methodF0005SoapService = services.GetRequiredService<IMethodF0005SoapService>();
    
    var respuestaProductos = await methodF0005SoapService.ObtenerProductosSoap();
    

}


await builder.RunConsoleAsync();