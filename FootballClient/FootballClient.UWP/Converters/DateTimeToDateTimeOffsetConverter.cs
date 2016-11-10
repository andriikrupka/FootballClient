namespace FootballClient.UWP.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTime = (DateTime)value;
            var dateTimeOffset = new DateTimeOffset(dateTime);
            return dateTimeOffset;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dateTimeOffset = (DateTimeOffset)value;
            return dateTimeOffset.DateTime;
        }
    }
}
