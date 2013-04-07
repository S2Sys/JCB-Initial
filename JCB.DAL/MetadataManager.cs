using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using System.Data;
using System.Data.SqlClient;

namespace JCB.DAL
{
    public class MetadataManager : DataAccessBase
    {
        public int UpdateItemStatus(string name, int stat, int modifiedby)
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
                        cmd.CommandText = "UpdateItemStatus";
                        cmd.Parameters.Add("@UniqueName", SqlDbType.VarChar, 200).Value = name;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 100).Value = stat;
                        cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100).Value = modifiedby;
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
        public int UpdateTransationStatus(Guid transactionId, int stat, int modifiedby)
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
                        cmd.CommandText = "UpdateTransationStatus";
                        cmd.Parameters.Add("@transactionId", SqlDbType.UniqueIdentifier).Value = transactionId;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 100).Value = stat;
                        cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100).Value = modifiedby;
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





        public List<MetaData> GetMetaDatasByBranch(int branchId)
        {
            List<MetaData> items = new List<MetaData>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_Groups_By_Branch";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new MetaData()
                                {

                                    Id = CastInt(reader["ProductGroupId"]),
                                    UniqueId = CastGuid(reader["ProductGroupUniqueid"]),
                                    Name = CastString(reader["ProductGroupName"]),
                                   


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

        public List<MetaData> GetMetaDatas(int branchId)
        {
            List<MetaData> items = new List<MetaData>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_MetaDatas";
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new MetaData()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    Name = CastString(reader["Name"]),
                                    Description = CastString(reader["Description"]),
                                    Type = (Enumerations.MetadataType)CastInt(reader["Metadatatype"]),
                                    //BranchId = CastInt(reader["BranchId"]),
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

        public List<MetadataType> GetMetaDataTypes()
        {
            List<MetadataType> items = new List<MetadataType>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_MetadataTypes";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new MetadataType()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["UniqueId"]),
                                    Name = CastString(reader["Name"]),
                                    Description = CastString(reader["Description"])
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

        public List<MetaData> GetUserTypes()
        {
            List<MetaData> items = new List<MetaData>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GetUserTypes";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new MetaData()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    Name = CastString(reader["Name"]),
                                    Description = CastString(reader["Description"]),
                                    Type = (Enumerations.MetadataType)CastInt(reader["Metadatatype"]),
                                    //BranchId = CastInt(reader["BranchId"]),
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


        public List<MetaData> GetMetaDatas(Enumerations.MetadataType type, int branchId)
        {
            List<MetaData> items = new List<MetaData>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Get_MetaDataById";
                        cmd.Parameters.Add("@TypeId", SqlDbType.Int).Value = (int)type;
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = (int)branchId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new MetaData()
                                {

                                    Id = CastInt(reader["Id"]),
                                    UniqueId = CastGuid(reader["Uniqueid"]),
                                    Name = CastString(reader["Name"]),
                                    Description = CastString(reader["Description"]),
                                    Type = (Enumerations.MetadataType)CastInt(reader["Metadatatype"]),

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

        public int Insert(MetaData item)
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
                        cmd.CommandText = "Insert_MetaData";
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = item.Name;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = item.Description;
                        cmd.Parameters.Add("@MetadataType", SqlDbType.Int).Value = (int)item.Type;
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 100).Value = item.Branch.Id;
                        cmd.Parameters.Add("@Createdby", SqlDbType.Int).Value = item.CreatedBy;

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

        public int Update(MetaData item)
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
                        cmd.CommandText = "Update_MetaData";

                        cmd.Parameters.Add("@Uniqueid", SqlDbType.UniqueIdentifier).Value = item.UniqueId;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = item.Name;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = item.Description;
                        cmd.Parameters.Add("@Metadatatype", SqlDbType.Int).Value = (int)item.Type;

                        //cmd.Parameters.Add("@BranchId", SqlDbType.VarChar, 100).Value = item.Branch.Id;
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

    }
}
