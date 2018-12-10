using Analisador.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analisador.Methods.Tables;

namespace Analisador.Forms
{
    public partial class TablesForm : Form
    {
        Semantic _semantic;
        Syntactic _syntactic;
        LLParser _llanalyser;
        private SLRParser _slrParser;
        private LRParser _lrParser;
        private LALRParser _lalrParser;

        public TablesForm(Syntactic syntactic, Semantic semantic)
        {
            _semantic = semantic;
            _syntactic = syntactic;
            _llanalyser = new LLParser(_semantic, _syntactic);
            _lrParser = new LRParser(_semantic, _syntactic);
            _slrParser = new SLRParser(_semantic, _syntactic);
            _lalrParser = new LALRParser(_semantic, _syntactic);

            InitializeComponent();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSLR.Checked)
            {
                if (_slrParser.IsSLR)
                {
                    FillTable(_slrParser._tableParser);
                }
            }
        }


        private void btnGenerateTable_Click(object sender, EventArgs e)
        {


        }

        private void rbLL_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLL.Checked)
            {
                //Gerar tabela LL(1)
                _llanalyser = new LLParser(_semantic, _syntactic);
                _llanalyser.GenerateTable(this.gridTables);
            }
            if (_llanalyser.Generated)
            {
                //TODO: remover preenchimento de dentro método global, criar um método apenas para preenchimento
            }
        }

        private void FillTable(TableParser tableParser)
        {
            string[][] matrix = new string[tableParser.RowsNumber][];
            for (int a = 0; a < tableParser.RowsNumber; a++)
            {
                matrix[a] = new string[tableParser.ColumnsNumber];
            }
            int termColumns = 0;
            int termRows = 0;

            this.gridTables.ColumnCount = tableParser.ColumnsNumber;
            this.gridTables.RowCount = tableParser.RowsNumber;
            this.gridTables.Columns[0].Width = 40;
            foreach (var sym in tableParser.Symbols)
            {
                gridTables.Columns[termColumns].Name = sym.Value;
                gridTables.Columns[termColumns].Width = 40;
                termColumns++;
            }

            var groupedMatches = tableParser.Matches.GroupBy(item => item.StateNumber);
            foreach (var m in groupedMatches)
            {
                //agora crio uma linha pra cada simbolo nao terminal
                DataGridViewRow row = (DataGridViewRow)gridTables.Rows[0].Clone();
                row.HeaderCell.Value = m.First().StateNumber;

                //pego os matches desse stado
                foreach (var VARIABLE in m)
                {
                    gridTables.Rows[VARIABLE.StateNumber].Cells[gridTables.Columns[VARIABLE.SymbolValue].Index].Value =
                        VARIABLE.CellValue;

                }
                    
                //gridView.Rows[termRows] = row;
                gridTables.Rows[m.First().StateNumber].HeaderCell.Value = m.First().StateNumber.ToString();
            }
        }

        private void TablesForm_Load(object sender, EventArgs e)
        {
            //Gerar tabela LL


            

            //Gerar tabela SLR
            if (!_slrParser.Generated)
                _slrParser.GenerateTable(gridTables);
            if (_slrParser.IsSLR)
            {
                slrLabel.Text = "It's SLR(1)";
                slrLabel.ForeColor = Color.LightSeaGreen;
                rbSLR.Enabled = true;
            }
            else
            {
                slrLabel.Text = "It's not SLR(1)";
                slrLabel.ForeColor = Color.Red;
                rbSLR.Enabled = false;
            }

            //Gerar tabela LR
            if (!_lrParser.Generated)
                _lrParser.GenerateTable(gridTables);
            if (_lrParser.IsLR)
            {
                lrLabel.Text = "It's LR(1)";
                lrLabel.ForeColor = Color.LightSeaGreen;
                rbLR.Enabled = true;
            }
            else
            {
                lrLabel.Text = "It's not LR(1)";
                lrLabel.ForeColor = Color.Red;
                rbLR.Enabled = false;
            }

            //Gerar tabela LALR
            if (!_lalrParser.Generated)
                _lalrParser.GenerateTable(_lrParser.GetLrStates());
            if (_lrParser.IsLR)
            {
                lalrLabel.Text = "It's LALR(1)";
                lalrLabel.ForeColor = Color.LightSeaGreen;
                rbLALR.Enabled = true;
            }
            else
            {
                lalrLabel.Text = "It's not LALR(1)";
                lalrLabel.ForeColor = Color.Red;
                rbLALR.Enabled = false;
            }
        }

        private void lrLabel_Click(object sender, EventArgs e)
        {

        }

        private void rbLR_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLR.Checked)
            {

                if (_lrParser.IsLR)
                {
                    FillTable(_lrParser._tableParser);
                }
            }
        }

        private void rbLALR_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLALR.Checked)
            {

                if (_lalrParser.IsLALR)
                {
                    FillTable(_lalrParser._tableParser);
                }
            }
        }
    }
}
