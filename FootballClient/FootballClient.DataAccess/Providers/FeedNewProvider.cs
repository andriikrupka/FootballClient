using System;
using FootballClient.DataAccess.Request;
using FootballClient.DataAccess.Request.Parsers;
using FootballClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;
using System.Text;
using System.Linq;
using System.Globalization;

namespace FootballClient.DataAccess.Providers
{
    public class ParameterUriBuilder : IEnumerable<KeyValuePair<string, string>>
    {
        private string _baseAddress;
        private readonly List<KeyValuePair<string, string>> _collection;

        public ParameterUriBuilder(string baseAddress)
        {
            _baseAddress = baseAddress;
            _collection = new List<KeyValuePair<string, string>>();
        }

        public void Add(string key, string value)
        {
            _collection.Add(new KeyValuePair<string, string>(key, value));
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_collection).GetEnumerator();
        }

        public Uri BuildParametersUri()
        {
            var linkBuilder = new StringBuilder();
            if (_collection.Any())
            {

                if (!_baseAddress.Contains("?"))
                {
                    var lastLinkSymbol = _baseAddress[_baseAddress.Length - 1];

                    if (lastLinkSymbol == '/')
                    {
                        _baseAddress = _baseAddress.Substring(0, _baseAddress.Length - 1);
                    }

                    linkBuilder.Append("?");
                }
                else
                {
                    linkBuilder.Append("&");
                }

                foreach (var parameter in _collection)
                {
                    linkBuilder.AppendFormat(CultureInfo.InvariantCulture, parameter.Key + "=" + Uri.EscapeDataString(parameter.Value) + "&");
                }

                linkBuilder.Remove(linkBuilder.Length - 1, 1);
            }

            return new Uri(_baseAddress + linkBuilder.ToString());
        }
    }
    public class FeedNewsProvider
    {
        private const string FeedNewsPattern = "FeedNews_{0}_{1}";
        private const string FeedCategories = "FeedCategories";
        private readonly IRestClient _restClient;

        public FeedNewsProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<List<FeedItem>> LoadFeedNewsAsync(FeedItem lastFeedItem = null, string filterCode = "", RequestAccessMode mode = RequestAccessMode.Server)
        {
            var request = new HttpRequestMessage();
            var parser = new RssFeedParser();

            var uriBuilder = new ParameterUriBuilder("http://football.ua/handlers/stanfy/news.ashx");
            if (lastFeedItem != null)
            {
                var dateTimeOffset = lastFeedItem.Date.ToString("dd.MM.yyyy HH:mm:sszzz");
                    dateTimeOffset = dateTimeOffset.Remove(dateTimeOffset.LastIndexOf(":", StringComparison.Ordinal), 1);
                uriBuilder.Add("before", dateTimeOffset);
            }

            if (!string.IsNullOrEmpty(filterCode))
            {
                uriBuilder.Add("filterCode", filterCode);
            }
            request.RequestUri = uriBuilder.BuildParametersUri();

            return _restClient.SendMessageAsync(request, parser, mode);
        }

        public Task<ResponseCategory> LoadFeedCategoriesAsync()
        {
            var request = new HttpRequestMessage();
            var parser = new XmlParser<ResponseCategory>();
            request.RequestUri = new Uri("http://football.ua/handlers/stanfy/newsfilters.ashx?type=lenta");

            return _restClient.SendMessageAsync(request, parser);
        }
    }
}