<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="AddRemoveCourse.aspx.cs" Inherits="AddRemoveCourse" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
                <h2>Select all courses you would like to add or remove.</h2>
            </hgroup>
            <p>

                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum"></a>
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="appBody" ContentPlaceHolderID="MainContent">
    <div class="panel-heading">Then select Add or Remove</div>


    <asp:Panel runat="server" ID="addPanel">

        <div class="col-md-12">
            <div class="panel panel-info">
       
            <div class="panel-body">
            <table class="table">
                <tr class="text-primary" style="background-color: #333333; font-size: large; border: thin solid #999999">
                   <td>Requirement Area</td>
                    <td>Sub Area / Topic</td>
                    <td>Credit Hours</td>
                    <td>Courses</td>
                    <td>Completed</td>

                </tr>
            
            <tr>
                <td rowspan="3">I. Communication</td>
                <td rowspan="2">English</td>
                <td>3</td>
                <td>

                    <asp:Label runat="server" Text="ENGL U101"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ENGLU101" runat="server" /></td>
                
            </tr>
            <tr>
                
                <td>3</td>
                <td>ENGL U102</td>
                <td><asp:CheckBox ID="ENGLU102" runat="server"  /></td>
            </tr>
            <tr>
                <td>Speech</td>
                <td>3</td>
                <td>SPCH U201</td>
                <td><asp:CheckBox ID="SPCHU201" runat="server" /></td>

            </tr>
            <tr>
                <td rowspan="4">II. Mathematics, Logic &amp; Natural Sciences</td>
                <td rowspan="2">Mathematics</td>
                <td>4</td>
                <td>Math U141</td>
                <td>
                    <asp:CheckBox ID="MATHU141" runat="server" ToolTip="Red = Complete Green = Not Complete" /></td>
                <td>
                    </td>
            </tr>
                <tr>
                
                
                <td>4</td>
                <td>Math U142</td>
               
                <td>
                    <asp:CheckBox ID="MATHU142" runat="server" /></td>
            </tr>
            <tr>
                <td rowspan="2">Natural Science (w/lab)</td>
                <td>8</td>
                <td>
                    <asp:DropDownList ID="ns101DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ns101DropBox_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td colspan="2">
                    <asp:CheckBox ID="ns101" runat="server" /></td>


            </tr>
            <tr>
                <td>4</td>
                <td>
                    <asp:DropDownList ID="ns102DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ns102DropBox_SelectedIndexChanged"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="ns102" runat="server" /></td>

            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>

            </tr>
            <tr>
                <td>III. Information Technology</td>
                <td>Information Technology</td>
                <td>3</td>
                <td>CSCI U150</td>
                <td colspan="2">
                    <asp:CheckBox ID="CSCIU150" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td rowspan="3">IV. Fine Arts, Humanities & History</td>
                <td>Fine Arts</td>
                <td>3</td>
                <td>
                    <asp:DropDownList ID="art101DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="art101DropBox_SelectedIndexChanged"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="art101" runat="server" /></td>

            </tr>
            <tr>
                <td>History</td>
                <td>3</td>
                <td>
                    <asp:DropDownList ID="his101DropBox" runat="server" OnSelectedIndexChanged="his101DropBox_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="his101" runat="server" /></td>
            </tr>
            <tr>
                <td>Humanities</td>
                <td>3</td>
                <td>
                    <asp:DropDownList ID="hum101DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="hum101DropBox_SelectedIndexChanged"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="hum101" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td>V. Foreign Language & Culture</td>
                <td>Foreign Language</td>
                <td>3</td>
                <td>
                    <asp:DropDownList ID="for101DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="for101DropBox_SelectedIndexChanged"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="for101" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td rowspan="2">VI. Social & Behavioral Sciences</td>
                <td rowspan="2">Social & Behavioral Sciences</td>
                <td>3</td>
                <td>
                    <asp:DropDownList ID="soc101DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="soc101DropBox_SelectedIndexChanged"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="soc101" runat="server" /></td>
            </tr>
            <tr>
                <td>3</td>
                <td>
                    <asp:DropDownList ID="soc102DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="soc102DropBox_SelectedIndexChanged"></asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="soc102" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>

            </table>
                </div>
        </div>
                
            </div>





        <div class="col-md-12">
            <div class="panel panel-info">
       
            <div class="panel-body">
        <table class="table">
            <tbody class="mytable">
                <tr class="text-primary" style="background-color: #333333; font-size: large; border: thin solid #999999">
                    <td colspan="2">Core Major Requirements</td>
                    <td>Credits Required: 33</td>

                </tr>
                <tr class="text-primary" style="background-color: #333333; font-size: large; border: thin solid #999999">
                    <td></td>
                    <td>Credit Hours</td>
                    <td>Completed</td>
                </tr>
                <tr>
                    <td>CSCIU200: Computer Science I</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU200" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU210: Computer Organization</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU210" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU234: Visual BASIC Programming or CSCIU238: C++ Programming</td>
                    <td>3</td>

                    <td>
                        <asp:CheckBox ID="CSCIU234" runat="server" /></td>
                </tr>

                <tr>
                    <td>CSCIU300: Computer Science II</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU300" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU310: Intro to Computer Architecture</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU310" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU321: Computer Science III</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU321" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU421: Design & Analysis of Algorithms</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU421" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU511: Operation Systems</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU511" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU530: Programming Language Structures</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU530" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU540: Software Engineering</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU540" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU599: Senior Seminar</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU599" runat="server" /></td>
                </tr>

            </tbody>
        </table></div></div></div>











        <div class="col-md-12">
            <div class="panel panel-info">
       
            <div class="panel-body">
        <table class="table">
            <tbody>
                <tr class="text-primary" style="background-color: #333333; font-size: large; border: thin solid #999999">
                    <td colspan="2">General</td>
                    <td>Credits Required: 9</td>

                </tr>
                <tr class="text-primary" style="background-color: #333333; font-size: large; border: thin solid #999999">
                    <td></td>
                    <td>Credit Hours</td>
                    <td>Completed</td>
                </tr>
                <tr>
                    <td>CSCIU311: Information Systems Hardware and Software</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU311" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU314: Industrial Robotics</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU314" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU315: Networking Technology</td>
                    <td>3</td>

                    <td>
                        <asp:CheckBox ID="CSCIU315" runat="server" /></td>
                </tr>

                <tr>
                    <td>CSCIU325: Fundamentals of Relational Database Management</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU325" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU355: Digital Forensics</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU355" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU370: Fundamental of Bioinformatics</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU370" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU399: Independent Study</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU399" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU412: Computer Networks I</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU412" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCI441: Experiential Learning in Computer Science</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCI441" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU450: E-Business Web Application Design</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU450" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU455: Computer Security</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU455" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU456: Applied Cryptography</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU456" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU512: Computer Networks II</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU512" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU515: Wireless Networks</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU515" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU516: Distributed and Network Programming</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU516" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU520: Database System Design</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU520" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU521: Database Implementation, Application, and Administration</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU521" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU525: Knowledge Discovery and Data Mining</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU525" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU526: Data Mining for Computer Security</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU526" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU555: Advanced Computer Security and Information Assurance</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU555" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU556: Web Development Security</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU556" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU580: Introduction to Artificial Intelligence</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU580" runat="server" /></td>
                </tr>
                <tr>
                    <td>CSCIU585: Introduction to Computer Vision</td>
                    <td>3</td>
                    <td>
                        <asp:CheckBox ID="CSCIU585" runat="server" /></td>
                </tr>


            </tbody>
        </table>
</div></div></div>







        <div class="col-md-12">
            <div class="panel panel-info">
       
            <div class="panel-body">


        <table class="table">
            <tbody>
                <tr class="text-primary" style="background-color: #333333; font-size: large; border: thin solid #999999">
                    <td colspan="2">Supporting Courses</td>
                    <td colspan="2">Credits Required: 13-14</td>

                </tr>
                <tr class="text-primary" style="background-color: #333333; font-size: large; border: thin solid #999999">
                    <td></td>
                    <td>Credit Hours</td>
                    <td>Course</td>
                    <td colspan="2">Completed</td>
                </tr>
                <tr>
                    <td>MATHU174: Elements of Discrete Mathematics</td>
                    <td>3</td>
                    <td>MATH U174</td>
                    <td colspan="2">
                        <asp:CheckBox ID="MATHU174" runat="server" /></td>
                </tr>
                <tr>
                    <td>MATHU315: Statistical Methods I</td>
                    <td>3</td>
                    <td>MATH U315</td>
                    <td colspan="2">
                        <asp:CheckBox ID="MATHU315" runat="server" /></td>
                </tr>
                <tr>
                    <td>Natural Science (w/lab)</td>
                    <td>4</td>
                    <td>
                        <asp:DropDownList ID="ns103DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ns103DropBox_SelectedIndexChanged"></asp:DropDownList>
                    <td>
                        <asp:CheckBox ID="ns103" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Elective #1
                    </td>
                    <td>3
                    </td>
                    <td>
                        <asp:DropDownList ID="e1DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="e1DropBox_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox ID="e1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Elective #2
                    </td>
                    <td>3
                    </td>
                    <td>
                        <asp:DropDownList ID="e2DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="e2DropBox_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox ID="e2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Elective #3
                    </td>
                    <td>3
                    </td>
                    <td>
                        <asp:DropDownList ID="e3DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="e3DropBox_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox ID="e3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Elective #4
                    </td>
                    <td>3
                    </td>
                    <td>
                        <asp:DropDownList ID="e4DropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="e4DropBox_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox ID="e4" runat="server" />
                    </td>
                </tr>


            </tbody>
        </table>
                </div></div></div>

        <asp:Button ID="btnSubmit" runat="server" Text="Add" OnClick="btnSubmit_Click" BorderColor="#003300" BackColor="#333333" ForeColor="#999999" BorderStyle="Solid" CssClass="btn" Font-Size="Large" Style="margin: 10%;"/>
        <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" BackColor="#333333" BorderColor="#003300" BorderStyle="Solid" CssClass="btn" Font-Size="Large" ForeColor="#999999" />
    </asp:Panel>
</asp:Content>





<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 41px;
        }
    </style>
</asp:Content>






