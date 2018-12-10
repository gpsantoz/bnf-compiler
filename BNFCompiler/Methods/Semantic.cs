using Analisador.Models;
using Analisador.Models.Semantic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Methods
{
    public class Semantic
    {
        List<First> nonTerminals_First;
        List<SentenceFirst> nonTerminals_SentenceFirst;
        List<Follow> nonTerminals_Follow;
        public Semantic()
        {
            nonTerminals_First = new List<First>();
            nonTerminals_Follow = new List<Follow>();
            nonTerminals_SentenceFirst = new List<SentenceFirst>();
        }

        public List<First> GetFirstList()
        {
            return this.nonTerminals_First;
        }
        public List<SentenceFirst> GetSentenceFirstList()
        {
            return this.nonTerminals_SentenceFirst;
        }

        public List<First> First(List<Symbol> nonTerminals, List<SingleProduction> productions)
        {
            foreach (Symbol NonTerminal in nonTerminals)
            {
                this.ProcessFirst(NonTerminal, productions);
            }
            return nonTerminals_First;
        }

        private First ProcessFirst(Symbol NonTerminal, List<SingleProduction> _productions)
        {
            if (!nonTerminals_First.Exists(u => u.NonTerminal.Value == NonTerminal.Value))
            {
                //SE NÃO EXISTE O FIRST DESSE NÃO TERMINAL, CRIA UM
                First first = new First();
                first.NonTerminal = NonTerminal;
                //PEGA TODAS AS PRODUÇÕES DESSE NÃO TERMINAL
                var nonTerminalProductions = _productions.Where(x => x.Producer.Value == NonTerminal.Value);
                //ANALISA CADA PRODUÇÃO
                foreach (var prod in nonTerminalProductions)
                {

                    SentenceFirst sentenceFirst;
                    //Verifica se ainda não existe o First local deste estado
                    if (!nonTerminals_SentenceFirst.Exists(w => w.NonTerminal.Value == prod.Producer.Value && w.SentenceNumber == prod.Number))
                    {
                        //Cria um novo first para esse sentença
                        sentenceFirst = new SentenceFirst();
                        sentenceFirst.SentenceNumber = prod.Number;
                        sentenceFirst.NonTerminal = prod.Producer;
                    }
                    else
                    {
                        sentenceFirst = nonTerminals_SentenceFirst.Find(w => w.NonTerminal.Value == prod.Producer.Value && w.SentenceNumber == prod.Number);
                    }

                    //PEGA O PRIMEIRO SIMBOLO DA PRODUÇÃO
                    var produced = prod.Produced.First();

                    //TRATAMENTO DA RECURSIVIDADE À ESQUERDA
                    if (produced.Value == NonTerminal.Value)
                    {
                        //VERIFICO SE ELE PRODUZ LAMBDA. CASO SIM, PULO ESSE SIMBOLO E PEGO O PROXIMO
                        if(nonTerminalProductions.ToList().Exists(r => r.Producer.Value == NonTerminal.Value && r.Produced.ToList().Exists(y => y.Type == SymbolType.Empty)))
                        {
                            //TODO: Pego o proximo, já que ele produz lambda
                        }
                    }
                    else
                    { 

                        if ((produced.Type == SymbolType.Terminal || produced.Type == SymbolType.Empty) && !first.Terminals.Exists(r => r.Value == produced.Value))
                        {
                            first.Terminals.Add(produced);
                            sentenceFirst.Terminals.Add(produced);
                        }
                        else
                        {
                            if (produced.Type == SymbolType.NonTerminal)
                            {
                                //VERIFICA SE JÁ EXISTE UM FIRST DESSE NÃO TERMINAL
                                if (nonTerminals_First.Exists(x => x.NonTerminal.Value == produced.Value))
                                {
                                    foreach (var VARIABLE in nonTerminals_First.Find(x => x.NonTerminal.Value == produced.Value).Terminals)
                                    {
                                        if (!first.Terminals.Exists(t => t.Value == VARIABLE.Value))
                                        {
                                            first.Terminals.Add(VARIABLE);
                                        }
                                        if (!sentenceFirst.Terminals.Exists(t => t.Value == VARIABLE.Value))
                                        {
                                            sentenceFirst.Terminals.Add(VARIABLE);
                                        }
                                    }
                                }
                                else
                                {   //NÃO EXISTE UM FIRST, GERA O FIRST
                                    foreach (var VARIABLE in ProcessFirst(produced, _productions).Terminals)
                                    {
                                        if (!first.Terminals.Exists(t => t.Value == VARIABLE.Value))
                                        {
                                            first.Terminals.Add(VARIABLE);
                                        }
                                        if (!sentenceFirst.Terminals.Exists(t => t.Value == VARIABLE.Value))
                                        {
                                            sentenceFirst.Terminals.Add(VARIABLE);
                                        }
                                    }
                                   
                                    //VERIFICA SE GERA LAMBDA. CASO SIM, PEGA O FIRST DO PROXIMO
                                    if (nonTerminals_First.Find(x => x.NonTerminal.Value == produced.Value).Terminals.Exists(y => y.Type == SymbolType.Empty) && prod.Produced.Find(produced).Next != null)
                                    {
                                        //SE FOR UM NAO TERMINAL, GERA/PEGA O FIRST DELE
                                        if (prod.Produced.Find(produced).Next.Value.Type == SymbolType.NonTerminal)
                                        {
                                            foreach (var VARIABLE in ProcessFirst(
                                                prod.Produced.Find(produced).Next.Value, _productions).Terminals)
                                            {
                                                if (!first.Terminals.Exists(t => t.Value == VARIABLE.Value))
                                                {
                                                    first.Terminals.Add(VARIABLE);
                                                }
                                                if (!sentenceFirst.Terminals.Exists(t => t.Value == VARIABLE.Value))
                                                {
                                                    sentenceFirst.Terminals.Add(VARIABLE);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //TODO: E SE FOR UM LAMBDA?
                                            if (prod.Produced.Find(produced).Next.Value.Type == SymbolType.Terminal)
                                            {
                                                if (!first.Terminals.Exists(t =>
                                                    t.Value == prod.Produced.Find(produced).Next.Value.Value))
                                                {
                                                    first.Terminals.Add(prod.Produced.Find(produced).Next.Value);
                                                }
                                                if (!sentenceFirst.Terminals.Exists(t =>
                                                    t.Value == prod.Produced.Find(produced).Next.Value.Value))
                                                {
                                                    sentenceFirst.Terminals.Add(prod.Produced.Find(produced).Next
                                                        .Value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    nonTerminals_SentenceFirst.Add(sentenceFirst);
                }
                    nonTerminals_First.Add(first);
              
                return first;
            }
            else
            {
                return this.nonTerminals_First.Find(w => w.NonTerminal.Value == NonTerminal.Value);
            }

        }
        public List<Follow> Follow(List<Symbol> nonTerminals, List<SingleProduction> productions)
        {
            this.ProcessFollow(nonTerminals, productions);
            while(nonTerminals_Follow.Any(e => e.Changed))
            {
                foreach (var f in nonTerminals_Follow)
                {
                    f.Changed = false;
                }
                this.ProcessFollow(nonTerminals, productions);
            }
            return nonTerminals_Follow;
        }

        private List<Symbol> SndSRuleFollow(LinkedListNode<Symbol> node, SingleProduction prod)
        {
            var terminals = new List<Symbol>();

            //Pega o Follow do Não Terminal do lado esquerdo
            var producerFollow = nonTerminals_Follow.Find(x => x.NonTerminal.Value == prod.Producer.Value);
            terminals.AddRange(producerFollow.Terminals);

            //Pega o First do Não Terminal para verificar se ele gera lambda
            var nodeFirst = nonTerminals_First.Find(x => x.NonTerminal.Value == node.Value.Value);
            if (nodeFirst.Terminals.Exists(w => w.Type == SymbolType.Empty) && node.Previous != null && node.Previous.Value.Type == SymbolType.NonTerminal)
            {
                terminals.AddRange(SndSRuleFollow(node.Previous, prod));
            }

            return terminals;
        }
        private List<Symbol> FstRuleFollow(LinkedListNode<Symbol> node)
        {
            var terminals = new List<Symbol>();
            if (node.Value.Type == SymbolType.Terminal)
            {
                terminals.Add(node.Value);
            }
            else
            {
                //Pega o First do Não Terminal
                var first = nonTerminals_First.Find(x => x.NonTerminal.Value == node.Value.Value);
                //Adiciona os terminais no Follow
                terminals.AddRange(first.Terminals);
                //Verifica se ele gera vazio e se o próximo é nule
                if (first.Terminals.Exists(w => w.Type == SymbolType.Empty) && node.Next != null)
                {
                    if (node.Value.Type == SymbolType.NonTerminal)
                        terminals.AddRange(FstRuleFollow(node.Next));
                    else
                    {
                        if (node.Value.Type == SymbolType.Terminal)
                        {
                            terminals.Add(node.Value);
                        }
                    }
                }
            }
            return terminals;
        }

        private void ProcessFollow(List<Symbol> nonTerminals, List<SingleProduction> _productions)
        {
            //ANALISA O FOLLOW DE TODOS OS NAO TERMINAIS, MAS APENAS A PRIMEIRA REGRA
            foreach (Symbol NonTerminal in nonTerminals)
            {
                if (NonTerminal.Value == "Slinha" && !nonTerminals_Follow.Exists(s => s.NonTerminal.Value == "Slinha"))
                {
                    Follow sLinha = new Follow();
                    sLinha.NonTerminal = NonTerminal;
                    sLinha.Terminals.Add(new Symbol() { Type = SymbolType.EndOfFile, Value = "$" });
                    nonTerminals_Follow.Add(sLinha);
                }
                else
                {
                    Follow follow = new Follow();
                    follow.NonTerminal = NonTerminal;
                    List<SingleProduction> presProductions = _productions.Where(x => x.Produced.Any(y => y.Value == NonTerminal.Value) == true).ToList();
                    //PRIMEIRO EXECUTA A PRIMEIRA REGRA PARA TODAS AS PRODUÇÕES
                    foreach (var prod in presProductions)
                    {
                        //GARANTE QUE VERIFICOU TODAS AS APARIÇÕES DO SIMBOLO NÃO TERMINAL: POR EXEMPLO A=>ABAa
                        for (var recentNode = prod.Produced.First; recentNode != null; recentNode = recentNode.Next)
                        {
                            if (recentNode.Value.Value == NonTerminal.Value)
                            {
                                if (recentNode.Next != null)
                                {
                                    foreach(var t in FstRuleFollow(recentNode.Next))
                                    {
                                        if(!follow.Terminals.Exists(x => x.Value == t.Value))
                                        {
                                            follow.Terminals.Add(t);
                                        }
                                    }
                                    
                                    //First do proximo vem pro follow do Não Terminal
                                }
                            }
                        }
                    }
                    if (!nonTerminals_Follow.Exists(f => f.NonTerminal.Value == follow.NonTerminal.Value))
                        nonTerminals_Follow.Add(follow);
                    else
                    {
                        foreach (var item in follow.Terminals)
                        {
                            if (!nonTerminals_Follow.Find(w => w.NonTerminal.Value == follow.NonTerminal.Value)
                                .Terminals.Exists(nt => nt.Equals(item)))
                            {
                                nonTerminals_Follow.Find(w => w.NonTerminal.Value == follow.NonTerminal.Value)
                                    .Terminals.Add(item);
                                nonTerminals_Follow.Find(w => w.NonTerminal.Value == follow.NonTerminal.Value)
                                    .Changed = true;
                            }
                        }
                    }
                }
            }

            //ANALISA O FOLLOW DE TODOS OS NAO TERMINAIS, MAS APENAS A SEGUNDA REGRA
            foreach (Symbol NonTerminal in nonTerminals)
            {
                if(NonTerminal.Value != "Slinha")
                {
                    List<SingleProduction> presentProductions = _productions.Where(x => x.Produced.Any(y => y.Value == NonTerminal.Value) == true).ToList();
                    //DEPOIS EXECUTA A SEGUNDA REGRA PARA TODAS AS PRODUÇÕES
                    foreach (var prod in presentProductions)
                    {
                        //Garante que verificou todas as aparições do símbolo na produção
                        for (var recentNode = prod.Produced.First; recentNode != null; recentNode = recentNode.Next)
                        {
                            if (recentNode.Value.Value == NonTerminal.Value)
                            {
                                //Verifica se ele é o último da sentença
                                if (recentNode.Next == null)
                                {
                                    var terminals = SndSRuleFollow(recentNode, prod);
                                    foreach (var t in terminals)
                                    {
                                        if (!nonTerminals_Follow.Find(x => x.NonTerminal.Value == NonTerminal.Value).Terminals.Exists(x => x.Value == t.Value))
                                        {
                                            nonTerminals_Follow.Find(x => x.NonTerminal.Value == NonTerminal.Value).Terminals.Add(t);
                                            nonTerminals_Follow.Find(x => x.NonTerminal.Value == NonTerminal.Value)
                                                .Changed = true;
                                        }
                                    }

                                }
                                else
                                {
                                    //Se não for o último, verifica se o próximo é um não terminal que gera vazio
                                    //TODO: Botar isso dentro de uma função recursiva para poder funcionar com <NaoTerminal>::=<E>""""""
                                    if(recentNode.Next.Value.Type == SymbolType.NonTerminal)
                                    { 
                                        //Pega o First do Não Terminal
                                        var firstNode = nonTerminals_First.Find(x => x.NonTerminal.Value == recentNode.Next.Value.Value);
                                        //VERIFICA SE GERA VAZIO
                                        if (firstNode.Terminals.Exists(n => n.Type == SymbolType.Empty) && recentNode.Next.Next == null)
                                        {
                                            var terminals = SndSRuleFollow(recentNode, prod);
                                            foreach (var t in terminals)
                                            {
                                                if (!nonTerminals_Follow.Find(x => x.NonTerminal.Value == NonTerminal.Value).Terminals.Exists(x => x.Value == t.Value))
                                                {
                                                    nonTerminals_Follow.Find(x => x.NonTerminal.Value == NonTerminal.Value).Terminals.Add(t);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public List<Follow> GetFollowList()
        {
            return this.nonTerminals_Follow;
        }

        private List<Production> GetNonTerminalProductions(Symbol NonTerminal, List<Production> Productions)
        {
            List<Production> NonTerminalProductions = new List<Production>();
            foreach(var prod in Productions)
            {
                if(prod.Producer.Value == NonTerminal.Value)
                {
                    NonTerminalProductions.Add(prod);
                }
            }
            return NonTerminalProductions;
        }
    }
}
