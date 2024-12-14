<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectReportWebForm.aspx.cs" Inherits="ArianaRiseConstructionCompany.Reports.ProjectReportWebForm" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" %>

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
            <rsweb:ReportViewer ZoomMode="Percent" ID="ReportViewer1" runat="server" AsyncRendering="false" Width="800px"  Height="800px"></rsweb:ReportViewer>
        </div>  
        
    </form>
</center>
</html>
