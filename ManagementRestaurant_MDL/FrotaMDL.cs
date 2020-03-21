using System;

namespace ManagementRestaurant_MDL
{
    public class FrotaMDL
    {
        public String Placa { get; set; }
        public String Tipo { get; set; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public DateTime D_Entrada { get; set; }
        public DateTime D_Saida { get; set; }
        public Int16 Vagas { get; set; }
        public String Fun_Nome { get; set; }
        public Boolean Validador { get; set; }
    }
}