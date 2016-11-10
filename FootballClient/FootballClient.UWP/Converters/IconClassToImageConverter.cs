using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace FootballClient.UWP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class IconClassToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = string.Empty;
            var className = value as string;
            switch (className)
            {
                case "icon icon1":
                    result = "/Assets/MatchDetails/whistle.png";
                    break;

                case "icon icon7":
                case "icon icon12":
                    result = "/Assets/MatchDetails/ball.png";
                    break;

                case "icon icon4":
                    result = "/Assets/MatchDetails/yellow_card.png";
                    break;

                case "icon icon9":
                    result = "/Assets/MatchDetails/timer.png";
                    break;

                case "icon icon3":
                    result = "/Assets/MatchDetails/change.png";
                    break;

                case "icon icon5":
                    result = "/Assets/MatchDetails/red_card.png";
                    break;
                    
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
