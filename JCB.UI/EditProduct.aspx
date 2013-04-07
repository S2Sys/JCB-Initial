<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditProduct.aspx.cs" Inherits="JCB.UI.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <table cellpadding="5" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="pageTitle" colspan="2">
                Edit Product
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
                Branch
            </td>
            <td>
                <asp:DropDownList ID="ctlBranch" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlBranch" Display="Dynamic" InitialValue="-- Select --"
                    runat="server" ID="RequiredFieldValidator1" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
      <tr runat="server" id="trType">
            <td align="right">
                Product Group
            </td>
            <td>
                <asp:DropDownList ID="ctlProductGroup" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlProductGroup" Display="Dynamic"
                    InitialValue="-- Select --" runat="server" ID="RequiredFieldValidator3" ValidationGroup="cmd"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr> 
        <tr>
            <td align="right">
                Name
            </td>
            <td>
                <asp:TextBox ID="ctlName" runat="server" ToolTip="3-20 Char length & Allowed A-Z a-z 0-9 _ . only" />
                <asp:RequiredFieldValidator ControlToValidate="ctlName" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator4" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
       <%-- <tr runat="server" id="trProductCode">
            <td align="right">
                Product Code
            </td>
            <td>
                <asp:TextBox ID="ctlProductCode" runat="server" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="ctlProductCode"
                    runat="server" ValidationExpression="^([a-zA-Z0-9_-]{3,20})$" Display="Dynamic"
                    ValidationGroup="cmd" ErrorMessage="Not valid">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ControlToValidate="ctlProductCode" Display="Dynamic"
                    runat="server" ID="RequiredFieldValidator5" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
        <tr runat="server" id="tr1">
            <td align="right">
                Unit
            </td>
            <td>
                <asp:DropDownList ID="ctlUnit" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlUnit" Display="Dynamic" InitialValue="-- Select --"
                    runat="server" ID="RequiredFieldValidator2" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
       <%-- <tr>
            <td align="right">
                Commodity Code
            </td>
            <td>
                <asp:TextBox ID="ctlCommodityCode" runat="server" ToolTip="3-20 Char length & Allowed A-Z a-z 0-9 _ . only" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="ctlCommodityCode"
                    runat="server" ValidationExpression="^([a-zA-Z0-9_-]{3,20})$" Display="Dynamic"
                    ValidationGroup="cmd" ErrorMessage="Not valid">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ControlToValidate="ctlCommodityCode" Display="Dynamic"
                    runat="server" ID="RequiredFieldValidator6" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
        <tr>
            <td align="right">
                Reorder Level
            </td>
            <td>
                <asp:TextBox ID="ctlReorderLevel" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlReorderLevel" Display="Dynamic"
                    runat="server" ID="RequiredFieldValidator12" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Purchase Price
            </td>
            <td>
                <asp:TextBox ID="ctlPurchasePrice" runat="server" ToolTip="ex) 0.5,11.0,11" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="ctlPurchasePrice"
                    Display="Dynamic" runat="server" ValidationExpression="^(\-)?\d*(\.\d+)?$" ValidationGroup="cmd"
                    ErrorMessage="Not valid" />
                <asp:RequiredFieldValidator ControlToValidate="ctlPurchasePrice" Display="Dynamic"
                    runat="server" ID="RequiredFieldValidator7" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Whole Seller Price
            </td>
            <td>
                <asp:TextBox ID="ctlWholesellerPrice" runat="server" ToolTip="ex) 0.5,11.0,11" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="ctlWholesellerPrice"
                    Display="Dynamic" runat="server" ValidationExpression="^(\-)?\d*(\.\d+)?$" ValidationGroup="cmd"
                    ErrorMessage="Not valid" />
                <asp:RequiredFieldValidator ControlToValidate="ctlWholesellerPrice" Display="Dynamic"
                    runat="server" ID="RequiredFieldValidator8" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Retailer Price
            </td>
            <td>
                <asp:TextBox ID="ctlRetailerPrice" runat="server" ToolTip="ex) 0.5,11.0,11" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="ctlRetailerPrice"
                    Display="Dynamic" runat="server" ValidationExpression="^(\-)?\d*(\.\d+)?$" ValidationGroup="cmd"
                    ErrorMessage="Not valid" />
                <asp:RequiredFieldValidator ControlToValidate="ctlRetailerPrice" Display="Dynamic"
                    runat="server" ID="RequiredFieldValidator9" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Op Stock
            </td>
            <td>
                <asp:TextBox ID="ctlOpStock" runat="server" ToolTip="ex) 0.5,11.0,11" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="ctlOpStock"
                    Display="Dynamic" runat="server" ValidationExpression="^(\-)?\d*(\.\d+)?$" ValidationGroup="cmd"
                    ErrorMessage="Not valid" />
                <asp:RequiredFieldValidator ControlToValidate="ctlOpStock" Display="Dynamic" runat="server"
                    ID="RequiredFieldValidator10" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                VatTax
            </td>
            <td>
              <asp:DropDownList ID="ctlVat" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="ctlVat" Display="Dynamic" runat="server"  InitialValue="-- Select --"
                    ID="RequiredFieldValidator11" ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="formSubmit">
                <asp:Button ID="btnCreate" runat="server" Text=" Update " OnClick="btnUpdate_Click"
                    ValidationGroup="cmd" />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
