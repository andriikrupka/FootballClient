namespace FootballClient.UWP.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class EmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
