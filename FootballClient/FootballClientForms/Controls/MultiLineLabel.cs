using System;
using Xamarin.Forms;

namespace FootballClientForms.Controls
{
    public class MultiLineLabel : Label
    {
		public static readonly BindableProperty LinesProperty = BindableProperty.Create(
			propertyName: "Lines",
			returnType: typeof(int),
			declaringType: typeof(MultiLineLabel),
			defaultValue: 1);

        public int Lines
        {
            get => (int)GetValue(LinesProperty);
            set => SetValue(LinesProperty, value);
        }

        public MultiLineLabel()
        {
        }
    }
}
