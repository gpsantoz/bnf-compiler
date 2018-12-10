using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models.Semantic
{
    public class SentenceFirst
    {
        public Symbol NonTerminal { get; set; }
        public List<Symbol> Terminals { get; set; }
        public int SentenceNumber;

        public SentenceFirst()
        {
            Terminals = new List<Symbol>();
        }
    }
}
