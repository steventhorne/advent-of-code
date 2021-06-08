using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 3)]
	public class Day_03 : BaseDay
	{
		private string Directions;

		public Day_03(ILogger<Day_03> logger)
			: base(logger)
		{
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
			// overestimate capacity for performance
			Grid<int> Houses = new(Directions.Length);

			int x = 0;
			int y = 0;
			++Houses[x, y];
			foreach (var dir in Directions)
			{
				(int newX, int newY) = ParseDirection(dir);
				x += newX;
				y += newY;

				++Houses[x, y];
			}

			return Task.FromResult((object)Houses.Count(x => x.Value > 0));
		}

		private Task<object> Puzzle_02()
		{
			// overestimate capacity for performance
			Grid<int> Houses = new(Directions.Length);

			int x1 = 0;
			int y1 = 0;
			int x2 = 0;
			int y2 = 0;
			++Houses[x1, y1];
			++Houses[x2, y2];

			for (int i = 0; i < Directions.Length; ++i)
			{
				(int newX, int newY) = ParseDirection(Directions[i]);
				x1 += newX;
				y1 += newY;

				++Houses[x1, y1];

				if (++i < Directions.Length)
				{
					(newX, newY) = ParseDirection(Directions[i]);
					x2 += newX;
					y2 += newY;

					++Houses[x2, y2];
				}
			}

			return Task.FromResult((object)Houses.Count(x => x.Value > 0));
		}

		/// <summary>
		/// Returns the change in x and y for the given direction.
		/// 
		/// Sacrifices some performance for code clarify.
		/// </summary>
		/// <param name="dir">The direction to move in.</param>
		/// <returns></returns>
		private (int newX, int newY) ParseDirection(char dir)
		{
			switch (dir)
			{
				case '^':
					return (0, 1);
				case '>':
					return (1, 0);
				case 'v':
					return (0, -1);
				case '<':
					return (-1, 0);
				default:
					return (0, 0);
			}
		}
	}
}
