using System.Threading;
using FootballClient.DataAccess.Request.Parsers;
using FootballClient.Models;
using System;
using System.Threading.Tasks;
using FootballClient.DataAccess.Parsers;
using System.Net.Http;

namespace FootballClient.DataAccess.Providers
{
    public class MatchesProvider
    {
        private readonly IRestClient _restClient;

        public MatchesProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<MatchesResponse> LoadMatchesAsync(DateTime date, RequestAccessMode mode)
        {
            return LoadMatchesAsync(date, mode, CancellationToken.None);
        }
        public async Task<MatchesResponse> LoadMatchesAsync(DateTime date, RequestAccessMode mode, CancellationToken cancellationToken)
        {
            var month = date.Month;
            var year = date.Year;
            var day = date.Day - 1;
            if (day == 0)
            {
                month -= 1;
                if (month == 0)
                {
                    year -= 1;
                    month = 12;
                }

                day = DateTime.DaysInMonth(date.Year, month);
            }

            var dtKey = string.Format("{0}-{1}-{2}T00:00:00", year, month, day);
            var uri = new ParameterUriBuilder("http://services.football.ua/api/FootballGameBlockMain/GameList");
            uri.Add("dt", dtKey);
            uri.Add("direction", "false");
            uri.Add("callback", "");
            uri.Add("_1413706301734", "");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri.BuildParametersUri());
            var settings = new RestSettings<MatchesResponse>()
                               .AddMode(mode)
                               .AddParser(new JsonParser<MatchesResponse>())
                               .AddRequestMessage(requestMessage);

            return await _restClient.SendAsync<MatchesResponse>(settings, null);
        }

        public async Task<MatchDetails> LoadMatchDetailsAsync(RequestAccessMode mode, string detailsLink, int gameId)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(detailsLink);

            var settings = new RestSettings<MatchDetails>()
                               .AddMode(mode)
                               .AddParser(new MatchDetailsParser())
                               .AddRequestMessage(request);

            return await _restClient.SendAsync(settings, null);
        }
    }
}
