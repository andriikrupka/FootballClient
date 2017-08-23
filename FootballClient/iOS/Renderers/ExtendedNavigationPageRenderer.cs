using System;
using System.Diagnostics;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using FootballClientForms;
using FootballClientForms.iOS.Renderers;
using FootballClientForms.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedNavigationPage), typeof(ExtendedNavigationPageRenderer))]
namespace FootballClientForms.iOS.Renderers
{
	public class ExtendedNavigationPageRenderer : NavigationRenderer
	{
        
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
			WriteLine();
			base.OnElementChanged(e);

            if (Element != null)
            {
                var t = Element as IPageController;

            }

            var z = this as IPageController;

        }

        public override UIViewController[] PopToRootViewController(bool animated)
        {
            WriteLine();
            return base.PopToRootViewController(animated);
        }

        public override UIViewController PopViewController(bool animated)
        {
            WriteLine();
            return base.PopViewController(animated);
        }

        public override void ViewWillLayoutSubviews()
        {
            WriteLine();
            base.ViewWillLayoutSubviews();
        }

        public override void ViewDidLayoutSubviews()
        {
            WriteLine();
            UpdateNavigationBarVisibility(true);
            base.ViewDidLayoutSubviews();
        }

        protected override System.Threading.Tasks.Task<bool> OnPopToRoot(Page page, bool animated)
        {
            WriteLine();
            return base.OnPopToRoot(page, animated);
        }

        protected override System.Threading.Tasks.Task<bool> OnPushAsync(Page page, bool animated)
        {
            WriteLine();
            return base.OnPushAsync(page, animated);
        }

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            WriteLine();
            base.PushViewController(viewController, animated);
        }

        public override void ViewWillAppear(bool animated)
        {
            WriteLine();
            UpdateNavigationBarVisibility((true));
            base.ViewWillAppear(animated);
        }


        private void WriteLine([System.Runtime.CompilerServices.CallerMemberName] string nameMethod = null)
		{
            Debug.WriteLine(nameMethod);
		}

        public override void ViewDidAppear(bool animated)
        {
			UpdateNavigationBarVisibility(true);
			base.ViewDidAppear(animated);
        }



		void UpdateNavigationBarVisibility(bool animated)
		{

            var current = Element as IPageController;

			if (current == null || NavigationController == null)
				return;

			var hasNavBar = NavigationPage.GetHasNavigationBar((Page)current);

			if (NavigationController.NavigationBarHidden == hasNavBar)
			{
				// prevent bottom content "jumping"
				current.IgnoresContainerArea = !hasNavBar;
				NavigationController.SetNavigationBarHidden(!hasNavBar, animated);
			}
		}
	}
}