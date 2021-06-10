using System;

namespace dev.sthorne.AdventOfCode.Utils
{
    public static class InputUtils
    {
        public static string[] GetLines(string input)
        {
            return input.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
        }
    }
}
