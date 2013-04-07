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
    public partial class ManageTransactions : System.Web.UI.Page
    {
        public bool ShowPurchaseRequestNo = false;
        public bool ShowInvoiceNo = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            TransactionController uc = new TransactionController();
            if (!IsPostBack)
            {
                ShowInvoiceNo = false;
                ShowPurchaseRequestNo = false;
                if (Request.QueryString["Type"] != null)
                {
                    string uniqueid = Request.QueryString["Type"];
                    Enumerations.TransactionType type = uniqueid.GetEnumFromDescription<Enumerations.TransactionType>();

                    this.TransactionType = type;
                    lblType.Text = type.ToString().Replace('_', ' ');
                    //<a href="/CreateUser.aspx?Type=D4AA9E6D-1C36-4016-BA3D-297B545B8EA7">Supllier</a>


                    if (TransactionType == Enumerations.TransactionType.Purchase_Request)
                    {
                        ShowPurchaseRequestNo = true;
                    }
                    else
                    {
                        ShowInvoiceNo = true;
                    }


                    hlAdd.NavigateUrl = "~/CreateTransactionNew.aspx?Type=" + uniqueid;

                    PageSortDirection = SortDirection.Ascending;
                    ResultSet = uc.GetTransactions(type, ApplicationContext.BranchId);
                    gvItems.DataSource = ResultSet;
                    gvItems.DataBind();
                    SetColumnHeader();

                }
            }
        }

        protected void gv_RC(object sender, GridViewCommandEventArgs e)
        {
            TransactionController uc = new TransactionController();
            MetadataController mc = new MetadataController();
            switch (e.CommandName)
            {
                case "D":
                    mc.UpdateTransationStatus(new Guid(Convert.ToString(e.CommandArgument)), Convert.ToInt32(ItemStatus.Delete.GetDescription()), ApplicationContext.UserId);

                    break;
            }

            ResultSet = uc.GetTransactions(this.TransactionType, ApplicationContext.BranchId);
            gvItems.DataSource = ResultSet;
            gvItems.DataBind();
        }

        public void SetColumnHeader()
        {
            if (gvItems.HeaderRow != null)
                if (TransactionType == Enumerations.TransactionType.Purchase ||

                             TransactionType == Enumerations.TransactionType.Purchase_Return)
                {
                    // gvItems.Columns[1].HeaderText = "Invoice";

                    gvItems.HeaderRow.Cells[1].Text = "Invoice";
                    gvItems.HeaderRow.Cells[4].Text = "Supplier";
                }
                else if (TransactionType == Enumerations.TransactionType.Purchase_Request)
                {
                    gvItems.HeaderRow.Cells[1].Text = "Purchase Request No";
                    gvItems.HeaderRow.Cells[4].Text = "Supplier";
                }
                else
                {
                    gvItems.HeaderRow.Cells[1].Text = "Bill No";
                    gvItems.Columns[1].HeaderText = "Bill No";
                    gvItems.HeaderRow.Cells[4].Text = "Customer";
                }
        }
        public Enumerations.TransactionType TransactionType
        {
            get
            {
                //SortDirection direction = (SortDirection)System.Enum.ToObject(typeof(SortDirection), ViewState["SortDirection"]);
                //return (direction == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;

                return (Enumerations.TransactionType)System.Enum.ToObject(typeof(Enumerations.TransactionType), ViewState["TransactionType"]);

            }
            set
            {
                ViewState["TransactionType"] = value;
            }
        }
        public List<TransactionView> ResultSet
        {
            get
            {
                var list = (ViewState["ResultSet"] != null) ? (List<TransactionView>)(ViewState["ResultSet"]) : new List<TransactionView>();
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
                gvItems.PageIndex = 0;

                if (e.SortExpression == this.PageSortExpression)
                    this.PageSortDirection = (PageSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
                else
                    this.PageSortDirection = SortDirection.Ascending;

                this.PageSortExpression = e.SortExpression;

                Comparer.CustomSortDirection sd = (this.PageSortDirection == SortDirection.Ascending) ? Comparer.CustomSortDirection.Asc : Comparer.CustomSortDirection.Desc;
                List<TransactionView> sortedResultSet = Comparer.Sort(this.ResultSet, e.SortExpression, sd);
                gvItems.DataSource = sortedResultSet;
                gvItems.DataBind();
                SetColumnHeader();

                string imgAsc = @" <img src='../Images/Icons/asc.png' title='Ascending' />";
                string imgDes = @" <img src='../Images/Icons/desc.png' title='Descendng' />";

                LinkButton lbSort = (LinkButton)gvItems.HeaderRow.Cells[GetColumnIndex(e.SortExpression)].Controls[0];
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
                if (TransactionType == Enumerations.TransactionType.Purchase_Request)
                {
                    ShowPurchaseRequestNo = true;
                }
                else
                {
                    ShowInvoiceNo = true;
                }
            }
            catch (Exception ex)
            {
                //Log.Append(ex.StackTrace);
            }
        }

        private int GetColumnIndex(string SortExpression)
        {
            int i = 0;
            foreach (DataControlField c in gvItems.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;
                i++;
            }
            return i;
        }
        protected void gv_Paging(object sender, GridViewPageEventArgs e)
        {

            gvItems.PageIndex = e.NewPageIndex;
            gvItems.DataSource = ResultSet;
            gvItems.DataBind();
            SetColumnHeader(); 
            if (TransactionType == Enumerations.TransactionType.Purchase_Request)
            {
                ShowPurchaseRequestNo = true;
            }
            else
            {
                ShowInvoiceNo = true;
            }
        }

    }
}