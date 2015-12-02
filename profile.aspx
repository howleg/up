<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="profile.aspx.cs" Inherits="profile" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="container">
        <div class=" row">
            <div class="col-md-12 text-center">
                <asp:Label ID="Label2" runat="server" Text="label" class="alert alert-success" Width="100%"></asp:Label>
            </div>
        </div>


        <div class="row">
            <div class="col-md-4">
                <h3><u>Select Profile Image</u></h3>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <br>
                <asp:Image ID="Image1" class="img-rounded" runat="server" ImageUrl='<%# "ImageHandler.ashx?UserId=" + Eval("UserId") %>' Height="150px" Width="150px" />
            </div>
            <div class="col-md-4">
            </div>

            <div class="col-md-4">
                <h2>Username</h2>
                <asp:Label ID="usernameLabel" runat="server" Text="username" Font-Size="Large"></asp:Label>
                <h2><u>Full Name</u></h2>
                <asp:Label ID="studentNameLabel" runat="server" Text="name" Font-Size="Large"></asp:Label>
                <asp:TextBox ID="actualNameBox" runat="server" Width="290px" Text=""></asp:TextBox>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <h2><u>A little about you</u></h2>
                    <asp:TextBox ID="aboutMeBox" runat="server" Text="something about you" Width="617px" Height="183px" Columns="10" Rows="5" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                </div>
                <div class="col-md-4 ">
                </div>
                <div class="col-md-4">
                </div>
            </div>

        </div>
    </div>
    <asp:Button class="btn btn-default" ID="btnUpdate" Text="Update profile" OnClick="Update_Click" runat="server"></asp:Button>





</asp:Content>
