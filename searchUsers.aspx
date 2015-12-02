<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="searchUsers.aspx.cs" Inherits="searchUsers" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


    <div class="container">

        <div class="panel panel-heading">
            <h3>Enter a username to search</h3>
        </div>
        <div class="form form-group">
            <div class="form form-inline">
                <div class="input-group">
                    <asp:TextBox ID="searchBox" runat="server" Width="100%"></asp:TextBox>
                    <asp:Label ID="userFoundLabel" runat="server" Text="is user founnd" Font-Size="Large" BackColor="#99FF66"></asp:Label>
                    <asp:Button ID="btn_OneUser" class="btn btn-primary" Text="Search" OnClick="search_Click" runat="server"></asp:Button>
                    <asp:Button ID="btn_allUSers" class="btn btn-default" Text="All users" OnClick="allUSers_Click" runat="server"></asp:Button>
                </div>
            </div>
        </div>

    <div class="allUSers" id="seachAllUSers" runat="server">
        <div class="container-fluid">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

        </div>

    </div>
    </div>

</asp:Content>
