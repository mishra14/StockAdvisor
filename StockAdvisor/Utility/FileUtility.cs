using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdvisor.Utility
{
    public static class FileUtility
    {
        private const string apiKey = "apikey";
        private const string baseUrl = "baseurl";
        private const string rawdataurl = "rawdataurl";
        private const string smaurl = "smaurl";
        public static Tuple<string, string> AlphaVantage = Tuple.Create("alphavantage", @"C:\Users\anmishr\Documents\API Keys\AlphaVantageAPI.json");


        public static Source GetSourceFromFile(Tuple<string, string> sourceInfo)
        {
            Source source = null;
            var sourceName = sourceInfo.Item1;
            var sourceFilePath = sourceInfo.Item2;

            if (File.Exists(sourceFilePath))
            {
                var json = JObject.Parse(File.ReadAllText(sourceFilePath));
                if (json != null && json.TryGetValue(sourceName, out var sourceJson) == true && sourceJson != null)
                {
                    source = new Source()
                    {
                        ApiKey = sourceJson.Value<string>(apiKey),
                        SmaUrl = sourceJson.Value<string>(smaurl),
                        RawDataUrl = sourceJson.Value<string>(rawdataurl),
                        BaseUrl = sourceJson.Value<string>(baseUrl),
                        Name = sourceName
                    };
                }
            }

            return source;
        }
    }
}
