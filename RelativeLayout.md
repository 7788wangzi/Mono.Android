##使用RelativeLayout控制WebView以及Bottom按钮的位置  

在Design View中加入控件**RelativeLayout, WebView, LinearLayout(Horizontal), Button, Button**。

1. 添加新Layout - `WebViewLayout.axml`， 打开文件。 选中默认的LinearLayout并删除。  
2. 添加RelativeLayout， 在Toolbox中拖动RelativeLayout控件到页面中。  
3. 拖动WebView控件到RelativeLayout中。
4. 拖动LinearLayout(Horizontal)到RelativeLayout中， 置于WebView之下。
5. 拖动两个Button到LinearLayout(Horizontal)中。

完成以上步骤以后，页面源代码如下：

    <?xml version="1.0" encoding="utf-8"?>
    <RelativeLayout xmlns:p1="http://schemas.android.com/apk/res/android"
        p1:layout_width="match_parent"
        p1:layout_height="match_parent"
        p1:id="@+id/relativeLayout1">
        <WebView
            p1:layout_width="wrap_content"
            p1:layout_height="wrap_content"
            p1:id="@+id/webView1" />
        <LinearLayout
            p1:orientation="horizontal"
            p1:minWidth="25px"
            p1:minHeight="25px"
            p1:layout_width="wrap_content"
            p1:layout_height="wrap_content"
            p1:layout_below="@id/webView1"
            p1:id="@+id/linearLayout1">
            <Button
                p1:text="Button"
                p1:layout_width="wrap_content"
                p1:layout_height="match_parent"
                p1:id="@+id/button2" />
            <Button
                p1:text="Button"
                p1:layout_width="wrap_content"
                p1:layout_height="match_parent"
                p1:id="@+id/button1" />
        </LinearLayout>
    </RelativeLayout>

切换到Source View， 实现如下修改：

1. 在WebView中，加入位置属性。

		p1:layout_above="@+id/linearLayout1"
2. 修改WebView的layout_width, layout_height属性为`fill_parent`。

		p1:layout_width="fill_parent"
    	p1:layout_height="fill_parent"

3. 添加LinearLayout的`layout_alignParentBottom`属性。

	
		p1:layout_alignParentBottom="true"
4. 修改LinearLayout的layout_width, layout_height属性。

		p1:layout_width="match_parent"
        p1:layout_height="wrap_content"
5. 为了使两个Button平分空间， 加入`layout_weight`属性。

		p1:layout_weight="0.5"

修改后的源代码如下：

      <?xml version="1.0" encoding="utf-8"?>
      <RelativeLayout xmlns:p1="http://schemas.android.com/apk/res/android"
          p1:layout_width="match_parent"
          p1:layout_height="match_parent"
          p1:id="@+id/relativeLayout1">
          <WebView
              p1:layout_width="fill_parent"
              p1:layout_height="fill_parent"
              p1:id="@+id/webView1"
              p1:layout_above="@+id/linearLayout1" />
          <LinearLayout
              p1:orientation="horizontal"
              p1:minWidth="25px"
              p1:minHeight="25px"
              p1:layout_width="match_parent"
              p1:layout_height="wrap_content"
              p1:layout_below="@id/webView1"
              p1:id="@+id/linearLayout1"
              p1:layout_alignParentBottom="true">
              <Button
                  p1:text="Button"
                  p1:layout_width="wrap_content"
                  p1:layout_height="match_parent"
                  p1:id="@+id/button2"
                  p1:layout_weight="0.5" />
              <Button
                  p1:text="Button"
                  p1:layout_width="wrap_content"
                  p1:layout_height="match_parent"
                  p1:id="@+id/button1"
                  p1:layout_weight="0.5" />
          </LinearLayout>
      </RelativeLayout>

页面截图如下
![picture](C:\Users\v-qixue.FAREAST\Desktop\Android\New folder\webview.png)