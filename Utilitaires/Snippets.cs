using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilitaires
{
    public class Snippets
    {
        #region "Méthodes et fonctions publiques"

        public Snippets() { }

        #region "Calcules"

        public string EncodageHash(string source) 
        {
            SHA512 sha512Hash = SHA512.Create();
            return Convert.ToBase64String(sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(source)));
        }

        public int UnixTimestamp(DateTime thisDate)
        {
            return (int)thisDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        public static string Base64Decode(string base64EncodedData)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        }


        public string CompleteRequete(string chaine, Dictionary<string,string> dictionary)
        {
            string pattern = @"<\s*[^>]*>";
            foreach (Match match in Regex.Matches(chaine, pattern))
            {
                if(dictionary.ContainsKey(match.Value))
                    chaine = chaine.Replace(match.Value, dictionary[match.Value]);
            }
            return chaine;    
        }

        #endregion

        #region "URL"
        public string UrlRoute(string url, string hote)
        {
            return url.Replace("http://", "").Replace("https://", "").Replace(hote + "/", "");
        }

        public string UrlUtile(string url, string hote)
        {
            string _cleanUrl = UrlRoute(url, hote);
            if (_cleanUrl.Contains("&id"))
            {
                _cleanUrl = _cleanUrl.Substring(0,_cleanUrl.IndexOf("&id"));
            }
            else if (_cleanUrl.Contains("&filter"))
            {
                _cleanUrl = _cleanUrl.Substring(0, _cleanUrl.IndexOf("&filter"));
            }
            else if (_cleanUrl.Contains("&params"))
            {
                _cleanUrl = _cleanUrl.Substring(0, _cleanUrl.IndexOf("&params"));
            }
            return _cleanUrl;
        }
        #endregion

        #region "Update database"
        public DataSet UpdateInfoHits(DataSet dataSet, int info_id,int user_id)
        {
            //côté console
            //dans console construire un dataset contenant 2 tables :  cmsINFO et cmsCONTENT_HIT
            //envoyer le dataset + user_id + info_id  au composant

            try
            {
                //récupérer le dataset
                //Traitement de la table [0] cmsINFO
                //identifier la ligne qui correspond à info_id et la mettre à jour

                bool exist = false;
                foreach (DataRow ligne in dataSet.Tables[0].Rows)
                {
                    if ((int) ligne["info_id"] == info_id)
                    {
                        ligne["info_hits"] = (int) ligne["info_hits"] + 1;
                        exist = true;
                    }
                }
                if (exist == false)
                {
                    DataRow r = dataSet.Tables[0].NewRow();
                    r["info_id"] = info_id;
                    r["info_hits"] = 1;
                    dataSet.Tables[0].Rows.Add(r);
                }


                //traitement de la table [1] : cmsCONTENT_HIT
                //création d'une nouvelle ligne
                //insertion de cette nouvelle ligne dans la table
                //mise à jour du dataset
                DataRow row = dataSet.Tables[1].NewRow();
                row["hit_date"] = DateTime.Now;
                row["hit_content"] = info_id;
                row["hit_user"] = user_id;
                dataSet.Tables[1].Rows.Add(row);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            //renvoi du dataset
            //si erreur renvoie du datasset d'origine

            return dataSet;

            //côté console
            //récupération du dataset et mise en cache
            //à la fin de la session, mise à jour de la base données avec les infos contenus dans le dataset
        }
        #endregion

        #region "Snippets"
        public string ImageDisplay(string imgID, string imgUrl)
        {
            if (imgUrl == "") {
                imgUrl = "/downloads/adnv10/images/empty.jpg";
            }
            return "<img id='"+ imgID +"' onerror=adnImage('" + imgID + "') src='" + imgUrl + "' class='img-fluid' alt='' />";
        }

        public string ChargeAttribut(String prop, DataTable components, String componentId)
        {
            if(componentId.IndexOf("AdnResponsive") > 0)
            {
                componentId = componentId.Replace("AdnResponsive", "");
            }
            return components.Select("composant_nom='" + componentId + "' AND parametre_nom='" + prop + "'")[0]["parametre_valeur"].ToString();
        }


        #endregion

        #endregion
    }
}
