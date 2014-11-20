using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AceSoft.KeyBoardHook
{
    public partial class NoControl : Form
    {
        private DialogResult dialog;
        private string pvtKeyboardKeyPressed = "";

        public DialogResult Result { get { return dialog; } }
        public decimal NoValue { get; set; }

        public event KeyboardDelegate UserKeyPressed;
        protected virtual void OnUserKeyPressed(KeyboardEventArgs e)
        {
            if (UserKeyPressed != null)
                UserKeyPressed(this, e);
        }

        private void SetPressButton(string strKey)
        {
            pvtKeyboardKeyPressed = strKey;
            KeyboardEventArgs dea = new KeyboardEventArgs(pvtKeyboardKeyPressed);
            OnUserKeyPressed(dea);
        }

        public NoControl()
        {
            InitializeComponent();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            SetPressButton("{ENTER}");
        }

        private void cmdDot_Click(object sender, EventArgs e)
        {
            SetPressButton(".");
        }

        private void cmd0_Click(object sender, EventArgs e)
        {
            SetPressButton("0");
        }

        private void cmd1_Click(object sender, EventArgs e)
        {
            SetPressButton("1");
        }

        private void cmd2_Click(object sender, EventArgs e)
        {
            SetPressButton("2");
        }

        private void cmd3_Click(object sender, EventArgs e)
        {
            SetPressButton("3");
        }

        private void cmd4_Click(object sender, EventArgs e)
        {
            SetPressButton("4");
        }

        private void cmd5_Click(object sender, EventArgs e)
        {
            SetPressButton("5");
        }

        private void cmd6_Click(object sender, EventArgs e)
        {
            SetPressButton("6");
        }

        private void cmd7_Click(object sender, EventArgs e)
        {
            SetPressButton("7");
        }

        private void cmd8_Click(object sender, EventArgs e)
        {
            SetPressButton("8");
        }

        private void cmd9_Click(object sender, EventArgs e)
        {
            SetPressButton("9");
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtNo.Text = "";
        }

        private void cmdBksp_Click(object sender, EventArgs e)
        {
            SetPressButton("{BACKSPACE}");
        }

        private void NoControl_Load(object sender, EventArgs e)
        {

        }

        private void NoControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

                case Keys.Enter:
                    if (isValuesAssigned())
                    {
                        dialog = DialogResult.OK;
                        this.Hide();
                    }
                    break;
            }
        }

        private bool isValuesAssigned()
        {
            decimal decRetValue = 0;

            if (!decimal.TryParse(txtNo.Text, out decRetValue))
            {
                MessageBox.Show("Sorry an invalid no was has been detected.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNo.SelectAll();
                return false;
            }
            else { NoValue = decRetValue; }
            return true;
        }
    }
}
