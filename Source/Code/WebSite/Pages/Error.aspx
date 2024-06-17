<%@ Page Language="C#" MasterPageFile="~/Pages/Simulators.Master" AutoEventWireup="true" Inherits="Pages_Error" Title="Erro" Codebehind="Error.aspx.cs" %>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="Server" SkinID="Heavy" Text="Lamentamos mas ocorreu um Erro:"></asp:Label></td>
        </tr>
    </table>
    <table>
        <tr>
            <td><asp:Label ID="Label2" runat="server" SkinID="Heavy"></asp:Label></td>
            <td><asp:Label ID="mErrorMessageLabel" runat="Server"></asp:Label></td>
        </tr>
    </table>
</asp:Content>