using System;
using System.Configuration;

namespace JCB.DAL
{
    public class DataAccessBase
    {
        // public static string ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected string CastString(object column)
        {
            return (DBNull.Value == column) ? string.Empty : Convert.ToString(column).Trim();
        }
        protected Guid CastGuid(object column)
        {
            return (DBNull.Value == column) ? Guid.Empty : new Guid(Convert.ToString(column));
        }
        protected int? CastNullableInt(object column)
        {
            return (DBNull.Value == column) ? (int?)null : Convert.ToInt32(column);
        }
        protected int CastInt(object column)
        {
            return (DBNull.Value == column) ? 0 : Convert.ToInt32(column);
        }
        protected bool? CastNullableBoolean(object column)
        {
            return (DBNull.Value == column) ? (bool?)null : Convert.ToBoolean(column);
        }
        protected bool CastBoolean(object column)
        {
            return (DBNull.Value == column) ? false : Convert.ToBoolean(column);
        }


        protected DateTime? CastNullableDateTime(object column)
        {
            try
            {
                return (DBNull.Value == column) ? (DateTime?)null : Convert.ToDateTime(column);
            }
            catch
            {
                return (DBNull.Value == column) ? (DateTime?)null : DateTime.Parse(Convert.ToString(column));

            }
        }
        protected DateTime CastDateTime(object column)
        {
            try
            {
                return (DBNull.Value == column) ? DateTime.MinValue : Convert.ToDateTime(column);
            }
            catch
            {
                return (DBNull.Value == column) ? DateTime.MinValue : DateTime.Parse(Convert.ToString(column));
                
            }
        }
        protected double CastDouble(object column)
        {
            return (DBNull.Value == column) ? 0 : Convert.ToDouble(column);
        }

        protected float CastFloat(object column)
        {
            return (DBNull.Value == column) ? (float)0 : (float)Math.Round(Convert.ToDouble(column), 2);
        }

    }

}