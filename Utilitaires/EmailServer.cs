using System.Net;
using System.Net.Mail;
using System.Data;
using System;

namespace Utilitaires
{
    public class EmailServer : SmtpClient
    {
        private Snippets snippets = new Snippets();

        public EmailServer(string _composant, DataTable _composants)
        {
            ChargeProprietes(_composants, _composant);
            AutoriseServeur();
        }

        public string ServerLogin;

        public string ServerName;

        public string ServerPassword;

        public int ServerPort;
        public bool ServerPrivacy;

        private void AutoriseServeur()
        {
            Host = ServerName;
            Port = ServerPort;
            if (ServerPrivacy)
            {
                Credentials = new NetworkCredential(ServerLogin, ServerPassword);
            }
        }

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