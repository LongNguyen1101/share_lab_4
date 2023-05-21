using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB4
{
    public partial class DownloadHtmlForm : Form
    {
        string pathSaveFile;

        public DownloadHtmlForm()
        {
            InitializeComponent();
            txtUrl.ReadOnly = true;
        }

        public DownloadHtmlForm(string Url)
        {
            InitializeComponent();
            txtUrl.ReadOnly = true;
            txtUrl.Text = Url;
        }

        private string getHTML()
        {
            WebRequest request = WebRequest.Create(txtUrl.Text);
            WebResponse response = request.GetResponse();
            StreamReader readFromUrl = new StreamReader(response.GetResponseStream());
            string html = readFromUrl.ReadToEnd();

            response.Close();
            return html;
        }

        private void DownloadHtmlForm_Load(object sender, EventArgs e)
        {
            try
            {
                rtxtShowHtml.Clear();
                rtxtShowHtml.Text = getHTML();
            }
            catch (Exception ex)
            {
                rtxtShowHtml.Text = $"Error from getting html file from this URL: {ex.Message}" + Environment.NewLine;
            }
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.Title = "Save file";
           
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pathSaveFile = sfd.FileName;
                txtPath.Text = pathSaveFile;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Task.Run(() => handleDownload());
        }

        private void handleDownload()
        {
            try
            {
                WebClient myClient = new WebClient();
                Stream response = myClient.OpenRead(txtUrl.Text);
                myClient.DownloadFile(txtUrl.Text, txtPath.Text);
                MessageBox.Show("Downloaded successfully!", "Congratulation");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error from downloading file HTML: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
