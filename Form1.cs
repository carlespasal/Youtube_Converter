using AltoHttp;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace youtube_converter
{
    public partial class Form1 : Form
    {
        private HttpDownloader httpDownloader;
        //Folder where music is saved
        private const string FOLDERNAME = "videos_music";
        //Wich disk is where the program is installed
        private string root = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
        public Form1()
        {
            InitializeComponent();

            root = root.Remove(root.Length - 1);
            createYoutubefolder();
            
            if (!CheckYoutubedl())
            {
                DownloadYoutubedl();
                MessageBox.Show("Descarga completada");
            }
            else
            {
                MessageBox.Show("Youtube-dl encontrado");
            }
        }

        
        private void createYoutubefolder()
        {
            string folderpath = $"{Application.StartupPath}\\{FOLDERNAME}";
            if (!Directory.Exists(folderpath)) Directory.CreateDirectory(folderpath);
        }

        // Youtube-dl downloader
        private void DownloadYoutubedl()
        {
            string youtubedl = "https://youtube-dl.org/downloads/latest/youtube-dl.exe";
            httpDownloader = new HttpDownloader(youtubedl, $"{Application.StartupPath}\\{FOLDERNAME}\\{Path.GetFileName(youtubedl)}");
            httpDownloader.Start();
        }
        private void Download_Click(object sender, EventArgs e)
        {
            string youtubedllocation = $"{Application.StartupPath}\\{FOLDERNAME}";
            string fullpath = Path.GetFullPath(youtubedllocation);
            
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = "cmd.exe";
            ps.WindowStyle = ProcessWindowStyle.Normal;

            if (rbAudio.Checked)
            {
                ps.Arguments = $@"/c cd\ && {root} && cd {fullpath} && youtube-dl -x --audio-format mp3 {textBoxURL.Text}";
                Process.Start(ps);
            }
            else if (rbVideo.Checked)
            {
                ps.Arguments = $@"/c cd\ && {root} && cd {fullpath} && youtube-dl {textBoxURL.Text}";
                Process.Start(ps);
                //ps.Arguments = $"youtube"
            }

        }
        private bool CheckYoutubedl()
        {
            string youtubedlname = "youtube-dl.exe";
            string youtubedllocation = $"{Application.StartupPath}\\{FOLDERNAME}\\{youtubedlname}";

            if (File.Exists(youtubedllocation)) return true;
            return false;

        }
    }
}
