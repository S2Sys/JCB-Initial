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
    public class ReportController
    {

        ReportManager db = new ReportManager();


        public List<ReorderLevelReport> GetReorderLevelReport(int branchId, DateTime sd, DateTime ed, string key)
        {
            return db.GetReorderLevelReport(branchId);
        }

        public List<StockReport> GetStockReport(int branchId, DateTime sd, DateTime ed, string key)
        {
            return db.GetStockReport(branchId);
        }
        public List<BalanceReport> GetBalanceReport(int userId, DateTime sd, DateTime ed)
        {
            return db.GetBalanceReport(userId, sd, ed);
        }

    }
}
