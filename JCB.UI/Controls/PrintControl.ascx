<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintControl.ascx.cs"
    Inherits="JCB.UI.Controls.PrintControl" %>
<table cellpadding="0" cellspacing="0" border="0px" width="800px">
    <tr>
        <td align="center">
          <b>  <asp:Label ID="lblMode" runat="server" CssClass="lblMode"></asp:Label></b> 
        </td>
    </tr>
    <tr>
        <td align="center" style="height: 80px">
            <h1>
                OFFICE HEADER HERE
            </h1>
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="5" cellspacing="0" border="0px" width="100%">
                <tr>
                    <td width="33%">
                    </td>
                    <td width="33%">
                    </td>
                    <td width="33%">
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" colspan="2">
                        <span class="dontShow">To :</span>
                        <asp:Label runat="server" CssClass="lblTo" ID="lblTo"></asp:Label>
                    </td>
                    <td>
                        <span class="dontShow">Bill No :</span>
                        <asp:Label ID="lblBillNo" runat="server" CssClass="lblBillNo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="dontShow">Date:</span>
                        <asp:Label ID="lblDate" runat="server" CssClass="lblDate"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" colspan="1">
                        <span class="dontShow">Tin :</span>
                        <asp:Label runat="server" CssClass="lblTin" ID="lblTin"></asp:Label>
                    </td>
                    <td rowspan="2" colspan="1">
                        <span class="dontShow">Phone:</span>
                        <asp:Label runat="server" CssClass="lblPhone" ID="lblPhone"></asp:Label>
                    </td>
                    <td>
                        <span class="dontShow">LR:</span>
                        <asp:Label ID="lblLRNo" runat="server" CssClass="lblLRNo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="dontShow">Through:</span>
                        <asp:Label ID="lblThrough" runat="server" CssClass="lblThrough"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="dontShow">Ref :</span>
                        <asp:Label ID="lblRef" runat="server" CssClass="lblRef"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" CellPadding="0"
                CellSpacing="0" BorderWidth="0px" GridLines="None" Width="100%" CssClass="gv gvSmall"
                ShowFooter="false" DataKeyNames="TempID" s>
                <HeaderStyle CssClass="h  dontShow" />
                <RowStyle CssClass="r" />
                <AlternatingRowStyle CssClass="ar" />
                <PagerStyle CssClass="pager" />
                <FooterStyle CssClass="fr" />
                <SortedAscendingHeaderStyle CssClass="sortAsc" />
                <SortedDescendingHeaderStyle CssClass="sortDesc" />
                <EmptyDataRowStyle CssClass="empty" />
                <EmptyDataTemplate>
                    Sorry! No item(s) found in the Order.
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="rightAlign" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="45%">
                        <ItemTemplate>
                            <%# Eval("Product.ProductGroup.Name")%>-<%# Eval("Product.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="rightAlign" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <%# Eval("Quantity", "{0:#,##0.00;(#,##0.00);0.00}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate" ItemStyle-CssClass="rightAlign" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <%# Eval("Rate", "{0:#,##0.00;(#,##0.00);0.00}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount in RS" ItemStyle-CssClass="rightAlign" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <%# Eval("TotalDiscount", "{0:#,##0.00;(#,##0.00);0.00}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--  <asp:BoundField DataField="TotalPrice" HeaderText="Amont" ItemStyle-CssClass="rightAlign"
            ReadOnly="true" />
        <asp:BoundField DataField="TotalDiscount" HeaderText="Discount" ItemStyle-CssClass="rightAlign"
            ReadOnly="true" />--%>
                    <asp:BoundField DataField="VatTax" HeaderText="VAT %" ItemStyle-CssClass="rightAlign"
                        ItemStyle-Width="10%" ReadOnly="true" DataFormatString="{0:#,##0.00;(#,##0.00);0}" />
                    <asp:BoundField DataField="TotalAmountPrint" HeaderText="Amount" ItemStyle-CssClass="rightAlign"
                        DataFormatString="{0:#,##0.00;(#,##0.00);0.00}" ReadOnly="true" ItemStyle-Width="10%" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0px" width="100%">
                <tr>
                    <td rowspan="5" style="width: 35%">
                    </td>
                    <td width="15%" class="rightAlign">
                        <span class="dontShow">Sub Total</span>
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblSTQty" runat="server" CssClass="lblSTQty"></asp:Label>
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblTotal" runat="server" CssClass="lblTotal"></asp:Label>
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblSTDis" runat="server" CssClass="lblSTDis"></asp:Label>
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblSTAmt" runat="server" CssClass="lblSTAmt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rightAlign">
                        <span class="dontShow">Vat 5 %</span>
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblVAT5" runat="server" CssClass="lblVAT5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rightAlign">
                        <span class="dontShow">Vat 14.5 %</span>
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblVAT14" runat="server" CssClass="lblVAT14"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rightAlign">
                        <span class="dontShow">Round Off</span>
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblRound" runat="server" CssClass="lblRound"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rightAlign">
                        <span class="dontShow">Grand Total</span>
                    </td>
                    <td width="10%" class="rightAlign" colspan="4">
                        <asp:Label ID="lblRupees" runat="server" CssClass="lblRupees"></asp:Label>
                    </td>
                    <td width="10%" class="rightAlign">
                        <asp:Label ID="lblGTotal" runat="server" CssClass="lblTotal"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
