<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Charts.aspx.cs" Inherits="AssetBuddy.Private.Charts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="utf-8" />
    <link rel="icon" type="image/x-icon" href="../static/imgs/favicon.ico" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Raleway" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <!-- localized stylesheets -->
    <link rel="stylesheet" href="~/static/css/master.css" />
    <link rel="stylesheet" href="~/static/css/Charts.css" />
    <script src="../static/Javascripts/Master.js"></script>
    <script src='https://cdn.plot.ly/plotly-2.8.3.min.js'></script>
    <title>Home</title>
</head>
<body>
    <form id="form1" runat="server">
        <section id="header">
            <div id="topHeader">
                <h2 id="mainHeader">Asset Buddy</h2>
                <br />
            </div>
            <div id="bottomHeader">
                <asp:HyperLink runat="server" ID="logoutLink" NavigateUrl="~/Default.aspx" class="w3-bar-item w3-button w3-hide-small headerButton">Logout</asp:HyperLink>
            </div>
        </section>
        <section id="content">
            <div id="contentPane">
                <div id="tickersPane">

                </div>
                <div id="chartPane">
                    <div id="chart"></div>
                    <div id="settings">

                    </div>
                </div>
            </div>

            <!--Message Modal -->
            <div id="myModal" class="modal">
                <div id="modalContent" class="modal-content">
                    <span class="close">&times;</span>
                    <asp:Label ID="angryLabel" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </section>
    </form>
</body>
<script src="../static/Javascripts/Charts.js"></script>
</html>
