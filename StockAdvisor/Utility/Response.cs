using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdvisor.Utility
{
    public class Response
    {
        public ResponseMetadata Metadata { get; private set; }

        public List<Tuple<DateTimeOffset, double>> Data { get; private set; }

        public Response(ResponseMetadata metadata, List<Tuple<DateTimeOffset, double>> data)
        {
            Metadata = metadata;
            Data = data;
        }
    }
}
