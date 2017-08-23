using System;
using System.Collections.Generic;
using FootballClientForms.ViewModels;
using Xamarin.Forms;

namespace FootballClientForms.Views
{
    public partial class NewsPage : ContentPage
    {
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            
            (BindingContext as NewsPageViewModel)?.ViewDetailsCommand.Execute(e.SelectedItem as FootballClient.Models.NewsItem);
            listView.SelectedItem = null;
        }

        public NewsPage()
        {
            InitializeComponent();
            this.listView.ItemSelected+= OnListViewItemSelected;
            listView.ItemAppearing += OnListViewItemAppearing;
        }

        void OnListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var itemsCount = ViewModel.NewsItems.Count;
            var invokePart = (int)(itemsCount * 0.7);
            if (e.Item == ViewModel.NewsItems[invokePart])
            {
                ViewModel.LoadMoreCommand.Execute();
            }
        }

        public NewsPageViewModel ViewModel => BindingContext as NewsPageViewModel;
    }
}
