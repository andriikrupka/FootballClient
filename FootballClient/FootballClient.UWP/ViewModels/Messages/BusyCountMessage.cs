using Prism.Events;

namespace FootballClient.UWP.ViewModels.Messages
{
    public class BusyCountChangeMessage : PubSubEvent<int>
    {
        public BusyCountChangeMessage()
        {
            
        }
    }
}
