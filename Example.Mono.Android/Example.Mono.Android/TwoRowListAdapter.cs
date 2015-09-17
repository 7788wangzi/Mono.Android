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
}