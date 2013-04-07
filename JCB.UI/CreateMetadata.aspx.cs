using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JCB.Entities;

using JCB.BAL;
namespace JCB.UI
{
    public partial class CreateMetadata : BasePage
    {
        MetadataController controller = new MetadataController();
        //UserController userController = new UserController();
        List<MetaData> MetaDatas { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                PopulateMetadata(ctlType, controller.GetMetadataTypes());
                PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, this.MetaDatas);

                if (Request.QueryString["Type"] != null)
                {
                    string uniqueid = Request.QueryString["Type"];
                    this.MetadataType = uniqueid.GetEnumFromDescription<Enumerations.MetadataType>();

                    lblType.Text = this.MetadataType.ToString().Replace('_', ' ');

                    PreSelect(ctlType, uniqueid);
                    trType.Visible = trBranch.Visible = false;

                    PreSelect(ctlBranch, Common.AllBranchesId.ToString());
                    switch (this.MetadataType)
                    {
                        case Enumerations.MetadataType.Branch:
                            ctlDescription.TextMode = TextBoxMode.SingleLine;
                            lblDesc.Text = "Branch Code";

                            break;
                        default:
                            ctlDescription.TextMode = TextBoxMode.MultiLine;
                            ctlDescription.Height = new Unit(100);
                            lblDesc.Text = "Description";
                            break;

                    }
                }

            }
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {

            Entities.MetaData newEntity = new Entities.MetaData()
            {

                Branch = new MetaData() { Id = ApplicationContext.BranchId },
                CreatedBy = 1,
                Name = ctlName.Text,
                Type = this.MetadataType,
                Description = ctlDescription.Text

            };


            bool exists = true;
            string message = string.Empty;
            List<MetaData> users = controller.GetMetaDatas(MetadataType, ApplicationContext.BranchId);

            exists = users.Exists(delegate(MetaData u)
            {
                return (u.Name.ToLower() == ctlName.Text.Trim().ToLower());
            });
            if (exists)
                message = "Name Already exists, Try with some other name?";


            if (exists)
            {
                ShowAlert(message);

            }
            else
            {

                int inserted = controller.Insert(newEntity);

                string title = this.MetadataType.ToString().Replace("_", " ");
                if (inserted == 0)
                    ShowAlert(title + " creation failed");
                else
                    ShowAlert(title + " created successfully", "/ManageMetadata.aspx?Type=" + MetadataType.GetDescription());
            }
        }


        public Enumerations.MetadataType MetadataType
        {
            get
            {
                return (ViewState["MetadataType"] != null) ? (Enumerations.MetadataType)(ViewState["MetadataType"]) : Enumerations.MetadataType.None;

            }
            set
            {
                ViewState["MetadataType"] = value;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string uniqueid = Request.QueryString["Type"];
            Response.Redirect("~/ManageMetadata.aspx?Type=" + uniqueid);
        }

    }
}