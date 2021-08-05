<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdmPayElement.aspx.cs" Inherits="SMEForm.AdmPayElement" %>
<%@ MasterType VirtualPath="~/Admin.Master" %>
<%@ Register Src="~/UserControls/AdminMenu.ascx" TagPrefix="uc1" TagName="AdminMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="ui-widget-content" style="padding:5px">
        Manage pay type to pay element mapping, please select a pay type: <asp:DropDownList ID="ddlPayType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged"></asp:DropDownList>
        &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" style="float:right" OnClick="btnSave_Click" />
    </div>
    <div class="ui-widget-content" style="padding:5px; margin-top:5px;">
        <div>
            Please tick the pay element belongs to the selected pay type and click on "Save":

        </div>
        <asp:CheckBoxList ID="cblPayElement" runat="server">

        </asp:CheckBoxList>
    </div>
</asp:Content>