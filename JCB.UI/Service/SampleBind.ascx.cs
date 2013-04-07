using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JCB.UI.Service
{
    public partial class SampleBind : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = getFruitList();
            GridView1.DataBind();
        }
        public List<Fruit> getFruitList()
        {
            List<Fruit> Obj = new List<Fruit>(){
           new Fruit{ Name="Apple",  Price=20, Qantity=100},
           new Fruit{ Name="Banana",  Price=25, Qantity=1500},
           new Fruit{ Name="Manago",  Price=40, Qantity=400},
           new Fruit{ Name="Orange",  Price=60, Qantity=300},
       };
            return Obj;
        }
        public class Fruit
        {
            public string Name { get; set; }
            public int Qantity { get; set; }
            public double? Price { get; set; }
        }
    }
}