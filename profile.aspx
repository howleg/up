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
                <div class="well">
                    <h3 class="text-center"><u>Select Profile Image</u></h3>

                    <br>
                    <asp:Image ID="Image1" class="img-circle center-block" runat="server" ImageUrl='<%# "ImageHandler.ashx?UserId=" + Eval("UserId") %>' Height="150px" Width="150px" />
                    <br/><br/>
                    <asp:FileUpload ID="FileUpload1" CssClass="well-sm" runat="server" />
                </div>
            </div>
            <div class="col-md-4">
            </div>

            <div class="col-md-4">
                <div class="well">
                    <h2><u>Username</u></h2>
                    <asp:Label ID="usernameLabel" runat="server" Text="username" CssClass="" Font-Size="Large"></asp:Label>
                    <h2><u>Full Name</u></h2>
                    <asp:Label ID="studentNameLabel" runat="server" Text="name" Font-Size="Large"></asp:Label>
                    <asp:TextBox ID="actualNameBox" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    <h2><u>Major</u></h2>
                    <asp:Label ID="majorNameLabel" runat="server" Text="major" Font-Size="Large"></asp:Label>
                    <asp:DropDownList ID="majorsDropDown" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <h2><u>A bout you</u></h2>
                    <div class="well">
                        <asp:TextBox ID="aboutMeBox" runat="server" Width="100%" Height="200px" placeholder="something about you" TextMode="MultiLine" MaxLength="300"></asp:TextBox>

                    </div>

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
