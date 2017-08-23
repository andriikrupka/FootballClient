using System;
using FootballClientForms.Controls;
using FootballClientForms.iOS.Renderers;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]
namespace FootballClientForms.iOS.Renderers
{
    public class HtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
            {
                SetHtmlToControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                SetHtmlToControl();
            }
        }

        private void SetHtmlToControl()
        {
            
			var attr = new NSAttributedStringDocumentAttributes();
			var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;
            
            var myHtmlData = NSData.FromString(Element.Text ?? string.Empty, NSStringEncoding.Unicode);
			
            Control.Lines = 0;
			Control.AttributedText = new NSAttributedString(myHtmlData, attr, ref nsError);

            var font = UIKit.UIFont.FromName("HelveticaNeue", new nfloat(Element.FontSize));

            Control.Font = font;
        }
    }
}
