﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;

namespace FootballClientForms.Droid
{
    [Activity(Label = "FootballClientForms.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
			ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

			LoadApplication(new App(new DroidInitializer()));
        }
    }

	public class DroidInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IUnityContainer container)
		{

		}
	}
}
