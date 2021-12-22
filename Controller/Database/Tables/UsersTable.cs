using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsersTable
/// </summary>
public class UsersTable : DBTable
{

    public UsersTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Inserts a new user.
    public void insertUser(User user)
    {
        string query = "spInsertUser";
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("password", user.Password);
        parameters[1] = new SqlParameter("email", user.Email);
        parameters[2] = new SqlParameter("confirmationID", user.ConfirmationID);

        database.uploadCommand(query, parameters);
    }

    //Gets a specific user using the passed parameters
    public User authenticateUser(User user)
    {
        //Make query
        string query = "spAuthenticateUser";
        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("email", user.Email);    //Set sql parameter 1 (with sql name of "userID"), with a value of userID
        parameters[1] = new SqlParameter("password", user.Password);    //Set sql parameter 1 (with sql name of "userID"), with a value of userID

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return whether or not the db found the user by returning the userID or 0.
        if (data.Tables[0].Rows.Count == 1)
        {
            user.UserID = (Int32)data.Tables[0].Rows[0]["userID"];
            user.Email = (string)data.Tables[0].Rows[0]["email"];
            if (data.Tables[0].Rows[0]["initialInvestment"] is DBNull) user.InitialInvestment = 0;
            else user.InitialInvestment = (double)data.Tables[0].Rows[0]["initialInvestment"];

            if (data.Tables[0].Rows[0]["confirmationID"] is DBNull) user.ConfirmationID = null;
            else user.ConfirmationID = (string)data.Tables[0].Rows[0]["confirmationID"];

            return user;
        }
        else return null;
    }

    //Checks if Email exists in the db
    public bool checkEmail(string email)
    {
        string query = "spCheckEmail";
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("Email", email);

        DataSet data = database.downloadCommand(query, parameters);

        if (data.Tables[0].Rows.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Deletes the provided User.
    public void deleteUser(int userID)
    {
        string query = "spDeleteUser";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("userID", userID);

        database.uploadCommand(query, parameters);
    }

    //Gets a specific user using the passed parameters
    public int authenticateConfirmationID(string confirmationGUID)
    {
        //Make query
        string query = "spAuthenticateConfirmationID";
        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("confirmationID", confirmationGUID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return whether or not the db found the GUID by returning the userID or 0.
        if (data.Tables[0].Rows.Count == 1) return (Int32)data.Tables[0].Rows[0]["userID"];

        else return 0;
    }

    //Deletes the provided Confirmation GUID.
    public void deleteConfirmationID(string confirmationGUID)
    {
        string query = "spDeleteConfirmationID";
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("confirmationID", confirmationGUID);

        database.uploadCommand(query, parameters);
    }

    //Deletes the provided Confirmation GUID.
    public void updateInitialInvestment(int userID, double investment)
    {
        string query = "spUpdateInitialInvestment";
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("userID", userID);
        parameters[1] = new SqlParameter("investment", investment);

        database.uploadCommand(query, parameters);
    }
}