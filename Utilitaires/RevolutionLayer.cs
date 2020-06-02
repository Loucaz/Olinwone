using System.Data;

namespace Utilitaires
{

    public partial class RevolutionLayer
    {
        private DataTable LayerData = new DataTable();

        public RevolutionLayer(string _layer, string _dsn)
        {
            ChargeData(_layer, _dsn);
            LayerHtml();
        }

        /// <summary>
        /// Code HTML du layer à retenir
        /// </summary>
        public string HtmlCode;

        private void ChargeData(string _layer, string _dsn)
        {
            var oData = new DataServices(_dsn, "select * from wmsCOMPOSANT_ATTRIBUT WHERE composant_id=" + _layer);
            oData.GetStructures();
            LayerData = oData.UtilityDataTable;
            oData.Dispose();
        }

        private string LayerAttribut(string _rsProperty, string _owProperty)
        {
            string _valeur = LayerData.Select("parametre_nom='" + _owProperty + "'")[0]["parametre_valeur"].ToString();
            if (!string.IsNullOrEmpty(_valeur))
                return " " + _rsProperty + "=\"" + _valeur + "\"";
            else
                return null;
        }

        private string LayerAttributSpecial(string _rsProperty, string _owProperty)
        {
            string _valeur = LayerData.Select("parametre_nom='" + _owProperty + "'")[0]["parametre_valeur"].ToString();
            if (!string.IsNullOrEmpty(_valeur))
                return " " + _rsProperty + "='" + _valeur + "'";
            else
                return null;
        }

        private string LayerValue(string _owProperty)
        {
            string _valeur = LayerData.Select("parametre_nom='" + _owProperty + "'")[0]["parametre_valeur"].ToString();
            if (!string.IsNullOrEmpty(_valeur))
                return _valeur;
            else
                return null;
        }

        private void LayerHtml()
        {
            HtmlCode = "<div";
            HtmlCode += LayerAttributSpecial("data-actions", "LayerActions");
            HtmlCode += LayerAttribut("data-audio", "LayerAudio");
            HtmlCode += LayerAttribut("data-autoplay", "LayerAutoplay");
            HtmlCode += LayerAttribut("data-autoplayonlyfirsttime", "LayerAutoplayfirsttime");
            HtmlCode += LayerAttribut("data-basealign", "LayerBasealign");
            HtmlCode += LayerAttribut("class", "LayerClass");
            HtmlCode += LayerAttribut("data-color", "LayerColor");
            HtmlCode += LayerAttribut("data-columnwidth", "LayerColumnwidth");
            HtmlCode += LayerAttribut("data-elementdelay", "LayerElementdelay");
            HtmlCode += LayerAttribut("data-endelementdelay", "LayerEndelementdelay");
            HtmlCode += LayerAttribut("data-fontsize", "LayerFontsize");
            HtmlCode += LayerAttribut("data-fontweight", "LayerFontweight");
            HtmlCode += LayerAttribut("data-frames", "LayerFrames");
            HtmlCode += LayerAttribut("data-height", "LayerHeight");
            HtmlCode += LayerAttribut("data-hoffset", "LayerHoffset");
            HtmlCode += LayerAttribut("id", "LayerId");
            HtmlCode += LayerAttribut("data-lineheight", "LayerLineheight");
            HtmlCode += LayerAttribut("data-marginbottom", "LayerMarginbottom");
            HtmlCode += LayerAttribut("data-marginleft", "LayerMarginleft");
            HtmlCode += LayerAttribut("data-marginright", "LayerMarginright");
            HtmlCode += LayerAttribut("data-margintop", "LayerMargintop");
            HtmlCode += LayerAttribut("data-paddingbottom", "LayerPaddingbottom");
            HtmlCode += LayerAttribut("data-paddingleft", "LayerPaddingleft");
            HtmlCode += LayerAttribut("data-paddingright", "LayerPaddingright");
            HtmlCode += LayerAttribut("data-paddingtop", "LayerPaddingtop");
            HtmlCode += LayerAttribut("data-responsive", "LayerResponsive");
            HtmlCode += LayerAttribut("data-responsive_offset", "LayerResponsiveoffset");
            HtmlCode += LayerAttribut("data-splitin", "LayerSplitin");
            HtmlCode += LayerAttribut("data-splitout", "LayerSplitout");
            HtmlCode += LayerAttribut("data-start", "LayerStart");
            HtmlCode += LayerAttribut("style", "LayerStyle");
            HtmlCode += LayerAttribut("data-svg_idle", "LayerSvgidle");
            HtmlCode += LayerAttribut("data-svg_src", "LayerSvgsrc");
            HtmlCode += LayerAttribut("data-textAlign", "LayerTextAlign");
            HtmlCode += LayerAttribut("data-transform_idle", "LayerTransformidle");
            HtmlCode += LayerAttribut("data-transform_in", "LayerTransformin");
            HtmlCode += LayerAttribut("data-transform_out", "LayerTransformout");
            HtmlCode += LayerAttribut("data-type", "LayerType");
            HtmlCode += LayerAttribut("data-allowfullscreen", "LayerVideoallowfullscreen");
            HtmlCode += LayerAttribut("data-aspectratio", "LayerVideoaspectratio");
            HtmlCode += LayerAttribut("data-videoattributes", "LayerVideoattributes");
            HtmlCode += LayerAttribut("data-videocontrols", "LayerVideocontrols");
            HtmlCode += LayerAttribut("data-videoendat", "LayerVideoend");
            HtmlCode += LayerAttribut("data-forceCover", "LayerVideoforcecover");
            HtmlCode += LayerAttribut("data-forcerewind", "LayerVideoforcerewind");
            HtmlCode += LayerAttribut("data-videoheight", "LayerVideoheight");
            HtmlCode += LayerAttribut("data-videoloop", "LayerVideoloop");
            HtmlCode += LayerAttribut("data-videomp4", "LayerVideomp4");
            HtmlCode += LayerAttribut("data-nextslideatsend", "LayerVideonextslideatsend");
            HtmlCode += LayerAttribut("data-videopreload", "LayerVideopreload");
            HtmlCode += LayerAttribut("data-videopreloadwait", "LayerVideopreloadwait");
            HtmlCode += LayerAttribut("data-videorate", "LayerVideorate");
            HtmlCode += LayerAttribut("data-videostartat", "LayerVideostart");
            HtmlCode += LayerAttribut("data-vimeoid", "LayerVideovimeoid");
            HtmlCode += LayerAttribut("data-volume", "LayerVideoVolume");
            HtmlCode += LayerAttribut("data-videowidth", "LayerVideowidth");
            HtmlCode += LayerAttribut("data-ytid", "LayerVideoytid");
            HtmlCode += LayerAttribut("data-voffset", "LayerVoffset");
            HtmlCode += LayerAttribut("data-whiteboard", "LayerWhiteboard");
            HtmlCode += LayerAttribut("data-whitespace", "LayerWhitespace");
            HtmlCode += LayerAttribut("data-width", "LayerWidth");
            HtmlCode += LayerAttribut("data-x", "LayerX");
            HtmlCode += LayerAttribut("data-y", "LayerY");
            HtmlCode += LayerAttribut("data-transform_hover", "LayerTransformhover");
            HtmlCode += LayerAttribut("data-style_hover", "LayerStylehover");
            HtmlCode += LayerAttribut("data-mask_in", "LayerMaskin");
            HtmlCode += LayerAttribut("data-mask_out", "LayerMaskout");
            HtmlCode += ">" + LayerValue("LayerHtml") + "</div>";
        }

    }
}
