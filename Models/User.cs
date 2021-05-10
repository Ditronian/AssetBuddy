using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private int userID;
    private Byte[] password;
    private string email;
    private string confirmationID;

    public int UserID { get => userID; set => userID = value; }
    public Byte[] Password { get => password; set => password = value; }
    public string Email { get => email; set => email = value; }
    public string ConfirmationID { get => confirmationID; set => confirmationID = value; }
}