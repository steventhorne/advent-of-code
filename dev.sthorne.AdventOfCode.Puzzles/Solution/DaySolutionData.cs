﻿using System;
using System.Collections.Generic;

namespace dev.sthorne.AdventOfCode.Puzzles.Solution
{
	public class DaySolutionData
	{
		public int Year { get; set; }
		public int Day { get; set; }
		public TimeSpan ReadInputDuration { get; set; }
		public TimeSpan ProcessInputDuration { get; set; }
		public TimeSpan TotalDuration
		{
			get
			{
				var total = ReadInputDuration + ProcessInputDuration;
				if (PuzzleSolutionData != null)
				{
					foreach (var data in PuzzleSolutionData)
						total += data.Duration;
				}
				return total;
			}
		}
		public List<PuzzleSolutionData> PuzzleSolutionData { get; set; }

		public DaySolutionData()
		{
			PuzzleSolutionData = new();
		}

		public override string ToString()
		{
			var repr = @$"{Year}-{Day.ToString("00")}:
	Read Input Duration: {ReadInputDuration.TotalMilliseconds}
	Process Input Duration: {ProcessInputDuration.TotalMilliseconds}";

			for (int i = 0; i < PuzzleSolutionData.Count; i++)
			{
				var data = PuzzleSolutionData[i];
				repr += @$"
	Puzzle {i + 1}:
		Solution: {data.Solution}
		Duration: {data.Duration.TotalMilliseconds}
";
			}

			repr += @$"
	Total Duration: {TotalDuration.TotalMilliseconds}";

			return repr;
		}
	}
}
