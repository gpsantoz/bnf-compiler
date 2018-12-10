using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analisador.Models.SLR_Parser;

namespace Analisador.Models.LR_Parser
{
    public class LRStateProduction
    {
        public SingleProduction Production { get; set; }
        public MarkedSymbol Marked { get; set; }
        public ActionType Action { get; set; }
        public int ActionNumber { get; set; }
        public bool Checked { get; set; }
        public List<Symbol> LocalFolow { get; set; }
        public int GeneratorStateNumber { get; set; }

        public LRStateProduction()
        {
            LocalFolow = new List<Symbol>();
            Checked = false;
            this.Marked = new MarkedSymbol();
            this.Marked.Index = -1;
        }
    }


    public enum ActionType
    {
        State = 0,
        Reduction = 1
    };
}
