<%@ Page Title="" Language="C#" MasterPageFile="~/Setup.Master" AutoEventWireup="true" CodeBehind="MailSetup.aspx.cs" Inherits="SMEForm.MailSetup" %>


<asp:Content ID="Content2" ContentPlaceHolderID="SetupContent" runat="server">
      <div class="ui-widget-header pageHeader" >
       Email Configuration
    </div>
    <div class="auto-style1" style="padding:5px">
 
       <table class="jsDataTable" style="width:500px; ">
            <tr>
                <td style="font-weight: 700" class="auto-style3"> Mail SetUp</td>
                <td> &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Smtp Server:</td>
                <td> <asp:TextBox ID="txtsmtp" runat="server" Text="" Width="275px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsmtp" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Email: </td>
                <td class="auto-style1"> <asp:TextBox ID="txtemail" runat="server" Text="" Width="277px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtemail" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Sending Port:</td>
                <td class="auto-style2"> <asp:TextBox ID="txtport" runat="server" Text="" Width="275px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtport" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">User ID: </td>
                <td> <asp:TextBox ID="txtuserid" runat="server" Text="" Width="276px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtuserid" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Password: </td>
                <td> <asp:TextBox ID="txtpassword" runat="server" Text="" Width="276px"  ValidationGroup="KFC" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtpassword" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3"><asp:Button ID="btnCreateNew" runat="server" Text="Save" OnClick="btnCreateNew_Click" ValidationGroup="KFC" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
       <br />  
        &nbsp;</div>
</asp:Content>
