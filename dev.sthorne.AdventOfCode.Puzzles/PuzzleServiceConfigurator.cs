using dev.sthorne.AdventOfCode.Puzzles.Day;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace dev.sthorne.AdventOfCode.Puzzles
{
	public delegate IDay PuzzleServiceResolver(int year, int day);

	public static class PuzzleServiceConfigurator
	{
		public static void ConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
		{
			Dictionary<(int, int), Type> serviceDictionary = new Dictionary<(int, int), Type>();

			var idayType = typeof(IDay);
			var baseDayType = typeof(BaseDay);
			var dayTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => baseDayType.IsAssignableFrom(x) && x != baseDayType);

			foreach (var dayType in dayTypes)
			{
				PuzzleDateAttribute puzzleDateAttr = dayType.GetCustomAttribute(typeof(PuzzleDateAttribute)) as PuzzleDateAttribute;
				if (puzzleDateAttr != null)
				{
					serviceDictionary.Add((puzzleDateAttr.Year, puzzleDateAttr.Day), dayType);
					serviceCollection.AddTransient(dayType);
				}
			}

			serviceCollection.AddTransient<PuzzleServiceResolver>(serviceProvider => (year, day) =>
			{
				if (!serviceDictionary.TryGetValue((year, day), out Type dayType))
					return null;

				return serviceProvider.GetService(dayType) as IDay;
			});
		}
	}
}
