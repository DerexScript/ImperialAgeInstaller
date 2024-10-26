namespace ImperialAgeInstaller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.installDirectoryLabel=new Label();
            this.progressBar=new ProgressBar();
            this.installationCompleteNoticeLabel=new Label();
            this.installationStatusLabel=new Label();
            this.background=new PictureBox();
            this.discordButton=new PictureBox();
            this.websiteButton=new PictureBox();
            this.closeButton=new PictureBox();
            this.nextButton=new PictureBox();
            this.installDirectoryTextBox=new TextBox();
            this.changeDirectoryButton=new PictureBox();
            this.playButton=new PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.background).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.discordButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.websiteButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.closeButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nextButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.changeDirectoryButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.playButton).BeginInit();
            this.SuspendLayout();
            // 
            // installDirectoryLabel
            // 
            this.installDirectoryLabel.AutoSize=true;
            this.installDirectoryLabel.BackColor=Color.Transparent;
            this.installDirectoryLabel.Font=new Font("HoloLens MDL2 Assets", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.installDirectoryLabel.ForeColor=Color.Transparent;
            this.installDirectoryLabel.Location=new Point(78, 151);
            this.installDirectoryLabel.Name="installDirectoryLabel";
            this.installDirectoryLabel.Size=new Size(226, 16);
            this.installDirectoryLabel.TabIndex=0;
            this.installDirectoryLabel.Text="Instalar Ultima Online Imperial Age em:";
            // 
            // progressBar
            // 
            this.progressBar.Location=new Point(78, 235);
            this.progressBar.Name="progressBar";
            this.progressBar.Size=new Size(534, 23);
            this.progressBar.TabIndex=5;
            this.progressBar.Visible=false;
            // 
            // installationCompleteNoticeLabel
            // 
            this.installationCompleteNoticeLabel.AutoSize=true;
            this.installationCompleteNoticeLabel.BackColor=Color.Transparent;
            this.installationCompleteNoticeLabel.Font=new Font("HoloLens MDL2 Assets", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.installationCompleteNoticeLabel.ForeColor=Color.Transparent;
            this.installationCompleteNoticeLabel.Location=new Point(78, 235);
            this.installationCompleteNoticeLabel.Name="installationCompleteNoticeLabel";
            this.installationCompleteNoticeLabel.Size=new Size(200, 16);
            this.installationCompleteNoticeLabel.TabIndex=6;
            this.installationCompleteNoticeLabel.Text="Download e instalação concluídos!";
            this.installationCompleteNoticeLabel.Visible=false;
            // 
            // installationStatusLabel
            // 
            this.installationStatusLabel.AutoSize=true;
            this.installationStatusLabel.BackColor=Color.Transparent;
            this.installationStatusLabel.Font=new Font("HoloLens MDL2 Assets", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.installationStatusLabel.ForeColor=Color.Transparent;
            this.installationStatusLabel.Location=new Point(78, 261);
            this.installationStatusLabel.Name="installationStatusLabel";
            this.installationStatusLabel.Size=new Size(68, 16);
            this.installationStatusLabel.TabIndex=9;
            this.installationStatusLabel.Text="Baixando...";
            this.installationStatusLabel.Visible=false;
            // 
            // background
            // 
            this.background.Image=(Image)resources.GetObject("background.Image");
            this.background.Location=new Point(-4, -3);
            this.background.Name="background";
            this.background.Size=new Size(711, 489);
            this.background.SizeMode=PictureBoxSizeMode.StretchImage;
            this.background.TabIndex=10;
            this.background.TabStop=false;
            // 
            // discordButton
            // 
            this.discordButton.BackColor=Color.Transparent;
            this.discordButton.Cursor=Cursors.Hand;
            this.discordButton.Image=Properties.Resources.discord;
            this.discordButton.Location=new Point(362, 378);
            this.discordButton.Name="discordButton";
            this.discordButton.Size=new Size(142, 50);
            this.discordButton.SizeMode=PictureBoxSizeMode.StretchImage;
            this.discordButton.TabIndex=13;
            this.discordButton.TabStop=false;
            this.discordButton.Click+=this.DscordButton_Click;
            this.discordButton.MouseEnter+=this.DiscordButton_Enter;
            this.discordButton.MouseLeave+=this.DiscordButton_Leave;
            // 
            // websiteButton
            // 
            this.websiteButton.BackColor=Color.Transparent;
            this.websiteButton.Cursor=Cursors.Hand;
            this.websiteButton.Image=Properties.Resources.website;
            this.websiteButton.Location=new Point(186, 378);
            this.websiteButton.Name="websiteButton";
            this.websiteButton.Size=new Size(142, 50);
            this.websiteButton.SizeMode=PictureBoxSizeMode.StretchImage;
            this.websiteButton.TabIndex=14;
            this.websiteButton.TabStop=false;
            this.websiteButton.Click+=this.WebsiteButton_Click;
            this.websiteButton.MouseEnter+=this.WebsiteButton_Enter;
            this.websiteButton.MouseLeave+=this.WebsiteButton_Leave;
            // 
            // closeButton
            // 
            this.closeButton.BackColor=Color.Transparent;
            this.closeButton.Cursor=Cursors.Hand;
            this.closeButton.Image=Properties.Resources.exit1;
            this.closeButton.Location=new Point(668, 12);
            this.closeButton.Name="closeButton";
            this.closeButton.Size=new Size(25, 25);
            this.closeButton.SizeMode=PictureBoxSizeMode.StretchImage;
            this.closeButton.TabIndex=16;
            this.closeButton.TabStop=false;
            this.closeButton.Click+=this.CloseButton_Click;
            this.closeButton.MouseEnter+=this.CloseButton_Enter;
            this.closeButton.MouseLeave+=this.CloseButton_Leave;
            // 
            // nextButton
            // 
            this.nextButton.BackColor=Color.Transparent;
            this.nextButton.Cursor=Cursors.Hand;
            this.nextButton.Image=Properties.Resources.avancar1;
            this.nextButton.Location=new Point(268, 284);
            this.nextButton.Name="nextButton";
            this.nextButton.Size=new Size(157, 57);
            this.nextButton.TabIndex=17;
            this.nextButton.TabStop=false;
            this.nextButton.Click+=this.NextButton_ClickAsync;
            this.nextButton.MouseEnter+=this.NextButton_Enter;
            this.nextButton.MouseLeave+=this.NextButton_Leave;
            // 
            // installDirectoryTextBox
            // 
            this.installDirectoryTextBox.BackColor=SystemColors.InactiveBorder;
            this.installDirectoryTextBox.Location=new Point(78, 177);
            this.installDirectoryTextBox.Name="installDirectoryTextBox";
            this.installDirectoryTextBox.Size=new Size(534, 23);
            this.installDirectoryTextBox.TabIndex=19;
            this.installDirectoryTextBox.Text="C:\\Games\\Ultima Online Imperial Age";
            // 
            // changeDirectoryButton
            // 
            this.changeDirectoryButton.BackColor=Color.Transparent;
            this.changeDirectoryButton.Image=Properties.Resources.alterar1;
            this.changeDirectoryButton.Location=new Point(78, 206);
            this.changeDirectoryButton.Name="changeDirectoryButton";
            this.changeDirectoryButton.Size=new Size(98, 23);
            this.changeDirectoryButton.SizeMode=PictureBoxSizeMode.StretchImage;
            this.changeDirectoryButton.TabIndex=20;
            this.changeDirectoryButton.TabStop=false;
            this.changeDirectoryButton.Click+=this.ChangeDirectoryButton_Click;
            this.changeDirectoryButton.MouseEnter+=this.ChangeDirectoryButton_Enter;
            this.changeDirectoryButton.MouseLeave+=this.ChangeDirectoryButton_Leave;
            // 
            // playButton
            // 
            this.playButton.BackColor=Color.Transparent;
            this.playButton.Cursor=Cursors.Hand;
            this.playButton.Image=Properties.Resources.jogar1;
            this.playButton.Location=new Point(268, 284);
            this.playButton.Name="playButton";
            this.playButton.Size=new Size(157, 57);
            this.playButton.SizeMode=PictureBoxSizeMode.StretchImage;
            this.playButton.TabIndex=21;
            this.playButton.TabStop=false;
            this.playButton.Visible=false;
            this.playButton.MouseEnter+=this.PlayButton_Enter;
            this.playButton.MouseLeave+=this.PlayButton_Leave;
            // 
            // Form1
            // 
            this.AutoScaleDimensions=new SizeF(7F, 15F);
            this.AutoScaleMode=AutoScaleMode.Font;
            this.AutoSizeMode=AutoSizeMode.GrowAndShrink;
            this.ClientSize=new Size(705, 482);
            this.ControlBox=false;
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.changeDirectoryButton);
            this.Controls.Add(this.installDirectoryTextBox);
            this.Controls.Add(this.installDirectoryLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.discordButton);
            this.Controls.Add(this.websiteButton);
            this.Controls.Add(this.installationStatusLabel);
            this.Controls.Add(this.installationCompleteNoticeLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.background);
            this.MaximizeBox=false;
            this.MinimizeBox=false;
            this.Name="Form1";
            this.Text="Form1";
            this.Load+=this.Form1_Load;
            ((System.ComponentModel.ISupportInitialize)this.background).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.discordButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.websiteButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.closeButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nextButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.changeDirectoryButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.playButton).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label installDirectoryLabel;
        private ProgressBar progressBar;
        private Label installationCompleteNoticeLabel;
        private Label installationStatusLabel;
        private PictureBox background;
        private PictureBox discordButton;
        private PictureBox websiteButton;
        private PictureBox closeButton;
        private PictureBox nextButton;
        private TextBox installDirectoryTextBox;
        private PictureBox changeDirectoryButton;
        private PictureBox playButton;
    }
}
