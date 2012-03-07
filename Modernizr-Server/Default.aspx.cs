using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Dynamic;

namespace ModernizrServer
{
    public partial class Default : System.Web.UI.Page
    {
        //Configurable private members
        private string _key = "Modernizr";
        private int _MaxCookieAttempts = 3;

        /// <summary>
        /// The magic behind Modernizr-Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[_key] != null && !String.IsNullOrEmpty(Session[_key].ToString()))
            {
                // The Modernizr object is in session and ready to use
                dynamic Modernizr = Session[_key];
                
                if (Modernizr != null)
                {
                    // At this point you are ready to test a particular property 
                    // and do something with it

                }
                else
                {
                    // The Modernizr object cannot be retrieved from session.
                    //
                    // A workaround needs to be put here for this case.
                    // Ex: load a javascript file that checks the modernizr 
                    // capability client-side and does something with it
                }
            }
            else
            {
                // This is likely a new session...
                //
                // Check to see if a Modernizr cookie has been set
                HttpCookieCollection TheCookies = Request.Cookies;
                HttpCookie ModernizrCookie = TheCookies.Get(_key);
                if (ModernizrCookie != null)
                {
                    // Build the dynamic object from cookie found on user's machine
                    dynamic ModernizrDynamic = new ExpandoObject();
                    string[] FeatureArray = ModernizrCookie.Value.Split('|');
                    // Feature Array would look something like [ cssgradients:1, draganddrop:0, ....]
                    string[] FeatureKeyValueArray = new string[2];
                    foreach (string Feature in FeatureArray)
                    {
                        if (!String.IsNullOrWhiteSpace(Feature))
                        {
                            FeatureKeyValueArray = Feature.Split(new[] { ':' }, 2);
                            // FeatureKeyValueArray could be a simply property like [cssgradients,1]
                            // but it could be a more complicated property like [video,/ogg:1/webm:0/h264:1]
                            var f = ModernizrDynamic as IDictionary<String, object>;
                            if (FeatureKeyValueArray[1].StartsWith("/"))
                            {
                                dynamic ValueObject = new ExpandoObject();
                                string[] SubFeatureArray = FeatureKeyValueArray[1].Trim('/').Split('/');
                                //SubFeatureArray would be [ogg:1,webm:1,h264:1]
                                foreach (string SubFeature in SubFeatureArray)
                                {
                                    string[] SubFeatureKeyValueArray = SubFeature.Split(new[] { ':' }, 2);
                                    //SubFeatureKeyValueArray would be [ogg,1] 
                                    var v = ValueObject as IDictionary<String, object>;
                                    //change the 0 or 1 to false or true so that boolean logic can be used 
                                    v[SubFeatureKeyValueArray[0]] = SubFeatureKeyValueArray[1] == "0" ? false : true;
                                }
                                f[FeatureKeyValueArray[0]] = ValueObject;
                            }
                            else
                            {
                                //change the 0 or 1 to false or true so that boolean logic can be used 
                                f[FeatureKeyValueArray[0]] = FeatureKeyValueArray[1] == "0" ? false : true;
                            }
                        }
                    }
                    // Store the generated object in session for later use
                    Session[_key] = ModernizrDynamic;
                }
                else
                {
                    // No Modernizr cookie exists, so we need to try to create one
                    if (Session["ModernizrCookieAttempts"] == null)
                    {
                        // ModernizrCookie has not been called before during this session
                        Session["ModernizrCookieAttempts"] = 1;
                        Response.Redirect("ModernizrCookie.aspx");
                    }
                    else
                    {
                        // ModernizrCookie has been called before during this session and was 
                        // unsuccessful, or the user cleared their cookies during the session
                        // AND the session object was also lost. The latter would be unlikely,
                        // but I guess it's possible...
                        int CookieAttempts = int.Parse(Session["ModernizrCookieAttempts"].ToString());
                        if (CookieAttempts <= _MaxCookieAttempts)
                        {
                            CookieAttempts++;
                            Session["ModernizrCookieAttempts"] = CookieAttempts;
                            Response.Redirect("ModernizrCookie.aspx");
                        }
                        else
                        {
                            // The application has tried several times (depending on 
                            // the configured "MaxCookieAttempts" variable) during this session
                            // to set a cookie and has been unsuccessful. 
                            //
                            // A workaround needs to be put here for these cases.
                            // Ex: load a javascript file that checks the modernizr 
                            // capability client-side and does something with it
                        }
                    }
                }
            }
        }
    }
}