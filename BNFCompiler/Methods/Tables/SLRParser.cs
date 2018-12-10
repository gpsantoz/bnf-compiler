using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analisador.Models;
using Analisador.Models.SLR_Parser;

namespace Analisador.Methods.Tables
{
    public class SLRParser
    {
        //Colunas: Terminais
        //Linhas: Não terminais
        Semantic _semantic;
        Syntactic _syntactic;
        public TableParser _tableParser { get; set; }
        public bool Generated { get; set; }
        public bool IsSLR { get; set; }

        public SLRParser(Semantic semantic, Syntactic syntactic)
        {
            _tableParser = new TableParser();
            Generated = false;
            IsSLR = true;
            _semantic = semantic;
            _syntactic = syntactic;
        }

        public void GenerateTable(DataGridView gridView)
        {
            int NextStateNumber = 0; //Número do próximo estado
            List<State> states = new List<State>();  //Estados
            var productions = _syntactic.GetSingleProductions(); //Pega todas as produções
            var firstProduction = productions.First();
            //Inicio com o primeiro estado (0)

            var firstState = new State();
            firstState.Number = NextStateNumber;
            NextStateNumber++;

            //Adiciona a primeira produção (Slinha)
            var firstStateProduction = new StateProduction();
            firstStateProduction.Production = firstProduction;
            //firstStateProduction.Marked.Symbol = firstProduction.Produced.First.Value;
            firstState.Productions.Add(firstStateProduction);
            states.Add(firstState);

            while (states.Exists(e => e.Done == false))
            {
                var actualState = states.First(w => w.Done == false);
                while (actualState.Productions.Exists(u => u.Checked == false))
                {
                    var p = actualState.Productions.First(i => i.Checked == false);
                    if (!p.Checked)
                    {
                        Closure(p, ref actualState, productions);
                    }
                }
                if (!actualState.Productions.Exists(g => g.Checked == false))
                {
                    actualState.Done = true;
                    //TERMINOU O FECHO, CRIA OS PRÓXIMOS ESTADOS
                }

                var grouped = actualState.Productions.Where(e => e.Action == ActionType.State).GroupBy(item => item.Marked.Symbol.Value);

                foreach (var item in grouped)
                {
                    bool similarTransitionFound = false;
                    //VERIFICA SE JÁ EXISTE ALGUMA TRANSIÇÃO SIMILAR
                    // item = transições para o mesmo estado, agrupadas
                    foreach (var VARIABLE in states.Where(e => !e.Equals(actualState)))
                    {
                        var stateGroupedProductions = VARIABLE.Productions.GroupBy(x => new { x.ActionNumber });
                        foreach (var gp in stateGroupedProductions)
                        {
                            if (gp.ToList().Count == item.ToList().Count() && gp.All(a => a.Action == ActionType.State))
                            {
                                bool flag = true;
                                //SÃO DO MESMO TAMANHO, POSSO VERIFICAR SE SÃO IGUAIS
                                for (int i = 0; i < gp.ToList().Count; i++)
                                {
                                    if (actualState.Number == 4)
                                    {

                                    }
                                    if (!(gp.ToList()[i].Production.Equals(item.ToList()[i].Production)) ||
                                        !(gp.ToList()[i].Marked.Index == item.ToList()[i].Marked.Index))
                                    {
                                        flag = false;
                                    }
                                }
                                if (flag)
                                {
                                    similarTransitionFound = true;
                                    foreach (var va in item)
                                    {
                                        va.ActionNumber = gp.First().ActionNumber;
                                    }
                                    break;
                                }
                            }
                        }
                        if (similarTransitionFound)
                            break;
                    }
                    if (!similarTransitionFound)
                    {
                        var newState = new State();
                        newState.Number = NextStateNumber;

                        foreach (var VARIABLE in item)
                        {
                            newState.Productions.Add(new StateProduction()
                            {
                                Production = VARIABLE.Production,
                                Marked = new MarkedSymbol()
                                {
                                    Index = VARIABLE.Marked.Index,
                                    Symbol = new Symbol() { Type = VARIABLE.Marked.Symbol.Type, Value = VARIABLE.Marked.Symbol.Value }
                                },
                                Checked = false
                            });
                        }
                        foreach (var pro in newState.Productions)
                        {
                            pro.Checked = false;
                        }
                        states.Add(newState);
                        foreach (var VARIABLE in item)
                        {
                            actualState.Productions.Find(w => w.Equals(VARIABLE)).ActionNumber = NextStateNumber;
                        }
                        NextStateNumber++;
                    }

                }

            }

            CreateTableMatches(states);
            Generated = true;
            //PrintTable(gridView, states);
        }

        public void CreateTableMatches(List<State> states)
        {
            var nonTerminals = _syntactic.GetNonTerminals();
            var terminals = _syntactic.GetTerminals();
            var symbols = new List<Symbol>();
            foreach (var nt in nonTerminals)
            {
                if (nt.Value != "Slinha")
                {
                    symbols.Add(nt);
                }
            }
            foreach (var t in terminals)
            {
                if (t.Type != SymbolType.Empty)
                {
                    symbols.Add(t);
                }
            }

            //Adiciona símbolo de Fim de Arquivo
            symbols.Add(new Symbol() { Type = SymbolType.EndOfFile, Value = "$" });
            //Colunas: Coluna de descrição, coluna para os Terminais e End of File 

            //Linhas: Não Terminais
            _tableParser.ColumnsNumber = symbols.Count;
            _tableParser.RowsNumber = states.Count();

            _tableParser.Symbols.AddRange(symbols);

            int termColumns = 0;
            int termRows = 0;

            foreach (var s in states)
            {
                //pego as produções desse estado
                foreach (var VARIABLE in s.Productions)
                {
                    if (VARIABLE.Action == ActionType.Reduction)
                    {
                        Follow follow = _semantic.GetFollowList().Find(q => q.NonTerminal.Value == VARIABLE.Production.Producer.Value);
                        foreach (var f in follow.Terminals)
                        {
                            if (f.Type != SymbolType.Empty)
                            {
                                var match = new TableMatch();
                                match.StateNumber = s.Number;
                                match.CellValue = " r" + VARIABLE.ActionNumber;
                                match.SymbolValue = f.Value;
                                if (_tableParser.Matches.Exists(m =>
                                    m.StateNumber == match.StateNumber && m.SymbolValue == match.SymbolValue))
                                {
                                    var existingMatch = _tableParser.Matches.Find(m =>
                                        m.StateNumber == match.StateNumber && m.SymbolValue == match.SymbolValue);
                                    //Cell já foi preenchida, verifica se é o mesmo valor
                                    if (existingMatch.CellValue != match.CellValue)
                                    {
                                        IsSLR = false;
                                    }

                                }
                                else
                                {
                                    _tableParser.Matches.Add(match);
                                }
                            }
                        }

                    }
                    else
                    {
                        var match = new TableMatch();
                        match.StateNumber = s.Number;
                        match.SymbolValue = VARIABLE.Marked.Symbol.Value;
                        match.CellValue = VARIABLE.ActionNumber.ToString();
                        if (_tableParser.Matches.Exists(m =>
                            m.StateNumber == match.StateNumber && m.SymbolValue == match.SymbolValue))
                        {
                            var existingMatch = _tableParser.Matches.Find(m =>
                                m.StateNumber == match.StateNumber && m.SymbolValue == match.SymbolValue);
                            //Cell já foi preenchida, verifica se é o mesmo valor
                            if (existingMatch.CellValue != match.CellValue)
                            {
                                IsSLR = false;
                            }

                        }
                        else
                        {
                            _tableParser.Matches.Add(match);
                        }
                    }
                }
            }
        }

        public List<StateProduction> IsThereASimilarTransition(List<StateProduction> productions, State s, State actualState)
        {
            bool flag = false;
            var grouped = s.Productions.Where(e => e.Action == ActionType.State && e.ActionNumber != actualState.Number).GroupBy(item => item.ActionNumber);
            foreach (var item in grouped)
            {
                flag = true;
                if (item.ToList().Count == productions.Count)
                {
                    for (int i = 0; i < productions.Count; i++)
                    {
                        if (!productions[i].Production.Equals(item.ToList()[i].Production))
                        {
                            flag = false;
                        }
                    }
                }
                else
                {
                    flag = false;
                }
                if (flag)
                    return item.ToList();
            }
            return null;
        }

        public void Closure(StateProduction p, ref State actualState, List<SingleProduction> productions)
        {
            //VERIFICO SE PRECISO ANDAR COM O FECHO E INCREMENTO O INDEX
            p.Marked.Index++;
            if (p.Marked.Index < p.Production.Produced.Count)
            {
                p.Marked.Symbol = p.Production.Produced.ElementAt(p.Marked.Index);
            }
            //Verifica se existe um simbolo nesse index. Caso não, é uma redução
            if ((p.Marked.Index >= p.Production.Produced.Count) || ((p.Marked.Symbol.Type == SymbolType.Empty) && (p.Marked.Index + 1 == p.Production.Produced.Count)))
            {
                p.Action = ActionType.Reduction;
                p.ActionNumber = p.Production.Number;
                //TODO: Colocar o número da redução
            }
            else
            {
                p.Marked.Symbol = p.Production.Produced.ElementAt(p.Marked.Index);
                p.Action = ActionType.State;
                //ANDEI COM O FECHO, VERIFICO SE É UM NÃO TERMINAL
                var s = p.Marked.Symbol;
                if (s.Type == SymbolType.NonTerminal && !actualState.ClosureSymbols.Exists(i => i.Value == s.Value))
                {
                    //PEGO AS PRODUÇÕES DESSE NÃO TERMINAL
                    var nonTerminalProductions = productions.Where(t => t.Producer.Value == s.Value);
                    //CRIEI UM NOVO StateProduction para o estado, que é igual à production desse não terminal
                    foreach (var VARIABLE in nonTerminalProductions)
                    {
                        StateProduction sp = new StateProduction();
                        sp.Production = VARIABLE;
                        actualState.Productions.Add(sp);
                    }
                    //SALVA QUE JÁ GEROU O FECHO DESSE NÃO TERMINAL
                    actualState.ClosureSymbols.Add(s);
                }
            }
            p.Checked = true;
            //MARCO QUE TERMINEI O FECHO DESSE 
        }

        public bool IsThereASimilarTransition(List<StateProduction> productions, List<State> states)
        {
            bool flag = true;
            foreach (var s in states)
            {
                var grouped = s.Productions.Where(e => e.Action == ActionType.State).GroupBy(item => item.ActionNumber);
                if (grouped.ToList().Count == productions.Count)
                {
                    for (int i = 0; i < productions.Count; i++)
                    {
                        if (!productions[i].Equals(grouped.ToList()[i]))
                        {
                            flag = false;
                        }
                    }
                }
                else
                {
                    flag = false;
                }

            }
            return flag;
        }

        public void PrintTable(DataGridView gridView, List<State> states)
        {
            var nonTerminals = _syntactic.GetNonTerminals();
            var terminals = _syntactic.GetTerminals();
            var followSet = _semantic.GetFollowList();
            var symbols = new List<Symbol>();
            foreach (var nt in nonTerminals)
            {
                if (nt.Value != "Slinha")
                {
                    symbols.Add(nt);
                }
            }
            foreach (var t in terminals)
            {
                if (t.Type != SymbolType.Empty)
                {
                    symbols.Add(t);
                }
            }

            //Adiciona símbolo de Fim de Arquivo
            symbols.Add(new Symbol() { Type = SymbolType.EndOfFile, Value = "$" });
            //Colunas: Coluna de descrição, coluna para os Terminais e End of File 


            //Linhas: Não Terminais
            int Ncolumns = symbols.Count;
            int Nrows = states.Count();

            string[][] matrix = new string[Nrows][];
            for (int a = 0; a < Nrows; a++)
            {
                matrix[a] = new string[Ncolumns];
            }
            int termColumns = 0;
            int termRows = 0;

            gridView.ColumnCount = Ncolumns;
            gridView.RowCount = Nrows;
            gridView.Columns[0].Width = 40;
            foreach (var sym in symbols)
            {
                gridView.Columns[termColumns].Name = sym.Value;
                gridView.Columns[termColumns].Width = 40;
                termColumns++;
            }
            foreach (var s in states)
            {
                //agora crio uma linha pra cada simbolo nao terminal
                DataGridViewRow row = (DataGridViewRow)gridView.Rows[0].Clone();
                row.HeaderCell.Value = s.Number;

                //pego as produções desse estado
                foreach (var VARIABLE in s.Productions)
                {
                    if (VARIABLE.Action == ActionType.Reduction)
                    {
                        //TODO: Pegar o follow do Producer
                        Follow follow = followSet.Find(q => q.NonTerminal.Value == VARIABLE.Production.Producer.Value);

                        foreach (var f in follow.Terminals)
                        {
                            if (f.Type != SymbolType.Empty)
                            {
                                gridView.Rows[s.Number].Cells[gridView.Columns[f.Value].Index].Value += " r" + VARIABLE.ActionNumber;
                            }
                        }

                    }
                    else
                    {
                        if (gridView.Rows[s.Number].Cells[gridView.Columns[VARIABLE.Marked.Symbol.Value].Index].Value ==
                            null)
                            gridView.Rows[s.Number].Cells[gridView.Columns[VARIABLE.Marked.Symbol.Value].Index].Value =
                                VARIABLE.ActionNumber;
                        else
                        {
                            //TODO: ja foi preenchido, mostrar erro
                        }
                    }
                    //row.Cells[gridView.Columns[VARIABLE.Terminal.Value].Index].Value = VARIABLE.State;
                }
                //gridView.Rows[termRows] = row;
                gridView.Rows[s.Number].HeaderCell.Value = s.Number.ToString();
            }
        }
    }
}
