using System;
using System.Collections.Generic;
using System.Text;

namespace StockAdvisor.Utility
{
    public class Stock
    {
        public string Symbol { get; }
        public string Company { get; }

        public Stock(string symbol, string company)
        {
            Symbol = symbol;
            Company = company;
        }
    }
}
