# modernizr-server-dotnet

A library for accessing browser capabilities by [Modernizr](http://modernizr.com) on the server-side. 

Loosely translated from James Pearce's [modernizr-server](https://github.com/jamesgpearce/modernizr-server) into .NET 4.0. Currently this is .NET forms version. I plan on making MVC versions as well.

## How to Use


## How it Works
Much like the PHP implementation, the .NET version uses a combination of a cookie and the user's browsing session. The first time a user visits the site (specifically the default.aspx page), the server will redirect to a ModernizrCookie.aspx page, which is responsible for running the Modernizr JS and creating a serialized cookie that holds the browsers capabilities. This cookie is set to expire after 24 hours.

After a cookie has been created (and on any new sessions for that particular user on that particular machine while the cookie is still valid) the server parses the cookie's data and places it in a dynamic object. The object is then stored in session and is available to any requests in that session. 

A bit of an improvement from the PHP version...If a cookie cannot be created the first time during a particular session, it will try again up to a set number of times. If it is still unsuccesful, the Modernizr capabilities will simply not be available to use. Therefore, it is important to always have a fallback in case the Modernizr session object is not available, or a particular 

## To-do's, etc.

