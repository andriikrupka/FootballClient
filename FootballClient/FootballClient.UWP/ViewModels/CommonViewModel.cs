using FootballClient.UWP.ViewModels.Messages;

namespace FootballClient.UWP.ViewModels
{
    public class CommonViewModel : BaseViewModel
    {
        public CommonViewModel()
        {
            _eventAggregator.GetEvent<BusyCountChangeMessage>().Subscribe(ChangeBusyCounter);
        }

        private void ChangeBusyCounter(int changeValue)
        {
            BusyCount += changeValue;
        }

        protected override void OnBusyCountChanged(int previousValue, int newValue)
        {
            // Nothing to do. This allow not send message about busy count change back to model. 
        }
    }
}
