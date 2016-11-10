//namespace FootballClient.ViewModels
//{
//    using FootballClient.DataAccess.Providers;
//    using FootballClient.Models;
//    using FootballClient.ViewModels.Navigation;
//    using GalaSoft.MvvmLight.Command;
//    using FootballClient.Core.Utils;
//    using FootballClient.Core.Navigation;
//    using FootballClient.Core.Serializer;
//    using System;
//    using System.Collections.ObjectModel;
//    using System.IO;
//    using Windows.Storage;

//    public class CommentsViewModel : BaseViewModel
//    {
//        private string title;
//        private string publishedDate;
//        private int currentIndex;
//        private int lasIndex;
//        private bool isLoadNow;
//        private CommentsPageParameters pageParameters;
//        private bool isResultEmpty;
//        private CommentType commentType;

//        public CommentsViewModel()
//        {
//            this.Comments = new ObservableCollection<PageComment>();

//            base.InitializeViewModelCommand = new RelayCommand(this.InitializeViewModelExecute);
//            this.RefreshCommand = new RelayCommand(this.RefreshExecute, this.RefreshCanExecute);
//            this.LoadNextPageCommand = new RelayCommand(this.LoadNextPageExecute, this.LoadNextPageCanExecute);
//            this.LoadPreviousPageCommand = new RelayCommand(this.LoadPreviousPageExecute, this.LoadPreviousPageCanExecute);
//        }

//        public RelayCommand LoadNextPageCommand { get; private set; }
        
//        public RelayCommand LoadPreviousPageCommand { get; private set; }

//        public RelayCommand RefreshCommand { get; private set; }

//        public ObservableCollection<PageComment> Comments { get; private set; }

//        public string Title
//        {
//            get
//            {
//                return this.title;
//            }
//            set
//            {
//                if (value != this.title)
//                {
//                    this.title = value;
//                    base.RaisePropertyChanged();
//                }
//            }
//        }

//        public bool IsResultEmpty
//        {
//            get
//            {
//                return this.isResultEmpty;
//            }
//            set
//            {
//                if (value != this.isResultEmpty)
//                {
//                    this.isResultEmpty = value;
//                    base.RaisePropertyChanged();
//                }
//            }
//        }

//        public int LastIndex
//        {
//            get { return this.lasIndex; }
//            set
//            {
//                if (value != this.lasIndex)
//                {
//                    this.lasIndex = value;
//                    base.RaisePropertyChanged();
//                }
//            }
//        }

//        public int CurrentIndex
//        {
//            get { return this.currentIndex; }
//            set
//            {
//                if (value != this.currentIndex)
//                {
//                    this.currentIndex = value;
//                    base.RaisePropertyChanged();
//                }
//            }
//        }

//        public string PublishedDate
//        {
//            get
//            {
//                return this.publishedDate;
//            }
//            set
//            {
//                if (value != this.publishedDate)
//                {
//                    this.publishedDate = value;
//                    base.RaisePropertyChanged();
//                }
//            }
//        }


//        private async void InitializeViewModelExecute()
//        {
//            var parameters = NavigationProvider.Instance.CurrentParameters as CommentsPageParameters;

//            //parameters = new CommentsPageParameters()
//            //{
//            //    Title = @"ХЕНДЕРСОН: ""ЛЮБЛЮ ЛИВЕРПУЛЬ И ХОЧУ ОСТАТЬСЯ НА ДОЛГИЕ ГОДЫ""",
//            //    PublishedDate = "08 ОКТЯБРЯ 2014, 12:26"
//            //};

//            //this.Title = parameters.Title;
//            //this.PublishedDate = parameters.PublishedDate;

//            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///CommentsData.txt"));
//            //var streamRandom = await file.OpenReadAsync();

//            //var serializer = new JsonSerializer();
//            //var items = (CommentsResponse) serializer.Deserialize<CommentsResponse>(streamRandom.AsStream());
//            //foreach (var item in items.PageComments)
//            //{
//            //    this.Comments.Add(item);
//            //}

//            if (parameters.IsNotNull())
//            {
//                this.pageParameters = parameters;
//                this.Title = parameters.Title;
//                this.publishedDate = parameters.PublishedDate;
//                this.commentType = parameters.CommentType;
//                this.LoadData(parameters.NewsId, this.currentIndex, isNeeedClear: false);
//            }
//        }

//        private async void LoadData(uint postId, int pageIndex, bool isNeeedClear = true)
//        {
//            base.BusyCount++;
//            this.isLoadNow = true;
//            this.IsResultEmpty = false;
//            this.LoadNextPageCommand.RaiseCanExecuteChanged();
//            this.LoadPreviousPageCommand.RaiseCanExecuteChanged();
//            this.RefreshCommand.RaiseCanExecuteChanged();

//            var commentsResponse = await DataProviders.CommentsProvider.LoadComments(postId, pageIndex, commentType);
//            if (commentsResponse.IsSuccess)
//            {
//                if (commentsResponse.Result.IsNotNull() )
//                {
//                    this.CurrentIndex = commentsResponse.Result.PagerCurrent;
                    
//                    if (commentsResponse.Result.PageComments.IsNotNull())
//                    {
//                        this.LastIndex = commentsResponse.Result.TotalCount / 25;
//                        if (isNeeedClear)
//                        {
//                            this.Comments.Clear();
//                        }

//                        foreach (var item in commentsResponse.Result.PageComments)
//                        {
//                            this.Comments.Add(item);
//                        }
//                    }
//                }
//                else
//                {

//                }
//            }

//            this.isLoadNow = false;
//            this.IsResultEmpty = this.Comments.Count == 0;
//            this.LoadNextPageCommand.RaiseCanExecuteChanged();
//            this.LoadPreviousPageCommand.RaiseCanExecuteChanged();
//            this.RefreshCommand.RaiseCanExecuteChanged();

//            base.BusyCount--;
//        }

//        private void RefreshExecute()
//        {
//            this.LoadData(this.pageParameters.NewsId, this.currentIndex);
//        }

//        private bool RefreshCanExecute()
//        {
//            return !this.isLoadNow;
//        }

//        private void LoadPreviousPageExecute()
//        {
//            this.LoadData(this.pageParameters.NewsId, this.currentIndex - 1);
//        }

//        private bool LoadPreviousPageCanExecute()
//        {
//            return this.CurrentIndex > 0 && !this.isLoadNow;
//        }

//        private void LoadNextPageExecute()
//        {
//            this.LoadData(this.pageParameters.NewsId, this.currentIndex + 1);
//        }

//        private bool LoadNextPageCanExecute()
//        {
//            return this.CurrentIndex < this.LastIndex && !this.isLoadNow;
//        }
//    }
//}
