using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace Example.Mono.Android
{
    [Activity(Label = "WebViewLayoutActivity")]
    public class WebViewLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.WebViewLayout);
            WebView myWebView = FindViewById<WebView>(Resource.Id.webView1);
            myWebView.LoadUrl("http://www.baidu.com");
        }
    }
}