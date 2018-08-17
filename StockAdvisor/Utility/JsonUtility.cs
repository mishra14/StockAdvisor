using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdvisor.Utility
{
    public static class JsonUtility
    {
        private const string METADATA = "Meta Data";
        private const string SYMBOL = "1: Symbol";
        private const string FUNCTION_TYPE = "2: Indicator";
        private const string LAST_REFRESHED = "3: Last Refreshed";
        private const string INTERVAL = "4: Interval";
        private const string TIME_PERIOD = "5: Time Period";
        private const string SERIES_TYPE = "6: Series Type";
        private const string TIMEZONE = "7: Time Zone";
        private const string SMA = "SMA";

        public static Response ParseJsonForStockResponse(JObject json)
        {
            var keys = json.ToObject<Dictionary<string, object>>()
                .Keys
                .ToList();

            var metadata = json[keys[0]].ToObject<Dictionary<string, string>>();
            var data = json[keys[1]].ToObject<Dictionary<string, Dictionary<string, string>>>();

            var responseMetadata = new ResponseMetadata()
            {
                Symbol = metadata[SYMBOL],
                FunctionType = metadata[FUNCTION_TYPE],
                LastRefreshed = DateTimeOffset.Parse(metadata[LAST_REFRESHED]),
                Interval = metadata[INTERVAL],
                SeriesType = metadata[SERIES_TYPE],
                TimePeriod = metadata[TIME_PERIOD],
                TimeZone = metadata[TIMEZONE]
            };

            var responseData = new List<Tuple<DateTimeOffset, double>>();

            foreach(var pair in data)
            {
                var dataTuple = Tuple.Create(DateTimeOffset.Parse(pair.Key), Double.Parse(pair.Value[SMA]));
                responseData.Add(dataTuple);
            }

            return new Response(responseMetadata, responseData);
        }
    }
}
