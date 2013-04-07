using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JCB.UI
{
    public partial class PrintPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Control ctrl = (Control)Session["PrintControl"];
                PrintHelper.PrintWebControl(ctrl);

                Response.Redirect(Request.QueryString["RedirectUrl"]);
            }
        }
    }
}