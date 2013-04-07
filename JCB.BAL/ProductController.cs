using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using JCB.DAL;
using JCB.Enumerations;

namespace JCB.BAL
{
    public class ProductController
    {
        ProductManager db = new ProductManager();
        public List<Product> GetProductsView(int branchId)
        {
            return db.GetProductsView(branchId);

        }
        public List<Product> GetProducts(int branchId)
        {
            return db.GetProducts(branchId);

        }


        public StockReport GetStockReportByProduct(int branch, int product)
        {
            return db.GetStockReportByProduct(branch, product);

        }

        public List<Product> GetProducts(Guid itemGuid)
        {
            return db.GetProducts(itemGuid);

        }


        public Product GetProduct(Guid itemGuid)
        {
            return db.GetProduct(itemGuid);

        }
        public int Insert(Product item)
        {
            return db.Insert(item);
        }



        public int Update(Product item)
        {
            return db.Update(item);
        }
    }
}
