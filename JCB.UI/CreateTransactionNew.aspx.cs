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
    public partial class CreateTransactionNew : BasePage
    {
        MetadataController controller = new MetadataController();
        TransactionController itemController = new TransactionController();
        List<MetaData> MetaDatas { get; set; }
        UserController u = new UserController();
        ProductController pc = new ProductController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                ctlDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ctlUName.Visible = false;
                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);

                PopulateMetadata(ctlUnit, Enumerations.MetadataType.Unit, this.MetaDatas);
                PopulateMetadata(ctlMode, Enumerations.MetadataType.Payment_Mode, this.MetaDatas);
                PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, this.MetaDatas);
                PopulateMetadata(ctlVatTax, Enumerations.MetadataType.VAT, this.MetaDatas);

                RemoveItemByText(ctlBranch, "All");
                MetaData Branch = MetaDatas.Find(delegate(MetaData d) { return d.Id == ApplicationContext.BranchId; });
                PreSelect(ctlBranch, Branch.UniqueId.ToString());
                PopulateMetadata(ctlProduct, pc.GetProducts(ApplicationContext.BranchId));
                resetRate.Visible = false;

                ctlInvoice.Enabled = false;
                if (Request.QueryString["Type"] != null)
                {
                    ctlMode.AutoPostBack = false;
                    rfReference.Enabled = false;
                    tdPRN_0.Visible = false;
                    tdPRN_1.Visible = false;

                    string uniqueid = Request.QueryString["Type"];
                    Enumerations.TransactionType type = uniqueid.GetEnumFromDescription<Enumerations.TransactionType>();

                    if (ApplicationContext.UserType == UserType.Branch_User)
                    {
                        PreSelect(ctlBranch, ApplicationContext.CurrentUser.Branch.UniqueId.ToString());
                        cellBLbl.Visible = tdBC.Visible = false;
                    }
                    this.CurrentTransactionType = type;

                    if (type == Enumerations.TransactionType.Purchase_Request)
                    {
                        MakeAsPurchaseRequestForm(Branch);
                    }
                    else if (type == Enumerations.TransactionType.Purchase ||
                             type == Enumerations.TransactionType.Purchase_Return)
                    {

                        MakeAsPurchaseForm(Branch);
                    }
                    else
                    {
                        MakeAsSalesForm(Branch);
                    }


                    lblType.Text = type.ToString().Replace('_', ' ');
                }

                BindCart();

            }
        }


        private void MakeAsPurchaseRequestForm(MetaData Branch)
        {


            ctlInvoice.Text = itemController.GetBillNumber(this.CurrentTransactionType, Branch.Id).ToString();
            this.Users = u.GetUsers(Enumerations.UserType.Supplier, Branch.Id);
            PopulateMetadata(ctlSupplier, Users, string.Empty, "Id");
            lblTransactionType.Text = "Invoice No";
            lblUserType.Text = "Supplier";

            if (this.CurrentTransactionType == TransactionType.Purchase_Request)
            {
                tdInvoice_0.Visible = false;
                tdInvoice_1.Visible = false;
                tdPRN_0.Visible = true;
                tdPRN_1.Visible = true;
                if (ctlBranch.SelectedIndex != 0)
                {
                    ctlReference.Text = Branch.Description +
                        "/" + DateTime.Now.ToString("dd/MM/yy") + "/" + itemController.GetBillNumber(this.CurrentTransactionType, Branch.Id).ToString();
                    ctlInvoice.Text = itemController.GetBillNumber(this.CurrentTransactionType, Branch.Id).ToString();

                    ctlReference.Enabled = false;
                }
            }
        }

        private void MakeAsPurchaseForm(MetaData Branch)
        {
            ctlInvoice.Text = itemController.GetBillNumber(this.CurrentTransactionType, Branch.Id).ToString();
            this.Users = u.GetUsers(Enumerations.UserType.Supplier, Branch.Id);
            PopulateMetadata(ctlSupplier, Users, string.Empty, "Id");
            lblTransactionType.Text = "Invoice No";
            lblUserType.Text = "Supplier";

            if (this.CurrentTransactionType == TransactionType.Purchase)
            {
                tdPRN_0.Visible = true;
                tdPRN_1.Visible = true;
            }
            ctlInvoice.Enabled = true;
        }

        private void MakeAsSalesForm(MetaData Branch)
        {

            if (CurrentTransactionType == TransactionType.Sales)
                ctlMode.AutoPostBack = true;
            ctlInvoice.Text = itemController.GetBillNumber(CurrentTransactionType, Branch.Id).ToString();

            this.Users = u.GetUsers(Enumerations.UserType.Customer, Branch.Id);
            PopulateMetadata(ctlSupplier, this.Users, string.Empty, "Id");
            lblTransactionType.Text = "Bill No";
            lblUserType.Text = "Customer";
            ctlBranch.SelectedIndexChanged += new EventHandler(ctlBranch_SelectedIndexChanged);
            ctlBranch.AutoPostBack = true;


            trSales0.Visible = true;
            trSales1.Visible = true;
        }

        protected void ctlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ctlBranch.SelectedIndex > 0)
            {
                string uniqueid = Request.QueryString["Type"];
                Enumerations.TransactionType type = uniqueid.GetEnumFromDescription<Enumerations.TransactionType>();
                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                MetaData Branch = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); });

                PopulateMetadata(ctlProduct, pc.GetProducts(Branch.Id));
                this.CurrentTransactionType = type;

                if (type == Enumerations.TransactionType.Purchase_Request)
                {
                    MakeAsPurchaseRequestForm(Branch);
                }
                else if (type == Enumerations.TransactionType.Purchase ||
                     type == Enumerations.TransactionType.Purchase_Return)
                {
                    MakeAsPurchaseForm(Branch);
                }
                else
                {
                    MakeAsSalesForm(Branch);
                }
                ctlProduct.SelectedIndex = 0;
                ctlSupplier.SelectedIndex = 0;
                BindCart();
            }
        }

        public void BindCart()
        {

            try
            {
                gv.DataSource = this.Cart;
                gv.DataBind();
                if (this.Cart.Count == 0)
                {
                    Button1.Visible = false;
                    btnOrder.Visible = false;
                }
                else
                {
                    Button1.Visible = true;
                    btnOrder.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
        }

        //protected void btnCreate_Click(object sender, EventArgs e)
        //{

        //    string uniqueid = Request.QueryString["Type"];
        //    Enumerations.UserType type = uniqueid.GetEnumFromDescription<Enumerations.UserType>();
        //    Entities.Transaction newUser = new Entities.Transaction()
        //    {


        //    };
        //    int inserted = itemController.Insert(newUser);

        //    if (inserted == 0)
        //        ShowAlert("Transaction creation failed");
        //    else
        //        ShowAlert("Transaction created successfully", "/ManageTransactions.aspx?Type=" + uniqueid);
        //}

        protected void productSelectionChanged(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ctlProduct.SelectedIndex != 0)
                {
                    Product item = pc.GetProduct(new Guid(ctlProduct.SelectedValue));


                    if (this.CurrentTransactionType == TransactionType.Sales)
                    {
                        float orderedCount = this.Cart.Where(d => d.ProductId == item.Id).Sum(i => i.Quantity);
                        StockReport r = pc.GetStockReportByProduct(item.Branch.Id, item.Id);
                        hdnStock.Value = (r.OutstandingQuantity - orderedCount).ToString();
                    }
                    if (this.CurrentTransactionType == Enumerations.TransactionType.Purchase ||
                         this.CurrentTransactionType == Enumerations.TransactionType.Purchase_Return ||
                         this.CurrentTransactionType == Enumerations.TransactionType.Purchase_Request
                        )
                    {
                        ctlRate.Text = item.PurchasePrice.ToString();
                        ctlWholesalePrice.Value = item.PurchasePrice.ToString();
                    }
                    else
                    {
                        resetRate.Visible = true;
                        ctlRate.Text = item.RetailerPrice.ToString();
                        ctlWholesalePrice.Value = item.WholesellerPrice.ToString();
                    }

                    // ctlVatTax.Text = item.VatTax.ToString();

                    PreSelectByText(ctlVatTax, item.VatTax.ToString());
                    ctlQty.Text = "1";
                    //    ctlPN.Text = item.Name;
                    ctlDis.Text = "0";
                    ctlDisType.SelectedIndex = 0;

                    ctlHdnProductId.Value = item.Id.ToString();
                    ctlHdnPgId.Value = item.ProductGroup.UniqueId.ToString();
                    ctlHdnUnitId.Value = item.Unit.UniqueId.ToString();
                    PreSelect(ctlUnit, item.Unit.UniqueId.ToString());

                    BindCart();

                }
                else
                {
                    ctlProduct.SelectedIndex = 0;
                }
            }
            else
            {
                ctlProduct.SelectedIndex = 0;
            }
        }


        public Guid TransactionId
        {
            get
            {

                if (ViewState["TransactionId"] == null)
                    ViewState["TransactionId"] = Guid.NewGuid();
                return (Guid)ViewState["TransactionId"];

            }

        }



        //string uniqueid = Request.QueryString["Type"];
        //Enumerations.TransactionType type = uniqueid.GetEnumFromDescription<Enumerations.TransactionType>();

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
        public List<User> Users
        {
            get
            {
                var list = (ViewState["User"] != null) ? (List<User>)(ViewState["User"]) : new List<User>();
                return list;
            }
            set
            {
                ViewState["User"] = value;
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

        //[WebMethod(EnableSession = true)]
        //public List<Transaction> CartItems(Transaction newTrans)
        //{
        //    List<Transaction> items = new List<Transaction>();
        //    if (Session["CartItems"] != null)
        //    {
        //        items = (List<Transaction>)Session["CartItems"];
        //    }

        //    Transaction inserted = newTrans;
        //    items.Add(inserted);
        //    return items;
        //    //return DateTime.Now.ToString();
        //}


        [WebMethod(EnableSession = true)]
        public static Transaction CartItems(Transaction newTrans)
        {

            return newTrans;

        }


        protected void ItemAdded(object sender, EventArgs e)
        {


            if (Page.IsValid)
            {
                List<MetaData> MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                List<Transaction> old = this.Cart;

                try
                {
                    Transaction added = new Transaction()
                    {
                        TempID = Guid.NewGuid(),
                        Active = true,
                        CreatedBy = ApplicationContext.CurrentUser.Id,
                        Discount = (float)Convert.ToDouble(ctlDis.Text),
                        Branch = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); }),
                        VatTax = (float)Convert.ToDouble(ctlVatTax.SelectedItem.Text),
                        Quantity = (float)Convert.ToDouble(ctlQty.Text),
                        DiscountType = ctlDisType.SelectedValue.GetEnumFromDescription<DiscountType>(),
                        ProductId = Convert.ToInt32(ctlHdnProductId.Value),
                        ReorderLevel = "",
                        ProductGroup = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlHdnPgId.Value); }),
                        Product = pc.GetProduct(new Guid(ctlProduct.SelectedValue)),
                        Rate = (float)Convert.ToDouble(ctlRate.Text),
                        TransactionUniqueId = TransactionId,
                        Type = this.CurrentTransactionType,
                        Unit = (float)MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlHdnUnitId.Value); }).Id,
                        CreatedOn = DateTime.Now,

                        //UserId = Convert.ToInt32(ctlSupplier.SelectedValue),
                        //InvoiceOrBillNo = ctlInvoice.Text,
                        //TransactionDate = ConvertToMMddyyyy(ctlDate.Text),
                        //Reference = ctlReference.Text

                    };
                    DiscountType dt = DiscountType.Percentage;

                    dt.GetDescription();
                    old.Add(added);
                    this.Cart = old;
                    BindCart();

                }
                catch (Exception ex)
                {
                    Log.Append(ex);
                }
            }
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            TransactionController t = new TransactionController();
            List<MetaData> MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
            foreach (Transaction cartItem in Cart)
            {

                cartItem.UserId = Convert.ToInt32(ctlSupplier.SelectedValue);
                cartItem.InvoiceOrBillNo = ctlInvoice.Text;
                cartItem.TransactionDate = ConvertToMMddyyyy(ctlDate.Text);
                cartItem.Reference = ctlReference.Text;
                cartItem.BillName = ctlUName.Text;
                cartItem.Mode = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlMode.SelectedValue); }).Id;

                cartItem.LRNo = ctlLr.Text;
                cartItem.Through = ctlThrough.Text;
                cartItem.RefNo = ctlRef.Text;
                cartItem.RefDate = ctlRef.Text;
                int n = t.Insert(cartItem);
            }

            string typeName = this.CurrentTransactionType.ToString().Replace("_", " ");

            //PrintRedirect(gv, "/ManageTransaction.aspx?Type=" + this.CurrentTransactionType.GetDescription());

            //PrintRedirect(gv, "/ManageTransaction.aspx?Type=" + this.CurrentTransactionType.GetDescription());
            if (CurrentTransactionType == TransactionType.Sales)
            {


                if (printRequired.Checked)
                {
                    string r = SubmitPrint("Sales Submitted Successfully",
                        string.Format("PrintTransaction.aspx?Id={0}&Type={1}", Cart[0].TransactionUniqueId.ToString(), this.CurrentTransactionType.GetDescription()),
                 "/ManageTransaction.aspx?Type=" + this.CurrentTransactionType.GetDescription());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SubmitPrint", r, true);
                }
                else
                {
                    Response.Redirect("~/ManageTransaction.aspx?Type=" + this.CurrentTransactionType.GetDescription());
                }
            }
            else
            {
                Response.Redirect("~/ManageTransaction.aspx?Type=" + this.CurrentTransactionType.GetDescription());
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

                total = (float)Math.Round(total, 0, MidpointRounding.AwayFromZero);
                e.Row.Cells[1].Controls.Add(new LiteralControl("Total "));
                e.Row.Cells[2].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalQty).ToString()));
                e.Row.Cells[6].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalPrice).ToString()));
                e.Row.Cells[7].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalDiscount).ToString()));
                e.Row.Cells[8].Controls.Add(new LiteralControl(String.Format("{0:0.00}", totalTax).ToString()));
                e.Row.Cells[9].Controls.Add(new LiteralControl(String.Format("{0:0.00}", total).ToString()));



            }
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
            Cart[index].Quantity = (float)Convert.ToDouble(ctlQuantity.Text);
            Cart[index].Rate = (float)Convert.ToDouble(ctlRate.Text);
            Cart[index].Discount = (float)Convert.ToDouble(ctlDiscount.Text);
            Cart[index].DiscountType = ctlDisType.SelectedValue.GetEnumFromDescription<DiscountType>();
            // Cart[index].VatTax = (float)Convert.ToDouble(ctlVAT.Text);
            gv.EditIndex = -1;
            BindCart();
        }

        #region Header Format

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

        protected void rowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //SetRowspan(e.Row);


                string hc = "test";
                //Build custom header.
                GridView gv = (GridView)sender;
                GridViewRow newRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                newRow.CssClass = "h";


                TableCell newCell = new TableCell() { Text = "Product", ColumnSpan = 1, RowSpan = 1, CssClass = hc };
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

                SetRowspan(e.Row, "Total Amount");
                newCell = new TableCell() { Text = "Total Amount", ColumnSpan = 1, RowSpan = 2, CssClass = hc };
                newRow.Cells.Add(newCell);

                SetRowspan(e.Row, "");
                newCell = new TableCell() { Text = "", ColumnSpan = 1, RowSpan = 2, CssClass = hc };
                newRow.Cells.Add(newCell);

                gv.Controls[0].Controls.AddAt(0, newRow);
            }
        }

        #endregion

        protected void modeChanged(object sender, EventArgs e)
        {
            string uniqueid = Request.QueryString["Type"];
            Enumerations.TransactionType type = uniqueid.GetEnumFromDescription<Enumerations.TransactionType>();


            if (type == TransactionType.Sales)
            {
                string userId = string.Empty;
                if (ctlSupplier.SelectedIndex != 0)
                    userId = ctlSupplier.SelectedValue;
                PopulateMetadata(ctlSupplier, Users, string.Empty, "Id");
                if (ctlMode.SelectedItem.Text == "Cash")
                {
                    PreSelectByText(ctlSupplier, CommonHelper.DummyUserForCashSale);
                    ctlUName.Visible = true;
                }
                else
                {
                    ctlUName.Visible = false;
                    if (ctlSupplier.Items.FindByText(CommonHelper.DummyUserForCashSale) != null)
                        ctlSupplier.Items.FindByText(CommonHelper.DummyUserForCashSale).Enabled = false;
                    if (userId != string.Empty)
                        PreSelect(ctlSupplier, userId);
                }

            }

            BindCart();
        }

        protected void userChanged(object sender, EventArgs e)
        {
            if (ctlSupplier.SelectedItem.Text == CommonHelper.DummyUserForCashSale)
                ctlUName.Text = string.Empty;
            else
                ctlUName.Text  = ctlSupplier.SelectedItem.Text;

             BindCart();
        }
    }
}