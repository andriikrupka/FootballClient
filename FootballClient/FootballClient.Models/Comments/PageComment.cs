using System;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace FootballClient.Models
{
    [DataContract]
    public class PageComment
    {
        private readonly CultureInfo ruInfo = new CultureInfo("ru-RU");

        private const string ProfileImagePattern = "http://s.ill.in.ua/i/user/49x49/{0}/{1}";

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public int ItemTypeId { get; set; }

        [DataMember]
        public string DateCreated { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string IP { get; set; }

        [DataMember]
        public bool State { get; set; }

        [DataMember]
        public int Rate { get; set; }

        [DataMember]
        public List<Quote> Quotes { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string PhotoFilename { get; set; }

        [DataMember]
        public string UserDateCreated { get; set; }

        [DataMember]
        public string UserDateLastActivity { get; set; }

        [DataMember]
        public bool IsRatedByCurrentUser { get; set; }

        [IgnoreDataMember]
        public string UserPhoto { get; private set; }

        [IgnoreDataMember]
        public string PostedTime { get; private set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (!string.IsNullOrEmpty(this.PhotoFilename))
            {
                this.UserPhoto = string.Format(ProfileImagePattern, this.PhotoFilename.Substring(0, 2),this.PhotoFilename);
            }

            if (!string.IsNullOrEmpty(this.DateCreated))
            {
                var dateTime = DateTime.Parse(this.DateCreated);
                this.PostedTime = dateTime.ToString("dd MMMM yyyy, HH:mm", ruInfo);
            }

            if (!string.IsNullOrEmpty(this.Text))
            {
                this.Text = WebUtility.HtmlDecode(this.Text);
                //this.Text = HtmlUtilities.ConvertToText(this.Text);
            }
        }
    }
}
