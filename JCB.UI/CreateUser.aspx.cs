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
    public partial class CreateUser : BasePage
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
                if (Request.QueryString["Type"] != null)
                {
                    string uniqueid = Request.QueryString["Type"];
                    UserType = uniqueid.GetEnumFromDescription<Enumerations.UserType>();

                    MetaData branch = this.MetaDatas.Find(delegate(MetaData d) { return d.Id == ApplicationContext.BranchId; });
                    PreSelect(ctlBranch, branch.UniqueId.ToString());


                    lblType.Text = UserType.ToString().Replace('_', ' ');

                    PreSelect(ctlType, uniqueid);

                    PreSelectByText(ctlState, "Tamil Nadu");
                    trType.Visible = trUsername.Visible = trTin.Visible = trOB.Visible = trPassword.Visible = trCst.Visible = false;
                    switch (UserType)
                    {
                        case Enumerations.UserType.Supplier:
                            RemoveItemByText(ctlBranch, "All");
                            trTin.Visible = trOB.Visible = trCst.Visible = true;
                            break;
                        case Enumerations.UserType.Admin:
                            trUsername.Visible = trPassword.Visible = true;
                            PreSelectByText(ctlBranch, "All");
                            break;

                        case Enumerations.UserType.Branch_User:
                            RemoveItemByText(ctlBranch, "All");
                            trUsername.Visible = trPassword.Visible = true;
                            break;
                        case Enumerations.UserType.Customer:
                            RemoveItemByText(ctlBranch, "All");
                            //  trBranch.Visible = trUsername.Visible = trPassword.Visible = false;
                            trTin.Visible = trOB.Visible = trCst.Visible = true;
                            break;


                    }
                }

            }
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
            string uniqueid = Request.QueryString["Type"];
            Enumerations.UserType type = uniqueid.GetEnumFromDescription<Enumerations.UserType>();
            Entities.User newUser = new Entities.User()
            {
                Address = ctlAddress.Text,
                Branch = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); }),
                City = ctlCity.Text,
                CreatedBy = ApplicationContext.UserId,
                Cst = ctlCst.Text,
                Mobile = ctlMobile.Text,
                Name = ctlName.Text,
                OpeningBalance = (float)Convert.ToDouble((ctlOB.Text == string.Empty) ? "0" : ctlOB.Text),
                Password = ctlPassword.Text,
                Phone = ctlPhone.Text,
                //State = ctlState.SelectedValue,

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
                    Id = (int)type
                    //Uniqueid = new Guid(ctlState.SelectedValue)

                },
                //Type = type, // Convert.ToInt32(ctlType.SelectedValue),
                Username = ctlUsername.Text

            };


            bool userExists = true;
            string message = "";

            List<User> users = (userController.GetUsers(type, newUser.Branch.Id));
            string a = type.GetDescription();
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
                int inserted = userController.Insert(newUser);

                string title = this.UserType.ToString().Replace("_", " ");
                if (inserted == 0)
                    ShowAlert(title + " creation failed");
                else
                    ShowAlert(title + " creation successfully", "/ManageUser.aspx?Type=" + UserType.GetDescription());
            }

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
            //string uniqueid = Request.QueryString["Type"];
            //Enumerations.UserType type = uniqueid.GetEnumFromDescription<Enumerations.UserType>();
            Response.Redirect("~/ManageUser.aspx?Type=" + UserType.GetDescription());
        }

    }
}