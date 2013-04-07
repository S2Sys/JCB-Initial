<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageUser.aspx.cs" Inherits="JCB.UI.ManageUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="pageTitle" colspan="2" width="100%">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="pageTitle">
                            Manage
                            <asp:Label runat="server" ID="lblType" />
                        </td>
                        <td style="vertical-align: top; text-align: right;" valign="middle" align="right">
                            <asp:HyperLink runat="server" ID="hlAdd" class="swap"><img src="Images/Icons/Add_on.png"  alt="Click here to create new user"/> New User?</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="subHead" colspan="2">
                <img src="Images/Icons/Printer.png" onclick="javascript:MakePrint();" />
                <script>                    function MakePrint() {
                        LoadContent(document.location + '&Mode=Print', 'print1');
                    }</script>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="print1">
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" CellPadding="0"
                    CellSpacing="0" BorderWidth="0px" GridLines="None" Width="100%" CssClass="gv"
                    AllowPaging="true" AllowSorting="true" OnSorting="gv_Sorting" OnPageIndexChanging="gv_Paging"
                    PageSize="10" OnRowCommand="gvUsers_RowCommand">
                    <HeaderStyle CssClass="h" />
                    <RowStyle CssClass="r" />
                    <AlternatingRowStyle CssClass="ar" />
                    <PagerStyle CssClass="pager" />
                    <SortedAscendingHeaderStyle CssClass="sortAsc" />
                    <SortedDescendingHeaderStyle CssClass="sortDesc" />
                    <EmptyDataRowStyle CssClass="empty" />
                    <%--<PagerTemplate>
                        <asp:Button ID="Button1" runat="server" Text="First" CommandName="Page" CommandArgument="First"
                            Enabled="<%# gvUsers.PageIndex > 0 %>" />
                        <asp:Button ID="Button2" runat="server" Text="Prev" CommandName="Page" CommandArgument="Prev"
                            Enabled="<%# gvUsers.PageIndex > 0 %>" />
                        <span id="Span1" runat="server">Page
                            <%= gvUsers.PageIndex + 1%>
                            of
                            <%= gvUsers.PageCount%>
                        </span>
                        <asp:Button ID="Button3" runat="server" Text="Next" CommandName="Page" CommandArgument="Next"
                            Enabled="<%# gvUsers.PageIndex + 1 < gvUsers.PageCount %>" />
                        <asp:Button ID="Button4" runat="server" Text="Last" CommandName="Page" CommandArgument="Last"
                            Enabled="<%# gvUsers.PageIndex + 1 < gvUsers.PageCount %>" />
                    </PagerTemplate>--%>
                    <EmptyDataTemplate>
                        Sorry! No user(s) Report found.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
                        <asp:BoundField DataField="City" SortExpression="City" HeaderText="City" />
                        <asp:BoundField DataField="Username" SortExpression="Username" HeaderText="Username" />
                        <asp:BoundField DataField="Password" SortExpression="Password" HeaderText="Password" />
                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="State" SortExpression="State.Name">
                            <ItemTemplate>
                                <%# Eval("State.Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Mobile" SortExpression="Mobile" HeaderText="Mobile" />
                        <%--  <asp:TemplateField HeaderText="User Type" SortExpression="Type.Name">
                            <ItemTemplate>
                                <%# Eval("Type.Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Branch" SortExpression="Branch.Name">
                            <ItemTemplate>
                                <%# Eval("Branch.Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tin" SortExpression="Tin" HeaderText="Tin" />
                        <asp:BoundField DataField="Cst" SortExpression="Cst" HeaderText="Cst" />
                        <asp:BoundField DataField="OpeningBalance" SortExpression="OpeningBalance" HeaderText="Opening Balance" />
                        <asp:TemplateField HeaderText="" SortExpression="" ItemStyle-CssClass="dontShow"
                            FooterStyle-CssClass="dontShow" HeaderStyle-CssClass="dontShow">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" Visible='<%#ShowPassowrdReset %>' runat="Server" NavigateUrl='<%# "~/ResetPassword.aspx?Id="+ Eval("UniqueId")%>'
                                    ToolTip="Edit User Password"><img src="Images/Icons/EditPassword.png" /></asp:HyperLink>
                                <asp:HyperLink runat="Server" NavigateUrl='<%# "~/EditUser.aspx?Id="+ Eval("UniqueId")%>'
                                    ToolTip="Edit User"><img src="Images/Icons/Edit.png" /></asp:HyperLink>
                                <asp:LinkButton ID="del" CommandName="D" OnClientClick="return confirm('Are you sure ?');"
                                    CommandArgument='<%#Eval("Id")%>' runat="server"><img src="Images/Icons/Cancel.png"  ToolTip="Delete User" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            var urlVars = $.getUrlVars();
            mode = urlVars['Mode'];            
            if (mode == 'Print')
                PrintContent($('#print1').html());
        });
    </script>
     
</asp:Content>
