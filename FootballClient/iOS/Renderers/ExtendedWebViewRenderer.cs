using FootballClientForms.Controls;
using FootballClientForms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace FootballClientForms.iOS.Renderers
{
    public class ExtendedWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			Delegate = new ExtendedUIWebViewDelegate(this);
		}

		public class ExtendedUIWebViewDelegate : UIWebViewDelegate
		{
			ExtendedWebViewRenderer webViewRenderer;

			public ExtendedUIWebViewDelegate(ExtendedWebViewRenderer _webViewRenderer = null)
			{
				webViewRenderer = _webViewRenderer ?? new ExtendedWebViewRenderer();
			}

			public override async void LoadingFinished(UIWebView webView)
			{

				var wv = webViewRenderer.Element as ExtendedWebView;
				if (wv != null)
				{
                    var stringdata = webView.EvaluateJavascript("document.body.scrollHeight;");
					await System.Threading.Tasks.Task.Delay(500); // wait here till content is rendered
                    wv.HeightRequest = double.Parse(stringdata);
                    webView.ScrollView.ScrollEnabled = false;
                    var request = webView.GetSizeRequest(double.PositiveInfinity, double.PositiveInfinity);
				}
			}
		}
	}
}