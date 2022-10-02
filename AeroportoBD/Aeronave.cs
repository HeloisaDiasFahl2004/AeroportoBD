using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class Aeronave
    {
        public string Inscricao { get; set; }
        public int Capacidade { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public string Cnpj { get; set; }

        public Aeronave()
        {

        }
        public Aeronave(string inscricao, int capacidade,  DateTime UltimaVenda, DateTime Cadastro, char situacao, string cnpj)
        {
            this.Inscricao = inscricao;
            this.Capacidade = capacidade;
            this.UltimaVenda = System.DateTime.Now;
            this.DataCadastro = System.DateTime.Now;
            this.Situacao = situacao;
            this.Cnpj = cnpj;
          
        }
        public override string ToString()
        {
            string s;
            if (Situacao == 'A') s = "ATIVA";
            else s = "INATIVA";

            return "\nDADOS AERONAVE: \nInscrição: " + Inscricao + "\nCapacidade: " + Capacidade +  "\nData Cadastro: " + DataCadastro.ToString("dd/MM/yyyy HH:mm") + "\nÚltima Venda: " + UltimaVenda.ToString("dd/MM/yyyy HH:mm") + "\nSituação: " + s + "\nCNPJ: " + Cnpj;
        }
        
    }
}
