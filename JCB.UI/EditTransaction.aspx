<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditTransaction.aspx.cs" Inherits="JCB.UI.EditTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="scriptMgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="pageTitle" colspan="2">
                        Edit
                        <asp:Label runat="server" ID="lblType" />
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
                                                Date
                                            </td>
                                            <td align="left">
                                                Mode
                                            </td>
                                            <td align="left">
                                                <asp:Label runat="server" ID="lblTransactionType"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label runat="server" ID="lblUserType"></asp:Label>
                                            </td>
                                            <td align="left">
                                                Product
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlDate" Width="80px" Enabled="false"></asp:TextBox>
                                                <asp:Image ImageUrl="~/Images/Icons/Date.png" CssClass="calImage" runat="server"
                                                    ID="calButton" Enabled="false" />
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="ctlDate">
                                                </asp:CalendarExtender>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="calButton"
                                                    Format="dd/MM/yyyy" TargetControlID="ctlDate">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlMode">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlInvoice" Width="100px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlSupplier">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlProduct" CssClass="productSelect" AutoPostBack="true"
                                                    OnSelectedIndexChanged="productSelectionChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="5" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left">
                                                Product Group
                                            </td>
                                            <%--  <td align="left">
                                                Product
                                            </td>--%>
                                            <td align="left">
                                                Unit
                                            </td>
                                            <td align="left">
                                                Qty
                                            </td>
                                            <td align="left">
                                                Rate
                                            </td>
                                            <td align="left">
                                                Dis
                                            </td>
                                            <td align="left">
                                                Dis Type
                                            </td>
                                            <td align="left">
                                                Vat Tax
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlPG" CssClass="pg">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="ctlHdnProductId" runat="server" />
                                            </td>
                                            <%--  <td align="left">
                                               
                                                <asp:TextBox runat="server" ID="ctlPN" CssClass="pname"></asp:TextBox>
                                            </td>--%>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlUnit" CssClass="punit">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlQty" Width="40" Text="1"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlRate" Width="75" CssClass="prate"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlDis" Width="40"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlDisType" CssClass="punit">
                                                    <asp:ListItem Text="%" Value="%"></asp:ListItem>
                                                    <asp:ListItem Text="RS" Value="RS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlVatTax" Width="40"></asp:TextBox>
                                            </td>
                                            <%--<asp:Button runat="server" ID="btnAdd" Width="40" Text="Add"></asp:Button>--%>
                                            <td style="vertical-align: top; text-align: right;" valign="middle" align="right">
                                                <%--<a class="swap" id="addItem"><img src="Images/Icons/Add_on.png"  alt="Click here to add new Item?"/> Add Product</a>--%>
                                                <asp:LinkButton runat="server" ID="hlAdd" class="swap" OnClick="ItemAdded"><img src="Images/Icons/Add_on.png"  alt="Click here to add new Item?"/> Add Product</asp:LinkButton>
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
                                            <asp:BoundField DataField="ProductId" HeaderText="ProductId" Visible="false" />
                                            <asp:TemplateField HeaderText="Name" SortExpression="Product.Name">
                                                <ItemTemplate>
                                                    <%# Eval("Product.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group" SortExpression="Product.ProductGroup.Name">
                                                <ItemTemplate>
                                                    <%# Eval("Product.ProductGroup.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ControlStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Quantity")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="ctlQuantity" Text='<%# Eval("Quantity")%>' Width="50"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate" ControlStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Rate")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="ctlRate" Text='<%# Eval("Rate")%>' Width="50"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount" ControlStyle-CssClass="rightAlign">
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
                                            <asp:TemplateField HeaderText="VAT %" ControlStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("VatTax")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="ctlVatTax" Text='<%# Eval("VatTax")%>' Width="50"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TotalPrice" HeaderText="Price" ItemStyle-CssClass="rightAlign"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="TotalDiscount" HeaderText="Discount" ItemStyle-CssClass="rightAlign"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="TotalVAT" HeaderText="VAT" ItemStyle-CssClass="rightAlign"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="FinalTotal" HeaderText="Total Amount" ItemStyle-CssClass="rightAlign"
                                                ReadOnly="true" />
                                            <asp:TemplateField HeaderText="" SortExpression="" ItemStyle-CssClass="dontShow" FooterStyle-CssClass="dontShow" HeaderStyle-CssClass="dontShow">
                                                <ItemTemplate>
                                                    <%-- <asp:ImageButton ID="btnedit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("TempID")%>'
                                                        ImageUrl="~/Images/Icons/Edit.png" /></asp:ImageButton>--%>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" CommandArgument='<%# Eval("TempID")%>'><img src="Images/Icons/Edit.png" /></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" CommandArgument='<%# Eval("TempID")%>'><img src="Images/Icons/Garbage.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" CommandArgument='<%# Eval("TempID")%>'><img src="Images/Icons/Save.png" /></asp:LinkButton>
                                                    <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel"><img src="Images/Icons/Cancel.png" /></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="Server" NavigateUrl='<%# "~/EditUser.aspx?Id="+ Eval("UniqueId")%>'><img src="Images/Icons/Edit.png" /></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="formSubmit" style="text-align: right">
                                    <asp:Button ID="btnOrder" runat="server" Text=" Update " OnClick="btnOrder_Click" />
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
