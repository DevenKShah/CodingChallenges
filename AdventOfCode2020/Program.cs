using System;
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
    IRunner runner = dayArg.ParsedValue switch 
    { 
        1 => new Day1(), 
        _ => new DummyRunner() 
    };

    await runner.Run();
    return 0;
});

return await app.ExecuteAsync(args);