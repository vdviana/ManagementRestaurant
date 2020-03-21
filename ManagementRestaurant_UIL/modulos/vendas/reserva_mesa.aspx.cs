using System;
using System.Web.UI;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

using ManagementRestaurant_MDL;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.vendas
{
    public partial class reserva_mesa : System.Web.UI.Page
    {
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ClienteMDL _clienteMDL = new ClienteMDL();
        private ClienteBLL _clienteBLL = new ClienteBLL();
        private ClienteGLL _clienteGLL = new ClienteGLL();

        private PedidoMDL _pedidoMDL = new PedidoMDL();
        private PedidoBLL _pedidoBLL = new PedidoBLL();

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

                    if (_funcionarioMDL.N_Acesso != 1 && _funcionarioMDL.N_Acesso != 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "'alertscript",
                            string.Format("window.alert(\"{0}\");history.go(-{1});", "Voce não está autorizado a acessar a página", 1), true);
                    }
                    else
                    {
                        txtCpfFuncionario.Text = _funcionarioMDL.Cpf;
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
            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Digite o CPF para consulta');</script>");
            }
            else
            {
                _clienteMDL.Documento = txtCpf.Text;

                _conexaoMDL.Validador = _clienteGLL.ValidaCPF(txtCpf.Text);

                if (_conexaoMDL.Validador == false)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF inválido!');</script>");
                }
                else
                {
                    _clienteMDL.Documento = txtCpf.Text;

                    try
                    {
                        _clienteMDL = _clienteBLL.CarregaInformacoesClienteReserva(_clienteMDL);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                    }

                    if (_clienteMDL.Validador == false)
                    {
                        txtCpf.Text = string.Empty;

                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                    "<script>alert('CPF não consta no sistema');</script>");
                    }
                    else
                    {
                        txtNome.Text = _clienteMDL.Nome;
                        txtTelefone.Text = _clienteMDL.Telefone;

                        txtCpf.Enabled = false;
                        pnReserva.Visible = true;
                    }
                }
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            calData.Enabled = true;
            ddlHora.Enabled = true;
            ddlMesa.Enabled = true;

            ddlHora.SelectedValue = ("0");
            ddlMesa.SelectedValue = ("0");

            lblStatusMesa.Visible = false;
            btnAplicar.Enabled = false;
        }

        #endregion

        #region ddlMesa_SelectedIndexChanged

        protected void ddlMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (calData.SelectedDate.Ticks != 0 && ddlHora.SelectedValue != "0")
            {
                _pedidoMDL.D_Reserva = calData.SelectedDate.Date;
                _pedidoMDL.H_Reserva = Convert.ToDateTime(ddlHora.SelectedValue);

                _pedidoMDL.Reserva = new DateTime(_pedidoMDL.D_Reserva.Year, _pedidoMDL.D_Reserva.Month, _pedidoMDL.D_Reserva.Day,
                          _pedidoMDL.H_Reserva.Hour, _pedidoMDL.H_Reserva.Minute, _pedidoMDL.H_Reserva.Second);

                _pedidoMDL.Mesa = Convert.ToInt32(ddlMesa.SelectedValue);

                _conexaoMDL = _pedidoBLL.ValidaMesa(_pedidoMDL);

                if (_conexaoMDL.Validador == false)
                {
                    calData.Enabled = false;
                    ddlHora.Enabled = false;
                    ddlMesa.Enabled = false;

                    btnAplicar.Enabled = true;
                    lblStatusMesa.Visible = true;
                    lblStatusMesa.ForeColor = Color.Green;
                    lblStatusMesa.Text = ("Mesa disponível");
                }
                else
                {
                    lblStatusMesa.Visible = true;
                    lblStatusMesa.ForeColor = Color.Red;
                    lblStatusMesa.Text = ("Mesa indisponível");
                }
            }
            else
            {
                ddlMesa.SelectedValue = ("0");

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                           "<script>alert('Preencha todos os campos para efetuar a validação corretamente');</script>");
            }
        }

        #endregion

        #region btnAplicar_Click

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            _funcionarioMDL.Cpf = txtCpfFuncionario.Text;
            _clienteMDL.Documento = txtCpf.Text;

            _pedidoMDL.D_Reserva = calData.SelectedDate.Date;
            _pedidoMDL.H_Reserva = Convert.ToDateTime(ddlHora.SelectedValue);

            _pedidoMDL.Reserva = new DateTime(_pedidoMDL.D_Reserva.Year, _pedidoMDL.D_Reserva.Month, _pedidoMDL.D_Reserva.Day,
                      _pedidoMDL.H_Reserva.Hour, _pedidoMDL.H_Reserva.Minute, _pedidoMDL.H_Reserva.Second);

            _pedidoMDL.Mesa = Convert.ToInt32(ddlMesa.SelectedValue);

            if (ValidaDatas(_pedidoMDL))
            {
                try
                {
                    _pedidoBLL.CadastraReserva(_funcionarioMDL, _clienteMDL, _pedidoMDL);

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>");
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                   "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ValidaDatas

        private Boolean ValidaDatas(PedidoMDL pedidoMDL)
        {
            int ValidaReserva = DateTime.Compare(pedidoMDL.D_Reserva, DateTime.Today);

            if (ValidaReserva < 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('A data inicial ou final da reserva não pode ser anterior ao dia atual');</script>");

                return false;
            }

            return true;
        }

        #endregion
    }
}