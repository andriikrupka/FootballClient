using System;
using System.Collections.Generic;
using FootballClient.DataAccess.Providers;
using FootballClient.Models;
using Prism.Mvvm;
using Prism.Navigation;

namespace FootballClientForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CommentsPageViewModel : BindableBase, INavigationAware
    {
        private readonly CommentsProvider commentsProvider;

        public CommentsPageViewModel(CommentsProvider commentsProvider)
        {
            this.commentsProvider = commentsProvider;
        }

		public string Title { get; set; }
        public string DatePublish { get; set; }
        public List<PageComment> CommentItems { get; private set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Id"))
            {
                var id = parameters.GetValue<int>("Id");
                var type = parameters.GetValue<CommentType>("Type");
                Title = parameters.GetValue<string>("Title");
                DatePublish = parameters.GetValue<string>("PublishedDateString");

                var comments = await commentsProvider.LoadCommentsAsync(id, 0, type);
                if (comments != null)
                {
                    CommentItems = comments.PageComments;
                }

				//RaisePropertyChanged(nameof(Title));
				//RaisePropertyChanged(nameof(CommentItems));
            }
        }
    }
}
