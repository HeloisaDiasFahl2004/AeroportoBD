using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class Bloqueado
    {
        public string Cnpj { get; set; }
        public Bloqueado(string cnpj)
        {
            Cnpj = cnpj;
        }
    }
}
