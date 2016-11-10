using FootballClient.DataAccess.Parsers;
using FootballClient.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballClient.DataAccess.Providers
{
    public enum CommentType
    {
        News = 1,
        Mathces = 2
    }
    public class CommentsProvider
    {
        private const string CommentsKeyPattern = "Comments_{0}_Page_{1}";

        private readonly IRestClient _restClient;

        public CommentsProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<CommentsResponse> LoadComments(uint id, int pageIndex, CommentType commentType)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://services.football.ua/api/Comment/Comments");
            request.Properties.Add("itemId", id.ToString());
            request.Properties.Add("commentType", ((int)commentType).ToString());
            request.Properties.Add("pageIndex", pageIndex.ToString());
            request.Properties.Add("pageSize", "25");
            request.Properties.Add("sort", "0");
            request.Properties.Add("anchor", "");
            request.Properties.Add("callback","");
            var parser = new JsonParser<CommentsResponse>();
            return _restClient.SendMessageAsync(request, parser);
        }
    }
}

