using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JCB.BAL;
using JCB.Entities;
using JCB.Enumerations;

namespace JCB.UI
{
    public partial class ReportBalance : BasePage
    {
        MetadataController controller = new MetadataController();
        UserController u = new UserController();

        List<MetaData> MetaDatas { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                PopulateMetadata(ctlUT, Enumerations.MetadataType.User_Type, controller.GetUserTypes());
                PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, this.MetaDatas);
                RemoveItemByText(ctlBranch, "All");

                List<User> all = new List<Entities.User>();
                all.AddRange(u.GetUsers(Enumerations.UserType.Supplier, ApplicationContext.BranchId));
                all.AddRange(u.GetUsers(Enumerations.UserType.Customer, ApplicationContext.BranchId));
                PopulateMetadata(ctlUser, all, "Name", "Id");
                PreSelect(ctlBranch, MetaDatas.Find(delegate(MetaData d) { return d.Id == ApplicationContext.BranchId; }).UniqueId.ToString());

            }
        }

        protected void ctlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ctlBranch.SelectedIndex != 0)
            {
                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);

                int seleId = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); }).Id;

                List<User> all = new List<Entities.User>();
                all.AddRange(u.GetUsers(Enumerations.UserType.Supplier, seleId));
                all.AddRange(u.GetUsers(Enumerations.UserType.Customer, seleId));
                PopulateMetadata(ctlUser, all, "Name", "Id");
                ctlUser.SelectedIndex = 0;
            }

        }

        protected void ctlUT_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);

            int seleId = MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); }).Id;

            List<User> all = new List<Entities.User>();
            all.AddRange(u.GetUsers(ctlUT.SelectedValue.GetEnumFromDescription<UserType>(), seleId));
            PopulateMetadata(ctlUser, all, "Name", "Id");
            ctlUser.SelectedIndex = 0;
        }

        protected void showBalanceReport(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int userId = Convert.ToInt32(ctlUser.SelectedValue);
                DateTime sd = ConvertToMMddyyyy(ctlSDate.Text);
                DateTime ed = ConvertToMMddyyyy(ctlEDate.Text);

                ReportController rpt = new ReportController();
                List<BalanceReport> cart = rpt.GetBalanceReport(userId, sd, ed);
                this.Cart = cart;
                gv.DataSource = cart;
                gv.DataBind();
            }
        }


        public List<BalanceReport> Cart
        {
            get
            {
                var list = (ViewState["Cart"] != null) ? (List<BalanceReport>)(ViewState["Cart"]) : new List<BalanceReport>();
                return list;
            }
            set
            {
                ViewState["Cart"] = value;
            }
        }


        protected void rowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                float totalCredit = this.Cart.Sum(item => item.Credit);
                float totalDebit = this.Cart.Sum(x => x.Debit);

                e.Row.Cells[2].Controls.Add(new LiteralControl("Total "));
                e.Row.Cells[3].Controls.Add(new LiteralControl(totalCredit.ToString()));
                e.Row.Cells[4].Controls.Add(new LiteralControl(totalDebit.ToString()));


                GridView gv = (GridView)sender;
                GridViewRow newRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal);



                TableCell newCell = new TableCell() { Text = "Balance", ColumnSpan = 4, RowSpan = 1 };
                newRow.Cells.Add(newCell);


                newCell = new TableCell() { Text = (totalCredit - totalDebit).ToString(), ColumnSpan = 1, RowSpan = 1 };
                newRow.Cells.Add(newCell);

                gv.Controls[0].Controls.AddAt(gv.Rows.Count + 2, newRow);

            }
        }

    }
}