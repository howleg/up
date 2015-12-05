<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="viewProfile.aspx.cs" Inherits="viewProfile" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div>
    </div>


    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-default text-center">
                <div class="panel-heading">
                   <h3>Username:
                    <asp:Label ID="usernameLabel" runat="server" Text="username" Font-Size="Large" ></asp:Label></h3>
                </div>
                <asp:Image ID="Image1" runat="server" ImageUrl='<%# "ImageHandler.ashx?UserId=" + Eval("UserId") %>' Height="135px" Width="135px" />
            </div>

            <div class="text-center">
                <asp:Button ID="btnFriends" runat="server" Text=@viewProfile.name  OnClick="btnFriends_Click"  CssClass="btn btn-primary" />
                <asp:Button ID="btnMutalFriends" runat="server" Text="MutualFriends" OnClick="btnMutualFriends_Click"  cssclass="btn btn-info" />
                <asp:Button ID="btnSendMessage" Text="Message" OnClick="Message_Click" runat="server" CssClass="btn btn-success"></asp:Button>
                <asp:Button ID="btnAddFriend" Text="Add Friend" OnClick="addFriend_Click" runat="server" CssClass="btn btn-primary"></asp:Button>
            </div>

        </div>
        <div class="col-md-6">
            <div class="panel panel-default text-center ">
                <div class="panel-heading">
                    <h3>Full Name:
                    <asp:Label ID="studentNameLabel" runat="server" Text="name" Font-Size="Large"></asp:Label></h3>
                </div>
                <h3>Major:</h3>
                <asp:Label ID="majorNameLabel" runat="server" Text="major"  Font-Size="Large"></asp:Label>
                <h3>Self-Summary:</h3>
                

                <asp:Label ID="summaryLabel" runat="server" Text="something"></asp:Label>


            </div>
        </div>
    </div>

    <br>

    <div class="row">
        <div class="col-md-6">
            <div class="well-sm text-center">

                <div id="DivMessage" runat="server" visible="false">
                    <!--a box to enter a message to send to the User-->
                    <h3 id="messageh3" runat="server" text="Message"></h3>
                    <asp:TextBox ID="messageBox" runat="server" Width="80%" Height="183px" Columns="10" Rows="10" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnCancel" Text="Cancel" OnClick="Cancel_Click" runat="server"></asp:Button>
                    <!--a button to send a message to the user whos profile was selected -->
                    <asp:Button ID="btnSend" Text="Send" OnClick="Send_Click" runat="server"></asp:Button>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
