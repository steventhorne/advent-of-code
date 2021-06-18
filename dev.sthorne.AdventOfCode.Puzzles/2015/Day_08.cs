using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 8)]
	public class Day_08 : BaseDay
	{
        private string[] StringCodes;
		public Day_08(ILogger<Day_08> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
            StringCodes = InputUtils.GetLines(RawInput);
			return Task.CompletedTask;
		}

        private int GetParsedLength(string stringCode)
        {
            return Regex.Replace(stringCode[1..^1], @"(?:\\""|\\\\|\\x[a-f0-9]{2})", ".").Length;
        }

        private int GetEscapedLength(string stringCode)
        {
            int total = 2;
            foreach (var charCode in stringCode)
            {
                switch (charCode)
                {
                    case '"':
                        total += 2;
                        break;
                    case '\\':
                        total += 2;
                        break;
                    default:
                        ++total;
                        break;
                }
            }

            return total;
        }

        private Task<object> Puzzle_01()
        {
            int realTotal = 0,
                parsedTotal = 0;
            foreach (var stringCode in StringCodes)
            {
                realTotal += stringCode.Length;
                parsedTotal += GetParsedLength(stringCode);
            }
			return Task.FromResult((object)(realTotal - parsedTotal));
        }

		private Task<object> Puzzle_02()
		{
            int realTotal = 0,
                escapedTotal = 0;
            foreach (var stringCode in StringCodes)
            {
                realTotal += stringCode.Length;
                escapedTotal += GetEscapedLength(stringCode);
            }
            return Task.FromResult((object)(escapedTotal - realTotal));
		}
	}
}
