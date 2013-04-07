using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JCB.Entities;
using JCB.Enumerations;

namespace JCB.UI
{
    public class Common
    {

        public static Guid AllBranchesId = new Guid("484B4A4E-5432-4E94-BE9B-A9A6CB375328");
    }

    public class ApplicationContext
    {
        public static User CurrentUser
        {
            get
            {


                return (User)HttpContext.Current.Session["CurrentUser"];
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }
        public static int ShowBranch
        {
            get
            {
                return ((UserType == Enumerations.UserType.Admin && HttpContext.Current.Session["ShowBranch"] != null) ?
                    (int)HttpContext.Current.Session["ShowBranch"] :
                    CurrentUser.Branch.Id);

            }
            set
            {
                HttpContext.Current.Session["ShowBranch"] = value;
            }
        }
        public static UserType UserType
        {
            get
            {
                return CurrentUser.Type.UniqueId.ToString().GetEnumFromDescription<UserType>();
            }

        }
        public static int BranchId
        {
            get
            {
                //return (CurrentUser != null) ?
                //    ((UserType == Enumerations.UserType.Admin) ? 19 : CurrentUser.Branch.Id) :
                //    -1;

                return ShowBranch;
            }

        }

        public static int UserId
        {
            get
            {
                return (CurrentUser != null) ? CurrentUser.Id : -1;
            }

        }

        public static string UserTitle
        {
            get
            {
                return (CurrentUser != null) ? string.Format("{0} [ {1} ]", CurrentUser.Username, CurrentUser.Type.Name) : "Guest";
            }

        }

        public static Guid UserUniqueId
        {
            get
            {
                return (CurrentUser != null) ? CurrentUser.UniqueId : Guid.Empty;
            }

        }
    }
}