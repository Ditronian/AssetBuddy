using AssetBuddy.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetBuddy
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                Session["msg"] = "You have been logged out!";
            }

            if (Session["msg"] != null)
            {
                angryLabel.Text = (string) Session["msg"];
                Session["msg"] = null;
            }
        }

        //Attempts login of the user
        protected void loginButton_Click(object sender, EventArgs e)
        {
            UsersTable UsersTable = new UsersTable(new DatabaseConnection());

            User loginUser = new User();
            loginUser.Email = emailTextBox.Text;
            loginUser.Password = Security.encrypt(passwordTextBox.Text);

            loginUser = UsersTable.authenticateUser(loginUser);

            //Deny entry
            if (loginUser == null)
            {
                angryLabel.Text = "Login failed.  Please check your login credentials.";
                return;
            }
            else if (loginUser.ConfirmationID != null)
            {
                //Change to send to confirmation page
                angryLabel.Text = "Login failed.  Please check your email for confirmation instructions.";
                return;
            }
            //Login to Home Page
            else
            {
                this.Session["user"] = loginUser;

                //Load Main page
                Response.Redirect("~/Private/Home.aspx");
            }
        }
    }
}