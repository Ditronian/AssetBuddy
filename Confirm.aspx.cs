using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetBuddy
{
    public partial class Confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string guid = Request["confirm"];

            if (guid != null)
            {
                UsersTable userTable = new UsersTable(new DatabaseConnection());
                int userID = userTable.authenticateConfirmationID(guid);
                if (userID == 0)
                {
                    angryLabel.Text = "Confirmation ID not found. Please use the link contained in your confirmation email.";
                    return;
                }
                else
                {
                    userTable.deleteConfirmationID(guid);

                    angryLabel.Text = "Congratulations, your account is now confirmed and you may login.";
                    return;
                }
            }
        }
    }
}