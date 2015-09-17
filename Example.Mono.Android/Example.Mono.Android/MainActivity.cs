using System;
using Android.Content;
using Android.Runtime;
using Android.Views;

    using Android.App;
    using Android.Widget;
    using Android.OS;
    using System.IO;
    using System.Xml;

    namespace Example.Mono.Android
    {
        [Activity(Label = "Example.Mono.Android", MainLauncher = true, Icon = "@drawable/icon")]
        public class MainActivity : Activity
        {
            protected override void OnCreate(Bundle bundle)
            {
                base.OnCreate(bundle);

                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.Main);

                // Get our button from the layout resource,
                // and attach an event to it
                Button button = FindViewById<Button>(Resource.Id.MyButton);          

                button.Click += delegate {
                    var secondActivity = new Intent(this, typeof(SecondActivity));
                    secondActivity.PutExtra("Arg1", "Argument from main page!");
                    StartActivity(secondActivity);
                };

            Button btnActionbarTab = FindViewById<Button>(Resource.Id.btnActionTab);
            btnActionbarTab.Click += (sender, e) =>
              {
                  var actionbarTabActivity = new Intent(this, typeof(ActionbarTabActivity));
                  StartActivity(actionbarTabActivity);
              };

            Button btnWebView = FindViewById<Button>(Resource.Id.btnWebView);
            btnWebView.Click += delegate
            {
                var webViewActivity = new Intent(this, typeof(WebViewLayoutActivity));
                StartActivity(webViewActivity);
            };
                using (StreamReader sr = new StreamReader(Assets.Open("Sample.xml")))
                {
                    string content = sr.ReadToEnd();
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(content);

                    var level = xDoc.SelectNodes("//SecondLevel[@id='sl1']");
                }
            }
        }
    }

