<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="RateMyCourse.aspx.cs" Inherits="RateMyCourse" %>

<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <style type="text/css">
        .emptypng {
            background-image: url(/Images/ratingStarEmpty.png);
            width: 32px;
            height: 32px;
        }

        .smileypng {
            background-image: url(/Images/ratingStarFilled.png);
            width: 32px;
            height: 32px;
        }

        .donesmileypng {
            background-image: url(/Images/ratingStarSaved.png);
            width: 32px;
            height: 32px;
        }
    </style>

    <html>

    <body>

        <br />
        <br />
        <br />
        <br />
        <br />





        <table width="100%" style="padding: 20px">
            <tr>
                <td colspan="2" align="center">
                    <asp:Image ID="rateCourseTitlePic" runat="server" ImageUrl="~/Images/rateCourseTitle.png" /></td>

            </tr>
            <tr>
                <td align="right"></td>
                <td align="left">
                    <asp:Label ID="lblmessage" runat="server" Style="color: #3366FF" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>

            <tr>
                <td align="right">Select Course: </td>
                <td>

                    <asp:DropDownList ID="courseDropBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="courseDropBox_SelectedIndexChanged"></asp:DropDownList>

                </td>

            </tr>


            <tr>
                <td align="right" style="padding: 20px">Rating: 
                </td>
                <td align="left" style="padding: 20px">
                    <ajaxToolkit:Rating ID="Rating1" runat="server" CurrentRating="0" MaxRating="5"
                        EmptyStarCssClass="emptypng" FilledStarCssClass="smileypng"
                        StarCssClass="smileypng" WaitingStarCssClass="donesmileypng">
                    </ajaxToolkit:Rating>

                </td>
            </tr>

            <tr>
                <td align="right" style="padding: 20px">Name: 
                </td>
                <td align="left" style="padding: 20px">
                    <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtName" ErrorMessage="Please enter name"
                        Style="color: #FF0000"></asp:RequiredFieldValidator>
                </td>


            </tr>



            <tr>
                <td align="right" style="padding: 20px">Comment: 
                </td>
                <td align="left" style="padding: 20px">
                    <asp:TextBox ID="txtComment" runat="server" Height="87px" TextMode="MultiLine"
                        Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtComment" ErrorMessage="Please enter some comment"
                        Style="color: #FF0000"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td align="left" colspan="1" style="padding: 20px">
                    <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Submit"
                        OnClick="btnSubmit_Click" />
                    <asp:Button CssClass="btn btn-danger" ID="btnRemove" runat="server" Text="Remove Comment" Visible="false" OnClick="btnRemove_Click" CausesValidation="false" autopostback="true" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="padding: 20px">

                    <asp:GridView ID="gdvUserComment" runat="server" AutoGenerateColumns="False"
                        ShowHeader="False" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="width: 20%"><b>Name :</b> <%#Eval("Name")%></td>
                                            <td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <itemtemplate>
                                            
                                <ContentTemplate>
                               <ajaxToolkit:Rating ID="RatingDone" runat="server" CurrentRating='<%#Eval("Rating")%>' ReadOnly="true" MinRating="0" MaxRating="5" 
                                 EmptyStarCssClass="emptypng" FilledStarCssClass="smileypng"
                                  StarCssClass="smileypng" WaitingStarCssClass="donesmileypng"></ajaxToolkit:Rating>
                                    </ContentTemplate>
                                               
                                            </itemtemplate>


                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"><%#Eval("Comment")%></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                </td>
            </tr>
        </table>
        <br />

    </body>
    </html>
</asp:Content>
