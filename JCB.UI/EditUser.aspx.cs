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
    public partial class EditUser : BasePage
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
                PopulateMetadata(ctlState, Enumerations.MetadataType.States, this.MetaDatas);

                if (Request.QueryString["Id"] != null)
                {
                    string uniqueid = Request.QueryString["Id"];
                    User updateUser = userController.GetUserById(new Guid(uniqueid));

                    UserType = updateUser.Type.UniqueId.ToString().GetEnumFromDescription<Enumerations.UserType>();

                    lblType.Text = UserType.ToString().Replace('_', ' ');

                    PreSelect(ctlType, uniqueid);
                    trType.Visible = trUsername.Visible = trTin.Visible = trOB.Visible = trCst.Visible = false;
                    switch (UserType)
                    {
                        case Enumerations.UserType.Supplier:
                            trTin.Visible = trOB.Visible = trCst.Visible = true;
                            break;
                        case Enumerations.UserType.Admin:
                        case Enumerations.UserType.Branch_User:
                            //case Enumerations.UserType.Sales_Man:
                            trUsername.Visible = true;
                            break;
                        case Enumerations.UserType.Customer:
                            trTin.Visible = trOB.Visible = trCst.Visible = true;
                            break;

                    }

                    FillForm(updateUser);
                }

            }
        }

        private void FillForm(User item)
        {
            ctlAddress.Text = item.Address;
            PreSelect(ctlBranch, item.Branch.UniqueId.ToString());
            PreSelect(ctlState, item.State.UniqueId.ToString());
            PreSelect(ctlType, item.Type.UniqueId.ToString());
            ctlCity.Text = item.City;
            ctlCst.Text = item.Cst;
            ctlMobile.Text = item.Mobile;
            ctlName.Text = item.Name;
            ctlOB.Text = item.OpeningBalance.ToString();
            //        ctlPassword.Text = item.Password;
            ctlPhone.Text = item.Phone;
            ctlTin.Text = item.Tin;
            ctlUsername.Text = item.Username;
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string uniqueid = Request.QueryString["Id"];
            Enumerations.UserType type = ctlType.SelectedValue.GetEnumFromDescription<Enumerations.UserType>();
            Entities.User newUser = new Entities.User()
            {
                Address = ctlAddress.Text,
                Branch = new MetaData() { Id = ApplicationContext.BranchId },
                City = ctlCity.Text,
                CreatedBy = 1,
                Cst = ctlCst.Text,
                Mobile = ctlMobile.Text,
                Name = ctlName.Text,
                OpeningBalance = (float)Convert.ToDouble((ctlOB.Text == string.Empty) ? "0" : ctlOB.Text),
                //  Password = ctlPassword.Text,
                Phone = ctlPhone.Text,
                //State = ctlState.SelectedValue,
                UniqueId = new Guid(uniqueid),
                State = new MetaData()
                {
                    //BranchId = CastInt(reader["BranchId"]),
                    //Name = CastString(reader["StateName"]),
                    //Id = CastInt(reader["StateId"]),
                    UniqueId = new Guid(ctlState.SelectedValue)

                },
                //  State = CastString(reader["State"]),
                Tin = ctlTin.Text,
                Type = new MetaData()
                {
                    //BranchId = CastInt(reader["BranchId"]),
                    //Name = CastString(reader["StateName"]),
                    Id = (int)type,
                    UniqueId = new Guid(ctlType.SelectedValue)

                },
                //Type = type, // Convert.ToInt32(ctlType.SelectedValue),
                Username = ctlUsername.Text,
                Active = true

            };

            bool userExists = true;
            string message = string.Empty;
            List<User> users = (userController.GetUsers(type, newUser.Branch.Id));
            users.Remove(users.Find(delegate(User u) { return u.UniqueId == new Guid(uniqueid); }));
            if (type == Enumerations.UserType.Customer || type == Enumerations.UserType.Supplier)
            {
                userExists = users.Exists(delegate(User u)
                {
                    return (u.Name.ToLower() == ctlName.Text.Trim().ToLower());
                });
                if (userExists)
                    message = "Name Already exists, Try with some other name?";
            }
            else
            {
                userExists = users.Exists(delegate(User u)
                {
                    return (u.Username.ToLower() == ctlUsername.Text.Trim().ToLower());
                });

                if (userExists)
                    message = "Username Already exists, Try with some other Username?";
            }

            if (userExists)
            {
                ShowAlert(message);

            }
            else
            {
                int updated = userController.Update(newUser);

                string title = this.UserType.ToString().Replace("_", " ");
                if (updated == 0)
                    ShowAlert(title + " updation failed");
                else
                    ShowAlert(title + " updation successfully", "/ManageUser.aspx?Type=" + UserType.GetDescription());
            }
            //if (inserted == 0)
            //    ShowAlert("User updation failed");
            //else
            //    ShowAlert("User updated successfully", "/ManageUser.aspx?Type=" + type.GetDescription());
        }


        public Enumerations.UserType UserType
        {
            get
            {
                return (ViewState["UserType"] != null) ? (Enumerations.UserType)(ViewState["UserType"]) : Enumerations.UserType.None;

            }
            set
            {
                ViewState["UserType"] = value;
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Enumerations.UserType type = ctlType.SelectedValue.GetEnumFromDescription<Enumerations.UserType>();

            Response.Redirect("~/ManageUser.aspx?Type=" + UserType.GetDescription());
        }
    }
}