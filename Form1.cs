using System.Diagnostics;
using System.IO.Compression;

namespace ImperialAgeInstaller
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = label2.Text;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                label2.Text = folderDialog.SelectedPath + @"\Ultima Online Imperial Age";
            }
        }

        private void HideComponents()
        {
            foreach (Control control in this.Controls)
            {
                // if (control != progressBar1)
                control.Visible = false;
                
            }
        }

        private async Task DownloadFileAsync(string fileUrl, string destinationPath)
        {
            using (var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var canReportProgress = totalBytes != -1;

                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var httpStream = await response.Content.ReadAsStreamAsync())
                {
                    var buffer = new byte[8192];
                    long totalRead = 0;
                    int bytesRead;
                    while ((bytesRead = await httpStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        totalRead += bytesRead;

                        if (canReportProgress)
                        {
                            progressBar1.Value = (int)((totalRead * 100) / totalBytes);
                        }
                    }
                }
            }
        }

        private void ExtractZipFile(string zipFilePath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            HideComponents();
            progressBar1.Visible = true;
            label4.Visible = true;
            linkLabel1.Visible = true;
            linkLabel2.Visible = true;

            string zipUrl = "https://drive.usercontent.google.com/download?id=1xorUs-cu918kfx1Lg15b-YTh6eOVgeqe&export=download&authuser=0&confirm=t&uuid=8a4a5b93-9746-437c-8890-5105aea86551&at=AN_67v2dxW32a3NqqOyLeDM0GBMO%3A1729453485197";
            // string zipUrl = "https://drive.google.com/uc?export=download&id=1xorUs-cu918kfx1Lg15b-YTh6eOVgeqe";
            string downloadDirectory = label2.Text;
            string zipFilePath = Path.Combine(downloadDirectory, "ImperialAge.zip");
            // Verifica se o diretório existe, se não, cria o diretório
            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }
            try
            {
                // Baixar o arquivo zip
                await DownloadFileAsync(zipUrl, zipFilePath);
                label4.Visible = false;
                label4.Text = "Extraindo...";
                label4.Visible = true;
                await Task.Delay(2000);
                // Extrair o conteúdo do zip
                ExtractZipFile(zipFilePath, downloadDirectory);

                // Excluir o arquivo zip
                File.Delete(zipFilePath);

                // MessageBox.Show("Download e instalação concluídos!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
            finally
            {
                HideComponents();
                label3.Visible = true;
                linkLabel1.Visible = true;
                linkLabel2.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Você tem certeza que deseja cancelar?",
                "Confirmar saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://imperialage.com.br/") { UseShellExecute = true });
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/AsY7kv8H") { UseShellExecute = true });
        }
    }
}
