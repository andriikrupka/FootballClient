using System;
using System.Globalization;
using Xamarin.Forms;

namespace FootballClientForms.Converters
{
    public class StringToUriSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = value as string ?? string.Empty;

            var uriSource = new UriImageSource()
            {
                CachingEnabled = true,
                CacheValidity = TimeSpan.FromDays(4)
            };

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                uriSource.Uri = new Uri(url);    
            }

            return uriSource; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
