using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Email
/// </summary>
public static class Email
{
    //Generates a SmtpClient connection
    private static SmtpClient getClient()
    {
        SmtpClient client = new SmtpClient();
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.EnableSsl = true;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("assetbuddyOfficial@gmail.com", "assetbuddy123");
        
        return client;
    }

    //Sends the email based on the provided values.
    public static bool sendEmail(string sendAddress, string subject, string body, string sender = "Asset Buddy")
    {
        MailMessage message = new MailMessage("assetbuddyOfficial@gmail.com", sendAddress);
        message.From = new MailAddress("assetbuddyOfficial@gmail.com", sender);
        message.Subject = subject;
        message.Body = body;

        try
        {
            getClient().Send(message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}