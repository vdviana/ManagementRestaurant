using System;
using System.Web.UI;
using System.Data;

using ManagementRestaurant_BLL;
using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL
{
    public partial class login : Page
    {
        FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        FuncionarioBLL _funcionarioBLL = new FuncionarioBLL();
        FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region btnLogar_Click

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('Preencha todos os campos para efetuar o login corretamente');</script>");
            }
            else
            {
                _conexaoMDL.Ds.Clear();

                _funcionarioMDL.Cpf = txtCpf.Text;
                _funcionarioMDL.Senha = txtSenha.Text;

                _conexaoMDL = _funcionarioBLL.CarregaFuncionario(_funcionarioMDL);

                if (_conexaoMDL.Validador)
                {
                    _conexaoMDL = _funcionarioGLL.ValidaSenha(_conexaoMDL);

                    if (_conexaoMDL.Validador)
                    {
                        pnLogin.Visible = false;
                        pnSenhaN.Visible = true;

                        Session["CarregaDS"] = _conexaoMDL.Ds;
                    }
                    else
                    {
                        _conexaoMDL = _funcionarioGLL.ValidaStatus(_conexaoMDL);

                        _conexaoMDL = _funcionarioBLL.ValidaPeriodoFerias(_funcionarioMDL);

                        if (_conexaoMDL.Validador == true && _conexaoMDL.Validador2 == true)
                        {
                            Session["PassaInfo"] = _conexaoMDL.Ds;

                            Response.Redirect("~/modulos/home/home.aspx");
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                        "<script>alert('Voce não está autorizado a acessar o sistema');location.href='login.aspx';</script>");
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                                "<script>alert('CPF ou senha incorretos');</script>");
                }
            }
        }

        #endregion

        #region btnConfirmar_Click

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtSenhaN.Text.Equals(txtSenhaC.Text))
            {
                _conexaoMDL.Ds = (DataSet) Session["CarregaDS"];

                _funcionarioMDL.Senha = txtSenhaN.Text;

                _conexaoMDL = _funcionarioGLL.ValidaDocumentos(_conexaoMDL, _funcionarioMDL);

                if (_conexaoMDL.Validador)
                {
                    Session["PassaInfo"] = _conexaoMDL.Ds;

                    _conexaoMDL = _funcionarioBLL.AtualizaSenha(_funcionarioMDL);

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                        "<script>alert('Senha atualizada com sucesso');location.href='/modulos/home/home.aspx';</script>");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                            "<script>alert('A senha não pode ser igual a nenhum de seus documentos cadastrados');</script>"); 
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                                            "<script>alert('As senhas não conferem');</script>"); 
            }
        }

        #endregion
    }
}