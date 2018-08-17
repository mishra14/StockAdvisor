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
        public Source Source { get; set; }

        private DateTimeOffset LastCall { get; set; }

        public Market()
        {
            Source = FileUtility.GetSourceFromFile(FileUtility.AlphaVantage);
            LastCall = DateTimeOffset.UtcNow.AddMinutes(-1);
        }

        // Current market price
        public void UpdateCurrentPrice(IList<StockData> stockSymbols)
        {
            foreach (var stock in stockSymbols)
            {
                UpdateCurrentPrice(stock);
            }
        }

        // Current market price
        public void UpdateCurrentPrice(StockData stock)
        {
            var requestUrl = Source.AddSymbolToUrl(Source.RawDataUrl, stock.Stock.Symbol);
            var jObject = GetJsonResponse(requestUrl.ToString());

        }

        // Simple Moving Average
        public void UpdateSMA(IList<StockData> stockSymbols)
        {
            foreach (var stockSymbol in stockSymbols)
            {
                UpdateSMA(stockSymbol);
            }
        }

        // Simple Moving Average
        public void UpdateSMA(StockData stock)
        {
            var smaUrl = Source.GetSmaUrl(Source.SmaInterval.Daily, Source.SeriesType.Close, dataPointCount: 50);
            var requestUrl = Source.AddSymbolToUrl(smaUrl, stock.Stock.Symbol);
            var response = JsonUtility.ParseJsonForStockResponse(GetJsonResponse(requestUrl.ToString()));

            if (response != null)
            {
                stock.UpdateSma(response);
            }
        }

        private void WaitForMinInterval()
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

        private JObject GetJsonResponse(string requestUrl)
        {
            var response = GetResponse(requestUrl);

            var jObject = JObject.Parse(response);

            return jObject;
        }

        private string GetResponse(string requestUrl)
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
    }
}
