using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 9)]
	public class Day_09 : BaseDay
	{
		private readonly Regex InputRegex;
		private readonly Dictionary<(string, string), int> Distances;
		private readonly HashSet<string> Locations;

		public Day_09(ILogger<Day_09> logger)
			: base(logger)
		{
			InputRegex = new(@"^([A-Za-z]+) to ([A-Za-z]+) = (\d+)$");
			Distances = new();
			Locations = new();

			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
            string[] lines = InputUtils.GetLines(RawInput);
			foreach (var line in lines)
			{
				var match = InputRegex.Match(line);
				if (match.Success)
				{
					Distances.Add((match.Groups[1].Value, match.Groups[2].Value), int.Parse(match.Groups[3].Value));
					Distances.Add((match.Groups[2].Value, match.Groups[1].Value), int.Parse(match.Groups[3].Value));
					Locations.Add(match.Groups[1].Value);
					Locations.Add(match.Groups[2].Value);
				}
			}
			return Task.CompletedTask;
		}

		private int GetDistance(Dictionary<(string, string), int> distances, HashSet<string> locations, Func<int, int, bool> comparer)
		{
			int? best = null;
			foreach (var permutation in Locations.Permute())
			{
				int current = 0;
				string previous = null;
				foreach (var location in permutation)
				{
					if (previous != null)
						current += Distances[(location, previous)];
					previous = location;
				}

				if (!best.HasValue || comparer(best.Value, current))
					best = current;
			}

			return best.GetValueOrDefault();
		}

        private Task<object> Puzzle_01()
        {
			var shortest = GetDistance(Distances, Locations, (best, current) => current < best);

			return Task.FromResult((object)shortest);
        }

		private Task<object> Puzzle_02()
		{
			var longest = GetDistance(Distances, Locations, (best, current) => current > best);

			return Task.FromResult((object)longest);
		}
	}
}
