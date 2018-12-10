using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analisador.Models;

namespace Analisador.Methods
{
    public class Syntactic
    {
        List<Production> productions;
        List<SingleProduction> singleProductions;
        Semantic semantic;
        List<Symbol> NonTerminals;
        List<Symbol> Terminals;
        public string ErrorMessage { get; set; }

        public Syntactic(Semantic Semantic)
        {
            this.semantic = Semantic;
            ErrorMessage = String.Empty;
            productions = new List<Production>();
        }

        public bool VerifySyntactic(List<Sentence> lines)
        {
            //Verifica se a sequencia das senteças está correta: NãoTerminal Produção [Terminal] ou [Pipe] ou [Vazio]
            bool result = CheckProductions(lines);
           
            //Separa os simbolos não terminais para obtenção do First e do Follow
            NonTerminals = new List<Symbol>();
            List<First> NonTerminals_First;
            List<Follow> NonTerminals_Follow;
            //List<Follow> NonTerminals_Follow;
            //Cria um novo estado inicial que produz o primeiro simbolo
            Symbol newState = new Symbol()
            {
                Type = SymbolType.NonTerminal,
                Value = "Slinha",
            };
            foreach (Production prod in productions)
            {
                if (!NonTerminals.Exists(x => x.Value == prod.Producer.Value))
                {
                    NonTerminals.Add(prod.Producer);
                }
                

            }
            //Criação de um novo estado S => Primeiro Simbolo
            Production newProduction = new Production() { Producer = newState };
            var newProduced = new List<Symbol>();
            newProduced.Add(NonTerminals.First());
            newProduction.Produced.Add(newProduced);
            productions.Insert(0, newProduction);
           
            NonTerminals.Insert(0, newState);

            this.SeparateProductions();

            //Preenchendo lista de Terminais
            Terminals = new List<Symbol>();
            foreach(var s in singleProductions)
            {
                foreach(var l in s.Produced)
                {
                    if(l.Type == SymbolType.Terminal && !Terminals.Exists(w => w.Value == l.Value))
                    {
                        Terminals.Add(l);
                    }
                }
            }

            return result;
        }

        private bool CheckProductions(List<Sentence> sent)
        {
            Production production;
            bool error = false, newProduced = false;
            List<Sentence> _sentences = sent.ToList();
            foreach (var sentence in _sentences)
            {
                production = new Production();
                Symbol symbol;
                symbol = sentence.Symbols.First();
                //Simbolo não terminal da esquerda
                if (symbol.Type == SymbolType.NonTerminal)
                {
                    sentence.Symbols.Remove(symbol);
                    production.Producer = symbol;
                }
                else
                {
                    foreach(var s in sentence.Symbols)
                    {
                        ErrorMessage += s.Value;
                    }
                
                    return error; //TODO: Mostrar Erro Sintático
                }
                //Simbolo de produção
                symbol = sentence.Symbols.First();
                if (symbol.Type == SymbolType.Production)
                {
                    sentence.Symbols.Remove(symbol);
                }
                else
                {
                    foreach (var s in sentence.Symbols)
                    {
                        ErrorMessage += s.Value;
                    }
                    return error;//TODO: Mostrar Erro Sintático
                }

                List<Symbol> _produced = new List<Symbol>();
                foreach (var s in sentence.Symbols)
                {
                    newProduced = false;
                    if (s.Type == SymbolType.NonTerminal || s.Type == SymbolType.Terminal || s.Type == SymbolType.Empty)
                    {
                        _produced.Add(s);
                        sentence.Symbols.Remove(symbol);
                    }
                    else if (s.Type == SymbolType.Pipe && !_produced.Count.Equals(0) && _produced.Last().Type != SymbolType.Pipe && !s.Equals(sentence.Symbols.Last()))
                    {
                        production.Produced.Add(_produced);
                        _produced = new List<Symbol>();
                    }
                    else
                    {
                        //TODO: Mostrar Erro Sintático ::= |
                        foreach (var p in sentence.Symbols)
                        {
                            ErrorMessage += p.Value;
                        }
                        return false;
                    }
                }
                production.Produced.Add(_produced);
                productions.Add(production);

            }

            return true;
        }
        private void SeparateProductions()
        {
            singleProductions = new List<SingleProduction>();
            int i = 0;
            foreach(var _prod in productions)
            {
                foreach(var p in _prod.Produced)
                {
                    SingleProduction sProd = new SingleProduction();
                    sProd.Producer = _prod.Producer;
                    sProd.Produced = new LinkedList<Symbol>(p);
                    sProd.Number = i;
                    singleProductions.Add(sProd);
                    i++;
                }
            }
        }

        public List<Symbol> GetTerminals()
        {
            var empty = this.Terminals.Find(w => w.Type == SymbolType.Empty);
            Terminals.Remove(empty);
            return this.Terminals;
        }
        public List<Symbol> GetNonTerminals()
        {
            return this.NonTerminals;
        }
        public List<SingleProduction> GetSingleProductions()
        {
            return this.singleProductions;
        }

    }
}
