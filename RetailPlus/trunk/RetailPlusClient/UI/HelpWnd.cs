using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace AceSoft.RetailPlus.Client.UI
{
    /// <summary>
    /// Summary description for HelpWnd.
    /// </summary>
    public class HelpWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private Label label87;
        private Label label88;
        private Label label85;
        private Label label86;
        private Label label89;
        private Label label90;
        private Label label97;
        private Label label98;
        private Label label95;
        private Label label96;
        private Label label93;
        private Label label94;
        private Label label91;
        private Label label92;
        private Label label20;
        private Label label38;
        private Label label39;
        private Label label54;
        private Label label55;
        private Label label66;
        private Label label67;
        private Label label80;
        private Label label81;
        private Label label99;
        private Label label100;
        private Label label101;
        private Label label102;
        private Label label6;
        private Label label103;
        private Label label104;
        private Button cmdCancel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public HelpWnd()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.TabIndex = 51;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label103);
            this.groupBox1.Controls.Add(this.label104);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this.label55);
            this.groupBox1.Controls.Add(this.label66);
            this.groupBox1.Controls.Add(this.label67);
            this.groupBox1.Controls.Add(this.label80);
            this.groupBox1.Controls.Add(this.label81);
            this.groupBox1.Controls.Add(this.label99);
            this.groupBox1.Controls.Add(this.label100);
            this.groupBox1.Controls.Add(this.label101);
            this.groupBox1.Controls.Add(this.label102);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label97);
            this.groupBox1.Controls.Add(this.label98);
            this.groupBox1.Controls.Add(this.label95);
            this.groupBox1.Controls.Add(this.label96);
            this.groupBox1.Controls.Add(this.label93);
            this.groupBox1.Controls.Add(this.label94);
            this.groupBox1.Controls.Add(this.label91);
            this.groupBox1.Controls.Add(this.label92);
            this.groupBox1.Controls.Add(this.label89);
            this.groupBox1.Controls.Add(this.label90);
            this.groupBox1.Controls.Add(this.label87);
            this.groupBox1.Controls.Add(this.label88);
            this.groupBox1.Controls.Add(this.label85);
            this.groupBox1.Controls.Add(this.label86);
            this.groupBox1.Controls.Add(this.label83);
            this.groupBox1.Controls.Add(this.label84);
            this.groupBox1.Controls.Add(this.label82);
            this.groupBox1.Controls.Add(this.label79);
            this.groupBox1.Controls.Add(this.label78);
            this.groupBox1.Controls.Add(this.label77);
            this.groupBox1.Controls.Add(this.label76);
            this.groupBox1.Controls.Add(this.label75);
            this.groupBox1.Controls.Add(this.label74);
            this.groupBox1.Controls.Add(this.label70);
            this.groupBox1.Controls.Add(this.label71);
            this.groupBox1.Controls.Add(this.label72);
            this.groupBox1.Controls.Add(this.label73);
            this.groupBox1.Controls.Add(this.label68);
            this.groupBox1.Controls.Add(this.label69);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.label49);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.label65);
            this.groupBox1.Controls.Add(this.label62);
            this.groupBox1.Controls.Add(this.label63);
            this.groupBox1.Controls.Add(this.label60);
            this.groupBox1.Controls.Add(this.label61);
            this.groupBox1.Controls.Add(this.label58);
            this.groupBox1.Controls.Add(this.label59);
            this.groupBox1.Controls.Add(this.label56);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Controls.Add(this.label50);
            this.groupBox1.Controls.Add(this.label51);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.label46);
            this.groupBox1.Controls.Add(this.label47);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label64);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(8, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox1.Size = new System.Drawing.Size(1008, 532);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Help Details";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(656, 511);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(170, 13);
            this.label103.TabIndex = 187;
            this.label103.Text = "Apply Percentage Amount Charge";
            this.label103.Visible = false;
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.BackColor = System.Drawing.Color.Transparent;
            this.label104.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.ForeColor = System.Drawing.Color.Red;
            this.label104.Location = new System.Drawing.Point(566, 511);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(46, 13);
            this.label104.TabIndex = 186;
            this.label104.Text = "Alt + F6";
            this.label104.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(566, 240);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 184;
            this.label20.Text = "Alt + F11";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(656, 240);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(142, 13);
            this.label38.TabIndex = 185;
            this.label38.Text = "Suspend LOST Reward Card";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(566, 222);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(52, 13);
            this.label39.TabIndex = 182;
            this.label39.Text = "Alt + F10";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(656, 222);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(156, 13);
            this.label54.TabIndex = 183;
            this.label54.Text = "Replace EXPIRED Reward Card";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(656, 186);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(151, 13);
            this.label55.TabIndex = 181;
            this.label55.Text = "Renew EXPIRED Reward Card";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.BackColor = System.Drawing.Color.Transparent;
            this.label66.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.ForeColor = System.Drawing.Color.Red;
            this.label66.Location = new System.Drawing.Point(566, 186);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(46, 13);
            this.label66.TabIndex = 180;
            this.label66.Text = "Alt + F8";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.Gray;
            this.label67.Location = new System.Drawing.Point(526, 152);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(211, 13);
            this.label67.TabIndex = 179;
            this.label67.Text = "Reward (Advantage) Card Functions";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.BackColor = System.Drawing.Color.Transparent;
            this.label80.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.ForeColor = System.Drawing.Color.Red;
            this.label80.Location = new System.Drawing.Point(566, 258);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(52, 13);
            this.label80.TabIndex = 177;
            this.label80.Text = "Alt + F12";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(656, 204);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(139, 13);
            this.label81.TabIndex = 176;
            this.label81.Text = "Replace LOST Reward Card";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.ForeColor = System.Drawing.Color.Blue;
            this.label99.Location = new System.Drawing.Point(656, 168);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(125, 13);
            this.label99.TabIndex = 175;
            this.label99.Text = "Issue NEW Reward Card";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.BackColor = System.Drawing.Color.Transparent;
            this.label100.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.ForeColor = System.Drawing.Color.Red;
            this.label100.Location = new System.Drawing.Point(566, 204);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(46, 13);
            this.label100.TabIndex = 174;
            this.label100.Text = "Alt + F9";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.BackColor = System.Drawing.Color.Transparent;
            this.label101.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label101.ForeColor = System.Drawing.Color.Red;
            this.label101.Location = new System.Drawing.Point(566, 168);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(46, 13);
            this.label101.TabIndex = 173;
            this.label101.Text = "Alt + F7";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(656, 258);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(153, 13);
            this.label102.TabIndex = 178;
            this.label102.Text = "Reactivate LOST Reward Card";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(526, 476);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 172;
            this.label6.Text = "Charge Functions";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.BackColor = System.Drawing.Color.Transparent;
            this.label97.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.ForeColor = System.Drawing.Color.Red;
            this.label97.Location = new System.Drawing.Point(566, 114);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(56, 13);
            this.label97.TabIndex = 170;
            this.label97.Text = "Ctrl + F11";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(656, 114);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(134, 13);
            this.label98.TabIndex = 171;
            this.label98.Text = "Suspend LOST Credit Card";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.BackColor = System.Drawing.Color.Transparent;
            this.label95.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.ForeColor = System.Drawing.Color.Red;
            this.label95.Location = new System.Drawing.Point(566, 95);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(56, 13);
            this.label95.TabIndex = 168;
            this.label95.Text = "Ctrl + F10";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(656, 95);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(148, 13);
            this.label96.TabIndex = 169;
            this.label96.Text = "Replace EXPIRED Credit Card";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(656, 57);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(143, 13);
            this.label93.TabIndex = 167;
            this.label93.Text = "Renew EXPIRED Credit Card";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.BackColor = System.Drawing.Color.Transparent;
            this.label94.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.ForeColor = System.Drawing.Color.Red;
            this.label94.Location = new System.Drawing.Point(566, 57);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(50, 13);
            this.label94.TabIndex = 166;
            this.label94.Text = "Ctrl + F8";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.ForeColor = System.Drawing.Color.Blue;
            this.label91.Location = new System.Drawing.Point(208, 511);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(44, 13);
            this.label91.TabIndex = 165;
            this.label91.Text = "Log-out";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.BackColor = System.Drawing.Color.Transparent;
            this.label92.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.ForeColor = System.Drawing.Color.Red;
            this.label92.Location = new System.Drawing.Point(74, 511);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(43, 13);
            this.label92.TabIndex = 164;
            this.label92.Text = "Ctrl + L";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(208, 394);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(223, 13);
            this.label89.TabIndex = 163;
            this.label89.Text = "Select Costumer (Swipe Reward/Credit Card)";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.Red;
            this.label90.Location = new System.Drawing.Point(74, 394);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(19, 13);
            this.label90.TabIndex = 162;
            this.label90.Text = "F6";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(208, 276);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(52, 13);
            this.label87.TabIndex = 161;
            this.label87.Text = "Void Item";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.BackColor = System.Drawing.Color.Transparent;
            this.label88.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.ForeColor = System.Drawing.Color.Red;
            this.label88.Location = new System.Drawing.Point(74, 276);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(57, 13);
            this.label88.TabIndex = 160;
            this.label88.Text = "Backspace";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.ForeColor = System.Drawing.Color.Blue;
            this.label85.Location = new System.Drawing.Point(208, 114);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(123, 13);
            this.label85.TabIndex = 159;
            this.label85.Text = "Customer\'s Price Inquiry";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.BackColor = System.Drawing.Color.Transparent;
            this.label86.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.ForeColor = System.Drawing.Color.Red;
            this.label86.Location = new System.Drawing.Point(74, 114);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(116, 13);
            this.label86.TabIndex = 158;
            this.label86.Text = "F2 (Terminal is Logout)";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.ForeColor = System.Drawing.Color.Blue;
            this.label83.Location = new System.Drawing.Point(656, 324);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(91, 13);
            this.label83.TabIndex = 157;
            this.label83.Text = "Print ORDER SLIP";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.BackColor = System.Drawing.Color.Transparent;
            this.label84.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.ForeColor = System.Drawing.Color.Red;
            this.label84.Location = new System.Drawing.Point(566, 324);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(44, 13);
            this.label84.TabIndex = 156;
            this.label84.Text = "Ctrl + S";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.ForeColor = System.Drawing.Color.Gray;
            this.label82.Location = new System.Drawing.Point(33, 476);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(192, 13);
            this.label82.TabIndex = 155;
            this.label82.Text = "Cashier(bagger/waiter) Function";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.ForeColor = System.Drawing.Color.Gray;
            this.label79.Location = new System.Drawing.Point(526, 276);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(105, 13);
            this.label79.TabIndex = 152;
            this.label79.Text = "Drawer Functions";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.ForeColor = System.Drawing.Color.Gray;
            this.label78.Location = new System.Drawing.Point(526, 22);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(176, 13);
            this.label78.TabIndex = 151;
            this.label78.Text = "In-House Credit Card Function";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.ForeColor = System.Drawing.Color.Gray;
            this.label77.Location = new System.Drawing.Point(33, 410);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(121, 13);
            this.label77.TabIndex = 150;
            this.label77.Text = "DISCOUNT Functions";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.ForeColor = System.Drawing.Color.Gray;
            this.label76.Location = new System.Drawing.Point(33, 292);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(143, 13);
            this.label76.TabIndex = 149;
            this.label76.Text = "TRANSACTION Functions";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.ForeColor = System.Drawing.Color.Gray;
            this.label75.Location = new System.Drawing.Point(33, 152);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(92, 13);
            this.label75.TabIndex = 148;
            this.label75.Text = "ITEM Functions";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.ForeColor = System.Drawing.Color.Gray;
            this.label74.Location = new System.Drawing.Point(33, 22);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(86, 13);
            this.label74.TabIndex = 147;
            this.label74.Text = "POS Functions";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.ForeColor = System.Drawing.Color.Blue;
            this.label70.Location = new System.Drawing.Point(208, 494);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(109, 13);
            this.label70.TabIndex = 146;
            this.label70.Text = "Select Bagger/Waiter";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.ForeColor = System.Drawing.Color.Red;
            this.label71.Location = new System.Drawing.Point(74, 494);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(55, 13);
            this.label71.TabIndex = 145;
            this.label71.Text = "Shift + F6";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(656, 494);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(176, 13);
            this.label72.TabIndex = 144;
            this.label72.Text = "Apply SUBTOTAL Additional Charge";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.ForeColor = System.Drawing.Color.Red;
            this.label73.Location = new System.Drawing.Point(566, 494);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(46, 13);
            this.label73.TabIndex = 143;
            this.label73.Text = "Alt + F6";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.ForeColor = System.Drawing.Color.Blue;
            this.label68.Location = new System.Drawing.Point(656, 308);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(98, 13);
            this.label68.TabIndex = 142;
            this.label68.Text = "Print Check-Out Bill";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.BackColor = System.Drawing.Color.Transparent;
            this.label69.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.ForeColor = System.Drawing.Color.Red;
            this.label69.Location = new System.Drawing.Point(566, 308);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(44, 13);
            this.label69.TabIndex = 141;
            this.label69.Text = "Ctrl + P";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(656, 410);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(119, 13);
            this.label48.TabIndex = 138;
            this.label48.Text = "Deposit Customer Fund";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.Red;
            this.label49.Location = new System.Drawing.Point(566, 410);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(61, 13);
            this.label49.TabIndex = 137;
            this.label49.Text = "Shift + F11";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(656, 376);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(88, 13);
            this.label42.TabIndex = 136;
            this.label42.Text = "Paid-Out amount";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.Red;
            this.label43.Location = new System.Drawing.Point(566, 376);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(61, 13);
            this.label43.TabIndex = 135;
            this.label43.Text = "Shift + F10";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(656, 394);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(179, 13);
            this.label40.TabIndex = 134;
            this.label40.Text = "Withhold / Receive-On-Acc. amount";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.Red;
            this.label41.Location = new System.Drawing.Point(566, 394);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(25, 13);
            this.label41.TabIndex = 133;
            this.label41.Text = "F11";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.label65.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.ForeColor = System.Drawing.Color.Red;
            this.label65.Location = new System.Drawing.Point(566, 133);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(56, 13);
            this.label65.TabIndex = 131;
            this.label65.Text = "Ctrl + F12";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(208, 445);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(180, 13);
            this.label62.TabIndex = 130;
            this.label62.Text = "Apply discounts to all punched items";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.BackColor = System.Drawing.Color.Transparent;
            this.label63.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.Red;
            this.label63.Location = new System.Drawing.Point(74, 445);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(55, 13);
            this.label63.TabIndex = 129;
            this.label63.Text = "Shift + F4";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.ForeColor = System.Drawing.Color.Blue;
            this.label60.Location = new System.Drawing.Point(208, 204);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(84, 13);
            this.label60.TabIndex = 128;
            this.label60.Text = "Change Amount";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.ForeColor = System.Drawing.Color.Red;
            this.label61.Location = new System.Drawing.Point(74, 204);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(46, 13);
            this.label61.TabIndex = 127;
            this.label61.Text = "Alt + F2";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.ForeColor = System.Drawing.Color.Blue;
            this.label58.Location = new System.Drawing.Point(208, 95);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(121, 13);
            this.label58.TabIndex = 126;
            this.label58.Text = "Reload System Defaults";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.Red;
            this.label59.Location = new System.Drawing.Point(74, 95);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(64, 13);
            this.label59.TabIndex = 125;
            this.label59.Text = "Ctrl + Enter";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.ForeColor = System.Drawing.Color.Blue;
            this.label56.Location = new System.Drawing.Point(208, 358);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(118, 13);
            this.label56.TabIndex = 124;
            this.label56.Text = "Enter CREDIT Payment";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.Red;
            this.label57.Location = new System.Drawing.Point(74, 358);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(69, 13);
            this.label57.TabIndex = 123;
            this.label57.Text = "Shift + Enter";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.ForeColor = System.Drawing.Color.Blue;
            this.label52.Location = new System.Drawing.Point(208, 341);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(101, 13);
            this.label52.TabIndex = 120;
            this.label52.Text = "Refund Transaction";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.Color.Transparent;
            this.label53.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.Red;
            this.label53.Location = new System.Drawing.Point(74, 341);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(45, 13);
            this.label53.TabIndex = 119;
            this.label53.Text = "Ctrl + R";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(656, 341);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(162, 13);
            this.label50.TabIndex = 118;
            this.label50.Text = "Enter Float or Beginning Balance";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.Red;
            this.label51.Location = new System.Drawing.Point(566, 341);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(53, 13);
            this.label51.TabIndex = 117;
            this.label51.Text = "Ctrl + Ins";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(208, 222);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(111, 13);
            this.label44.TabIndex = 114;
            this.label44.Text = "Cashier\'s price inquiry";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(74, 222);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(114, 13);
            this.label45.TabIndex = 113;
            this.label45.Text = "Left Arrow or Page Up";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(208, 240);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(123, 13);
            this.label46.TabIndex = 112;
            this.label46.Text = "Select item to purchase.";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.Red;
            this.label47.Location = new System.Drawing.Point(74, 240);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(134, 13);
            this.label47.TabIndex = 111;
            this.label47.Text = "Right Arrow or Page Down";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.ForeColor = System.Drawing.Color.Blue;
            this.label36.Location = new System.Drawing.Point(656, 292);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(71, 13);
            this.label36.TabIndex = 104;
            this.label36.Text = "Open Drawer";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.Red;
            this.label37.Location = new System.Drawing.Point(566, 292);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(46, 13);
            this.label37.TabIndex = 103;
            this.label37.Text = "Ctrl + O";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Blue;
            this.label34.Location = new System.Drawing.Point(208, 76);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(84, 13);
            this.label34.TabIndex = 102;
            this.label34.Text = "Initialize Z-Read";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.Red;
            this.label35.Location = new System.Drawing.Point(74, 76);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(22, 13);
            this.label35.TabIndex = 101;
            this.label35.Text = "Del";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.Blue;
            this.label32.Location = new System.Drawing.Point(656, 427);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(266, 13);
            this.label32.TabIndex = 100;
            this.label32.Text = "Cash Count (1 Cash Count Per Terminal / Per Cashier)";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(566, 427);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(22, 13);
            this.label33.TabIndex = 99;
            this.label33.Text = "Ins";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(208, 427);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(100, 13);
            this.label30.TabIndex = 98;
            this.label30.Text = "Apply item discount";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(74, 427);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(19, 13);
            this.label31.TabIndex = 97;
            this.label31.Text = "F4";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(208, 376);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(86, 13);
            this.label29.TabIndex = 96;
            this.label29.Text = "Void Transaction";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(208, 57);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(119, 13);
            this.label28.TabIndex = 95;
            this.label28.Text = "Closes an open window";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(208, 133);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(97, 13);
            this.label26.TabIndex = 94;
            this.label26.Text = "Show print window";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Blue;
            this.label25.Location = new System.Drawing.Point(208, 186);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(70, 13);
            this.label25.TabIndex = 93;
            this.label25.Text = "Change Price";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(656, 358);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(131, 13);
            this.label24.TabIndex = 92;
            this.label24.Text = "Disburse / Pick-up amount";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.Blue;
            this.label23.Location = new System.Drawing.Point(208, 463);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(131, 13);
            this.label23.TabIndex = 91;
            this.label23.Text = "Apply SUBTOTAL discount";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(208, 324);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(161, 13);
            this.label22.TabIndex = 90;
            this.label22.Text = "Resume suspended transaction.";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(208, 308);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(147, 13);
            this.label21.TabIndex = 89;
            this.label21.Text = "Suspend current transaction.";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(656, 76);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(131, 13);
            this.label19.TabIndex = 87;
            this.label19.Text = "Replace LOST Credit Card";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(656, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(117, 13);
            this.label18.TabIndex = 86;
            this.label18.Text = "Issue NEW Credit Card";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(208, 258);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 85;
            this.label17.Text = "Return Item";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(208, 168);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 13);
            this.label16.TabIndex = 84;
            this.label16.Text = "Change Quantity";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(208, 38);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(96, 13);
            this.label27.TabIndex = 83;
            this.label27.Text = "Show this window.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(74, 168);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 82;
            this.label15.Text = "F2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(74, 376);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 81;
            this.label14.Text = "F9";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(74, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 80;
            this.label13.Text = "Esc";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(74, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 79;
            this.label12.Text = "F12";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(74, 186);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 78;
            this.label11.Text = "Shift + F2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(566, 358);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 77;
            this.label10.Text = "F10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(74, 463);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 76;
            this.label9.Text = "F5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(74, 324);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 75;
            this.label8.Text = "F8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(74, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "F7";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(566, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "Ctrl + F9";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(566, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Ctrl + F7";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(74, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "F3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(74, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "F1";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(656, 133);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(145, 13);
            this.label64.TabIndex = 132;
            this.label64.Text = "Reactivate LOST Credit Card";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "AceSoft RetailPlus ™ Help";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(740, 34);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(176, 13);
            this.lblDescription.TabIndex = 91;
            this.lblDescription.Tag = "";
            this.lblDescription.Text = "Press Esc Key to close this window.";
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(877, 618);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(106, 83);
            this.cmdCancel.TabIndex = 92;
            this.cmdCancel.Text = "CLOSE";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // HelpWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "HelpWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.HelpWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HelpWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void HelpWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Help.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
        }

        private void HelpWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    this.Hide();
                    break;

            }
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
