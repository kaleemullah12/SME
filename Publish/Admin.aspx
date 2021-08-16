<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SMEForm.Admin" %>
<%@ MasterType VirtualPath="~/Admin.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="ui-widget-header pageHeader" >
        System User management
    </div>
    <div class="ui-widget-content" style="padding:5px">
        Add New User - Username: <asp:TextBox ID="txtUsername" runat="server" Text="" Width="100px" Required="Required"></asp:TextBox>
        &nbsp;- password: <asp:TextBox ID="txtPassword" runat="server" Text="" Width="100px" Required="Required"></asp:TextBox>
        &nbsp;- Email: <asp:TextBox ID="txtEmail" runat="server" Text="" Width="100px" Required="Required"></asp:TextBox>
        &nbsp;- role <asp:DropDownList ID="ddlRole" runat="server"></asp:DropDownList>                    
        &nbsp;<asp:Button ID="btnCreateNew" runat="server" Text="Add User" OnClick="btnCreateNew_Click"/>
    </div>
    <asp:GridView ID="gvUsers" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnRowDataBound="gvUsers_RowDataBound" OnRowDeleting="gvUsers_RowDeleting">
        <HeaderStyle CssClass="ui-widget-header" Height="30px" />
        <FooterStyle CssClass="ui-widget-header" HorizontalAlign="Center"/>
        <AlternatingRowStyle CssClass ="ui-widget-content" HorizontalAlign="Center"/>
        <RowStyle CssClass="ui-widget-content" HorizontalAlign="Center"/>
        <EditRowStyle CssClass="ui-widget-content ui-state-focus" HorizontalAlign="Center"/>
        <PagerStyle CssClass="ui-widget-header" HorizontalAlign="Right" />
        <PagerSettings Mode="NumericFirstLast"/>
        <Columns>
            <asp:TemplateField HeaderText="Username">
                <ItemTemplate>
                    <asp:Label ID="lblUsername" runat="server" Text="<%# bind('Username') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Password">
                <ItemTemplate>
                    <asp:Label ID="lblPassword" runat="server" Text="********"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPassword" runat="server" Text="" Width="100px"  Required="Required"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text="<%# bind('Email') %>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text="<%# bind('Email') %>" Width="100px"  Required="Required"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created by">
                <ItemTemplate>
                    <asp:Label ID="lblCreateby" runat="server" Text="<%# bind('CreatedBy') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created On">
                <ItemTemplate>
                    <asp:Label ID="lblCreatedTime" runat="server" Text="<%# bind('CreatedTime', '{0:dd/MM/yyyy}') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Role ID">
                <ItemTemplate>
                    <asp:Label ID="lblRole" runat="server" Text="<%# bind('RoleID') %>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlRole" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField HeaderText="Is Active" DataField="IsActive" />
            <asp:CommandField ButtonType="Link" ShowCancelButton="true" ShowDeleteButton="true" ShowEditButton="true"  ItemStyle-Width="10%"></asp:CommandField>
        </Columns>
    </asp:GridView>
</asp:Content>
