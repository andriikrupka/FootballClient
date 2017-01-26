using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
}
