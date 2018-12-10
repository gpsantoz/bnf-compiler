using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analisador.Models;
using Analisador.Models.LL_Analyser_Models;

namespace Analisador.Methods.Tables
{
    public class TableParser
    {
        public List<TableMatch> Matches { get; set; }
        public int RowsNumber { get; set; }
        public int ColumnsNumber { get; set; }
        public List<Symbol> Symbols { get; set; }

        public TableParser()
        {
            Matches = new List<TableMatch>();
            Symbols = new List<Symbol>();
        }

    }

}
