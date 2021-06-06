using System;

namespace dev.sthorne.AdventOfCode.Puzzles.Day
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	sealed class PuzzleDateAttribute : Attribute
	{
		public readonly int Year;
		public readonly int Day;

		public PuzzleDateAttribute(int year, int day)
		{
			Year = year;
			Day = day;
		}
	}
}
