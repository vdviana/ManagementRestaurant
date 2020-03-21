using System;
using System.Data;

namespace ManagementRestaurant_MDL
{
    public class FuncionarioMDL
    {
        public String Nome { get; set; }
        public String Telefone { get; set; }
        public String Rg { get; set; }
        public String Cpf { get; set; }
        public String Cnh { get; set; }
        public String C_Trabalho { get; set; }
        public String C_Trabalho2 { get; set; }
        public String Cep { get; set; }
        public String Rua { get; set; }
        public String N_Estabelecimento { get; set; }
        public String Bairro { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public Int16 N_Acesso { get; set; }
        public DateTime H_Entrada { get; set; }
        public DateTime H_Saida { get; set; }
        public String Cargo { get; set; }
        public Double S_Hora { get; set; }
        public DateTime D_Admissao { get; set; }
        public Double Proventos { get; set; }
        public DateTime I_Ferias { get; set; }
        public DateTime F_Ferias { get; set; }
        public Int16 D_Uteis { get; set; }
        public String Senha { get; set; }
        public String RG_Senha { get; set; }
        public Boolean Validador { get; set; }
        public Int16 Status { get; set; }
        public DataSet Registro { get; set; }
    }
}