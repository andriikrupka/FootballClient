using FootballClientForms.Controls;
using FootballClientForms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ParallaxScrollView), typeof(ParallaxScrollViewRenderer))]
namespace FootballClientForms.iOS.Renderers
{
    public class ParallaxScrollViewRenderer : ScrollViewRenderer
    {
       protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.AlwaysBounceVertical = true;
        }
    }
}
