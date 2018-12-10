using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models.SLR_Parser
{
    public class StateProduction
    {
        public SingleProduction Production { get; set; }
        public MarkedSymbol Marked { get; set; }
        public ActionType Action { get; set; }
        public int ActionNumber { get; set; }
        public bool Checked { get; set; }

        public StateProduction()
        {
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
