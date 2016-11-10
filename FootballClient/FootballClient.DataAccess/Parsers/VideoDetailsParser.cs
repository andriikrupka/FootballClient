using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClient.DataAccess.Request.Parsers
{
    //public class VideoDetailsParser : Parser<string>
    //{
    //    private const string StartLink = "http://footballua.tv/player?h=";

    //    public override Response<string> ParseSafe(Stream stream)
    //    {
    //        var response = new Response<string>();

    //        using (var streamReader = new StreamReader(stream))
    //        {
    //            var stringData = streamReader.ReadToEnd();
    //            var index = stringData.IndexOf(StartLink);
    //            if (index > 0)
    //            {
    //                response.Result = stringData.Substring(index + StartLink.Length, 32);
    //            }
    //        }

    //        return response;
    //    }
    //}
}
