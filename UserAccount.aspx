<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="UserAccount.aspx.cs" Inherits="UserAccount" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <section class="featured">
        <div class="container">
            <hgroup class="page-header">

                <h1>User Account Page : Click a button to manage account</h1>
            </hgroup>
            <div class="well">
                <asp:Button ID="btnProfile" runat="server" Text="Profile" OnClick="btnProfile_Click" class="btn btn-success" />
                <asp:Button ID="btnAddCourse" runat="server" Text="Add / Remove Courses" OnClick="btnAddCourse_Click" Class="btn btn-success" />
                <asp:Button ID="btnShowRecommended" runat="server" Text="Recommended / Possible courses" OnClick="btnShowRecommended_Click" Class=" btn btn-success" />
                <asp:Button ID="btnStatus" runat="server" Text="Status / Progress page" OnClick="btnStatus_Click" Class="btn btn-success" />
                <asp:Button ID="btnRateCourse" runat="server" Text="Rate My Course" OnClick="btnRateCourse_Click" Class="btn btn-success" />
            </div>


        </div>
    </section>




</asp:Content>
