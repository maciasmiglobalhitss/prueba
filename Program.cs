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
    await VerificarFuncionaMethodF4101UnidadMediaSoap(services);
    await VerificarFuncionaMethodRangoValorSoap(services);
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
static async Task VerificarFuncionaMethodF4101UnidadMediaSoap(IServiceProvider services)
{
    var service = services.GetRequiredService<IMethodF4101UnidadMedidaSoapService>();
    try
    {
        var respuesta = await service.ObtenerUnidadesMedida();
        if (respuesta.Any())
        {
            Console.WriteLine("IMethodF4101UnidadMedidaSoapService OK!!");
        }
        else
        {
            Console.WriteLine("IMethodF4101UnidadMedidaSoapService Empty!!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("IMethodF4101UnidadMedidaSoapService Error!!");
    }
}
static async Task VerificarFuncionaMethodRangoValorSoap(IServiceProvider services)
{
    var service = services.GetRequiredService<IMethodRangoValorSoapService>();
    try
    {
        var respuesta = await service.ObtenerLargoDedoMaximo();
        if (respuesta.Any())
        {
            Console.WriteLine("IMethodRangoValorSoapService OK!!");
        }
        else
        {
            Console.WriteLine("IMethodRangoValorSoapService Empty!!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("IMethodRangoValorSoapService Error!!");
    }
}


//await builder.RunConsoleAsync();