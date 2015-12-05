<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="NotLoggedIn.aspx.cs" Inherits="NotLoggedIn" %>


<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <br />
    <br />
    <br />
    <br />
    <br />

    <div class="panel panel-warning">
        <div class="panel-body">
            Please <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /> or <asp:Button ID="btnRegister" runat="server" Text="Register" onclick="btnRegister_Click"/> to start chatting for free!
        </div>


    </div>
   
    </asp:Content>