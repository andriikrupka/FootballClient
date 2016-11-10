namespace FootballClient.UWP.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class IntegerIncrementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var integer = System.Convert.ToInt32(value);
            return ++integer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
