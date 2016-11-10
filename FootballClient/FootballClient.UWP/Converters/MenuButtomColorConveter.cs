namespace FootballClient.UWP.Converters
{
    using FootballClient.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI;
    using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

    public class MenuButtomColorConveter : IValueConverter
    {
        private static SolidColorBrush greenBrush = new SolidColorBrush(Color.FromArgb(255, 0, 166, 4));
        private static SolidColorBrush grayBrush = new SolidColorBrush(Color.FromArgb(255, 98, 98, 98));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var currentPage = (AppPages)value;
            var buttonPage = (AppPages)Enum.Parse(typeof(AppPages), (string)parameter);
            var result = grayBrush;
            if (currentPage == buttonPage)
            {
                result = greenBrush;
            }
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
