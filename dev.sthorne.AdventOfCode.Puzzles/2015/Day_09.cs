using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 9)]
	public class Day_09 : BaseDay
	{
		public Day_09(ILogger<Day_09> logger)
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
