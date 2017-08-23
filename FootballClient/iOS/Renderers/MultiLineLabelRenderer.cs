using System;
using FootballClientForms.Controls;
using FootballClientForms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLineLabelRenderer))]
namespace FootballClientForms.iOS.Renderers
{
    public class MultiLineLabelRenderer : LabelRenderer
    {
       protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
				MultiLineLabel multiLineLabel = (MultiLineLabel)Element;
				if (multiLineLabel != null)
				{
					Control.Lines = (multiLineLabel.Lines);
				}
			}
        }
    }
}
