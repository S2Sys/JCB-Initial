using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JCB.Entities;

using JCB.BAL;
using System.Data;
namespace JCB.UI
{
    public partial class CreateProduct : BasePage
    {
        MetadataController controller = new MetadataController();
        ProductController pageController = new ProductController();
        private List<MetaData> MetaDatas { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
                PopulateMetadata(ctlUnit, Enumerations.MetadataType.Unit, this.MetaDatas);
                PopulateMetadata(ctlBranch, Enumerations.MetadataType.Branch, this.MetaDatas);
                PopulateMetadata(ctlVat, Enumerations.MetadataType.VAT, this.MetaDatas);
                PopulateMetadata(ctlProductGroup, Enumerations.MetadataType.Product_Group, this.MetaDatas);

                if (Request.QueryString["Id"] != null)
                    PreSelect(ctlProductGroup, Request.QueryString["Id"]);
                RemoveItemByText(ctlBranch, "All");



            }
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);

            Entities.Product newItem = new Entities.Product()
            {
                Branch = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); }),

                CreatedBy = 1,
                Name = ctlName.Text,
                //  CommodityCode = ctlCommodityCode.Text,
                OpStock = (float)Convert.ToDouble(ctlOpStock.Text),
                // ProductCode = ctlProductCode.Text,
                ProductGroup = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlProductGroup.SelectedValue); }),

                PurchasePrice = (float)Convert.ToDouble(ctlPurchasePrice.Text),
                ReorderLevel = (float)Convert.ToDouble(ctlReorderLevel.Text),
                RetailerPrice = (float)Convert.ToDouble(ctlRetailerPrice.Text),
                Unit = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlUnit.SelectedValue); }),
                VatTax = (float)Convert.ToDouble(ctlVat.SelectedItem.Text),
                WholesellerPrice = (float)Convert.ToDouble(ctlWholesellerPrice.Text),


            };

            bool exists = true;
            string message = "";

            List<Product> items = pageController.GetProductsView(newItem.Branch.Id);

            exists = items.Exists(delegate(Product u)
              {
                  return (u.Name.ToLower() == ctlName.Text.Trim().ToLower() &&
                      u.ProductGroup.UniqueId == newItem.ProductGroup.UniqueId);
              });
            if (exists)
                message = "Name Already exists in the Product Group, Try with some other name?";

            if (exists)
            {
                ShowAlert(message);
            }
            else
            {
                int inserted = pageController.Insert(newItem);

                if (inserted == 0)
                    ShowAlert("Product creation failed");
                else
                    if (Request.QueryString["Id"] == null)
                        ShowAlert("Product created successfully", "/ManageProducts.aspx");
                    else
                        ShowAlert("Product created successfully", "/ManageProducts.aspx?Parent=" + Request.QueryString["Id"]);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["Id"] == null)
                Response.Redirect("~/ManageProducts.aspx");
            else
                Response.Redirect("~/ManageProducts.aspx?Parent=" + Request.QueryString["Id"]);
        }

    }
}