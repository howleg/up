<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddCourse.aspx.cs" Inherits="Admin_privleges_AddCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <link href="../Content/bootstrap.css" rel="stylesheet" />

    <table>
        <tr>
            <td>Enter the Course number: </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" ForeColor="Black" MaxLength="9" ToolTip="Should be in the format of all caps (e.g.  CSCI) then a space (_) followed by the three digit course number"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Enter the Course title: </td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" CausesValidation="True" ForeColor="Black" MaxLength="70"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Enter the number of credits for the Course: </td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server" ForeColor="Black" MaxLength="1" ToolTip="Should be a single digit number (Usually between 1 and 3)"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" Text="Save Course" OnClick="Button1_Click" CssClass="btn btn-success" />
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Is it an elective?" />
            </td>
        </tr>
    </table>

</asp:Content>

