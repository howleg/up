<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>



<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <div class="row">
            <h1 class="text-center">Communication is key </h1>
            <div class="col-md-6">




                <div class="list list-group">
                    <h2>Messaging has a new look</h2>
                    <div class="list list-group-item">
                        <asp:Label ID="lblVerify" runat="server" Text="Label" Visible="False">User Verified</asp:Label>
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
                        
                        Welcome to our site! Here we have means of chatting, friending, and showing your personality. Find friends and keep in touch!
                    </div>
                    <asp:LoginView runat="server" ViewStateMode="Disabled">
                        <AnonymousTemplate>
                            <p>New To Up?</p>
                            <p>Please Register <a runat="server" href="~/Account/Register.aspx">HERE</a></p>
                        </AnonymousTemplate>

                        <LoggedInTemplate>
                            <h3>Start messaging your friends now</h3>
                            <ul class="list list-group">
                                <li class="list list-group-item"> View your <a runat="server" href="~/Profile.aspx">Profile</a></li>
                                <li class="list list-group-item"> chat with <a runat="server" href="~/friends.aspx">Friends</a></li>
                                <li class="list list-group-item"> Search for <a runat="server" href="~/searchUsers.aspx">users</a></li>
                                <li class="list list-group-item"> Having trouble?  Contact us <a runat="server" href="~/contact.aspx">here</a></li>
                            </ul>
                        </LoggedInTemplate>
                    </asp:LoginView>
                </div>
            </div>
            <div class="col-lg-6">
                <%--<img class="img-responsive" src="Images/image2.jpg" />--%>
                <img class="img-responsive" src="Images/test.png" />

            </div>
        </div>
    </div>

</asp:Content>
