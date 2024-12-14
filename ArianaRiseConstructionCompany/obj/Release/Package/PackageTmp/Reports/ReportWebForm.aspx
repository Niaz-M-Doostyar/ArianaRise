<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportWebForm.aspx.cs" Inherits="ArianaRiseConstructionCompany.Reports.ReportWebForm" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body dir="rtl">
<center>
    <form id="formCustomerReport" runat="server">  
        <div class="col-sm-12">  
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" Width="800px"  Height="800px"></rsweb:ReportViewer>
        </div>  
        
    </form>
</center>
</body>
</html>
