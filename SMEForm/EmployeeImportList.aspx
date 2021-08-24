<%@ Page Title="" Language="C#" MasterPageFile="~/Import.Master" AutoEventWireup="true" CodeBehind="EmployeeImportList.aspx.cs" Inherits="SMEForm.EmployeeImportList" %>
<%@ MasterType VirtualPath="~/Import.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ImportContent" runat="server">
    <div class="ui-widget-header pageHeader" >
        Sage employee file Import
    </div>
    <div class="ui-widget-content pageContent fullHeightContent" >
        <div  style="padding:5px">
             Import a sage employee file:
             <asp:FileUpload ID="fuSageFile" runat="server" ToolTip="Select the sage employee file to import" />
             <asp:Button ID="btnUpload" runat = "server" Text="Upload" onclick="btnUpload_Click" />
<%--            &nbsp; OR &nbsp; <asp:Button ID="btnCreateNew" Text="Create New Employee" runat="server" OnClick="btnCreateNew_Click" />--%>
        </div>
        <div style="display:none">

            Search By Company <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList> &nbsp; 
            Shop: <asp:DropDownList ID="ddlShop" runat="server"></asp:DropDownList> &nbsp; 
            <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" />            
        </div>
        <hr />
        <asp:GridView ID="GVImports" runat="server" Width="100%"  CssClass="jsDataTable" DataKeyNames="WorkID"  AllowSorting="true" AllowPaging="true"
        AutoGenerateColumns="false" onrowcancelingedit="GVImports_RowCancelingEdit"  onrowediting="GVImports_RowEditing"
        onrowdatabound="GVImports_RowDataBound" PageSize="25" OnPageIndexChanging="GVImports_PageIndexChanging" OnSorting="GVImports_Sorting" OnRowDeleting="GVImports_RowDeleting">
            <HeaderStyle CssClass="ui-widget-header" Height="30px" />
            <FooterStyle CssClass="ui-widget-header" />
            <AlternatingRowStyle CssClass ="ui-widget-content" HorizontalAlign="Center"/>
            <RowStyle CssClass="ui-widget-content" HorizontalAlign="Center"/>
            <EditRowStyle CssClass="ui-widget-content ui-state-focus" HorizontalAlign="Center"/>
            <PagerStyle CssClass="ui-widget-header" HorizontalAlign="Right" />
            <PagerSettings Mode="NumericFirstLast"/>
            <Columns>
                <asp:TemplateField HeaderText="Sage ID" ItemStyle-Width="5%" SortExpression="SageID">
                    <ItemTemplate>
                        <asp:Label ID="lblSageID" runat="server" Text="<%# bind('SageID') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Work ID" ItemStyle-Width="5%" SortExpression="WorkID">
                    <ItemTemplate>
                        <asp:Label ID="lblWorkID" runat="server" Text="<%# bind('WorkID') %>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="JobDesc" ItemStyle-Width="5%" SortExpression="JobDescriptionID">
                    <ItemTemplate>
                        <asp:Label ID="lblJobDescID" runat="server" Text="<%# bind('JobDescriptionID') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlJobDesc" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FirstName" ItemStyle-Width="20%" SortExpression="Forename">
                    <ItemTemplate>
                        <asp:Label ID="lblForename" runat="server" Text="<%# bind('Forename') %>" ToolTip="<%# bind('Forename') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtForename" runat="server" Text="<%# bind('Forename') %>" Width="100px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Surname" ItemStyle-Width="20%" SortExpression="Surname">
                    <ItemTemplate>
                        <asp:Label ID="lblSurname" runat="server" Text="<%# bind('Surname') %>" ToolTip="<%# bind('Surname') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSurname" runat="server" Text="<%# bind('Surname') %>" Width="100px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shop" SortExpression="ShopID">
                    <ItemTemplate>
                        <asp:Label ID="lblShop" runat="server" Text="<%# bind('ShopID') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlShop" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VisaExpire" ItemStyle-Width="10%" SortExpression="VisaExpireDate">
                    <ItemTemplate>
                        <asp:Label ID="lblVisaExpire" runat="server" Text="<%# bind('VisaExpireDate', '{0:dd/MM/yyyy}') %>"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVisaExpire" runat="server" Text="<%# bind('VisaExpireDate', '{0:dd/MM/yyyy}') %>" Width="100px" CssClass="textBoxDate"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>                
                <%--<asp:HyperLinkField Text="Holidays" DataNavigateUrlFormatString="~/EmployeeHolidays.aspx?workID={0}" DataNavigateUrlFields="WorkID" />
                <asp:HyperLinkField Visible="false" Text="Transfer" DataNavigateUrlFormatString="~/EmployeeTransfer.aspx?workID={0}" DataNavigateUrlFields="WorkID" />
                <asp:HyperLinkField Text="TimeSheets" DataNavigateUrlFormatString="~/EmployeeWorktimes.aspx?workID={0}" DataNavigateUrlFields="WorkID" />
                <asp:HyperLinkField Text="Edit" DataNavigateUrlFormatString="~/EmployeeDetails.aspx?workID={0}" DataNavigateUrlFields="WorkID" />--%>
                <%--<asp:CommandField ButtonType="Link" ShowCancelButton="true" itemStyle-Width="10%" ShowDeleteButton="True"></asp:CommandField>--%>
            </Columns>
        </asp:GridView>
    </div>
       
</asp:Content>
