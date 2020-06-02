using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitaires;

namespace Website2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Snippets snippets = new Snippets();
                Session["UpdateInfoHits"] = snippets.UpdateInfoHits((DataSet)Session["UpdateInfoHits"], 6, 42);
            }
        }
        public void button(object sender, EventArgs e)
        {
            //Session.Clear();
            Session.Abandon();
           Response.Redirect("Default.aspx");
        }
    }
}