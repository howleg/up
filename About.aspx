<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
        <div class="jumbotron">
                <h1><%: Title %>.</h1>
                <h2>Course Hunter helps you determine which classes you can take.</h2>
                <p>We know signing up for classes can be a pill. Let us help.</p>
        </div>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="row">
     
        <div class="col-md-8">
            <div class="panel panel-default">
                    <div class="panel-heading panel-collapse">
                        About
                    </div>
                    <div class="panel-body">
                    Project Zookeeper is an application that allows the user to have a profile, where he/she may select what classes they have completed. Using that information this software will <mark>‘suggest’</mark> what classes the user should take next. This software’s purpose is not to register the user for their class but only to clear some confusion that comes with being advised.  Project Zookeeper will help students be prepared for advisement and not totally rely on the advisors themselves. It should give the student a clear display that shows what they have left to take to get a bachelor’s degree in Computer Science, as well as some recommendations on what to take next based on their pre-requisites. In the end this online application will increase on-time graduation rate as well as scheduling conflicts such as registering for a class that you don’t meet the pre-requisites for
                    </div>
                </div>
        </div>
         <div class="col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading">Give it a try</div>
                <div class="panel-body">Register now and prepare for the future

                </div>
            </div>
        </div>
    </div>
</asp:Content>
