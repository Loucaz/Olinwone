using System;
using System.Data;

namespace Utilitaires
{
    //////////////////////////////////////////////////
    /// @class RevolutionSlide
    /// @par Description
    /// @note Note
    //////////////////////////////////////////////////
    public class RevolutionSlide
    {
        private DataTable SlideData = new DataTable();
        private DataTable layers = new DataTable();

        //////////////////////////////////////////////////
        /// @brief Code HTML du slide à retenir
        //////////////////////////////////////////////////
        public string HtmlCode;

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        public RevolutionSlide(string _slide, string _dsn)
        {
            SlideData = Charge("select * from wmsCOMPOSANT_ATTRIBUT WHERE composant_id=" + _slide, _dsn);
            layers = Charge("select * from wmsCOMPOSANT WHERE composant_associe=" + _slide + " order by composant_rang", _dsn);
            SlideHtml(_dsn);
        }


        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        private DataTable Charge(string _slide, string _dsn)
        {
            var oData = new DataServices(_dsn, _slide);
            oData.GetStructures();
            DataTable dataTable = oData.UtilityDataTable;
            oData.Dispose();
            return dataTable;
        }

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        private string SlideAttribut(string _rsProperty, string _owProperty)
        {
            string _valeur = SlideData.Select("parametre_nom='" + _owProperty + "'")[0]["parametre_valeur"].ToString();
            if (!string.IsNullOrEmpty(_valeur))
                return " " + _rsProperty + "=\"" + _valeur + "\"";
            else
                return null;
        }

        //////////////////////////////////////////////////
        /// @brief Desc
        //////////////////////////////////////////////////
        private void SlideHtml(string _dsn)
        {
            RevolutionLayer newLayer;
            HtmlCode = "<li";
            HtmlCode += SlideAttribut("data-description", "SlideDescription");
            HtmlCode += SlideAttribut("data-easein", "SlideEasein");
            HtmlCode += SlideAttribut("data-easeout", "SlideEaseout");
            HtmlCode += SlideAttribut("data-hideafterloop", "SlideHideafterloop");
            HtmlCode += SlideAttribut("data-hideslideonmobile", "SlideHideonmobile");
            HtmlCode += SlideAttribut("data-index", "SlideIndex");
            HtmlCode += SlideAttribut("data-link", "SlideLink");
            HtmlCode += SlideAttribut("data-linktoslide", "SlideLinktoslide");
            HtmlCode += SlideAttribut("data-masterspeed", "SlideMasterspeed");
            HtmlCode += SlideAttribut("data-rotate", "SlideRotate");
            HtmlCode += SlideAttribut("data-saveperformance", "SlideSaveperformance");
            HtmlCode += SlideAttribut("data-slotamount", "SlideSlotamount");
            HtmlCode += SlideAttribut("data-target", "SlideTarget");
            HtmlCode += SlideAttribut("data-thumb", "SlideThumb");
            HtmlCode += SlideAttribut("data-title", "SlideTitle");
            HtmlCode += SlideAttribut("data-transition", "SlideTransition");
            HtmlCode += ">" + Environment.NewLine + "<img";
            HtmlCode += SlideAttribut("alt", "SlideImagealt");
            HtmlCode += SlideAttribut("class", "SlideImageclass");
            HtmlCode += SlideAttribut("data-bgfit", "SlideImagefit");
            HtmlCode += SlideAttribut("height", "SlideImageheight");
            HtmlCode += SlideAttribut("data-bgparallax", "SlideImageparallax");
            HtmlCode += SlideAttribut("data-bgposition", "SlideImageposition");
            HtmlCode += SlideAttribut("data-bgrepeat", "SlideImagerepeat");
            HtmlCode += SlideAttribut("src", "SlideImagesrc");
            HtmlCode += SlideAttribut("width", "SlideImagewidth");
            HtmlCode += " data-no-retina>" + Environment.NewLine;

            // boucle sur layers
            foreach (DataRow layer in layers.Rows)
            {
                newLayer = new RevolutionLayer(layer["composant_id"].ToString(), _dsn);
                HtmlCode += newLayer.HtmlCode + Environment.NewLine;
            }

            HtmlCode += "</li>";
        }
    }
}
