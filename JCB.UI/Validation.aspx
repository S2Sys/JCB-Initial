<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Validation.aspx.cs" Inherits="JCB.UI.Validation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <table cellpadding="5" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="pageTitle" colspan="2">
                Create
                <asp:Label runat="server" ID="lblType" />
            </td>
        </tr>
        <tr>
            <td width="200px" align="right">
            </td>
            <td>
            </td>
        </tr>
        <tr runat="server" id="trBranch">
            <td align="right">
                Branch
            </td>
            <td>
                <asp:DropDownList ID="ctlBranch" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="trType">
            <td align="right">
                Type
            </td>
            <td>
                <asp:DropDownList ID="ctlType" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Name
            </td>
            <td>
                <asp:TextBox ID="ctlName" runat="server"  />
            </td>
        </tr>
        <tr runat="server" id="trUsername">
            <td align="right">
                Username <span class="validate">*</span>
            </td>
            <td>
                <asp:TextBox ID="ctlUsername" runat="server" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="ctlUsername" 
                runat="server"   
                ValidationExpression="^([a-zA-Z0-9_-]{3,20})$" Display="Dynamic"
                    ValidationGroup="cmd" ErrorMessage="3-20 Char length & Allowed A-Z a-z 0-9 _ . only">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr runat="server" id="trPassword">
            <td align="right">
                Password <span class="validate">*</span>
            </td>
            <td>
                <asp:TextBox ID="ctlPassword" runat="server" TextMode="Password"  />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="ctlPassword" 
                runat="server" 
                ValidationExpression="^([a-zA-Z0-9_-]{3,20})$" Display="Dynamic"
                    ValidationGroup="cmd" ErrorMessage="3-20 Char length & Allowed Char & Numbers only">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Address
            </td>
            <td>
                <asp:TextBox ID="ctlAddress" runat="server" TextMode="MultiLine" Height="100" />
            </td>
        </tr>
        <tr>
            <td align="right">
                3-20 Char length & Allowed A-Z a-z _' only
            </td>
            <td>
                <asp:TextBox ID="ctlCity" runat="server"   />
                 
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="ctlCity" 
                runat="server" 
                ValidationExpression="^([a-zA-Z\s'_-]{3,20})$" Display="Dynamic"
                    ValidationGroup="cmd" ErrorMessage="3-20 Char length & Allowed A-Z a-z _' only">
                </asp:RegularExpressionValidator>

            </td>
        </tr>
        <tr>
            <td align="right">
                Required
            </td>
            <td>
                <asp:DropDownList ID="ctlState" runat="server" />
               <asp:RequiredFieldValidator ControlToValidate="ctlState" Display="Dynamic" InitialValue="-- Select --" runat="server" ID="RequiredFieldValidator2"
                    ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Mobile
            </td>
            <td>
                <asp:TextBox ID="ctlPhone" runat="server"  />

                  <asp:RequiredFieldValidator ControlToValidate="ctlPhone" Display="Dynamic" runat="server" ID="RequiredFieldValidator1"
                    ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="ctlPhone"  Display="Dynamic"
                runat="server" 
                ValidationExpression="^(((0|((\+)?91(\-)?))|((\((\+)?91\)(\-)?)))?[7-9]\d{9})?$"
                    ValidationGroup="cmd" ErrorMessage="Valid Mobile no">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                PIN
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"  />

                  <asp:RequiredFieldValidator ControlToValidate="TextBox1" Display="Dynamic" runat="server" ID="RequiredFieldValidator3"
                    ValidationGroup="cmd" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="ctlPhone"  Display="Dynamic"
                runat="server" 
                ValidationExpression="([0-9]{6}|[0-9]{3}\s[0-9]{3})"
                    ValidationGroup="cmd" ErrorMessage="Valid PIN">
                </asp:RegularExpressionValidator>
            </td>
        </tr>

        
        <tr>
            <td align="right">
                Integers
            </td>
            <td>
                <asp:TextBox ID="ctlMobile" runat="server"   />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="ctlMobile"  Display="Dynamic"
                runat="server" 
                ValidationExpression="^\d+$"
                    ValidationGroup="cmd" ErrorMessage="Only integers">
                </asp:RegularExpressionValidator>


                
            </td>
        </tr>
        <tr runat="server" id="trTin">
            <td align="right">
                + Integers
            </td>
            <td>
                <asp:TextBox ID="ctlTin" runat="server"   />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="ctlTin"  Display="Dynamic"
                runat="server" 
                ValidationExpression="^[1-9]+[0-9]*$"
                    ValidationGroup="cmd" ErrorMessage="Only positive integers">
                </asp:RegularExpressionValidator>


                
            </td>
        </tr>
        <tr runat="server" id="trCst">
            <td width="50%" align="right">
                Max 100
            </td>
            <td>
                <asp:TextBox ID="ctlCst" runat="server"   />

                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="ctlCst"  Display="Dynamic"
                runat="server" 
                ValidationExpression="^((100)|(\d{0,2}))$"
                    ValidationGroup="cmd" ErrorMessage="Cannot exceed 100">
                </asp:RegularExpressionValidator>

                
            </td>
        </tr>
        <tr runat="server" id="trOB">
            <td width="50%" align="right">
                Floats
            </td>
            <td>
                <asp:TextBox ID="ctlOB" runat="server"  />

                <asp:RegularExpressionValidator  ID="RegularExpressionValidator7" ControlToValidate="ctlOB"  Display="Dynamic"
                runat="server" 
                ValidationExpression="^(\-)?\d*(\.\d+)?$"
                    ValidationGroup="cmd" ErrorMessage="Only positive integers">
                </asp:RegularExpressionValidator>

                
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnCreate" runat="server" Text=" Create "  ValidationGroup="cmd" />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " />
            </td>
        </tr>
    </table>
</asp:Content>
