using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models
{
    public class Sentence
    {
        public List<Symbol> Symbols { get; set; }

        public Sentence()
        {
            Symbols = new List<Symbol>();
        }
    }
}
