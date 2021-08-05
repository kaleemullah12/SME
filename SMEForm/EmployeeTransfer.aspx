<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeTransfer.aspx.cs" Inherits="SMEForm.EmployeeTransfer" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-widget-header pageHeader" >
        Transfer Employee to a different company
    </div>
    <div class="ui-widget-content ui-corner-bottom pageContent fullHeightContent" >
        <div style="margin:20px 0px 0px 20px;">
            <p>Please select a new company and shop:</p>
            Company: <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList> - 
            Shop: <asp:DropDownList ID="ddlShop" runat="server"></asp:DropDownList> &nbsp;
            <asp:Button ID="btnSearch" Text="Transfer" runat="server" OnClick="btnSearch_Click" />
        </div>
    </div>
</asp:Content>
