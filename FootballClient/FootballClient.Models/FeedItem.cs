using System.Globalization;

namespace FootballClient.Models
{
    using SQLiteAttributes;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("FeedItems")]
    public class FeedItem : BindableObject
    {
        private readonly CultureInfo ruInfo = new CultureInfo("ru-RU");
        private string _thumbnailImage;
        private bool isReaded;

        [DataMember]
        [Column(nameof(Title))]
        public string Title { get; set; }

        [DataMember]
        [Column(nameof(Link))]
        public string Link { get; set; }

        [DataMember]
        [Column(nameof(PubDate))]
        public string PubDate { get; set; }

        [IgnoreDataMember]
        [Ignore]
        public DateTime Date
        {
            get
            {
                return DateTime.Parse(this.PubDate);
            }
        }

        [IgnoreDataMember]
        [Ignore]
        public string PublishedDate
        {
            get
            {
                return this.Date.ToString("dd MMMM yyyy, HH:mm", ruInfo);
            }
        }


        [DataMember]
        [PrimaryKey]
        [Indexed]
        [Column(nameof(Id))]
        public uint Id { get; set; }

        [DataMember]
        public string Annotation { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public byte Highlighted { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Thumbnail { get; set; }


        [DataMember]
        public string ThumbnailSmall { get; set; }


        [DataMember]
        public string ThumbnailBig { get; set; }


        [DataMember]
        public string ThumbnailLarge { get; set; }

        [IgnoreDataMember]
        public bool IsReaded
        {
            get
            {
                return isReaded;
            }
            set
            {
                if (value != isReaded)
                {
                    isReaded = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public string DescriptionImage { get; set; }

        [IgnoreDataMember]
        public string ThumbnailImage
        {
            get
            {
                if (string.IsNullOrEmpty(_thumbnailImage))
                {
                    if (ThumbnailLarge != null && ThumbnailLarge.Contains("280x186"))
                    {
                        _thumbnailImage = ThumbnailLarge.Replace("280x186", "630x373");
                    }
                    else
                    {
                        _thumbnailImage = ThumbnailLarge;
                    }
                }

                return _thumbnailImage;
            }
        }


        [DataMember]
        public string DescriptionImagenote { get; set; }

        public override bool Equals(object obj)
        {
            var isEquals = false;

            var someItem = obj as FeedItem;
            if (someItem != null)
            {
                if (someItem.Link == this.Link)
                {
                    isEquals = true;
                }
            }

            return isEquals;
        }

        public override int GetHashCode()
        {
            return Link != null ? Link.GetHashCode() : 0;
        }
    }
}
