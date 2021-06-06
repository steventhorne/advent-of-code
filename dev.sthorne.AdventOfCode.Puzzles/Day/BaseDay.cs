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
		private readonly ILogger Logger;

		protected List<Func<Task<object>>> Puzzles;
		protected int Year;
		protected int Day;

		protected string RawInput;

		public BaseDay(ILogger logger)
		{
			Logger = logger;

			Puzzles = new List<Func<Task<object>>>();

			PuzzleDateAttribute pdAttr = GetType().GetCustomAttribute(typeof(PuzzleDateAttribute)) as PuzzleDateAttribute;
			if (pdAttr != null)
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

			Stopwatch sw = new Stopwatch();

			await ReadInput();
			solutionData.ReadInputDuration = RecordDuration(sw);

			await ProcessInput();
			solutionData.ProcessInputDuration = RecordDuration(sw);

			foreach (var puzzle in Puzzles)
			{
				sw.Start();

				var solution = await puzzle();

				solutionData.PuzzleSolutionData.Add(new()
				{
					Solution = solution,
					Duration = RecordDuration(sw)
				});
				sw.Reset();
			}

			return solutionData;
		}

		private TimeSpan RecordDuration(Stopwatch sw)
		{
			var ts = new TimeSpan(sw.Elapsed.Ticks);
			sw.Reset();
			return ts;
		}
	}
}
