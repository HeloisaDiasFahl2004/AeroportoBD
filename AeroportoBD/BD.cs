using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class BD
    {
        #region Banco de Dados
        static string Conexao = "Data Source = 'localhost\\SQLSERVER'; Initial Catalog = OnTheFly;User Id = sa; Password= sqlservermeu;";
        static SqlConnection conn = new SqlConnection(Conexao);
        public BD()
        {

        }
        public string Caminho()
        {
            return Conexao;
        }
        #endregion

        #region Insert Update Dados
        public void InsertDado(String insert)
        {
            conn.Open();
            try
            {

              
                SqlCommand cmdINSERT = new SqlCommand(insert, conn);
                cmdINSERT.Connection = conn;
                cmdINSERT.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                if (e.Number != 2627) // chave duplicada
                    throw;

                Console.WriteLine("CPF/CNPJ já existente");
                Console.ReadKey();
            }
            conn.Close();

            Console.ReadKey();
        } //OK

        public void UpdateDado( String update)
        {
            conn.Open();
            try
            {
              
                SqlCommand cmdUPDATE = new SqlCommand(update, conn);
                cmdUPDATE.Connection = conn;
                cmdUPDATE.ExecuteNonQuery();

            }
            catch (SqlException)
            {
                Console.WriteLine("Não foi possível atualizar dado!");
            }
            conn.Close();
            Console.ReadKey();
        } //OK

        public void DeleteDado( String update)
        {
            conn.Open();
            try
            {
             
                SqlCommand cmdDELETE = new SqlCommand(update, conn);
                cmdDELETE.Connection = conn;
                cmdDELETE.ExecuteNonQuery();
                Console.WriteLine("Cpf Removido com sucesso!");


            }
            catch (Exception)
            {
                Console.Write("Não foi possível Deletar Dado!");
            }
            conn.Close();

            Console.ReadKey();
        }
        #endregion

        #region Companhia Aerea

        public String SelectCompanhiaAerea(String selectC)
        {
            conn.Open();
            String s = "";
            try
            {


                SqlCommand cmdSELECT = new SqlCommand(selectC,conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS COMPANHIA AEREA <<<");
                    Console.WriteLine("\nCNPJ  \t\tRazão Social \t\t\t\t\t Data Abertura \t Data Último Voo\tDataCadastro \tSituacao \n");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.Write("{0} ", reader.GetString(0));
                        Console.Write(" {0} ", reader.GetString(1));
                        Console.Write("{0} ", reader.GetDateTime(2).ToShortDateString());
                        Console.Write("  {0}\t", reader.GetDateTime(3).ToShortDateString());
                        Console.Write("\t{0}\t", reader.GetDateTime(4).ToShortDateString());
                        Console.WriteLine("\t{0}", reader.GetString(5));

                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("\nFim da Impressão!");

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();

            Console.ReadKey();
            return s;
        }
        public CompanhiaAerea SelectCompanhiaAereaVER( String selectC)
        {
            conn.Open();
            CompanhiaAerea compAtual = new CompanhiaAerea();
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectC, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();
               

                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        compAtual.Cnpj = reader.GetString(0);
                        compAtual.RazaoSocial = reader.GetString(1);
                        compAtual.DataAbertura = reader.GetDateTime(2);
                        compAtual.DataCadastro = reader.GetDateTime(3);
                        compAtual.DataUltimoVoo = reader.GetDateTime(4);
                        compAtual.Situacao = char.Parse(reader.GetString(5));

                    }

                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();

            Console.ReadKey();
            return compAtual;
        }

        #endregion

        #region Passageiro
        public String SelectPassageiroVerUm( String selectP)
        {
            conn.Open();
            String s = "";
            try
            {
                
                SqlCommand cmdSELECT = new SqlCommand(selectP,conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

               
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
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        } // OK
        public String SelectPassageiroVizualizarTodos( String selectP)
        {
            conn.Open();
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectP, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

               
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
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        }
        public bool CpfExiste(String selectPass)
        {
            conn.Open();
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectPass, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

               

                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {

                        return true;
                    }


                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();

            Console.ReadKey();
            return false;
        }
        public Passageiro SelectPassageiroVER( String selectPassageiro)
        {

            conn.Open();
            Passageiro passageiroAtual = new Passageiro();
            String s = "";
            try
            {

                SqlCommand cmdSELECT = new SqlCommand(selectPassageiro, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                

                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        //passageiroAtual = new Passageiro(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetChar(3), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetChar(6));


                        passageiroAtual.Cpf = reader.GetString(0);
                        passageiroAtual.Nome = reader.GetString(1);
                        passageiroAtual.DataNascimento = reader.GetDateTime(2);
                        passageiroAtual.Sexo = char.Parse(reader.GetString(3));
                        passageiroAtual.DataUltimaCompra = reader.GetDateTime(4);
                        passageiroAtual.DataCadastro = reader.GetDateTime(5);
                        passageiroAtual.Situacao = char.Parse(reader.GetString(6));

                    }

                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();

            Console.ReadKey();
            return passageiroAtual;
        }

        #endregion

        #region Aeroporto
        public String SelectIATA(String selectAEROPORTO) //tenho q abrir a conexao
        {
            conn.Open();
            String s = "";
            try
            {
              
                SqlCommand cmdSELECT = new SqlCommand(selectAEROPORTO,conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS AEROPORTO <<<");
                    Console.WriteLine("\tIATA\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        Console.WriteLine("{0}\t", reader.GetString(0));

                    }
                    Console.WriteLine("Fim da Impressão!");
                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        }
        public Aeroporto SelectIATAdestino( String selectAEROPORTO)
        {

            conn.Open();
            Aeroporto dest = new Aeroporto();
            String s = "";
            try
            {
                SqlCommand cmdSELECT = new SqlCommand(selectAEROPORTO, conn);

                SqlDataReader reader = cmdSELECT.ExecuteReader();

                while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        dest.IATA = reader.GetString(0);

                    }
                    
                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return dest;
        }

        #endregion

        #region Exceções
        public String SelectRestrito(String selectPR)
        {

            conn.Open();
            String s = "";
            try
            {
                SqlCommand cmdSELECT = new SqlCommand(selectPR, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

              
                    Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    Console.WriteLine("\nCPF\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer,faz
                    {
                        s = reader.GetString(0);
                        Console.WriteLine(" {0} ", reader.GetString(0));
                    }
                    Console.WriteLine("Fim da Impressão!");
                

                Console.ReadKey();
            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            return s;
        }
        public Restrito SelectRestritoVER(String selectPR)
        {
            conn.Open();
            Restrito r = null;
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectPR, conn);

                SqlDataReader reader = cmdSELECT.ExecuteReader();

                while (reader.Read()) // enquanto tiver leitura para fazer,faz
                    {
                        s = reader.GetString(0);
                        r = new Restrito(reader.GetString(0));
                    }

                

                Console.ReadKey();
            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            return r;
        }
        public String SelectBloqueado( String selectBloq)
        {
            conn.Open();
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectBloq, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS <<<");
                    Console.WriteLine("\nCNPJ\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer,faz
                    {
                        s = reader.GetString(0);
                        Console.Write(" {0} ", reader.GetString(0));
                    }
                    Console.WriteLine("Fim da Impressão!");
                

                Console.ReadKey();
            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            return s;
        }

        #endregion

        #region Aeronave
        public String SelectAeronave( String selectA)
        {
            conn.Open();
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectA, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS AERONAVE <<<");
                    Console.WriteLine("\nInscrição\t\tCapacidade\tData Ultima Venda\tData Cadastro\t\tSituacao\tCNPJ\n");
                    while (reader.Read()) // enquanto tiver leitura para fazer,faz
                    {
                        s = reader.GetString(0);
                        Console.Write("{0}\t", reader.GetString(0));
                        Console.Write("\t\t{0} ", reader.GetInt32(1));
                        Console.Write("\t\t{0} ", reader.GetDateTime(2).ToShortDateString());
                        Console.Write("\t\t{0} ", reader.GetDateTime(3).ToShortDateString());
                        Console.Write("\t\t{0} \t", reader.GetString(4));
                        Console.WriteLine("\t{0} ", reader.GetString(5));


                    }
                    Console.WriteLine("\n>>> Fim da Impressão! <<<");
                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        }    //OK
        public Aeronave SelectAeronaveVER( String selectA)
        {

            conn.Open();
            Aeronave aeronaveAtual = new Aeronave();
            String s = "";
            try
            {
                SqlCommand cmdSELECT = new SqlCommand(selectA, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();


                while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        aeronaveAtual.Inscricao = reader.GetString(0);
                        aeronaveAtual.Capacidade =reader.GetInt32(1);
                        aeronaveAtual.UltimaVenda = reader.GetDateTime(2);
                        aeronaveAtual.DataCadastro = reader.GetDateTime(3);
                        aeronaveAtual.Situacao = char.Parse(reader.GetString(4));
                        aeronaveAtual.Cnpj = reader.GetString(5);

                    }

                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();

            Console.ReadKey();
            return aeronaveAtual;
        }


        #endregion

        #region Venda
        public String SelectVenda(String selectVenda)
        {

            conn.Open();
            String s = "";
            try
            {
                SqlCommand cmdSELECT = new SqlCommand(selectVenda, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS VENDA <<<");
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
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        }  //OK
        public String SelectPassagem( String selectR)
        {
            conn.Open();
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectR, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS RESERVADAS<<<");
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
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        }  //OK


        public Passagem VerPassagem( String selectR)
        {
            conn.Open();
            Passagem pass = null;
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectR, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();


                while (reader.Read()) // enquanto tiver leitura para fazer, faz
                    {

                        s = reader.GetString(0);
                        pass = new Passagem(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetFloat(3), reader.GetChar(4));


                    }

                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return pass;
        }
        public String SelectVendaPassagem( String selectVP)
        {
            String s = "";
            try
            {
                conn.Open();
                SqlCommand cmdSELECT = new SqlCommand(selectVP, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS ITEM VENDA <<<");
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
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        }


        #endregion

        #region VOO
        public String SelectVoo( String selectVOO)
        {
            conn.Open();
            String s = "";
            try
            {
                
              
                SqlCommand cmdSELECT = new SqlCommand(selectVOO, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();

                Console.WriteLine(">>> INÍCIO IMPRESSÃO DOS DADOS VOO <<<");
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
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
              
            }

            conn.Close();
            Console.ReadKey();
            return s;
        } // OK
        public Voo VerVoo(String selectVOO)
        {
            conn.Open();
            Voo v = null;
            String s = "";
            try
            {
               
                SqlCommand cmdSELECT = new SqlCommand(selectVOO, conn);
                SqlDataReader reader = null;
                using (reader = cmdSELECT.ExecuteReader())
                {
                    while (reader.Read()) // enquanto tiver leitura para fazer
                    {
                        s = reader.GetString(0);
                        v = new Voo(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetInt32(4), reader.GetChar(5));
                    }

                }

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return v;



        }
        #endregion

        #region Passagem
        public int ContaP( String selectV)
        {
            conn.Open();
            int s = 0;
            try
            {
              
                SqlCommand cmdSELECT = new SqlCommand(selectV, conn);
                SqlDataReader reader = cmdSELECT.ExecuteReader();


                while (reader.Read()) // enquanto tiver leitura para fazer, faz
                    {

                        s = reader.GetInt32(0);


                    }

                

            }
            catch (SqlException)
            {
                Console.Write("Não foi possível imprimir");
            }
            conn.Close();
            Console.ReadKey();
            return s;
        }

        #endregion

    }

}

