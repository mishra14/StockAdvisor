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
        public IDictionary<string, StockData> WatchListStocks{ get; private set; } = new Dictionary<string, StockData>(StringComparer.OrdinalIgnoreCase);

        public Market Market { get; private set; } = new Market();

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
                WatchListStocks[stock.Symbol] = new StockData(stock);
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
            //UpdateWatchListCurrentPrice();
            UpdateWatchListSMA();
        }

        public void UpdateWatchListSMA()
        {
            // Update the watch list
            Market.UpdateSMA(WatchListStocks.Values.ToList());
        }

        public void UpdateWatchListCurrentPrice()
        {
            // Update the watch list
            Market.UpdateCurrentPrice(WatchListStocks.Values.ToList());
        }

        public void DisplayWatchList()
        {
            Console.WriteLine("=====================================================================");
            Console.WriteLine($"Time: {DateTimeOffset.UtcNow}");
            foreach (var symbol in WatchListStocks.Keys)
            {
                PrintPrice(WatchListStocks[symbol]);
            }
            Console.WriteLine("=====================================================================");
        }

        private void PrintPrice(StockData stockData)
        {
            Console.Write(stockData.GetPrintData());
        }
    }
}
