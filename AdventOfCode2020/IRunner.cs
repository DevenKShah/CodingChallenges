using System;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public interface IRunner
    {
        Task Run();
    }

    public class DummyRunner : IRunner
    {
        public Task Run()
        {
            Console.WriteLine("The day is not available yet.");
            return Task.CompletedTask;
        }
    }
}