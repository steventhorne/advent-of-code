using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using dev.sthorne.AdventOfCode.Puzzles._2015;
using System;
using System.Threading.Tasks;

namespace dev.sthorne.AdventOfCode.Tests
{
	[TestClass]
	public class Year2015Tests
	{
        private static ILoggerFactory LogFactory;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            LogFactory = LoggerFactory.Create(builder => builder.AddConsole());
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            LogFactory.Dispose();
        }

        private static ILogger<T> GetLogger<T>()
        {
            return LogFactory.CreateLogger<T>();
        }

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
			var day = new Day_01(GetLogger<Day_01>());
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
			var day = new Day_02(GetLogger<Day_02>());
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
			var day = new Day_03(GetLogger<Day_03>());
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, "abcdef", 609043)]
		[DataRow(0, "pqrstuv", 1048970)]
		public async Task Day_04Tests(int index, string input, object expected)
		{
			var day = new Day_04(GetLogger<Day_04>());
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
			var day = new Day_05(GetLogger<Day_05>());
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, "turn on 0,0 through 999,999", 1000000)]
		[DataRow(0, "turn on 0,0 through 999,999\ntoggle 0,0 through 999,0", 999000)]
		[DataRow(0, "turn on 0,0 through 999,999\ntoggle 0,0 through 999,0\nturn off 499,499 through 500,500", 998996)]
		public async Task Day_06Tests(int index, string input, object expected)
		{
			var day = new Day_06(GetLogger<Day_06>());
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

        [TestMethod]
        [DataRow(0, @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> a
", (UInt16)65079)]
        public async Task Day_07Tests(int index, string input, object expected)
        {
            var day = new Day_07(GetLogger<Day_07>());
            var result = await day.Test(index, input);
            Assert.AreEqual(expected, result);
        }

		[TestMethod]
		[DataRow(0, @"""""", 2)]
		[DataRow(0, @"""abc""", 2)]
		[DataRow(0, @"""aaa\""aaa""", 3)]
		[DataRow(0, @"""\x27""", 5)]
		[DataRow(1, @"""""", 4)]
		[DataRow(1, @"""abc""", 4)]
		[DataRow(1, @"""aaa\""aaa""", 6)]
		[DataRow(1, @"""\x27""", 5)]
		public async Task Day_08Tests(int index, string input, object expected)
		{
			var day = new Day_08(GetLogger<Day_08>());
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		[DataRow(0, @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141", 605)]
		public async Task Day_09Tests(int index, string input, object expected)
		{
			var day = new Day_09(GetLogger<Day_09>());
			var result = await day.Test(index, input);
			Assert.AreEqual(expected, result);
		}
	}
}
