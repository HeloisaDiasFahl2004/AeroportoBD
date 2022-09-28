using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportoBD
{
    internal class Aeroporto
    {
        public string IATA { get; set; }
        public Aeroporto(string iata)
        {
            this.IATA = iata;
        }
    }
}
