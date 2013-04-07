<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageMetadata.aspx.cs" Inherits="JCB.UI.ManageMetadata" %>

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
                            <asp:HyperLink runat="server" ID="hlAdd" class="swap">
                                <img src="Images/Icons/Add_on.png" alt="Click here to create new user" />
                                New
                                <asp:Label runat="server" ID="lblType1" />?</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <img src="Images/Icons/Printer.png" onclick="javascript:PrintMe('print1');" />
            </td>
        </tr>
        <tr>
            <td colspan="2" id="print1">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" CellPadding="0"
                    CellSpacing="0" BorderWidth="0px" GridLines="None" Width="100%" CssClass="gv"
                    OnRowCommand="gv_RC" AllowPaging="true" AllowSorting="true" OnSorting="gv_Sorting"
                    OnPageIndexChanging="gv_Paging" PageSize="10">
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
                        Sorry! No record(s) found.</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" ToolTip="View Product" runat="Server" NavigateUrl='<%# "~/ManageProducts.aspx?Parent="+ Eval("UniqueId")%>'> <%#Eval("Name")%>
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Description" SortExpression="Description" HeaderText="Description" />
                        <%--<asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Metadata Type" />
                         <asp:TemplateField HeaderText="Branch Name" SortExpression="Branch.Name">
                            <ItemTemplate>
                                <%# Eval("Branch.Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>  --%>
                        <asp:BoundField DataField="CreatedOn" SortExpression="CreatedOn" HeaderText="Created Date"
                            DataFormatString="{0:dd/MM/yy}" />
                        <asp:TemplateField HeaderText="" SortExpression="" ItemStyle-CssClass="dontShow"
                            FooterStyle-CssClass="dontShow" HeaderStyle-CssClass="dontShow">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="Server" Visible="<%# ShowNewProductOption %>"
                                    ToolTip="View Stock Report" NavigateUrl='<%# "~/ReportStock.aspx?Parent="+ Eval("UniqueId")%>'><img src="Images/Icons/stockreport.png" />
                                </asp:HyperLink>
                                <asp:HyperLink ID="HyperLink5" runat="Server" Visible="<%# ShowNewProductOption %>"
                                    ToolTip="New Product" NavigateUrl='<%# "~/CreateProduct.aspx?Id="+ Eval("UniqueId")%>'><img src="Images/Icons/Add_on.png" />
                                </asp:HyperLink>
                                <asp:HyperLink ID="HyperLink1" runat="Server" ToolTip="Edit Item" NavigateUrl='<%# "~/EditMetadata.aspx?Id="+ Eval("UniqueId")%>'><img src="Images/Icons/Edit.png" />
                                </asp:HyperLink>
                                <asp:LinkButton ID="del" CommandName="D" CommandArgument='<%#Eval("Id")%>' runat="server"
                                    ToolTip="Delete Item" OnClientClick="return confirm('Are you sure?');">
                                    <img src="Images/Icons/Cancel.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
            </td>
        </tr>
    </table>
</asp:Content>
