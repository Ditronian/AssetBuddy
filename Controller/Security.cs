using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public static class Security
{
    //SHA1 encryption...it works good nuff.
    public static Byte[] encrypt(string unencryptedString)
    {

        SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(unencryptedString));

        return hash;
    }
}