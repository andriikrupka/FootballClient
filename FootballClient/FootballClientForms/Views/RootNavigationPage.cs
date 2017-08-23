using System;
using Xamarin.Forms;

namespace FootballClientForms.Views
{
    public class RootNavigationPage : NavigationPage
    {
        public RootNavigationPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
