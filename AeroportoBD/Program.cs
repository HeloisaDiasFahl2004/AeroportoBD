using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace AeroportoBD
{
    internal class Program
    {
        #region Declarações
        static BD bd = new BD();

        #endregion

        #region DataConversão
        static public DateTime DateConverter(string data)
        {
            char[] datasembarra = data.ToCharArray();
            string datacombarras = (datasembarra[0].ToString() + datasembarra[1].ToString() + "/" + datasembarra[2].ToString() + datasembarra[3].ToString() + "/" + datasembarra[4].ToString() + datasembarra[5].ToString() + datasembarra[6].ToString() + datasembarra[7].ToString());
            return DateTime.Parse(datacombarras);
        }

        static public DateTime DateHourConverter(string datahora)
        {
            char[] datahorastring = datahora.ToCharArray();
            char[] datahoracombarra = new char[] { datahorastring[0], datahorastring[1], '/', datahorastring[2], datahorastring[3], '/', datahorastring[4], datahorastring[5], datahorastring[6], datahorastring[7], ' ', datahorastring[8], datahorastring[9], ':', datahorastring[10], datahorastring[11] };
            string datacombarras = null;
            foreach (var v in datahoracombarra)
            {
                datacombarras = datacombarras + v;
            }
            return DateTime.Parse(datacombarras);
        }

        #endregion

        #region MensagemAguardar
        static bool PausaMensagem()
        {
            bool repetirdo;
            do
            {
                Console.WriteLine("\nPressione S para informar novamente ou C para cancelar:");
                ConsoleKeyInfo op = Console.ReadKey(true);
                if (op.Key == ConsoleKey.S)
                {
                    Console.Clear();
                    return false;
                }
                else
                {
                    if (op.Key == ConsoleKey.C)
                    {
                        Console.Clear();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Escolha uma opção válida!");
                        repetirdo = true;
                    }
                }
            } while (repetirdo == true);
            return true;
        }
        static void Pausa()
        {
            Console.WriteLine("\nAperte 'ENTER' para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion

        #region GerarIDVOO, IDVENDA, IDITEMVENDA
        static public string GeradorId(String id)
        {
            switch (id)
            {
                case "idvoo":

                    #region IDVoo

                    Random random = new Random();
                    bool encontrado = false;
                    string idvoogerado;
                    do
                    {
                        try
                        {
                            encontrado = false;
                            idvoogerado = ("V" + random.Next(1000, 9999).ToString());
                            string BuscarVoo = $"SELECT * FROM VOO WHERE IDVOO='{idvoogerado}";

                            if (!string.IsNullOrEmpty(bd.SelectPassagem(BuscarVoo)))
                            {
                                encontrado = true;
                                break;
                            }
                            if (encontrado == false)
                                return idvoogerado;

                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Erro, não foi possível gerar ID do Voo! Banco de vendas está cheio");
                                Pausa();
                                return null;
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Erro, não foi possível gerar ID do Voo!");
                            Pausa();
                            return null;
                        }
                    } while (encontrado == true);
                    return null;
                #endregion

                case "idvenda":

                    #region IDVenda
                    Random randomV = new Random();

                    string idVenda;
                    do
                    {

                        encontrado = false;
                        idVenda = (randomV.Next(1000, 9999).ToString());
                        if (idVenda.Length == 1) idVenda = ("0000" + idVenda);
                        else if (idVenda.Length == 2) idVenda = ("000" + idVenda);
                        else if (idVenda.Length == 3) idVenda = ("00" + idVenda);
                        else if (idVenda.Length == 4) idVenda = ("0" + idVenda);
                        
                            string BuscarV = $"SELECT * FROM Venda WHERE IDVenda='{idVenda}'";

                        if (!string.IsNullOrEmpty(bd.SelectVenda(BuscarV)))
                        {
                            encontrado = true;
                            
                        }

                    } while (encontrado);

                    return idVenda;


                #endregion


                case "iditemvenda":

                    #region IDItemVenda

                    Random randomVenda = new Random();
                  
                    string idItemVenda;
                    do
                    {
                       
                            encontrado = false;
                            idItemVenda = (randomVenda.Next(1000, 9999).ToString());

                            if (idItemVenda.Length == 1) idItemVenda=("0000" + idItemVenda);
                            else if (idItemVenda.Length == 2) idItemVenda= ("000" + idItemVenda);
                            else if (idItemVenda.Length == 3) idItemVenda = ("00" + idItemVenda);
                            else if (idItemVenda.Length == 4) idItemVenda = ("0" + idItemVenda);
                        
                            string BuscarVenda = $"SELECT * FROM VendaPassagem WHERE IDITEMVENDA='{idItemVenda}'";

                            if (!string.IsNullOrEmpty(bd.SelectVendaPassagem(BuscarVenda)))
                            {
                                encontrado = true;
                                
                            }
                       

                    } while (encontrado);

                    return idItemVenda;





                #endregion

                default:
                    return null;
            }
        }
        #endregion

        #region Validar Entrada
        static string ValidarEntrada(string entrada)
        {
            string[] vetorletras = new string[] {"Ç","ç","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S",
            "T","U","V","W","X","Y","Z","Á","É","Í","Ó","Ú","À","È","Ì","Ò","Ù","Â","Ê","Î","Ô","Û","Ã","Õ"," "};
            string[] vetornumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            bool encontrado;
            bool retornar = true;
            int qtdnumerosiguais = 0;


            switch (entrada)
            {
                case "menu":

                    #region menu

                    do
                    {

                        try
                        {
                            char[] vetortecla;
                            Console.CursorVisible = false;
                            ConsoleKeyInfo op = Console.ReadKey(true);
                            vetortecla = op.Key.ToString().ToCharArray();

                            if (vetortecla[0] == 'N')
                            {
                                if (vetornumeros.Contains(vetortecla[6].ToString()) == true)
                                {
                                    return vetortecla[6].ToString();
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }
                            else
                            {
                                if (vetortecla[0] == 'D')
                                {
                                    if (vetornumeros.Contains(vetortecla[1].ToString()) == true)
                                    {
                                        return vetortecla[1].ToString();
                                    }
                                    else
                                    {
                                        encontrado = false;
                                    }
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            encontrado = false;
                        }
                    } while (encontrado == false);

                    return null;


                #endregion


                case "cpf":

                    #region CPF;

                    do
                    {
                        //Seta encontrado e validado sempre que retorna o laço do processo:
                        encontrado = true; // seta true para não quebrar o for de primeira
                        retornar = false; // só retorna se o usuário quiser
                        qtdnumerosiguais = 0;
                        string cpf;

                        try
                        {
                            Console.Clear();
                            Console.Write("Informe o CPF: ");

                            cpf = Console.ReadLine();

                            char[] letras = cpf.ToCharArray();

                            //verifica se tem 11 caracteres:
                            if (letras.Length == 11)
                            {
                                //verifica os 11 caracteres se são obrigatóriamente números:
                                for (int i = 0; i < 11 && encontrado != false; i++)
                                {
                                    foreach (var v in vetornumeros)
                                    {
                                        if (letras[i].ToString().ToUpper().Equals(v))
                                        {
                                            encontrado = true;
                                            break; // sai do foreach e volta pro for
                                        }
                                        else encontrado = false;
                                    }
                                }

                                //Verifica se é um cpf válido calculando os 2 últimos digitos, segundo a receita federal:
                                if (encontrado == true)
                                {
                                    int soma = 0;
                                    int resto = 0;
                                    int digito1 = 0;
                                    int digito2 = 0;

                                    //Verifica se os números são iguais

                                    for (int i = 0; i < 9; i++)
                                    {
                                        if (letras[i] == letras[i + 1])
                                            qtdnumerosiguais = qtdnumerosiguais + 1;

                                    }

                                    //Se os 9 primeiros digitos forem todos iguais, invalida o cpf:
                                    if (qtdnumerosiguais != 9)
                                    {
                                        //calcula o primeiro digito verificador do cpf:
                                        for (int i = 1, j = 0; i < 10; i++, j++)
                                            soma = soma + (int.Parse(letras[j].ToString()) * i);

                                        resto = soma % 11;

                                        if (resto >= 10)
                                        {
                                            digito1 = 0;

                                        }
                                        else
                                        {
                                            digito1 = resto;
                                        }

                                        //Verifica se o primeiro digito digitado é igual ao que era pra ser:
                                        if (digito1 == int.Parse(letras[9].ToString()))
                                        {
                                            soma = 0; //seta o soma em 0 para o processo de soma do segundo digito do cpf:

                                            //calcula o segundo digito verificador do cpf:
                                            for (int i = 0, j = 0; i < 10; i++, j++)
                                                soma = soma + (int.Parse(letras[j].ToString()) * i);

                                            resto = soma % 11;

                                            if (resto >= 10)
                                            {
                                                digito2 = 0;
                                            }
                                            else
                                            {
                                                digito2 = resto;
                                            }
                                            //Verifica se o segundo digito digitado é igual ao que era pra ser:
                                            if (digito2 == int.Parse(letras[10].ToString()))
                                            {

                                                encontrado = false;
                                                string buscarestrito = $"SELECT CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao FROM PASSAGEIRO WHERE CPF='{cpf}'; ";

                                                if (!string.IsNullOrEmpty(bd.SelectPassageiroVerUm(buscarestrito)))
                                                {

                                                    encontrado = true;
                                                }
                                                if (encontrado == true)
                                                {
                                                    Console.WriteLine("CPF já cadastrado!");
                                                    retornar = PausaMensagem();
                                                    break;
                                                }


                                                //Ao fim da procura, se não possuir o cpf no banco, encontrado = false e retorna o cpf cadastrado:
                                                if (encontrado == false)
                                                    //////////RETORNA O CPF
                                                    return cpf;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Esse não é um CPF válido!");
                                                retornar = PausaMensagem();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Esse não é um CPF válido!");
                                            retornar = PausaMensagem();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("CPF com números sequenciais iguais não é válido!");
                                        retornar = PausaMensagem();
                                    }
                                }
                                else
                                {

                                    Console.WriteLine("Só aceita números válidos de 11 digitos");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Só aceita números válidos de 11 digitos");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERRO: Só aceita números válidos de 11 digitos");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion;


                case "cnpj":

                    #region CNPJ;

                    do
                    {
                        //Seta encontrado e validado sempre que retorna o laço do processo:
                        encontrado = true; // seta true para não quebrar o for de primeira
                        retornar = false; // só retorna se o usuário quiser
                        qtdnumerosiguais = 0;
                        string cnpj;

                        try
                        {
                            Console.Clear();
                            Console.Write("Informe o CNPJ: ");

                            cnpj = Console.ReadLine();

                            char[] letras = cnpj.ToCharArray();

                            //verifica se tem 14 caracteres:
                            if (letras.Length == 14)
                            {
                                //verifica os 14 caracteres se são obrigatóriamente números:
                                for (int i = 0; i < 14 && encontrado != false; i++)
                                {
                                    foreach (var v in vetornumeros)
                                    {
                                        if (letras[i].ToString().ToUpper().Equals(v))
                                        {
                                            encontrado = true;
                                            break; // sai do foreach e volta pro for
                                        }
                                        else encontrado = false; // se não encontrado, sai do foreach e quebra a condição do for
                                    }
                                }

                                //Qualquer valor que não seja um número invalida o cnpj:
                                if (encontrado == true)
                                {
                                    //Verifica se os números são iguais
                                    for (int i = 0; i < 12; i++)
                                    {
                                        if (letras[i] == letras[i + 1])
                                            qtdnumerosiguais = qtdnumerosiguais + 1;
                                    }

                                    //Se os 12 primeiros digitos forem todos iguais, invalida o cnpj:
                                    if (qtdnumerosiguais != 12)
                                    {
                                        int soma = 0;
                                        int resto = 0;
                                        int digito1 = 0;
                                        int digito2 = 0;

                                        //calcula o primeiro digito verificador do cnpj:
                                        for (int i = 6, j = 0; i < 10; i++, j++)
                                            soma = soma + (int.Parse(letras[j].ToString()) * i);

                                        for (int i = 2, j = 4; i < 10; i++, j++)
                                            soma = soma + (int.Parse(letras[j].ToString()) * i);

                                        resto = soma % 11;

                                        if (resto >= 10)
                                        {
                                            digito1 = 0;

                                        }
                                        else
                                        {
                                            digito1 = resto;
                                        }

                                        //Verifica se o primeiro digito digitado é igual ao que era pra ser:
                                        if (digito1 == int.Parse(letras[12].ToString()))
                                        {
                                            soma = 0; //seta o soma em 0 para o processo de soma do segundo digito do cnpj:

                                            //calcula o segundo digito verificador do cpf:
                                            for (int i = 5, j = 0; i < 10; i++, j++)
                                                soma = soma + (int.Parse(letras[j].ToString()) * i);

                                            for (int i = 2, j = 5; i < 10; i++, j++)
                                                soma = soma + (int.Parse(letras[j].ToString()) * i);

                                            resto = soma % 11;

                                            if (resto >= 10)
                                            {
                                                digito2 = 0;

                                            }
                                            else
                                            {
                                                digito2 = resto;
                                            }
                                            //Verifica se o segundo digito digitado é igual ao que era pra ser:
                                            if (digito2 == int.Parse(letras[13].ToString()))
                                            {
                                                //Se digitos validados, procura na lista de cadastro se já existe o cnpj cadastrado:
                                                encontrado = false;

                                                String localiza = $"SELECT CNPJ,RazaoSocial,DataAbertura,DataUltimoVoo,DataCadastro,Situacao FROM CompanhiaAerea WHERE CNPJ=('{cnpj}');";
                                                if (!string.IsNullOrEmpty(bd.SelectCompanhiaAerea(localiza)))
                                                {
                                                    encontrado = true;
                                                }

                                                if (encontrado == true)
                                                {
                                                    Console.WriteLine("CNPJ já cadastrado!");
                                                    retornar = PausaMensagem();
                                                    break;
                                                }
                                                else
                                                    encontrado = false;
                                            }
                                            if (encontrado == false)
                                                return cnpj;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Esse não é um CNPJ válido!");
                                            retornar = PausaMensagem();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Esse não é um CNPJ válido!");
                                        retornar = PausaMensagem();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("CNPJ com números sequenciais iguais não é válido!");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {

                                Console.WriteLine("Só aceita números válidos de 14 digitos");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERRO: Só aceita números válidos de 14 digitos");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion;


                case "nome":

                    #region Nome

                    do
                    {
                        string nome;
                        encontrado = true;
                        retornar = false;

                        Console.Write("Informe o Nome/Razão Social: ");
                        try
                        {
                            nome = Console.ReadLine();

                            char[] letras = nome.ToCharArray();

                            //Verifica se o nome tem no mínimo 3 e no máximo 50 caracteres:
                            if (letras.Length > 3 && letras.Length <= 50)
                            {
                                //Verifica se o nome só tem letras válidas:
                                for (int i = 0; i < letras.Length && encontrado != false; i++)
                                {
                                    foreach (var v in vetorletras)
                                    {
                                        if (letras[i].ToString().ToUpper().Equals(v))
                                        {
                                            encontrado = true;
                                            break;
                                        }
                                        else encontrado = false;
                                    }
                                }

                                //Se possuir somente letras válidas, prossegue:
                                if (encontrado == true)
                                {
                                    int qtdmax = 50;
                                    int qtdescrito = letras.Length;

                                    //Verifica a quantidade de caracteres que falta para 50 caracteres e preenche de espaço, se preciso:
                                    if (qtdescrito < qtdmax)
                                    {
                                        int qtdfaltante = qtdmax - qtdescrito;

                                        for (int i = 0; i <= qtdfaltante; i++)
                                        {
                                            nome = nome + " ";
                                        }
                                        ///////RETORNA O NOME COM 50 CARACTERES (PREENCHIDO COM ESPAÇOS PARA COMPLETAR 50)
                                        return nome;
                                    }
                                    else
                                    {
                                        ///////RETORNA O NOME COM 50 CARACTERES (SE O USUÁRIO UTILIZAR OS 50 CARACTERES)
                                        return nome;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nome só aceita letras.");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nome informado não é válido! Insira o nome completo, máximo 50 caracteres!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Insira um valor válido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion


                case "sexo":

                    #region Sexo
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Informe o sexo:\n[M] - Masculino\n[F] - Feminino\n[N] - Não informar");
                        Console.CursorVisible = false;
                        ConsoleKeyInfo op = Console.ReadKey(true);

                        //Verificar se tecla pressionada foi M / F ou N (independente do CAPSLOCK estar ativado!)
                        if (op.Key == ConsoleKey.M)
                        {
                            Console.Clear();
                            return "M";
                        }
                        else
                        {
                            if (op.Key == ConsoleKey.F)
                            {
                                Console.Clear();
                                return "F";
                            }
                            else
                            {
                                if (op.Key == ConsoleKey.N)
                                {
                                    Console.Clear();
                                    return "N";
                                }
                                else
                                {
                                    Console.WriteLine("Escolha uma opção válida!");
                                    retornar = PausaMensagem();
                                }
                            }
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion


                case "datanascimento":

                    #region DataNascimento

                    do
                    {
                        try
                        {
                            ConsoleKeyInfo teclaData;
                            char[] vetortecla;
                            string DataNascimento = null;
                            encontrado = false;
                            string[] vetordata = new string[] { "_", "_", "_", "_", "_", "_", "_", "_" };

                            static void AtualizarTela(string[] vetordata)
                            {
                                Console.Clear();
                                Console.WriteLine("Insira a Data de Nascimento:");
                                Console.WriteLine(vetordata[0] + vetordata[1] + "/" + vetordata[2] + vetordata[3] + "/" + vetordata[4] + vetordata[5] + vetordata[6] + vetordata[7]);
                                Console.CursorVisible = false;
                            }

                            for (int i = 0; i < 8; i++)
                            {
                                AtualizarTela(vetordata);

                                teclaData = Console.ReadKey(true);

                                vetortecla = teclaData.Key.ToString().ToCharArray();

                                if (vetortecla[0] == 'N')
                                {
                                    if (vetornumeros.Contains(vetortecla[6].ToString()) == true)
                                    {
                                        encontrado = true;
                                        vetordata[i] = vetortecla[6].ToString();
                                        DataNascimento = DataNascimento + vetordata[i];
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        break;
                                    }

                                    AtualizarTela(vetordata);
                                }
                                else
                                {

                                    if (vetortecla[0] == 'D')
                                    {
                                        if (vetornumeros.Contains(vetortecla[1].ToString()) == true)
                                        {
                                            encontrado = true;
                                            vetordata[i] = vetortecla[1].ToString();
                                            DataNascimento = DataNascimento + vetordata[i];
                                        }
                                        else
                                        {
                                            encontrado = false;
                                            break;
                                        }

                                        AtualizarTela(vetordata);
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        break;
                                    }
                                }
                            }
                            if (encontrado == true)
                            {
                                if (DateTime.Compare(DateConverter(DataNascimento), System.DateTime.Now) < 0)
                                {
                                    return DataNascimento;
                                }
                                else
                                {
                                    Console.WriteLine("Data de nascimento não aceita datas futuras, insira uma data válida!");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Por favor, insira uma data válida!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Erro: Por favor, insira uma data válida!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    return null;


                #endregion


                case "dataabertura":

                    #region DataAberturaCompanhiaAerea

                    do
                    {
                        try
                        {
                            ConsoleKeyInfo teclaData;
                            char[] vetortecla;
                            string DataAbertura = null;
                            encontrado = false;
                            string[] vetordata = new string[] { "_", "_", "_", "_", "_", "_", "_", "_" };

                            static void AtualizarTela(string[] vetordata)
                            {
                                Console.Clear();
                                Console.WriteLine("Insira a Data de abertura da Empresa:");

                                Console.WriteLine(vetordata[0] + vetordata[1] + "/" + vetordata[2] + vetordata[3] + "/" + vetordata[4] + vetordata[5] + vetordata[6] + vetordata[7]);
                                Console.CursorVisible = false;

                            }

                            //Verificar se digitou só nrs válidos
                            for (int i = 0; i < 8; i++)
                            {
                                AtualizarTela(vetordata);

                                teclaData = Console.ReadKey(true);

                                vetortecla = teclaData.Key.ToString().ToCharArray();

                                if (vetortecla[0] == 'N')
                                {
                                    if (vetornumeros.Contains(vetortecla[6].ToString()) == true)
                                    {
                                        encontrado = true;
                                        vetordata[i] = vetortecla[6].ToString();
                                        DataAbertura = DataAbertura + vetordata[i];
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        break;
                                    }

                                    AtualizarTela(vetordata);
                                }
                                else
                                {
                                    if (vetortecla[0] == 'D')
                                    {
                                        if (vetornumeros.Contains(vetortecla[1].ToString()) == true)
                                        {
                                            encontrado = true;
                                            vetordata[i] = vetortecla[1].ToString();
                                            DataAbertura = DataAbertura + vetordata[i];
                                        }
                                        else
                                        {
                                            encontrado = false;
                                            break;
                                        }

                                        AtualizarTela(vetordata);
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        break;
                                    }
                                }
                            }
                            //Se só digitou números válidos, continua:
                            if (encontrado == true)
                            {
                                //Verificar se é data futura:
                                if (DateTime.Compare(DateConverter(DataAbertura), System.DateTime.Now) < 0)
                                {
                                    //Verificar se a abertura da empresa é maior que 6 meses:
                                    if (DateTime.Compare(DateConverter(DataAbertura), System.DateTime.Now.AddMonths(-6)) < 0)
                                    {
                                        ///////RETORNA A DATA DE ABERTURA
                                        return DataAbertura;
                                    }
                                    else
                                    {
                                        Console.Clear();

                                        Console.WriteLine("\nO Aeroporto não aceita cadastrar companhia aérea com menos de 6 meses de existência.");

                                        Console.WriteLine("\nVocê será redirecionado para o menu anterior.");
                                        Pausa();

                                        //Retorna nullo direto, não tem a opção de digitar a data novamente
                                        return null;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Não aceita datas futuras, insira uma data válida!");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Por favor, insira uma data válida!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Erro: Por favor, insira uma data válida!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);
                    Console.Clear();
                    return null;


                #endregion


                case "datavoo":

                    #region DataVoo

                    do
                    {
                        try
                        {
                            ConsoleKeyInfo teclaData;
                            char[] vetortecla;
                            string DataVoo = null;
                            encontrado = false;
                            string[] vetordata = new string[] { "_", "_", "_", "_", "_", "_", "_", "_", " ", "_", "_", "_", "_" };

                            static void AtualizarTela(string[] vetordata)
                            {
                                Console.Clear();
                                Console.WriteLine("Insira a Data e hora do Voo:");
                                Console.WriteLine(vetordata[0] + vetordata[1] + "/" + vetordata[2] + vetordata[3] + "/" + vetordata[4] + vetordata[5] + vetordata[6] + vetordata[7] + " " + vetordata[9] + vetordata[10] + ":" + vetordata[11] + vetordata[12]);
                                Console.CursorVisible = false;
                            }


                            for (int i = 0; i < 8; i++)
                            {
                                AtualizarTela(vetordata);

                                teclaData = Console.ReadKey(true);

                                vetortecla = teclaData.Key.ToString().ToCharArray();

                                //Verifica se foi teclado realmente um número:
                                if (vetortecla[0] == 'N')
                                {
                                    if (vetornumeros.Contains(vetortecla[6].ToString()) == true)
                                    {
                                        encontrado = true;
                                        vetordata[i] = vetortecla[6].ToString();
                                        DataVoo = DataVoo + vetordata[i];
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        break;
                                    }

                                    AtualizarTela(vetordata);
                                }
                                else
                                {
                                    if (vetortecla[0] == 'D')
                                    {
                                        if (vetornumeros.Contains(vetortecla[1].ToString()) == true)
                                        {
                                            encontrado = true;
                                            vetordata[i] = vetortecla[1].ToString();
                                            DataVoo = DataVoo + vetordata[i];
                                        }
                                        else
                                        {
                                            encontrado = false;
                                            break;
                                        }

                                        AtualizarTela(vetordata);
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        break;
                                    }
                                }
                            }

                            //Se todas entradas foram números válidos, continua:
                            //A variável DataVoo nesse instante só tem datas sem barras, ex: "12345678":
                            if (encontrado == true)
                            {
                                //Pede os dados da hora separado da data:
                                for (int i = 9; i < 13; i++)
                                {
                                    AtualizarTela(vetordata);

                                    teclaData = Console.ReadKey(true);

                                    vetortecla = teclaData.Key.ToString().ToCharArray();

                                    //Verifica se foi teclado realmente um número:
                                    if (vetortecla[0] == 'N')
                                    {
                                        if (vetornumeros.Contains(vetortecla[6].ToString()) == true)
                                        {
                                            encontrado = true;
                                            vetordata[i] = vetortecla[6].ToString();
                                            DataVoo = DataVoo + vetordata[i];
                                        }
                                        else
                                        {
                                            encontrado = false;
                                            break;
                                        }

                                        AtualizarTela(vetordata);
                                    }
                                    else
                                    {
                                        if (vetortecla[0] == 'D')
                                        {
                                            if (vetornumeros.Contains(vetortecla[1].ToString()) == true)
                                            {
                                                encontrado = true;
                                                vetordata[i] = vetortecla[1].ToString();
                                                DataVoo = DataVoo + vetordata[i];
                                            }
                                            else
                                            {
                                                encontrado = false;
                                                break;
                                            }

                                            AtualizarTela(vetordata);
                                        }
                                    }
                                }

                                //Se dados da hora forem válidos, continua:
                                //A variável DataVoo nesse instante tem a data e a hora sem formatação, ex: "123456781234":
                                if (encontrado == true)
                                {
                                    //Verifica se a data de cadastro do voo não é data antiga:
                                    if (DateTime.Compare(DateHourConverter(DataVoo), System.DateTime.Now) > 0)
                                    {
                                        //////RETORNA A DATA DO VOO "123456781234"
                                        return DataVoo;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Não é possível agendar voo em datas passadas!");
                                        retornar = PausaMensagem();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Insira uma hora válida!");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Por favor, insira uma data válida!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Erro: Por favor, insira uma data válida!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    Console.Clear();
                    return null;

                #endregion


                case "idaeronave":

                    #region IdAeronave

                    //Os prefixos de nacionalidade que identificam aeronaves privadas e comerciais do Brasil são PT, PR, PP, PS e PH.
                    string[] prefixoaeronave = new string[] { "PT", "PR", "PP", "PS", "PH" };

                    //A Agência Nacional de Aviação Civil(Anac) proíbe o registro de marcas de identificação em aeronaves iniciadas com a letra Q
                    //ou que tenham W como segunda letra.Os arranjos SOS, XXX, PAN, TTT, VFR, IFR, VMC e IMC não podem ser utilizados.
                    string[] idproibido = new string[] { "SOS", "XXX", "PAN", "TTT", "VFR", "IFR", "VMC", "IMC" };
                    encontrado = false;
                    string idaeronave;

                    do
                    {
                        Console.Write("Informe o código Nacional de identificação da Aeronave: ");
                        try
                        {
                            idaeronave = Console.ReadLine().ToUpper();

                            char[] letras = idaeronave.ToCharArray();

                            //Verifica se tem 6 caracteres obrigatoriamente:
                            if (letras.Length == 6)
                            {
                                //verifica se foi inserido o traço - na inscrição:
                                if (letras[2] == '-')
                                {
                                    //Verifica se tem Q e W onde não pode na matrícula da aeronave:
                                    if (letras[3] != 'Q' && letras[4] != 'W')
                                    {
                                        //Separa a escrita depois do traço, referente à matrícula do avião:
                                        string matriculaaviao = letras[3].ToString() + letras[4].ToString() + letras[5].ToString();
                                        //Verifica se a matrícula possui um nome proibido, contido no vetor idproibido;
                                        if (idproibido.Contains(matriculaaviao) == false)
                                        {
                                            //Separa os 2 primeiros prefixos e guarda na variável prefixoaviao:
                                            string prefixoaviao = letras[0].ToString() + letras[1].ToString();
                                            //Verifica se os 2 primeiros prefixos são válidos:
                                            if (prefixoaeronave.Contains(prefixoaviao) == true)
                                            {

                                                Console.Write("\nBuscando Aeronave . ");
                                                Thread.Sleep(200);
                                                Console.Write(" .");
                                                Thread.Sleep(200);
                                                Console.Write(" .\n");
                                                Thread.Sleep(200);

                                                string cnpj = ValidarEntrada("cnpjexiste");
                                                string BuscaAero = $"SELECT Inscricao,Capacidade,UltimaVenda, DataCadastro,Situacao,Cnpj FROM AERONAVE WHERE INSCRICAO='{idaeronave}'AND CNPJ='{cnpj}';";
                                                if (!string.IsNullOrEmpty(bd.SelectAeronave(BuscaAero)))
                                                {
                                                    encontrado = true;
                                                    Console.WriteLine("Essa Aeronave já possui cadastro!");
                                                    retornar = PausaMensagem();
                                                    break;
                                                }
                                                if (encontrado == false)
                                                {
                                                    return idaeronave;
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("Os prefixos devem ser obrigatóriamente PT ou PR ou PP ou PS ou PH ");
                                                retornar = PausaMensagem();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("As matrículas SOS, XXX, PAN, TTT, VFR, IFR, VMC e IMC não podem ser utilizadas");
                                            retornar = PausaMensagem();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Não é permitido a letra Q como primeira letra e nem a letra W como segunda letra da matrícula da aeronave");
                                        retornar = PausaMensagem();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Digite obrigatóriamente o traço - após prefixos de nacionalidade");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Digite obrigatóriamente o traço - Quantidade incorreta de dígitos de identificação.");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Insira um valor válido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion


                case "capacidade":

                    #region Capacidade

                    encontrado = false;
                    string capacidade;
                    string[] vetorcapacidade = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", " " };
                    do
                    {
                        Console.Write("Informe a quantidade de passageiros que a aeronave comporta: ");
                        try
                        {
                            capacidade = Console.ReadLine();

                            //Cria um vetor onde cada casa é um caracter:
                            char[] caracteres = capacidade.ToString().ToCharArray();
                            char[] caraccapacidade = new char[3];

                            //Verifica se tem no máximo 3 caracteres obrigatoriamente:
                            if (caracteres.Length > 0 && caracteres.Length <= 3)
                            {
                                if (caracteres.Length == 1)
                                {
                                    caraccapacidade[0] = ' ';
                                    caraccapacidade[1] = ' ';
                                    caraccapacidade[2] = caracteres[0];
                                }
                                else
                                {
                                    if (caracteres.Length == 2)
                                    {
                                        caraccapacidade[0] = ' ';
                                        caraccapacidade[1] = caracteres[0];
                                        caraccapacidade[2] = caracteres[1];
                                    }
                                    else
                                    {
                                        if (caracteres.Length == 3)
                                        {
                                            caraccapacidade[0] = caracteres[0];
                                            caraccapacidade[1] = caracteres[1];
                                            caraccapacidade[2] = caracteres[2];
                                        }
                                    }
                                }

                                string cap = caraccapacidade[0].ToString() + caraccapacidade[1].ToString() + caraccapacidade[2].ToString();

                                for (int i = 0; i < 3; i++)
                                {
                                    if (vetorcapacidade.Contains(caraccapacidade[i].ToString()))
                                    {
                                        encontrado = true;
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        break;
                                    }
                                }
                                if (encontrado == true)
                                {
                                    return cap;
                                }
                                else
                                {
                                    Console.WriteLine("Só aceita dígitos numéricos válidos!");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Deve ter um valor de quantidade de passageiros, só aceita no máximo 3 dígitos!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Insira apenas números!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;



                #endregion


                case "situacao":

                    #region Situacao


                    do
                    {
                        Console.Clear();
                        Console.WriteLine("A situação ficará no sistema como:\n[A] - Ativa\n[I] - Inativa");
                        Console.CursorVisible = false;
                        ConsoleKeyInfo op = Console.ReadKey(true);

                        //Verificar se tecla pressionada foi A ou I (independente do CAPSLOCK estar ativado!)
                        if (op.Key == ConsoleKey.A)
                        {
                            Console.Clear();
                            return "A";
                        }
                        else
                        {
                            if (op.Key == ConsoleKey.I)
                            {
                                Console.Clear();
                                return "I";
                            }
                            else
                            {
                                Console.WriteLine("Escolha uma opção válida!");
                                retornar = PausaMensagem();
                            }
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;


                #endregion

                case "destino":

                    #region Destino

                    do
                    {
                        retornar = false;
                        encontrado = false;
                        Console.Write("Informe o código IATA do aeroporto de destino: ");
                        try
                        {
                            string iata = Console.ReadLine().ToUpper();
                            string SelectAeroporto = $"SELECT IATA FROM Aeroporto WHERE IATA='{iata}';";
                            if (!string.IsNullOrEmpty(bd.SelectIATA(SelectAeroporto)))
                            {
                                encontrado = true;
                                Console.Write("Aeroporto já cadastrado! \n");

                            }
                            if (encontrado == true)
                            {
                                return iata;
                            }

                            else
                            {
                                string insert = $"INSERT INTO Aeroporto(IATA) VALUES('{iata}');";

                                bd.InsertDado(insert);
                                Console.WriteLine("\nAeroporto Cadastrado com Sucesso!");


                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Insira um código IATA válido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion


                case "aeronave":

                    #region Aeronave

                    do
                    {
                        retornar = false;
                        Aeronave a = null;

                        Console.Write("Informe o código Nacional de identificação da Aeronave: ");
                        try
                        {
                            idaeronave = Console.ReadLine().ToUpper();

                            string BuscaAero = $"SELECT Inscricao,Capacidade,UltimaVenda, DataCadastro,Situacao,Cnpj FROM AERONAVE WHERE INSCRICAO='{idaeronave}';";
                            a = bd.SelectAeronaveVER(BuscaAero);


                            if (a != null)
                            {
                                if (a.Situacao == 'A')
                                {
                                    return idaeronave;
                                }
                                else
                                {
                                    Console.WriteLine("Essa Aeronave encontra-se inativa no sistema.");
                                    retornar = PausaMensagem();
                                }

                            }
                            else
                            {
                                Console.WriteLine("Código não encontrado! Insira um código válido!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERRO: Insira um código válido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;


                #endregion


                case "aeronaveeditar":
                    //OK
                    #region AeronaveEditar

                    do
                    {
                        retornar = false;

                        Console.Write("Informe o código Nacional de identificação da Aeronave: ");
                        try
                        {
                            idaeronave = Console.ReadLine().ToUpper();
                            string BuscaAeronave = $"SELECT Inscricao,Capacidade,UltimaVenda, DataCadastro,Situacao,Cnpj FROM AERONAVE WHERE INSCRICAO='{idaeronave}';";

                            if (!string.IsNullOrEmpty(bd.SelectAeronave(BuscaAeronave)))
                            {
                                return idaeronave;

                            }

                            else
                            {
                                Console.WriteLine("Código não encontrado! Insira um código válido!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERRO: Insira um código válido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;


                #endregion


                case "valorpassagem":

                    #region ValorPassagem

                    do
                    {
                        try
                        {
                            Console.Write("insira o valor da passagem: ");
                            float valor = float.Parse(Console.ReadLine());
                            if (valor > 0 && valor < 10000)
                            {
                                return valor.ToString("N2");
                            }
                            else
                            {
                                Console.WriteLine("Não é possível vender passagens desse valor");
                                retornar = PausaMensagem();
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Escolha uma opção válida!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    return null;

                #endregion

                case "cpflogin": //na hora da venda


                    #region CpflLogin   

                    do
                    {
                        retornar = false;

                        Console.Write("Informe o CPF para prosseguir: ");


                        try
                        {
                            string cpf = Console.ReadLine();

                            Passageiro p = null;
                            Restrito r = null;
                            encontrado = false;
                            string buscarestrito = $"SELECT CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao FROM PASSAGEIRO WHERE CPF='{cpf}'; ";
                            p = bd.SelectPassageiroVER(buscarestrito);
                            if (p != null)
                            {
                                if (p.Situacao == 'A')
                                {
                                    //Verifica se é maior de idade:
                                    if (DateTime.Compare(p.DataNascimento, System.DateTime.Now.AddYears(-18)) <= 0)
                                    {
                                        encontrado = false;
                                        string buscaRestrito = $"SELECT CPF FROM Restritos WHERE CPF=('{cpf}');";
                                        r = bd.SelectRestritoVER(buscaRestrito);
                                        if (r != null)// tá restrito
                                        {
                                            encontrado = true;
                                            break;
                                        }
                                        if (encontrado == false)
                                        {
                                            return cpf;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Impossível prosseguir! Esse Passageiro se encontra restrito!");
                                            retornar = PausaMensagem();
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Impossível prosseguir! Esse Passageiro é menor de idade!");
                                        retornar = PausaMensagem();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Passageiro Inativo no sistema!");
                                    retornar = PausaMensagem();
                                }
                            }
                            else
                            {
                                Console.WriteLine("CPF não encontrado! Insira um CPF válido!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Insira um CPF válido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);




                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;



                #endregion


                case "cpfexiste":

                    #region CpfExiste

                    do
                    {
                        retornar = false;

                        Console.Write("Informe o CPF para prosseguir: ");
                        try
                        {
                            string cpf = Console.ReadLine();


                            string localiza = $"SELECT CPF FROM PASSAGEIRO WHERE CPF=('{cpf}'); ";

                            if (bd.CpfExiste(localiza))
                            {
                                return cpf;
                            }
                            else
                            {
                                Console.WriteLine("CPF não encontrado! Insira um CPF válido!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("CPF Inválido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion

                case "idvoo":
                    //OK
                    #region IDVoo

                    do
                    {

                        encontrado = false;
                        retornar = false;
                        Console.Write("Informe o ID do Voo para prosseguir: ");
                        try
                        {
                            string idvoo = Console.ReadLine().ToUpper();

                            encontrado = false;
                            string selectIdVOO = $"SELECT IDVOO,IATA,DataVoo,DataCadastro,QuantidadeAssentosOcupados,Situacao FROM Voo WHERE IDVOO='{idvoo}'";
                            if (!string.IsNullOrEmpty(bd.SelectVoo(selectIdVOO)))
                            {
                                return idvoo;
                            }
                            else
                            {
                                Console.WriteLine("Código não encontrado! Insira um ID Voo válido!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ID do Voo inválido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;


                #endregion


                case "idvenda":
                    //OK
                    #region idvenda

                    do
                    {
                        encontrado = false;
                        retornar = false;

                        Console.Write("Informe o ID da venda: ");
                        try
                        {
                            string idvenda = Console.ReadLine().ToUpper();
                            string selectIdVENDA = $"SELECT  IDVenda,DataVenda,ValorTotal,CPF FROM Venda WHERE IDVenda='{idvenda}'";
                            if (!string.IsNullOrEmpty(bd.SelectVenda(selectIdVENDA)))
                            {
                                encontrado = true;
                            }

                            if (encontrado == true)
                            {
                                return idvenda;
                            }
                            else
                            {
                                Console.WriteLine("Código não encontrado! Insira um ID Venda válido!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Insira um código ID Venda válido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);

                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;

                #endregion

                case "cnpjexiste":
                    //OK
                    #region cnplogin
                    do
                    {
                        retornar = false;


                        Console.Write("Informe o CNPJ para prosseguir: ");
                        try
                        {
                            string cnpj = Console.ReadLine();

                            String localiza = $"SELECT CNPJ,RazaoSocial,DataAbertura,DataUltimoVoo,DataCadastro,Situacao FROM CompanhiaAerea WHERE CNPJ=('{cnpj}');";
                            if (!string.IsNullOrEmpty(bd.SelectCompanhiaAerea(localiza)))
                            {

                                return cnpj;
                            }

                            else
                            {
                                Console.WriteLine("CNPJ não encontrado! Insira um CNPJ válido!");
                                retornar = PausaMensagem();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("CPF Inválido!");
                            retornar = PausaMensagem();
                        }
                    } while (retornar == false);
                    //Retorna nulo se o usuário quiser cancelar no meio do cadastro;
                    return null;
                #endregion //OK

                default:
                    return null;
            }
        }
        #endregion

        #region Formatar Valor 
        static string ValorConverter(float valor)
        {
            try
            {
                string[] valorpassagem = new string[] { "0", ".", "0", "0", "0", ",", "0", "0" };
                string valorp = null;
                string valorstring = valor.ToString("N2");
                char[] vetorvalorstring = valorstring.ToCharArray();
                if (valorstring.Length == 4)
                {
                    valorpassagem[4] = vetorvalorstring[0].ToString();
                }
                else
                {
                    if (valorstring.Length == 5)
                    {
                        valorpassagem[3] = vetorvalorstring[0].ToString();
                        valorpassagem[4] = vetorvalorstring[1].ToString();
                    }
                    else
                    {
                        if (valorstring.Length == 6)
                        {
                            valorpassagem[2] = vetorvalorstring[0].ToString();
                            valorpassagem[3] = vetorvalorstring[1].ToString();
                            valorpassagem[4] = vetorvalorstring[2].ToString();
                        }
                        else
                        {
                            if (valorstring.Length == 8)
                            {
                                return valorstring;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                for (int i = 0; i < valorpassagem.Length; i++)
                {
                    valorp = valorp + valorpassagem[i];
                }
                return valorp;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region MENU INICIAL
        static void TelaInicial()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Bem vindo à On The Fly!");
                Console.WriteLine("\nPor Favor, informe a Opção Desejada:\n");
                Console.WriteLine(" 1 - Companhia Aérea\n"); //OK
                Console.WriteLine(" 2 - Passageiro\n"); // OK
                Console.WriteLine(" 3 - Compras de Passagens\n");
                Console.WriteLine(" 4 - Acesso a Lista de CPF Restritos\n");
                Console.WriteLine(" 5 - Acesso a Lista de CNPJ Restritos\n");
                Console.WriteLine(" 6 - Aeronaves\n"); // OK

                Console.WriteLine(" 7 - Voos Disponíveis");
                Console.WriteLine("\n 0 - Encerrar Sessão\n");
                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:
                        Console.Write("Saindo . ");
                        Thread.Sleep(200);
                        Console.Write(" .");
                        Thread.Sleep(200);
                        Console.Write(" .");
                        Thread.Sleep(200);
                        Environment.Exit(0);
                        break;

                    case 1:
                        TelaInicialCompanhiasAereas();
                        break;

                    case 2:
                        TelaInicialPassageiros();
                        break;

                    case 3:
                        TelaVendas();
                        break;

                    case 4:
                        TelaInicialCpfRestritos();
                        break;

                    case 5:
                        TelaInicialCnpjRestritos();
                        break;

                    case 6:
                        TelaVerAeronavesCadastradas();
                        break;
                    case 7:
                        TelaVoosDisponiveis();
                        Pausa();
                        break;
                }

            } while (opc != 0);

        }
        #endregion

        #region  VENDAS
        static void TelaVendas()
        {

            int opc;
            Console.WriteLine("Informe a opção desejada: \n");
            Console.WriteLine("1 - Compra de Passagem\n");
            Console.WriteLine("2 - Ver Passagens Vendidas\n");
            Console.WriteLine("3 - Ver Passagens Reservadas\n");
            Console.WriteLine("\n0 -  SAIR\n");
            opc = int.Parse(ValidarEntrada("menu"));
            Console.Clear();

            switch (opc)
            {
                case 0:
                    TelaInicialPassageiros();
                    break;
                case 1:
                    string cpfLogin = ValidarEntrada("cpflogin");
                    if (cpfLogin == null) TelaInicial();
                    string loginPassageiro = $"SELECT CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao FROM PASSAGEIRO WHERE CPF=('{cpfLogin}');";
                    bd.SelectPassageiroVerUm(loginPassageiro);

                    Console.Clear();
                    TelaVoosDisponiveis();
                    break;
                case 2:
                    TelaHistoricoVendas();
                    break;
                case 3:
                    TelaHistoricoReservadas();
                    break;
            }
        }

        #endregion

        #region Históricos
        static void TelaHistoricoVendas()
        {
            string selectVenda = $"SELECT IDPASSAGEM,IDVOO,DataUltimaOperacao ,ValorUnitario,Situacao FROM Passagem WHERE Situacao='{'P'}'";
            bd.SelectPassagem(selectVenda);
            Pausa();
            TelaVendas();
        } //OK

       

        static void TelaHistoricoReservadas()
        {

            string selectReserva = $"SELECT IDPASSAGEM,IDVOO,DataUltimaOperacao ,ValorUnitario,Situacao FROM Passagem WHERE Situacao='{'R'}'";
            bd.SelectPassagem(selectReserva);
           
            Pausa();
            TelaVendas();
        }  //OK


        #endregion

        #region Opções Voo
        //Cadastro Voo com ID Aleatório, Cadastro Passagens desse voo com ID Aleatório



        static void TelaCadastrarVoo(CompanhiaAerea compAtivo)
        {
            Console.Clear();
            string idVoo;
            string destino;
            string idAeronave;
            string auxData;
            DateTime dataVoo;
            float valor;

            string cnpj = ValidarEntrada("cnpjexiste");
            if (compAtivo.Situacao == 'A')
            {
                string dest = $"SELECT * FROM Aeroporto";
                bd.SelectIATA(dest);
                destino = ValidarEntrada("destino");
                if (destino == null) TelaOpcoesCompanhiaAerea(compAtivo);


                //Preciso da aeronave apenas para cadastrar as passagens abaixo, não é necessário eu ter uma propriedade aeronave em voo.
                idAeronave = ValidarEntrada("aeronave");
                if (idAeronave == null) TelaOpcoesCompanhiaAerea(compAtivo);

                auxData = ValidarEntrada("datavoo");
                if (auxData == null) TelaOpcoesCompanhiaAerea(compAtivo);
                dataVoo = DateHourConverter(auxData);

                valor = float.Parse(ValidarEntrada("valorpassagem"));
                if (valor.Equals(null)) TelaOpcoesCompanhiaAerea(compAtivo);

                idVoo = GeradorId("idvoo");
                if (idVoo == null) TelaOpcoesCompanhiaAerea(compAtivo);

                Voo novoVoo = new Voo(idVoo, destino, dataVoo, System.DateTime.Now, 0, 'A');

                string insertVOO = $"INSERT INTO VOO(IDVOO,IATA,DataVoo,DataCadastro,QuantidadeAssentosOcupados,Situacao) VALUES('{idVoo}'," + $"'{destino}','{dataVoo}','{novoVoo.DataCadastro}','{novoVoo.QuantidadeAssentosOcupados}','{novoVoo.Situacao}');" +
                $"EXEC dbo.CadastroPassagens {valor};";
                bd.InsertDado(insertVOO);

                string buscaVoo = $"SELECT * FROM VOO WHERE IDVOO ='{idVoo}'  AND INSCRICAO='{idAeronave}';";
                bd.SelectVoo(buscaVoo);
                Console.WriteLine("Voo cadastrado com sucesso!");
                Pausa();
                TelaOpcoesCompanhiaAerea(compAtivo);
            }
            else
            {
                Console.WriteLine("\nCompanhia INATIVA no sistema, não pode cadastrar voo!");
                Pausa();
                TelaOpcoesCompanhiaAerea(compAtivo);
            }





        } // OK
        static void TelaVoosDisponiveis()  // OK
        {
            int opc;
            string VooDisp = $"SELECT * FROM Voo WHERE Situacao='{'A'}'";
            bd.SelectVoo(VooDisp);

            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
            Console.WriteLine("\n1 - Escolher o Voo Desejado: ");
            Console.WriteLine("0 - Voltar");
            opc = int.Parse(ValidarEntrada("menu"));
            Console.Clear();

            switch (opc)
            {
                case 0:
                    TelaInicial();
                    break;
                case 1:

                    string idvoo = ValidarEntrada("idvoo");
                    if (idvoo == null) TelaVoosDisponiveis();
                    TelaDescricaoVoo(idvoo);
                    break;
            }
        }
        static void TelaDescricaoVoo(string idvoo)
        {

            int opc;

            Passagem pass = new();
            string voo = $"SELECT * FROM Voo WHERE IDVOO='{idvoo}'";
            Voo V = bd.VerVoo(voo);
            Console.WriteLine("ID: " + V.IDVoo + "   Destino: " + V.Destino);
            Console.ReadKey();
            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
            Console.WriteLine("1 - Comprar: ");
            Console.WriteLine("2 - Reservar: ");
            Console.WriteLine("0 - Voltar: ");
            opc = int.Parse(ValidarEntrada("menu"));
            Console.Clear();

            switch (opc)
            {
                case 0:
                    TelaVoosDisponiveis();
                    break;
                case 1:

                case 2:

                    int cont = 0;
                    bool retornar = false;
                    int quantPassagem;
                    string cpf;

                    do
                    {
                        string IDVenda = GeradorId("idvenda");
                        Console.Clear();
                        cpf = ValidarEntrada("cpflogin");
                        if (cpf == null) TelaInicial();
                        Console.WriteLine("\nInforme a quantidade de passagens (máximo 4): \n1  2  3  4");
                        quantPassagem = int.Parse(ValidarEntrada("menu"));
                        if (quantPassagem > 0 && quantPassagem <= 4)
                        {
                            string conta = $"SELECT COUNT (*) FROM PASSAGEM WHERE IDVOO='{idvoo}'AND Situacao='{'L'}' ;";
                            int contagem = bd.ContaP(conta); //retorna um inteiro
                            if (contagem < quantPassagem) //SE A QUANTIDADE DE PASSAGENS SOLICITADAS FOR MAIOR DOQ A QUANTIDADE DE PASSAGENS DISPONÍVEIS
                            {
                                Console.WriteLine("Não possui esta quantidade de passagens disponíveis: ");
                                retornar = PausaMensagem();

                            }
                            do
                            {
                                string livre = $"SELECT TOP 1 * FROM PASSAGEM WHERE IDVOO='{idvoo}'AND Situacao='{'L'}' ;";
                                pass = bd.VerPassagem(livre);
                                string updatePassagem = $"UPDATE Passagem set Situacao = '{(opc == 1 ? 'P' : 'R')}' WHERE IDVOO='{idvoo}'AND IDPASSAGEM='{pass.IDPassagem}';";//if ternário
                                bd.UpdateDado(updatePassagem);
                                string updateUltimaOp = $"UPDATE Passagem set DataUltimaOperacao = '{System.DateTime.Now}' WHERE IDVOO='{idvoo}'AND IDPASSAGEM='{pass.IDPassagem}';";
                                bd.UpdateDado(updateUltimaOp);
                               
                                if (cont == 0)
                                {

                                    Venda venda = new Venda( IDVenda, System.DateTime.Now, (pass.ValorUnitario * quantPassagem), cpf);
                                    string insertVENDA = $"INSERT INTO Venda(IDVENDA,DataVenda,ValorTotal,CPF) VALUES('{venda.IDVenda}'," + $"'{venda.DataVenda}','{venda.ValorTotal}','{cpf}')";
                                    bd.InsertDado(insertVENDA);
                                  

                                }
                                VendaPassagem item = new VendaPassagem(GeradorId("iditemvenda"), IDVenda, pass.ValorUnitario);

                                string insertITEMVENDA = $"INSERT INTO VendaPassagem(IDITEMVENDA,IDVENDA,ValorUnitario) VALUES('{item.IDItemVenda}'," + $"'{item.IDVenda}','{item.ValorUnitario}')";
                                bd.InsertDado(insertITEMVENDA);

                                conta = $"SELECT COUNT (*) FROM PASSAGEM WHERE IDVOO='{idvoo}'AND Situacao='{'P'}' or Situacao='{'R'}' ;";
                                contagem = bd.ContaV(conta);
                               
                                string AssentosOcupados = $"UPDATE VOO set QuantidadeAssentosOcupados='{(contagem)}' WHERE IDVOO='{idvoo}'";
                                bd.UpdateDado(AssentosOcupados);
                                cont++;

                            } while (cont < quantPassagem);

                            Console.WriteLine("Compra realizada com sucesso!");
                            Pausa();
                            TelaVendas();



                        }
                        else
                        {
                            Console.WriteLine("Só é possível comprar [4] passagens por venda");
                            retornar = PausaMensagem();
                        }

                    } while (retornar == true);

                    break;


            }
        }


        #endregion

        // Login/Cadastrar/Opções/Editar 
        #region Companhia Aérea Opções
        static void TelaLoginCompanhiaAerea()
        {
            string cnpj;

            Console.Clear();

            cnpj = ValidarEntrada("cnpjexiste");
            if (cnpj == null) TelaInicialCompanhiasAereas();

            string SelectComp = $"SELECT * FROM CompanhiaAerea WHERE CNPJ='{cnpj}'";

            CompanhiaAerea compAtivo = bd.SelectCompanhiaAereaVER(SelectComp);
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Login efetuado com sucesso! ");
            Pausa();

            TelaOpcoesCompanhiaAerea(compAtivo);



        } // ok
        static void TelaCadastrarCompanhiaAerea()
        {
            string nomeComp;
            string cnpj;
            string dataAbertura;

            nomeComp = ValidarEntrada("nome");
            if (nomeComp == null) TelaInicialCompanhiasAereas();

            cnpj = ValidarEntrada("cnpj");
            if (cnpj == null) TelaInicialCompanhiasAereas();

            dataAbertura = ValidarEntrada("dataabertura");
            if (dataAbertura == null) TelaInicialCompanhiasAereas();

            CompanhiaAerea novaComp = new CompanhiaAerea(cnpj, nomeComp, DateConverter(dataAbertura), System.DateTime.Now, System.DateTime.Now, 'A');


            string insert = $"INSERT INTO CompanhiaAerea(CNPJ,RazaoSocial,DataAbertura,DataUltimoVoo,DataCadastro,Situacao) VALUES('{novaComp.Cnpj}'," + $"'{novaComp.RazaoSocial}','{novaComp.DataAbertura}','{novaComp.DataUltimoVoo}','{novaComp.DataCadastro}','{novaComp.Situacao}');";

            bd.InsertDado(insert);
            TelaInicialCompanhiasAereas();


        } // OK
        static void TelaOpcoesCompanhiaAerea(CompanhiaAerea compAtivo)
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\nCompanhia Aérea " + compAtivo.RazaoSocial);
                Console.WriteLine("\nPor Favor, informe a Opção Desejada:\n");
                Console.WriteLine(" 1 - Cadastrar uma nova Aeronave\n"); // OK
                Console.WriteLine(" 2 - Programar um novo Voo\n"); // OK 
                Console.WriteLine(" 3 - Cancelar um voo\n"); // update voo situacao=C -- OK
                Console.WriteLine(" 4 - Ativar/Inativar Aeronave\n"); // update aeronave situacao = A/I -- OK
                Console.WriteLine(" 5 - Editar dados da Companhia Aerea\n"); // update Companhia Aerea  -- OK
                Console.WriteLine(" 6 - Vizualizar dados da Companhia Aerea\n");// OK
                Console.WriteLine(" 7 - Ver Aeroportos Cadastrados\n");// OK

                Console.WriteLine("\n 0 - Encerrar Sessão\n");
                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:

                        TelaInicial();

                        break;

                    case 1:

                        TelaCadastrarAeronave(compAtivo); //OK

                        break;

                    case 2:

                        TelaCadastrarVoo(compAtivo); // OK

                        break;


                    case 3:
                        string selectVoo = $"SELECT IDVOO,IATA,DataVoo,DataCadastro,QuantidadeAssentosOcupados,Situacao FROM Voo";
                        bd.SelectVoo(selectVoo);

                        Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                        Console.WriteLine("\n1 - Escolher o Voo Desejado: ");
                        Console.WriteLine("0 - Voltar");
                        opc = int.Parse(ValidarEntrada("menu"));
                        Console.Clear();

                        switch (opc)
                        {
                            case 0:
                                TelaOpcoesCompanhiaAerea(compAtivo);
                                break;
                            case 1:
                                Console.Clear();
                                string idvoo = ValidarEntrada("idvoo");
                                if (idvoo == null) TelaOpcoesCompanhiaAerea(compAtivo);



                                string updateSituacao = $"UPDATE Voo set Situacao = '{'C'}' WHERE IDVOO='{idvoo}';";
                                bd.UpdateDado(updateSituacao);
                                Console.WriteLine("Voo CANCELADO!! Um novo Voo deve ser cadastrado.");
                                Pausa();
                                TelaOpcoesCompanhiaAerea(compAtivo);
                                break;
                        }

                        break;


                    case 4:

                        TelaEditarAeronave(compAtivo); //OK


                        break;

                    case 5:

                        TelaEditarCompanhiaAerea(compAtivo); // OK

                        break;
                    case 6:
                        Console.WriteLine("Confirmação de seu CNPJ ");
                        string cnpj = ValidarEntrada("cnpjexiste");
                     //   String selectComp = $"SELECT CNPJ,RazaoSocial,DataAbertura,DataUltimoVoo,DataCadastro,Situacao FROM CompanhiaAerea WHERE CNPJ=('{cnpj}');";
                     //   bd.SelectCompanhiaAerea(selectComp);
                        break;
                    case 7:
                        string verAeroporto = $"SELECT IATA FROM Aeroporto ;";
                        bd.SelectIATA(verAeroporto);
                        break;

                }


            } while (true);
        } // Visualiza
        static void TelaEditarCompanhiaAerea(CompanhiaAerea companhiaAerea)
        {
            DateTime datanova;
            int opc;
            char novaSituacao;
            string novadata;

            do
            {
                Console.Clear();
                Console.WriteLine("\nEDTAR DADOS");
                Console.WriteLine("\nEscolha qual Dado deseja Editar: ");
                Console.Write("\n 1 - Data de Abertura\n");
                Console.Write("\n 2 - Situação (Ativo / Inativo)\n\n");
                Console.Write("\n 0 - Voltar");
                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:
                        TelaOpcoesCompanhiaAerea(companhiaAerea);
                        break;

                    case 1:
                        Console.WriteLine("Confirmação de seu CNPJ ");
                        string cnpj = ValidarEntrada("cnpjexiste");
                        Console.WriteLine("\nData de abertura Atual: " + companhiaAerea.DataAbertura.ToString("dd/MM/yyyy"));
                        Console.Write("\n\nInforme a nova data");
                        Pausa();
                        novadata = ValidarEntrada("dataabertura");
                        if (novadata == null) TelaEditarCompanhiaAerea(companhiaAerea);
                        datanova = DateConverter(novadata);

                        companhiaAerea.DataAbertura = datanova;
                        //  GravarCompanhiaAerea();
                        string updateDataAbertura = $"UPDATE CompanhiaAerea set DataAbertura = '{datanova}' WHERE CNPJ='{cnpj}';";
                        bd.UpdateDado(updateDataAbertura);

                        Console.Clear();
                        Console.WriteLine("\nData de abertura alterada com Sucesso!");
                        Pausa();
                        TelaEditarCompanhiaAerea(companhiaAerea);
                        break;

                    case 2:
                        Console.Clear();

                        Console.WriteLine("Confirmação de seu CNPJ ");
                        cnpj = ValidarEntrada("cnpjexiste");
                        Console.WriteLine("\nCompanhia Aérea: " + companhiaAerea.RazaoSocial);
                        if (companhiaAerea.Situacao == 'A')
                        { Console.WriteLine("\nSituação Atual: ATIVA"); }

                        if (companhiaAerea.Situacao == 'I')
                        { Console.WriteLine("\nSituação Atual: INATIVA"); }

                        Pausa();

                        novaSituacao = char.Parse(ValidarEntrada("situacao"));
                        if (novaSituacao.Equals(null)) TelaEditarCompanhiaAerea(companhiaAerea);

                        companhiaAerea.Situacao = novaSituacao;
                        // GravarCompanhiaAerea();
                        string updateSituacao = $"UPDATE CompanhiaAerea set Situacao = '{novaSituacao}' WHERE CNPJ='{cnpj}';";
                        bd.UpdateDado(updateSituacao);
                        Console.Clear();
                        Console.WriteLine("\nSituação de Cadastro Alterada com Sucesso!");
                        Pausa();
                        TelaEditarCompanhiaAerea(companhiaAerea);
                        break;

                }

            } while (true);
        } // OK
        #endregion

        //Login/Cadastrar/Editar 
        #region Passageiro Opções
        static void TelaLoginPassageiro()
        {
            string cpf;
            Passageiro passageiroAtivo = new Passageiro();
            Console.Clear();
            cpf = ValidarEntrada("cpfexiste");
            if (cpf == null) TelaInicialPassageiros();
            //se encontrar o cpf
            Console.WriteLine("Login efetuado com sucesso! ");
            Pausa();
            Console.Write("Deseja:  1 - Editar Dados   2 - Visualizar Passageiro ");
            int quero = int.Parse(Console.ReadLine());
            if (quero == 1)
            {
                String selectPassageiro = $"SELECT CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao FROM PASSAGEIRO WHERE CPF=('{cpf}');";
                bd.SelectPassageiroVerUm(selectPassageiro);
                TelaEditarPassageiro(passageiroAtivo);

            }
            else if (quero == 2)
            {
                String selectP = $"SELECT CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao FROM PASSAGEIRO WHERE CPF=('{cpf}');";
                bd.SelectPassageiroVerUm(selectP);
                Pausa();
            }
            else
            {
                Console.WriteLine("Opção Inválida!");
            }
        } // Visualiza 
        static void TelaCadastrarPassageiro()
        {
            do
            {
                string nome, cpf;
                string dataNascimento;
                char sexo;

                nome = ValidarEntrada("nome");
                if (nome == null) TelaInicialPassageiros();
                cpf = ValidarEntrada("cpf");
                if (cpf == null) TelaInicialPassageiros();

                dataNascimento = ValidarEntrada("datanascimento");
                if (dataNascimento == null) TelaInicialPassageiros();

                sexo = char.Parse(ValidarEntrada("sexo"));
                if (sexo.Equals(null)) TelaInicialPassageiros();


                Passageiro novoPassageiro = new Passageiro(cpf, nome, DateConverter(dataNascimento), sexo, System.DateTime.Now, System.DateTime.Now, 'A');

                string insert = $"INSERT INTO Passageiro(CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao) VALUES('{novoPassageiro.Cpf}'," + $"'{novoPassageiro.Nome}','{novoPassageiro.DataNascimento}','{novoPassageiro.Sexo}','{novoPassageiro.DataUltimaCompra}','{novoPassageiro.DataCadastro}','{novoPassageiro.Situacao}');";

                bd.InsertDado(insert);
                Console.WriteLine("\nPassageiro Cadastrado com Sucesso!");
                Pausa();
                TelaInicialPassageiros();
            } while (true);
        }  // OK
        static void TelaEditarPassageiro(Passageiro passageiroAtivo)
        {
            int opc;
            string novoNome;
            string novaDataNascimento;
            DateTime data;
            char novoSexo;
            char novaSituacao;

            do
            {
                Console.Clear();
                Console.WriteLine("\nEDTAR DADOS ");
                Console.WriteLine("\nEscolha qual Dado deseja Editar: ");
                Console.Write("\n 1 - Nome");
                Console.Write("\n 2 - Data de Nascimento");
                Console.Write("\n 3 - Sexo");
                Console.Write("\n 4 - Situação (Ativo / Inativo)");
                Console.Write("\n\n 0 - Sair");
                opc = int.Parse(ValidarEntrada("menu"));

                switch (opc)
                {
                    case 0:
                        TelaInicial();
                        break;

                    case 1:

                        Console.Clear();
                        Console.WriteLine("Confirmação de seu cpf ");
                        string cpf = ValidarEntrada("cpfexiste");

                        Console.Write("\n\nInforme o Novo Nome");
                        Pausa();
                        novoNome = ValidarEntrada("nome");
                        if (novoNome == null) TelaEditarPassageiro(passageiroAtivo);

                        passageiroAtivo.Nome = novoNome;

                        string updateN = $"UPDATE Passageiro set Nome = '{novoNome}' WHERE CPF='{cpf}';";
                        bd.UpdateDado(updateN);
                        Console.Clear();
                        Console.WriteLine("\nNome Alterado com Sucesso!");
                        Pausa();
                        TelaEditarPassageiro(passageiroAtivo);

                        break;

                    case 2:

                        Console.Clear();
                        Console.WriteLine("Confirmação de seu cpf ");
                        cpf = ValidarEntrada("cpfexiste");

                        Console.Write("\n\nInforme a Nova Data de Nascimento");
                        Pausa();
                        novaDataNascimento = ValidarEntrada("datanascimento");
                        if (novaDataNascimento == null) TelaEditarPassageiro(passageiroAtivo);

                        data = DateConverter(novaDataNascimento);
                        passageiroAtivo.DataNascimento = data;

                        string updateDN = $"UPDATE Passageiro set DataNascimento = '{data}' WHERE CPF='{cpf}';";
                        bd.UpdateDado(updateDN);
                        Console.Clear();
                        Console.WriteLine("\nData de Nascimento Alterada com Sucesso!");
                        Pausa();
                        TelaEditarPassageiro(passageiroAtivo);

                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Confirmação de seu cpf ");
                        cpf = ValidarEntrada("cpfexiste");

                        Console.Write("\n\nInforme o Novo Sexo");
                        Pausa();
                        novoSexo = char.Parse(ValidarEntrada("sexo"));
                        if (novoSexo.Equals(null)) TelaInicialPassageiros();

                        passageiroAtivo.Sexo = novoSexo;

                        string updateS = $"UPDATE Passageiro set Sexo = '{novoSexo}' WHERE CPF='{cpf}';";
                        bd.UpdateDado(updateS);
                        Console.Clear();
                        Console.WriteLine("\nSexo Alterado com Sucesso!");
                        Pausa();
                        TelaEditarPassageiro(passageiroAtivo);
                        break;


                    case 4:

                        Console.Clear();
                        Console.WriteLine("Confirmação de seu cpf ");
                        cpf = ValidarEntrada("cpfexiste");

                        if (passageiroAtivo.Situacao == 'A')
                        { Console.WriteLine("\nSituação Atual: ATIVO"); }

                        if (passageiroAtivo.Situacao == 'I')
                        { Console.WriteLine("\nSituação Atual: INATIVO"); }

                        Pausa();

                        novaSituacao = char.Parse(ValidarEntrada("situacao"));
                        if (novaSituacao.Equals(null)) TelaInicialPassageiros();

                        passageiroAtivo.Situacao = novaSituacao;

                        string updateSituacao = $"UPDATE Passageiro set Situacao = '{novaSituacao}' WHERE CPF='{cpf}';";
                        bd.UpdateDado(updateSituacao);
                        Console.Clear();
                        Console.WriteLine("\nSituação de Cadastro Alterada com Sucesso!");
                        Pausa();
                        TelaEditarPassageiro(passageiroAtivo);
                        break;
                }

            } while (true);
        }    // OK

        #endregion

        #region MENU COMPANHIA AÉREA
        static void TelaInicialCompanhiasAereas()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Companhia Aérea já cadastrada\n");
                Console.WriteLine(" 2 - Cadastrar uma Nova Companhia Aérea\n");
                Console.WriteLine(" 3 - Visualizar todas as Companhias Aéreas\n");
                Console.WriteLine("\n 0 - SAIR\n");
                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                    case 0:
                        TelaInicial();
                        break;

                    case 1:
                        TelaLoginCompanhiaAerea();
                        break;

                    case 2:
                        TelaCadastrarCompanhiaAerea();
                        break;
                    case 3:
                        String selectC = $"SELECT CNPJ,RazaoSocial,DataAbertura,DataUltimoVoo,DataCadastro,Situacao FROM CompanhiaAerea";
                        bd.SelectCompanhiaAerea(selectC);
                        break;
                }

            } while (opc != 0);
        }
        #endregion

        #region MENU PASSAGEIRO
        static void TelaInicialPassageiros()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Passageiro já cadastrado\n");
                Console.WriteLine(" 2 - Cadastrar um novo Passageiro\n");
                Console.WriteLine(" 3 - Visualizar todos Passageiros\n");
                Console.WriteLine("\n 0 - SAIR\n");
                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                    case 0:
                        TelaInicial();
                        break;

                    case 1:
                        TelaLoginPassageiro();
                        break;

                    case 2:
                        TelaCadastrarPassageiro();
                        break;
                    case 3:
                        String selectP = $"SELECT CPF,Nome,DataNascimento,Sexo,DataUltimaCompra,DataCadastro,Situacao FROM PASSAGEIRO";
                        bd.SelectPassageiroVizualizarTodos(selectP);
                        break;

                }

            } while (opc != 0);
        }
        #endregion

        //Cadastra/Vizualiza/Edita 
        #region Opções Aeronave
        static void TelaCadastrarAeronave(CompanhiaAerea compAtivo)
        {
            string idAeronave;
            int capacidade;

            char situacao;
            Aeronave novaAeronave;



            idAeronave = ValidarEntrada("idaeronave");
            if (idAeronave == null) TelaOpcoesCompanhiaAerea(compAtivo);
            string Aero = $"Select * from Aeronave where INSCRICAO='{idAeronave}';";
            novaAeronave = bd.SelectAeronaveCadastrar(Aero);
            if (novaAeronave != null)
            {
                Console.WriteLine("ID já cadastrado!");
                //está cadastrada, para  o cadastro

            }
            else
            {

                capacidade = int.Parse(ValidarEntrada("capacidade"));
                if (capacidade.Equals(null)) TelaOpcoesCompanhiaAerea(compAtivo);

                situacao = char.Parse(ValidarEntrada("situacao"));
                if (situacao.Equals(null)) TelaOpcoesCompanhiaAerea(compAtivo);

                Console.ReadKey();
                novaAeronave = new Aeronave(idAeronave, capacidade, System.DateTime.Now, System.DateTime.Now, situacao, compAtivo.Cnpj);

                string insert = $"INSERT INTO Aeronave(INSCRICAO,Capacidade,UltimaVenda,DataCadastro,Situacao,CNPJ) VALUES('{novaAeronave.Inscricao}'," + $"'{novaAeronave.Capacidade}','{novaAeronave.UltimaVenda}','{novaAeronave.DataCadastro}','{novaAeronave.Situacao}','{novaAeronave.Cnpj}');";

                bd.InsertDado(insert);
                Console.WriteLine("\nCadastro Realizado com Sucesso!");
            }
            Pausa();
            TelaOpcoesCompanhiaAerea(compAtivo);
        } //OK

        static void TelaVerAeronavesCadastradas()
        {
            Console.Clear();


            String selectA = $"SELECT Inscricao,Capacidade,UltimaVenda, DataCadastro,Situacao,Cnpj FROM AERONAVE;";
            bd.SelectAeronave(selectA);

            Pausa();
            TelaInicial();
        } // OK

        static void TelaEditarAeronave(CompanhiaAerea compAtivo)
        {
            string idaeronave = ValidarEntrada("aeronaveeditar");
            if (idaeronave == null) TelaOpcoesCompanhiaAerea(compAtivo);

            string situacao = ValidarEntrada("situacao");
            if (situacao == null) TelaOpcoesCompanhiaAerea(compAtivo);
            char s = char.Parse(situacao);

            string updateSituacao = $"UPDATE Aeronave set Situacao = '{s}' WHERE INSCRICAO='{idaeronave}';";
            bd.UpdateDado(updateSituacao);

            Console.Clear();
            Console.WriteLine("Situação alterada com sucesso!");
            Pausa();
            TelaOpcoesCompanhiaAerea(compAtivo);
        } // OK
        #endregion


        #region Exceções
        static void TelaInicialCpfRestritos()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\n'CPF' RESTRITOS");
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Ver a Lista de 'CPF' Restritos\n");
                Console.WriteLine(" 2 - Adicionar um 'CPF' à Lista de Restritos\n");
                Console.WriteLine(" 3 - Remover um 'CPF' da Lista de Restritos\n");
                Console.WriteLine("\n 0 - Sair\n");

                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:

                        TelaInicial();

                        break;

                    case 1:
                        string verRestrito = "SELECT * FROM Restrito"; //só tem cpf em restrito
                        bd.SelectRestrito(verRestrito);
                        Pausa();
                        break;
                    case 2:
                        Console.WriteLine("Só é possível adicionar na lista de restritos um CPF cadastrado no sistema!\n");
                        Pausa();
                        string adcCpf = ValidarEntrada("cpfexiste");
                        if (adcCpf == null) TelaInicialCpfRestritos();
                        string adcRestrito = $"INSERT INTO Restrito(CPF) VALUES('{adcCpf}');";
                        bd.InsertDado(adcRestrito);
                        Console.Clear();
                        Console.WriteLine("Cpf adiconado com sucesso");
                        Pausa();
                        TelaInicialCpfRestritos();
                        break;

                    case 3:
                   
                        string cpf = ValidarEntrada("cpfexiste");
                        if (cpf == null) TelaInicialCpfRestritos();
                        string removeRestrito = $"DELETE FROM Restrito WHERE CPF = '{cpf}';";
                        bd.DeleteDado(removeRestrito);

                        Console.Clear();
                        Pausa();
                        TelaInicialCpfRestritos();

                        break;
                }

            } while (true);
        }
        static void TelaInicialCnpjRestritos()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\n'CNPJ' RESTRITOS");
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Ver a Lista de 'CNPJ' Bloqueados\n");
                Console.WriteLine(" 2 - Adicionar um 'CNPJ' à Lista de Bloqueados\n");
                Console.WriteLine(" 3 - Remover um 'CNPJ' da Lista de Bloqueados\n");
                Console.WriteLine("\n 0 - Sair\n");

                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:

                        TelaInicial();
                        break;

                    case 1:
                        string verBloqueado = "SELECT * FROM Bloqueado"; //só tem cpf em restrito
                        bd.SelectBloqueado(verBloqueado);
                        Pausa();
                        break;

                    case 2:
                        Console.WriteLine("Só é possível adicionar na lista de restritos um CNPJ cadastrado no sistema!\n");
                        Pausa();
                        string adcCnpj = ValidarEntrada("cnpjexiste");
                        if (adcCnpj == null) TelaInicialCpfRestritos();
                        string adcBloq = $"INSERT INTO Bloqueado(CNPJ) VALUES('{adcCnpj}');";
                        bd.InsertDado(adcBloq);
                        Console.Clear();
                        Console.WriteLine("Cnpj adiconado com sucesso");
                        Pausa();
                        TelaInicialCnpjRestritos();
                        break;
                    case 3:
                        Console.Write("Informe o CNPJ que deseja excluir: ");
                        string cnpj = ValidarEntrada("cnpjexiste");
                        if (cnpj == null) TelaInicialCnpjRestritos();
                        string removeBloq = $"DELETE FROM Bloqueado WHERE CNPJ='{cnpj}';";
                        bd.DeleteDado(removeBloq);

                        Console.Clear();
                        Console.WriteLine("Cnpj Removido com sucesso!");
                        Pausa();
                        TelaInicialCnpjRestritos();
                        break;
                }

            } while (true);
        }

        #endregion

        static void Main(string[] args)
        {
            TelaInicial();
        }

    }
}
