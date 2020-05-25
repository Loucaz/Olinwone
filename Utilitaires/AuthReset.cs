using System.Data;

namespace Utilitaires
{
    public class AuthReset
    {
        #region "Propriétés"
        public string PasswordAccessErrorText;
        public string PasswordButtonText;
        public string PasswordEmailTransfer;
        public string PasswordIntroductionText;
        public string PasswordJqueryScript;
        public string PasswordLabel;
        public string PasswordLabelMatch;
        public string PasswordNoteResetFailed;
        public string PasswordTargetPage;
        public string PasswordNote1;
        public string PasswordNote2;
        #endregion

        #region "Constructeur"
        public AuthReset(string composantNom,DataTable composants)
        {
            Snippets snippets = new Snippets();
            PasswordAccessErrorText = snippets.ChargeAttribut("PasswordAccessErrorText", composants, composantNom);
            PasswordButtonText = snippets.ChargeAttribut("PasswordButtonText", composants, composantNom);
            PasswordEmailTransfer = snippets.ChargeAttribut("PasswordEmailTransfer", composants, composantNom);
            PasswordIntroductionText = snippets.ChargeAttribut("PasswordIntroductionText", composants, composantNom);
            PasswordJqueryScript = snippets.ChargeAttribut("PasswordJqueryScript", composants, composantNom);
            PasswordLabel = snippets.ChargeAttribut("PasswordLabel", composants, composantNom);
            PasswordLabelMatch = snippets.ChargeAttribut("PasswordLabelMatch", composants, composantNom);
            PasswordNoteResetFailed = snippets.ChargeAttribut("PasswordNoteResetFailed", composants, composantNom);
            PasswordTargetPage = snippets.ChargeAttribut("PasswordTargetPage", composants, composantNom);
            PasswordNote1 = snippets.ChargeAttribut("PasswordNote1", composants, composantNom);
            PasswordNote2 = snippets.ChargeAttribut("PasswordNote2", composants, composantNom);
        }
        #endregion


    }
}
