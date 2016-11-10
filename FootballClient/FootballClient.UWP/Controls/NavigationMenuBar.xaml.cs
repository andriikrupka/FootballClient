using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FootballClient.UWP.Controls
{
    public sealed partial class NavigationMenuBar : UserControl
    {
        public NavigationMenuBar()
        {
            this.InitializeComponent();
            adaptiveGridView.Loaded += AdaptiveGridView_Loaded;
        }

        private void AdaptiveGridView_Loaded(object sender, RoutedEventArgs e)
        {
            //var root = adaptiveGridView.ItemsPanelRoot as ItemsWrapGrid;
            //if (root != null)
            //{
            //    root.Orientation = Orientation.Vertical;
            //}
        }
    }
}
