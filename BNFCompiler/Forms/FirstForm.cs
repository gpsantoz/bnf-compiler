using Analisador.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analisador.Forms
{
    public partial class FirstForm : Form
    {
        List<First> firstList;
        public FirstForm(List<First> _firstList)
        {
            this.firstList = _firstList;
            InitializeComponent();
            SetListBox();
        }

        private void SetListBox()
        {
            List<string> _items = new List<string>();
            foreach(var f in firstList)
            {
                if (f.NonTerminal.Value != "Slinha")
                {
                    string sentence = f.NonTerminal.Value + " => ";
                    foreach (var t in f.Terminals.Where(e => e.Type != SymbolType.Empty))
                    {
                        sentence += t.Value + " ";
                    }
                    _items.Add(sentence); 
                }

            }

            this.lbFirst.DataSource = _items;
        }
    }
}
