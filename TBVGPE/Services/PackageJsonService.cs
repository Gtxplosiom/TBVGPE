using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

public class PackageInfo
{
    [JsonPropertyName("appversion")]
    public string? AppVersion { get; set; }    
}

public class JsonVersionService
{
    private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    private readonly string _filePath = "package.json";

    public string? GetVersion()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine($"Error: The file at path '{_filePath}' was not found.");
            return null;
        }

        try
        {
            string jsonString = File.ReadAllText(_filePath);

            var packageInfo = JsonSerializer.Deserialize<PackageInfo>(jsonString);

            return packageInfo?.AppVersion;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the JSON file: {ex.Message}");
            return null;
        }
    }

    public void SetVersion(string filePath, string newVersion)
    {
        PackageInfo packageInfo;

        if (File.Exists(filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                packageInfo = JsonSerializer.Deserialize<PackageInfo>(jsonString) ?? new PackageInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not read existing JSON file. A new one will be created. Error: {ex.Message}");
                packageInfo = new PackageInfo();
            }
        }
        else
        {
            packageInfo = new PackageInfo();
        }

        packageInfo.AppVersion = newVersion;

        try
        {
            string updatedJson = JsonSerializer.Serialize(packageInfo, _options);

            File.WriteAllText(filePath, updatedJson);
            Console.WriteLine($"Successfully updated version to '{newVersion}' in '{filePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the JSON file: {ex.Message}");
        }
    }
}
