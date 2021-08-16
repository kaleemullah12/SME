<%@ Page Title="" Language="C#" MasterPageFile="~/Setup.Master" AutoEventWireup="true" CodeBehind="ExcelInfo.aspx.cs" Inherits="SMEForm.ExcelInfo" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="SetupContent" runat="server">

    <style type="text/css">
    .auto-style1 {
        border: 1px solid #e0cfc2;
        background: #f4f0ec url('Styles/images/ui-bg_inset-soft_100_f4f0ec_1x100.png') repeat-x 50% bottom;
        color: #1e1b1d;
        text-align: left;
    }
        .auto-style2 {
            height: 26px;
        }
    </style>

        <div class="ui-widget-header pageHeader" >
       Excel Information 
    </div>
    <div class="auto-style1" style="padding:5px">
 
       <table class="jsDataTable" style="width:500px; ">
            <tr>
                <td> <b>KFC Column Information </b></td>
                <td> &nbsp;</td>
            </tr>
            <tr>
                <td>WorkID:</td>
                <td> <asp:TextBox ID="txtWorkID" runat="server" Text="" Width="100px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWorkID" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Date: </td>
                <td> <asp:TextBox ID="txtDate" runat="server" Text="" Width="100px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">ClockIn:</td>
                <td class="auto-style2"> <asp:TextBox ID="txtClockIn" runat="server" Text="" Width="100px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtClockIn" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>ClockOut: </td>
                <td> <asp:TextBox ID="txtClockOut" runat="server" Text="" Width="100px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtClockOut" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>First Name: </td>
                <td> <asp:TextBox ID="txtFirstName" runat="server" Text="" Width="100px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFirstName" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Work Hour:</td>
                <td> <asp:TextBox ID="txtWorkHour" runat="server" Text="" Width="100px"  ValidationGroup="KFC"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtWorkHour" ErrorMessage=" *" ValidationGroup="KFC"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:Button ID="btnCreateNew" runat="server" Text="Save" OnClick="btnCreateNew_Click" ValidationGroup="KFC" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
       <br />  
        &nbsp;</div>

    <div class="auto-style1" style="padding:5px">
 
       <table class="jsDataTable" style="width:500px; ">
            <tr>
                <td> <b>Generic Column Information </b></td>
                <td> &nbsp;</td>
            </tr>
            <tr>
                <td>WorkID:</td>
                <td> <asp:TextBox ID="TextBox1" runat="server" Text="" Width="100px"  ValidationGroup="Generic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox1" ErrorMessage=" *" ValidationGroup="Generic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Date: </td>
                <td> <asp:TextBox ID="TextBox2" runat="server" Text="" Width="100px"  ValidationGroup="Generic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox2" ErrorMessage=" *" ValidationGroup="Generic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">ClockIn:</td>
                <td class="auto-style2"> <asp:TextBox ID="TextBox3" runat="server" Text="" Width="100px"  ValidationGroup="Generic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox3" ErrorMessage=" *" ValidationGroup="Generic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>ClockOut: </td>
                <td> <asp:TextBox ID="TextBox4" runat="server" Text="" Width="100px"  ValidationGroup="Generic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TextBox4" ErrorMessage=" *" ValidationGroup="Generic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>First Name: </td>
                <td> <asp:TextBox ID="TextBox5" runat="server" Text="" Width="100px"  ValidationGroup="Generic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox5" ErrorMessage=" *" ValidationGroup="Generic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Work Hour:</td>
                <td> <asp:TextBox ID="TextBox6" runat="server" Text="" Width="100px"  ValidationGroup="Generic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox6" ErrorMessage=" *" ValidationGroup="Generic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Start Row No:</td>
                <td> <asp:TextBox ID="TextBox7" runat="server" Text="" Width="100px"  ValidationGroup="Generic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="TextBox7" ErrorMessage=" *" ValidationGroup="Generic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="btnCreateNew1" runat="server" Text="Save" OnClick="btnCreateNew1_Click" ValidationGroup="Generic"  />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

       <br />  
        &nbsp;</div>
</asp:Content>
