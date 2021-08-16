<%@ Page Title="" Language="C#" MasterPageFile="~/Import.master" AutoEventWireup="true" CodeBehind="StatutaryConflict.aspx.cs" Inherits="SMEForm.StatutaryConflict" %>
<%@ MasterType VirtualPath="~/Import.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ImportContent" runat="server">
 <div class="ui-widget-header pageHeader" >
        Statutary file Import
    </div>
    <div class="ui-widget-content pageContent fullHeightContent" >
        <div>
             Please select a Statutary Sheet:
             <asp:FileUpload ID="StatutaryFiles" runat="server" ToolTip="Select the Statutary file to import" AllowMultiple="true"/>
             <asp:Button ID="btnUpload" runat = "server" Text="Upload" onclick="btnUpload_Click" />
        </div>
      <hr />
        
        <div id="navmenu" class="menu">
                <ul>
	                <li><a href="<%=ResolveClientUrl("~/StatutaryImport.aspx")%>" title="Statutary">Statutary</a></li>
                    <li><a href="<%=ResolveClientUrl("~/StatutaryConflict.aspx")%>" title="Conflict">Conflict</a></li>
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
                 <asp:TemplateField HeaderText="RmfID" ItemStyle-Width="10%" SortExpression="RmfID">
                    <ItemTemplate>
                        <asp:Label ID="lblRmfID" runat="server" Text="<%# bind('RmfID') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRmfID" Width="100px" runat="server" Text="<%# bind('RmfID') %>" CssClass="textBoxNumber"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WorkID" ItemStyle-Width="10%" SortExpression="WorkID">
                    <ItemTemplate>
                        <asp:Label ID="lblWorkID" runat="server" Text="<%# bind('WorkID') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWorkID" Width="100px" runat="server" Text="<%# bind('WorkID') %>" CssClass="textBoxNumber"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FromDate" ItemStyle-Width="10%" SortExpression="FromDate">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text="<%# bind('FromDate', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDate" Width="100px" runat="server" Text="<%# bind('FromDate', '{0:dd/MM/yyyy}') %>" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ToDate" ItemStyle-Width="10%" SortExpression="ToDate">
                    <ItemTemplate>
                        <asp:Label ID="lblDate1" runat="server" Text="<%# bind('ToDate', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDate2" Width="100px" runat="server" Text="<%# bind('ToDate', '{0:dd/MM/yyyy}') %>" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="LeaveYear" ItemStyle-Width="10%" SortExpression="LeaveYear">
                    <ItemTemplate>
                        <asp:Label ID="lblLeaveYear" runat="server" Text="<%# bind('LeaveYear') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLeaveYear" Width="100px" runat="server" Text="<%# bind('LeaveYear') %>" CssClass="textbox"></asp:TextBox>
                    </EditItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SSPSMP" ItemStyle-Width="10%" SortExpression="SSPSMP">
                    <ItemTemplate>
                        <asp:Label ID="lblSSPSMP" runat="server" Text="<%# bind('SSPSMP') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSSPSMP" Width="100px" runat="server" Text="<%# bind('SSPSMP') %>" CssClass="textbox"></asp:TextBox>
                    </EditItemTemplate>  
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="EmpName" ItemStyle-Width="10%" SortExpression="EmpName">
                    <ItemTemplate>
                        <asp:Label ID="lblEmpName" runat="server" Text="<%# bind('EmpName') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEmpName" Width="100px" runat="server" Text="<%# bind('EmpName') %>" CssClass="textbox"></asp:TextBox>
                    </EditItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FileName" ItemStyle-Width="10%" SortExpression="FileName">
                    <ItemTemplate>
                        <asp:Label ID="lblFileName" runat="server" Text="<%# bind('FileName') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFileName" Width="100px" runat="server" Text="<%# bind('FileName') %>" CssClass="textbox"></asp:TextBox>
                    </EditItemTemplate>  
                </asp:TemplateField>
                </Columns>
        </asp:GridView>
    </div>
</asp:Content>
