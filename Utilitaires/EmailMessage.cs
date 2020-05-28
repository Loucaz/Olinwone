using System;
using System.Text;
using System.Net.Mail;
using System.Data;

namespace Utilitaires
{
    public  class EmailMessage : MailMessage
    {
        private Snippets snippets = new Snippets();

        #region "Constructeur"
        public EmailMessage(string _composant, DataTable _composants, string _nom, string _url, string _adresse)
        {
            ChargeProprietes(_composants, _composant);
            PrepareMessage(_nom, _url, _adresse);
        }
        #endregion
        #region "Propriétés"
        /**
        * \brief Encodage du message
*/
        public string MessageEncoding;

        /**
        * \brief Nom litéral de l'émetteur
*/
        public string MessageFrom;

        /**
        * \brief Adresse de messagerie de l'émetteur
*/
        public string MessageSender;

        public string MessageSubject;


        public string MessageTemplate;


        #endregion
        private void PrepareMessage(string personnaliseNom, string personnaliseUrl, string personnaliseAdresse)
        {
            From = new MailAddress(MessageSender, MessageFrom);
            Sender = new MailAddress(MessageSender, MessageFrom);
            Headers.Add("Message-ID", "<" + MessageFrom + ">");
            SubjectEncoding = Encoding.GetEncoding(MessageEncoding);
            Subject = MessageSubject;
            BodyEncoding = Encoding.GetEncoding(MessageEncoding);
            IsBodyHtml = true;
            // Attachments;

            string _htmlText = MessageTemplate.Replace("<URL>", personnaliseUrl);
            _htmlText = _htmlText.Replace("<NOM>", personnaliseNom);
            _htmlText = _htmlText.Replace("<EMAIL>", personnaliseAdresse);
            ChargeAlternateView(_htmlText, "text/html;charset=utf-8");
            ChargeAlternateView(Html2Text(_htmlText), "text/plain;charset=utf-8");
        }
        private void ChargeAlternateView(string text,string charset)
        {
            var View = AlternateView.CreateAlternateViewFromString(text);
            View.ContentType = new System.Net.Mime.ContentType(charset);
            View.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            AlternateViews.Add(View);
        }

        private void ChargeProprietes(DataTable _composants, string _composantNom)
        {
            MessageEncoding = snippets.ChargeAttribut("MessageEncoding", _composants, _composantNom);
            MessageFrom = snippets.ChargeAttribut("MessageFrom", _composants, _composantNom);
            MessageSender = snippets.ChargeAttribut("MessageSender", _composants, _composantNom);
            MessageSubject = snippets.ChargeAttribut("MessageSubject", _composants, _composantNom);
            MessageTemplate = snippets.ChargeAttribut("MessageTemplate", _composants, _composantNom);
        }


        private string Html2Text(string sourceHTML)
        {
            string reponse = sourceHTML.Replace( "<br>", Environment.NewLine);
            reponse = reponse.Replace( "<br />", Environment.NewLine);
            reponse = reponse.Replace("</p>", Environment.NewLine);
            reponse = reponse.Replace("</div>", Environment.NewLine);
            reponse.Replace( @"<[^>]*>", String.Empty); ;
            return reponse;
        }

    }
}
