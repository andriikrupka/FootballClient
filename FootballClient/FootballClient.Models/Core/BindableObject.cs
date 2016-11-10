using System.Runtime.Serialization;

namespace FootballClient.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [DataContract]
    public class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
