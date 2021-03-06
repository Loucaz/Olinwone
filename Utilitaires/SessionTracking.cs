﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class SessionTracking
    /// @par Classe permettant d'avoir a suivi des pages consulter par le visiteur / user
    //////////////////////////////////////////////////
    public class SessionTracking
    {
        #region "Propriétés"

        private int _config_id;

        //////////////////////////////////////////////////
        /// @brief Adresse IP du visiteur
        //////////////////////////////////////////////////
        public String VisiteurAddress;

        //////////////////////////////////////////////////
        /// @brief Chaine de connexion à la base de données
        //////////////////////////////////////////////////
        public String VisiteurDsn;

        //////////////////////////////////////////////////
        /// @brief Chaine décrivant l'agent utilisateur (navigateur)
        //////////////////////////////////////////////////
        public String VisiteurUserAgent;

        //////////////////////////////////////////////////
        /// @brief Browser du visiteur
        //////////////////////////////////////////////////
        public HttpBrowserCapabilities VisiteurBrowser;

        //////////////////////////////////////////////////
        /// @brief Id du visiteur
        //////////////////////////////////////////////////
        public int VisiteurId;

        //////////////////////////////////////////////////
        /// @brief Langue du visiteur
        //////////////////////////////////////////////////
        public string VisiteurLangue;

        //////////////////////////////////////////////////
        /// @brief Page d'accueil du visiteur
        //////////////////////////////////////////////////
        public string VisiteurHomePage;

        //////////////////////////////////////////////////
        /// @brief Profil du visiteur
        //////////////////////////////////////////////////
        public int VisiteurProfil;

        //////////////////////////////////////////////////
        /// @brief Id ADN du site
        //////////////////////////////////////////////////
        public int VisiteurSite;

        //////////////////////////////////////////////////
        /// @brief Identifiant ADN de la visite
        //////////////////////////////////////////////////
        public int VisiteurVisiteId;

        //////////////////////////////////////////////////
        /// @brief Rubriques accessibles au visiteur
        //////////////////////////////////////////////////
        public DataTable VisiteurDroits;

        //////////////////////////////////////////////////
        /// @brief Page vue par le visiteur
        //////////////////////////////////////////////////
        public int VisiteurPage;

        //////////////////////////////////////////////////
        /// @brief URL vivitée
        //////////////////////////////////////////////////
        public int VisiteurPageURL;

        #endregion

        #region "Actions"

        #region "Enregistrement des visites et des configurations"

        //////////////////////////////////////////////////
        /// @brief Récupère la configuration du visiteur
        //////////////////////////////////////////////////
        public void Visiteur_Configuration()
        {
            string req = "SELECT config_id FROM wmsUSERAGENT WHERE config_useragent=@USERAGENT";
            DataServices oData = new DataServices(VisiteurDsn, req);

            oData.AddParameter("@USERAGENT", SqlDbType.NVarChar, 1000, ParameterDirection.Input, VisiteurUserAgent, "config_useragent");
            oData.GetStructures();

            if (!oData.UtilityDataExiste)
            {
                req = "INSERT INTO wmsUSERAGENT (config_OS,config_navigateur,config_version,config_screen,config_cookies,config_crawler, config_useragent) VALUES (@PLATFORM,@NAVIGATEUR,@VERSION,@SCREENW,@COOKIE,@CRAWLER,@USERAGENT)";
                oData.UtilitySqlCommand = new SqlCommand(req, oData.UtilitySqlConnection);
                oData.AddParameter("@PLATFORM", SqlDbType.VarChar, 50, ParameterDirection.Input, VisiteurBrowser.Platform, "config_OS");
                oData.AddParameter("@NAVIGATEUR", SqlDbType.VarChar, 50, ParameterDirection.Input, VisiteurBrowser.Type, "config_navigateur");
                oData.AddParameter("@VERSION", SqlDbType.VarChar, 50, ParameterDirection.Input, VisiteurBrowser.Version, "config_version");
                oData.AddParameter("@SCREENW", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurBrowser.ScreenPixelsWidth, "config_screen");
                oData.AddParameter("@COOKIE", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurBrowser.Cookies, "config_cookies");
                oData.AddParameter("@CRAWLER", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurBrowser.Crawler, "config_crawler");
                oData.AddParameter("@USERAGENT", SqlDbType.NVarChar, 1000, ParameterDirection.Input, VisiteurUserAgent, "config_useragent");
                oData.Execute(DataServices.ExecutionMode.INSERT);
                _config_id = (int) oData.UtilityDataId;

            }
            else
            {
                _config_id = (int) oData.UtilityDataField;
            }

            oData.Dispose();
        }

        //////////////////////////////////////////////////
        /// @brief Enregistre la visite
        //////////////////////////////////////////////////
        public void Visiteur_Enregistre_Visite(string _session)
        {
            string pays_nom;
            double code_ip;
            string req;
            DataServices oData;

            Visiteur_Configuration();
            try {
                string[] IP = VisiteurAddress.Split('.') ;
                code_ip = (16777216 * Convert.ToDouble((IP[0]))) + (65536 * Convert.ToDouble((IP[1]))) + (256 * Convert.ToDouble((IP[2]))) + Convert.ToDouble((IP[3]));
                req = "SELECT TOP 1 iplocation_pays FROM wmsIPLOCATION WHERE iplocation_code_debut <=@DEBUT AND iplocation_code_fin >= @FIN";
                oData = new DataServices(VisiteurDsn, req);
                oData.AddParameter("@DEBUT", SqlDbType.Float, 0, ParameterDirection.Input, code_ip, "iplocation_code_debut");
                oData.AddParameter("@FIN", SqlDbType.Float, 0, ParameterDirection.Input, code_ip, "iplocation_code_fin");
                oData.GetStructures();
                if (oData.UtilityDataExiste) {
                    pays_nom = oData.UtilityDataField.ToString();
                } else {
                    pays_nom = "N/A";
                    code_ip = 0;
                }
            }
            catch
            {
                pays_nom = "N/A";
                code_ip = 0;
            }

            req = "INSERT INTO wmsVISITE (visite_date_debut,visite_session,visiteur_id,config_id,visite_host_ip,visite_site,visite_code,visite_pays,visite_debut,visite_fin,visite_duree,visite_pages,visite_pages_in) ";
            req += "values (@DATE,@SESSION,@VISITEUR,@CONFIG,@IP,@SITE,@VISITE,@PAYS,@DEBUT,@FIN,@DUREE,@PAGES,@PAGESINT)";

            oData = new DataServices(VisiteurDsn, req);
            oData.AddParameter("@DATE", SqlDbType.DateTime, 0, ParameterDirection.Input, DateTime.Now, "visite_date_debut");
            oData.AddParameter("@VISITEUR", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurId, "visiteur_id");
            oData.AddParameter("@CONFIG", SqlDbType.Int, 0, ParameterDirection.Input, _config_id, "config_id");
            oData.AddParameter("@IP", SqlDbType.VarChar, 20, ParameterDirection.Input, VisiteurAddress, "visite_host_ip");
            oData.AddParameter("@SITE", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurSite, "visite_site");
            oData.AddParameter("@VISITE", SqlDbType.Float, 0, ParameterDirection.Input, code_ip, "visite_code");
            oData.AddParameter("@PAYS", SqlDbType.VarChar, 50, ParameterDirection.Input, pays_nom, "visite_pays");
            oData.AddParameter("@SESSION", SqlDbType.VarChar, 50, ParameterDirection.Input, _session, "visite_session");
            oData.AddParameter("@DEBUT", SqlDbType.DateTime, 0, ParameterDirection.Input, DateTime.Now, "visite_debut");
            oData.AddParameter("@FIN", SqlDbType.DateTime, 0, ParameterDirection.Input, DateTime.Now, "visite_fin");
            oData.AddParameter("@DUREE", SqlDbType.Int, 0, ParameterDirection.Input, 0, "visite_duree");
            oData.AddParameter("@PAGES", SqlDbType.Int, 0, ParameterDirection.Input, 0, "visite_pages_in");
            oData.AddParameter("@PAGESINT", SqlDbType.Int, 0, ParameterDirection.Input, 0, "visite_pages");
            oData.Execute(DataServices.ExecutionMode.INSERT);
            VisiteurVisiteId = (int) oData.UtilityDataId;
            oData.Dispose();



        }
        //////////////////////////////////////////////////
        /// @brief Modifie la visite
        //////////////////////////////////////////////////
        public void Visiteur_Anonyme_To_Member(int memberid, int visiteid) {

            String req = "UPDATE wmsVISITE SET visiteur_id=@VISITEUR where visite_id=@SESSIONADN";
            DataServices oData = new DataServices(VisiteurDsn, req);
            oData.AddParameter("@SESSIONADN", SqlDbType.Int, 0, ParameterDirection.Input, visiteid, "visite_id");
            oData.AddParameter("@VISITEUR", SqlDbType.Int, 0, ParameterDirection.Input, memberid, "visiteur_id");
            oData.Execute(DataServices.ExecutionMode.UPDATE);
            oData.Dispose();

            req = "UPDATE crmPUBLIC SET public_connexions =COALESCE(public_connexions, 0)+1, public_dernierevisite=@DATE WHERE public_id =@VISITEUR";
            oData = new DataServices(VisiteurDsn, req);
            oData.AddParameter("@VISITEUR", SqlDbType.Int, 0, ParameterDirection.Input, memberid, "public_id");
            oData.AddParameter("@DATE", SqlDbType.DateTime, 0, ParameterDirection.Input, DateTime.Now, "public_dernierevisite");
            oData.Execute(DataServices.ExecutionMode.UPDATE);
            oData.Dispose();

        }
        #endregion

        #region "Enregistrement des pages vues"


        //////////////////////////////////////////////////
        /// @brief Voir la procedure stocker "Visiteur_Enregistre_Hit" pour savoir comment l'utiliser
        //////////////////////////////////////////////////
        public void Visiteur_Enregistre_Hit(string _referer, string _domaine)
            {
            DataServices ds = new DataServices(VisiteurDsn, "Visiteur_Enregistre_Hit");
            ds.UtilitySqlCommand.CommandType = CommandType.StoredProcedure;
            ds.AddParameter("@referer", SqlDbType.VarChar, 255, ParameterDirection.Input, _referer, "referer_url");
            ds.AddParameter("@domaine", SqlDbType.VarChar, 255, ParameterDirection.Input, _domaine, "referer_domaine");
            ds.AddParameter("@site", SqlDbType.Int, 255, ParameterDirection.Input, VisiteurSite, "referer_site");
            ds.AddParameter("@date", SqlDbType.DateTime, 255, ParameterDirection.Input, DateTime.Now, "hit_date");
            ds.AddParameter("@session", SqlDbType.Int, 255, ParameterDirection.Input, VisiteurVisiteId, "visite_id");
            ds.AddParameter("@rubrique", SqlDbType.Int, 255, ParameterDirection.Input, VisiteurPage, "page_id");
            ds.AddParameter("@url", SqlDbType.VarChar, 255, ParameterDirection.Input, VisiteurPageURL, "hit_url");
            ds.UtilitySqlCommand.ExecuteReader();
            ds.Dispose();
        }


        #endregion


        #region "Gestion du profil"

        //////////////////////////////////////////////////
        /// @brief  Construit la suite des rubriques autorisées au format CSV (OR remplace ;)
        //////////////////////////////////////////////////
        public string Visiteur_Droits_Filtre()
        {
            DataTable oDroits = VisiteurDroits;
            StringBuilder oBuilder = new StringBuilder();
            foreach (DataRow oRow in oDroits.Rows) {
                oBuilder.Append("wmsPAGE.page_id=" + oRow[0].ToString() + " OR ");
                }
            string _csvFormat = oBuilder.ToString();
            return _csvFormat.Substring(_csvFormat.Length - 4);
        }
        //////////////////////////////////////////////////
        /// @brief Récupère le profil du visiteur (visiteurID, langue, profil)
        //////////////////////////////////////////////////
        public void Profil_Visiteur(int _visiteur, string _langue, int _profil)
        {
            VisiteurId = _visiteur;
            VisiteurLangue = _langue;
            VisiteurProfil = _profil;
            VisiteurDroits = Charge_Profil_Visiteur();
        }
        //////////////////////////////////////////////////
        /// @brief Récupère le profil du visiteur (enregistrement Datarow)
        //////////////////////////////////////////////////
        public void Profil_Visiteur(DataRow _visiteurData)
        {
            VisiteurId = (int) _visiteurData["public_id"];
            VisiteurLangue = (string) _visiteurData["public_langue"];
            VisiteurProfil = (int) _visiteurData["public_profil"];
            VisiteurDroits = Charge_Profil_Visiteur();
        }

        //////////////////////////////////////////////////
        /// @brief Construit un Datatable contenant les rubriques accessibles par le profil pour le site sélectionné
        //////////////////////////////////////////////////
        private DataTable Charge_Profil_Visiteur()
        {
            DataTable oDroits = new DataTable();
            Object[] newRow= new object[0];
            oDroits.Columns.Add(new DataColumn("page_id", Type.GetType("System.Int32")));

            String req = "SELECT * FROM crmDROIT_PAGE WHERE profil_id IN (SELECT profil_id FROM crmDROIT_SITE WHERE site_id=@SITE) AND profil_id=@PROFIL";
            DataServices oData = new DataServices(VisiteurDsn, req);
            oData.AddParameter("@SITE", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurSite, "site_id");
            oData.AddParameter("@PROFIL", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurProfil, "profil_id");
            oData.GetStructures();
            foreach (DataRow oRub in oData.UtilityDataTable.Rows) {
                newRow[0] = oRub["page_id"];
                oDroits.Rows.Add(newRow);
            }
            oData.Dispose();

            VisiteurHomePage = Charge_Visiteur_Homepage();
            return oDroits;
        }

        //////////////////////////////////////////////////
        /// @brief Charge le chemin de la page d'accueil pour le profil et pour le site sélectionné
        //////////////////////////////////////////////////
        private string Charge_Visiteur_Homepage()
        {
            string _accueil = "/";

            string req = "select page_url FROM wmsPAGE where page_id = (select page_id from crmDROIT_SITE where site_id=@SITE AND profil_id=@PROFIL)";
            DataServices oData = new DataServices(VisiteurDsn, req);
            oData.AddParameter("@PROFIL", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurProfil, "profil_id");
            oData.AddParameter("@SITE", SqlDbType.Int, 0, ParameterDirection.Input, VisiteurSite, "site_id");
            oData.GetStructures();
            _accueil += oData.UtilityDataField;
            oData.Dispose();

            return _accueil;
        }

        #endregion

        #endregion
    }
}
