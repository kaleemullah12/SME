﻿<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SMEForm.Account.ChangePassword" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/Scripts/login.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="loginContainer">
    <div class="ui-widget-header ui-corner-top pageHeader">
        Change Password
    </div>
    <div id="divChangePassword">
        <div class="accountInfo ui-widget-content ui-corner-bottom pageContent">
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="ChangeUserPasswordValidationGroup"/>
            <fieldset class="changePassword">
                <legend>Account Information</legend>
                <p>
                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                    <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                    <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                            CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                    <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                            ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                </p>
            </fieldset>
            <p class="submitButton">
                <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                    Text="Cancel" onclick="CancelPushButton_Click"/>
                <asp:Button ID="ChangePasswordPushButton" runat="server" 
                    CommandName="ChangePassword" Text="Change Password" 
                    ValidationGroup="ChangeUserPasswordValidationGroup" 
                    onclick="ChangePasswordPushButton_Click"/>
            </p>
        </div>
    </div>
</div>
</asp:Content>
