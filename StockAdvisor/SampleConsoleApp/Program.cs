using StockAdvisor.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockAdvisor.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var google = new Stock("GOOG", "Google");
            var microsoft = new Stock("MSFT", "Microsoft");
            while (true)
            {
                Console.WriteLine("=====================================================================");
                var prices = Market.GetCurrentPrice(new List<Stock> { google, microsoft });

                Console.WriteLine($"Time: {DateTimeOffset.UtcNow}");
                PrintPrice(google.Company, prices[google.Symbol]);
                PrintPrice(microsoft.Company, prices[microsoft.Symbol]);
            }
        }

        static void PrintPrice(string company, double price)
        {
            Console.WriteLine(string.Format($"{company}: {price}", company, price));
        }
    }
}