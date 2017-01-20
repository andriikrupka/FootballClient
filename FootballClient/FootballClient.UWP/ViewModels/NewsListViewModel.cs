using FootballClient.DataAccess.Providers;
using FootballClient.Models;
using Newtonsoft.Json;
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
                Name = "Все новости",
                Code = "1"
            });
            
            list.Add(new Category
            {
                Name = "Украина",
                Code = "4"
            });
            list.Add(new Category
            {
                Name = "Украина. Лига 1",
                Code = "12"
            });
            list.Add(new Category
            {
                Name = "Англия",
                Code = "66"
            });

            list.Add(new Category
            {
                Name = "Аргентина",
                Code = "96"
            });

            list.Add(new Category
            {
                Name = "Бразилия",
                Code = "70"
            });

            list.Add(new Category
            {
                Name = "Германия",
                Code = "74"
            });

            list.Add(new Category
            {
                Name = "Италия",
                Code = "113"
            });

            list.Add(new Category
            {
                Name = "Испания",
                Code = "121"
            });

            list.Add(new Category
            {
                Name = "Нидерланды",
                Code = "129"
            });

            list.Add(new Category
            {
                Name = "Португалия",
                Code = "168"
            });

            list.Add(new Category
            {
                Name = "Северная Америка",
                Code = "184"
            });

            list.Add(new Category
            {
                Name = "Турция",
                Code = "197"
            });

            list.Add(new Category
            {
                Name = "Франция",
                Code = "189"
            });

            list.Add(new Category
            {
                Name = "Другие страны",
                Code = "181"
            });

            list.Add(new Category
            {
                Name = "Лига чемпионов",
                Code = "580"
            });

            list.Add(new Category
            {
                Name = "Лига Европы",
                Code = "105"
            });

            list.Add(new Category
            {
                Name = "Копа Либертадорес",
                Code = "156"
            });

            list.Add(new Category
            {
                Name = "ЧМ-2018",
                Code = "569"
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