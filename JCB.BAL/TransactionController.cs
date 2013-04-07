using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;
using JCB.DAL;
using JCB.Enumerations;

namespace JCB.BAL
{
    public class TransactionController
    {
        TransactionManager db = new TransactionManager();

        public int GetBillNumber(TransactionType itemType, int branchId)
        {
            return db.GetBillNumber(itemType, branchId);
        }
        //public List<Transaction> GetReport(TransactionType type, int branchId, DateTime sd, DateTime ed, string keywords)
        //{
        //    return db.GetReport(type, branchId, sd, ed, keywords);
        //}

        public List<Transaction> GetCustomPagingReport(TransactionType type, int branchId, DateTime sd, DateTime ed, int userId, int mode,
            int pageIndex, int pageSize, ref int rowCount)
        {
            return db.GetCustomPagingReport(type, branchId, sd, ed, userId, mode, pageIndex, pageSize, ref rowCount);
        }



        public List<TransactionView> GetTransactions(TransactionType type, int branchId)
        {
            return db.GetTransactionByType(type, branchId);
        }

        public int Insert(Transaction item)
        {
            return db.Insert(item);
        }

        public int Update(Transaction item)
        {
            return db.Update(item);
        }

        public List<Transaction> GetTransactionsById(Guid id)
        {
            return db.GetTransactionsById(id);
        }
    }
}
