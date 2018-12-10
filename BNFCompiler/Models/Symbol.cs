using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models
{
    public class Symbol
    {
        public string Value { get; set; }
        public SymbolType Type { get; set; }
    }

    public enum SymbolType
    {
        NonTerminal = 0,
        Terminal = 1,
        Production = 2,
        Pipe = 3,
        Empty = 4,
        EndOfFile = 5
    };
}
