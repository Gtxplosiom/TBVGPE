using NuGet.Versioning;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using TBVGPE.Services;

namespace TBVGPE.Views
{
    public partial class UpdaterWindow : Window
    {
        // diri ko nala ig mvvm ini kay para separate ngan ma reusable ko ini iba na projects
        // either that or i'm just too lazy for mvvm
        private readonly UpdateService _updateService;

        private string? _version;
        private string? _downloadUpdateUrl;

        public UpdaterWindow(string? version, string downloadLink)
        {
            _updateService = new UpdateService();

            _version = version;
            _downloadUpdateUrl = downloadLink;

            InitializeComponent();

            // check kun updated nalat
            // na code block para ini pag may update ha preemptive tapos pag open guds na kay kailangan
            // pa ig check update para matuhay 
            if (_version != "" && _downloadUpdateUrl != "")
            {
                if (_version.CompareTo(App.CurrentVersion) > 0)
                {
                    ResultText.Text = $"Update available: TBVGPEv{_version}";
                    UpdateBtn.IsEnabled = true;
                }
                else
                {
                    ResultText.Text = $"Already up to date: {App.CurrentVersion}";
                    UpdateBtn.IsEnabled = false;
                }
            }
            else
            {
                ResultText.Text = $"Already up to date: {App.CurrentVersion}";
                UpdateBtn.IsEnabled = false;
            }
        }

        // beri mesi, clean code later
        // as well as didto ha update service hahayss
        public async void CheckUpdate(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                ResultText.Text = "Checking for update...";

                var result = await _updateService.CheckForUpdateAsync();

                if (result != (null, null))
                {
                    var currentVersion = SemanticVersion.Parse(App.CurrentVersion);
                    if (result.version.CompareTo(currentVersion) == 0)
                    {
                        ResultText.Text = $"Already up to date: {currentVersion}";
                    }
                    else
                    {
                        _downloadUpdateUrl = result.downloadLink;

                        ResultText.Text = $"Update available: TBVGPEv{result.version}";

                        UpdateBtn.IsEnabled = true;
                    }
                }
                else
                {
                    ResultText.Text = "Failed fetching update";
                }
            }
        }

        public async void Update(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                await DownloadUpdate(_downloadUpdateUrl);
            }
        }

        public void Close(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                this.Close();
            }
        }

        private async Task DownloadUpdate(string url)
        {
            ResultText.Text = "Starting download...";

            using var client = new HttpClient();
            try
            {
                using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1;
                var bytesRead = 0L;
                var buffer = new byte[8192];
                var isMoreToRead = true;

                string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(url));

                await using var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                await using var contentStream = await response.Content.ReadAsStreamAsync();

                do
                {
                    var read = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    if (read == 0)
                    {
                        isMoreToRead = false;
                    }
                    else
                    {
                        await fileStream.WriteAsync(buffer, 0, read);
                        bytesRead += read;

                        if (totalBytes != -1)
                        {
                            var progress = (double)bytesRead / totalBytes * 100;
                            ResultText.Text = $"Downloading update... {progress:F2}% ({bytesRead} of {totalBytes} bytes)";
                        }
                        else
                        {
                            ResultText.Text = $"Downloading update... {bytesRead} bytes downloaded";
                        }
                    }
                } while (isMoreToRead);

                ResultText.Text = "Download complete!";

                // Now, launch the UpdateApplicator console application.
                LaunchUpdateApplicator(tempPath);

                // Optionally, close the updater window to let the new process take over.
                this.Close();
            }
            catch (HttpRequestException ex)
            {
                ResultText.Text = $"Download failed: {ex.Message}";
            }
            catch (Exception ex)
            {
                ResultText.Text = $"An unexpected error occurred: {ex.Message}";
            }
        }

        private void LaunchUpdateApplicator(string downloadPath)
        {
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string appDir = Path.GetDirectoryName(exePath)!;

            string targetDirectory = appDir;
            string exeName = Path.GetFileName(exePath);
            string applicatorPath = Path.GetFullPath(Path.Combine(appDir, "..", "Tools", "UpdateApplicator.exe"));

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = applicatorPath,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                // Add arguments safely
                processInfo.ArgumentList.Add(downloadPath);
                processInfo.ArgumentList.Add(targetDirectory);
                processInfo.ArgumentList.Add(exeName);
                processInfo.ArgumentList.Add(exeName);

                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch the update process: {ex.Message}", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
