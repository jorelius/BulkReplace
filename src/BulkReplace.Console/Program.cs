using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerArgs;

namespace BulkReplace.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = ConfigureServices();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider(true))
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                ILogger<Program> logger = scope.ServiceProvider.GetService<ILogger<Program>>();

                try
                {
                    Controller parsed = null;
                    try
                    {
                        System.Console.WriteLine();

                        Args.RegisterFactory(typeof(Controller),
                            () => { return scope.ServiceProvider.GetRequiredService<Controller>(); });
                        Args.InvokeAction<Controller>(args);
                    }
                    catch (ArgException ex)
                    {
                        System.Console.WriteLine(ex.Message);
                        System.Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<Controller>());
                    }

                    // exit if help is requested
                    if (parsed == null || parsed.Help)
                    {
                        return;
                    }
                } 
                catch (Exception e)
                {
                    logger.LogError(e.Message, e);
                }
            }
        }

        private static IServiceCollection ConfigureServices()
        {
            //setup our DI
            IServiceCollection serviceProvider = new ServiceCollection()
                .AddLogging(
                    logging =>
                    {
                        logging.AddConsole();
                    })
                .AddTransient<Controller>();

            return serviceProvider;
        }
    }
}
