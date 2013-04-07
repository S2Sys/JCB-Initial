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
    public partial class EditMetadata : BasePage
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

                if (Request.QueryString["Id"] != null)
                {
                    string uniqueid = Request.QueryString["Id"];
                    MetaData updatedItem = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(uniqueid); });

                    this.MetadataType = updatedItem.Type;//.Uniqueid.ToString().GetEnumFromDescription<Enumerations.UserType>();


                    //if (Request.QueryString["Type"] != null)
                    //{
                    //    string uniqueid = Request.QueryString["Type"];
                    //    Enumerations.MetadataType type = uniqueid.GetEnumFromDescription<Enumerations.MetadataType>();

                    lblType.Text = this.MetadataType.ToString().Replace('_', ' ');

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


                    PreSelect(ctlType, uniqueid);
                    trType.Visible = trBranch.Visible = false;
                    FillForm(updatedItem);
                    //switch (type)
                    //{
                    //    case Enumerations.MetadataType.Supplier:

                    //        break;
                    //    case Enumerations.MetadataType.Admin:

                    //        break;
                    //    case Enumerations.MetadataType.Sales_Man:

                    //        break;
                    //    case Enumerations.UserType.Customer:

                    //        break;
                    //    case Enumerations.UserType.Branch_Admin:
                    //        break;

                    //}
                }

            }
        }
        private void FillForm(MetaData item)
        {
            PreSelect(ctlBranch, Common.AllBranchesId.ToString());
            //PreSelect(ctlBranch, item.Branch.Uniqueid.ToString());
            PreSelect(ctlType, item.Type.GetDescription());

            ctlName.Text = item.Name;
            ctlDescription.Text = item.Description;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            string uniqueid = Request.QueryString["Id"];
            //Enumerations.UserType type = ctlType.SelectedValue.GetEnumFromDescription<Enumerations.UserType>();

            //string uniqueid = Request.QueryString["Type"];
            Enumerations.MetadataType type = ctlType.SelectedValue.GetEnumFromDescription<Enumerations.MetadataType>();
            Entities.MetaData newEntity = new Entities.MetaData()
            {
                UniqueId = new Guid(uniqueid),
                Branch = new MetaData() { Id = ApplicationContext.BranchId },
                CreatedBy = 1,
                Name = ctlName.Text,
                Type = type,
                Description = ctlDescription.Text,
                Active = true

            };



            bool exists = true;
            string message = string.Empty;
            List<MetaData> items = controller.GetMetaDatas(MetadataType, ApplicationContext.BranchId);
            items.Remove(items.Find(delegate(MetaData u) { return u.UniqueId == new Guid(uniqueid); }));
            exists = items.Exists(delegate(MetaData u)
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

                int updated = controller.Update(newEntity);
                string title = this.MetadataType.ToString().Replace("_", " ");
                if (updated == 0)
                    ShowAlert(title + " updation failed");
                else
                    ShowAlert(title + " updated successfully", "/ManageMetadata.aspx?Type=" + MetadataType.GetDescription());
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