using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class lista_ativos : System.Web.UI.Page
    {
        private FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

        private int _linha;
        private string coluna;
        private string parametro;

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

        #region btnPesquisar_Click

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            parametro = txtPesquisa.Text;
            coluna = ddlColuna.SelectedValue;
            CarregaGrid(parametro, coluna);

        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Response.Redirect("lista_ativos.aspx");
        }

        #endregion

        #region CarregaGrid

        private void CarregaGrid(string parametro, string coluna)
        {
            _conexaoMDL2.Ds.Clear();
            _conexaoMDL2 = _funcionarioBLL.PesquisaFuncionarios(parametro, coluna);

            grdFuncionarios.DataSource = _conexaoMDL2.Ds;
            grdFuncionarios.DataBind();

            for (int i = 0; i < _conexaoMDL2.Ds.Tables[0].Rows.Count; i++)
            {
                var lbtNome = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtNome");
                var lbtCargo = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtCargo");
                var lbtCPF = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtCpf");
                var lbtRG = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtRG");

                var lbtCEP = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtCEP");
                var lbtEndereco = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtEndereco");

                var lbtBairro = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtBairro");
                var lbtDtAdm = (LinkButton)grdFuncionarios.Rows[i].FindControl("lbtDtAdm");


                lbtNome.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_Nome"].ToString());
                lbtCargo.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Cargo_Nome"].ToString());
                lbtCPF.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_CPF"].ToString());
                lbtRG.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_RG"].ToString());
                lbtCEP.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_CEP"].ToString());
                lbtEndereco.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_Rua"].ToString());
                lbtBairro.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Fun_Bairro"].ToString());
                lbtDtAdm.Text = Convert.ToString(_conexaoMDL2.Ds.Tables[0].Rows[i]["Admissao_Data"].ToString());
            }
        }

        #endregion

        #region grdFuncionariosRowCommand

        protected void grdFuncionarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Desabilitar")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intCPF = (LinkButton)(grdFuncionarios.Rows[_linha].FindControl("lbtCpf"));

                _funcionarioMDL.Cpf = intCPF.Text;

                _conexaoMDL2 = _funcionarioBLL.DesabilitaFuncionario(_funcionarioMDL);

                _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                var i = 3;
                var j = intCPF.Text;

                _funcionarioBLL.RegistraLog(_conexaoMDL, i, j);

                CarregaGrid(parametro, coluna);
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                   "<script>alert('Conta desabilitada com sucesso');</script>");
            }
            else if (e.CommandName == "Ver")
            {
                _linha = Convert.ToInt32(e.CommandArgument);

                var intCPF = (LinkButton)(grdFuncionarios.Rows[_linha].FindControl("lbtCpf"));
                _funcionarioMDL.Cpf = intCPF.Text;
                _conexaoMDL2.Ds.Clear();

                _funcionarioMDL = _funcionarioBLL.CarregaInformacoesFuncionario(_funcionarioMDL);

                Session["PassaDadosFunc"] = _funcionarioMDL.Registro;

                Response.Redirect("dados_funcionario.aspx");
            }
        }

        #endregion
    }
}