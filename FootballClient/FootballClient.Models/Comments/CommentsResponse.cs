using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FootballClient.Models
{
    [DataContract]
    public class CommentsResponse
    {
        [DataMember]
        public string Now { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public bool IsCommentingAllowed { get; set; }

        [DataMember]
        public bool UserIsLoginned { get; set; }

        [DataMember]
        public bool UserIsBanned { get; set; }

        [DataMember]
        public bool UserIsLockedOut { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserPhotoFilename { get; set; }

        [DataMember]
        public bool UserIsBanAllowed { get; set; }

        [DataMember]
        public bool UserIsComplainAllowed { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public object TopComments { get; set; }

        [DataMember]
        public List<PageComment> PageComments { get; set; }

        [DataMember]
        public bool PagerIsShow { get; set; }

        [DataMember]
        public int PagerCurrent { get; set; }

        [DataMember]
        public int PagerLast { get; set; }

        [DataMember]
        public object PagerPages { get; set; }

        [DataMember]
        public bool UserIsCanComment { get; set; }
    }
}
