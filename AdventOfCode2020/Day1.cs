using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day1 : IRunner
    {
        public void Run(string[] inputs)
        {
            var arr = inputs.Select(r => int.Parse(r));

            Func<int, int> GetProduct = (layer) => arr
                .SelectMany(a => arr.Select(ar => layer > 1 ? new[] { a, ar } : new[] {a}) ) //create array by adding the next value
                .SelectMany(a => arr.Select(ar => layer > 2 ? a.Append(ar) : a)) //add element to the array
                .First(a => a.Sum() == 2020) // check the sum of all the elements of the array meets criteria
                .Aggregate((x, y) => x * y); // multiply each element
    
            Console.WriteLine($"Product for 2 numbers - expected: 319531 actual: {GetProduct(2)}"); 
            Console.WriteLine($"Product for 3 numbers - expected: 244300320 actual: {GetProduct(3)}");
        }
    }
}