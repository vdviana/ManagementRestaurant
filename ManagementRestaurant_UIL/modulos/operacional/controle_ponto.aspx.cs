using System;
using System.Web.UI;
using System.Data;

using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class controle_ponto : Page
    {
        private readonly FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL = _funcionarioGLL.ValidaUsuario(_conexaoMDL);

                    if (_funcionarioMDL.N_Acesso == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                            string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
                    }
                    else
                    {
                        txtCpf.Text = _funcionarioMDL.Cpf;

                        CarregaInformacoesPonto();
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

        #region CarregaInformacoesPonto

        public void CarregaInformacoesPonto()
        {
            if (ValidaCampos())
            {
                try
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;

                    _funcionarioMDL = _funcionarioBLL.CarregaInformacoesPonto(_funcionarioMDL);
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }

                if (_funcionarioMDL.H_Saida.DayOfYear != DateTime.Now.DayOfYear)
                {
                    if (_funcionarioMDL.H_Entrada.Ticks == 0)
                    {
                        txtCpf.Enabled = false;
                        pnCadastro.Visible = true;
                    }
                    else if (_funcionarioMDL.H_Entrada.DayOfYear == DateTime.Now.DayOfYear)
                    {
                        txtEntrada.Text = Convert.ToString(_funcionarioMDL.H_Entrada.TimeOfDay);

                        txtCpf.Enabled = false;
                        pnCadastro.Visible = true;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Já consta no sistema entrada e saida para o dia atual');location.href='../home/home.aspx';</script>");
                }
            }
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEntrada.Text == string.Empty)
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;
                    _funcionarioMDL.H_Entrada = DateTime.Now;

                    _funcionarioMDL.H_Entrada = new DateTime(_funcionarioMDL.H_Entrada.Year, _funcionarioMDL.H_Entrada.Month, _funcionarioMDL.H_Entrada.Day,
                        _funcionarioMDL.H_Entrada.Hour, _funcionarioMDL.H_Entrada.Minute, _funcionarioMDL.H_Entrada.Second);

                    _conexaoMDL = _funcionarioBLL.CadastraPontoEntrada(_funcionarioMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('Hora de entrada registrada com sucesso');location.href='../home/home.aspx';</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;
                    _funcionarioMDL.H_Entrada = Convert.ToDateTime(txtEntrada.Text);
                    _funcionarioMDL.H_Saida = DateTime.Now;

                    _funcionarioMDL.H_Saida = new DateTime(_funcionarioMDL.H_Saida.Year, _funcionarioMDL.H_Saida.Month, _funcionarioMDL.H_Saida.Day,
                        _funcionarioMDL.H_Saida.Hour, _funcionarioMDL.H_Saida.Minute, _funcionarioMDL.H_Saida.Second);

                    _conexaoMDL = _funcionarioBLL.CadastraPontoSaida(_funcionarioMDL);

                    if (_conexaoMDL.Validador == false)
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('Hora de saida registrada com sucesso');location.href='../home/home.aspx';</script>");
                }
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
            }
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion
    }
}