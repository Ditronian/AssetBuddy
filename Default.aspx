﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AssetBuddy.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <link rel="icon" type="image/x-icon" href="../static/imgs/favicon.ico" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Raleway" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <!-- localized stylesheets -->
    <link rel="stylesheet" href="static/css/master.css" />
    <link rel="stylesheet" href="static/css/login.css" />
    <script src="static/Javascripts/Master.js"></script>
    <title>Login</title>
</head>

<body>
    <form id="form1" runat="server">
        <section id="header">
            <div id="topHeader">
                <h2 id="mainHeader">Asset Buddy</h2>
                <br />
            </div>
            <div id="bottomHeader">
                <asp:HyperLink runat="server" ID="loginLink" NavigateUrl="Default.aspx" class="w3-bar-item w3-button w3-hide-small headerButton">Login</asp:HyperLink>
                <asp:HyperLink runat="server" ID="registerLink" NavigateUrl="Register.aspx" class="w3-bar-item w3-button w3-hide-small headerButton">Register</asp:HyperLink>
            </div>
        </section>

        <!--PAGE SPECIFIC CONTENT-->
        <section id="content">
            <div id="contentPane">
                <br />
                <h2 class="w3-center">Login</h2>
                <hr />
                <table id="loginTable">
                    <tr>
                        <td><label for="username"><b>Email</b></label></td>
                        <td><asp:TextBox runat="server" placeholder="Enter Email" ID="emailTextBox"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><label for="password"><b>Password</b></label></td>
                        <td><asp:TextBox runat="server" TextMode="Password" placeholder="Enter Password" ID="passwordTextBox"></asp:TextBox></td>
                    </tr>
                </table>
                <hr />
                <asp:Button runat="server" class="loginButton" Text="Login" OnClick="loginButton_Click"/>
            </div>

            <div id="myModal" class="modal">
                <div id="modalContent" class="modal-content">
                    <span class="close">&times;</span>
                    <asp:Label ID="angryLabel" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
