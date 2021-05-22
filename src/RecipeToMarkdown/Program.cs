using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace RecipeToMarkdown
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Hello World! {DateTimeOffset.Now}");

                //DI
                await createHostBuilder(args).RunConsoleAsync();
            }
            catch (Exception sww)
            {
                Console.Error.WriteLine($"SWW: {sww}");
            }
            finally
            {
                Console.WriteLine($"Goodbye World! {DateTimeOffset.Now}");
            }
        }

        // https://dfederm.com/building-a-console-app-with-.net-generic-host/
        // What options does .net generichost have out-of-the box?
        public static IHostBuilder createHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseContentRoot(System.IO.Path.GetDirectoryName(
                 System.Reflection.Assembly.GetExecutingAssembly().Location // becomes 'contentRoot' in config
            ))
            .ConfigureAppConfiguration((hostBuilderContext, configBuilder) =>
            {
                // configBuilder
                // .AddInMemoryCollection(initialData:new Dictionary<string,string>(){{"a","b"},{"c","d"}})
                // .AddJsonFile("appsettings.json", optional: false)
                // .AddCommandLine(args)
                ;

                //FIXME: do they collide? how to i enforce expected settings?
                var config = configBuilder.Build();
                System.Console.WriteLine(config.GetDebugView());
            })
            .ConfigureServices((hostBuilderContext, svc) =>
            {
                svc.AddLogging(ilb => ilb.SetMinimumLevel(LogLevel.Information));
                //svc.AddSingleton();
                svc.AddHostedService<RecipeWorker>();
            });
    }

    class RecipeWorker : IHostedService // MyServiceWorkerWhichExtendsIHostedServiceOrBackgroundService
    {
        public RecipeWorker(
            IHostApplicationLifetime appLifetime,
            IConfiguration configuration,
            Microsoft.Extensions.Logging.ILogger<RecipeWorker> logger
        )
        {
            this.logger = logger;
            this.config = configuration;
            this.app = appLifetime;
        }

        public ILogger<RecipeWorker> logger { get; init; }
        public IConfiguration config { get; }
        public IHostApplicationLifetime app { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // this is a great big mess. surely it can be simpler?
            app.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        // config.GetChildren().Select(section=> section.)
                        logger.LogWarning("PWD:"+Environment.CurrentDirectory);
                        this.Act();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Unhandled exception!");
                    }
                    finally
                    {
                        app.StopApplication();// Stop the application once the work is done

                        logger.LogWarning("PWD:"+Environment.CurrentDirectory);
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogWarning("die");
            return Task.CompletedTask;
        }

        void Act()
        {
            //TODO: something with ConfigurationBuilder https://www.daveabrock.com/2021/01/19/config-top-level-programs/
            //  https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/store-custom-information-config-file
            Func<string,bool> itemExists = (string path)=>System.IO.Directory.Exists(path) || System.IO.File.Exists(path);

            var rdir = System.IO.Path.GetFullPath(config.GetValue<string>("recipedir"));
            var rpat = System.IO.Path.GetFullPath(config.GetValue<string>("recipe") ?? System.IO.Path.Combine(rdir, "Tortellini.jsonc"));

            logger.LogInformation($"CntRt= {(itemExists(config.GetValue<string>("contentRoot"))? "❤":"💥")} {config.GetValue<string>("contentRoot")}");
            logger.LogInformation($"  PWD = {(true? "❤":"💥")} {Environment.CurrentDirectory}");
            logger.LogInformation($" RDIR = {(itemExists(rdir)? "❤":"💥")} {rdir}");
            logger.LogInformation($"RPATH = {(itemExists(rpat)? "❤":"💥")} {rpat}");

            //var recpath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("UserProfile"), "Projects", "Recipe-Framework", "src", "Assets", "Tortellini.jsonc");
            var recpath = System.IO.File.Exists(rpat) ? rpat : throw new System.IO.FileNotFoundException($"Need a recipe file, got '{rpat}'");
            using (System.IO.StreamReader fs = new(recpath))
            {
                var c = fs.ReadToEnd();
                var k = Newtonsoft.Json.JsonConvert.DeserializeObject<RecipeEntity.Recipe>(c);

                this.logger.LogDebug($"element1 | {k.Steps.ElementAt(1)}");int i=0;
                foreach (var ingredient in k.Steps.ElementAt(1).Ingredients[..3])
                {
                    logger.LogDebug("..."+i++);
                    this.logger.LogInformation(ingredient.ToString());
                }
            }
        }
    }
}
