using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JCB.BAL;
using JCB.Entities;

namespace JCB.UI
{
    public partial class SiteMaster : BaseMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                trMenuAdmin.Visible = trMenuUser.Visible = false;

                if (ApplicationContext.CurrentUser == null)
                {
                    if (!Request.Url.PathAndQuery.ToLower().Contains("login.aspx"))
                    {
                        Response.Redirect("~/Login.aspx");

                    }
                    else
                    {
                        ImageButton1.Visible = false;
                        ctlBranch.Visible = false;

                    }

                }
                else
                {

                    MetadataController controller = new MetadataController();
                    List<MetaData> md = controller.GetMetaDatas(ApplicationContext.BranchId);
                    PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, md);
                    RemoveItemByText(ctlBranch, "-- Select --");


                    lblTitle.Text = ApplicationContext.UserTitle;
                    switch (ApplicationContext.UserType)
                    {
                        case Enumerations.UserType.Admin:
                            trMenuAdmin.Visible = true;
                            ctlBranch.Visible = true;
                            break;
                        case Enumerations.UserType.Branch_User:
                            ctlBranch.Enabled = false;
                            trMenuUser.Visible = true;
                            break;

                    }
                    MetaData CurrentBranch = md.Find(delegate(MetaData d) { return d.Id == ApplicationContext.BranchId; });
                    PreSelect(ctlBranch, CurrentBranch.UniqueId.ToString());
                }

            }


        }

        protected void btnLogout_Click(object sender, ImageClickEventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void onBranchChanged(object sender, EventArgs e)
        {
            MetadataController controller = new MetadataController();
            List<MetaData> md = controller.GetMetaDatas(ApplicationContext.BranchId);
            ApplicationContext.ShowBranch = md.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); }).Id;
            Response.Redirect(Request.Url.PathAndQuery);
        }
    }
}
