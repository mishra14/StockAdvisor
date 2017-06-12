using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdvisor.Utility
{
    public class Portfolio
    {
        // Symbol -> StockData
        public IDictionary<string, StockData> PortfolioStocks { get; private set; } = new Dictionary<string, StockData>(StringComparer.OrdinalIgnoreCase);

        // Symbol -> Price
        public IDictionary<string, double> WatchListStocks{ get; private set; } = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Update the portfolio on stock buy.
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="count"></param>
        /// <param name="averageCost"></param>
        /// <returns>
        ///  0 -> success
        ///  others -> error
        /// </returns>
        public int UpdateOnBuy(Stock stock, int count, double averageCost)
        {
            if (!PortfolioStocks.ContainsKey(stock.Symbol))
            {
                PortfolioStocks[stock.Symbol] = new StockData(stock);
            }

            return PortfolioStocks[stock.Symbol].UpdateOnBuy(count, averageCost);
        }

        /// <summary>
        /// Update the portfolio on stock sale.
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="count"></param>
        /// <param name="averagePrice"></param>
        /// <returns>
        ///  0 -> success
        /// -1 -> Protfolio does not contain the stock
        ///  other -> error
        /// </returns>
        public int UpdateOnSell(Stock stock, int count, double averagePrice)
        {
            var result = -1;

            if (PortfolioStocks.ContainsKey(stock.Symbol))
            {
                result =  PortfolioStocks[stock.Symbol].UpdateOnBuy(count, averagePrice);
            }

            return result;
        }


        public void AddToWatchList(Stock stock)
        {
            if (!WatchListStocks.ContainsKey(stock.Symbol))
            {
                WatchListStocks[stock.Symbol] = 0;
            }
        }

        public void RemoveFromWatchList(Stock stock)
        {
            if (WatchListStocks.ContainsKey(stock.Symbol))
            {
                WatchListStocks.Remove(stock.Symbol);
            }
        }

        public void UpdateAndDisplayWatchList()
        {
            UpdateWatchList();
            DisplayWatchList();
        }

        public void UpdateWatchList()
        {
            
            // Update the watch list
            WatchListStocks = Market.GetCurrentPrice(WatchListStocks.Keys.ToList());
        }

        public void DisplayWatchList()
        {
            Console.WriteLine("=====================================================================");
            Console.WriteLine($"Time: {DateTimeOffset.UtcNow}");
            foreach (var symbol in WatchListStocks.Keys)
            {
                PrintPrice(symbol, WatchListStocks[symbol]);
            }
            Console.WriteLine("=====================================================================");
        }

        private void PrintPrice(string company, double price)
        {
            Console.WriteLine(string.Format($"{company}: {price}", company, price));
        }
    }
}
