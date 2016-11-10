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
        private readonly FictionProvider _fictionProvider;

        public FictionListViewModel(FictionProvider fictionProvider)
        {
            _fictionProvider = fictionProvider;
            FictionsViewModels = new ObservableRangeCollection<FictionViewModel>();
            
        }

        public ObservableRangeCollection<FictionViewModel> FictionsViewModels { get; set; }

        public FictionViewModel SelectedViewModel { get; set; }

        private async Task<List<Category>> GeneratesCategories()
        {
            var list = new List<Category>();

            try
            {
                var categoriesResponse = await _fictionProvider.LoadFictionCategoriesAsync();
                list.AddRange(categoriesResponse?.Data?.FilterParam);
            }
            catch (Exception ex)
            {

            }

            if (list.Count == 0)
            {
                list.Add(new Category
                {
                    Name = "Все чтиво"
                });
            }
            
            return list;
        }

        public async Task InitializeViewModelAsync()
        {
            var categories = await GeneratesCategories();
            FictionsViewModels.AddRange(categories.Select(x => new FictionViewModel(_fictionProvider, x)));
            SelectedViewModel = FictionsViewModels[0];
        }
    }
}
