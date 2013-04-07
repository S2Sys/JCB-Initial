<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportReorderLevel.aspx.cs" Inherits="JCB.UI.ReportReorderLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="scriptMgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="pageTitle" colspan="2">
                        <%-- <asp:Label runat="server" ID="lblType" />--%>
                        Reorder Level Report
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="searchBox">
                            <tr>
                                <td>
                                    <table cellpadding="5" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                Group
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlGroup" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:LinkButton runat="server" ID="hlAdd" class="swap" OnClick="reportSearch"><img src="Images/Icons/Add_on.png"  alt="Click here to add new Item?"/> Search ? </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                <tr>
                    <td colspan="2">
                        <table cellpadding="5" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="right">
                                    <img src="Images/Icons/Printer.png" onclick="javascript:PrintMe('print1');" />
                                </td>
                            </tr>
                            <tr>
                                <td id="print1">
                                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" CellPadding="0"
                                        CellSpacing="0" BorderWidth="0px" GridLines="None" Width="100%" CssClass="gv gvSmall"
                                        ShowFooter="true">
                                        <HeaderStyle CssClass="h" />
                                        <RowStyle CssClass="r" />
                                        <AlternatingRowStyle CssClass="ar" />
                                        <PagerStyle CssClass="pager" />
                                        <FooterStyle CssClass="fr" />
                                        <SortedAscendingHeaderStyle CssClass="sortAsc" />
                                        <SortedDescendingHeaderStyle CssClass="sortDesc" />
                                        <EmptyDataRowStyle CssClass="empty" />
                                        <EmptyDataTemplate>
                                            Sorry! No item(s) found in the Report.
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="ProductId" HeaderText="ProductId" Visible="false" />
                                            <asp:TemplateField HeaderText="Name" SortExpression="Product.ProductGroup.Name">
                                                <ItemTemplate>
                                                    <%# Eval("Product.ProductGroup.Name")%>-<%# Eval("Product.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch" SortExpression="Branch.Name">
                                                <ItemTemplate>
                                                    <%# Eval("Branch.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--     <asp:TemplateField HeaderText="Opening Stock" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Product.OpStock")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Buy Qty" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("BuyQuantity")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sell Qty" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("SellQuantity")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Reorder Level" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Product.ReorderLevel", "{0:#,##0.00;(#,##0.00);0}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stock In Hand" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("OutstandingQuantity", "{0:#,##0.00;(#,##0.00);0}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <%--<script src="Service/Script.Transaction.js" type="text/javascript"></script>
            <script type="text/javascript">
                $(function () {
                    $(".datepicker").datepicker();
                });
            </script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
