<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetupMenu.ascx.cs" Inherits="SMEForm.UserControls.SetupMenu" %>
<script type="text/javascript">
    $(function () {
        $("#SetupMenu").menu();
    });
</script>

<ul id="SetupMenu">
    <li><a href="<%=ResolveClientUrl("~/ExcelInfo.aspx")%>" title="ExcelInfo Setup">Excel Import</a></li>
    <li><a href="<%=ResolveClientUrl("~/MailSetup.aspx")%>" title="Mail Setup">E Mail</a></li>
    <li><a href="<%=ResolveClientUrl("~/Alert.aspx")%>" title="Alert Setup">Alerts</a></li>
</ul>