<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="JCB.UI.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" style="height: 500px; vertical-align: middle;"
       >
        <tr>
            <td style="height: 440px; text-align: middle; font-size: 20pt; vertical-align: center;">
                <center>
                    <table width="65%" cellpadding="5" cellspacing="10"  style="vertical-align: middle;" class="login">
                        <tr>
                            <td class="title">
                                Login
                            </td>
                            <td>
                            </td>
                        </tr>

                          <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblError" runat="server" ></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td align="right">
                                Username
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtUser" runat="server" Text="" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Password
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Text=""  Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        



                        <tr>
                            <td colspan="2" align="center" class="formSubmit">
                                <asp:Button ID="btnLogin" runat="server" Text="login" OnClick="btnLogin_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </center>
            </td>
        </tr>
    </table>
</asp:Content>
