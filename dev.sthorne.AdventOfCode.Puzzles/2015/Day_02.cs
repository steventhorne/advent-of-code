using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 2)]
	public class Day_02 : BaseDay
	{
		class Dimensions
		{
			public int Length { get; set; }
			public int Width { get; set; }
			public int Height { get; set; }
		}

		private readonly List<Dimensions> Presents = new();

		public Day_02(ILogger<Day_02> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
            string[] lines = InputUtils.GetLines(RawInput);

			foreach (var line in lines)
			{
				string[] dimenions = line.Split("x", StringSplitOptions.RemoveEmptyEntries);

				Presents.Add(new Dimensions()
				{
					Length = int.Parse(dimenions[0]),
					Width = int.Parse(dimenions[1]),
					Height = int.Parse(dimenions[2])
				});
			}

			return Task.CompletedTask;
		}

		private Task<object> Puzzle_01()
		{
			int sqrFeet = 0;
			foreach (var present in Presents)
			{
				var side1 = present.Length * present.Width;
				var side2 = present.Width * present.Height;
				var side3 = present.Height * present.Length;
				sqrFeet += 2*side1 + 2*side2 + 2*side3 + Math.Min(side1, Math.Min(side2, side3));
			}
			return Task.FromResult((object)sqrFeet);
		}

		private Task<object> Puzzle_02()
		{
			int ribbonLength = 0;
			foreach (var present in Presents)
			{
				int wrapLength = 2 * present.Length
					+ 2 * present.Width
					+ 2 * present.Height
					- 2 * Math.Max(present.Length, Math.Max(present.Width, present.Height));
				ribbonLength += wrapLength + present.Length * present.Width * present.Height;
			}
			return Task.FromResult((object)ribbonLength);
		}
	}
}
