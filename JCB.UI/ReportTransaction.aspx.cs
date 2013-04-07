using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JCB.Entities;
using JCB.BAL;
using System.Web.Services;
using JCB.Enumerations;

namespace JCB.UI
{
    [System.Web.Script.Services.ScriptService]
    public partial class ReportTransaction : BasePage
    {
        MetadataController controller = new MetadataController();
        TransactionController itemController = new TransactionController();
        List<MetaData> MetaDatas { get; set; }
        UserController u = new UserController();
        ProductController pc = new ProductController();
        public bool ShowPurchaseRequestNo = false;
        public bool ShowInvoiceNo = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ShowInvoiceNo = false;
                ShowPurchaseRequestNo = false;


                string id = Request.QueryString["Type"];

                this.CurrentTransactionType = id.GetEnumFromDescription<TransactionType>();
                if (CurrentTransactionType == Enumerations.TransactionType.Purchase_Request)
                {
                    ShowPurchaseRequestNo = true;
                }
                else
                {
                    ShowInvoiceNo = true;
                }
                this.PageIndex = 1;
                this.PageSize = 10;
                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
              //  PopulateMetadata(ctlMode, Enumerations.MetadataType.Payment_Mode, this.MetaDatas, "Id", "Name", "All");


                PopulateMetadata(ctlMode, MetaDatas, Enumerations.MetadataType.Payment_Mode, "Id", "Name", "All");

                lblType.Text = this.CurrentTransactionType.ToString().Replace('_', ' ');

                this.OrderId = Guid.Empty;
                this.OrderQuantity = 0;


                if (CurrentTransactionType == Enumerations.TransactionType.Purchase_Request)
                {
                    MakeAsPurchaseRequestForm();
                }
                else if (CurrentTransactionType == Enumerations.TransactionType.Purchase ||
                         CurrentTransactionType == Enumerations.TransactionType.Purchase_Return)
                {

                    MakeAsPurchaseForm();
                }
                else
                {
                    MakeAsSalesForm();
                }


                BindCart();

            }
        }


        private void MakeAsPurchaseRequestForm()
        {
            PopulateMetadata(ctlUser, u.GetUsers(Enumerations.UserType.Supplier, ApplicationContext.BranchId), "Name", "Id","All");
            lblUserType.Text = "Supplier";
        }

        private void MakeAsPurchaseForm()
        {

            PopulateMetadata(ctlUser, u.GetUsers(Enumerations.UserType.Supplier, ApplicationContext.BranchId), string.Empty, "Id");
            lblUserType.Text = "Supplier";
        }

        private void MakeAsSalesForm()
        {
            PopulateMetadata(ctlUser, u.GetUsers(Enumerations.UserType.Customer, ApplicationContext.BranchId), string.Empty, "Id");
            lblUserType.Text = "Customer";

        }


        public void BindCart()
        {

            try
            {
                int rowCount = 0;

                DateTime sd = DateTime.MinValue;
                DateTime ed = DateTime.MinValue;
                int userId = 0, mode = 0;
                if (ctlStrteDate.Text != "")
                    sd = ConvertToMMddyyyy(ctlStrteDate.Text);
                if (ctlEndDate.Text != "")
                    ed = ConvertToMMddyyyy(ctlEndDate.Text);
                if (ctlUser.SelectedIndex != 0)
                    userId = Convert.ToInt32(ctlUser.SelectedValue);

                if (ctlMode.SelectedIndex != 0)
                    mode = Convert.ToInt32(ctlUser.SelectedValue);


                this.Cart = itemController.GetCustomPagingReport(CurrentTransactionType,
                 ApplicationContext.BranchId, sd, ed, userId, mode,
                 PageIndex, PageSize, ref rowCount);
                RowCount = rowCount;

                gv.DataSource = this.Cart;
                gv.DataBind();
                SetColumnHeader();

                PopulatePager();
            }
            catch (Exception ex)
            {

            }
        }


        public void SetColumnHeader()
        {
            if (gv.HeaderRow != null)

                if (CurrentTransactionType == Enumerations.TransactionType.Purchase_Request)
                {
                    gv.HeaderRow.Cells[1].Text = "Purchase Request No";
                    gv.HeaderRow.Cells[3].Text = "Supplier";
                }
                else if (CurrentTransactionType == Enumerations.TransactionType.Purchase ||

                             CurrentTransactionType == Enumerations.TransactionType.Purchase_Return)
                {
                    // gvItems.Columns[4].HeaderText = "Invoice";

                    gv.HeaderRow.Cells[1].Text = "Invoice";
                    gv.HeaderRow.Cells[3].Text = "Supplier";
                }
                else
                {
                    gv.HeaderRow.Cells[1].Text = "Bill No";
                    gv.Columns[1].HeaderText = "Bill No";
                    gv.HeaderRow.Cells[3].Text = "Customer";
                }
        }


        public TransactionType CurrentTransactionType
        {
            get
            {
                return (Enumerations.TransactionType)ViewState["CurrentTransactionType"];
            }
            set
            {
                ViewState["CurrentTransactionType"] = value;
            }
        }

        public List<Transaction> Cart
        {
            get
            {
                var list = (ViewState["Cart"] != null) ? (List<Transaction>)(ViewState["Cart"]) : new List<Transaction>();
                return list;
            }
            set
            {
                ViewState["Cart"] = value;
            }
        }

        //http://www.aspsnippets.com/Articles/Custom-Paging-in-ASP.Net-GridView-using-SQL-Server-Stored-Procedure.aspx
        private void PopulatePager()
        {

            double dblPageCount = (double)((decimal)RowCount / (decimal)PageSize);
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 1)
            {

                int startPageIndex = 1;
                int endPageIndex = (pageCount > 10) ? 10 : pageCount;
                if (PageIndex >= 10)
                {
                    startPageIndex = (PageIndex) - 8;
                    endPageIndex = endPageIndex = (endPageIndex + 1 > pageCount) ? PageIndex + 1 : pageCount; ;
                }

                if (pageCount > 0)
                {
                    pages.Add(new ListItem("First", "1", PageIndex > 1));
                    pages.Add(new ListItem("Prev", (PageIndex - 1).ToString(), PageIndex > 1));
                    for (int i = startPageIndex; i <= endPageIndex; i++)
                    //for (int i = 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != PageIndex));
                    }
                    pages.Add(new ListItem("Next", (PageIndex + 1).ToString(), PageIndex < pageCount));
                    pages.Add(new ListItem("Last", pageCount.ToString(), PageIndex < pageCount));
                }

            }

            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.PageIndex = pageIndex;
            BindCart();
        }

        public int PageSize
        {
            get
            {
                return (int)ViewState["PageSize"];
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }
        public int PageIndex
        {
            get
            {
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        public int RowCount
        {
            get
            {
                return (int)ViewState["RowCount"];
            }
            set
            {
                ViewState["RowCount"] = value;
            }
        }

        public int OrderQuantity
        {
            get
            {
                return (int)ViewState["OrderQuantity"];
            }
            set
            {
                ViewState["OrderQuantity"] = value;
            }
        }

        public Guid OrderId
        {
            get
            {
                return (Guid)ViewState["OrderId"];
            }
            set
            {
                ViewState["OrderId"] = value;
            }
        }

        private int GetColumnIndex(string SortExpression)
        {
            int i = 0;
            foreach (DataControlField c in gv.Columns)
            {
                if (c.HeaderText == SortExpression)
                    break;
                i++;
            }
            return i;
        }

        protected void rowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                float totalPrice = this.Cart.Sum(x => x.TotalPrice);
                float totalTax = this.Cart.Sum(x => x.TotalVAT);
                float totalDiscount = this.Cart.Sum(x => x.TotalDiscount);
                float totalQty = this.Cart.Sum(x => x.Quantity);
                float total = this.Cart.Sum(x => x.FinalTotal);

                var sums = this.Cart.GroupBy(x => new { x.TransactionUniqueId })
                            .Select(group => group.Sum(x => x.FinalTotal));
                float newTotal = 0;
                foreach (float sum in sums)
                {
                    newTotal += (float)Math.Round(sum, 0, MidpointRounding.AwayFromZero);
                }

                e.Row.Cells[2].Controls.Add(new LiteralControl("Total "));
                e.Row.Cells[6].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalQty).ToString()));
                e.Row.Cells[10].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalPrice).ToString()));
                e.Row.Cells[11].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalDiscount).ToString()));
                e.Row.Cells[12].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalTax).ToString()));
                e.Row.Cells[13].Controls.Add(new LiteralControl(String.Format("{0:0.00}", newTotal).ToString()));

            }

            //if (e.Row.RowType == DataControlRowType.Pager)
            //{
            //    GridView gvPager = e.Row.FindControl("Pager") as GridView;
            //    PopulatePager(gvPager);
            //}
        }

        protected void rowEditing(object sender, GridViewEditEventArgs e)
        {
            gv.EditIndex = e.NewEditIndex;

            BindCart();
        }

        protected void rowCanceling(object sender, GridViewCancelEditEventArgs e)
        {
            gv.EditIndex = -1;
            BindCart();
        }

        protected void rowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowEdited = e.RowIndex;
            string itemId = gv.DataKeys[rowEdited].Value.ToString();
            Transaction deleted = this.Cart.Find(delegate(Transaction t) { return (t.TempID == new Guid(itemId)); });
            bool status = this.Cart.Remove(deleted);
            BindCart();
        }

        protected void rowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int rowEdited = e.RowIndex;
            string itemId = gv.DataKeys[rowEdited].Value.ToString();
            GridViewRow row = gv.Rows[rowEdited];

            int index = Cart.FindIndex(x => x.TempID == new Guid(itemId));

            Transaction updated = this.Cart.Find(delegate(Transaction t) { return (t.TempID == new Guid(itemId)); });
            TextBox ctlQuantity = row.FindControl("ctlQuantity") as TextBox;
            TextBox ctlRate = row.FindControl("ctlRate") as TextBox;
            TextBox ctlDiscount = row.FindControl("ctlDiscount") as TextBox;
            TextBox ctlVAT = row.FindControl("ctlVatTax") as TextBox;
            DropDownList ctlDisType = row.FindControl("ctlDisType") as DropDownList;
            Cart[index].Quantity = (float)Convert.ToInt32(ctlQuantity.Text);
            Cart[index].Rate = (float)Convert.ToInt32(ctlRate.Text);
            Cart[index].Discount = (float)Convert.ToInt32(ctlDiscount.Text);
            Cart[index].DiscountType = ctlDisType.SelectedValue.GetEnumFromDescription<DiscountType>();
            Cart[index].VatTax = (float)Convert.ToInt32(ctlVAT.Text);
            gv.EditIndex = -1;
            BindCart();
        }

        private void SetRowspan(GridViewRow row)
        {
            foreach (TableCell dc in row.Cells)
            {
                switch (dc.Text)
                {
                    case "Quantity":
                    case "Rate":
                    case "Discount":
                    case "VAT":
                        dc.Visible = false;
                        break;
                }
            }
        }

        private void SetRowspan(GridViewRow row, string name)
        {

            int rIndex = -1;
            for (int index = 0; index < row.Cells.Count; index++)
            {
                rIndex = index;
                if (row.Cells[index].Text == name)
                    break;
            }

            row.Cells.RemoveAt(rIndex);
        }

        string cssclass = "r";
        protected void rowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                string hc = "test";
                //Build custom header.
                GridView gv = (GridView)sender;
                GridViewRow newRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                newRow.CssClass = "h";


                TableCell newCell = new TableCell() { Text = "Order Detail", ColumnSpan = 6, RowSpan = 1, CssClass = hc };
                newRow.Cells.Add(newCell);

                SetRowspan(e.Row, "Quantity");
                newCell = new TableCell() { Text = "Qty", ColumnSpan = 1, RowSpan = 2, CssClass = hc };
                newRow.Cells.Add(newCell);

                SetRowspan(e.Row, "Rate");
                newCell = new TableCell() { Text = "Rate", ColumnSpan = 1, RowSpan = 2, CssClass = hc };
                newRow.Cells.Add(newCell);

                SetRowspan(e.Row, "Discount");

                newCell = new TableCell() { Text = "Disc", ColumnSpan = 1, RowSpan = 2, CssClass = hc };
                newRow.Cells.Add(newCell);

                SetRowspan(e.Row, "VAT %");
                newCell = new TableCell() { Text = "VAT %", ColumnSpan = 1, RowSpan = 2, CssClass = hc };
                newRow.Cells.Add(newCell);

                newCell = new TableCell() { Text = "Total in RS", ColumnSpan = 3, RowSpan = 1, CssClass = hc };
                newRow.Cells.Add(newCell);

                SetRowspan(e.Row, "FinalTotal");
                newCell = new TableCell() { Text = "Total Amount", ColumnSpan = 1, RowSpan = 2, CssClass = hc };
                newRow.Cells.Add(newCell);

                gv.Controls[0].Controls.AddAt(0, newRow);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                try
                {
                    Transaction tran = e.Row.DataItem as Transaction;

                    if (tran != null)
                    {
                        List<Transaction> orderItems = this.Cart.FindAll(delegate(Transaction t)
                        {
                            return t.TransactionUniqueId == tran.TransactionUniqueId;
                        });

                        if (this.OrderId != tran.TransactionUniqueId)
                        {
                            this.OrderId = tran.TransactionUniqueId;
                            this.OrderQuantity = orderItems.Count;

                            e.Row.Cells[0].RowSpan = orderItems.Count;
                            e.Row.Cells[1].RowSpan = orderItems.Count;
                            e.Row.Cells[2].RowSpan = orderItems.Count;
                            e.Row.Cells[3].RowSpan = orderItems.Count;
                            e.Row.Cells[0].CssClass = "hl";
                            e.Row.Cells[1].CssClass = "hl";
                            e.Row.Cells[2].CssClass = "hl";
                            e.Row.Cells[3].CssClass = "hl";
                            // e.Row.CssClass = cssclass;

                            //if (cssclass == "r")
                            //    cssclass = "ar";
                            //else
                            //    cssclass = "r";

                        }
                        else
                        {
                            e.Row.Cells.RemoveAt(3);
                            e.Row.Cells.RemoveAt(2);
                            e.Row.Cells.RemoveAt(1);
                            e.Row.Cells.RemoveAt(0);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Append(ex);
                }


            }
        }

        protected void reportSearch(object sender, EventArgs e)
        {

            if (CurrentTransactionType == Enumerations.TransactionType.Purchase_Request)
            {
                ShowPurchaseRequestNo = true;
            }
            else
            {
                ShowInvoiceNo = true;
            }

            this.PageIndex = 1;
            this.OrderId = Guid.Empty;
            BindCart();
        }

    }
}