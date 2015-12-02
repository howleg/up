<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="databaseChanges.aspx.cs" Inherits="databaseChanges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <link href="../Content/bootstrap.css" rel="stylesheet" />

    <h2>Edit Courses
        Here or add a course(s) by clicking &#39;Add Course&#39;</h2>
    <h5>&nbsp;<asp:Button ID="Button1" runat="server" Height="41px" Text="Add Course(s)" Width="120px" PostBackUrl="~/Admin privleges/AddCourse.aspx" CssClass="btn btn-success" />
    </h5>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="course_id" DataSourceID="coursesTable" ForeColor="#333333" GridLines="None" Width="761px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="course_id" HeaderText="crse_id" InsertVisible="False" ReadOnly="True" SortExpression="course_id" />
            <asp:BoundField DataField="course_number" HeaderText="crse_#" SortExpression="course_number" />
            <asp:BoundField DataField="course_title" HeaderText="crse_title" SortExpression="course_title" />
            <asp:BoundField DataField="credit" HeaderText="credits" SortExpression="credit" />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" HorizontalAlign="Center" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>
    <asp:SqlDataSource ID="coursesTable" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [course] WHERE [course_id] = @original_course_id AND [course_number] = @original_course_number AND [course_title] = @original_course_title AND (([credit] = @original_credit) OR ([credit] IS NULL AND @original_credit IS NULL))" InsertCommand="INSERT INTO [course] ([course_number], [course_title], [credit]) VALUES (@course_number, @course_title, @credit)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [course] ORDER BY [course_number]" UpdateCommand="UPDATE [course] SET [course_number] = @course_number, [course_title] = @course_title, [credit] = @credit WHERE [course_id] = @original_course_id AND [course_number] = @original_course_number AND [course_title] = @original_course_title AND (([credit] = @original_credit) OR ([credit] IS NULL AND @original_credit IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_course_id" Type="Int32" />
            <asp:Parameter Name="original_course_number" Type="String" />
            <asp:Parameter Name="original_course_title" Type="String" />
            <asp:Parameter Name="original_credit" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="course_number" Type="String" />
            <asp:Parameter Name="course_title" Type="String" />
            <asp:Parameter Name="credit" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="course_number" Type="String" />
            <asp:Parameter Name="course_title" Type="String" />
            <asp:Parameter Name="credit" Type="Int32" />
            <asp:Parameter Name="original_course_id" Type="Int32" />
            <asp:Parameter Name="original_course_number" Type="String" />
            <asp:Parameter Name="original_course_title" Type="String" />
            <asp:Parameter Name="original_credit" Type="Int32" />
        </UpdateParameters>
</asp:SqlDataSource>
    </asp:Content>

