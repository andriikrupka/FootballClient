using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FootballClient.Models
{
    public class IncrementalObservableCollection<T> : ObservableRangeCollection<T>, ISupportIncrementalLoading
    {
        private Func<Task<IList<T>>> _loandFunction;

        public IncrementalObservableCollection(Func<Task<IList<T>>> load)
            : base()
        {
            this.HasMoreItems = true;
            _loandFunction = load;
        }

        public IncrementalObservableCollection(IEnumerable<T> collection, Func<Task<IList<T>>> load)
            : base(collection)
        {
            this.HasMoreItems = true;
            _loandFunction = load;
        }

        public bool HasMoreItems
        {
            get;
            set;
        }

        public void CanMoreItems() => HasMoreItems = true;

        public Windows.Foundation.IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async c =>
            {
                var data = await _loandFunction();

                if (data.Count > 0)
                {
                    AddRange(data);
                }
                else
                {
                    HasMoreItems = false;
                }

                return new LoadMoreItemsResult()
                {
                    Count = (uint)data.Count,
                };
            });
        }
    }
}
