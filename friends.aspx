<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeFile="friends.aspx.cs" Inherits="friends" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
     <div class="friendsList" id="friendsList" runat="server">
        <asp:PlaceHolder ID="friendsListHolder" runat="server"></asp:PlaceHolder> 
     </div>



</asp:Content>