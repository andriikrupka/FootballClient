namespace FootballClient.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ExpirationRate
    {
        [DataMember]
        public bool IsAlreadyDisplayed { get; set; }

        [DataMember]
        public int LaunchesCount { get; set; }

        [DataMember]
        public string ApplicationVersion { get; set; }
    }
}
