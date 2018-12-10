using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models
{
    public class SingleProduction
    {
        public Symbol Producer { get; set; }
        public LinkedList<Symbol> Produced { get; set; }
        public int Number { get; set; }

        public SingleProduction()
        {
            Number = 0;
            Produced = new LinkedList<Symbol>();
        }
    }
}
