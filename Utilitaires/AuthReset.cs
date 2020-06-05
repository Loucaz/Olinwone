using System.Data;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class AuthReset
    /// @par Description
    /// @note Note
    //////////////////////////////////////////////////
    public class AuthReset
    {
        #region "Propriétés"
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordAccessErrorText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordButtonText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordEmailTransfer;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordIntroductionText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordJqueryScript;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordLabel;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordLabelMatch;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordNoteResetFailed;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordTargetPage;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordNote1;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string PasswordNote2;
        #endregion

        #region "Constructeur"
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
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
