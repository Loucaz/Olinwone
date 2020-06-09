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
    //////////////////////////////////////////////////
    /// @class Snippets
    /// @par Classe avec diverse fonction utile à Olinwone avec notamment du chiffrement et des modifications d'url
    //////////////////////////////////////////////////
    public class Snippets
    {
        #region "Méthodes et fonctions publiques"

        //////////////////////////////////////////////////
        /// @brief Constructeur
        //////////////////////////////////////////////////
        public Snippets() { }

        #region "Calcules"

        //////////////////////////////////////////////////
        /// @brief Permet de chiffrer un string en SHA512
        //////////////////////////////////////////////////
        public string EncodageHash(string source) 
        {
            SHA512 sha512Hash = SHA512.Create();
            return Convert.ToBase64String(sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(source)));
        }

        //////////////////////////////////////////////////
        /// @brief Convertir un DateTime en heure Unix, mesure du temps basée sur le nombre de secondes écoulées depuis le 1ᵉʳ janvier 1970
        //////////////////////////////////////////////////
        public int UnixTimestamp(DateTime thisDate)
        {
            return (int)thisDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        //////////////////////////////////////////////////
        /// @brief Encode your data in Base64
        //////////////////////////////////////////////////
        public string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        //////////////////////////////////////////////////
        /// @brief Decode your data
        //////////////////////////////////////////////////
        public string Base64Decode(string base64EncodedData)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        }


        //////////////////////////////////////////////////
        /// @brief Adapte la Requete avec un Dictionary du genre : "<url>", _url
        //////////////////////////////////////////////////
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
        //////////////////////////////////////////////////
        /// @brief Enleve le http de l'url 
        //////////////////////////////////////////////////
        public string UrlRoute(string url, string hote)
        {
            return url.Replace("http://", "").Replace("https://", "").Replace(hote + "/", "");
        }

        //////////////////////////////////////////////////
        /// @brief Enlève certains paramètres de la requête
        //////////////////////////////////////////////////
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
        //////////////////////////////////////////////////
        /// @brief
        /// Traitement de la table [0] cmsINFO
        /// puis identifie la ligne qui correspond à info_id et la mettre à jour.
        /// @n
        /// Traitement de la table [1] : cmsCONTENT_HIT
        /// puis insertion de cette nouvelle ligne dans la table
        /// 
        /// @par Côté console avant la fonction:
        /// Dans console construire un dataset contenant 2 tables :  cmsINFO et cmsCONTENT_HIT.
        /// @n Envoyer le dataset + user_id + info_id  au composant.
        /// @n Apres la fonction:
        /// @n Récupération du dataset et mise en cache.
        /// @n A la fin de la session, mise à jour de la base données avec les infos contenus dans le dataset.
        //////////////////////////////////////////////////
        public DataSet UpdateInfoHits(DataSet dataSet, int info_id,int user_id)
        {
            try
            {
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
                DataRow row = dataSet.Tables[1].NewRow();
                row["hit_date"] = DateTime.Now;
                row["hit_content"] = info_id;
                row["hit_user"] = user_id;
                dataSet.Tables[1].Rows.Add(row);
            }
            catch (Exception) { }
            return dataSet;
        }
        #endregion

        #region "Snippets"
        //////////////////////////////////////////////////
        /// @brief Afficher une image
        //////////////////////////////////////////////////
        public string ImageDisplay(string imgID, string imgUrl)
        {
            if (imgUrl == "") {
                imgUrl = "/downloads/adnv10/images/empty.jpg";
            }
            return "<img id='"+ imgID +"' onerror=adnImage('" + imgID + "') src='" + imgUrl + "' class='img-fluid' alt='' />";
        }

        //////////////////////////////////////////////////
        /// @brief Exraire une valeur precise
        //////////////////////////////////////////////////
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
