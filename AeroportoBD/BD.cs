using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class BD
    {
        string Conexao = "Data Source = 'localhost\\SQLSERVER'; Initial Catalog = OnTheFly;User Id = sa; Password= sqlservermeu;";
        SqlConnection conn;
        public BD()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }


        #region Insert Update Dados
        public void InsertDado(SqlConnection conexaosql, String insert)
        {
            try
            {
                conexaosql.Open();
                SqlCommand cmdINSERT = new SqlCommand(insert, conn);
                cmdINSERT.Connection = conexaosql;
                cmdINSERT.ExecuteNonQuery();
                conexaosql.Close();
            }
            catch (SqlException e)
            {
                if (e.Number != 2627) // chave duplicada
                    throw;

                Console.WriteLine("CPF/CNPJ já existente");
                Console.ReadKey();
            }
        } //OK

        public void UpdateDado(SqlConnection conexaosql, String update)
        {
            try
            {
                conexaosql.Open();
                SqlCommand cmdUPDATE = new SqlCommand(update, conexaosql);
                cmdUPDATE.Connection = conexaosql;
                cmdUPDATE.ExecuteNonQuery();
                conexaosql.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Não foi possível atualizar dado!");
            }
            Console.ReadKey();
        } //OK
#endregion


        #region Companhia Aerea

        public String SelectCompanhiaAerea(SqlConnection conexaosql, String selectC) 
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectC, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.Write(" {0} ", reader.GetString(0));
                        Console.Write(" {0} ", reader.GetString(1));
                        Console.Write(" {0} ", reader.GetString(2));
                        Console.Write(" {0} ", reader.GetString(3));
                        Console.Write(" {0} ", reader.GetString(4));
                        Console.Write(" {0} ", reader.GetString(5));


                    }
                    Console.WriteLine("Fim da Impressão!");
                }
                conexaosql.Close();
                Console.ReadKey();
            }
            catch (SqlException e)
            {
                Console.Write("Não foi possível imprimir");
            }
            return s;
        }

        public string SelectUmDadoCompanhia(SqlConnection conexaosql, String selectC) 
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectC, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.Write(" {0} ", reader.GetString(0));
                        Console.Write(" {0} ", reader.GetString(1));
                        Console.Write(" {0} ", reader.GetString(2));
                        Console.Write(" {0} ", reader.GetString(3));
                        Console.Write(" {0} ", reader.GetString(4));
                        Console.Write(" {0} ", reader.GetString(5));
                       

                    }
                    Console.WriteLine("Fim da Impressão!");
                }
                conexaosql.Close();
                Console.ReadKey();
            }
            catch (SqlException e)
            {
                Console.Write("Não foi possível imprimir");
            }
            return s;
        }

        #endregion


        #region Passageiro
        public String SelectUmDadoPassageiro(SqlConnection conexaosql, String selectP)
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectP, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.Write(" {0} ", reader.GetString(0));
                        Console.Write(" {0} ", reader.GetString(1));
                        Console.Write(" {0} ", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" {0} ", reader.GetString(3));
                        Console.Write(" {0} ", reader.GetDateTime(4).ToShortDateString());
                        Console.Write(" {0} ", reader.GetDateTime(5).ToShortDateString());
                        Console.WriteLine(" {0} ", reader.GetString(6));

                    }
                    Console.WriteLine("Fim da Impressão!");
                }
                conexaosql.Close();
                Console.ReadKey();
            }
            catch (SqlException e)
            {
                Console.Write("Não foi possível imprimir");
            }
            return s;
        } //OK

        public String SelectPassageiro(SqlConnection conexaosql, String selectP)
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectP, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.Write(" {0} ", reader.GetString(0));
                        Console.Write(" {0} ", reader.GetString(1));
                        Console.Write(" {0} ", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" {0} ", reader.GetString(3));
                        Console.Write(" {0} ", reader.GetDateTime(4).ToShortDateString());
                        Console.Write(" {0} ", reader.GetDateTime(5).ToShortDateString());
                        Console.WriteLine(" {0} ", reader.GetString(6));

                    }
                    Console.WriteLine("Fim da Impressão!");
                }
                conexaosql.Close();
                Console.ReadKey();
            }
            catch (SqlException e)
            {
                Console.Write("Não foi possível imprimir");
            }
            return s;
        } // OK
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
