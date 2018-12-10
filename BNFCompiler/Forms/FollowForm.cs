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
    public partial class FollowForm : Form
    {
        List<Follow> followList;
        public FollowForm(List<Follow> _followList)
        {
            this.followList = _followList;
            InitializeComponent();
            SetListBox();
        }
        private void SetListBox()
        {
            List<string> _items = new List<string>();
            foreach (var f in followList)
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

            this.lbFollow.DataSource = _items;
        }
    }
}


