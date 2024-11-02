using System.Diagnostics;

namespace ImperialAgeInstaller;

public partial class Form1 : Form {

    private bool dragging = false;
    private Point dragCursorPoint;
    private Point dragFormPoint;

    private void Background_MouseDown(object? sender, MouseEventArgs e) {
        dragging=true;
        dragCursorPoint=Cursor.Position;
        dragFormPoint=this.Location;
    }

    private void Background_MouseMove(object? sender, MouseEventArgs e) {
        if(dragging) {
            Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            this.Location=Point.Add(dragFormPoint, new Size(diff));
        }
    }

    private void Background_MouseUp(object? sender, MouseEventArgs e) {
        dragging=false;
    }

    public Form1() {
        InitializeComponent();
        this.StartPosition=FormStartPosition.CenterScreen;
        this.FormBorderStyle=FormBorderStyle.None;
        this.Region=Region.FromHrgn(Utility.CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15));
        this.background.MouseDown+=this.Background_MouseDown;
        this.background.MouseMove+=this.Background_MouseMove;
        this.background.MouseUp+=this.Background_MouseUp;
    }

    private void Form1_Load(object sender, EventArgs e) {
        websiteButton.Parent=background;
        discordButton.Parent=background;
        nextButton.Parent=background;
        nextButton.Image=Properties.Resources.avancar1;
        closeButton.Parent=background;
        changeDirectoryButton.Parent=background;
        playButton.Parent=background;
        installDirectoryLabel.Parent=background;
        installationCompleteNoticeLabel.Parent=background;
        installationStatusLabel.Parent=background;
    }

    private void CloseButton_Click(object sender, EventArgs e) {
        DialogResult result = MessageBox.Show(
            "Você tem certeza que deseja cancelar?",
            "Confirmar saída",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );
        if(result==DialogResult.Yes) {
            Application.Exit();
        }
    }

    private void CloseButton_Enter(object sender, EventArgs e) {
        closeButton.Image=Properties.Resources.exit2;
    }

    private void CloseButton_Leave(object sender, EventArgs e) {
        closeButton.Image=Properties.Resources.exit1;
    }

    private async void NextButton_ClickAsync(object sender, EventArgs e) {
        Utility.HideComponents(this.Controls);
        progressBar.Visible=true;
        installationStatusLabel.Visible=true;
        installDirectoryLabel.Visible=false;
        websiteButton.Visible=true;
        discordButton.Visible=true;
        background.Visible=true;
        nextButton.Visible=false;
        changeDirectoryButton.Visible=false;
        // string zipUrl = "https://drive.usercontent.google.com/download?id=1xorUs-cu918kfx1Lg15b-YTh6eOVgeqe&export=download&authuser=0&confirm=t&uuid=53b88648-57cd-4b85-8712-9b815e8c6907&at=AN_67v1SFkvd_NZtr_t0JT1gsG8M%3A1730051928633";
        // string zipUrl = "https://ftp.imperialageshard.com.br/files/Ultima%20Online%20Imperial%20Age.zip";
        string zipUrl = "https://imperialageshard.com.br/files/Ultima%20Online%20Imperial%20Age.zip";
        string downloadDirectory = installDirectoryTextBox.Text;
        string zipFilePath = Path.Combine(downloadDirectory, "ImperialAge.zip");
        try {
            await Utility.DownloadFileAsync(zipUrl, zipFilePath, progressBar, downloadDirectory);
            installationStatusLabel.Visible=false;
            installationStatusLabel.Text="Extraindo...";
            installationStatusLabel.Visible=true;
            //await Task.Delay(2000);
            //await Utility.ExtractZipFileAsync(zipFilePath, downloadDirectory);
            await Utility.ExtractZipWithProgressAsync(zipFilePath, downloadDirectory, progressBar);
        } catch(Exception ex) {
            MessageBox.Show($"Ocorreu um erro: {ex.Message}");
        } finally {
            await Task.Run(() => File.Delete(zipFilePath));
            Utility.HideComponents(this.Controls);
            installationCompleteNoticeLabel.Visible=true;
            installationStatusLabel.Visible=false;
            websiteButton.Visible=true;
            discordButton.Visible=true;
            background.Visible=true;
            await Utility.EnsureLauncherSettings(this.installDirectoryTextBox.Text);
            await Utility.CreateShortcut(downloadDirectory);
            playButton.Visible=true;
        }
    }

    private void NextButton_Enter(object sender, EventArgs e) {
        nextButton.Image=Properties.Resources.avancar2;
    }

    private void NextButton_Leave(object sender, EventArgs e) {
        nextButton.Image=Properties.Resources.avancar1;
    }

    private void ChangeDirectoryButton_Click(object sender, EventArgs e) {
        using var folderDialog = new FolderBrowserDialog();
        folderDialog.SelectedPath=installDirectoryTextBox.Text;
        if(folderDialog.ShowDialog()==DialogResult.OK) {
            installDirectoryTextBox.Text=folderDialog.SelectedPath+@"\Ultima Online Imperial Age";
        }
    }

    private void ChangeDirectoryButton_Enter(object sender, EventArgs e) {
        changeDirectoryButton.Image=Properties.Resources.alterar2;
        changeDirectoryButton.SizeMode=PictureBoxSizeMode.StretchImage;
    }

    private void ChangeDirectoryButton_Leave(object sender, EventArgs e) {
        changeDirectoryButton.Image=Properties.Resources.alterar1;
        changeDirectoryButton.SizeMode=PictureBoxSizeMode.StretchImage;
    }

    private void DscordButton_Click(object sender, EventArgs e) {
        Process.Start(new ProcessStartInfo("https://discord.gg/AsY7kv8H") { UseShellExecute=true });
    }

    private void DiscordButton_Enter(object sender, EventArgs e) {
        discordButton.Image=Properties.Resources.discord1;
        discordButton.SizeMode=PictureBoxSizeMode.StretchImage;
    }

    private void DiscordButton_Leave(object sender, EventArgs e) {
        discordButton.Image=Properties.Resources.discord;
        discordButton.SizeMode=PictureBoxSizeMode.StretchImage;

    }

    private void WebsiteButton_Click(object sender, EventArgs e) {
        Process.Start(new ProcessStartInfo("https://imperialage.com.br/") { UseShellExecute=true });
    }

    private void WebsiteButton_Enter(object sender, EventArgs e) {
        websiteButton.Image=Properties.Resources.website1;
        websiteButton.SizeMode=PictureBoxSizeMode.StretchImage;
    }

    private void WebsiteButton_Leave(object sender, EventArgs e) {
        websiteButton.Image=Properties.Resources.website;
        websiteButton.SizeMode=PictureBoxSizeMode.StretchImage;

    }

    private void PlayButton_Enter(object sender, EventArgs e) {
        playButton.Image=Properties.Resources.jogar2;
        playButton.SizeMode=PictureBoxSizeMode.StretchImage;
    }

    private void PlayButton_Leave(object sender, EventArgs e) {
        playButton.Image=Properties.Resources.jogar1;
        playButton.SizeMode=PictureBoxSizeMode.StretchImage;
    }

    private void PlayButton_Click(object sender, EventArgs e) {
        Utility.RunImperialAgeLauncher(playButton, this, installDirectoryTextBox);
    }
}
