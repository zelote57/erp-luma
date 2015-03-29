using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Management;
using System.Xml;

using AceSoft.RetailPlus.Reports;
using AceSoft.RetailPlus.Security;
using AceSoft.RetailPlus.Data;

using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.IO;    
using System.Drawing.Drawing2D;

namespace AceSoft.RetailPlus.Client.UI
{
    public class MainRestoWnd : MainWndExtension
	{
		#region Declarations
		
		private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainRestoWnd));
		private IContainer components;
		private Panel panLocked;
		private Label lblPress;
		private Label lblThisStation;
		private PictureBox imgIcon;
		private GroupBox grpTop;
		private Label lblTransactionNoName;
		private Label lblTransNo;
		private Label lblCustomer;
		private GroupBox grpBottom;
		private Label lblCompanyName;
		private Label lblCashierName;
		private Label lblTerminalNoName;
		private GroupBox grptxtBarcode;
		private TextBox txtBarCode;
		private GroupBox grpMarquee;
		private Label lblMessage;
		private GroupBox grpRLC;
		private Button cmdRLCClose;
		private Label lblMallForwarderStatus;
		private System.Windows.Forms.Timer tmrRLC;

		private KeyBoardHook.KeyBoardHook Hook;
		private int tempHeight = 0, tempWidth = 0;
		private Label lblAgent;
		private SubGroupButton cmdSubGroupRight;
		private MenuButton cmd1;
		private Label label16;
		private Label label17;
		private Label label18;
		private Label label19;
		private Label label20;
		private Label lblSubTotal;
		private Label lblTransDiscount;
		private Label lblCurrency;
		private Label lblTransCharge;
		private Label lblOrderType;
		private Label lblSubtotalName;
		private Label lblOrders;
		private MenuButton cmd2;
		private MenuButton cmd3;
		private MenuButton cmd6;
		private MenuButton cmd5;
		private MenuButton cmd4;
		private MenuButton cmd7;
		private MenuButton cmd9;
		private MenuButton cmd8;
		private MenuButton cmd10;
		private MenuButton cmd11;
		private DataGridTableStyle dgStyle;
		private DataGrid dgItems;
		private DataGridTextBoxColumn Commision;
        private DataGridTextBoxColumn RewardPoints;
        private DataGridTextBoxColumn ItemRemarks;
		private DataGridTextBoxColumn PercentageCommision;
		private DataGridTextBoxColumn OrderSlipPrinted;
        private DataGridTextBoxColumn OrderSlipPrinter1;
        private DataGridTextBoxColumn OrderSlipPrinter2;
        private DataGridTextBoxColumn OrderSlipPrinter3;
        private DataGridTextBoxColumn OrderSlipPrinter4;
        private DataGridTextBoxColumn OrderSlipPrinter5;
		private DataGridTextBoxColumn IncludeInSubtotalDiscount;
        private DataGridTextBoxColumn IsCreditChargeExcluded;
		private DataGridTextBoxColumn PurchaseAmount;
		private DataGridTextBoxColumn PurchasePrice;
		private DataGridTextBoxColumn PromoApplied;
		private DataGridTextBoxColumn PromoType;
		private DataGridTextBoxColumn PromoInPercent;
		private DataGridTextBoxColumn PromoValue;
		private DataGridTextBoxColumn PromoQuantity;
		private DataGridTextBoxColumn PackageQuantity;
		private DataGridTextBoxColumn MatrixPackageID;
		private DataGridTextBoxColumn ProductPackageID;
		private DataGridTextBoxColumn DiscountRemarks;
		private DataGridTextBoxColumn DiscountCode;
		private DataGridTextBoxColumn TransactionItemStat;
		private DataGridTextBoxColumn ProductSubGroup;
		private DataGridTextBoxColumn ProductGroup;
		private DataGridTextBoxColumn MatrixDescription;
		private DataGridTextBoxColumn VariationsMatrixID;
		private DataGridTextBoxColumn LocalTax;
		private DataGridTextBoxColumn EVAT;
		private DataGridTextBoxColumn VAT;
		private DataGridTextBoxColumn Amount;
		private DataGridTextBoxColumn ItemDiscountType;
		private DataGridTextBoxColumn ItemDiscount;
		private DataGridTextBoxColumn Discount;
		private DataGridTextBoxColumn Price;
		private DataGridTextBoxColumn Quantity;
		private DataGridTextBoxColumn ProductUnitCode;
		private DataGridTextBoxColumn ProductUnitID;
		private DataGridTextBoxColumn Description;
		private DataGridTextBoxColumn BarCode;
		private DataGridTextBoxColumn ProductCode;
		private DataGridTextBoxColumn ProductID;
		private DataGridTextBoxColumn PaxNo;
		private DataGridTextBoxColumn ItemNo;
		private DataGridTextBoxColumn TransactionItemsID;
		private Label lblAgentPositionDepartment;
		private Label lblCashier;
		private Label lblTerminalNo;
		private Label lblTransDate;
		private GroupBox grpSubGroup;
		private SubGroupButton cmdSubGroupLeft;
		private Button cmdPaxAdd;
		private Button cmdPaxDeduct;
		private GroupBox panSubTotal;
		private GroupBox grpItems;
		private TableLayoutPanel tblLayoutProducts;
		private GroupBox groupBox1;
		private Label lblMenu;
		private Button cmdProductRight;
		private Button cmdProductLeft;
		private Label lblItems;
		private GroupBox groupBox2;
		private TableLayoutPanel tblLayoutGroup;
		private Label lblServedBy;
		private MenuButton cmdExit;
		private MenuButton cmd12;
		private Thread MarqueeThread;

		#endregion
		
		#region Public Get/Set Properties

		public bool Locked
		{
			get { return mboLocked; }
			set { mboLocked = value; }
		}

		#endregion

		#region Constructors and Destructors

		public MainRestoWnd()
		{
			InitializeComponent();

            //initialized loadoptions
            this.LoadOptions();

			this.Lock();

			try
			{ MarqueeThread = new Thread(new ThreadStart(DrawMovingText)); }
			catch { }

            grpRLC.Visible = false;
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogo.jpg"); }
            catch { }
            //try
            //{ this.grpItems.BackgroundImage = new Bitmap(Application.StartupPath + "/images/CompanyLogo.jpg"); }
            //catch { }
            try
            { this.cmdRLCClose.Image = new Bitmap(Application.StartupPath + "/images/close.gif"); }
            catch { }

            Data.SysConfig clsSysConfig = new Data.SysConfig(mConnection, mTransaction);
            mConnection = clsSysConfig.Connection; mTransaction = clsSysConfig.Transaction;

            try
            { mclsSysConfigDetails = clsSysConfig.get_SysConfigDetails(); }
            catch (Exception ex)
            { clsEvent.AddErrorEventLn(ex); }
            clsSysConfig.CommitAndDispose();
		}
		protected override void Dispose(bool disposing)
		{
			if (mclsTerminalDetails.FORM_Behavior == FORM_Behavior.MODAL)
			{
				TaskBarWnd taskbar = new TaskBarWnd();
				taskbar.Show();
				Cursor.Show();
				Resolution ChangeRes = new Resolution(tempWidth, tempHeight);
			}
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
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.panLocked = new System.Windows.Forms.Panel();
            this.cmdExit = new AceSoft.RetailPlus.Client.MenuButton();
            this.lblPress = new System.Windows.Forms.Label();
            this.lblThisStation = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.grpTop = new System.Windows.Forms.GroupBox();
            this.lblServedBy = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblTransNo = new System.Windows.Forms.Label();
            this.lblTransactionNoName = new System.Windows.Forms.Label();
            this.grpBottom = new System.Windows.Forms.GroupBox();
            this.lblTerminalNo = new System.Windows.Forms.Label();
            this.lblTransDate = new System.Windows.Forms.Label();
            this.lblCashier = new System.Windows.Forms.Label();
            this.lblAgent = new System.Windows.Forms.Label();
            this.lblTerminalNoName = new System.Windows.Forms.Label();
            this.lblCashierName = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.grptxtBarcode = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.grpMarquee = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tmrRLC = new System.Windows.Forms.Timer(this.components);
            this.grpRLC = new System.Windows.Forms.GroupBox();
            this.cmdRLCClose = new System.Windows.Forms.Button();
            this.lblMallForwarderStatus = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblTransDiscount = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblTransCharge = new System.Windows.Forms.Label();
            this.lblOrderType = new System.Windows.Forms.Label();
            this.lblSubtotalName = new System.Windows.Forms.Label();
            this.lblOrders = new System.Windows.Forms.Label();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.Commision = new System.Windows.Forms.DataGridTextBoxColumn();
            this.RewardPoints = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemRemarks = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PercentageCommision = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinted = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinter1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinter2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinter3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinter4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinter5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.IncludeInSubtotalDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.IsCreditChargeExcluded = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PurchaseAmount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PurchasePrice = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoApplied = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoInPercent = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoValue = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoQuantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PackageQuantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixPackageID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductPackageID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DiscountRemarks = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DiscountCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionItemStat = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductSubGroup = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductGroup = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixDescription = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VariationsMatrixID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LocalTax = new System.Windows.Forms.DataGridTextBoxColumn();
            this.EVAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemDiscountType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PaxNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionItemsID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblAgentPositionDepartment = new System.Windows.Forms.Label();
            this.grpSubGroup = new System.Windows.Forms.GroupBox();
            this.tblLayoutGroup = new System.Windows.Forms.TableLayoutPanel();
            this.cmdSubGroupRight = new AceSoft.RetailPlus.Client.SubGroupButton();
            this.cmdSubGroupLeft = new AceSoft.RetailPlus.Client.SubGroupButton();
            this.cmdPaxAdd = new System.Windows.Forms.Button();
            this.cmdPaxDeduct = new System.Windows.Forms.Button();
            this.panSubTotal = new System.Windows.Forms.GroupBox();
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.cmdProductRight = new System.Windows.Forms.Button();
            this.cmdProductLeft = new System.Windows.Forms.Button();
            this.lblItems = new System.Windows.Forms.Label();
            this.tblLayoutProducts = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMenu = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmd11 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd10 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd7 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd9 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd8 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd6 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd1 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd2 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd3 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd4 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd5 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd12 = new AceSoft.RetailPlus.Client.MenuButton();
            this.panLocked.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpTop.SuspendLayout();
            this.grpBottom.SuspendLayout();
            this.grptxtBarcode.SuspendLayout();
            this.grpMarquee.SuspendLayout();
            this.grpRLC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.grpSubGroup.SuspendLayout();
            this.panSubTotal.SuspendLayout();
            this.grpItems.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panLocked
            // 
            this.panLocked.BackColor = System.Drawing.Color.White;
            this.panLocked.Controls.Add(this.cmdExit);
            this.panLocked.Controls.Add(this.lblPress);
            this.panLocked.Controls.Add(this.lblThisStation);
            this.panLocked.Controls.Add(this.imgIcon);
            this.panLocked.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panLocked.Location = new System.Drawing.Point(0, 225);
            this.panLocked.Name = "panLocked";
            this.panLocked.Size = new System.Drawing.Size(923, 283);
            this.panLocked.TabIndex = 65;
            this.panLocked.Click += new System.EventHandler(this.panLocked_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.Red;
            this.cmdExit.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdExit.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.cmdExit.GradientBottom = System.Drawing.Color.Maroon;
            this.cmdExit.GradientTop = System.Drawing.Color.Red;
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExit.Location = new System.Drawing.Point(950, 669);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(65, 40);
            this.cmdExit.TabIndex = 119;
            this.cmdExit.Text = "exit";
            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // lblPress
            // 
            this.lblPress.AutoSize = true;
            this.lblPress.BackColor = System.Drawing.Color.Transparent;
            this.lblPress.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPress.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPress.Location = new System.Drawing.Point(462, 304);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(325, 11);
            this.lblPress.TabIndex = 16;
            this.lblPress.Text = "Press the RestoPlus logo or tap anywhere to login in the system.";
            // 
            // lblThisStation
            // 
            this.lblThisStation.AutoSize = true;
            this.lblThisStation.BackColor = System.Drawing.Color.Transparent;
            this.lblThisStation.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThisStation.ForeColor = System.Drawing.Color.DarkRed;
            this.lblThisStation.Location = new System.Drawing.Point(477, 279);
            this.lblThisStation.Name = "lblThisStation";
            this.lblThisStation.Size = new System.Drawing.Size(241, 23);
            this.lblThisStation.TabIndex = 15;
            this.lblThisStation.Text = "NEXT COUNTER PLEASE";
            this.lblThisStation.Click += new System.EventHandler(this.lblThisStation_Click);
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.White;
            this.imgIcon.Location = new System.Drawing.Point(236, 176);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(220, 225);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 13;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // grpTop
            // 
            this.grpTop.Controls.Add(this.lblServedBy);
            this.grpTop.Controls.Add(this.lblCustomer);
            this.grpTop.Controls.Add(this.lblTransNo);
            this.grpTop.Controls.Add(this.lblTransactionNoName);
            this.grpTop.Location = new System.Drawing.Point(0, -7);
            this.grpTop.Name = "grpTop";
            this.grpTop.Size = new System.Drawing.Size(1018, 37);
            this.grpTop.TabIndex = 75;
            this.grpTop.TabStop = false;
            // 
            // lblServedBy
            // 
            this.lblServedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblServedBy.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServedBy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblServedBy.Location = new System.Drawing.Point(439, 9);
            this.lblServedBy.Name = "lblServedBy";
            this.lblServedBy.Size = new System.Drawing.Size(279, 26);
            this.lblServedBy.TabIndex = 58;
            this.lblServedBy.Text = "RestoPlus ™";
            this.lblServedBy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblServedBy.Click += new System.EventHandler(this.lblServedBy_Click);
            // 
            // lblCustomer
            // 
            this.lblCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCustomer.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCustomer.Location = new System.Drawing.Point(719, 9);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(295, 26);
            this.lblCustomer.TabIndex = 56;
            this.lblCustomer.Text = "RetailPlus Customer ™";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCustomer.Click += new System.EventHandler(this.lblCustomer_Click);
            // 
            // lblTransNo
            // 
            this.lblTransNo.BackColor = System.Drawing.Color.Transparent;
            this.lblTransNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransNo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTransNo.Location = new System.Drawing.Point(132, 9);
            this.lblTransNo.Name = "lblTransNo";
            this.lblTransNo.Size = new System.Drawing.Size(306, 26);
            this.lblTransNo.TabIndex = 55;
            this.lblTransNo.Text = "READY...";
            this.lblTransNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTransNo.Click += new System.EventHandler(this.lblTransNo_Click);
            // 
            // lblTransactionNoName
            // 
            this.lblTransactionNoName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransactionNoName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionNoName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTransactionNoName.Location = new System.Drawing.Point(2, 9);
            this.lblTransactionNoName.Name = "lblTransactionNoName";
            this.lblTransactionNoName.Size = new System.Drawing.Size(129, 26);
            this.lblTransactionNoName.TabIndex = 54;
            this.lblTransactionNoName.Text = "  Transaction No :";
            this.lblTransactionNoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpBottom
            // 
            this.grpBottom.Controls.Add(this.lblTerminalNo);
            this.grpBottom.Controls.Add(this.lblTransDate);
            this.grpBottom.Controls.Add(this.lblCashier);
            this.grpBottom.Controls.Add(this.lblAgent);
            this.grpBottom.Controls.Add(this.lblTerminalNoName);
            this.grpBottom.Controls.Add(this.lblCashierName);
            this.grpBottom.Controls.Add(this.lblCompanyName);
            this.grpBottom.Location = new System.Drawing.Point(1, 699);
            this.grpBottom.Name = "grpBottom";
            this.grpBottom.Size = new System.Drawing.Size(1017, 34);
            this.grpBottom.TabIndex = 76;
            this.grpBottom.TabStop = false;
            // 
            // lblTerminalNo
            // 
            this.lblTerminalNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTerminalNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalNo.Location = new System.Drawing.Point(810, 9);
            this.lblTerminalNo.Name = "lblTerminalNo";
            this.lblTerminalNo.Size = new System.Drawing.Size(49, 22);
            this.lblTerminalNo.TabIndex = 71;
            this.lblTerminalNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTransDate
            // 
            this.lblTransDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransDate.Location = new System.Drawing.Point(859, 9);
            this.lblTransDate.Name = "lblTransDate";
            this.lblTransDate.Size = new System.Drawing.Size(152, 22);
            this.lblTransDate.TabIndex = 70;
            this.lblTransDate.Text = "Jan. 01, 0001 12:00:00 AM";
            this.lblTransDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCashier
            // 
            this.lblCashier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCashier.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashier.Location = new System.Drawing.Point(320, 9);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(211, 22);
            this.lblCashier.TabIndex = 69;
            this.lblCashier.Text = "Administrator";
            this.lblCashier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAgent
            // 
            this.lblAgent.BackColor = System.Drawing.Color.DimGray;
            this.lblAgent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAgent.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgent.ForeColor = System.Drawing.Color.White;
            this.lblAgent.Location = new System.Drawing.Point(637, 9);
            this.lblAgent.Name = "lblAgent";
            this.lblAgent.Size = new System.Drawing.Size(173, 22);
            this.lblAgent.TabIndex = 68;
            this.lblAgent.Text = "  Agent";
            this.lblAgent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTerminalNoName
            // 
            this.lblTerminalNoName.BackColor = System.Drawing.Color.DimGray;
            this.lblTerminalNoName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTerminalNoName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalNoName.ForeColor = System.Drawing.Color.White;
            this.lblTerminalNoName.Location = new System.Drawing.Point(531, 9);
            this.lblTerminalNoName.Name = "lblTerminalNoName";
            this.lblTerminalNoName.Size = new System.Drawing.Size(105, 22);
            this.lblTerminalNoName.TabIndex = 66;
            this.lblTerminalNoName.Text = "  Terminal No.:";
            this.lblTerminalNoName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCashierName
            // 
            this.lblCashierName.BackColor = System.Drawing.Color.DimGray;
            this.lblCashierName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCashierName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashierName.ForeColor = System.Drawing.Color.White;
            this.lblCashierName.Location = new System.Drawing.Point(237, 9);
            this.lblCashierName.Name = "lblCashierName";
            this.lblCashierName.Size = new System.Drawing.Size(82, 22);
            this.lblCashierName.TabIndex = 63;
            this.lblCashierName.Text = "  Cashier : ";
            this.lblCashierName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.BackColor = System.Drawing.Color.SlateGray;
            this.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCompanyName.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.White;
            this.lblCompanyName.Location = new System.Drawing.Point(3, 9);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(233, 22);
            this.lblCompanyName.TabIndex = 62;
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCompanyName.Click += new System.EventHandler(this.lblCompanyName_Click);
            // 
            // grptxtBarcode
            // 
            this.grptxtBarcode.BackColor = System.Drawing.Color.White;
            this.grptxtBarcode.Controls.Add(this.label16);
            this.grptxtBarcode.Controls.Add(this.label17);
            this.grptxtBarcode.Controls.Add(this.label18);
            this.grptxtBarcode.Controls.Add(this.label19);
            this.grptxtBarcode.Controls.Add(this.label20);
            this.grptxtBarcode.Controls.Add(this.txtBarCode);
            this.grptxtBarcode.Location = new System.Drawing.Point(8, 640);
            this.grptxtBarcode.Name = "grptxtBarcode";
            this.grptxtBarcode.Size = new System.Drawing.Size(395, 33);
            this.grptxtBarcode.TabIndex = 77;
            this.grptxtBarcode.TabStop = false;
            this.grptxtBarcode.Visible = false;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.SystemColors.Control;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.Font = new System.Drawing.Font("Arial Narrow", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(577, 59);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(218, 81);
            this.label16.TabIndex = 62;
            this.label16.Text = "SUBTOTAL: REFUND";
            this.label16.Click += new System.EventHandler(this.lblSubtotalName_Click);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.Font = new System.Drawing.Font("Tahoma", 7.9F);
            this.label17.ForeColor = System.Drawing.Color.Crimson;
            this.label17.Location = new System.Drawing.Point(686, 121);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 17);
            this.label17.TabIndex = 72;
            this.label17.Text = "Less 10% / 10.00";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.SystemColors.Control;
            this.label18.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(579, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(213, 28);
            this.label18.TabIndex = 64;
            this.label18.Text = "PHP";
            this.label18.Click += new System.EventHandler(this.lblCurrency_Click);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.SystemColors.Control;
            this.label19.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(632, 97);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(159, 28);
            this.label19.TabIndex = 63;
            this.label19.Text = "0.0000000000";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label19.Click += new System.EventHandler(this.lblSubTotal_Click);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.SystemColors.Control;
            this.label20.Font = new System.Drawing.Font("Tahoma", 7.9F);
            this.label20.ForeColor = System.Drawing.Color.Crimson;
            this.label20.Location = new System.Drawing.Point(580, 121);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 17);
            this.label20.TabIndex = 78;
            this.label20.Text = "Plus 10% / 10.00";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBarCode
            // 
            this.txtBarCode.BackColor = System.Drawing.Color.White;
            this.txtBarCode.Enabled = false;
            this.txtBarCode.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.ForeColor = System.Drawing.Color.Black;
            this.txtBarCode.Location = new System.Drawing.Point(47, 3);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(348, 30);
            this.txtBarCode.TabIndex = 81;
            // 
            // grpMarquee
            // 
            this.grpMarquee.Controls.Add(this.lblMessage);
            this.grpMarquee.Location = new System.Drawing.Point(1, 669);
            this.grpMarquee.Name = "grpMarquee";
            this.grpMarquee.Size = new System.Drawing.Size(923, 36);
            this.grpMarquee.TabIndex = 82;
            this.grpMarquee.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(1, 10);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(915, 24);
            this.lblMessage.TabIndex = 80;
            this.lblMessage.Text = " Your suggestive selling message and/or description.  Your suggestive selling mes" +
    "sage and/or    .dasdasdas";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrRLC
            // 
            this.tmrRLC.Interval = 1;
            this.tmrRLC.Tick += new System.EventHandler(this.tmrRLC_Tick);
            // 
            // grpRLC
            // 
            this.grpRLC.BackColor = System.Drawing.Color.White;
            this.grpRLC.Controls.Add(this.cmdRLCClose);
            this.grpRLC.Controls.Add(this.lblMallForwarderStatus);
            this.grpRLC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRLC.ForeColor = System.Drawing.Color.Blue;
            this.grpRLC.Location = new System.Drawing.Point(439, 642);
            this.grpRLC.Name = "grpRLC";
            this.grpRLC.Size = new System.Drawing.Size(485, 33);
            this.grpRLC.TabIndex = 97;
            this.grpRLC.TabStop = false;
            // 
            // cmdRLCClose
            // 
            this.cmdRLCClose.AutoSize = true;
            this.cmdRLCClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdRLCClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdRLCClose.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRLCClose.ForeColor = System.Drawing.Color.White;
            this.cmdRLCClose.Location = new System.Drawing.Point(470, 11);
            this.cmdRLCClose.Name = "cmdRLCClose";
            this.cmdRLCClose.Size = new System.Drawing.Size(39, 18);
            this.cmdRLCClose.TabIndex = 92;
            this.cmdRLCClose.UseVisualStyleBackColor = true;
            this.cmdRLCClose.Click += new System.EventHandler(this.cmdRLCClose_Click);
            // 
            // lblMallForwarderStatus
            // 
            this.lblMallForwarderStatus.AutoSize = true;
            this.lblMallForwarderStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblMallForwarderStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMallForwarderStatus.ForeColor = System.Drawing.Color.Green;
            this.lblMallForwarderStatus.Location = new System.Drawing.Point(4, 12);
            this.lblMallForwarderStatus.Name = "lblMallForwarderStatus";
            this.lblMallForwarderStatus.Size = new System.Drawing.Size(302, 16);
            this.lblMallForwarderStatus.TabIndex = 91;
            this.lblMallForwarderStatus.Text = "RLC Notification: Trying to send unsent files...";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotal.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.Color.Red;
            this.lblSubTotal.Location = new System.Drawing.Point(151, 42);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(280, 28);
            this.lblSubTotal.TabIndex = 63;
            this.lblSubTotal.Text = "0.0000000000";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransDiscount
            // 
            this.lblTransDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblTransDiscount.Font = new System.Drawing.Font("Tahoma", 7.9F);
            this.lblTransDiscount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblTransDiscount.Location = new System.Drawing.Point(221, 71);
            this.lblTransDiscount.Name = "lblTransDiscount";
            this.lblTransDiscount.Size = new System.Drawing.Size(210, 15);
            this.lblTransDiscount.TabIndex = 72;
            this.lblTransDiscount.Text = "Less 10% / 10.00";
            this.lblTransDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrency
            // 
            this.lblCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrency.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.ForeColor = System.Drawing.Color.White;
            this.lblCurrency.Location = new System.Drawing.Point(7, 43);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(117, 28);
            this.lblCurrency.TabIndex = 64;
            this.lblCurrency.Text = "PHP";
            // 
            // lblTransCharge
            // 
            this.lblTransCharge.BackColor = System.Drawing.Color.Transparent;
            this.lblTransCharge.Font = new System.Drawing.Font("Tahoma", 7.9F);
            this.lblTransCharge.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblTransCharge.Location = new System.Drawing.Point(7, 71);
            this.lblTransCharge.Name = "lblTransCharge";
            this.lblTransCharge.Size = new System.Drawing.Size(229, 15);
            this.lblTransCharge.TabIndex = 3;
            this.lblTransCharge.Text = "Plus 10% / 10.00";
            this.lblTransCharge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderType
            // 
            this.lblOrderType.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblOrderType.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblOrderType.Location = new System.Drawing.Point(217, 10);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(214, 19);
            this.lblOrderType.TabIndex = 4;
            this.lblOrderType.Text = "DELIVERY";
            this.lblOrderType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtotalName
            // 
            this.lblSubtotalName.AutoSize = true;
            this.lblSubtotalName.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtotalName.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalName.ForeColor = System.Drawing.Color.White;
            this.lblSubtotalName.Location = new System.Drawing.Point(7, 10);
            this.lblSubtotalName.Name = "lblSubtotalName";
            this.lblSubtotalName.Size = new System.Drawing.Size(218, 23);
            this.lblSubtotalName.TabIndex = 1;
            this.lblSubtotalName.Text = "SUBTOTAL: REFUND";
            // 
            // lblOrders
            // 
            this.lblOrders.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOrders.BackColor = System.Drawing.Color.Gold;
            this.lblOrders.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrders.ForeColor = System.Drawing.Color.Black;
            this.lblOrders.Location = new System.Drawing.Point(2, 9);
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Size = new System.Drawing.Size(433, 32);
            this.lblOrders.TabIndex = 111;
            this.lblOrders.Text = "CUSTOMER\'s ORDER";
            this.lblOrders.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOrders.Click += new System.EventHandler(this.lblOrders_Click);
            // 
            // dgStyle
            // 
            this.dgStyle.AllowSorting = false;
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.BackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgItems;
            this.dgStyle.GridLineColor = System.Drawing.Color.Blue;
            this.dgStyle.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
            this.dgStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblProducts";
            this.dgStyle.PreferredColumnWidth = 0;
            this.dgStyle.PreferredRowHeight = 50;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.White;
            this.dgItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgItems.BackColor = System.Drawing.Color.White;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgItems.CaptionBackColor = System.Drawing.Color.White;
            this.dgItems.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgItems.CaptionVisible = false;
            this.dgItems.CausesValidation = false;
            this.dgItems.DataMember = "";
            this.dgItems.FlatMode = true;
            this.dgItems.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.GridLineColor = System.Drawing.Color.Blue;
            this.dgItems.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgItems.HeaderFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderForeColor = System.Drawing.Color.White;
            this.dgItems.Location = new System.Drawing.Point(2, 40);
            this.dgItems.Name = "dgItems";
            this.dgItems.ParentRowsBackColor = System.Drawing.Color.Blue;
            this.dgItems.PreferredRowHeight = 100;
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowHeaderWidth = 40;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(433, 388);
            this.dgItems.TabIndex = 49;
            this.dgItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgItems.TabStop = false;
            this.dgItems.CurrentCellChanged += new System.EventHandler(this.dgItems_CurrentCellChanged);
            this.dgItems.Click += new System.EventHandler(this.dgItems_Click);
            this.dgItems.GotFocus += new System.EventHandler(this.dgItems_GotFocus);
            this.dgItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgItems_KeyUp);
            this.dgItems.LostFocus += new System.EventHandler(this.dgItems_LostFocus);
            this.dgItems.MouseLeave += new System.EventHandler(this.dgItems_MouseLeave);
            this.dgItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgItems_MouseMove);
            // 
            // Commision
            // 
            this.Commision.Format = "";
            this.Commision.FormatInfo = null;
            this.Commision.MappingName = "Commision";
            this.Commision.NullText = "";
            this.Commision.ReadOnly = true;
            this.Commision.Width = 0;
            // 
            // RewardPoints
            // 
            this.RewardPoints.Format = "";
            this.RewardPoints.FormatInfo = null;
            this.RewardPoints.MappingName = "RewardPoints";
            this.RewardPoints.NullText = "";
            this.RewardPoints.ReadOnly = true;
            this.RewardPoints.Width = 0;
            // 
            // ItemRemarks
            // 
            this.ItemRemarks.Format = "";
            this.ItemRemarks.FormatInfo = null;
            this.ItemRemarks.MappingName = "ItemRemarks";
            this.ItemRemarks.NullText = "";
            this.ItemRemarks.ReadOnly = true;
            this.ItemRemarks.Width = 0;
            // 
            // PercentageCommision
            // 
            this.PercentageCommision.Format = "";
            this.PercentageCommision.FormatInfo = null;
            this.PercentageCommision.MappingName = "PercentageCommision";
            this.PercentageCommision.NullText = "";
            this.PercentageCommision.ReadOnly = true;
            this.PercentageCommision.Width = 0;
            // 
            // OrderSlipPrinted
            // 
            this.OrderSlipPrinted.Format = "";
            this.OrderSlipPrinted.FormatInfo = null;
            this.OrderSlipPrinted.MappingName = "OrderSlipPrinted";
            this.OrderSlipPrinted.NullText = "";
            this.OrderSlipPrinted.ReadOnly = true;
            this.OrderSlipPrinted.Width = 0;
            // 
            // OrderSlipPrinter1
            // 
            this.OrderSlipPrinter1.Format = "";
            this.OrderSlipPrinter1.FormatInfo = null;
            this.OrderSlipPrinter1.MappingName = "OrderSlipPrinter1";
            this.OrderSlipPrinter1.NullText = "";
            this.OrderSlipPrinter1.ReadOnly = true;
            this.OrderSlipPrinter1.Width = 0;
            // 
            // OrderSlipPrinter2
            // 
            this.OrderSlipPrinter2.Format = "";
            this.OrderSlipPrinter2.FormatInfo = null;
            this.OrderSlipPrinter2.MappingName = "OrderSlipPrinter2";
            this.OrderSlipPrinter2.NullText = "";
            this.OrderSlipPrinter2.ReadOnly = true;
            this.OrderSlipPrinter2.Width = 0;
            // 
            // OrderSlipPrinter3
            // 
            this.OrderSlipPrinter3.Format = "";
            this.OrderSlipPrinter3.FormatInfo = null;
            this.OrderSlipPrinter3.MappingName = "OrderSlipPrinter3";
            this.OrderSlipPrinter3.NullText = "";
            this.OrderSlipPrinter3.ReadOnly = true;
            this.OrderSlipPrinter3.Width = 0;
            // 
            // OrderSlipPrinter4
            // 
            this.OrderSlipPrinter4.Format = "";
            this.OrderSlipPrinter4.FormatInfo = null;
            this.OrderSlipPrinter4.MappingName = "OrderSlipPrinter4";
            this.OrderSlipPrinter4.NullText = "";
            this.OrderSlipPrinter4.ReadOnly = true;
            this.OrderSlipPrinter4.Width = 0;
            // 
            // OrderSlipPrinter5
            // 
            this.OrderSlipPrinter5.Format = "";
            this.OrderSlipPrinter5.FormatInfo = null;
            this.OrderSlipPrinter5.MappingName = "OrderSlipPrinter5";
            this.OrderSlipPrinter5.NullText = "";
            this.OrderSlipPrinter5.ReadOnly = true;
            this.OrderSlipPrinter5.Width = 0;
            // 
            // IncludeInSubtotalDiscount
            // 
            this.IncludeInSubtotalDiscount.Format = "";
            this.IncludeInSubtotalDiscount.FormatInfo = null;
            this.IncludeInSubtotalDiscount.MappingName = "IncludeInSubtotalDiscount";
            this.IncludeInSubtotalDiscount.NullText = "";
            this.IncludeInSubtotalDiscount.ReadOnly = true;
            this.IncludeInSubtotalDiscount.Width = 0;
            // 
            // IsCreditChargeExcluded
            // 
            this.IsCreditChargeExcluded.Format = "";
            this.IsCreditChargeExcluded.FormatInfo = null;
            this.IsCreditChargeExcluded.MappingName = "IsCreditChargeExcluded";
            this.IsCreditChargeExcluded.NullText = "";
            this.IsCreditChargeExcluded.ReadOnly = true;
            this.IsCreditChargeExcluded.Width = 0;
            // 
            // PurchaseAmount
            // 
            this.PurchaseAmount.Format = "";
            this.PurchaseAmount.FormatInfo = null;
            this.PurchaseAmount.MappingName = "PurchaseAmount";
            this.PurchaseAmount.NullText = "";
            this.PurchaseAmount.ReadOnly = true;
            this.PurchaseAmount.Width = 0;
            // 
            // PurchasePrice
            // 
            this.PurchasePrice.Format = "";
            this.PurchasePrice.FormatInfo = null;
            this.PurchasePrice.MappingName = "PurchasePrice";
            this.PurchasePrice.NullText = "";
            this.PurchasePrice.ReadOnly = true;
            this.PurchasePrice.Width = 0;
            // 
            // PromoApplied
            // 
            this.PromoApplied.Format = "";
            this.PromoApplied.FormatInfo = null;
            this.PromoApplied.MappingName = "PromoApplied";
            this.PromoApplied.NullText = "";
            this.PromoApplied.ReadOnly = true;
            this.PromoApplied.Width = 0;
            // 
            // PromoType
            // 
            this.PromoType.Format = "";
            this.PromoType.FormatInfo = null;
            this.PromoType.MappingName = "PromoType";
            this.PromoType.NullText = "";
            this.PromoType.ReadOnly = true;
            this.PromoType.Width = 0;
            // 
            // PromoInPercent
            // 
            this.PromoInPercent.Format = "";
            this.PromoInPercent.FormatInfo = null;
            this.PromoInPercent.MappingName = "PromoInPercent";
            this.PromoInPercent.NullText = "0";
            this.PromoInPercent.ReadOnly = true;
            this.PromoInPercent.Width = 0;
            // 
            // PromoValue
            // 
            this.PromoValue.Format = "";
            this.PromoValue.FormatInfo = null;
            this.PromoValue.MappingName = "PromoValue";
            this.PromoValue.NullText = "0";
            this.PromoValue.ReadOnly = true;
            this.PromoValue.Width = 0;
            // 
            // PromoQuantity
            // 
            this.PromoQuantity.Format = "";
            this.PromoQuantity.FormatInfo = null;
            this.PromoQuantity.MappingName = "PromoQuantity";
            this.PromoQuantity.NullText = "0";
            this.PromoQuantity.ReadOnly = true;
            this.PromoQuantity.Width = 0;
            // 
            // PackageQuantity
            // 
            this.PackageQuantity.Format = "";
            this.PackageQuantity.FormatInfo = null;
            this.PackageQuantity.MappingName = "PackageQuantity";
            this.PackageQuantity.NullText = "";
            this.PackageQuantity.ReadOnly = true;
            this.PackageQuantity.Width = 0;
            // 
            // MatrixPackageID
            // 
            this.MatrixPackageID.Format = "";
            this.MatrixPackageID.FormatInfo = null;
            this.MatrixPackageID.MappingName = "MatrixPackageID";
            this.MatrixPackageID.NullText = "";
            this.MatrixPackageID.ReadOnly = true;
            this.MatrixPackageID.Width = 0;
            // 
            // ProductPackageID
            // 
            this.ProductPackageID.Format = "";
            this.ProductPackageID.FormatInfo = null;
            this.ProductPackageID.MappingName = "ProductPackageID";
            this.ProductPackageID.NullText = "";
            this.ProductPackageID.ReadOnly = true;
            this.ProductPackageID.Width = 0;
            // 
            // DiscountRemarks
            // 
            this.DiscountRemarks.Format = "";
            this.DiscountRemarks.FormatInfo = null;
            this.DiscountRemarks.MappingName = "DiscountRemarks";
            this.DiscountRemarks.NullText = "";
            this.DiscountRemarks.ReadOnly = true;
            this.DiscountRemarks.Width = 0;
            // 
            // DiscountCode
            // 
            this.DiscountCode.Format = "";
            this.DiscountCode.FormatInfo = null;
            this.DiscountCode.MappingName = "DiscountCode";
            this.DiscountCode.NullText = "";
            this.DiscountCode.ReadOnly = true;
            this.DiscountCode.Width = 0;
            // 
            // TransactionItemStat
            // 
            this.TransactionItemStat.Format = "";
            this.TransactionItemStat.FormatInfo = null;
            this.TransactionItemStat.MappingName = "TransactionItemStat";
            this.TransactionItemStat.NullText = "";
            this.TransactionItemStat.ReadOnly = true;
            this.TransactionItemStat.Width = 0;
            // 
            // ProductSubGroup
            // 
            this.ProductSubGroup.Format = "";
            this.ProductSubGroup.FormatInfo = null;
            this.ProductSubGroup.MappingName = "ProductSubGroup";
            this.ProductSubGroup.NullText = "";
            this.ProductSubGroup.ReadOnly = true;
            this.ProductSubGroup.Width = 0;
            // 
            // ProductGroup
            // 
            this.ProductGroup.Format = "";
            this.ProductGroup.FormatInfo = null;
            this.ProductGroup.MappingName = "ProductGroup";
            this.ProductGroup.NullText = "";
            this.ProductGroup.ReadOnly = true;
            this.ProductGroup.Width = 0;
            // 
            // MatrixDescription
            // 
            this.MatrixDescription.Format = "";
            this.MatrixDescription.FormatInfo = null;
            this.MatrixDescription.MappingName = "MatrixDescription";
            this.MatrixDescription.NullText = "";
            this.MatrixDescription.ReadOnly = true;
            this.MatrixDescription.Width = 0;
            // 
            // VariationsMatrixID
            // 
            this.VariationsMatrixID.Format = "";
            this.VariationsMatrixID.FormatInfo = null;
            this.VariationsMatrixID.MappingName = "VariationsMatrixID";
            this.VariationsMatrixID.NullText = "";
            this.VariationsMatrixID.ReadOnly = true;
            this.VariationsMatrixID.Width = 0;
            // 
            // LocalTax
            // 
            this.LocalTax.Format = "";
            this.LocalTax.FormatInfo = null;
            this.LocalTax.MappingName = "LocalTax";
            this.LocalTax.NullText = "";
            this.LocalTax.ReadOnly = true;
            this.LocalTax.Width = 0;
            // 
            // EVAT
            // 
            this.EVAT.Format = "";
            this.EVAT.FormatInfo = null;
            this.EVAT.MappingName = "EVAT";
            this.EVAT.NullText = "";
            this.EVAT.ReadOnly = true;
            this.EVAT.Width = 0;
            // 
            // VAT
            // 
            this.VAT.Format = "";
            this.VAT.FormatInfo = null;
            this.VAT.MappingName = "VAT";
            this.VAT.NullText = "";
            this.VAT.ReadOnly = true;
            this.VAT.Width = 0;
            // 
            // Amount
            // 
            this.Amount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Amount.Format = "#,##0.#0";
            this.Amount.FormatInfo = null;
            this.Amount.HeaderText = "Amount";
            this.Amount.MappingName = "Amount";
            this.Amount.NullText = "";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 0;
            // 
            // ItemDiscountType
            // 
            this.ItemDiscountType.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItemDiscountType.Format = "";
            this.ItemDiscountType.FormatInfo = null;
            this.ItemDiscountType.MappingName = "ItemDiscountType";
            this.ItemDiscountType.NullText = "";
            this.ItemDiscountType.ReadOnly = true;
            this.ItemDiscountType.Width = 0;
            // 
            // ItemDiscount
            // 
            this.ItemDiscount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItemDiscount.Format = "###,##0";
            this.ItemDiscount.FormatInfo = null;
            this.ItemDiscount.MappingName = "ItemDiscount";
            this.ItemDiscount.NullText = "";
            this.ItemDiscount.ReadOnly = true;
            this.ItemDiscount.Width = 0;
            // 
            // Discount
            // 
            this.Discount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Discount.Format = "###,##0";
            this.Discount.FormatInfo = null;
            this.Discount.MappingName = "Discount";
            this.Discount.NullText = "";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 0;
            // 
            // Price
            // 
            this.Price.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Price.Format = "###,##0.#0";
            this.Price.FormatInfo = null;
            this.Price.HeaderText = "Price";
            this.Price.MappingName = "Price";
            this.Price.NullText = "";
            this.Price.ReadOnly = true;
            this.Price.Width = 0;
            // 
            // Quantity
            // 
            this.Quantity.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Quantity.Format = "###,##0.#0";
            this.Quantity.FormatInfo = null;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.MappingName = "Quantity";
            this.Quantity.NullText = "";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 0;
            // 
            // ProductUnitCode
            // 
            this.ProductUnitCode.Format = "";
            this.ProductUnitCode.FormatInfo = null;
            this.ProductUnitCode.MappingName = "ProductUnitCode";
            this.ProductUnitCode.NullText = "";
            this.ProductUnitCode.ReadOnly = true;
            this.ProductUnitCode.Width = 0;
            // 
            // ProductUnitID
            // 
            this.ProductUnitID.Format = "";
            this.ProductUnitID.FormatInfo = null;
            this.ProductUnitID.MappingName = "ProductUnitID";
            this.ProductUnitID.NullText = "";
            this.ProductUnitID.ReadOnly = true;
            this.ProductUnitID.Width = 0;
            // 
            // Description
            // 
            this.Description.Format = "";
            this.Description.FormatInfo = null;
            this.Description.HeaderText = "Description";
            this.Description.MappingName = "Description";
            this.Description.NullText = "";
            this.Description.ReadOnly = true;
            this.Description.Width = 0;
            // 
            // BarCode
            // 
            this.BarCode.Format = "";
            this.BarCode.FormatInfo = null;
            this.BarCode.MappingName = "BarCode";
            this.BarCode.NullText = "";
            this.BarCode.ReadOnly = true;
            this.BarCode.Width = 0;
            // 
            // ProductCode
            // 
            this.ProductCode.Format = "";
            this.ProductCode.FormatInfo = null;
            this.ProductCode.MappingName = "ProductCode";
            this.ProductCode.NullText = "";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 0;
            // 
            // ProductID
            // 
            this.ProductID.Format = "";
            this.ProductID.FormatInfo = null;
            this.ProductID.MappingName = "ProductID";
            this.ProductID.NullText = "";
            this.ProductID.ReadOnly = true;
            this.ProductID.Width = 0;
            // 
            // ItemNo
            // 
            this.ItemNo.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.ItemNo.Format = "";
            this.ItemNo.FormatInfo = null;
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.MappingName = "ItemNo";
            this.ItemNo.NullText = "";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Width = 0;
            // 
            // PaxNo
            // 
            this.PaxNo.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.PaxNo.Format = "";
            this.PaxNo.FormatInfo = null;
            this.PaxNo.HeaderText = "PaxNo";
            this.PaxNo.MappingName = "PaxNo";
            this.PaxNo.NullText = "";
            this.PaxNo.ReadOnly = true;
            this.PaxNo.Width = 0;
            // 
            // TransactionItemsID
            // 
            this.TransactionItemsID.Format = "";
            this.TransactionItemsID.FormatInfo = null;
            this.TransactionItemsID.MappingName = "TransactionItemsID";
            this.TransactionItemsID.NullText = "";
            this.TransactionItemsID.ReadOnly = true;
            this.TransactionItemsID.Width = 0;
            // 
            // lblAgentPositionDepartment
            // 
            this.lblAgentPositionDepartment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAgentPositionDepartment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentPositionDepartment.ForeColor = System.Drawing.Color.White;
            this.lblAgentPositionDepartment.Location = new System.Drawing.Point(86, 559);
            this.lblAgentPositionDepartment.Name = "lblAgentPositionDepartment";
            this.lblAgentPositionDepartment.Size = new System.Drawing.Size(210, 22);
            this.lblAgentPositionDepartment.TabIndex = 123;
            this.lblAgentPositionDepartment.Text = "Administrator";
            this.lblAgentPositionDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAgentPositionDepartment.Visible = false;
            // 
            // grpSubGroup
            // 
            this.grpSubGroup.BackColor = System.Drawing.Color.Transparent;
            this.grpSubGroup.Controls.Add(this.tblLayoutGroup);
            this.grpSubGroup.Controls.Add(this.cmdSubGroupRight);
            this.grpSubGroup.Controls.Add(this.cmdSubGroupLeft);
            this.grpSubGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSubGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpSubGroup.Location = new System.Drawing.Point(1, 64);
            this.grpSubGroup.Name = "grpSubGroup";
            this.grpSubGroup.Size = new System.Drawing.Size(923, 95);
            this.grpSubGroup.TabIndex = 53;
            this.grpSubGroup.TabStop = false;
            // 
            // tblLayoutGroup
            // 
            this.tblLayoutGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLayoutGroup.BackColor = System.Drawing.Color.Transparent;
            this.tblLayoutGroup.CausesValidation = false;
            this.tblLayoutGroup.ColumnCount = 7;
            this.tblLayoutGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayoutGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tblLayoutGroup.Location = new System.Drawing.Point(62, 7);
            this.tblLayoutGroup.Name = "tblLayoutGroup";
            this.tblLayoutGroup.RowCount = 1;
            this.tblLayoutGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutGroup.Size = new System.Drawing.Size(799, 88);
            this.tblLayoutGroup.TabIndex = 115;
            // 
            // cmdSubGroupRight
            // 
            this.cmdSubGroupRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdSubGroupRight.BackColor = System.Drawing.Color.Red;
            this.cmdSubGroupRight.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdSubGroupRight.FlatAppearance.BorderSize = 0;
            this.cmdSubGroupRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSubGroupRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.cmdSubGroupRight.GradientBottom = System.Drawing.Color.Red;
            this.cmdSubGroupRight.GradientTop = System.Drawing.Color.Gold;
            this.cmdSubGroupRight.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSubGroupRight.Location = new System.Drawing.Point(863, 10);
            this.cmdSubGroupRight.Name = "cmdSubGroupRight";
            this.cmdSubGroupRight.Size = new System.Drawing.Size(57, 82);
            this.cmdSubGroupRight.TabIndex = 102;
            this.cmdSubGroupRight.Text = "}";
            this.cmdSubGroupRight.UseVisualStyleBackColor = false;
            this.cmdSubGroupRight.Visible = false;
            this.cmdSubGroupRight.Click += new System.EventHandler(this.cmdSubGroupRight_Click);
            // 
            // cmdSubGroupLeft
            // 
            this.cmdSubGroupLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdSubGroupLeft.BackColor = System.Drawing.Color.Red;
            this.cmdSubGroupLeft.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdSubGroupLeft.FlatAppearance.BorderSize = 0;
            this.cmdSubGroupLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.cmdSubGroupLeft.GradientBottom = System.Drawing.Color.Red;
            this.cmdSubGroupLeft.GradientTop = System.Drawing.Color.Gold;
            this.cmdSubGroupLeft.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSubGroupLeft.Location = new System.Drawing.Point(3, 10);
            this.cmdSubGroupLeft.Name = "cmdSubGroupLeft";
            this.cmdSubGroupLeft.Size = new System.Drawing.Size(57, 82);
            this.cmdSubGroupLeft.TabIndex = 114;
            this.cmdSubGroupLeft.Text = "{";
            this.cmdSubGroupLeft.UseVisualStyleBackColor = false;
            this.cmdSubGroupLeft.Visible = false;
            this.cmdSubGroupLeft.Click += new System.EventHandler(this.cmdSubGroupLeft_Click);
            // 
            // cmdPaxAdd
            // 
            this.cmdPaxAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdPaxAdd.AutoEllipsis = true;
            this.cmdPaxAdd.BackColor = System.Drawing.Color.Gold;
            this.cmdPaxAdd.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdPaxAdd.FlatAppearance.BorderSize = 0;
            this.cmdPaxAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdPaxAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.cmdPaxAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPaxAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPaxAdd.Location = new System.Drawing.Point(4, 11);
            this.cmdPaxAdd.Name = "cmdPaxAdd";
            this.cmdPaxAdd.Size = new System.Drawing.Size(50, 28);
            this.cmdPaxAdd.TabIndex = 125;
            this.cmdPaxAdd.Text = "+";
            this.cmdPaxAdd.UseVisualStyleBackColor = false;
            this.cmdPaxAdd.Click += new System.EventHandler(this.cmdPaxAdd_Click);
            // 
            // cmdPaxDeduct
            // 
            this.cmdPaxDeduct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdPaxDeduct.AutoEllipsis = true;
            this.cmdPaxDeduct.BackColor = System.Drawing.Color.Gold;
            this.cmdPaxDeduct.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdPaxDeduct.FlatAppearance.BorderSize = 0;
            this.cmdPaxDeduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdPaxDeduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.cmdPaxDeduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPaxDeduct.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPaxDeduct.Location = new System.Drawing.Point(381, 11);
            this.cmdPaxDeduct.Name = "cmdPaxDeduct";
            this.cmdPaxDeduct.Size = new System.Drawing.Size(50, 28);
            this.cmdPaxDeduct.TabIndex = 126;
            this.cmdPaxDeduct.Text = "-";
            this.cmdPaxDeduct.UseVisualStyleBackColor = false;
            this.cmdPaxDeduct.Click += new System.EventHandler(this.cmdPaxDeduct_Click);
            // 
            // panSubTotal
            // 
            this.panSubTotal.BackColor = System.Drawing.Color.Maroon;
            this.panSubTotal.Controls.Add(this.lblSubtotalName);
            this.panSubTotal.Controls.Add(this.lblSubTotal);
            this.panSubTotal.Controls.Add(this.lblTransDiscount);
            this.panSubTotal.Controls.Add(this.lblTransCharge);
            this.panSubTotal.Controls.Add(this.lblCurrency);
            this.panSubTotal.Controls.Add(this.lblOrderType);
            this.panSubTotal.Location = new System.Drawing.Point(1, 581);
            this.panSubTotal.Name = "panSubTotal";
            this.panSubTotal.Size = new System.Drawing.Size(437, 94);
            this.panSubTotal.TabIndex = 128;
            this.panSubTotal.TabStop = false;
            // 
            // grpItems
            // 
            this.grpItems.BackColor = System.Drawing.Color.Transparent;
            this.grpItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.grpItems.Controls.Add(this.cmdProductRight);
            this.grpItems.Controls.Add(this.cmdProductLeft);
            this.grpItems.Controls.Add(this.lblItems);
            this.grpItems.Controls.Add(this.tblLayoutProducts);
            this.grpItems.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpItems.ForeColor = System.Drawing.Color.YellowGreen;
            this.grpItems.Location = new System.Drawing.Point(439, 153);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(485, 521);
            this.grpItems.TabIndex = 52;
            this.grpItems.TabStop = false;
            // 
            // cmdProductRight
            // 
            this.cmdProductRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdProductRight.BackColor = System.Drawing.Color.Gold;
            this.cmdProductRight.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdProductRight.FlatAppearance.BorderSize = 0;
            this.cmdProductRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdProductRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.cmdProductRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdProductRight.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdProductRight.Location = new System.Drawing.Point(429, 11);
            this.cmdProductRight.Name = "cmdProductRight";
            this.cmdProductRight.Size = new System.Drawing.Size(50, 28);
            this.cmdProductRight.TabIndex = 127;
            this.cmdProductRight.Text = ">";
            this.cmdProductRight.UseVisualStyleBackColor = false;
            this.cmdProductRight.Click += new System.EventHandler(this.cmdProductRight_Click);
            // 
            // cmdProductLeft
            // 
            this.cmdProductLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdProductLeft.AutoEllipsis = true;
            this.cmdProductLeft.BackColor = System.Drawing.Color.Gold;
            this.cmdProductLeft.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdProductLeft.FlatAppearance.BorderSize = 0;
            this.cmdProductLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdProductLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.cmdProductLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdProductLeft.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdProductLeft.Location = new System.Drawing.Point(5, 11);
            this.cmdProductLeft.Name = "cmdProductLeft";
            this.cmdProductLeft.Size = new System.Drawing.Size(50, 28);
            this.cmdProductLeft.TabIndex = 125;
            this.cmdProductLeft.Text = "<";
            this.cmdProductLeft.UseVisualStyleBackColor = false;
            this.cmdProductLeft.Click += new System.EventHandler(this.cmdProductLeft_Click);
            // 
            // lblItems
            // 
            this.lblItems.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItems.BackColor = System.Drawing.Color.Gold;
            this.lblItems.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.ForeColor = System.Drawing.Color.Black;
            this.lblItems.Location = new System.Drawing.Point(2, 9);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(480, 32);
            this.lblItems.TabIndex = 126;
            this.lblItems.Text = "ITEMS";
            this.lblItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblLayoutProducts
            // 
            this.tblLayoutProducts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLayoutProducts.BackColor = System.Drawing.Color.Transparent;
            this.tblLayoutProducts.CausesValidation = false;
            this.tblLayoutProducts.ColumnCount = 4;
            this.tblLayoutProducts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutProducts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutProducts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutProducts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutProducts.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tblLayoutProducts.Location = new System.Drawing.Point(0, 47);
            this.tblLayoutProducts.Name = "tblLayoutProducts";
            this.tblLayoutProducts.RowCount = 5;
            this.tblLayoutProducts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutProducts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutProducts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutProducts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutProducts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutProducts.Size = new System.Drawing.Size(484, 475);
            this.tblLayoutProducts.TabIndex = 101;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lblMenu);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(1, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(923, 46);
            this.groupBox1.TabIndex = 130;
            this.groupBox1.TabStop = false;
            // 
            // lblMenu
            // 
            this.lblMenu.BackColor = System.Drawing.Color.Gold;
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.ForeColor = System.Drawing.Color.Black;
            this.lblMenu.Location = new System.Drawing.Point(3, 9);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(916, 34);
            this.lblMenu.TabIndex = 130;
            this.lblMenu.Text = "M  E  N  U";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.cmdPaxDeduct);
            this.groupBox2.Controls.Add(this.cmdPaxAdd);
            this.groupBox2.Controls.Add(this.lblOrders);
            this.groupBox2.Controls.Add(this.dgItems);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(1, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 434);
            this.groupBox2.TabIndex = 131;
            this.groupBox2.TabStop = false;
            // 
            // cmd11
            // 
            this.cmd11.BackColor = System.Drawing.Color.Transparent;
            this.cmd11.Enabled = false;
            this.cmd11.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd11.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd11.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd11.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd11.Location = new System.Drawing.Point(926, 583);
            this.cmd11.Name = "cmd11";
            this.cmd11.Size = new System.Drawing.Size(92, 91);
            this.cmd11.TabIndex = 122;
            this.cmd11.Text = "pay order";
            this.cmd11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd11.UseVisualStyleBackColor = false;
            this.cmd11.Click += new System.EventHandler(this.cmd11_Click);
            // 
            // cmd10
            // 
            this.cmd10.BackColor = System.Drawing.Color.Transparent;
            this.cmd10.Enabled = false;
            this.cmd10.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd10.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd10.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd10.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd10.Location = new System.Drawing.Point(926, 532);
            this.cmd10.Name = "cmd10";
            this.cmd10.Size = new System.Drawing.Size(92, 51);
            this.cmd10.TabIndex = 121;
            this.cmd10.Text = "split\r\ntransaction";
            this.cmd10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd10.UseVisualStyleBackColor = false;
            this.cmd10.Click += new System.EventHandler(this.cmd10_Click);
            // 
            // cmd7
            // 
            this.cmd7.BackColor = System.Drawing.Color.Transparent;
            this.cmd7.Enabled = false;
            this.cmd7.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd7.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd7.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd7.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd7.Location = new System.Drawing.Point(926, 377);
            this.cmd7.Name = "cmd7";
            this.cmd7.Size = new System.Drawing.Size(92, 51);
            this.cmd7.TabIndex = 120;
            this.cmd7.Text = "check-in table";
            this.cmd7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd7.UseVisualStyleBackColor = false;
            this.cmd7.Click += new System.EventHandler(this.cmd7_Click);
            // 
            // cmd9
            // 
            this.cmd9.BackColor = System.Drawing.Color.Transparent;
            this.cmd9.Enabled = false;
            this.cmd9.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd9.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd9.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd9.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd9.Location = new System.Drawing.Point(926, 481);
            this.cmd9.Name = "cmd9";
            this.cmd9.Size = new System.Drawing.Size(92, 51);
            this.cmd9.TabIndex = 119;
            this.cmd9.Text = "discount";
            this.cmd9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd9.UseVisualStyleBackColor = false;
            this.cmd9.Click += new System.EventHandler(this.cmd9_Click);
            // 
            // cmd8
            // 
            this.cmd8.BackColor = System.Drawing.Color.Transparent;
            this.cmd8.Enabled = false;
            this.cmd8.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd8.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd8.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd8.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd8.Location = new System.Drawing.Point(926, 429);
            this.cmd8.Name = "cmd8";
            this.cmd8.Size = new System.Drawing.Size(92, 51);
            this.cmd8.TabIndex = 118;
            this.cmd8.Text = "check-out bill";
            this.cmd8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd8.UseVisualStyleBackColor = false;
            this.cmd8.Click += new System.EventHandler(this.cmd8_Click);
            // 
            // cmd6
            // 
            this.cmd6.BackColor = System.Drawing.Color.Transparent;
            this.cmd6.Enabled = false;
            this.cmd6.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd6.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd6.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd6.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd6.Location = new System.Drawing.Point(926, 327);
            this.cmd6.Name = "cmd6";
            this.cmd6.Size = new System.Drawing.Size(92, 51);
            this.cmd6.TabIndex = 117;
            this.cmd6.Text = "reports";
            this.cmd6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd6.UseVisualStyleBackColor = false;
            this.cmd6.Click += new System.EventHandler(this.cmd6_Click);
            // 
            // cmd1
            // 
            this.cmd1.BackColor = System.Drawing.Color.Transparent;
            this.cmd1.Enabled = false;
            this.cmd1.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd1.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd1.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd1.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd1.Location = new System.Drawing.Point(926, 31);
            this.cmd1.Name = "cmd1";
            this.cmd1.Size = new System.Drawing.Size(92, 92);
            this.cmd1.TabIndex = 109;
            this.cmd1.Text = "table";
            this.cmd1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd1.UseVisualStyleBackColor = false;
            this.cmd1.Click += new System.EventHandler(this.cmd1_Click);
            // 
            // cmd2
            // 
            this.cmd2.BackColor = System.Drawing.Color.Transparent;
            this.cmd2.Enabled = false;
            this.cmd2.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd2.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd2.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd2.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd2.Location = new System.Drawing.Point(926, 123);
            this.cmd2.Name = "cmd2";
            this.cmd2.Size = new System.Drawing.Size(92, 51);
            this.cmd2.TabIndex = 113;
            this.cmd2.Text = "suspend";
            this.cmd2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd2.UseVisualStyleBackColor = false;
            this.cmd2.Click += new System.EventHandler(this.cmd2_Click);
            // 
            // cmd3
            // 
            this.cmd3.BackColor = System.Drawing.Color.Transparent;
            this.cmd3.Enabled = false;
            this.cmd3.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd3.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd3.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd3.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd3.Location = new System.Drawing.Point(926, 174);
            this.cmd3.Name = "cmd3";
            this.cmd3.Size = new System.Drawing.Size(92, 51);
            this.cmd3.TabIndex = 114;
            this.cmd3.Text = "void";
            this.cmd3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd3.UseVisualStyleBackColor = false;
            this.cmd3.Click += new System.EventHandler(this.cmd3_Click);
            // 
            // cmd4
            // 
            this.cmd4.BackColor = System.Drawing.Color.Transparent;
            this.cmd4.Enabled = false;
            this.cmd4.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd4.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd4.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd4.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd4.Location = new System.Drawing.Point(926, 225);
            this.cmd4.Name = "cmd4";
            this.cmd4.Size = new System.Drawing.Size(92, 51);
            this.cmd4.TabIndex = 115;
            this.cmd4.Text = "dine-in\r\ntake-out\r\ndelivery";
            this.cmd4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd4.UseVisualStyleBackColor = false;
            this.cmd4.Click += new System.EventHandler(this.cmd4_Click);
            // 
            // cmd5
            // 
            this.cmd5.BackColor = System.Drawing.Color.Transparent;
            this.cmd5.Enabled = false;
            this.cmd5.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd5.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd5.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd5.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd5.Location = new System.Drawing.Point(926, 276);
            this.cmd5.Name = "cmd5";
            this.cmd5.Size = new System.Drawing.Size(92, 51);
            this.cmd5.TabIndex = 116;
            this.cmd5.Text = "other\r\ncommands";
            this.cmd5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd5.UseVisualStyleBackColor = false;
            this.cmd5.Click += new System.EventHandler(this.cmd5_Click);
            // 
            // cmd12
            // 
            this.cmd12.BackColor = System.Drawing.Color.Transparent;
            this.cmd12.Enabled = false;
            this.cmd12.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd12.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cmd12.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd12.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd12.Location = new System.Drawing.Point(926, 675);
            this.cmd12.Name = "cmd12";
            this.cmd12.Size = new System.Drawing.Size(92, 30);
            this.cmd12.TabIndex = 132;
            this.cmd12.Text = "logout";
            this.cmd12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd12.UseVisualStyleBackColor = false;
            this.cmd12.Click += new System.EventHandler(this.cmd12_Click);
            // 
            // MainRestoWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1019, 744);
            this.ControlBox = false;
            this.Controls.Add(this.panLocked);
            this.Controls.Add(this.lblAgentPositionDepartment);
            this.Controls.Add(this.cmd11);
            this.Controls.Add(this.cmd10);
            this.Controls.Add(this.cmd7);
            this.Controls.Add(this.cmd9);
            this.Controls.Add(this.cmd8);
            this.Controls.Add(this.cmd6);
            this.Controls.Add(this.cmd1);
            this.Controls.Add(this.grpTop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpSubGroup);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.grpRLC);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panSubTotal);
            this.Controls.Add(this.grptxtBarcode);
            this.Controls.Add(this.grpMarquee);
            this.Controls.Add(this.cmd2);
            this.Controls.Add(this.cmd3);
            this.Controls.Add(this.cmd4);
            this.Controls.Add(this.cmd5);
            this.Controls.Add(this.cmd12);
            this.Controls.Add(this.grpBottom);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainRestoWnd";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.MainRestoWnd_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainRestoWnd_Closing);
            this.Load += new System.EventHandler(this.MainRestoWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainRestoWnd_KeyDown);
            this.panLocked.ResumeLayout(false);
            this.panLocked.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpTop.ResumeLayout(false);
            this.grpBottom.ResumeLayout(false);
            this.grptxtBarcode.ResumeLayout(false);
            this.grptxtBarcode.PerformLayout();
            this.grpMarquee.ResumeLayout(false);
            this.grpRLC.ResumeLayout(false);
            this.grpRLC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.grpSubGroup.ResumeLayout(false);
            this.panSubTotal.ResumeLayout(false);
            this.panSubTotal.PerformLayout();
            this.grpItems.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Locking and Unlocking the window, Enable/Disable Command Controls

		public void Lock()
		{
			try
			{
                this.panLocked.Visible = true;

				clsEvent.AddEvent("[" + lblCashier.Text + "] Locking client application.");

				this.lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
				this.lblCashier.Tag = "";
				this.lblCashier.Text = "";
                this.mCashierName = "";

				this.lblCustomer.Tag = Constants.C_RETAILPLUS_CUSTOMERID.ToString();
				this.lblCustomer.Text = Constants.C_RETAILPLUS_CUSTOMER;
				this.lblAgent.Tag = Constants.C_RETAILPLUS_AGENTID.ToString();
				this.lblAgent.Text = Constants.C_RETAILPLUS_AGENT;
				this.lblAgentPositionDepartment.Text = Constants.C_RETAILPLUS_AGENT_POSITIONNAME;
				this.lblAgentPositionDepartment.Tag = Constants.C_RETAILPLUS_AGENT_DEPARTMENT_NAME;
				this.lblServedBy.Tag = Constants.C_RETAILPLUS_WAITERID.ToString();
				this.lblServedBy.Text = Constants.C_RESTOPLUS; 

				// dgItems.Height = 363;

				EnableCommandControls(false);
				this.cmdSubGroupLeft.Visible = false;
				this.cmdSubGroupRight.Visible = false;

				this.mboLocked = true;
				this.txtBarCode.Text = "";
				this.txtBarCode.Enabled = false;
				
				this.Focus();

				InsertAuditLog(AccessTypes.LockTerminal, "Lock Terminal #: " + mclsTerminalDetails.TerminalNo);
				clsEvent.AddEventLn("Done!");
			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); }
		}
		public void UnLock(long UserID)
		{
            AccessUser clsUser = new AccessUser(mConnection, mTransaction);
            mConnection = clsUser.Connection; mTransaction = clsUser.Transaction;

            AccessUserDetails details = clsUser.Details(UserID);

            clsEvent.AddEvent("[" + details.Name + "] UnLocking client application.");
            try
            {
                this.panLocked.Visible = false;

                this.lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
                this.lblCashier.Tag = details.UID;
                this.lblCashier.Text = details.Name;
                this.mCashierName = details.Name;

                EnableCommandControls(true);
                this.cmdSubGroupLeft.Visible = true;
                this.cmdSubGroupRight.Visible = true;

                this.mboLocked = false;
                this.panLocked.Visible = false;
                this.txtBarCode.Text = "";
                this.txtBarCode.Enabled = true;
                this.txtBarCode.Focus();

                mclsSalesTransactionDetails.CashierID = details.UID;
                mclsSalesTransactionDetails.CashierName = details.Name;

                InsertAuditLog(AccessTypes.UnlockTerminal, "Unlock terminal #: " + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                clsEvent.AddEventLn("Done!");
            }
            catch (Exception ex)
            {
                clsEvent.AddErrorEventLn(ex);
            }
            finally
            {
                clsUser.CommitAndDispose();
            }
		}
		private void EnableCommandControls(bool isEnable)
		{
			this.cmd1.Enabled = isEnable;
			this.cmd2.Enabled = isEnable;
			this.cmd3.Enabled = isEnable;
			this.cmd4.Enabled = isEnable;
			this.cmd5.Enabled = isEnable;
			this.cmd6.Enabled = isEnable;
			this.cmd8.Enabled = isEnable;
			this.cmd9.Enabled = isEnable;
			this.cmd7.Enabled = isEnable;
			this.cmd10.Enabled = isEnable;
			this.cmd11.Enabled = isEnable;
			this.cmd12.Enabled = isEnable;
		}

		#endregion

		#region Window Form Methods

		private void MainRestoWnd_Activated(object sender, System.EventArgs e)
		{
			txtBarCode.Focus();
		}
		private void MainRestoWnd_Load(object sender, System.EventArgs e)
		{
            //// 08Dec2014 : remove this here
            //// it's already loaded in MainWnd() initialization
            //this.LoadOptions();

			txtBarCode.Focus();
			lblTerminalNo.Text = mclsTerminalDetails.TerminalNo;
			lblCompanyName.Text = CompanyDetails.CompanyName;

			if (mclsTerminalDetails.FORM_Behavior == FORM_Behavior.MODAL)
			{
				TaskBarWnd taskbar = new TaskBarWnd();
				taskbar.Hide();

				Screen Srn = Screen.PrimaryScreen;
				tempHeight = Srn.Bounds.Height;
				tempWidth = Srn.Bounds.Width;

				Resolution ChangeRes = new Resolution(1024, 768);

				Hook = new KeyBoardHook.KeyBoardHook();
				Hook.KeyDown += new KeyEventHandler(MyKeyDown);

				this.FormBorderStyle = FormBorderStyle.FixedDialog;
				this.ControlBox = false;
				this.TopMost = true;
				this.Text = "";
				this.Height = 768;
				this.Width = 1024;
				this.Location = new Point(-1, -1);
				this.ShowInTaskbar = false;
				Cursor.Hide();
			}
			else
			{
				Screen Srn = Screen.PrimaryScreen;
				if (Srn.Bounds.Height <= 768)
				{
					this.FormBorderStyle = FormBorderStyle.FixedDialog;
					this.ControlBox = false;
					this.TopMost = true;
					this.Text = "";
					this.Height = 768;
					this.Width = 1024;
					this.Location = new Point(-1, -1);
					this.ShowInTaskbar = false;
				}
			}

            panLocked.Top = 30;
            panLocked.Left = 0;
			panLocked.Height = 714;
			panLocked.Width = 1018;

			IsStartCutOffTimeOK();

			if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
				tmrRLC.Enabled = true;
		}
		private void MainRestoWnd_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				e.Cancel = true;
				base.OnClosing(e);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Closing Main window. TRACE: " + ex.Message);
			}
		}
		private void MainRestoWnd_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyData == Keys.F1)
				{ ShowHelp(); }
				else if (mboLocked && e.KeyData == Keys.F2)
				{ this.PriceInquiry(); }
				else if (e.KeyData == Keys.Escape)
				{
					if (txtBarCode.Text != string.Empty)
					{ txtBarCode.Text = string.Empty; txtBarCode.Focus(); }
					else
					{ this.Exit(); }
				}
				else if (mboLocked)
				{
					if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.L)
						LoggedOutCashier(true);
					else if (e.KeyData == Keys.Enter)
						this.DoLogin();
				}
				else if (!mboLocked)
				{
					if (txtBarCode.Text != string.Empty && e.KeyCode != Keys.Enter) return;
					if (Control.ModifierKeys == Keys.Control)
					{
						switch (e.KeyCode)
						{
							case Keys.C:
								grpRLC.Visible = false;
								break;

							case Keys.H:
								DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTransactionHeader);

								if (loginresult == DialogResult.OK)
								{
                                    PrintReportHeaderSection(true, DateTime.MinValue);
									MessageBox.Show("Transaction header has been printed.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
								}
								break;

							case Keys.E:
								ReleaseItems();
								break;

							case Keys.L:
								LoggedOutCashier(true);
								break;

							case Keys.O:
								OpenTransactionDrawer();
								break;

							case Keys.P:
								PrintCheckOutBill();
								break;

							case Keys.R:
								RefundTransaction();
								break;

							case Keys.S:
								PrintOrderSlip(false); 
								break;

							case Keys.U:
								PackTransaction();
								break;

                            case Keys.F2:
                                ChangeProductCode();
                                break;

                            case Keys.F5:
                                UpdateContact();
                                break;

							case Keys.Insert:
								Float();
								break;

							case Keys.F6:
								SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT);
								break;

							case Keys.Enter:
								if (!mboIsInTransaction)
								{
									if (MessageBox.Show("Are you sure you want to reload the default options?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
									{
                                        Data.SysConfig clsSysConfig = new Data.SysConfig(mConnection, mTransaction);
                                        mConnection = clsSysConfig.Connection; mTransaction = clsSysConfig.Transaction;

                                        try
                                        {
                                            mclsSysConfigDetails = clsSysConfig.get_SysConfigDetails();
                                        }
                                        catch (Exception ex)
                                        {
                                            clsEvent.AddErrorEventLn(ex);
                                        }
                                        clsSysConfig.CommitAndDispose();

										LoadOptions();
										MessageBox.Show("Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
									}
								}
								break;
							case Keys.F7:
								IssueCreditCard();
								break;

							case Keys.F8:
								RenewCreditCard();
								break;

							case Keys.F9:
								CreditCardReplacement(CreditCardStatus.Replaced_Lost);
								break;

							case Keys.F10:
								CreditCardReplacement(CreditCardStatus.Replaced_Expired);
								break;

							case Keys.F11:
								CreditCardDeclareAsSuspended();
								break;

							case Keys.F12:
								CreditCardReactivate();
								break;
						}
					}
					else if (Control.ModifierKeys == Keys.Shift)
					{
						switch (e.KeyCode)
						{
							case Keys.F2:
								ChangePrice();
								break;

							case Keys.F4:
								ApplyItemDiscount(); //DiscountTypes.FixedValue
								break;

							case Keys.F5:
								ApplyTransDiscount(); //DiscountTypes.FixedValue
								break;

							case Keys.F6:
								SelectWaiter();
								break;

							case Keys.F10:
								PaidOut();
								break;

							case Keys.F11:
								Deposit();
								break;

							case Keys.F12:
								ReprintTransaction();
								break;

							case Keys.Enter:
								EnterCreditItemizePayment();
								break;

                            case Keys.Delete:
                                InitializeZRead(true);
                                break;
						}
					}
					else if (Control.ModifierKeys == Keys.Alt)
					{
						switch (e.KeyCode)
						{
                            case Keys.F1:
                                string strFileName = Application.ExecutablePath.ToUpper().Replace("RETAILPLUS.EXE", "") + "print.prn";
                                RawPrinterHelper.SendFileToPrinter(mclsTerminalDetails.PrinterName, strFileName, "RetailPlus " + "print.prn");
                                CutPrinterPaper(mclsTerminalDetails.PrinterName);
                                MessageBox.Show("Done printing 'print.prn'", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;

                            case Keys.Enter:
                                VerifyCredit();
                                break;

							case Keys.S:
								/**********************
								 * December 18, 2008
								 * Added to reprint all items that are already printed
								 * *******************/
								PrintOrderSlip(true);
								break;

							case Keys.U:
								UnPackTransaction();
								break;

							case Keys.F2:
								ChangeAmount();
								break;

							case Keys.F5:
								ApplyTransCharge(); //ChargeTypes.Percentage
								break;

							case Keys.F6:
								ApplyTransCharge(); //ChargeTypes.FixedValue
								break;

							case Keys.F7:
								IssueRewardCard();
								break;

							case Keys.F8:
								RenewRewardCard();
								break;

							case Keys.F9:
								RewardCardReplacement(RewardCardStatus.Replaced_Lost);
								break;

							case Keys.F10:
								RewardCardReplacement(RewardCardStatus.Replaced_Expired);
								break;

							case Keys.F11:
								RewardCardDeclareAsLost();
								break;

							case Keys.F12:
								RewardCardReactivate();
								break;

                            case Keys.Insert:
                                ChangeZeroRated(true);
                                break;

                            case Keys.Delete:
                                ChangeZeroRated(false);
                                break;

							//case Keys.Enter:
							//    mclsTerminalDetails.TrustFund = 0;
							//    MessageBox.Show("Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
							//    break;
							
						}
					}
                    else if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
                    {
                        Security.AccessRights clsAccessRights; Security.AccessRightsDetails clsDetails;
                        switch (e.KeyCode)
                        {
                            case Keys.Enter:
                                clsAccessRights = new Security.AccessRights();
                                clsDetails = new Security.AccessRightsDetails();
                                clsDetails = clsAccessRights.Details(mclsSalesTransactionDetails.CashierID, (Int16)AccessTypes.ReprintZRead);
                                clsAccessRights.CommitAndDispose();

                                if (clsDetails.Read)
                                {
                                    UpdateBranchAndTerminalNo();
                                }
                                break;
                            case Keys.F2:
                                clsAccessRights = new Security.AccessRights();
                                clsDetails = new Security.AccessRightsDetails();
                                clsDetails = clsAccessRights.Details(mclsSalesTransactionDetails.CashierID, (Int16)AccessTypes.Contacts);
                                clsAccessRights.CommitAndDispose();

                                if (clsDetails.Write)
                                {
                                    Data.ContactDetails clsContactDetails;
                                    DialogResult addresult = System.Windows.Forms.DialogResult.Cancel;

                                    if (!mclsTerminalDetails.ShowCustomerSelection)
                                    {
                                        ContactAddDetWnd clsContactAddWnd = new ContactAddDetWnd();
                                        clsContactAddWnd.Caption = "Quickly add new customer.";
                                        clsContactAddWnd.ShowDialog(this);
                                        addresult = clsContactAddWnd.Result;
                                        clsContactDetails = clsContactAddWnd.ContactDetails;
                                        clsContactAddWnd.Close();
                                        clsContactAddWnd.Dispose();
                                    }
                                    else
                                    {
                                        ContactAddWnd clsContactAddWnd = new ContactAddWnd();
                                        clsContactAddWnd.Caption = "Quickly add new customer.";
                                        clsContactAddWnd.ShowDialog(this);
                                        addresult = clsContactAddWnd.Result;
                                        clsContactDetails = clsContactAddWnd.ContactDetails;
                                        clsContactAddWnd.Close();
                                        clsContactAddWnd.Dispose();
                                    }

                                    if (addresult == DialogResult.OK) LoadContact(Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                                }
                                break;
                            case Keys.F6:
                                UpdateContact();
                                break;
                            case Keys.F9:
                                // needs the counter so that it will only show if CTRL+SHIFT+F9 is hit twice
                                if (miReprintZReadCounter >= 1)
                                {
                                    clsAccessRights = new Security.AccessRights();
                                    clsDetails = new Security.AccessRightsDetails();
                                    clsDetails = clsAccessRights.Details(mclsSalesTransactionDetails.CashierID, (Int16)AccessTypes.ReprintZRead);
                                    clsAccessRights.CommitAndDispose();

                                    if (clsDetails.Read)
                                    {
                                        ReprintZRead(true);
                                    }
                                    miReprintZReadCounter = 0;
                                }
                                else
                                { miReprintZReadCounter += 1; }
                                break;
                        }
                    }
					else
					{
						switch (e.KeyData)
						{
							case Keys.Up:
								MoveItemUp();
								break;

							case Keys.Down:
								MoveItemDown();
								break;

							case Keys.Escape:
								this.Exit();
								break;

							case Keys.F1:
								ShowHelp();
								break;

							case Keys.F2:
								ChangeQuantity();
								break;

							case Keys.F3:
								ReturnItem();
								break;

							case Keys.F4:
								ApplyItemDiscount();
								break;

							case Keys.F5:
								ApplyTransDiscount();
								break;

							case Keys.F6:
								SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER);
								break;

							case Keys.F7:
								SuspendTransaction(true);
								break;

							case Keys.F8:
								ResumeTransaction();
								break;

							case Keys.F9:
								VoidTransaction();
								break;

							case Keys.F10:
								Disburse();
								break;

							case Keys.F11:
								WithHold();
								break;

							case Keys.F12:
								ShowPrintWindow();
								break;

							case Keys.Enter:
								if (txtBarCode.Text.Trim() != "" && txtBarCode.Text.Trim() != null)
									if (txtBarCode.Text.Contains(Constants.SWIPE_REWARD_CARD))
									{ 
										LoadContact(Data.ContactGroupCategory.CUSTOMER, new Data.ContactDetails()); }
                                    else
                                    {
                                        if (txtBarCode.Text.StartsWith("-") && txtBarCode.Text.Contains("*"))
                                            MessageBox.Show("Sorry you cannot enter a negative quantity when selling an item.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                        else
                                            ReadBarCode();
                                    }
								else if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
									CloseTransactionAsOrderSlip();
								else
									CloseTransaction();
								break;

							case Keys.Back:
								if (txtBarCode.Text.Trim() == "")
									VoidItem();
								break;

							case Keys.Insert:
								{
									if (mclsTerminalDetails.CashCountBeforeReport)
									{ if (!mboIsCashCountInitialized) CashCount(); }
									else { CashCount(); }
									break;
								}

							case Keys.Delete:
								InitializeZRead(false);
								break;

							case Keys.PageDown:
								SelectProduct(false);
								break;

							case Keys.Right:
								SelectProduct(false);
								break;

							case Keys.PageUp:
								SelectProduct(true);
								break;

							case Keys.Left:
								PriceInquiry();
								break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Closing Main window. TRACE: " + ex.Message);
			}
		}
		public void MyKeyDown(object sender, KeyEventArgs e)
		{
			//Console.WriteLine("KeyDown 	- " + e.KeyData.ToString());
		}

		#endregion

		#region Window Control Methods

		private void txtBarcode_GotFocus(object sender, System.EventArgs e)
		{
			txtBarCode.SelectAll();
		}
		private void lblCashier_Click(object sender, EventArgs e)
		{
			LoggedOutCashier(true);
		}
		private void lblCompanyName_Click(object sender, EventArgs e)
		{
			this.ShowHelp();
		}
		private void lblTerminalNo_Click(object sender, EventArgs e)
		{
			this.Exit();
		}
		private void lblSubtotalName_Click(object sender, EventArgs e)
		{
			KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
			MainRestoWnd_KeyDown(null, enter);
			txtBarCode.Focus();
		}
		private void lblCurrency_Click(object sender, EventArgs e)
		{
			KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
			MainRestoWnd_KeyDown(null, enter);
			txtBarCode.Focus();
		}
		private void lblSubTotal_Click(object sender, EventArgs e)
		{
			KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
			MainRestoWnd_KeyDown(null, enter);
			txtBarCode.Focus();
		}
		private void tmr_Tick(object sender, EventArgs e)
		{
			mdtCurrentDateTime.AddMilliseconds(1);
		}
		private void panLocked_Click(object sender, EventArgs e)
		{
			if (mboLocked)
				this.DoLogin();
		}
		private void imgIcon_Click(object sender, EventArgs e)
		{
			if (mboLocked)
				this.DoLogin();
		}
		private void lblThisStation_Click(object sender, EventArgs e)
		{
			if (mboLocked)
				this.DoLogin();
		}
		private void cmdBackSpace_Click(object sender, EventArgs e)
		{
			cmdNoClick("{BACKSPACE}");
		}
		private void lblOrderType_Click(object sender, EventArgs e)
		{
			ChangeOrderType();
		}
		private void tmrRLC_Tick(object sender, EventArgs e)
		{
			grpRLC.Visible = true;
			tmrRLC.Interval = CONFIG.FTPAutoResendInterval;
			SendRLCDelegate delSendRLCDelegate = new SendRLCDelegate(SendRLC);
			delSendRLCDelegate.BeginInvoke(null, null);
		}
		private void cmdRLCClose_Click(object sender, EventArgs e)
		{
			grpRLC.Visible = false;
		}
		private void lblCustomer_Click(object sender, System.EventArgs e)
		{
			if (!mboLocked) SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER);
		}
		//private void lblAgent_Click(object sender, EventArgs e)
		//{
		//    SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT);
		//}

		#endregion

		#region Overloads

		private void LoadOptions()
		{
			try
			{
				clsEvent.AddEvent("Loading transaction defaults...");

				Cursor.Current = Cursors.WaitCursor;

                lblCurrency.Text = CompanyDetails.Currency;
                lblTransNo.Text = "READY...";
                lblCustomer.Text = Constants.C_RETAILPLUS_CUSTOMER;
                lblCustomer.Tag = Constants.C_RETAILPLUS_CUSTOMERID.ToString();
                lblAgent.Text = Constants.C_RETAILPLUS_AGENT;
                lblAgent.Tag = Constants.C_RETAILPLUS_AGENTID.ToString();
                lblAgentPositionDepartment.Text = Constants.C_RETAILPLUS_AGENT_POSITIONNAME;
                lblAgentPositionDepartment.Tag = Constants.C_RETAILPLUS_AGENT_DEPARTMENT_NAME;
                if (mboLocked) lblServedBy.Text = Constants.C_RESTOPLUS; else lblServedBy.Text = "Served by: " + lblCashier.Text;
				lblServedBy.Tag = lblCashier.Tag;
				lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
				lblSubTotal.Text = "0.00";
                lblTransDiscount.Text = "Less 0% / 0.00";
				lblTransDiscount.Tag = DiscountTypes.NotApplicable.ToString("d");
				lblOrders.Text = "CUSTOMER's ORDER: 1 pax";
				lblOrders.Tag = "1";

				if (mclsTerminalDetails.WithRestaurantFeatures)
				{ lblSubtotalName.Text = "SUBTOTAL:"; lblOrderType.Visible = true; lblOrderType.Text = OrderTypes.DineIn.ToString("G").ToUpper(); }
				else
				{ lblSubtotalName.Text = "SUBTOTAL"; lblOrderType.Visible = false; lblOrderType.Text = OrderTypes.DineIn.ToString("G").ToUpper(); }

                lblMessage.Text = " Your suggestive selling message and/or description";
                lblTransCharge.Text = lblTransCharge.Text = "Plus 0% / 0.00";
                lblTransCharge.Tag = ChargeTypes.NotApplicable.ToString("d");
                txtBarCode.Text = "";

                mboIsRefund = false;
                mboDoNotPrintTransactionDate = false;
                //mboIsDiscountAuthorized = false;

                mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
                try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
                catch { }
                mclsSalesTransactionDetails.CashierName = lblCashier.Text;

                Data.Terminal clsTerminal = new Data.Terminal(mConnection, mTransaction);
                mConnection = clsTerminal.Connection; mTransaction = clsTerminal.Transaction;

                mclsTerminalDetails = clsTerminal.Details(Constants.TerminalBranchID, CompanyDetails.TerminalNo);
                lblTerminalNoName.Text = mclsTerminalDetails.BranchDetails.BranchName;

                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                mclsContactDetails = clsContact.Details(Constants.C_RETAILPLUS_CUSTOMERID);

                // Sep 24, 2011      Lemuel E. Aceron
                // Added order slip wherein all punch items will not change sales and inventory
                // Override the reserved and commit if order slip
                // a customer named ORDER SLIP should be defined in contacts
                if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                { mclsTerminalDetails.ReservedAndCommit = false; }

                // Dec 01, 2008      Lemuel E. Aceron
                // added the IsCashCountInitialized for 1 time 
                // Cash count every printing of report.
                if (mclsTerminalDetails.CashCountBeforeReport)
                    mboIsCashCountInitialized = clsTerminal.IsCashCountInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID);

                clsTerminal.CommitAndDispose();

                cmdPaxAdd.Visible = mclsTerminalDetails.IsFineDining;
                cmdPaxDeduct.Visible = mclsTerminalDetails.IsFineDining;

				cmdSubGroupLeft.Tag = "0";
				cmdSubGroupRight.Tag = "0";
				LoadProductSubGroups(System.Data.SqlClient.SortOrder.Ascending);

                SetGridItems();
                SetGridItemsWidth();

                mboIsInTransaction = false;
                mboIsItemHeaderPrinted = false;
                mboCreditCardSwiped = false;
                mboRewardCardSwiped = false;
                mdteOverRidingPrintDate = DateTime.MinValue;

                StartMarqueeThread();
                Cursor.Current = Cursors.Default;

                msbToPrint.Clear();
                msbToPrint = new StringBuilder();
                msbEJournalToPrint = new StringBuilder();

				clsEvent.AddEventLn("Done!");

			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); }
		}

        private void SetGridItems()
        {
            ItemDataTable = new System.Data.DataTable("tblProducts");
            ItemDataTable.Columns.Add("TransactionItemsID");
            ItemDataTable.Columns.Add("ItemNo");
            ItemDataTable.Columns.Add("ProductID");
            ItemDataTable.Columns.Add("ProductCode");
            ItemDataTable.Columns.Add("BarCode");
            ItemDataTable.Columns.Add("Description");
            ItemDataTable.Columns.Add("ProductUnitID");
            ItemDataTable.Columns.Add("ProductUnitCode");
            ItemDataTable.Columns.Add("Quantity");
            ItemDataTable.Columns.Add("Price");
            ItemDataTable.Columns.Add("Discount");
            ItemDataTable.Columns.Add("ItemDiscount");
            ItemDataTable.Columns.Add("ItemDiscountType");
            ItemDataTable.Columns.Add("Amount");
            ItemDataTable.Columns.Add("VAT");
            ItemDataTable.Columns.Add("EVAT");
            ItemDataTable.Columns.Add("LocalTax");
            ItemDataTable.Columns.Add("VariationsMatrixID");
            ItemDataTable.Columns.Add("MatrixDescription");
            ItemDataTable.Columns.Add("ProductGroup");
            ItemDataTable.Columns.Add("ProductSubGroup");
            ItemDataTable.Columns.Add("TransactionItemStat");
            ItemDataTable.Columns.Add("DiscountCode");
            ItemDataTable.Columns.Add("DiscountRemarks");
            ItemDataTable.Columns.Add("ProductPackageID");
            ItemDataTable.Columns.Add("MatrixPackageID");
            ItemDataTable.Columns.Add("PackageQuantity");
            ItemDataTable.Columns.Add("PromoQuantity");
            ItemDataTable.Columns.Add("PromoValue");
            ItemDataTable.Columns.Add("PromoInPercent");
            ItemDataTable.Columns.Add("PromoType");
            ItemDataTable.Columns.Add("PromoApplied");
            ItemDataTable.Columns.Add("PurchasePrice");
            ItemDataTable.Columns.Add("PurchaseAmount");
            ItemDataTable.Columns.Add("IncludeInSubtotalDiscount");
            ItemDataTable.Columns.Add("IsCreditChargeExcluded");
            ItemDataTable.Columns.Add("OrderSlipPrinter1");
            ItemDataTable.Columns.Add("OrderSlipPrinter2");
            ItemDataTable.Columns.Add("OrderSlipPrinter3");
            ItemDataTable.Columns.Add("OrderSlipPrinter4");
            ItemDataTable.Columns.Add("OrderSlipPrinter5");
            ItemDataTable.Columns.Add("OrderSlipPrinted");
            ItemDataTable.Columns.Add("PercentageCommision");
            ItemDataTable.Columns.Add("Commision");
            ItemDataTable.Columns.Add("RewardPoints");
            ItemDataTable.Columns.Add("ItemRemarks");
            ItemDataTable.Columns.Add("PaxNo");
            

            this.dgStyle.MappingName = ItemDataTable.TableName;
            dgItems.DataSource = ItemDataTable;
        }

		private void SetGridItemsWidth()
		{
			dgStyle.GridColumnStyles["ItemNo"].Width = 65;
			dgStyle.GridColumnStyles["Description"].Width = dgItems.Width - 332;
			dgStyle.GridColumnStyles["Quantity"].Width = 50;
			dgStyle.GridColumnStyles["Price"].Width = 70;
			dgStyle.GridColumnStyles["Amount"].Width = 95;
			dgStyle.GridColumnStyles["PaxNo"].Width = 48;
			dgStyle.GridColumnStyles["Quantity"].HeaderText = "Qty";
			dgStyle.GridColumnStyles["ItemNo"].Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			dgStyle.GridColumnStyles["Quantity"].Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			dgStyle.GridColumnStyles["Price"].Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			dgStyle.GridColumnStyles["Amount"].Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			dgStyle.GridColumnStyles["PaxNo"].Alignment = System.Windows.Forms.HorizontalAlignment.Center;
		}

		#endregion

		#region Commands

		private void MoveItemUp()
		{
			if (ItemDataTable.Rows.Count > 0 && dgItems.CurrentRowIndex != 0)
			{
				int oldindex = dgItems.CurrentRowIndex;
				dgItems.CurrentRowIndex -= 1;
				try { dgItems.UnSelect(0); }
				catch { }
				dgItems.UnSelect(oldindex);
				dgItems.Select(dgItems.CurrentRowIndex);
				Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
				DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
				DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
			}
		}
		private void MoveItemDown()
		{
			if (dgItems.CurrentRowIndex + 1 < ItemDataTable.Rows.Count && dgItems.CurrentRowIndex + 1 != ItemDataTable.Rows.Count)
			{
				int oldindex = dgItems.CurrentRowIndex;

				dgItems.CurrentRowIndex += 1;
				try { dgItems.UnSelect(0); }
				catch { }
				dgItems.UnSelect(oldindex);
				dgItems.Select(dgItems.CurrentRowIndex);
				Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
				DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
				DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
			}
		}
		private void Exit()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Cannot exit the system current transaction is active. Please SUSPEND or VOID the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			if (MessageBox.Show("Are you sure you want to exit?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				if (this.LoggedOutCashier(false) == DialogResult.OK)
				{
					try { MarqueeThread.Abort(); }
					catch { }
					Application.Exit();
				}
			}
		}
		private void ShowHelp()
		{
			HelpWnd help = new HelpWnd();
            help.TerminalDetails = mclsTerminalDetails;
			help.ShowDialog(this);
			help.Close();
			help.Dispose();
		}
		private void PriceInquiry()
		{
			try
			{
				clsEvent.AddEventLn(" Opening Price Inquiry module...", true);

				PriceInquiryWnd clsPriceInquiryWnd = new PriceInquiryWnd();
				clsPriceInquiryWnd.ShowDialog(this);
				DialogResult result = clsPriceInquiryWnd.Result;
				clsPriceInquiryWnd.Close();
				clsPriceInquiryWnd.Dispose();

				clsEvent.AddEventLn(" Price Inquiry module closed...", true);
			}
			catch { }
		}
        private void ChangeItemRemarks()
        {

            int iOldRow = dgItems.CurrentRowIndex;
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID")
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot change quantity if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangeQuantity);

                    if (loginresult == DialogResult.OK)
                    {
                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        string oldItemRemarks = Details.ItemRemarks;
                        ChangeItemRemarksWnd clsChangeItemRemarksWnd = new ChangeItemRemarksWnd();
                        clsChangeItemRemarksWnd.Details = Details;
                        clsChangeItemRemarksWnd.TerminalDetails = mclsTerminalDetails;
                        clsChangeItemRemarksWnd.ShowDialog(this);
                        DialogResult result = clsChangeItemRemarksWnd.Result;
                        Details = clsChangeItemRemarksWnd.Details;

                        clsChangeItemRemarksWnd.Close();
                        clsChangeItemRemarksWnd.Dispose();

                        if (result == DialogResult.OK && oldItemRemarks != Details.ProductCode)
                        {
                            Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                            mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                            mbodgItemRowClick = true;

                            clsEvent.AddEventLn("Updating item #".PadRight(15) + ":" + Details.ItemNo + "".PadRight(15) + " remarks from:" + oldItemRemarks + " to " + Details.ItemRemarks, true);

                            System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

                            dr = setCurrentRowItemDetails(dr, Details);

                            ComputeSubTotal(); setTotalDetails();

                            Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);

                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, Details);
                            clsSalesTransactions.CommitAndDispose();

                            clsEvent.AddEventLn("Updating item #".PadRight(15) + ":" + Details.ItemNo + " : done", true);

                            clsProduct.CommitAndDispose();

                            mbodgItemRowClick = false;

                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                        }
                    }
                }
            }

        }
        private void ChangeProductCode()
        {

            int iOldRow = dgItems.CurrentRowIndex;
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID")
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot change quantity if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangeQuantity);

                    if (loginresult == DialogResult.OK)
                    {
                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        string oldproductCode = Details.ProductCode;
                        ChangeProductCodeWnd ProdCodeWnd = new ChangeProductCodeWnd();
                        ProdCodeWnd.Details = Details;
                        ProdCodeWnd.TerminalDetails = mclsTerminalDetails;
                        ProdCodeWnd.ShowDialog(this);
                        DialogResult result = ProdCodeWnd.Result;
                        Details = ProdCodeWnd.Details;

                        ProdCodeWnd.Close();
                        ProdCodeWnd.Dispose();

                        if (result == DialogResult.OK && oldproductCode != Details.ProductCode)
                        {
                            Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                            mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                            mbodgItemRowClick = true;

                            clsEvent.AddEventLn("Updating item #".PadRight(15) + ":" + Details.ItemNo + "".PadRight(15) + " productcode from:" + oldproductCode + " to " + Details.ProductCode, true);

                            System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

                            dr = setCurrentRowItemDetails(dr, Details);

                            ComputeSubTotal(); setTotalDetails();

                            Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);

                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, Details);
                            clsSalesTransactions.CommitAndDispose();

                            clsEvent.AddEventLn("Updating item #".PadRight(15) + ":" + Details.ItemNo + " : done", true);

                            clsProduct.CommitAndDispose();

                            mbodgItemRowClick = false;

                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                        }
                    }
                }
            }

        }
        private void ChangeQuantity()
        {
            int iOldRow = dgItems.CurrentRowIndex;
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID")
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot change quantity if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangeQuantity);

                    if (loginresult == DialogResult.OK)
                    {
                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        decimal oldQuantity = Details.Quantity;
                        ChangeQuantityWnd QtyWnd = new ChangeQuantityWnd();
                        QtyWnd.Details = Details;
                        QtyWnd.TerminalDetails = mclsTerminalDetails;
                        QtyWnd.ShowDialog(this);
                        DialogResult result = QtyWnd.Result;
                        Details = QtyWnd.Details;

                        QtyWnd.Close();
                        QtyWnd.Dispose();

                        if (result == DialogResult.OK && oldQuantity != Details.Quantity)
                        {
                            Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                            mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                            if (mboIsRefund == false)
                            {
                                if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                                {
                                    if (Details.TransactionItemStatus != TransactionItemStatus.Return)
                                    {
                                        Data.ProductDetails det = clsProduct.Details(Details.ProductID, Details.VariationsMatrixID, mclsTerminalDetails.BranchID);

                                        decimal decProductCurrentQuantity = det.Quantity - det.ReservedQuantity + oldQuantity;

                                        // 04Sep2014 : Include exception for CreditPayment
                                        if (decProductCurrentQuantity < Details.Quantity &&
                                            mclsTerminalDetails.ShowItemMoreThanZeroQty &&
                                            Details.BarCode != Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_SUPER_CARD_MEMBERSHIP_FEE_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_SUPER_CARD_RENEWAL_FEE_BARCODE &&
                                            Details.BarCode != Data.Products.DEFAULT_SUPER_CARD_REPLACEMENT_FEE_BARCODE)
                                        {
                                            clsProduct.CommitAndDispose();
                                            MessageBox.Show("Sorry the quantity you entered is greater than the current stock. " + Environment.NewLine + "Current Stock: " + decProductCurrentQuantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                }
                            }

                            if (!mboIsRefund)
                            {
                                if (Details.TransactionItemStatus != TransactionItemStatus.Return)
                                {
                                    Data.ProductUnit clsProductUnit = new Data.ProductUnit(mConnection, mTransaction);
                                    mConnection = clsProductUnit.Connection; mTransaction = clsProductUnit.Transaction;

                                    decimal decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, oldQuantity);

                                    clsProduct.SubtractReservedQuantity(mclsTerminalDetails.BranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_QTY_RESERVE_AND_COMMIT_CHANGE_QTY), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                                }
                            }

                            //mbodgItemRowClick = true;

                            if (mboIsRefund)
                                ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Quantity");
                            else
                            {
                                Details = ApplyPromo(Details);

                                ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Quantity");

                                mbodgItemRowClick = false;
                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = iRow + 1; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Details = getCurrentRowItemDetails();

                                    System.Data.DataRow dr = dt.Rows[x];
                                    if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
                                    {
                                        Details = ApplyPromo(Details);
                                        ApplyChangeQuantityPriceAmountDetails(x, Details, "Change Quantity");
                                    }
                                }

                                dgItems.CurrentRowIndex = iOldRow;
                                dgItems.Select(iOldRow);
                                mbodgItemRowClick = true;
                            }
                            Details = getCurrentRowItemDetails();

                            // Added May 7, 2011 to Cater Reserved and Commit functionality    
                            // Details.Quantity = -oldQuantity + Details.Quantity;
                            // Jul 26, 2011 Change the AddQuantity and SubtractQuantity
                            ReservedAndCommitItem(Details, Details.TransactionItemStatus);

                            clsProduct.CommitAndDispose();
                            mbodgItemRowClick = false;

                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                        }
                    }
                }
            }
        }
        private void ChangePrice()
        {
            int iOldRow = dgItems.CurrentRowIndex;
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID" && dgItems[iRow, 8].ToString().IndexOf("RETURN") == -1)
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot change price if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangePrice);

                    if (loginresult == DialogResult.OK)
                    {
                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        decimal oldPrice = Details.Price;
                        ChangePriceWnd clsChangePriceWnd = new ChangePriceWnd();
                        clsChangePriceWnd.Details = Details;
                        clsChangePriceWnd.TerminalDetails = mclsTerminalDetails;
                        clsChangePriceWnd.ShowDialog(this);
                        DialogResult result = clsChangePriceWnd.Result;
                        Details = clsChangePriceWnd.Details;

                        clsChangePriceWnd.Close();
                        clsChangePriceWnd.Dispose();

                        if (result == DialogResult.OK && oldPrice != Details.Price)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            //mbodgItemRowClick = true;

                            if (mboIsRefund)
                                ApplyChangeQuantityPriceAmountDetails(iRow, Details);
                            else
                            {
                                Details = ApplyPromo(Details);

                                ApplyChangeQuantityPriceAmountDetails(iRow, Details);

                                mbodgItemRowClick = false;
                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = iRow + 1; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Details = getCurrentRowItemDetails();

                                    System.Data.DataRow dr = dt.Rows[x];
                                    if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
                                    {
                                        Details = ApplyPromo(Details);
                                        ApplyChangeQuantityPriceAmountDetails(x, Details);
                                    }

                                }
                                dgItems.CurrentRowIndex = iOldRow;
                                dgItems.Select(iOldRow);
                                mbodgItemRowClick = true;
                            }
                            Details = getCurrentRowItemDetails();
                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                            InsertAuditLog(AccessTypes.ChangePrice, "Change price for item " + Details.ProductCode + " to " + Details.Price.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                            mbodgItemRowClick = false;
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
        }
        private void ChangeAmount()
        {
            int iOldRow = dgItems.CurrentRowIndex;
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {

                if (dgItems[iRow, 8].ToString() != "VOID" && dgItems[iRow, 8].ToString().IndexOf("RETURN") == -1)
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot change price if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangePrice);

                    if (loginresult == DialogResult.OK)
                    {
                        //mbodgItemRowClick = true;

                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        decimal oldAmount = Details.Amount;
                        ChangeAmountWnd clsChangeAmountWnd = new ChangeAmountWnd();
                        clsChangeAmountWnd.Details = Details;
                        clsChangeAmountWnd.TerminalDetails = mclsTerminalDetails;
                        clsChangeAmountWnd.ShowDialog(this);
                        DialogResult result = clsChangeAmountWnd.Result;
                        Details = clsChangeAmountWnd.Details;

                        clsChangeAmountWnd.Close();
                        clsChangeAmountWnd.Dispose();

                        if (result == DialogResult.OK && oldAmount != Details.Amount)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            if (mboIsRefund)
                                ApplyChangeQuantityPriceAmountDetails(iRow, Details);
                            else
                            {
                                Details = ApplyPromo(Details);

                                ApplyChangeQuantityPriceAmountDetails(iRow, Details);

                                mbodgItemRowClick = false;
                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = iRow + 1; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Details = getCurrentRowItemDetails();

                                    System.Data.DataRow dr = dt.Rows[x];
                                    if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
                                    {
                                        Details = ApplyPromo(Details);
                                        ApplyChangeQuantityPriceAmountDetails(x, Details);
                                    }

                                }
                                dgItems.CurrentRowIndex = iOldRow;
                                dgItems.Select(iOldRow);
                                mbodgItemRowClick = true;
                            }
                            Details = getCurrentRowItemDetails();
                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                            InsertAuditLog(AccessTypes.ChangePrice, "Change amount for item " + Details.ProductCode + " to " + Details.Amount.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                            mbodgItemRowClick = false;
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
        }
		private void ChangePaxNo()
		{
			int iOldRow = dgItems.CurrentRowIndex;
			int iRow = dgItems.CurrentRowIndex;

			if (iRow >= 0)
			{
				if (dgItems[iRow, 8].ToString() != "VOID" && dgItems[iRow, 8].ToString().IndexOf("RETURN") == -1)
				{
					if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
					{
						MessageBox.Show("Sorry you cannot change pax no if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					int iMaxPaxNo = 1;
					try { iMaxPaxNo = int.Parse(lblOrders.Tag.ToString()); }
					catch { }
					if (iMaxPaxNo == 1) return;

					Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
					Cursor.Current = Cursors.WaitCursor;

					if (iMaxPaxNo >= Details.PaxNo + 1)
					{ Details.PaxNo += 1; }
					else { Details.PaxNo = 1; }

					Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems();
					clsSalesTransactionItems.UpdatePaxNo(Details.TransactionItemsID, mclsSalesTransactionDetails.TransactionDate, Details.PaxNo);
					clsSalesTransactionItems.CommitAndDispose();

					System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];
					dr["PaxNo"] = Details.PaxNo;
					
					DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
					DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
					InsertAuditLog(AccessTypes.None, "Change pax no for item " + Details.ProductCode + " to " + Details.Amount.ToString("#,##0.#0"));
					Cursor.Current = Cursors.Default;
					
				}
			}
		}
        private void ApplyChangeQuantityPriceAmountDetails(int iRow, Data.SalesTransactionItemDetails clsItemDetails, string strReason = "")
        {
            if (!string.IsNullOrEmpty(strReason)) clsEvent.AddEventLn(strReason, true);
            clsEvent.AddEventLn("Updating item #:" + clsItemDetails.ItemNo + "".PadRight(15) + ":" + clsItemDetails.BarCode + " " + clsItemDetails.ProductCode + " Price:" + clsItemDetails.Price + " Quantity:" + clsItemDetails.Quantity + " Amount:" + clsItemDetails.Amount + ".", true);

            clsItemDetails = ComputeItemTotal(clsItemDetails);

            System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

            dr = setCurrentRowItemDetails(dr, clsItemDetails);

            ComputeSubTotal(); setTotalDetails();

            clsItemDetails.TransactionID = Convert.ToInt64(lblTransNo.Tag);

            //mclsSalesTransactionDetails.SubTotal = mclsSalesTransactionDetails.SubTotal;
            /*******Added: January 18, 2008***********************
             * update purchase amount everytime there a change in 
             *  Quantity
             *  Price
             *  Amount *********************************/
            clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
            dr["PurchaseAmount"] = clsItemDetails.PurchaseAmount;

            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

            clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, clsItemDetails);
            clsSalesTransactions.CommitAndDispose();

            clsEvent.AddEventLn("Updating item #:" + clsItemDetails.ItemNo + " : done", true);

        }
        private void ReturnItem()
        {
            if (mboIsRefund)
                return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReturnItem);

            if (loginresult == DialogResult.OK)
            {
                TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
                clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
                clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
                clsTransactionNoWnd.TerminalDetails = mclsTerminalDetails;
                clsTransactionNoWnd.ShowDialog(this);
                DialogResult result = clsTransactionNoWnd.Result;
                string strTransactionNo = clsTransactionNoWnd.TransactionNo;
                string strTerminalNo = clsTransactionNoWnd.TerminalNo;
                clsTransactionNoWnd.Close();
                clsTransactionNoWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    TransactionReturnItemSelectWnd ItemWnd = new TransactionReturnItemSelectWnd();

                    ItemWnd.TransactionNo = strTransactionNo;
                    ItemWnd.TerminalDetails = mclsTerminalDetails;
                    ItemWnd.TransactionTerminalNo = strTerminalNo;
                    ItemWnd.ShowDialog(this);
                    if (ItemWnd.Result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (!mboIsInTransaction)
                        {
                            lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
                            if (!this.CreateTransaction()) return;
                        }

                        Data.SalesTransactionItemDetails clsItemDetails = ItemWnd.Details;

                        clsItemDetails = ComputeItemTotal(clsItemDetails); // set the grossales, vat, discounts, etc.

                        clsItemDetails.TransactionItemStatus = TransactionItemStatus.Return;

                        System.Data.DataRow dr = ItemDataTable.NewRow();

                        clsItemDetails.TransactionItemStatus = TransactionItemStatus.Return;
                        clsItemDetails.ItemNo = Convert.ToString(ItemDataTable.Rows.Count + 1);

                        dr = setCurrentRowItemDetails(dr, clsItemDetails);

                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        clsItemDetails.TransactionItemsID = AddItemToDB(clsItemDetails);
                        dr["TransactionItemsID"] = clsItemDetails.TransactionItemsID.ToString();

                        // Added May 7, 2011 to Cater Reserved and Commit functionality    
                        ReservedAndCommitItem(clsItemDetails, clsItemDetails.TransactionItemStatus);

                        ItemDataTable.Rows.Add(dr);

                        dgItems.CurrentRowIndex = ItemDataTable.Rows.Count;
                        dgItems.Select(dgItems.CurrentRowIndex);
                        
                        ComputeSubTotal(); setTotalDetails();

                        clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                        clsSalesTransactions.CommitAndDispose();

                        InsertAuditLog(AccessTypes.RefundTransaction, "Return Item " + clsItemDetails.ProductCode + "." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        try
                        {
                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke("RET-" + clsItemDetails.ProductCode, clsItemDetails.ProductUnitCode, clsItemDetails.Quantity, clsItemDetails.Price, clsItemDetails.Discount, clsItemDetails.PromoApplied, clsItemDetails.Amount, clsItemDetails.VAT, clsItemDetails.EVAT, null, null);
                        }
                        catch { }
                        if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                        {
                            PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
                            string strProductCode = clsItemDetails.ProductCode;
                            if (clsItemDetails.MatrixDescription != string.Empty && clsItemDetails.MatrixDescription != null) strProductCode += "-" + clsItemDetails.MatrixDescription;
                            PrintItemDel.BeginInvoke(clsItemDetails.ItemNo, strProductCode + " - RET ", clsItemDetails.ProductUnitCode, clsItemDetails.Quantity, clsItemDetails.Price, clsItemDetails.Discount, clsItemDetails.PromoApplied, clsItemDetails.Amount, clsItemDetails.VAT, clsItemDetails.EVAT, clsItemDetails.DiscountCode, clsItemDetails.ItemDiscountType, null, null);
                        }


                        Cursor.Current = Cursors.Default;
                    }

                    ItemWnd.Close();
                    ItemWnd.Dispose();
                }
            }
        }

        private void ApplyItemDiscount()
        {
            int iRow = dgItems.CurrentRowIndex;
            if (iRow < 0) return;

            if (dgItems[iRow, 8].ToString() != "VOID")
            {

                if (dgItems[iRow, 8].ToString().IndexOf("RETURN") == -1)
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot apply a discount to an item if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyItemDiscount);

                    if (loginresult == DialogResult.OK)
                    {
                        //mboIsDiscountAuthorized = true;
                        try
                        {
                            Data.SalesTransactionItemDetails clsItemDetails;
                            clsItemDetails = getCurrentRowItemDetails();
                            clsEvent.AddEvent("[" + lblCashier.Text + "] Applying item discount for item. no. [" + clsItemDetails.TransactionItemsID + "]" + clsItemDetails.ProductCode);

                        Back:
                            DiscountWnd discount = new DiscountWnd();
                            discount.Header = "Apply Item Discount for item #" + clsItemDetails.ItemNo + " - " + clsItemDetails.ProductCode;
                            discount.BalanceAmount = clsItemDetails.Amount;
                            discount.DiscountType = clsItemDetails.ItemDiscountType;
                            discount.DiscountAmount = clsItemDetails.ItemDiscount;
                            discount.DiscountCode = clsItemDetails.DiscountCode;
                            discount.Remarks = clsItemDetails.DiscountRemarks;
                            discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
                            discount.TerminalDetails = mclsTerminalDetails;
                            //discount.DisableVATExempt = true;
                            discount.ShowDialog(this);
                            decimal DiscountAmount = discount.DiscountAmount;
                            string ItemDiscountCode = discount.DiscountCode;
                            string ItemDiscountRemarks = discount.Remarks;
                            DiscountTypes itemDiscountType = discount.DiscountType;
                            DialogResult result = discount.Result;
                            discount.Close();
                            discount.Dispose();

                            if (result == DialogResult.OK)
                            {
                                if (ItemDiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                                {
                                    MessageBox.Show("Sorry you cannot use the Senior Citizen as item discount, Senior Citizen is applied thru transactions.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                    return;
                                }
                                else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                                {
                                    MessageBox.Show("Sorry Senior Citizen Discount is already applied in the transaction. Please separate the transaction for items without senior citizen discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                    return;
                                }

                                Cursor.Current = Cursors.WaitCursor;
                                clsItemDetails.ItemDiscount = DiscountAmount;
                                clsItemDetails.Discount = clsItemDetails.ItemDiscount;
                                clsItemDetails.ItemDiscountType = itemDiscountType;
                                clsItemDetails.DiscountCode = ItemDiscountCode;
                                clsItemDetails.DiscountRemarks = ItemDiscountRemarks;

                                clsItemDetails = ComputeItemTotal(clsItemDetails); // set the grossales, vat, discounts, etc.

                                if (!mclsSysConfigDetails.AllowDiscountGreaterThanAmount)
                                {
                                    if (clsItemDetails.Discount > clsItemDetails.Amount && clsItemDetails.DiscountCode != Constants.C_DISCOUNT_CODE_FREE)
                                    {
                                        MessageBox.Show("Sorry the discount is more than the transaction amount. Please select another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                        goto Back;
                                    }
                                }
                                if (decimal.Parse("0") >= clsItemDetails.Amount && clsItemDetails.DiscountCode == Constants.C_DISCOUNT_CODE_FREE)
                                {
                                    MessageBox.Show("Sorry the FREE discount cannot be more than 100%. Please set the FREE discount to 100% or less.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                    goto Back;
                                }

                                clsEvent.AddEventLn("discount=" + clsItemDetails.Discount.ToString("#,###.#0"));

                                System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

                                dr = setCurrentRowItemDetails(dr, clsItemDetails);

                                ComputeSubTotal(); setTotalDetails();

                                if (mclsSalesTransactionDetails.DiscountableAmount == 0)
                                {
                                    mclsSalesTransactionDetails.TransDiscountType = DiscountTypes.NotApplicable;
                                    mclsSalesTransactionDetails.TransDiscount = 0;
                                    ComputeSubTotal(); setTotalDetails();
                                }

                                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                                /*******Added: April 12, 2010***********************
                                 * update purchase amount everytime there a change in 
                                 *  
                                 *  discount *********************************/
                                clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
                                dr["PurchaseAmount"] = clsItemDetails.PurchaseAmount;

                                clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, clsItemDetails);
                                clsSalesTransactions.CommitAndDispose();

                                InsertAuditLog(AccessTypes.Discounts, "Apply item discount for " + clsItemDetails.ProductCode + ". discount=" + clsItemDetails.Discount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                                clsEvent.AddEventLn("Done applying item discount...", true);

                                DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                                DisplayItemToTurretDel.BeginInvoke(clsItemDetails.Description, clsItemDetails.ProductUnitCode, clsItemDetails.Quantity, clsItemDetails.Price, clsItemDetails.Discount, clsItemDetails.PromoApplied, clsItemDetails.Amount, clsItemDetails.VAT, clsItemDetails.EVAT, null, null);

                            }
                            else { clsEvent.AddEventLn("Cancelled!"); }
                        }
                        catch (Exception ex)
                        {
                            InsertErrorLogToFile(ex, "ERROR!!! Applying item discount.");
                        }

                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }
        private void ApplyItemDiscountForAllItem()
        {
            if (ItemDataTable.Rows.Count <= 0) return;

            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
            {
                MessageBox.Show("Sorry you cannot apply a discount to an item if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyItemDiscount);

            if (loginresult == DialogResult.OK)
            {
                //mboIsDiscountAuthorized = true;

                DiscountTypes TransDiscountType = DiscountTypes.NotApplicable;
                DiscountWnd discount = new DiscountWnd();
                discount.Header = "Apply Discounts on punched items";
                discount.BalanceAmount = mclsSalesTransactionDetails.SubTotal;
                discount.DiscountType = DiscountTypes.Percentage;
                discount.DiscountAmount = mclsSalesTransactionDetails.TransDiscount;
                discount.DiscountCode = mclsSalesTransactionDetails.DiscountCode;
                discount.Remarks = mclsSalesTransactionDetails.DiscountRemarks;
                discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
                discount.TerminalDetails = mclsTerminalDetails;
                //discount.DisableVATExempt = true;
                discount.ShowDialog(this);
                DialogResult result = discount.Result;
                decimal DiscountAmount = discount.DiscountAmount;
                string TransDiscountCode = discount.DiscountCode;
                string TransDiscountRemarks = discount.Remarks;
                TransDiscountType = discount.DiscountType;
                discount.Close();
                discount.Dispose();

                if (result == DialogResult.OK)
                {
                    if (TransDiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                    {
                        MessageBox.Show("Sorry you cannot use the Senior Citizen as item discount, Senior Citizen is applied thru transactions.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        return;
                    }
                    else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                    {
                        MessageBox.Show("Sorry Senior Citizen Discount is already applied in the transaction. Please separate the transaction for items without senior citizen discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;

                    int iCurrentSelectedRow = dgItems.CurrentRowIndex;

                    for (int iRowCtr = 0; iRowCtr < ItemDataTable.Rows.Count; iRowCtr++)
                    {
                        dgItems.Select(iRowCtr);
                        dgItems.CurrentRowIndex = iRowCtr;
                        if (dgItems[iRowCtr, 8].ToString() != "VOID")
                        {
                            if (dgItems[iRowCtr, 8].ToString().IndexOf("RETURN") == -1)
                            {
                                try
                                {
                                    Data.SalesTransactionItemDetails clsItemDetails;
                                    clsItemDetails = getCurrentRowItemDetails();
                                    clsEvent.AddEvent("[" + lblCashier.Text + "] Applying item discount for item. no. [" + clsItemDetails.TransactionItemsID + "]" + clsItemDetails.ProductCode);

                                    clsItemDetails.ItemDiscount = DiscountAmount;
                                    clsItemDetails.Discount = DiscountAmount;
                                    clsItemDetails.ItemDiscountType = TransDiscountType;
                                    clsItemDetails.DiscountCode = TransDiscountCode;
                                    clsItemDetails.DiscountRemarks = TransDiscountRemarks;

                                    clsItemDetails = ComputeItemTotal(clsItemDetails); // set the grossales, vat, discounts, etc.

                                    //if (clsItemDetails.Discount >= clsItemDetails.Amount)
                                    //{
                                    //    MessageBox.Show("Sorry the input discount will yield a less than ZERO amount. Please type another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                    //    goto Back;
                                    //}

                                    clsEvent.AddEventLn("discount=" + clsItemDetails.Discount.ToString("#,###.#0"));

                                    System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRowCtr];

                                    dr = setCurrentRowItemDetails(dr, clsItemDetails);

                                    ComputeSubTotal(); setTotalDetails();

                                    if (mclsSalesTransactionDetails.DiscountableAmount == 0)
                                    {
                                        mclsSalesTransactionDetails.TransDiscountType = DiscountTypes.NotApplicable;
                                        mclsSalesTransactionDetails.TransDiscount = 0;
                                        ComputeSubTotal(); setTotalDetails();
                                    }

                                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                                    /*******Added: April 12, 2010***********************
                                    * update purchase amount everytime there a change in 
                                    *  
                                    *  discount *********************************/
                                    clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
                                    dr["PurchaseAmount"] = clsItemDetails.PurchaseAmount;

                                    clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, clsItemDetails);
                                    clsSalesTransactions.CommitAndDispose();

                                    InsertAuditLog(AccessTypes.Discounts, "Apply item discount for " + clsItemDetails.ProductCode + ". discount=" + clsItemDetails.Discount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                                    clsEvent.AddEventLn("Done applying item discount...", true);

                                    DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                                    DisplayItemToTurretDel.BeginInvoke(clsItemDetails.Description, clsItemDetails.ProductUnitCode, clsItemDetails.Quantity, clsItemDetails.Price, clsItemDetails.Discount, clsItemDetails.PromoApplied, clsItemDetails.Amount, clsItemDetails.VAT, clsItemDetails.EVAT, null, null);
                                }
                                catch (Exception ex)
                                {
                                    InsertErrorLogToFile(ex, "ERROR!!! Applying discount for all item.");
                                }
                                Cursor.Current = Cursors.Default;
                            }
                        }

                        dgItems.UnSelect(iRowCtr);
                    }
                    dgItems.Select(iCurrentSelectedRow);
                    dgItems.CurrentRowIndex = iCurrentSelectedRow;
                }
                else { clsEvent.AddEventLn("Cancelled!"); }
            }
        }
		private void SelectTable()
		{
			// Sep 24, 2011      Lemuel E. Aceron
			// Added order slip wherein all punch items will not change sales and inventory
			// a customer named ORDER SLIP should be defined in contacts
			if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot select ORDER SLIP customer when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				clsEvent.AddEvent("[" + lblCashier.Text + "] Selecting customer.");

                DialogResult result; Data.ContactDetails details; bool isMergeTable = false;
				TableSelectWnd clsTableSelectWnd = new TableSelectWnd();
				clsTableSelectWnd.TerminalDetails = mclsTerminalDetails;
                clsTableSelectWnd.ContactGroupCategory = ContactGroupCategory.TABLES;
				clsTableSelectWnd.ShowAvailableTableOnly = mboIsInTransaction;
				clsTableSelectWnd.ShowDialog(this);
				details = clsTableSelectWnd.Details;
				result = clsTableSelectWnd.Result;
                isMergeTable = clsTableSelectWnd.isMergeTable;
				clsTableSelectWnd.Close();
				clsTableSelectWnd.Dispose();

				if (result == DialogResult.OK)
				{
                    if (isMergeTable)
                    {
                        List<Data.ContactDetails> lstDetails;
                        TableMergeWnd clsTableMergeWnd = new TableMergeWnd();
                        clsTableMergeWnd.TerminalDetails = mclsTerminalDetails;
                        clsTableMergeWnd.ContactGroupCategory = ContactGroupCategory.TABLES;
                        clsTableMergeWnd.ShowAvailableTableOnly = true; // inde pwede imerge and table na may laman na
                        clsTableMergeWnd.MainTableToMerge = details;
                        clsTableMergeWnd.ShowDialog(this);
                        //details = clsTableMergeWnd.Details;
                        result = clsTableMergeWnd.Result;
                        lstDetails = clsTableMergeWnd.MergeTables;
                        clsTableMergeWnd.Close();
                        clsTableMergeWnd.Dispose();

                        if (result == DialogResult.OK)
                        {
                            // 22Nov2014 : remove the merge tables when closed or void or create a new list
                            RemoveFromMergeTable(details.ContactCode);

                            if (lstDetails.Count > 0)
                            {
                                Data.MergeTable clsMergeTable = new Data.MergeTable();
                                Data.MergeTableDetails clsMergeTableDetails;

                                // insert the main table
                                clsMergeTableDetails = new Data.MergeTableDetails()
                                {
                                    MainTableCode = details.ContactCode,
                                    ChildTableCode = details.ContactCode
                                };
                                clsMergeTable.Insert(clsMergeTableDetails);

                                // insert the child tables
                                for (int x = 0; x < lstDetails.Count; x++)
                                {
                                    clsMergeTableDetails = new Data.MergeTableDetails()
                                    {
                                        MainTableCode = details.ContactCode,
                                        ChildTableCode = lstDetails[x].ContactCode
                                    };
                                    clsMergeTable.Insert(clsMergeTableDetails);

                                }
                                clsMergeTable.CommitAndDispose();
                            }
                        }
                        return;
                    }
					// Nov 18, 2011 : Lemu - auto suspend if already doing a transaction
					if (mboIsInTransaction)
					{
						if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID && mclsSalesTransactionDetails.CustomerID != details.ContactID)
						{
                            Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                            mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                            clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);
                            clsContacts.UpdateLastCheckInDate(details.ContactID, mclsSalesTransactionDetails.TransactionDate);

                            // Jan 31, 2015 : Lemu
                            // put back to SuspendedOpen so that it won't be open somewhere else
                            Data.SalesTransactions clsSalesTransactions1 = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions1.Connection; mTransaction = clsSalesTransactions1.Transaction;
                            clsEvent.AddEventLn("Putting transaction SuspendedOpen: " + mclsSalesTransactionDetails.TransactionNo);
                            clsSalesTransactions1.UpdateTransactionToSuspendedOpen(mclsSalesTransactionDetails.TransactionID);

                            clsContacts.CommitAndDispose();

							LoadContact(ContactGroupCategory.CUSTOMER, details);
							return;
						}
						else if (mclsSalesTransactionDetails.CustomerID == details.ContactID)
						{ return; }
						else if (mclsSalesTransactionDetails.CustomerID != details.ContactID)
						{
                            if (MessageBox.Show("Would you like to move from table: " + mclsSalesTransactionDetails.CustomerDetails.ContactCode + " to table: " + details.ContactCode + "." + Environment.NewLine + "Please click [Yes] to move, [Cancel] to create new transaction in the selected table.", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                            {
                                Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                                mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                                clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);
                                clsContacts.UpdateLastCheckInDate(details.ContactID, mclsSalesTransactionDetails.TransactionDate);

                                // Jan 31, 2015 : Lemu
                                // put back to SuspendedOpen so that it won't be open somewhere else
                                Data.SalesTransactions clsSalesTransactions1 = new Data.SalesTransactions(mConnection, mTransaction);
                                mConnection = clsSalesTransactions1.Connection; mTransaction = clsSalesTransactions1.Transaction;
                                clsEvent.AddEventLn("Putting transaction SuspendedOpen: " + mclsSalesTransactionDetails.TransactionNo);
                                clsSalesTransactions1.UpdateTransactionToSuspendedOpen(mclsSalesTransactionDetails.TransactionID);

                                clsContacts.CommitAndDispose();

                                LoadContact(ContactGroupCategory.CUSTOMER, details);
                                return;
                            }
                            else
                            {
                                this.SuspendTransaction(false); 
                            }
                        }
					}
                    else
                    {
                        Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                        mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                        clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);
                        clsContacts.UpdateLastCheckInDate(details.ContactID, mclsSalesTransactionDetails.TransactionDate);
                        clsContacts.CommitAndDispose();
                    }

					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

					string stTransactionNo = clsSalesTransactions.getSuspendedTransactionNo(details.ContactID, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);

					if (stTransactionNo != string.Empty)
					{
						LoadTransaction(stTransactionNo, mclsTerminalDetails.TerminalNo);
					}
					else
					{
						this.LoadOptions();
						LoadContact(ContactGroupCategory.CUSTOMER, details);
					}
                    clsSalesTransactions.CommitAndDispose();
				}
				else { 
					clsEvent.AddEventLn("Cancelled!"); 
				}
			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); }
		}
		private void SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory enumContactGroupCategory)
		{
			// Sep 24, 2011      Lemuel E. Aceron
			// Added order slip wherein all punch items will not change sales and inventory
			// a customer named ORDER SLIP should be defined in contacts
			if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && mboIsInTransaction && enumContactGroupCategory == AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER)
			{
				MessageBox.Show("Sorry you cannot select ORDER SLIP customer when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				switch (enumContactGroupCategory)
				{ 
					case AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER:
						MessageBox.Show("Sorry you cannot select a customer when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
					case AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT:
						MessageBox.Show("Sorry you cannot select an agent when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
				}

				return;
			}

			try
			{
				switch (enumContactGroupCategory)
				{
					case AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER:
						clsEvent.AddEvent("[" + lblCashier.Text + "] Selecting customer.");
						if (mclsTerminalDetails.ShowCustomerSelection == false)
						{
							clsEvent.AddEventLn("Cancelled! ShowCustomerSelection is OFF, reward is ON.");
							txtBarCode.Text = Constants.SWIPE_REWARD_CARD;
							txtBarCode.Focus(); txtBarCode.SelectionStart = txtBarCode.Text.Length + 1;
							return;
						}
						break;
					case AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT:
						clsEvent.AddEvent("[" + lblCashier.Text + "] Selecting agent.");
						break;
				}

				DialogResult result; Data.ContactDetails details;
				TableSelectWnd clsTableSelectWnd = new TableSelectWnd();
				clsTableSelectWnd.TerminalDetails = mclsTerminalDetails;
                clsTableSelectWnd.ContactGroupCategory = enumContactGroupCategory;
				clsTableSelectWnd.ShowAvailableTableOnly = mboIsInTransaction;
				clsTableSelectWnd.ShowDialog(this);
				details = clsTableSelectWnd.Details;
				result = clsTableSelectWnd.Result;
				clsTableSelectWnd.Close();
				clsTableSelectWnd.Dispose();

				if (result == DialogResult.OK)
				{
					// Nov 18, 2011 : Lemu - auto suspend if already doing a transaction
                    if (mboIsInTransaction)
                    {
                        if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID && mclsSalesTransactionDetails.CustomerID != details.ContactID)
                        {
                            Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                            mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                            clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);
                            clsContacts.UpdateLastCheckInDate(details.ContactID, mclsSalesTransactionDetails.TransactionDate);
                            clsContacts.CommitAndDispose();

                            LoadContact(ContactGroupCategory.CUSTOMER, details);
                            return;
                        }
                        else if (mclsSalesTransactionDetails.CustomerID == details.ContactID)
                        { return; }
                        else if (mclsSalesTransactionDetails.CustomerID != details.ContactID)
                        {
                            if (MessageBox.Show("Would you like to move from table: " + mclsSalesTransactionDetails.CustomerDetails.ContactCode + " to table: " + details.ContactCode + "." + Environment.NewLine + "Please click [Yes] to move, [Cancel] to create new transaction in the selected table.", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                            {
                                Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                                mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                                clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);
                                clsContacts.UpdateLastCheckInDate(details.ContactID, mclsSalesTransactionDetails.TransactionDate);
                                clsContacts.CommitAndDispose();

                                LoadContact(ContactGroupCategory.CUSTOMER, details);
                                return;
                            }
                            else
                            {
                                this.SuspendTransaction(false);
                            }
                        }
                    }
                    else
                    {
                        Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                        mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                        clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);
                        clsContacts.UpdateLastCheckInDate(details.ContactID, mclsSalesTransactionDetails.TransactionDate);
                        clsContacts.CommitAndDispose();
                    }

                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    string stTransactionNo = clsSalesTransactions.getSuspendedTransactionNo(details.ContactID, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);

                    if (stTransactionNo != string.Empty)
                    {
                        LoadTransaction(stTransactionNo, mclsTerminalDetails.TerminalNo);
                    }
                    else
                    {
                        this.LoadOptions();
                        LoadContact(ContactGroupCategory.CUSTOMER, details);

                        // 13Mar2015 : MPC, override the price using the PriceLevel
                        //             For PriceLevel1...5
                        if (mclsSysConfigDetails.EnablePriceLevel)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            Int32 iOldRow = dgItems.CurrentRowIndex;
                            Data.SalesTransactionItemDetails Details = new Data.SalesTransactionItemDetails();

                            Data.ProductPackage clsProductPackage = new Data.ProductPackage(mConnection, mTransaction);
                            mConnection = clsProductPackage.Connection; mTransaction = clsProductPackage.Transaction;

                            Data.ProductPackageDetails clsProductPackageDetails = new Data.ProductPackageDetails();

                            System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                dgItems.CurrentRowIndex = x;
                                Details = getCurrentRowItemDetails();

                                dgItems.UnSelect(x);
                                if (Details.TransactionItemStatus != TransactionItemStatus.Void)
                                {
                                    clsProductPackageDetails = clsProductPackage.Details(Details.ProductPackageID);

                                    switch (mclsContactDetails.PriceLevel)
                                    {
                                        case PriceLevel.SRP: Details.Price = clsProductPackageDetails.Price; break;
                                        case PriceLevel.One: Details.Price = clsProductPackageDetails.Price1 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price1; break;
                                        case PriceLevel.Two: Details.Price = clsProductPackageDetails.Price2 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price2; break;
                                        case PriceLevel.Three: Details.Price = clsProductPackageDetails.Price3 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price3; break;
                                        case PriceLevel.Four: Details.Price = clsProductPackageDetails.Price4 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price4; break;
                                        case PriceLevel.Five: Details.Price = clsProductPackageDetails.Price5 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price5; break;
                                        case PriceLevel.WSPrice: Details.Price = clsProductPackageDetails.WSPrice == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.WSPrice; break;
                                        default: Details.Price = clsProductPackageDetails.Price; break;
                                    }
                                    Details = ApplyPromo(Details);

                                    ApplyChangeQuantityPriceAmountDetails(x, Details, "Change Price: Change Contact");
                                }
                            }

                            dgItems.CurrentRowIndex = iOldRow;
                            dgItems.Select(iOldRow);

                            clsProductPackage.CommitAndDispose();

                            Details = getCurrentRowItemDetails();
                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                            InsertAuditLog(AccessTypes.ChangePrice, "Change price: change contact : for item " + Details.ProductCode + " to " + Details.Price.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                            mbodgItemRowClick = false;
                            Cursor.Current = Cursors.Default;
                        }
                    }
                    clsSalesTransactions.CommitAndDispose();
				}
				else { clsEvent.AddEventLn("Cancelled!"); }
			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); }
		}
		private void LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory enumContactGroupCategory, Data.ContactDetails pContactDetails)
		{
            try
            {
                if (mclsTerminalDetails.ShowCustomerSelection || pContactDetails.ContactID != 0)
                    mclsContactDetails = pContactDetails;
                else
                {
                    string strContactCardNo = txtBarCode.Text.Replace(Constants.SWIPE_REWARD_CARD, "").Trim();

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    // check using reward card info
                    mclsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
                    if (mclsContactDetails.ContactID != 0)
                        mboRewardCardSwiped = true;
                    else if (mclsContactDetails.ContactID == 0)
                    {
                        // check using credit card info
                        mclsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
                        if (mclsContactDetails.ContactID == 0 && strContactCardNo.Length == 7) mclsContactDetails = clsContact.DetailsByCreditCardNo("888880" + strContactCardNo);
                        if (mclsContactDetails.ContactID == 0 && strContactCardNo.Length == 7) mclsContactDetails = clsContact.DetailsByCreditCardNo("800000" + strContactCardNo);
                        if (mclsContactDetails.ContactID == 0 && strContactCardNo.Length == 9) mclsContactDetails = clsContact.DetailsByCreditCardNo("9902" + strContactCardNo);
                        if (mclsContactDetails.ContactID == 0 && strContactCardNo.Length == 9) mclsContactDetails = clsContact.DetailsByCreditCardNo("9903" + strContactCardNo);

                        if (mclsContactDetails.ContactID != 0)
                            mboCreditCardSwiped = true;
                        else if (mclsContactDetails.ContactID == 0)
                        {
                            strContactCardNo = strContactCardNo.Remove(strContactCardNo.Length - 1);
                            // check using reward card info
                            mclsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
                            if (mclsContactDetails.ContactID != 0)
                                mboRewardCardSwiped = true;
                            else if (mclsContactDetails.ContactID == 0)
                            {
                                // check using credit card info
                                mclsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
                                if (mclsContactDetails.ContactID != 0)
                                    mboCreditCardSwiped = true;
                                else if (mclsContactDetails.ContactID == 0)
                                { clsContact.CommitAndDispose(); SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER); return; }
                            }
                        }

                    }
                    clsContact.CommitAndDispose();
                }

                // Sep 24, 2011      Lemuel E. Aceron
                // Added order slip wherein all punch items will not change sales and inventory
                // a customer named ORDER SLIP should be defined in contacts
                if (mclsContactDetails.ContactName.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && mboIsInTransaction && enumContactGroupCategory == AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER)
                {
                    MessageBox.Show("Sorry you cannot select ORDER SLIP customer when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clsEvent.AddEventLn("Cancelled!"); return;
                }

                switch (enumContactGroupCategory)
                {
                    case AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER:
                        lblCustomer.Tag = mclsContactDetails.ContactID;
                        lblCustomer.Text = mclsContactDetails.ContactName;

                        if (!mclsTerminalDetails.ShowCustomerSelection) { txtBarCode.Text = string.Empty; txtBarCode.Focus(); }
                        clsEvent.AddEventLn("Done! Selected customer: " + lblCustomer.Text);

                        if (mboIsInTransaction)
						{
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            if (mboRewardCardSwiped)
                            {
                                mclsSalesTransactionDetails.RewardsCustomerID = mclsContactDetails.ContactID;
                                mclsSalesTransactionDetails.RewardsCustomerName = mclsContactDetails.ContactName;
                                clsSalesTransactions.UpdateRewardsContactUpdate(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.TransactionID, mclsContactDetails.ContactID, mclsContactDetails.ContactName);
                            }

							clsSalesTransactions.UpdateContact(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsContactDetails);
							clsSalesTransactions.CommitAndDispose();
						}
                        if (mboRewardCardSwiped)
                        {
                            mclsSalesTransactionDetails.RewardsCustomerID = mclsContactDetails.ContactID;
                            mclsSalesTransactionDetails.RewardsCustomerName = mclsContactDetails.ContactName;
                            mclsSalesTransactionDetails.RewardCardActive = mclsContactDetails.RewardDetails.RewardActive;
                            mclsSalesTransactionDetails.RewardCardNo = mclsContactDetails.RewardDetails.RewardCardNo;
                            mclsSalesTransactionDetails.RewardPreviousPoints = mclsContactDetails.RewardDetails.RewardPoints;
                            mclsSalesTransactionDetails.RewardCurrentPoints = mclsSalesTransactionDetails.RewardPreviousPoints;

                            // no need to check if the current customer for the transaction is the default customer
                            //if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                            //{
                            mclsSalesTransactionDetails.CustomerID = mclsContactDetails.ContactID;
                            mclsSalesTransactionDetails.CustomerName = mclsContactDetails.ContactName;
                            mclsSalesTransactionDetails.CustomerDetails = mclsContactDetails;
                            //}
                        }
                        else
                        {
                            mclsSalesTransactionDetails.CustomerID = mclsContactDetails.ContactID;
                            mclsSalesTransactionDetails.CustomerName = mclsContactDetails.ContactName;
                            mclsSalesTransactionDetails.CustomerDetails = mclsContactDetails;
                        }

                        break;
                    case AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT:
                        lblAgent.Tag = mclsContactDetails.ContactID;
                        lblAgent.Text = mclsContactDetails.ContactName;
                        lblAgentPositionDepartment.Text = mclsContactDetails.PositionName;
                        lblAgentPositionDepartment.Tag = mclsContactDetails.DepartmentName;
                        clsEvent.AddEventLn("Done! Selected agent: " + lblAgent.Text);

                        if (mboIsInTransaction)
						{
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateAgent(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, pContactDetails);
							clsSalesTransactions.CommitAndDispose();
						}
                        mclsSalesTransactionDetails.AgentID = pContactDetails.ContactID;
                        mclsSalesTransactionDetails.AgentName = pContactDetails.ContactName;

                        break;
                }
            }
            catch (Exception ex)
            { clsEvent.AddErrorEventLn(ex); }
		}
        private void UpdateContact()
        {
            try
            {

                DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts);
                if (loginresult == DialogResult.OK)
                {
                    loginresult = System.Windows.Forms.DialogResult.Cancel;
                    Data.ContactDetails clsContactDetails = new Data.ContactDetails();
                    if (mclsSalesTransactionDetails.CustomerID != 0 && mclsSalesTransactionDetails.CustomerID != Constants.C_RETAILPLUS_CUSTOMERID)
                    {
                        Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                        mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;
                        clsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
                        clsContact.CommitAndDispose();
                        loginresult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                        clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                        clsContactWnd.TerminalDetails = mclsTerminalDetails;
                        clsContactWnd.ContactGroupCategory = Data.ContactGroupCategory.CUSTOMER;
                        clsContactWnd.ShowDialog(this);
                        clsContactDetails = clsContactWnd.Details;
                        loginresult = clsContactWnd.Result;
                        clsContactWnd.Close();
                        clsContactWnd.Dispose();
                    }

                    if (loginresult == System.Windows.Forms.DialogResult.OK)
                    {
                        DialogResult addresult = System.Windows.Forms.DialogResult.Cancel;
                        if (!mclsTerminalDetails.ShowCustomerSelection)
                        {
                            ContactAddDetWnd clsContactAddWnd = new ContactAddDetWnd();
                            clsContactAddWnd.Caption = "Update Customer [" + mclsContactDetails.ContactName + "]";
                            clsContactAddWnd.ContactDetails = clsContactDetails;
                            clsContactAddWnd.ShowDialog(this);
                            addresult = clsContactAddWnd.Result;
                            clsContactDetails = clsContactAddWnd.ContactDetails;
                            clsContactAddWnd.Close();
                            clsContactAddWnd.Dispose();
                        }
                        else
                        {
                            ContactAddWnd clsContactAddWnd = new ContactAddWnd();
                            clsContactAddWnd.Caption = "Update Customer [" + mclsContactDetails.ContactName + "]";
                            clsContactAddWnd.ContactDetails = clsContactDetails;
                            clsContactAddWnd.ShowDialog(this);
                            addresult = clsContactAddWnd.Result;
                            clsContactDetails = clsContactAddWnd.ContactDetails;
                            clsContactAddWnd.Close();
                            clsContactAddWnd.Dispose();
                        }
                        if (addresult == DialogResult.OK)
                        {
                            LoadContact(Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                            MessageBox.Show("Customer has been updated and the details has been reloaded for this transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch { }
        }
        /// <summary>
        /// Check if there is an ongoing transaction
        /// If yes, returns true if will continue
        /// If No, returns false
        /// </summary>
        /// <returns></returns>
        private bool SuspendTransactionAndContinue()
        {
            bool boRetValue = true;

            if (mboIsInTransaction)
            {
                if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return SuspendTransaction(true);
                }
                else
                    boRetValue = false;
            }

            return boRetValue;

        }
		private bool SuspendTransaction(bool ShowNotificationWindow = true)
		{
			bool boRetValue = false;

			if (!mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot suspend an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return boRetValue;
			}
			if (mboIsRefund)
			{
				MessageBox.Show("Sorry you cannot suspend a REFUND transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return boRetValue;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.SuspendTransaction);

			if (loginresult == DialogResult.OK)
			{
				if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				{
					try
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Suspending transaction no. " + lblTransNo.Text);

                        DialogResult addresult; Data.ContactDetails details;
						TableSelectWnd clsTableSelectWnd = new TableSelectWnd();
						clsTableSelectWnd.TerminalDetails = mclsTerminalDetails;
						clsTableSelectWnd.ShowAvailableTableOnly = mboIsInTransaction;
                        clsTableSelectWnd.ContactGroupCategory = ContactGroupCategory.TABLES;
						clsTableSelectWnd.ShowDialog(this);
						details = clsTableSelectWnd.Details;
						addresult = clsTableSelectWnd.Result;
						clsTableSelectWnd.Close();
						clsTableSelectWnd.Dispose();

                        if (addresult == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            LoadContact(Data.ContactGroupCategory.CUSTOMER, details);

                            lblCustomer.Text = details.ContactName;
                            lblCustomer.Tag = details.ContactID.ToString();

                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.Suspend(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, details);

                            // Sep 24, 2014 : update back the LastCheckInDate to min date
                            Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                            mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                            clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);
                            clsContact.UpdateLastCheckInDate(details.ContactID, mclsSalesTransactionDetails.TransactionDate);

                            InsertAuditLog(AccessTypes.SuspendTransaction, "Suspend transaction #: " + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                                PrintReportFooterSection(true, TransactionStatus.Suspended, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

                            clsSalesTransactions.CommitAndDispose();
                            clsEvent.AddEventLn("Done!");

                            if (mclsTerminalDetails.WithRestaurantFeatures)
                            {
                                // show only the message if success printing.
                                // error message is already shown during error printing.
                                if (PrintOrderSlip(false, true))
                                    MessageBox.Show("Transaction has been SUSPENDED & Order's has been re-send to Kitchen/Bar printer's. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK);
                            }
                            else
                                MessageBox.Show("Transaction has been SUSPENDED. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK);

                            this.LoadOptions();

                            boRetValue = true;

                            Cursor.Current = Cursors.Default;
                        }
                        else { clsEvent.AddEventLn("Cancelled!"); }
					}
					catch (Exception ex)
					{ clsEvent.AddErrorEventLn(ex); }
				}
				else
				{
                    try
                    {
                        clsEvent.AddEvent("[" + lblCashier.Text + "] Suspending transaction no. " + lblTransNo.Text);

                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        clsSalesTransactions.Suspend(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales);

                        InsertAuditLog(AccessTypes.SuspendTransaction, "Suspend transaction #: " + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                            PrintReportFooterSection(true, TransactionStatus.Suspended, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

                        clsSalesTransactions.CommitAndDispose();
                        clsEvent.AddEventLn("Done!");

                        if (mclsTerminalDetails.WithRestaurantFeatures)
                        {
                            // show only the message if success printing.
                            // error message is already shown during error printing.
                            if (PrintOrderSlip(false, true))
                                MessageBox.Show("Transaction has been SUSPENDED & Order's has been re-send to Kitchen/Bar printer's. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK);
                            
                        }
                        else
                            MessageBox.Show("Transaction has been SUSPENDED. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK);

                        this.LoadOptions();

                        boRetValue = true;
                    }
                    catch (Exception ex)
                    { clsEvent.AddErrorEventLn(ex); }
				}
			}
			return boRetValue;
		}
		private bool SelectWaiter()
		{
            bool retValue = false;

            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot select a waiter when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return retValue;
            }

            try
            {
                clsEvent.AddEvent("[" + lblCashier.Text + "] Selecting waiter.");

                WaiterSelectWnd clsWaiterSelectWnd = new WaiterSelectWnd();
                clsWaiterSelectWnd.TerminalDetails = mclsTerminalDetails;
                clsWaiterSelectWnd.ShowDialog(this);
                long iWaiterID = clsWaiterSelectWnd.getWaiterID;
                string stWaiterName = clsWaiterSelectWnd.getWaiterName;
                DialogResult result = clsWaiterSelectWnd.Result;
                clsWaiterSelectWnd.Close();
                clsWaiterSelectWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    lblServedBy.Text = "Served by: " + stWaiterName;
                    lblServedBy.Tag = iWaiterID.ToString();
                    clsEvent.AddEventLn("Done! Selected Waiter: " + stWaiterName);

                    if (mboIsInTransaction)
                    {
                        mclsSalesTransactionDetails.WaiterID = iWaiterID;
                        mclsSalesTransactionDetails.WaiterName = stWaiterName;
                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        clsSalesTransactions.UpdateWaiter(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, iWaiterID, stWaiterName);
                        clsSalesTransactions.CommitAndDispose();

                        retValue = true;
                    }
                }
                else { clsEvent.AddEventLn("Cancelled!"); }
            }
            catch (Exception ex)
            { clsEvent.AddErrorEventLn(ex); }

            return retValue;
		}
		private void ResumeTransaction()
		{
            if (!SuspendTransactionAndContinue()) return;

            // ShowOneTerminalSuspendedTransactions 
            // Only same cashier in same terminal can be resume.
            // if terminalno and cashier is not the same to not allow   
            if (mclsTerminalDetails.ShowOneTerminalSuspendedTransactions)
            {
                Data.SalesTransactions clsSales = new Data.SalesTransactions(mConnection, mTransaction);
                int count = clsSales.CountSuspended(mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, mclsTerminalDetails.BranchID);
                clsSales.CommitAndDispose();

                if (count == 0)
                {
                    MessageBox.Show("No suspended transaction found for this day.", "RetailPlus", MessageBoxButtons.OK);
                    return;
                }
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ResumeTransaction);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    ResumeTransactionWnd ResumeWnd = new ResumeTransactionWnd();

                    ResumeWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                    ResumeWnd.TerminalDetails = mclsTerminalDetails;
                    ResumeWnd.ShowDialog(this);
                    DialogResult result = ResumeWnd.Result;
                    Data.SalesTransactionDetails details = ResumeWnd.Details;
                    ResumeWnd.Close();
                    ResumeWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        if (details.TransactionStatus == TransactionStatus.SuspendedOpen)
                        {
                            if (MessageBox.Show("This transaction is already open in another terminal. Please suspend in the other terminal first before opening." + Environment.NewLine + "Would you like to force open this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                DialogResult resResumeSuspendedOpenTransaction = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ResumeSuspendedOpenTransaction);

                                if (resResumeSuspendedOpenTransaction != System.Windows.Forms.DialogResult.OK)
                                {
                                    clsEvent.AddEvent("[" + lblCashier.Text + "] Resuming transaction no. " + details.TransactionNo + " cancelled. SuspendedOpen");
                                    return;
                                }
                            }
                        }

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Resuming transaction no. " + details.TransactionNo);

                        mclsSalesTransactionDetails = details;

                        if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Refund || mclsSalesTransactionDetails.TransactionType == TransactionTypes.POSRefund)
                        {
                            mboIsRefund = true;
                            lblSubtotalName.Text = "SUBTOTAL: REFUND";
                            lblOrderType.Visible = false;
                        }
                        lblOrderType.Text = mclsSalesTransactionDetails.OrderType.ToString("G").ToUpper();
                        lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;
                        lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();
                        lblCustomer.Text = mclsSalesTransactionDetails.CustomerName;
                        lblCustomer.Tag = mclsSalesTransactionDetails.CustomerID.ToString();
                        lblAgent.Text = mclsSalesTransactionDetails.AgentName;
                        lblAgent.Tag = mclsSalesTransactionDetails.AgentID.ToString();
                        lblAgentPositionDepartment.Text = mclsSalesTransactionDetails.AgentPositionName;
                        lblAgentPositionDepartment.Tag = mclsSalesTransactionDetails.AgentDepartmentName;
                        lblServedBy.Text = "Served by: " + details.WaiterName;
                        lblServedBy.Tag = mclsSalesTransactionDetails.WaiterID.ToString();

                        lblTransDate.Text = mclsSalesTransactionDetails.TransactionDate.ToString("MMM. dd, yyyy hh:mm:ss tt");
                        mdteOverRidingPrintDate = mclsSalesTransactionDetails.TransactionDate;

                        lblTransDiscount.Tag = mclsSalesTransactionDetails.TransDiscountType.ToString("d");
                        // lblConsignment.Visible = mclsSalesTransactionDetails.isConsignment; not applicable for restaurant

                        if (mclsSalesTransactionDetails.ChargeAmount == 0)
                            lblTransCharge.Tag = ChargeTypes.NotApplicable.ToString("d");
                        else
                        {
                            Data.ChargeType clsChargeType = new Data.ChargeType(mConnection, mTransaction);
                            bool bolInPercent = clsChargeType.Details(mclsSalesTransactionDetails.ChargeCode).InPercent;
                            clsChargeType.CommitAndDispose();

                            if (bolInPercent)
                                lblTransCharge.Tag = ChargeTypes.Percentage.ToString("d");
                            else
                                lblTransCharge.Tag = ChargeTypes.FixedValue.ToString("d");
                        }

                        // Aug 6, 2011 : Lemu
                        // Put here from CloseTransaction
                        try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
                        catch { }
                        mclsSalesTransactionDetails.CashierName = lblCashier.Text;

                        LoadResumedItems(details.TransactionItems, false);

                        // Jan 31, 2015 : Lemu
                        // put back to SuspendedOpen so that it won't be open somewhere else
                        if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Suspended)
                        {
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;
                            clsEvent.AddEvent("Putting transaction SuspendedOpen: " + mclsSalesTransactionDetails.TransactionNo);
                            clsSalesTransactions.UpdateTransactionToSuspendedOpen(mclsSalesTransactionDetails.TransactionID);
                            clsSalesTransactions.CommitAndDispose();
                        }

                        mboIsInTransaction = true;

                        InsertAuditLog(AccessTypes.ResumeTransaction, "Resume transaction #: " + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        clsEvent.AddEventLn("[" + lblCashier.Text + "] Resuming transaction no. " + details.TransactionNo + " Done.", true);
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }


                }
                catch (Exception ex)
                { clsEvent.AddErrorEventLn(ex); }
            }
		}
        private void VoidTransaction()
        {
            if (!mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot void an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to void this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.VoidTransaction);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEventLn("[" + lblCashier.Text + "] Voiding transaction no. " + lblTransNo.Text, true);

                    mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Void;

                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
                        if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)
                    {
                        //if (mclsTerminalDetails.IsPrinterAutoCutter)
                        //    PrintReportPageHeaderSectionChecked(true);
                        //else
                        PrintReportHeadersSection(true);

                        mboIsItemHeaderPrinted = true;

                        foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                        {
                            string stItemNo = "" + dr["ItemNo"].ToString();
                            string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                            decimal decPrice = Convert.ToDecimal(dr["Price"]);
                            decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                            decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                            decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                            decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                            decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);
                            string stProductCode = "";
                            decimal decQuantity = 0;
                            string stDiscountCode = "" + dr["DiscountCode"].ToString();
                            DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString());

                            if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
                            {
                                stProductCode = "" + dr["ProductCode"].ToString() + "-RET";
                                decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
                                decAmount = -decAmount;
                            }
                            else if (dr["Quantity"].ToString() != "VOID")
                            {
                                stProductCode = "" + dr["ProductCode"].ToString();
                                decQuantity = Convert.ToDecimal(dr["Quantity"]);
                            }
                            if (dr["Quantity"].ToString().IndexOf("VOID") != -1)
                            {
                                if (mclsTerminalDetails.WillPrintVoidItem)
                                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                        PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT, stDiscountCode, ItemDiscountType);
                            }
                            else
                            {
                                if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                    PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT, stDiscountCode, ItemDiscountType);
                            }
                        }
                    }

                    Cursor.Current = Cursors.WaitCursor;

                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    // Added May 7, 2011 to Cater Reserved and Commit functionality    
                    #region Reserved And Commit
                    //if (mclsTerminalDetails.ReservedAndCommit == true)
                    //{
                    System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        dgItems.CurrentRowIndex = x;
                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        if (Details.TransactionItemStatus != TransactionItemStatus.Void)
                        {
                            Details.TransactionItemStatus = TransactionItemStatus.Void;
                            ReservedAndCommitItem(Details, Details.TransactionItemStatus);
                        }
                    }
                    //}
                    #endregion

                    //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                    UpdateTerminalReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                    //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                    UpdateCashierReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                    clsSalesTransactions.Void(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);

                    clsSalesTransactions.UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

                    // Sep 24, 2014 : update back the LastCheckInDate to min date
                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                    // 22Nov2014 : remove the merge tables when closed or void
                    RemoveFromMergeTable(mclsSalesTransactionDetails.CustomerDetails.ContactCode);

                    try
                    {
                        clsSalesTransactions.CommitAndDispose();
                    }
                    catch
                    {
                        mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Open;
                    }

                    InsertAuditLog(AccessTypes.VoidTransaction, "VOID transaction #:" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                    clsEvent.AddEventLn("Done transaction no. " + lblTransNo.Text + " has been void.", true);

                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                    {
                        PrintReportFooterSection(true, TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
                    {
                        PrintSalesInvoice();
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.DeliveryReceipt)
                    {
                        PrintDeliveryReceipt();
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceAndDR)
                    {
                        PrintSalesInvoice();
                        PrintDeliveryReceipt();
                    }
                    //Added April 12, 2014
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.OfficialReceiptAndDR)
                    {
                        PrintOfficialReceipt();
                        PrintDeliveryReceipt();
                    }
                    //Added February 10, 2010
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
                    {
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
                    }
                    //Added May 11, 2010
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
                    {
                        PrintDeliveryReceipt();
                    }
                    //Added January 17, 2011
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
                    {
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusPrinter);
                    }
                    //Added February 22, 2011
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
                    {
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon); //8.5inc x 7inch
                    }

                    //Data.Contact clsContact = new Data.Contact();
                    //clsContact.Update(mclsSalesTransactionDetails.CustomerName, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CustomerID);
                    //clsContact.CommitAndDispose();
                    this.LoadOptions();

                    MessageBox.Show("Transaction has been VOID. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Voiding transaction.");
                }
            }
        }
        private void WithHold()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Withhold);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] WithHolding amount.");

                    WithholdWnd frmWithHoldWnd = new WithholdWnd();
                    frmWithHoldWnd.TerminalDetails = mclsTerminalDetails;
                    frmWithHoldWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                    frmWithHoldWnd.ShowDialog(this);
                    DialogResult result = frmWithHoldWnd.Result;
                    Data.WithholdDetails clsWithHoldDetails = frmWithHoldWnd.WithHoldDetails;
                    frmWithHoldWnd.Close();
                    frmWithHoldWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        Data.Withhold clsWithHold = new Data.Withhold(mConnection, mTransaction);
                        mConnection = clsWithHold.Connection; mTransaction = clsWithHold.Transaction;

                        clsWithHold.Insert(clsWithHoldDetails);
                        clsWithHold.CommitAndDispose();

                        InsertAuditLog(AccessTypes.Withhold, "WithHold payment: type='" + clsWithHoldDetails.PaymentType.ToString("G") + "' amount='" + clsWithHoldDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        //PrintWithHoldDelegate printwithholDel = new PrintWithHoldDelegate(PrintWithHold);
                        //printwithholDel.BeginInvoke(clsWithHoldDetails, null, null);
                        PrintWithHold(clsWithHoldDetails);

                        // Sep 28, 2011 : Lemu 
                        // As per request of houseware plaze. Print a second copy
                        //printwithholDel = new PrintWithHoldDelegate(PrintWithHold);
                        //printwithholDel.BeginInvoke(clsWithHoldDetails, null, null);
                        System.Threading.Thread.Sleep(100);
                        PrintWithHold(clsWithHoldDetails);

                        clsEvent.AddEventLn("Done! type=" + clsWithHoldDetails.PaymentType.ToString("G") + " amount=" + clsWithHoldDetails.Amount.ToString("#,###.#0"));

                        Cursor.Current = Cursors.Default;
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }

                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Withholding amount.");
                }
            }
        }
        private void Disburse()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Disburse);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Disbursing amount.");

                    DisburseWnd frmDisburseWnd = new DisburseWnd();
                    frmDisburseWnd.TerminalDetails = mclsTerminalDetails;
                    frmDisburseWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                    frmDisburseWnd.ShowDialog(this);
                    DialogResult result = frmDisburseWnd.Result;
                    Data.DisburseDetails clsDisburseDetails = frmDisburseWnd.DisburseDetails;
                    frmDisburseWnd.Close();
                    frmDisburseWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        Data.Disburses clsDisburse = new Data.Disburses(mConnection, mTransaction);
                        mConnection = clsDisburse.Connection; mTransaction = clsDisburse.Transaction;

                        clsDisburse.Insert(clsDisburseDetails);
                        clsDisburse.CommitAndDispose();

                        InsertAuditLog(AccessTypes.Disburse, "Disburse: type='" + clsDisburseDetails.PaymentType.ToString("G") + "' amount='" + clsDisburseDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        //PrintDisbursementDelegate printdisburseDel = new PrintDisbursementDelegate(PrintDisbursement);
                        //printdisburseDel.BeginInvoke(clsDisburseDetails, null, null);
                        PrintDisbursement(clsDisburseDetails);

                        // Sep 28, 2011 : Lemu 
                        // As per request of houseware plaze. Print a second copy
                        //printdisburseDel = new PrintDisbursementDelegate(PrintDisbursement);
                        //printdisburseDel.BeginInvoke(clsDisburseDetails, null, null);
                        System.Threading.Thread.Sleep(100);
                        PrintDisbursement(clsDisburseDetails);

                        clsEvent.AddEventLn("Done! type=" + clsDisburseDetails.PaymentType.ToString("G") + " amount=" + clsDisburseDetails.Amount.ToString("#,###.#0"));

                        Cursor.Current = Cursors.Default;
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }

                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Disbursing amount.");
                }
            }
        }
        private void PaidOut()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PaidOut);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing paid-out amount.");

                    PaidOutWnd frmPaidOutWnd = new PaidOutWnd();
                    frmPaidOutWnd.TerminalDetails = mclsTerminalDetails;
                    frmPaidOutWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                    frmPaidOutWnd.ShowDialog(this);
                    DialogResult result = frmPaidOutWnd.Result;
                    Data.PaidOutDetails clsPaidOutDetails = frmPaidOutWnd.PaidOutDetails;
                    frmPaidOutWnd.Close();
                    frmPaidOutWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        Data.PaidOut clsPaidOut = new Data.PaidOut(mConnection, mTransaction);
                        mConnection = clsPaidOut.Connection; mTransaction = clsPaidOut.Transaction;

                        clsPaidOut.Insert(clsPaidOutDetails);
                        clsPaidOut.CommitAndDispose();

                        InsertAuditLog(AccessTypes.PaidOut, "Paid-out: type='" + clsPaidOutDetails.PaymentType.ToString("G") + "' amount='" + clsPaidOutDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        //PrintPaidOutDelegate paidoutDel = new PrintPaidOutDelegate(PrintPaidOut);
                        //paidoutDel.BeginInvoke(clsPaidOutDetails, null, null);
                        PrintPaidOut(clsPaidOutDetails);

                        // Sep 28, 2011 : Lemu 
                        // As per request of houseware plaze. Print a second copy
                        //paidoutDel = new PrintPaidOutDelegate(PrintPaidOut);
                        //paidoutDel.BeginInvoke(clsPaidOutDetails, null, null);
                        System.Threading.Thread.Sleep(100);
                        PrintPaidOut(clsPaidOutDetails);

                        clsEvent.AddEventLn("Done! type=" + clsPaidOutDetails.PaymentType.ToString("G") + " amount=" + clsPaidOutDetails.Amount.ToString("#,###.#0"));

                        Cursor.Current = Cursors.Default;
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing paid out.");
                }
            }
        }
        private void Deposit()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Deposit);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Depositing amount.");

                    DepositWnd frmDepositWnd = new DepositWnd();
                    frmDepositWnd.TerminalDetails = mclsTerminalDetails;
                    frmDepositWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                    frmDepositWnd.ShowDialog(this);
                    DialogResult result = frmDepositWnd.Result;
                    Data.DepositDetails clsDepositDetails = frmDepositWnd.DepositDetails;
                    frmDepositWnd.Close();
                    frmDepositWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        Data.Deposits clsDeposit = new Data.Deposits(mConnection, mTransaction);
                        mConnection = clsDeposit.Connection; mTransaction = clsDeposit.Transaction;

                        clsDeposit.Insert(clsDepositDetails);

                        Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                        mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                        clsContact.AddDebit(clsDepositDetails.ContactID, clsDepositDetails.Amount);
                        clsDeposit.CommitAndDispose();

                        InsertAuditLog(AccessTypes.Deposit, "Deposit: type='" + clsDepositDetails.PaymentType.ToString("G") + "' amount='" + clsDepositDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        PrintDeposit(clsDepositDetails);
                        //PrintDepositDelegate printdepositDel = new PrintDepositDelegate(PrintDeposit);
                        //printdepositDel.BeginInvoke(clsDepositDetails, null, null);

                        clsEvent.AddEventLn("Done! type=" + clsDepositDetails.PaymentType.ToString("G") + " amount=" + clsDepositDetails.Amount.ToString("#,###.#0"));

                        Cursor.Current = Cursors.Default;
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }

                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Receiving customer deposit.");
                }
            }
        }
		private void ShowOtherCommands()
		{
            // if (!SuspendTransactionAndContinue()) return;

            OtherCommandsWnd clsOtherCommandsWnd = new OtherCommandsWnd();
            clsOtherCommandsWnd.TerminalDetails = mclsTerminalDetails;
            clsOtherCommandsWnd.CashierID = mclsSalesTransactionDetails.CashierID;
            clsOtherCommandsWnd.SalesTransactionDetails = mclsSalesTransactionDetails;
            clsOtherCommandsWnd.ShowDialog(this);
            DialogResult result = clsOtherCommandsWnd.Result;
            Keys KeyData = clsOtherCommandsWnd.KeyData;
            clsOtherCommandsWnd.Close();
            clsOtherCommandsWnd.Dispose();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                switch (KeyData)
                {
                    case Keys.F1:
                        InitializeZRead(false);
                        break;

                    case Keys.F2:
                        if (mclsTerminalDetails.CashCountBeforeReport)
                        { if (!mboIsCashCountInitialized) CashCount(); }
                        else { CashCount(); }
                        break;

                    case Keys.F3:
                        PaidOut();
                        break;

                    case Keys.F4:
                        Disburse();
                        break;

                    case Keys.F5:
                        WithHold();
                        break;

                    case Keys.F6:
                        RefundTransaction();
                        break;

                    case Keys.F7:
                        ReturnItem();
                        break;

                    case Keys.F8:
                        //OpenDrawer();
                        OpenTransactionDrawer();
                        break;

                    case Keys.F9:
                        PrintOrderSlip(true);
                        break;

                    case Keys.F10:
                        PrintPLUPerOrderSlipPrinter();
                        break;

                    case Keys.F11:
                        EnterCreditItemizePayment();
                        break;

                    case Keys.F12:
                        ApplyTransCharge();
                        break;

                    case Keys.F13:
                        ChangeZeroRated(!mclsSalesTransactionDetails.isZeroRated);
                        break;
                }
            }
		}
		private void ShowPrintWindow()
		{
            if (!SuspendTransactionAndContinue()) return;

			// Dec 01, 2008      Lemuel E. Aceron
			// added the IsCashCountInitialized for 1 time 
			// Cash count every printing of report.
			if (mclsTerminalDetails.CashCountBeforeReport)
			{ if (!mboIsCashCountInitialized) { if (!CashCount()) return; } }

			ReportsWnd clsReportsWnd = new ReportsWnd();
            clsReportsWnd.TerminalDetails = mclsTerminalDetails;
			clsReportsWnd.CashierID = mclsSalesTransactionDetails.CashierID;
			clsReportsWnd.ShowDialog(this);
			DialogResult reportsresult = clsReportsWnd.Result;
			Keys KeyData = clsReportsWnd.KeyData;
			clsReportsWnd.Close();
			clsReportsWnd.Dispose();

			switch (KeyData)
			{
                case Keys.F1:
                    ReprintTransaction();
                    break;

                case Keys.F2:
                    PrintTerminalReport();
                    break;

                case Keys.F3:
                    PrintCashiersReport();
                    break;

                case Keys.F4:
                    PrintTerminalXRead();
                    break;

                case Keys.F5:
                    PrintHourly();
                    break;

                case Keys.F6:
                    PrintGroup();
                    break;

                case Keys.F7:
                    PrintPLU();
                    break;

                case Keys.F8:
                    PrintEJournal();
                    break;

                case Keys.F9:
                    ReprintZRead(false);
                    break;

                case Keys.F10:
                    PrintPLUPerOrderSlipPrinter();
                    break;

                case Keys.F11:
                    ReprintDeliveryReceipt();
                    break;

                case Keys.F12:
                    PrintPLUGroup();
                    break;
			}
		}
		private void ShowMallForwarder()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.MallForwarder);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("Mall Forwarder showing window to [" + lblCashier.Text + "]...", true);

					ForwarderWnd clsForwarderWnd = new ForwarderWnd();
					clsForwarderWnd.TerminalDetails = mclsTerminalDetails;
					clsForwarderWnd.ShowDialog(this);
					DialogResult result = clsForwarderWnd.Result;
					clsForwarderWnd.Close();
					clsForwarderWnd.Dispose();

					if (result == DialogResult.OK)
					{
						ProcessMallForwarder(clsForwarderWnd.DateLastInitialized, true);
					}
					else { clsEvent.AddEventLn("Mall Forwarder: Cancelled!", true); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
				Cursor.Current = Cursors.Default;
			}
		}
		private delegate void ProcessMallForwarderDelegate(DateTime pvtDateToProcess, bool ShowSuccessMessageBox);
		private void ProcessMallForwarder(DateTime pvtDateToProcess, bool ShowSuccessMessageBox)
		{
			try
			{
				switch (CONFIG.MallCode.ToUpper())
				{
					case MallCodes.NA:
						break;
					case MallCodes.AYALA:
						AceSoft.RetailPlus.Forwarder.AyalaDetails clsAyalaDetails = new AceSoft.RetailPlus.Forwarder.AyalaDetails();
						clsAyalaDetails.TenantCode = AYALA_CONFIG.TenantCode;
						clsAyalaDetails.TenantName = AYALA_CONFIG.TenantName;
						clsAyalaDetails.OutputDirectory = AYALA_CONFIG.OutputDirectory;
						clsAyalaDetails.FTPIPAddress = CONFIG.FTPIPAddress;
						clsAyalaDetails.FTPUsername = CONFIG.FTPUsername;
						clsAyalaDetails.FTPPassword = CONFIG.FTPPassword;
						clsAyalaDetails.FTPDirectory = CONFIG.FTPDirectory;

						AceSoft.RetailPlus.Forwarder.Ayala clsAyala = new AceSoft.RetailPlus.Forwarder.Ayala();
						clsAyala.AyalaDetails = clsAyalaDetails;
						clsAyala.CreateAndTransferFile(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, pvtDateToProcess);
						clsAyala.CommitAndDispose();
						clsEvent.AddEventLn("Mall Forwarder: Done!", true);
						if (ShowSuccessMessageBox) MessageBox.Show("Sales file successfully sent to Ayala server.", "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case MallCodes.RLC:
						AceSoft.RetailPlus.Forwarder.RLCDetails clsRLCDetails = new AceSoft.RetailPlus.Forwarder.RLCDetails();
						clsRLCDetails.TenantCode = RLC_CONFIG.TenantCode;
						clsRLCDetails.TenantName = RLC_CONFIG.TenantName;
						clsRLCDetails.OutputDirectory = RLC_CONFIG.OutputDirectory;
						clsRLCDetails.FTPIPAddress = CONFIG.FTPIPAddress;
						clsRLCDetails.FTPUsername = CONFIG.FTPUsername;
						clsRLCDetails.FTPPassword = CONFIG.FTPPassword;
						clsRLCDetails.FTPDirectory = CONFIG.FTPDirectory;

						AceSoft.RetailPlus.Forwarder.RLC clsRLC = new AceSoft.RetailPlus.Forwarder.RLC();
						clsRLC.RLCDetails = clsRLCDetails;
                        clsRLC.CreateAndTransferFile(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, pvtDateToProcess);
						clsEvent.AddEventLn("Mall Forwarder: Done!", true);
						if (ShowSuccessMessageBox) MessageBox.Show("Sales file successfully sent to RLC server.", "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case MallCodes.FSI:
						AceSoft.RetailPlus.Forwarder.FSIDetails clsFSIDetails = new AceSoft.RetailPlus.Forwarder.FSIDetails();
						clsFSIDetails.TenantCode = FSI_CONFIG.TenantCode;
						clsFSIDetails.TenantName = FSI_CONFIG.TenantName;
						clsFSIDetails.OutputDirectory = FSI_CONFIG.OutputDirectory;
						clsFSIDetails.SalesTypeCode = FSI_CONFIG.SalesTypeCode;
						clsFSIDetails.FTPIPAddress = CONFIG.FTPIPAddress;
						clsFSIDetails.FTPUsername = CONFIG.FTPUsername;
						clsFSIDetails.FTPPassword = CONFIG.FTPPassword;
						clsFSIDetails.FTPDirectory = CONFIG.FTPDirectory;

						AceSoft.RetailPlus.Forwarder.FSI clsFSI = new AceSoft.RetailPlus.Forwarder.FSI();
						clsFSI.FSIDetails = clsFSIDetails;
						clsFSI.CreateAndTransferFile(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, pvtDateToProcess);
						clsEvent.AddEventLn("Mall Forwarder: Done!", true);
						if (ShowSuccessMessageBox) MessageBox.Show("Sales file successfully sent to FSI server.", "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					default:
						MessageBox.Show("Sorry no applicable mall code for:" + CONFIG.MallCode, "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
        private void VoidItem()
        {
            Data.SalesTransactionItemDetails Details;

            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID")
                {
                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.VoidItem);

                    if (loginresult == DialogResult.OK)
                    {
                        if (mclsTerminalDetails.ItemVoidConfirmation)
                        {
                            if (MessageBox.Show("Are you sure you want to void this item?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        Details = getCurrentRowItemDetails();
                        clsEvent.AddEvent("[" + lblCashier.Text + "] Voiding item no. " + Details.ItemNo + "".PadRight(15) + ":" + Details.Description + ".");
                        try
                        {
                            // override the transaction item status
                            TransactionItemStatus _previousTransactionItemStatus = Details.TransactionItemStatus;

                            Details.TransactionItemStatus = TransactionItemStatus.Void;

                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.VoidItem(Details.TransactionItemsID, mclsSalesTransactionDetails.TransactionDate);
                            clsEvent.AddEventLn("Voiding item #: " + Details.ItemNo + "".PadRight(15) + ":" + Details.Description + ".", true);

                            // Added May 7, 2011 to Cater Reserved and Commit functionality    
                            ReservedAndCommitItem(Details, _previousTransactionItemStatus);
                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.VoidItem, "Voiding item #: " + Details.ItemNo + "".PadRight(15) + ":" + Details.Description + "." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                            dgItems[iRow, 8] = "VOID";
                            dgItems[iRow, 9] = "0.00";
                            dgItems[iRow, 10] = "0.00";
                            dgItems[iRow, 11] = "0.00";
                            dgItems[iRow, 13] = "0.00";

                            //SetItemDetails();

                            clsEvent.AddEventLn("Done!");

                            ComputeSubTotal(); setTotalDetails();

                            try
                            {
                                DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                                DisplayItemToTurretDel.BeginInvoke("VOID-" + Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                            }
                            catch { }
                            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                            {
                                PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
                                PrintItemDel.BeginInvoke(Details.ItemNo, Details.ProductCode + " - VOID ", Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, Details.DiscountCode, Details.ItemDiscountType, null, null);
                                //PrintItemDel.BeginInvoke(Details.ProductCode + " - VOID ", Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                            }

                        }
                        catch (Exception ex)
                        {
                            InsertErrorLogToFile(ex, "ERROR!!! Voiding item." + Details.ItemNo + "".PadRight(15) + ":" + Details.Description);
                        }
                    }
                }
            }
        }
        private bool CashCount()
        {
            bool boRetValue = false;

            if (!SuspendTransactionAndContinue()) return boRetValue;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CashCount);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEventLn("[" + lblCashier.Text + "] Issuing cash count.", true);

                    CashCountWnd frmCashCountWnd = new CashCountWnd();
                    frmCashCountWnd.TerminalDetails = mclsTerminalDetails;
                    frmCashCountWnd.SalesTransactionDetails = mclsSalesTransactionDetails;
                    frmCashCountWnd.ShowDialog(this);
                    DialogResult result = frmCashCountWnd.Result;
                    Data.CashCountDetails[] clsCashCountDetails = frmCashCountWnd.Details;
                    decimal Amount = frmCashCountWnd.Amount;
                    frmCashCountWnd.Close();
                    frmCashCountWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        if (clsCashCountDetails != null)
                        {
                            // Sep 3, 2014 Move it here from the CashCountWnd
                            Data.CashCounts clsCashCount = new Data.CashCounts();
                            mConnection = clsCashCount.Connection; mTransaction = clsCashCount.Transaction;

                            clsCashCount.Insert(clsCashCountDetails);

                            Data.Terminal clsTerminal = new Data.Terminal(mConnection, mTransaction);
                            clsTerminal.UpdateIsCashCountInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, true);

                            clsCashCount.CommitAndDispose();

                            // Dec 01, 2008      Lemuel E. Aceron
                            // added the IsCashCountInitialized for 1 time 
                            // Cash count every printing of report.
                            mboIsCashCountInitialized = true;

                            //PrintCashCountDelegate printcashcountDel = new PrintCashCountDelegate(PrintCashCount);
                            //printcashcountDel.BeginInvoke(clsCashCountDetails, null, null);
                            PrintCashCount(clsCashCountDetails);

                            // Sep 28, 2011 : Lemu 
                            // As per request of houseware plaze. Print a second copy
                            // printcashcountDel = new PrintCashCountDelegate(PrintCashCount);
                            // printcashcountDel.BeginInvoke(clsCashCountDetails, null, null);
                            System.Threading.Thread.Sleep(100);
                            PrintCashCount(clsCashCountDetails);
                        }
                        InsertAuditLog(AccessTypes.CashCount, "Issue cash count. amount=" + Amount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        clsEvent.AddEventLn("Done! amount=" + Amount.ToString("#,###.#0"), true);

                        boRetValue = true;

                        MessageBox.Show("Cash Count has been initialized...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else { clsEvent.AddEventLn("Cancelled!", true); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing cash count");
                }
            }
            return boRetValue;
        }
        private void Float()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterFloat);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEventLn("[" + lblCashier.Text + "] Entering beginning balance...", true);

                    BalanceWnd clsBalanceWnd = new BalanceWnd();
                    clsBalanceWnd.TerminalDetails = mclsTerminalDetails;
                    clsBalanceWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                    clsBalanceWnd.ShowDialog(this);
                    DialogResult balanceResult = clsBalanceWnd.Result;
                    decimal Amount = clsBalanceWnd.Amount;
                    clsBalanceWnd.Close();
                    clsBalanceWnd.Dispose();

                    if (balanceResult == DialogResult.OK)
                    {
                        //OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                        //Invoke(opendrawerDel);
                        OpenDrawer();
                        InsertAuditLog(AccessTypes.EnterFloat, "Issue beginning balance. amount=" + Amount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        clsEvent.AddEventLn("Entering beginning balance Done! amount=" + Amount.ToString("#,###.#0"), true);
                    }
                    else { clsEvent.AddEventLn("Entering beginning balance cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing beginning balance.");
                }
            }
        }
        private void InitializeZRead(bool boWithOutTF)
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.InitializeZRead);

            if (loginresult == DialogResult.OK)
            {
                Data.SalesTransactions clsSales = new Data.SalesTransactions(mConnection, mTransaction);
                int count = clsSales.CountSuspended(mclsTerminalDetails.TerminalNo, 0, mclsTerminalDetails.BranchID);
                clsSales.CommitAndDispose();

                if (count != 0)
                {
                    MessageBox.Show("Sorry there are suspended transactions for this day. Please CLOSE the transactions first...", "RetailPlus", MessageBoxButtons.OK);
                    return;
                }
                if (IsDateLastInitializationOK() == false)
                {
                    return;
                }
                if (MessageBox.Show("Warning!!! Z-Read will be initialized...Press OK to continue.", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    try
                    {
                        clsEvent.AddEvent("[" + lblCashier.Text + "] Initializing ZReading.", true);
                        PrintZRead(true);

                        Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                        mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                        // Dec 01, 2008      Lemuel E. Aceron
                        // added the IsCashCountInitialized for
                        // 1 time Cash count every printing of report.
                        Data.Terminal clsTerminal = new Data.Terminal(mConnection, mTransaction);
                        mConnection = clsTerminal.Connection; mTransaction = clsTerminal.Transaction;

                        clsTerminal.UpdateIsCashCountInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, false);

                        //initialize Z-Read
                        clsTerminalReport.InitializeZRead(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName, Constants.C_DATE_MIN_VALUE, boWithOutTF);

                        InsertAuditLog(AccessTypes.InitializeZRead, "Initialize Z-Read." + " [Branch]:" + mclsTerminalDetails.BranchDetails.BranchCode + " [TerminalNo]" + mclsTerminalDetails.TerminalNo);

                        DateTime dteMAXDateLastInitialized = DateTime.MinValue;

                        // May 21, 2009      Lemuel E. Aceron
                        // added the for auto FTP of file for RLC
                        // get the maxdatelastinitialized
                        if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
                        {
                            Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory(mConnection, mTransaction);
                            mConnection = clsTerminalReportHistory.Connection; mTransaction = clsTerminalReportHistory.Transaction;

                            dteMAXDateLastInitialized = clsTerminalReportHistory.MINDateLastInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, DateTime.Now);
                        }

                        clsTerminalReport.CommitAndDispose();

                        clsEvent.AddEventLn("Done!");

                        MessageBox.Show("Z-Read has been initialized for [Branch]:" + mclsTerminalDetails.BranchDetails.BranchCode + " [TerminalNo]" + mclsTerminalDetails.TerminalNo + "...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // May 21, 2009      Lemuel E. Aceron
                        // added the for auto FTP of file for RLC
                        // send the data to RLC
                        if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
                            ProcessMallForwarder(dteMAXDateLastInitialized, true);

                        // 23Mar2015 : Initialize all reading with the same ORSeriesBranchID and ORSeriesTerminalNo
                        InitializeAllZreadWithSameORSeries(boWithOutTF);

                        LoggedOutCashier(false);
                    }
                    catch (Exception ex)
                    {
                        InsertErrorLogToFile(ex, "ERRROR!!! Initializing ZREAD for [Branch]:" + mclsTerminalDetails.BranchDetails.BranchCode + " [TerminalNo]" + mclsTerminalDetails.TerminalNo);
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public void InitializeAllZreadWithSameORSeries(bool boWithOutTF)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Data.Terminal clsTerminal = new Data.Terminal(mConnection, mTransaction);
                mConnection = clsTerminal.Connection; mTransaction = clsTerminal.Transaction;

                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                AccessUser clsUser = new AccessUser(mConnection, mTransaction);
                mConnection = clsUser.Connection; mTransaction = clsUser.Transaction;

                System.Data.DataTable dt = clsTerminal.ListORSeries(mclsTerminalDetails.ORSeriesBranchID, mclsTerminalDetails.ORSeriesTerminalNo);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Int32 iBranchID = Int32.Parse(dr["BranchID"].ToString());
                    string stTerminalNo = dr["TerminalNo"].ToString();

                    mclsTerminalDetails = clsTerminal.Details(iBranchID, stTerminalNo);
                    mclsSalesTransactionDetails.CashierID = clsTerminal.getLastLoggedCashierID(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);

                    AccessUserDetails details = clsUser.Details(mclsSalesTransactionDetails.CashierID);

                    mclsSalesTransactionDetails.CashierName = details.Name;

                    if (IsDateLastInitializationOK())
                    {
                        try
                        {
                            clsEvent.AddEvent("[" + lblCashier.Text + "] Initializing ZReading.", true);
                            PrintZRead(false);

                            // Dec 01, 2008      Lemuel E. Aceron
                            // added the IsCashCountInitialized for
                            // 1 time Cash count every printing of report.
                            clsTerminal.UpdateIsCashCountInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, false);

                            //initialize Z-Read
                            clsTerminalReport.InitializeZRead(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName, Constants.C_DATE_MIN_VALUE, boWithOutTF);

                            InsertAuditLog(AccessTypes.InitializeZRead, "Initialize Z-Read." + " [Branch]:" + mclsTerminalDetails.BranchDetails.BranchCode + " [TerminalNo]" + mclsTerminalDetails.TerminalNo);

                            DateTime dteMAXDateLastInitialized = DateTime.MinValue;

                            // May 21, 2009      Lemuel E. Aceron
                            // added the for auto FTP of file for RLC
                            // get the maxdatelastinitialized
                            if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
                            {
                                Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory(mConnection, mTransaction);
                                mConnection = clsTerminalReportHistory.Connection; mTransaction = clsTerminalReportHistory.Transaction;

                                dteMAXDateLastInitialized = clsTerminalReportHistory.MINDateLastInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, DateTime.Now);
                            }

                            clsEvent.AddEventLn("Done!");

                            MessageBox.Show("Z-Read has been initialized for [Branch]:" + mclsTerminalDetails.BranchDetails.BranchCode + " [TerminalNo]" + mclsTerminalDetails.TerminalNo + "...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // May 21, 2009      Lemuel E. Aceron
                            // added the for auto FTP of file for RLC
                            // send the data to RLC
                            if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
                                ProcessMallForwarder(dteMAXDateLastInitialized, true);
                        }
                        catch (Exception ex)
                        {
                            InsertErrorLogToFile(ex, "ERRROR!!! Initializing ZREAD for [Branch]:" + mclsTerminalDetails.BranchDetails.BranchCode + " [TerminalNo]" + mclsTerminalDetails.TerminalNo);
                        }
                    }
                }

                clsTerminal.CommitAndDispose();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERRROR!!! Initializing ALL ZREAD [ORSeriesBranchID]:" + mclsTerminalDetails.ORSeriesBranchID.ToString() + " [ORSeriesTerminalNo]" + mclsTerminalDetails.ORSeriesTerminalNo);
            }

            // 23Mar2015 : Needed to do this
            mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag);
            mclsSalesTransactionDetails.CashierName = lblCashier.Text;
        }

        private void ReadBarCode()
        {
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreateTransaction);
            
            if (loginresult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(txtBarCode.Text.Trim()))
                {
                    string stBarcode = txtBarCode.Text.Trim();
                    decimal decQuantity = 1;

                    // 21Jul2013 Check if parking ticket and has already an item.
                    if (mclsTerminalDetails.IsParkingTerminal && ItemDataTable.Rows.Count >= 1)
                    {
                        txtBarCode.Text = "";
                        MessageBox.Show("Sorry you can only park 1 vehicle per transaction. ", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (stBarcode.EndsWith("*"))
                    {
                        MessageBox.Show("Sorry please scan the item after the quantity before you press the enter key.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (stBarcode.IndexOf("*") > -1)
                    {
                        try
                        {
                            decQuantity = Convert.ToDecimal(stBarcode.Substring(0, stBarcode.IndexOf("*")).Trim());
                            stBarcode = stBarcode.Substring(stBarcode.IndexOf("*") + 1, stBarcode.Length - (stBarcode.IndexOf("*") + 1));
                        }
                        catch
                        {
                            MessageBox.Show("Sorry the quantity you entered is not valid. Please enter a valid quantity to purchase.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Data.ProductPackage clsProductPackage = new Data.ProductPackage(mConnection, mTransaction);
                    mConnection = clsProductPackage.Connection; mTransaction = clsProductPackage.Transaction;

                    Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                    mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                    Data.ProductPackageDetails clsProductPackageDetails = new Data.ProductPackageDetails();
                    Data.ProductDetails clsProductDetails = new Data.ProductDetails();

                    if (ProductModel.PackageID != 0) //PackageID is not zero if selection is used.
                    {
                        clsProductPackageDetails = clsProductPackage.Details(ProductModel.PackageID);
                        clsProductDetails = clsProduct.Details(clsProductPackageDetails.ProductID, clsProductPackageDetails.MatrixID, mclsTerminalDetails.BranchID);
                    }
                    else //PackageID is zero if selection is not used.
                    {
                        // check if the product exist and with quantity
                        clsProductDetails = clsProduct.Details(mclsTerminalDetails.BranchID, stBarcode, mclsTerminalDetails.ShowItemMoreThanZeroQty, decQuantity);

                        // check if the product exist and zero quantity
                        if (clsProductDetails.ProductID == 0) clsProductDetails = clsProduct.Details(stBarcode, mclsTerminalDetails.BranchID);

                        // check if the product is weighted
                        if (clsProductDetails.ProductID == 0)
                        {
                            if (stBarcode.Length == 12) stBarcode = "0" + stBarcode;
                            if (stBarcode.Length > Data.Products.DEFAULT_WEIGHTED_BARCODE_CHARACTER_COUNT + 1)
                            {
                                clsProductDetails = clsProduct.Details(mclsTerminalDetails.BranchID, stBarcode.Remove(Data.Products.DEFAULT_WEIGHTED_BARCODE_CHARACTER_COUNT));

                                if (clsProductDetails.ProductID != 0)
                                {
                                    decQuantity = (decimal.Parse(stBarcode.Remove(stBarcode.Length - 1).Remove(1, Data.Products.DEFAULT_WEIGHTED_BARCODE_CHARACTER_COUNT)) / 100) / clsProductDetails.Price;
                                }
                            }
                        }

                        // get the package details
                        if (clsProductDetails.ProductID != 0) clsProductPackageDetails = clsProductPackage.Details(clsProductDetails.PackageID);
                    }

                    txtBarCode.Text = "";
                    ProductModel.Clear();

                    if (clsProductDetails.ProductID != 0)
                    {
                        // 06Mar2015 : Override the price base on the Price Level
                        if (mclsSysConfigDetails.EnablePriceLevel)
                        {
                            switch (mclsContactDetails.PriceLevel)
                            {
                                case PriceLevel.SRP: clsProductDetails.Price = clsProductPackageDetails.Price; break;
                                case PriceLevel.One: clsProductDetails.Price = clsProductPackageDetails.Price1 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price1; break;
                                case PriceLevel.Two: clsProductDetails.Price = clsProductPackageDetails.Price2 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price2; break;
                                case PriceLevel.Three: clsProductDetails.Price = clsProductPackageDetails.Price3 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price3; break;
                                case PriceLevel.Four: clsProductDetails.Price = clsProductPackageDetails.Price4 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price4; break;
                                case PriceLevel.Five: clsProductDetails.Price = clsProductPackageDetails.Price5 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price5; break;
                                case PriceLevel.WSPrice: clsProductDetails.Price = clsProductPackageDetails.WSPrice == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.WSPrice; break;
                                default: clsProductDetails.Price = clsProductPackageDetails.Price; break;
                            }
                        }

                        // 21Jul2013 : Include getting of rates for parking
                        if (mclsTerminalDetails.IsParkingTerminal)
                        {
                            Data.ParkingRates clsParkingRate = new Data.ParkingRates(mConnection, mTransaction);
                            mConnection = clsParkingRate.Connection; mTransaction = clsParkingRate.Transaction;

                            Data.ParkingRateDetails clsParkingRateDetails = clsParkingRate.Details(clsProductDetails.ProductID, DateTime.Now.ToString("dddd"));
                            if (clsParkingRateDetails.ParkingRateID != 0)
                            {
                                clsProductDetails.Price = clsParkingRateDetails.MinimumStayPrice;
                            }
                        }

                        if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER &&
                            !mboIsRefund && clsProductDetails.Quantity - clsProductDetails.ReservedQuantity < decQuantity &&
                            mclsTerminalDetails.ShowItemMoreThanZeroQty &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_SUPER_CARD_MEMBERSHIP_FEE_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_SUPER_CARD_RENEWAL_FEE_BARCODE &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_SUPER_CARD_REPLACEMENT_FEE_BARCODE)
                        {
                            if (clsProductDetails.Quantity >= decQuantity)
                            {
                                clsProductPackage.CommitAndDispose();
                                MessageBox.Show("Sorry the quantity you entered is already reserved. Current Stock: " + clsProductDetails.Quantity.ToString("#,##0.#0") + " Reserved Stock: " + clsProductDetails.ReservedQuantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                clsProductPackage.CommitAndDispose();
                                MessageBox.Show("Sorry the quantity you entered is greater than the current stock. " + Environment.NewLine + "Current Stock: " + clsProductDetails.Quantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        if (clsProductDetails.IsLock)
                        {
                            clsProductPackage.CommitAndDispose();
                            MessageBox.Show("Sorry this product is currently LOCKED for inventory. Please advise the inventory officer if you really want to sell this product.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        Data.SalesTransactionItemDetails clsItemDetails = new Data.SalesTransactionItemDetails();

                        clsItemDetails.ProductID = clsProductDetails.ProductID;
                        clsItemDetails.ProductCode = clsProductDetails.ProductCode;
                        clsItemDetails.BarCode = clsProductDetails.BarCode;
                        clsItemDetails.Description = clsProductDetails.ProductDesc;
                        clsItemDetails.ProductGroup = clsProductDetails.ProductGroupName;
                        clsItemDetails.ProductSubGroup = clsProductDetails.ProductSubGroupName;
                        clsItemDetails.TransactionItemStatus = TransactionItemStatus.Valid;
                        clsItemDetails.ProductUnitID = clsProductDetails.BaseUnitID;
                        clsItemDetails.ProductUnitCode = clsProductDetails.BaseUnitCode;
                        clsItemDetails.Quantity = decQuantity;
                        clsItemDetails.Price = clsProductDetails.Price;
                        clsItemDetails.Discount = 0;
                        clsItemDetails.ItemDiscount = 0;
                        clsItemDetails.ItemDiscountType = DiscountTypes.NotApplicable;
                        clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
                        clsItemDetails.VAT = clsProductDetails.VAT;
                        clsItemDetails.EVAT = clsProductDetails.EVAT;
                        clsItemDetails.LocalTax = clsProductDetails.LocalTax;
                        clsItemDetails.TransactionItemStatus = TransactionItemStatus.Valid;
                        clsItemDetails.PurchasePrice = clsProductDetails.PurchasePrice;
                        clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
                        clsItemDetails.IncludeInSubtotalDiscount = clsProductDetails.IncludeInSubtotalDiscount;
                        clsItemDetails.IsCreditChargeExcluded = clsProductDetails.IsCreditChargeExcluded;
                        clsItemDetails.OrderSlipPrinter1 = clsProductDetails.OrderSlipPrinter1;
                        clsItemDetails.OrderSlipPrinter2 = clsProductDetails.OrderSlipPrinter2;
                        clsItemDetails.OrderSlipPrinter3 = clsProductDetails.OrderSlipPrinter3;
                        clsItemDetails.OrderSlipPrinter4 = clsProductDetails.OrderSlipPrinter4;
                        clsItemDetails.OrderSlipPrinter5 = clsProductDetails.OrderSlipPrinter5;
                        clsItemDetails.OrderSlipPrinted = false;
                        clsItemDetails.PercentageCommision = clsProductDetails.PercentageCommision;
                        clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
                        clsItemDetails.RewardPoints = clsProductDetails.RewardPoints;
                        clsItemDetails.ItemRemarks = "";

                        clsItemDetails.ProductPackageID = clsProductPackageDetails.PackageID;
                        clsItemDetails.ProductUnitID = clsProductPackageDetails.UnitID;
                        clsItemDetails.ProductUnitCode = clsProductPackageDetails.UnitCode;

                        if (!mclsTerminalDetails.IsParkingTerminal)
                        {
                            // 06Mar2015 : Override the price base on the Price Level
                            if (mclsSysConfigDetails.EnablePriceLevel)
                            {
                                switch (mclsContactDetails.PriceLevel)
                                {
                                    case PriceLevel.SRP: clsItemDetails.Price = clsProductPackageDetails.Price; break;
                                    case PriceLevel.One: clsItemDetails.Price = clsProductPackageDetails.Price1 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price1; break;
                                    case PriceLevel.Two: clsItemDetails.Price = clsProductPackageDetails.Price2 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price2; break;
                                    case PriceLevel.Three: clsItemDetails.Price = clsProductPackageDetails.Price3 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price3; break;
                                    case PriceLevel.Four: clsItemDetails.Price = clsProductPackageDetails.Price4 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price4; break;
                                    case PriceLevel.Five: clsItemDetails.Price = clsProductPackageDetails.Price5 == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.Price5; break;
                                    case PriceLevel.WSPrice: clsItemDetails.Price = clsProductPackageDetails.WSPrice == 0 ? clsProductPackageDetails.Price : clsProductPackageDetails.WSPrice; break;
                                    default: clsItemDetails.Price = clsProductPackageDetails.Price; break;
                                }
                                if (mclsContactDetails.PriceLevel != PriceLevel.SRP)
                                {
                                    InsertAuditLog(AccessTypes.ChangePrice, "Barcode: " + clsItemDetails.BarCode + " ... Product Code:" + clsItemDetails.ProductCode + " ... Price Level:" + mclsContactDetails.PriceLevel.ToString("G") + "... SRP: " + clsProductPackageDetails.Price.ToString("#,###.#0") + " NewPrice: " + clsItemDetails.Price.ToString("#,###.#0"));
                                }
                            }

                            clsItemDetails.Price = clsProductPackageDetails.Price;
                            clsItemDetails.PackageQuantity = clsProductPackageDetails.Quantity;
                            clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
                            clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
                        }
                        clsItemDetails.MatrixPackageID = clsProductPackageDetails.MatrixID;
                        clsItemDetails.VariationsMatrixID = clsProductDetails.MatrixID;
                        clsItemDetails.MatrixDescription = clsProductDetails.MatrixDescription;

                        clsItemDetails = ComputeItemTotal(clsItemDetails); // set the grossales, vat, discounts, etc.(Details);

                        if (!mboIsInTransaction)
                        {
                            lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
                            if (!this.CreateTransaction()) { clsProductPackage.CommitAndDispose(); return; }
                        }

                        AddItem(clsItemDetails);
                    }
                    else
                    {
                        MessageBox.Show("Sorry the item is not yet entered in the system. Please select the generic code for this item.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    clsProductPackage.CommitAndDispose();
                }
            }
        }
		private void ShowProductPrice()
		{
            if (!string.IsNullOrEmpty(txtBarCode.Text.Trim()))
            {
                string stBarcode = txtBarCode.Text.Trim();
                decimal decQuantity = 1;

                if (stBarcode.IndexOf("*") > -1)
                {
                    try
                    {
                        decQuantity = Convert.ToDecimal(stBarcode.Substring(0, stBarcode.IndexOf("*")).Trim());
                        stBarcode = stBarcode.Substring(stBarcode.IndexOf("*") + 1, stBarcode.Length - (stBarcode.IndexOf("*") + 1));
                    }
                    catch
                    {
                        MessageBox.Show("Sorry the quantity you entered is not valid. Please enter a valid quantity to purchase.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                // do nothing
                txtBarCode.Text = "";
			}
		}
		private void SplitTransaction()
		{
            if (!mboIsInTransaction)
            {
                MessageBox.Show("No active transaction found.", "RetailPlus", MessageBoxButtons.OK);
                return;
            }
            if (mboIsInTransaction)
            {
                
                SplitPaymentSelectWnd payment = new SplitPaymentSelectWnd();
                payment.MainWndTop = cmd1.Location.Y;
                payment.MainWndLeft = cmd10.Location.X + 1;
                payment.ShowDialog(this);
                DialogResult result = payment.Result;
                SplitPaymentTypes SplitPaymentType = payment.SplitPaymentType;
                payment.Close();
                payment.Dispose();

                if (result == DialogResult.OK)
                {
                    decimal decRetValue = 0;
                    Int32 iNoOfDiners = mclsSalesTransactionDetails.PaxNo;

                    if (SplitPaymentType == SplitPaymentTypes.Equally || SplitPaymentType == SplitPaymentTypes.ByAmount)
                    {
                        if (ShowNoControl(this, out decRetValue, decimal.Parse(iNoOfDiners.ToString()), "Enter no. of diners to pay.") == DialogResult.Cancel)
                            return;
                        else
                        {
                            iNoOfDiners = Int32.Parse(decRetValue.ToString());

                            // just close the transaction if it's just 1 dinner
                            if (iNoOfDiners == 1) { CloseTransaction(); return; }
                        }
                    }

                    DialogResult paymentResult = DialogResult.Cancel;
                    Data.SplitPaymentDetails[] clsSplitPaymentDetails;

                    // get the item details
                    Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems();
                    mConnection = clsItems.Connection; mTransaction = clsItems.Transaction;
                    Data.SalesTransactionItemDetails[] TransactionItems = clsItems.Details(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate);
                    mclsSalesTransactionDetails.TransactionItems = TransactionItems;
                    clsItems.CommitAndDispose();

                    switch (SplitPaymentType)
                    {
                        case SplitPaymentTypes.Equally:

                            #region Equal payments

                            //insert payment details
                            SplitPaymentEqualWnd clsSplitPaymentEqualWnd = new SplitPaymentEqualWnd();
                            clsSplitPaymentEqualWnd.TerminalDetails = mclsTerminalDetails;
                            clsSplitPaymentEqualWnd.SysConfigDetails = mclsSysConfigDetails;
                            clsSplitPaymentEqualWnd.CustomerDetails = mclsContactDetails;
                            clsSplitPaymentEqualWnd.SalesTransactionDetails = mclsSalesTransactionDetails;
                            clsSplitPaymentEqualWnd.CreditCardSwiped = mboCreditCardSwiped;
                            clsSplitPaymentEqualWnd.IsRefund = mboIsRefund;
                            clsSplitPaymentEqualWnd.IsCreditChargeExcluded = false; //mTopItemDetails.IsCreditChargeExcluded;
                            clsSplitPaymentEqualWnd.NoOfDiners = iNoOfDiners;
                            clsSplitPaymentEqualWnd.ShowDialog(this);
                            paymentResult = clsSplitPaymentEqualWnd.Result;
                            clsSplitPaymentDetails = clsSplitPaymentEqualWnd.arrSplitPaymentDetails;
                            clsSplitPaymentEqualWnd.Close();
                            clsSplitPaymentEqualWnd.Dispose();

                            if (paymentResult == DialogResult.OK)
                            {
                                //save the old in a temp Details
                                Data.SalesTransactionDetails tmpSalesTransactionDetails = mclsSalesTransactionDetails;

                                LocalDB clsLocalDB = new LocalDB();
                                mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                                // save all the payments made
                                foreach (Data.SplitPaymentDetails det in clsSplitPaymentDetails)
                                {
                                    mclsSalesTransactionDetails = tmpSalesTransactionDetails;
                                    mclsSalesTransactionDetails.TransactionItems = TransactionItems;

                                    decimal decSplitPercentage = mclsSalesTransactionDetails.SubTotal - mclsSalesTransactionDetails.Discount + mclsSalesTransactionDetails.Charge;
                                    decSplitPercentage = det.AmountDue / decSplitPercentage;

                                    SetGridItems();
                                    SetGridItemsWidth();

                                    #region create the transaction
                                    CreateTransaction();
                                    #endregion

                                    #region punch all the items divided by the NoOfDiner

                                    foreach (Data.SalesTransactionItemDetails item in tmpSalesTransactionDetails.TransactionItems)
                                    {
                                        Data.SalesTransactionItemDetails clsDetails = item;
                                        //clsDetails.Amount = item.Amount / iNoOfDiners;
                                        //clsDetails.Price = item.Price / iNoOfDiners;
                                        //clsDetails.PurchasePrice = item.PurchasePrice / iNoOfDiners;
                                        //clsDetails.Discount = item.Discount / iNoOfDiners;
                                        //clsDetails.Quantity = item.Quantity / iNoOfDiners;
                                        //clsDetails.Amount = item.Amount * decSplitPercentage;
                                        //clsDetails.Price = item.Price * decSplitPercentage;
                                        //clsDetails.PurchasePrice = item.PurchasePrice * decSplitPercentage;
                                        //clsDetails.Discount = item.Discount * decSplitPercentage;
                                        clsDetails.Quantity = item.Quantity * decSplitPercentage;

                                        AddItem(clsDetails);
                                    }
                                    #endregion

                                    #region Apply existing discount if required

                                    if (tmpSalesTransactionDetails.Discount != 0)
                                    {
                                        if (tmpSalesTransactionDetails.TransDiscountType == DiscountTypes.NotApplicable)
                                        {
                                            lblTransDiscount.Text = "Less 0% / 0.00";
                                        }
                                        lblTransDiscount.Tag = tmpSalesTransactionDetails.TransDiscountType.ToString("d");
                                        mclsSalesTransactionDetails.TransDiscountType = tmpSalesTransactionDetails.TransDiscountType;
                                        mclsSalesTransactionDetails.TransDiscount = tmpSalesTransactionDetails.TransDiscount;
                                        mclsSalesTransactionDetails.Discount = tmpSalesTransactionDetails.Discount / iNoOfDiners;
                                        mclsSalesTransactionDetails.DiscountableAmount = tmpSalesTransactionDetails.DiscountableAmount / iNoOfDiners;
                                        mclsSalesTransactionDetails.DiscountCode = tmpSalesTransactionDetails.DiscountCode;
                                        mclsSalesTransactionDetails.DiscountRemarks = tmpSalesTransactionDetails.DiscountRemarks;

                                        ComputeSubTotal(); setTotalDetails();

                                        if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
                                        {
                                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                                            clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                                            clsSalesTransactions.CommitAndDispose();

                                            InsertAuditLog(AccessTypes.Discounts, "Apply transaction discount for " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                                        }
                                    }

                                    #endregion

                                    Data.SalesTransactionItemDetails mTopItemDetails = getCurrentRowItemDetails();

                                    #region close the transaction

                                    // override the customerinformation if it's paid with in-house creditcard
                                    if (det.clsCreditorDetails.ContactID != 0 && det.clsCreditorDetails.ContactID != mclsSalesTransactionDetails.CustomerID)
                                    {
                                        LoadContact(Data.ContactGroupCategory.CUSTOMER, det.clsCreditorDetails);
                                    }

                                    //close then print 1 by 1
                                    CloseTransaction(mTopItemDetails,
                                    det.AmountPaid, det.ChangeAmount, det.BalanceAmount, det.CashPayment, det.ChequePayment,
                                    det.CreditCardPayment, det.CreditPayment, det.CreditChargeAmount, det.DebitPayment,
                                    det.RewardConvertedPayment, det.RewardPointsPayment, det.PaymentType,
                                    det.arrCashPaymentDetails, det.arrChequePaymentDetails, det.arrCreditCardPaymentDetails,
                                    det.arrCreditPaymentDetails, det.arrDebitPaymentDetails);

                                    #endregion

                                    clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                    this.LoadOptions();
                                }

                                #region void the old transaction

                                mclsSalesTransactionDetails = tmpSalesTransactionDetails;

                                // load the transaction
                                LoadTransaction(tmpSalesTransactionDetails.TransactionNo, tmpSalesTransactionDetails.TerminalNo);

                                clsEvent.AddEventLn("[" + lblCashier.Text + "] Voiding transaction no. " + lblTransNo.Text, true);

                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = 0; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                                    if (Details.TransactionItemStatus != TransactionItemStatus.Void)
                                    {
                                        Details.TransactionItemStatus = TransactionItemStatus.Void;
                                        ReservedAndCommitItem(Details, Details.TransactionItemStatus);
                                    }
                                }

                                //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                                UpdateTerminalReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                                //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                                UpdateCashierReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                                new Data.SalesTransactions(mConnection, mTransaction).Void(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);

                                new Data.SalesTransactions(mConnection, mTransaction).UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

                                // Sep 24, 2014 : update back the LastCheckInDate to min date
                                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                                clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                                InsertAuditLog(AccessTypes.VoidTransaction, "VOID transaction #:" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                                clsEvent.AddEventLn("Done transaction no. " + lblTransNo.Text + " has been void.", true);

                                #endregion

                                clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                this.LoadOptions();

                                // commit all in the database
                                clsLocalDB.CommitAndDispose();
                            }

                            #endregion
                            break;
                        case SplitPaymentTypes.ByItem:

                            #region Item Payment

                                //insert payment details
                            SplitPaymentItemWnd clsSplitPaymentItemWnd = new SplitPaymentItemWnd();
                            clsSplitPaymentItemWnd.TerminalDetails = mclsTerminalDetails;
                            clsSplitPaymentItemWnd.SysConfigDetails = mclsSysConfigDetails;
                            clsSplitPaymentItemWnd.CustomerDetails = mclsContactDetails;
                            clsSplitPaymentItemWnd.SalesTransactionDetails = mclsSalesTransactionDetails;
                            clsSplitPaymentItemWnd.CreditCardSwiped = mboCreditCardSwiped;
                            clsSplitPaymentItemWnd.IsRefund = mboIsRefund;
                            clsSplitPaymentItemWnd.IsCreditChargeExcluded = false; //mTopItemDetails.IsCreditChargeExcluded;
                            clsSplitPaymentItemWnd.NoOfDiners = iNoOfDiners;
                            clsSplitPaymentItemWnd.SalesTransactionItemDetails = mclsSalesTransactionDetails.TransactionItems;
                            clsSplitPaymentItemWnd.ShowDialog(this);
                            paymentResult = clsSplitPaymentItemWnd.Result;
                            clsSplitPaymentDetails = clsSplitPaymentItemWnd.arrSplitPaymentDetails;
                            clsSplitPaymentItemWnd.Close();
                            clsSplitPaymentItemWnd.Dispose();

                            if (paymentResult == DialogResult.OK)
                            {
                                //save the old in a temp Details
                                Data.SalesTransactionDetails tmpSalesTransactionDetails = mclsSalesTransactionDetails;

                                LocalDB clsLocalDB = new LocalDB();
                                mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                                // save all the payments made
                                foreach (Data.SplitPaymentDetails det in clsSplitPaymentDetails)
                                {
                                    mclsSalesTransactionDetails = tmpSalesTransactionDetails;
                                    mclsSalesTransactionDetails.TransactionItems = TransactionItems;

                                    SetGridItems();
                                    SetGridItemsWidth();

                                    #region create the transaction
                                    CreateTransaction();
                                    #endregion

                                    #region punch all the items for the selected payee

                                    foreach (Data.SalesTransactionItemDetails item in tmpSalesTransactionDetails.TransactionItems)
                                    {
                                        if (item.PaxNo == det.PaxNo)
                                        {
                                            Data.SalesTransactionItemDetails clsDetails = item;
                                            //clsDetails.Amount = item.Amount * decSplitPercentage;
                                            //clsDetails.Price = item.Price * decSplitPercentage;
                                            //clsDetails.PurchasePrice = item.PurchasePrice * decSplitPercentage;
                                            //clsDetails.Discount = item.Discount * decSplitPercentage;
                                            //clsDetails.Quantity = item.Quantity * decSplitPercentage;

                                            AddItem(clsDetails);
                                        }
                                    }
                                    #endregion

                                    #region Apply existing discount if required

                                    if (tmpSalesTransactionDetails.Discount != 0)
                                    {
                                        if (tmpSalesTransactionDetails.TransDiscountType == DiscountTypes.NotApplicable)
                                        {
                                            lblTransDiscount.Text = "Less 0% / 0.00";
                                        }
                                        lblTransDiscount.Tag = tmpSalesTransactionDetails.TransDiscountType.ToString("d");
                                        mclsSalesTransactionDetails.TransDiscountType = tmpSalesTransactionDetails.TransDiscountType;
                                        mclsSalesTransactionDetails.TransDiscount = tmpSalesTransactionDetails.TransDiscount;
                                        mclsSalesTransactionDetails.Discount = tmpSalesTransactionDetails.Discount / iNoOfDiners;
                                        mclsSalesTransactionDetails.DiscountableAmount = tmpSalesTransactionDetails.DiscountableAmount / iNoOfDiners;
                                        mclsSalesTransactionDetails.DiscountCode = tmpSalesTransactionDetails.DiscountCode;
                                        mclsSalesTransactionDetails.DiscountRemarks = tmpSalesTransactionDetails.DiscountRemarks;

                                        ComputeSubTotal(); setTotalDetails();

                                        if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
                                        {
                                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                                            clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                                            clsSalesTransactions.CommitAndDispose();

                                            InsertAuditLog(AccessTypes.Discounts, "Apply transaction discount for " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                                        }
                                    }

                                    #endregion

                                    Data.SalesTransactionItemDetails mTopItemDetails = getCurrentRowItemDetails();

                                    #region close the transaction

                                    // override the customerinformation if it's paid with in-house creditcard
                                    if (det.clsCreditorDetails.ContactID != 0 && det.clsCreditorDetails.ContactID != mclsSalesTransactionDetails.CustomerID)
                                    {
                                        LoadContact(Data.ContactGroupCategory.CUSTOMER, det.clsCreditorDetails);
                                    }

                                    //close then print 1 by 1
                                    CloseTransaction(mTopItemDetails,
                                    det.AmountPaid, det.ChangeAmount, det.BalanceAmount, det.CashPayment, det.ChequePayment,
                                    det.CreditCardPayment, det.CreditPayment, det.CreditChargeAmount, det.DebitPayment,
                                    det.RewardConvertedPayment, det.RewardPointsPayment, det.PaymentType,
                                    det.arrCashPaymentDetails, det.arrChequePaymentDetails, det.arrCreditCardPaymentDetails,
                                    det.arrCreditPaymentDetails, det.arrDebitPaymentDetails);

                                    #endregion

                                    clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                    this.LoadOptions();
                                }

                                #region void the old transaction

                                mclsSalesTransactionDetails = tmpSalesTransactionDetails;

                                // load the transaction
                                LoadTransaction(tmpSalesTransactionDetails.TransactionNo, tmpSalesTransactionDetails.TerminalNo);

                                clsEvent.AddEventLn("[" + lblCashier.Text + "] Voiding transaction no. " + lblTransNo.Text, true);

                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = 0; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                                    if (Details.TransactionItemStatus != TransactionItemStatus.Void)
                                    {
                                        Details.TransactionItemStatus = TransactionItemStatus.Void;
                                        ReservedAndCommitItem(Details, Details.TransactionItemStatus);
                                    }
                                }

                                //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                                UpdateTerminalReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                                //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                                UpdateCashierReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                                new Data.SalesTransactions(mConnection, mTransaction).Void(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);

                                new Data.SalesTransactions(mConnection, mTransaction).UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

                                // Sep 24, 2014 : update back the LastCheckInDate to min date
                                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                                clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                                InsertAuditLog(AccessTypes.VoidTransaction, "VOID transaction #:" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                                clsEvent.AddEventLn("Done transaction no. " + lblTransNo.Text + " has been void.", true);

                                #endregion

                                clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                this.LoadOptions();

                                // commit all in the database
                                clsLocalDB.CommitAndDispose();
                            }

                            #endregion
                            break;
                        case SplitPaymentTypes.ByAmount:

                            #region Amount Payments

                                //insert payment details
                            SplitPaymentAmountWnd clsSplitPaymentAmountWnd = new SplitPaymentAmountWnd();
                            clsSplitPaymentAmountWnd.TerminalDetails = mclsTerminalDetails;
                            clsSplitPaymentAmountWnd.SysConfigDetails = mclsSysConfigDetails;
                            clsSplitPaymentAmountWnd.CustomerDetails = mclsContactDetails;
                            clsSplitPaymentAmountWnd.SalesTransactionDetails = mclsSalesTransactionDetails;
                            clsSplitPaymentAmountWnd.CreditCardSwiped = mboCreditCardSwiped;
                            clsSplitPaymentAmountWnd.IsRefund = mboIsRefund;
                            clsSplitPaymentAmountWnd.IsCreditChargeExcluded = false; //mTopItemDetails.IsCreditChargeExcluded;
                            clsSplitPaymentAmountWnd.NoOfDiners = iNoOfDiners;
                            clsSplitPaymentAmountWnd.ShowDialog(this);
                            paymentResult = clsSplitPaymentAmountWnd.Result;
                            clsSplitPaymentDetails = clsSplitPaymentAmountWnd.arrSplitPaymentDetails;
                            clsSplitPaymentAmountWnd.Close();
                            clsSplitPaymentAmountWnd.Dispose();

                            if (paymentResult == DialogResult.OK)
                            {
                                //save the old in a temp Details
                                Data.SalesTransactionDetails tmpSalesTransactionDetails = mclsSalesTransactionDetails;

                                LocalDB clsLocalDB = new LocalDB();
                                mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                                // save all the payments made
                                foreach (Data.SplitPaymentDetails det in clsSplitPaymentDetails)
                                {
                                    mclsSalesTransactionDetails = tmpSalesTransactionDetails;
                                    mclsSalesTransactionDetails.TransactionItems = TransactionItems;

                                    decimal decSplitPercentage = mclsSalesTransactionDetails.SubTotal - mclsSalesTransactionDetails.Discount + mclsSalesTransactionDetails.Charge;
                                    decSplitPercentage = det.AmountDue / decSplitPercentage;

                                    SetGridItems();
                                    SetGridItemsWidth();

                                    #region create the transaction
                                    CreateTransaction();
                                    #endregion

                                    #region punch all the items divided by the NoOfDiner

                                    foreach (Data.SalesTransactionItemDetails item in tmpSalesTransactionDetails.TransactionItems)
                                    {
                                        Data.SalesTransactionItemDetails clsDetails = item;
                                        //clsDetails.Amount = item.Amount * decSplitPercentage;
                                        //clsDetails.Price = item.Price * decSplitPercentage;
                                        //clsDetails.PurchasePrice = item.PurchasePrice * decSplitPercentage;
                                        //clsDetails.Discount = item.Discount * decSplitPercentage;
                                        clsDetails.Quantity = item.Quantity * decSplitPercentage;

                                        AddItem(clsDetails);
                                    }
                                    #endregion

                                    #region Apply existing discount if required

                                    if (tmpSalesTransactionDetails.Discount != 0)
                                    {
                                        if (tmpSalesTransactionDetails.TransDiscountType == DiscountTypes.NotApplicable)
                                        {
                                            lblTransDiscount.Text = "Less 0% / 0.00";
                                        }
                                        lblTransDiscount.Tag = tmpSalesTransactionDetails.TransDiscountType.ToString("d");
                                        mclsSalesTransactionDetails.TransDiscountType = tmpSalesTransactionDetails.TransDiscountType;
                                        mclsSalesTransactionDetails.TransDiscount = tmpSalesTransactionDetails.TransDiscount;
                                        mclsSalesTransactionDetails.Discount = tmpSalesTransactionDetails.Discount / iNoOfDiners;
                                        mclsSalesTransactionDetails.DiscountableAmount = tmpSalesTransactionDetails.DiscountableAmount / iNoOfDiners;
                                        mclsSalesTransactionDetails.DiscountCode = tmpSalesTransactionDetails.DiscountCode;
                                        mclsSalesTransactionDetails.DiscountRemarks = tmpSalesTransactionDetails.DiscountRemarks;

                                        ComputeSubTotal(); setTotalDetails();

                                        if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
                                        {
                                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                                            clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                                            clsSalesTransactions.CommitAndDispose();

                                            InsertAuditLog(AccessTypes.Discounts, "Apply transaction discount for " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                                        }
                                    }

                                    #endregion

                                    Data.SalesTransactionItemDetails mTopItemDetails = getCurrentRowItemDetails();

                                    #region close the transaction

                                    // override the customerinformation if it's paid with in-house creditcard
                                    if (det.clsCreditorDetails.ContactID != 0 && det.clsCreditorDetails.ContactID != mclsSalesTransactionDetails.CustomerID)
                                    {
                                        LoadContact(Data.ContactGroupCategory.CUSTOMER, det.clsCreditorDetails);
                                    }

                                    //close then print 1 by 1
                                    CloseTransaction(mTopItemDetails,
                                    det.AmountPaid, det.ChangeAmount, det.BalanceAmount, det.CashPayment, det.ChequePayment,
                                    det.CreditCardPayment, det.CreditPayment, det.CreditChargeAmount, det.DebitPayment,
                                    det.RewardConvertedPayment, det.RewardPointsPayment, det.PaymentType,
                                    det.arrCashPaymentDetails, det.arrChequePaymentDetails, det.arrCreditCardPaymentDetails,
                                    det.arrCreditPaymentDetails, det.arrDebitPaymentDetails);

                                    #endregion

                                    clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                    this.LoadOptions();
                                }

                                #region void the old transaction

                                mclsSalesTransactionDetails = tmpSalesTransactionDetails;

                                // load the transaction
                                LoadTransaction(tmpSalesTransactionDetails.TransactionNo, tmpSalesTransactionDetails.TerminalNo);

                                clsEvent.AddEventLn("[" + lblCashier.Text + "] Voiding transaction no. " + lblTransNo.Text, true);

                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = 0; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                                    if (Details.TransactionItemStatus != TransactionItemStatus.Void)
                                    {
                                        Details.TransactionItemStatus = TransactionItemStatus.Void;
                                        ReservedAndCommitItem(Details, Details.TransactionItemStatus);
                                    }
                                }

                                //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                                UpdateTerminalReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                                //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                                UpdateCashierReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                                new Data.SalesTransactions(mConnection, mTransaction).Void(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);

                                new Data.SalesTransactions(mConnection, mTransaction).UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

                                // Sep 24, 2014 : update back the LastCheckInDate to min date
                                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                                clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                                InsertAuditLog(AccessTypes.VoidTransaction, "VOID transaction #:" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                                clsEvent.AddEventLn("Done transaction no. " + lblTransNo.Text + " has been void.", true);

                                #endregion

                                clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                this.LoadOptions();

                                // commit all in the database
                                clsLocalDB.CommitAndDispose();
                            }

                            #endregion
                            break;
                        default:
                            break;
                    }

                    
                }
            }
		}
        private void CloseTransaction()
        {
            if (!mboIsInTransaction)
            {
                MessageBox.Show("No active transaction found.", "RetailPlus", MessageBoxButtons.OK);
                return;
            }
            if ((mclsSalesTransactionDetails.SubTotal - mclsSalesTransactionDetails.Discount) < 0)
            {
                MessageBox.Show("Sorry you cannot close a less than ZERO transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Feb 13, 2009
            // overwrite cashierid and cashiername that will close the transaction
            // Aug 6, 2011 : Lemu
            // Remove this and put in ResumeTransaction 
            //try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
            //catch { }
            //mclsSalesTransactionDetails.CashierName = lblCashier.Text;
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

            if (loginresult == DialogResult.OK)
            {
                // 02Nov2014 : do not let close the CreditPayment coz the selected transactions to be paid are not identified anymore.
                //           : this should not come to this if no error has encountered
                if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.CreditPayment)
                {
                    MessageBox.Show("Sorry there was an error when paying this CREDIT transaction." + Environment.NewLine + "You should VOID this transaction and re-issue the payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (mclsSalesTransactionDetails.SubTotal == 0)
                {
                    if (!mclsSysConfigDetails.AllowZeroAmountTransaction)
                    {
                        MessageBox.Show("Sorry you cannot close this ZERO amount transaction." + Environment.NewLine + "You can VOID this transaction instead.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (MessageBox.Show("Are you sure you want to close this  ZERO amount transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }

                try
                {
                    clsEvent.AddEventLn("Closing transaction no. " + lblTransNo.Text, true);

                    clsEvent.AddEventLn("      showing payment screen", true);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    mclsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
                    LoadContact(Data.ContactGroupCategory.CUSTOMER, mclsContactDetails);
                    clsContact.CommitAndDispose();

                    Data.SalesTransactionItemDetails mTopItemDetails = getCurrentRowItemDetails();

                    //insert payment details
                    PaymentsWnd payment = new PaymentsWnd();
                    payment.TerminalDetails = mclsTerminalDetails;
                    payment.SysConfigDetails = mclsSysConfigDetails;
                    payment.CustomerDetails = mclsContactDetails;
                    payment.SalesTransactionDetails = mclsSalesTransactionDetails;
                    payment.CreditCardSwiped = mboCreditCardSwiped;
                    payment.IsRefund = mboIsRefund;
                    payment.IsCreditChargeExcluded = mTopItemDetails.IsCreditChargeExcluded;

                    payment.ShowDialog(this);

                    DialogResult paymentResult = payment.Result;

                    decimal AmountPaid = payment.AmountPaid;
                    decimal CashPayment = payment.CashPayment;
                    decimal ChequePayment = payment.ChequePayment;
                    decimal CreditCardPayment = payment.CreditCardPayment;
                    decimal CreditPayment = payment.CreditPayment;
                    decimal DebitPayment = payment.DebitPayment;
                    decimal CreditChargeAmount = payment.SalesTransactionDetails.CreditChargeAmount;

                    decimal BalanceAmount = payment.BalanceAmount;
                    decimal ChangeAmount = payment.ChangeAmount;
                    PaymentTypes PaymentType = payment.PaymentType;
                    ArrayList arrCashPaymentDetails = payment.CashPaymentDetails;
                    ArrayList arrChequePaymentDetails = payment.ChequePaymentDetails;
                    ArrayList arrCreditCardPaymentDetails = payment.CreditCardPaymentDetails;
                    ArrayList arrCreditPaymentDetails = payment.CreditPaymentDetails;
                    ArrayList arrDebitPaymentDetails = payment.DebitPaymentDetails;
                    decimal RewardPointsPayment = payment.RewardPointsPayment;
                    decimal RewardConvertedPayment = payment.RewardConvertedPayment;
                    Data.ContactDetails clsCreditorDetails = payment.CreditorDetails;
                    payment.Close();
                    payment.Dispose();

                    this.KeyPreview = false;
                    clsEvent.AddEventLn("      payment screen closed.", true);

                    if (paymentResult != DialogResult.OK)
                    {
                        clsEvent.AddEventLn(" cancelled.", true);
                    }
                    else
                    {
                        // override the customerinformation if it's paid with in-house creditcard
                        if (clsCreditorDetails.ContactID !=0 && clsCreditorDetails.ContactID != mclsSalesTransactionDetails.CustomerID)
                        {
                            LoadContact(Data.ContactGroupCategory.CUSTOMER, clsCreditorDetails);
                        }

                        CloseTransaction(mTopItemDetails,
                                        AmountPaid, ChangeAmount, BalanceAmount, CashPayment, ChequePayment,
                                        CreditCardPayment, CreditPayment, CreditChargeAmount, DebitPayment,
                                        RewardConvertedPayment, RewardPointsPayment, PaymentType,
                                        arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails,
                                        arrCreditPaymentDetails, arrDebitPaymentDetails);

                        clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        this.LoadOptions();
                    }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Closing transaction.");
                }
                this.KeyPreview = true;
            }
            Cursor.Current = Cursors.Default;
        }

		private void CloseTransactionAsOrderSlip()
		{
            if (!mboIsInTransaction)
            {
                MessageBox.Show("No active transaction found.", "RetailPlus", MessageBoxButtons.OK);
                return;
            }

            // Feb 13, 2009
            // overwrite cashierid and cashiername that will close the transaction
            // Aug 6, 2011 : Lemu
            // Remove this and put in ResumeTransaction 
            //try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
            //catch { }
            //mclsSalesTransactionDetails.CashierName = lblCashier.Text;
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEventLn("[" + lblCashier.Text + "] Closing as order slip transaction no. " + lblTransNo.Text, true);


                    // start a connection for the database.
                    //update the transaction table 
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    clsSalesTransactions.CloseAsOrderSlip(mclsSalesTransactionDetails.TransactionID);

                    // Sep 24, 2014 : update back the LastCheckInDate to min date
                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                    clsSalesTransactions.CommitAndDispose();

                    InsertAuditLog(AccessTypes.CloseTransaction, "Close transaction #: " + lblTransNo.Text + "... Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + 0.ToString("#,###.#0") + " ChequePayment: " + 0.ToString("#,###.#0") + " CreditCardPayment: " + 0 + " CreditPayment: " + 0.ToString("#,###.#0") + " DebitPayment: " + 0.ToString("#,###.#0") + " ChangeAmount: " + 0.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                    clsEvent.AddEventLn("Done! Transaction no. " + lblTransNo.Text + " has been closed as order slip. Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + 0.ToString("#,###.#0") + " ChequePayment: " + 0.ToString("#,###.#0") + " CreditCardPayment: " + 0 + " CreditPayment: " + 0.ToString("#,###.#0") + " DebitPayment: " + 0.ToString("#,###.#0") + " ChangeAmount: " + 0.ToString("#,###.#0"), true);
                    this.LoadOptions();

                    MessageBox.Show("Transaction has been closed as ORDER SLIP.", "RetailPlus", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    clsEvent.AddErrorEventLn(ex);
                }
            }
            Cursor.Current = Cursors.Default;
		}

		private void SelectProduct(bool isPriceInq)
		{
            Cursor.Current = Cursors.WaitCursor;
            string strSearchCode = string.Empty;
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreateTransaction);

            if (loginresult == DialogResult.OK)
            {

            BackToSelectionProduct:
                try
                {
                    DialogResult result = DialogResult.Cancel;
                    string strSelectedBarCode = string.Empty;

                    ItemSelectWnd ItemWnd = new ItemSelectWnd();

                    ItemWnd.TerminalDetails = mclsTerminalDetails;
                    ItemWnd.IsPriceInq = isPriceInq;
                    ItemWnd.SearchCode = strSearchCode;

                    if (isPriceInq == true || mboIsRefund || lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                        ItemWnd.ShowItemMoreThanZeroQty = false;
                    else
                        ItemWnd.ShowItemMoreThanZeroQty = mclsTerminalDetails.ShowItemMoreThanZeroQty;
                    // Aug 6, 2011 : Lemu
                    // Include InAvtive Products during REFUND
                    if (mboIsRefund == true) ItemWnd.ShowInActiveProducts = true;

                    ItemWnd.ShowDialog(this);
                    result = ItemWnd.Result;
                    strSelectedBarCode = ItemWnd.SelectedBarCode;
                    strSearchCode = ItemWnd.SearchCode;
                    ItemWnd.Close();
                    ItemWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        txtBarCode.Text += strSelectedBarCode;
                        if (!isPriceInq) ReadBarCode(); else ShowProductPrice();

                        // Added June 15, 2010
                        // Reload Product Selection Window if WillContinueSelectionProduct
                        if (mclsTerminalDetails.WillContinueSelectionProduct == true)
                        {
                            goto BackToSelectionProduct;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Error in selecting product...");
                    clsEvent.AddErrorEventLn(ex);
                }
            }
            Cursor.Current = Cursors.Default;
		}
		private void ReleaseItems()
		{
			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot release an item if Auto-print is ON and an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReleaseItems);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Releasing items...");

					ItemReleaseWnd _ItemReleaseWnd = new ItemReleaseWnd();
					_ItemReleaseWnd.ReleaserID = long.Parse(lblCashier.Tag.ToString());
					_ItemReleaseWnd.ReleaserName = lblCashier.Text;
					_ItemReleaseWnd.TerminalDetails = mclsTerminalDetails;
					_ItemReleaseWnd.ShowDialog(this);
					_ItemReleaseWnd.Close();
					_ItemReleaseWnd.Dispose();

					clsEvent.AddEventLn("[" + lblCashier.Text + "] Done... Releasing items...");
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
				Cursor.Current = Cursors.Default;
			}
		}

        private DialogResult LoggedOutCashier(bool LogOutOnly)
		{
            if (!mboLocked)
            {
                if (!SuspendTransactionAndContinue()) return System.Windows.Forms.DialogResult.Cancel;

                DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.LogoutFE);

                if (loginresult == DialogResult.OK)
                {
                    if (LogOutOnly)
                    {
                        if (MessageBox.Show("Are you sure you want to Log Out?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return DialogResult.Cancel;
                        }
                    }

                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        clsEvent.AddEvent("[" + lblCashier.Text + "] Logging out...");

                        CashierLogs clsCashierLogs = new CashierLogs(mConnection, mTransaction);
                        mConnection = clsCashierLogs.Connection; mTransaction = clsCashierLogs.Transaction;

                        clsCashierLogs.Logout(Convert.ToInt64(lblCashierName.Tag));

                        InsertAuditLog(AccessTypes.LogoutFE, "User logout." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        clsEvent.AddEventLn("Done!");

                        this.Lock();

                        clsEvent.AddEventLn("System is now closed for any transaction!", true);
                        clsCashierLogs.CommitAndDispose();
                    }
                    catch (Exception ex)
                    { clsEvent.AddErrorEventLn(ex); }
                    return DialogResult.OK;
                }
                Cursor.Current = Cursors.Default;
                return DialogResult.Cancel;
            }
            return DialogResult.OK;
		}
        private void CheckInTable()
        {
            // Added Sep 24, 2014 as required by Bellevue to check how long the customer is already in
            if (!SuspendTransactionAndContinue()) return;

            try
            {
                clsEvent.AddEvent("[" + lblCashier.Text + "] Checkin table.");

                DialogResult result; Data.ContactDetails details;
                TableSelectWnd clsTableSelectWnd = new TableSelectWnd();
                clsTableSelectWnd.TerminalDetails = mclsTerminalDetails;
                clsTableSelectWnd.ContactGroupCategory = ContactGroupCategory.TABLES;
                clsTableSelectWnd.ShowAvailableTableOnly = true; //mboIsInTransaction
                clsTableSelectWnd.ShowDialog(this);
                details = clsTableSelectWnd.Details;
                result = clsTableSelectWnd.Result;
                clsTableSelectWnd.Close();
                clsTableSelectWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    DateTime dteCheckIn = DateTime.Now;

                    Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;
                    clsContacts.UpdateLastCheckInDate(details.ContactID, dteCheckIn);
                    clsContacts.CommitAndDispose();
                    clsEvent.AddEventLn("Done!");

                    MessageBox.Show(details.ContactName + " has been successfully checkin @ " + dteCheckIn.ToString("yyyy-MM-dd hh:mm"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    clsEvent.AddEventLn("Cancelled!");
                }
            }
            catch (Exception ex)
            { clsEvent.AddErrorEventLn(ex); }
        }
		private void ApplyTransDiscount()
		{
            if (ItemDataTable.Rows.Count <= 0) return;
            //if (iRow < 0) return;

            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot apply a transaction discount if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyTransDiscount);

            if (loginresult == DialogResult.OK)
            {
                //mboIsDiscountAuthorized = true;
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Applying transaction discount for trans. no. " + lblTransNo.Text);

                Back:
                    DiscountTypes TransDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), lblTransDiscount.Tag.ToString());
                    DiscountWnd discount = new DiscountWnd();
                    discount.Header = "Apply Transaction Discount";
                    discount.BalanceAmount = mclsSalesTransactionDetails.SubTotal;
                    discount.DiscountType = TransDiscountType;
                    discount.DiscountAmount = mclsSalesTransactionDetails.TransDiscount;
                    discount.DiscountCode = mclsSalesTransactionDetails.DiscountCode;
                    discount.Remarks = mclsSalesTransactionDetails.DiscountRemarks;
                    discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
                    discount.TerminalDetails = mclsTerminalDetails;
                    //discount.DisableVATExempt = false;
                    discount.ShowDialog(this);
                    DialogResult result = discount.Result;
                    decimal DiscountAmount = discount.DiscountAmount;
                    string TransDiscountCode = discount.DiscountCode;
                    string TransDiscountRemarks = discount.Remarks;
                    TransDiscountType = discount.DiscountType;
                    discount.Close();
                    discount.Dispose();

                    if (result == DialogResult.OK)
                    {
                        if (mclsSalesTransactionDetails.ItemsDiscount > 0)
                        {
                            MessageBox.Show("Sorry you cannot use the Senior Citizen, another discount is already applied to an item. Please separate the transaction for items with senior citizen discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            return;
                        }

                        int iCurrentSelectedRow = dgItems.CurrentRowIndex;

                        Cursor.Current = Cursors.WaitCursor;
                        if (!mboIsInTransaction) //false ito
                        {
                            this.LoadOptions();
                            if (!this.CreateTransaction()) return;
                        }

                        decimal OldDiscount = mclsSalesTransactionDetails.TransDiscount;
                        string OldTransDiscountType = lblTransDiscount.Tag.ToString();

                        if (TransDiscountType == DiscountTypes.NotApplicable)
                        {
                            lblTransDiscount.Text = "Less 0% / 0.00";
                        }
                        lblTransDiscount.Tag = TransDiscountType.ToString("d");
                        mclsSalesTransactionDetails.TransDiscountType = TransDiscountType;
                        mclsSalesTransactionDetails.TransDiscount = DiscountAmount;
                        mclsSalesTransactionDetails.DiscountCode = TransDiscountCode;
                        mclsSalesTransactionDetails.DiscountRemarks = TransDiscountRemarks;

                        ComputeSubTotal(); setTotalDetails();

                        if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
                        {
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.Discounts, "Apply transaction discount for " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        }
                        else
                        {
                            MessageBox.Show("Sorry the input discount will yield a less than ZERO amount. Please type another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            mclsSalesTransactionDetails.TransDiscount = OldDiscount;
                            lblTransDiscount.Tag = OldTransDiscountType;

                            ComputeSubTotal(); setTotalDetails();
                            goto Back;
                        }
                        dgItems.Select(iCurrentSelectedRow);
                        clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Discount.ToString("#,###.#0"));
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Applying transaction discount.");
                }
                Cursor.Current = Cursors.Default;
            }
		}
		private void ApplyTransCharge()
		{
            int iRow = dgItems.CurrentRowIndex;
            if (iRow < 0) return;

            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot apply a transaction Charge if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChargeType);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Applying transaction Charge for trans. no. " + lblTransNo.Text);

                BackToCharge:
                    ChargeTypes TransChargeType = mclsSalesTransactionDetails.ChargeType;
                    ChargeWnd charge = new ChargeWnd();
                    charge.TerminalDetails = mclsTerminalDetails;
                    charge.BalanceAmount = mclsSalesTransactionDetails.SubTotal;
                    charge.ChargeType = TransChargeType;
                    charge.ChargeAmount = mclsSalesTransactionDetails.ChargeAmount;
                    charge.ChargeCode = mclsSalesTransactionDetails.ChargeCode;
                    charge.Remarks = mclsSalesTransactionDetails.ChargeRemarks;
                    charge.IsChargeEditable = mclsTerminalDetails.IsChargeEditable;
                    charge.ShowDialog(this);
                    DialogResult result = charge.Result;
                    decimal ChargeAmount = charge.ChargeAmount;
                    string TransChargeCode = charge.ChargeCode;
                    string TransChargeRemarks = charge.Remarks;
                    TransChargeType = charge.ChargeType;
                    charge.Close();
                    charge.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (!mboIsInTransaction) //false ito
                        {
                            this.LoadOptions();
                            if (!this.CreateTransaction()) return;
                        }

                        decimal OldCharge = mclsSalesTransactionDetails.ChargeAmount;
                        ChargeTypes OldTransChargeType = mclsSalesTransactionDetails.ChargeType;

                        mclsSalesTransactionDetails.ChargeAmount = ChargeAmount;
                        mclsSalesTransactionDetails.ChargeCode = TransChargeCode;
                        mclsSalesTransactionDetails.ChargeRemarks = TransChargeRemarks;
                        mclsSalesTransactionDetails.ChargeType = TransChargeType;

                        ComputeSubTotal(); setTotalDetails();

                        if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
                        {
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.ChargeType, "Apply transaction Charge for " + mclsSalesTransactionDetails.Charge.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        }
                        else
                        {
                            MessageBox.Show("Sorry the input Charge will yield a less than ZERO amount. Please type another Charge.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            mclsSalesTransactionDetails.ChargeAmount = OldCharge;
                            mclsSalesTransactionDetails.ChargeType = OldTransChargeType;

                            ComputeSubTotal(); setTotalDetails();
                            goto BackToCharge;
                        }

                        clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Charge.ToString("#,###.#0"));
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Applying transaction charge.");
                }
                Cursor.Current = Cursors.Default;
            }
		}
        
        private void ApplyTransDefaultCharge()
        {
            // must be called only during creation of transaction
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!string.IsNullOrEmpty(mclsTerminalDetails.DefaultTransactionChargeCode))
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Applying default transaction Charge for trans. no. " + lblTransNo.Text + ".");

                    Data.ChargeType clsChargeType = new Data.ChargeType(mConnection, mTransaction);
                    mConnection = clsChargeType.Connection; mTransaction = clsChargeType.Transaction;

                    Data.ChargeTypeDetails clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.DefaultTransactionChargeCode);

                    setTransCharge(clsChargeTypeDetails, "Default Transaction Charge Code: " + mclsTerminalDetails.DefaultTransactionChargeCode);
                    
                    clsChargeType.CommitAndDispose();

                    InsertAuditLog(AccessTypes.ChargeType, "Apply transaction Charge for " + mclsSalesTransactionDetails.Charge.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode + ". DefaultTransactionCharge:" + mclsTerminalDetails.DefaultTransactionChargeCode);

                    clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Charge.ToString("#,###.#0"));
                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Applying default transaction charge.");
            }
            Cursor.Current = Cursors.Default;
        }
        private void ApplyTransZeroCharge()
        {
            // must be called only during creation of transaction
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsEvent.AddEvent("[" + mclsSalesTransactionDetails.CashierName + "] Applying zero transaction Charge for trans. no. " + mclsSalesTransactionDetails.TransactionNo + ".");

                Data.ChargeTypeDetails clsChargeTypeDetails = new Data.ChargeTypeDetails
                {
                    ChargeAmount = 0,
                    ChargeType = ChargeTypes.NotApplicable.ToString("G"),
                    ChargeTypeCode = "",
                    ChargeTypeID = 0,
                    InPercent = true,
                    CreatedOn = DateTime.Now,
                    LastModified = DateTime.Now
                };

                setTransCharge(clsChargeTypeDetails, "Zero Transaction Charge Code: " + mclsTerminalDetails.DefaultTransactionChargeCode);

                InsertAuditLog(AccessTypes.ChargeType, "Apply Zero transaction Charge for " + mclsSalesTransactionDetails.Charge.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + mclsSalesTransactionDetails.TransactionNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode + ". ZeroTransactionCharge");

                clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Charge.ToString("#,###.#0"));
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Applying zero transaction charge.");
            }
            Cursor.Current = Cursors.Default;
        }
        private void setTransCharge(Data.ChargeTypeDetails ChargeTypeDetails, string Remarks)
        {
            try
            {
                mclsSalesTransactionDetails.ChargeAmount = ChargeTypeDetails.ChargeAmount;
                mclsSalesTransactionDetails.ChargeCode = ChargeTypeDetails.ChargeTypeCode;
                mclsSalesTransactionDetails.ChargeRemarks = Remarks;

                if (ChargeTypeDetails.ChargeAmount == 0)
                    mclsSalesTransactionDetails.ChargeType = ChargeTypes.NotApplicable;
                else if (ChargeTypeDetails.InPercent)
                    mclsSalesTransactionDetails.ChargeType = ChargeTypes.Percentage;
                else
                    mclsSalesTransactionDetails.ChargeType = ChargeTypes.FixedValue;

                ComputeSubTotal(); setTotalDetails();

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                clsSalesTransactions.CommitAndDispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		private void ChangeOrderType()
		{
            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot change Order Type if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangeOrderType);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Changing order type of trans. no. " + lblTransNo.Text);

                    OrderTypeWnd clsOrderTypeWnd = new OrderTypeWnd();
                    clsOrderTypeWnd.TerminalDetails = mclsTerminalDetails;
                    clsOrderTypeWnd.ShowDialog(this);
                    DialogResult result = clsOrderTypeWnd.Result;
                    OrderTypes clsOrderType = clsOrderTypeWnd.orderType;
                    clsOrderTypeWnd.Close();
                    clsOrderTypeWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (!mboIsInTransaction)
                        {
                            this.LoadOptions();
                            if (!this.CreateTransaction()) return;
                        }

                        mclsSalesTransactionDetails.OrderType = clsOrderType;

                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        clsSalesTransactions.UpdateOrderType(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.OrderType);
                        InsertAuditLog(AccessTypes.ChargeType, "Change order type to " + mclsSalesTransactionDetails.OrderType.ToString("G") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        clsEvent.AddEventLn("Done!");

                        if (clsOrderType != OrderTypes.DineIn && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                        {
                            Int64 iOldContactID = mclsSalesTransactionDetails.CustomerID;

                            SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER);

                            if (iOldContactID != mclsSalesTransactionDetails.CustomerID)
                            {
                                Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                                mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;
                                clsContacts.UpdateLastCheckInDate(iOldContactID, Constants.C_DATE_MIN_VALUE);
                                clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.TransactionDate);
                                clsContacts.CommitAndDispose();
                            }
                        }
                        if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                        {
                            mclsSalesTransactionDetails.OrderType = OrderTypes.DineIn;

                            clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateOrderType(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.OrderType);
                            InsertAuditLog(AccessTypes.ChargeType, "System override order type to " + mclsSalesTransactionDetails.OrderType.ToString("G") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        
                        }

                        clsSalesTransactions.CommitAndDispose();

                        lblOrderType.Text = mclsSalesTransactionDetails.OrderType.ToString("G").ToUpper();

                        // [09/24/2014] apply the default charge if changed to dine in

                        Data.ChargeType clsChargeType = new ChargeType(mConnection, mTransaction);
                        mConnection = clsChargeType.Connection; mTransaction = clsChargeType.Transaction;

                        Data.ChargeTypeDetails clsChargeTypeDetails = new ChargeTypeDetails();
                        if (mclsSalesTransactionDetails.OrderType == OrderTypes.DineIn && !string.IsNullOrEmpty(mclsTerminalDetails.DineInChargeCode))
                        {
                            clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.DineInChargeCode);
                            setTransCharge(clsChargeTypeDetails, "Change Order Type to Dine-In. Charge Code:" + mclsTerminalDetails.DineInChargeCode);
                        }
                        else if (mclsSalesTransactionDetails.OrderType == OrderTypes.TakeOut && !string.IsNullOrEmpty(mclsTerminalDetails.TakeOutChargeCode))
                        {
                            clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.TakeOutChargeCode);
                            setTransCharge(clsChargeTypeDetails, "Change Order Type to Take-Out. Charge Code:" + mclsTerminalDetails.TakeOutChargeCode);
                        }
                        else if (mclsSalesTransactionDetails.OrderType == OrderTypes.Delivery && !string.IsNullOrEmpty(mclsTerminalDetails.DeliveryChargeCode))
                        {
                            clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.DeliveryChargeCode);
                            setTransCharge(clsChargeTypeDetails, "Change Order Type to Delivery. Charge Code:" + mclsTerminalDetails.DeliveryChargeCode);
                        }
                        else
                        {
                            ApplyTransZeroCharge();
                        }

                        clsChargeType.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Changing order type.");
                }
                Cursor.Current = Cursors.Default;
            }
		}
        private void ChangeZeroRated(bool isZeroRated)
        {
            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot change Order Type if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Changing ZeroRated. " + lblTransNo.Text + " as " + isZeroRated.ToString());

                    mclsSalesTransactionDetails.isZeroRated = isZeroRated;

                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    clsSalesTransactions.UpdateisZeroRated(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.isZeroRated);

                    ComputeSubTotal();
                    clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);

                    InsertAuditLog(AccessTypes.ChargeType, "Change zerorated type to " + mclsSalesTransactionDetails.isZeroRated.ToString() + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                    clsEvent.AddEventLn("Done!");
                    clsSalesTransactions.CommitAndDispose();
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Changing zero rated type.");
                }
                Cursor.Current = Cursors.Default;
            }
        }
		private void OpenTransactionDrawer()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.OpenDrawer);

			if (loginresult == DialogResult.OK)
			{
                //OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                //Invoke(opendrawerDel);
                OpenDrawer();
			}
		}
        private void VerifyCredit()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterCreditPayment);

            if (loginresult == DialogResult.OK)
            {
                CreditVerificationWnd clsCreditVerificationWnd = new CreditVerificationWnd();
                clsCreditVerificationWnd.TerminalDetails = mclsTerminalDetails;
                clsCreditVerificationWnd.SysConfigDetails = mclsSysConfigDetails;
                clsCreditVerificationWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                clsCreditVerificationWnd.CashierName = mclsSalesTransactionDetails.CashierName;
                clsCreditVerificationWnd.ShowDialog(this);
                DialogResult result = clsCreditVerificationWnd.Result;
                Data.ContactDetails details = clsCreditVerificationWnd.CreditorDetails;
                Data.ContactDetails guarantordetails = clsCreditVerificationWnd.GuarantorDetails;
                Keys keyCommand = clsCreditVerificationWnd.keyCommand;
                clsCreditVerificationWnd.Close();
                clsCreditVerificationWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    if (keyCommand == Keys.F12)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        try
                        {
                            //print the verification slip
                            PrintCreditVerificationSlip(details, guarantordetails);
                        }
                        catch (Exception ex)
                        {
                            InsertErrorLogToFile(ex, "ERROR!!! Credit verification procedure. Err Description: ");
                        }
                        clsLocalDB.CommitAndDispose();
                    }
                }
            }
        }
        private void EnterCreditItemizePayment()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterCreditPayment);

            if (loginresult == DialogResult.OK)
            {
                LoadOptions();
                if (!CheckIfOKToSell(true)) return;

                ContactSelectWnd ContactWnd = new ContactSelectWnd();
                ContactWnd.HasCreditOnly = true;
                ContactWnd.ShowDialog(this);
                DialogResult result = ContactWnd.Result;
                mclsContactDetails = ContactWnd.Details;
                ContactWnd.Close();
                ContactWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    CreditsItemizeWnd creditWnd = new CreditsItemizeWnd();
                    creditWnd.TerminalDetails = mclsTerminalDetails;
                    creditWnd.SysConfigDetails = mclsSysConfigDetails;
                    creditWnd.CashierID = mclsSalesTransactionDetails.CashierID;
                    creditWnd.CustomerDetails = mclsContactDetails;
                    creditWnd.ShowDialog(this);

                    Keys keyData = creditWnd.KeyData;
                    string strTransactionNoToReprint = creditWnd.TransactionNoToReprint;
                    string strTerminalNoToReprint = creditWnd.TerminalNoToReprint;

                    decimal AmountPaid = creditWnd.AmountPayment;
                    decimal CashPayment = creditWnd.CashPayment;
                    decimal ChequePayment = creditWnd.ChequePayment;
                    decimal CreditCardPayment = creditWnd.CreditCardPayment;
                    decimal DebitPayment = creditWnd.DebitPayment;
                    decimal BalanceAmount = creditWnd.BalanceAmount;
                    decimal ChangeAmount = creditWnd.ChangeAmount;

                    DataGridViewSelectedRowCollection dgvItemsSelectedRows = creditWnd.dgvItemsSelectedRows;
                    PaymentTypes PaymentType = creditWnd.PaymentType;
                    ArrayList arrCashPaymentDetails = creditWnd.CashPaymentDetails;
                    ArrayList arrChequePaymentDetails = creditWnd.ChequePaymentDetails;
                    ArrayList arrCreditCardPaymentDetails = creditWnd.CreditCardPaymentDetails;
                    ArrayList arrDebitPaymentDetails = creditWnd.DebitPaymentDetails;
                    result = creditWnd.Result;

                    // do not dispose here coz the dgvItemsSelectedRows is also disposed
                    //creditWnd.Close();
                    //creditWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        if (keyData == Keys.F12)
                        {
                            ReprintTransaction(strTransactionNoToReprint, strTerminalNoToReprint);
                            return;
                        }

                        bool boActivateSuspendedAccount = true;

                        if (!mclsContactDetails.CreditDetails.CreditActive)
                        {
                            if (MessageBox.Show("Account is InActive, would you like to re-activate? Remarks: " + Environment.NewLine + mclsContactDetails.Remarks + Environment.NewLine + Environment.NewLine + "Press [yes] to automatically activate or [no] to disregard activation.", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                boActivateSuspendedAccount = false;
                        }

                        Cursor.Current = Cursors.WaitCursor;

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
                        mclsSalesTransactionDetails.CustomerDetails = mclsContactDetails;
                        mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.CreditPayment;

                        try
                        {
                            Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                            mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                            if (clsProducts.Details(Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE).ProductID == 0)
                            {
                                clsProducts.CREATE_CREDITPAYMENT_PRODUCT();
                                Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.EnterCreditPayment, "CREDIT PAYMENT product has been created coz it's not configured");
                            }

                            /************** April 21, 2006: added transaction no. ***************/
                            lblCustomer.Tag = mclsContactDetails.ContactID;
                            lblCustomer.Text = mclsContactDetails.ContactName;

                            clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE + " transaction for customer: ");
                            LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, mclsContactDetails);

                            if (!this.CreateTransaction()) return;

                            txtBarCode.Text = Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE;
                            ReadBarCode();
                            int iRow = dgItems.CurrentRowIndex;

                            Data.SalesTransactionItemDetails clsItemDetails = getCurrentRowItemDetails();
                            clsItemDetails.Price = AmountPaid;
                            clsItemDetails.Amount = AmountPaid;

                            clsItemDetails = ComputeItemTotal(clsItemDetails); // set the grossales, vat, discounts, etc.(Details);

                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            ApplyChangeQuantityPriceAmountDetails(iRow, clsItemDetails, "Change Quantity, Price, Amount for Credit Payment");

                            mclsSalesTransactionDetails.AmountDue = AmountPaid;
                            mclsSalesTransactionDetails.AmountPaid = AmountPaid;
                            mclsSalesTransactionDetails.ChangeAmount = ChangeAmount;
                            mclsSalesTransactionDetails.CashPayment = CashPayment;
                            mclsSalesTransactionDetails.ChequePayment = ChequePayment;

                            // for assignment of payments
                            mclsSalesTransactionDetails.PaymentDetails = AssignArrayListPayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails);

                            SavePayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails);

                            // save the details of credit payments
                            SaveCreditPayments(dgvItemsSelectedRows, arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails, boActivateSuspendedAccount);

                            //OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                            //Invoke(opendrawerDel);
                            OpenDrawer();

                            //update the transaction table 
                            Int64 iTransactionID = Convert.ToInt64(lblTransNo.Tag);
                            string strORNo = ""; // no need to put an OR no for credit payment coz it's already declared before
                            clsSalesTransactions.Close(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TerminalNo, strORNo, 0, 0, AmountPaid, AmountPaid, AmountPaid, 0, 0, 0, 0, 0, 0, 0, 0, 0, DiscountTypes.NotApplicable, 0, 0, 0, 0, 0, 0, 0, 0, 0, AmountPaid, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, 0, 0, PaymentType, null, null, 0, 0, null, null, mclsSalesTransactionDetails.CashierID, lblCashier.Text, TransactionStatus.CreditPayment);

                            //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                            UpdateTerminalReport(TransactionStatus.CreditPayment, 0, 0, AmountPaid, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, PaymentType);

                            //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                            UpdateCashierReport(TransactionStatus.CreditPayment, 0, 0, AmountPaid, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, PaymentType);

                            // Sep 24, 2014 : update back the LastCheckInDate to min date
                            Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                            mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                            clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                            //// 3Nov2014 : automatically deposit the change if credit payment
                            //#region Auto deposit for creditpayment
                            //if (mclsSysConfigDetails.WillDepositChangeOfCreditPayment)
                            //{
                            //    InsertAuditLog(AccessTypes.Deposit, "Auto depositing change of trx #: " + mclsSalesTransactionDetails.TransactionNo + ".");
                            //    clsEvent.AddEventLn("Auto depositing change of trx #: " + mclsSalesTransactionDetails.TransactionNo + ".");

                            //    Data.Deposits clsDeposit = new Data.Deposits(mConnection, mTransaction);
                            //    mConnection = clsDeposit.Connection; mTransaction = clsDeposit.Transaction;

                            //    Data.DepositDetails clsDepositDetails = new Data.DepositDetails()
                            //    {
                            //        BranchDetails = mclsTerminalDetails.BranchDetails,
                            //        TerminalNo = mclsTerminalDetails.TerminalNo,
                            //        Amount = mclsSalesTransactionDetails.ChangeAmount,
                            //        PaymentType = mclsSalesTransactionDetails.PaymentType,
                            //        DateCreated = DateTime.Now,
                            //        CashierID = mclsSalesTransactionDetails.CashierID,
                            //        CashierName = mclsSalesTransactionDetails.CashierName,
                            //        ContactID = mclsSalesTransactionDetails.CustomerDetails.ContactID,
                            //        ContactName = mclsSalesTransactionDetails.CustomerDetails.ContactName,
                            //        Remarks = "Auto deposit from trx #: " + mclsSalesTransactionDetails.TransactionNo + ".",
                            //        CreatedOn = DateTime.Now,
                            //        LastModified = DateTime.Now
                            //    };
                            //    clsDeposit.Insert(clsDepositDetails);

                            //    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                            //    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                            //    clsContact.AddDebit(clsDepositDetails.ContactID, clsDepositDetails.Amount);
                            //    clsDeposit.CommitAndDispose();

                            //    InsertAuditLog(AccessTypes.Deposit, "Deposit: type='" + clsDepositDetails.PaymentType.ToString("G") + "' amount='" + clsDepositDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                            //    clsEvent.AddEventLn("Done! type=" + clsDepositDetails.PaymentType.ToString("G") + " amount=" + clsDepositDetails.Amount.ToString("#,###.#0"));
                            //}
                            //#endregion

                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.CreditPayment, "Pay credit for " + mclsContactDetails.ContactName + "." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                            if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
                                if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                            // print credit payment as normal transaction if not HP
                            if (mclsSysConfigDetails.CreditPaymentType == CreditPaymentType.Houseware)
                            {
                                PrintCreditPayment();
                            }
                            else
                            {
                                // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                                //if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
                                //{
                                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                                {
                                    if (dr["Quantity"].ToString() != "VOID")
                                    {
                                        string stItemNo = "" + dr["ItemNo"].ToString();
                                        string stProductCode = "" + dr["ProductCode"].ToString();
                                        if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
                                        string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                                        decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);
                                        decimal decPrice = Convert.ToDecimal(dr["Price"]);
                                        decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                                        decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                                        decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                                        decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                                        decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);
                                        string stDiscountCode = "" + dr["DiscountCode"].ToString();
                                        DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString());

                                        PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT, stDiscountCode, ItemDiscountType );
                                    }
                                }
                                //}
                                // Sep 14, 2014 Control printing in mclsFilePrinter.Write

                                PrintReportFooterSection(true, TransactionStatus.CreditPayment, 0, 0, AmountPaid, 0, 0, AmountPaid, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails);
                            }

                            this.LoadOptions();
                        }
                        catch (Exception ex)
                        {
                            InsertErrorLogToFile(ex, "ERROR!!! Credit payment procedure. Err Description: ");
                        }
                        clsLocalDB.CommitAndDispose();
                        Cursor.Current = Cursors.Default;
                    }

                    creditWnd.Close();
                    creditWnd.Dispose();
                }
            }
        }
        private void RefundTransaction()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RefundTransaction);

            if (loginresult == DialogResult.OK)
            {
                if (MessageBox.Show("Press OK to issue REFUND transaction or CANCEL to disregard.", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;

                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    clsEvent.AddEventLn("Issuing REFUND Transaction!");
                    LoadOptions();

                    mboIsRefund = true;
                    if (!this.CreateTransaction()) return;
                    
                    // Aug 6, 2011 : Lemu
                    // Include items with zero quantity for REFUND
                    mclsTerminalDetails.ShowItemMoreThanZeroQty = false;

                    lblSubtotalName.Text = "SUBTOTAL: REFUND";
                    lblOrderType.Visible = false;
                    InsertAuditLog(AccessTypes.RefundTransaction, "Initialize refund transaction defaults. Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                    clsEvent.AddEventLn("Done! Tran. #" + lblTransNo.Text);
                }
                catch (Exception ex)
                { clsEvent.AddErrorEventLn(ex); }

                Cursor.Current = Cursors.Default;
            }
        }

        private void IssueRewardCard()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot issue a Reward Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardIssuance);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing reward card no to " + clsContactDetails.ContactName);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    clsContact.CommitAndDispose();

                    if (clsContactDetails.RewardDetails.RewardCardNo != string.Empty && clsContactDetails.RewardDetails.RewardCardNo != null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.RewardDetails.RewardAwardDate.ToString("MMM dd, yyyy hh:mm tt"));
                        MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.RewardDetails.RewardAwardDate.ToString("MMM dd, yyyy hh:mm tt") + "." +
                                        Environment.NewLine + " Please select another customer to issue Reward Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing reward card no to " + clsContactDetails.ContactName);

                    ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
                    clsContactRewardWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactRewardWnd.Caption = "Reward Card Issuance";
                    clsContactRewardWnd.ContactDetails = clsContactDetails;
                    clsContactRewardWnd.RewardCardStatus = RewardCardStatus.New;
                    clsContactRewardWnd.ShowDialog(this);
                    result = clsContactRewardWnd.Result;
                    clsContactDetails = clsContactRewardWnd.ContactDetails;
                    clsContactRewardWnd.Close();
                    clsContactRewardWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardIssuance, "ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was issued to " + clsContactDetails.ContactName + "." +
                                        Environment.NewLine + "Please collect the payment then close the transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was issued to " + clsContactDetails.ContactName + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE + "transaction for customer: ");
                        LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                        if (!this.CreateTransaction()) return;

                        txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE;
                        ReadBarCode();
                        int iRow = dgItems.CurrentRowIndex;

                        txtBarCode.Text = "";
                        CloseTransaction();

                        clsLocalDB.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing reward card.");
                }
                Cursor.Current = Cursors.Default;
            }

        }
        private void RenewRewardCard()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot renew a Reward Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing reward card.");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    clsContact.CommitAndDispose();

                    if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing reward card #: " + clsContactDetails.RewardDetails.RewardCardNo + " of " + clsContactDetails.ContactName + ".");

                    ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
                    clsContactRewardWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactRewardWnd.Caption = "Reward Card Renewal";
                    clsContactRewardWnd.ContactDetails = clsContactDetails;
                    clsContactRewardWnd.RewardCardStatus = RewardCardStatus.ReNew;
                    clsContactRewardWnd.ShowDialog(this);
                    result = clsContactRewardWnd.Result;
                    clsContactDetails = clsContactRewardWnd.ContactDetails;
                    clsContactRewardWnd.Close();
                    clsContactRewardWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, "ADVANTAGE_CARD_RENEWAL_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been renewed with new expiry date " + clsContactDetails.RewardDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been renewed with new expiry date " + clsContactDetails.RewardDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE + "transaction for customer: ");
                        LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                        if (!this.CreateTransaction()) return;

                        txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE;
                        ReadBarCode();
                        int iRow = dgItems.CurrentRowIndex;

                        txtBarCode.Text = "";
                        CloseTransaction();

                        clsLocalDB.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Renewing reward card.");
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void RewardCardReplacement(RewardCardStatus pvtRewardCardStatus)
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot replace a Reward Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing reward card...");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    clsContact.CommitAndDispose();

                    if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing reward card #: " + clsContactDetails.RewardDetails.RewardCardNo + " of " + clsContactDetails.ContactName + " as " + pvtRewardCardStatus.ToString("G"));

                    string strOldRewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
                    ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
                    clsContactRewardWnd.TerminalDetails = mclsTerminalDetails;
                    if (pvtRewardCardStatus == RewardCardStatus.Replaced_Lost)
                        clsContactRewardWnd.Caption = "Reward Card Replacement of LOST CARD ";
                    else if (pvtRewardCardStatus == RewardCardStatus.Replaced_Expired)
                        clsContactRewardWnd.Caption = "Reward Card Replacement of EXPIRED CARD ";
                    clsContactRewardWnd.ContactDetails = clsContactDetails;
                    clsContactRewardWnd.RewardCardStatus = pvtRewardCardStatus;
                    clsContactRewardWnd.ShowDialog(this);
                    result = clsContactRewardWnd.Result;
                    clsContactDetails = clsContactRewardWnd.ContactDetails;
                    clsContactRewardWnd.Close();
                    clsContactRewardWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, "ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Reward Card No: " + strOldRewardCardNo + " has been replaced with new card #: " + clsContactDetails.RewardDetails.RewardCardNo + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Reward Card No: " + strOldRewardCardNo + " has been replaced with new card #: " + clsContactDetails.RewardDetails.RewardCardNo + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE + "transaction for customer: ");
                        LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                        if (!this.CreateTransaction()) return;

                        txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE;
                        ReadBarCode();
                        int iRow = dgItems.CurrentRowIndex;

                        txtBarCode.Text = "";
                        CloseTransaction();

                        clsLocalDB.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Replacing reward card.");
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void RewardCardReactivate()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot reactivate a Reward Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange, "LOST Reward Card Reactivation");

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Reactivating lost reward card...");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    clsContact.CommitAndDispose();

                    if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Reactivating reward card #: " + clsContactDetails.RewardDetails.RewardCardNo + " of " + clsContactDetails.ContactName + ".");

                    string strOldRewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
                    ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
                    clsContactRewardWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactRewardWnd.Caption = "OVERRIDE: Reward Card Reactivation / Change Expiry";
                    clsContactRewardWnd.ContactDetails = clsContactDetails;
                    clsContactRewardWnd.RewardCardStatus = RewardCardStatus.Reactivated_Lost;
                    clsContactRewardWnd.ShowDialog(this);
                    result = clsContactRewardWnd.Result;
                    clsContactDetails = clsContactRewardWnd.ContactDetails;
                    clsContactRewardWnd.Close();
                    clsContactRewardWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been reactivated / changed expiry date / changed card no...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been reactivated / changed expiry date / changed card no...", true);
                        this.LoadOptions();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Activating reward card.");
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void RewardCardDeclareAsLost()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot declare a Reward Card as LOST while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange, "Reward Card Declaration as LOST");

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Declaring reward card as LOST.");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    clsContact.CommitAndDispose();

                    if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
                    clsContactRewardWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactRewardWnd.Caption = "Reward Card Declaration as LOST";
                    clsContactRewardWnd.ContactDetails = clsContactDetails;
                    clsContactRewardWnd.RewardCardStatus = RewardCardStatus.Lost;
                    clsContactRewardWnd.ShowDialog(this);
                    result = clsContactRewardWnd.Result;
                    clsContactDetails = clsContactRewardWnd.ContactDetails;
                    clsContactRewardWnd.Close();
                    clsContactRewardWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been declared as LOST.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been declared as LOST.", true);
                        this.LoadOptions();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERRROR!!! Declaring reward card as lost.");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void IssueCreditCard()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot issue a Credit Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardIssuance);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result;
                    Data.CardTypeDetails clsCardTypeDetails = new Data.CardTypeDetails();
                    Data.ContactDetails clsGuarantorDetails = new AceSoft.RetailPlus.Data.ContactDetails();
                    ContactCreditTypeSelectWnd clsContactCreditTypeSelectWnd = new ContactCreditTypeSelectWnd();
                    clsContactCreditTypeSelectWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactCreditTypeSelectWnd.ShowDialog(this);
                    clsCardTypeDetails = clsContactCreditTypeSelectWnd.CardTypeDetails;
                    result = clsContactCreditTypeSelectWnd.Result;
                    clsContactCreditTypeSelectWnd.Close();
                    clsContactCreditTypeSelectWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;

                    if (clsCardTypeDetails.WithGuarantor)
                    {
                        MessageBox.Show("Please select a GUARANTOR to issue Credit Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                        clsContactWnd.Header = "Select GUARANTOR to issue Credit Card.";
                        clsContactWnd.ShowDialog(this);
                        clsGuarantorDetails = clsContactWnd.Details;
                        result = clsContactWnd.Result;
                        clsContactWnd.Close();
                        clsContactWnd.Dispose();

                        if (result != DialogResult.OK)
                        { return; }

                        MessageBox.Show(clsGuarantorDetails.ContactName + " has been selected as guarantor." + Environment.NewLine + "Please select the CUSTOMER to issue Credit Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Select CUSTOMER to issue Credit Card.";
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    if (!clsCardTypeDetails.WithGuarantor) clsGuarantorDetails = clsContactDetails;

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing credit card no to " + clsContactDetails.ContactName);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    clsContact.CommitAndDispose();

                    if (clsContactDetails.CreditDetails.CreditCardNo != string.Empty && clsContactDetails.CreditDetails.CreditCardNo != null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.CreditDetails.CreditAwardDate.ToString("MMM dd, yyyy hh:mm tt"));
                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.CreditDetails.CreditAwardDate.ToString("MMM dd, yyyy hh:mm tt") + "." +
                                        Environment.NewLine + " Please select another customer to issue Credit Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing credit card no to " + clsContactDetails.ContactName);

                    ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
                    clsContactCreditWnd.Header = "Credit Card Issuance";
                    clsContactCreditWnd.CardTypeDetails = clsCardTypeDetails;
                    clsContactCreditWnd.Guarantor = clsGuarantorDetails;
                    clsContactCreditWnd.ContactDetails = clsContactDetails;
                    clsContactCreditWnd.CreditCardStatus = CreditCardStatus.New;
                    clsContactCreditWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactCreditWnd.ShowDialog(this);
                    result = clsContactCreditWnd.Result;
                    clsContactDetails = clsContactCreditWnd.ContactDetails;
                    clsContactCreditWnd.Close();
                    clsContactCreditWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, "CREDIT_CARD_MEMBERSHIP_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was issued to " + clsContactDetails.ContactName + "." +
                                        Environment.NewLine + "Please collect the payment then close the transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was issued to " + clsContactDetails.ContactName + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE + "transaction for customer: ");
                        LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                        if (!this.CreateTransaction()) return;

                        txtBarCode.Text = Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE;
                        ReadBarCode();
                        int iRow = dgItems.CurrentRowIndex;

                        txtBarCode.Text = "";
                        CloseTransaction();

                        clsLocalDB.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing internal credit-card.");
                }
                Cursor.Current = Cursors.Default;
            }

        }
        private void RenewCreditCard()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot renew a Credit Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card renewal.";
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing credit card.");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);

                    clsContact.CommitAndDispose();

                    if (string.IsNullOrEmpty(clsContactDetails.CreditDetails.CreditCardNo))
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing credit card #: " + clsContactDetails.CreditDetails.CreditCardNo + " of " + clsContactDetails.ContactName + ".");

                    ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
                    clsContactCreditWnd.Header = "Credit Card Renewal";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
                    clsContactCreditWnd.Guarantor = clsGuarantor;
                    clsContactCreditWnd.ContactDetails = clsContactDetails;
                    clsContactCreditWnd.CreditCardStatus = CreditCardStatus.ReNew;
                    clsContactCreditWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactCreditWnd.ShowDialog(this);
                    result = clsContactCreditWnd.Result;
                    clsContactDetails = clsContactCreditWnd.ContactDetails;
                    clsContactCreditWnd.Close();
                    clsContactCreditWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_CREDIT_CARD_RENEWAL_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, "CREDIT_CARD_RENEWAL_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been renewed with new expiry date " + clsContactDetails.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been renewed with new expiry date " + clsContactDetails.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE + "transaction for customer: ");
                        LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                        if (!this.CreateTransaction()) return;

                        txtBarCode.Text = Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE;
                        ReadBarCode();
                        int iRow = dgItems.CurrentRowIndex;

                        txtBarCode.Text = "";
                        CloseTransaction();

                        clsLocalDB.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Renewing internal credit card.");
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void CreditCardReplacement(CreditCardStatus pvtCreditCardStatus)
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot replace a Credit Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card replacement.";
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing credit card...");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);
                    clsContact.CommitAndDispose();

                    if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing credit card #: " + clsContactDetails.CreditDetails.CreditCardNo + " of " + clsContactDetails.ContactName + " as " + pvtCreditCardStatus.ToString("G"));

                    string strOldCreditCardNo = clsContactDetails.CreditDetails.CreditCardNo;
                    ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
                    if (pvtCreditCardStatus == CreditCardStatus.Replaced_Lost)
                        clsContactCreditWnd.Header = "Credit Card Replacement of LOST CARD ";
                    else if (pvtCreditCardStatus == CreditCardStatus.Replaced_Expired)
                        clsContactCreditWnd.Header = "Credit Card Replacement of EXPIRED CARD ";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
                    clsContactCreditWnd.Guarantor = clsGuarantor;
                    clsContactCreditWnd.ContactDetails = clsContactDetails;
                    clsContactCreditWnd.CreditCardStatus = pvtCreditCardStatus;
                    clsContactCreditWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactCreditWnd.ShowDialog(this);
                    result = clsContactCreditWnd.Result;
                    clsContactDetails = clsContactCreditWnd.ContactDetails;
                    clsContactCreditWnd.Close();
                    clsContactCreditWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_CREDIT_CARD_REPLACEMENT_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_CREDIT_CARD_REPLACEMENT_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, "CREDIT_CARD_REPLACEMENT_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Credit Card No: " + strOldCreditCardNo + " has been replaced with new card #: " + clsContactDetails.CreditDetails.CreditCardNo + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Credit Card No: " + strOldCreditCardNo + " has been replaced with new card #: " + clsContactDetails.CreditDetails.CreditCardNo + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_CREDIT_CARD_REPLACEMENT_FEE_BARCODE + "transaction for customer: ");
                        LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
                        if (!this.CreateTransaction()) return;

                        txtBarCode.Text = Data.Products.DEFAULT_CREDIT_CARD_REPLACEMENT_FEE_BARCODE;
                        ReadBarCode();
                        int iRow = dgItems.CurrentRowIndex;

                        txtBarCode.Text = "";
                        CloseTransaction();

                        clsLocalDB.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Replacing internal credit card.");
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void CreditCardReactivate()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot reactivate a Credit Card while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange, "Credit Card Reactivation");

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card re-activation.";
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Reactivating lost credit card...");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);

                    clsContact.CommitAndDispose();

                    if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing credit card #: " + clsContactDetails.CreditDetails.CreditCardNo + " of " + clsContactDetails.ContactName + ".");

                    string strOldCreditCardNo = clsContactDetails.CreditDetails.CreditCardNo;
                    ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
                    clsContactCreditWnd.Header = "OVERRIDE: Credit Card Reactivation / Change Credit Limit / Change Credit Card #";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
                    clsContactCreditWnd.Guarantor = clsGuarantor;
                    clsContactCreditWnd.ContactDetails = clsContactDetails;
                    clsContactCreditWnd.CreditCardStatus = CreditCardStatus.Reactivated_Lost;
                    clsContactCreditWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactCreditWnd.ShowDialog(this);
                    result = clsContactCreditWnd.Result;
                    clsContactDetails = clsContactCreditWnd.ContactDetails;
                    clsContactCreditWnd.Close();
                    clsContactCreditWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been reactivated / changed credit limit / changed card no...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been reactivated / changed credit limit / changed card no...", true);
                        this.LoadOptions();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Reactivating internal credit card.");
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void CreditCardDeclareAsSuspended()
        {
            if (mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot deaclare a Credit Card as lost while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange, "Credit Card Declaration as Lost");

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    DialogResult result; Data.ContactDetails clsContactDetails;
                    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts) == System.Windows.Forms.DialogResult.OK;
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card suspension.";
                    clsContactWnd.ShowDialog(this);
                    clsContactDetails = clsContactWnd.Details;
                    result = clsContactWnd.Result;
                    clsContactWnd.Close();
                    clsContactWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

                    if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    { return; }

                    clsEvent.AddEvent("[" + lblCashier.Text + "] Declaring credit card as suspended.");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);

                    clsContact.CommitAndDispose();

                    if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
                    {
                        clsEvent.AddEventLn("Cancelled!");
                        clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
                        MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
                    clsContactCreditWnd.Header = "Credit Card Suspension";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
                    clsContactCreditWnd.Guarantor = clsGuarantor;
                    clsContactCreditWnd.ContactDetails = clsContactDetails;
                    clsContactCreditWnd.CreditCardStatus = CreditCardStatus.Suspended;
                    clsContactCreditWnd.TerminalDetails = mclsTerminalDetails;
                    clsContactCreditWnd.ShowDialog(this);
                    result = clsContactCreditWnd.Result;
                    clsContactDetails = clsContactCreditWnd.ContactDetails;
                    clsContactCreditWnd.Close();
                    clsContactCreditWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been SUSPENDED.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        clsEvent.AddEventLn("Done!");
                        clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been SUSPENDED.", true);
                        this.LoadOptions();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Declaring internal credit card as lost.");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateBranchAndTerminalNo()
        {
            BranchWnd clsBranchWnd = new BranchWnd();
            clsBranchWnd.TerminalDetails = mclsTerminalDetails;
            clsBranchWnd.BranchDetails = mclsTerminalDetails.BranchDetails;
            clsBranchWnd.ShowDialog(this);
            DialogResult result = clsBranchWnd.Result;
            Data.BranchDetails clsBranchDetails = clsBranchWnd.BranchDetails;
            Data.TerminalDetails clsTerminalDetails = clsBranchWnd.TerminalDetails;
            clsBranchWnd.Close();
            clsBranchWnd.Dispose();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                CONFIG.SaveConfig(clsTerminalDetails);
                MessageBox.Show("New configuration has been saved. Please re-start the application, system will now exit.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try { MarqueeThread.Abort(); }
                catch { }
                Application.Exit();
                Environment.Exit(1);
            }

        }

		#endregion

		#region Private Modifiers

        private void InitializeTransaction(Int64 UID, bool WillLoadOptions = false)
        {
            try
            {
                clsEvent.AddEvent("Checking for pending transaction.");

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                string stTransactionNo = null;
                bool HasPendingTransaction = clsSalesTransactions.HasPendingTransaction(UID, mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, out stTransactionNo);

                if (HasPendingTransaction)
                {   clsEvent.AddEventLn(stTransactionNo + " found pending.");  LoadTransaction(stTransactionNo, mclsTerminalDetails.TerminalNo); }
                else
                { clsEvent.AddEventLn("None."); if (WillLoadOptions) this.LoadOptions(); }
                clsSalesTransactions.CommitAndDispose();
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR! Initializing transaction using username : " + lblCashier.Text + "TRACE: " + ex.Message);
            }
        }
        public void DoLogin()
        {
            try
            {
                LogInWnd login = new LogInWnd();
                login.AccessType = AccessTypes.LoginFE;
                login.Header = "Enter user name and password to login.";
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);

                DialogResult loginResult = login.Result;
                Int64 UserID = login.UserID;

                DialogResult loginresult = login.Result;
                login.Close();
                login.Dispose();

                if (loginresult == DialogResult.OK)
                {
                    clsEvent.AddEventLn("UID: [" + UserID.ToString() + "] successfully logged-in.", true);
                    clsEvent.AddEventLn("Checking if beggining balance already initialized.", true);

                    //	check if already initialize beginning balance.
                    if (!IsBeginningBalanceInitialized(UserID))
                    {
                        clsEvent.AddEventLn("Not yet initialized and cancelled.");
                        return;
                    }
                    clsEvent.AddEventLn("Done. Balance is already initialized.", true);

                    Cursor.Current = Cursors.WaitCursor;

                    //	ZRead and Beginning balance is already initialize 
                    //	Process Transaction.
                    lblPress.Tag = DateTime.Now.ToString("MMM/dd/yyyy hh:mm:ss tt");

                    CashierLogsDetails clsLogDetails = new CashierLogsDetails();
                    clsLogDetails.CashierID = UserID;
                    clsLogDetails.LoginDate = Convert.ToDateTime(lblPress.Tag.ToString());
                    clsLogDetails.BranchID = mclsTerminalDetails.BranchID;
                    clsLogDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
                    clsLogDetails.IPAddress = System.Net.Dns.GetHostName();
                    clsLogDetails.LogoutDate = clsLogDetails.LoginDate;
                    clsLogDetails.Status = CashierLogStatus.LoggedIn;

                    CashierLogs clsCashierLogs = new CashierLogs(mConnection, mTransaction);
                    mConnection = clsCashierLogs.Connection; mTransaction = clsCashierLogs.Transaction;

                    lblCashierName.Tag = clsCashierLogs.Insert(clsLogDetails).ToString();

                    this.UnLock(UserID);
                    this.LoadOptions(); // need to do this to reload the defaults
                    this.InitializeTransaction(UserID, false);
                    clsCashierLogs.CommitAndDispose();

                    txtBarCode.Focus();
                    InsertAuditLog(AccessTypes.LoginFE, "System login at terminal no. " + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                    clsEvent.AddEventLn("System is now ready for transaction. Current user: " + lblCashier.Text, true);
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR! System login: TRACE: " + ex.Message);
                Cursor.Current = Cursors.Default;
                throw ex;
            }
        }
        private Data.SalesTransactionItemDetails getCurrentRowItemDetails()
        {
            try
            {
                Int32 iRow = dgItems.CurrentRowIndex;

                Data.SalesTransactionItemDetails Details = new Data.SalesTransactionItemDetails();

                Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);
                Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;
                Details.TransactionItemsID = Convert.ToInt64(dgItems[iRow, 0].ToString());
                Details.ItemNo = dgItems[iRow, 1].ToString();
                Details.ProductID = Convert.ToInt32(dgItems[iRow, 2].ToString());
                Details.ProductCode = dgItems[iRow, 3].ToString();
                Details.BarCode = dgItems[iRow, 4].ToString();
                Details.Description = dgItems[iRow, 5].ToString();
                if (Details.Description.IndexOf(Environment.NewLine) > -1)
                {
                    Details.Description = Details.Description.Remove(Details.Description.IndexOf(Environment.NewLine), Details.Description.Length - Details.Description.IndexOf(Environment.NewLine));
                }
                Details.ProductUnitID = Convert.ToInt32(dgItems[iRow, 6].ToString());
                Details.ProductUnitCode = dgItems[iRow, 7].ToString();
                if (dgItems[iRow, 8].ToString().IndexOf("RETURN") != -1)
                {
                    Details.Quantity = Convert.ToDecimal(dgItems[iRow, 8].ToString().Replace(" - RETURN", "").Trim());
                }
                else if (dgItems[iRow, 8].ToString().IndexOf("VOID") != -1)
                {
                    Details.Quantity = 0;
                }
                else
                {
                    Details.Quantity = Convert.ToDecimal(dgItems[iRow, 8].ToString());
                }
                Details.Price = Convert.ToDecimal(dgItems[iRow, 9].ToString());
                Details.Discount = Convert.ToDecimal(dgItems[iRow, 10].ToString());
                Details.ItemDiscount = Convert.ToDecimal(dgItems[iRow, 11].ToString());
                Details.ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dgItems[iRow, 12].ToString());
                Details.Amount = Details.Quantity * Details.Price;//Convert.ToDecimal(dgItems[iRow, 13].ToString()) ;
                Details.VAT = Convert.ToDecimal(dgItems[iRow, 14].ToString());
                Details.EVAT = Convert.ToDecimal(dgItems[iRow, 15].ToString());
                Details.LocalTax = Convert.ToDecimal(dgItems[iRow, 16].ToString());
                Details.VariationsMatrixID = Convert.ToInt64(dgItems[iRow, 17].ToString());
                Details.MatrixDescription = dgItems[iRow, 18].ToString();
                Details.ProductGroup = dgItems[iRow, 19].ToString();
                Details.ProductSubGroup = dgItems[iRow, 20].ToString();
                Details.TransactionItemStatus = (TransactionItemStatus)Enum.Parse(typeof(TransactionItemStatus), dgItems[iRow, 21].ToString());
                Details.DiscountCode = dgItems[iRow, 22].ToString();
                Details.DiscountRemarks = dgItems[iRow, 23].ToString();
                Details.ProductPackageID = Convert.ToInt64(dgItems[iRow, 24].ToString());
                Details.MatrixPackageID = Convert.ToInt64(dgItems[iRow, 25].ToString());
                Details.PackageQuantity = Convert.ToDecimal(dgItems[iRow, 26].ToString());
                Details.PromoQuantity = Convert.ToDecimal(dgItems[iRow, 27].ToString());
                Details.PromoValue = Convert.ToDecimal(dgItems[iRow, 28].ToString());
                Details.PromoInPercent = Convert.ToBoolean(dgItems[iRow, 29].ToString());
                Details.PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dgItems[iRow, 30].ToString());
                Details.PromoApplied = Convert.ToDecimal(dgItems[iRow, 31].ToString());
                Details.PurchasePrice = Convert.ToDecimal(dgItems[iRow, 32].ToString());
                Details.PurchaseAmount = Convert.ToDecimal(dgItems[iRow, 33].ToString());
                Details.IncludeInSubtotalDiscount = Convert.ToBoolean(dgItems[iRow, 34].ToString());
                Details.IsCreditChargeExcluded = Convert.ToBoolean(dgItems[iRow, 35].ToString());
                Details.OrderSlipPrinter1 = bool.Parse(dgItems[iRow, 36].ToString());
                Details.OrderSlipPrinter2 = bool.Parse(dgItems[iRow, 37].ToString());
                Details.OrderSlipPrinter3 = bool.Parse(dgItems[iRow, 38].ToString());
                Details.OrderSlipPrinter4 = bool.Parse(dgItems[iRow, 39].ToString());
                Details.OrderSlipPrinter5 = bool.Parse(dgItems[iRow, 40].ToString());
                Details.OrderSlipPrinted = Convert.ToBoolean(dgItems[iRow, 41].ToString());
                Details.PercentageCommision = Convert.ToDecimal(dgItems[iRow, 42].ToString());
                Details.Commision = Convert.ToDecimal(dgItems[iRow, 43].ToString());
                Details.RewardPoints = Convert.ToDecimal(dgItems[iRow, 44].ToString());
                Details.ItemRemarks = dgItems[iRow, 45].ToString();
                Details.PaxNo = Convert.ToInt32(dgItems[iRow, 46].ToString());

                return Details;
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR! Getting current row item details. TRACE: " + ex.Message);
                throw ex;
            }
        }
        private System.Data.DataRow setCurrentRowItemDetails(System.Data.DataRow dr, Data.SalesTransactionItemDetails Details)
        {
            try
            {
                //if (Details.ItemDiscountType == DiscountTypes.NotApplicable)
                //{	Details.Description		= Details.Description;	}
                if (Details.ItemDiscountType == DiscountTypes.FixedValue)
                { Details.Description = Details.Description + Environment.NewLine + "@ " + Details.ItemDiscount.ToString("###,##0.#0") + " disc"; }
                else if (Details.ItemDiscountType == DiscountTypes.Percentage)
                { Details.Description = Details.Description + Environment.NewLine + "@ " + Details.ItemDiscount.ToString("###,##0.#0") + " % disc"; }

                if (Details.PromoApplied != 0)
                { Details.Description = Details.Description + Environment.NewLine + "@ " + Details.PromoApplied.ToString("###,##0.#0") + " promo"; }

                if (ItemDataTable.Rows.Count + 1 > 8)
                    dgStyle.GridColumnStyles["Amount"].Width = 80;
                else
                    dgStyle.GridColumnStyles["Amount"].Width = 90;

                dr["TransactionItemsID"] = Details.TransactionItemsID;
                dr["ItemNo"] = Details.ItemNo;
                dr["ProductID"] = Details.ProductID;
                dr["ProductCode"] = Details.ProductCode;
                dr["BarCode"] = Details.BarCode;
                dr["Description"] = Details.Description;
                dr["ProductUnitID"] = Details.ProductUnitID;
                dr["ProductUnitCode"] = Details.ProductUnitCode;
                dr["Quantity"] = Details.Quantity.ToString("###,##0.###");	//8
                dr["Price"] = Details.Price.ToString("###,##0.#0");	//9
                dr["Discount"] = Details.Discount.ToString("###,##0.#0"); //10
                dr["ItemDiscount"] = Details.ItemDiscount.ToString("###,##0.#0");//11
                dr["ItemDiscountType"] = Details.ItemDiscountType.ToString("d");//12
                dr["Amount"] = Details.Amount.ToString("###,##0.#0"); //13
                dr["VAT"] = Details.VAT.ToString("###,##0.#0");
                dr["EVAT"] = Details.EVAT.ToString("###,##0.#0");
                dr["LocalTax"] = Details.LocalTax.ToString("###,##0.#0");
                dr["VariationsMatrixID"] = Details.VariationsMatrixID;
                dr["MatrixDescription"] = Details.MatrixDescription;
                dr["ProductGroup"] = Details.ProductGroup;
                dr["ProductSubGroup"] = Details.ProductSubGroup;
                dr["TransactionItemStat"] = Details.TransactionItemStatus.ToString("d");
                dr["DiscountCode"] = Details.DiscountCode;
                dr["DiscountRemarks"] = Details.DiscountRemarks;
                dr["ProductPackageID"] = Details.ProductPackageID;
                dr["MatrixPackageID"] = Details.MatrixPackageID;
                dr["PackageQuantity"] = Details.PackageQuantity;
                dr["PromoQuantity"] = Details.PromoQuantity;
                dr["PromoValue"] = Details.PromoValue;
                dr["PromoInPercent"] = Details.PromoInPercent;
                dr["PromoType"] = Details.PromoType;
                dr["PromoApplied"] = Details.PromoApplied;
                dr["PurchasePrice"] = Details.PurchasePrice;
                dr["PurchaseAmount"] = Details.PurchaseAmount;
                dr["IncludeInSubtotalDiscount"] = Details.IncludeInSubtotalDiscount;
                dr["IsCreditChargeExcluded"] = Details.IsCreditChargeExcluded;
                dr["OrderSlipPrinter1"] = Details.OrderSlipPrinter1;
                dr["OrderSlipPrinter2"] = Details.OrderSlipPrinter2;
                dr["OrderSlipPrinter3"] = Details.OrderSlipPrinter3;
                dr["OrderSlipPrinter4"] = Details.OrderSlipPrinter4;
                dr["OrderSlipPrinter5"] = Details.OrderSlipPrinter5;
                dr["OrderSlipPrinted"] = Details.OrderSlipPrinted.ToString();
                dr["PercentageCommision"] = Details.PercentageCommision;
                dr["Commision"] = Details.Amount * (Details.PercentageCommision / 100);
                dr["RewardPoints"] = Details.RewardPoints;
                dr["ItemRemarks"] = Details.ItemRemarks;
                dr["PaxNo"] = Details.PaxNo;

                if (Details.TransactionItemStatus == TransactionItemStatus.Void)
                {
                    dr["Quantity"] = "VOID";
                    dr["Price"] = "0.00";
                    dr["Discount"] = "0.00";
                    dr["Amount"] = "0.00";
                    dr["VAT"] = "0.00";
                    dr["EVAT"] = "0.00";
                    dr["LocalTax"] = "0.00";
                    dr["PromoApplied"] = "0.00";
                    dr["PercentageCommision"] = "0.00";
                    dr["Commision"] = "0.00";
                    dr["RewardPoints"] = "0.00";
                }
                else if (Details.TransactionItemStatus == TransactionItemStatus.Return)
                {
                    dr["Quantity"] = Details.Quantity + " - RETURN";
                    if (Details.Amount < 0)
                    {
                        dr["Amount"] = Convert.ToDecimal(-Details.Amount).ToString("###,##0.#0");
                        dr["PercentageCommision"] = -Details.PercentageCommision;
                        dr["Commision"] = Convert.ToDecimal(Convert.ToDecimal(dr["Commision"]) * -1).ToString("###,##0.#0");
                    }
                }

                if (ItemDataTable.Rows.Count + 1 > 8)
                    dgStyle.GridColumnStyles["Amount"].Width = 80;
                else
                    dgStyle.GridColumnStyles["Amount"].Width = 90;

                return dr;
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR! Setting current row item details. TRACE: " + ex.Message);
                return dr;
            }
        }
        private bool IsBeginningBalanceInitialized(long CashierID)
        {
            try
            {
                bool boRetValue = false;

                Data.CashierReports clsCashierReport = new Data.CashierReports();
                bool IsBeginningBalanceInitialized = clsCashierReport.IsBeginningBalanceInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, CashierID);
                clsCashierReport.CommitAndDispose();

                if (!IsBeginningBalanceInitialized)
                {
                    BalanceWnd clsBalanceWnd = new BalanceWnd();
                    clsBalanceWnd.TerminalDetails = mclsTerminalDetails;
                    clsBalanceWnd.CashierID = CashierID;
                    clsBalanceWnd.ShowDialog(this);

                    DialogResult balanceResult = clsBalanceWnd.Result;

                    clsBalanceWnd.Close();
                    clsBalanceWnd.Dispose();

                    if (balanceResult == DialogResult.OK)
                    {
                        boRetValue = true;
                        //OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                        //Invoke(opendrawerDel);
                        OpenDrawer();
                    }
                }
                else
                {
                    boRetValue = true;
                }
                return boRetValue;
            }
            catch (Exception ex)
            {
                clsEvent.AddEventLn("ERROR! Initializing balance.");
                clsEvent.AddErrorEventLn(ex);
                throw ex;
            }
        }
        private void AddItem(Data.SalesTransactionItemDetails Details)
        {
            try
            {
                Details.ItemNo = Convert.ToString(ItemDataTable.Rows.Count + 1);
                Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;
                Details.PaxNo = 1;

                Details = ApplyPromo(Details);

                System.Data.DataRow dr = ItemDataTable.NewRow();
                dr = setCurrentRowItemDetails(dr, Details);

                Details.TransactionItemsID = AddItemToDB(Details);
                dr["TransactionItemsID"] = Details.TransactionItemsID;

                // Added May 7, 2011 to Cater Reserved and Commit functionality    
                ReservedAndCommitItem(Details, Details.TransactionItemStatus);

                try { dgItems.UnSelect(dgItems.CurrentRowIndex); }
                catch { }

                ItemDataTable.Rows.Add(dr);

                dgItems.CurrentRowIndex = ItemDataTable.Rows.Count;
                try
                {
                    dgItems.Select(dgItems.CurrentRowIndex);
                    dgItems.CurrentRowIndex = dgItems.CurrentRowIndex;
                }
                catch { }

                //SetItemDetails();
                ComputeSubTotal(); setTotalDetails();

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.GrossSales, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.SNRItemsDiscount, mclsSalesTransactionDetails.PWDItemsDiscount, mclsSalesTransactionDetails.OtherItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.ZeroRatedSales, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                clsSalesTransactions.CommitAndDispose();

                try
                {
                    DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                    DisplayItemToTurretDel.BeginInvoke(Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                }
                catch { }
                if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                {
                    PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
                    PrintItemDel.BeginInvoke(Details.ItemNo, Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, Details.DiscountCode, Details.ItemDiscountType, null, null);
                }
            }

            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Adding sales item. TRACE: ");
                throw ex;
            }
        }
        private void PackTransaction()
        {
            if (!mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot pack an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mclsSalesTransactionDetails.WaiterID == Constants.C_RETAILPLUS_WAITERID)
            {
                if (MessageBox.Show("Sorry you need to select waiter/packer before you can serve this transaction! \nSelect now?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                { if (!SelectWaiter()) return; }
                else
                    return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PackUnpackTransaction);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    clsSalesTransactions.Pack(mclsSalesTransactionDetails.TransactionID);
                    clsSalesTransactions.CommitAndDispose();

                    MessageBox.Show("Packing Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Event clsEvent = new Event();
                    clsEvent.AddEventLn("ERROR! Packing sales transaction to database. TRACE: " + ex.Message);
                    throw ex;
                }
            }
        }
        private void UnPackTransaction()
        {
            if (!mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot unpack an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mclsSalesTransactionDetails.WaiterID == Constants.C_RETAILPLUS_WAITERID)
            {
                if (MessageBox.Show("Sorry you need to select waiter/packer before you can un-pack! Select now?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                { if (!SelectWaiter()) return; }
                else
                    return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PackUnpackTransaction);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    clsSalesTransactions.UnPack(mclsSalesTransactionDetails.TransactionID);
                    clsSalesTransactions.CommitAndDispose();

                    MessageBox.Show("UnPacking Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Event clsEvent = new Event();
                    clsEvent.AddEventLn("ERROR! UnPacking sales transaction to database. TRACE: " + ex.Message);
                    throw ex;
                }
            }
        }
        private void ReprintTransaction(string TransactionNo = "", string TerminalNo = "")
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReprintTransaction);

            if (loginresult == DialogResult.OK)
            {
                DialogResult result = System.Windows.Forms.DialogResult.OK;
                string strTransactionNo = TransactionNo;
                string strTerminalNo = TerminalNo;

                if (string.IsNullOrEmpty(TransactionNo))
                {
                    TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
                    clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
                    clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
                    clsTransactionNoWnd.TerminalDetails = mclsTerminalDetails;
                    clsTransactionNoWnd.ShowDialog(this);
                    result = clsTransactionNoWnd.Result;
                    strTransactionNo = clsTransactionNoWnd.TransactionNo;
                    strTerminalNo = clsTransactionNoWnd.TerminalNo;
                    clsTransactionNoWnd.Close();
                    clsTransactionNoWnd.Dispose();
                }

                if (result == DialogResult.OK)
                {
                    LoadOptions();

                    clsEvent.AddEventLn("Reprinting transaction #: " + strTransactionNo, true);

                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;
                    string OldTerminalNo = mclsTerminalDetails.TerminalNo; //put to print the correct terminal no
                    mclsTerminalDetails.TerminalNo = strTerminalNo;

                    //open salestransaction data
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    LoadTransaction(strTransactionNo, strTerminalNo);
                    
                    AddToReprintedTransaction(strTransactionNo, strTerminalNo);

                    clsSalesTransactions.CommitAndDispose();

                    ArrayList arrChequePaymentDetails = null;
                    if (mclsSalesTransactionDetails.PaymentDetails.arrChequePaymentDetails != null)
                    {
                        arrChequePaymentDetails = new ArrayList();
                        foreach (Data.ChequePaymentDetails det in mclsSalesTransactionDetails.PaymentDetails.arrChequePaymentDetails)
                        { arrChequePaymentDetails.Add(det); }
                    }

                    ArrayList arrCreditCardPaymentDetails = null;
                    {
                        arrCreditCardPaymentDetails = new ArrayList();
                        if (mclsSalesTransactionDetails.PaymentDetails.arrCreditCardPaymentDetails != null)
                            foreach (Data.CreditCardPaymentDetails det in mclsSalesTransactionDetails.PaymentDetails.arrCreditCardPaymentDetails)
                            { arrCreditCardPaymentDetails.Add(det); }
                    }

                    ArrayList arrCreditPaymentDetails = null;
                    if (mclsSalesTransactionDetails.PaymentDetails.arrCreditPaymentDetails != null)
                    {
                        arrCreditPaymentDetails = new ArrayList();
                        foreach (Data.CreditPaymentDetails det in mclsSalesTransactionDetails.PaymentDetails.arrCreditPaymentDetails)
                        { arrCreditPaymentDetails.Add(det); }
                    }

                    ArrayList arrDebitPaymentDetails = null;
                    if (mclsSalesTransactionDetails.PaymentDetails.arrDebitPaymentDetails != null)
                    {
                        arrDebitPaymentDetails = new ArrayList();
                        foreach (Data.DebitPaymentDetails det in mclsSalesTransactionDetails.PaymentDetails.arrDebitPaymentDetails)
                        { arrDebitPaymentDetails.Add(det); }
                    }

                    //print transactionfooter
                    //items are already printed during the loading of items.
                    //if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                    //    PrintReportFooterSection(true, TransactionStatus.Reprinted, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, mclsSalesTransactionDetails.CashPayment, mclsSalesTransactionDetails.ChequePayment, mclsSalesTransactionDetails.CreditCardPayment, mclsSalesTransactionDetails.CreditPayment, mclsSalesTransactionDetails.DebitPayment, mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardConvertedPayment, mclsSalesTransactionDetails.ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);
                    if (mclsSalesTransactionDetails.isConsignment)
                    {
                        // 18Feb2015 : Print DR only if the transaction is consignment
                        clsEvent.AddEventLn("      re-printing delivery receipt as consginment...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        PrintDeliveryReceipt();
                    }
                    else if (mclsSalesTransactionDetails.CustomerDetails.ContactCode == mclsSysConfigDetails.WalkInCustomerCode &&
                        (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice ||
                         mclsTerminalDetails.ReceiptType == TerminalReceiptType.DeliveryReceipt ||
                        mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceAndDR))
                    {
                        clsEvent.AddEventLn("      re-printing walk-in customer quote form...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        PrintWalkInReceipt();
                    }
                    else if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.CreditPayment &&
                        mclsSysConfigDetails.CreditPaymentType == CreditPaymentType.Houseware)
                    {
                        // do another report for credit payment if HP
                        PrintCreditPayment();

                        // do this twice as per request of CN trader's and CS
                        PrintCreditPayment();
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
                    {
                        clsEvent.AddEventLn("      re-printing sales invoice...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        PrintSalesInvoice();
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.DeliveryReceipt)
                    {
                        clsEvent.AddEventLn("      re-printing delivery receipt...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        PrintDeliveryReceipt();
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceAndDR)
                    {
                        clsEvent.AddEventLn("      re-printing sales invoice & delivery receipt...", true, mclsSysConfigDetails.WillWriteSystemLog);

                        PrintSalesInvoice();

                        PrintDeliveryReceipt();
                    }
                    //Added February 10, 2010
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
                    {
                        clsEvent.AddEventLn("      re-printing sales invoice for LX300...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
                    }
                    //Added May 11, 2010
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
                    {
                        clsEvent.AddEventLn("      re-printing sales invoice or OR...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        if (mclsSalesTransactionDetails.CashPayment != 0 || mclsSalesTransactionDetails.CreditCardPayment != 0)
                            PrintSalesInvoice();
                        if (mclsSalesTransactionDetails.ChequePayment != 0 || mclsSalesTransactionDetails.CreditPayment != 0)
                            PrintDeliveryReceipt();
                    }
                    //Added January 17, 2011
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
                    {
                        clsEvent.AddEventLn("      re-printing sales invoice for LX300 Plus...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusPrinter);
                    }
                    //Added February 22, 2011
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
                    {
                        clsEvent.AddEventLn("      re-printing sales invoice for LX300 Plus Amazon...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon);
                    }
                    else if (!mboIsRefund) // do not print if refund coz its already printed above
                    {
                        // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                        //if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
                        //{
                        clsEvent.AddEventLn("      printing items to POS printer...", true, mclsSysConfigDetails.WillWriteSystemLog);

                        // do this here in case void items are to be printed
                        if (!mboIsItemHeaderPrinted)
                        {
                            PrintReportHeadersSection(true);
                            mboIsItemHeaderPrinted = true;
                        }
                        foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                        {
                            string stItemNo = "" + dr["ItemNo"].ToString();
                            string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                            decimal decPrice = Convert.ToDecimal(dr["Price"]);
                            decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                            decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                            decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                            decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                            decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);
                            string stProductCode = "" + dr["ProductCode"].ToString();
                            if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
                            decimal decQuantity = 0;
                            string stDiscountCode = "" + dr["DiscountCode"].ToString();
                            DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString());

                            if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
                            {
                                stProductCode = "" + dr["ProductCode"].ToString() + "-RET";
                                decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
                                decAmount = -decAmount;
                            }
                            else if (dr["Quantity"].ToString() != "VOID")
                            {
                                decQuantity = Convert.ToDecimal(dr["Quantity"]);
                            }

                            if (dr["Quantity"].ToString().IndexOf("VOID") != -1)
                            {
                                if (mclsTerminalDetails.WillPrintVoidItem)
                                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                        PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT, stDiscountCode, ItemDiscountType);
                            }
                            else
                            {
                                if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                    PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT, stDiscountCode, ItemDiscountType);
                            }

                        }
                        if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                        {
                            PrintReportFooterSection(true, TransactionStatus.Reprinted, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, mclsSalesTransactionDetails.CashPayment, mclsSalesTransactionDetails.ChequePayment, mclsSalesTransactionDetails.CreditCardPayment, mclsSalesTransactionDetails.CreditPayment, mclsSalesTransactionDetails.DebitPayment, mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardConvertedPayment, mclsSalesTransactionDetails.ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

                            // print the charge slip if not refund and will print
                            if (mclsTerminalDetails.WillPrintChargeSlip && !mboIsRefund)
                            {
                                clsEvent.AddEventLn("      printing charge slip...", true, mclsSysConfigDetails.WillWriteSystemLog);

                                // Nov 05, 2011 : Print Charge Slip
                                PrintChargeSlip(ChargeSlipType.Customer);
                                PrintChargeSlip(ChargeSlipType.Original);
                                if (mclsTerminalDetails.IncludeCreditChargeAgreement && mclsSalesTransactionDetails.CustomerDetails.CreditDetails.CardTypeDetails.WithGuarantor) //do not print the guarantor if there is no agreement printed
                                {
                                    PrintChargeSlip(ChargeSlipType.Guarantor);
                                }
                            }
                        }
                        //}
                        // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                    }
                    clsEvent.AddEventLn("Done reprinting transaction #".PadRight(15) + ":" + strTransactionNo, true);

                    mclsTerminalDetails.TerminalNo = OldTerminalNo; //put back after printing the correct terminal no
                    this.LoadOptions();
                }
            }
        }
        private void ReprintDeliveryReceipt()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReprintTransaction);

            if (loginresult == DialogResult.OK)
            {
                TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
                clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
                clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
                clsTransactionNoWnd.TerminalDetails = mclsTerminalDetails;
                clsTransactionNoWnd.ShowDialog(this);
                DialogResult result = clsTransactionNoWnd.Result;
                string strTransactionNo = "";
                string strTerminalNo = mclsTerminalDetails.TerminalNo;
                if (result == DialogResult.OK)
                {
                    strTransactionNo = clsTransactionNoWnd.TransactionNo;
                    strTerminalNo = clsTransactionNoWnd.TerminalNo;
                }

                clsTransactionNoWnd.Close();
                clsTransactionNoWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    clsEvent.AddEventLn("Reprinting transaction #: " + strTransactionNo, true);

                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                    //open salestransaction data
                    LoadTransaction(strTransactionNo, strTerminalNo);

                    ArrayList arrChequePaymentDetails = null;
                    ArrayList arrCreditCardPaymentDetails = null;
                    ArrayList arrCreditPaymentDetails = null;
                    ArrayList arrDebitPaymentDetails = null;

                    //print transactionfooter
                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                        PrintReportFooterSection(true, TransactionStatus.Reprinted, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, mclsSalesTransactionDetails.CashPayment, mclsSalesTransactionDetails.ChequePayment, mclsSalesTransactionDetails.CreditCardPayment, mclsSalesTransactionDetails.CreditPayment, mclsSalesTransactionDetails.DebitPayment, mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardConvertedPayment, mclsSalesTransactionDetails.ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

                    PrintDeliveryReceipt();

                    clsEvent.AddEventLn("Done reprinting delivery receipt transaction #:" + strTransactionNo, true);

                    this.LoadOptions();
                }
            }
        }

        private void PrintTerminalZRead()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintZRead);

            if (loginresult == DialogResult.OK)
            {
                PrintZRead(true);
            }
        }
        private void PrintTerminalXRead()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintXRead);

            if (loginresult == DialogResult.OK)
            {
                PrintXRead();
            }
        }
        private void PrintHourly()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintXRead);

            if (loginresult == DialogResult.OK)
            {
                PrintHourlyReport();
            }
        }
        private void PrintGroup()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintGroupReport);

            if (loginresult == DialogResult.OK)
            {
                PrintGroupReport();
            }
        }
        private void PrintPLU()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport);

            if (loginresult == DialogResult.OK)
            {
                PrintPLUReport();
            }
        }
        private void PrintPLUGroup()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport);

            if (loginresult == DialogResult.OK)
            {
                PrintPLUReportPerGroup();
            }
        }
        private void PrintEJournal()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintElectronicJournal);

            if (loginresult == DialogResult.OK)
            {
                PrintEJournalReport();
            }
        }
        private void PrintPLUPerOrderSlipPrinter()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport);

            if (loginresult == DialogResult.OK)
            {
                PrintPLUReportPerOrderSlipPrinter();
            }
        }
        private bool IsDateLastInitializationOK()
        {
            try
            {
                clsEvent.AddEvent("Checking last initialization date");

                Data.Database clsDatabase = new Data.Database(mConnection, mTransaction);
                mConnection = clsDatabase.Connection; mTransaction = clsDatabase.Transaction;

                DateTime dtDateLastInitialized = clsDatabase.DateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);

                bool boRetValue = false;

                if (DateTime.Now > dtDateLastInitialized)
                {
                    clsEvent.AddEventLn("OK: Last initialization is smaller than system date. DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm") + " SystemDate=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
                }
                else
                {
                    clsEvent.AddEventLn("Error: Last initialization is greater than system date. DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm") + " SystemDate=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
                    clsDatabase.CommitAndDispose();
                    MessageBox.Show("FATAL ERROR Level 2.!!! System date is behind ZREAD last initialization date. Please adjust SYSTEM DATE!!!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return boRetValue;
                }

                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

                DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime);

                // if StartCutOffTime is greater than EndCutOffTime
                // this means that EndCutOffTime is in the morning.
                // Add 1 more day.
                if (dteStartCutOffTime >= dteEndCutOffTime)
                    dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

                DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
                if (dteTransactionDate < dteEndCutOffTime)
                {
                    dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
                    dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                }
                else
                {
                    dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
                    dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
                }
                if (dteTransactionDate < dteAllowedEndDateTime)
                {
                    dteStartCutOffTime = dteStartCutOffTime.AddDays(1);
                    dteEndCutOffTime = dteEndCutOffTime.AddDays(1);
                }

                DateTime dtePreviousStartCutOffTime = dteStartCutOffTime.AddDays(-1);
                DateTime dtePreviousEndCutOffTime = dteEndCutOffTime.AddDays(-1);
                DateTime dtePreviousAllowedStartDateTime = dteAllowedStartDateTime.AddDays(-1);
                DateTime dtePreviousAllowedEndDateTime = dteAllowedEndDateTime.AddDays(-1);

                DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

                clsEvent.AddEventLn("Checking if MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is already initialized.", true);
                if (dteMAXDateLastInitialized >= dteAllowedStartDateTime && dteMAXDateLastInitialized <= dteAllowedEndDateTime)
                {
                    clsDatabase.CommitAndDispose();
                    clsEvent.AddEventLn("Transaction is not allowed, ZRead is already initialized for this date.", true);
                    MessageBox.Show("Sorry initialization is not permitted this time, ZRead is already initialized for this date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBarCode.Text = "";
                    return boRetValue;
                }
                clsEvent.AddEventLn("OK. MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is for previous zreading.", true);

                if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
                {
                    if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
                    {
                        clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
                    }
                    else if (mclsTerminalDetails.CheckCutOffTime == true)
                    {
                        clsEvent.AddEventLn("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE.", true);
                        MessageBox.Show("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE." +
                            Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                }
                if (dteMAXDateLastInitialized > dteTransactionDate)
                {
                    clsDatabase.CommitAndDispose();
                    clsEvent.AddEventLn("Transaction is not allowed, transaction date is delayed. Please restart FE.", true);
                    MessageBox.Show("Transaction is not allowed, transaction date is delayed. Please restart FE.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBarCode.Text = "";
                    return boRetValue;
                }
                clsEvent.AddEventLn("OK to initialize...", true);
                clsDatabase.CommitAndDispose();

                boRetValue = true;

                return boRetValue;
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
                return false;
            }
        }
        private bool CheckIfOKToSell(bool WillCommitAndDispose)
        {
            Boolean boRetValue = true;
            try
            {
                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

                // Added checking of Cutofftime
                if (mclsTerminalDetails.CheckCutOffTime)
                {
                    DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                    DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime);

                    // if StartCutOffTime is greater than EndCutOffTime
                    // this means that EndCutOffTime is in the morning.
                    // Add 1 more day.
                    if (dteStartCutOffTime >= dteEndCutOffTime)
                        dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

                    DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
                    if (dteTransactionDate < dteEndCutOffTime)
                    {
                        dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
                        dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                    }
                    else
                    {
                        dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
                        dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
                    }
                    if (dteTransactionDate < dteAllowedEndDateTime)
                    {
                        dteStartCutOffTime = dteStartCutOffTime.AddDays(1);
                        dteEndCutOffTime = dteEndCutOffTime.AddDays(1);
                    }

                    DateTime dtePreviousStartCutOffTime = dteStartCutOffTime.AddDays(-1);
                    DateTime dtePreviousEndCutOffTime = dteEndCutOffTime.AddDays(-1);
                    DateTime dtePreviousAllowedStartDateTime = dteAllowedStartDateTime.AddDays(-1);
                    DateTime dtePreviousAllowedEndDateTime = dteAllowedEndDateTime.AddDays(-1);

                    clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if not between cutoff: '" + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm") + "' & '" + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm") + "'", true);
                    if (dteTransactionDate >= dteStartCutOffTime && dteTransactionDate <= dteEndCutOffTime)
                    {
                        clsEvent.AddEventLn("Transaction is not allowed, transaction date is within the cutofftime.", true);
                        MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

                    clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if between selling time: '" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm") + "' & '" + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm") + "'", true);
                    if (dteTransactionDate < dteAllowedStartDateTime && dteTransactionDate > dteAllowedEndDateTime)
                    {
                        clsEvent.AddEventLn("Transaction is not allowed, transaction date is not within the allowable transaction date.", true);
                        MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

                    DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

                    clsEvent.AddEventLn("PreviousStartCutOff       :    " + dtePreviousStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PreviousEndCutOff         :    " + dtePreviousEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PrevAllowedStartDateTime  :    " + dtePreviousAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PrevAllowedEndDateTime    :    " + dtePreviousAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);

                    clsEvent.AddEventLn("StartCutOff               :    " + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("EndCutOff                 :    " + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("AllowedStartDateTime      :    " + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("AllowedEndDateTime        :    " + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("MAXDateLastInitialized    :    " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"), true);

                    clsEvent.AddEventLn("Checking if MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is already initialized.", true);
                    if (dteMAXDateLastInitialized >= dteAllowedStartDateTime && dteMAXDateLastInitialized <= dteAllowedEndDateTime)
                    {
                        clsTerminalReport.CommitAndDispose();
                        clsEvent.AddEventLn("Transaction is not allowed, ZRead is already initialized for this date.", true);
                        MessageBox.Show("Sorry selling is not permitted this time, ZRead is already initialized for this date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("OK. MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is for previous zreading.", true);
                    clsEvent.AddEventLn("Checking Transactiondate: '" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "' < '" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm") + "'", true);
                    if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
                    {
                        if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
                        {
                            clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
                        }
                        else
                        {
                            clsTerminalReport.CommitAndDispose();
                            clsEvent.AddEventLn("Transaction is not allowed, transaction date is 2-Days delayed. Please restart FE.", true);
                            MessageBox.Show("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE." +
                                Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtBarCode.Text = "";
                            return false;
                        }
                    }
                    if (dteMAXDateLastInitialized > dteTransactionDate)
                    {
                        clsTerminalReport.CommitAndDispose();
                        clsEvent.AddEventLn("Transaction is not allowed, transaction date is delayed. Please restart FE.", true);
                        MessageBox.Show("Transaction is not allowed, transaction date is delayed. Please restart FE." +
                            Environment.NewLine + "Sorry selling/payment is not permitted this time, Please consult for the Selling/Payment time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("OK to sell...", true);
                }
                else
                {
                    // this is to check if the last maxdate is too far from current date. that means they did not zread for at least 2 days which is not allowed.
                    // or the system date has been altered.
                    DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

                    if (dteTransactionDate > dteMAXDateLastInitialized.AddDays(2))
                    {
                        clsTerminalReport.CommitAndDispose();
                        if (MessageBox.Show("Transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Do you want to continue?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        {
                            clsEvent.AddEventLn("Transaction is not allowed, transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Please change the date.", true);
                            MessageBox.Show("Transaction is not allowed, transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Please change the date." +
                                Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtBarCode.Text = "";
                            return false;
                        }
                    }
                }
                if (WillCommitAndDispose) clsTerminalReport.CommitAndDispose();
                clsEvent.AddEventLn("Done! ok to sell.", true);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
                boRetValue = false;
            }

            return boRetValue;
        }
        private bool CreateTransaction()
        {
            Boolean boRetValue = true;
            try
            {
                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

                boRetValue = CheckIfOKToSell(false);

                if (!boRetValue) return boRetValue;

                // Feb 16, 2015
                if (mclsTerminalDetails.WithRestaurantFeatures)
                {
                    if (mclsContactDetails.ContactID == Constants.ZERO ||
                        mclsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
                    {
                        MessageBox.Show("Sorry you must select a table / customer to order before punching an item.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                clsEvent.AddEventLn("[" + lblCashier.Text + "] Creating new transaction.", true);

                mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
                try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
                catch { }

                //mclsSalesTransactionDetails.CustomerID = Convert.ToInt64(lblCustomer.Tag);
                //mclsSalesTransactionDetails.CustomerName = lblCustomer.Text;

                if (mboRewardCardSwiped)
                {
                    mclsSalesTransactionDetails.RewardsCustomerID = mclsContactDetails.ContactID;
                    mclsSalesTransactionDetails.RewardsCustomerName = mclsContactDetails.ContactName;
                    mclsSalesTransactionDetails.RewardCardActive = mclsContactDetails.RewardDetails.RewardActive;
                    mclsSalesTransactionDetails.RewardCardNo = mclsContactDetails.RewardDetails.RewardCardNo;
                    mclsSalesTransactionDetails.RewardCardExpiry = mclsContactDetails.RewardDetails.ExpiryDate;
                    mclsSalesTransactionDetails.RewardPreviousPoints = mclsContactDetails.RewardDetails.RewardPoints;
                }
                else
                {
                    mclsSalesTransactionDetails.RewardsCustomerID = Constants.C_RETAILPLUS_CUSTOMERID;
                    mclsSalesTransactionDetails.RewardsCustomerName = Constants.C_RETAILPLUS_CUSTOMER;
                    mclsSalesTransactionDetails.RewardCardActive = mclsContactDetailsDEFCustomer.RewardDetails.RewardActive;
                    mclsSalesTransactionDetails.RewardCardNo = mclsContactDetailsDEFCustomer.RewardDetails.RewardCardNo;
                    mclsSalesTransactionDetails.RewardCardExpiry = mclsContactDetailsDEFCustomer.RewardDetails.ExpiryDate;
                    mclsSalesTransactionDetails.RewardPreviousPoints = mclsContactDetailsDEFCustomer.RewardDetails.RewardPoints;
                }
                mclsSalesTransactionDetails.CustomerDetails = mclsContactDetails;
                mclsSalesTransactionDetails.CustomerID = mclsContactDetails.ContactID;
                mclsSalesTransactionDetails.CustomerName = mclsContactDetails.ContactName;
                if (mclsSalesTransactionDetails.CustomerDetails.LastCheckInDate == Constants.C_DATE_MIN_VALUE) mclsSalesTransactionDetails.CustomerDetails.LastCheckInDate = dteTransactionDate;

                mclsSalesTransactionDetails.AgentID = Convert.ToInt64(lblAgent.Tag);
                mclsSalesTransactionDetails.AgentName = lblAgent.Text;
                mclsSalesTransactionDetails.AgentPositionName = lblAgentPositionDepartment.Text;
                mclsSalesTransactionDetails.AgentDepartmentName = lblAgentPositionDepartment.Tag.ToString();
                mclsSalesTransactionDetails.WaiterID = Convert.ToInt64(lblServedBy.Tag);
                mclsSalesTransactionDetails.WaiterName = lblServedBy.Text.Remove(0, 11);
                mclsSalesTransactionDetails.CreatedByID = Convert.ToInt64(lblCashier.Tag);
                mclsSalesTransactionDetails.CreatedByName = lblCashier.Text;
                mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag);
                mclsSalesTransactionDetails.CashierName = lblCashier.Text;
                mclsSalesTransactionDetails.TransactionDate = dteTransactionDate;
                mclsSalesTransactionDetails.DateSuspended = DateTime.MinValue;
                mclsSalesTransactionDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
                mclsSalesTransactionDetails.BranchID = mclsTerminalDetails.BranchID;
                mclsSalesTransactionDetails.BranchCode = mclsTerminalDetails.BranchDetails.BranchCode;
                mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Open;
                mclsSalesTransactionDetails.TransactionType = mboIsRefund ? TransactionTypes.POSRefund : TransactionTypes.POSNormal;

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                mclsSalesTransactionDetails.TransactionNo = clsSalesTransactions.CreateTransactionNo(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                // mclsTransactionStream.Create(mclsSalesTransactionDetails);

                lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;

                //insert to transaction table 
                mclsSalesTransactionDetails.TransactionID = clsSalesTransactions.Insert(mclsSalesTransactionDetails);

                lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();

                // Sep 24, 2014 : update back the LastCheckInDate to transaction date
                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, dteTransactionDate);

                // Jan 31, 2015 : Lemu
                // put back to SuspendedOpen so that it won't be open somewhere else
                clsEvent.AddEventLn("Putting transaction SuspendedOpen: " + mclsSalesTransactionDetails.TransactionNo);
                clsSalesTransactions.UpdateTransactionToSuspendedOpen(mclsSalesTransactionDetails.TransactionID);

                mboIsInTransaction = true;
                clsTerminalReport.CommitAndDispose();

                InsertAuditLog(AccessTypes.CreateTransaction, "Create transaction #:" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                clsEvent.AddEventLn("Done! Trans #: " + lblTransNo.Text + " has been created.", true);

                // Added to put the default transaction charge during creation of transaction
                // this is set during the system setup
                ApplyTransDefaultCharge();
            }
            catch (Exception ex)
            { clsEvent.AddErrorEventLn(ex); boRetValue = false; }

            return boRetValue;
        }
        //private bool CreateTransaction()
        //{
        //    Boolean boRetValue = true;
        //    try
        //    {
        //        Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
        //        mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

        //        DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

        //        // Added checking of Cutofftime
        //        if (mclsTerminalDetails.CheckCutOffTime)
        //        {
        //            DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
        //            DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime);

        //            // if StartCutOffTime is greater than EndCutOffTime
        //            // this means that EndCutOffTime is in the morning.
        //            // Add 1 more day.
        //            if (dteStartCutOffTime >= dteEndCutOffTime)
        //                dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

        //            DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
        //            if (dteTransactionDate < dteEndCutOffTime)
        //            {
        //                dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
        //                dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
        //            }
        //            else
        //            {
        //                dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
        //                dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
        //            }
        //            if (dteTransactionDate < dteAllowedEndDateTime)
        //            {
        //                dteStartCutOffTime = dteStartCutOffTime.AddDays(1);
        //                dteEndCutOffTime = dteEndCutOffTime.AddDays(1);
        //            }

        //            DateTime dtePreviousStartCutOffTime = dteStartCutOffTime.AddDays(-1);
        //            DateTime dtePreviousEndCutOffTime = dteEndCutOffTime.AddDays(-1);
        //            DateTime dtePreviousAllowedStartDateTime = dteAllowedStartDateTime.AddDays(-1);
        //            DateTime dtePreviousAllowedEndDateTime = dteAllowedEndDateTime.AddDays(-1);

        //            clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if not between cutoff: " + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm") + " & " + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm"), true);
        //            if (dteTransactionDate >= dteStartCutOffTime && dteTransactionDate <= dteEndCutOffTime)
        //            {
        //                clsEvent.AddEventLn("Transaction is not allowed, transaction date is within the cutofftime.", true);
        //                MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                txtBarCode.Text = "";
        //                return false;
        //            }
        //            clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

        //            clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if between selling time: " + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm") + " & " + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm"), true);
        //            if (dteTransactionDate < dteAllowedStartDateTime && dteTransactionDate > dteAllowedEndDateTime)
        //            {
        //                clsEvent.AddEventLn("Transaction is not allowed, transaction date is not within the allowable transaction date.", true);
        //                MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                txtBarCode.Text = "";
        //                return false;
        //            }
        //            clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

        //            DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

        //            clsEvent.AddEventLn("PreviousStartCutOff".PadRight(15) + ":" + dtePreviousStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
        //            clsEvent.AddEventLn("PreviousEndCutOff".PadRight(15) + ":" + dtePreviousEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
        //            clsEvent.AddEventLn("PrevAllowedStartDateTime".PadRight(15) + ":" + dtePreviousAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
        //            clsEvent.AddEventLn("PrevAllowedEndDateTime".PadRight(15) + ":" + dtePreviousAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);

        //            clsEvent.AddEventLn("StartCutOff".PadRight(15) + ":" + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
        //            clsEvent.AddEventLn("EndCutOff".PadRight(15) + ":" + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
        //            clsEvent.AddEventLn("AllowedStartDateTime".PadRight(15) + ":" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
        //            clsEvent.AddEventLn("AllowedEndDateTime".PadRight(15) + ":" + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
        //            clsEvent.AddEventLn("MAXDateLastInitialized".PadRight(15) + ":" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"), true);

        //            clsEvent.AddEventLn("Checking if MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is already initialized.", true);
        //            if (dteMAXDateLastInitialized >= dteAllowedStartDateTime && dteMAXDateLastInitialized <= dteAllowedEndDateTime)
        //            {
        //                clsTerminalReport.CommitAndDispose();
        //                clsEvent.AddEventLn("Transaction is not allowed, ZRead is already initialized for this date.", true);
        //                MessageBox.Show("Sorry selling is not permitted this time, ZRead is already initialized for this date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                txtBarCode.Text = "";
        //                return false;
        //            }
        //            clsEvent.AddEventLn("OK. MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is for previous zreading.", true);
        //            clsEvent.AddEventLn("Checking Transactiondate: '" + dteMAXDateLastInitialized.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "' < " + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm"), true);
        //            if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
        //            {
        //                if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
        //                {
        //                    clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
        //                }
        //                else
        //                {
        //                    clsEvent.AddEventLn("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE.", true);
        //                    MessageBox.Show("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE." +
        //                        Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    txtBarCode.Text = "";
        //                    return false;
        //                }
        //            }
        //            if (dteMAXDateLastInitialized > dteTransactionDate)
        //            {
        //                clsEvent.AddEventLn("Transaction is not allowed, transaction date is delayed. Please restart FE.", true);
        //                MessageBox.Show("Transaction is not allowed, transaction date is delayed. Please restart FE." +
        //                    Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                txtBarCode.Text = "";
        //                return false;
        //            }
        //            clsEvent.AddEventLn("OK to sell...", true);
        //        }
        //        else
        //        {
        //            // this is to check if the last maxdate is too far from current date. that means they did not zread for at least 2 days which is not allowed.
        //            // or the system date has been altered.
        //            DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

        //            if (dteTransactionDate > dteMAXDateLastInitialized.AddDays(2))
        //            {
        //                clsTerminalReport.CommitAndDispose();
        //                if (MessageBox.Show("Transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Do you want to continue?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
        //                {
        //                    clsEvent.AddEventLn("Transaction is not allowed, transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Please change the date.", true);
        //                    MessageBox.Show("Transaction is not allowed, transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Please change the date." +
        //                        Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    txtBarCode.Text = "";
        //                    return false;
        //                }
        //                clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
        //                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;
        //            }
        //        }

        //        clsEvent.AddEventLn("[" + lblCashier.Text + "] Creating new transaction.", true);

        //        mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
        //        try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
        //        catch { }

        //        //mclsSalesTransactionDetails.CustomerID = Convert.ToInt64(lblCustomer.Tag);
        //        //mclsSalesTransactionDetails.CustomerName = lblCustomer.Text;

        //        mclsSalesTransactionDetails.CustomerDetails = mclsContactDetails;
        //        mclsSalesTransactionDetails.CustomerID = mclsContactDetails.ContactID;
        //        mclsSalesTransactionDetails.CustomerName = mclsContactDetails.ContactName;
        //        if (mclsSalesTransactionDetails.CustomerDetails.LastCheckInDate == Constants.C_DATE_MIN_VALUE) mclsSalesTransactionDetails.CustomerDetails.LastCheckInDate = dteTransactionDate;

        //        mclsSalesTransactionDetails.AgentID = Convert.ToInt64(lblAgent.Tag);
        //        mclsSalesTransactionDetails.AgentName = lblAgent.Text;
        //        mclsSalesTransactionDetails.AgentPositionName = lblAgentPositionDepartment.Text;
        //        mclsSalesTransactionDetails.AgentDepartmentName = lblAgentPositionDepartment.Tag.ToString();
        //        mclsSalesTransactionDetails.WaiterID = Convert.ToInt64(lblServedBy.Tag);
        //        mclsSalesTransactionDetails.WaiterName = lblServedBy.Text.Remove(0, 11);
        //        mclsSalesTransactionDetails.CreatedByID = Convert.ToInt64(lblCashier.Tag);
        //        mclsSalesTransactionDetails.CreatedByName = lblCashier.Text;
        //        mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag);
        //        mclsSalesTransactionDetails.CashierName = lblCashier.Text;
        //        mclsSalesTransactionDetails.TransactionDate = dteTransactionDate;
        //        mclsSalesTransactionDetails.DateSuspended = DateTime.MinValue;
        //        mclsSalesTransactionDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
        //        mclsSalesTransactionDetails.BranchID = mclsTerminalDetails.BranchID;
        //        mclsSalesTransactionDetails.BranchCode = mclsTerminalDetails.BranchDetails.BranchCode;
        //        mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Open;
        //        mclsSalesTransactionDetails.TransactionType = mboIsRefund ? TransactionTypes.POSRefund : TransactionTypes.POSNormal;

        //        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
        //        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

        //        mclsSalesTransactionDetails.TransactionNo = clsSalesTransactions.CreateTransactionNo(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
        //        // mclsTransactionStream.Create(mclsSalesTransactionDetails);

        //        lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;

        //        //insert to transaction table 
        //        mclsSalesTransactionDetails.TransactionID = clsSalesTransactions.Insert(mclsSalesTransactionDetails);

        //        mclsSalesTransactionDetails.RewardCardActive = mclsContactDetails.RewardDetails.RewardActive;
        //        mclsSalesTransactionDetails.RewardCardNo = mclsContactDetails.RewardDetails.RewardCardNo;
        //        mclsSalesTransactionDetails.RewardCardExpiry = mclsContactDetails.RewardDetails.ExpiryDate;
        //        mclsSalesTransactionDetails.RewardPreviousPoints = mclsContactDetails.RewardDetails.RewardPoints;

        //        lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();
                
        //        // Sep 24, 2014 : update back the LastCheckInDate to transaction date
        //        Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
        //        mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

        //        clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, dteTransactionDate);

        //        mboIsInTransaction = true;
        //        clsTerminalReport.CommitAndDispose();

        //        InsertAuditLog(AccessTypes.CreateTransaction, "Create transaction #:" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
        //        clsEvent.AddEventLn("Done! Trans #: " + lblTransNo.Text + " has been created.", true);

        //        // Added to put the default transaction charge during creation of transaction
        //        // this is set during the system setup
        //        ApplyTransDefaultCharge();
        //    }
        //    catch (Exception ex)
        //    { clsEvent.AddErrorEventLn(ex); boRetValue = false; }

        //    return boRetValue;
        //}
        private void LoadTransaction(string stTransactionNo, string pstrTerminalNo)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsEvent.AddEvent("Loading transaction : " + stTransactionNo);

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                mclsSalesTransactionDetails = clsSalesTransactions.Details(stTransactionNo, pstrTerminalNo, mclsTerminalDetails.BranchID);

                if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Open ||
                    mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Suspended)
                {
                    // Aug 6, 2011 : Lemu
                    // overwrite to change cashierid and name
                    try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
                    catch { }
                    mclsSalesTransactionDetails.CashierName = lblCashier.Text;

                    // Jan 31, 2015 : Lemu
                    // put back to SuspendedOpen so that it won't be open somewhere else
                    if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Suspended)
                    {
                        clsEvent.AddEvent("Putting transaction SuspendedOpen: " + stTransactionNo);
                        clsSalesTransactions.UpdateTransactionToSuspendedOpen(mclsSalesTransactionDetails.TransactionID);
                    }
                }

                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                Data.ContactDetails clsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
                LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);

                //mclsSalesTransactionDetails.RewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
                //mclsSalesTransactionDetails.RewardCardExpiry = clsContactDetails.RewardDetails.ExpiryDate;
                //mclsSalesTransactionDetails.RewardPreviousPoints = clsContactDetails.RewardDetails.RewardPoints;

                if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Refund || mclsSalesTransactionDetails.TransactionType == TransactionTypes.POSRefund)
                {
                    mboIsRefund = true;
                    lblSubtotalName.Text = "SUBTOTAL: REFUND";
                    lblOrderType.Visible = false;
                }
                lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;
                lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();
                lblCustomer.Text = mclsSalesTransactionDetails.CustomerName;
                lblCustomer.Tag = mclsSalesTransactionDetails.CustomerID.ToString();
                lblAgent.Text = mclsSalesTransactionDetails.AgentName;
                lblAgent.Tag = mclsSalesTransactionDetails.AgentID.ToString();
                lblAgentPositionDepartment.Text = mclsSalesTransactionDetails.AgentPositionName;
                lblAgentPositionDepartment.Tag = mclsSalesTransactionDetails.AgentDepartmentName;
                lblServedBy.Text = "Served by: " + mclsSalesTransactionDetails.WaiterName;
                lblServedBy.Tag = mclsSalesTransactionDetails.WaiterID;

                lblTransDate.Text = mclsSalesTransactionDetails.TransactionDate.ToString("MMM. dd, yyyy hh:mm:ss tt");
                mdteOverRidingPrintDate = mclsSalesTransactionDetails.TransactionDate;

                lblTransDiscount.Tag = mclsSalesTransactionDetails.TransDiscountType.ToString("d");
                cmdPaxAdd.Visible = true; cmdPaxDeduct.Visible = true;
                lblOrders.Text = Constants.C_RESTOPLUS_CUSTOMER_ORDERS + ": " + mclsSalesTransactionDetails.PaxNo.ToString() + " PAX";
                lblOrders.Tag = mclsSalesTransactionDetails.PaxNo.ToString();

                //mclsSalesTransactionDetails.ChargeAmount = mclsSalesTransactionDetails.ChargeAmount;
                if (mclsSalesTransactionDetails.ChargeAmount == 0)
                    lblTransCharge.Tag = ChargeTypes.NotApplicable.ToString("d"); //details.TransDiscountType.ToString("d");
                else
                {
                    //lblTransCharge.Tag = ChargeTypes.Percentage.ToString("d"); //details.TransDiscountType.ToString("d");
                    Data.ChargeType clsChargeType = new Data.ChargeType(mConnection, mTransaction);
                    bool bolInPercent = clsChargeType.Details(mclsSalesTransactionDetails.ChargeCode).InPercent;
                    clsChargeType.CommitAndDispose();

                    if (bolInPercent)
                        lblTransCharge.Tag = ChargeTypes.Percentage.ToString("d");
                    else
                        lblTransCharge.Tag = ChargeTypes.FixedValue.ToString("d");
                }

                Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems(mConnection, mTransaction);
                Data.SalesTransactionItemDetails[] TransactionItems = clsItems.Details(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate);

                clsEvent.AddEventLn("Done loading transaction : " + stTransactionNo, true);

                if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    LoadResumedItems(TransactionItems, true);
                else
                    LoadResumedItems(TransactionItems, false);

                mboIsInTransaction = true;

                clsSalesTransactions.CommitAndDispose();
            }
            catch (Exception ex)
            {
                clsEvent.AddEventLn("ERROR! Loading transaction. TRACE: " + ex.Message, true);
            }
            Cursor.Current = Cursors.Default;
        }
        private void LoadResumedItems(Data.SalesTransactionItemDetails[] Items, bool WillPrintItem)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsEvent.AddEventLn("loading items...", true);
                ItemDataTable.Rows.Clear();

                foreach (Data.SalesTransactionItemDetails item in Items)
                {
                    System.Data.DataRow dr = ItemDataTable.NewRow();

                    dr = setCurrentRowItemDetails(dr, item);
                    dr["ItemNo"] = ItemDataTable.Rows.Count + 1;

                    ItemDataTable.Rows.Add(dr);

                    //dgItems.CurrentRowIndex = ItemDataTable.Rows.Count;
                    //dgItems.Select(dgItems.CurrentRowIndex);
                    //SetItemDetails();

                    string strProductCode = item.ProductCode;
                    if (!string.IsNullOrEmpty(item.MatrixDescription)) strProductCode += "-" + item.MatrixDescription;

                    if (WillPrintItem)
                    {
                        if (item.TransactionItemStatus == TransactionItemStatus.Return)
                        {
                            if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                PrintItem(item.ItemNo, strProductCode + "-RET", item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT, item.DiscountCode, item.ItemDiscountType);
                        }
                        else if (item.TransactionItemStatus != TransactionItemStatus.Void)
                        {
                            if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                PrintItem(item.ItemNo, strProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT, item.DiscountCode, item.ItemDiscountType);
                        }
                    }
                    else
                    {
                        if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                        {
                            if (item.TransactionItemStatus == TransactionItemStatus.Return)
                            {
                                if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                    PrintItem(item.ItemNo, strProductCode + "-RET", item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT, item.DiscountCode, item.ItemDiscountType);
                            }
                            else if (item.TransactionItemStatus != TransactionItemStatus.Void)
                            {
                                if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                    PrintItem(item.ItemNo, strProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT, item.DiscountCode, item.ItemDiscountType);
                            }
                        }
                    }
                }
                if (ItemDataTable.Rows.Count != 0)
                {
                    dgItems.CurrentRowIndex = 0;  //ItemDataTable.Rows.Count;
                    dgItems.Select(dgItems.CurrentRowIndex);
                    SetItemDetails();

                    // 02Nov2014 : set the status as credit payment if creditpayment so that it wont be paid when closing the transaction
                    //           : this will throw an error
                    Data.SalesTransactionItemDetails clsSalesTransactionItemDetails = getCurrentRowItemDetails();
                    if (clsSalesTransactionItemDetails.BarCode == Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE)
                        mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.CreditPayment;
                }

                clsEvent.AddEventLn("Done loading transaction items.", true);

                ComputeSubTotal(); setTotalDetails();
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR! Loading transaction items. TRACE: " + ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// This should always be called to display the TotalDetails
        /// </summary>
        private void setTotalDetails()
        {
            lblSubTotal.Text = mclsSalesTransactionDetails.SubTotal.ToString("###,##0.#0");

            if (mclsSalesTransactionDetails.TransDiscountType == DiscountTypes.NotApplicable)
            {
                lblTransDiscount.Text = "Less 0% / 0.00";
            }
            else if (mclsSalesTransactionDetails.TransDiscountType == DiscountTypes.FixedValue)
            {
                lblTransDiscount.Text = "Less " + mclsSalesTransactionDetails.TransDiscount.ToString("#,##0") + " / " + mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
            }
            else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
            {
                lblTransDiscount.Text = "Less " + mclsSalesTransactionDetails.TransDiscount.ToString("#,##0") + " % / " + mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
            }
            else if (mclsSalesTransactionDetails.TransDiscountType == DiscountTypes.Percentage)
            {
                lblTransDiscount.Text = "Less " + mclsSalesTransactionDetails.TransDiscount.ToString("#,##0") + " % / " + mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
            }

            if (mclsSalesTransactionDetails.ChargeType == ChargeTypes.NotApplicable)
            {
                lblTransCharge.Text = "Plus 0% / 0.00";
            }
            else if (mclsSalesTransactionDetails.ChargeType == ChargeTypes.Percentage)
            {
                lblTransCharge.Text = "Plus " + mclsSalesTransactionDetails.ChargeAmount.ToString("#,##0") + " % / " + mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
            }
            else
            {
                lblTransCharge.Text = "Plus " + mclsSalesTransactionDetails.ChargeAmount.ToString("#,##0") + " / " + mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
            }
        }
        private void SetItemDetails()
        {
            // this is not applicable for restoplus
        }
        private bool IsStartCutOffTimeOK()
        {
            bool bolRetValue = false;
            bool bolMessageBoxShown = false;
            try
            {
                // check if with Cutofftime
                if (mclsTerminalDetails.CheckCutOffTime)
                {
                    clsEvent.AddEventLn("Checking StartCutOffTime vs MAXDateLastInitialized...", true);

                    Data.TerminalReportDetails clsTerminalReportDetails;
                    Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                    mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                    DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

                    DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);
                    DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                    DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime);

                    // if StartCutOffTime is greater than EndCutOffTime
                    // this means that EndCutOffTime is in the morning.
                    // Add 1 more day.
                    if (dteStartCutOffTime >= dteEndCutOffTime)
                        dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

                    DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
                    if (dteTransactionDate < dteEndCutOffTime)
                    {
                        dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
                        dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                    }
                    else
                    {
                        dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
                        dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
                    }
                    if (dteTransactionDate < dteAllowedEndDateTime)
                    {
                        dteStartCutOffTime = dteStartCutOffTime.AddDays(1);
                        dteEndCutOffTime = dteEndCutOffTime.AddDays(1);
                    }

                    DateTime dtePreviousStartCutOffTime = dteStartCutOffTime.AddDays(-1);
                    DateTime dtePreviousEndCutOffTime = dteEndCutOffTime.AddDays(-1);
                    DateTime dtePreviousAllowedStartDateTime = dteAllowedStartDateTime.AddDays(-1);
                    DateTime dtePreviousAllowedEndDateTime = dteAllowedEndDateTime.AddDays(-1);

                Back:
                    clsEvent.AddEventLn("PreviousStartCutOff       :    " + dtePreviousStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PreviousEndCutOff         :    " + dtePreviousEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PrevAllowedStartDateTime  :    " + dtePreviousAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PrevAllowedEndDateTime    :    " + dtePreviousAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);

                    clsEvent.AddEventLn("StartCutOff               :    " + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("EndCutOff                 :    " + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("AllowedStartDateTime      :    " + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("AllowedEndDateTime        :    " + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("MAXDateLastInitialized    :    " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"), true);

                    if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
                    {
                        if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
                        {
                            if (dteTransactionDate >= dteStartCutOffTime && dteTransactionDate <= dteEndCutOffTime)
                            {
                                if (!bolMessageBoxShown)
                                {
                                    MessageBox.Show("Today's EOD was not performed. Performing Auto Z-Read this may take some time. Please wait...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    bolMessageBoxShown = true;
                                }

                                clsEvent.AddEventLn("Today's EOD was not performed: System found AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] > MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
                                clsEvent.AddEventLn("System is Auto-Initializing ZREAD", true);

                                clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                                mstrBeginningTransactionNo = clsTerminalReportDetails.BeginningTransactionNo;

                                PrintZRead(false, clsTerminalReportDetails);

                                dteMAXDateLastInitialized = Convert.ToDateTime(dteMAXDateLastInitialized.AddDays(1));
                                clsTerminalReport.InitializeZRead(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, "System Auto Z-Read", dteMAXDateLastInitialized, true);
                                clsEvent.AddEventLn("Done.", true);
                                goto Back;
                            }
                            else
                            {
                                bolRetValue = true;
                                clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
                            }
                        }
                        else
                        {
                            if (!bolMessageBoxShown)
                            {
                                MessageBox.Show("Previous' day's EOD was not performed. Performing Auto Z-Read this may take some time. Please wait...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bolMessageBoxShown = true;
                            }

                            clsEvent.AddEventLn("Previous' day's EOD was not performed: System found AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] > MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
                            clsEvent.AddEventLn("System is Auto-Initializing ZREAD", true);

                            clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                            mstrBeginningTransactionNo = clsTerminalReportDetails.BeginningTransactionNo;

                            PrintZRead(false, clsTerminalReportDetails);

                            dteMAXDateLastInitialized = Convert.ToDateTime(dteMAXDateLastInitialized.AddDays(1));
                            clsTerminalReport.InitializeZRead(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, "System Auto Z-Read", dteMAXDateLastInitialized, true);
                            clsEvent.AddEventLn("Done.", true);
                            goto Back;
                        }
                    }
                    else
                    {
                        bolRetValue = true;
                        clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
                    }

                    clsTerminalReport.CommitAndDispose();

                }
                else { clsEvent.AddEventLn("OK: StartCutOffTime not configured if greater than MAXDateLastInitialized", true); }

                return bolRetValue;
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
                return false;
            }
        }
        private delegate void SendRLCDelegate();
        private void SendRLC()
        {
            try
            {
                tmr.Enabled = false;

                Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
                DateTime dteDateToProcess = clsTerminalReportHistory.getRLCDateLastInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo);
                clsTerminalReportHistory.CommitAndDispose();

                if (dteDateToProcess == DateTime.MinValue)
                {
                    grpRLC.Visible = false;
                }
                else
                {
                    try
                    {
                        try { grpRLC.Visible = true; }
                        catch { }
                    Back:

                        lblMallForwarderStatus.Text = "RLC Notification: Trying to send unsent files...";

                        AceSoft.RetailPlus.Forwarder.RLCDetails clsRLCDetails = new AceSoft.RetailPlus.Forwarder.RLCDetails();
                        clsRLCDetails.TenantCode = RLC_CONFIG.TenantCode;
                        clsRLCDetails.TenantName = RLC_CONFIG.TenantName;
                        clsRLCDetails.OutputDirectory = RLC_CONFIG.OutputDirectory;
                        clsRLCDetails.FTPIPAddress = CONFIG.FTPIPAddress;
                        clsRLCDetails.FTPUsername = CONFIG.FTPUsername;
                        clsRLCDetails.FTPPassword = CONFIG.FTPPassword;
                        clsRLCDetails.FTPDirectory = CONFIG.FTPDirectory;

                        AceSoft.RetailPlus.Forwarder.RLC clsRLC = new AceSoft.RetailPlus.Forwarder.RLC();
                        clsRLC.RLCDetails = clsRLCDetails;
                        bool bolCreateAndTransferFile = clsRLC.CreateAndTransferFile(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, dteDateToProcess);

                        if (bolCreateAndTransferFile)
                        { tmrRLC.Enabled = false; lblMallForwarderStatus.Text = "Success in sending file(s) to RLC server. Press [Ctrl+C] to close this notification."; }
                        else
                        { tmrRLC.Enabled = true; lblMallForwarderStatus.Text = "Error sending file(s) to RLC server. Press [Ctrl+C] to close this notification."; }

                        clsTerminalReportHistory = new Data.TerminalReportHistory();
                        dteDateToProcess = clsTerminalReportHistory.getRLCDateLastInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo);
                        clsTerminalReportHistory.CommitAndDispose();

                        if (dteDateToProcess != DateTime.MinValue)
                        { goto Back; }
                    }
                    catch { tmrRLC.Enabled = true; lblMallForwarderStatus.Text = "Error sending file(s) to RLC server. Press [Ctrl+C] to close this notification."; }
                }
            }
            catch { }
        }

		#endregion

		#region Marquee

		private void StartMarqueeThread()
		{
			try
			{
				MarqueeThread.Abort();
				// give the thread time to die
				Thread.Sleep(100);
				Invalidate();
				MarqueeThread = new Thread(new ThreadStart(DrawMovingText));
				MarqueeThread.Start();
			}
			catch (ThreadAbortException taex)
			{
				StartMarqueeThread();
				clsEvent.AddEventLn("Marquee Thread has expired. New instance of marquee thread has been started.", true);
				clsEvent.AddEventLn("Error;" + taex.Message, true);
			}
			catch { }
		}
		private void DrawMovingText()
		{
			string marqueemessage = mclsTerminalDetails.MarqueeMessage;

			if (marqueemessage.Length < 97)
				marqueemessage = marqueemessage + " " + marqueemessage;

			// 
			// StringBuilder is used to allow for efficient manipulation of one string, 
			// rather than generating many separate strings
			//
			StringBuilder str = new StringBuilder(marqueemessage);

			int numCycles = lblMessage.Width * 3 + 1;
			for (int i = 0; i < numCycles; ++i)
			{
				//lblMessage.Text = str.ToString();
				this.SetText(str.ToString());

				if (!mboIsInTransaction)
				{
					if (lblMessage.Text.Length <= 40)
						SendStringToTurret(lblMessage.Text);
					else
						SendStringToTurret(lblMessage.Text.Substring(0, 40));
				}
				// relocate the first char to the end of the string
				str.Append(str[0]);
				str.Remove(0, 1);
				// pause for visual effect
				Thread.Sleep(800);
			}
		}
		delegate void SetTextCallback(string text);
		private void SetText(string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (this.lblMessage.InvokeRequired)
			{
				SetTextCallback d = new SetTextCallback(SetText);
				this.Invoke(d, new object[] { text });
			}
			else
			{
				this.lblMessage.Text = text;
			}
		}

		#endregion

		#region Command No Click

		private void cmdNo1_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("1");
		}
		private void cmdNo2_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("2");
		}
		private void cmdNo3_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("3");
		}
		private void cmdNo4_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("4");
		}
		private void cmdNo5_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("5");
		}
		private void cmdNo6_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("6");
		}
		private void cmdNo7_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("7");
		}
		private void cmdNo8_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("8");
		}
		private void cmdNo9_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("9");
		}
		private void cmdNo0_Click(object sender, System.EventArgs e)
		{
			cmdNoClick("0");
		}
		private void cmdNoClick(string Value)
		{
			//txtBarCode.Text = txtBarCode.Text + Value;
			txtBarCode.Focus();
			SendKeys.Send(Value);
		}
		private void cmdEnter_Click(object sender, System.EventArgs e)
		{
			//KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
			//MainRestoWnd_KeyDown(null, enter);
			//txtBarCode.Focus();
			cmdNoClick("{ENTER}");
		}

		#endregion

		#region Command Click

		private void CommandClick(int no)
		{
			try
			{
				//dgItems.Height = 363;

				switch (no)
				{
					//
					case 1: this.SelectTable(); break;

					//	
					case 2: this.SuspendTransaction(true); break;

					//	
					case 3: this.VoidTransaction(); break;

					//
					case 4: this.ChangeOrderType(); break;
						
					//	
					case 5: this.ShowOtherCommands(); break;

					//	
					case 6: this.ShowPrintWindow(); break;

					//	
					case 7: this.CheckInTable(); break;

					//	
                    case 8: this.PrintCheckOutBill(); break;

					//	
                    case 9: this.ApplyTransDiscount(); break;

					//	
					case 10: this.SplitTransaction(); break;

					//	
					case 11: this.CloseTransaction(); break;

					//	
					case 12: this.LoggedOutCashier(true); break;

					//	Return Item
					case 13: { this.ReturnItem(); break; }

					//	Void Item
					case 14: { this.VoidItem(); break; }

					//	Discount
					case 15: { this.ApplyItemDiscount(); break; }

					//	
					case 16: { break; }

					//	Select Bagger/Waiter
					case 17: this.SelectWaiter(); break;

					//	ChangeOrderType
					case 18: this.ChangeOrderType(); break;

					//	customer refund
					case 19: this.RefundTransaction(); break;

					//	Apply Trans Charge
					case 20: this.ApplyTransCharge(); break;

					//	Apply Trans Discount
					case 21: { this.ApplyTransDiscount(); break; }

					//	void transaction
					case 22: { this.VoidTransaction(); break; }

					//	check-out bill
					case 23: { this.PrintCheckOutBill(); break; }

					//	order slip
					case 24: { PrintOrderSlip(false); break; }

					//	show reports window
					case 25: this.ShowPrintWindow(); break;

					//	Print Header
					case 26:
						{
							DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTransactionHeader);

							if (loginresult == DialogResult.OK)
							{
                                PrintReportHeaderSection(true, DateTime.MinValue);
								MessageBox.Show("Transaction header has been printed.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							break;
						}

					//	Open Drawer
					case 27: this.OpenDrawer(); break;

					//	Z-read
					case 28: { this.InitializeZRead(false); break; }

					//	Mall forwarder
					case 29: { this.ShowMallForwarder(); break; }

					//	
					case 30: { break; }

					//	
					case 31: { break; }

					//	
					case 32: { break; }
				}
			}
			catch (Exception ex)
            {
                clsEvent.AddErrorEventLn(ex);
            }
			finally
			{
				txtBarCode.Focus();
			}
		}

		#endregion

		#region Transaction Command Click

		private void cmd1_Click(object sender, EventArgs e)
		{
			CommandClick(1);
		}
		private void cmd2_Click(object sender, EventArgs e)
		{
			CommandClick(2);
		}
		private void cmd3_Click(object sender, EventArgs e)
		{
			CommandClick(3);
		}
		private void cmd4_Click(object sender, EventArgs e)
		{
			CommandClick(4);
		}
		private void cmd5_Click(object sender, EventArgs e)
		{
			CommandClick(5);
		}
		private void cmd6_Click(object sender, EventArgs e)
		{
			CommandClick(6);
		}
		private void cmd7_Click(object sender, EventArgs e)
		{
			CommandClick(7);
		}
		private void cmd8_Click(object sender, EventArgs e)
		{
			CommandClick(8);
		}
		private void cmd9_Click(object sender, EventArgs e)
		{
			CommandClick(9);
		}
		private void cmd10_Click(object sender, EventArgs e)
		{
			CommandClick(10);
		}
		private void cmd11_Click(object sender, EventArgs e)
		{
			CommandClick(11);
		}
		private void cmd12_Click(object sender, EventArgs e)
		{
			CommandClick(12);
		}

		#endregion

		#region Item Command Click

		private void dgItems_CurrentCellChanged(object sender, EventArgs e)
		{
			try
			{
				if (mbodgItemRowClick)
				{
					int iRow = dgItems.CurrentRowIndex;
					DataGridCell dgCell = dgItems.CurrentCell;

					if (dgCell.ColumnNumber == 1) this.VoidItem();
					if (dgCell.ColumnNumber == 5) this.ApplyItemDiscount();
					if (dgCell.ColumnNumber == 8) this.ChangeQuantity();
					if (dgCell.ColumnNumber == 9) this.ChangePrice();
					if (dgCell.ColumnNumber == 13) this.ChangeItemRemarks();
					if (dgCell.ColumnNumber == 46) this.ChangePaxNo();

					try { dgItems.Select(iRow); }
					catch { }
					Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
					DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
					DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
				}
			}
			catch { }
			finally { txtBarCode.Focus(); }
		}
		private void dgItems_KeyUp(object sender, KeyEventArgs e)
		{
			try { txtBarCode.Focus(); } catch { }
		}
		private void dgItems_Click(object sender, EventArgs e)
		{
            if (mboIsInTransaction || ItemDataTable.Rows.Count > 0)
            {
                dgItems_CurrentCellChanged(sender, e);
                try { txtBarCode.Focus(); }
                catch { }
            }
		}
		private void dgItems_GotFocus(object sender, System.EventArgs e)
		{
			mbodgItemRowClick = true;
		}
		private void dgItems_LostFocus(object sender, System.EventArgs e)
		{
			mbodgItemRowClick = false;
			try { dgItems.Select(dgItems.CurrentRowIndex); } catch { }
		}
		private void dgItems_MouseMove(object sender, MouseEventArgs e)
		{
			mbodgItemRowClick = true;
			try { dgItems.Select(dgItems.CurrentRowIndex); }
			catch { }
		}
		private void dgItems_MouseLeave(object sender, EventArgs e)
		{
			mbodgItemRowClick = false;
			try { dgItems.Select(dgItems.CurrentRowIndex); } catch { }
		}
		private void lblTransNo_Click(object sender, System.EventArgs e)
		{
            ResumeTransaction();
		}
		

		#endregion

		#region Product SubGroup Click

		private void cmdSubGroupRight_Click(object sender, EventArgs e)
		{
			LoadProductSubGroups(System.Data.SqlClient.SortOrder.Ascending);
		}
		private void cmdSubGroupLeft_Click(object sender, EventArgs e)
		{
			LoadProductSubGroups(System.Data.SqlClient.SortOrder.Descending);
		}

		private void LoadProductSubGroups(System.Data.SqlClient.SortOrder SequenceSortOrder)
		{
			try
			{
				tblLayoutGroup.Controls.Clear();
				if (mboLocked) return;

				Int64 intSequenceNoStart = 0;

				if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
					try { intSequenceNoStart = long.Parse(cmdSubGroupLeft.Tag.ToString()); }
					catch { }
				else
					try { intSequenceNoStart = long.Parse(cmdSubGroupRight.Tag.ToString()); }
					catch { }

                // Sep 24, 2014 put an override if cmdSubGroupLeft.Tag = 0
                // always do an asceding coz its already the end.
                if (intSequenceNoStart < Constants.C_RESTOPLUS_MAX_SUB_GROUP) intSequenceNoStart = 0; //reset to 0 if it's 1
                if (intSequenceNoStart == 0) SequenceSortOrder = System.Data.SqlClient.SortOrder.Ascending;

				ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
				clsProductSubGroupColumns.ProductSubGroupCode = true;
				clsProductSubGroupColumns.SequenceNo = true;

				ProductSubGroup clsProductSubGroup = new ProductSubGroup(mConnection, mTransaction);
                System.Data.DataTable dtProductSubGroup = clsProductSubGroup.ListAsDataTable(clsProductSubGroupColumns, new ProductSubGroupDetails(), intSequenceNoStart, SequenceSortOrder, SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending ? Constants.C_RESTOPLUS_MAX_SUB_GROUP : Constants.C_RESTOPLUS_MAX_SUB_GROUP + 1, "SequenceNo", SequenceSortOrder);
				clsProductSubGroup.CommitAndDispose();

                // re-order the products by sequence no
                if (dtProductSubGroup.Rows.Count > 0)
                {
                    System.Data.DataView dv = dtProductSubGroup.DefaultView;
                    dv.Sort = "SequenceNo";
                    dtProductSubGroup = dv.ToTable();
                }

				int iRow = 0;
				int iCol = 0;
				int iCtr = 1;

				if (dtProductSubGroup.Rows.Count == 0)
				{
					cmdSubGroupLeft.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
					cmdSubGroupRight.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
				}
				foreach (System.Data.DataRow dr in dtProductSubGroup.Rows)
				{
					if (iCtr > Constants.C_RESTOPLUS_MAX_SUB_GROUP) break;

                    if (iCtr == 1) cmdSubGroupLeft.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString();
                    if (iCtr >= 1 && dtProductSubGroup.Rows.Count > Constants.C_RESTOPLUS_MAX_SUB_GROUP) cmdSubGroupRight.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString();

					SubGroupButton cmdSubGroup = new SubGroupButton();

					cmdSubGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					cmdSubGroup.BackColor = System.Drawing.Color.Red;
					cmdSubGroup.Dock = System.Windows.Forms.DockStyle.Fill;
					cmdSubGroup.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
					cmdSubGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					cmdSubGroup.ForeColor = System.Drawing.SystemColors.ControlText;
					cmdSubGroup.GradientBottom = System.Drawing.Color.Red;
					cmdSubGroup.GradientTop = System.Drawing.Color.Gold;
					cmdSubGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
					cmdSubGroup.Location = new System.Drawing.Point(3, 3);
					cmdSubGroup.Size = new System.Drawing.Size(110, 82);
					cmdSubGroup.TabIndex = iCtr-1;
					cmdSubGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
					cmdSubGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					cmdSubGroup.UseVisualStyleBackColor = false;

					cmdSubGroup.Name = "cmdSubGroup" + iCtr.ToString();
					cmdSubGroup.Text = dr[Data.ProductSubGroupColumnNames.ProductSubGroupCode].ToString();
					cmdSubGroup.Tag = dr[Data.ProductSubGroupColumnNames.ProductSubGroupID].ToString();
					cmdSubGroup.Click += new System.EventHandler(cmdSubGroup_Click);

                    try { cmdSubGroup.Image = new Bitmap(Application.StartupPath + "/images/subgroups/" + dr[Data.ProductSubGroupColumnNames.ImagePath].ToString()); }
					catch { }
                    //try { cmdSubGroup.Image = new Bitmap(Application.StartupPath + "/images/subgroups/subgroup" + iCtr.ToString() + ".gif"); }
                    //catch { }

					tblLayoutGroup.Controls.Add(cmdSubGroup, iCol, iRow);

					iCol++; iCtr++;
				}
			}
			catch { }
		}
		private void cmdSubGroup_Click(object sender, EventArgs e)
		{
			try
			{
				SubGroupButton cmdSubGroup = (SubGroupButton)sender;

				lblItems.Text = cmdSubGroup.Text;
				lblItems.Tag = cmdSubGroup.Tag.ToString();
				cmdProductLeft.Tag = "0"; cmdProductRight.Tag = "0";
				LoadProducts(System.Data.SqlClient.SortOrder.Ascending);
			}
			catch { }
			finally { txtBarCode.Focus(); }
		}

		#endregion

		#region Product Click

		private void LoadProducts(System.Data.SqlClient.SortOrder SequenceSortOrder)
		{
			try
			{
				tblLayoutProducts.Controls.Clear();
				if (mboLocked) return;

				Int64 intSequenceNoStart = 0;

				if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
					try { intSequenceNoStart = long.Parse(cmdProductLeft.Tag.ToString()); }
					catch { }
				else
					try { intSequenceNoStart = long.Parse(cmdProductRight.Tag.ToString()); }
					catch { }

                // Sep 24, 2014 put an override if cmdSubGroupLeft.Tag = 0
                // always do an asceding coz its already the end.
                if (intSequenceNoStart < Constants.C_RESTOPLUS_MAX_PRODUCTS) intSequenceNoStart = 0; //reset to 0 if it's 1
                if (intSequenceNoStart == 0) SequenceSortOrder = System.Data.SqlClient.SortOrder.Ascending;

                //if (tblLayoutProducts.Controls.Count <= Constants.C_RESTOPLUS_MAX_PRODUCTS) { intSequenceNoStart -= 1; SequenceSortOrder = System.Data.SqlClient.SortOrder.Ascending; }

				ProductColumns clsProductColumns = new ProductColumns();
				clsProductColumns.BarCode = true;
				clsProductColumns.ProductCode = true;
				clsProductColumns.SequenceNo = true;

				ProductColumns clsSearchColumns = new ProductColumns();

				long lngProductSubGroupID = 0;
				try { lngProductSubGroupID = long.Parse(lblItems.Tag.ToString()); }
				catch { }
				Products clsProduct = new Products(mConnection, mTransaction);
                // always put the branchid as zero so that it will show all products
                System.Data.DataTable dtProduct = clsProduct.ListAsDataTable(clsProductColumns, 0, ProductListFilterType.ShowActiveOnly, intSequenceNoStart, SequenceSortOrder,
                    clsSearchColumns, string.Empty, Constants.ZERO, Constants.ZERO, string.Empty, lngProductSubGroupID, string.Empty, SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending ? Constants.C_RESTOPLUS_MAX_PRODUCTS : Constants.C_RESTOPLUS_MAX_PRODUCTS + 1, mclsTerminalDetails.ShowItemMoreThanZeroQty, true, "SequenceNo", SequenceSortOrder == System.Data.SqlClient.SortOrder.Ascending ? SortOption.Ascending : SortOption.Desscending);

                //System.Data.DataTable dtProduct = clsProduct.ListAsDataTableFE(Constants.BRANCH_ID_MAIN, string.Empty, ProductListFilterType.ShowActiveOnly, Constants.C_RESTOPLUS_MAX_PRODUCTS + 1, false, "SequenceNo", System.Data.SqlClient.SortOrder.Ascending);

                clsProduct.CommitAndDispose();

                // re-order the products by sequcen no
                if (dtProduct.Rows.Count > 0)
                {
                    System.Data.DataView dv = dtProduct.DefaultView;
                    dv.Sort = "SequenceNo";
                    dtProduct = dv.ToTable();
                }

				int iRow = 0;
				int iCol = 0;
				int iCtr = 1;

				if (dtProduct.Rows.Count == 0)
				{
					cmdProductLeft.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
					cmdProductRight.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
				}
				foreach (System.Data.DataRow dr in dtProduct.Rows)
				{
					if (iCtr > Constants.C_RESTOPLUS_MAX_PRODUCTS) break;

                    if (iCtr == 1) cmdProductLeft.Tag = dr[Data.ProductColumnNames.SequenceNo].ToString();
                    if (iCtr >= 1 && dtProduct.Rows.Count > Constants.C_RESTOPLUS_MAX_PRODUCTS) cmdProductRight.Tag = dr[Data.ProductColumnNames.SequenceNo].ToString();

                    //if (iCtr == Constants.C_RESTOPLUS_MAX_PRODUCTS && dtProduct.Rows.Count > Constants.C_RESTOPLUS_MAX_PRODUCTS)
                    //{
                    //    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                    //        try { if (iCtr == 1) cmdProductLeft.Tag = dr[Data.ProductColumnNames.SequenceNo].ToString(); }
                    //        catch { }
                    //    else
                    //        try { cmdProductRight.Tag = dr[Data.ProductColumnNames.SequenceNo].ToString(); }
                    //        catch { }
                    //}
                    //else if (dtProduct.Rows.Count > 0 && dtProduct.Rows.Count <= Constants.C_RESTOPLUS_MAX_PRODUCTS)
                    //{
                    //    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                    //    {
                    //        try {
                    //            if (Int32.Parse(cmdProductRight.Tag.ToString()) < Int32.Parse(dr[Data.ProductColumnNames.SequenceNo].ToString()))
                    //                cmdProductRight.Tag = dr[Data.ProductColumnNames.SequenceNo].ToString(); 
                    //        }
                    //        catch { }
                    //        cmdProductLeft.Tag = "0".ToString();
                    //    }
                    //    else
                    //    {
                    //        try { if (iCtr == 1) cmdProductLeft.Tag = dr[Data.ProductColumnNames.SequenceNo].ToString(); }
                    //        catch { }
                    //        // cmdProductRight.Tag = cmdProductRight.Tag; // do not reset
                    //    }
                    //}

					ProductButton cmdProduct = new ProductButton();

					cmdProduct.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					cmdProduct.BackColor = System.Drawing.Color.WhiteSmoke;
					cmdProduct.Dock = System.Windows.Forms.DockStyle.Fill;
					cmdProduct.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
					cmdProduct.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					cmdProduct.ForeColor = System.Drawing.SystemColors.ControlText;

					cmdProduct.GradientBottom = System.Drawing.Color.DarkGray;
					cmdProduct.GradientTop = System.Drawing.Color.WhiteSmoke;
					cmdProduct.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
					cmdProduct.Location = new System.Drawing.Point(3, 3);
					cmdProduct.Size = new System.Drawing.Size(115, 89);
					cmdProduct.TabIndex = iCtr - 1;
					cmdProduct.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
					cmdProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					cmdProduct.UseVisualStyleBackColor = false;

					cmdProduct.Name = "cmdProduct" + iCtr.ToString();
					string strProductCode = dr[Data.ProductColumnNames.ProductCode].ToString();
					if (strProductCode.Length > 12)
					{
                        //string strTempProductCode = "";
						string[] strCodes = strProductCode.Split(' ');
                        int ixCtr = 0;
						foreach (string strCode in strCodes)
						{
                            if (ixCtr == 0)
                                cmdProduct.Text = strCode;
                            else
                                cmdProduct.Text += "\r\n" + strCode;

                            ixCtr++;
                            //if ((strTempProductCode.Length + strCode.Length) < 12)
                            //{
                            //    strTempProductCode += (strTempProductCode.Length == 0) ? strCode : " " + strCode;
                            //}
                            //else
                            //{
                            //    cmdProduct.Text += (cmdProduct.Text.Length == 0) ? strTempProductCode : "\r\n" + strTempProductCode;
                            //    strTempProductCode = strCode;
                            //}
                            //if ((strProductCode.LastIndexOf(strCode) + strCode.Length) == strProductCode.Length) //mean this is the last
                            //    cmdProduct.Text += "\r\n" + strTempProductCode;
						}
						
					}
					else {
						cmdProduct.Text = strProductCode;
					}
					
					cmdProduct.Tag = dr[Data.ProductColumnNames.BarCode].ToString();
					cmdProduct.Click += new System.EventHandler(cmdProduct_Click);
					
					tblLayoutProducts.Controls.Add(cmdProduct, iCol, iRow);

					iCol++; iCtr++;
				}
			}
			catch { }

		}

		private void cmdProduct_Click(object sender, EventArgs e)
		{
			try
			{
				ProductButton cmdProduct = (ProductButton)sender;

				txtBarCode.Text = cmdProduct.Tag.ToString();
				ReadBarCode();
			}
			catch { }
			finally { txtBarCode.Focus(); }
		}

		#endregion

		private void cmdExit_Click(object sender, EventArgs e)
		{
			this.Exit();
		}

		private void lblServedBy_Click(object sender, EventArgs e)
		{
			if (!mboLocked) this.SelectWaiter();
		}

		private void cmdPaxAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (mboIsInTransaction)
				{
					int iNoOfPax = 1;
					try { iNoOfPax = int.Parse(lblOrders.Tag.ToString()); }
					catch { }
					iNoOfPax += 1;
					lblOrders.Text = Constants.C_RESTOPLUS_CUSTOMER_ORDERS + ": " + iNoOfPax.ToString() + " PAX";
					lblOrders.Tag = iNoOfPax.ToString();
                    mclsSalesTransactionDetails.PaxNo = iNoOfPax;
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					clsSalesTransactions.UpdatePaxNo(mclsSalesTransactionDetails.TransactionID, iNoOfPax);
					clsSalesTransactions.CommitAndDispose();
				}
			}
			catch (Exception ex) { throw ex; }
			finally { txtBarCode.Focus(); }
		}

		private void cmdPaxDeduct_Click(object sender, EventArgs e)
		{
			try
			{
				if (mboIsInTransaction)
				{
					int iNoOfPax = 1;
					try { iNoOfPax = int.Parse(lblOrders.Tag.ToString()); }
					catch { }
					if (iNoOfPax <= 1)
					{ iNoOfPax = 1; }
					else
					{ iNoOfPax -= 1; }
					lblOrders.Text = Constants.C_RESTOPLUS_CUSTOMER_ORDERS + ": " + iNoOfPax.ToString() + " PAX";
					lblOrders.Tag = iNoOfPax.ToString();
                    mclsSalesTransactionDetails.PaxNo = iNoOfPax;
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					clsSalesTransactions.UpdatePaxNo(mclsSalesTransactionDetails.TransactionID, iNoOfPax);
					clsSalesTransactions.CommitAndDispose();
				}
			}
			catch (Exception ex) { throw ex; }
			finally { txtBarCode.Focus(); }
		}

		private void cmdProductLeft_Click(object sender, EventArgs e)
		{
			LoadProducts(System.Data.SqlClient.SortOrder.Descending);
		}

		private void cmdProductRight_Click(object sender, EventArgs e)
		{
			LoadProducts(System.Data.SqlClient.SortOrder.Ascending);
		}

        private void lblOrders_Click(object sender, EventArgs e)
        {
            if (!mboIsInTransaction)
            {
                MessageBox.Show("No active transaction found.", "RetailPlus", MessageBoxButtons.OK);
                return;
            }
            if (mboIsInTransaction)
            {
                decimal decRetValue = 0;
                int iNoOfPax = Int32.TryParse(lblOrders.Tag.ToString(), out iNoOfPax) ? iNoOfPax : 0;
                if (ShowNoControl(this, out decRetValue, decimal.Parse(iNoOfPax.ToString()), "Enter no of Pax to pay.") == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        iNoOfPax = Int32.TryParse(decRetValue.ToString(), out iNoOfPax) ? iNoOfPax : 0;
                        lblOrders.Text = Constants.C_RESTOPLUS_CUSTOMER_ORDERS + ": " + iNoOfPax.ToString() + " PAX";
                        lblOrders.Tag = iNoOfPax.ToString();
                        mclsSalesTransactionDetails.PaxNo = iNoOfPax;
                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
                        clsSalesTransactions.UpdatePaxNo(mclsSalesTransactionDetails.TransactionID, iNoOfPax);
                        clsSalesTransactions.CommitAndDispose();
                    }
                    catch (Exception ex) { throw ex; }
                    finally { txtBarCode.Focus(); }
                }
            }
        }
	}
}