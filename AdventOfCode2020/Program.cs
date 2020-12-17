using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2020;
using McMaster.Extensions.CommandLineUtils;

var app = new CommandLineApplication();

app.HelpOption();


var dayArg = app.Argument<int>("Day", "Accepted values are from 1 to 25").IsRequired(true);

var runners = typeof(IRunner).Assembly.GetTypes().Where(a => typeof(IRunner).IsAssignableFrom(a) && a.IsInterface is not true);

app.OnExecuteAsync(async (cancellationToken) => 
{
    Console.WriteLine($"Requesting... Day{dayArg.ParsedValue}");

    Func<int, string> GetPath = (int day) => Path.Combine($"Inputs/Day{day}.txt",".");

    Func<string, Task<string[]>> GetInputs = (path) => File.Exists(path) ? File.ReadAllLinesAsync(path) : Task.FromResult(Array.Empty<string>());

    Func<string, IRunner> GetRunner = (name) => Activator.CreateInstance((runners.FirstOrDefault(r => r.Name == name) ?? typeof(DummyRunner))) as IRunner;

    var inputs = await GetInputs(GetPath(dayArg.ParsedValue));

    IRunner runner = GetRunner($"Day{dayArg.ParsedValue}");

    runner.Run(inputs);
    return 0;
});

return await app.ExecuteAsync(args);

