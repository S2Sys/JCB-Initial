<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageProducts.aspx.cs" Inherits="JCB.UI.ManageProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="pageTitle" colspan="2" width="100%">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="pageTitle">
                            Manage Products <asp:Label runat="server" ID="lblBranch"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: right;" valign="middle" align="right">
                            <asp:HyperLink runat="server" ID="hlAdd" class="swap"><img src="Images/Icons/Add_on.png"  alt="Click here to create new user"/> New Products?</asp:HyperLink>
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
                        Sorry! No product(s) found.
                    </EmptyDataTemplate>
                    <Columns>
                        <%--<asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />--%>
                        <asp:TemplateField HeaderText="Group/Name" SortExpression="ProductGroup.Name">
                            <ItemTemplate>
                                <%# Eval("Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch" SortExpression="Branch.Name">
                            <ItemTemplate>
                                <%# Eval("Branch.Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PurchasePrice" SortExpression="PurchasePrice" HeaderText="Purchase"  
                        DataFormatString="{0:#,##0.00;(#,##0.00);0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="WholesellerPrice" SortExpression="WholesellerPrice" HeaderText="Wholeseller"  DataFormatString="{0:#,##0.00;(#,##0.00);0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="RetailerPrice" SortExpression="RetailerPrice" HeaderText="Retailer"  DataFormatString="{0:#,##0.00;(#,##0.00);0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="OpStock" HeaderText="Op Stock"  DataFormatString="{0:#,##0.00;(#,##0.00);0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="ReOrderLevel" HeaderText="Re Order Level"  DataFormatString="{0:#,##0.00;(#,##0.00);0}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:TemplateField HeaderText="" SortExpression="" ItemStyle-CssClass="dontShow" FooterStyle-CssClass="dontShow" HeaderStyle-CssClass="dontShow">
                            <ItemTemplate>
                                <asp:HyperLink runat="Server" NavigateUrl='<%# "~/EditProduct.aspx?Id="+ Eval("UniqueId")%>'><img src="Images/Icons/Edit.png" /></asp:HyperLink>
                                <asp:LinkButton ID="del" CommandName="D" CommandArgument='<%#Eval("Id")%>' runat="server"
                                    OnClientClick="return confirm('Are you sure ?');">
                                 <img src="Images/Icons/Cancel.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
