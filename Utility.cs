using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using IWshRuntimeLibrary;

namespace ImperialAgeInstaller;

internal static class Utility {


    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    public static async Task ExtractZipFileAsync(string zipFilePath, string extractPath) {
        if(!Directory.Exists(extractPath)) {
            Directory.CreateDirectory(extractPath);
        }
        await Task.Run(() => {
            ZipFile.ExtractToDirectory(zipFilePath, extractPath, overwriteFiles: true);
        });
    }

    public static async Task ExtractZipWithProgressAsync(string zipFilePath, string extractPath, ProgressBar progressBar) {
        if(!Directory.Exists(extractPath)) {
            Directory.CreateDirectory(extractPath);
        }
        using ZipArchive archive = ZipFile.OpenRead(zipFilePath);
        int totalEntries = archive.Entries.Count;
        progressBar.Invoke((Action)(() => {
            progressBar.Maximum=totalEntries;
            progressBar.Value=0;
        }));
        foreach(ZipArchiveEntry entry in archive.Entries) {
            string destinationPath = Path.Combine(extractPath, entry.FullName);
            if(entry.Name=="") {
                Directory.CreateDirectory(destinationPath);
            } else {
                string? dirPath = Path.GetDirectoryName(destinationPath);
                if(!String.IsNullOrEmpty(dirPath)) {
                    Directory.CreateDirectory(dirPath);
                }
                await Task.Run(() => entry.ExtractToFile(destinationPath, overwrite: true));
            }
            progressBar.Invoke((Action)(() => progressBar.Value++));
            //progressBar.Value++;
        }
    }

    public static async Task DownloadFileAsync(string fileUrl, string destinationPath, ProgressBar progressBar, string downloadDirectory) {
        if(!Directory.Exists(downloadDirectory)) {
            Directory.CreateDirectory(downloadDirectory);
        }
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback=(HttpRequestMessage, cert, chain, sslPolicyErrors) => true };
        using var httpClient = new HttpClient(handler);
        using var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        var totalBytes = response.Content.Headers.ContentLength??-1L;
        var canReportProgress = totalBytes!=-1;

        using var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);
        using var httpStream = await response.Content.ReadAsStreamAsync();
        var buffer = new byte[8192];
        long totalRead = 0;
        int bytesRead;
        while((bytesRead=await httpStream.ReadAsync(buffer, 0, buffer.Length))>0) {
            await fileStream.WriteAsync(buffer, 0, bytesRead);
            totalRead+=bytesRead;
            if(canReportProgress) {
                int progressPercentage = (int)((totalRead*100)/totalBytes);
                progressBar.Invoke((Action)(() => progressBar.Value=progressPercentage));
            }
        }
    }

    public static void HideComponents(Control.ControlCollection controls) {
        foreach(Control control in controls) {
            control.Visible=false;
        }
    }

    public static async Task EnsureLauncherSettings(string installDirectoryTextBoxText) {
        await Task.Run(() => {
            // Caminho do diretório e do arquivo
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string launcherDirectory = Path.Combine(appDataPath, "ClassicUOLauncher");
            string settingsFilePath = Path.Combine(launcherDirectory, "launcher_settings.xml");

            // Verifica se o diretório existe, se não cria o diretório
            if(!Directory.Exists(launcherDirectory)) {
                Directory.CreateDirectory(launcherDirectory);
            }

            // Verifica se o arquivo existe
            if(!System.IO.File.Exists(settingsFilePath)) {
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
                        if(((string?)profile.Attribute("server")??"")=="190.2.72.35") {
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
        });
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
                ),
                new XElement("plugin",
                    new XAttribute("path", "ClassicAssist\\ClassicAssist.dll"),
                    new XAttribute("enabled", "True")
                )
            )
        );
    }

    private static void SetShortcutToRunAsAdmin(string shortcutPath) {
        string xmlContent = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
    <application xmlns=""urn:schemas-microsoft-com:compatibility.v1"">
        <applicationCompatibility xmlns=""urn:schemas-microsoft-com:compatibility.v1"">
            <compatibility xmlns=""urn:schemas-microsoft-com:compatibility.v1"">
                <application>
                    <executable>
                        <file>{shortcutPath}</file>
                        <runAsAdmin>true</runAsAdmin>
                    </executable>
                </application>
            </compatibility>
        </applicationCompatibility>
    </application>";

        string appCompatPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\Application Compatibility", "appcompat.xml");
        string? directory = Path.GetDirectoryName(appCompatPath);
        if(!Directory.Exists(directory)&&!String.IsNullOrEmpty(directory)) {
            Directory.CreateDirectory(directory);
        }
        System.IO.File.WriteAllText(appCompatPath, xmlContent);
    }

    private static void SetShortcutToRunAsAdmin1(string shortcutPath) {
        // Comando PowerShell para ativar "Executar como administrador" no atalho
        string powershellScript = $@"
    $shortcut = [System.IO.FileInfo]::new('{shortcutPath}')
    $shell = New-Object -ComObject WScript.Shell
    $link = $shell.CreateShortcut($shortcut.FullName)
    $link.TargetPath = '{shortcutPath}'
    $link.Save()
    $link.ShortcutFlags = 8
    ";

        // Executa o script PowerShell
        Process.Start(new ProcessStartInfo {
            FileName="powershell",
            Arguments=$"-Command \"{powershellScript}\"",
            Verb="runas",
            UseShellExecute=true
        });
    }

    public static async Task CreateShortcut(string installDirectoryTextBoxText) {
        await Task.Run(() => {
            try {
                string targetPath = Path.Combine(installDirectoryTextBoxText, "ClassicUOLauncher.exe");
                string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ClassicUOLauncher.lnk");

                // Inicializa a instância do Windows Script Host para criar o atalho
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

                // Define o caminho do executável
                shortcut.TargetPath=targetPath;

                // Define o caminho de trabalho e o ícone para o executável
                shortcut.WorkingDirectory=installDirectoryTextBoxText;
                shortcut.IconLocation=targetPath;

                // Salva o atalho
                shortcut.Save();


                using var fs = new FileStream(shortcutPath, FileMode.Open, FileAccess.ReadWrite);
                fs.Seek(21, SeekOrigin.Begin);
                fs.WriteByte(0x22);

                // Configura o atalho para executar como administrador editando as permissões (opcional)
                //SetShortcutToRunAsAdmin(shortcutPath);
                //SetShortcutToRunAsAdmin1(shortcutPath);
            } catch(Exception ex) {
                MessageBox.Show($"Failed to create shortcut: {ex.Message}");
            }
        });
    }
}

