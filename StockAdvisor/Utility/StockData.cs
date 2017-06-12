using System;
using System.Collections.Generic;
using System.Text;

namespace StockAdvisor.Utility
{
    public class StockData
    {
        public Stock Stock { get; private set; }

        public int Count { get; private set; }

        public double AverageCost {get; private set; }

        public double TotalProfit { get; private set; }

        public StockData(Stock stock)
        {
            Stock = stock;
        }

        public int UpdateOnBuy(int count, double averageCost)
        {
            AverageCost = (AverageCost * Count + averageCost * count) / (Count + count);
            Count += count;

            return 0;
        }

        public int UpdateOnSell(int count, double averagePrice)
        {
            TotalProfit += ((averagePrice - AverageCost) * count);
            Count -= count;

            return 0;
        }
    }
}
