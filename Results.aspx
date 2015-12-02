<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Results.aspx.cs" Inherits="Results" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="appBody" ContentPlaceHolderID="MainContent">


    <div class="col-md-6" id="panel1">
            <div class="panel panel-success">
                <div class="panel-heading"><asp:Label ID="reclbl" runat="server" Text="Label">Recommended Courses</asp:Label></div>
                <div class="panel-body"><ul>
          <li><asp:Label ID="rec1" runat="server" Text="Label"></asp:Label></li>
          <li><asp:Label ID="rec2" runat="server" Text="Label1"></asp:Label></li>
          <li><asp:Label ID="rec3" runat="server" Text="Label"></asp:Label></li>
          <li><asp:Label ID="rec4" runat="server" Text="Label"></asp:Label></li>
          <li><asp:Label ID="rec5" runat="server" Text="Label"></asp:Label></li>
      </ul></div>
            </div>
        </div>


    <div class="col-md-6" >
            <div class="panel panel-success">
                <div class="panel-heading"><asp:Label ID="poslbl" runat="server" Text="Label">All Possible Courses</asp:Label></div>
                <div class="panel-body"><asp:ListBox ID="allPosListBox" runat="server" Width="100%"></asp:ListBox></div>
            </div>
        </div>
   

    </asp:Content>
    
