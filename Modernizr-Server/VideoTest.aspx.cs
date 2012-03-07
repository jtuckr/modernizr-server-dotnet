using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ModernizrServer
{
    public partial class VideoTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dynamic Modernizr = Session["Modernizr"];

            if (Modernizr != null)
            {
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
                // End Example 
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
    }
}