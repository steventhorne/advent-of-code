using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 10)]
	public class Day_10 : BaseDay
	{
		private string Input;

		public Day_10(ILogger<Day_10> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
			Input = InputUtils.GetLines(RawInput)[0];
			return Task.CompletedTask;
		}

		private string GetLookAndSay(string input, int cycles)
		{
			StringBuilder output = new();

			for (int i = 0; i < cycles; ++i)
			{
				output.Clear();
				int count = 0;
				char cur = (char)0;
				foreach (var c in input)
				{
					if (c != cur && count > 0)
					{
						output.Append(count);
						output.Append(cur);
						count = 0;
					}

					cur = c;
					++count;
				}

				if (count > 0)
				{
					output.Append(count);
					output.Append(cur);
				}

				input = output.ToString();
			}

			return input;
		}

		private Task<object> Puzzle_01()
		{
			return Task.FromResult((object)GetLookAndSay(Input, 40).Length);
		}

		private Task<object> Puzzle_02()
		{
			return Task.FromResult((object)GetLookAndSay(Input, 50).Length);
		}
	}
}
