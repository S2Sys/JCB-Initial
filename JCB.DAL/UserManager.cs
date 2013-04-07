using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using System.Data.SqlClient;
using System.Data;

namespace JCB.DAL
{
    public class UserManager : DataAccessBase
    {
        #region Users

        public List<User> GetUsers(int branchId)
        {
            List<User> items = new List<User>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_Users";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new User()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    Name = CastString(reader["Name"]),
                                    Username = CastString(reader["Username"]),
                                    Password = CastString(reader["Password"]),
                                    Type = new MetaData()
                                    {
                                        //BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["UserTypeName"]),
                                        Id = CastInt(reader["UserTypeId"]),
                                        UniqueId = CastGuid(reader["UserTypeUniqueId"])

                                    },
                                    // Type = (Enumerations.UserType)CastInt(reader["Usertype"]),
                                    Address = CastString(reader["Address"]),
                                    City = CastString(reader["City"]),
                                    State = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["StateName"]),
                                        Id = CastInt(reader["StateId"]),
                                        UniqueId = CastGuid(reader["StateUniqueId"])

                                    },
                                    //  State = CastString(reader["State"]),
                                    Phone = CastString(reader["Phone"]),
                                    Mobile = CastString(reader["Mobile"]),
                                    Tin = CastString(reader["Tin"]),
                                    Cst = CastString(reader["Cst"]),
                                    OpeningBalance = CastFloat(reader["Openingbalace"]),
                                    Branch = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    // BranchId = CastInt(reader["BranchId"]),
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

        public User GetUserById(Guid userId)
        {
            User item = new User();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_User_ById";
                        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                item = (new User()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    Name = CastString(reader["Name"]),
                                    Username = CastString(reader["Username"]),
                                    Password = CastString(reader["Password"]),
                                    Type = new MetaData()
                                    {
                                        //BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["UserTypeName"]),
                                        Id = CastInt(reader["UserTypeId"]),
                                        UniqueId = CastGuid(reader["UserTypeUniqueId"])

                                    },
                                    // Type = (Enumerations.UserType)CastInt(reader["Usertype"]),
                                    Address = CastString(reader["Address"]),
                                    City = CastString(reader["City"]),
                                    State = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["StateName"]),
                                        Id = CastInt(reader["StateId"]),
                                        UniqueId = CastGuid(reader["StateUniqueId"])

                                    },
                                    //  State = CastString(reader["State"]),
                                    Phone = CastString(reader["Phone"]),
                                    Mobile = CastString(reader["Mobile"]),
                                    Tin = CastString(reader["Tin"]),
                                    Cst = CastString(reader["Cst"]),
                                    OpeningBalance = CastFloat(reader["Openingbalace"]),
                                    Branch = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    // BranchId = CastInt(reader["BranchId"]),
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


        public User LoginUser(string username, string password)
        {
            User result = null;

            int count = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LoginUser";
                        cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar, 100).Value = username;
                        cmd.Parameters.Add("@PASS", SqlDbType.VarChar, 100).Value = password;
                        //count = (int)cmd.ExecuteScalar();
                        //result = (count == 1) ? true : false;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = (new User()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    Name = CastString(reader["Name"]),
                                    Username = CastString(reader["Username"]),
                                    Password = CastString(reader["Password"]),
                                    Type = new MetaData()
                                    {
                                        //BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["UserTypeName"]),
                                        Id = CastInt(reader["UserTypeId"]),
                                        UniqueId = CastGuid(reader["UserTypeUniqueId"])

                                    },
                                    // Type = (Enumerations.UserType)CastInt(reader["Usertype"]),
                                    Address = CastString(reader["Address"]),
                                    City = CastString(reader["City"]),
                                    State = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["StateName"]),
                                        Id = CastInt(reader["StateId"]),
                                        UniqueId = CastGuid(reader["StateUniqueId"])

                                    },
                                    //  State = CastString(reader["State"]),
                                    Phone = CastString(reader["Phone"]),
                                    Mobile = CastString(reader["Mobile"]),
                                    Tin = CastString(reader["Tin"]),
                                    Cst = CastString(reader["Cst"]),
                                    OpeningBalance = CastFloat(reader["Openingbalace"]),
                                    Branch = new MetaData()
                                    {
                                        // BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["BranchName"]),
                                        Id = CastInt(reader["BranchId"]),
                                        UniqueId = CastGuid(reader["BranchUniqueId"])

                                    },
                                    // BranchId = CastInt(reader["BranchId"]),
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
            return result;

        }

        public List<User> GetUsers(Enumerations.UserType type, int branchId)
        {
            List<User> items = new List<User>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_UsersByType";
                        cmd.Parameters.Add("@Usertype", SqlDbType.Int).Value = (int)type;
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new User()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    Name = CastString(reader["Name"]),
                                    Username = CastString(reader["Username"]),
                                    Password = CastString(reader["Password"]),
                                    //Type = (Enumerations.UserType)CastInt(reader["Usertype"]),
                                    Type = new MetaData()
                                    {
                                        //BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["UserTypeName"]),
                                        Id = CastInt(reader["UserTypeId"]),
                                        UniqueId = CastGuid(reader["UserTypeUniqueId"])

                                    },
                                    Address = CastString(reader["Address"]),
                                    City = CastString(reader["City"]),
                                    State = new MetaData()
                                    {
                                        //BranchId = CastInt(reader["BranchId"]),
                                        Name = CastString(reader["StateName"]),
                                        Id = CastInt(reader["StateId"]),
                                        UniqueId = CastGuid(reader["StateUniqueId"])

                                    },
                                    //  State = CastString(reader["State"]),
                                    Phone = CastString(reader["Phone"]),
                                    Mobile = CastString(reader["Mobile"]),
                                    Tin = CastString(reader["Tin"]),
                                    Cst = CastString(reader["Cst"]),
                                    OpeningBalance = CastFloat(reader["Openingbalace"]),

                                    //BranchId = CastInt(reader["BranchId"]),
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

        //public List<Supplier> GetSuppliers()
        //{
        //    List<Supplier> items = new List<Supplier>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(ConnectionString))
        //        {
        //            con.Open();
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "Get_Suplliers";

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        items.Add(new Supplier()
        //                        {

        //                            Id = CastInt(reader["Id"]),
        //                            UniqueId = CastGuid(reader["Uniqueid"]),
        //                            Name = CastString(reader["Name"]),
        //                            ////Username = Caststring(reader["Username"]),
        //                            // Password = CastString(reader["Password"]),
        //                            //Usertype = CastInt(reader["Usertype"]),
        //                            Address = CastString(reader["Address"]),
        //                            City = CastString(reader["City"]),
        //                            State = CastString(reader["State"]),
        //                            Phone = CastString(reader["Phone"]),
        //                            Mobile = CastString(reader["Mobile"]),
        //                            Tin = CastString(reader["Tin"]),
        //                            Cst = CastString(reader["Cst"]),
        //                            Openingbalace = CastFloat(reader["Openingbalace"]),

        //                            BranchId = CastInt(reader["BranchId"]),

        //                            Uniquename = CastString(reader["Uniquename"]),
        //                            CreatedOn = CastDateTime(reader["Createdon"]),
        //                            CreatedBy = CastInt(reader["Createdby"]),
        //                            ModifiedOn = CastDateTime(reader["Modifiedon"]),
        //                            ModifiedBy = CastInt(reader["Modifiedby"]),
        //                            Active = CastBoolean(reader["Active"])


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

        //public List<Customer> GetCustomers()
        //{
        //    List<Customer> items = new List<Customer>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(ConnectionString))
        //        {
        //            con.Open();
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "Get_Customers";

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        items.Add(new Customer()
        //                        {

        //                            Id = CastInt(reader["Id"]),
        //                            UniqueId = CastGuid(reader["Uniqueid"]),
        //                            Name = CastString(reader["Name"]),
        //                            ////Username = Caststring(reader["Username"]),
        //                            // Password = CastString(reader["Password"]),
        //                            //Usertype = CastInt(reader["Usertype"]),
        //                            Address = CastString(reader["Address"]),
        //                            City = CastString(reader["City"]),
        //                            State = CastString(reader["State"]),
        //                            Phone = CastString(reader["Phone"]),
        //                            Mobile = CastString(reader["Mobile"]),
        //                            //Tin = CastString(reader["Tin"]),
        //                            //Cst = CastString(reader["Cst"]),
        //                            //Openingbalace = CastFloat(reader["Openingbalace"]),

        //                            BranchId = CastInt(reader["BranchId"]),
        //                            Uniquename = CastString(reader["Uniquename"]),
        //                            CreatedOn = CastDateTime(reader["Createdon"]),
        //                            CreatedBy = CastInt(reader["Createdby"]),
        //                            ModifiedOn = CastDateTime(reader["Modifiedon"]),
        //                            ModifiedBy = CastInt(reader["Modifiedby"]),
        //                            Active = CastBoolean(reader["Active"])


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

        //public List<SalesMan> GetSalesMans()
        //{
        //    List<SalesMan> items = new List<SalesMan>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(ConnectionString))
        //        {
        //            con.Open();
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "Get_Suplliers";

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        items.Add(new SalesMan()
        //                        {

        //                            Id = CastInt(reader["Id"]),
        //                            UniqueId = CastGuid(reader["Uniqueid"]),
        //                            Name = CastString(reader["Name"]),
        //                            ////Username = Caststring(reader["Username"]),
        //                            // Password = CastString(reader["Password"]),
        //                            //Usertype = CastInt(reader["Usertype"]),
        //                            Address = CastString(reader["Address"]),
        //                            City = CastString(reader["City"]),
        //                            State = CastString(reader["State"]),
        //                            Phone = CastString(reader["Phone"]),
        //                            Mobile = CastString(reader["Mobile"]),
        //                            //Tin = CastString(reader["Tin"]),
        //                            //Cst = CastString(reader["Cst"]),
        //                            //Openingbalace = CastFloat(reader["Openingbalace"]),

        //                            BranchId = CastInt(reader["BranchId"]),
        //                            Uniquename = CastString(reader["Uniquename"]),
        //                            CreatedOn = CastDateTime(reader["Createdon"]),
        //                            CreatedBy = CastInt(reader["Createdby"]),
        //                            ModifiedOn = CastDateTime(reader["Modifiedon"]),
        //                            ModifiedBy = CastInt(reader["Modifiedby"]),
        //                            Active = CastBoolean(reader["Active"])


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

        public int Insert(User item)
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
                        cmd.CommandText = "Insert_User";


                        //cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.Id;
                        //cmd.Parameters.Add("@Uniqueid", SqlDbType.UniqueIdentifier).Value = item.UniqueId;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = item.Name;
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = item.Username;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = item.Password;
                        cmd.Parameters.Add("@Usertype", SqlDbType.Int).Value = (int)item.Type.Id;
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar, 250).Value = item.Address;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = item.City;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 100).Value = item.State.UniqueId.ToString();
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = item.Phone;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 100).Value = item.Mobile;
                        cmd.Parameters.Add("@Tin", SqlDbType.VarChar, 100).Value = item.Tin;
                        cmd.Parameters.Add("@Cst", SqlDbType.VarChar, 100).Value = item.Cst;
                        cmd.Parameters.Add("@Openingbalace", SqlDbType.Float).Value = item.OpeningBalance;

                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 100).Value = item.Branch.Id;
                        //cmd.Parameters.Add("@Uniquename", SqlDbType.VarChar, 100).Value = item.Uniquename;
                        //cmd.Parameters.Add("@Createdon", SqlDbType.Date).Value = item.CreatedOn;
                        cmd.Parameters.Add("@Createdby", SqlDbType.Int).Value = item.CreatedBy;
                        //cmd.Parameters.Add("@Modifiedon", SqlDbType.Date).Value = item.ModifiedOn;
                        //cmd.Parameters.Add("@Modifiedby", SqlDbType.Int).Value = item.ModifiedBy;
                        //cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = item.Active;

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

        public int Update(User item)
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
                        cmd.CommandText = "Update_User";
                        cmd.Parameters.Add("@Uniqueid", SqlDbType.UniqueIdentifier).Value = item.UniqueId;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = item.Name;
                        cmd.Parameters.Add("@Usertype", SqlDbType.Int).Value = (int)item.Type.Id;
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar, 250).Value = item.Address;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = item.City;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 100).Value = item.State.UniqueId.ToString();
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = item.Phone;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 100).Value = item.Mobile;
                        cmd.Parameters.Add("@Tin", SqlDbType.VarChar, 100).Value = item.Tin;
                        cmd.Parameters.Add("@Cst", SqlDbType.VarChar, 100).Value = item.Cst;
                        cmd.Parameters.Add("@Openingbalace", SqlDbType.Float).Value = item.OpeningBalance;
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


        public int UpdatePassword(Guid id,string password)
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
                        cmd.CommandText = "Update_UserPassword";
                        cmd.Parameters.Add("@Uniqueid", SqlDbType.UniqueIdentifier).Value = id;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = password; 
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
