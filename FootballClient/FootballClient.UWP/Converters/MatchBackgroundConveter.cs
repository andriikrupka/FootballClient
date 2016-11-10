namespace FootballClient.UWP.Converters
{
    using System;
    using Windows.UI;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    public class MatchBackgroundConveter : IValueConverter
    {
        private static readonly SolidColorBrush ShapeBrush = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));

        private static readonly SolidColorBrush WhiteBrush = new SolidColorBrush(Colors.White);
        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = System.Convert.ToInt32(value);
            var result = ShapeBrush;
            if (index % 2 == 0)
            {
                result = WhiteBrush;
            }

            if ((string) parameter == "invert")
            {
                result = result == WhiteBrush ? ShapeBrush : WhiteBrush;
            }


            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
