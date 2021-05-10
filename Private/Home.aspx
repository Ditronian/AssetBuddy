<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AssetBuddy.Private.Home" MaintainScrollPositionOnPostback="true" %>

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
    <link rel="stylesheet" href="~/static/css/Home.css" />
    <script src="../static/Javascripts/Master.js"></script>
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

        <!--PAGE SPECIFIC CONTENT-->
        <section id="content">
            <div id ="contentPane">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <h3 class="w3-center">Stocks</h3>
                        <asp:Table ID="stockTable" Cssclass="assetTable" runat="server">
                            <asp:TableRow>
                                <asp:TableHeaderCell></asp:TableHeaderCell>
                                <asp:TableHeaderCell>Symbol</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Amount</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Initial Price</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Initial Value</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Current Value</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Current Price</asp:TableHeaderCell>
                                <asp:TableHeaderCell>24H Gain $</asp:TableHeaderCell>
                                <asp:TableHeaderCell>24H Gain %</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Total Gain $</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Total Gain %</asp:TableHeaderCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <h3 class="w3-center">Cryptocurrencies</h3>
                        <asp:Table ID="cryptoTable" Cssclass="assetTable" runat="server">
                            <asp:TableRow>
                                <asp:TableHeaderCell></asp:TableHeaderCell>
                                <asp:TableHeaderCell>Symbol</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Amount</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Initial Price</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Initial Value</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Current Value</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Current Price</asp:TableHeaderCell>
                                <asp:TableHeaderCell>24H Gain $</asp:TableHeaderCell>
                                <asp:TableHeaderCell>24H Gain %</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Total Gain $</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Total Gain %</asp:TableHeaderCell>
                            </asp:TableRow>
                        </asp:Table>
                        <hr />
                        <asp:Table ID="totalTable" runat="server" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell>Initial Investment</asp:TableCell>
                                <asp:TableCell>Current Investment</asp:TableCell>
                                <asp:TableCell>Total Gain $</asp:TableCell>
                                <asp:TableCell>Total Gain %</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <hr />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers> 
                </asp:UpdatePanel>
                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
                <table id="buyTable">
                        <tr>
                            <th>Symbol</th>
                            <th>Purchase Amount</th>
                            <th>Average Purchase Price</th>
                            <th></th>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="symbolBox" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="amountBox" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="priceBox" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="cryptoDropdown" runat="server">
                                    <asp:ListItem Selected="True" Value="Crypto">Cryptocurrency</asp:ListItem>
                                    <asp:ListItem Value="Stock">Stock</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><asp:Button runat="server" class="loginButton" Text="Buy" OnClick="buyButton_Click" /></td>
                            <td><asp:Button runat="server" class="loginButton" Text="Sell" OnClick="sellButton_Click" /></td>
                        </tr>
                    </table>
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
