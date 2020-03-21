<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_prato.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.cadastro_prato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style2 { width: 1050px; }
        .auto-style3 {
            height: 18px;
        }
        .auto-style4 {
        }
        .auto-style6 {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Pratos</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="10">&nbsp;</td>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style6">Nome do prato:</td>
                    <td>
                        <asp:TextBox ID="txtNomePrato" onkeyup="formataTexto(this,event)" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td>Valor:</td>
                    <td>
                        <asp:TextBox ID="txtValorPrato" onkeyup="formataValor(this,event)" runat="server" Width="70px" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" colspan="4"></td>
                </tr>
                <tr>
                    <td class="auto-style6">Ingrediente:</td>
                    <td>
                        <asp:DropDownList ID="ddlIngrediente1" runat="server" OnLoad="ddlIngrediente1_Load">
                        </asp:DropDownList>
                    </td>
                    <td>Quantidade:</td>
                    <td>
                        <asp:TextBox ID="txtQuantidade1" onkeyup="formataDouble(this,event)" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td rowspan="4" class="auto-style6">&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="ddlIngrediente2" runat="server" OnLoad="ddlIngrediente2_Load">
                        </asp:DropDownList>
                    </td>
                    <td rowspan="4">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtQuantidade2" onkeyup="formataDouble(this,event)" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlIngrediente3" runat="server" OnLoad="ddlIngrediente2_Load">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantidade3" onkeyup="formataDouble(this,event)" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlIngrediente4" runat="server" OnLoad="ddlIngrediente2_Load">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantidade4" onkeyup="formataDouble(this,event)" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlIngrediente5" runat="server" OnLoad="ddlIngrediente2_Load">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantidade5" onkeyup="formataDouble(this,event)" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style6" colspan="4">
                        <asp:Button ID="btnCadastrar" runat="server" CssClass="Botoes" style="text-align: right" Text="Cadastrar" OnClick="btnCadastrar_Click" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" Text="Limpar" OnClick="btnLimpar_Click" />
                        &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/home/home.aspx" Text="Cancelar" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>
