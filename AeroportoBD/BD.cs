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
                Console.WriteLine("Companhia inserida com sucesso!");
                Console.ReadKey();
            }
            catch (SqlException e)
            {
                if (e.Number != 2627) // chave duplicada
                    throw;

                Console.WriteLine("CNPJ já existente");
                Console.ReadKey();
            }
        }

        #endregion


    }
}
