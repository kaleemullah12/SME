<%@ Page Title="" Language="C#" MasterPageFile="~/Import.Master" AutoEventWireup="true" CodeBehind="KFCImport.aspx.cs" Inherits="SMEForm.KFCImport" %>
<%@ MasterType VirtualPath="~/Import.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ImportContent" runat="server">
    <div class="ui-widget-header pageHeader" >
        KFC format TimeSheet file Import
    </div>
    <div class="ui-widget-content pageContent fullHeightContent" >
        <div>
             Please select a KFC timesheet:
             <asp:FileUpload ID="fuKFCFile" runat="server" ToolTip="Select the KFC file to import" AllowMultiple="true"/>
             <asp:Button ID="btnUpload" runat = "server" Text="Upload" onclick="btnUpload_Click" />
        </div>
     <hr />
        
        <div id="navmenu" class="menu">
                <ul>
	                <li><a href="<%=ResolveClientUrl("~/KFCImport.aspx")%>" title="KFC">KFC</a></li>
                    <li><a href="<%=ResolveClientUrl("~/KFCConflict.aspx")%>" title="Conflict">Conflict</a></li>
                </ul>
            </div>
        <asp:Literal runat="server" ID="litMsg"></asp:Literal>
        <asp:GridView ID="GVImports" runat="server" Width="100%" CssClass="jsDataTable" AllowSorting="true" AllowPaging="true" DataKeyNames="ID"
                AutoGenerateColumns="false" onrowcancelingedit="GVImports_RowCancelingEdit"
                onrowdeleting="GVImports_RowDeleting" 
                onrowediting="GVImports_RowEditing"
                onrowupdating="GVImports_RowUpdating" 
                onrowdatabound="GVImports_RowDataBound" PageSize="25" OnPageIndexChanging="GVImports_PageIndexChanging" OnSorting="GVImports_Sorting" OnSelectedIndexChanged="GVImports_SelectedIndexChanged">
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
                <asp:TemplateField HeaderText="WorkID" ItemStyle-Width="10%" SortExpression="WorkID">
                    <ItemTemplate>
                        <asp:Label ID="lblWorkID" runat="server" Text="<%# bind('WorkID') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWorkID" Width="100px" runat="server" Text="<%# bind('WorkID') %>" CssClass="textBoxNumber"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%" SortExpression="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text="<%# bind('Date', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDate" Width="100px" runat="server" Text="<%# bind('Date', '{0:dd/MM/yyyy}') %>" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ClockIn" ItemStyle-Width="10%" SortExpression="ClockIn">
                    <ItemTemplate>
                        <asp:Label ID="lblStart" runat="server" Text="<%# bind('ClockIn', '{0:HH\:mm}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStart" Width="100px" runat="server" Text="<%# bind('ClockIn', '{0:HH\:mm}') %>" CssClass="textboxTime"></asp:TextBox>
                    </EditItemTemplate>                
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ClockOut" ItemStyle-Width="10%" SortExpression="ClockOut">
                    <ItemTemplate>
                        <asp:Label ID="lblEnd" runat="server" Text="<%# bind('ClockOut', '{0:HH\:mm}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEnd" Width="100px" runat="server" Text="<%# bind('ClockOut', '{0:HH\:mm}') %>" CssClass="textboxTime"></asp:TextBox>
                    </EditItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PayHour" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblPayHour" runat="server" Text="<%# bind('WorkHour') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPayHour" Width="100px" runat="server" Text="<%# bind('WorkHour') %>" CssClass="textBoxNumber" Enabled="false"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText ="Name" DataField="FirstName"/>
                <asp:TemplateField HeaderText="FileName" ItemStyle-Width="10%" SortExpression="FileName">
                    <ItemTemplate>
                        <asp:Label ID="lblFileName" runat="server" Text="<%# bind('FileName') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ImportedBy" ItemStyle-Width="10%" SortExpression="ImportedBy">
                    <ItemTemplate>
                        <asp:Label ID="lblImportedBy" runat="server" Text="<%# bind('ImportedBy') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField EditText="Edit" CancelText="Cancel" DeleteText="Delete" ButtonType="Link" ShowCancelButton="true" ShowDeleteButton="true" ShowEditButton="true"/>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
