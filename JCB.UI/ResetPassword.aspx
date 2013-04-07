<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ResetPassword.aspx.cs" Inherits="JCB.UI.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <table cellpadding="5" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="pageTitle" colspan="2">
                Password Reset
                <asp:Label runat="server" ID="lblType" />
            </td>
        </tr>
        <tr>
            <td width="35%" align="right">
            </td>
            <td width="65%">
            </td>
        </tr>
        <tr runat="server" id="trBranch">
            <td align="right">
                Branch
            </td>
            <td>
                <asp:DropDownList ID="ctlBranch" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlBranch" Display="Dynamic" InitialValue="-- Select --"
                    runat="server" ID="RequiredFieldValidator13" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" id="trType">
            <td align="right">
                Type
            </td>
            <td>
                <asp:DropDownList ID="ctlType" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlType" Display="Dynamic" InitialValue="-- Select --"
                    runat="server" ID="RequiredFieldValidator2" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Name
            </td>
            <td>
                <asp:Label ID="ctlName" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="trUsername">
            <td align="right">
                Username
            </td>
            <td>
                <asp:Label ID="ctlUsername" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="trPassword">
            <td align="right">
                Password
            </td>
            <td>
                <asp:TextBox ID="ctlPassword" runat="server" TextMode="Password" ToolTip="3-20 Char length & Allowed Char & Numbers only" />
                <asp:RequiredFieldValidator ControlToValidate="ctlPassword" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator5" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="ctlPassword"
                    runat="server" ValidationExpression="^([a-zA-Z0-9_-]{3,20})$" Display="Dynamic"
                    ValidationGroup="cmd" ErrorMessage="">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr runat="server" id="tr1">
            <td align="right">
                Password
            </td>
            <td>
                <asp:TextBox ID="ctlConfirmPassword" runat="server" TextMode="Password" ToolTip="3-20 Char length & Allowed Char & Numbers only" />
                <asp:RequiredFieldValidator ControlToValidate="ctlConfirmPassword" Display="Dynamic"
                    runat="server" ID="RequiredFieldValidator3" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="ctlConfirmPassword"
                    runat="server" ValidationExpression="^([a-zA-Z0-9_-]{3,20})$" Display="Dynamic"
                    ValidationGroup="cmd" ErrorMessage="">
                </asp:RegularExpressionValidator>
                <asp:CompareValidator ControlToCompare="ctlConfirmPassword" runat="server" ControlToValidate="ctlPassword"
                    Display="Dynamic" ValidationGroup="cmd"  ErrorMessage="Passwords doesn't Match">
                
                </asp:CompareValidator>
            </td>
        </tr>
         
        <tr>
            <td colspan="2" align="center" class="formSubmit">
                <asp:Button ID="btnUpdate" runat="server" Text=" Update " OnClick="btnUpdate_Click"
                    ValidationGroup="cmd" />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
