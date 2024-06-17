<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutputES_PDF.aspx.cs" Inherits="Lusitania.Simuladores.WebSite.Pages.OutputES_PDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
    <%--
        <link href="../Script/css jq ui/ui-lightness/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    --%>
    
    <link href="../Script/Custom popUp/popUp.css" rel="stylesheet" type="text/css" />
    
    <!--
        <script type="text/javascript" src="/Script/jquery-1.8.3.js"></script>
        <script type="text/javascript" src="/Script/jquery-ui-1.10.4.custom.js"></script> 
    -->
    
    <script type="text/javascript" src="Script/jquery-3.6.0.js"></script>
    <script type="text/javascript" src="Script/jquery-ui-1.13.1.custom/jquery-ui.js"></script>
    
    <script type="text/javascript" src="Script/Custom popUp/popUp.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ajax:scriptmanager id="ScriptManager1" runat="server" enablepartialrendering="True"
            enablescriptglobalization="True" enablescriptlocalization="True" scriptmode="Release">            
        </ajax:scriptmanager>        
    </div>
    </form>
</body>
</html>