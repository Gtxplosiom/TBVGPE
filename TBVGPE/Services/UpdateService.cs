using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using NuGet.Versioning;

namespace TBVGPE.Services
{
    public class UpdateService
    {
        public const string UpdateUrl = "https://gtxplosiom.github.io/gtxplosiom-releases/TBVGPE-latest.json";
        private readonly HttpClient _httpClient = new HttpClient();

        public UpdateService()
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("TBVGPE-Update-Checker");
        }

        public async Task<(SemanticVersion? version, string? downloadLink)> CheckForUpdateAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(UpdateUrl);
                var releaseInfo = JsonSerializer.Deserialize<LatestRelease>(response);

                if (releaseInfo != null && !string.IsNullOrEmpty(releaseInfo.LatestVersion))
                {
                    if (SemanticVersion.TryParse(releaseInfo.LatestVersion, out var latestVersion) && SemanticVersion.TryParse(App.CurrentVersion, out var currentVersion))
                    {
                        if (latestVersion > currentVersion)
                        {
                            return (latestVersion, releaseInfo.DownloadLink);
                        }
                        else
                        {
                            return (currentVersion, releaseInfo.DownloadLink);
                        }
                    }
                    else
                    {
                        Debug.WriteLine(App.CurrentVersion);
                        Debug.WriteLine("Error Parsing");
                    }
                }

                return (null, null);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error checking for updates: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return (null, null);
        }

        public class LatestRelease
        {
            [JsonPropertyName("latestversion")]
            public string? LatestVersion { get; set; }

            [JsonPropertyName("downloadlink")]
            public string? DownloadLink { get; set; }
        }
    }
}