<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="lista_clientes.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.lista_clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .auto-style2 { width: 1050px; }
        
        .auto-style10 { text-align: left; }

        .auto-style12 { width: 75px; }
    
        .auto-style13 {
            text-align: center;
            height: 18px;
        }

        .auto-style14 {
            text-align: left;
        }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Pesquisa de Clientes</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="6">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>Tipo de cliente:</td>
                    <td>
                        <asp:DropDownList ID="ddltipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltipo_SelectedIndexChanged" OnTextChanged="ddltipo_SelectedIndexChanged" style="text-align: center">
                            <asp:ListItem Text="Selecione..." Value="0" />
                            <asp:ListItem Text="Físico" Value="1" />
                            <asp:ListItem Text="Jurídico" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnCadastro" runat="server" Visible="False" Width="1054px">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style14">Parametro:</td>
                                    <td class="auto-style10">
                                        <asp:TextBox ID="txtPesquisa" runat="server" Width="185px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">Pesquisar por:</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlColuna" runat="server" style="text-align: left">
                                            <asp:ListItem Value="Cli_Nome">Nome</asp:ListItem>
                                            <asp:ListItem Value="Cli_Email">Email</asp:ListItem>
                                            <asp:ListItem Value="Cli_CPF">CPF/CNPJ</asp:ListItem>
                                            <asp:ListItem Value="Cli_RG">RG</asp:ListItem>
                                            <asp:ListItem Value="Cli_Cep">Cep</asp:ListItem>
                                            <asp:ListItem Value="Cli_Rua">Endereço</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style12">
                                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Botoes" OnClick="btnPesquisar_Click" style="text-align: right" Text="Buscar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10" colspan="5">
                                        <asp:Panel ID="pGridPesquisa" runat="server" HorizontalAlign="Center" ScrollBars="Vertical">
                                            <asp:GridView ID="grdClientes" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="grdClientes_RowCommand" style="margin-top: 24px" Width="1025px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nome">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtNome" runat="server" CommandArgument='<%#DataBinder. Eval (Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CPF/CNPJ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtDoc" runat="server" CommandArgument='<%#DataBinder.Eval(Container,"RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CEP">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtCEP" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Endereço">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtEndereco" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bairro">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtBairro" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtEmail" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
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
                        <asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" Text="Limpar" OnClick="btnLimpar_Click" Visible="False" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>
