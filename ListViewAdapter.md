##使用自定的Adapter绑定ListView/GridView数据  
对于**ListView/Gridview**的数据绑定， google提供了一些Adapter的模板， 自己也可以自定义一些个性化的显示样式，这样自定义一个Adapter并不复杂，本文自定义了一个显示两行Text的Adapter，使用这个Adapter为ListView绑定数据。  

首先，创建一个有两个TextView组成的Layout - `TwoRowLayout.axml`，这个文件就相当于ListView中一个项目模板。   
源代码如下：

    <?xml version="1.0" encoding="utf-8"?>
    <LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
        android:orientation="vertical"
        android:layout_width="fill_parent"
        android:layout_height="fill_parent">
        <TextView
            android:text="Text"
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:textSize="20dip"
            android:id="@+id/Text1" />
        <TextView
            android:text="Text"
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:textSize="14dip"
            android:id="@+id/Text2" />
    </LinearLayout>

其次， 要定义个性化类`TwoRowItem`, ListView绑定的数据将会是一个List&lt;TwoRowItem&gt;。

    public class TwoRowItem
    {
        public string Row1 { get; set; }
        public string Row2 { get; set; }
    }

创建一个`TwoRowListAdapter`，该类继承`BaseAdapter<TwoRowItem>`。并实现该基类的一些方法。

    public class TwoRowListAdapter:BaseAdapter<TwoRowItem>
    {
        List<TwoRowItem> items;
        Activity context;

        public TwoRowListAdapter(Activity context, List<TwoRowItem> items)
        {
            this.items = items;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override TwoRowItem this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.TwoRowLayout, null);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Row1;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.Row2;

            return view;
        }

    }

然后， 就可以使用自定义的`TwoRowListAdapter`为ListView或GridView绑定数据了。在Second.axml中加入新控件ListView - `listView1`, 然后在SecondActivity.cs中为其绑定数据。  

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



![picture]('C:\Users\v-qixue.FAREAST\Desktop\Android\New folder\ListAdapter.png')