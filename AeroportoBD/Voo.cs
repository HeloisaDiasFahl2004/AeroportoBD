using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class Voo
    {
        public string IDVoo { get; set; }
        public string Destino { get; set; } //IATA
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeAssentosOcupados { get; set; }
        public char Situacao { get; set; }
        public Voo(string idVoo, string destino, DateTime dataVoo, DateTime DataCadastro,int quantAO, char Situacao)
        {
            this.IDVoo = idVoo;
            this.Destino = destino;
            this.DataVoo = dataVoo;
            this.DataCadastro = System.DateTime.Now;
            this.QuantidadeAssentosOcupados = quantAO;
            this.Situacao = Situacao; //Ativo,Cancelado
        }
        public override string ToString()
        {
            return "\nDADOS VOO: \nID Voo: " + IDVoo + "\nDestino: " + Destino + "\nData Voo: " + DataVoo.ToString("dd/MM/yyyy HH:mm") + "\nData Cadastro: " + DataCadastro.ToString("dd/MM/yyyy HH:mm") +"\nQuantidade Assentos Ocupados: "+ QuantidadeAssentosOcupados + "\nSituação: " + Situacao;
        }
       /* public string ObterDados()
        {
            return this.IDVoo + Destino + IDAeronave + DataVoo.ToString("ddMMyyyyHHmm") + DataCadastro.ToString("ddMMyyyyHHmm") + Situacao;
        }*/

        public string DadosVooRealizado()
        {
            return "ID Voo:" + IDVoo + "   Destino:" + Destino + "  Data e hora que o Voo foi inciado:" + DataVoo.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
