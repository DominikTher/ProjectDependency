using Microsoft.Extensions.DependencyInjection;
using ProjectDependecy.ConsoleApp.Composite;
using ProjectDependecy.ConsoleApp.Services;

namespace ProjectDependecy.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var serviceProvider = BuilServiceProvider();
            var application = serviceProvider.GetRequiredService<Application>();

            var solutionFilePath = args[0];
            application.Start(solutionFilePath);
        }

        private static ServiceProvider BuilServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IJsonService, JsonService>();
            serviceCollection.AddSingleton<IHtmlChartService, HtmlChartService>();
            serviceCollection.AddSingleton<ISolutionProjectClient, SolutionProjectClient>();
            serviceCollection.AddSingleton<Application>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
