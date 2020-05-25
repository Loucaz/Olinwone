using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitaires
{
    class AuthLogin
    {
        private Snippets snippets;

        #region "Propriétés"
        public string LoginButtonText;
        public string LoginEmailText;
        public string LoginNoteLoginFailed;
        public string LoginNotePasswordReset;
        public string LoginNoteResetFailed;
        public string LoginForgotEmailTransfer1;
        public string LoginForgotEmailTransfer2;
        public string LoginForgotPage;
        public string LoginForgotText;
        public string LoginHeadText;
        public string LoginHomepage;
        public string LoginJqueryScript;
        public string LoginPasswordText;
        public bool LoginSSOAccess;
        public int LoginSSOApplicationId;
        public string LoginSSOCallbackPage;
        public string LoginSSOKey;
        #endregion

        #region "Constructeur"
        public AuthLogin(string composantNom, DataTable composants)
        {
            snippets = new Snippets();
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
