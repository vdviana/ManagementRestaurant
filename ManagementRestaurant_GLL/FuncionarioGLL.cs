using System;
using System.Security.Cryptography;
using System.Text;

using ManagementRestaurant_MDL;

namespace ManagementRestaurant_GLL
{
    public class FuncionarioGLL
    {
        FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();

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

        #region PesquisaCEP

        public FuncionarioMDL PesquisaCEP(FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL.Ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" +
                                    funcionarioMDL.Cep.Replace("-", "").Trim() + "&formato=xml");

            funcionarioMDL.Rua = _conexaoMDL.Ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim() + ": " +
                                      _conexaoMDL.Ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
            funcionarioMDL.Bairro = _conexaoMDL.Ds.Tables[0].Rows[0]["bairro"].ToString();
            funcionarioMDL.Cidade = _conexaoMDL.Ds.Tables[0].Rows[0]["cidade"].ToString();
            funcionarioMDL.Estado = _conexaoMDL.Ds.Tables[0].Rows[0]["uf"].ToString();

            return funcionarioMDL;
        }

        #endregion

        #region TrataDados

        public FuncionarioMDL TrataDados(FuncionarioMDL funcionarioMDL)
        {
            if (funcionarioMDL.Telefone != null)
                funcionarioMDL.Telefone =
                    funcionarioMDL.Telefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim();
            if (funcionarioMDL.Cpf != null)
                funcionarioMDL.Cpf = Convert.ToString(funcionarioMDL.Cpf.Replace(".", "").Replace("-", "").Trim());
            if (funcionarioMDL.C_Trabalho != null)
                funcionarioMDL.C_Trabalho = funcionarioMDL.C_Trabalho.ToUpper();
            if (funcionarioMDL.Cep != null)
                funcionarioMDL.Cep = funcionarioMDL.Cep.Replace("-", "").Trim();
            if (funcionarioMDL.Rua != null)
                funcionarioMDL.Rua = funcionarioMDL.Rua.Replace(":", "").Replace(".", "");
            if (funcionarioMDL.Bairro != null)
                funcionarioMDL.Bairro = funcionarioMDL.Bairro.Replace(":", "").Replace(".", "");
            if (funcionarioMDL.Cidade != null)
                funcionarioMDL.Cidade = funcionarioMDL.Cidade.Replace(":", "").Replace(".", "");
            if (funcionarioMDL.Estado != null)
                funcionarioMDL.Estado = funcionarioMDL.Estado.ToUpper();

            return funcionarioMDL;
        }

        #endregion

        #region GeraSenha

        public FuncionarioMDL GeraSenha(FuncionarioMDL funcionarioMDL)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] data = Encoding.ASCII.GetBytes(funcionarioMDL.Senha);

            byte[] hash = sha1.ComputeHash(data);
            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            funcionarioMDL.Senha = sb.ToString();

            return funcionarioMDL;
        }

        #endregion

        #region GeraSenhaRG

        public FuncionarioMDL GeraSenhaRG(FuncionarioMDL funcionarioMDL)
        {
            funcionarioMDL.RG_Senha = funcionarioMDL.Rg;

            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] data = Encoding.ASCII.GetBytes(funcionarioMDL.RG_Senha);

            byte[] hash = sha1.ComputeHash(data);
            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            funcionarioMDL.RG_Senha = sb.ToString();

            return funcionarioMDL;
        }

        #endregion

        #region ValidaStatus

        public ConexaoMDL ValidaStatus(ConexaoMDL conexaoMDL)
        {
            _funcionarioMDL.Status = Convert.ToInt16(conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Status"].ToString());

            if (_funcionarioMDL.Status == 1)
            {
                conexaoMDL.Validador = true;
            }

            return conexaoMDL;
        }

        #endregion

        #region ValidaSenha

        public ConexaoMDL ValidaSenha(ConexaoMDL conexaoMDL)
        {
            _funcionarioMDL.Senha = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Senha"].ToString();
            _funcionarioMDL.Rg = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_RG"].ToString();

            _funcionarioMDL = GeraSenhaRG(_funcionarioMDL);

            if (_funcionarioMDL.Senha.Equals(_funcionarioMDL.RG_Senha))
            {
                conexaoMDL.Validador = true;
            }
            else
            {
                conexaoMDL.Validador = false;
            }

            return conexaoMDL;
        }

        #endregion

        #region ValidaDocumentos

        public ConexaoMDL ValidaDocumentos(ConexaoMDL conexaoMDL, FuncionarioMDL funcionarioMDL)
        {
            funcionarioMDL.Cpf = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();
            funcionarioMDL.Rg = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_RG"].ToString();
            funcionarioMDL.Cnh = conexaoMDL.Ds.Tables[0].Rows[0]["CNH"].ToString();

            if (funcionarioMDL.Senha.Equals(funcionarioMDL.Cpf) || funcionarioMDL.Senha.Equals(funcionarioMDL.Rg) || 
                funcionarioMDL.Senha.Equals(funcionarioMDL.Cnh))
            {
                conexaoMDL.Validador = false;
            }
            else
            {
                conexaoMDL.Validador = true;
            }

            return conexaoMDL;
        }

        #endregion

        #region ValidaUsuario

        public FuncionarioMDL ValidaUsuario(ConexaoMDL conexaoMDL)
        {
            try
            {
                _funcionarioMDL.N_Acesso = Convert.ToInt16(conexaoMDL.Ds.Tables[0].Rows[0]["Nivel_Acesso"].ToString());
                _funcionarioMDL.Cpf = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();
            }
            catch
            {
                _funcionarioMDL.N_Acesso = 0;
            }

            return _funcionarioMDL;
        }

        #endregion

        #region CarregaDadosFuncionario

        public FuncionarioMDL CarregaDadosFuncionario(ConexaoMDL conexaoMDL)
        {
            _funcionarioMDL.Nome = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();
            _funcionarioMDL.Telefone = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Telefone"].ToString();
            _funcionarioMDL.Rg = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Rg"].ToString();
            _funcionarioMDL.Cpf = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Cpf"].ToString();

            if ((conexaoMDL.Ds.Tables[0].Rows[0]["CNH"].ToString()) != "")
            {
                _funcionarioMDL.Cnh = conexaoMDL.Ds.Tables[0].Rows[0]["CNH"].ToString();
            }

            _funcionarioMDL.C_Trabalho = (conexaoMDL.Ds.Tables[0].Rows[0]["Carteira_de_Trabalho"].ToString()).Substring(0, 6);
            _funcionarioMDL.C_Trabalho2 = (conexaoMDL.Ds.Tables[0].Rows[0]["Carteira_de_Trabalho"].ToString()).Substring(9, 8);

            _funcionarioMDL.Cep = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Cep"].ToString();
            _funcionarioMDL.Rua = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Rua"].ToString();
            _funcionarioMDL.N_Estabelecimento = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_NumEstabelecimento"].ToString();
            _funcionarioMDL.Bairro = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Bairro"].ToString();
            _funcionarioMDL.Cidade = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Cidade"].ToString();
            _funcionarioMDL.Estado = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Estado"].ToString();
            _funcionarioMDL.Cargo = conexaoMDL.Ds.Tables[0].Rows[0]["Cargo_Nome"].ToString();

            return _funcionarioMDL;
        }

        #endregion

        #region VoltaLista

        public ConexaoMDL VoltaLista(ConexaoMDL conexaoMDL)
        {
            if (conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Status"].ToString() == "1")
            {
                conexaoMDL.Validador = true;
            }
            else
            {
                conexaoMDL.Validador = false;
            }

            return conexaoMDL;
        }

        #endregion
    }
}