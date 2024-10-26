using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImperialAgeInstaller

    
        
    {
    public partial class Form1 : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Quando o mouse é pressionado, iniciamos o arrasto
            dragging = true;
            // Pega a posição atual do cursor e do formulário
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Se estiver arrastando, mova o formulário
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Finaliza o arrasto quando o botão do mouse é solto
            dragging = false;
        }

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(
            0, 0, this.Width, this.Height, 15, 15)); // Altere 30 para ajustar o grau de arredondamento
            // Conecte os eventos do PictureBox
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Define o PictureBox1 como o "pai" do PictureBox6

            pictureBox5.Parent = pictureBox1;
            pictureBox4.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;
            pictureBox2.Parent = pictureBox1;
            pictureBox6.Parent = pictureBox1;
            pictureBox7.Parent = pictureBox1;
            label1.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;

            //pictureBox3.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.avancar1);
            pictureBox3.Image = Properties.Resources.avancar1;




            //// Define a cor de fundo do PictureBox6 como transparente
            //pictureBox6.BackColor = Color.Transparent;
            //pictureBox5.BackColor = Color.Transparent;
            //pictureBox4.BackColor = Color.Transparent;
            //pictureBox3.BackColor = Color.Transparent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = textBox2.Text;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderDialog.SelectedPath + @"\Ultima Online Imperial Age";
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
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //this.pictureBox6.Parent = this.pictureBox1;
            //this.pictureBox6.BackColor = Color.Transparent;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://imperialage.com.br/") { UseShellExecute = true });
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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
        private void pictureBox2_Enter(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.exit2;
        }
        private void pictureBox2_Leave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.exit1;
        }
        private void pictureBox3_Enter(object sender, EventArgs e)
        {
            //pictureBox3.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.avancar2);
            pictureBox3.Image = Properties.Resources.avancar2;

        }
        private void pictureBox3_Leave(object sender, EventArgs e)
        {
            //pictureBox3.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.avancar1);
            pictureBox3.Image = Properties.Resources.avancar1;
        }

        private void pictureBox6_Enter(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar1);
            pictureBox6.Image = Properties.Resources.alterar2;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void pictureBox6_Leave(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar2);
            pictureBox6.Image = Properties.Resources.alterar1;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;

        }
        private void pictureBox4_Enter(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar1);
            pictureBox4.Image = Properties.Resources.discord1;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void pictureBox4_Leave(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar2);
            pictureBox4.Image = Properties.Resources.discord;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

        }
        private void pictureBox5_Enter(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar1);
            pictureBox5.Image = Properties.Resources.website1;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void pictureBox5_Leave(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar2);
            pictureBox5.Image = Properties.Resources.website;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;

        }
        private void pictureBox7_Enter(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar1);
            pictureBox7.Image = Properties.Resources.jogar2;
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox7.Height = pictureBox7.Height + 2;
            //pictureBox7.Width = pictureBox7.Width + 2;
            //pictureBox7.Location = new Point(267, 283);
        }
        private void pictureBox7_Leave(object sender, EventArgs e)
        {
            //pictureBox6.Image = (Bitmap)new ImageConverter().ConvertFrom(Properties.Resources.alterar2);
            pictureBox7.Image = Properties.Resources.jogar1;
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox7.Height = pictureBox7.Height - 1;
            //pictureBox7.Width = pictureBox7.Width - 1;
            //pictureBox7.Location = new Point(268, 284);

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/AsY7kv8H") { UseShellExecute = true });
        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            HideComponents();
            progressBar1.Visible = true;
            label4.Visible = true;
            label1.Visible = false;
            pictureBox5.Visible = true;
            pictureBox4.Visible = true;
            pictureBox1.Visible = true;
            pictureBox3.Visible = false;
            pictureBox6.Visible = false;

            string zipUrl = "https://www.dropbox.com/scl/fi/myi5tv9udobkpq4m9sqlz/UltimaOnlineImperialAge.zip?rlkey=2ge3zc4dd1kn8dwfrt08r2ytx&st=2elvbmh1&dl=1";
            // string zipUrl = "https://drive.google.com/uc?export=download&id=1xorUs-cu918kfx1Lg15b-YTh6eOVgeqe";
            string downloadDirectory = textBox2.Text;
            string zipFilePath = Path.Combine(downloadDirectory, "ImperialAge.zip");
            // Verifica se o diret�rio existe, se n�o, cria o diret�rio
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
                // Extrair o conte�do do zip
                ExtractZipFile(zipFilePath, downloadDirectory);

                // Excluir o arquivo zip
                File.Delete(zipFilePath);

                // MessageBox.Show("Download e instala��o conclu�dos!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
            finally
            {
                HideComponents();
                label3.Visible = true;
                label4.Visible = false;
                pictureBox5.Visible = true;
                pictureBox4.Visible = true;
                pictureBox1.Visible = true;
                pictureBox7.Visible = true;
            }
        }
    }
}
