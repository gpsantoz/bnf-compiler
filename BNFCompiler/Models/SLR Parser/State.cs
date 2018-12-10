using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Models.SLR_Parser
{
    public class State
    {
        public List<StateProduction> Productions { get; set; }
        public int Number { get; set; }
        public bool Done { get; set; }
        public List<Symbol> ClosureSymbols { get; set; }

        public State()
        {
            ClosureSymbols = new List<Symbol>();
            Done = false;
            Productions = new List<StateProduction>();
        }

        public bool AreProductionsEquals(List<StateProduction> singleProductions)
        {
            bool flag = true;
            if (singleProductions.Count == Productions.Count)
            {
                for (int i = 0; i < singleProductions.Count; i++)
                {
                    if (!singleProductions[i].Production.Equals(Productions[i].Production))
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
