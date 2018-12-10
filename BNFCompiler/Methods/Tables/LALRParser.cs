using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analisador.Models;
using Analisador.Models.LR_Parser;

namespace Analisador.Methods.Tables
{
    public class LALRParser
    {
        public TableParser _tableParser { get; set; }
        public bool Generated { get; set; }
        public bool IsLALR { get; set; }
        Semantic _semantic;
        Syntactic _syntactic;
        private List<LRState> states;

        public LALRParser(Semantic semantic, Syntactic syntactic)
        {
            Generated = false;
            IsLALR = true;
            _semantic = semantic;
            _syntactic = syntactic;
            _tableParser = new TableParser();
            states = new List<LRState>();
        }

        public void GenerateTable(List<LRState> LRstates)
        {
            var states = LRstates.ToList();
            var groupedStates = new List<LRState>();
            var statesNumbers = new List<int>();
            var nextStateNumber = 0;
            foreach (var variable in states)
            {
                if (!statesNumbers.Exists(w => w.Equals(variable.Number)))
                {
                    var similarStates = new List<LRState>();
                    similarStates.Add(variable);
                    if (variable.Number == 6)
                    {
                        
                    }
                    foreach (var state in LRstates.ToList().Where(e => e.Number != variable.Number))
                    {
                        bool equalFlag = true;
                        if (state.Productions.Count() == variable.Productions.Count())
                        {

                            for (int i = 0; i < variable.Productions.Count; i++)
                            {
                                if (variable.Productions[i].Production.Equals(state.Productions[i].Production) &&
                                    variable.Productions[i].Marked.Index == state.Productions[i].Marked.Index)
                                {
                                    
                                }
                                else
                                {
                                    equalFlag = false;
                                }
                            }
                        }
                        else
                        {
                            equalFlag = false;
                        }
                        if(equalFlag)
                            similarStates.Add(state);
                    }
                    
                    LRState newState = similarStates.ToList().First();
                   

                    foreach (var oldState in similarStates) {
                        foreach (var s in states.Where(w =>
                            w.Productions.Exists(q => q.Action == ActionType.State && q.ActionNumber == oldState.Number)))
                        {
                            foreach (var p in s.Productions.Where(q => q.Action == ActionType.State && q.ActionNumber == oldState.Number))
                            {
                                p.ActionNumber = nextStateNumber;
                            }
                        }
                    }

                    foreach (var oldState in similarStates.Where(y => y.Number != newState.Number))
                    {
                        statesNumbers.Add(oldState.Number);
                       
                        //Atualizar o Follow Local
                        var localFollowSize = oldState.Productions.Count;
                        for (int j = 0; j < localFollowSize; j ++)
                        {
                            foreach (var lf in oldState.Productions[j].LocalFolow)
                            {
                                if (!newState.Productions[j].LocalFolow.Exists(r => r.Value == lf.Value))
                                {
                                    newState.Productions[j].LocalFolow.Add(lf);
                                }
                            }
                        }
                    }
                    newState.Number = nextStateNumber;
                    statesNumbers.Add(newState.Number);
                    nextStateNumber++;
                    groupedStates.Add(newState); 
                }
            }
            Generated = true;
            CreateTableMatches(groupedStates);
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
                                IsLALR = false;
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

    }
}
