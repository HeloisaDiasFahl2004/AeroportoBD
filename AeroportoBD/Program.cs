using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AeroportoBD
{
    internal class Program
    {
        #region Declarações
        static BD bd = new BD();

        static CompanhiaAerea c = new CompanhiaAerea();//Pra poder chamar seus métodos

        static List<Passageiro> listPassageiro = new List<Passageiro>();
        static List<CompanhiaAerea> listCompanhia = new List<CompanhiaAerea>();
        static List<Aeronave> listAeronaves = new List<Aeronave>();
        static List<string> voosrealizados = new List<string>();
        static List<Voo> listVoo = new List<Voo>();
        static List<Passagem> listPassagem = new List<Passagem>();
        static List<Venda> listVenda = new List<Venda>();
        static List<VendaPassagem> listItemVenda = new List<VendaPassagem>();
        static List<string> listRestritos = new List<string>();// classe pra cadastrar
        static List<string> listBloqueados = new List<string>();// classe pra cadastrar
        static List<string> listDestino = new List<string>();// iata-classe pra cadastrar 
        #endregion

        #region Atualizar Passagens e Voos
        static void Atualizar()
        {
            try
            {
                //Exclui voos já realizados
                for (int j = 0; j < listVoo.Count; j++)
                {
                    if (DateTime.Compare(listVoo[j].DataVoo, System.DateTime.Now) < 0)
                    {
                        for (int i = 0; i < listPassagem.Count; i++)
                        {
                            if (listPassagem[i].IDVoo == listVoo[j].IDVoo)
                            {
                                if (listPassagem.Count > 1)
                                {
                                    Passagem aux = listPassagem[i + 1];
                                    listPassagem.Remove(listPassagem[i]);
                                    listPassagem[i] = aux;
                                    i--;
                                }
                                else
                                {
                                    listPassagem.Remove(listPassagem[i]);
                                }
                            }
                        }

                        voosrealizados.Add(listVoo[j].DadosVooRealizado());
                      //  GravarVooRealizado(); insert

                        if (listVoo.Count > 1)
                        {
                            Voo auxi = listVoo[j + 1];
                            listVoo.Remove(listVoo[j]);
                            listVoo[j] = auxi;
                            j--;
                        }
                        else
                        {
                            listVoo.Remove(listVoo[j]);
                        }
                    }
                }

                //Altera as passagens reservadas para livres em 2 dias após a compra
                foreach (var passagem in listPassagem)
                {
                    if (passagem.Situacao == 'R' && DateTime.Compare(passagem.DataUltimaOperacao.AddDays(2), System.DateTime.Now) < 0)
                    {
                        passagem.Situacao = 'L';
                    }
                }
              //  GravarVoo(); insert
              //  GravarPassagem(); insert
            }
            catch (Exception)
            {
                Console.WriteLine("ERRO de arquivo! Não foi possível atualizar os voos e passagens!");
                Pausa();
            }
        }
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

        #region GerarIDPASSAGEM
        static List<string> GeradorIdPassagens(int capacidadeassentos)
        {
            try
            {
                Random random = new Random();
                List<string> listaId = new List<string>();
                string id;
                bool encontrado;

                for (int i = 0; i < capacidadeassentos; i++)
                {
                    encontrado = false;

                    id = random.Next(1001, 9999).ToString();

                    foreach (var idnalista in listaId)
                    {
                        if (idnalista == id)
                        {
                            encontrado = true;
                            i--;
                            break;
                        }
                    }
                    if (encontrado == false)
                        listaId.Add("PA" + id.ToString());
                }
                return listaId;
            }
            catch (Exception)
            {
                Console.WriteLine("Erro, não foi possível gerar ID's das passagens!");
                Pausa();
                return null;
            }
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

                            foreach (var voo in listVoo)
                            {
                                if (voo.IDVoo == idvoogerado)
                                {
                                    encontrado = true;
                                    break;
                                }
                            }

                            if (listVoo.Count <= 8999)
                            {
                                if (encontrado == false)
                                    return idvoogerado;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Erro, não foi possível gerar ID do Voo! Lista de vendas está cheia");
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

                    try
                    {
                        string idvenda = (listVenda.Count + 1).ToString();
                        if (idvenda.Length == 1) return ("0000" + idvenda);
                        else if (idvenda.Length == 2) return ("000" + idvenda);
                        else if (idvenda.Length == 3) return ("00" + idvenda);
                        else if (idvenda.Length == 4) return ("0" + idvenda);
                        else if (idvenda.Length == 5) return idvenda;
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Erro, não foi possível gerar ID da venda! Lista de vendas está cheia");
                            Pausa();
                            return null;
                        }
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Erro, não foi possível gerar ID da venda!");
                        Pausa();
                        return null;
                    }

                #endregion


                case "iditemvenda":

                    #region IDItemVenda
                    try
                    {
                        string idvenda = (listVenda.Count + 1).ToString();
                        if (idvenda.Length == 1) return ("0000" + idvenda);
                        else if (idvenda.Length == 2) return ("000" + idvenda);
                        else if (idvenda.Length == 3) return ("00" + idvenda);
                        else if (idvenda.Length == 4) return ("0" + idvenda);
                        else if (idvenda.Length == 5) return idvenda;
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Erro, não foi possível gerar ID do Item Venda! Lista está cheia");
                            Pausa();
                            return null;
                        }
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Erro, não foi possível gerar ID do Item Venda!");
                        Pausa();
                        return null;
                    }

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
                                                //Se digitos validados, procura na lista de cadastro se já existe o cpf cadastrado:
                                                encontrado = false;
                                                foreach (var passageiro in listPassageiro)
                                                {
                                                    //Se achar na lista não deixa prosseguir
                                                    if (passageiro.Cpf == cpf)
                                                    {
                                                        //Se encontrar na lista, invalida o cadastro
                                                        encontrado = true;
                                                        Console.WriteLine("CPF já cadastrado!");
                                                        retornar = PausaMensagem();
                                                        break; //Quando encontrar um cpf igual na lista, quebra o foreach
                                                    }
                                                    else
                                                        encontrado = false; //Mantem encontrado como false enquanto não achar na lista
                                                }

                                                //Ao fim da procura, se não possuir o cpf na lista, encontrado = false e retorna o cpf cadastrado:
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
                                                foreach (var companhia in listCompanhia)
                                                {
                                                    //Se achar na lista não deixa prosseguir
                                                    if (companhia.Cnpj == cnpj)
                                                    {
                                                        //Se encontrar na lista, invalida o cadastro
                                                        encontrado = true;
                                                        Console.WriteLine("CNPJ já cadastrado!");
                                                        retornar = PausaMensagem();
                                                        break; //Quando encontrar um cnpj igual na lista, quebra o foreach
                                                    }
                                                    else
                                                        encontrado = false; //Mantem encontrado como false enquanto não achar na lista
                                                }

                                                //Ao fim da procura, se não possuir o cnpj na lista, encontrado = false e retorna o cnpj cadastrado:
                                                if (encontrado == false)
                                                    //////////RETORNA O CNPJ
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
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nO Aeroporto não aceita cadastrar companhia aérea com menos de 6 meses de existência.");
                                        Console.ForegroundColor = ConsoleColor.White;
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
                                                foreach (var aeronave in listAeronaves)
                                                {
                                                    if (aeronave.Inscricao == idaeronave)
                                                    {
                                                        encontrado = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        encontrado = false;
                                                    }
                                                }

                                                if (encontrado == false)
                                                {
                                                    return idaeronave;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Essa Aeronave já possui cadastro!");
                                                    retornar = PausaMensagem();
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

                        Console.Write("Informe o código IATA do aeroporto de destino: ");
                        try
                        {
                            string iata = Console.ReadLine().ToUpper();

                            if (listDestino.Contains(iata))
                            {
                                return iata;
                            }
                            else
                            {
                                Console.WriteLine("Código não encontrado! Insira um código IATA válido!");
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
                    encontrado = false;
                    do
                    {
                        retornar = false;
                        Aeronave a = null;

                        Console.Write("Informe o código Nacional de identificação da Aeronave: ");
                        try
                        {
                            idaeronave = Console.ReadLine().ToUpper();

                            foreach (var aeronave in listAeronaves)
                            {
                                if (aeronave.Inscricao == idaeronave)
                                {
                                    a = aeronave;
                                    encontrado = true;
                                    break;
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }
                            if (encontrado == true)
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

                    #region AeronaveEditar
                    encontrado = false;
                    do
                    {
                        retornar = false;
                        Aeronave a = null;

                        Console.Write("Informe o código Nacional de identificação da Aeronave: ");
                        try
                        {
                            idaeronave = Console.ReadLine().ToUpper();

                            foreach (var aeronave in listAeronaves)
                            {
                                if (aeronave.Inscricao == idaeronave)
                                {
                                    a = aeronave;
                                    encontrado = true;
                                    break;
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }
                            if (encontrado == true)
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

                case "cpflogin":

                    #region CpflLogin

                    do
                    {
                        retornar = false;

                        Console.Write("Informe o CPF para prosseguir: ");
                        try
                        {
                            string cpf = Console.ReadLine();

                            //Retira o passageiro escolhido da lista de passageiros:
                            Passageiro p = null;
                            encontrado = false;
                            foreach (var passageiro in listPassageiro)
                            {
                                if (passageiro.Cpf == cpf)
                                {
                                    p = passageiro;
                                    encontrado = true;
                                    break;
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }
                            if (encontrado == true)
                            {
                                //Se situação estivar ativa, continua
                                if (p.Situacao == 'A')
                                {
                                    //Verifica se é maior de idade:
                                    if (DateTime.Compare(p.DataNascimento, System.DateTime.Now.AddYears(-18)) <= 0)
                                    {
                                        encontrado = false;
                                        //Procura na lista de restritos:
                                        foreach (var restrito in listRestritos)
                                        {
                                            if (p.Cpf == restrito)
                                            {
                                                encontrado = true;
                                                break;
                                            }
                                            else
                                            {
                                                encontrado = false;
                                            }
                                        }
                                        //Se não estiver na lista de restritos, retorna o cpf.
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

                            //Consulta o cpf do passageiro escolhido da lista de passageiros:
                            encontrado = false;
                            foreach (var passageiro in listPassageiro)
                            {
                                if (passageiro.Cpf == cpf)
                                {
                                    encontrado = true;
                                    break;
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }
                            if (encontrado == true)
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

                    #region IDVoo

                    do
                    {
                        retornar = false;

                        Console.Write("Informe o ID do Voo para prosseguir: ");
                        try
                        {
                            string idvoo = Console.ReadLine().ToUpper();

                            //Consulta o id do voo escolhido da lista de voos:
                            encontrado = false;
                            foreach (var voo in listVoo)
                            {
                                if (voo.IDVoo == idvoo)
                                {
                                    encontrado = true;
                                    break;
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }

                            if (encontrado == true)
                            {
                                return idvoo;
                            }
                            else
                            {
                                Console.WriteLine("ID do Voo não encontrado! Insira um código válido!");
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

                    #region idvenda

                    do
                    {
                        encontrado = false;
                        retornar = false;

                        Console.Write("Informe o ID da venda: ");
                        try
                        {
                            string idvenda = Console.ReadLine().ToUpper();

                            foreach (var venda in listVenda)
                            {
                                if (venda.IDVenda == idvenda)
                                {
                                    encontrado = true;
                                    break;
                                }
                                else
                                {
                                    encontrado = false;
                                }
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

                    #region cnpjogin
                    do
                    {
                        retornar = false;
                        encontrado = false;
                        CompanhiaAerea c = null;
                        Console.Write("Informe o CNPJ para prosseguir: ");
                        try
                        {
                            string cnpj = Console.ReadLine();

                            foreach (var companhia in listCompanhia)
                            {
                                if (companhia.Cnpj == cnpj)
                                {
                                    c = companhia;
                                    encontrado = true;
                                    break;
                                }
                                else
                                {
                                    encontrado = false;
                                }
                            }

                            if (encontrado == true)
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
                #endregion

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
                Console.WriteLine(" 1 - Companhia Aérea\n");
                Console.WriteLine(" 2 - Passageiro\n");
                Console.WriteLine(" 3 - Compras de Passagens\n");
                Console.WriteLine(" 4 - Acesso a Lista de CPF Restritos\n");
                Console.WriteLine(" 5 - Acesso a Lista de CNPJ Restritos\n");
                Console.WriteLine(" 6 - Aeronaves\n");
                Console.WriteLine(" 7 - Voos Realizados\n");
                Console.WriteLine(" 8 - Voos Disponíveis");
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
                        //TelaInicialPassageiros();
                        break;
                    case 3:
                       // TelaVendas();
                        break;
                    case 4:
                      //  TelaInicialCpfRestritos();
                        break;
                    case 5:
                     //   TelaInicialCnpjBloqueados();
                        break;
                    case 6:
                    //    TelaVerAeronavesCadastradas();
                        break;
                    case 7:
                        foreach (var voorealizado in voosrealizados)
                        {
                            Console.WriteLine(voorealizado);
                        }
                        Pausa();
                        break;
                    case 8:
                        foreach (var Voo in listVoo)
                        {
                            if (Voo.Situacao == 'A')
                            {
                                Console.WriteLine("IDVoo: " + Voo.IDVoo + " Destino: " + Voo.Destino + " Data e Hora do Voo: " + Voo.DataVoo.ToString("dd/MM/yyyy HH:mm"));
                            }
                        }
                        Pausa();
                        break;
                }

            } while (opc != 0);
        }
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
                Console.WriteLine("\n 0 - SAIR\n");
                opc = int.Parse(ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:
                        TelaInicial();
                        break;

                    case 1:
                        TelaLoginCompanhiaAerea();
                        break;

                    case 2:
                        TelaCadastrarCompanhiaAerea();
                        break;
                }

            } while (opc != 0);
        }
        #endregion

        #region Companhia Aérea funções
        static void TelaLoginCompanhiaAerea()
        {
            string cnpj;
            CompanhiaAerea compAtivo;
            Console.Clear();
            Console.WriteLine("\nInforme o 'CNPJ' para Entrar\n");
            Pausa();
            cnpj = ValidarEntrada("cnpjexiste");
            if (cnpj == null) TelaInicialCompanhiasAereas();

            foreach (CompanhiaAerea companhia in listCompanhia)
            {
                if (companhia.Cnpj == cnpj)
                {
                    compAtivo = companhia;
                    //TelaOpcoesCompanhiaAerea(compAtivo);
                }
            }

        }

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
            listCompanhia.Add(novaComp);
            //GravarCompanhiaAerea();
            bd.InsertCompanhiaAerea(novaComp);
            TelaInicialCompanhiasAereas();


        }

        #endregion
        static void Main(string[] args)
        {
            TelaInicial();
        }

    }
}



