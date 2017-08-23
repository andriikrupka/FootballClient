using System;
using System.Globalization;
using Xamarin.Forms;

namespace FootballClientForms.Converters
{
    public class HtmlTextToWebViewSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var htmlData = value as string ?? string.Empty;
            return new HtmlWebViewSource { Html = htmlData };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
