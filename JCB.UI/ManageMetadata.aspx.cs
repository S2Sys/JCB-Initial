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
    public partial class ManageMetadata : System.Web.UI.Page
    {


        public bool ShowNewProductOption = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            MetadataController mc = new MetadataController();
            if (!IsPostBack)
            {
                if (Request.QueryString["Type"] != null)
                {
                    string uniqueid = Request.QueryString["Type"];
                    Enumerations.MetadataType type = uniqueid.GetEnumFromDescription<Enumerations.MetadataType>();
                    if (type == Enumerations.MetadataType.Product_Group)
                        ShowNewProductOption = true;


                    lblType.Text = lblType1.Text = type.ToString().Replace('_', ' ');

                    hlAdd.NavigateUrl = "~/CreateMetadata.aspx?Type=" + uniqueid;

                    PageSortDirection = SortDirection.Ascending;
                    ResultSet = mc.GetMetaDatas(type, ApplicationContext.BranchId);

                    BindMe();



                }
            }
        }

        public void BindMe()
        {
            string uniqueid = Request.QueryString["Type"];
            Enumerations.MetadataType type = uniqueid.GetEnumFromDescription<Enumerations.MetadataType>();
            if (type == Enumerations.MetadataType.Product_Group)
                ShowNewProductOption = true;
            if (type == Enumerations.MetadataType.Branch)
            {
                MetaData u = ResultSet.Find(delegate(MetaData search) { return search.Name == "All"; });
                ResultSet.Remove(u);
            }

            gv.DataSource = ResultSet;
            gv.DataBind();
            gv.Columns[0].Visible = false;
            gv.Columns[1].Visible = false;
            if (type == Enumerations.MetadataType.Product_Group)
                gv.Columns[0].Visible = true;
            else
                gv.Columns[1].Visible = true;

            switch (type)
            {
                case Enumerations.MetadataType.Branch:
                    gv.HeaderRow.Cells[2].Text = "Branch Code";
                    break;
                default:
                    gv.HeaderRow.Cells[2].Text = "Description";
                    break;

            }

        }
        public List<MetaData> ResultSet
        {
            get
            {
                var list = (ViewState["ResultSet"] != null) ? (List<MetaData>)(ViewState["ResultSet"]) : new List<MetaData>();


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
                List<MetaData> sortedResultSet = Comparer.Sort(this.ResultSet, e.SortExpression, sd);
                this.ResultSet = sortedResultSet;
                BindMe();

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
                Log.Append(ex.StackTrace);
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
            BindMe();
        }

        protected void gv_RC(object sender, GridViewCommandEventArgs e)
        {

            MetadataController mc = new MetadataController();
            switch (e.CommandName)
            {
                case "D":
                    mc.UpdateItemStatus("MD_" + e.CommandArgument, Convert.ToInt32(ItemStatus.Delete.GetDescription()), ApplicationContext.UserId);


                    break;
            }

            string uniqueid = Request.QueryString["Type"];
            Enumerations.MetadataType type = uniqueid.GetEnumFromDescription<Enumerations.MetadataType>();


            ResultSet = mc.GetMetaDatas(type, ApplicationContext.BranchId);
            BindMe();
        }

    }
}