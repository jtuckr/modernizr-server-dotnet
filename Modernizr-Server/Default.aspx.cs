using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Dynamic;
using System.Web.UI.HtmlControls;

namespace ModernizrServer
{
    public partial class Default : System.Web.UI.Page
    {
        private string _key = "Modernizr";
        private int _MaxCookieAttempts = 3;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[_key] != null && !String.IsNullOrEmpty(Session[_key].ToString()))
            {
                // The Modernizr object is in session and ready to use
                dynamic Modernizr = Session["Modernizr"];
                // Typical usage of testing an individual modernizr capability
                if (Modernizr.video != null)
                {
                    HtmlGenericControl VideoControl = new HtmlGenericControl("video");
                    VideoControl.Attributes.Add("autoplay", "autoplay");
                    VideoControl.Attributes.Add("controls", "controls");
                    if (Modernizr.video.h264)
                    {
                        VideoCapabilities.Text += "Your browser supports h.264. ";
                        HtmlGenericControl VideoSource = new HtmlGenericControl("source");
                        VideoSource.Attributes.Add("src", "assets/video/h.264/gizmo.mp4");
                        VideoControl.Controls.Add(VideoSource);
                    }
                    if (Modernizr.video.webm)
                    {
                        VideoCapabilities.Text += "Your browser supports webm. ";
                        HtmlGenericControl VideoSource = new HtmlGenericControl("source");
                        VideoSource.Attributes.Add("src", "assets/video/webm/gizmo.webm");
                        VideoSource.Attributes.Add("type", "video/webm");
                        VideoControl.Controls.Add(VideoSource);
                    }
                    if (Modernizr.video.ogg)
                    {
                        VideoCapabilities.Text += "Your browser supports ogg. ";
                        HtmlGenericControl VideoSource = new HtmlGenericControl("source");
                        VideoSource.Attributes.Add("src", "assets/video/ogg/gizmo.ogv");
                        VideoSource.Attributes.Add("type", "video/ogg");
                        VideoControl.Controls.Add(VideoSource);
                    }
                    else
                    {
                        VideoCapabilities.Text = "Your browser does not support h.264, webm or ogg video formats";
                    }
                    VideoPlaceHolder.Controls.Add(VideoControl);
                }
                else
                {
                    VideoCapabilities.Text = "Your browser does not support h.264, webm or ogg video formats";
                }
            }
            else
            {
                // Build the object from cookie found on user's machine
                HttpCookieCollection TheCookies = Request.Cookies;
                HttpCookie ModernizrCookie = TheCookies.Get(_key);
                if (ModernizrCookie != null)
                {
                    dynamic ModernizrDynamic = new ExpandoObject();
                    string[] FeatureArray = ModernizrCookie.Value.Split('|');
                    string[] FeatureKeyValueArray = new string[2];
                    foreach (string Feature in FeatureArray)
                    {
                        if (!String.IsNullOrWhiteSpace(Feature))
                        {
                            FeatureKeyValueArray = Feature.Split(new[] { ':' }, 2);
                            var f = ModernizrDynamic as IDictionary<String, object>;
                            if (FeatureKeyValueArray[1].StartsWith("/"))
                            {
                                dynamic ValueObject = new ExpandoObject();
                                string[] SubFeatureArray = FeatureKeyValueArray[1].Split('/');
                                foreach (string SubFeature in SubFeatureArray)
                                {
                                    if (!String.IsNullOrWhiteSpace(SubFeature))
                                    {
                                        string[] SubFeatureKeyValueArray = SubFeature.Split(new[] { ':' }, 2);
                                        var v = ValueObject as IDictionary<String, object>;
                                        v[SubFeatureKeyValueArray[0]] = SubFeatureKeyValueArray[1] == "0" ? false : true;
                                    }
                                }
                                f[FeatureKeyValueArray[0]] = ValueObject;
                            }
                            else
                            {
                                f[FeatureKeyValueArray[0]] = FeatureKeyValueArray[1] == "0" ? false : true;
                            }
                        }
                    }
                    // Store the generated object in session for later use
                    Session[_key] = ModernizrDynamic;
                }
                else
                {
                    if (Session["ModernizrCookieAttempts"] == null)
                    {
                        Session["ModernizrCookieAttempts"] = 1;
                        Response.Redirect("ModernizrCookie.aspx");
                    }
                    else
                    {
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
                            // the configured "MaxCookieAttempts" variable) to set a cookie 
                            // and has been unsuccessful. 
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