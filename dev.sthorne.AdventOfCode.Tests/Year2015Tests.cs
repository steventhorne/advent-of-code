using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using dev.sthorne.AdventOfCode.Puzzles._2015;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Tests
{
	[TestClass]
	public class Year2015Tests
	{
		[TestMethod]
		[DataRow(0, "(())", 0)]
		[DataRow(0, "()()", 0)]
		[DataRow(0, "(((", 3)]
		[DataRow(0, "(()(()(", 3)]
		[DataRow(0, "))(((((", 3)]
		[DataRow(0, "())", -1)]
		[DataRow(0, "))(", -1)]
		[DataRow(0, ")))", -3)]
		[DataRow(0, ")())())", -3)]
		[DataRow(1, ")", 1)]
		[DataRow(1, "()())", 5)]
		public async Task Day_01Tests(int index, string input, object expected)
		{
			var logger = new Mock<ILogger<Day_01>>();
			var day = new Day_01(logger.Object);
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, "2x3x4", 58)]
		[DataRow(0, "1x1x10", 43)]
		[DataRow(1, "2x3x4", 34)]
		[DataRow(1, "1x1x10", 14)]
		public async Task Day_02Tests(int index, string input, object expected)
		{
			var logger = new Mock<ILogger<Day_02>>();
			var day = new Day_02(logger.Object);
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, ">", 2)]
		[DataRow(0, "^>v<", 4)]
		[DataRow(0, "^v^v^v^v^v", 2)]
		[DataRow(1, "^v", 3)]
		[DataRow(1, "^>v<", 3)]
		[DataRow(1, "^v^v^v^v^v", 11)]
		public async Task Day_03Tests(int index, string input, object expected)
		{
			var logger = new Mock<ILogger<Day_03>>();
			var day = new Day_03(logger.Object);
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, "abcdef", 609043)]
		[DataRow(0, "pqrstuv", 1048970)]
		public async Task Day_04Tests(int index, string input, object expected)
		{
			var logger = new Mock<ILogger<Day_04>>();
			var day = new Day_04(logger.Object);
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, "ugknbfddgicrmopn", 1)]
		[DataRow(0, "aaa", 1)]
		[DataRow(0, "jchzalrnumimnmhp", 0)]
		[DataRow(0, "haegwjzuvuyypxyu", 0)]
		[DataRow(0, "dvszwmarrgswjxmb", 0)]
        [DataRow(1, "qjhvhtzxzqqjkmpb", 1)]
        [DataRow(1, "xxyxx", 1)]
        [DataRow(1, "uurcxstgmygtbstg", 0)]
        [DataRow(1, "ieodomkazucvgmuy", 0)]
		public async Task Day_05Tests(int index, string input, object expected)
		{
			var logger = new Mock<ILogger<Day_05>>();
			var day = new Day_05(logger.Object);
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, "turn on 0,0 through 999,999", 1000000)]
		[DataRow(0, "turn on 0,0 through 999,999\ntoggle 0,0 through 999,0", 999000)]
		[DataRow(0, "turn on 0,0 through 999,999\ntoggle 0,0 through 999,0\nturn off 499,499 through 500,500", 998996)]
		public async Task Day_06Tests(int index, string input, object expected)
		{
			var logger = new Mock<ILogger<Day_06>>();
			var day = new Day_06(logger.Object);
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}
	}
}
