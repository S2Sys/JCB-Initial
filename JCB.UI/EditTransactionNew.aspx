<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditTransactionNew.aspx.cs" Inherits="JCB.UI.EditTransactionNew" %>

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
                                            <td align="left" runat="server" id="cellBLbl">
                                                Branch
                                            </td>
                                            <td align="left">
                                                Product
                                            </td>
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
                                            <td id="tdBC" align="left" runat="server">
                                                <asp:DropDownList runat="server" ID="ctlBranch" AutoPostBack="true" Enabled="false">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlBranch" Display="None" runat="server"
                                                    InitialValue="-- Select --" ID="RequiredFieldValidator3" ValidationGroup="top"
                                                    ErrorMessage="Branch Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:ValidationSummary ID="sumtop" runat="server" ValidationGroup="top" ShowSummary="false"
                                                    ShowMessageBox="true" />
                                                <asp:DropDownList runat="server" ID="ctlProduct" CssClass="productSelect" AutoPostBack="true"
                                                    CausesValidation="true" ValidationGroup="top" OnSelectedIndexChanged="productSelectionChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlUnit" CssClass="punit" Enabled="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlQty" Width="40" Text="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlQty" Display="None" runat="server"
                                                    ID="RequiredFieldValidator6" ValidationGroup="add" ErrorMessage="Quantity Required">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="ctlQty"
                                                    Display="Dynamic" runat="server" ValidationExpression="^[1-9]+[0-9]*$" ValidationGroup="cmd"
                                                    ErrorMessage="Only positive integers">
                                                </asp:RegularExpressionValidator>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlRate" Width="75" CssClass="prate"></asp:TextBox>
                                                <img runat="server" id="resetRate" src="~/Images/Icons/Help.png" onclick="SetRate()"
                                                    title="Set Rate as Wholesale Price" />
                                                <asp:RequiredFieldValidator ControlToValidate="ctlRate" Display="None" runat="server"
                                                    ID="RequiredFieldValidator7" ValidationGroup="add" ErrorMessage="Quantity Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlDis" Width="40" Text="0"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlDisType" CssClass="punit">
                                                    <asp:ListItem Text="%" Value="%" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="RS" Value="RS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlVatTax" Width="120px" Enabled="false">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlVatTax" Display="None" runat="server"
                                                    InitialValue=" -- Select --" ID="RequiredFieldValidator8" ValidationGroup="add"
                                                    ErrorMessage="Vat/Tax Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <%--<asp:Button runat="server" ID="btnAdd" Width="40" Text="Add"></asp:Button>--%>
                                            <td style="vertical-align: top; text-align: right;" valign="middle" align="right">
                                                <a class="swap" id="addItem">
                                                    <asp:HiddenField ID="ctlWholesalePrice" runat="server" Value="-1" />
                                                    <asp:HiddenField ID="hdnStock" runat="server" Value="-1" />
                                                    <asp:HiddenField ID="ctlHdnProductId" runat="server" />
                                                    <asp:HiddenField ID="ctlHdnPgId" runat="server" />
                                                    <asp:HiddenField ID="ctlHdnUnitId" runat="server" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="add"
                                                        ShowSummary="false" ShowMessageBox="true" />
                                                    <asp:LinkButton runat="server" OnClientClick="javascript:return CheckStockCount();"
                                                        ValidationGroup="add" ID="hlAdd" class="swap" OnClick="ItemAdded"><img src="Images/Icons/Add_on.png" alt="Click here to add new Item?" /> Add</asp:LinkButton>
                                                    <script>
                                                        function CheckStockCount() {
                                                            var stockCount = $('#<%=hdnStock.ClientID %>').val();
                                                            var givenValue = $('#<%=ctlQty.ClientID %>').val();
                                                            if (stockCount != '-1') {
                                                                if (parseInt(givenValue) <= parseInt(stockCount)) {
                                                                    $('#<%=hdnStock.ClientID %>').val(parseInt(stockCount) - parseInt(givenValue));
                                                                    return true;
                                                                }
                                                                else {
                                                                    alert("You have only " + stockCount + " item in your hand, You cannot give more than this in Quantity");
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }
                                                    </script>
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
                                <td>
                                    <table cellpadding="5" cellspacing="0" border="0" width="100%">
                                        <%-- <tr>
                                            <td>
                                                <table cellpadding="5" cellspacing="0" border="0">--%>
                                        <tr>
                                            <td align="left">
                                                Date
                                            </td>
                                            <td align="left">
                                                Mode
                                            </td>
                                            <td align="left" runat="server" id="tdInvoice_0">
                                                <asp:Label ID="lblTransactionType" runat="server"></asp:Label>
                                            </td>
                                            <td id="tdPRN_0" runat="server" align="left">
                                                Purchase Request No
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblUserType" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                       <%-- </td> </tr>--%>
                                        <tr>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlDate" Width="80px"></asp:TextBox>
                                                <asp:Image ImageUrl="~/Images/Icons/Date.png" CssClass="calImage" runat="server"
                                                    ID="calButton" />
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="ctlDate">
                                                </asp:CalendarExtender>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="calButton"
                                                    Format="dd/MM/yyyy" TargetControlID="ctlDate">
                                                </asp:CalendarExtender>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlDate" Display="None" runat="server"
                                                    ID="RequiredFieldValidator1" ValidationGroup="save" ErrorMessage="Date Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlMode" AutoPostBack="True" Enabled="false"
                                                    OnSelectedIndexChanged="modeChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlMode" Display="None" runat="server"
                                                    InitialValue="-- Select --" ID="RequiredFieldValidator2" ValidationGroup="save"
                                                    ErrorMessage="Mode Required"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td id="tdInvoice_1" runat="server">
                                                <asp:TextBox runat="server" ID="ctlInvoice" Width="60px" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlInvoice" Display="None" runat="server"
                                                    ID="RequiredFieldValidator4" ValidationGroup="save" ErrorMessage="Invoice Required"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left" runat="server" id="tdPRN_1">
                                                <asp:TextBox runat="server" ID="ctlReference" Width="200px" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlReference" Display="None" runat="server"
                                                    ID="rfReference" ValidationGroup="save" ErrorMessage="Reference Required"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlSupplier" Enabled="false" 
                                                    AutoPostBack="True" onselectedindexchanged="userChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="ctlUName" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlSupplier" Display="None" runat="server"
                                                    InitialValue="-- Select --" ID="RequiredFieldValidator5" ValidationGroup="save"
                                                    ErrorMessage="Supplier Required"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left" class="formSubmit">
                                                <asp:Button ID="Button1" runat="server" Text="Update" ValidationGroup="save" OnClick="btnOrder_Click" />
                                            </td>
                                        </tr>


                                         <tr runat="server" id="trSales0" visible="false">
                                            <td>
                                                LR No
                                            </td>
                                            <td>
                                                Reference
                                            </td>
                                            <td>
                                                Through
                                            </td>
                                            <td>
                                               
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trSales1" visible="false">
                                            <td align="left" id="tdlr1" runat="server">
                                                <asp:TextBox runat="server" ID="ctlLr" Width="100px"></asp:TextBox>
                                            </td>
                                            <td align="left" id="td1ref1" runat="server">
                                                <asp:TextBox runat="server" ID="ctlRef" Width="100px"></asp:TextBox>
                                            </td>
                                            <td align="left" id="tdThorught1" runat="server">
                                                <asp:TextBox runat="server" ID="ctlThrough" Width="100px"></asp:TextBox>
                                            </td>
                                            <td> <asp:CheckBox ID="printRequired" Text="Print This Order?" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="s1" runat="server" ShowMessageBox="true" ShowSummary="true"
                            ValidationGroup="save" />
                    </td>
                </tr>
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
                                        <%# Eval("Product.ProductGroup.Name")%>-<%# Eval("Product.Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ControlStyle-CssClass="rightAlign" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# Eval("Quantity", "{0:#,##0.00;(#,##0.00);0}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="ctlQuantity" Text='<%# Eval("Quantity","{0:#,##0.00;(#,##0.00);0}")%>'
                                            Width="50"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" ControlStyle-CssClass="rightAlign" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# Eval("Rate","{0:#,##0.00;(#,##0.00);0}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="ctlRate" Text='<%# Eval("Rate","{0:#,##0.00;(#,##0.00);0}")%>'
                                            Width="50"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount" ControlStyle-CssClass="rightAlign" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# Eval("DiscountDetail")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="ctlDiscount" Text='<%# Eval("Discount","{0:#,##0.00;(#,##0.00);0}")%>'
                                            Width="50"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="ctlDisType" CssClass="punit">
                                            <asp:ListItem Text="%" Value="%"></asp:ListItem>
                                            <asp:ListItem Text="RS" Value="RS"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT %" ControlStyle-CssClass="rightAlign" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# Eval("VatTax", "{0:#,##0.00;(#,##0.00);0}")%>
                                    </ItemTemplate>
                                    <%--<EditItemTemplate>
                                        <asp:TextBox runat="server" ID="ctlVatTax" Text='<%# Eval("VatTax","{0:#,##0.00;(#,##0.00);0}")%>'
                                            Width="50"></asp:TextBox>
                                    </EditItemTemplate>--%>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TotalPrice" HeaderText="Amont" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                    ItemStyle-HorizontalAlign="Right" ReadOnly="true" />
                                <asp:BoundField DataField="TotalDiscount" HeaderText="Discount" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                    ItemStyle-HorizontalAlign="Right" ReadOnly="true" />
                                <asp:BoundField DataField="TotalVAT" HeaderText="VAT" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                    ItemStyle-HorizontalAlign="Right" ReadOnly="true" />
                                <asp:BoundField DataField="FinalTotal" HeaderText="Total Amount" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                    ItemStyle-HorizontalAlign="Right" ReadOnly="true" />
                                <asp:TemplateField HeaderText="" SortExpression="" ItemStyle-CssClass="dontShow"
                                    FooterStyle-CssClass="dontShow" HeaderStyle-CssClass="dontShow">
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
            </td> </tr> </table>
            <script type="text/javascript">

                function SetRate() {
                    var actual = $('#<%=ctlRate.ClientID %>');
                    var wholesale = $('#<%=ctlWholesalePrice.ClientID %>');

                    var aVal = actual.val();
                    var wVal = wholesale.val();


                    actual.val(wVal);
                    wholesale.val(aVal);
                    if (wVal != '-1') {
                        if (actual.attr('title') == 'Set Rate as Wholesale Price') {
                            actual.attr('title', 'Set Rate as Retailer Price');
                        }
                        else {
                            actual.attr('title', 'Set Rate as Wholesale Price');
                        }
                    }
                }
                function PrintForm(url, redirect) {
                    var printWindow = window.open(url, '', 'letf=0,top=0,toolbar=0,scrollbars=0,status=0,width=0,height=0');
                    //PrintMe('PrintFormOnSubmit');
                    window.location = redirect;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
