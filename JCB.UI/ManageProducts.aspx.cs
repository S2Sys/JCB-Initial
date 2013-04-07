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
    public partial class ManageProducts : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            ProductController uc = new ProductController();
            if (!IsPostBack)
            {
                MetadataController mc = new MetadataController();

                PageSortDirection = SortDirection.Ascending;
                if (Request.QueryString["Parent"] != null)
                {
                    hlAdd.NavigateUrl = "~/CreateProduct.aspx?Id=" + Request.QueryString["Parent"];
                    ResultSet = uc.GetProductsView(ApplicationContext.BranchId).FindAll(delegate(Product p)
                    {
                        return p.ProductGroup.UniqueId == new Guid(Request.QueryString["Parent"]);
                    });

                    lblBranch.Text = "( " + mc.GetMetaDatas(ApplicationContext.BranchId).Find(
                        delegate(MetaData d)
                        {
                            return d.UniqueId == new Guid(Request.QueryString["Parent"]);
                        }).Name + " )";
                }
                else
                {
                    hlAdd.NavigateUrl = "~/CreateProduct.aspx";
                    ResultSet = uc.GetProductsView(ApplicationContext.BranchId);
                }

                gv.DataSource = ResultSet;
                gv.DataBind();

            }
        }

        protected void gv_RC(object sender, GridViewCommandEventArgs e)
        {
            ProductController uc = new ProductController();
            MetadataController mc = new MetadataController();
            switch (e.CommandName)
            {
                case "D":
                    mc.UpdateItemStatus("P_" + e.CommandArgument, Convert.ToInt32(ItemStatus.Delete.GetDescription()), ApplicationContext.UserId);


                    break;
            }

            ResultSet = uc.GetProductsView(ApplicationContext.BranchId);
            gv.DataSource = ResultSet;
            gv.DataBind();
        }

        public List<Product> ResultSet
        {
            get
            {
                var list = (ViewState["ResultSet"] != null) ? (List<Product>)(ViewState["ResultSet"]) : new List<Product>();
                return list;
            }
            set
            {
                ViewState["ResultSet"] = value;
            }
        }

        public SortDirection PageSortDirection
        {
            get
            {
                //SortDirection direction = (SortDirection)System.Enum.ToObject(typeof(SortDirection), ViewState["SortDirection"]);
                //return (direction == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;

                return (SortDirection)System.Enum.ToObject(typeof(SortDirection), ViewState["SortDirection"]);

            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        string defaultSortExpression = "Name";
        public string PageSortExpression
        {
            get
            {
                return (ViewState["SortExpression"] != null) ? (string)(ViewState["SortExpression"]) : defaultSortExpression;

            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
        public void gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                gv.PageIndex = 0;

                if (e.SortExpression == this.PageSortExpression)
                    this.PageSortDirection = (PageSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
                else
                    this.PageSortDirection = SortDirection.Ascending;

                this.PageSortExpression = e.SortExpression;

                Comparer.CustomSortDirection sd = (this.PageSortDirection == SortDirection.Ascending) ? Comparer.CustomSortDirection.Asc : Comparer.CustomSortDirection.Desc;
                List<Product> sortedResultSet = Comparer.Sort(this.ResultSet, e.SortExpression, sd);
                gv.DataSource = sortedResultSet;
                gv.DataBind();


                string imgAsc = @" <img src='../Images/Icons/asc.png' title='Ascending' />";
                string imgDes = @" <img src='../Images/Icons/desc.png' title='Descendng' />";

                LinkButton lbSort = (LinkButton)gv.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].Controls[0];
                if (PageSortDirection == SortDirection.Ascending)
                {
                    lbSort.Text += imgAsc;
                }
                else
                {
                    lbSort.Text += imgDes;
                }

                //if (lbSort.Text == PageSortDirection)
                //{
                //    if (grdCust.SortDirection == SortDirection.Ascending)
                //        lbSort.Text += imgAsc;
                //    else
                //        lbSort.Text += imgDes;
                //}

                //if (PageSortDirection == SortDirection.Ascending)
                //{
                //    gvUsers.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].CssClass = "Access";
                //}
                //else
                //{
                //    gvUsers.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].Controls = "Access";
                //}

            }
            catch (Exception ex)
            {
                //Log.Append(ex.StackTrace);
            }
        }

        private int GetColumnIndex(string SortExpression)
        {
            int i = 0;
            foreach (DataControlField c in gv.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;
                i++;
            }
            return i;
        }
        protected void gv_Paging(object sender, GridViewPageEventArgs e)
        {

            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = ResultSet;
            gv.DataBind();
        }

    }
}