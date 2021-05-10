using AssetBuddy.Models;
using AssetBuddy.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace AssetBuddy.Private
{
    public partial class Home : System.Web.UI.Page
    {
        User user;
        int userID;
        int selectedAssetID = -1;
        double initialInvestment;
        double currentInvestment;
        double initialStock;
        double currentStock;
        double initialCrypto;
        double currentCrypto;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Gate Keeper
            if (Session["user"] == null)
            {
                Session["msg"] = "You must be logged in to access this page.";
                Response.Redirect("~/Default.aspx");
            }
            else userID = ((User)Session["user"]).UserID;

            //Needed to preserve which radio button was checked.
            if (Request.Form["selectedAsset"] != null)
            {
                selectedAssetID = Int32.Parse(Request.Form["selectedAsset"]);
            }

            //Display a popup msg modal if there is a msg.
            //To Fix:  There is a bug with this on buy/sell where it displays when it should not.
            if (Session["msg"] != null)
            {
                angryLabel.Text = (string)Session["msg"];
                Session["msg"] = null;
            }
        }

        //Occurs AFTER any events and page load execute.
        protected void Page_PreRender(object sender, EventArgs e)
        {
            loadTable();
        }

        //Loads all of the User's assets to tables on every postback/ajax update.
        protected void loadTable()
        {
            initialInvestment = 0;
            initialStock = 0;
            initialCrypto = 0;
            currentInvestment = 0;
            currentStock = 0;
            currentCrypto = 0;
            double dailyCryptoGain = 0;

            //Clear old rows if they exist
            if (stockTable.Rows.Count > 1 || cryptoTable.Rows.Count > 1)
            {
                TableRow saveMe = stockTable.Rows[0];
                stockTable.Rows.Clear();
                stockTable.Rows.Add(saveMe);

                TableRow saveMe2 = cryptoTable.Rows[0];
                cryptoTable.Rows.Clear();
                cryptoTable.Rows.Add(saveMe2);

                TableRow saveMe3 = totalTable.Rows[0];
                totalTable.Rows.Clear();
                totalTable.Rows.Add(saveMe3);
            }

            AssetsTable assetsTable = new AssetsTable(new DatabaseConnection());

            List<Asset> assets = assetsTable.getAssets(userID);
            assets = assets.OrderByDescending(o=>o.Amount*o.PurchasePrice).ToList();

            //Foreach asset in the list, add to the table.
            foreach (Asset asset in assets)
            {
                double currentPrice;
                double rawPrice;
                AssetRow assetRow = new AssetRow();
                double dailyGainDollars = 0;
                double dailyGainPercent = 0;

                if (asset.IsCrypto)
                {

                    BinanceReturn bReturn = PriceGrabber.GetCryptoPrice(asset.Symbol);
                    rawPrice = bReturn.LastPrice;
                    dailyGainDollars = bReturn.DailyPriceChange;
                    dailyCryptoGain += dailyGainDollars*asset.Amount;
                    dailyGainPercent = bReturn.DailyPriceChangePercent;

                    //Control decimals, more if under $1.
                    if (rawPrice > 1) currentPrice = Math.Round(rawPrice, 2);
                    else currentPrice = Math.Round(rawPrice, 5);

                    cryptoTable.Rows.Add(assetRow);
                    initialCrypto += asset.Amount * asset.PurchasePrice;
                    currentCrypto += currentPrice * asset.Amount;
                }

                else
                {
                    rawPrice = PriceGrabber.GetStockPrice(asset.Symbol);

                    //Control decimals, more if under $1.
                    if (rawPrice > 1) currentPrice = Math.Round(rawPrice, 2);
                    else currentPrice = Math.Round(rawPrice, 5);

                    stockTable.Rows.Add(assetRow);
                    initialStock += asset.Amount * asset.PurchasePrice;
                    currentStock += currentPrice * asset.Amount;
                }

                double totalGain = Math.Round((currentPrice - asset.PurchasePrice) * asset.Amount, 2);
                double percentageGain = Math.Round(((currentPrice - asset.PurchasePrice) / asset.PurchasePrice) * 100.0, 2);
                initialInvestment += asset.Amount * asset.PurchasePrice;
                currentInvestment += currentPrice * asset.Amount;

                assetRow.Asset = asset;

                TableCell selectCell = new TableCell();
                RadioButton radioButton = new RadioButton();
                radioButton.GroupName = "selectedAsset";
                radioButton.Attributes.Add("value", asset.AssetID.ToString());
                selectCell.Controls.Add(radioButton);

                TableCell symbolCell = new TableCell();
                symbolCell.Text = asset.Symbol;

                TableCell amountCell = new TableCell();
                if(asset.Amount > 1) amountCell.Text = Math.Round(asset.Amount, 2).ToString();
                else amountCell.Text = Math.Round(asset.Amount, 5).ToString();

                TableCell initialPriceCell = new TableCell();
                if(asset.PurchasePrice > 1) initialPriceCell.Text = "$" + Math.Round(asset.PurchasePrice, 2).ToString();
                else initialPriceCell.Text = "$" + Math.Round(asset.PurchasePrice, 5).ToString();

                TableCell initialValueCell = new TableCell();
                initialValueCell.Text = "$" + Math.Round(asset.PurchasePrice*asset.Amount,2).ToString();

                TableCell currentValueCell = new TableCell();
                currentValueCell.Text = "$" + Math.Round(currentPrice * asset.Amount, 2).ToString();
                if (currentPrice - asset.PurchasePrice > 0) currentValueCell.ForeColor = Color.Green;
                else if (currentPrice - asset.PurchasePrice < 0) currentValueCell.ForeColor = Color.Red;

                TableCell currentPriceCell = new TableCell();
                currentPriceCell.Text = "$"+currentPrice.ToString();
                if (currentPrice - asset.PurchasePrice > 0) currentPriceCell.ForeColor = Color.Green;
                else if (currentPrice - asset.PurchasePrice < 0) currentPriceCell.ForeColor = Color.Red;

                TableCell dailyUSDCell = new TableCell();
                dailyUSDCell.Text = "$" + Math.Round(dailyGainDollars * asset.Amount, 2).ToString();
                if (dailyGainDollars > 0) dailyUSDCell.ForeColor = Color.Green;
                else if (dailyGainDollars < 0) dailyUSDCell.ForeColor = Color.Red;
                else dailyUSDCell.Text = "";

                TableCell dailyPercentageCell = new TableCell();
                dailyPercentageCell.Text = Math.Round(dailyGainPercent, 2).ToString() + "%";
                if (dailyGainPercent > 0) dailyPercentageCell.ForeColor = Color.Green;
                else if (dailyGainPercent < 0) dailyPercentageCell.ForeColor = Color.Red;
                else dailyPercentageCell.Text = "";

                TableCell totalUSDCell = new TableCell();
                totalUSDCell.Text = "$" + totalGain.ToString();
                if (totalGain > 0) totalUSDCell.ForeColor = Color.Green;
                else if(totalGain < 0) totalUSDCell.ForeColor = Color.Red;

                TableCell totalPercentageCell = new TableCell();
                totalPercentageCell.Text = percentageGain.ToString() + "%";
                if (percentageGain > 0) totalPercentageCell.ForeColor = Color.Green;
                else if (percentageGain < 0) totalPercentageCell.ForeColor = Color.Red;

                assetRow.Cells.Add(selectCell);
                assetRow.Cells.Add(symbolCell);
                assetRow.Cells.Add(amountCell);
                assetRow.Cells.Add(initialPriceCell);
                assetRow.Cells.Add(initialValueCell);
                assetRow.Cells.Add(currentValueCell);
                assetRow.Cells.Add(currentPriceCell);
                assetRow.Cells.Add(dailyUSDCell);
                assetRow.Cells.Add(dailyPercentageCell);
                assetRow.Cells.Add(totalUSDCell);
                assetRow.Cells.Add(totalPercentageCell);
            }

            //Add up the totals for both stocks and crypto, then both.
            resetTotalTable(dailyCryptoGain);
        }

        //The other method was getting way too long...
        protected void resetTotalTable(double dailyCryptoGain)
        {
            //Stock Total
            TableRow totalStock = new TableRow();
            totalStock.CssClass = "totalAssetRow";
            TableCell blankCell1 = new TableCell();
            TableCell blankCell2 = new TableCell();
            TableCell blankCell3 = new TableCell();
            TableCell blankCell4 = new TableCell();
            TableCell blankCell9 = new TableCell();
            TableCell blankCell11 = new TableCell();
            TableCell blankCell12 = new TableCell();
            TableCell blankCell13 = new TableCell();
            TableCell stockCell = new TableCell();

            TableCell stockGain = new TableCell();
            stockGain.Text = "$" + Math.Round((currentStock - initialStock), 2);
            if (currentStock - initialStock > 0) stockGain.ForeColor = Color.Green;
            else if (currentStock - initialStock < 0) stockGain.ForeColor = Color.Red;

            TableCell stockGainPercent = new TableCell();
            stockGainPercent.Text = Math.Round(((currentStock - initialStock) / initialStock) * 100.0, 2) + "%";
            if (((currentStock - initialStock) / initialStock) * 100.0 > 0) stockGainPercent.ForeColor = Color.Green;
            else if (((currentStock - initialStock) / initialStock) * 100.0 < 0) stockGainPercent.ForeColor = Color.Red;

            totalStock.Cells.Add(stockCell);
            totalStock.Cells.Add(blankCell1);
            totalStock.Cells.Add(blankCell2);
            totalStock.Cells.Add(blankCell3);
            totalStock.Cells.Add(blankCell4);
            totalStock.Cells.Add(blankCell9);
            totalStock.Cells.Add(blankCell11);
            totalStock.Cells.Add(blankCell12);
            totalStock.Cells.Add(blankCell13);
            totalStock.Cells.Add(stockGain);
            totalStock.Cells.Add(stockGainPercent);
            stockTable.Rows.Add(totalStock);

            //Now for the crypto total
            TableRow totalCrypto = new TableRow();
            totalCrypto.CssClass = "totalAssetRow";
            TableCell blankCell5 = new TableCell();
            TableCell blankCell6 = new TableCell();
            TableCell blankCell7 = new TableCell();
            TableCell blankCell8 = new TableCell();
            TableCell blankCell10 = new TableCell();
            TableCell blankCell14 = new TableCell();
            TableCell blankCell15 = new TableCell();
            TableCell blankCell16 = new TableCell();
            TableCell cryptoCell = new TableCell();

            TableCell dailyCryptoGainCell = new TableCell();
            dailyCryptoGainCell.Text = "$" + Math.Round(dailyCryptoGain, 2);
            if (dailyCryptoGain > 0) dailyCryptoGainCell.ForeColor = Color.Green;
            else if (dailyCryptoGain < 0) dailyCryptoGainCell.ForeColor = Color.Red;

            TableCell cryptoGain = new TableCell();
            cryptoGain.Text = "$" + Math.Round((currentCrypto - initialCrypto), 2);
            if (currentCrypto - initialCrypto > 0) cryptoGain.ForeColor = Color.Green;
            else if (currentCrypto - initialCrypto < 0) cryptoGain.ForeColor = Color.Red;

            TableCell cryptoGainPercent = new TableCell();
            cryptoGainPercent.Text = Math.Round(((currentCrypto - initialCrypto) / initialCrypto) * 100.0, 2) + "%";
            if (((currentCrypto - initialCrypto) / initialCrypto) * 100.0 > 0) cryptoGainPercent.ForeColor = Color.Green;
            else if (((currentCrypto - initialCrypto) / initialCrypto) * 100.0 < 0) cryptoGainPercent.ForeColor = Color.Red;

            totalCrypto.Cells.Add(cryptoCell);
            totalCrypto.Cells.Add(blankCell5);
            totalCrypto.Cells.Add(blankCell6);
            totalCrypto.Cells.Add(blankCell7);
            totalCrypto.Cells.Add(blankCell8);
            totalCrypto.Cells.Add(blankCell10);
            totalCrypto.Cells.Add(blankCell14);
            totalCrypto.Cells.Add(dailyCryptoGainCell);
            totalCrypto.Cells.Add(blankCell16);
            totalCrypto.Cells.Add(cryptoGain);
            totalCrypto.Cells.Add(cryptoGainPercent);
            cryptoTable.Rows.Add(totalCrypto);


            TableRow totalRow = new TableRow();
            TableCell initialCell = new TableCell();
            initialCell.Text = "$" + Math.Round(initialInvestment, 2);

            TableCell currentInvCell = new TableCell();
            currentInvCell.Text = "$" + Math.Round(currentInvestment, 2);
            if (currentInvestment - initialInvestment > 0) currentInvCell.ForeColor = Color.Green;
            else if (currentInvestment - initialInvestment < 0) currentInvCell.ForeColor = Color.Red;

            TableCell gainDollarCell = new TableCell();
            gainDollarCell.Text = "$" + Math.Round((currentInvestment - initialInvestment), 2);
            if (currentInvestment - initialInvestment > 0) gainDollarCell.ForeColor = Color.Green;
            else if (currentInvestment - initialInvestment < 0) gainDollarCell.ForeColor = Color.Red;

            TableCell gainPercentageCell = new TableCell();
            gainPercentageCell.Text = Math.Round(((currentInvestment - initialInvestment) / initialInvestment) * 100.0, 2) + "%";
            if (((currentInvestment - initialInvestment) / initialInvestment) * 100.0 > 0) gainPercentageCell.ForeColor = Color.Green;
            else if (((currentInvestment - initialInvestment) / initialInvestment) * 100.0 < 0) gainPercentageCell.ForeColor = Color.Red;

            totalRow.Cells.Add(initialCell);
            totalRow.Cells.Add(currentInvCell);
            totalRow.Cells.Add(gainDollarCell);
            totalRow.Cells.Add(gainPercentageCell);
            totalTable.Rows.Add(totalRow);
        }

        //Attempts to add an Asset to the User
        protected void buyButton_Click(object sender, EventArgs e)
        {
            AssetsTable assetsTable = new AssetsTable(new DatabaseConnection());

            Asset asset = new Asset();
            asset.Symbol = symbolBox.Text;
            asset.UserID = userID;

            if (double.TryParse(amountBox.Text, out double amount)) asset.Amount = amount;
            else
            {
                angryLabel.Text = "Invalid Input.  Purchase Amount must be a valid decimal number!";
                return;
            }

            if (double.TryParse(priceBox.Text, out double purchasePrice)) asset.PurchasePrice = purchasePrice;
            else
            {
                angryLabel.Text = "Invalid Input.  Purchase Price must be a valid decimal number!";
                return;
            }

            if (cryptoDropdown.SelectedValue == "Crypto") asset.IsCrypto = true;
            else asset.IsCrypto = false;

            assetsTable.insertAsset(asset);

            symbolBox.Text = "";
            priceBox.Text = "";
            amountBox.Text = "";
        }

        //Attempts to remove an Asset to the User
        protected void sellButton_Click(object sender, EventArgs e)
        {
            
            if(selectedAssetID != -1)
            {
                AssetsTable assetsTable = new AssetsTable(new DatabaseConnection());
                assetsTable.deleteAsset(selectedAssetID);
            }
            else
            {
                angryLabel.Text = "Invalid Input.  Purchase Price must be a valid decimal number!";
                return;
            }
        }

        //Timer
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            // your stuff to refresh after some interval.
            loadTable();
        }
    }
}