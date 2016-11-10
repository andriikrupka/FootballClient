using Prism.Events;
using Prism.Unity.Windows;
using Prism.Windows.Mvvm;
using PropertyChanged;
using Microsoft.Practices.Unity;
using System;
using FootballClient.UWP.ViewModels.Messages;

namespace FootballClient.UWP.ViewModels
{
    [ImplementPropertyChanged]
    public abstract class BaseViewModel : ViewModelBase
    {
        protected readonly IEventAggregator _eventAggregator;

        private int _busyCount;
        protected BaseViewModel()
        {
            _eventAggregator = PrismUnityApplication.Current.Container.Resolve<EventAggregator>();
        }

        [DoNotNotify]
        protected int BusyCount
        {
            get
            {
                return _busyCount;
            }

            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Be careful, please. BusyCount can't be negative.");
                }

                var previousIsBusy = IsBusy;
                var previousCount = _busyCount;
                _busyCount = value;
                OnBusyCountChanged(previousCount, value);
                if (previousIsBusy != IsBusy)
                {
                    OnPropertyChanged(nameof(IsBusy));
                }
            }
        }

        [DoNotNotify]
        public bool IsBusy
        {
            get
            {
                return BusyCount != 0;
            }
        }

        protected virtual void OnBusyCountChanged(int previousValue, int newValue)
        {
            var change = newValue - previousValue;
            var message = new BusyCountChangeMessage();
            _eventAggregator.GetEvent<BusyCountChangeMessage>().Publish(change);
        }
    }
}
