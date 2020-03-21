using System;
using System.Data;

using ManagementRestaurant_MDL;
using ManagementRestaurant_GLL;

namespace ManagementRestaurant_UIL.master
{
    public partial class principal : System.Web.UI.MasterPage
    {
        FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();
        FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PassaInfo"] != null)
                {
                    _conexaoMDL.Ds = (DataSet)Session["PassaInfo"];

                    _funcionarioMDL.Nome = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();

                    lblNome.Text = _funcionarioMDL.Nome + "!";
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "alertscript",
                                               "<script>alert('Sua sessão foi encerrada automaticamente por por atingir o tempo limite de conexão, faça o login novamente para iniciar uma nova sessão');location.href='../login.aspx';</script>");
                }
            }
        }

        #endregion

        #region lbtSair_Click

        protected void lbtSair_Click(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        #endregion
    }
}