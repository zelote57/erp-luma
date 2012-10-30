using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AceSoft.KeyBoardHook
{
    public partial class Keyboardcontrol : UserControl
    {
        public Keyboardcontrol()
        {
            InitializeComponent();
        }

        private Boolean shiftindicator = false;
        private Boolean capslockindicator = false;
        private string pvtKeyboardKeyPressed = "";

        public event KeyboardDelegate UserKeyPressed;
        protected virtual void OnUserKeyPressed(KeyboardEventArgs e)
        {
            if (UserKeyPressed != null)
                UserKeyPressed(this, e);
        }

        private string HandleShiftableKey(string theKey)
        {
            if (shiftindicator)
            {
                return "+" + theKey;
            }
            else
            {
                return theKey;
            }
        }

        private string HandleShiftableCaplockableKey(string theKey)
        {
            switch (theKey)
            {
                case ("q"):
                    theKey = "a";
                    break;
                case ("w"):
                    theKey = "b";
                    break;
                case ("e"):
                    theKey = "c";
                    break;
                case ("r"):
                    theKey = "d";
                    break;
                case ("t"):
                    theKey = "e";
                    break;
                case ("y"):
                    theKey = "f";
                    break;
                case ("u"):
                    theKey = "g";
                    break;
                case ("i"):
                    theKey = "h";
                    break;
                case ("o"):
                    theKey = "i";
                    break;
                case ("p"):
                    theKey = "j";
                    break;
                case ("a"):
                    theKey = "k";
                    break;
                case ("s"):
                    theKey = "l";
                    break;
                case ("d"):
                    theKey = "m";
                    break;
                case ("f"):
                    theKey = "n";
                    break;
                case ("g"):
                    theKey = "o";
                    break;
                case ("h"):
                    theKey = "p";
                    break;
                case ("j"):
                    theKey = "q";
                    break;
                case ("k"):
                    theKey = "r";
                    break;
                case ("l"):
                    theKey = "s";
                    break;
                case ("z"):
                    theKey = "t";
                    break;
                case ("x"):
                    theKey = "u";
                    break;
                case ("c"):
                    theKey = "v";
                    break;
                case ("v"):
                    theKey = "w";
                    break;
                case ("b"):
                    theKey = "x";
                    break;
                case ("n"):
                    theKey = "y";
                    break;
                case ("m"):
                    theKey = "z";
                    break;
            }
            if (capslockindicator)
            {
                return "+" + theKey;
            }
            else if (shiftindicator)
            {
                return "+" + theKey;
            }
            else
            {
                return theKey;
            }
        }

        private void HandleShiftClick()
        {
            if (shiftindicator)
            {
                shiftindicator = false;
            }
            else
            {
                shiftindicator = true;
            }
        }

        private void HandleCapsLock()
        {
            if (capslockindicator)
            {
                capslockindicator = false;
            }
            else
            {
                capslockindicator = true;
            }
        }

        private void SetPressButton(string strKey)
        {
            pvtKeyboardKeyPressed = strKey;
            KeyboardEventArgs dea = new KeyboardEventArgs(pvtKeyboardKeyPressed);
            OnUserKeyPressed(dea);
        }

        private void cmdA_Click(object sender, EventArgs e)
        {
            SetPressButton("A");
        }

        private void cmdB_Click(object sender, EventArgs e)
        {
            SetPressButton("B");
        }

        private void cmdC_Click(object sender, EventArgs e)
        {
            SetPressButton("C");
        }

        private void cmdD_Click(object sender, EventArgs e)
        {
            SetPressButton("D");
        }

        private void cmdE_Click(object sender, EventArgs e)
        {
            SetPressButton("E");
        }

        private void cmdF_Click(object sender, EventArgs e)
        {
            SetPressButton("F");
        }

        private void cmdG_Click(object sender, EventArgs e)
        {
            SetPressButton("G");
        }

        private void cmdH_Click(object sender, EventArgs e)
        {
            SetPressButton("H");
        }

        private void cmdI_Click(object sender, EventArgs e)
        {
            SetPressButton("I");
        }

        private void cmdJ_Click(object sender, EventArgs e)
        {
            SetPressButton("J");
        }

        private void cmdK_Click(object sender, EventArgs e)
        {
            SetPressButton("K");
        }

        private void cmdL_Click(object sender, EventArgs e)
        {
            SetPressButton("L");
        }

        private void cmdM_Click(object sender, EventArgs e)
        {
            SetPressButton("M");
        }

        private void cmdN_Click(object sender, EventArgs e)
        {
            SetPressButton("N");
        }

        private void cmdO_Click(object sender, EventArgs e)
        {
            SetPressButton("O");
        }

        private void cmdP_Click(object sender, EventArgs e)
        {
            SetPressButton("P");
        }

        private void cmdQ_Click(object sender, EventArgs e)
        {
            SetPressButton("Q");
        }

        private void cmdR_Click(object sender, EventArgs e)
        {
            SetPressButton("R");
        }

        private void cmdS_Click(object sender, EventArgs e)
        {
            SetPressButton("S");
        }

        private void cmdT_Click(object sender, EventArgs e)
        {
            SetPressButton("T");
        }

        private void cmdU_Click(object sender, EventArgs e)
        {
            SetPressButton("U");
        }

        private void cmdV_Click(object sender, EventArgs e)
        {
            SetPressButton("V");
        }

        private void cmdW_Click(object sender, EventArgs e)
        {
            SetPressButton("W");
        }

        private void cmdX_Click(object sender, EventArgs e)
        {
            SetPressButton("X");
        }

        private void cmdY_Click(object sender, EventArgs e)
        {
            SetPressButton("Y");
        }

        private void cmdZ_Click(object sender, EventArgs e)
        {
            SetPressButton("Z");
        }

        private void cmdBackspace_Click(object sender, EventArgs e)
        {
            SetPressButton("{BACKSPACE}");
        }

        private void cmdSpace_Click(object sender, EventArgs e)
        {
            SetPressButton(" ");
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

        private void cmd0_Click(object sender, EventArgs e)
        {
            SetPressButton("0");
        }

        private void cmdAsterisk_Click(object sender, EventArgs e)
        {
            SetPressButton("*");
        }

        private void cmdDOT_Click(object sender, EventArgs e)
        {
            SetPressButton(".");
        }

        private void cmdComma_Click(object sender, EventArgs e)
        {
            SetPressButton(",");
        }

        private void cmdF2_Click(object sender, EventArgs e)
        {
            SetPressButton("{F2}");
        }

        private void cmdAtSign_Click(object sender, EventArgs e)
        {
            SetPressButton("@");
        }

        private void cmdDollar_Click(object sender, EventArgs e)
        {
            SetPressButton("$");
        }

    }

    public delegate void KeyboardDelegate(object sender, KeyboardEventArgs e);

    public class KeyboardEventArgs : EventArgs
    {
        private readonly string pvtKeyboardKeyPressed;

        public KeyboardEventArgs(string KeyboardKeyPressed)
        {
            this.pvtKeyboardKeyPressed = KeyboardKeyPressed;
        }

        public string KeyboardKeyPressed
        {
            get
            {
                return pvtKeyboardKeyPressed;
            }
        }
    }

    [Category("Keyboard Type"), Description("Type of keyboard to use")]
    public enum BoW { Standard, Alphabetical, Kids };
}
