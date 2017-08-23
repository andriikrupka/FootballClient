using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace FootballClientForms.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public NewsPageViewModel NewsPageViewModel { get; }
        public MainPageViewModel(NewsPageViewModel newsPageViewModel)
        {
            NewsPageViewModel = newsPageViewModel;
        }


    }
}
