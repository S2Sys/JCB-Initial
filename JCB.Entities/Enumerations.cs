using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace JCB.Enumerations
{
    public enum DiscountType
    {
        [Description("%")]
        Percentage,
        [Description("RS")]
        Rupees
    }

    public enum ItemStatus
    {
        [Description("-1")]
        Delete,
        [Description("0")]
        InActive,
        [Description("1")]
        Active
    }



    public enum UserType
    {
        None,
        //[Description("1CC23AF6-8CB5-41E5-BD1C-6CB27541776D")]
        //Sales_Man = 6,
        [Description("2E7136A8-A322-44F4-BDE1-DA2D43647BF0")]
        Admin = 11,
        [Description("54C62167-24E0-4065-8FF6-367140C72B6E")]
        Branch_User = 24,
        [Description("AC39CD42-92F9-4A2C-ADD8-61EE78A395F9")]
        Customer = 40,
        [Description("D4AA9E6D-1C36-4016-BA3D-297B545B8EA7")]
        Supplier = 48

    }


    public enum MetadataType
    {
        [Description("E67F7932-5E7E-452A-B9DD-9BB87C13CCF2")]
        User_Type = 1,
        [Description("90C253AA-8A3E-4198-93B5-6CC7F517E34E")]
        Unit = 2,
        [Description("48F23095-5885-4883-96B9-84FF566520C2")]
        Product_Group = 3,
        [Description("BA71D2FA-B80F-4C5B-AA4F-729CD39CF968")]
        Transation_Type = 4,
        [Description("E5C86896-4B66-4FA8-82A9-B1DF622EB38F")]
        Payment_Mode = 5,
        [Description("457089DA-CCB1-4CA5-B20E-25F0BBC78177")]
        Branch = 6,
        [Description("A9BBF02D-664D-481F-AD36-8AEA15377070")]
        States = 7,
        [Description("8505D7A9-32A8-456E-9DD5-841139C292E8")]
        VAT,

        None
    }

    public enum TransactionType
    {
        None,
        [Description("0388CE18-8933-47D4-B4DE-18688C9A2402")]
        Purchase_Request = 2,
        [Description("13B80370-CEE8-4297-9F15-724C60A1A562")]
        Purchase_Return = 5,
        [Description("35BF2478-4E45-4EDC-B220-330606E2175D")]
        Sales_Return = 14,
        [Description("689065B3-DF93-4C1C-96D8-4731E4955A8C")]
        Sales = 30,
        [Description("F0253045-A295-4926-A427-4ACF32CD747A")]
        Purchase = 58,



    }

}
