using LoggingLibrary;
using Api.Services;

namespace Api;

/// <summary>
/// Точка входа приложения
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Запуск приложения
    /// </summary>
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();

        // Запускаем демонстрацию жизненных циклов
        DemonstrateServiceLifetimes(host.Services);

        host.Run();
    }

    private static void DemonstrateServiceLifetimes(IServiceProvider hostServices)
    {

        // Первый scope
        Console.WriteLine("===== СОЗДАНИЕ SCOPE 1 =====");
        using (var scope1 = hostServices.CreateScope())
        {
            var services = scope1.ServiceProvider;

            services.PrintServiceDifference<SingletonService1>("Scope1");
            services.PrintServiceDifference<SingletonService2>("Scope1");
            services.PrintServiceDifference<ScopedService1>("Scope1");
            services.PrintServiceDifference<ScopedService2>("Scope1");
            services.PrintServiceDifference<TransientService1>("Scope1");
            services.PrintServiceDifference<TransientService2>("Scope1");
        }

        // Второй scope
        Console.WriteLine("===== СОЗДАНИЕ SCOPE 2 =====");
        using (var scope2 = hostServices.CreateScope())
        {
            var services = scope2.ServiceProvider;

            services.PrintServiceDifference<SingletonService1>("Scope2");
            services.PrintServiceDifference<SingletonService2>("Scope2");
            services.PrintServiceDifference<ScopedService1>("Scope2");
            services.PrintServiceDifference<ScopedService2>("Scope2");
            services.PrintServiceDifference<TransientService1>("Scope2");
            services.PrintServiceDifference<TransientService2>("Scope2");
        }

    }
}