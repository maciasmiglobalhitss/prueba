using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebaConexionIntegracion.SoapServices;
using PruebaConexionIntegracion.SoapServices.Interfaces;
using System.DirectoryServices.AccountManagement;


var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigurarServicioSoap();
    });

//// Prueba Conexion
//OpenSqlConnection();

//// Prueba de los servicios
//var host = builder.Build();
//using (var scope = host.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await VerificarFuncionaMethodF0005Soap(services);
//    await VerificarFuncionaMethodF4101ProductoSoap(services);
//    await VerificarFuncionaMethodF4101UnidadMediaSoap(services);
//    await VerificarFuncionaMethodRangoValorSoap(services);
//}


// Prueba de login
VerificarLoginAdOnPremisse();

#region Métodos adicionales


static void OpenSqlConnection()
{
    var template = "Server={0};database={1};User ID={2};Password={3};Trusted_Connection=true;trustServerCertificate=yes;Integrated Security=false;";
    var servidor = ".,1433";
    var baseDatos = "sinergia2";
    var usuario = "sa";
    var contraseña = "Farkas1205_";

    var connectionString = string.Format(template, servidor, baseDatos, usuario, contraseña);
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
            Console.WriteLine("State: {0}", connection.State);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("State: {0}", ex.Message + "Interna :" + ex.InnerException?.Message);
    }
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


static void VerificarLoginAdOnPremisse()
{
    try
    {
        string domainName = Environment.UserDomainName;
        var username = "mamz9524";
        var password = "Departamento9*";

        var context = new PrincipalContext(ContextType.Domain, domainName);
        bool isValid = context.ValidateCredentials(username, password, ContextOptions.Negotiate);

        if (isValid)
        {
            Console.WriteLine("Credenciais válidas");
        }
        else
        {
            Console.WriteLine("Credenciais inválidas");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
        Console.WriteLine("Error Interno: " + ex.InnerException?.Message);
    }
}
#endregion

//await builder.RunConsoleAsync(); 