﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Export.master" AutoEventWireup="true" CodeBehind="CostaExport.aspx.cs" Inherits="SMEForm.CostaExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ExportContent" runat="server">
    <div class="ui-widget-header pageHeader" >
        Costa File Export
    </div>
    <div class="ui-widget-content pageContent fullHeightContent" >
        <p>Please select a week and pay element group to export payroll files, you can go back up to 5 weeks.</p>
        <p>Week: <asp:DropDownList ID="ddlWeek" runat="server"></asp:DropDownList>&nbsp; 
            Pay element group:<asp:DropDownList ID="ddlPayElementGroup" runat="server"></asp:DropDownList>
            <asp:Button ID="btnRunExportAll" runat="server" Text="Run" OnClick="btnRunExportAll_Click" />
            <a href="javascript:OpenReport('WeeklyTimeSheetQA');" title="Open timesheet report in a new window">Open Report</a>
        </p>
        <br />
        <div style="max-height:350px; overflow:auto;">
            <asp:Literal ID="liExportSummary" runat="server"></asp:Literal>
        </div>      
    </div>
</asp:Content>
