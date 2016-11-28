using FootballClient.DataAccess.Parsers;
using FootballClient.Models;
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
        private readonly IRestClient _restClient;

        public CommentsProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<CommentsResponse> LoadComments(uint id, int pageIndex, CommentType commentType, RequestAccessMode mode = RequestAccessMode.Server)
        {
            var request = new HttpRequestMessage();

            var uriBuilder = new ParameterUriBuilder("http://services.football.ua/api/Comment/Comments");
            uriBuilder.Add("itemId", id.ToString());
            uriBuilder.Add("commentType", ((int)commentType).ToString());
            uriBuilder.Add("pageIndex", pageIndex.ToString());
            uriBuilder.Add("pageSize", "25");
            uriBuilder.Add("sort", "0");
            uriBuilder.Add("anchor", "");
            uriBuilder.Add("callback","");

            request.RequestUri = uriBuilder.BuildParametersUri();

            var parser = new JsonParser<CommentsResponse>();
            return _restClient.SendMessageAsync(request, parser, mode);
        }
    }
}

