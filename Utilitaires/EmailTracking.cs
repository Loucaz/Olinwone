using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;

namespace Utilitaires
{
    public class EmailTracking : IHttpModule
    {
        private DataServices oData;
        private DataTable _mailinglinks;
        private string _dsn;


        public void Dispose()
        {
        }

        public void Init(HttpApplication Appl)
        {
            Appl.BeginRequest += GetURL_BeginRequest;
        }

        public void GetURL_BeginRequest(object sender, EventArgs args)
        {
            HttpApplication Instance = (HttpApplication)sender;
            string _urlPattern = "/emailing/link{NUM}.aspx";
            Regex _pattern;
            string _mailID = Instance.Request.QueryString["mid"];
            string _userID = Instance.Request.QueryString["uid"];
            string _linkID;
            string _linkURL;
            if (!string.IsNullOrEmpty(_mailID) && !string.IsNullOrEmpty(_userID))
            {
                _dsn = Instance.Application["dsn"].ToString();
                _mailID = GetDataRow("select mailing_id from crmMAILING where mailing_cle='" + _mailID + "'")["mailing_id"].ToString();
                _userID = GetDataRow("select public_id from crmPUBLIC where public_cle='" + _userID + "'")["public_id"].ToString();
                _mailinglinks = GetDataTable("select * from crmMAILING_LINKS where link_mailing=" + _mailID);
                foreach (DataRow _link in _mailinglinks.Rows)
                {
                    _linkID = _link["link_id"].ToString();
                    _linkURL = _link["link_url"].ToString();
                    _pattern = new Regex(_urlPattern.Replace("{NUM}", _linkID), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (_pattern.IsMatch(Instance.Request.Path))
                    {
                        if (_link["link_type"].ToString() == "IMAGE")
                        {
                            // renvoie l'image associée et stocke la stat
                            try
                            {
                                Instance.Response.AppendHeader("Content-Disposition", "attachment; filename=logo_officiel");
                                Instance.Response.ContentType = "image/png";
                                UpdateDatabase("update crmMAILING_REPORT set report_hits=1 where report_user=" + _userID + " and report_link=" + _linkID);
                                Instance.Response.TransmitFile(_linkURL);
                            }
                            catch
                            {
                            }

                            Instance.Response.End();
                        }
                        else if (_link["link_type"].ToString() == "OPTOUT" | _link["link_type"].ToString() == "ONLINE")
                        {
                            UpdateDatabase("update crmMAILING_REPORT set report_hits=1 where report_user=" + _userID + " and report_link=" + _linkID);
                            Instance.Response.Redirect(_linkURL);
                        }
                        else
                        {
                            // redirige vers le site concerné et stocke la stat
                            UpdateDatabase("update crmMAILING_REPORT set report_hits=report_hits+1 where report_user=" + _userID + " and report_link=" + _linkID);
                            Instance.Response.Redirect(_linkURL);
                        }

                        break;
                    }
                }
            }
        }

        private DataTable GetDataTable(string requete)
        {
            DataTable _result;
            oData = new DataServices(_dsn, requete);
            oData.GetStructures();
            _result = oData.UtilityDataTable;
            oData.Dispose();
            return _result;
        }

        private DataRow GetDataRow(string requete)
        {
            DataRow _result;
            oData = new DataServices(_dsn, requete);
            oData.GetStructures();
            _result = oData.UtilityDataRow;
            oData.Dispose();
            return _result;
        }

        private void UpdateDatabase(string requete)
        {
            oData = new DataServices(_dsn, requete);
            oData.Execute(DataServices.ExecutionMode.UPDATE);
            oData.Dispose();
        }
    }
}
