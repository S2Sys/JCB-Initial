using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using System.Data.SqlClient;
using System.Data;

namespace JCB.DAL
{
    public class ProductManager : DataAccessBase
    {
        #region Users

        public List<Product> GetProducts(int branchId)
        {
            List<Product> items = new List<Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_Products";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new Product()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    ProductGroup = new MetaData()
                                    {
                                        Name = CastString(reader["ProductGroupName"]),
                                        Id = CastInt(reader["ProductGroupId"]),
                                        UniqueId = CastGuid(reader["ProductGroupUniqueId"])
                                    },
                                    Name = CastString(reader["Name"]),
                                  //  ProductCode = CastString(reader["Productcode"]),
                                    Unit = new MetaData()
                                    {
                                        Name = CastString(reader["UnitName"]),
                                        Id = CastInt(reader["UnitId"]),
                                        UniqueId = CastGuid(reader["UnitUniqueId"])
                                    },
                                   // CommodityCode = CastString(reader["Commoditycode"]),
                                    ReorderLevel = CastFloat(reader["Reorderlevel"]),
                                    OpStock = CastFloat(reader["Opstock"]),

                                    PurchasePrice = CastFloat(reader["Purchaseprice"]),
                                    WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
                                    RetailerPrice = CastFloat(reader["Retailerprice"]),

                                    VatTax = CastFloat(reader["Vattax"]),

                                    Branch = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    Uniquename = CastString(reader["Uniquename"]),
                                    CreatedOn = CastDateTime(reader["Createdon"]),
                                    CreatedBy = CastInt(reader["Createdby"]),
                                    ModifiedOn = CastDateTime(reader["Modifiedon"]),
                                    ModifiedBy = CastInt(reader["Modifiedby"]),
                                    Active = CastBoolean(reader["Active"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
            return items;

        }


        public List<Product> GetProductsView(int branchId)
        {
            List<Product> items = new List<Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_ProductsView";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new Product()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    ProductGroup = new MetaData()
                                    {
                                        Name = CastString(reader["ProductGroupName"]),
                                        Id = CastInt(reader["ProductGroupId"]),
                                        UniqueId = CastGuid(reader["ProductGroupUniqueId"])
                                    },
                                    Name = CastString(reader["Title"]),
                                  //  ProductCode = CastString(reader["Productcode"]),
                                    Unit = new MetaData()
                                    {
                                        Name = CastString(reader["UnitName"]),
                                        Id = CastInt(reader["UnitId"]),
                                        UniqueId = CastGuid(reader["UnitUniqueId"])
                                    },
                                   // CommodityCode = CastString(reader["Commoditycode"]),
                                    //AllReorderLevels = CastString(reader["Reorderlevels"]),
                                    //AllOpStocks = CastString(reader["Opstocks"]),
                                    ReorderLevel = CastFloat(reader["Reorderlevel"]),
                                    OpStock = CastFloat(reader["Opstock"]),

                                    PurchasePrice = CastFloat(reader["Purchaseprice"]),
                                    WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
                                    RetailerPrice = CastFloat(reader["Retailerprice"]),

                                    VatTax = CastFloat(reader["Vattax"]),

                                    Branch = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    Uniquename = CastString(reader["Uniquename"]),
                                    CreatedOn = CastDateTime(reader["Createdon"]),
                                    CreatedBy = CastInt(reader["Createdby"]),
                                    ModifiedOn = CastDateTime(reader["Modifiedon"]),
                                    ModifiedBy = CastInt(reader["Modifiedby"]),
                                    Active = CastBoolean(reader["Active"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
            return items;

        }

        public List<Product> GetProducts(Guid itemGuid)
        {
            List<Product> item = new List<Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_Product";
                        cmd.Parameters.Add("@UniqueId", SqlDbType.UniqueIdentifier).Value = itemGuid;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                item.Add(new Product()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    ProductGroup = new MetaData()
                                    {
                                        Name = CastString(reader["ProductGroupName"]),
                                        Id = CastInt(reader["ProductGroupId"]),
                                        UniqueId = CastGuid(reader["ProductGroupUniqueId"])
                                    },
                                    Name = CastString(reader["Name"]),
                                    //ProductCode = CastString(reader["Productcode"]),
                                    Unit = new MetaData()
                                    {
                                        Name = CastString(reader["UnitName"]),
                                        Id = CastInt(reader["UnitId"]),
                                        UniqueId = CastGuid(reader["UnitUniqueId"])
                                    },
                                   // CommodityCode = CastString(reader["Commoditycode"]),
                                    ReorderLevel = CastFloat(reader["Reorderlevel"]),
                                    PurchasePrice = CastFloat(reader["Purchaseprice"]),
                                    WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
                                    RetailerPrice = CastFloat(reader["Retailerprice"]),
                                    OpStock = CastFloat(reader["Opstock"]),
                                    VatTax = CastFloat(reader["Vattax"]),

                                    Branch = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    Uniquename = CastString(reader["Uniquename"]),
                                    CreatedOn = CastDateTime(reader["Createdon"]),
                                    CreatedBy = CastInt(reader["Createdby"]),
                                    ModifiedOn = CastDateTime(reader["Modifiedon"]),
                                    ModifiedBy = CastInt(reader["Modifiedby"]),
                                    Active = CastBoolean(reader["Active"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
            return item;

        }

        public Product GetProduct(Guid itemGuid)
        {
            Product item = new Product();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_Product";
                        cmd.Parameters.Add("@UniqueId", SqlDbType.UniqueIdentifier).Value = itemGuid;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                item = (new Product()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    ProductGroup = new MetaData()
                                    {
                                        Name = CastString(reader["ProductGroupName"]),
                                        Id = CastInt(reader["ProductGroupId"]),
                                        UniqueId = CastGuid(reader["ProductGroupUniqueId"])
                                    },
                                    Name = CastString(reader["Name"]),
                                   // ProductCode = CastString(reader["Productcode"]),
                                    Unit = new MetaData()
                                    {
                                        Name = CastString(reader["UnitName"]),
                                        Id = CastInt(reader["UnitId"]),
                                        UniqueId = CastGuid(reader["UnitUniqueId"])
                                    },
                                   // CommodityCode = CastString(reader["Commoditycode"]),
                                    ReorderLevel = CastFloat(reader["Reorderlevel"]),
                                    PurchasePrice = CastFloat(reader["Purchaseprice"]),
                                    WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
                                    RetailerPrice = CastFloat(reader["Retailerprice"]),
                                    OpStock = CastFloat(reader["Opstock"]),
                                    VatTax = CastFloat(reader["Vattax"]),

                                    Branch = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    Uniquename = CastString(reader["Uniquename"]),
                                    CreatedOn = CastDateTime(reader["Createdon"]),
                                    CreatedBy = CastInt(reader["Createdby"]),
                                    ModifiedOn = CastDateTime(reader["Modifiedon"]),
                                    ModifiedBy = CastInt(reader["Modifiedby"]),
                                    Active = CastBoolean(reader["Active"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
            return item;

        }

        public StockReport GetStockReportByProduct(int branchId, int productId)
        {
            StockReport item = new StockReport();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Report_Stock_ByProduct";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = (int)productId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                item = (new StockReport()
                                {
                                    Branch = new MetaData()
                                    {
                                        //BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        // UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    Product = new Product()
                                    {
                                        Id = CastInt(reader["ProductId"]),
                                        UniqueId = CastGuid(reader["ProductUniqueid"]),
                                        OpStock = CastFloat(reader["Opstock"]),
                                        ProductGroup = new MetaData()
                                        {
                                            Name = CastString(reader["ProductGroupName"]),
                                            Id = CastInt(reader["ProductGroupId"]),
                                            UniqueId = CastGuid(reader["ProductGroupUniqueId"])
                                        },
                                        Name = CastString(reader["ProductName"]),
                                        //   ProductCode = CastString(reader["ProductProductCode"]),
                                        //  CommodityCode = CastString(reader["Commoditycode"]),
                                        ReorderLevel = CastFloat(reader["ProductReorderlevel"]),
                                        //PurchasePrice = CastFloat(reader["Purchaseprice"]),
                                        //WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
                                        //RetailerPrice = CastFloat(reader["Retailerprice"]),
                                    },
                                    BuyQuantity = CastFloat(reader["BuyQuantity"]),
                                    SellQuantity = CastFloat(reader["SellQuantity"]),
                                    OutstandingQuantity = CastFloat(reader["OutstandingQuantity"])

                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
            return item;

        }

        public int Insert(Product item)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Insert_Product";
                        cmd.Parameters.Add("@Productgroup", SqlDbType.Int).Value = item.ProductGroup.Id;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = item.Name;
                        //cmd.Parameters.Add("@Productcode", SqlDbType.VarChar, 100).Value = item.ProductCode;
                        cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = item.Unit.Id;
                        //cmd.Parameters.Add("@Commoditycode", SqlDbType.VarChar, 50).Value = item.CommodityCode;
                        cmd.Parameters.Add("@Reorderlevel", SqlDbType.Float).Value = item.ReorderLevel;
                        cmd.Parameters.Add("@Purchaseprice", SqlDbType.Float).Value = item.PurchasePrice;
                        cmd.Parameters.Add("@Wholesellerprice", SqlDbType.Float).Value = item.WholesellerPrice;
                        cmd.Parameters.Add("@Retailerprice", SqlDbType.Float).Value = item.RetailerPrice;
                        cmd.Parameters.Add("@Opstock", SqlDbType.Float).Value = item.OpStock;
                        cmd.Parameters.Add("@Vattax", SqlDbType.Float).Value = item.VatTax;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 100).Value = item.Branch.Id;
                        cmd.Parameters.Add("@Createdby", SqlDbType.Int).Value = item.CreatedBy;
                        //cmd.Parameters.Add("@AdditionalDetails", SqlDbType.VarChar, 8000).Value = item.AdditionalDetails;

                        result = Convert.ToInt32(cmd.ExecuteNonQuery());

                    }
                }


            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }

            return result;

        }

        public int Update(Product item)
        {
            int result = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Update_Product";                         
                            
                        cmd.Parameters.Add("@Uniqueid", SqlDbType.UniqueIdentifier).Value = item.UniqueId;
                        cmd.Parameters.Add("@Productgroup", SqlDbType.Int).Value = item.ProductGroup.Id;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = item.Name;
                       // cmd.Parameters.Add("@Productcode", SqlDbType.VarChar, 100).Value = item.ProductCode;
                        cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = item.Unit.Id;
                       // cmd.Parameters.Add("@Commoditycode", SqlDbType.VarChar, 50).Value = item.CommodityCode;
                        cmd.Parameters.Add("@Reorderlevel", SqlDbType.Float).Value = item.ReorderLevel;
                        cmd.Parameters.Add("@Purchaseprice", SqlDbType.Float).Value = item.PurchasePrice;
                        cmd.Parameters.Add("@Wholesellerprice", SqlDbType.Float).Value = item.WholesellerPrice;
                        cmd.Parameters.Add("@Retailerprice", SqlDbType.Float).Value = item.RetailerPrice;
                        cmd.Parameters.Add("@Opstock", SqlDbType.Float).Value = item.OpStock;
                        cmd.Parameters.Add("@Vattax", SqlDbType.Float).Value = item.VatTax;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 100).Value = item.Branch.Id;
                        cmd.Parameters.Add("@Modifiedby", SqlDbType.Int).Value = item.ModifiedBy;
                        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = item.Active;
                        result = Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }

            return result;

        }

        #endregion

    }
}
