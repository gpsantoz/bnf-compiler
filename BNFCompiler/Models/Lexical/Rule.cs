using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Analisador.Models.Symbol;

namespace Analisador.Models
{
    public class Rule
    {
        public string Expression { get; set; }
        public SymbolType Type { get; set; }
    }
}
