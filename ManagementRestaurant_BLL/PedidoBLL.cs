using System;

using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class PedidoBLL
    {
        private FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ClienteGLL _clienteGLL = new ClienteGLL();

        private PedidoDAL _pedidoDAL = new PedidoDAL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CadastraPedido

        public ConexaoMDL CadastraPedido(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL, string[] Prato, string[] P_Quantidade)
        {
            clienteMDL = _clienteGLL.TrataDados(clienteMDL);

            return _pedidoDAL.CadastraPedido(funcionarioMDL, clienteMDL, pedidoMDL, Prato, P_Quantidade);
        }

        #endregion

        #region PesquisaPedidos
        public ConexaoMDL PesquisaPedidos(string tipo)
        {

            return _pedidoDAL.PesquisaPedidos(tipo);

        }
        #endregion

        #region ValidaMesa

        public ConexaoMDL ValidaMesa(PedidoMDL pedidoMDL)
        {
            return _pedidoDAL.spc_valida_mesa(pedidoMDL);
        }

        #endregion

        #region CadastraReserva

        public ConexaoMDL CadastraReserva(FuncionarioMDL funcionarioMDL, ClienteMDL clienteMDL, PedidoMDL pedidoMDL)
        {
            funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);
            clienteMDL = _clienteGLL.TrataDados(clienteMDL);

            return _pedidoDAL.spc_cadastra_reserva(funcionarioMDL, clienteMDL, pedidoMDL);
        }

        #endregion
    }
}