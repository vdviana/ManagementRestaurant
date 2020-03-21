using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

using ManagementRestaurant_MDL;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.vendas
{
    public partial class cadastro_pedido : System.Web.UI.Page
    {
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ClienteMDL _clienteMDL = new ClienteMDL();
        private ClienteGLL _clienteGLL = new ClienteGLL();

        private EstoqueBLL _estoqueBLL = new EstoqueBLL();

        private PedidoMDL _pedidoMDL = new PedidoMDL();
        private PedidoBLL _pedidoBLL = new PedidoBLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        double i;

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.N_Acesso != 1 && _funcionarioMDL.N_Acesso != 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                            string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
                    }
                    else
                    {
                        txtCpf.Text = _funcionarioMDL.Cpf;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                               "<script>alert('Sua sessão foi encerrada automaticamente por por atingir o tempo limite de conexão, faça o login novamente para iniciar uma nova sessão');location.href='../login.aspx';</script>");
                }
            }
        }

        #endregion

        #region ddlCliente_SelectedIndexChanged

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCliente.SelectedValue == "1")
            {
                btnAplicar.Visible = true;
                btnLimpar.Visible = true;
                btnCancelar.Visible = true;

                btnAplicarExterno.Visible = false;
                btnCancelarExterno.Visible = false;
                btnLimparExterno.Visible = false;

                pnPratos.Visible = true;
                pnClienteF.Visible = true;
                pnClienteJ.Visible = false;
            }
            else if (ddlCliente.SelectedValue == "2")
            {
                btnAplicarExterno.Visible = true;
                btnCancelarExterno.Visible = true;
                btnLimparExterno.Visible = true;

                btnAplicar.Visible = false;
                btnLimpar.Visible = false;
                btnCancelar.Visible = false;

                pnPratos.Visible = true;
                pnClienteJ.Visible = true;
                pnClienteF.Visible = false;
            }
            else
            {
                Response.Redirect("cadastro_pedido.aspx");
            }
        }

        #endregion

        #region ddlPrato1_Load

        protected void ddlPrato1_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _estoqueBLL.CarregaNomePratoDropdown();

                    ddlPrato1.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlPrato1.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlPrato1.DataSource = _conexaoMDL.Ds;
                    ddlPrato1.DataBind();

                    ddlPrato1.Items.Insert(0, new ListItem("Selecione...", "0"));

                    Session["CarregaDS"] = _conexaoMDL.Ds;
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ddlPrato2_Load

        protected void ddlPrato2_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    ddlPrato2.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlPrato2.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlPrato2.DataSource = _conexaoMDL.Ds;
                    ddlPrato2.DataBind();

                    ddlPrato2.Items.Insert(0, new ListItem("Selecione...", "0"));

                    ddlPrato3.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlPrato3.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlPrato3.DataSource = _conexaoMDL.Ds;
                    ddlPrato3.DataBind();

                    ddlPrato3.Items.Insert(0, new ListItem("Selecione...", "0"));

                    ddlPrato4.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlPrato4.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlPrato4.DataSource = _conexaoMDL.Ds;
                    ddlPrato4.DataBind();

                    ddlPrato4.Items.Insert(0, new ListItem("Selecione...", "0"));

                    ddlPrato5.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlPrato5.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlPrato5.DataSource = _conexaoMDL.Ds;
                    ddlPrato5.DataBind();

                    ddlPrato5.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                              "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ddlPrato1_SelectedIndexChanged

        protected void ddlPrato1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _conexaoMDL.Ds = (DataSet)Session["CarregaDS"];

            if (ddlPrato1.SelectedValue != "0")
            {
                txtValor1.Text = Convert.ToString(_conexaoMDL.Ds.Tables[0].Rows[ddlPrato1.SelectedIndex - 1]["Valor_Prato"]);
            }
            else
            {
                txtValor1.Text = string.Empty;
            }

            if (ddlPrato2.SelectedValue != "0")
            {
                txtValor2.Text = Convert.ToString(_conexaoMDL.Ds.Tables[0].Rows[ddlPrato2.SelectedIndex - 1]["Valor_Prato"]);
            }
            else
            {
                txtValor2.Text = string.Empty;
            }

            if (ddlPrato3.SelectedValue != "0")
            {
                txtValor3.Text = Convert.ToString(_conexaoMDL.Ds.Tables[0].Rows[ddlPrato3.SelectedIndex - 1]["Valor_Prato"]);
            }
            else
            {
                txtValor3.Text = string.Empty;
            }

            if (ddlPrato4.SelectedValue != "0")
            {
                txtValor4.Text = Convert.ToString(_conexaoMDL.Ds.Tables[0].Rows[ddlPrato4.SelectedIndex - 1]["Valor_Prato"]);
            }
            else
            {
                txtValor4.Text = string.Empty;
            }

            if (ddlPrato5.SelectedValue != "0")
            {
                txtValor5.Text = Convert.ToString(_conexaoMDL.Ds.Tables[0].Rows[ddlPrato5.SelectedIndex - 1]["Valor_Prato"]);
            }
            else
            {
                txtValor5.Text = string.Empty;
            }
        }

        #endregion

        #region btnCalcular_Click

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            if (ValidaCamposF() && ValidaCampos0())
            {
                ddlCliente.Enabled = false;

                txtCpfCliente.Enabled = false;
                ddlCompra.Enabled = false;

                ddlPrato1.Enabled = false;
                txtQuantidade1.Enabled = false;

                i = 0 + Convert.ToDouble(txtQuantidade1.Text);
                _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor1.Text);

                _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);

                if (ddlPrato2.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade2.Text))
                {
                    ddlPrato2.Enabled = false;
                    txtQuantidade2.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade2.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor2.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                if (ddlPrato3.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade3.Text))
                {
                    ddlPrato3.Enabled = false;
                    txtQuantidade3.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade3.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor3.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                if (ddlPrato4.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade4.Text))
                {
                    ddlPrato4.Enabled = false;
                    txtQuantidade4.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade4.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor4.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                if (ddlPrato5.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade5.Text))
                {
                    ddlPrato5.Enabled = false;
                    txtQuantidade5.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade5.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor5.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                if (ddlCompra.SelectedValue == "1")
                {
                    txtValorPrato.Text = Convert.ToString(_pedidoMDL.ValorTotal * 1.1);
                }
                else if (ddlCompra.SelectedValue == "3")
                {
                    txtValorPrato.Text = Convert.ToString(_pedidoMDL.ValorTotal * 1.15);
                }
                else
                {
                    txtValorPrato.Text = Convert.ToString(_pedidoMDL.ValorTotal);
                }           
            }
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            if (ValidaCamposF() && txtValorPrato.Text != string.Empty)
            {
                _conexaoMDL.Validador = _clienteGLL.ValidaCPF(txtCpfCliente.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;
                    _clienteMDL.Documento = txtCpfCliente.Text;
                    _pedidoMDL.Tipo = ddlCompra.Text;
                    _pedidoMDL.ValorTotal = Convert.ToDouble(txtValorPrato.Text);

                    i = ValidaDropDown();

                    String[] Prato = new string[Convert.ToInt32(i)];
                    String[] P_Quantidade = new string[Convert.ToInt32(i)];

                    i = 0;

                    Prato[Convert.ToInt32(i)] = ddlPrato1.Text;
                    P_Quantidade[Convert.ToInt32(i)] = txtQuantidade1.Text;

                    if (ddlPrato2.Enabled == false && txtQuantidade2.Enabled == false)
                    {
                        i = i + 1;

                        Prato[Convert.ToInt32(i)] = ddlPrato2.Text;
                        P_Quantidade[Convert.ToInt32(i)] = txtQuantidade2.Text;
                    }

                    if (ddlPrato3.Enabled == false && txtQuantidade3.Enabled == false)
                    {
                        i = i + 1;

                        Prato[Convert.ToInt32(i)] = ddlPrato3.Text;
                        P_Quantidade[Convert.ToInt32(i)] = txtQuantidade3.Text;
                    }

                    if (ddlPrato4.Enabled == false && txtQuantidade4.Enabled == false)
                    {
                        i = i + 1;

                        Prato[Convert.ToInt32(i)] = ddlPrato4.Text;
                        P_Quantidade[Convert.ToInt32(i)] = txtQuantidade4.Text;
                    }

                    if (ddlPrato5.Enabled == false && txtQuantidade5.Enabled == false)
                    {
                        i = i + 1;

                        Prato[Convert.ToInt32(i)] = ddlPrato5.Text;
                        P_Quantidade[Convert.ToInt32(i)] = txtQuantidade5.Text;
                    }

                    try
                    {
                        _conexaoMDL = _pedidoBLL.CadastraPedido(_funcionarioMDL, _clienteMDL, _pedidoMDL, Prato, P_Quantidade);

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                _conexaoMDL.Validador
                                    ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                    : "<script>alert('CPF não consta no sistema');</script>");
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }
                }
            }
        }

        #endregion

        #region btnCalcularJ_Click

        protected void btnCalcularJ_Click(object sender, EventArgs e)
        {
            _pedidoMDL.Q_Dia = Convert.ToInt32(txtQuantidadeDia.Text);
            _pedidoMDL.I_Contrato = calI_Contrato.SelectedDate;
            _pedidoMDL.F_Contrato = calF_Contrato.SelectedDate;

            if (ValidaCamposJ() && ValidaCampos0() && ValidaDatas(_pedidoMDL))
            {
                ddlCliente.Enabled = false;

                txtCnpjCliente.Enabled = false;
                ddlCompra2.Enabled = false;

                ddlPrato1.Enabled = false;
                txtQuantidade1.Enabled = false;

                txtQuantidadeDia.Enabled = false;
                calI_Contrato.Enabled = false;
                calF_Contrato.Enabled = false;

                i = 0 + Convert.ToDouble(txtQuantidade1.Text);
                _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor1.Text);

                _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);

                if (ddlPrato2.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade2.Text))
                {
                    ddlPrato2.Enabled = false;
                    txtQuantidade2.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade2.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor2.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                if (ddlPrato3.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade3.Text))
                {
                    ddlPrato3.Enabled = false;
                    txtQuantidade3.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade3.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor3.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                if (ddlPrato4.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade4.Text))
                {
                    ddlPrato4.Enabled = false;
                    txtQuantidade4.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade4.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor4.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                if (ddlPrato5.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtQuantidade5.Text))
                {
                    ddlPrato5.Enabled = false;
                    txtQuantidade5.Enabled = false;

                    i = 0 + Convert.ToDouble(txtQuantidade5.Text);
                    _pedidoMDL.Valor = 0 + Convert.ToDouble(txtValor5.Text);

                    _pedidoMDL.ValorTotal = _pedidoMDL.ValorTotal + (i * _pedidoMDL.Valor);
                }

                TimeSpan ValidaUteis = _pedidoMDL.F_Contrato - _pedidoMDL.I_Contrato;
                int Intervalo = ValidaUteis.Days;

                if (ddlCompra.SelectedValue == "1")
                {
                    txtValorPrato2.Text = Convert.ToString((_pedidoMDL.ValorTotal * (_pedidoMDL.Q_Dia * Intervalo)));
                }
                else 
                {
                    txtValorPrato2.Text = Convert.ToString((_pedidoMDL.ValorTotal * (_pedidoMDL.Q_Dia * Intervalo)) * 1.15);
                }
            }
        }

        #endregion

        #region btnAplicarExterno_Click

        protected void btnAplicarExterno_Click(object sender, EventArgs e)
        {
            if (ValidaCamposJ() && txtValorPrato2.Text != string.Empty)
            {
                _conexaoMDL.Validador = _clienteGLL.ValidaCNPJ(txtCnpjCliente.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CNPJ inválido!');</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;
                    _clienteMDL.Documento = txtCnpjCliente.Text;
                    _pedidoMDL.Tipo = ddlCompra2.Text;
                    _pedidoMDL.ValorTotal = Convert.ToDouble(txtValorPrato2.Text);
                    _pedidoMDL.Q_Dia = Convert.ToInt32(txtQuantidadeDia.Text);
                    _pedidoMDL.I_Contrato = calI_Contrato.SelectedDate;
                    _pedidoMDL.F_Contrato = calF_Contrato.SelectedDate;

                    if (ValidaDatas(_pedidoMDL))
                    {
                        i = ValidaDropDown();

                        String[] Prato = new string[Convert.ToInt32(i)];
                        String[] P_Quantidade = new string[Convert.ToInt32(i)];

                        i = 0;

                        Prato[Convert.ToInt32(i)] = ddlPrato1.Text;
                        P_Quantidade[Convert.ToInt32(i)] = txtQuantidade1.Text;

                        if (ddlPrato2.Enabled == false && txtQuantidade2.Enabled == false)
                        {
                            i = i + 1;

                            Prato[Convert.ToInt32(i)] = ddlPrato2.Text;
                            P_Quantidade[Convert.ToInt32(i)] = txtQuantidade2.Text;
                        }

                        if (ddlPrato3.Enabled == false && txtQuantidade3.Enabled == false)
                        {
                            i = i + 1;

                            Prato[Convert.ToInt32(i)] = ddlPrato3.Text;
                            P_Quantidade[Convert.ToInt32(i)] = txtQuantidade3.Text;
                        }

                        if (ddlPrato4.Enabled == false && txtQuantidade4.Enabled == false)
                        {
                            i = i + 1;

                            Prato[Convert.ToInt32(i)] = ddlPrato4.Text;
                            P_Quantidade[Convert.ToInt32(i)] = txtQuantidade4.Text;
                        }

                        if (ddlPrato5.Enabled == false && txtQuantidade5.Enabled == false)
                        {
                            i = i + 1;

                            Prato[Convert.ToInt32(i)] = ddlPrato5.Text;
                            P_Quantidade[Convert.ToInt32(i)] = txtQuantidade5.Text;
                        }

                        try
                        {
                            _conexaoMDL = _pedidoBLL.CadastraPedido(_funcionarioMDL, _clienteMDL, _pedidoMDL, Prato, P_Quantidade);

                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                    _conexaoMDL.Validador
                                        ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                        : "<script>alert('CNPJ não consta no sistema');</script>");
                        }
                        catch
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                        }
                    }
                }
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadastro_pedido.aspx");
        }

        #endregion

        #region ValidaCamposF

        private Boolean ValidaCamposF()
        {
            if (string.IsNullOrWhiteSpace(txtCpfCliente.Text) || ddlCompra.SelectedValue == "0" ||
                ddlPrato1.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtQuantidade1.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ValidaCamposJ

        private Boolean ValidaCamposJ()
        {
            if (string.IsNullOrWhiteSpace(txtCnpjCliente.Text) || ddlCompra2.SelectedValue == "0" ||
                ddlPrato1.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtQuantidade1.Text) ||
                string.IsNullOrWhiteSpace(txtQuantidadeDia.Text) || calI_Contrato.SelectedDate.Ticks == 0 ||
                calF_Contrato.SelectedDate.Ticks == 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ValidaDatas

        private Boolean ValidaDatas(PedidoMDL pedidoMDL)
        {
            int ValidaContratoI = DateTime.Compare(pedidoMDL.I_Contrato, DateTime.Today);
            int ValidaContratoF = DateTime.Compare(pedidoMDL.F_Contrato, DateTime.Today);

            if (ValidaContratoI < 0 || ValidaContratoF < 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('A data inicial ou final do contrato não pode ser anterior ao dia atual');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ValidaDropDown

        public double ValidaDropDown()
        {
            i = 5;

            if (ddlPrato5.Enabled == true)
            {
                i = i - 1;
            }

            if (ddlPrato4.Enabled == true)
            {
                i = i - 1;
            }

            if (ddlPrato3.Enabled == true)
            {
                i = i - 1;
            }

            if (ddlPrato2.Enabled == true)
            {
                i = i - 1;
            }

            return i;
        }

        #endregion

        #region ValidaCampos0

        private Boolean ValidaCampos0()
        {
            if (!string.IsNullOrWhiteSpace(txtQuantidade1.Text))
            {
                if (Convert.ToInt32(txtQuantidade1.Text) == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                               "<script>alert('A quantidade informada do produto não pode ser igual a 0');</script>");

                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtQuantidade2.Text))
            {
                if (Convert.ToInt32(txtQuantidade2.Text) == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                               "<script>alert('A quantidade informada do produto não pode ser igual a 0');</script>");

                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtQuantidade3.Text))
            {
                if (Convert.ToInt32(txtQuantidade3.Text) == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                               "<script>alert('A quantidade informada do produto não pode ser igual a 0');</script>");

                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtQuantidade4.Text))
            {
                if (Convert.ToInt32(txtQuantidade4.Text) == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                               "<script>alert('A quantidade informada do produto não pode ser igual a 0');</script>");

                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtQuantidade5.Text))
            {
                if (Convert.ToInt32(txtQuantidade5.Text) == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                               "<script>alert('A quantidade informada do produto não pode ser igual a 0');</script>");

                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}