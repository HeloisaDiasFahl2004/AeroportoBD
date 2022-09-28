using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class Restrito
    {
        public string Cpf { get; set; }
        public Restrito(string cpf)
        {
            this.Cpf = cpf;
        }
    }
}
