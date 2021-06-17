using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 8)]
	public class Day_08 : BaseDay
	{
		public Day_08(ILogger<Day_08> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
            string[] lines = InputUtils.GetLines(RawInput);
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
