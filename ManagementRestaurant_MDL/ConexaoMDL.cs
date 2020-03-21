using System;
using System.Data;
using System.Data.SqlClient;

namespace ManagementRestaurant_MDL
{
    public class ConexaoMDL
    {
        public SqlCommand Cmd = new SqlCommand();
        public SqlDataAdapter Da = new SqlDataAdapter();
        public DataSet Ds = new DataSet();
        public DataSet Ds2 = new DataSet();
        public Boolean Validador { get; set; }
        public Boolean Validador2 { get; set; }
    }
}