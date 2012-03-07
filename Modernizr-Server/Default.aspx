<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ModernizrServer.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modernizr Server</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Modernizr Server</h1>
        <p>Modernizr Server allows for an application to access browser capabilities detected
            by Modernizr in server-side code.</p>
        
        <h2>Usage</h2>
        <p>Include the ModernizrCookie.aspx page in your application, as well as the logic
            found in the code-behind of this page. See the <a href="VideoTest.aspx">Video Test</a> 
            page for sample usage.</p>
        
        <h2>Modernizr Features</h2>
        <p>See a <a href="ModernizrTestPage.aspx">full list of the features</a> detected by
            Modernizr and which of those your browser supports!</p>
    </div>
    </form>
</body>
</html>
