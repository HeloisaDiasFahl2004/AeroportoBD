using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class BD
    {
        string Conexao = "Data Source = 'localhost\\SQLSERVER'; Initial Catalog = OnTheFly;User Id = sa; Password= sqlservermeu;";

        public BD()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }

        #region Companhia Aerea
        public void InsertCompanhiaAerea(CompanhiaAerea novaComp)
        {
            try
            {
                BD bd = new BD();
                SqlConnection conexaosql = new SqlConnection(bd.Caminho());
                conexaosql.Open();
                string insertComp = $"INSERT INTO CompanhiaAerea(CNPJ,RazaoSocial,DataAbertura,DataUltimoVoo,DataCadastro,Situacao) VALUES ('{novaComp.Cnpj}'," + $"'{novaComp.RazaoSocial}','{novaComp.DataAbertura}','{novaComp.DataUltimoVoo}','{novaComp.DataCadastro}','{novaComp.Situacao}');";
                SqlCommand cmdINSERTcomp = new SqlCommand(insertComp, conexaosql);
                cmdINSERTcomp.ExecuteNonQuery();
                conexaosql.Close();
              //  Console.WriteLine("Companhia inserida com sucesso!");
              //  Console.ReadKey();
            }
            catch (SqlException e)
            {
                if (e.Number != 2627) // chave duplicada
                    throw;

                Console.WriteLine("CNPJ já existente");
                Console.ReadKey();
            }
        }
      
        public void UpdateCompanhiaAerea(CompanhiaAerea editComp) 
        {
        }

        public void SelectCompanhiaAerea(CompanhiaAerea verComps) 
        {
        }

        public void SelectUMACompanhiaAerea(CompanhiaAerea verComp) 
        {
        }

        #endregion

        #region Passageiro
        public void InsertPassageiro(Passageiro novoPassageiro)
        {
            try
            {
                BD bd = new BD();
                SqlConnection conexaosql = new SqlConnection(bd.Caminho());
                conexaosql.Open();
                string insertPassageiro = $"INSERT INTO Passageiro(CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao) VALUES('{novoPassageiro.Cpf}'," + $"'{novoPassageiro.Nome}','{novoPassageiro.DataNascimento}','{novoPassageiro.Sexo}','{novoPassageiro.DataUltimaCompra}','{novoPassageiro.DataCadastro}','{novoPassageiro.Situacao}');";
                SqlCommand cmdINSERTpassageiro = new SqlCommand(insertPassageiro, conexaosql);
                cmdINSERTpassageiro.ExecuteNonQuery();
                conexaosql.Close();
               // Console.WriteLine("Passageiro inserido com sucesso!");
               // Console.ReadKey();
            }
            catch (SqlException e)
            {
                if (e.Number != 2627) // chave duplicada
                    throw;

                Console.WriteLine("CPF já existente");
                Console.ReadKey();
            }
        }

        public void UpdatePassageiro(Passageiro editPassageiro)
        {
        }

        public void SelectPassageiro(Passageiro verPassageiros)
        {
        }

        public void SelectUMPassageiro(Passageiro verPassageiro)
        {
        }

        #endregion

        #region Aeronave
        public void InsertAeronave(Aeronave novaAeronave)
       {
            try
            {
                BD bd = new BD();
                SqlConnection conexaosql = new SqlConnection(bd.Caminho());
                conexaosql.Open();
                string insertAeronave = $"INSERT INTO Aeronave(INSCRICAO,Capacidade,UltimaVenda,DataCadastro,Situacao,CNPJ) VALUES('{novaAeronave.Inscricao}'," + $"'{novaAeronave.Capacidade}','{novaAeronave.UltimaVenda}','{novaAeronave.DataCadastro}','{novaAeronave.Situacao}','{novaAeronave.Cnpj}');";
                SqlCommand cmdINSERTaeronave = new SqlCommand(insertAeronave, conexaosql);
                cmdINSERTaeronave.ExecuteNonQuery();
                conexaosql.Close();
             //   Console.WriteLine("Aeronave inserida com sucesso!");
              //  Console.ReadKey();
            }
            catch (SqlException e)
            {
                if (e.Number != 2627) // chave duplicada
                    throw;

                Console.WriteLine("CPF já existente");
                Console.ReadKey();
            }
        }

        public void UpdateAeronave(Aeronave editAeronave)
        {
        }

        public void SelectAeronave(Aeronave verAeronaves)
        {
        }

        public void SelectUMAAeronave(Aeronave verAeronave)
        {
        }

        #endregion

        #region VOO

        #endregion 
    }

}
