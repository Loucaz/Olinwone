using System;
using System.Data;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class EmailTransfer
    /// @par Description
    /// @note Note
    //////////////////////////////////////////////////
    public partial class EmailTransfer
    {
        private Snippets snippets = new Snippets();
        private EmailMessage _emailMessage;
        private EmailServer _emailServer;
        private string s_choix;
        private string s_dsn;

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public EmailTransfer(string _composant, DataTable _composants, string _nom, string _url, string _adresse, string _option, string _dsn)
        {
            s_choix = _option;
            s_dsn = _dsn;
            ChargeProprietes(_composants, _composant);
            _emailMessage = new EmailMessage(TransferEmailMessageName, _composants, _nom, _url, _adresse);
            _emailServer = new EmailServer(TransferEmailServerName, _composants);
        }

        //////////////////////////////////////////////////
        /// @brief Nom du composant message email utilisé
        //////////////////////////////////////////////////
        public string TransferEmailMessageName;

        //////////////////////////////////////////////////
        /// @brief Nom du composant serveur de mail utilisé
        //////////////////////////////////////////////////
        public string TransferEmailServerName;

        //////////////////////////////////////////////////
        /// @brief Adresse de messagerie des destinataires en copie
        //////////////////////////////////////////////////
        public string TransferRecipientBcc;

        //////////////////////////////////////////////////
        /// @brief Destinataires BCC dynamiques
        //////////////////////////////////////////////////
        public bool TransferRecipientBccDynamic;

        //////////////////////////////////////////////////
        /// @brief Requête pour récupération des adresses de messagerie des destinataires en copie sous conditions
        //////////////////////////////////////////////////
        public string TransferRecipientBccSql;

        //////////////////////////////////////////////////
        /// @brief Adresses de messagerie des destinataires
        //////////////////////////////////////////////////
        public string TransferRecipientTo;

        //////////////////////////////////////////////////
        /// @brief Destinataires TO dynamiques
        //////////////////////////////////////////////////
        public bool TransferRecipientToDynamic;

        //////////////////////////////////////////////////
        /// @brief Requête pour récupération des adresses de messagerie des destinataires sous conditions
        //////////////////////////////////////////////////
        public string TransferRecipientToSql;

        //////////////////////////////////////////////////
        /// @brief Envoi du message
        //////////////////////////////////////////////////
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

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
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
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        private string[] ChargeDestinataires(string _code)
        {
            string[] _emailCondition = s_choix.Split('|');
            string _req;
            DataServices oData;
            for (int i = 0; i <= 2; i++)
            {
                if (string.IsNullOrEmpty(_emailCondition[i]))
                    _emailCondition[i] = "-";
                else
                    _emailCondition[i] = CleanQuotes(_emailCondition[i]);
            }

            _req = "select destinataire_emails from crmDESTINATAIRES WHERE destinataire_source='" + _code + "'";
            _req += " and (destinataire_filtre1='" + _emailCondition[0] + "' or destinataire_filtre1='*')";
            _req += " and (destinataire_filtre2='" + _emailCondition[1] + "' or destinataire_filtre2='*')";
            _req += " and (destinataire_filtre3='" + _emailCondition[2] + "' or destinataire_filtre3='*')";
            oData = new DataServices(s_dsn, _req);
            oData.GetStructures();
            string[] result;
            if (oData.UtilityDataExiste)
            {
                result = oData.UtilityDataField.ToString().Split('|');
            }
            else
            {
                result = "-".Split('|');
            }

            oData.Dispose();
            return result;
        }

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        private string CleanQuotes(string _content)
        {
            return _content.Replace( "'", "''");
        }

    }
}
