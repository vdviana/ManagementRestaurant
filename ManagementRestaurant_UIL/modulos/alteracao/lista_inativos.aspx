<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="lista_inativos.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.alteracao.lista_inativos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">



        .auto-style2 { width: 1050px; }
        
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Pesquisa de Funcionários Inativos</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="6">&nbsp;</td>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td>Parametro:</td>
                    <td>
                        <asp:TextBox ID="txtPesquisa" runat="server" style="text-align: left" Width="185px"></asp:TextBox>
                    </td>
                    <td>Pesquisar por:</td>
                    <td>
                        <asp:DropDownList ID="ddlColuna" runat="server">
                            <asp:ListItem Value="Fun_Nome">Nome</asp:ListItem>
                            <asp:ListItem Value="Cargo_Nome">Cargo</asp:ListItem>
                            <asp:ListItem Value="Fun_CPF">CPF</asp:ListItem>
                            <asp:ListItem Value="Fun_RG">RG</asp:ListItem>
                            <asp:ListItem Value="Fun_Cep">Cep</asp:ListItem>
                            <asp:ListItem Value="Fun_Rua">Endereço</asp:ListItem>
                            <asp:ListItem Value="Admissao_Data">Data de Admissão</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Botoes" OnClick="btnPesquisar_Click" style="text-align: right" Text="Buscar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="grdFuncionarios" runat="server" AutoGenerateColumns="False" OnRowCommand="grdFuncionarios_RowCommand" style="margin-top: 24px; text-align: center;" Width="1025px">
                            <Columns>
                                <asp:TemplateField HeaderText="Nome Funcionário">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtNome0" runat="server" CommandArgument='<%#DataBinder. Eval (Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cargo">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtCargo0" runat="server" CommandArgument='<%#DataBinder.Eval(Container,"RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CPF">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtCPF0" runat="server" CommandArgument='<%#DataBinder.Eval(Container,"RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RG">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtRG0" runat="server" CommandArgument='<%#DataBinder.Eval(Container,"RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CEP">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtCEP0" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Endereço">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtEndereco0" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bairro">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtBairro0" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data de Admissão">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtDtAdm0" runat="server" CommandArgument='<%#                DataBinder.Eval(Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="Habilitar" Text="Habilitar Conta" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnAtivos" runat="server" CssClass="Botoes" PostBackUrl="~/modulos/alteracao/lista_ativos.aspx" style="text-align: right" Text="Ativos" />
                        &nbsp;<asp:Button ID="btnLimpar" runat="server" CssClass="Botoes" Text="Limpar" OnClick="btnLimpar_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>
