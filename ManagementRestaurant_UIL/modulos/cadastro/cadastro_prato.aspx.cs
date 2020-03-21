using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

using ManagementRestaurant_MDL;
using ManagementRestaurant_BLL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.modulos.alteracao
{
    public partial class cadastro_prato : System.Web.UI.Page
    {
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private EstoqueBLL _estoqueBLL = new EstoqueBLL();
        private EstoqueMDL _estoqueMDL = new EstoqueMDL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        int i = 5;

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

        #region ddlIngrediente1_Load

        protected void ddlIngrediente1_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _conexaoMDL = _estoqueBLL.CarregaNomeProdutoDropdown();

                    ddlIngrediente1.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlIngrediente1.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlIngrediente1.DataSource = _conexaoMDL.Ds;
                    ddlIngrediente1.DataBind();

                    ddlIngrediente1.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region ddlIngrediente2_Load

        protected void ddlIngrediente2_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    ddlIngrediente2.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlIngrediente2.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlIngrediente2.DataSource = _conexaoMDL.Ds;
                    ddlIngrediente2.DataBind();

                    ddlIngrediente2.Items.Insert(0, new ListItem("Selecione...", "0"));

                    ddlIngrediente3.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlIngrediente3.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlIngrediente3.DataSource = _conexaoMDL.Ds;
                    ddlIngrediente3.DataBind();

                    ddlIngrediente3.Items.Insert(0, new ListItem("Selecione...", "0"));

                    ddlIngrediente4.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlIngrediente4.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlIngrediente4.DataSource = _conexaoMDL.Ds;
                    ddlIngrediente4.DataBind();

                    ddlIngrediente4.Items.Insert(0, new ListItem("Selecione...", "0"));

                    ddlIngrediente5.DataTextField = _conexaoMDL.Ds.Tables[0].Columns[0].ToString();
                    ddlIngrediente5.DataValueField = _conexaoMDL.Ds.Tables[0].Columns[1].ToString();
                    ddlIngrediente5.DataSource = _conexaoMDL.Ds;
                    ddlIngrediente5.DataBind();

                    ddlIngrediente5.Items.Insert(0, new ListItem("Selecione...", "0"));
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                              "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region btnCadastrar_Click

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos() && ValidaCampos0())
            {
                _estoqueMDL.N_Prato = txtNomePrato.Text;
                _estoqueMDL.V_Prato = Convert.ToDouble(txtValorPrato.Text);

                i = ValidaDropDown();

                String[] Ingrediente = new string[i];
                String[] I_Quantidade = new string[i];

                i = 0;

                Ingrediente[i] = ddlIngrediente1.Text;
                I_Quantidade[i] = txtQuantidade1.Text;

                if (ddlIngrediente2.SelectedValue != "0" && txtQuantidade2.Text != string.Empty)
                {
                    i = i + 1;

                    Ingrediente[i] = ddlIngrediente2.Text;
                    I_Quantidade[i] = txtQuantidade2.Text;
                }

                if (ddlIngrediente3.SelectedValue != "0" && txtQuantidade3.Text != string.Empty)
                {
                    i = i + 1;

                    Ingrediente[i] = ddlIngrediente3.Text;
                    I_Quantidade[i] = txtQuantidade3.Text;
                }

                if (ddlIngrediente4.SelectedValue != "0" && txtQuantidade4.Text != string.Empty)
                {
                    i = i + 1;

                    Ingrediente[i] = ddlIngrediente4.Text;
                    I_Quantidade[i] = txtQuantidade4.Text;
                }

                if (ddlIngrediente5.SelectedValue != "0" && txtQuantidade5.Text != string.Empty)
                {
                    i = i + 1;

                    Ingrediente[i] = ddlIngrediente5.Text;
                    I_Quantidade[i] = txtQuantidade5.Text;
                }

                try
                {
                    _conexaoMDL = _estoqueBLL.CadastraPrato(_estoqueMDL, Ingrediente, I_Quantidade);

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                            _conexaoMDL.Validador
                                ? "<script>alert('Cadastro efetuado com sucesso');location.href='../home/home.aspx';</script>"
                                : "<script>alert('Prato já consta no sistema');</script>");
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                            "<script>alert('Ocorreu um erro de comunicação com a base de dados, tente novamente mais tarde');</script>");
                }
            }
        }

        #endregion

        #region btnLimpar_Click

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomePrato.Text = string.Empty;
            txtValorPrato.Text = string.Empty;
            ddlIngrediente1.SelectedValue = "0";
            ddlIngrediente2.SelectedValue = "0";
            ddlIngrediente3.SelectedValue = "0";
            ddlIngrediente4.SelectedValue = "0";
            ddlIngrediente5.SelectedValue = "0";
            txtQuantidade1.Text = string.Empty;
            txtQuantidade2.Text = string.Empty;
            txtQuantidade3.Text = string.Empty;
            txtQuantidade4.Text = string.Empty;
            txtQuantidade5.Text = string.Empty;
        }

        #endregion

        #region ValidaCampos

        private Boolean ValidaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNomePrato.Text) || string.IsNullOrWhiteSpace(txtValorPrato.Text) ||
                ddlIngrediente1.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtQuantidade1.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o cadastro corretamente');</script>");

                return false;
            }

            return true;
        }

        #endregion

        #region ValidaDropDown

        public int ValidaDropDown()
        {
            if (ddlIngrediente5.SelectedValue == "0")
            {
                i = i - 1;            
            }

            if (ddlIngrediente4.SelectedValue == "0")
            {
                i = i - 1;
            }

            if (ddlIngrediente3.SelectedValue == "0")
            {
                i = i - 1;
            }

            if (ddlIngrediente2.SelectedValue == "0")
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
                if (Convert.ToDouble(txtQuantidade1.Text) == 0)
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