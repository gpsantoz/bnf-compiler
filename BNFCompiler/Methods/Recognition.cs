using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analisador.Methods.Tables;
using Analisador.Models;

namespace Analisador.Methods
{
    public class Recognition
    {

        public bool Recognize(TableParser _parser)
        {
            Stack<int> States = new Stack<int>();
            Stack<Symbol> Symbols = new Stack<Symbol>();
            Stack<Symbol> String = new Stack<Symbol>();
            Stack<Symbol> Action = new Stack<Symbol>();


            return true;
        } 
    }
}
