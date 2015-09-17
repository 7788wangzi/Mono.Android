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
using System.IO;

namespace Example.Mono.Android
{
    [Activity(Label = "ActionbarTabActivity")]
    public class ActionbarTabActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.ActionbarTab);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("list1", Resource.Drawable.Icon, new firstFragment());
            if(bundle!=null)
                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("tab", this.ActionBar.SelectedNavigationIndex);
            base.OnSaveInstanceState(outState);
        }
        void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.frameLayout1);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.frameLayout1, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);

        }

        public class firstFragment:Fragment
        {
            List<string> Sources = null;

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView(inflater, container, savedInstanceState);
                var view = inflater.Inflate(Resource.Layout.ListViewlayout, container, false);
                ListView myList = view.FindViewById<ListView>(Resource.Id.myList);

                Sources = new List<string>();
                for(int i = 1; i<=10;i++)
                {
                    Sources.Add(string.Format(@"item {0}", i));
                }
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this.Activity,
                global::Android.Resource.Layout.SimpleListItem1, Sources);
                myList.SetAdapter(adapter);
                myList.ItemClick += ListClick;

                return view;
            }

            void ListClick(object sender, AdapterView.ItemClickEventArgs e)
            {
                ListView list = sender as ListView;
                string selectedFromList = list.GetItemAtPosition(e.Position).ToString();
            }
        }
    }
}