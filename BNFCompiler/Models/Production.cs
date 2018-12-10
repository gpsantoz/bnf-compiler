using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models
{
    public class Production
    {
        public Symbol Producer { get; set; }
        public List<List<Symbol>> Produced { get; set; }

        public Production()
        {
            Produced = new List<List<Symbol>>();

        }
    }
}
