using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Swm.Api.Business.Services;

namespace Swm.Api.Business
{
    // TODO: Add cached DI and/or in-memory based context to store users to improve performance

    class Program
    {
        /// <summary>
        /// Retrieves Environment Variables to set as Production or Development mode
        /// Though environment is just to show for best practice currently. But it does enable you to change the URI for the API
        /// Adds logging, configures the appsettings.json
        /// calls the userService to run the tasks for the questions
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Task</returns>
        static async Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine(environment);

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSimpleConsole(i =>
                {
                    i.ColorBehavior = LoggerColorBehavior.Enabled;
                    i.IncludeScopes = true;
                });
            });
            var logger = loggerFactory.CreateLogger<Program>();

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddEnvironmentVariables();

            var configurationRoot = configurationBuilder.Build();
            var app = configurationRoot.GetSection(nameof(AppConfig)).Get<AppConfig>();

            var host = CreateHostBuilder(args, app).Build();
            var userService = host.Services.GetRequiredService<ISwmApiService>();

            await RunQuestions(userService);

        }

        /// <summary>
        /// Build a host and register DI services.
        /// Adds SwmApiService to interface with the HTTP calls
        /// Adds a HttpClient
        /// Adds a logger
        /// </summary>
        /// <param name="args"></param>
        /// <param name="appConfig"></param>
        /// <returns>Host</returns>
        static IHostBuilder CreateHostBuilder(string[] args, AppConfig appConfig)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient("swm", config =>
                {
                    config.BaseAddress = appConfig.ApiUri;
                    config.DefaultRequestHeaders
                          .Accept
                          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });
                services.AddLogging();
                services.AddTransient<ISwmApiService, SwmApiService>();
                //services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(""));
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddSimpleConsole(options => options.IncludeScopes = true);
            })
            .UseConsoleLifetime();
        }

        /// <summary>
        /// Function to quickly run the questions + some of my own questions.
        /// I reccommend putting a breakpoint and then stepping through due to logging bloating the console a little bit.
        /// </summary>
        /// <param name="userService"></param>
        /// <returns>Task</returns>
        private static async Task RunQuestions(ISwmApiService userService)
        {
            WaitUserInput("Get Full Name by ID for ID=53");
            var question1_a = await userService.GetFullNameById(53);
            Console.WriteLine(question1_a);

            WaitUserInput("Get Full Name by ID for ID=62");
            Console.ReadKey();
            var question1_b = await userService.GetFullNameById(62);
            Console.WriteLine(question1_b);

            WaitUserInput("Get Full Name by ID for ID=31");
            var question1_c = await userService.GetFullNameById(31);
            Console.WriteLine(question1_c);

            WaitUserInput("Get Full Name by ID for ID=42");
            var question1_d = await userService.GetFullNameById(42);
            Console.WriteLine(question1_d);

            WaitUserInput("Get First Names by Age in CSV for age=23");
            var question2_a = await userService.GetFirstNamesByAgeCsv(23);
            Console.WriteLine(question2_a);

            WaitUserInput("Get First Names by Age in CSV for age=54");
            var question2_b = await userService.GetFirstNamesByAgeCsv(54);
            Console.WriteLine(question2_b);

            WaitUserInput("Get First Names by Age in CSV for age=66");
            var question2_c = await userService.GetFirstNamesByAgeCsv(66);
            Console.WriteLine(question2_c);

            WaitUserInput("Get First Names by Age in CSV for age=102");
            var question2_d = await userService.GetFirstNamesByAgeCsv(102);
            Console.WriteLine(question2_d);

            WaitUserInput("Get Gender Per Age Count");
            var question3 = (await userService.GetGenderCountPerAge()).AgePerGender.ToString();
            Console.WriteLine(question3);
            Console.WriteLine("Finished!");
        }

        private static void WaitUserInput(string message)
        {
            Console.WriteLine("Press any key to run next question...");
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}