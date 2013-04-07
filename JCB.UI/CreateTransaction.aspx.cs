﻿using System;
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
    public partial class CreateTransaction : BasePage
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

                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                PopulateMetadata(ctlPG, Enumerations.MetadataType.Product_Group, this.MetaDatas);
                PopulateMetadata(ctlUnit, Enumerations.MetadataType.Unit, this.MetaDatas);
                PopulateMetadata(ctlMode, Enumerations.MetadataType.Payment_Mode, this.MetaDatas);
                PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, this.MetaDatas);
                RemoveItemByText(ctlBranch, "All");
                PopulateMetadata(ctlProduct, pc.GetProducts(ApplicationContext.BranchId));
                if (Request.QueryString["Type"] != null)
                {
                    rfReference.Enabled = false;
                    ref2.Visible = false;
                    ref1.Visible = false;

                    //rfUser.Enabled = false;
                    //usr2.Visible = false;
                    //usr1.Visible = false;
                    string uniqueid = Request.QueryString["Type"];
                    Enumerations.TransactionType type = uniqueid.GetEnumFromDescription<Enumerations.TransactionType>();

                    if (ApplicationContext.UserType == UserType.Branch_User)
                    {
                        PreSelect(ctlBranch, ApplicationContext.CurrentUser.Branch.UniqueId.ToString());
                        cellBLbl.Visible = tdBC.Visible = false;
                    }
                    this.CurrentTransactionType = type;
                    if (type == Enumerations.TransactionType.Purchase ||
                         type == Enumerations.TransactionType.Purchase_Return)
                    {

                        if (type == Enumerations.TransactionType.Purchase)
                        {
                            rfReference.Enabled = true;
                            ref2.Visible = true;
                            ref1.Visible = true;
                        }
                        PopulateMetadata(ctlSupplier, u.GetUsers(Enumerations.UserType.Supplier, ApplicationContext.BranchId), string.Empty, "Id");
                        lblTransactionType.Text = "Invoice No";
                        lblUserType.Text = "Supplier";
                    }
                    else
                    {

                        //if (type == TransactionType.Sales)
                        //{
                           
                        //        rfUser.Enabled = true;
                        //        usr2.Visible = true;
                        //        usr1.Visible = true;
                            
                        //}
                        PopulateMetadata(ctlSupplier, u.GetUsers(Enumerations.UserType.Customer, ApplicationContext.BranchId), string.Empty, "Id");
                        lblTransactionType.Text = "Bill No";
                        ctlInvoice.Text = itemController.GetBillNumber(type, ApplicationContext.BranchId).ToString();
                        lblUserType.Text = "Customer";
                        ctlInvoice.Enabled = false;
                        //ctlBranch.SelectedIndexChanged += new EventHandler(ctlBranch_SelectedIndexChanged);
                        //ctlBranch.AutoPostBack = true;
                    }

                    PreSelect(ctlBranch, MetaDatas.Find(delegate(MetaData d) { return d.Id == ApplicationContext.BranchId; }).UniqueId.ToString());
                    lblType.Text = type.ToString().Replace('_', ' ');
                }

                BindCart();

            }
        }

        protected void ctlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ctlBranch.SelectedIndex > 0)
            {
                string uniqueid = Request.QueryString["Type"];
                Enumerations.TransactionType type = uniqueid.GetEnumFromDescription<Enumerations.TransactionType>();
                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                MetaData Branch = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); });
                ctlInvoice.Text = itemController.GetBillNumber(type, Branch.Id).ToString();
                PopulateMetadata(ctlProduct, pc.GetProducts(Branch.Id));
                this.CurrentTransactionType = type;
                if (type == Enumerations.TransactionType.Purchase ||
                     type == Enumerations.TransactionType.Purchase_Return)
                {
                    PopulateMetadata(ctlSupplier, u.GetUsers(Enumerations.UserType.Supplier, ApplicationContext.BranchId), string.Empty, "Id");
                    lblTransactionType.Text = "Invoice No";
                    lblUserType.Text = "Supplier";
                }
                else
                {
                    PopulateMetadata(ctlSupplier, u.GetUsers(Enumerations.UserType.Customer, ApplicationContext.BranchId), string.Empty, "Id");
                    lblTransactionType.Text = "Bill No";
                    ctlInvoice.Text = itemController.GetBillNumber(type, Branch.Id).ToString();
                    lblUserType.Text = "Customer";

                    ctlBranch.SelectedIndexChanged += new EventHandler(ctlBranch_SelectedIndexChanged);
                    ctlBranch.AutoPostBack = true;
                }
                ctlProduct.SelectedIndex = 0;
                ctlSupplier.SelectedIndex = 0;
            }
        }

        public void BindCart()
        {

            try
            {
                gv.DataSource = this.Cart;
                gv.DataBind();
                if (this.Cart.Count == 0)
                    btnOrder.Visible = false;
                else
                    btnOrder.Visible = true;
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

                    if (this.CurrentTransactionType == Enumerations.TransactionType.Purchase ||
                         this.CurrentTransactionType == Enumerations.TransactionType.Purchase_Return)
                    {
                        ctlRate.Text = item.PurchasePrice.ToString();

                    }
                    else
                    {
                        ctlRate.Text = item.RetailerPrice.ToString();
                    }

                    ctlVatTax.Text = item.VatTax.ToString();
                    ctlQty.Text = "1";
                    //    ctlPN.Text = item.Name;
                    ctlDis.Text = "0";
                    ctlDisType.SelectedIndex = 0;

                    ctlHdnProductId.Value = item.Id.ToString();
                    PreSelect(ctlPG, item.ProductGroup.UniqueId.ToString());
                    PreSelect(ctlUnit, item.Unit.UniqueId.ToString());

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
            //List<Transaction> items = new List<Transaction>();
            //if (Session["CartItems"] != null)
            //{
            //    items = (List<Transaction>)Session["CartItems"];
            //}

            //Transaction inserted = newTrans;
            //items.Add(inserted);
            return newTrans;
            //return DateTime.Now.ToString();
        }



        //[WebMethod]
        //public static string CartItems(int customerId, int branchId)
        //{

        //    return "Hi From Page Method";
        //    //List<Transaction> items = new List<Transaction>();
        //    //if (Session["CartItems"] != null)
        //    //{
        //    //    items = (List<Transaction>)Session["CartItems"];
        //    //}

        //    //Transaction inserted = new Transaction()
        //    //{
        //    //    Active = true,
        //    //    CreatedBy = 0,
        //    //    DiscountPer = disPer,
        //    //    Branch = MetaDatas.Find(delegate(MetaData d) { return d.Id == branchId; }),
        //    //    VatTax = tax,
        //    //    Quantity = qty,
        //    //    DiscountRS = disRS,

        //    //    InvoiceNo = ctlInvoice.Text,
        //    //    ProductId = pid,
        //    //    ReorderLevel = "",

        //    //    Mode = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(mode); }).Id,
        //    //    ProductGroup = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(pg); }),
        //    //    Product = pc.GetProduct(new Guid(proGuid)),
        //    //    Rate = price,

        //    //    UserId = 1,
        //    //    TransactionUniqueId = TransactionId,
        //    //    // CustomerId = 0,
        //    //    //SupplierId = 0,
        //    //    TransactionOn = DateTime.Now,
        //    //    Unit = (float)MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(unit); }).Id,
        //    //    CreatedOn = tranDate

        //    //};
        //    //items.Add(inserted);
        //    //return items;
        //    //return DateTime.Now.ToString();
        //}

        //[WebMethod]
        //public List<Transaction> CartItems11(int customerId, int branchId, string mode, string unit, string proGuid, string pg, int pid, int qty, float price, float disPer, float disRS, float tax, DateTime tranDate)
        //{
        //    List<Transaction> items = new List<Transaction>();
        //    if (Session["CartItems"] != null)
        //    {
        //        items = (List<Transaction>)Session["CartItems"];
        //    }

        //    Transaction inserted = new Transaction()
        //    {
        //        Active = true,
        //        CreatedBy = 0,
        //        DiscountPer = disPer,
        //        Branch = MetaDatas.Find(delegate(MetaData d) { return d.Id == branchId; }),
        //        VatTax = tax,
        //        Quantity = qty,
        //        DiscountRS = disRS,

        //        InvoiceNo = ctlInvoice.Text,
        //        ProductId = pid,
        //        ReorderLevel = "",

        //        Mode = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(mode); }).Id,
        //        ProductGroup = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(pg); }),
        //        Product = pc.GetProduct(new Guid(proGuid)),
        //        Rate = price,

        //        UserId = 1,
        //        TransactionUniqueId = TransactionId,
        //        // CustomerId = 0,
        //        //SupplierId = 0,
        //        TransactionOn = DateTime.Now,
        //        Unit = (float)MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(unit); }).Id,
        //        CreatedOn = tranDate

        //    };
        //    items.Add(inserted);
        //    return items;
        //    //return DateTime.Now.ToString();
        //}


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
                        VatTax = (float)Convert.ToDouble(ctlVatTax.Text),
                        Quantity = (float)Convert.ToDouble(ctlQty.Text),
                        DiscountType = ctlDisType.SelectedValue.GetEnumFromDescription<DiscountType>(),

                        InvoiceOrBillNo = ctlInvoice.Text,
                        ProductId = Convert.ToInt32(ctlHdnProductId.Value),
                        ReorderLevel = "",
                        Reference = ctlReference.Text,
                        Mode = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlMode.SelectedValue); }).Id,
                        ProductGroup = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlPG.SelectedValue); }),
                        Product = pc.GetProduct(new Guid(ctlProduct.SelectedValue)),
                        Rate = (float)Convert.ToDouble(ctlRate.Text),

                        UserId = Convert.ToInt32(ctlSupplier.SelectedValue),
                        TransactionUniqueId = TransactionId,
                        // CustomerId = 0,
                        //SupplierId = 0,
                        Type = this.CurrentTransactionType,
                        TransactionDate = ConvertToMMddyyyy(ctlDate.Text),
                        Unit = (float)MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlUnit.SelectedValue); }).Id,
                        CreatedOn = DateTime.Now

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
            foreach (Transaction cartItem in Cart)
            {
                TransactionController t = new TransactionController();
                int n = t.Insert(cartItem);
            }

            string typeName = this.CurrentTransactionType.ToString().Replace("_", " ");

            //PrintRedirect(gv, "/ManageTransaction.aspx?Type=" + this.CurrentTransactionType.GetDescription());

            Response.Redirect("~/ManageTransaction.aspx?Type=" + this.CurrentTransactionType.GetDescription());
            //ShowAlert(string.Format("New {0} order placed successfully", typeName), "/ManageTransactions.aspx?Type=" + this.CurrentTransactionType.GetDescription());
            //switch (type)
            //{
            //    case Enumerations.TransactionType.Purchase:
            //        message=s"New {0} order placed successfully")  
            //        break;

            //    case Enumerations.TransactionType.Purchase_Return:
            //        break;

            //    case Enumerations.TransactionType.Sales:
            //        break;
            //    case Enumerations.TransactionType.Sales_Return:
            //        break;

            //}
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

                //e.Row.Cells[GetColumnIndex("Name")].Controls.Add(new LiteralControl("Total "));
                //e.Row.Cells[GetColumnIndex("Discount")].Controls.Add(new LiteralControl(totalDiscount.ToString()));
                //e.Row.Cells[GetColumnIndex("Total")].Controls.Add(new LiteralControl(total.ToString()));
                //e.Row.Cells[GetColumnIndex("Quantity")].Controls.Add(new LiteralControl(totalQty.ToString()));

                total = (float)Math.Round(total, 0, MidpointRounding.AwayFromZero);
                e.Row.Cells[1].Controls.Add(new LiteralControl("Total "));
                e.Row.Cells[3].Controls.Add(new LiteralControl(totalQty.ToString()));
                e.Row.Cells[7].Controls.Add(new LiteralControl(totalPrice.ToString()));
                e.Row.Cells[8].Controls.Add(new LiteralControl(totalDiscount.ToString()));
                e.Row.Cells[9].Controls.Add(new LiteralControl(totalTax.ToString()));
                e.Row.Cells[10].Controls.Add(new LiteralControl(total.ToString()));



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
            Cart[index].VatTax = (float)Convert.ToDouble(ctlVAT.Text);
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


                TableCell newCell = new TableCell() { Text = "Product", ColumnSpan = 2, RowSpan = 1, CssClass = hc };
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
    }
}