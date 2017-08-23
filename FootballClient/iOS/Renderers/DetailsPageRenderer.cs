using System;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using FootballClientForms.iOS.Renderers;
using FootballClientForms.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DetailsPage), typeof(DetailsPageRenderer))]
namespace FootballClientForms.iOS.Renderers
{

    public class SwipeGestureDelegate : UIGestureRecognizerDelegate
    {
        public override bool ShouldBeRequiredToFailBy (UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
        { 
            return gestureRecognizer is UIScreenEdgePanGestureRecognizer; 
        }
    }

    public class DetailsPageRenderer : PageRenderer, IUIGestureRecognizerDelegate
    {


		//public override void ViewWillAppear(bool animated)
		//{
		//	NavigationController.SetNavigationBarHidden(hidden: true, animated: true);
		//	base.ViewWillAppear(animated);
		//	NavigationController.NavigationBar.Translucent = true;
		//	UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, animated: true);
		//          NavigationController.InteractivePopGestureRecognizer.Delegate = new SwipeGestureDelegate();
		//}

		//public override void ViewDidDisappear(bool animated)
		//{
		//	//NavigationController.SetNavigationBarHidden(hidden: false, animated: true);
		//	//base.ViewWillDisappear(animated);
		//	//UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, animated: true);
		//}

		//public override void WillMoveToParentViewController(UIViewController parent)
		//{
		//	base.WillMoveToParentViewController(parent);

		//	if (parent != null)
		//	{
		//		parent.EdgesForExtendedLayout = UIRectEdge.None;
		//	}
		//}

		      public override void ViewWillAppear(bool animated)
		      {
		          base.ViewWillAppear(animated);
       		    	NavigationController.InteractivePopGestureRecognizer.Delegate = new SwipeGestureDelegate();
	        }

  //      public override void ViewDidAppear(bool animated)
  //      {
  //          base.ViewDidAppear(animated);

  //          UpdateNavigationBarVisibility(true);
  //      }

		//void UpdateNavigationBarVisibility(bool animated)
		//{

		//	var current = Element as IPageController;

		//	if (current == null || NavigationController == null)
		//		return;

		//	var hasNavBar = NavigationPage.GetHasNavigationBar((Page)current);
			
		//		// prevent bottom content "jumping"
		//		current.IgnoresContainerArea = hasNavBar;
		//}

    }
}
