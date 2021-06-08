using dev.sthorne.AdventOfCode.Puzzles.Day;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace dev.sthorne.AdventOfCode.Puzzles
{
	public delegate IDay PuzzleServiceResolver(int year, int day);

	public class PuzzleServiceConfigurator
	{
		public readonly Dictionary<int, List<int>> PuzzleDates = new();

		public PuzzleServiceConfigurator(IServiceCollection serviceCollection)
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
					if (!PuzzleDates.ContainsKey(puzzleDateAttr.Year))
						PuzzleDates[puzzleDateAttr.Year] = new();

					PuzzleDates[puzzleDateAttr.Year].Add(puzzleDateAttr.Day);
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

		public List<int> GetYears()
		{
			return PuzzleDates.Keys.OrderBy(x => x).ToList();
		}

		public List<int> GetDays(int year)
		{
			return PuzzleDates[year].OrderBy(x => x).ToList();
		}

		public (int Year, int Day) GetLatestPuzzleDate()
		{
			return PuzzleDates.OrderByDescending(x => x.Key).Select(x => (x.Key, x.Value.OrderByDescending(x => x).FirstOrDefault())).FirstOrDefault();
		}
	}
}
