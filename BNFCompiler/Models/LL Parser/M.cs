using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models.LL_Analyser_Models
{
    public class M
    {
        public Symbol NonTerminal { get; set; }
        public Symbol Terminal { get; set; }
        public int State { get; set; }
    }
}
