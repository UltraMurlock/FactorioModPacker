using System.IO.Compression;

namespace FactorioModPacker;
public static class Archiver
{
    public static void PackMod(DirectoryInfo modDirectory, string modName, string modVersion)
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string archivePath = Path.Combine(appDataPath, "Factorio\\mods", $"{modName}_{modVersion}.zip");
        using FileStream stream = new FileStream(archivePath, FileMode.Create, FileAccess.Write);
        using ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create);
        AddDirectoryToArchive(archive, modDirectory, modName);
    }

    private static void AddDirectoryToArchive(ZipArchive archive, DirectoryInfo directory, string entryNameBase)
    {
        FileInfo[] files = directory.GetFiles();
        foreach(FileInfo file in files)
        {
            if(file.Name.StartsWith('.'))
                continue;
            
            AddFileToArchive(archive, file, entryNameBase);
        }
        
        DirectoryInfo[] subdirectories = directory.GetDirectories();
        foreach(DirectoryInfo subdirectory in subdirectories)
        {
            if(subdirectory.Name.StartsWith('.'))
                continue;

            string subEntryBaseName = $"{entryNameBase}/{subdirectory.Name}";
            AddDirectoryToArchive(archive, subdirectory, subEntryBaseName);
        }
    }

    private static void AddFileToArchive(ZipArchive archive, FileInfo file, string entryNameBase)
    {
        string entryName = $"{entryNameBase}/{file.Name}";
        archive.CreateEntryFromFile(file.FullName, entryName);
    }
}