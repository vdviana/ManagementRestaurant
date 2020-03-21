<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="cadastro_pedido.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.vendas.cadastro_pedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../js/mascara.js" type="text/javascript"> </script>
    <style type="text/css">

        .auto-style2 { width: 1050px; }
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
        width: 4px;
    }
    .auto-style4 {
        height: 26px;
    }
    .auto-style5 {
        height: 152px;
    }
    .auto-style6 {
        width: 280px;
    }
    .auto-style7 {
        width: 270px;
    }
        .auto-style8 {
            width: 241px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Cadastro de Pedidos</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="9" class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtCpf" runat="server" Enabled="False" MaxLength="14" Width="125px" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Tipo de cliente:</td>
                    <td>
                        <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                            <asp:ListItem Value="1">Físico</asp:ListItem>
                            <asp:ListItem Value="2">Jurídico</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnClienteF" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td>CPF do cliente:</td>
                                    <td>
                                        <asp:TextBox ID="txtCpfCliente" runat="server" MaxLength="14" onkeyup="formataCPF(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>Valor total:</td>
                                    <td>
                                        <asp:TextBox ID="txtValorPrato" runat="server" Enabled="False" MaxLength="6" onkeyup="formataValor(this,event)" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCalcular" runat="server" CssClass="Botoes" Text="Calcular" OnClick="btnCalcular_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>Realização da compra:</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlCompra" runat="server">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Pessoal</asp:ListItem>
                                            <asp:ListItem Value="2">Viagem</asp:ListItem>
                                            <asp:ListItem Value="3">Entrega</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnClienteJ" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style16">CNPJ do cliente:</td>
                                    <td>
                                        <asp:TextBox ID="txtCnpjCliente" runat="server" MaxLength="18" onkeyup="formataCNPJ(this,event)" ToolTip="Em caso de digito X no documento, substituir por 0" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>Valor:</td>
                                    <td>
                                        <asp:TextBox ID="txtValorPrato2" runat="server" Enabled="False" MaxLength="6" onkeyup="formataValor(this,event)" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCalcularJ" runat="server" CssClass="Botoes" Text="Calcular" OnClick="btnCalcularJ_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Quantidade por dia:</td>
                                    <td>
                                        <asp:TextBox ID="txtQuantidadeDia" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>Realização da compra:</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlCompra2" runat="server">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Viagem</asp:ListItem>
                                            <asp:ListItem Value="2">Entrega</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style5">Data de inicio:</td>
                                    <td class="auto-style5">
                                        <asp:Calendar ID="calI_Contrato" runat="server" ToolTip="Escolha um dia à partir da data atual"></asp:Calendar>
                                    </td>
                                    <td class="auto-style5">Até:</td>
                                    <td class="auto-style5" colspan="2">
                                        <asp:Calendar ID="calF_Contrato" runat="server" ToolTip="Escolha um dia à partir da data atual"></asp:Calendar>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">
                        <asp:Panel ID="pnPratos" runat="server" Visible="False">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style8">Prato:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlPrato1" runat="server" OnLoad="ddlPrato1_Load" AutoPostBack="True" OnSelectedIndexChanged="ddlPrato1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Valor:</td>
                                    <td>
                                        <asp:TextBox ID="txtValor1" runat="server" Width="50px" Enabled="False" MaxLength="5"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">Quantidade:</td>
                                    <td>
                                        <asp:TextBox ID="txtQuantidade1" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8" rowspan="4">&nbsp;</td>
                                    <td>
                                        <asp:DropDownList ID="ddlPrato2" runat="server" OnLoad="ddlPrato2_Load" AutoPostBack="True" OnSelectedIndexChanged="ddlPrato1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td rowspan="4">&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtValor2" runat="server" Enabled="False" MaxLength="5" Width="50px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7" rowspan="4">&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtQuantidade2" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlPrato3" runat="server" OnLoad="ddlPrato2_Load" AutoPostBack="True" OnSelectedIndexChanged="ddlPrato1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValor3" runat="server" Enabled="False" MaxLength="5" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantidade3" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">
                                        <asp:DropDownList ID="ddlPrato4" runat="server" OnLoad="ddlPrato2_Load" AutoPostBack="True" OnSelectedIndexChanged="ddlPrato1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtValor4" runat="server" Enabled="False" MaxLength="5" Width="50px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtQuantidade4" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">
                                        <asp:DropDownList ID="ddlPrato5" runat="server" OnLoad="ddlPrato2_Load" AutoPostBack="True" OnSelectedIndexChanged="ddlPrato1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtValor5" runat="server" Enabled="False" MaxLength="5" Width="50px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtQuantidade5" runat="server" MaxLength="6" onkeyup="formataInteiro(this,event)" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" colspan="6">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" colspan="6">
                                        <asp:Button ID="btnAplicar" runat="server" CssClass="Botoes" Text="Aplicar" Visible="False" OnClick="btnAplicar_Click" />
                                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" Text="Limpar" Visible="False" OnClick="btnLimpar_Click" />
                                        &nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/home/home.aspx" Text="Cancelar" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" colspan="6">
                                        <asp:Button ID="btnAplicarExterno" runat="server" CssClass="Botoes" Text="Aplicar" Visible="False" OnClick="btnAplicarExterno_Click" />
                                        &nbsp;<asp:Button ID="btnLimparExterno" runat="server" CssClass="Botoes" Text="Limpar" Visible="False" OnClick="btnLimpar_Click" />
                                        &nbsp;<asp:Button ID="btnCancelarExterno" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/home/home.aspx" Text="Cancelar" Visible="False" />
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
