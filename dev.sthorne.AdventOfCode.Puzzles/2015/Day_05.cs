using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 5)]
	public class Day_05 : BaseDay
	{
        private string[] Inputs;
		public Day_05(ILogger<Day_05> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
            Inputs = InputUtils.GetLines(RawInput);

			return Task.CompletedTask;
		}

		private Task<object> Puzzle_01()
		{
            var count = 0;
            foreach (var input in Inputs)
                count += IsNice_01(input) ? 1 : 0;
			return Task.FromResult((object)count);
		}

		private Task<object> Puzzle_02()
		{
            var count = 0;
            foreach (var input in Inputs)
                count += IsNice_02(input) ? 1 : 0;
			return Task.FromResult((object)count);
		}

        private bool IsNice_01(string input)
        {
            int vowelCount = 0;
            bool dbl = false;
            for (int i = 0; i < input.Length; ++i)
            {
                if (i > 0 && input[i - 1] == input[i])
                    dbl = true;

                switch (input[i])
                {
                    case 'b':
                        if (i > 0 && input[i - 1] == 'a')
                            return false;
                        break;
                    case 'd':
                        if (i > 0 && input[i - 1] == 'c')
                            return false;
                        break;
                    case 'q':
                        if (i > 0 && input[i - 1] == 'p')
                            return false;
                        break;
                    case 'y':
                        if (i > 0 && input[i - 1] == 'x')
                            return false;
                        break;
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        ++vowelCount;
                        break;
                    default:
                        break;
                }
            }

            if (vowelCount >= 3)
                return dbl;

            return false;
        }

        private bool IsNice_02(string input)
        {
            bool dblDbl = false;
            bool dblSkip = false;

            for (int i = 0; i < input.Length; ++i)
            {
                if (!dblSkip && i > 1 && input[i] == input[i - 2])
                    dblSkip = true;

                if (!dblDbl && i > 2)
                {
                    for (int j = 0; j < i - 2; ++j)
                    {
                        if (input[j] == input[i - 1]
                            && input[j + 1] == input[i])
                        {
                            dblDbl = true;
                            break;
                        }
                    }
                }
            }

            return dblDbl && dblSkip;
        }
	}
}
