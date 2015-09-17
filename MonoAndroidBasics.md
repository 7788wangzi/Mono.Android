##Mono.Android 基础  
###Mono.Android项目结构是  

	— Project
	 + Assets
	 + Resources
		 + drawable
		 + layout
		 + values
		  Resource.Designer.cs
	  XXActivity.cs  

其中, **Layout**文件夹下存放App的前端UI文件，前端UI是一个后缀名为.axml的XML文件，该文件有两个视图：**Design**和**Source**。在Design视图中支持可视化控件的拖拽。  
App的后端是Activity的类，自己写的类都要继承基类Activity， 并在自己类中操作前端页面的控件。  
**Assets**文件夹下存放项目的静态文件，例如你的大纲XML文件等，这里的文件可以通过以下流方法**`Assets.Open()`**读取：  

            using (StreamReader sr = new StreamReader(Assets.Open("sample.xml")))
            {
                string content = sr.ReadToEnd();
            }

**Resource.Designer.cs**文件会记录所有项目中的控件的Id, 也包括UI页面。有时候在页面上加入一个新的控件以后，它的Id并没有自动加入Resource.Designer.cs这个文件，或者是这个文件没有重新生成。出现这个情况，一是可以单击**保存所有** 按钮，  然后在解决方案窗口中单击**刷新**图标， 然后，打开文件**Resource.Designer.cs** ， 然后关闭文件**Resource.Designer.cs**。 如果还是不行，可以检查项目文件（XX.csproj,使用Notepad打开)， 确保以下三行存在：  

    <MonoAndroidResourcePrefix>Resources</MonoAndroidResourcePrefix>
    <AndroidResgenFile>Resources\Resource.Designer.cs</AndroidResgenFile>
    <AndroidResgenClass>Resource</AndroidResgenClass>

###关联Activity的前端UI页面
使用`SetContentView(Resource.Layout.Main)`将Activity类关联到前端页面。完成关联以后，可以通过`FindViewById()`获得页面中定义的控件。

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);          

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

Activity的特性`MainLauncher=true`，标识这个文件是应用的入口。

初始时代码如下：  

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
            int count = 1;

            protected override void OnCreate(Bundle bundle)
            {
                base.OnCreate(bundle);

                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.Main);

                // Get our button from the layout resource,
                // and attach an event to it
                Button button = FindViewById<Button>(Resource.Id.MyButton);          

                button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

                using (StreamReader sr = new StreamReader(Assets.Open("sample.xml")))
                {
                    string content = sr.ReadToEnd();
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(content);

                    var level = xDoc.SelectNodes("//SecondLevel[@id='sl1']");
                }
            }
        }
    }


###关于页面跳转  
在Layout中加入新Android Layout页面`Second.axml`, 在项目中加入新Activity类`SecondActivity.cs`。在Main页面，单击Button，然后跳转到Second页面，并且把参数传递过去。 创建新的Activity的实例是使用`Intent`，在Intent中把当前Activity的上下文传进去，使用`SecondActivity`类型初始化Intent，即`var secondActivity = new Intent(this, typeof(SecondActivity));`。 
使用`secondActivity.PutExtra()`可以把参数传到second页, `secondActivity.PutExtra("Arg1", "Argument from main page!");`。启动该Intent，`StartActivity(secondActivity);`。  
代码如下：

                button.Click += delegate {
                    var secondActivity = new Intent(this, typeof(SecondActivity));
                    secondActivity.PutExtra("Arg1", "Argument from main page!");
                    StartActivity(secondActivity);
                };

在second页的OnCreate方法中，使用`Intent.GetStringExtra`接受传递的参数。  
代码如下：

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
        }
    }