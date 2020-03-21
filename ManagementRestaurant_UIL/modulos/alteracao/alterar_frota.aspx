﻿<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" AutoEventWireup="true"  MasterPageFile="~/master/principal.Master"  CodeBehind="alterar_frota.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.alterar_frota" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .auto-style2 { width: 1050px; }
        .auto-style3 {
            height: 36px;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Atualização de Frota</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="4">&nbsp;</td>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td>CPF do funcionário:</td>
                    <td>
                        <asp:TextBox ID="txtCpf" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Botoes" OnClick="btnBuscar_Click" Text="Buscar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnCadastro" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style4">Nome:</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" Width="185px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4">Telefone:</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" Width="125px" MaxLength="14"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4">CNH:</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtCnh" runat="server" Enabled="False" MaxLength="11" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" class="auto-style5"></td>
                                </tr>
                                <tr>
                                    <td>Placa:</td>
                                    <td>
                                        <asp:TextBox ID="txtPlaca" runat="server" Width="125px" MaxLength="8" ToolTip="Preencha a placa do carro no formato AAA-0000" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>Tipo de veículo:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipo" runat="server" Enabled="False" >
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="Moto">Moto</asp:ListItem>
                                            <asp:ListItem Value="Carro">Carro</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;Marca:</td>
                                    <td>
                                        <asp:TextBox ID="txtMarca" runat="server" MaxLength="20" Width="125px" onkeyup="formataTexto(this,event)" ToolTip="Informe a marca do veículo. Ex: Fiat" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Modelo:</td>
                                    <td>
                                        <asp:TextBox ID="txtModelo" runat="server" MaxLength="30" Width="125px" onkeyup="formataTexto(this,event)" ToolTip="Informe o modelo do veículo. Ex: Fox" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" colspan="6"></td>
                                </tr>
                                <tr>
                                    <td colspan="6" class="auto-style3">
                                        &nbsp;<asp:Button ID="btnAlterar" runat="server" CssClass="Botoes" OnClick="btnAlterar_Click" Text="Alterar" />
                                        &nbsp;<asp:Button ID="btnConcluir" runat="server" CssClass="Botoes" OnClick="btnConcluir_Click" Text="Concluir" Visible="False" />
                                        &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/home/home.aspx" Text="Cancelar" />
                                        &nbsp;<asp:Button ID="btnVoltar" runat="server" CssClass="Botoes"  Text="Cancelar"  Visible="False" OnClick="btnVoltar_Click" />
                                        &nbsp;&nbsp;</td>
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