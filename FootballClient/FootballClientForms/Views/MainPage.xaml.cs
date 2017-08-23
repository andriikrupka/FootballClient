using System;
using System.Collections.Generic;
using Prism.Navigation;
using Xamarin.Forms;

namespace FootballClientForms.Views
{
    public partial class MainPage : TabbedPage, INavigatingAware
    {
        public MainPage()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

        public void OnNavigatingTo(NavigationParameters parameters)
        {
			foreach (var child in Children)
			{
				(child as INavigatingAware)?.OnNavigatingTo(parameters);
				(child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
			}
        }
    }
}
