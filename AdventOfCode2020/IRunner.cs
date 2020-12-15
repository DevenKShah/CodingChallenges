using System;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public interface IRunner
    {
        void Run(string[] inputs);
    }

    public class DummyRunner : IRunner
    {
        public void Run(string[] inputs)
        {
            Console.WriteLine("The day is not available yet.");
        }
    }
}