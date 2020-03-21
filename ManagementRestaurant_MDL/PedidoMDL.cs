using System;

namespace ManagementRestaurant_MDL
{
    public class PedidoMDL
    {
        public String Tipo { get; set; }
        public Double Valor { get; set; }
        public Double ValorTotal { get; set; }
        public Int32 C_Pedido { get; set; }
        public DateTime I_Contrato { get; set; }
        public DateTime F_Contrato { get; set; }
        public Int32 Q_Dia { get; set; }
        public DateTime Reserva { get; set; }
        public DateTime D_Reserva { get; set; }
        public DateTime H_Reserva { get; set; }
        public Int32 Mesa { get; set; }
    }
}