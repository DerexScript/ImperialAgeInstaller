using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Xml.Linq;


namespace ImperialAgeInstaller;

internal static class Utility {
    private static readonly HttpClient httpClient = new();

    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    public static void ExtractZipFile(string zipFilePath, string extractPath) {
        ZipFile.ExtractToDirectory(zipFilePath, extractPath);
    }

    public static async Task DownloadFileAsync(string fileUrl, string destinationPath, ProgressBar progressBar) {
        using(var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead)) {
            response.EnsureSuccessStatusCode();
            var totalBytes = response.Content.Headers.ContentLength??-1L;
            var canReportProgress = totalBytes!=-1;

            using(var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
            using(var httpStream = await response.Content.ReadAsStreamAsync()) {
                var buffer = new byte[8192];
                long totalRead = 0;
                int bytesRead;
                while((bytesRead=await httpStream.ReadAsync(buffer, 0, buffer.Length))>0) {
                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                    totalRead+=bytesRead;

                    if(canReportProgress) {
                        progressBar.Value=(int)((totalRead*100)/totalBytes);
                    }
                }
            }
        }
    }

    public static void HideComponents(Control.ControlCollection controls) {
        foreach(Control control in controls) {
            control.Visible=false;

        }
    }


    public static void EnsureLauncherSettings(string installDirectoryTextBoxText) {
        // Caminho do diretório e do arquivo
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string launcherDirectory = Path.Combine(appDataPath, "ClassicUOLauncher");
        string settingsFilePath = Path.Combine(launcherDirectory, "launcher_settings.xml");

        // Verifica se o diretório existe, se não cria o diretório
        if(!Directory.Exists(launcherDirectory)) {
            Directory.CreateDirectory(launcherDirectory);
        }

        // Verifica se o arquivo existe
        if(!File.Exists(settingsFilePath)) {
            // Cria o arquivo com o conteúdo padrão
            var defaultSettings = CreateDefaultSettingsXml(installDirectoryTextBoxText);
            defaultSettings.Save(settingsFilePath);
        } else {
            // Abre e verifica o arquivo existente
            var settingsXml = XDocument.Load(settingsFilePath);
            var profiles = settingsXml.Root?.Element("profiles");

            bool profileExists = false;

            if(profiles!=null) {
                foreach(var profile in profiles.Elements("profile")) {
                    if((string)profile.Attribute("server")=="190.2.72.35") {
                        profileExists=true;
                        break;
                    }
                }

                if(!profileExists) {
                    // Adiciona o novo profile se não existir
                    var newProfile = CreateProfileElement(installDirectoryTextBoxText);
                    profiles.Add(newProfile);
                    settingsXml.Save(settingsFilePath);
                }
            }
        }
    }

    private static XDocument CreateDefaultSettingsXml(string installDirectoryTextBoxText) {
        return new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement("settings",
                new XAttribute("close_on_launch", "False"),
                new XAttribute("use_preview_package", "True"),
                new XAttribute("auto_apply_updates", "True"),
                new XAttribute("last_profile_name", "Imperial Age"),
                new XAttribute("driver_type", "0"),
                new XElement("profiles",
                    CreateProfileElement(installDirectoryTextBoxText)
                )
            )
        );
    }

    private static XElement CreateProfileElement(string installDirectoryTextBoxText) {
        return new XElement("profile",
            new XAttribute("name", "Imperial Age"),
            new XAttribute("cuo_path", ""),
            new XAttribute("username", ""),
            new XAttribute("password", ""),
            new XAttribute("server", "190.2.72.35"),
            new XAttribute("port", "2593"),
            new XAttribute("charname", ""),
            new XAttribute("client_version", ""),
            new XAttribute("uo_protocol", "0"),
            new XAttribute("uopath", installDirectoryTextBoxText),
            new XAttribute("server_type", "0"),
            new XAttribute("last_server_index", "255"),
            new XAttribute("last_server_name", ""),
            new XAttribute("debug", "False"),
            new XAttribute("profiler", "False"),
            new XAttribute("save_account", "False"),
            new XAttribute("skip_login_screen", "False"),
            new XAttribute("autologin", "False"),
            new XAttribute("reconnect", "False"),
            new XAttribute("reconnect_time", "1000"),
            new XAttribute("has_music", "True"),
            new XAttribute("high_dpi", "False"),
            new XAttribute("use_verdata", "False"),
            new XAttribute("music_volume", "50"),
            new XAttribute("encryption_type", "0"),
            new XAttribute("force_driver", "0"),
            new XAttribute("packet_log", "False"),
            new XAttribute("args", ""),
            new XElement("plugins",
                new XElement("plugin",
                    new XAttribute("path", "Razor\\Razor.exe"),
                    new XAttribute("enabled", "True")
                )
            )
        );
    }
}

