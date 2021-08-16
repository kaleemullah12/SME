<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="Shops.aspx.cs" Inherits="SMEForm.Shops" %>
<%@ MasterType VirtualPath="~/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="ui-widget-header pageHeader">
        Create a new shop or edit existing shop
    </div>
    <div class="ui-widget-content" style="padding:5px">
        Please select a company: <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>        
    </div>
        <div class="ui-widget-content" style="padding:5px">
        Add New shop  - ID: <asp:TextBox ID="txtID" runat="server" Text="" Width="100px" Required="Required"></asp:TextBox>
        &nbsp;- Name: <asp:TextBox ID="txtName" runat="server" Text="" Width="100px" Required="Required"></asp:TextBox>
        &nbsp;- Brand <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>                    
        &nbsp;<asp:Button ID="btnCreateNew" runat="server" Text="Add Company" OnClick="btnCreateNew_Click"/>
    </div>
    <asp:GridView ID="gvCompanies" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" OnRowCancelingEdit="gvCompanies_RowCancelingEdit" OnRowEditing="gvCompanies_RowEditing" OnRowUpdating="gvCompanies_RowUpdating" OnRowDataBound="gvCompanies_RowDataBound" OnRowDeleting="gvCompanies_RowDeleting">
        <HeaderStyle CssClass="ui-widget-header" Height="30px" />
        <FooterStyle CssClass="ui-widget-header" HorizontalAlign="Center"/>
        <AlternatingRowStyle CssClass ="ui-widget-content" HorizontalAlign="Center"/>
        <RowStyle CssClass="ui-widget-content" HorizontalAlign="Center"/>
        <EditRowStyle CssClass="ui-widget-content ui-state-focus" HorizontalAlign="Center"/>
        <PagerStyle CssClass="ui-widget-header" HorizontalAlign="Right" />
        <PagerSettings Mode="NumericFirstLast"/>
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text="<%# bind('ID') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text="<%# bind('Name') %>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" Text="<%# bind('Name') %>"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Company">
                <ItemTemplate>
                    <asp:Label ID="lblCompany" runat="server" Text="<%# bind('Company.Name') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Brand">
                <ItemTemplate>
                    <asp:Label ID="lblBrand" runat="server" Text="<%# bind('Brand.Name') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowCancelButton="true" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="10%"></asp:CommandField>
            <asp:HyperLinkField HeaderText="Edit Employees" Text="Employees" DataNavigateUrlFormatString="~/EmployeeList.aspx?companyID={0}&shopID={1}" DataNavigateUrlFields="CompanyID,ID" ItemStyle-Width="10%"/>
        </Columns>
    </asp:GridView>
</asp:Content>
