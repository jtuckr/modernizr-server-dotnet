# modernizr-server-dotnet

A library for accessing browser capabilities by [Modernizr](http://modernizr.com) on the server. 

Adapted from James Pearce's [modernizr-server](https://github.com/jamesgpearce/modernizr-server) into ASP.NET 4.0 with C# code-behind. Currently this is ASP.NET forms version. I plan on making an MVC version as well (see to-do's below).

## How to Use
For existing projects, include the server-side code on Default.aspx.cs in the Page_Load method of your default page. For new projects, you can simply copy Default.aspx and Default.aspx.cs and use that as a start to your default page.

For new or existing projects, copy the ModernizrCookie.aspx and ModernizrCookie.aspx.cs files to your project. Also, grab the latest modernizr JS file from [Modernizr.com](http://modernizr.com) and include it in your project in the /js folder.

In your web.config file, make sure to configure the session state if you have a particular preference on the session state mode. See [MSDN](msdn.microsoft.com/en-us/library/ms972429.aspx) for a brief overview of the session state configurations. I have the following (pretty close to default) configuration in the project:

	<sessionState mode="InProc" cookieless="false" timeout="30" />
	
This mode is the fastest, and uses a cookie to store the SessionId. I also have a sliding timeout of 30 minutes.

After you have completed the above, on any page in your site, you can retrieve the Modernizr session object and store it in a dynamic variable like so:

	dynamic Modernizr = Session["Modernizr"];

You can see an example of this in VideoTest.aspx:

	dynamic Modernizr = Session["Modernizr"];
	if (Modernizr != null)
	{
		// Session object was succesfully retrieved and set
		if (Modernizr.video != null)
		{
			// at least one video property is supported
			if (Modernizr.video.h264)
			{
				// browser supports h.264
			}
			if (Modernizr.video.webm)
			{
				// browser supports webm
			}
			if (Modernizr.video.ogg)
			{
				// browser supports ogg
			}
		}
	}

## How it Works
Much like the PHP implementation, the .NET version uses a combination of a cookie and the user's browsing session. The first time a user visits the site (specifically the default.aspx page), the server will redirect to a ModernizrCookie.aspx page, which is responsible for running the Modernizr JS and creating a serialized cookie that holds the browsers capabilities. This cookie is set to expire after 24 hours.

After a cookie has been created (and on any new sessions for that particular user on that particular machine while the cookie is still valid) the server parses the cookie's data and places it in a dynamic object. The object is then stored in session and is available to any requests in that session. 

A bit of an improvement from the PHP version...If a cookie cannot be created the first time during a particular session, it will try again up to a set number of times. If it is still unsuccesful, the Modernizr capabilities will simply not be available to use. Therefore, it is important to always have a fallback in case the Modernizr session object is not available, or a particular 

## To-do's, etc.
1. This is not compatible with earlier versions of .NET (3.5, 2.0, etc.). The dynamic object I am using was introduced in .NET 4.0
2. Use with caution. I have not evaluated this for speed and efficiency in a production environment. This is merely for demonstration.
3. I plan on making an MVC version of this so it can be used in MVC applications.