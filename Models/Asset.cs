using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetBuddy.Models
{
    public class Asset
    {
        private int assetID;
        private int userID;
        private string symbol;
        private double purchasePrice;
        private double amount;
        private bool isCrypto;

        public int AssetID { get => assetID; set => assetID = value; }
        public int UserID { get => userID; set => userID = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public double PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
        public double Amount { get => amount; set => amount = value; }
        public bool IsCrypto { get => isCrypto; set => isCrypto = value; }
    }
}