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
    public partial class ManageUser : System.Web.UI.Page
    {
        public bool ShowPassowrdReset = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            UserController uc = new UserController();
            if (!IsPostBack)
            {

                if(Request.QueryString["Mode"] !=null)
                {
                    gvUsers.AllowPaging = false;
                }
                if (Request.QueryString["Type"] != null)
                {
                    string uniqueid = Request.QueryString["Type"];
                    Enumerations.UserType type = uniqueid.GetEnumFromDescription<Enumerations.UserType>();

                    lblType.Text = type.ToString().Replace('_', ' ');
                    //<a href="/CreateUser.aspx?Type=D4AA9E6D-1C36-4016-BA3D-297B545B8EA7">Supllier</a>

                    hlAdd.NavigateUrl = "~/CreateUser.aspx?Type=" + uniqueid;

                    PageSortDirection = SortDirection.Ascending;
                    ResultSet = uc.GetUsers(type, ApplicationContext.BranchId);
                    if (type == UserType.Admin)
                    {

                        User u = ResultSet.Find(delegate(User search){ return  search.Username=="admin";});
                        ResultSet.Remove(u);
                    }
                    switch (type)
                    {
                        case Enumerations.UserType.Admin:
                        case Enumerations.UserType.Branch_User:
     
                            ShowPassowrdReset = true;
                            break;
                        case Enumerations.UserType.Supplier:
                        case Enumerations.UserType.Customer:
 
                            break;

                    }


                    gvUsers.DataSource = ResultSet;
                    gvUsers.DataBind();

                    switch (type)
                    {
                        case Enumerations.UserType.Admin:
                        case Enumerations.UserType.Branch_User:
                            gvUsers.Columns[7].Visible = false;
                            gvUsers.Columns[8].Visible = false;
                            gvUsers.Columns[9].Visible = false;

                            ShowPassowrdReset = true;
                            break;
                        case Enumerations.UserType.Supplier:
                        case Enumerations.UserType.Customer:
                            gvUsers.Columns[2].Visible = false;
                            gvUsers.Columns[3].Visible = false;
                            break;

                    }
                }
            }
        }


        public List<User> ResultSet
        {
            get
            {
                var list = (ViewState["ResultSet"] != null) ? (List<User>)(ViewState["ResultSet"]) : new List<User>();
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
                gvUsers.PageIndex = 0;

                if (e.SortExpression == this.PageSortExpression)
                    this.PageSortDirection = (PageSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
                else
                    this.PageSortDirection = SortDirection.Ascending;

                this.PageSortExpression = e.SortExpression;

                Comparer.CustomSortDirection sd = (this.PageSortDirection == SortDirection.Ascending) ? Comparer.CustomSortDirection.Asc : Comparer.CustomSortDirection.Desc;
                List<User> sortedResultSet = Comparer.Sort(this.ResultSet, e.SortExpression, sd);
                gvUsers.DataSource = sortedResultSet;
                gvUsers.DataBind();


                string imgAsc = @" <img src='../Images/Icons/asc.png' title='Ascending' />";
                string imgDes = @" <img src='../Images/Icons/desc.png' title='Descendng' />";

                LinkButton lbSort = (LinkButton)gvUsers.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].Controls[0];
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
            foreach (DataControlField c in gvUsers.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;
                i++;
            }
            return i;
        }
        protected void gv_Paging(object sender, GridViewPageEventArgs e)
        {

            gvUsers.PageIndex = e.NewPageIndex;
            gvUsers.DataSource = ResultSet;
            gvUsers.DataBind();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UserController uc = new UserController();

            MetadataController mc = new MetadataController();
            switch (e.CommandName)
            {
                case "D":
                    mc.UpdateItemStatus("U_" + e.CommandArgument, Convert.ToInt32(ItemStatus.Delete.GetDescription()), ApplicationContext.UserId);
                    break;
            }

            string uniqueid = Request.QueryString["Type"];
            Enumerations.UserType type = uniqueid.GetEnumFromDescription<Enumerations.UserType>();
            ResultSet = uc.GetUsers(type, ApplicationContext.BranchId);

            gvUsers.DataSource = ResultSet;
            gvUsers.DataBind();

        }

    }
}