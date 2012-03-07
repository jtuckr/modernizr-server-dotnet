<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModernizrCookie.aspx.cs" Inherits="ModernizrServer.ModernizrCookie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/modernizr.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server"></form>
    <script>
        window.onload = function () {
            var m = Modernizr,
                f,
                t,
                c = '',
                dt,
                expires;
            dt = new Date();
            dt.setTime(dt.getTime() + (1 * 24 * 60 * 60 * 1000));
            expires = "; expires=" + dt.toGMTString(); ;
            for (f in m) {
                if (f[0] == '_') {
                    continue
                }
                t = typeof m[f];
                if (t == 'function') {
                    continue
                }
                c += (c ? '|' : 'Modernizr=') + f + ':';
                if (t == 'object') {
                    for (var s in m[f]) {
                        c += '/' + s + ':' + (m[f][s] ? '1' : '0')
                    }
                } else {
                    c += m[f] ? '1' : '0'
                }
            }
            c += ';path=/';
            try {
                document.cookie = c + expires;
            } catch (e) { }
            window.top.location = 'default.aspx';
        };
    </script>
</body>
</html>
