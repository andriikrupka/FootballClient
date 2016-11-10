using System;
using Windows.UI.Text;
using Windows.UI.Xaml.Data;

namespace FootballClient.UWP.Converters
{
    public class TextClassToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = FontWeights.Normal;
            var className = value as string;
            if (className == "text important")
            {
                result = FontWeights.SemiBold;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
