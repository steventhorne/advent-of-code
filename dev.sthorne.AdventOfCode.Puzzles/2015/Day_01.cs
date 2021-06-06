using dev.sthorne.AdventOfCode.Puzzles.Day;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 1)]
	public class Day_01 : BaseDay
	{
		private readonly ILogger<Day_01> Logger;

		private string Directions;

		public Day_01(ILogger<Day_01> logger)
			: base(logger)
		{
			Logger = logger;

			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
			Directions = RawInput;
			return Task.CompletedTask;
		}

		private Task<object> Puzzle_01()
		{
			int floor = 0;
			foreach (var dir in Directions)
				floor = HandleDirection(floor, dir);

			return Task.FromResult((object) floor);
		}

		private Task<object> Puzzle_02()
		{
			int floor = 0;
			for (int i = 0; i < Directions.Length; ++i)
			{
				floor = HandleDirection(floor, Directions[i]);
				if (floor < 0)
					return Task.FromResult((object) (i + 1));
			}

			return Task.FromResult((object) -1);
		}

		private int HandleDirection(int floor, char dir)
		{
			switch (dir)
			{
				case '(':
					return ++floor;
				case ')':
					return --floor;
				default:
					return floor;
			}
		}
	}
}
