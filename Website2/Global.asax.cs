using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Utilitaires;

namespace Website2
{
    public class Global : HttpApplication
    {
        private string dsn = ConfigurationManager.ConnectionStrings["dsn"].ConnectionString;
        void Application_Start(object sender, EventArgs e)
        {
            // Code qui s’exécute au démarrage de l’application
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            //Charge dataset avec tables vides
            //récupère l'id de user
            
            Snippets snippets = new Snippets();
            DataSet dataSet = new DataSet();


            DBQuery DBQuery=new DBQuery();
        //démarrage de la session :création du dataset avec les tables vides au démarrage de la session du user


        DataTables dataTable = new DataTables(dsn);
            dataSet.Tables.Add(dataTable.ChargeTable(DBQuery.QcmsINFO));
            dataSet.Tables.Add(dataTable.ChargeTable(DBQuery.QcmsCONTENT_HIT));
            Session["dataSet"] = dataSet;

        }

        void Session_End(object sender, EventArgs e)
        {

            DataSet dataSet = (DataSet) Session["dataSet"];
            DataServices dataS;
            DBQuery DBQuery = new DBQuery();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                dataS = new DataServices(dsn, DBQuery.QcmsINFO_Update);
                dataS.AddParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, row["info_id"], "info_id");
                dataS.AddParameter("@HITS", SqlDbType.Int, 0, ParameterDirection.Input, row["info_hits"], "info_hits");
                dataS.Execute(DataServices.ExecutionMode.UPDATE);
                dataS.Dispose();
            }
            foreach (DataRow row in dataSet.Tables[1].Rows)
            {
                dataS = new DataServices(dsn, DBQuery.QcmsCONTENT_HIT_Update);
                dataS.AddParameter("@DATE", SqlDbType.DateTime, 0, ParameterDirection.Input, row["hit_date"], "hit_date");
                dataS.AddParameter("@CONTENT", SqlDbType.Int, 0, ParameterDirection.Input, row["hit_content"], "hit_content");
                dataS.AddParameter("@USER", SqlDbType.Int, 0, ParameterDirection.Input, row["hit_user"], "hit_user");
                dataS.Execute(DataServices.ExecutionMode.INSERT);
                dataS.Dispose();
            }
            //Mise à jour de la base de données
        }
        
    }
}