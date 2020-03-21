using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class dados_fornecedor : Page
    {
        private EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private EstoqueGLL _estoqueGLL = new EstoqueGLL();
        private EstoqueMDL _estoqueMDL = new EstoqueMDL();

        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private ConexaoMDL _conexaoMDL2 = new ConexaoMDL();

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
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                "<script>alert('Sua sessão foi encerrada automaticamente por por atingir o tempo limite de conexão, faça o login novamente para iniciar uma nova sessão');location.href='../login.aspx';</script>");
                }
            }
        }

        #endregion

        #region btnAlterar_Click

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            txtBairro.Enabled = true;
            txtCep.Enabled = true;
            txtCidade.Enabled = true;
            txtComplemento.Enabled = true;
            txtEmail.Enabled = true;
            txtEndereco.Enabled = true;
            txtEstado.Enabled = true;
            txtNome.Enabled = true;
            txtNumero.Enabled = true;
            txtTelefone.Enabled = true;
            btnConcluirAlteracao.Visible = true;
            btnBack.Visible = true;
            btnAlterar.Visible = false;
            btnCancelar.Visible = true;
        }

        #endregion

        #region btnBuscar_Click

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                _estoqueMDL.F_Cep = txtCep.Text;

                _estoqueMDL = _estoqueGLL.PesquisaCEP(_estoqueMDL);

                txtCidade.Text = _estoqueMDL.F_Cidade;
                txtEstado.Text = _estoqueMDL.F_Estado;
                txtBairro.Text = _estoqueMDL.F_Bairro;
                txtEndereco.Text = _estoqueMDL.F_Rua;

                txtNumero.Focus();
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('O serviço de preenchimento automático não está disponivel no momento');</script>");

                txtEndereco.Focus();
            }
        }

        #endregion

        #region AlteraFornecedor

        private void AlteraFornecedor()
        {
            _estoqueMDL.F_Nome = txtNome.Text;
            _estoqueMDL.F_Telefone = txtTelefone.Text;
            _estoqueMDL.F_Email = txtEmail.Text;
            _estoqueMDL.F_Cnpj = txtCnpj.Text;
            _estoqueMDL.F_Cep = txtCep.Text;
            _estoqueMDL.F_Rua = txtEndereco.Text;
            _estoqueMDL.F_NEstabelecimento = txtNumero.Text;
            _estoqueMDL.F_Bairro = txtBairro.Text;
            _estoqueMDL.F_Cidade = txtCidade.Text;
            _estoqueMDL.F_Estado = txtEstado.Text;
            _estoqueMDL.F_Complemento = txtComplemento.Text;
            _estoqueMDL.F_Email = txtEmail.Text;

            try
            {
                _conexaoMDL2 = _estoqueBLL.AlteraFornecedor(_estoqueMDL);

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Alteração efetuada com sucesso');location.href='../alteracao/dados_fornecedor.aspx';</script>");
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
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text) ||
                string.IsNullOrWhiteSpace(txtCnpj.Text) ||
                string.IsNullOrWhiteSpace(txtCep.Text) || string.IsNullOrWhiteSpace(txtEndereco.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) || string.IsNullOrWhiteSpace(txtBairro.Text) ||
                string.IsNullOrWhiteSpace(txtCidade.Text) || string.IsNullOrWhiteSpace(txtEstado.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ddlFornecedor_Load

        protected void ddlFornecedor_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ddlFornecedor.Items.Count == 0)
                {
                    try
                    {
                        _conexaoMDL2 = _estoqueBLL.CarregaFornecedor(_conexaoMDL2);

                        ddlFornecedor.DataTextField = _conexaoMDL2.Ds.Tables[0].Columns[0].ToString();
                        ddlFornecedor.DataValueField = _conexaoMDL2.Ds.Tables[0].Columns[1].ToString();
                        ddlFornecedor.DataSource = _conexaoMDL2.Ds;
                        ddlFornecedor.DataBind();

                        ddlFornecedor.Items.Insert(0, new ListItem("Selecione...", "0"));
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

        #region LimpaCampos

        private void LimpaCampos()
        {
            txtNome.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCep.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtComplemento.Text = string.Empty;
        }

        #endregion

        #region ddlFornecedor_SelectedIndexChanged

        protected void ddlFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            _estoqueMDL.F_Cnpj = ddlFornecedor.SelectedValue;

            _estoqueMDL = _estoqueBLL.CarregaDadosFornecedor(_estoqueMDL);

            txtNome.Text = _estoqueMDL.F_Nome;
            txtTelefone.Text = _estoqueMDL.F_Telefone;
            txtEmail.Text = _estoqueMDL.F_Email;
            txtCnpj.Text = _estoqueMDL.F_Cnpj;
            txtCep.Text = _estoqueMDL.F_Cep;
            txtEndereco.Text = _estoqueMDL.F_Rua;
            txtNumero.Text = _estoqueMDL.F_NEstabelecimento;
            txtBairro.Text = _estoqueMDL.F_Bairro;
            txtCidade.Text = _estoqueMDL.F_Cidade;
            txtEstado.Text = _estoqueMDL.F_Estado;
            txtComplemento.Text = _estoqueMDL.F_Complemento;
            txtEmail.Text = _estoqueMDL.F_Email;

            pnAlteracao.Visible = true;
            btnAlterar.Visible = true;
            btnBack.Visible = true;
        }

        #endregion

        #region btnConcluirAlteracao_Click

        protected void btnConcluirAlteracao_Click(object sender, EventArgs e)
        {
            {
                if (ValidaCampos())
                {
                    {
                        AlteraFornecedor();
                    }
                }
            }
        }

        #endregion

        #region BtnBack_Click

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("dados_fornecedor.aspx");
        }

        #endregion

        #region btnCancelar_Click

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/modulos/home/home.aspx");
        }

        #endregion
    }
}