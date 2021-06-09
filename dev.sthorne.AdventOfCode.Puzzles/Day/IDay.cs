using dev.sthorne.AdventOfCode.Puzzles.Solution;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Puzzles.Day
{
	public interface IDay
	{
		Task<DaySolutionData> Execute();
		Task<object> Test(int puzzleIndex, string input);
	}
}
