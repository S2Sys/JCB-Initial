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
    public partial class ResetPassword : BasePage
    {
        MetadataController controller = new MetadataController();
        UserController userController = new UserController();
        List<MetaData> MetaDatas { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                PopulateMetadata(ctlType, Enumerations.MetadataType.User_Type, this.MetaDatas);
                PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, this.MetaDatas);

                if (Request.QueryString["Id"] != null)
                {
                    string uniqueid = Request.QueryString["Id"];
                    User updateUser = userController.GetUserById(new Guid(uniqueid));

                    Enumerations.UserType type = updateUser.Type.UniqueId.ToString().GetEnumFromDescription<Enumerations.UserType>();

                    lblType.Text = type.ToString().Replace('_', ' ');

                    PreSelect(ctlType, uniqueid);
                    trType.Visible = trBranch.Visible = trUsername.Visible = false;
                    switch (type)
                    {
                        case Enumerations.UserType.Supplier:
                             break;
                        case Enumerations.UserType.Admin:
                        case Enumerations.UserType.Branch_User:
                            trUsername.Visible = true;
                            break;
                        case Enumerations.UserType.Customer:

                            break;

                    }

                    FillForm(updateUser);
                }

            }
        }

        private void FillForm(User item)
        {
           
            PreSelect(ctlBranch, item.Branch.UniqueId.ToString());
            PreSelect(ctlType, item.Type.UniqueId.ToString());
            ctlName.Text = item.Name;
            ctlUsername.Text = item.Username;
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string uniqueid = Request.QueryString["Id"];
            Enumerations.UserType type = ctlType.SelectedValue.GetEnumFromDescription<Enumerations.UserType>();

            int inserted = userController.UpdatePassword(new Guid(uniqueid), ctlPassword.Text);
            if (inserted == 0)
                ShowAlert("User updation failed");
            else
                ShowAlert("User updated successfully", "/ManageUser.aspx?Type=" + type.GetDescription());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Enumerations.UserType type = ctlType.SelectedValue.GetEnumFromDescription<Enumerations.UserType>();

            Response.Redirect("~/ManageUser.aspx?Type=" + type.GetDescription());
        }
    }
}