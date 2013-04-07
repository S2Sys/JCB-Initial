using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JCB.BAL;
using JCB.Entities;

namespace JCB.UI.Controls
{
    public partial class PrintControl : System.Web.UI.UserControl
    {
        MetadataController controller = new MetadataController();
        TransactionController itemController = new TransactionController();
        List<MetaData> MetaDatas { get; set; }
        UserController u = new UserController();
        ProductController pc = new ProductController();
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


        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["Id"];
            this.Cart = itemController.GetTransactionsById(new Guid(id));
            BindReport();
        }
        public void BindReport()
        {
            string returnUrl = "ManageTransaction.aspx?Type= " + Request.QueryString["Type"];

            this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);

            if (this.Cart.Count > 0)
            {
                Transaction item = this.Cart[0];
                List<User> users = new List<User>();
                Enumerations.TransactionType type = item.Type;

                if (type == Enumerations.TransactionType.Sales || type == Enumerations.TransactionType.Sales_Return)
                    users = u.GetUsers(Enumerations.UserType.Customer, item.Branch.Id);
                else
                    users = u.GetUsers(Enumerations.UserType.Supplier, item.Branch.Id);

                User me = users.Find(delegate(User usr) { return usr.Id == item.UserId; });
                lblBillNo.Text = item.InvoiceOrBillNo;
                lblDate.Text = item.TransactionDate.ToString("dd/MM/yyyy");
                lblLRNo.Text = item.LRNo;
                lblPhone.Text = (me.Phone + "," + me.Mobile).Trim(',');
                lblRef.Text = item.RefNo;
                lblMode.Text = item.ModeData.Name + " Bill";
                lblThrough.Text = item.Through;// item.Mode;
                lblTin.Text = me.Tin;
                // lblTo.Text = (me.Name == CommonHelper.DummyUserForCashSale) ? item.BillName : me.Name;

                lblTo.Text = item.BillName + "<br>" + me.Address;
                float totalDiscount = this.Cart.Sum(x => x.TotalDiscount);
                float totalPrice = this.Cart.Sum(x => x.TotalPrice);
                float totalTax = this.Cart.Sum(x => x.TotalVAT);

                float totalQty = this.Cart.Sum(x => x.Quantity);
                float total = this.Cart.Sum(x => x.FinalTotal);
                float round = (float)Math.Round(total, 0, MidpointRounding.AwayFromZero);
                //e.Row.Cells[3].Controls.Add(new LiteralControl(totalQty.ToString()));
                //e.Row.Cells[7].Controls.Add(new LiteralControl(totalPrice.ToString()));
                //e.Row.Cells[8].Controls.Add(new LiteralControl(totalDiscount.ToString()));
                //lblVat.Text = totalTax.ToString();

                lblSTQty.Text = totalQty.ToString("##0.00;(#,##0.00);0.00");
                lblSTDis.Text = totalDiscount.ToString("##0.00;(#,##0.00);0.00");
                lblSTAmt.Text = (totalPrice - totalDiscount).ToString("##0.00;(#,##0.00);0.00");


                lblVAT14.Text = this.Cart.Where(x => x.VatTax == 14.5).Sum(x => x.TotalVAT).ToString("##0.00;(#,##0.00);0.00");
                lblVAT5.Text = this.Cart.Where(x => x.VatTax == 5.0).Sum(x => x.TotalVAT).ToString("##0.00;(#,##0.00);0.00");


                lblGTotal.Text = round.ToString("##0.00;(#,##0.00);0");
                lblRound.Text = (round - total).ToString("##0.00;(#,##0.00);0");
                lblRupees.Text = CommonHelper.AmountToText((int)round) + " Only";
            }
            BindCart();

            //Page.RegisterStartupScript("PrintOperation", string.Format("<script>window.print(); window.location='{0}';</script>", returnUrl));


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
                Log.Append(ex);
            }
        }




    }
}