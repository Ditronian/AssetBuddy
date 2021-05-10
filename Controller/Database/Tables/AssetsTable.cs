using AssetBuddy.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsersTable
/// </summary>
public class AssetsTable : DBTable
{

    public AssetsTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Inserts a new asset.
    public void insertAsset(Asset asset)
    {
        string query = "spInsertAsset";
        SqlParameter[] parameters = new SqlParameter[5];
        parameters[0] = new SqlParameter("userID", asset.UserID);
        parameters[1] = new SqlParameter("symbol", asset.Symbol);
        parameters[2] = new SqlParameter("purchasePrice", asset.PurchasePrice);
        parameters[3] = new SqlParameter("amount", asset.Amount);
        parameters[4] = new SqlParameter("isCrypto", asset.IsCrypto);

        database.uploadCommand(query, parameters);
    }

    //Returns all owned assets as an array
    public List<Asset> getAssets(int userID)
    {
        //Make query
        string query = "spGetAssets";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("userID", userID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Make sure the database found the asset, else return an empty list
        List<Asset> assets = new List<Asset>();

        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int assetID = (Int32)data.Tables[0].Rows[i]["assetID"];
            string symbol = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["symbol"].ToString());
            double purchasePrice = (double)data.Tables[0].Rows[i]["purchasePrice"];
            double amount = (double)data.Tables[0].Rows[i]["amount"];
            bool isCrypto = (bool) data.Tables[0].Rows[i]["isCrypto"];

            Asset asset = new Asset();
            asset.Symbol = symbol;
            asset.AssetID = assetID;
            asset.UserID = userID;
            asset.PurchasePrice = purchasePrice;
            asset.Amount = amount;
            asset.IsCrypto = isCrypto;

            assets.Add(asset);
        }
        return assets;
    }

    //Deletes a provided assetID
    public void deleteAsset(int assetID)
    {
        string query = "spDeleteAsset";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("assetID", assetID);

        database.uploadCommand(query, parameters);
    }
}