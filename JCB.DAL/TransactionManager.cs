using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using System.Data.SqlClient;
using System.Data;
using JCB.Enumerations;
using System.Data.SqlTypes;

namespace JCB.DAL
{
    public class TransactionManager : DataAccessBase
    {

        public int GetBillNumber(TransactionType itemType, int branchId)
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
                        cmd.CommandText = "GetBillno";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchId;
                        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = (int)itemType;
                        result = Convert.ToInt32(cmd.ExecuteScalar());

                    }
                }


            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }

            return result;

        }

        //public List<Transaction> GetReport(TransactionType type, int branchId, DateTime sd, DateTime ed, string keywords)
        //{
        //    List<Transaction> items = new List<Transaction>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(ConnectionString))
        //        {
        //            con.Open();
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "GetReport";
        //                //@StartDate DATETIME, @EndDate DATETIME, @Key VARCHAR(100)
        //                cmd.Parameters.Add("@Type", SqlDbType.UniqueIdentifier).Value = new Guid(type.GetDescription());
        //                cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId;

        //                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = sd;

        //                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = ed;
        //                cmd.Parameters.Add("@Key", SqlDbType.VarChar, 100).Value = keywords;


        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        items.Add(new Transaction()
        //                        {

        //                            ProductId = CastInt(reader["ProductId"]),
        //                            //ReorderLevel = CastString(reader["ReorderLevel"]),
        //                            Unit = CastInt(reader["Unit"]),
        //                            Quantity = CastInt(reader["Quantity"]),
        //                            Rate = CastFloat(reader["Rate"]),
        //                            Type = (TransactionType)CastInt(reader["Type"]),
        //                            Discount = CastFloat(reader["Discount"]),
        //                            DiscountType = (DiscountType)CastInt(reader["DiscountType"]),
        //                            VatTax = CastFloat(reader["VatTax"]),
        //                            Id = CastInt(reader["Id"]),
        //                            UniqueId = CastGuid(reader["UniqueId"]),

        //                            TransactionUniqueId = CastGuid(reader["TransactionUniqueId"]),
        //                            TransactionDate = CastDateTime(reader["TransactionDate"]),
        //                            InvoiceOrBillNo = CastString(reader["InvoiceOrBillNo"]),
        //                            Mode = CastInt(reader["Mode"]),
        //                            UserId = CastInt(reader["UserId"]),
        //                            TransactionBy = new User()
        //                            {
        //                                Id = CastInt(reader["UserId"]),
        //                                Name = CastString(reader["UserTitle"]),
        //                                UniqueId = CastGuid(reader["UserUniqueId"])
        //                            },

        //                            Branch = new MetaData { Id = CastInt(reader["BranchId"]), Name = CastString(reader["BranchName"]) },


        //                            Product = new Product()
        //                            {
        //                                Id = CastInt(reader["ProductId"]),
        //                                UniqueId = CastGuid(reader["ProductUniqueid"]),
        //                                // ProductCode = CastString(reader["ProductProductCode"]),
        //                                Name = CastString(reader["ProductName"]),
        //                                // CommodityCode = CastString(reader["Commoditycode"]),
        //                                ReorderLevel = CastFloat(reader["ProductReorderlevel"]),
        //                                PurchasePrice = CastFloat(reader["Purchaseprice"]),
        //                                WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
        //                                RetailerPrice = CastFloat(reader["Retailerprice"]),
        //                                OpStock = CastFloat(reader["Opstock"]),
        //                                VatTax = CastFloat(reader["ProductVattax"]),
        //                                ProductGroup = new MetaData()
        //                                {
        //                                    Id = CastInt(reader["ProductGroupId"]),
        //                                    UniqueId = CastGuid(reader["ProductGroupUniqueId"]),
        //                                    Name = CastString(reader["ProductGroupName"])
        //                                },
        //                                Unit = new MetaData()
        //                                {
        //                                    Id = CastInt(reader["UnitId"]),
        //                                    UniqueId = CastGuid(reader["UnitUniqueId"]),
        //                                    Name = CastString(reader["UnitName"])
        //                                }

        //                            }
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Append(ex);
        //    }
        //    return items;

        //}

        public List<Transaction> GetCustomPagingReport(TransactionType type, int branchId, DateTime sd, DateTime ed, int userId, int mode,
            int pageIndex, int pageSize, ref int rowCount)
        {
            List<Transaction> items = new List<Transaction>();
            try
            {

                SqlDateTime dateNull = SqlDateTime.Null;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GetCustomPagingReport";
                        //@StartDate DATETIME, @EndDate DATETIME, @Key VARCHAR(100)
                        cmd.Parameters.Add("@Type", SqlDbType.UniqueIdentifier).Value = new Guid(type.GetDescription());
                        cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId;
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = (sd == DateTime.MinValue) ? dateNull : sd;
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = (ed == DateTime.MinValue) ? dateNull : ed;
                        //UserUniqueID = CASE WHEN @UserId =  '00000000-0000-0000-0000-000000000000' THEN UserUniqueID ELSE  @UserId  END
                        // cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = ed;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@ModeId", SqlDbType.Int).Value = mode;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new Transaction()
                                {

                                    ProductId = CastInt(reader["ProductId"]),
                                    //ReorderLevel = CastString(reader["ReorderLevel"]),
                                    Unit = CastInt(reader["Unit"]),
                                    Quantity = CastInt(reader["Quantity"]),
                                    Rate = CastFloat(reader["Rate"]),
                                    Type = (TransactionType)CastInt(reader["Type"]),
                                    Discount = CastFloat(reader["Discount"]),
                                    DiscountType = (DiscountType)CastInt(reader["DiscountType"]),
                                    VatTax = CastFloat(reader["VatTax"]),
                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["UniqueId"]),
                                    Reference = CastString(reader["Reference"]),
                                    TransactionUniqueId = CastGuid(reader["TransactionUniqueId"]),
                                    TransactionDate = CastDateTime(reader["TransactionDate"]),
                                    InvoiceOrBillNo = CastString(reader["InvoiceOrBillNo"]),
                                    Mode = CastInt(reader["Mode"]),
                                    UserId = CastInt(reader["UserId"]),
                                    TransactionBy = new User()
                                    {
                                        Id = CastInt(reader["UserId"]),
                                        Name = CastString(reader["UserTitle"]),
                                        UniqueId = CastGuid(reader["UserUniqueId"])
                                    },

                                    Branch = new MetaData { Id = CastInt(reader["BranchId"]), Name = CastString(reader["BranchName"]) },
                                    ModeData = new MetaData()
                                    {
                                        Id = CastInt(reader["ModeId"]),
                                        UniqueId = CastGuid(reader["ModeUniqueId"]),
                                        Name = CastString(reader["ModeName"])
                                    },
                                    Product = new Product()
                                    {
                                        Id = CastInt(reader["ProductId"]),
                                        UniqueId = CastGuid(reader["ProductUniqueid"]),
                                        // ProductCode = CastString(reader["ProductProductCode"]),
                                        Name = CastString(reader["ProductName"]),
                                        // CommodityCode = CastString(reader["Commoditycode"]),
                                        ReorderLevel = CastFloat(reader["ProductReorderlevel"]),
                                        PurchasePrice = CastFloat(reader["Purchaseprice"]),
                                        WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
                                        RetailerPrice = CastFloat(reader["Retailerprice"]),
                                        OpStock = CastFloat(reader["Opstock"]),
                                        VatTax = CastFloat(reader["ProductVattax"]),
                                        ProductGroup = new MetaData()
                                        {
                                            Id = CastInt(reader["ProductGroupId"]),
                                            UniqueId = CastGuid(reader["ProductGroupUniqueId"]),
                                            Name = CastString(reader["ProductGroupName"])
                                        },
                                        Unit = new MetaData()
                                        {
                                            Id = CastInt(reader["UnitId"]),
                                            UniqueId = CastGuid(reader["UnitUniqueId"]),
                                            Name = CastString(reader["UnitName"])
                                        }

                                    }
                                });
                            }
                        }


                        rowCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Append(ex);
            }
            return items;

        }


        public List<TransactionView> GetTransactionByType(TransactionType type, int branchId)
        {
            List<TransactionView> items = new List<TransactionView>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GET_TransactionView";
                        cmd.Parameters.Add("@Type", SqlDbType.UniqueIdentifier).Value = new Guid(type.GetDescription());
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new TransactionView()
                                {

                                    TransactionUniqueId = CastGuid(reader["TransactionUniqueId"]),
                                    TransactionDate = CastDateTime(reader["TransactionDate"]),
                                    InvoiceOrBillNo = CastString(reader["InvoiceOrBillNo"]),
                                    ModeName = CastString(reader["ModeName"]),
                                    Mode = CastInt(reader["Mode"]),
                                    UserId = CastInt(reader["UserId"]),
                                    TransactionBy = new User()
                                    {
                                        Id = CastInt(reader["UserId"]),
                                        Name = CastString(reader["UserName"]),
                                    },

                                    TotalQuantity = CastFloat(reader["Quantity"]),
                                    TotalPrice = CastFloat(reader["TotalPrice"]),
                                    TotalDiscount = CastFloat(reader["TotalDiscount"]),
                                    TotalVAT = CastFloat(reader["TotalVAT"]),
                                    FinalTotal = CastFloat(reader["FinalTotal"]),
                                    Reference = CastString(reader["Reference"]),
                                    Branch = new MetaData { Id = CastInt(reader["BranchId"]), Name = CastString(reader["BranchName"]) },

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

        public List<Transaction> GetTransactionsById(Guid id)
        {
            List<Transaction> items = new List<Transaction>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GET_Transactions_ById";
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new Transaction()
                                {
                                    TempID = CastGuid(reader["UniqueId"]),
                                    ProductId = CastInt(reader["ProductId"]),
                                    //  ReorderLevel = CastString(reader["ReorderLevel"]),
                                    Unit = CastInt(reader["Unit"]),
                                    Quantity = CastInt(reader["Quantity"]),
                                    Rate = CastFloat(reader["Rate"]),
                                    Type = (TransactionType)CastInt(reader["Type"]),
                                    Discount = CastFloat(reader["Discount"]),
                                    DiscountType = (DiscountType)CastInt(reader["DiscountType"]),
                                    VatTax = CastFloat(reader["VatTax"]),
                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["UniqueId"]),

                                    TransactionUniqueId = CastGuid(reader["TransactionUniqueId"]),
                                    TransactionDate = CastDateTime(reader["TransactionDate"]),
                                    InvoiceOrBillNo = CastString(reader["InvoiceOrBillNo"]),
                                    Mode = CastInt(reader["Mode"]),
                                    UserId = CastInt(reader["UserId"]),
                                    TransactionBy = new User()
                                    {
                                        Id = CastInt(reader["UserId"])//,
                                        //  Name = CastString(reader["UserName"]),
                                    },

                                    Branch = new MetaData { Id = CastInt(reader["BranchId"]), Name = CastString(reader["BranchName"]) },
                                    ModeData = new MetaData { Id = CastInt(reader["ModeId"]), Name = CastString(reader["ModeName"]), UniqueId = CastGuid(reader["ModeUniqueId"]) },
                                    Reference = CastString(reader["Reference"]),
                                    BillName = CastString(reader["BillName"]),
                                    Product = new Product()
                                    {
                                        Id = CastInt(reader["ProductId"]),
                                        UniqueId = CastGuid(reader["ProductUniqueid"]),
                                        //  ProductCode = CastString(reader["ProductProductCode"]),
                                        Name = CastString(reader["ProductName"]),
                                        // CommodityCode = CastString(reader["Commoditycode"]),
                                        //  ReorderLevel = CastFloat(reader["ProductReorderlevel"]),
                                        PurchasePrice = CastFloat(reader["Purchaseprice"]),
                                        WholesellerPrice = CastFloat(reader["Wholesellerprice"]),
                                        RetailerPrice = CastFloat(reader["Retailerprice"]),
                                        OpStock = CastFloat(reader["Opstock"]),
                                        VatTax = CastFloat(reader["ProductVattax"]),
                                        ProductGroup = new MetaData()
                                        {
                                            Id = CastInt(reader["ProductGroupId"]),
                                            UniqueId = CastGuid(reader["ProductGroupUniqueId"]),
                                            Name = CastString(reader["ProductGroupName"])
                                        },
                                        Unit = new MetaData()
                                      {
                                          Id = CastInt(reader["UnitId"]),
                                          UniqueId = CastGuid(reader["UnitUniqueId"]),
                                          Name = CastString(reader["UnitName"])
                                      }

                                    },
                                    RefNo = CastString(reader["RefNo"]),
                                    RefDate = CastString(reader["RefDate"]),
                                    Through = CastString(reader["Through"]),
                                    LRNo = CastString(reader["LRNo"])


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



        public int Insert(Transaction item)
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
                        cmd.CommandText = "Insert_Transaction";

                        //@Purchasedon DATETIME , 
                        //@Invoiceno VARCHAR(15) , 
                        //@Mode VARCHAR(100) , 
                        //@Supplierid INT , @Customerid INT , @Productid VARCHAR(50) , 
                        //@Reorderlevel VARCHAR(100) , 
                        //@Unit FLOAT , @Quantity FLOAT , @Rate FLOAT , @Type INT , 
                        //@Discountper FLOAT , @Discountrs FLOAT , @Vattax FLOAT , 
                        //@branchId int,    @Createdby int	 
                        cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = item.TransactionDate;
                        cmd.Parameters.Add("@InvoiceOrBillNo", SqlDbType.VarChar, 15).Value = item.InvoiceOrBillNo;
                        cmd.Parameters.Add("@Mode", SqlDbType.Int).Value = item.Mode;
                        cmd.Parameters.Add("@Userid", SqlDbType.Int).Value = item.UserId;
                        cmd.Parameters.Add("@TransactionUniqueId", SqlDbType.UniqueIdentifier).Value = item.TransactionUniqueId;
                        cmd.Parameters.Add("@Productid", SqlDbType.Int).Value = item.Product.Id;
                        // cmd.Parameters.Add("@Reorderlevel", SqlDbType.VarChar, 100).Value = item.ReorderLevel;
                        cmd.Parameters.Add("@Unit", SqlDbType.Float).Value = item.Unit;
                        cmd.Parameters.Add("@Quantity", SqlDbType.Float).Value = item.Quantity;
                        cmd.Parameters.Add("@Rate", SqlDbType.Float).Value = item.Rate;
                        cmd.Parameters.Add("@Type", SqlDbType.Int, 3).Value = (int)item.Type;
                        cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = item.Discount;
                        cmd.Parameters.Add("@DiscountType", SqlDbType.Float).Value = (int)item.DiscountType;
                        cmd.Parameters.Add("@Vattax", SqlDbType.Float).Value = item.VatTax;
                        cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = item.TotalPrice;
                        cmd.Parameters.Add("@TotalTax", SqlDbType.Float).Value = item.TotalVAT;
                        cmd.Parameters.Add("@TotalDiscount", SqlDbType.Float).Value = item.TotalDiscount;
                        cmd.Parameters.Add("@FinalPrice", SqlDbType.Float).Value = item.FinalTotal;
                        cmd.Parameters.Add("@branchId", SqlDbType.Int).Value = item.Branch.Id;
                        cmd.Parameters.Add("@Createdby", SqlDbType.Int).Value = item.CreatedBy;
                        cmd.Parameters.Add("@Reference", SqlDbType.VarChar, 100).Value = item.Reference;
                        cmd.Parameters.Add("@BillName", SqlDbType.VarChar, 100).Value = item.BillName;
                        cmd.Parameters.Add("@RefNo", SqlDbType.VarChar, 100).Value = item.RefNo;
                        cmd.Parameters.Add("@RefDate", SqlDbType.VarChar, 100).Value = item.RefDate;
                        cmd.Parameters.Add("@LRNo", SqlDbType.VarChar, 100).Value = item.LRNo;
                        cmd.Parameters.Add("@Through", SqlDbType.VarChar, 100).Value = item.Through;
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

        public int Update(Transaction item)
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
                        cmd.CommandText = "Update_Transaction";

                        cmd.Parameters.Add("@Uniqueid", SqlDbType.UniqueIdentifier).Value = item.UniqueId;
                        cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = item.TransactionDate;
                        cmd.Parameters.Add("@InvoiceOrBillNo", SqlDbType.VarChar, 15).Value = item.InvoiceOrBillNo;
                        cmd.Parameters.Add("@Mode", SqlDbType.Int).Value = item.Mode;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = item.UserId;
                        //cmd.Parameters.Add("@TransactionUniqueId", SqlDbType.UniqueIdentifier).Value = item.TransactionUniqueId;
                        cmd.Parameters.Add("@Productid", SqlDbType.Int).Value = item.ProductId;
                        cmd.Parameters.Add("@Unit", SqlDbType.Float).Value = item.Unit;
                        cmd.Parameters.Add("@Quantity", SqlDbType.Float).Value = item.Quantity;
                        cmd.Parameters.Add("@Rate", SqlDbType.Float).Value = item.Rate;
                        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = (int)item.Type;
                        cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = item.Discount;
                        cmd.Parameters.Add("@DiscountType", SqlDbType.Float).Value = (int)item.DiscountType;
                        cmd.Parameters.Add("@Vattax", SqlDbType.Float).Value = item.VatTax;
                        cmd.Parameters.Add("@Modifiedby", SqlDbType.Int).Value = item.ModifiedBy;
                        cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = item.Active;
                        cmd.Parameters.Add("@Reference", SqlDbType.VarChar, 100).Value = item.Reference;
                        cmd.Parameters.Add("@BillName", SqlDbType.VarChar, 100).Value = item.BillName;
                        cmd.Parameters.Add("@TotalPrice", SqlDbType.Float).Value = item.TotalPrice;
                        cmd.Parameters.Add("@TotalTax", SqlDbType.Float).Value = item.TotalVAT;
                        cmd.Parameters.Add("@TotalDiscount", SqlDbType.Float).Value = item.TotalDiscount;
                        cmd.Parameters.Add("@FinalPrice", SqlDbType.Float).Value = item.FinalTotal;
                        cmd.Parameters.Add("@RefNo", SqlDbType.VarChar, 100).Value = item.RefNo;
                        cmd.Parameters.Add("@RefDate", SqlDbType.VarChar, 100).Value = item.RefDate;
                        cmd.Parameters.Add("@LRNo", SqlDbType.VarChar, 100).Value = item.LRNo;
                        cmd.Parameters.Add("@Through", SqlDbType.VarChar, 100).Value = item.Through;


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
    }
}
