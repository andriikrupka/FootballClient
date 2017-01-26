using FootballClient.DataAccess.Providers;
using FootballClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FootballClient.UWP.ViewModels
{
    public class FictionListViewModel : BaseViewModel
    {
        private readonly FeedNewsProvider _feedNewsProvider;

        public FictionListViewModel(FeedNewsProvider feedNewsProvider)
        {
            _feedNewsProvider = feedNewsProvider;
            FictionsViewModels = new ObservableRangeCollection<FictionViewModel>();
        }

        public ObservableRangeCollection<FictionViewModel> FictionsViewModels { get; set; }

        public FictionViewModel SelectedViewModel { get; set; }

        private async Task<List<Category>> GeneratesCategories()
        {
            var list = new List<Category>();

            //try
            //{
            //    var categoriesResponse = await _fictionProvider.LoadFictionCategoriesAsync();
            //    list.AddRange(categoriesResponse?.Data?.FilterParam);
            //}
            //catch (Exception ex)
            //{

            //}

            if (list.Count == 0)
            {
                list.Add(new Category
                {
                    Name = "Все чтиво",
                    Code = "220"
                });
                list.Add(new Category
                {
                    Name = "Бей-беги: История",
                    Code = "207"
                });
                list.Add(new Category
                {
                    Name = "Бей-беги: Личности",
                    Code = "209"
                });
                list.Add(new Category
                {
                    Name = "Calcio dello Stivale: Герои",
                    Code = "299"
                });
                list.Add(new Category
                {
                    Name = "Calcio dello Stivale: События",
                    Code = "299"
                });

                list.Add(new Category
                {
                    Name = "Правила жизни",
                    Code = "506"
                });
                list.Add(new Category
                {
                    Name = "Бизнес и Финансы",
                    Code = "203"
                });

                list.Add(new Category
                {
                    Name = "4-4-2",
                    Code = "244"
                });

                list.Add(new Category
                {
                    Name = "Топ-11 легенд английской Премьер-лиги",
                    Code = "595"
                });


            }

            return list;
        }

        public async Task InitializeViewModelAsync()
        {
            var categories = await GeneratesCategories();
            FictionsViewModels.AddRange(categories.Select(x => new FictionViewModel(_feedNewsProvider, x)));
            SelectedViewModel = FictionsViewModels[0];
        }
    }
}
