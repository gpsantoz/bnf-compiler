using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analisador.Models.SLR_Parser;

namespace Analisador.Models.LR_Parser
{
    public class LRState
    {
        public List<LRStateProduction> Productions { get; set; }
        public int Number { get; set; }
        public bool Done { get; set; }
        public List<Symbol> ClosureSymbols { get; set; }

        public LRState()
        {
            ClosureSymbols = new List<Symbol>();
            Done = false;
            Productions = new List<LRStateProduction>();
        }

        public bool AreProductionsEquals(List<LRStateProduction> singleProductions)
        {
            bool flag = true;
            if (singleProductions.Count == Productions.Count)
            {
                for (int i = 0; i < singleProductions.Count; i++)
                {
                    if (!(singleProductions[i].Production.Equals(Productions[i].Production) && singleProductions[i].LocalFolow.Equals(Productions[i].LocalFolow)))
                    {
                        flag = false;
                    }
                  
                }


            }
            else
            {
                flag = false;
            }
            return flag;
        }
    }
}
