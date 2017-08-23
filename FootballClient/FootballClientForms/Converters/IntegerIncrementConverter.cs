using System;
using System.Globalization;
using Xamarin.Forms;

namespace FootballClientForms.Converters
{
    public class IntegerIncrementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			var integer = System.Convert.ToInt32(value);
			return ++integer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
