using System;
using System.Web.UI;
using System.Data;

using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class ferias_funcionario : Page
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

                    if (_funcionarioMDL.N_Acesso != 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                            string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
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

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            _funcionarioMDL.I_Ferias = calI_Ferias.SelectedDate;
            _funcionarioMDL.F_Ferias = calF_Ferias.SelectedDate;

            if (ValidaCampos())
            {
                if (string.IsNullOrWhiteSpace(txtProventos.Text))
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Preencha inicial e final nos calendários');</script>");
                }
                else
                {
                    AplicaFerias();
                }
            }
        }

        #endregion

        #region LimpaCampos

        private void LimpaCampos()
        {
            txtCpf.Text = string.Empty;
            txtDias.Text = string.Empty;
            txtHora.Text = string.Empty;
            txtProventos.Text = string.Empty;

            txtCpf.Enabled = true;
            txtDias.Enabled = true;
            txtHora.Enabled = true;

            pnCadastro.Visible = false;
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (_funcionarioMDL.I_Ferias.Ticks == 0 || _funcionarioMDL.F_Ferias.Ticks == 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha inicial e final nos calendários');</script>");

                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtDias.Text) || string.IsNullOrWhiteSpace(txtHora.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region AplicaFerias

        private void AplicaFerias()
        {
            _funcionarioMDL.Cpf = txtCpf.Text;
            _funcionarioMDL.Cargo = txtCargo.Text;

            _funcionarioMDL.D_Admissao = Convert.ToDateTime(txtAdmissao.Text);

            _funcionarioMDL.D_Uteis = Convert.ToInt16(txtDias.Text);
            _funcionarioMDL.Proventos = Convert.ToDouble(txtProventos.Text);

            int ValidaFeriasI = DateTime.Compare(_funcionarioMDL.I_Ferias, DateTime.Today);
            int ValidaFeriasF = DateTime.Compare(_funcionarioMDL.F_Ferias, DateTime.Today);

            if (ValidaFeriasI < 0 || ValidaFeriasF < 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('A data inicial ou final das férias não pode ser anterior ao dia atual');</script>");
            }
            else
            {
                TimeSpan ValidaUteis = _funcionarioMDL.F_Ferias - _funcionarioMDL.I_Ferias;
                int Intervalo = ValidaUteis.Days;

                if (_funcionarioMDL.D_Uteis > Intervalo)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Os dias úteis não podem ser maiores que o intervalo das férias');</script>");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtProventos.Text) || txtProventos.Text == "0")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('É necessário calcular os proventos antes de aplicar as férias');</script>");
                    }

                    TimeSpan ValidaPermanencia = _funcionarioMDL.I_Ferias -
                                                 Convert.ToDateTime(_funcionarioMDL.D_Admissao);

                    int Permanencia = ValidaPermanencia.Days;

                    if (Permanencia < 364)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Só é possivel aplicar férias a funcionários com permanencia minima de um ano');</script>");
                    }
                    else if (Convert.ToInt16(txtHora.Text) > 24)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('A quantidade máxima de horas trabalhadas não podem exceder 24 horas');</script>");
                    }
                    else
                    {
                        try
                        {
                            _conexaoMDL = _funcionarioBLL.AplicaFerias(_funcionarioMDL);

                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        _conexaoMDL.Validador
                                                                            ? "<script>alert('Férias aplicadas com sucesso');location.href='../home/home.aspx';</script>"
                                                                            : "<script>alert('Já existe férias cadastradas para este funcionário nesse periodo');</script>");
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

        #region btnCalcular_Click

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            _funcionarioMDL.I_Ferias = calI_Ferias.SelectedDate;
            _funcionarioMDL.F_Ferias = calF_Ferias.SelectedDate;

            if (ValidaCampos())
            {
                _funcionarioMDL.S_Hora = Convert.ToDouble(txtS_Hora.Text);
                _funcionarioMDL.D_Uteis = Convert.ToInt16(txtDias.Text);
                _funcionarioMDL.Proventos = _funcionarioMDL.D_Uteis*Convert.ToInt16(txtHora.Text)*_funcionarioMDL.S_Hora;

                txtProventos.Text = _funcionarioMDL.Proventos.ToString();

                calI_Ferias.Enabled = false;
                calF_Ferias.Enabled = false;
                txtDias.Enabled = false;
                txtHora.Enabled = false;
            }
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Digite o CPF para consulta');</script>");
            }
            else
            {
                _funcionarioMDL.Cpf = txtCpf.Text;

                _conexaoMDL.Validador = _funcionarioGLL.ValidaCPF(txtCpf.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    _funcionarioMDL.Cpf = txtCpf.Text;

                    try
                    {
                        _funcionarioMDL = _funcionarioBLL.CarregaInformacoesFuncionario(_funcionarioMDL);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }

                    if (_funcionarioMDL.Validador == false)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CPF não consta no sistema');</script>");
                    }
                    else
                    {
                        txtNome.Text = _funcionarioMDL.Nome;
                        txtTelefone.Text = _funcionarioMDL.Telefone;
                        txtRg.Text = _funcionarioMDL.Rg;
                        txtCnh.Text = _funcionarioMDL.Cnh;
                        txtAdmissao.Text = Convert.ToString(_funcionarioMDL.D_Admissao).Substring(0,10);
                        txtN_Ctrabalho.Text = _funcionarioMDL.C_Trabalho;
                        txtCargo.Text = _funcionarioMDL.Cargo;
                        txtS_Hora.Text = Convert.ToString(_funcionarioMDL.S_Hora);

                        txtCep.Text = _funcionarioMDL.Cep;
                        txtEndereco.Text = _funcionarioMDL.Rua;
                        txtNumero.Text = _funcionarioMDL.N_Estabelecimento;
                        txtBairro.Text = _funcionarioMDL.Bairro;
                        txtCidade.Text = _funcionarioMDL.Cidade;
                        txtEstado.Text = _funcionarioMDL.Estado;

                        txtCpf.Enabled = false;
                        pnCadastro.Visible = true;
                    }
                }
            }
        }

        #endregion
    }
}