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
    public partial class ReportStock : BasePage
    {
        ReportController controller = new ReportController();
        ProductController product = new ProductController();
        MetadataController md = new MetadataController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["Type"];

                if (Request.QueryString["Parent"] != null)
                    this.ParentId = new Guid(Request.QueryString["Parent"]);
                this.Cart = controller.GetStockReport(ApplicationContext.BranchId, DateTime.Now.AddYears(-2), DateTime.Now, string.Empty);
                BindCart();

                PopulateMetadata(ctlGroup,  md.GetMetaDatasByBranch(ApplicationContext.BranchId));

                //     this.Cart.Distinct(p => p.);

                // data = context.TripInfo.Where(i => i.PlantVisited.Length > 0).Select(i => new MetaData { Text = i.PlantVisited, Value = i.PlantVisited }).Distinct().ToList();
                // .Where(i => i.PlantVisited.Length > 0)
                //List<MetaData> mdatas = this.Cart
                //    //.Where(i => i.Product.ProductGroup.UniqueId != Guid.Empty)
                //    .Select(i => new MetaData
                //{
                //    Name = i.Product.ProductGroup.Name + '_' + i.Product.ProductGroup.UniqueId.ToString(),
                //    //Id = i.Product.ProductGroup.Id,
                //    //UniqueId = i.Product.ProductGroup.UniqueId,
                //    Type = Enumerations.MetadataType.Product_Group
                //}).Distinct().ToList();

                //PopulateMetadata(ctlGroup, Enumerations.MetadataType.Product_Group, mdatas);


                if (this.ParentId != Guid.Empty)
                {
                    PreSelect(ctlGroup, this.ParentId.ToString());
                }
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

        public List<StockReport> Cart
        {
            get
            {
                var list = (ViewState["Cart"] != null) ? (List<StockReport>)(ViewState["Cart"]) : new List<StockReport>();

                if (ParentId != Guid.Empty)
                {
                    list = list.FindAll(delegate(StockReport i) { return i.Product.ProductGroup.UniqueId == ParentId; });
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