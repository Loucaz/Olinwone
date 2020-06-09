using System;
using System.Data.SqlClient;
using System.Data;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class DataTables
    /// @par Facilite l'utilisation des requêtes en utilisant des fonctions déjà préparer
    /// @note Se renseigner sur System.Data.DataTable pour mieux comprendre le principe d'une DataTable 
    //////////////////////////////////////////////////
    public class DataTables
    {
        #region "Variables locales"
        private string _dsn;
        private DataServices oData;
        private DBQuery DBQuery;
        #endregion

        #region "Méthodes et fonctions publiques"
        //////////////////////////////////////////////////
        /// @brief Constructeur: prepare la requete
        //////////////////////////////////////////////////
        public DataTables(string dsn)
        {
            _dsn = dsn;
            DBQuery = new DBQuery();
        }

        //////////////////////////////////////////////////
        /// \brief Charge les collections du site
        //////////////////////////////////////////////////
        public DataTable Collection(int _site)
        {
            return ChargeTable(DBQuery.QCollection, _site, "site_id");
        }
        //////////////////////////////////////////////////
        /// \brief Charge les collections du site actif
        //////////////////////////////////////////////////
        public DataTable CollectionForm(int _site, int _siteActif)
        {
            ChangeVariable("listes", _siteActif);
            return ChargeTable(DBQuery.QCollection, _site, "site_id");
        }

        //////////////////////////////////////////////////
        /// @brief Charge la liste des composants pour le site
        //////////////////////////////////////////////////
        public DataTable Composants(int _site)
        {
            return ChargeTable(DBQuery.QComposant, _site, "composant_site");
        }
        //////////////////////////////////////////////////
        /// @brief Charge la liste des composants pour le site actif
        //////////////////////////////////////////////////
        public DataTable ComposantsForm(int _site, int _siteActif)
        {
            ChangeVariable("composants", _siteActif);
            return ChargeTable(DBQuery.QComposant, _site, "composant_site");
        }

        //////////////////////////////////////////////////
        /// @brief Appelle données d'une table
        //////////////////////////////////////////////////
        public DataTable DataTable(int _site, string _table, string _champ)
        {
            return ChargeTable("SELECT * FROM " + _table + " WHERE " + _champ + "=@SITE", _site, _champ);
        }
        //////////////////////////////////////////////////
        /// @brief Appelle données d'une table d'un site actif
        //////////////////////////////////////////////////
        public DataTable DataTableForm(int _site, string _table, string _champ, int _siteActif)
        {
            AnalyseVariable(_table, _siteActif);
            return ChargeTable("SELECT * FROM " + _table + " WHERE " + _champ + "=@SITE", _site, _champ);
        }

        //////////////////////////////////////////////////
        /// @brief Charge la table avec les données attendues
        //////////////////////////////////////////////////
        private DataTable Charge(string _requete, int _site = -1, string _champ = null)
        {
            oData = new DataServices(_dsn, _requete);
            oData.UtilitySqlCommand = new SqlCommand(_requete, oData.UtilitySqlConnection);
            if (_champ != null)
            {
                oData.AddParameter("@SITE", SqlDbType.Int, 0, ParameterDirection.Input, _site, _champ);
            }
            oData.GetStructures();
            oData.Dispose();
            if (_requete.IndexOf("wmsCOMPOSANT_PAGE") > 1 && _site != -1)
            {
                ChangeVariable("composants", _site);
            }
            return oData.UtilityDataTable;
        }
        //////////////////////////////////////////////////
        /// @brief Charge la table avec uniquement la requete
        //////////////////////////////////////////////////
        public DataTable ChargeTable(string _requete)
        {
            return Charge(_requete);
        }
        //////////////////////////////////////////////////
        /// @brief Charge la table avec la requete + le site
        //////////////////////////////////////////////////
        public DataTable ChargeTableForm(string _requete, int _site)
        {
            return Charge(_requete, _site);
        }
        //////////////////////////////////////////////////
        /// @brief Charge la table avec tout les parametres
        //////////////////////////////////////////////////
        private DataTable ChargeTable(string _requete, int _site, string _champ)
        {
            return Charge(_requete, _site, _champ);
        }
        //////////////////////////////////////////////////
        /// @brief Change la variable pour le site actif
        //////////////////////////////////////////////////
        private void ChangeVariable(string _var, int _site)
        {
            oData = new DataServices(_dsn, DBQuery.QVariable);
            oData.AddParameter("@VAR", SqlDbType.VarChar, 50, ParameterDirection.Input, _var, "var_nom");
            oData.AddParameter("@SITE", SqlDbType.Int, 0, ParameterDirection.Input, _site, "var_site");
            oData.Execute(DataServices.ExecutionMode.UPDATE);
            oData.Dispose();
        }
        //////////////////////////////////////////////////
        /// @brief Change la variable pour le site actif selon notre mot cle
        //////////////////////////////////////////////////
        private void AnalyseVariable(string _table, int _site)
        {
            switch (_table)
            {
                case "cmsDOSSIER":
                    ChangeVariable("dossiers", _site);
                    break;
                case "cmsDOSSIER_INFO":
                    ChangeVariable("dossiersinfos", _site);
                    break;
                case "cmsINFO":
                    ChangeVariable("infos", _site);
                    break;
                case "wmsPAGES":
                    ChangeVariable("pages", _site);
                    break;
                case "wmsPARAMETRE":
                    ChangeVariable("parametres", _site);
                    break;
                case "wmsSITE":
                    ChangeVariable("siteconfig", _site);
                    break;
                case "wmsTOOLBAR":
                    ChangeVariable("toolbarlist", _site);
                    break;
                case "wmsTOOLBAR_BOUTON":
                    ChangeVariable("toolbaritems", _site);
                    break;
            }
        }
        #endregion

    }
}
