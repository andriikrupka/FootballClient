namespace FootballClient.UWP.Converters
{
    using System;
    using System.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class EmptyToVisibilityConverter : IValueConverter
    {
        private const string InvertParameter = "invert";

        public static readonly DateTime EmptyDateTime = new DateTime();

        public static readonly TimeSpan EmptyTime = new TimeSpan();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = Visibility.Collapsed;
            if (value is ICollection)
            {
                var collection = (ICollection)value;
                if (collection.Count > 0)
                {
                    result = Visibility.Visible;
                }
            }
            else if (value is int)
            {
                var count = int.Parse(value.ToString());
                if (count != 0)
                {
                    result = Visibility.Visible;
                }
            }
            else if (value is long)
            {
                var count = long.Parse(value.ToString());
                if (count != 0)
                {
                    result = Visibility.Visible;
                }
            }
            else if (value is double)
            {
                var count = double.Parse(value.ToString());
                if (count != 0.0)
                {
                    result = Visibility.Visible;
                }
            }
            else if (value is string)
            {
                var str = (string)value;
                if (!String.IsNullOrEmpty(str))
                {
                    result = Visibility.Visible;
                }
            }
            else if (value is DateTime)
            {
                var date = (DateTime)value;
                if (date != EmptyDateTime)
                {
                    result = Visibility.Visible;
                }
            }
            else if (value is TimeSpan)
            {
                var time = (TimeSpan)value;
                if (time != EmptyTime)
                {
                    result = Visibility.Visible;
                }
            }
            else if (value != null)
            {
                result = Visibility.Visible;
            }

            if (parameter != null)
            {
                var stringParameter = (string)parameter;
                if (stringParameter == InvertParameter)
                {
                    if (result == Visibility.Visible)
                    {
                        result = Visibility.Collapsed;
                    }
                    else
                    {
                        result = Visibility.Visible;
                    }
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
