<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.ascx.cs" Inherits="SMEForm.UserControls.AdminMenu" %>
<script type="text/javascript">
    $(function () {
        $("#adminMenu").menu();
    });
</script>
<ul id="adminMenu">
    <li><a href="<%=ResolveClientUrl("~/Admin.aspx")%>" title="Add/Edit users">Users</a></li>
    <li><a href="<%=ResolveClientUrl("~/Companies.aspx")%>" title="Add/Edit companies.">Company</a></li>
    <li><a href="<%=ResolveClientUrl("~/Shops.aspx")%>" title="Add/Edit shops.">Shop</a></li>
    <li><a href="<%=ResolveClientUrl("~/AdmPayElement.aspx")%>" title="Add/Edit pay type and pay elements.">Payments</a></li>
</ul>