using System;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_GLL
{
    public class ClienteGLL
    {
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private readonly ClienteMDL _clienteMDL = new ClienteMDL();

        #region ValidaCPF

        public bool ValidaCPF(string cpf)
        {
            var multiplicador1 = new int[9] {10, 9, 8, 7, 6, 5, 4, 3, 2};
            var multiplicador2 = new int[10] {11, 10, 9, 8, 7, 6, 5, 4, 3, 2};

            string tempCpf;
            string digito;

            int soma;
            int resto;

            if (cpf == ("000.000.000-00"))
                return false;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString())*multiplicador1[i];

            resto = soma%11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString())*multiplicador2[i];

            resto = soma%11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        #endregion

        #region CarregaDadosCliente

        public ClienteMDL CarregaDadosCliente(ConexaoMDL conexaoMDL)
        {
            _clienteMDL.Nome = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Nome"].ToString();
            _clienteMDL.Telefone = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Telefone"].ToString();
         
            _clienteMDL.Tipo = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Tipo"].ToString();

                _clienteMDL.Documento = conexaoMDL.Ds.Tables[0].Rows[0]["Doc_Cliente"].ToString();
        
            _clienteMDL.Cep = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Cep"].ToString();
            _clienteMDL.Rua = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Rua"].ToString();
            _clienteMDL.N_Estabelecimento = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_NumEstabelecimento"].ToString();
            _clienteMDL.Bairro = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Bairro"].ToString();
            _clienteMDL.Cidade = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Cidade"].ToString();
            _clienteMDL.Estado = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Estado"].ToString();
            _clienteMDL.Registro = conexaoMDL.Ds;
            return _clienteMDL;
        }

        #endregion

        #region ValidaCNPJ

        public bool ValidaCNPJ(string cnpj)
        {
            var multiplicador1 = new int[12] {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            var multiplicador2 = new int[13] {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
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
                soma += int.Parse(tempCnpj[i].ToString())*multiplicador1[i];

            resto = (soma%11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString())*multiplicador2[i];

            resto = (soma%11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        #endregion

        #region PesquisaCEP

        public ClienteMDL PesquisaCEP(ClienteMDL clienteMDL)
        {
            _conexaoMDL.Ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" +
                                   clienteMDL.Cep.Replace("-", "").Trim() + "&formato=xml");
            clienteMDL.Rua = _conexaoMDL.Ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim() + ": " +
                                  _conexaoMDL.Ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
            clienteMDL.Bairro = _conexaoMDL.Ds.Tables[0].Rows[0]["bairro"].ToString();
            clienteMDL.Cidade = _conexaoMDL.Ds.Tables[0].Rows[0]["cidade"].ToString();
            clienteMDL.Estado = _conexaoMDL.Ds.Tables[0].Rows[0]["uf"].ToString();

            return clienteMDL;
        }

        #endregion

        #region TrataDados

        public ClienteMDL TrataDados(ClienteMDL clienteMDL)
        {
            if (clienteMDL.Telefone != null)
                clienteMDL.Telefone =
                    clienteMDL.Telefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim();

            if (clienteMDL.Email != null)
                clienteMDL.Email = clienteMDL.Email.ToLower().Trim();

            if (clienteMDL.Documento != null)
                clienteMDL.Documento = clienteMDL.Documento.Replace(".", "").Replace("-", "").Trim();

            if (clienteMDL.Documento != null)
                clienteMDL.Documento = clienteMDL.Documento.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            if (clienteMDL.Cep != null)
                clienteMDL.Cep = clienteMDL.Cep.Replace("-", "").Trim();

            if (clienteMDL.Rua != null)
                clienteMDL.Rua = clienteMDL.Rua.Replace(":", "").Replace(".", "");

            if (clienteMDL.Bairro != null)
                clienteMDL.Bairro = clienteMDL.Bairro.Replace(":", "").Replace(".", "");

            if (clienteMDL.Cidade != null)
                clienteMDL.Cidade = clienteMDL.Cidade.Replace(":", "").Replace(".", "");

            if (clienteMDL.Estado != null)
                clienteMDL.Estado = clienteMDL.Estado.ToUpper();

            return clienteMDL;
        }

        #endregion
    }
}