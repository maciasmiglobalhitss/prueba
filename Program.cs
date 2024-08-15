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
    await VerificarFuncionaMethodF0005Soap(services);
    await VerificarFuncionaMethodF4101ProductoSoap(services);

}

static async Task VerificarFuncionaMethodF0005Soap(IServiceProvider services)
{
    var service = services.GetRequiredService<IMethodF0005SoapService>();
    try
    {
        var respuesta = await service.ObtenerProductosSoap();
        if (respuesta.Any())
        {
            Console.WriteLine("IMethodF0005SoapService OK!!");
        }
        else
        {
            Console.WriteLine("IMethodF0005SoapService Empty!!");
        }
    }
    catch (Exception)
    {
        Console.WriteLine("IMethodF0005SoapService Error!!");
    }
}

static async Task VerificarFuncionaMethodF4101ProductoSoap(IServiceProvider services)
{
    var service = services.GetRequiredService<IMethodF4101ProductoSoapService>();
    try
    {
        var respuesta = await service.ObtenerFundasSoap();
        if (respuesta.Any())
        {
            Console.WriteLine("IMethodF4101ProductoSoapService OK!!");
        }
        else
        {
            Console.WriteLine("IMethodF4101ProductoSoapService Empty!!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("IMethodF4101ProductoSoapService Error!!");
    }
}


//await builder.RunConsoleAsync();