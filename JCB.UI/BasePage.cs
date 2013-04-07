using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using JCB.Entities;
using System.Web.UI;
using System.Collections.ObjectModel;
using System.Globalization;
using JCB.BAL;

namespace JCB.UI
{
    public class BasePage : System.Web.UI.Page
    {
        string deafultValueField = "UniqueId";
        string deafultTextField = "Name";
        string defaultFirstItem = "-- Select --";


        public int BranchId
        {
            get
            {

                if (ViewState["BranchId"] == null)
                    ViewState["BranchId"] = Guid.NewGuid();
                return (int)ViewState["BranchId"];

            }

        }

        public DateTime ConvertToMMddyyyy(string date)
        {
            //string form1 = string.Empty;
            //string form2 = string.Empty;
            //char splitChar = '/';

            date = date.Replace(".", "/").Replace("-", "/");

            DateTime output = DateTime.MinValue;
            try
            {
                Log.Append("O-I");
                output = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Log.Append(ex);
                Log.Append("O-II");
                output = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);

                try
                {
                    Log.Append("O-III");
                    char[] splitter = { '/' };
                    string[] values = date.Split(splitter);

                    output = new DateTime(Convert.ToInt32(values[1]), Convert.ToInt32(values[0]), Convert.ToInt32(values[2]));
                }
                catch (Exception ex1)
                {
                    Log.Append(ex1);
                }
            }

            finally
            {

            } return output;
        }

        public void PopulateMetadata(ListControl ctlType, List<MetadataType> datas, string textField, string valueField, string defaultItem)
        {

            PopulateListControl<MetadataType>(datas, ctlType, textField, valueField, defaultFirstItem);
        }
        public void PopulateMetadata(ListControl ctlType, List<MetadataType> datas)
        {
            //ctlType.DataSource = datas;
            //ctlType.DataTextField = deafultTextField;
            //ctlType.DataValueField = deafultValueField;
            //ctlType.DataBind();
            //ctlType.Items.Insert(0, defaultFirstItem);

            PopulateListControl<MetadataType>(datas, ctlType, deafultTextField, deafultValueField, defaultFirstItem);
        }
        public void PopulateMetadata(ListControl ctlType, List<User> datas, string textField, string valueField)
        {
            //ctlType.DataSource = datas;
            //ctlType.DataTextField = (textField == string.Empty) ? deafultTextField : textField;
            //ctlType.DataValueField = (valueField == string.Empty) ? deafultValueField : valueField;
            //ctlType.DataBind();
            //ctlType.Items.Insert(0, defaultFirstItem);

            PopulateListControl<User>(datas, ctlType,
                (textField == string.Empty) ? deafultTextField : textField,
                (valueField == string.Empty) ? deafultValueField : valueField, defaultFirstItem);
        }

        public void PopulateMetadata(ListControl ctlType, List<Product> items)
        {
            //ctlType.DataSource = items;//  datas.FindAll(delegate(MetaData d) { return d.Type == type; });
            //ctlType.DataTextField = deafultTextField;
            //ctlType.DataValueField = deafultValueField;
            //ctlType.DataBind();
            //ctlType.Items.Insert(0, defaultFirstItem);

            PopulateListControl<Product>(items, ctlType,
                deafultTextField,
                 deafultValueField, defaultFirstItem);

        }
        public void PopulateMetadata(ListControl ctlType, Enumerations.MetadataType type, List<MetaData> datas)
        {
            //ctlType.DataSource = datas.FindAll(delegate(MetaData d) { return d.Type == type; });


            //ctlType.DataTextField = deafultTextField;
            //ctlType.DataValueField = deafultValueField;
            //ctlType.DataBind();
            //ctlType.Items.Insert(0, defaultFirstItem);

            PopulateListControl<MetaData>(datas.FindAll(delegate(MetaData d) { return d.Type == type; }), ctlType,
               deafultTextField,
                deafultValueField, defaultFirstItem);
        }


        public void PopulateMetadata(ListControl ctlType, List<MetaData> datas, Enumerations.MetadataType type, string textField, string valueField, string defaultItem)
        {
            //ctlType.DataSource = datas;
            //ctlType.DataTextField = (textField == string.Empty) ? deafultTextField : textField;
            //ctlType.DataValueField = (valueField == string.Empty) ? deafultValueField : valueField;
            //ctlType.DataBind();
            //ctlType.Items.Insert(0, defaultItem);


            PopulateListControl<MetaData>(datas.FindAll(delegate(MetaData d) { return d.Type == type; }),
                ctlType,
                (textField == string.Empty) ? deafultTextField : textField,
                (valueField == string.Empty) ? deafultValueField : valueField,
                defaultFirstItem);
        }

        public void PopulateMetadata(ListControl ctlType, List<MetaData> datas, string textField, string valueField, string defaultItem)
        {
            //ctlType.DataSource = datas;
            //ctlType.DataTextField = (textField == string.Empty) ? deafultTextField : textField;
            //ctlType.DataValueField = (valueField == string.Empty) ? deafultValueField : valueField;
            //ctlType.DataBind();
            //ctlType.Items.Insert(0, defaultItem);


            PopulateListControl<MetaData>(datas, ctlType,
                (textField == string.Empty) ? deafultTextField : textField,
                (valueField == string.Empty) ? deafultValueField : valueField,
                defaultFirstItem);
        }
        public void PopulateMetadata(ListControl ctlType, List<MetaData> datas)
        {
            PopulateListControl<MetaData>(datas, ctlType,
                 deafultTextField,
                deafultValueField,
                defaultFirstItem);
        }



        public void PopulateListControl<T>(List<T> datas, ListControl ctlType, string textField, string valueField, string defaultItem)
        {
            ctlType.DataSource = datas;
            ctlType.DataTextField = (textField == string.Empty) ? deafultTextField : textField;
            ctlType.DataValueField = (valueField == string.Empty) ? deafultValueField : valueField;
            ctlType.DataBind();
            ctlType.Items.Insert(0, defaultItem);
        }


        public List<MetaData> GetMetadata(Enumerations.MetadataType type, List<MetaData> datas)
        {
            return datas.FindAll(delegate(MetaData d) { return d.Type == type && d.Name != "All"; });

        }



        public void PreSelect(ListControl ctlType, string value)
        {
            ctlType.ClearSelection();
            if (ctlType.Items.FindByValue(value) != null)
                ctlType.Items.FindByValue(value).Selected = true;
            if (ctlType.Items.FindByValue(value.ToLower()) != null)
                ctlType.Items.FindByValue(value.ToLower()).Selected = true;

        }


        public void RemoveItemByText(ListControl ctlType, string value)
        {
            ListItem item = null;
            if (ctlType.Items.FindByText(value) != null)
                item = ctlType.Items.FindByText(value);
            if (ctlType.Items.FindByText(value.ToLower()) != null)
                item = ctlType.Items.FindByText(value.ToLower());
            if (item != null)
                ctlType.Items.Remove(item);

        }


        public void PreSelectByText(ListControl ctlType, string value)
        {
            ctlType.ClearSelection();
            if (ctlType.Items.FindByText(value) != null)
                ctlType.Items.FindByText(value).Selected = true;
            if (ctlType.Items.FindByText(value.ToLower()) != null)
                ctlType.Items.FindByText(value.ToLower()).Selected = true;

        }
        public void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Key", "alert('" + message.Replace("'", "\\'") + "');", true);
        }
        public void ShowAlert(string message, string url)
        {

            Label lblmessage = new Label();
            lblmessage.Text = "<script language='javascript'>" + Environment.NewLine + string.Format("alert('{0}'); window.location ='{1}'",
                message.Replace("'", "\\'"), url) + "</script>";
            Page.Controls.Add(lblmessage);

        }

        public string SubmitPrint(string message, string url, string redirect)
        {


            return string.Format("alert('{0}');PrintForm('{1}','{2}');", message.Replace("'", "\\'"), url, redirect);
            //Page.Controls.Add(lblmessage);\
            // ScriptManager.RegisterStartupScript(panel.ID, panel.GetType(), "CloseWindow", lblmessage.Text, true);
            //ScriptManager.RegisterStartupScript("key", lblmessage.Text);

        }

        public void PrintRedirect(Control ctrl, string url)
        {
            Session["PrintControl"] = ctrl;
            Response.Redirect("~/PrintPage.aspx?RedirectUrl=" + url);
            //ClientScript.RegisterStartupScript(this.GetType(),
            //    "onclick",
            //    string.Format("<script language=javascript>window.open('PrintPage.aspx','PrintMe','height=0px,width=0px,scrollbars=0');window.location ='{0}';</script>",
            //     url));
        }

        public void Print(Control ctrl)
        {
            Session["PrintControl"] = ctrl;
            //ClientScript.RegisterStartupScript(this.GetType(),
            //    "onclick",
            //    "<script language=javascript>window.open('Print.aspx','PrintMe','height=0px,width=0px,scrollbars=0');</script>"
            //     );
        }


        //public static void ShowAlert(string message, string redirect)
        //{
        //    // Cleans the message to allow single quotation marks
        //    string cleanMessage = message.Replace("'", "\\'");
        //    string script = string.Format("<script type=\"text/javascript\">alert('{0}'); window.location.href='{1}</script>", cleanMessage, redirect);

        //    // Gets the executing web page
        //    Page page = HttpContext.Current.CurrentHandler as Page;

        //    // Checks if the handler is a Page and that the script isn't allready on the Page
        //    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        //    {
        //        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script);
        //    }
        //}
        //public static void ShowAlert(string message)
        //{
        //    // Cleans the message to allow single quotation marks
        //    string cleanMessage = message.Replace("'", "\\'");
        //    string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

        //    // Gets the executing web page
        //    Page page = HttpContext.Current.CurrentHandler as Page;

        //    // Checks if the handler is a Page and that the script isn't allready on the Page
        //    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        //    {
        //        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script);
        //    }
        //}

    }




    // 
    public class BaseMaster : System.Web.UI.MasterPage
    {
        string deafultValueField = "UniqueId";
        string deafultTextField = "Name";
        string defaultFirstItem = "-- Select --";

        public int BranchId
        {
            get
            {

                if (ViewState["BranchId"] == null)
                    ViewState["BranchId"] = Guid.NewGuid();
                return (int)ViewState["BranchId"];

            }

        }

        public DateTime ConvertToMMddyyyy(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy", null);
        }
        public void PopulateMetadata(ListControl ctlType, List<MetadataType> datas)
        {
            ctlType.DataSource = datas;
            ctlType.DataTextField = deafultTextField;
            ctlType.DataValueField = deafultValueField;
            ctlType.DataBind();
            ctlType.Items.Insert(0, defaultFirstItem);
        }
        public void PopulateMetadata(ListControl ctlType, List<User> datas, string textField, string valueField)
        {
            ctlType.DataSource = datas;
            ctlType.DataTextField = (textField == string.Empty) ? deafultTextField : textField;
            ctlType.DataValueField = (valueField == string.Empty) ? deafultValueField : valueField;
            ctlType.DataBind();
            ctlType.Items.Insert(0, defaultFirstItem);
        }

        public void PopulateMetadata(ListControl ctlType, List<MetaData> datas, string textField, string valueField)
        {
            ctlType.DataSource = datas;
            ctlType.DataTextField = (textField == string.Empty) ? deafultTextField : textField;
            ctlType.DataValueField = (valueField == string.Empty) ? deafultValueField : valueField;
            ctlType.DataBind();
            ctlType.Items.Insert(0, defaultFirstItem);
        }

        public void PopulateMetadata(ListControl ctlType, List<Product> items)
        {
            ctlType.DataSource = items;//  datas.FindAll(delegate(MetaData d) { return d.Type == type; });
            ctlType.DataTextField = deafultTextField;
            ctlType.DataValueField = deafultValueField;
            ctlType.DataBind();
            ctlType.Items.Insert(0, defaultFirstItem);
        }
        public void PopulateMetadata(ListControl ctlType, Enumerations.MetadataType type, List<MetaData> datas)
        {
            ctlType.DataSource = datas.FindAll(delegate(MetaData d) { return d.Type == type; });
            ctlType.DataTextField = deafultTextField;
            ctlType.DataValueField = deafultValueField;
            ctlType.DataBind();
            ctlType.Items.Insert(0, defaultFirstItem);


        }



        public List<MetaData> GetMetadata(Enumerations.MetadataType type, List<MetaData> datas)
        {
            return datas.FindAll(delegate(MetaData d) { return d.Type == type && d.Name != "All"; });

        }



        public void PreSelect(ListControl ctlType, string value)
        {
            ctlType.ClearSelection();
            if (ctlType.Items.FindByValue(value) != null)
                ctlType.Items.FindByValue(value).Selected = true;
            if (ctlType.Items.FindByValue(value.ToLower()) != null)
                ctlType.Items.FindByValue(value.ToLower()).Selected = true;

        }


        public void RemoveItemByText(ListControl ctlType, string value)
        {
            ListItem item = null;
            if (ctlType.Items.FindByText(value) != null)
                item = ctlType.Items.FindByText(value);
            if (ctlType.Items.FindByText(value.ToLower()) != null)
                item = ctlType.Items.FindByText(value.ToLower());
            if (item != null)
                ctlType.Items.Remove(item);

        }


        public void PreSelectByText(ListControl ctlType, string value)
        {
            ctlType.ClearSelection();
            if (ctlType.Items.FindByText(value) != null)
                ctlType.Items.FindByText(value).Selected = true;
            if (ctlType.Items.FindByText(value.ToLower()) != null)
                ctlType.Items.FindByText(value.ToLower()).Selected = true;

        }
        public void ShowAlert(string message)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Key", "alert('" + message.Replace("'", "\\'") + "');", true);
        }
        public void ShowAlert(string message, string url)
        {

            Label lblmessage = new Label();
            lblmessage.Text = "<script language='javascript'>" + Environment.NewLine + string.Format("alert('{0}'); window.location ='{1}'", message.Replace("'", "\\'"), url) + "</script>";
            Page.Controls.Add(lblmessage);

        }

        //public static void ShowAlert(string message, string redirect)
        //{
        //    // Cleans the message to allow single quotation marks
        //    string cleanMessage = message.Replace("'", "\\'");
        //    string script = string.Format("<script type=\"text/javascript\">alert('{0}'); window.location.href='{1}</script>", cleanMessage, redirect);

        //    // Gets the executing web page
        //    Page page = HttpContext.Current.CurrentHandler as Page;

        //    // Checks if the handler is a Page and that the script isn't allready on the Page
        //    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        //    {
        //        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script);
        //    }
        //}
        //public static void ShowAlert(string message)
        //{
        //    // Cleans the message to allow single quotation marks
        //    string cleanMessage = message.Replace("'", "\\'");
        //    string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

        //    // Gets the executing web page
        //    Page page = HttpContext.Current.CurrentHandler as Page;

        //    // Checks if the handler is a Page and that the script isn't allready on the Page
        //    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        //    {
        //        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script);
        //    }
        //}

    }
}