using System.CommandLine;
using System.Text.Json;

namespace FactorioModPacker.Commands;
public class PackCommand
{
    private readonly Option<DirectoryInfo> _modDirectoryOption = new("--from", "The path to under development mod folder.");
    
    public Command Command { get; }

    public PackCommand()
    {
        Command = new Command("pack", "Pack the mod files into a zip and place it in %AppData%/Roaming/Factorio/mods.")
        {
            _modDirectoryOption
        };
        
        Command.SetHandler(directory =>
        {
            if(!directory.Exists)
            {
                Console.WriteLine("The directory doesn't exist.");
                return;
            }
            
            string infoPath = Path.Combine(directory.FullName, "info.json");
            JsonDocument jsonDocument;
            try
            {
                jsonDocument = JsonDocument.Parse(File.ReadAllText(infoPath));
            }
            catch
            {
                Console.WriteLine("The file info.json could not be read.");
                return;
            }
            
            string modName = jsonDocument.RootElement.GetProperty("name").GetString()!;
            string modVersion = jsonDocument.RootElement.GetProperty("version").GetString()!;

            try
            {
                Archiver.PackMod(directory, modName, modVersion);
                Console.WriteLine("Success!");
            }
            catch
            {
                Console.WriteLine("The archive could not be packed.");
            }
        }, _modDirectoryOption);
    }
}