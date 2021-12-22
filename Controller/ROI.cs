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
    public static class ROI
    {
        private const string cryptoURL = "https://api.binance.com/api/v3/klines?symbol=";
        //https://api.binance.com/api/v3/klines?symbol=BTCUSDT&interval=1h

        public static string GetROI(string ticker)
        {

            string json = (new WebClient()).DownloadString("https://api.binance.com/api/v3/klines?symbol="+ticker+"USDT&interval=1d");
            JArray data = JsonConvert.DeserializeObject<JArray>(json);

            //Foreach Day Candle, Decode information if desired.
            foreach (JArray obj in data.Children())
            {
                long epochTime = (long)obj[0];
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(epochTime);
                DateTime dateTime = dateTimeOffset.DateTime;
                System.Diagnostics.Debug.WriteLine(dateTime.ToShortDateString());
            }




            
            return "";
        }
    }
}