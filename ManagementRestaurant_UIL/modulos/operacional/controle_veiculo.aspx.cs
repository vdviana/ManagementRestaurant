using System;
using System.Data;
using System.Web.UI;

using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class controle_veiculo : Page
    {
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        private FrotaMDL _frotaMDL = new FrotaMDL();
        private FrotaBLL _frotaBLL = new FrotaBLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.N_Acesso != 1 && _funcionarioMDL.N_Acesso != 5)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                            string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
                    }
                    else
                    {
                        CarregaVagas();
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

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");
            }
            else
            {
                _frotaMDL.Placa = txtPlaca.Text;

                try
                {
                 string   varcpf = _funcionarioMDL.Cpf;
                 _funcionarioMDL.Cpf = string.Empty;
                    _frotaMDL = _frotaBLL.CarregaPlacaVeiculoInterno(_frotaMDL,_funcionarioMDL);
                    _funcionarioMDL.Cpf = varcpf;
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                if (_frotaMDL.Validador)
                {
                    CarregaInformacoesFuncionario();
                }
                else
                {
                    CarregaInformacoesCliente();
                }
            }
        }

        #endregion

        #region CarregaInformacoesFuncionario

        private void CarregaInformacoesFuncionario()
        {
            try
            {
                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                              "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_frotaMDL.D_Entrada.Ticks != 0 && _frotaMDL.D_Saida.Ticks != 0 || _frotaMDL.D_Entrada.Ticks == 0)
            {
                txtNome.Text = _frotaMDL.Fun_Nome;
                txtMarca.Text = _frotaMDL.Marca;
                txtTipo.Text = _frotaMDL.Tipo;
                txtModelo.Text = _frotaMDL.Modelo;

                pnFuncionario.Visible = true;

                txtPlaca.Enabled = false;
            }
            else if (_frotaMDL.D_Entrada.Ticks != 0 && _frotaMDL.D_Saida.Ticks == 0)
            {
                txtEntrada.Text = Convert.ToString(_frotaMDL.D_Entrada.TimeOfDay);

                txtNome.Text = _frotaMDL.Fun_Nome;
                txtMarca.Text = _frotaMDL.Marca;
                txtTipo.Text = _frotaMDL.Tipo;
                txtModelo.Text = _frotaMDL.Modelo;

                pnFuncionario.Visible = true;

                txtPlaca.Enabled = false;
            }
        }

        #endregion

        #region CarregaInformacoesCliente

        private void CarregaInformacoesCliente()
        {
            try
            {
                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                              "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }

            if (_frotaMDL.D_Entrada.Ticks != 0 && _frotaMDL.D_Saida.Ticks != 0 || _frotaMDL.D_Entrada.Ticks == 0)
            {
                pnCliente.Visible = true;
                txtPlaca.Enabled = false;
            }
            else if (_frotaMDL.D_Entrada.Ticks != 0 && _frotaMDL.D_Saida.Ticks == 0)
            {
                txtEntradaExterno.Text = Convert.ToString(_frotaMDL.D_Entrada.TimeOfDay);

                pnCliente.Visible = true;
                txtPlaca.Enabled = false;

                _frotaMDL = _frotaBLL.CarregaEntradaVeiculo(_frotaMDL);
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion

        #region btnLimparExterno_Click

        protected void btnLimparExterno_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion

        #region LimpaCampos

        private void LimpaCampos()
        {
            pnFuncionario.Visible = false;
            pnCliente.Visible = false;

            txtPlaca.Text = string.Empty;

            txtEntrada.Text = string.Empty;
            txtSaida.Text = string.Empty;

            txtEntradaExterno.Text = string.Empty;
            txtSaidaExterno.Text = string.Empty;

            txtPlaca.Enabled = true;
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEntrada.Text == string.Empty)
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = DateTime.Now;

                    _frotaMDL.D_Entrada = new DateTime(_frotaMDL.D_Entrada.Year, _frotaMDL.D_Entrada.Month, _frotaMDL.D_Entrada.Day,
                        _frotaMDL.D_Entrada.Hour, _frotaMDL.D_Entrada.Minute, _frotaMDL.D_Entrada.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraEntradaInternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('Hora de entrada registrada com sucesso');</script>");

                    LimpaCampos();

                    CarregaVagas();
                }
                else
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = Convert.ToDateTime(txtEntrada.Text);
                    _frotaMDL.D_Saida = DateTime.Now;

                    _frotaMDL.D_Saida = new DateTime(_frotaMDL.D_Saida.Year, _frotaMDL.D_Saida.Month, _frotaMDL.D_Saida.Day,
                        _frotaMDL.D_Saida.Hour, _frotaMDL.D_Saida.Minute, _frotaMDL.D_Saida.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraSaidaInternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('Hora de saida registrada com sucesso');</script>");

                    LimpaCampos();

                    CarregaVagas();
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }
        }

        #endregion

        #region btnAplicarExterno_Click

        protected void btnAplicarExterno_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEntradaExterno.Text == string.Empty)
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = DateTime.Now;

                    _frotaMDL.D_Entrada = new DateTime(_frotaMDL.D_Entrada.Year, _frotaMDL.D_Entrada.Month, _frotaMDL.D_Entrada.Day,
                        _frotaMDL.D_Entrada.Hour, _frotaMDL.D_Entrada.Minute, _frotaMDL.D_Entrada.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraEntradaExternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('Hora de entrada registrada com sucesso');</script>");

                    LimpaCampos();

                    CarregaVagas();
                }
                else
                {
                    _frotaMDL.Placa = txtPlaca.Text;
                    _frotaMDL.D_Entrada = Convert.ToDateTime(txtEntradaExterno.Text);
                    _frotaMDL.D_Saida = DateTime.Now;

                    _frotaMDL.D_Saida = new DateTime(_frotaMDL.D_Saida.Year, _frotaMDL.D_Saida.Month, _frotaMDL.D_Saida.Day,
                        _frotaMDL.D_Saida.Hour, _frotaMDL.D_Saida.Minute, _frotaMDL.D_Saida.Second);

                    _conexaoMDL = _frotaBLL.CadastraHoraSaidaExternos(_frotaMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('Hora de saida registrada com sucesso');</script>");

                    LimpaCampos();

                    CarregaVagas();
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }           
        }

        #endregion

        #region CarregaVagas

        private void CarregaVagas()
        {
            _frotaMDL = _frotaBLL.CarregaVagas(_frotaMDL);

            txtVaga.Text = Convert.ToString(_frotaMDL.Vagas);
        }

        #endregion
    }
}