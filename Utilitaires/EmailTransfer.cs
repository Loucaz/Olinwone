using System;
using System.Data;

namespace Utilitaires
{
    public partial class EmailTransfer
    {
        private Snippets snippets = new Snippets();
        private EmailMessage _emailMessage;
        private EmailServer _emailServer;
        private string s_choix;
        private string s_dsn;

        public EmailTransfer(string _composant, DataTable _composants, string _nom, string _url, string _adresse, string _option, string _dsn)
        {
            s_choix = _option;
            s_dsn = _dsn;
            ChargeProprietes(_composants, _composant);
            _emailMessage = new EmailMessage(TransferEmailMessageName, _composants, _nom, _url, _adresse);
            _emailServer = new EmailServer(TransferEmailServerName, _composants);
        }

        /// <summary>
        /// Nom du composant message email utilisé
        /// </summary>
        public string TransferEmailMessageName;

        /// <summary>
        /// Nom du composant serveur de mail utilisé
        /// </summary>
        public string TransferEmailServerName;

        /// <summary>
        /// Adresse de messagerie des destinataires en copie
        /// </summary>
        public string TransferRecipientBcc;

        /// <summary>
        /// Destinataires BCC dynamiques
        /// </summary>
        public bool TransferRecipientBccDynamic;

        /// <summary>
        /// Requête pour récupération des adresses de messagerie des destinataires en copie sous conditions
        /// </summary>
        public string TransferRecipientBccSql;

        /// <summary>
        /// Adresses de messagerie des destinataires
        /// </summary>
        public string TransferRecipientTo;

        /// <summary>
        /// Destinataires TO dynamiques
        /// </summary>
        public bool TransferRecipientToDynamic;

        /// <summary>
        /// Requête pour récupération des adresses de messagerie des destinataires sous conditions
        /// </summary>
        public string TransferRecipientToSql;

        /// <summary>
        /// Envoi du message
        /// </summary>
        public void EnvoieMessage()
        {
            string[] _recipients;
            if (TransferRecipientToDynamic)
            {
                _recipients = ChargeDestinataires(TransferRecipientToSql);
            }
            else
            {
                _recipients = TransferRecipientTo.Split('|');
            }

            foreach (string _recipient in _recipients)
            {
                try
                {
                    _emailMessage.To.Add(_recipient);
                }
                catch
                {
                }
            }

            if (TransferRecipientBccDynamic)
            {
                _recipients = ChargeDestinataires(TransferRecipientBccSql);
            }
            else
            {
                _recipients = TransferRecipientBcc.Split('|');
            }

            foreach (string _recipient in _recipients)
            {
                try
                {
                    _emailMessage.Bcc.Add(_recipient);
                }
                catch
                {
                }
            }

            try
            {
                _emailServer.Send(_emailMessage);
            }
            catch
            {
            }
        }

        private void ChargeProprietes(DataTable _composants, string _composantNom)
        {
            TransferEmailMessageName = snippets.ChargeAttribut("TransferEmailMessageName", _composants, _composantNom);
            TransferEmailServerName = snippets.ChargeAttribut("TransferEmailServerName", _composants, _composantNom);
            TransferRecipientBcc = snippets.ChargeAttribut("TransferRecipientBcc", _composants, _composantNom);
            TransferRecipientBccDynamic = Convert.ToBoolean(snippets.ChargeAttribut("TransferRecipientBccDynamic", _composants, _composantNom));
            TransferRecipientBccSql = snippets.ChargeAttribut("TransferRecipientBccSql", _composants, _composantNom);
            TransferRecipientTo = snippets.ChargeAttribut("TransferRecipientTo", _composants, _composantNom);
            TransferRecipientToDynamic = Convert.ToBoolean(snippets.ChargeAttribut("TransferRecipientToDynamic", _composants, _composantNom));
            TransferRecipientToSql = snippets.ChargeAttribut("TransferRecipientToSql", _composants, _composantNom);
        }

        private string[] ChargeDestinataires(string _code)
        {
            return null;
        }

        private string CleanQuotes(string _content)
        {
            return _content.Replace( "'", "''");
        }

    }
}
