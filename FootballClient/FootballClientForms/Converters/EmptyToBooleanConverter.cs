using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace FootballClientForms.Converters
{
    public class EmptyToBooleanConverter : IValueConverter
    {
		public static readonly DateTime EmptyDateTime = new DateTime();
        public static readonly TimeSpan EmptyTime = new TimeSpan();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			var result = false;
			switch (value)
			{
				case string stringValue:
					result = !string.IsNullOrWhiteSpace(stringValue);
					break;
				case IEnumerable enumerable:
					result = (enumerable as IEnumerable<object>)?.Any() ?? false;
					break;
				case int intCount:
					result = intCount != 0;
					break;
				case long longCount:
					result = longCount != 0;
					break;
				case double doubleValue:
					result = Math.Abs(doubleValue) > 0.000000101;
					break;
				case DateTime dateTimeValue:
					result = dateTimeValue != EmptyDateTime;
					break;
				case TimeSpan timeSpanValue:
					result = timeSpanValue != EmptyTime;
					break;
				default:
					result = value != null;
					break;
			}

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
