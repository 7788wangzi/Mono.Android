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

namespace Example.Mono.Android
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Second);

            TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
            var argument = Intent.GetStringExtra("Arg1") ?? "Not Available";
            textView1.Text = "Welcome! It's TextView from second page." + argument;

            ListView myList = FindViewById<ListView>(Resource.Id.listView1);

            List<TwoRowItem> items = new List<TwoRowItem>();
            items.Add(new TwoRowItem
            {
                Row1 = "Hello Android!",
                Row2 = "Using java"
            });
            items.Add(new TwoRowItem
            {
                Row1 = "Hello Android again!",
                Row2 = "Using C#"
            });

            TwoRowListAdapter adapter = new TwoRowListAdapter(this, items);
            myList.SetAdapter(adapter);

            myList.ItemClick += (sender, e) =>
              {
                  TwoRowItem clickedItem = items[e.Position];
                  string method = clickedItem.Row2;
              };
        }
    }
}