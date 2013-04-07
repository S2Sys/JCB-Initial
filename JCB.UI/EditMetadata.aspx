<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditMetadata.aspx.cs" Inherits="JCB.UI.EditMetadata" %>

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
            <td width="35%" align="right">
                Branch <span class="validate">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ctlBranch" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlBranch" Display="Dynamic" InitialValue="-- Select --"
                    runat="server" ID="RequiredFieldValidator2" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" id="trType">
            <td align="right">
                Type
            </td>
            <td>
                <asp:DropDownList ID="ctlType" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlType" Display="Dynamic" InitialValue="-- Select --"
                    runat="server" ID="RequiredFieldValidator1" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Name <span class="validate">*</span>
            </td>
            <td>
                <asp:TextBox ID="ctlName" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlName" runat="server" ID="rfName"
                    ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label runat="server" ID="lblDesc" Text="Description"></asp:Label>  <span class="validate">*</span>
            </td>
            <td>
                <asp:TextBox ID="ctlDescription" runat="server" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ControlToValidate="ctlDescription" runat="server" ID="rfDesc"
                    ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <%-- (?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{3,20})$--%>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="formSubmit">
                <asp:Button ID="btnCreate" runat="server" Text=" Update " OnClick="btnUpdate_Click"  ValidationGroup="cmd"/>
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
