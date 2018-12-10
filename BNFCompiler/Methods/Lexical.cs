using Analisador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analisador.Methods
{
    public class Lexical
    {
        List<Rule> rules;
        List<Symbol> symbols;
        List<Sentence> symbolsArray;
        public string ErrorMessage { get; set; }
        public Lexical()
        {
            rules = new List<Rule>();
            symbols = new List<Symbol>();
            symbolsArray = new List<Sentence>();
            rules.Add(new Rule() { Expression = @"^<[A-Za-z]+>", Type = SymbolType.NonTerminal }); //NonTerminal
            rules.Add(new Rule() { Expression = @"^""[A-Za-z]+""", Type = SymbolType.Terminal }); //Terminal
            rules.Add(new Rule() { Expression = @"^::=", Type = SymbolType.Production }); //Production
            rules.Add(new Rule() { Expression = "^[#]", Type = SymbolType.Empty }); //Empty
            rules.Add(new Rule() { Expression = "^[|]", Type = SymbolType.Pipe }); //Pipe
            //TODO: Inserir regra para simbolo opcional
        }
        public bool verifyLines(string text)
        {
            bool flag = false;
            //text = Regex.Replace(text, @"\s+", "");
            if (text.Contains("#")) //Verifica se o usuário digitou '#', pois no nosso programa é o episilon
            {
                ErrorMessage = "Unrecognized # symbol.";
                return flag;
            }
            string grammar = text.Replace(@"""""", "#"); //Vazio passa a ser '#'
            string[] sentences = grammar.Split('\n');
            string newSentence = string.Empty;
            foreach (var sentence in sentences)
            {
                Sentence _sentence = new Sentence();
                int index = 0;
                newSentence = sentence.ToString();
                while (index != sentence.Length)
                {
                    flag = false;
                    foreach (var rule in rules) //Verifica toda as regras permitidas
                    {
                        var match = Regex.Match(newSentence, rule.Expression);
                        if (match.Success) //Se der match, adiciona o token e anda na string
                        {
                            var symbol = new Symbol() { Value = match.Value, Type = rule.Type };
                            _sentence.Symbols.Add(symbol);
                            index += match.Length;
                            newSentence = sentence.Substring(index);
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        ErrorMessage = sentence;
                        return false;
                    }
                }
                symbolsArray.Add(_sentence);
            }
            return true;
        }

        public List<Sentence> GetSentence()
        {
            return this.symbolsArray;
        }
    }
}
