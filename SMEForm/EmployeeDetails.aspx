<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="SMEForm.EmployeeDetails" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="ui-widget-header pageHeader" >
        <asp:HyperLink ID="lkDetail" Visible="false" runat="server" Text="<< Back"></asp:HyperLink>
        &nbsp;
        Employee
    </div>
    <div class="ui-widget-content ui-corner-bottom pageContent fullHeightContent" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div class="ui-widget-content ui-state-highlight" id="divWorkID" runat="server" visible="false" style="padding:5px">
                WorkID: <asp:Label ID="lbWorkID" runat="server"></asp:Label>
                &nbsp; SageID <asp:Label ID="lbSageID" runat="server"></asp:Label><asp:TextBox ID="txtSageID" runat="server" TextMode="Number" CssClass="textBoxNumber" Visible="false" Width="100"></asp:TextBox>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Company: <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList> - 
                Shop: <asp:DropDownList ID="ddlShop" runat="server"></asp:DropDownList>
                &nbsp;<asp:CompareValidator ID="cvCompany" runat="server" ControlToValidate="ddlCompany" ValueToCompare="-1" Operator="NotEqual" ErrorMessage="Please select a company!" CssClass="ui-state-error"></asp:CompareValidator>
                &nbsp;<asp:CompareValidator ID="cvShop" runat="server" ControlToValidate="ddlShop" ValueToCompare="-1" Operator="NotEqual" ErrorMessage="Please select a Shop!" CssClass="ui-state-error"></asp:CompareValidator>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Forename: <asp:TextBox ID="txtForeName" runat="server" Width="200" MaxLength="100"></asp:TextBox> &nbsp; Surname:  <asp:TextBox ID="txtSurname" runat="server" Width="200" MaxLength="100"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvForename" runat="server" ControlToValidate="txtForeName" ErrorMessage="Please give Forename!" CssClass="ui-state-error"></asp:RequiredFieldValidator>
                &nbsp;<asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="txtSurname" ErrorMessage="Please give Surname!" CssClass="ui-state-error"></asp:RequiredFieldValidator>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Address1: <asp:TextBox ID="txtAddress1" runat="server" Width="400" MaxLength="255"></asp:TextBox>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Address2: <asp:TextBox ID="txtAddress2" runat="server" Width="400" MaxLength="255"></asp:TextBox>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Address2: <asp:TextBox ID="txtAddress3" runat="server" Width="400" MaxLength="255"></asp:TextBox> &nbsp; Postcode: <asp:TextBox ID="txtPostcode" runat="server" Width="200" MaxLength="100"></asp:TextBox>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Job Title <asp:DropDownList ID="ddlJob" runat="server"></asp:DropDownList> &nbsp; Date of Birth <asp:TextBox ID="txtDoB" runat="server" Width="200" CssClass="textBoxDate"></asp:TextBox>
                &nbsp; Gender: <asp:DropDownList ID="ddlGender" runat="server"><asp:ListItem Text="Male" Value="1"></asp:ListItem><asp:ListItem Text="Female" Value="0"></asp:ListItem></asp:DropDownList>
                &nbsp;<asp:CompareValidator ID="cvJob" runat="server" ControlToValidate="ddlJob" ValueToCompare="-1" Operator="NotEqual" ErrorMessage="Please select a Job Description!" CssClass="ui-state-error"></asp:CompareValidator>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Pay type <asp:DropDownList ID="ddlPay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPay_SelectedIndexChanged"></asp:DropDownList>
                &nbsp; <asp:HyperLink ID="lkHoliday" runat="server" Text="Edit holidays"></asp:HyperLink>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Visa Expire Date <asp:TextBox ID="txtVisaExpire" runat="server" Width="200" CssClass="textBoxDate"></asp:TextBox>&nbsp;
                Visa Apply Date <asp:TextBox ID="txtVisaApply" runat="server" Width="200" CssClass="textBoxDate"></asp:TextBox>
                <asp:HyperLink ID="lkVacation" runat="server" Visible="false" Text="Edit student vacations"></asp:HyperLink>
                &nbsp;<asp:RequiredFieldValidator ID="rfVisaExpire" runat="server" ControlToValidate="txtVisaExpire" ErrorMessage="Please give Visa Expire Date!" Enabled="false" CssClass="ui-state-error"></asp:RequiredFieldValidator>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Start Date: <asp:TextBox ID="txtStartDate" runat="server" Width="200" CssClass="textBoxDate"></asp:TextBox> &nbsp;
                End Date: <asp:TextBox ID="txtEndDate" runat="server" Width="200" CssClass="textBoxDate"></asp:TextBox> &nbsp;
                NI Number <asp:TextBox ID="txtNI" runat="server" Width="100" MaxLength="10"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Please give Start Date!" CssClass="ui-state-error"></asp:RequiredFieldValidator>
            </div>
            <div class="ui-widget-content" style="padding:5px">
                Notes:<br />
                <asp:TextBox ID="txtNotes" runat="server" Width="600px" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
            <div class="ui-widget-content" style="padding:5px; text-align:right; padding-right:100px;">
                <asp:Button runat="server" ID="btnCreateUpdate" Text="Create" OnClick="btnCreateUpdate_Click" />
                <asp:Button runat="server" ID="btnRestart" Text="Create another" Visible="false" OnClick="btnRestart_Click"/>
            </div>
        <script type="text/javascript">
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.textBoxDate').datepicker({ dateFormat: 'dd/mm/yy' });
                $('.textboxTime').timepicker();
            })
        </script>
    </div>
</asp:Content>
