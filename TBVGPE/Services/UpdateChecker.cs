using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace TBVGPE.Services
{
    public class UpdateChecker
    {
        // GitHub API URL for the latest release
        private const string GitHubApiUrl = "https://api.github.com/repos/username/TBVGPE/releases/latest";
        private readonly string _updaterExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater.exe");

        public async Task CheckForUpdatesAsync()
        {
            try
            {
                using var http = new HttpClient();
                http.DefaultRequestHeaders.UserAgent.ParseAdd("TBVGPE-App"); // GitHub API requires User-Agent

                string json = await http.GetStringAsync(GitHubApiUrl);
                var release = JsonSerializer.Deserialize<GitHubRelease>(json);

                Version current = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                Version latest = new Version(release.TagName.TrimStart('v'));

                if (latest > current)
                {
                    MessageBox.Show($"New version {latest} available. Updating...");

                    // Download the first asset (assumes zip file)
                    var asset = release.Assets[0];
                    string tempPath = Path.Combine(Path.GetTempPath(), asset.Name);

                    using var stream = await http.GetStreamAsync(asset.BrowserDownloadUrl);
                    using var fileStream = File.Create(tempPath);
                    await stream.CopyToAsync(fileStream);

                    // Launch Updater.exe
                    Process.Start(_updaterExePath, $"\"{tempPath}\" \"{AppDomain.CurrentDomain.BaseDirectory}\" \"{Process.GetCurrentProcess().Id}\"");

                    // Close the app to allow updater to replace files
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update check failed: " + ex.Message);
            }
        }

        // Classes to deserialize GitHub API JSON
        private class GitHubRelease
        {
            public string TagName { get; set; }
            public GitHubAsset[] Assets { get; set; }
        }

        private class GitHubAsset
        {
            public string Name { get; set; }
            public string BrowserDownloadUrl { get; set; }
        }
    }
}
