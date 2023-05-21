using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LAB4
{
    public partial class Task3Form : Form
    {
        public Task3Form()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form_Resize);
            webView21.NavigationStarting += EnsureHttps;
            webView21.NavigationCompleted += WebView21_NavigationCompleted;
        }

        private void WebView21_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            string currentUrl = webView21.Source.AbsoluteUri;
            txtSearch.Text = currentUrl;
        }

        private void Task3Form_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://www.bing.com/");
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            webView21.Size = this.ClientSize - new System.Drawing.Size(webView21.Location);
            btnGo.Left = this.ClientSize.Width - btnGo.Width; 
            txtSearch.Width = btnGo.Left - txtSearch.Left;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (webView21 != null && webView21.CoreWebView2 != null)
            {
                if (txtSearch.Text.Contains("https://"))
                {
                    webView21.CoreWebView2.Navigate(txtSearch.Text);
                }
                else
                {
                    webView21.CoreWebView2.Navigate($"https://www.google.com/search?q={txtSearch.Text}");
                }
            }
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            if (webView21 != null && webView21.CoreWebView2 != null)
            {
                webView21.CoreWebView2.Reload();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webView21 != null && webView21.CoreWebView2 != null)
            {
                webView21.CoreWebView2.GoBack();
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (webView21 != null && webView21.CoreWebView2 != null)
            {
                webView21.CoreWebView2.GoForward();
            }
        }

        void EnsureHttps(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            String uri = args.Uri;
            if (!uri.StartsWith("https://"))
            {
                webView21.CoreWebView2.ExecuteScriptAsync($"alert('{uri} is not safe, try an https link')");
                args.Cancel = true;
            }
        }

        private void btnDownHtml_Click(object sender, EventArgs e)
        {
            DownloadHtmlForm downloadHtmlForm = new DownloadHtmlForm(txtSearch.Text); 
            downloadHtmlForm.ShowDialog();
        }

        private void btnDownResource_Click(object sender, EventArgs e)
        {
            DownloadResourceFormTask3 downloadResource = new DownloadResourceFormTask3(txtSearch.Text);
            downloadResource.ShowDialog();
        }
    }
}
