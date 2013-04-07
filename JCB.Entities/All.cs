using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using JCB.Enumerations;

namespace JCB.Entities
{

    public class MetadataType
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }


    //ENTITIES 
    //************************************************************ 

    [Serializable]
    public class Base
    {
        public string Uniquename { get; set; }
        public MetaData Branch { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public bool Active { get; set; }
    }

    #region Metadata
    [Serializable]
    public class MetaData : Base
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Enumerations.MetadataType Type { get; set; }
    }

    #endregion

    #region Transaction

    [Serializable]
    public class TransactionView : Base
    {
        public MetaData Branch { get; set; }
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceOrBillNo { get; set; }
        public int Mode { get; set; }

        public string Reference { get; set; }

        public string ModeName { get; set; }
        public float VatTax { get; set; }
        public User TransactionBy { get; set; }
        public int UserId { get; set; }
        public Guid TransactionUniqueId { get; set; }
        private float totalVAT;
        public float TotalVAT
        {
            get { return (float)Math.Round(totalVAT, 2); }
            set { totalVAT = value; }
        }

        private float totalQuantity;
        public float TotalQuantity
        {
            get { return (float)Math.Round(totalQuantity, 2); }
            set { totalQuantity = value; }
        }
        private float totalDiscount;
        public float TotalDiscount
        {
            get { return (float)Math.Round(totalDiscount, 2); }

            set { totalDiscount = value; }
        }
        private float totalPrice;
        public float TotalPrice
        {
            get { return (float)Math.Round(totalPrice, 2); }
            set { totalPrice = value; }

        }
        private float finalTotal;
        public float FinalTotal
        {
            get
            {
                // return (float)Math.Round(finalTotal, 2); 
                return (float)Math.Round((float)(finalTotal), 0, MidpointRounding.AwayFromZero);
            }
            set { finalTotal = value; }
        }
    }


    [Serializable]
    public class TransactionBase : Base
    {

        public Guid TempID { get; set; } // { return Guid.NewGuid(); } }
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceOrBillNo { get; set; }
        public int Mode { get; set; }
        public MetaData ModeData { get; set; }

        public int ProductId { get; set; }
        public MetaData ProductGroup { get; set; }
        public Product Product { get; set; }

        public string Reference { get; set; }
        public string BillName { get; set; }

        public string ReorderLevel { get; set; }
        public float Unit { get; set; }
        public float Quantity { get; set; }
        public float Rate { get; set; }
        public TransactionType Type { get; set; }
        public float Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public float VatTax { get; set; }
        public User TransactionBy { get; set; }

        public string RefNo { get; set; }
        public string RefDate { get; set; }
        public string LRNo { get; set; }
        public string Through { get; set; }

        public string DiscountDetail { get { return string.Format("{0} {1}", Discount.ToString(), DiscountType.GetDescription()); } }


        public float TotalAmountPrint
        {
            get
            {
                float totPrice = (float)(Quantity * Rate) - TotalDiscount;
                //float vat = (totPrice * VatTax) / 100;
                return (float)Math.Round(totPrice, 2);
            }
        }

        public float TotalVAT
        {
            get
            {
                float totPrice = (float)(Quantity * Rate) - TotalDiscount;
                float vat = (totPrice * VatTax) / 100;
                return (float)Math.Round(vat, 2);
            }
        }

        //public float TotalDiscount
        //{
        //    get
        //    {
        //        float discount = 0;
        //        if (DiscountType == Enumerations.DiscountType.Percentage)
        //            discount = (float)((Rate / 100) * Discount) * Quantity;
        //        else
        //            discount = (float)(Discount * Quantity);
        //        return (float)Math.Round(discount, 2);
        //    }

        //}
        public float TotalDiscount
        {
            get
            {
                float discount = 0;
                if (DiscountType == Enumerations.DiscountType.Percentage)
                    discount = (float)((Rate * Quantity) / 100) * Discount;
                else
                    discount = Discount;// (float)(Discount * Quantity);
                return (float)Math.Round(discount, 2);
            }

        }
        public float TotalPrice
        {

            get
            {
                return (float)(Quantity * Rate);

            }

        }

        public float FinalTotal
        {

            get
            {

                return (TotalPrice - TotalDiscount) + TotalVAT;

                //return (float)Math.Round((float)((TotalPrice - TotalDiscount) + TotalVAT), 0, MidpointRounding.AwayFromZero);
            }

        }
    }
    [Serializable]
    public class Transaction : TransactionBase
    {

        public int UserId { get; set; }
        public Guid TransactionUniqueId { get; set; }
    }

    #endregion

    #region Users
    [Serializable]
    public class BaseUser : Base
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public MetaData State { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

    }
    [Serializable]
    public class ApplicationUser : BaseUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public MetaData Type { get; set; }
    }

    [Serializable]
    public class User : BaseUser
    {
        public MetaData Type { get; set; }
        public string Tin { get; set; }
        public string Cst { get; set; }
        public float OpeningBalance { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AdditionalDetails { get; set; }
    }

    public class UserScope
    {
        public int UserScopeId { get; set; }
        public int UserId { get; set; }
        public float OpeningBalance { get; set; }

    }

    #endregion

    #region Product
    [Serializable]
    public class Product : Base
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public MetaData ProductGroup { get; set; }
        public string Name { get; set; }
        // public string ProductCode { get; set; }
        public MetaData Unit { get; set; }
        // public string CommodityCode { get; set; }
        public float ReorderLevel { get; set; }
        public float PurchasePrice { get; set; }
        public float WholesellerPrice { get; set; }
        public float RetailerPrice { get; set; }
        public float OpStock { get; set; }
        public float VatTax { get; set; }
        public MetaData Branch { get; set; }


        public string AdditionalDetails { get; set; }
        // public List<ProductScope> Scopes { get; set; }
    }
    public class ProductScope
    {
        public int ProductScopeId { get; set; }
        public int BranchId { get; set; }
        public float ReorderLevel { get; set; }
        public float OpStock { get; set; }
    }
    #endregion

    #region Reports


    [Serializable]
    public class ReorderLevelReport
    {
        public Product Product { get; set; }
        public MetaData Branch { get; set; }
        public float BuyQuantity { get; set; }
        public float SellQuantity { get; set; }
        public float OutstandingQuantity { get; set; }
    }


    [Serializable]
    public class BalanceReport
    {
        public DateTime TransactionDate { get; set; }
        public MetaData Branch { get; set; }
        public float Credit { get; set; }
        public float Debit { get; set; }
        public string Title { get; set; }
    }




    [Serializable]
    public class StockReport
    {
        public Product Product { get; set; }
        public MetaData Branch { get; set; }
        public float BuyQuantity { get; set; }
        public float SellQuantity { get; set; }
        public float OutstandingQuantity { get; set; }
        public float Sales { get; set; }
        public float SalesReturn { get; set; }

        public float Purchase { get; set; }
        public float PurchaseReturn { get; set; }
    }
    #endregion
}
