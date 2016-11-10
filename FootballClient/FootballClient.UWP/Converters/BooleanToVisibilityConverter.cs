namespace FootballClient.UWP.Converters
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using System;

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = Visibility.Collapsed;
            if (value != null)
            {
                var flag = (bool)value;
                if (parameter != null && ((string)parameter) == "invert")
                {
                    flag = !flag;
                }

                if (flag)
                {
                    result = Visibility.Visible;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
