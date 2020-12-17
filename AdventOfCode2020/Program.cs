using System;
using System.IO;
using System.Threading.Tasks;
using AdventOfCode2020;
using McMaster.Extensions.CommandLineUtils;

var app = new CommandLineApplication();

app.HelpOption();

// var optionDay = app.Option<int>("-d|--day <N>", "Day", CommandOptionType.SingleValue, (o) => o.IsRequired(true));
// optionDay.IsRequired(true, "");

var dayArg = app.Argument<int>("Day", "Accepted values are from 1 to 25").IsRequired(true);

app.OnExecuteAsync(async (cancellationToken) => 
{
    Console.WriteLine($"Requesting... Day{dayArg.ParsedValue}");

    Func<int, string> GetPath = (int day) => Path.Combine($"Inputs/Day{day}.txt",".");

    Func<string, Task<string[]>> GetInputs = (path) => File.Exists(path) ? File.ReadAllLinesAsync(path) : Task.FromResult(Array.Empty<string>());

    var inputs = await GetInputs(GetPath(dayArg.ParsedValue));

    IRunner runner = dayArg.ParsedValue switch 
    { 
        1 => new Day1(), 
        2 => new Day2(),
        4 => new Day4(),
        _ => new DummyRunner() 
    };

    runner.Run(inputs);
    return 0;
});

return await app.ExecuteAsync(args);