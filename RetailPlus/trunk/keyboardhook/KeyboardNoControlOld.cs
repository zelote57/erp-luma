using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AceSoft.KeyBoardHook
{
    //[Category("Command Blank 1"), Description("Type of command blank 1")]
    //public enum CommandBlank1 { Up, Plus, Default };

    //[Category("Command Blank 2"), Description("Type of command blank 2")]
    //public enum CommandBlank2 { Down, Minus, Default };

    public partial class KeyboardNoControlOld : UserControl
    {
        //private string mstrBlank1;
        //private string mstrBlank2;

        private CommandBlank1 pvtCommandBlank1 = CommandBlank1.Default;
        private CommandBlank2 pvtCommandBlank2 = CommandBlank2.Default;

        public CommandBlank1 commandBlank1
        {
            get
            {
                return pvtCommandBlank1;
            }
            set {
                pvtCommandBlank1 = value;
                if (pvtCommandBlank1 == CommandBlank1.Default)
                    cmdBlank1.Text = "";
                else
                    cmdBlank1.Text = pvtCommandBlank1.ToString();
            }
        }

        public CommandBlank2 commandBlank2
        {
            get
            {
                return pvtCommandBlank2;
            }
            set
            {
                pvtCommandBlank2 = value;
                if (pvtCommandBlank2 == CommandBlank2.Default)
                    cmdBlank2.Text = "";
                else
                    cmdBlank2.Text = pvtCommandBlank2.ToString();
            }
        }

        public KeyboardNoControlOld()
        {
            InitializeComponent();
        }

        private string pvtKeyboardKeyPressed = "";

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

        private void cmdNo1_Click(object sender, EventArgs e)
        {
            SetPressButton("1");
        }

        private void cmdNo2_Click(object sender, EventArgs e)
        {
            SetPressButton("2");
        }

        private void cmdNo3_Click(object sender, EventArgs e)
        {
            SetPressButton("3");
        }

        private void cmdNo4_Click(object sender, EventArgs e)
        {
            SetPressButton("4");
        }

        private void cmdNo5_Click(object sender, EventArgs e)
        {
            SetPressButton("5");
        }

        private void cmdNo6_Click(object sender, EventArgs e)
        {
            SetPressButton("6");
        }

        private void cmdNo7_Click(object sender, EventArgs e)
        {
            SetPressButton("7");
        }

        private void cmdNo8_Click(object sender, EventArgs e)
        {
            SetPressButton("8");
        }

        private void cmdNo9_Click(object sender, EventArgs e)
        {
            SetPressButton("9");
        }

        private void cmdNo0_Click(object sender, EventArgs e)
        {
            SetPressButton("0");
        }

        private void cmdDot_Click(object sender, EventArgs e)
        {
            SetPressButton(".");
        }

        private void cmdDel_Click(object sender, EventArgs e)
        {
            SetPressButton("{BACKSPACE}");
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            SetPressButton("{ENTER}");
        }

        private void cmdBlank1_Click(object sender, EventArgs e)
        {
            if (cmdBlank1.Text == CommandBlank1.Up.ToString())
                SetPressButton("{UP}");
            else if (cmdBlank1.Text == CommandBlank1.Plus.ToString())
                SetPressButton("+");
        }

        private void cmdBlank2_Click(object sender, EventArgs e)
        {
            if (cmdBlank2.Text == CommandBlank2.Down.ToString())
                SetPressButton("{DOWN}");
            else if (cmdBlank2.Text == CommandBlank2.Minus.ToString())
                SetPressButton("-");
        }
    }
}
