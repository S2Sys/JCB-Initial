using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JCB.Entities;
using JCB.Enumerations;
using JCB.BAL;

namespace JCB.UI
{
    public partial class ReportReorderLevel : BasePage
    {
        ReportController controller = new ReportController();
        MetadataController md = new MetadataController();

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Parent"] != null)
                        this.ParentId = new Guid(Request.QueryString["Parent"]);
                    this.Cart = controller.GetReorderLevelReport(ApplicationContext.BranchId, DateTime.Now.AddYears(-2), DateTime.Now, string.Empty);
                    PopulateMetadata(ctlGroup,  md.GetMetaDatasByBranch(ApplicationContext.BranchId));

                    BindCart();

                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
        }
        public Guid ParentId
        {
            get
            {
                var list = (ViewState["ParentId"] != null) ? (Guid)(ViewState["ParentId"]) : Guid.Empty;
                return list;
            }
            set
            {
                ViewState["ParentId"] = value;
            }
        }

        public void BindCart()
        {

            try
            {
                gv.DataSource = this.Cart;
                gv.DataBind();

            }
            catch (Exception ex)
            {
                
            }
        }


        //public void SetColumnHeader()
        //{
        //    if (gv.HeaderRow != null)
        //        if (CurrentTransactionType == Enumerations.TransactionType.Purchase ||
        //                     CurrentTransactionType == Enumerations.TransactionType.Purchase_Request ||
        //                     CurrentTransactionType == Enumerations.TransactionType.Purchase_Return)
        //        {

        //            gv.HeaderRow.Cells[4].Text = "Invoice";
        //        }
        //        else
        //        {
        //            gv.HeaderRow.Cells[4].Text = "Bill No";
        //            gv.Columns[4].HeaderText = "Bill No";
        //        }
        //}



        public List<ReorderLevelReport> Cart
        {
            get
            {
                var list = (ViewState["Cart"] != null) ? (List<ReorderLevelReport>)(ViewState["Cart"]) : new List<ReorderLevelReport>();

                if (ParentId != Guid.Empty)
                {
                    list = list.FindAll(delegate(ReorderLevelReport i) { return i.Product.ProductGroup.UniqueId == ParentId; });
                }

                return list;
            }
            set
            {
                ViewState["Cart"] = value;
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
                //float totalPrice = this.Cart.Sum(x => x.TotalPrice);
                //float totalTax = this.Cart.Sum(x => x.TotalVAT);
                //float totalDiscount = this.Cart.Sum(x => x.TotalDiscount);
                //float totalQty = this.Cart.Sum(x => x.Quantity);
                //float total = this.Cart.Sum(x => x.FinalTotal);
 

                //e.Row.Cells[1].Controls.Add(new LiteralControl("Total "));
                //e.Row.Cells[5].Controls.Add(new LiteralControl(totalQty.ToString()));
                //e.Row.Cells[9].Controls.Add(new LiteralControl(totalPrice.ToString()));
                //e.Row.Cells[10].Controls.Add(new LiteralControl(totalDiscount.ToString()));
                //e.Row.Cells[11].Controls.Add(new LiteralControl(totalTax.ToString()));
                //e.Row.Cells[12].Controls.Add(new LiteralControl(total.ToString()));



            }
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


        protected void reportSearch(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ctlGroup.SelectedIndex == 0)
                {
                    this.ParentId = Guid.Empty;
                }
                else
                {
                    this.ParentId = new Guid(ctlGroup.SelectedValue);

                }
                BindCart();
            }
        }
    }
}