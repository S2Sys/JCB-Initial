<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportBalance.aspx.cs" Inherits="JCB.UI.ReportBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="scriptMgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="5" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="pageTitle">
                        Balance Report
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="searchBox">
                            <tr>
                                <td>
                                    <table cellpadding="5" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left" runat="server" id="cellBLbl">
                                                Branch
                                            </td>
                                            <td align="left">
                                                User Type
                                            </td>
                                            <td align="left">
                                                User
                                            </td>
                                            <td align="left">
                                                Start Date
                                            </td>
                                            <td align="left">
                                                End Date
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdBC" align="left" runat="server">
                                                <asp:DropDownList runat="server" ID="ctlBranch" AutoPostBack="true" OnSelectedIndexChanged="ctlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlBranch" Display="None" runat="server"
                                                    InitialValue="-- Select --" ID="RequiredFieldValidator3" ValidationGroup="top"
                                                    ErrorMessage="Branch Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlUT" AutoPostBack="true" OnSelectedIndexChanged="ctlUT_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlUT" Display="None" runat="server"
                                                    InitialValue="-- Select --" ID="RequiredFieldValidator5" ValidationGroup="top"
                                                    ErrorMessage="Branch Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="ctlUser">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlUser" Display="None" runat="server"
                                                    InitialValue="-- Select --" ID="RequiredFieldValidator2" ValidationGroup="top"
                                                    ErrorMessage="Mode Required"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlSDate" Width="80px"></asp:TextBox>
                                                <asp:Image ImageUrl="~/Images/Icons/Date.png" CssClass="calImage" runat="server"
                                                    ID="calButton" />
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="ctlSDate">
                                                </asp:CalendarExtender>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="calButton"
                                                    Format="dd/MM/yyyy" TargetControlID="ctlSDate">
                                                </asp:CalendarExtender>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlSDate" Display="None" runat="server"
                                                    ID="RequiredFieldValidator1" ValidationGroup="top" ErrorMessage="Date Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="ctlEDate" Width="80px"></asp:TextBox>
                                                <asp:Image ImageUrl="~/Images/Icons/Date.png" CssClass="calImage" runat="server"
                                                    ID="Image1" />
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="ctlEDate">
                                                </asp:CalendarExtender>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Image1"
                                                    Format="dd/MM/yyyy" TargetControlID="ctlEDate">
                                                </asp:CalendarExtender>
                                                <asp:RequiredFieldValidator ControlToValidate="ctlEDate" Display="None" runat="server"
                                                    ID="RequiredFieldValidator4" ValidationGroup="top" ErrorMessage="Date Required">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left">
                                                <%--   <img src="Images/Icons/Add_on.png" alt="Click here to Show balance Report" />--%>
                                                <asp:LinkButton runat="server" ValidationGroup="add" ID="LinkButton3" class="swap"
                                                    OnClick="showBalanceReport"> Show Balance Report</asp:LinkButton>
                                                <asp:ValidationSummary ID="sumtop" runat="server" ValidationGroup="top" ShowSummary="false"
                                                    ShowMessageBox="true" />
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
                    <td align="right">
                        <img src="Images/Icons/Printer.png" onclick="javascript:PrintMe('print1');" />
                    </td>
                </tr>
                <tr>
                    <td id="print1">
                        <table cellpadding="5" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <%-- OnRowDataBound="rowDataBound" OnRowCancelingEdit="rowCanceling"
                                        OnRowEditing="rowEditing" OnRowDeleting="rowDeleting" OnRowUpdating="rowUpdating"
                                        DataKeyNames="TempID" OnRowCreated="rowCreated"--%>
                                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" CellPadding="0"
                                        OnRowDataBound="rowDataBound" CellSpacing="0" BorderWidth="0px" GridLines="None"
                                        Width="100%" CssClass="gv gvSmall" ShowFooter="true">
                                        <HeaderStyle CssClass="h" />
                                        <RowStyle CssClass="r" />
                                        <AlternatingRowStyle CssClass="ar" />
                                        <PagerStyle CssClass="pager" />
                                        <FooterStyle CssClass="fr" />
                                        <SortedAscendingHeaderStyle CssClass="sortAsc" />
                                        <SortedDescendingHeaderStyle CssClass="sortDesc" />
                                        <EmptyDataRowStyle CssClass="empty" />
                                        <EmptyDataTemplate>
                                            Sorry! No item(s) found in the report.
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:dd/MM/yy}" />
                                            <asp:TemplateField HeaderText="Branch" SortExpression="Branch">
                                                <ItemTemplate>
                                                    <%# Eval("Branch.Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title">
                                                <ItemTemplate>
                                                    <%# Eval("Title")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit" ControlStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Credit")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debit" ControlStyle-CssClass="rightAlign">
                                                <ItemTemplate>
                                                    <%# Eval("Debit")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
