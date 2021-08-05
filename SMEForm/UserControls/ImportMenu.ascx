<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportMenu.ascx.cs" Inherits="SMEForm.UserControls.ImportMenu" %>
<script type="text/javascript">
    $(function () {
        $("#importMenu").menu();
        $("#importMenu1").menu();
        $("#importMenu2").menu();
    });
</script>
<ul id="importMenu">
    <li><a href="<%=ResolveClientUrl("~/KFCImport.aspx")%>" title="KFC">KFC</a></li>
    <li><a href="<%=ResolveClientUrl("~/GenericImport.aspx")%>" title="Generic">Generic</a></li>
</ul>
<ul id="importMenu1">
    <li><a href="<%=ResolveClientUrl("~/HolidaysImport.aspx")%>" title="Holidays">Holidays</a></li>
    <li><a href="<%=ResolveClientUrl("~/StatutaryImport.aspx")%>" title="Statutary">Statutary</a></li>
</ul>
<ul id="importMenu2">
    <li><a href="<%=ResolveClientUrl("~/EmployeeImportList.aspx")%>" title="Employee">Employee</a></li>
</ul>