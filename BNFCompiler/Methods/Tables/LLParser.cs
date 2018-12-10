using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analisador.Models;
using Analisador.Models.LL_Analyser_Models;

namespace Analisador.Methods
{
    public class LLParser
    {
        //Colunas: Terminais
        //Linhas: Não terminais
        Semantic _semantic;
        Syntactic _syntactic;
        public bool Generated { get; set; }

        public bool IsLL { get; set; }

        public LLParser(Semantic semantic, Syntactic syntactic)
        {
            Generated = false;
            _semantic = semantic;
            IsLL = true;
            _syntactic = syntactic;
        }

        public void GenerateTable(DataGridView gridView)
        {
            var terminals = _syntactic.GetTerminals();
            var nonTerminals = _syntactic.GetNonTerminals();
            if(nonTerminals.Exists(w => w.Value == "Slinha"))
            {
                nonTerminals.Remove(nonTerminals.Find(a => a.Value == "Slinha"));
            }
            var singleProductions = _syntactic.GetSingleProductions();
            if (singleProductions.Exists(v => v.Producer.Value == "Slinha"))
            {
                singleProductions.Remove(singleProductions.Find(u => u.Producer.Value == "Slinha"));
            }

            List<M> matches = new List<M>();
            foreach(var p in singleProductions)
            {
                //Verificação do First dessa sentença
                var first = _semantic.GetSentenceFirstList().Find(w => w.NonTerminal.Value == p.Producer.Value && w.SentenceNumber == p.Number);
                foreach (var VARIABLE in first.Terminals)
                {
                    if (VARIABLE.Type == SymbolType.Terminal)
                    {
                        M m = new M();
                        m.State = p.Number;
                        m.NonTerminal = p.Producer;
                        m.Terminal = VARIABLE;
                        matches.Add(m);
                    }
                    else if (VARIABLE.Type == SymbolType.Empty)
                    {
                        //Pega o follow do producer
                        var follow = _semantic.GetFollowList().Find(e => e.NonTerminal.Value == p.Producer.Value);
                        //TODO: Verificar se já não existe um M para  esse Non Terminal x Terminal
                        foreach (var nt in follow.Terminals)
                        {
                            if (nt.Type != SymbolType.Empty)
                            {

                                M m = new M();
                                m.State = p.Number;
                                m.NonTerminal = p.Producer;
                                m.Terminal = nt;
                                matches.Add(m);
                            }
                        }
                    }
                }
            }

            //Adiciona símbolo de Fim de Arquivo
            terminals.Add(new Symbol(){ Type = SymbolType.EndOfFile, Value = "$"});
            //Colunas: Coluna de descrição, coluna para os Terminais e End of File 
            //Linhas: Não Terminais
            int Ncolumns = terminals.Count;
            int Nrows = nonTerminals.Count;

            string[][] matrix = new string[Nrows][];
            for (int a = 0; a < Nrows; a++)
            {
                matrix[a] = new string[Ncolumns];
            }
            int termColumns = 0;
            int termRows = 0;

            gridView.ColumnCount = Ncolumns;
            gridView.RowCount = Nrows;
            gridView.Columns[0].Width =40;
            foreach (var ter in terminals)
            {
                gridView.Columns[termColumns].Name = ter.Value;
                gridView.Columns[termColumns].Width = 40;
                termColumns++;
            }
            // string pattern = @"(?i<"")[^0-9áéíóúàèìòùâêîôûãõç\s]";
            //string replacement = "";
            //Regex rgx = new Regex(pattern);
            foreach (var nTer in nonTerminals)
            {
                //agora crio uma linha pra cada simbolo nao terminal
                DataGridViewRow row = (DataGridViewRow)gridView.Rows[0].Clone();
                row.HeaderCell.Value = nTer.Value;

                //pego os matches desse não terminal
                var nonTerminalsMatches = matches.Where(w => w.NonTerminal.Value == nTer.Value);
                foreach (var VARIABLE in nonTerminalsMatches)
                {
                    gridView.Rows[termRows].Cells[gridView.Columns[VARIABLE.Terminal.Value].Index].Value =
                        VARIABLE.State;
                }
                gridView.Rows[termRows].HeaderCell.Value = nTer.Value;
                termRows++;
            }

        }
    }
}
