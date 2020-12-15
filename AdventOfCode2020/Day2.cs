using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day2 : IRunner
    {
        public void Run(string[] inputs)
        {
            var firstPolicyCount = inputs.Select(GetPartsBySplittingString).Where(CheckFirstPolicy).Count();
            var SecondPolicyCount = inputs.Select(GetPartsUsingRegexGroups).Where(CheckSecondPolicy).Count();
            Console.WriteLine($"Count of correct passwords according to first policy {firstPolicyCount}");
            Console.WriteLine($"Count of correct passwords according to second policy {SecondPolicyCount}");
        }

        private (int lower, int higher, string letter, string password) GetPartsUsingRegexGroups(string input)
        {
            var match = Regex.Match(input, @"(\d+)-(\d+)\s(\w):\s(\w+)");
            return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value, match.Groups[4].Value);
        }

        private bool CheckSecondPolicy((int firstIndex, int secondIndex, string letter, string password) input)
        {
            var result = Regex.Matches(input.password, input.letter);
            var indexes = new [] {input.firstIndex - 1, input.secondIndex - 1};
            if (result.Any() == false) return false;
            var isValid = result.Where(r => indexes.Contains(r.Index)).Count() == 1;
            return isValid;
        }
        private (int lower, int higher, string letter, string password) GetPartsBySplittingString(string input)
        {
            var parts = input.Split(" ");
            var range = parts[0].Split("-");
            var letter = parts[1].Substring(0,1);
            return (int.Parse(range[0]), int.Parse(range[1]), letter, parts[2]);
        }
        private bool CheckFirstPolicy((int lower, int higher, string letter, string password) input)
        {
            var result = Regex.Matches(input.password, input.letter);
            return result.Count >= input.lower && result.Count <= input.higher;
        }
    }
}