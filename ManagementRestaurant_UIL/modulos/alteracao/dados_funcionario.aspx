﻿<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="dados_funcionario.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.dados_funcionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">


        .auto-style2 { width: 1050px; }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Atualização de Funcionários</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="11">&nbsp;</td>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>Nome:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtNome" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="185px"></asp:TextBox>
                    </td>
                    <td>Telefone:</td>
                    <td>
                        <asp:TextBox ID="txtTelefone" runat="server" Enabled="False" MaxLength="14" onkeyup="formataTelefone(this,event)" Width="125px"></asp:TextBox>
                    </td>
                    <td>RG:</td>
                    <td>
                        <asp:TextBox ID="txtRg" runat="server" Enabled="False" MaxLength="9" onkeyup="formataInteiro(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="90px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>CPF:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCpf" runat="server" Enabled="False" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                    </td>
                    <td>CNH:</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCnh" runat="server" Enabled="False" MaxLength="11" onkeyup="formataInteiro(this,event)" Width="125px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>N. Carteira:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtN_Ctrabalho" runat="server" Enabled="False" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="80px"></asp:TextBox>
                    </td>
                    <td>N. Série:</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtN_Ctrabalho2" runat="server" Enabled="False" MaxLength="8" ToolTip="Preencha o número de série no seguinte formato: 0000-AA" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>CEP:</td>
                    <td>
                        <asp:TextBox ID="txtCep" runat="server" Enabled="False" MaxLength="9" onkeyup="formataCEP(this,event)" Width="90px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="Botoes" OnClick="btnBuscar_Click" Text="Buscar" />
                    </td>
                    <td>Endereço:</td>
                    <td>
                        <asp:TextBox ID="txtEndereco" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                    </td>
                    <td>Número:</td>
                    <td>
                        <asp:TextBox ID="txtNumero" runat="server" Enabled="False" MaxLength="5" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Bairro:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtBairro" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                    </td>
                    <td>Cidade:</td>
                    <td>
                        <asp:TextBox ID="txtCidade" runat="server" Enabled="False" MaxLength="50" onkeyup="formataTexto(this,event)" Width="150px"></asp:TextBox>
                    </td>
                    <td>Estado:</td>
                    <td>
                        <asp:TextBox ID="txtEstado" runat="server" Enabled="False" MaxLength="2" onkeyup="formataTexto(this,event)" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td>Cargo:</td>
                    <td colspan="6">
                        <asp:DropDownList ID="ddlCargo" runat="server" Enabled="False" OnLoad="ddlCargo_Load">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:Button ID="btnAlterar" runat="server" CssClass="Botoes" OnClick="btnAlterar_Click" style="text-align: right" Text="Alterar " />
                        &nbsp;<asp:Button ID="btnConcluirAlteracao" runat="server" CssClass="Botoes" OnClick="btnConcluirAlteracao_Click" Text="Concluir" Visible="False" />
                        &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="Botoes" OnClick="BtnBack_Click" style="text-align: right" Text="Voltar" />
                        &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="Botoes" OnClick="btnCancelar_Click" Text="Cancelar" Visible="False" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>
