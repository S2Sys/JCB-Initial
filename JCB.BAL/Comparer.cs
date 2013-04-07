using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace JCB.BAL
{
    public static class Comparer
    {
        public enum CustomSortDirection { Asc, Desc }
        public static List<T> Sort<T>(List<T> listToBeSorted, string sortBasePropertyName, CustomSortDirection sortDirection)
        {
            IOrderedEnumerable<T> result;

            if (!string.IsNullOrEmpty(sortBasePropertyName))
            {
                if (sortDirection == CustomSortDirection.Asc)
                {
                    result = from i in listToBeSorted
                             orderby GetSortBaseProperty(i, sortBasePropertyName) ascending
                             select i;
                }
                else
                {
                    result = from i in listToBeSorted
                             orderby GetSortBaseProperty(i, sortBasePropertyName) descending
                             select i;
                }
                return result.ToList();
            }
            else
            {
                return listToBeSorted;
            }
        }


        public static object GetSortBaseProperty(object o, string SortBasePropertyName)
        {
            if (SortBasePropertyName.Contains('.'))
            {
                int dotIndex = SortBasePropertyName.IndexOf(".") + 1;
                object top1Prop = o.GetType().InvokeMember(SortBasePropertyName.Split('.')[0], BindingFlags.GetProperty, null, o, null);
                return GetSortBaseProperty(top1Prop, SortBasePropertyName.Substring(dotIndex, SortBasePropertyName.Length - dotIndex));
            }
            else
            {
                return o.GetType().InvokeMember(SortBasePropertyName, BindingFlags.GetProperty, null, o, null);
            }
        }


    }
  

   
}
