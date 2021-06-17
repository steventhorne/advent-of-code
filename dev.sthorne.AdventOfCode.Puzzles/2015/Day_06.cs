using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 6)]
	public class Day_06 : BaseDay
	{
        enum InstructionType
        {
            Toggle = 0,
            On,
            Off
        }

        struct Instruction
        {
            public InstructionType Type { get; init; }
            public (int X, int Y) Start { get; init; }
            public (int X, int Y) End { get; init; }
        }

        private readonly Regex InstructionPattern;
        private readonly List<Instruction> Instructions;

		public Day_06(ILogger<Day_06> logger)
			: base(logger)
		{
			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);

            InstructionPattern = new(@"^(toggle|turn off|turn on)\s(\d+),(\d+)\sthrough\s(\d+),(\d+)$");
            Instructions = new();
		}

		protected override Task ProcessInput()
		{
            string[] lines = InputUtils.GetLines(RawInput);
            foreach (var line in lines)
            {
                Match match = InstructionPattern.Match(line);
                if (match.Success)
                {
                    var typeString = match.Groups[1].Value;
                    var startX = int.Parse(match.Groups[2].Value);
                    var startY = int.Parse(match.Groups[3].Value);
                    var endX = int.Parse(match.Groups[4].Value);
                    var endY = int.Parse(match.Groups[5].Value);

                    InstructionType type;
                    switch (typeString)
                    {
                        case "toggle":
                            type = InstructionType.Toggle;
                            break;
                        case "turn on":
                            type = InstructionType.On;
                            break;
                        case "turn off":
                            type = InstructionType.Off;
                            break;
                        default:
                            continue;
                    }

                    Instructions.Add(new()
                        {
                            Type = type,
                            Start = (startX, startY),
                            End = (endX, endY)
                        }
                    );
                }
            }

			return Task.CompletedTask;
		}

		private Task<object> Puzzle_01()
		{
            var lights = new bool[1000000];
            foreach (var instruction in Instructions)
            {
                for (int i = instruction.Start.X; i <= instruction.End.X; ++i)
                {
                    for (int j = instruction.Start.Y; j <= instruction.End.Y; ++j)
                    {
                        switch (instruction.Type)
                        {
                            case InstructionType.Toggle:
                                lights[i * 1000 + j] = !lights[i * 1000 + j];
                                break;
                            case InstructionType.On:
                                lights[i * 1000 + j] = true;
                                break;
                            case InstructionType.Off:
                                lights[i * 1000 + j] = false;
                                break;
                        }
                    }
                }
            }
			return Task.FromResult((object)lights.Count(x => x));
		}

		private Task<object> Puzzle_02()
		{
            var lights = new int[1000000];
            foreach (var instruction in Instructions)
            {
                for (int i = instruction.Start.X; i <= instruction.End.X; ++i)
                {
                    for (int j = instruction.Start.Y; j <= instruction.End.Y; ++j)
                    {
                        switch (instruction.Type)
                        {
                            case InstructionType.Toggle:
                                lights[i * 1000 + j] += 2;
                                break;
                            case InstructionType.On:
                                ++lights[i * 1000 + j];
                                break;
                            case InstructionType.Off:
                                if (lights[i * 1000 + j] > 0)
                                    --lights[i * 1000 + j];
                                break;
                        }
                    }
                }
            }
			return Task.FromResult((object)lights.Sum(x => x));
		}
	}
}
