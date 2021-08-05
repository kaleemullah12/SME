<%@ Page Title="" Language="C#" MasterPageFile="~/Export.master" AutoEventWireup="true" CodeBehind="ClearWeeks.aspx.cs" Inherits="SMEForm.ClearWeeks" %>
<%@ MasterType VirtualPath="~/Export.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ExportContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".weekSelect").change(function () {                
                if (!confirm("Are you sure you want to clear a different week rather then current week?")) {
                    var items = $(this).find('option');
                    items.last().prop('selected', 'selected');
                    //$(this).prop('selectedIndex', items.length - 1);
                    return; 
                }
            });
        });
    </script>
    <div class="ui-widget-header pageHeader" >
        CLear weekly timesheet by company
    </div>
    <div class="ui-widget-content pageContent fullHeightContent" >
        <asp:GridView ID="GVCompany" runat="server" Width="100%"  AutoGenerateColumns="false" DataKeyNames="ID"
                onrowdatabound="GVCompany_RowDataBound" OnRowCommand="GVCompany_RowCommand">
            <HeaderStyle CssClass="ui-widget-header" Height="30px" />
            <FooterStyle CssClass="ui-widget-header" />
            <AlternatingRowStyle CssClass ="ui-widget-content" HorizontalAlign="Center"/>
            <RowStyle CssClass="ui-widget-content" HorizontalAlign="Center"/>
            <EditRowStyle CssClass="ui-widget-content ui-state-focus" HorizontalAlign="Center"/>
            <Columns>
                <asp:TemplateField HeaderText="Company ID" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyID" runat="server" Text="<%# bind('ID') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company Name" ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyName" runat="server" Text="<%# bind('Name') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Week" ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlWeek" CssClass="weekSelect" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnClear" runat="server" CommandName="Clear" CommandArgument="<%# bind('ID') %>" Text="Clear Selected Week" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
