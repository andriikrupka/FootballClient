namespace FootballClient.Models
{
    using System.Net;
    using System.Runtime.Serialization;

    [DataContract]
    public class Quote
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Text { get; set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                this.Text = WebUtility.HtmlDecode(this.Text);
                //this.Text = HtmlUtilities.ConvertToText(this.Text);
            }
        }
    }
}
