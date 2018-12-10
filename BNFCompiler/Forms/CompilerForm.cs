using Analisador.Forms;
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

namespace Analisador
{
    public partial class CompilerForm : Form
    {
        Lexical _lexical;
        Syntactic _syntactic;
        Semantic _semantic;
        FirstForm firstForm;
        FollowForm followForm;
        TablesForm tablesForm;
        public CompilerForm()
        {
            InitializeComponent();
            this.InitializeElements();
        }

        private void btnSyntatic_Click(object sender, EventArgs e)
        {
            _syntactic = new Syntactic(this._semantic);
            bool syntacticResult = _syntactic.VerifySyntactic(_lexical.GetSentence());
            if (!syntacticResult)
            {
                lblSyntactic.Text = "Erro Sintático no simbolo: " + _syntactic.ErrorMessage;
                lblSyntactic.ForeColor = Color.Red;
            }
            else
            {
                ShowSyntacticResult(syntacticResult);
            }
           

            //Só calcula o first e o follow se não tiver erro sintáticoo
            if (syntacticResult)
            {
                _semantic.First(_syntactic.GetNonTerminals(), _syntactic.GetSingleProductions());
                _semantic.Follow(_syntactic.GetNonTerminals(), _syntactic.GetSingleProductions()); 
            }
        }

        private void CompilerForm_Load(object sender, EventArgs e)
        {
            btnSyntatic.Enabled = false;
        }

        private void ShowLexicalResult(bool result)
        {
            btnLexical.Enabled = false;
            if (result)
            {
                ShowResult(lblLexical, "Lexical analysis completed without errors!", Color.Green);
                this.btnSyntatic.Enabled = true;
            }
            else
            {

                ShowResult(lblLexical, "Lexical error found during parsing: \n Token not recognized!", Color.Red);
                this.btnSyntatic.Enabled = false;
            }
        }

        private void ShowSyntacticResult(bool result)
        {
            btnSyntatic.Enabled = false;

            btnShowFirst.Enabled = true;

            btnFollow.Enabled = true;
            lblLexical.Text = "";
            btnLexical.Enabled = false;
            btnTables.Enabled = true;
            if (result)
            {
                ShowResult(lblSyntactic, "Syntactic analysis completed without errors!", Color.Green);
            }
            else
            {
                ShowResult(lblSyntactic, "Syntactic error encountered during parsing!", Color.Red);
            }
        }

        private void InitializeElements()
        {
            _lexical = new Lexical();
            _semantic = new Semantic();
            _syntactic = new Syntactic(_semantic);

            RtxtCompiler.Text = " < E >::=< T >< El >\n < T >::=< F >< Tl >\n < F >::=\"a\" <E>\"f\"\n<F>::=\"x\"\n<El>::=\"m\"<T><El>\n<El>::=\"\"\n<Tl>::=\"v\"<F><Tl>\n<Tl>::=\"\"".Replace(" ", string.Empty);
            btnLexical.Enabled = true;
            btnSyntatic.Enabled = false;
            btnShowFirst.Enabled = false;
            btnFollow.Enabled = false;
            btnTables.Enabled = false;
            rbGrammar1.Enabled = true;
            rbGrammar2.Enabled = true;
            lblLexical.Text = "";
            lblSyntactic.Text = "";
        }


        private void ShowResult(Label label, string message, Color color)
        {
            label.Text = message;
            label.ForeColor = color;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.InitializeElements();
        }

        private void btnShowFirst_Click(object sender, EventArgs e)
        {
 
            firstForm = new FirstForm(_semantic.GetFirstList());
            firstForm.ShowDialog();

        }

        private void btnFollow_Click(object sender, EventArgs e)
        {
            
            followForm = new FollowForm(_semantic.GetFollowList());
            followForm.ShowDialog();
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            tablesForm = new TablesForm(_syntactic, _semantic);
            tablesForm.ShowDialog();
        }

        private void rbGrammar2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGrammar2.Checked)
            {
                RtxtCompiler.Text = "< S >::=< A >\"b\"< B >\n < S >::=< B >\n < A >::=\"a\" <B>\n<A>::=\"b\"\n<B>::=<A>".Replace(" ", string.Empty);
            }
        }

        private void rbGrammar1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGrammar1.Checked)
            {
                RtxtCompiler.Text = "< E >::=< E >\"m\"< T >\n < E >::=< T >\n < T >::=<T>\"v\"<F>\n<T>::=<F>\n<F>::=\"pa\"<E>\"pf\"\n<F>::=\"a\"".Replace(" ", string.Empty);
            }
        }

        private void btnLexical_Click(object sender, EventArgs e)
        {
            //TODO: Verificar se "   " é um erro ou nao, para retirarmos os espaços em branco
            bool lexicalResult = _lexical.verifyLines(RtxtCompiler.Text.Replace(" ", string.Empty));
            if (!lexicalResult)
            {
                lblLexical.Text = "Lexical error on the line: " + _lexical.ErrorMessage;
                lblLexical.ForeColor = Color.Red;
            }
            else
            {
                ShowLexicalResult(lexicalResult);
            }
            
            rbGrammar1.Enabled = false;
            rbGrammar2.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGrammar3.Checked)
            {
                RtxtCompiler.Text = "< S >::=< S >< S >\"m\"\n < S >::=< S >< S >\"v\"\n < S >::=\"a\"".Replace(" ", string.Empty);
            }
        }

        private void lblLexical_Click(object sender, EventArgs e)
        {

        }
    }
}
