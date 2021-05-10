using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AssetBuddy.Models;
using System.IO;

namespace AssetBuddy.Controller
{

    //Uses an API call to grab the selected asset's current price.
    public static class PriceGrabber
    {
        private const string stockURL = "https://query1.finance.yahoo.com/v7/finance/chart/";
        private const string cryptoURL = "https://api.binance.com/api/v3/ticker/24hr?symbol=";
        //CRYPTO.COM  https://api.crypto.com/v2/public/get-ticker?instrument_name=BTC_USDT


        //Returns the current market price of the selected stock if it exists.
        //Updates about once a minute using this api.  Replace if able.
        public static double GetStockPrice(string ticker)
        {
            double marketPrice;

            try
            {
                string json = (new WebClient()).DownloadString(stockURL + ticker + "?interval=1m");
                JObject data = JsonConvert.DeserializeObject<JObject>(json);

                //You have no idea how long it took to find this magical bit of JSON code to get the market price.
                marketPrice = (double)data["chart"]["result"][0]["meta"]["regularMarketPrice"];
            }
            catch
            {
                marketPrice = 0;
            }
            

            return marketPrice;
        }

        //Returns the current market price of the selected cryptocurrency if it exists.
        public static BinanceReturn GetCryptoPrice(string ticker)
        {
            BinanceReturn binanceData = new BinanceReturn();

            try
            {
                string json = (new WebClient()).DownloadString(cryptoURL + ticker + "USDT");
                JObject data = JsonConvert.DeserializeObject<JObject>(json);

                binanceData.LastPrice = (double)data["lastPrice"];
                binanceData.DailyPriceChange = (double)data["priceChange"];
                binanceData.DailyPriceChangePercent = (double)data["priceChangePercent"];
            }
            catch
            {
                binanceData.LastPrice = 0;
                binanceData.DailyPriceChange = 0;
                binanceData.DailyPriceChangePercent = 0;
            }


            return binanceData;
        }
    }
}