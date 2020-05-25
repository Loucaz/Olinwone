using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitaires;

namespace Consol
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string dsn = ConfigurationManager.AppSettings["dsn"];
            Snippets snippets= new Snippets();
            DataSet dataSet = new DataSet();

            //démarrage de la session :création du dataset avec les tables vides au démarrage de la session du user
            string requete2 = "SELECT * FROM [dbo].[cmsINFO] where info_id=0";
            string requete3 = "SELECT * FROM [dbo].[cmsCONTENT_HIT] where content_hit=0";
            
            DataTables dataTable = new DataTables(dsn);
            dataSet.Tables.Add(dataTable.ChargeTable(requete2));
            dataSet.Tables.Add(dataTable.ChargeTable(requete3));

            //user appelle 1 displayCart

            //mise à jour de mon dataset par le displaycart

            int infoid = 3;
            affiche(dataSet);
            snippets.UpdateInfoHits(dataSet,infoid,0);
            affiche(dataSet);





            void affiche(DataSet ds)
            {
                foreach (DataTable table in ds.Tables)
                {
                    Console.WriteLine(table.TableName);
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            Console.WriteLine(row[column]);
                        }
                    }
                    Console.WriteLine();
                }
            }


        }

    }
}
