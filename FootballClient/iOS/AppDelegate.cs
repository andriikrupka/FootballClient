using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation;
using Microsoft.Practices.Unity;
using Prism.Unity;
using UIKit;

namespace FootballClientForms.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
			ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
			LoadApplication(new App(new IOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

	public class IOSInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IUnityContainer container)
		{

		}
	}
}
