using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using System.Data.SqlClient;
using System.Data;

namespace JCB.DAL
{

    public class ReportManager : DataAccessBase
    {
        public List<ReorderLevelReport> GetReorderLevelReport(int branchId)
        {
            List<ReorderLevelReport> items = new List<ReorderLevelReport>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Report_ReOrderLevel";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new ReorderLevelReport()
                                {

                                    Branch = new MetaData()
                                    {
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                    },
                                    Product = new Product()
                                    {
                                        Id = CastInt(reader["ProductId"]),
                                        UniqueId = CastGuid(reader["ProductUniqueid"]),
                                        OpStock = CastFloat(reader["OpStock"]),
                                        ProductGroup = new MetaData()
                                        {
                                            Name = CastString(reader["ProductGroupName"]),
                                            Id = CastInt(reader["ProductGroupId"]),
                                            UniqueId = CastGuid(reader["ProductGroupUniqueId"])
                                        },
                                        Name = CastString(reader["ProductName"]),
                                        // ProductCode = CastString(reader["ProductProductCode"]),
                                        //CommodityCode = CastString(reader["Commoditycode"]),
                                        ReorderLevel = CastFloat(reader["ProductReorderlevel"]),
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
            return items;

        }

        public List<BalanceReport> GetBalanceReport(int userId, DateTime sd, DateTime ed)
        {
            List<BalanceReport> items = new List<BalanceReport>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GET_BalanceReport";
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)userId;
                        cmd.Parameters.Add("@SD", SqlDbType.DateTime).Value = sd;
                        cmd.Parameters.Add("@ED", SqlDbType.DateTime).Value = ed;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new BalanceReport()
                                {
                                    Branch = new MetaData()
                                    {

                                        Name = CastString(reader["BranchName"]),

                                    },
                                    Title = CastString(reader["UserTitle"]),

                                    TransactionDate = CastDateTime(reader["TransactionDate"]),
                                    Debit = CastFloat(reader["Debit"]),
                                    Credit = CastFloat(reader["Credit"])

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

        public List<StockReport> GetStockReport(int branchId)
        {
            List<StockReport> items = new List<StockReport>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Report_Stock";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new StockReport()
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

                                    Sales = CastFloat(reader["SalesQuantity"]),
                                    SalesReturn = CastFloat(reader["SalesReturnQuantity"]),

                                    Purchase = CastFloat(reader["PurchaseQuantity"]),
                                    PurchaseReturn = CastFloat(reader["PurchaseReturnQuantity"]),

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
            return items;

        }





    }
}
