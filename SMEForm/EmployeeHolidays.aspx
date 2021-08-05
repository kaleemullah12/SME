<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeHolidays.aspx.cs" Inherits="SMEForm.EmployeeHolidays" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-widget-header pageHeader" >
        <asp:HyperLink ID="lkDetail" runat="server" Text="<< Back"></asp:HyperLink>
        &nbsp;Employee holidays        
    </div>
    <div class="ui-widget-content" style="padding:5px">
        Add New holiday From <asp:TextBox ID="txtFromDate" runat="server" Text="" Width="100px" CssClass="textBoxDate" required="required"></asp:TextBox>
        &nbsp;To <asp:TextBox ID="txtToDate" runat="server" Text="" Width="100px" CssClass="textBoxDate" required="required"></asp:TextBox>
        &nbsp;<asp:Button ID="btnCreateNew" runat="server" Text="Add Holiday" OnClick="btnCreateNew_Click" />
    </div>
    <div class="ui-widget-content" style="padding:5px">
        YTD Holiday accrual (in hours): <asp:Label ID="lbHolidayAccrual" runat="server" Font-Bold="true"></asp:Label>
    </div>
    <div class="ui-widget-content" style="padding:5px">
        TYD Holiday Booked (in hours): <asp:Label ID="lblHolidayBooked" runat="server" Font-Bold="true"></asp:Label>
    </div>
    <div class="ui-widget-content ui-corner-bottom pageContent fullHeightContent" >
        <asp:GridView ID="gvHolidays" runat="server" DataKeyNames="ID" Width="100%" AutoGenerateColumns="false" OnRowCancelingEdit="gvHolidays_RowCancelingEdit" OnRowDeleting="gvHolidays_RowDeleting" OnRowEditing="gvHolidays_RowEditing" OnRowUpdating="gvHolidays_RowUpdating">
            <HeaderStyle CssClass="ui-widget-header" Height="30px" />
            <FooterStyle CssClass="ui-widget-header" HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass ="ui-widget-content" HorizontalAlign="Center"/>
            <RowStyle CssClass="ui-widget-content" HorizontalAlign="Center"/>
            <EditRowStyle CssClass="ui-widget-content ui-state-focus" HorizontalAlign="Center"/>
            <PagerStyle CssClass="ui-widget-header" HorizontalAlign="Right" />
            <PagerSettings Mode="NumericFirstLast"/>
            <Columns>
                <asp:TemplateField HeaderText="From">
                    <ItemTemplate>
                        <asp:Label ID="lblFrom" runat="server" Text="<%# bind('FromDate', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFromDate" runat="server" Text="<%# bind('FromDate', '{0:dd/MM/yyyy}') %>" Width="100px" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To">
                    <ItemTemplate>
                        <asp:Label ID="lblTo" runat="server" Text="<%# bind('ToDate', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtToDate" runat="server" Text="<%# bind('ToDate', '{0:dd/MM/yyyy}') %>" Width="100px" CssClass="textBoxDate"></asp:TextBox>
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
                <asp:TemplateField HeaderText="Modified by">
                    <ItemTemplate>
                        <asp:Label ID="lblModifiedby" runat="server" Text="<%# bind('ModifiedBy') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modified on">
                    <ItemTemplate>
                        <asp:Label ID="lblModifiedTime" runat="server" Text="<%# bind('ModifiedTime', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Link" ShowCancelButton="true" ShowEditButton="true"  ItemStyle-Width="10%" ShowDeleteButton="True"></asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

