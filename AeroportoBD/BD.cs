﻿using System;
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

        public String SelectUmDadoCompanhia(SqlConnection conexaosql, String selectC) 
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
                    Console.WriteLine("\nCPF\tNome\tData Nascimento\tSexo\tData Última Compra\tDataCadastro\tSituacao\n");
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
                    Console.WriteLine("\nCPF\tNome\tData Nascimento\tSexo\tData Última Compra\tDataCadastro\tSituacao\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.Write("{0}\t", reader.GetString(0));
                        Console.Write("{0}\t", reader.GetString(1));
                        Console.Write("{0}\t", reader.GetDateTime(2).ToShortDateString());
                        Console.Write("{0}\t", reader.GetString(3));
                        Console.Write("{0}\t", reader.GetDateTime(4).ToShortDateString());
                        Console.Write("{0}\t", reader.GetDateTime(5).ToShortDateString());
                        Console.WriteLine("{0}\t", reader.GetString(6));

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
        public String Localizar(SqlConnection conexaosql, String selectP) 
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectP, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> Passageiro Localizado <<<");
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
        }
        #endregion


        #region Aeronave
        public String SelectAeronave(SqlConnection conexaosql, String selectA)
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectA, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    Console.WriteLine("\nInscrição\tCapacidade\tDataUltimaVenda\tData Cadastro\tSituacao\tCNPJ\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer,faz
                    {
                        s = reader.GetString(0);
                        Console.Write(" {0} ", reader.GetString(0));
                        Console.Write(" {0} ", reader.GetString(1));
                        Console.Write(" {0} ", reader.GetDateTime(2).ToShortDateString());
                        Console.Write(" {0} ", reader.GetDateTime(3).ToShortDateString());
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
        }    //OK

        #endregion


        #region Venda
        public String SelectVenda(SqlConnection conexaosql, String selectVenda)
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectVenda, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    Console.WriteLine("\nID Venda\tData Venda\tValor Total\tCPF\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                       
                        s = reader.GetString(0);
                        Console.Write("{0}\t", reader.GetString(0));
                        Console.Write("{0}\t", reader.GetDateTime(1).ToShortDateString());
                        Console.Write("{0}\t", reader.GetString(2));
                        Console.WriteLine("{0}\t", reader.GetString(3));

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
        }  //OK
        public String SelectReserva(SqlConnection conexaosql,String selectR)
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectR, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    Console.WriteLine("\nIDPASSAGEM\tIDVOO\tDataUltimaOperacao\tValorUnitario\tSituacao\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer, faz
                    {
                      
                        s = reader.GetString(0);
                        Console.Write("{0}\t", reader.GetString(0));
                        Console.Write("{0}\t", reader.GetString(1));
                        Console.Write("{0}\t", reader.GetDateTime(2).ToShortDateString());
                        Console.Write("{0}\t", reader.GetString(3));
                        Console.WriteLine("{0}\t", reader.GetString(4));

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
        }  //OK

        public String SelectVendaPassagem(SqlConnection conexaosql, String selectVP)
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectVP, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    Console.WriteLine("\nID Item Venda\tID Venda\tValorUnitario\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer, faz
                    {

                        s = reader.GetString(0);
                        Console.Write("{0}\t", reader.GetString(0));
                        Console.Write("{0}\t", reader.GetString(1));
                        Console.Write("{0}\t", reader.GetString(2));
                 
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
        #region VOO

        public String SelectVoo(SqlConnection conexaosql, String selectVOO)
        {
            String s = "";
            try
            {
                conexaosql.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectVOO, conexaosql);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    Console.WriteLine("\nID\tDestino\tData Voo\tDataCadastro\tQuantidade Assentos\tSituacao\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.Write("{0}\t", reader.GetString(0));
                        Console.Write("{0}\t", reader.GetString(1));
                        Console.Write("{0}\t", reader.GetDateTime(2).ToShortDateString());
                        Console.Write("{0}\t", reader.GetDateTime(3).ToShortDateString());
                        Console.WriteLine("{0}\t", reader.GetInt32(4));
                        Console.WriteLine("{0}\t", reader.GetString(5));

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
    }

}
