using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockAdvisor.Utility
{
    public class Market
    {
        private static readonly string BaseRequestUrl = @"http://finance.google.com/finance/info?client=ig&q=NASDAQ%3A";
        private static DateTimeOffset LastCall { get; set; } = DateTimeOffset.UtcNow.AddMinutes(-1);

        public static double GetCurrentPrice(Stock stock)
        {
            var requestUrl = BaseRequestUrl + stock.Symbol;

            var jArray = GetJsonResponse(requestUrl);

            var stockData = jArray[0];

            var hasPrice = Double.TryParse(stockData["l"].Value<string>(), out double price);

            return hasPrice ? price: -1 ;
        }

        public static IDictionary<string, double> GetCurrentPrice(IEnumerable<Stock> stocks)
        {
            var result = new Dictionary<string, double>();
            if (stocks.Any())
            {
                StringBuilder requestUrl = new StringBuilder();

                requestUrl.Append(BaseRequestUrl);

                foreach (var stock in stocks)
                {
                    requestUrl.Append(stock.Symbol);
                    requestUrl.Append(",");
                }

                var jArray = GetJsonResponse(requestUrl.ToString());

                foreach (var stockData in jArray)
                {
                    var symbol = stockData["t"].Value<string>();
                    var hasPrice = Double.TryParse(stockData["l"].Value<string>(), out double price);

                    if (hasPrice)
                    {
                        result.Add(symbol, price);
                    }
                    else
                    {
                        result.Add(symbol, -1);
                    }
                }

            }

            return result;
        }

        public static IDictionary<string, double> GetCurrentPrice(IEnumerable<string> stockSymbols)
        {
            var result = new Dictionary<string, double>();
            if (stockSymbols.Any())
            {
                StringBuilder requestUrl = new StringBuilder();

                requestUrl.Append(BaseRequestUrl);

                foreach (var stock in stockSymbols)
                {
                    requestUrl.Append(stock);
                    requestUrl.Append(",");
                }

                var jArray = GetJsonResponse(requestUrl.ToString());

                foreach (var stockData in jArray)
                {
                    var symbol = stockData["t"].Value<string>();
                    var hasPrice = Double.TryParse(stockData["l"].Value<string>(), out double price);

                    if (hasPrice)
                    {
                        result.Add(symbol, price);
                    }
                    else
                    {
                        result.Add(symbol, -1);
                    }
                }

            }

            return result;
        }

        private static JArray GetJsonResponse(string requestUrl)
        {
            var response = GetResponse(requestUrl);

            var jArray = ParseRespone(response);

            return jArray;
        }

        private static string GetResponse(string requestUrl)
        {
            string json = string.Empty;

            WaitForMinInterval();

            using (var web = new WebClient())
            {
                json = web.DownloadString(requestUrl);
            }

            json = json.Replace("//", "");

            return json;
        }

        private static JArray ParseRespone(string response)
        {
            return JArray.Parse(response);
        }

        private static void WaitForMinInterval()
        {
            var diff = DateTimeOffset.UtcNow - LastCall;

            if (diff.Minutes < 1)
            {
                // Google API can ban the source IP if its hit more than once per minute.
                var waitTime = new TimeSpan(hours: 0, minutes: 1, seconds: 0) - diff;
                Thread.Sleep(waitTime);
            }

            LastCall = DateTimeOffset.UtcNow;
        }
    }
}
