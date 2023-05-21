using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace LAB4
{
    public partial class DownloadResourceFormTask3 : Form
    {
        string pathSaveFile;

        public DownloadResourceFormTask3()
        {
            InitializeComponent();
            txtUrl.ReadOnly = true;
        }

        public DownloadResourceFormTask3(string url)
        {
            InitializeComponent();
            txtUrl.ReadOnly = true;
            txtUrl.Text = url;
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() == DialogResult.OK && !String.IsNullOrWhiteSpace(folder.SelectedPath))
            {
                pathSaveFile = folder.SelectedPath;
                txtPath.Text = folder.SelectedPath;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPath.Text))
            {
                Task.Run(() => handleDownResource());
            }
        }

        private void downloadFile (string Url)
        {
            try
            {
                string fileName = Path.GetFileName(Url);
                string filePath = Path.Combine(pathSaveFile, fileName);

                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(Url, filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading from link {Url}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void handleDownResource()
        {
            try
            {
                string url = txtUrl.Text;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);

                IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants()
                    .Where(node =>
                    (
                        (node.Name == "img" || node.Name == "video" || node.Name == "link") && node.Name != "script"
                    ));

                foreach (HtmlNode node in nodes)
                {
                    string Url = "";

                    if (node.Name.Equals("img") || node.Name.Equals("video"))
                        Url = node.GetAttributeValue("src", "");
                    else if (node.Name.Equals("link"))
                    {
                        string rel = node.GetAttributeValue("rel", "");
                        if (rel == "icon")
                        {
                            Url = node.GetAttributeValue("href", "");
                        }
                    }

                    if (!String.IsNullOrEmpty(Url))
                    {
                        downloadFile(Url);
                    }
                }

                MessageBox.Show("Downloaded successfully!", "Congratulation");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading from website: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
