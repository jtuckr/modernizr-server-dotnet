<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoTest.aspx.cs" Inherits="ModernizrServer.VideoTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modernizr Server - Video Test</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Modernizr Video Test</h1>
        <p>A sample video page that demonstrates how Modernizr Server would test
        for video support and include the appropriate source(s). Video samples are from 
        <a href="http://www.808.dk/?code-html-5-video">808.dk</a> and are for demonstration 
        purposes only.</p>
        <p><strong><asp:Literal ID="VideoCapabilities" runat="server"></asp:Literal></strong></p>
        <asp:PlaceHolder ID="VideoPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
