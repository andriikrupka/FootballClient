using FootballClient.DataAccess.Providers;
using FootballClient.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FootballClient.UWP.ViewModels
{
    public class NewsListViewModel : BaseViewModel
    {
        private FeedNewsProvider _feedNewsProvider;

        public NewsListViewModel(FeedNewsProvider feedNewsProvider)
        {
            _feedNewsProvider = feedNewsProvider;
            RefreshCommand = new DelegateCommand(RefreshExecute);

            var categories = GeneratesCategories();
            NewsViewModels = new ObservableCollection<NewsViewModel>(categories.Select(x=> new NewsViewModel(_feedNewsProvider, x)));
            SelectedViewModel = NewsViewModels[0];
        }

        public DelegateCommand RefreshCommand { get; private set; }

        public ObservableCollection<NewsViewModel> NewsViewModels { get; set; }

        public NewsViewModel SelectedViewModel { get; set; }

        private List<Category> GeneratesCategories()
        {
            var list = new List<Category>();
            #region Custom
            list.Add(new Category
            {
                Name = "Все новости"
            });
            list.Add(new Category
            {
                Name = "ЕВРО-2016",
                Code = "football_euro2016_newsi"
            });
            list.Add(new Category
            {
                Name = "Украина",
                Code = "football_ukraine_newsi"
            });

            list.Add(new Category
            {
                Name = "Англия",
                Code = "football_england_newsi"
            });

            list.Add(new Category
            {
                Name = "Аргентина",
                Code = "football_argentina_newsi"
            });

            list.Add(new Category
            {
                Name = "Бразилия",
                Code = "football_brazil_newsi"
            });

            list.Add(new Category
            {
                Name = "Германия",
                Code = "football_germany_newsi"
            });

            list.Add(new Category
            {
                Name = "Италия",
                Code = "football_italy_newsi"
            });

            list.Add(new Category
            {
                Name = "Испания",
                Code = "football_spain_newsi"
            });

            list.Add(new Category
            {
                Name = "Нидерланды",
                Code = "football_netherlands_newsi"
            });

            list.Add(new Category
            {
                Name = "Португалия",
                Code = "football_portugal_newsi"
            });

            list.Add(new Category
            {
                Name = "Северная Америка",
                Code = "football_northamerica_newsi"
            });

            list.Add(new Category
            {
                Name = "Турция",
                Code = "football_turkey_newsi"
            });

            list.Add(new Category
            {
                Name = "Франция",
                Code = "football_france_newsi"
            });

            list.Add(new Category
            {
                Name = "Другие страны",
                Code = "football_countrieselse_newsi"
            });

            list.Add(new Category
            {
                Name = "Лига чемпионов",
                Code = "football_champ_newsi"
            });

            list.Add(new Category
            {
                Name = "Лига Европы",
                Code = "football_europaleague_newsi"
            });

            list.Add(new Category
            {
                Name = "Копа Либертадорес",
                Code = "football_copalibertadores_newsi"
            });

            list.Add(new Category
            {
                Name = "ЧМ-2014",
                Code = "football_worldcup2014_newsi"
            });
            #endregion

            return list;
        }

        private void RefreshExecute()
        {
            SelectedViewModel.ReloadItemsAsync();
        }
    }
}