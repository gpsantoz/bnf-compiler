using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analisador.Models;
using Analisador.Models.LR_Parser;
using Analisador.Models.SLR_Parser;
using ActionType = Analisador.Models.LR_Parser.ActionType;

namespace Analisador.Methods.Tables
{
    public class LRParser
    {
        //Colunas: Terminais
        //Linhas: Não terminais
        Semantic _semantic;
        Syntactic _syntactic;
        public bool Generated { get; set; }
        public bool IsLR { get; set; }
        public TableParser _tableParser { get; set; }
        private List<LRState> LRstates;

        public LRParser(Semantic semantic, Syntactic syntactic)
        {
            Generated = false;
            IsLR = true;
            _tableParser = new TableParser();
            _semantic = semantic;
            _syntactic = syntactic;
            this.LRstates = new List<LRState>();
        }

        public void GenerateTable(DataGridView gridView)
        {
            int NextStateNumber = 0; //Número do próximo estado
            List<LRState> states = new List<LRState>();  //Estados
            var productions = _syntactic.GetSingleProductions(); //Pega todas as produções
            var firstProduction = productions.First();

            //Inicio com o primeiro estado (0)
            var firstState = new LRState();
            firstState.Number = NextStateNumber;
            NextStateNumber++;

            //Adiciona a primeira produção (Slinha)
            var firstStateProduction = new LRStateProduction();
            firstStateProduction.LocalFolow.Add(new Symbol() { Type = SymbolType.EndOfFile, Value = "$" });
            firstStateProduction.Production = firstProduction;

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
                                    if (!(gp.ToList()[i].Production.Equals(item.ToList()[i].Production)) ||!(gp.ToList()[i].Marked.Index == item.ToList()[i].Marked.Index))
                                    {
                                        flag = false;
                                    }else
                                    {
                                        if (gp.ToList()[i].LocalFolow.ToList().Count == item.ToList()[i].LocalFolow.Count())
                                        {
                                            //SÃO DO MESMO TAMANHO, POSSO VERIFICAR SE SÃO IGUAIS
                                            for (int j = 0; j < gp.ToList()[i].LocalFolow.Count; j++)
                                            {
                                                if (!(gp.ToList()[i].LocalFolow[j].Value.Equals(item.ToList()[i].LocalFolow[j].Value)))
                                                {
                                                    flag = false;
                                                }
                                            }
                                        }
                                        else 
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
                        var newState = new LRState();
                        newState.Number = NextStateNumber;

                        foreach (var VARIABLE in item)
                        {
                            
                             var nextStateNewProduction =  new LRStateProduction()
                            {
                                Production = VARIABLE.Production,
                                Marked = new MarkedSymbol()
                                {
                                    Index = VARIABLE.Marked.Index,
                                    Symbol = new Symbol() { Type = VARIABLE.Marked.Symbol.Type, Value = VARIABLE.Marked.Symbol.Value }
                                },
                                Checked = false
                            };
                            nextStateNewProduction.LocalFolow.AddRange(VARIABLE.LocalFolow);
                            newState.Productions.Add(nextStateNewProduction);
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
            this.LRstates.AddRange(states.ToList());
        }

        public List<LRState> GetLrStates()
        {
            return this.LRstates;
        }

        public void CreateTableMatches(List<LRState> states)
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
                        foreach (var f in VARIABLE.LocalFolow)
                        {
                            if (f.Type != SymbolType.Empty)
                            {
                                var match = new TableMatch();
                                match.StateNumber = s.Number;
                                match.CellValue = " r" + VARIABLE.ActionNumber;
                                match.SymbolValue = f.Value;
                                _tableParser.Matches.Add(match);
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
                                IsLR = false;
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

        public List<LRStateProduction> IsThereASimilarTransition(List<LRStateProduction> productions, LRState s, LRState actualState)
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

        public void Closure(LRStateProduction p, ref LRState actualState, List<SingleProduction> productions)
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
            }
            else
            {
                p.Marked.Symbol = p.Production.Produced.ElementAt(p.Marked.Index);
                p.Action = ActionType.State;
                //ANDEI COM O FECHO, VERIFICO SE É UM NÃO TERMINAL
                var s = p.Marked.Symbol;
                if (s.Type == SymbolType.NonTerminal)
                {
                    //PEGO AS PRODUÇÕES DESSE NÃO TERMINAL
                    var nonTerminalProductions = productions.Where(t => t.Producer.Value == s.Value);
                    //DECIDO QUAL VAI SER O FOLLOW

                    var localFollow = new List<Symbol>();
                    if (p.Marked.Index + 1 < p.Production.Produced.Count)
                    {
                        //Pego o first desse simbolo
                        var nextSymbol = p.Production.Produced.ElementAt(p.Marked.Index + 1);
                        if (nextSymbol.Type == SymbolType.Terminal)
                        {
                            localFollow.Add(new Symbol() {Type = nextSymbol.Type, Value = nextSymbol.Value});
                        }
                        if (nextSymbol.Type == SymbolType.NonTerminal)
                        {
                            localFollow.AddRange(_semantic.GetFirstList()
                                .Find(e => e.NonTerminal.Value == nextSymbol.Value).Terminals);
                        }
                    }
                    else
                    {
                        //Follow local é igual ao follow atual
                        localFollow.AddRange(p.LocalFolow);
                    }

                    //Já tenho o Follow local, verifico se preciso atualizar o follow de alguém
                    if (actualState.ClosureSymbols.Exists(i => i.Value == s.Value))
                    {
                        var actualStateNumber = actualState.Number;
                        var pendingUpdateProductions =
                            actualState.Productions.Where(r =>
                                r.Production.Producer.Value == s.Value && r.GeneratorStateNumber == actualStateNumber);
                        foreach (var lrp in pendingUpdateProductions)
                        {
                            foreach (var followSymbol in localFollow)
                            {
                                if (!lrp.LocalFolow.Exists(e => e.Value == followSymbol.Value))
                                {
                                    lrp.LocalFolow.Add(followSymbol);
                                }
                            }
                        }
                    }
                    else
                    {
                        //CRIEI UM NOVO LRStateProduction para o estado, que é igual à production desse não terminal
                        foreach (var VARIABLE in nonTerminalProductions)
                        {
                            LRStateProduction sp = new LRStateProduction();
                            sp.Production = VARIABLE;
                            sp.LocalFolow.AddRange(localFollow);
                            sp.GeneratorStateNumber = actualState.Number;
                            actualState.Productions.Add(sp);
                        }
                        //SALVA QUE JÁ GEROU O FECHO DESSE NÃO TERMINAL
                        actualState.ClosureSymbols.Add(s);
                    }
                }
            }
            p.Checked = true;
            //MARCO QUE TERMINEI O FECHO DESSE 
        }

        public bool IsThereASimilarTransition(List<LRStateProduction> productions, List<LRState> states)
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

       
    }
}
