using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetBuddy
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Registers a new User
        protected void registerButton_Click(object sender, EventArgs e)
        {
            if (registerPasswordTextBox.Text != registerConfirmPasswordTextBox.Text)
            {
                angryLabel.Text = "User registration failed.  Passwords do not match!";
                return;
            }

            UsersTable dbTable = new UsersTable(new DatabaseConnection());

            //Check if Email alrdy exists
            if (dbTable.checkEmail(registerEmailTextBox.Text))
            {
                angryLabel.Text = "User registration failed.  Email already exists!";
                return;
            }

            User registerUser = new User();
            registerUser.Email = registerEmailTextBox.Text;
            registerUser.Password = Security.encrypt(registerPasswordTextBox.Text);
            registerUser.ConfirmationID = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString();

            dbTable.insertUser(registerUser);
            registerUser = dbTable.authenticateUser(registerUser);

            if (registerUser == null)
            {
                angryLabel.Text = "User registration unsuccessful.  Please contact the system administrator.";
            }
            else
            {
                string body = "Greetings, and let me be the first to welcome you to AssetBuddy!";
                body += "\n\n Please click on this link to continue your journey!  http://35.82.10.10/Confirm.aspx?confirm=" + registerUser.ConfirmationID;

                Email.sendEmail(registerUser.Email, "Welcome to AssetBuddy!", body);

                Session["confirmUser"] = registerUser;
                Response.Redirect("Confirm.aspx");
            }
        }
    }
}