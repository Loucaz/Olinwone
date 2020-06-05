using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class DBQuery
    /// @par Description
    /// @note Note
    //////////////////////////////////////////////////
    public class DBQuery
    {

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public DBQuery() { }

        /**
        * \brief Charge les collections du site actif
        */
        public string QCollection = "SELECT wmsLISTE_ITEM.*, liste_titre FROM wmsLISTE_ITEM INNER JOIN wmsLISTE ON wmsLISTE_ITEM.liste_id=wmsLISTE.liste_id " +
        "WHERE wmsLISTE_ITEM.liste_id IN (SELECT liste_id FROM wmsDROIT_LISTE WHERE site_id=@SITE)";

        /**
        * \brief Charge la liste des composants pour le site actif
        */
        public string QComposant = "SELECT wmsCOMPOSANT.composant_id as composantId, composant_nom, composant_site, composant_type, " +
        "composant_associe,composant_description,composant_rang, composant_notice,parametre_nom, parametre_type, parametre_valeur " +
        "FROM wmsCOMPOSANT INNER JOIN wmsCOMPOSANT_ATTRIBUT on wmsCOMPOSANT.composant_id=wmsCOMPOSANT_ATTRIBUT.composant_id WHERE composant_site=@SITE";

        /**
        * \brief Change ma variable pour le site actif
        */
        public string QVariable = "UPDATE wmsVARIABLE set var_etat=0 WHERE var_nom=@VAR and var_site=@SITE";

        /**
        * \brief Charge le squelette de la database cmsINFO 
        */
        public string QcmsINFO = "SELECT info_id,info_hits FROM cmsINFO where info_id=0";
        /**
        * \brief Met a jour la database cmsINFO 
        */
        public string QcmsINFO_Update = "UPDATE cmsINFO set info_hits=COALESCE(info_hits, 0)+@HITS where info_id=@ID";

        /**
        * \brief Charge le squelette de la database cmsCONTENT_HIT 
        */
        public string QcmsCONTENT_HIT = "SELECT * FROM cmsCONTENT_HIT where content_hit=0";

        /**
        * \brief Met a jour la database cmsCONTENT_HIT 
        */
        public string QcmsCONTENT_HIT_Update = "INSERT INTO cmsCONTENT_HIT(hit_date, hit_content, hit_user) values(convert(datetime,@DATE,103),@CONTENT,@USER)";

        /**
        * \brief Verifie si le referer de l'url de page existe
        */
        public string QwmsREFERER_existe = "SELECT referer_id FROM wmsREFERER WHERE referer_url LIKE @REFERER";

        /**
        * \brief Met a jour la database wmsREFERER_Update
        */
        public string QwmsREFERER_Update = "INSERT INTO wmsREFERER (referer_url,referer_domaine,referer_site) VALUES (@REFERER,@DOMAINE,@SITE)";

        /**
       * \brief Charge le squelette de la database wmsHITS
       */
        public string QwmsHITS = "SELECT * FROM wmsHITS where hit_id=0";

        /**
        * \brief Met a jour la database wmsHITS
        */
        public string QwmsHITS_Update = "INSERT INTO wmsHITS (hit_date,referer_id,visite_id,page_id, hit_url) VALUES (@DATE,@REFERER,@SESSION,@RUBRIQUE,@URL)";

    }
}
