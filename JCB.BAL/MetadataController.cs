using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using System.Data.SqlClient;
using System.Data;
using JCB.DAL;

namespace JCB.BAL
{
    public class MetadataController
    {

        MetadataManager db = new MetadataManager();


        public int UpdateItemStatus(string name, int stat, int modifiedby)
        {
            return db.UpdateItemStatus(name, stat, modifiedby);
        }

        public int UpdateTransationStatus(Guid transactionId, int stat, int modifiedby)
        {
            return db.UpdateTransationStatus(transactionId, stat, modifiedby);
        }
        public List<MetadataType> GetMetadataTypes()
        {
            return db.GetMetaDataTypes();

        }
        public List<MetaData> GetMetaDatas(int branchId)
        {
            return db.GetMetaDatas(branchId);

        }

        public List<MetaData> GetMetaDatasByBranch(int branchId)
        {
            return db.GetMetaDatasByBranch(branchId);

        }
        
        public List<MetaData> GetUserTypes()
        {
            return db.GetUserTypes();

        }

 
        public List<MetaData> GetMetaDatas(Enumerations.MetadataType type, int branchId)
        {
            return db.GetMetaDatas(type, branchId);

        }

        public int Insert(MetaData item)
        {
            return db.Insert(item);
        }

        public int Update(MetaData item)
        {
            return db.Update(item);
        }
    }
}
