using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FootballClient.UWP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class NumberToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var number = System.Convert.ToInt32(value);
            var convertNumber = System.Convert.ToInt32(parameter);
            var result = number == convertNumber ? Visibility.Visible : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
