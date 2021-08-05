<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportMenu.ascx.cs" Inherits="SMEForm.UserControls.ExportMenu" %>
<script type="text/javascript">
    $(function () {
        $("#exportMenu").menu();
    });
</script>
<ul id="exportMenu">
    <li><a href="<%=ResolveClientUrl("~/FileExport.aspx")%>" title="Export">KFC</a></li>
    <li><a href="<%=ResolveClientUrl("~/PizzaExport.aspx")%>" title="Export">Pizza</a></li>
    <li><a href="<%=ResolveClientUrl("~/CostaExport.aspx")%>" title="Costa">Costa</a></li>
    <li><a href="<%=ResolveClientUrl("~/ClearWeeks.aspx")%>" title="Export">Clear weeks</a></li>
</ul>