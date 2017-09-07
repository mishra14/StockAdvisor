using StockAdvisor.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace StockAdvisor.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var google = new Stock("GOOG", "Google");
            var microsoft = new Stock("MSFT", "Microsoft");
            var portfolio = new Portfolio();

            portfolio.AddToWatchList(google);
            portfolio.AddToWatchList(microsoft);

            while (true)
            {
                portfolio.UpdateAndDisplayWatchList();

                // sleep for a minute
                Thread.Sleep(60000);
            }
        }
    }
}