<%@ Page Title="" Language="C#" MasterPageFile="~/Import.master" AutoEventWireup="true" CodeBehind="HolidaysImport.aspx.cs" Inherits="SMEForm.HolidaysImport" %>
<%@ MasterType VirtualPath="~/Import.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ImportContent" runat="server">
      <div class="ui-widget-header pageHeader" >
        Holidays file Import
    </div>
    <div class="ui-widget-content pageContent fullHeightContent" >
        <div>
             Please select a Holidays TimeSheet:
             <asp:FileUpload ID="HolidaysFile" runat="server" ToolTip="Select the Holidays file to import" AllowMultiple="true"/>
             <asp:Button ID="btnUpload" runat = "server" Text="Upload" onclick="btnUpload_Click" />
        </div>
        <hr />
        
        <div id="navmenu" class="menu">
                <ul>
	                <li><a href="<%=ResolveClientUrl("~/HolidaysImport.aspx")%>" title="Holidays">Holidays</a></li>
                    <li><a href="<%=ResolveClientUrl("~/HolidaysConflict.aspx")%>" title="Conflict">Conflict</a></li>
                </ul>
            </div>
        
        <asp:Literal runat="server" ID="litMsg"></asp:Literal>
        <asp:GridView ID="GVImports" runat="server" Width="100%" CssClass="jsDataTable" AllowSorting="True" AllowPaging="True" DataKeyNames="ID"
                AutoGenerateColumns="False" onrowcancelingedit="GVImports_RowCancelingEdit"
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

<ItemStyle Width="10%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FromDate" ItemStyle-Width="10%" SortExpression="FromDate">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text="<%# bind('FromDate', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDate" Width="100px" runat="server" Text="<%# bind('FromDate', '{0:dd/MM/yyyy}') %>" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ToDate" ItemStyle-Width="10%" SortExpression="ToDate">
                    <ItemTemplate>
                        <asp:Label ID="lblDate1" runat="server" Text="<%# bind('ToDate', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDate2" Width="100px" runat="server" Text="<%# bind('ToDate', '{0:dd/MM/yyyy}') %>" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="Hours" ItemStyle-Width="10%" SortExpression="Hours">
                    <ItemTemplate>
                        <asp:Label ID="lblEnd1" runat="server" Text="<%# bind('Hours') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtHours" Width="100px" runat="server" Text="<%# bind('Hours') %>" CssClass="textbox"></asp:TextBox>
                    </EditItemTemplate>  

<ItemStyle Width="10%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Days" ItemStyle-Width="10%" SortExpression="Days">
                    <ItemTemplate>
                        <asp:Label ID="lblEnd" runat="server" Text="<%# bind('Days') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdays" Width="10%" runat="server" Text="<%# bind('Days') %>" CssClass="textbox"></asp:TextBox>
                    </EditItemTemplate>  

<ItemStyle Width="10%"></ItemStyle>
                </asp:TemplateField>
                <asp:CommandField EditText="Edit" CancelText="Cancel" DeleteText="Delete" ButtonType="Link" ShowCancelButton="true" ShowDeleteButton="true" ShowEditButton="true">
                <ItemStyle Width="10%" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
