##使用ActionBar Tab  
**本文实现将页面分为多个选项卡，并在每一个选项卡中显示一个ListView。**
  
创建新Layout - `ActionbarTab.axml`, 并向页面中添加`FrameLayout`控件。 页面源代码如下：  

	  <?xml version="1.0" encoding="utf-8"?>
	  <LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
	      android:orientation="vertical"
	      android:layout_width="fill_parent"
	      android:layout_height="fill_parent"
	      android:minWidth="25px"
	      android:minHeight="25px">
	      <FrameLayout
	          android:minWidth="25px"
	          android:minHeight="25px"
	          android:layout_width="match_parent"
	          android:layout_height="wrap_content"
	          android:id="@+id/frameLayout1" />
	  </LinearLayout>
创建新Layout - `ListViewLayout.axml`, 这个Layout用于定义嵌在Frame - frameLayout1中的ListView控件。 在页面中添加ListView控件 - Id是myList。页面源代码如下：  

    <?xml version="1.0" encoding="utf-8"?>
    <LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
        android:orientation="vertical"
        android:layout_width="fill_parent"
        android:layout_height="fill_parent"
        android:minWidth="25px"
        android:minHeight="25px">
        <ListView
            android:minWidth="25px"
            android:minHeight="25px"
            android:layout_width="match_parent"
            android:layout_height="match_parent"
            android:id="@+id/myList" />
    </LinearLayout>

向项目中添加新Activity - `ActionbarTabActivity.cs`, 在Activity中实现新Tab的创建。  

1. 在`Oncreate()`方法中，关联前端UI.  
	
		SetContentView(Resource.Layout.ActionbarTab);	
2. 设置页面导航模式为Tabs.

		this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
3. 定义新`AddTab`方法, 将新创建的Fragment View添加到页面控件frameLayout1中。

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
4. 定义两个新Fragment类(此处仅demo一个)，继承基类Fragment。Fragment类似于子Activity, 这两个Fragment的作用是操作两个Tab页面中的控件。

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
            }

            void ListClick(object sender, AdapterView.ItemClickEventArgs e)
            {
                ListView list = sender as ListView;
                string selectedFromList = list.GetItemAtPosition(e.Position).ToString();
            }
        }
以上代码中需要注意， ArrayAdapter的第一参数在Fragment中是`this.Activity`, 而在Activity中是`this`。 `FindViewById()`修改为`view.FindViewById()`。
5. 添加新Tab.

		AddTab("list1", Resource.Drawable.Icon, new firstFragment());

**ActionbarTabActivity** 源代码如下：

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


![ActionBar Tab](https://github.com/7788wangzi/Mono.Android/blob/master/Screen/ActionbarTab.png "Actionbar Tab ")