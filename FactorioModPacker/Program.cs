using System.CommandLine;
using FactorioModPacker.Commands;

namespace FactorioModPacker;
class Program
{
    private static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Factorio Mod Packer");

        var pack = new PackCommand();
        rootCommand.AddCommand(pack.Command);

        await rootCommand.InvokeAsync(args);
    }
}