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
    public partial class EditProduct : BasePage
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
                {
                    string uniqueid = Request.QueryString["Id"];
                    Product updatedItem = pageController.GetProduct(new Guid(uniqueid));
                    FillForm(updatedItem);

                }
            }
        }
        private void FillForm(Product item)
        {
            PreSelect(ctlBranch, item.Branch.UniqueId.ToString());
            PreSelect(ctlProductGroup, item.ProductGroup.UniqueId.ToString());
            PreSelect(ctlUnit, item.Unit.UniqueId.ToString());
            PreSelectByText(ctlVat, item.VatTax.ToString());

            ctlName.Text = item.Name;
            //ctlCommodityCode.Text = item.CommodityCode;
            ctlOpStock.Text = item.OpStock.ToString();
            // ctlProductCode.Text = item.ProductCode;
            ctlPurchasePrice.Text = item.PurchasePrice.ToString();
            ctlReorderLevel.Text = item.ReorderLevel.ToString();
            ctlRetailerPrice.Text = item.RetailerPrice.ToString();
            //ctlVatTax.Text = item.VatTax.ToString();
            ctlWholesellerPrice.Text = item.WholesellerPrice.ToString();


        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            this.MetaDatas = controller.GetMetaDatas(ApplicationContext.BranchId);
            string uniqueid = Request.QueryString["Id"];
            //Enumerations.UserType type = uniqueid.GetEnumFromDescription<Enumerations.UserType>();


            Entities.Product updatedItem = new Entities.Product()
            {
                Branch = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlBranch.SelectedValue); }),
                UniqueId = new Guid(uniqueid),
                ModifiedBy = 1,
                Name = ctlName.Text,
                // CommodityCode = ctlCommodityCode.Text,
                OpStock = (float)Convert.ToDouble(ctlOpStock.Text),
                // ProductCode = ctlProductCode.Text,
                ProductGroup = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlProductGroup.SelectedValue); }),
                //new MetaData() { Uniqueid = new Guid(ctlProductGroup.SelectedValue) },
                PurchasePrice = (float)Convert.ToDouble(ctlPurchasePrice.Text),
                ReorderLevel = (float)Convert.ToDouble(ctlReorderLevel.Text),
                RetailerPrice = (float)Convert.ToDouble(ctlRetailerPrice.Text),
                Unit = this.MetaDatas.Find(delegate(MetaData d) { return d.UniqueId == new Guid(ctlUnit.SelectedValue); }),
                VatTax = (float)Convert.ToDouble(ctlVat.SelectedItem.Text),
                WholesellerPrice = (float)Convert.ToDouble(ctlWholesellerPrice.Text),

            };


            bool exists = true;
            string message = "";

            List<Product> items = pageController.GetProductsView(updatedItem.Branch.Id);
            items.Remove(items.Find(delegate(Product u)
            {
                return u.UniqueId == updatedItem.UniqueId;
            }));
            exists = items.Exists(delegate(Product u)
              {
                  return (u.Name.ToLower() == ctlName.Text.Trim().ToLower() &&
                      u.ProductGroup.UniqueId == updatedItem.ProductGroup.UniqueId);
              });
            if (exists)
                message = "Name Already exists in the Product Group, Try with some other name?";

            if (exists)
            {
                ShowAlert(message);
            }
            else
            {

                int updated = pageController.Update(updatedItem);

                if (updated == 0)
                    ShowAlert("Product updation failed");
                else
                    ShowAlert("Product updated successfully", "/ManageProducts.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageProducts.aspx");
        }

    }
}