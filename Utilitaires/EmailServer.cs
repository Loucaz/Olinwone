using System.Net;
using System.Net.Mail;
using System.Data;
using System;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class EmailServer
    /// @par Permet a Onlinwone d'autorise l'accès au serveur de protocole SMTP (Simple Mail Transfer Protocol)
    /// @note Une connaissance sur System.Net.Mail est requise pour comprendre le fonctionnement de la construction du mail notamment la classe SmtpClient
    //////////////////////////////////////////////////
    public class EmailServer : SmtpClient
    {
        private Snippets snippets = new Snippets();

        //////////////////////////////////////////////////
        /// @brief Constructeur
        //////////////////////////////////////////////////
        public EmailServer(string _composant, DataTable _composants)
        {
            ChargeProprietes(_composants, _composant);
            AutoriseServeur();
        }

        //////////////////////////////////////////////////
        /// @brief Identifiant nécessaire à l'accès sur le serveur
        //////////////////////////////////////////////////
        public string ServerLogin;

        //////////////////////////////////////////////////
        /// @brief Nom du serveur
        //////////////////////////////////////////////////
        public string ServerName;

        //////////////////////////////////////////////////
        /// @brief Identifiant nécessaire à l'accès sur le serveur
        //////////////////////////////////////////////////
        public string ServerPassword;

        //////////////////////////////////////////////////
        /// @brief Port du serveur à utiliser
        //////////////////////////////////////////////////
        public int ServerPort;
        //////////////////////////////////////////////////
        /// @brief  Sécurité du serveur est activée
        //////////////////////////////////////////////////
        public bool ServerPrivacy;

        //////////////////////////////////////////////////
        /// @brief Autorise l'accès au serveur
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
        /// @brief Charge les propriétés du composant stockés en base de données 
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