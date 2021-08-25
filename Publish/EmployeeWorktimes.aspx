<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeWorktimes.aspx.cs" Inherits="SMEForm.EmployeeWorktimes" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-widget-header pageHeader" >
        <asp:HyperLink ID="lkDetail" runat="server" Text="<< Back"></asp:HyperLink>
        &nbsp;
        Employee Work Times
    </div>
    <div class="ui-widget-content" style="padding:5px">
        Add New TimeSheet: Date - <asp:TextBox ID="txtDate" runat="server" Text="" Width="100px" CssClass="textBoxDate" required="required"></asp:TextBox>
        &nbsp;Clockin - <asp:TextBox ID="txtClockin" runat="server" Text="" Width="100px" CssClass="textboxTime" required="required"></asp:TextBox>
        &nbsp;Clockout - <asp:TextBox ID="txtClockout" runat="server" Text="" Width="100px" CssClass="textboxTime" required="required"></asp:TextBox>
        &nbsp;<asp:Button ID="btnCreateNew" runat="server" Text="Add Worktime" OnClick="btnCreateNew_Click" />
    </div>
    <div class="ui-widget-content ui-corner-bottom pageContent fullHeightContent" >
        <asp:GridView ID="gvWorkTime" runat="server" Width="100%" AllowSorting="false" AllowPaging="true" DataKeyNames="ID"
                AutoGenerateColumns="false" onrowcancelingedit="gvWorkTime_RowCancelingEdit"
                onrowdeleting="gvWorkTime_RowDeleting" 
                onrowediting="gvWorkTime_RowEditing"
                onrowupdating="gvWorkTime_RowUpdating" 
                onrowdatabound="gvWorkTime_RowDataBound" PageSize="25" OnPageIndexChanging="gvWorkTime_PageIndexChanging" OnSelectedIndexChanged="gvWorkTime_SelectedIndexChanged">
            <HeaderStyle CssClass="ui-widget-header" Height="30px" />
            <FooterStyle CssClass="ui-widget-header" />
            <AlternatingRowStyle CssClass ="ui-widget-content" HorizontalAlign="Center"/>
            <RowStyle CssClass="ui-widget-content" HorizontalAlign="Center"/>
            <EditRowStyle CssClass="ui-widget-content ui-state-focus" HorizontalAlign="Center"/>
            <PagerStyle CssClass="ui-widget-header" HorizontalAlign="Right" />
            <PagerSettings Mode="NumericFirstLast"/>
            <SortedAscendingCellStyle CssClass="ui-widget-content ui-state-focus" />
            <SortedDescendingCellStyle CssClass="ui-widget-content ui-state-focus" />
            <Columns>
                <asp:TemplateField HeaderText="Date" SortExpression="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text="<%# bind('Date', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDate" Width="100px" runat="server" Text="<%# bind('Date', '{0:dd/MM/yyyy}') %>" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ClockIn" SortExpression="ClockIn">
                    <ItemTemplate>
                        <asp:Label ID="lblStart" runat="server" Text="<%# bind('ClockIn', '{0:HH\:mm}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStart" Width="100px" runat="server" Text="<%# bind('ClockIn', '{0:HH\:mm}') %>" CssClass="textboxTime"></asp:TextBox>
                    </EditItemTemplate>                
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ClockOut" ItemStyle-Width="12%" SortExpression="ClockOut">
                    <ItemTemplate>
                        <asp:Label ID="lblEnd" runat="server" Text="<%# bind('ClockOut', '{0:HH\:mm}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEnd" Width="100px" runat="server" Text="<%# bind('ClockOut', '{0:HH\:mm}') %>" CssClass="textboxTime"></asp:TextBox>
                    </EditItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FileName" ItemStyle-Width="12%" SortExpression="Filename">
                    <ItemTemplate>
                        <asp:Label ID="lblFilename" runat="server" Text="<%# bind('Filename') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField EditText="Edit" CancelText="Cancel" DeleteText="Delete" ButtonType="Link" ShowCancelButton="true" ShowDeleteButton="true" ShowEditButton="true"/>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
