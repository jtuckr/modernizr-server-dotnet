using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ModernizrServer
{
    public partial class ModernizrTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dynamic ModernizrDynamic = Session["Modernizr"];
            // Typical usage of testing an individual modernizr capability
            if (ModernizrDynamic.video.h264)
            {
                
            }

            // For testing purposes only - print out all of the key/value objects 
            //  within the Modernizr object
            IDictionary<string, object> ModernizrDict = (IDictionary<string, object>)ModernizrDynamic;
            IDictionary<string, object> ModernizrSubDict;
            StringBuilder sb = new StringBuilder();
            sb.Append("<p>Modernizr values found:</p><ul>");
            foreach (var kvp in ModernizrDict)
            {
                sb.AppendFormat("<li>{0}:", kvp.Key);
                if (kvp.Value.GetType() == typeof(bool))
                {
                    sb.Append(kvp.Value.ToString());
                }
                else
                {
                    sb.Append("<ul>");
                    ModernizrSubDict = (IDictionary<string, object>)kvp.Value;
                    foreach (var subkvp in ModernizrSubDict)
                    {
                        sb.AppendFormat("<li>{0}:{1}</li>", subkvp.Key, subkvp.Value.ToString());
                    }
                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            ModernizrResults.InnerHtml = sb.ToString();
        }
    }
}