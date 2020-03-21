using ManagementRestaurant_MDL;

namespace ManagementRestaurant_GLL
{
    public class EstoqueGLL
    {
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region ValidaCNPJ

        public bool ValidaCNPJ(string cnpj)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;

            string digito;
            string tempCnpj;

            if (cnpj == ("00.000.000/0000-00"))
                return false;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        #endregion

        #region PesquisaCEP

        public EstoqueMDL PesquisaCEP(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL.Ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" +
                                   estoqueMDL.F_Cep.Replace("-", "").Trim() + "&formato=xml");
            estoqueMDL.F_Rua = _conexaoMDL.Ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim() + ": " +
                                  _conexaoMDL.Ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
            estoqueMDL.F_Bairro = _conexaoMDL.Ds.Tables[0].Rows[0]["bairro"].ToString();
            estoqueMDL.F_Cidade = _conexaoMDL.Ds.Tables[0].Rows[0]["cidade"].ToString();
            estoqueMDL.F_Estado = _conexaoMDL.Ds.Tables[0].Rows[0]["uf"].ToString();

            return estoqueMDL;
        }

        #endregion

        #region TrataDados

        public EstoqueMDL TrataDados(EstoqueMDL estoqueMDL)
        {
            estoqueMDL.F_Telefone =
                estoqueMDL.F_Telefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim();

            if (estoqueMDL.F_Email != null)
                estoqueMDL.F_Email = estoqueMDL.F_Email.ToLower().Trim();

            if (estoqueMDL.F_Cnpj != null)
                estoqueMDL.F_Cnpj = estoqueMDL.F_Cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            estoqueMDL.F_Cep = estoqueMDL.F_Cep.Replace("-", "").Trim();
            estoqueMDL.F_Rua = estoqueMDL.F_Rua.Replace(":", "").Replace(".", "");
            estoqueMDL.F_Bairro = estoqueMDL.F_Bairro.Replace(":", "").Replace(".", "");
            estoqueMDL.F_Cidade = estoqueMDL.F_Cidade.Replace(":", "").Replace(".", "");
            estoqueMDL.F_Estado = estoqueMDL.F_Estado.ToUpper();

            return estoqueMDL;
        }

        #endregion
    }
}