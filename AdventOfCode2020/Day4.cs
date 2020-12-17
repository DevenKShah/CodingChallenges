using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4 : IRunner
    {
        public void Run(string[] inputs)
        {
            var seed = new List<Dictionary<string,string>>() {new Dictionary<string, string>()};
            var initialList = inputs
                .SelectMany(s => s.Split(' ')) //splits inline elements and creates a single big array
                .Aggregate(seed, (accumulator, currentValue) => 
                {
                    if(string.IsNullOrWhiteSpace(currentValue))
                        accumulator.Add(new Dictionary<string, string>());
                    else 
                    {
                        var j = accumulator.Last();
                        j.Add(currentValue.Split(':')[0], currentValue.Split(':')[1]);
                    }

                    return accumulator;
                })
                // keeping this separate from other validations to get a count of initial filter
                // could easily make it a part of IsValid function
                .Where(dic => dic.Count == 8 || (dic.Count == 7 && dic.ContainsKey("cid") == false));

            var initialCount = initialList.Count();
            Console.WriteLine($"Number of valid passports - Expected: 237 Actual: {initialCount}");

            //Validate each field in the dictionary
            var finalCount = initialList.Where(dic => dic.Keys.All(x => IsValid(x, dic[x]))).Count();
            Console.WriteLine($"Number of valid passports - Expected: 172 Actual: {finalCount}");
        }

        private bool IsValid(string key, string value) 
        {
            Func<string, int, int, bool> IsValidRange = (data, min, max) => 
            {
                var result = false;
                if(int.TryParse(data, out int value) is not true) return false;
                result = value >= min && value <= max;
                return result;
            };

            Func<string, bool> IsValidHeight = (data) => 
            {
                var pattern = @"^(\d+)(in|cm)$";
                var result = Regex.Match(data, pattern);
                if (result.Success is not true) return false;

                var unit = result.Groups[2].Value;
                var lowerRange = unit == "cm" ? 150 : 59;
                var upperRange = unit == "cm" ? 193 : 76;
                var isInRange = IsValidRange(result.Groups[1].Value, lowerRange, upperRange);
                return isInRange;
            };

            Func<string, bool> IsValidHairColor = (value) => Regex.IsMatch(value, @"^#[0-9a-f]{6}$");

            Func<string, bool> IsValidEyeColor = (value) => Regex.IsMatch(value, @"^amb|blu|brn|gry|grn|hzl|oth$");

            Func<string, bool> IsValidPassportNumber = (value) => Regex.IsMatch(value, @"^[0-9]{9}$");

            return 
                key switch
                {
                    "byr" => IsValidRange(value, 1920, 2002),
                    "iyr" => IsValidRange(value, 2010, 2020),
                    "eyr" => IsValidRange(value, 2020, 2030),
                    "hgt" => IsValidHeight(value),
                    "hcl" => IsValidHairColor(value),
                    "ecl" => IsValidEyeColor(value),
                    "pid" => IsValidPassportNumber(value),
                    _ => true
                };
        }
    }
}