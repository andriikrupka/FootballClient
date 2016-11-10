namespace FootballClient.UWP.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var text = value as string;
            var param = (string)parameter;
            if (!string.IsNullOrEmpty(text))
            {
                if (param == "UpperCase")
                {
                    text = text.ToUpperInvariant();
                }
                else if (param == "LowerCase")
                {
                    text = text.ToLowerInvariant();
                }
                else if(param == "FirstUpper")
                {
                    var firstPart = text[0].ToString().ToUpper();
                    string lastPart = String.Empty;
                    if (text.Length > 1)
                    {
                        lastPart = text.Substring(1).ToLower();
                    }

                    text = firstPart + lastPart;
                }
            }

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
