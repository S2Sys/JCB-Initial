using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JCB.Entities;
using JCB.BAL;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace JCB.UI.Service
{


    //  http://stackoverflow.com/questions/6398171/map-entity-to-json-using-javascriptserializer

    /// <summary>
    /// Summary description for DataManager
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DataManager : System.Web.Services.WebService
    {

        ProductController pc = new ProductController();
        [WebMethod]
        public Product GetProduct(string id)
        {
            return pc.GetProduct(new Guid(id));
            //MemoryStream stream = new MemoryStream();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Product));
            //ser.WriteObject(stream, pc.GetProduct(new Guid(id)));
            //return Encoding.ASCII.GetString(stream.ToArray());

        }


        [WebMethod(EnableSession = true)]
        public List<Transaction> CartItems(Transaction newTrans)
        {
            List<Transaction> items = new List<Transaction>();
            if (Session["CartItems"] != null)
            {
                items = (List<Transaction>)Session["CartItems"];
            }

            Transaction inserted = newTrans;
            items.Add(inserted);
            return items;
            //return DateTime.Now.ToString();
        }

        [WebMethod]
        public string RenderUserControl(string controlName)
        {
            return RenderControl(controlName);
        }

        public string RenderControl(string controlName)
        {
            Page page = new Page();
            UserControl userControl = (UserControl)page.LoadControl(controlName);
            userControl.EnableViewState = false;
            HtmlForm form = new HtmlForm();
            form.Controls.Add(userControl);
            page.Controls.Add(form);

            StringWriter textWriter = new StringWriter();
            HttpContext.Current.Server.Execute(page, textWriter, false);
            return textWriter.ToString();
        }
    }
}
