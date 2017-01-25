using FootballClient.DataAccess.Providers;
using FootballClient.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FootballClient.UWP.ViewModels
{
    public class AuthorsListViewModel : BaseViewModel
    {
        private bool isInitialized;
        private readonly AuthorsProvider _authorsProvider;

        public AuthorsListViewModel(AuthorsProvider authorsProvider)
        {
            _authorsProvider = authorsProvider;
            AuthorsViewModels = new ObservableCollection<AuthorsViewModel>();
        }

        public ObservableCollection<AuthorsViewModel> AuthorsViewModels { get; set; }

        public AuthorsViewModel SelectedViewModel { get; set; }
        private async Task<List<Category>> GeneratesCategories()
        {
            var list = new List<Category>();

            //var categoriesResponse = await _authorsProvider.LoadAuthorsCategoriesAsync();
            //if (categoriesResponse != null
            //    && categoriesResponse.Data != null && categoriesResponse.Data.FilterParam != null)
            //{
            //    list = new List<Category>(categoriesResponse.Data.FilterParam);
            //}
            //else
            {
                #region Custom
                list.Add(new Category
                    {
                        Name = "Все авторы"
                    });
                #endregion
            }

            return list;
        }

        public async Task InitializeViewModelAsync()
        {
            if (!this.isInitialized)
            {
                this.isInitialized = true;
                var categories = await GeneratesCategories();
                foreach (var c in categories)
                {
                    AuthorsViewModels.Add(new AuthorsViewModel(_authorsProvider, c));
                }

                SelectedViewModel = AuthorsViewModels[0];
            }
        }

        private void RefreshExecute(int index)
        {
            SelectedViewModel.ReloadItemsAsync();
        }
    }
}
