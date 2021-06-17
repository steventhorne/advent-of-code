using dev.sthorne.AdventOfCode.Puzzles.Solution;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles.Day
{
	public abstract class BaseDay : IDay
	{
		protected readonly ILogger Logger;

		protected List<Func<Task<object>>> Puzzles;
		protected int Year;
		protected int Day;

		protected string RawInput;

		public BaseDay(ILogger logger)
		{
			Logger = logger;

			Puzzles = new List<Func<Task<object>>>();

			if (GetType().GetCustomAttribute(typeof(PuzzleDateAttribute)) is PuzzleDateAttribute pdAttr)
			{
				Year = pdAttr.Year;
				Day = pdAttr.Day;
			}
		}

		private async Task<string> ReadInput()
		{
			if (RawInput == null)
			{
				var fileName = GetType().Name;
				RawInput = await File.ReadAllTextAsync(Path.ChangeExtension(Path.Combine("Inputs", Year.ToString(), fileName), ".txt"));
			}

			return RawInput;
		}

		protected abstract Task ProcessInput();

		public async Task<DaySolutionData> Execute()
		{
			if (Puzzles == null || Puzzles.Count == 0)
			{
				Logger.LogError($"No solutions were provided for puzzle: {GetType().Name}.");
				return null;
			}

			DaySolutionData solutionData = new()
			{
				Year = Year,
				Day = Day
			};

			Stopwatch sw = new();
			sw.Start();

			await ReadInput();
			solutionData.ReadInputDuration = RecordDuration(sw);

			await ProcessInput();
			solutionData.ProcessInputDuration = RecordDuration(sw);

			foreach (var puzzle in Puzzles)
			{
				var solution = await puzzle();

				solutionData.PuzzleSolutionData.Add(new()
				{
					Solution = solution,
					Duration = RecordDuration(sw)
				});
			}

			return solutionData;
		}

		public async Task<object> Test(int puzzleIndex, string input)
		{
			if (Puzzles == null || Puzzles.Count == 0)
				throw new NullReferenceException("There are no puzzles available for the specified day.");

			if (puzzleIndex < 0 && puzzleIndex >= Puzzles.Count)
				throw new IndexOutOfRangeException();

			var puzzle = Puzzles[puzzleIndex];
			if (puzzle == null)
				throw new NullReferenceException("Puzzle at specified index is null.");

			RawInput = input;
			await ProcessInput();
			return await puzzle();
		}

		private static TimeSpan RecordDuration(Stopwatch sw)
		{
			var ts = new TimeSpan(sw.Elapsed.Ticks);
			sw.Restart();
			return ts;
		}
	}
}
