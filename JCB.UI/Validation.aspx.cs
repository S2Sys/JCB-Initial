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
    public partial class Validation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                MetadataController u = new MetadataController();
                List<MetaData> MetaDatas = u.GetMetaDatas(ApplicationContext.BranchId);

                PopulateMetadata(ctlType, Enumerations.MetadataType.User_Type, MetaDatas);
                PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, MetaDatas);
                PopulateMetadata(ctlState, Enumerations.MetadataType.States, MetaDatas);

            }
        }
    }
}