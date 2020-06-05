using System;
using System.Data;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class AuthLogin
    /// @par Description
    /// @note Note
    //////////////////////////////////////////////////
    public class AuthLogin
    {
        #region "Propriétés"
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginButtonText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginEmailText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginNoteLoginFailed;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginNotePasswordReset;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginNoteResetFailed;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginForgotEmailTransfer1;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginForgotEmailTransfer2;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginForgotPage;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginForgotText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginHeadText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginHomepage;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginJqueryScript;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginPasswordText;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public bool LoginSSOAccess;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public int LoginSSOApplicationId;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginSSOCallbackPage;
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public string LoginSSOKey;
        #endregion

        #region "Constructeur"       
        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public AuthLogin(string composantNom, DataTable composants)
        {
            Snippets snippets = new Snippets();
            LoginButtonText = snippets.ChargeAttribut("LoginButtonText", composants, composantNom);
            LoginEmailText = snippets.ChargeAttribut("LoginEmailText", composants, composantNom);
            LoginNoteLoginFailed = snippets.ChargeAttribut("LoginNoteLoginFailed", composants, composantNom);
            LoginForgotEmailTransfer1 = snippets.ChargeAttribut("LoginForgotEmailTransfer1", composants, composantNom);
            LoginForgotEmailTransfer2 = snippets.ChargeAttribut("LoginForgotEmailTransfer2", composants, composantNom);
            LoginNotePasswordReset = snippets.ChargeAttribut("LoginNotePasswordReset", composants, composantNom);
            LoginForgotPage = snippets.ChargeAttribut("LoginForgotPage", composants, composantNom);
            LoginForgotText = snippets.ChargeAttribut("LoginForgotText", composants, composantNom);
            LoginHeadText = snippets.ChargeAttribut("LoginHeadText", composants, composantNom);
            LoginHomepage = snippets.ChargeAttribut("LoginHomepage", composants, composantNom);
            LoginPasswordText = snippets.ChargeAttribut("LoginPasswordText", composants, composantNom);
            LoginSSOAccess = Convert.ToBoolean(snippets.ChargeAttribut("LoginSSOAccess", composants, composantNom));
            LoginSSOApplicationId = Int32.Parse(snippets.ChargeAttribut("LoginSSOApplicationId", composants, composantNom));
            LoginSSOCallbackPage = snippets.ChargeAttribut("LoginSSOCallbackPage", composants, composantNom);
            LoginSSOKey = snippets.ChargeAttribut("LoginSSOKey", composants, composantNom);
            LoginNoteResetFailed = snippets.ChargeAttribut("LoginNoteResetFailed", composants, composantNom);
            LoginJqueryScript = snippets.ChargeAttribut("LoginJqueryScript", composants, composantNom);
        }
        #endregion

    }
}
