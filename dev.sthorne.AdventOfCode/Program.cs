using dev.sthorne.AdventOfCode.Puzzles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			var manager = host.Services.GetService<PuzzleServiceConfigurator>();
			var resolver = host.Services.GetService<PuzzleServiceResolver>();

			var puzzleDate = manager.GetLatestPuzzleDate();
			var puzzle = resolver(puzzleDate.Year, puzzleDate.Day);

			Console.WriteLine(await puzzle.Execute());
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host
				.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((context, configuration) =>
				{
					IHostEnvironment env = context.HostingEnvironment;

					var secrets = new ConfigurationBuilder()
						.AddUserSecrets<Program>()
						.Build();

					var environmentOverride = secrets.GetValue<string>("Overrides:Environment");

					configuration.Sources.Clear();

					configuration
						.AddEnvironmentVariables(prefix: "AoC_")
						.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
						.AddJsonFile("appsettings.Environment.json", optional: true, reloadOnChange: true);

					if (!string.IsNullOrWhiteSpace(environmentOverride))
						configuration.AddJsonFile($"appsettings.{environmentOverride}.json", optional: false, reloadOnChange: true);

					if (env.IsDevelopment())
						configuration.AddUserSecrets<Program>();
				})
				.ConfigureServices((context, services) =>
				{
					// var conn = context.Configuration.GetConnectionString("DB");
					// services.AddDbContext<Context>(options =>
					//  options.UseSql(conn))

					PuzzleServiceConfigurator configurator = new(services);
					services.AddSingleton(configurator);
				})
				.ConfigureLogging((context, logging) =>
				{
					logging.AddConsole();
				})
				.UseConsoleLifetime();
		}
	}
}
