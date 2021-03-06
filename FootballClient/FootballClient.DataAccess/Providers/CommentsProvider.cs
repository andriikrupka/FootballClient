﻿using FootballClient.DataAccess.Parsers;
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

        public Task<CommentsResponse> LoadCommentsAsync(int id, int pageIndex, CommentType commentType)
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

            var settings = new RestSettings<CommentsResponse>()
                              .AddMode(RequestAccessMode.Server)
                              .AddParser(new JsonParser<CommentsResponse>())
                              .AddRequestMessage(request);

            return _restClient.SendAsync(settings, null);
        }
    }
}

