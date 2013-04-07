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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserController uc = new UserController();
            User loggedUser = uc.LoginUser(txtUser.Text.Trim(), txtPassword.Text.Trim());
            if (loggedUser != null)
            {

                ApplicationContext.CurrentUser = loggedUser;
                ApplicationContext.ShowBranch = loggedUser.Branch.Id;
                Response.Redirect("~/");
            }
            else
            {
                lblError.Text = "Invalid username/password contact administrator to reset your password?";
                lblError.CssClass = "fail";
                ApplicationContext.CurrentUser = null;
            }
        }
    }
}