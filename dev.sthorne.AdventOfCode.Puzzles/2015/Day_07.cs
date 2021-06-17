using dev.sthorne.AdventOfCode.Puzzles.Day;
using dev.sthorne.AdventOfCode.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dev.sthorne.AdventOfCode.Puzzles._2015
{
	[PuzzleDate(2015, 7)]
	public class Day_07 : BaseDay
	{
        enum InstructionType
        {
            Value = 0,
            Not,
            And,
            Or,
            Lshift,
            Rshift
        }

        class Instruction
        {
            public InstructionType Type { get; set; }
            public UInt16? Value1 { get; set; }
            public UInt16? Value2 { get; set; }
            public string Wire1 { get; set; }
            public string Wire2 { get; set; }
            public string Output { get; set; }
        }

        class Wire
        {
            private Func<UInt16> WireFunc;
            private UInt16? CachedResult;

            public Wire(Func<UInt16> wireFunc)
            {
                WireFunc = wireFunc;
            }

            public UInt16 GetValue()
            {
                if (!CachedResult.HasValue)
                    CachedResult = WireFunc();
                return CachedResult.Value;
            }
        }

        private readonly List<Instruction> Instructions;
        private readonly Regex InstructionPattern;

		public Day_07(ILogger<Day_07> logger)
			: base(logger)
		{
            Instructions = new();
            InstructionPattern = new(@"^(?:([a-z]+|\d+)\s)?(?:([A-Z]+)\s)?([a-z]+|\d+)\s->\s([a-z]+)$");

			Puzzles.Add(Puzzle_01);
			Puzzles.Add(Puzzle_02);
		}

		protected override Task ProcessInput()
		{
            string[] lines = InputUtils.GetLines(RawInput);
            foreach (string line in lines)
            {
                var match = InstructionPattern.Match(line);
                if (match.Success)
                {
                    UInt16 v;
                    Instruction i = new()
                    {
                        Output = match.Groups[4].Value
                    };

                    if (!UInt16.TryParse(match.Groups[1].Value, out v))
                        i.Wire1 = match.Groups[1].Value;
                    else
                        i.Value1 = v;

                    if (!UInt16.TryParse(match.Groups[3].Value, out v))
                        i.Wire2 = match.Groups[3].Value;
                    else
                        i.Value2 = v;

                    switch (match.Groups[2].Value)
                    {
                        case "":
                            i.Type = InstructionType.Value;
                            break;
                        case "NOT":
                            i.Type = InstructionType.Not;
                            break;
                        case "AND":
                            i.Type = InstructionType.And;
                            break;
                        case "OR":
                            i.Type = InstructionType.Or;
                            break;
                        case "LSHIFT":
                            i.Type = InstructionType.Lshift;
                            break;
                        case "RSHIFT":
                            i.Type = InstructionType.Rshift;
                            break;
                        default:
                            continue;
                    }
                    Instructions.Add(i);
                }
            }
			return Task.CompletedTask;
		}

        private Dictionary<string, Wire> SetupWires(List<Instruction> instructions)
        {
            Dictionary<string, Wire> wires = new();

            foreach (var i in Instructions)
            {
                switch (i.Type)
                {
                    case InstructionType.Value:
                        wires[i.Output] = new Func<string, UInt16?, Wire>(
                            (wire1, value1) =>
                            {
                                return new Wire(() => 
                                {
                                    return value1 ?? wires[wire1].GetValue();
                                });
                            }
                        )(i.Wire2, i.Value2);
                        break;
                    case InstructionType.Not:
                        wires[i.Output] = new Func<string, Wire>(
                            (wire) =>
                            {
                                return new Wire(() => {
                                    return (UInt16)(~wires[wire].GetValue());
                                });
                            }
                        )(i.Wire2);
                        break;
                    case InstructionType.And:
                        wires[i.Output] = new Func<string, UInt16?, string, UInt16?, Wire>(
                            (wire1, value1, wire2, value2) =>
                            {
                                return new Wire(() => {
                                    var w1 = value1 ?? wires[wire1].GetValue();
                                    var w2 = value2 ?? wires[wire2].GetValue();

                                    return (UInt16) (w1 & w2);
                                });
                            }
                        )(i.Wire1, i.Value1, i.Wire2, i.Value2);
                        break;
                    case InstructionType.Or:
                        wires[i.Output] = new Func<string, UInt16?, string, UInt16?, Wire>(
                            (wire1, value1, wire2, value2) =>
                            {
                                return new Wire(() => {
                                    var w1 = value1 ?? wires[wire1].GetValue();
                                    var w2 = value2 ?? wires[wire2].GetValue();

                                    return (UInt16) (w1 | w2);
                                });
                            }
                        )(i.Wire1, i.Value1, i.Wire2, i.Value2);
                        break;
                    case InstructionType.Lshift:
                        wires[i.Output] = new Func<string, UInt16?, UInt16?, Wire>(
                            (wire1, value1, value2) =>
                            {
                                return new Wire(() => {
                                    var w1 = value1 ?? wires[wire1].GetValue();

                                    return (UInt16) (w1 << value2.Value);
                                });
                            }
                        )(i.Wire1, i.Value1, i.Value2);
                        break;
                    case InstructionType.Rshift:
                        wires[i.Output] = new Func<string, UInt16?, UInt16?, Wire>(
                            (wire1, value1, value2) =>
                            {
                                return new Wire(() => {
                                    var w1 = value1 ?? wires[wire1].GetValue();

                                    return (UInt16) (w1 >> value2.Value);
                                });
                            }
                        )(i.Wire1, i.Value1, i.Value2);
                        break;
                    default:
                        break;
                }
            }

            return wires;
		}

        private Task<object> Puzzle_01()
        {
            var wires = SetupWires(Instructions);
			return Task.FromResult((object)wires["a"].GetValue());
        }

		private Task<object> Puzzle_02()
		{
            const UInt16 B_OVERRIDE = 3176;
            foreach (var i in Instructions)
            {
                if (i.Output == "b")
                {
                    i.Value2 = B_OVERRIDE;
                    break;
                }
            }
            var wires = SetupWires(Instructions);
			return Task.FromResult((object)wires["a"].GetValue());
		}
	}
}
