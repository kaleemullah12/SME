<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SMEForm.Account.Login" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/Scripts/login.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="loginContainer">
        <div class="ui-widget-header ui-corner-top pageHeader">
            Log In
        </div>
        <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" onauthenticate="LoginUser_Authenticate">
            <LayoutTemplate>
                <div class="accountInfo ui-widget-content ui-corner-bottom pageContent">
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup"/>
                    <fieldset class="login">
                        <legend>Account Information</legend>
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                 CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                 CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                 ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:CheckBox ID="RememberMe" runat="server"/>
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                        </p>
                    </fieldset>
                    <p class="submitButton">
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                    </p>
                </div>
            </LayoutTemplate>
        </asp:Login>
    </div>
</asp:Content>
