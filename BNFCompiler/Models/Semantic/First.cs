using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models
{
    public class First
    {
        public Symbol NonTerminal { get; set; }
        public List<Symbol> Terminals { get; set; }
        public bool Type { get; set; }
        public bool Changed { get; set; }

        public First()
        {
            Changed = false;
            Terminals = new List<Symbol>();
        }
    }
}
