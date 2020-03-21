<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ManagementRestaurant_UIL.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    
.Botoes {
    background-color: #ff6a00;
    border: solid 1px;
    border-bottom-color: #ff6a00;
    color: #ffffff;
    font-family: Arial, Verdana;
    font-size: medium;
}

    .auto-style2 {
        font-size: x-small;
    }
    .auto-style3 {
        font-size: x-small;
        height: 12px;
    }
    .auto-style4 {
        height: 12px;
    }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Panel ID="pnLogin" runat="server">
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style3">CPF</td>
                            <td class="auto-style3">Senha</td>
                            <td class="auto-style4"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnLogar" runat="server" CssClass="BotoesLogin" OnClick="btnLogar_Click" Text="Logar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Panel ID="pnSenhaN" runat="server" Visible="False">
                    <table class="auto-style1">
                        <tr>
                            <td>&nbsp;</td>
                            <td>Nova senha:</td>
                            <td>
                                <asp:TextBox ID="txtSenhaN" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Confirme a nova senha:</td>
                            <td>
                                <asp:TextBox ID="txtSenhaC" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnConfirmar" runat="server" CssClass="BotoesLogin" OnClick="btnConfirmar_Click" Text="Confirmar" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align: center">
                                        <asp:Button ID="btnEati" runat="server" CssClass="BotoesLogin" Text="Cadastrar" PostBackUrl="~/cadastro_eati.aspx" />
                                    </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
