using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FootballClient.UWP.Controls
{
    public class MenuRadioButton : RadioButton
    {
        public static readonly DependencyProperty PathDecodeWidthProperty =
            DependencyProperty.Register("PathDecodeWidth", typeof(int), typeof(MenuRadioButton), new PropertyMetadata(24));

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(MenuRadioButton), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty PathDataProperty =
            DependencyProperty.Register("PathData", typeof(Geometry), typeof(MenuRadioButton), new PropertyMetadata(null));

        public MenuRadioButton()
        {
            DefaultStyleKey = typeof(MenuRadioButton);
        }

        public Geometry PathData
        {
            get { return (Geometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public int PathDecodeWidth
        {
            get { return (int)GetValue(PathDecodeWidthProperty); }
            set { SetValue(PathDecodeWidthProperty, value); }
        }
    }
}
