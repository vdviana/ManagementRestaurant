using System;
using System.Data;
using System.Data.SqlClient;

using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class PedidoDAL
    {
        private ClienteMDL _clienteMDL = new ClienteMDL();
        private ClienteDAL _clienteDAL = new ClienteDAL();

        private PedidoMDL _pedidoMDL = new PedidoMDL();

        private ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        int i, j;

        #region CadastraPedido

        public ConexaoMDL CadastraPedido(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL, string[] Prato, string[] P_Quantidade)
        {
            _conexaoMDL = _clienteDAL.spc_valida_documento_cliente(clienteMDL);

            try
            {
                _clienteMDL.Documento = _conexaoMDL.Ds.Tables[0].Rows[0]["Doc_Cliente"].ToString();
            }
            catch
            {
                _conexaoMDL.Validador = false;
            }

            if (clienteMDL.Documento == _clienteMDL.Documento)
            {
                _conexaoMDL.Ds.Clear();

                _conexaoMDL = spc_cadastra_pedido(funcionarioMDL, clienteMDL, pedidoMDL);

                pedidoMDL.C_Pedido = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["Numero_Pedido"]);

                for (i = 0; i <= Prato.Length - 1; i++)
                {
                    _conexaoMDL = spc_cadastra_item_pedido(pedidoMDL, Prato, P_Quantidade);

                    _conexaoMDL = spc_cadastra_baixa_ingrediente(_conexaoMDL, P_Quantidade);
                }

                _conexaoMDL.Validador = true;
            }

            return _conexaoMDL;
        }

        #endregion

        #region PesquisaPedidos
        public ConexaoMDL PesquisaPedidos(string tipo)
        {
            return spc_pesquisa_pedidos(tipo);
        }
        #endregion

        #region spc_pesquisa_pedidos
    public ConexaoMDL spc_pesquisa_pedidos(string tipo)
    {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_pesquisa_pedidos") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipo", tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;

    }
        #endregion

        #region spc_cadastra_pedido

        public ConexaoMDL spc_cadastra_pedido(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_pedido") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcioanario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_tipo", pedidoMDL.Tipo));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_valor", pedidoMDL.ValorTotal));

            if (pedidoMDL.I_Contrato.Ticks == 0 && pedidoMDL.F_Contrato.Ticks == 0)
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_quantidade", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_inicio", null));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_fim", null));
            }
            else
            {
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_quantidade", pedidoMDL.Q_Dia));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_inicio", pedidoMDL.I_Contrato));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_fim", pedidoMDL.F_Contrato));
            }

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_item_pedido

        public ConexaoMDL spc_cadastra_item_pedido(PedidoMDL pedidoMDL, string[] Prato, string[] P_Quantidade)
        {
            _conexaoMDL.Ds2.Clear();

            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_item_pedido") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@prato", Prato[i]));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_pedido", pedidoMDL.C_Pedido));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_tipo", pedidoMDL.Tipo));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@pedido_data", DateTime.Now));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_prato", Convert.ToDouble(P_Quantidade[i])));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Ds2);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_baixa_ingrediente

        public ConexaoMDL spc_cadastra_baixa_ingrediente(ConexaoMDL _conexaoMDL, string[] P_Quantidade)
        {
            for (j = 0; j <= _conexaoMDL.Ds2.Tables[0].Rows.Count - 1; j++)
            {
                _conexaoDAL.Conexao.Open();

                _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_baixa_ingrediente") { CommandType = CommandType.StoredProcedure };

                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@ingrediente", _conexaoMDL.Ds2.Tables[0].Rows[j]["Cod_Produto"]));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_ingrediente", _conexaoMDL.Ds2.Tables[0].Rows[j]["Quantidade"]));
                _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@quantidade_prato", Convert.ToDouble(P_Quantidade[i])));

                _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
                _conexaoMDL.Cmd.ExecuteNonQuery();

                _conexaoDAL.Conexao.Close();
            }

            return _conexaoMDL;
        }

        #endregion

        #region spc_valida_mesa

        public ConexaoMDL spc_valida_mesa(PedidoMDL pedidoMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_valida_mesa") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_reserva", pedidoMDL.Reserva));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_mesa", pedidoMDL.Mesa));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Validador = Convert.ToBoolean(_conexaoMDL.Cmd.ExecuteScalar());

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion

        #region spc_cadastra_reserva

        public ConexaoMDL spc_cadastra_reserva(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_cadastra_reserva") { CommandType = CommandType.StoredProcedure };

            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@cpf_funcionario", funcionarioMDL.Cpf));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@doc_cliente", clienteMDL.Documento));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@data_reserva", pedidoMDL.Reserva));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@numero_mesa", pedidoMDL.Mesa));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Cmd.ExecuteNonQuery();

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        #endregion
    }
}