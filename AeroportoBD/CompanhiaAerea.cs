using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class CompanhiaAerea
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataUltimoVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public CompanhiaAerea()
        {

        }
        public CompanhiaAerea(string cnpj, string razaoSocial, DateTime DataAbertura, DateTime UltimoVoo, DateTime DataCadastro, char Situacao)
        {
            this.Cnpj = cnpj;
            this.RazaoSocial = razaoSocial;
            this.DataAbertura = DataAbertura;
            this.DataUltimoVoo = System.DateTime.Now;
            this.DataCadastro = System.DateTime.Now;
            this.Situacao = Situacao; //Ativo,Inativo
        }
        public override string ToString()
        {
            return "\nDADOS COMPANHIA AÉREA: \nRazão Social: " + RazaoSocial + "\nCNPJ: " + Cnpj + "\nData Abertura: " + DataAbertura.ToString("dd/MM/yyyy") + "\nData Cadastro: " + DataCadastro.ToString("dd/MM/yyyy HH:mm") + "\nÚltimo Voo: " + DataUltimoVoo.ToString("dd/MM/yyyy HH:mm") + "\nSituação: " + Situacao;
        }
      /*  public string ObterDados() //ARQUIVO TEXTO
        {
            return Cnpj + RazaoSocial + DataAbertura.ToString("ddMMyyyy") + UltimoVoo.ToString("ddMMyyyyHHmm") + DataCadastro.ToString("ddMMyyyyHHmm") + Situacao;
        }*/
    }
}
