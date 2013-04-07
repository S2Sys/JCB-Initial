using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace JCB.Entities
{
    /// <summary>
    /// Enum of the regions
    /// </summary>
    public enum Region
    {
        [Description("All")]
        AllRegions = 0,
        [Description("Gauteng")]
        Gauteng = 1,
     
    }
    public static class EnumExtensionManager 
    {

        //http://heathesh.com/post/2010/05/24/Reading-the-DescriptionAttribute-of-enumerators-and-returning-the-corresponding-enum-based-on-the-description.aspx

        //        for (CarColour cc = CarColour.White; cc <= CarColour.Red; cc++)
        //{
        //    Console.WriteLine(cc.Description());
        //}

         
        //Region regionEnumerator = Region.KwaZuluNatal;
        //string regionDescription = regionEnumerator.GetDescription();
        /// <summary>
        /// Gets the description of an enumerator
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumerator)
        {
            //get the enumerator type
            Type type = enumerator.GetType();

            //get the member info
            MemberInfo[] memberInfo = type.GetMember(enumerator.ToString());

            //if there is member information
            if (memberInfo != null && memberInfo.Length > 0)
            {
                //we default to the first member info, as it's for the specific enum value
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                //return the description if it's found
                if (attributes != null && attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }

            //if there's no description, return the string value of the enum
            return enumerator.ToString();
        }


        //string regionDescription = "Northern Cape";
        //Region regionEnumerator = regionDescription.GetEnumFromDescription<Region>();

        /// <summary>
        /// Gets the enumerator from the description passed in
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumFromDescription<T>(this string description)
        {
            //get the member info of the enum
            MemberInfo[] memberInfos = typeof(T).GetMembers();

            if (memberInfos != null && memberInfos.Length > 0)
            {
                //loop through the member info classes
                foreach (MemberInfo memberInfo in memberInfos)
                {
                    //get the custom attributes of the member info
                    object[] attributes = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    //if there are attributes
                    if (attributes != null && attributes.Length > 0)
                        //if the description attribute is equal to the description, return the enum
                        if (((DescriptionAttribute)attributes[0]).Description.ToUpper() == description.ToUpper())
                            return (T)Enum.Parse(typeof(T), memberInfo.Name);
                }
            }

            //this means the enum was not found from the description, so return the default
            return default(T);
        }
    }

    public class Helper
    {

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetEnumDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }
    }
}
