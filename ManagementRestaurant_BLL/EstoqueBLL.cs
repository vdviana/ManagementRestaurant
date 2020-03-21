using System;

using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class EstoqueBLL
    {
        private EstoqueMDL _estoqueMDL = new EstoqueMDL();
        private EstoqueDAL _estoqueDAL = new EstoqueDAL();
        private EstoqueGLL _estoqueGLL = new EstoqueGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CadastraFornecedor

        public ConexaoMDL CadastraFornecedor(EstoqueMDL estoqueMDL)
        {
            estoqueMDL = _estoqueGLL.TrataDados(estoqueMDL);

            return _estoqueDAL.CadastraFornecedor(estoqueMDL);
        }

        #endregion

        #region AlteraFornecedor

        public ConexaoMDL AlteraFornecedor(EstoqueMDL estoqueMDL)
        {
            estoqueMDL = _estoqueGLL.TrataDados(estoqueMDL);

            return _estoqueDAL.AlteraFornecedor(estoqueMDL);
        }

        #endregion

        #region CarregaFornecedor

        public ConexaoMDL CarregaFornecedor(ConexaoMDL conexaoMDL)
        {
            return _estoqueDAL.spc_carrega_nome_fornecedor(_conexaoMDL);
        }

        #endregion

        #region CarregaDadosFornecedor

        public EstoqueMDL CarregaDadosFornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = _estoqueDAL.CarregaDadosFornecedor( estoqueMDL);
            
            estoqueMDL.F_Bairro= _conexaoMDL.Ds.Tables[0].Rows[0]["For_Bairro"].ToString();
            estoqueMDL.F_Rua= _conexaoMDL.Ds.Tables[0].Rows[0]["For_Rua"].ToString();
            estoqueMDL.F_Nome= _conexaoMDL.Ds.Tables[0].Rows[0]["For_Nome"].ToString();
            estoqueMDL.F_Complemento= _conexaoMDL.Ds.Tables[0].Rows[0]["For_Complemento"].ToString();
            estoqueMDL.F_Telefone= _conexaoMDL.Ds.Tables[0].Rows[0]["For_Telefone"].ToString();
            estoqueMDL.F_Cidade= _conexaoMDL.Ds.Tables[0].Rows[0]["For_Cidade"].ToString();
            estoqueMDL.F_Estado= _conexaoMDL.Ds.Tables[0].Rows[0]["For_Estado"].ToString();
            estoqueMDL.F_Cep= _conexaoMDL.Ds.Tables[0].Rows[0]["For_CEP"].ToString();
            estoqueMDL.F_Email = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Email"].ToString();
            estoqueMDL.F_NEstabelecimento = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Num"].ToString();

            return estoqueMDL;
        }

        #endregion

        #region CarregaTipoProduto

        public ConexaoMDL CarregaTipoProduto(ConexaoMDL conexaoMDL)
        {
            return _estoqueDAL.spc_carrega_tipo_produto(_conexaoMDL);
        }

        #endregion

        #region CadastraProduto

        public ConexaoMDL CadastraProduto(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = _estoqueDAL.CadastraProduto(estoqueMDL);

            try
            {
                _estoqueMDL.C_Produto = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Produto"]);
            }
            catch (Exception)
            { }

            return _conexaoMDL;
        }

        #endregion

        #region CarregaNomeProduto

        public EstoqueMDL CarregaNomeProduto(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = _estoqueDAL.spc_carrega_nome_produto(estoqueMDL);

            try
            {
                _estoqueMDL.N_Produto = _conexaoMDL.Ds.Tables[0].Rows[0]["Pro_Nome"].ToString();

                _estoqueMDL.Validador = true;
            }
            catch
            {
                _estoqueMDL.Validador = false;
            }

            return _estoqueMDL;
        }

        #endregion

        #region CadastraEntradaEstoque

        public ConexaoMDL CadastraEntradaEstoque(EstoqueMDL estoqueMDL)
        {
            return _estoqueDAL.CadastraEntradaEstoque(estoqueMDL);
        }

        #endregion

        #region CarregaNomeProdutoDropdown

        public ConexaoMDL CarregaNomeProdutoDropdown()
        {
            return _estoqueDAL.spc_carrega_nome_produto_dropdown();
        }

        #endregion

        #region CadastraPrato

        public ConexaoMDL CadastraPrato(EstoqueMDL estoqueMDL, string[] Ingrediente, string[] I_Quantidade)
        {
            return _estoqueDAL.CadastraPrato(estoqueMDL, Ingrediente, I_Quantidade);
        }

        #endregion

        #region CarregaNomePratoDropdown

        public ConexaoMDL CarregaNomePratoDropdown()
        {
            return _estoqueDAL.spc_carrega_nome_prato_dropdown();
        }

        #endregion
    }
}



















