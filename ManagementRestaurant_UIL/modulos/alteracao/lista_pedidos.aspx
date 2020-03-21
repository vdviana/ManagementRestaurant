<%@ Page Title="..:: Sistema de gerenciamento de restaurantes ::.." Language="C#" MasterPageFile="~/master/principal.Master" AutoEventWireup="true" CodeBehind="lista_pedidos.aspx.cs" Inherits="ManagementRestaurant_UIL.modulos.vendas.lista_pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .auto-style2 { width: 1050px; }
        
        .auto-style10 { text-align: left; }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" style="height: auto; margin-bottom: 2px; margin-right: 0px; width: auto;">
        <fieldset id="fdsDadosPessoais">
            <legend style="font-family: Arial;" class="auto-style1">Pesquisa de Pedidos</legend>
            <table class="auto-style2">
                <tr>
                    <td rowspan="6">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>Tipo de Pedido:</td>
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
                     
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style10">
                                        <asp:Panel ID="pGridPesquisaFisico" runat="server" HorizontalAlign="Center" ScrollBars="Vertical" Height="156px">
                                            <asp:GridView ID="grdPedidosFisico" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="grdPedidosFisico_RowCommand"  style="margin-top: 24px" Width="1025px" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nº Pedido">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNPedido" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nome">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNome" runat="server" CommandArgument='<%#DataBinder. Eval (Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visualizar">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnVisualizar" runat="server" CssClass="Botoes" CommandArgument='<%#DataBinder. Eval (Container, "RowIndex") %>'  CommandName="Ver" style="text-align: right" Text="Visualizar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                     
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pGridPesquisaJuridico" runat="server" HorizontalAlign="Center" ScrollBars="Vertical" Height="148px">
                            <asp:GridView ID="grdPedidosJuridico" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="grdPedidosJuridico_RowCommand" style="margin-top: 24px" Width="1025px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nº Pedido">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNPedidoJuridico" runat="server" OnClick="lblNPedidoJ_Click"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Razão Social ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNomeJ" runat="server" CommandArgument='<%#DataBinder. Eval (Container, "RowIndex") %>' CommandName="Ver" Font-Size="Small"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previsão de Entrega">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrevisao" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visualizar">
                                        <ItemTemplate>
  <asp:Button ID="btnVisualizar" runat="server" CssClass="Botoes" CommandArgument='<%#DataBinder. Eval (Container, "RowIndex") %>'  CommandName="Ver" style="text-align: right" Text="Visualizar" />                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </asp:Panel>
</asp:Content>
