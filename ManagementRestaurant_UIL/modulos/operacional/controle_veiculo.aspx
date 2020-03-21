<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="controle_veiculo.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.controle_veiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Controle de Estacionamento</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="6">&nbsp;</td>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td>Placa do veículo:</td>
                    <td>
                        <asp:TextBox ID="txtPlaca" runat="server" MaxLength="8" ToolTip="Digite a placa do carro no formato AAA-0000" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Botoes" OnClick="btnBuscar_Click" Text="Buscar" />
                    </td>
                    <td>Vagas:</td>
                    <td>
                        <asp:TextBox ID="txtVaga" runat="server" Enabled="False" Width="30px" AutoPostBack="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Panel ID="pnFuncionario" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td>Nome do funcionário:</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" MaxLength="50" Width="185px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Marca:</td>
                                    <td>
                                        <asp:TextBox ID="txtMarca" runat="server" Enabled="False" MaxLength="20" onkeyup="formataTexto(this,event)" ToolTip="Informe a marca do veículo. Ex: Fiat" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>Modelo:</td>
                                    <td>
                                        <asp:TextBox ID="txtModelo" runat="server" Enabled="False" MaxLength="30" onkeyup="formataTexto(this,event)" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>Tipo:</td>
                                    <td>
                                        <asp:TextBox ID="txtTipo" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" colspan="6"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">
                                        <asp:Label ID="lblEntrada2" runat="server" Text="Hora de entrada:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEntrada" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSaida2" runat="server" Text="Hora de saida:"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSaida" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" colspan="6">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Button ID="btnAplicar" runat="server" CssClass="Botoes" Text="Aplicar" OnClick="btnAplicar_Click" />
                                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" Text="Limpar" OnClick="btnLimpar_Click" />
                                        &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/home/home.aspx" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="5"></td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="5">
                        <asp:Panel ID="pnCliente" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">
                                        <asp:Label ID="lblEntrada1" runat="server" Text="Hora de entrada:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEntradaExterno" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSaida1" runat="server" Text="Hora de saida:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSaidaExterno" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnAplicarExterno" runat="server" CssClass="Botoes" Text="Aplicar" OnClick="btnAplicarExterno_Click" />
                                        &nbsp;<asp:Button ID="btnLimparExterno" runat="server" CssClass="Botoes" Text="Limpar" OnClick="btnLimparExterno_Click" />
                                        &nbsp;<asp:Button ID="btnCancelarExterno" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/home/home.aspx" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>