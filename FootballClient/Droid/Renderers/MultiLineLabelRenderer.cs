using System;
using Android.Widget;
using FootballClientForms.Controls;
using FootballClientForms.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLineLabelRenderer))]
namespace FootballClientForms.Droid.Renderers
{
    public class MultiLineLabelRenderer : LabelRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(MultiLineLabel.Lines))
            {
                Control.SetLines(((MultiLineLabel)Element).Lines);
			}
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetSingleLine(false);
                Control.SetLines(((MultiLineLabel)Element).Lines);
			}
        }
    }
}
