<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportTransaction.aspx.cs" Inherits="JCB.UI.ReportTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="scriptMgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="pageTitle" colspan="2">
                        <asp:Label runat="server" ID="lblType" />
                        Report
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
                                                Start Date
                                            </td>
                                            <td align="left">
                                                End Date
                                            </td>
                                            <td align="left">
                                                <asp:Label runat="server" ID="lblUserType"></asp:Label>
                                            </td>
                                            <td>
                                                Mode
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlStrteDate" Width="80px"></asp:TextBox>
                                                <asp:Image ImageUrl="~/Images/Icons/Date.png" CssClass="calImage" runat="server"
                                                    ID="Image1" />
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="ctlStrteDate">
                                                </asp:CalendarExtender>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Image1"
                                                    Format="dd/MM/yyyy" TargetControlID="ctlStrteDate">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlEndDate" Width="80px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="ctlEndDate">
                                                </asp:CalendarExtender>
                                                <asp:Image ImageUrl="~/Images/Icons/Date.png" CssClass="calImage" runat="server"
                                                    ID="Image2" />
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="Image2"
                                                    Format="dd/MM/yyyy" TargetControlID="ctlEndDate">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlUser">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ctlMode" AutoPostBack="True"  >
                                                </asp:DropDownList>
                                            </td>
                                            <td style="vertical-align: top; text-align: right;" valign="middle" align="right">
                                                <%--<a class="swap" id="addItem"><img src="Images/Icons/Add_on.png"  alt="Click here to add new Item?"/> Add Product</a>--%>
                                                <asp:LinkButton runat="server" ID="hlAdd" class="swap" OnClick="reportSearch"><img src="Images/Icons/Search.png"  alt="Click here to Search"/> Search </asp:LinkButton>
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
                                        ShowFooter="true" OnRowDataBound="rowDataBound" OnRowCancelingEdit="rowCanceling"
                                        OnRowEditing="rowEditing" OnRowDeleting="rowDeleting" OnRowUpdating="rowUpdating"
                                        DataKeyNames="TempID" OnRowCreated="rowCreated">
                                        <HeaderStyle CssClass="h" />
                                        <RowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="pager" />
                                        <FooterStyle CssClass="fr" />
                                        <SortedAscendingHeaderStyle CssClass="sortAsc" />
                                        <SortedDescendingHeaderStyle CssClass="sortDesc" />
                                        <EmptyDataRowStyle CssClass="empty" />
                                        <EmptyDataTemplate>
                                            Sorry! No item(s) found in the Report.
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:dd/MM/yy}"
                                                SortExpression="TransactionDate" />
                                            <%-- <asp:BoundField DataField="InvoiceOrBillNo" HeaderText="Invoice/Bill" SortExpression="InvoiceOrBillNo" />--%>
                                            <asp:TemplateField HeaderText="Invoice/Bill" SortExpression="InvoiceOrBillNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Visible="<%#ShowPurchaseRequestNo %>"><%# Eval("Reference")%></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Visible="<%#ShowInvoiceNo %>"><%# Eval("InvoiceOrBillNo")%></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch" SortExpression="Branch.Name">
                                                <ItemTemplate>
                                                    <%# Eval("Branch.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User">
                                                <ItemTemplate>
                                                    <%# Eval("TransactionBy.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product-Group" SortExpression="Product.Name">
                                                <ItemTemplate>
                                                    <%# Eval("Product.ProductGroup.Name")%>-<%# Eval("Product.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode" SortExpression="ModeData.Name">
                                                <ItemTemplate>
                                                    <%# Eval("ModeData.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Quantity", "{0:#,##0.00;(#,##0.00);0}")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="ctlQuantity" Text='<%# Eval("Quantity", "{0:#,##0.00;(#,##0.00);0}")%>'
                                                        Width="50"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Rate", "{0:#,##0.00;(#,##0.00);0}")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="ctlRate" Text='<%# Eval("Rate", "{0:#,##0.00;(#,##0.00);0}")%>'
                                                        Width="50"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("DiscountDetail")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="ctlDiscount" Text='<%# Eval("Discount")%>' Width="50"></asp:TextBox>
                                                    <asp:DropDownList runat="server" ID="ctlDisType" CssClass="punit">
                                                        <asp:ListItem Text="%" Value="%"></asp:ListItem>
                                                        <asp:ListItem Text="RS" Value="RS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VAT %" ItemStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("VatTax", "{0:#,##0.00;(#,##0.00);0}")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="ctlVatTax" Text='<%# Eval("VatTax", "{0:#,##0.00;(#,##0.00);0}")%>'
                                                        Width="50"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TotalPrice" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                                HeaderText="Amount" ItemStyle-CssClass="rightAlign" ReadOnly="true" />
                                            <asp:BoundField DataField="TotalDiscount" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                                HeaderText="Discount" ItemStyle-CssClass="rightAlign" ReadOnly="true" />
                                            <asp:BoundField DataField="TotalVAT" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                                HeaderText="VAT" ItemStyle-CssClass="rightAlign" ReadOnly="true" />
                                            <asp:BoundField DataField="FinalTotal" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                                HeaderText="Total Amount" ItemStyle-CssClass="rightAlign" ReadOnly="true" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td align="right">
                                    <asp:Button ID="btnOrder" runat="server" Text="Order" OnClick="btnOrder_Click" />
                                </td>
                            </tr>--%>
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
            <script type="text/javascript">
                $('.hl').each(function () {

                    $(this).parent().addClass('orderSeparator');
                    // $('td', (this).parent()).addClass('orderSeparator');
                });


            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
