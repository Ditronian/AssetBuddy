using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetBuddy.Models
{
    public class BinanceReturn
    {

        double dailyPriceChange;
        double dailyPriceChangePercent;
        double lastPrice;

        public double DailyPriceChange { get => dailyPriceChange; set => dailyPriceChange = value; }
        public double DailyPriceChangePercent { get => dailyPriceChangePercent; set => dailyPriceChangePercent = value; }
        public double LastPrice { get => lastPrice; set => lastPrice = value; }
    }
}