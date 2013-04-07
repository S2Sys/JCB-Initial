<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditUser.aspx.cs" Inherits="JCB.UI.EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <table cellpadding="5" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="pageTitle" colspan="2">
                Update
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
                <asp:TextBox ID="ctlName" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlName" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator3" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" id="trUsername">
            <td align="right">
                Username
            </td>
            <td>
                <asp:TextBox ID="ctlUsername" runat="server" ToolTip="3-20 Char length, Allowed A-Z a-z 0-9 _ . only" />
                <asp:RequiredFieldValidator ControlToValidate="ctlUsername" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator4" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="ctlUsername"
                    runat="server" Text="Invalid" ValidationExpression="^([a-zA-Z0-9_-]{3,20})$"
                    Display="Dynamic" ValidationGroup="cmd" ErrorMessage="Invalid">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <%--<tr runat="server" id="trPassword">
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
        </tr>--%>
        <tr>
            <td align="right">
                Address
            </td>
            <td>
                <asp:TextBox ID="ctlAddress" runat="server" TextMode="MultiLine" Height="100" />
                <asp:RequiredFieldValidator ControlToValidate="ctlAddress" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator6" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                City
            </td>
            <td>
                <asp:TextBox ID="ctlCity" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlCity" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator7" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                State
            </td>
            <td>
                <asp:DropDownList ID="ctlState" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlState" Display="Dynamic" InitialValue="-- Select --"
                    runat="server" ID="RequiredFieldValidator1" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Phone
            </td>
            <td>
                <asp:TextBox ID="ctlPhone" runat="server" />
              <%--  <asp:RequiredFieldValidator ControlToValidate="ctlPhone" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator8" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                Mobile
            </td>
            <td>
                <asp:TextBox ID="ctlMobile" runat="server" />
              <%--  <asp:RequiredFieldValidator ControlToValidate="ctlMobile" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator9" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr runat="server" id="trTin">
            <td align="right">
                TIN
            </td>
            <td>
                <asp:TextBox ID="ctlTin" runat="server" Text="" />
             <%--   <asp:RequiredFieldValidator ControlToValidate="ctlTin" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator10" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr runat="server" id="trCst">
            <td align="right">
                CST
            </td>
            <td>
                <asp:TextBox ID="ctlCst" runat="server" Text="" />
               <%-- <asp:RequiredFieldValidator ControlToValidate="ctlCst" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator11" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr runat="server" id="trOB">
            <td align="right">
                Opening Balance
            </td>
            <td>
                <asp:TextBox ID="ctlOB" runat="server" Text="0" />
                <asp:RequiredFieldValidator ControlToValidate="ctlOB" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator12" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="formSubmit">
                <asp:Button ID="btnUpdate" runat="server" Text=" Update " OnClick="btnUpdate_Click" ValidationGroup="cmd" />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
