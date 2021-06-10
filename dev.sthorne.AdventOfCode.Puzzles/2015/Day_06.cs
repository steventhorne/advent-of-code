using dev.sthorne.AdventOfCode.Puzzles.Day;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 6)]
	public class Day_06 : BaseDay
	{
		public Day_06(ILogger<Day_06> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
			return Task.CompletedTask;
		}

		private Task<object> Puzzle_01()
		{
			return Task.FromResult((object)null);
		}

		private Task<object> Puzzle_02()
		{
			return Task.FromResult((object)null);
		}
	}
}
