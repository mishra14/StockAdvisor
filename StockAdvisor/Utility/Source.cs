using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdvisor.Utility
{
    public class Source
    {
        private static string[] _smaIntervalParamValues = { "1min", "5min", "15min", "30min", "60min", "daily", "weekly", "monthly" };
        private static string[] _seriesTypeParamValues = { "close", "open", "high", "low" };
        private static string _smaIntervalParam = "interval";
        private static string _timePeriodParam = "time_period";
        private static string _seriesTypeParam = "series_type";

        public enum SmaInterval
        {
            OneMin,
            FiveMin,
            FifteenMin,
            ThirtyMin,
            Sixtymin,
            Daily,
            Weekly,
            Monthly
        }

        public enum SeriesType
        {
            Close,
            Open,
            High,
            Low
        }

        public string Name { get; set; }

        public string BaseUrl { get; set; }

        public string ApiKey { get; set; }

        public string RawDataUrl { get; set; }

        public string SmaUrl { get; set; }

        public string GetSmaUrl(SmaInterval interval, SeriesType seriesType, int count)
        {
            return $"{SmaUrl}&{_smaIntervalParam}={_smaIntervalParamValues[(int)interval]}&{_seriesTypeParam}={_seriesTypeParamValues[(int)seriesType]}&{_timePeriodParam}={count}";
        }

        public string AddSymbolToUrl(string url, string symbol)
        {
            return $"{url}&symbol={symbol}";
        }
    }
}
