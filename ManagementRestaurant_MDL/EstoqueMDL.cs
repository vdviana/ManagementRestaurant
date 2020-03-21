using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ManagementRestaurant_MDL
{
    public class EstoqueMDL
    {
        public String F_Nome { get; set; }
        public String F_Telefone { get; set; }
        public String F_Email { get; set; }
        public String F_Cnpj { get; set; }
        public String F_Cep { get; set; }
        public String F_Rua { get; set; }
        public String F_NEstabelecimento { get; set; }
        public String F_Bairro { get; set; }
        public String F_Cidade { get; set; }
        public String F_Estado { get; set; }
        public String F_Complemento { get; set; }
        public Int32 C_Produto { get; set; }
        public String N_Produto { get; set; }
        public String T_Produto { get; set; }
        public String N_Fiscal { get; set; }
        public DateTime Validade { get; set; }
        public Int32 Lote { get; set; }
        public Int32 Quantidade { get; set; }
        public Int32 T_Compra { get; set; }
        public Boolean Validador { get; set; }
        public String N_Prato { get; set; }
        public Double V_Prato { get; set; }
        public String N_Ingrediente { get; set; }
        public Double Q_Ingrediente { get; set; }
        public Int32 C_Prato { get; set; }
    }
}