namespace FootballClient.UWP.Converters
{
    using System;
    using Windows.UI;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    public class ScoreBackgroundConveter : IValueConverter
    {
        private static readonly SolidColorBrush ShapeBrush = new SolidColorBrush(Color.FromArgb(255, 77, 77, 77));

        private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Color.FromArgb(255, 255, 64, 63));
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var cssClass = value as string;
            var result = ShapeBrush;
            if (cssClass == "inprogress")
            {
                result = RedBrush;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
