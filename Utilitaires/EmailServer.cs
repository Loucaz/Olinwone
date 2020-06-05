using System.Net;
using System.Net.Mail;
using System.Data;
using System;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class EmailServer
    /// @par Description
    /// @note Note
    //////////////////////////////////////////////////
    public class EmailServer : SmtpClient
    {
        private Snippets snippets = new Snippets();

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public EmailServer(string _composant, DataTable _composants)
        {
            ChargeProprietes(_composants, _composant);
            AutoriseServeur();
        }

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string ServerLogin;

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string ServerName;

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string ServerPassword;

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public int ServerPort;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public bool ServerPrivacy;

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        private void AutoriseServeur()
        {
            Host = ServerName;
            Port = ServerPort;
            if (ServerPrivacy)
            {
                Credentials = new NetworkCredential(ServerLogin, ServerPassword);
            }
        }

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        private void ChargeProprietes(DataTable _composants, string _composantNom)
        {
            ServerName = snippets.ChargeAttribut("ServerName", _composants, _composantNom);
            ServerLogin = snippets.ChargeAttribut("ServerLogin", _composants, _composantNom);
            ServerPassword = snippets.ChargeAttribut("ServerPassword", _composants, _composantNom);
            ServerPort = Int32.Parse(snippets.ChargeAttribut("ServerPort", _composants, _composantNom));
            ServerPrivacy = Convert.ToBoolean(snippets.ChargeAttribut("ServerPrivacy", _composants, _composantNom));
        }

    }
}