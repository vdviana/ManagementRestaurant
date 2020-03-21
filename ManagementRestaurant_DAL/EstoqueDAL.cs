using System;
using System.Data;
using System.Data.SqlClient;

using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class EstoqueDAL
    {
        private EstoqueMDL _estoqueMDL = new EstoqueMDL();

        private ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CadastraFornecedor

        public ConexaoMDL CadastraFornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_valida_documento_fornecedor(estoqueMDL);

            try
            {
                _estoqueMDL.F_Cnpj = _conexaoMDL.Ds.Tables[0].Rows[0]["For_CNPJ"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.F_Cnpj != _estoqueMDL.F_Cnpj)
            {
                _conexaoMDL = spc_cadastra_fornecedor(estoqueMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region AlteraFornecedor

        public ConexaoMDL AlteraFornecedor(EstoqueMDL estoqueMDL)
        {
           
                _conexaoMDL = spc_altera_fornecedor(estoqueMDL);

                _conexaoMDL.Validador = true;
           
            return _conexaoMDL;
        }

        #endregion

        #region CarregaDadosFornecedor

        public ConexaoMDL CarregaDadosFornecedor(EstoqueMDL estoqueMDL)
        {
            

            _conexaoMDL = spc_carrega_dados_fornecedor(estoqueMDL);

            
            

            return _conexaoMDL;
        }

        #endregion


        #region CadastraProduto

        public ConexaoMDL CadastraProduto(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_valida_nome_produto(estoqueMDL);

            try
            {
                _estoqueMDL.N_Produto = _conexaoMDL.Ds.Tables[0].Rows[0]["Pro_Nome"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.N_Produto != _estoqueMDL.N_Produto)
            {
                _conexaoMDL = spc_cadastra_produto(estoqueMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region CadastraEntradaEstoque

        public ConexaoMDL CadastraEntradaEstoque(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = spc_valida_nota_fiscal(estoqueMDL);

            try
            {
                _estoqueMDL.N_Fiscal = _conexaoMDL.Ds.Tables[0].Rows[0]["NF_Produto"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.N_Fiscal != _estoqueMDL.N_Fiscal)
            {
                _conexaoMDL = spc_cadastra_entrada_estoque(estoqueMDL);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region CadastraPrato

        public ConexaoMDL CadastraPrato(EstoqueMDL estoqueMDL, string[] Ingrediente, string[] I_Quantidade)
        {
            _conexaoMDL = spc_valida_nome_prato(estoqueMDL);

            try
            {
                _estoqueMDL.N_Prato = _conexaoMDL.Ds.Tables[0].Rows[0]["Nome_Prato"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (estoqueMDL.N_Prato != _estoqueMDL.N_Prato)
            {
                _conexaoMDL = spc_cadastra_prato(estoqueMDL);

                estoqueMDL.C_Prato = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["ID_Prato"].ToString());

                _conexaoMDL = spc_cadastra_ingrediente_prato(estoqueMDL, Ingrediente, I_Quantidade);

                _conexaoMDL.Validador = true;
            }
            else
            {
                _conexaoMDL.Validador = false;
            }

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_documento_fornecedor

        public ConexaoMDL spc_valida_documento_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_documento_fornecedor") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.F_Cnpj));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_nome_produto

        public ConexaoMDL spc_valida_nome_produto(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_nome_produto") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_produto", estoqueMDL.N_Produto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_fornecedor

        public ConexaoMDL spc_cadastra_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_fornecedor") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_fornecedor", estoqueMDL.F_Nome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_fornecedor", estoqueMDL.F_Telefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.F_Cnpj));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_fornecedor", estoqueMDL.F_Cep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_fornecedor", estoqueMDL.F_Rua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_fornecedor", estoqueMDL.F_NEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_fornecedor", estoqueMDL.F_Bairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_fornecedor", estoqueMDL.F_Cidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_fornecedor", estoqueMDL.F_Estado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@complemento_fornecedor", estoqueMDL.F_Complemento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@email_fornecedor", estoqueMDL.F_Email));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_altera_fornecedor

        public ConexaoMDL spc_altera_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_altera_fornecedor") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_fornecedor", estoqueMDL.F_Nome));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@telefone_fornecedor", estoqueMDL.F_Telefone));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.F_Cnpj));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cep_fornecedor", estoqueMDL.F_Cep));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@endereco_fornecedor", estoqueMDL.F_Rua));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_fornecedor", estoqueMDL.F_NEstabelecimento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@bairro_fornecedor", estoqueMDL.F_Bairro));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cidade_fornecedor", estoqueMDL.F_Cidade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado_fornecedor", estoqueMDL.F_Estado));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@complemento_fornecedor", estoqueMDL.F_Complemento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@email_fornecedor", estoqueMDL.F_Email));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_fornecedor

        public ConexaoMDL spc_carrega_nome_fornecedor(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_fornecedor") { CommandType = CommandType.StoredProcedure };

            conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            conexaoMDL.Da = new SqlDataAdapter(conexaoMDL.Cmd);
            conexaoMDL.Da.Fill(conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return conexaoMDL;
        }

        #endregion

        #region spc_carrega_tipo_produto

        public ConexaoMDL spc_carrega_tipo_produto(ConexaoMDL conexaoMDL)
        {
            _conexaoDAL.Conexao.Open();

            conexaoMDL.Cmd = new SqlCommand("spc_carrega_tipo_produto") { CommandType = CommandType.StoredProcedure };

            conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            conexaoMDL.Da = new SqlDataAdapter(conexaoMDL.Cmd);
            conexaoMDL.Da.Fill(conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_produto

        public ConexaoMDL spc_carrega_nome_produto(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_produto") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cod_produto", estoqueMDL.C_Produto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_produto

        public ConexaoMDL spc_cadastra_produto(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_produto") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_produto", estoqueMDL.N_Produto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_produto", estoqueMDL.T_Produto));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_nota_fiscal

        public ConexaoMDL spc_valida_nota_fiscal(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_nota_fiscal") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nf_produto", estoqueMDL.N_Fiscal));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_dados_fornecedor

        public ConexaoMDL spc_carrega_dados_fornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_dados_fornecedor") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.F_Cnpj));
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_entrada_estoque

        public ConexaoMDL spc_cadastra_entrada_estoque(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_entrada_estoque") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nf_produto", estoqueMDL.N_Fiscal));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo_compra", estoqueMDL.T_Compra));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@lote", estoqueMDL.Lote));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cnpj_fornecedor", estoqueMDL.F_Cnpj));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cod_produto", estoqueMDL.C_Produto));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@validade", estoqueMDL.Validade));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade", estoqueMDL.Quantidade));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_produto_dropdown

        public ConexaoMDL spc_carrega_nome_produto_dropdown()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_produto_dropdown") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_nome_prato

        public ConexaoMDL spc_valida_nome_prato(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_nome_prato") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_prato", estoqueMDL.N_Prato));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_prato

        public ConexaoMDL spc_cadastra_prato(EstoqueMDL estoqueMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_prato") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@nome_prato", estoqueMDL.N_Prato));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@valor_prato", estoqueMDL.V_Prato));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_ingrediente_prato

        public ConexaoMDL spc_cadastra_ingrediente_prato(EstoqueMDL estoqueMDL, string[] Ingrediente, string[] I_Quantidade)
        {
            int i;

            for (i = 0; i <= Ingrediente.Length -1; i++)
            {
                _conexaoDAL.Conexao.Open();

                _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_ingrediente_prato") { CommandType = CommandType.StoredProcedure };

                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@ingrediente_prato", Ingrediente[i]));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@id_prato", estoqueMDL.C_Prato));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_ingrediente", Convert.ToDouble(I_Quantidade[i])));

                _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
                _conexaoMDL.Cmd.ExecuteNonQuery();

                _conexaoDAL.Conexao.Close();
            }

            return _conexaoMDL;
        }

        #endregion

        #region spc_carrega_nome_prato_dropdown

        public ConexaoMDL spc_carrega_nome_prato_dropdown()
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_carrega_nome_prato_dropdown") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        #endregion
    }
}