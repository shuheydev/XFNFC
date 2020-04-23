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
using Shiny;

namespace XFNFC.Droid
{
    [Application]
    public class MyApplication : ShinyAndroidApplication<MyShinyStartup>
    {
        public MyApplication(IntPtr handle,JniHandleOwnership transfer):base(handle,transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            Shiny.AndroidShinyHost.Init(this, new MyShinyStartup());
        }
    }
}