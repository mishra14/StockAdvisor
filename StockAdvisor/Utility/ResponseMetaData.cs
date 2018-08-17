using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdvisor.Utility
{
    public class ResponseMetadata
    {
        public string Symbol { get; set; }

        public string FunctionType { get; set; }

        public DateTimeOffset LastRefreshed { get; set; }

        public string Interval { get; set; }

        public string TimePeriod { get; set; }

        public string SeriesType { get; set; }

        public string TimeZone { get; set; }
    }
}
