<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Progress.aspx.cs" Inherits="Progress" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent"> 

    <div class="jumbotron">
            <h1 style ="text-align:center"><asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label></h1>
            <h2 style ="text-align:center">Overall Student Progress</h2>
            <div id="progressbar" style="width:75%; margin:0 auto;"></div>   
                 
                  
                      <script type="text/jscript">
                          var myProg;
                          myProg = <%=myProg%>;
                         jQuery(document).ready(function () {
                             jQuery("#progressbar").progressbar({ value: myProg }).append("<div class='caption'>0%</div>");
                             jQuery("#progressbar").css({ 'background': 'url(images/white-40x100.png) #ffffff repeat-x 50% 50%;' });
                             jQuery("#pbar1 > div").css({ 'background': 'url(images/lime-1x100.png) #cccccc repeat-x 50% 50%;' });
                         });
                        </script>    

        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
                    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
                    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script> 
        </div>


   
        <div class="col-md-3">
            <div class="panel panel-success">
                <div class="panel-heading">Completed Courses</div>
                <div class="panel-body"><asp:ListBox ID="completeListBox" runat="server" Width="100%"></asp:ListBox></div>
            </div>
        </div>

    <div class="col-md-3">
            <div class="panel panel-success">
                <div class="panel-heading">Remaining Courses</div>
                <div class="panel-body"><asp:ListBox ID="remainingListBox" runat="server" Width="100%" ></asp:ListBox></div>
            </div>
        </div>


    <div class="col-md-3">
            <div class="panel panel-success">
                <div class="panel-heading">Courses</div>
                <div class="panel-body">
                    <h4>Completed: <asp:Label ID="lblCourseComplete" runat="server" Text=""></asp:Label></h4>
                    <h4>Remaining: <asp:Label ID="lblCourseRemaining" runat="server" Text=""></asp:Label></h4>
                </div>
            </div>
        </div>

    <div class="col-md-3">
            <div class="panel panel-success">
                <div class="panel-heading">Credits</div>
                <div class="panel-body">
                    <h4>Completed: <asp:Label ID="lblCreditComplete" runat="server" Text=""></asp:Label></h4>
                    <h4>Remaining: <asp:Label ID="lblCreditRemaining" runat="server" Text=""></asp:Label></h4>
                </div>
            </div>
        </div>
  


    </asp:Content>