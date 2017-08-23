using System;
using System.Collections.Generic;
using FootballClientForms.ViewModels;
using Xamarin.Forms;

namespace FootballClientForms.Views
{
    public class FootballNavigationPage : NavigationPage
    {
        public FootballNavigationPage() : base()
        {
            this.BarBackgroundColor = new Color(155, 25, 255, 24);
        }
    }


    public class DetailsPage : ContentPage
    {
        
    }

    public partial class NewsDetailsPage : DetailsPage
    {
        public NewsDetailsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //parallaxScrollView.ParallaxView = HeaderView;
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            //parallaxScrollView.Parallax();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
