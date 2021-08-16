<%@ Page Title="" Language="C#" MasterPageFile="~/Setup.master" AutoEventWireup="true" CodeBehind="Alert.aspx.cs" Inherits="SMEForm.Alert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetupContent" runat="server">
  <div class="ui-widget-header pageHeader" >
       Alerts Configuration 
    </div>
    <div class="auto-style1" style="padding:5px">
 
       <table class="jsDataTable" style="width:504px; height: 212px;" border="0">
            <tr>
                <td style="font-weight: 700" colspan="2"> Visa Expiry Alerts</td>
                <td style="width: 96px"> &nbsp;</td>
            </tr>
            <tr>
                <td style="font-weight: 700" colspan="2"> &nbsp;</td>
                <td style="width: 96px"> &nbsp;</td>
            </tr>

            <tr>
                <td class="auto-style3" style="width: 291px">Alert Days:</td>
                <td style="width: 1132px"> <asp:TextBox ID="txtdays" runat="server" Text="" Width="100%"  ValidationGroup="Visa"></asp:TextBox>
                </td>
                <td style="width: 96px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdays" ErrorMessage=" *" ValidationGroup="Visa"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style4" style="width: 291px">Task Name:</td>
                <td class="auto-style2" style="width: 1132px"> <asp:TextBox ID="txtName" runat="server" Text="" Width="100%"  ValidationGroup="Visa"></asp:TextBox>
                </td>
                <td class="auto-style2" style="width: 96px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" ErrorMessage=" *" ValidationGroup="Visa"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 291px">Task Path: </td>
                <td style="width: 1132px"> <asp:TextBox ID="txtPath" runat="server" Text="" Width="100%"  ValidationGroup="Visa"></asp:TextBox>
                </td>
                <td style="width: 96px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPath" ErrorMessage=" *" ValidationGroup="Visa"></asp:RequiredFieldValidator>
                </td>
            </tr>
           <tr>
                <td class="auto-style3" style="width: 291px">Mail To: </td>
                <td style="width: 1132px"> <asp:TextBox ID="txtMail" runat="server" Text="" Width="100%"  ValidationGroup="Visa"></asp:TextBox>
                </td>
                <td style="width: 96px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMail" ErrorMessage=" *" ValidationGroup="Visa"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 291px">Mail Subject: </td>
                <td style="width: 1132px"> <asp:TextBox ID="txtSubject" runat="server" Text="" Width="100%"  ValidationGroup="Visa"></asp:TextBox>
                </td>
                <td style="width: 96px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject" ErrorMessage=" *" ValidationGroup="Visa"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="vertical-align: top; width: 291px;">Mail Body: </td>
                <td style="width: 1132px"> <asp:TextBox ID="txtbody" runat="server" Text="" Width="100%"  ValidationGroup="Visa" TextMode="MultiLine" Height="80px"></asp:TextBox>
                </td>
                <td style="width: 96px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtbody" ErrorMessage=" *" ValidationGroup="Visa"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 291px">&nbsp;</td>
                <td style="width: 1132px; text-align: right;"><asp:Button ID="btnCreateNew" runat="server" Text="Save" OnClick="btnCreateNew_Click" ValidationGroup="Visa" />
                </td>
                <td style="width: 96px">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 291px">&nbsp;</td>
                <td style="width: 1132px">&nbsp;</td>
                <td style="width: 96px">&nbsp;</td>
            </tr>
        </table>
    <div class="auto-style1" style="padding:5px">
 
       <table class="jsDataTable" style="width:500px; " border="0">
            <tr>
                <td style="font-weight: 700; width: 226px;" class="auto-style3">AWOL Alerts</td>
                <td style="width: 878px"> &nbsp;</td>
                <td style="width: 88px"> &nbsp;</td>
            </tr>
            <tr>
                <td style="font-weight: 700; width: 226px;" class="auto-style3">&nbsp;</td>
                <td style="width: 878px"> &nbsp;</td>
                <td style="width: 88px"> &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 226px">Alert Days:</td>
                <td style="width: 878px"> <asp:TextBox ID="txtdays1" runat="server" Text="" Width="100%"  ValidationGroup="Awol"></asp:TextBox>
                </td>
                <td style="width: 88px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtdays1" ErrorMessage=" *" ValidationGroup="Awol"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1" style="width: 226px">Task Name:</td>
                <td class="auto-style1" style="width: 878px"> <asp:TextBox ID="txtName1" runat="server" Text="" Width="100%"  ValidationGroup="Awol"></asp:TextBox>
                </td>
                <td class="auto-style1" style="width: 88px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtName1" ErrorMessage=" *" ValidationGroup="Awol"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style4" style="width: 226px">Task Path: </td>
                <td class="auto-style2" style="width: 878px"> <asp:TextBox ID="txtpath1" runat="server" Text="" Width="100%"  ValidationGroup="Awol"></asp:TextBox>
                </td>
                <td class="auto-style2" style="width: 88px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtpath1" ErrorMessage=" *" ValidationGroup="Awol"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td class="auto-style3" style="width: 226px">Mail To: </td>
                <td style="width: 878px"> <asp:TextBox ID="txtMail2" runat="server" Text="" Width="100%"  ValidationGroup="Awol"></asp:TextBox>
                </td>
                <td style="width: 88px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMail" ErrorMessage=" *" ValidationGroup="Awol"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 226px">Mail Subject: </td>
                <td style="width: 878px"> <asp:TextBox ID="txtSubject2" runat="server" Text="" Width="100%"  ValidationGroup="Awol"></asp:TextBox>
                </td>
                <td style="width: 88px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtSubject" ErrorMessage=" *" ValidationGroup="Awol"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 226px; vertical-align: top;">Mail Body: </td>
                <td style="width: 878px"> <asp:TextBox ID="txtbody2" runat="server" Text="" Width="100%"  ValidationGroup="Awol" TextMode="MultiLine" height="86px"></asp:TextBox>
                </td>
                <td style="width: 88px"> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtbody" ErrorMessage=" *" ValidationGroup="Awol"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 226px">&nbsp;</td>
                <td style="width: 878px"><asp:Button ID="btnCreateNew12" runat="server" Text="Save" OnClick="btnCreateNew12_click" ValidationGroup="Awol" />
                </td>
                <td style="width: 88px">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3" style="width: 226px">&nbsp;</td>
                <td style="width: 878px">&nbsp;</td>
                <td style="width: 88px">&nbsp;</td>
            </tr>
        </table>
        </div>
       <br />  
        &nbsp;</div>
</asp:Content>
