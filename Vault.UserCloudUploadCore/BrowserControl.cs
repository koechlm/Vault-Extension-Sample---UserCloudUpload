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
using Microsoft.Web.WebView2.WinForms;
using System.Windows.Threading;

namespace VaultUserCloudUpload
{
    public partial class BrowserControl : UserControl
    {
        public BrowserControl()
        {
            InitializeComponent();
            mWebViewInit();
        }

        void mWebViewInit()
        {
            var frame = new DispatcherFrame();
            var env = CoreWebView2Environment.CreateAsync(null, Environment.GetEnvironmentVariable("TEMP"), null);

            using (var task = webView21.EnsureCoreWebView2Async(env.Result))
            {
                task.ContinueWith((dummy) => frame.Continue = false);
                frame.Continue = true;
                Dispatcher.PushFrame(frame);
            }
        }

        public void mNavigate(string mUrl)
        {
            //read the property of the corresponding project
            Uri uri = new Uri(mUrl, System.UriKind.Absolute);
            webView21.Source = uri;
        }

    }
}
