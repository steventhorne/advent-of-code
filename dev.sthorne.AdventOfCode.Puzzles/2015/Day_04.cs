using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 4)]
	public class Day_04 : BaseDay
	{
		private string Hash;

		public Day_04(ILogger<Day_04> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
			Hash = RawInput.Replace("\n", "").Replace("\r", "");

			return Task.CompletedTask;
		}

		private Task<object> Puzzle_01()
		{
            var md5 = MD5.Create();
            byte[] inputBytes;
            byte[] outputBytes;
            for (int i = 0; i < int.MaxValue; ++i)
            {
                inputBytes = Encoding.UTF8.GetBytes($"{Hash}{i}");
                outputBytes = md5.ComputeHash(inputBytes);

                bool valid = true;
                for (int j = 0; j < 3; ++j)
                {
                    if (outputBytes[j] != 0b00000000
                        && (j != 2 || outputBytes[j] >= 0b00010000))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                    return Task.FromResult((object)i);
            }
			return Task.FromResult((object)null);
		}

		private Task<object> Puzzle_02()
		{
            var md5 = MD5.Create();
            byte[] inputBytes;
            byte[] outputBytes;
            for (int i = 0; i < int.MaxValue; ++i)
            {
                inputBytes = Encoding.UTF8.GetBytes($"{Hash}{i}");
                outputBytes = md5.ComputeHash(inputBytes);
                bool valid = true;
                for (int j = 0; j < 3; ++j)
                {
                    if (outputBytes[j] != 0b00000000)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                    return Task.FromResult((object)i);
            }
			return Task.FromResult((object)null);
		}
	}
}
