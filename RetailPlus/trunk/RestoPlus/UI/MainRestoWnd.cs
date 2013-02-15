using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Text;

using AceSoft.RetailPlus.Reports;
using AceSoft.RetailPlus.Security;
using AceSoft.RetailPlus.Data;

using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Drawing2D;

namespace AceSoft.RetailPlus.Client.UI
{
	public class MainRestoWnd : Form
	{
		#region Declarations
		
		private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainRestoWnd));
		private IContainer components;
		private System.Data.DataTable ItemDataTable;
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
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.Timer tmrRLC;

		private bool mboIsRefund;
		private bool mboIsItemHeaderPrinted;
		private bool mboIsInTransaction;
		private bool mboLocked;
		private bool mbodgItemRowClick;
		private bool mboIsCashCountInitialized;
		private DateTime mdtCurrentDateTime;

		Data.TerminalDetails mclsTerminalDetails;
		Data.SalesTransactionDetails mclsSalesTransactionDetails;
		Data.ContactDetails mclsContactDetails;
		TransactionStream mclsTransactionStream = new TransactionStream();
		Event clsEvent = new Event();
		FilePrinter mclsFilePrinter = new FilePrinter();
		string mstrToPrint = string.Empty;

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
		private MenuButton cmd9;
		private MenuButton cmd8;
		private MenuButton cmd7;
		private MenuButton cmd10;
		private MenuButton cmd11;
		private DataGridTableStyle dgStyle;
		private DataGrid dgItems;
		private DataGridTextBoxColumn Commision;
		private DataGridTextBoxColumn PercentageCommision;
		private DataGridTextBoxColumn OrderSlipPrinted;
		private DataGridTextBoxColumn OrderSlipPrinter;
		private DataGridTextBoxColumn IncludeInSubtotalDiscount;
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
			this.Lock();
			try
			{
				MarqueeThread = new Thread(new ThreadStart(DrawMovingText));
			}
			catch { }
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
			this.tmr = new System.Windows.Forms.Timer(this.components);
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
			this.PercentageCommision = new System.Windows.Forms.DataGridTextBoxColumn();
			this.OrderSlipPrinted = new System.Windows.Forms.DataGridTextBoxColumn();
			this.OrderSlipPrinter = new System.Windows.Forms.DataGridTextBoxColumn();
			this.IncludeInSubtotalDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
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
			this.cmd9 = new AceSoft.RetailPlus.Client.MenuButton();
			this.cmd8 = new AceSoft.RetailPlus.Client.MenuButton();
			this.cmd7 = new AceSoft.RetailPlus.Client.MenuButton();
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
			this.panLocked.Location = new System.Drawing.Point(0, 30);
			this.panLocked.Name = "panLocked";
			this.panLocked.Size = new System.Drawing.Size(923, 478);
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
			this.lblTerminalNoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// tmr
			// 
			this.tmr.Interval = 1;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
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
			this.lblOrders.Text = "CUSTOMER ORDERS";
			this.lblOrders.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// OrderSlipPrinter
			// 
			this.OrderSlipPrinter.Format = "";
			this.OrderSlipPrinter.FormatInfo = null;
			this.OrderSlipPrinter.MappingName = "OrderSlipPrinter";
			this.OrderSlipPrinter.NullText = "";
			this.OrderSlipPrinter.ReadOnly = true;
			this.OrderSlipPrinter.Width = 0;
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
			this.cmd9.TabIndex = 120;
			this.cmd9.Text = "charge";
			this.cmd9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.cmd9.UseVisualStyleBackColor = false;
			this.cmd9.Click += new System.EventHandler(this.cmd9_Click);
			// 
			// cmd8
			// 
			this.cmd8.BackColor = System.Drawing.Color.Transparent;
			this.cmd8.Enabled = false;
			this.cmd8.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
			this.cmd8.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.cmd8.GradientBottom = System.Drawing.Color.DarkGreen;
			this.cmd8.GradientTop = System.Drawing.Color.GreenYellow;
			this.cmd8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.cmd8.Location = new System.Drawing.Point(926, 429);
			this.cmd8.Name = "cmd8";
			this.cmd8.Size = new System.Drawing.Size(92, 51);
			this.cmd8.TabIndex = 119;
			this.cmd8.Text = "discount";
			this.cmd8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.cmd8.UseVisualStyleBackColor = false;
			this.cmd8.Click += new System.EventHandler(this.cmd8_Click);
			// 
			// cmd7
			// 
			this.cmd7.BackColor = System.Drawing.Color.Transparent;
			this.cmd7.Enabled = false;
			this.cmd7.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
			this.cmd7.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmd7.GradientBottom = System.Drawing.Color.DarkGreen;
			this.cmd7.GradientTop = System.Drawing.Color.GreenYellow;
			this.cmd7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.cmd7.Location = new System.Drawing.Point(926, 378);
			this.cmd7.Name = "cmd7";
			this.cmd7.Size = new System.Drawing.Size(92, 51);
			this.cmd7.TabIndex = 118;
			this.cmd7.Text = "check-out bill";
			this.cmd7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.cmd7.UseVisualStyleBackColor = false;
			this.cmd7.Click += new System.EventHandler(this.cmd7_Click);
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
			this.Controls.Add(this.cmd9);
			this.Controls.Add(this.cmd8);
			this.Controls.Add(this.cmd7);
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
			AccessUser clsUser = new AccessUser();
			AccessUserDetails details = clsUser.Details(UserID);
			clsUser.CommitAndDispose();

			clsEvent.AddEvent("[" + details.Name + "] UnLocking client application.");
			try
			{
				this.panLocked.Visible = false;

				this.lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
				this.lblCashier.Tag = details.UID;
				this.lblCashier.Text = details.Name;

				EnableCommandControls(true);
				this.cmdSubGroupLeft.Visible = true;
				this.cmdSubGroupRight.Visible = true;

				this.mboLocked = false;
				
				this.txtBarCode.Text = "";
				this.txtBarCode.Enabled = true;
				this.txtBarCode.Focus();
				

				mclsSalesTransactionDetails.CashierID = details.UID;
				mclsSalesTransactionDetails.CashierName = details.Name;

				InsertAuditLog(AccessTypes.UnlockTerminal, "Unlock terminal #: " + mclsTerminalDetails.TerminalNo);
				clsEvent.AddEventLn("Done!");
			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); }
		}
		private void EnableCommandControls(bool isEnable)
		{
			this.cmd1.Enabled = isEnable;
			this.cmd2.Enabled = isEnable;
			this.cmd3.Enabled = isEnable;
			this.cmd4.Enabled = isEnable;
			this.cmd5.Enabled = isEnable;
			this.cmd6.Enabled = isEnable;
			this.cmd7.Enabled = isEnable;
			this.cmd8.Enabled = isEnable;
			this.cmd9.Enabled = isEnable;
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

			LoadOptions();

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
								DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTransactionHeader);

								if (loginresult == DialogResult.None)
								{
									LogInWnd login = new LogInWnd();

									login.AccessType = AccessTypes.PrintTransactionHeader;
									login.Header = "Print Transaction Header";
									login.ShowDialog(this);
									loginresult = login.Result;
									login.Close();
									login.Dispose();
								}
								if (loginresult == DialogResult.OK)
								{
									PrintReportHeaderSection(true);
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
								CreditCardDeclareAsLost();
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
								EnterCreditPayment();
								break;
						}
					}
					else if (Control.ModifierKeys == Keys.Alt)
					{
						switch (e.KeyCode)
						{
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

							//case Keys.Enter:
							//    mclsTerminalDetails.TrustFund = 0;
							//    MessageBox.Show("Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
							//    break;
							
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
										ReadBarCode();
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
								InitializeZRead();
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

				Data.Terminal clsTerminal = new Data.Terminal();
				mclsTerminalDetails = clsTerminal.Details(Constants.TerminalBranchID, CompanyDetails.TerminalNo);
				
				mclsTerminalDetails.CashCountBeforeReport = true; //this should always be true for touch screens

				Data.Contacts clsContact = new Data.Contacts(clsTerminal.Connection, clsTerminal.Transaction);
				mclsContactDetails = clsContact.Details(Constants.C_RETAILPLUS_CUSTOMERID);

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
				lblTransDiscount.Text = "";
				lblTransDiscount.Tag = DiscountTypes.NotApplicable.ToString("d");
				lblOrders.Text = "CUSTOMERS ORDERS: 1 pax";
				lblOrders.Tag = "1";

				if (mclsTerminalDetails.WithRestaurantFeatures)
				{ lblSubtotalName.Text = "SUBTOTAL:"; lblOrderType.Visible = true; lblOrderType.Text = OrderTypes.DineIn.ToString("G").ToUpper(); }
				else
				{ lblSubtotalName.Text = "SUBTOTAL"; lblOrderType.Visible = false; lblOrderType.Text = OrderTypes.DineIn.ToString("G").ToUpper(); }

				lblMessage.Text = " Your suggestive selling message and/or description";
				lblTransCharge.Text = "";
				lblTransCharge.Tag = ChargeTypes.NotApplicable.ToString("d");
				txtBarCode.Text = "";
				cmdPaxAdd.Visible = false;
				cmdPaxDeduct.Visible = false;

				mboIsRefund = false;

				mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
				try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
				catch { }
				mclsSalesTransactionDetails.CashierName = lblCashier.Text;

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
					mboIsCashCountInitialized = clsTerminal.IsCashCountInitialized(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID);

				clsTerminal.CommitAndDispose();

				cmdSubGroupLeft.Tag = "0";
				cmdSubGroupRight.Tag = "0";
				LoadProductSubGroups(System.Data.SqlClient.SortOrder.Ascending);

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
				ItemDataTable.Columns.Add("OrderSlipPrinter");
				ItemDataTable.Columns.Add("OrderSlipPrinted");
				ItemDataTable.Columns.Add("PercentageCommision");
				ItemDataTable.Columns.Add("Commision");
				ItemDataTable.Columns.Add("PaxNo");

				dgItems.DataSource = ItemDataTable;
				SetGridItemsWidth();

				mboIsInTransaction = false;
				mboIsItemHeaderPrinted = false;

				StartMarqueeThread();
				Cursor.Current = Cursors.Default;

				//dgItems.Height = 363;

				clsEvent.AddEventLn("Done!");

			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); }
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

					DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangeQuantity);

					if (loginresult == DialogResult.None)
					{
						LogInWnd login = new LogInWnd();

						login.AccessType = AccessTypes.ChangeQuantity;
						login.Header = "Change Quantity";
						login.ShowDialog(this);
						loginresult = login.Result;
						login.Close();
						login.Dispose();
					}

					if (loginresult == DialogResult.OK)
					{
						Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

						decimal oldQuantity = Details.Quantity;
						ChangeQuantityWnd QtyWnd = new ChangeQuantityWnd();
						QtyWnd.Details = Details;

						QtyWnd.ShowDialog(this);
						DialogResult result = QtyWnd.Result;
						Details = QtyWnd.Details;

						QtyWnd.Close();
						QtyWnd.Dispose();

						if (result == DialogResult.OK && oldQuantity != Details.Quantity)
						{
							Data.Products clsProduct = new Data.Products();
							clsProduct.GetConnection();

							if (mboIsRefund == false )
							{
								if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
								{
									if (Details.TransactionItemStatus != TransactionItemStatus.Return)
									{
										// Aug 3, 2011 : Lemu
										// Check the ProductVariationMatrixCurrentQuantity currenct quantity if sufficient for 
										if (Details.VariationsMatrixID != 0)
										{
											Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(clsProduct.Connection, clsProduct.Transaction);
											decimal decProductVariationMatrixCurrentQuantity = clsProductVariationsMatrix.BaseDetailsByMatrixID(Details.VariationsMatrixID).Quantity + oldQuantity;
											if (decProductVariationMatrixCurrentQuantity < Details.Quantity && mclsTerminalDetails.ShowItemMoreThanZeroQty == true)
											{
												clsProduct.CommitAndDispose();
												MessageBox.Show("Sorry the quantity you entered is greater than the current stock of Variation: " + Details.MatrixDescription + Environment.NewLine + "Current Stock: " + decProductVariationMatrixCurrentQuantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
												return;
											}
										}
										else
										{
											decimal decProductCurrentQuantity = clsProduct.Details(Details.ProductID).Quantity + oldQuantity;
											if (decProductCurrentQuantity < Details.Quantity && mclsTerminalDetails.ShowItemMoreThanZeroQty == true)
											{
												clsProduct.CommitAndDispose();
												MessageBox.Show("Sorry the quantity you entered is greater than the current stock. " + Environment.NewLine + "Current Stock: " + decProductCurrentQuantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
												return;
											}
										}
									}
								}
							}

							if (mboIsRefund == false && mclsTerminalDetails.ReservedAndCommit == true)
							{
								if (Details.TransactionItemStatus == TransactionItemStatus.Return)
								{
									Data.ProductUnit clsProductUnit = new Data.ProductUnit(clsProduct.Connection, clsProduct.Transaction);
									decimal decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, oldQuantity);

									clsProduct.SubtractQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_QTY_RESERVE_AND_COMMIT_RETURN_ITEM), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
								}
								else 
								{
									Data.ProductUnit clsProductUnit = new Data.ProductUnit(clsProduct.Connection, clsProduct.Transaction);
									decimal decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, oldQuantity);

									clsProduct.AddQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_RESERVE_AND_COMMIT_CHANGE_QTY), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
									// remove the ff codes for a change in Jul 26, 2011
									// clsProduct.AddQuantity(Details.ProductID, decNewQuantity);
									// if (Details.VariationsMatrixID != 0)
									// {
									//     Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(clsProduct.Connection, clsProduct.Transaction);
									//     clsProductVariationsMatrix.AddQuantity(Details.VariationsMatrixID, decNewQuantity);
									// }
								}
							}

							//mbodgItemRowClick = true;

							if (mboIsRefund)
								ApplyChangeQuantityPriceAmountDetails(clsProduct.Connection, clsProduct.Transaction, iRow, Details);
							else
							{
								Details = ApplyPromo(Details);

								ApplyChangeQuantityPriceAmountDetails(clsProduct.Connection, clsProduct.Transaction, iRow, Details);

								System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
								for (int x = iRow + 1; x < dt.Rows.Count; x++)
								{
									dgItems.CurrentRowIndex = x;
									Details = getCurrentRowItemDetails();

									System.Data.DataRow dr = dt.Rows[x];
									if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
									{
										Details = ApplyPromo(Details);
										ApplyChangeQuantityPriceAmountDetails(clsProduct.Connection, clsProduct.Transaction, x, Details);
									}

								}

								dgItems.CurrentRowIndex = iOldRow;
								dgItems.Select(iOldRow);
							}

							// Added May 7, 2011 to Cater Reserved and Commit functionality    
							// Details.Quantity = -oldQuantity + Details.Quantity;
							// Jul 26, 2011 Change the AddQuantity and SubtractQuantity
							ReservedAndCommitItem(clsProduct.Connection, clsProduct.Transaction, Details, Details.TransactionItemStatus);

							clsProduct.CommitAndDispose();
							//mbodgItemRowClick = false;

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

					DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangePrice);

					if (loginresult == DialogResult.None)
					{
						LogInWnd login = new LogInWnd();

						login.AccessType = AccessTypes.ChangePrice;
						login.Header = "Change Price";
						login.ShowDialog(this);
						loginresult = login.Result;
						login.Close();
						login.Dispose();
					}

					if (loginresult == DialogResult.OK)
					{
						Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

						decimal oldPrice = Details.Price;
						ChangePriceWnd PriceWnd = new ChangePriceWnd();
						PriceWnd.Details = Details;

						PriceWnd.ShowDialog(this);
						DialogResult result = PriceWnd.Result;
						Details = PriceWnd.Details;

						PriceWnd.Close();
						PriceWnd.Dispose();

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
							}
							Details = getCurrentRowItemDetails();
							DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
							DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
							InsertAuditLog(AccessTypes.ChangePrice, "Change price for item " + Details.ProductCode + " to " + Details.Price.ToString("#,##0.#0"));
						}

						//mbodgItemRowClick = false;
						Cursor.Current = Cursors.Default;
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
					DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangePrice);

					if (loginresult == DialogResult.None)
					{
						LogInWnd login = new LogInWnd();

						login.AccessType = AccessTypes.ChangePrice;
						login.Header = "Change Amount";
						login.ShowDialog(this);
						loginresult = login.Result;
						login.Close();
						login.Dispose();
					}

					if (loginresult == DialogResult.OK)
					{
						//mbodgItemRowClick = true;

						Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

						decimal oldAmount = Details.Amount;
						ChangeAmountWnd AmountWnd = new ChangeAmountWnd();
						AmountWnd.Details = Details;

						AmountWnd.ShowDialog(this);
						DialogResult result = AmountWnd.Result;
						Details = AmountWnd.Details;

						AmountWnd.Close();
						AmountWnd.Dispose();

						if (result == DialogResult.OK && oldAmount != Details.Amount)
						{
							Cursor.Current = Cursors.WaitCursor;
							if (mboIsRefund)
								ApplyChangeQuantityPriceAmountDetails(iRow, Details);
							else
							{
								Details = ApplyPromo(Details);

								ApplyChangeQuantityPriceAmountDetails(iRow, Details);

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
							}
							Details = getCurrentRowItemDetails();
							DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
							DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
							InsertAuditLog(AccessTypes.ChangePrice, "Change amount for item " + Details.ProductCode + " to " + Details.Amount.ToString("#,##0.#0"));
						}
						//mbodgItemRowClick = false;
						Cursor.Current = Cursors.Default;
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
		private void ApplyChangeQuantityPriceAmountDetails(int iRow, Data.SalesTransactionItemDetails Details)
		{
			System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

			dr = setCurrentRowItemDetails(dr, Details);

			ComputeSubTotal();
			mclsTransactionStream.AddItem(Details, mclsSalesTransactionDetails.TransactionDate);

			Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);

			//mclsSalesTransactionDetails.SubTotal = mclsSalesTransactionDetails.SubTotal;
			/*******Added: January 18, 2008***********************
			 * update purchase amount everytime there a change in 
			 *  Quantity
			 *  Price
			 *  Amount *********************************/
			Details.PurchaseAmount = Details.Quantity * Details.PurchasePrice;
			dr["PurchaseAmount"] = Details.PurchaseAmount;

			Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
			clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, Details);
			clsSalesTransactions.CommitAndDispose();

		}
		private void ApplyChangeQuantityPriceAmountDetails(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, int iRow, Data.SalesTransactionItemDetails Details)
		{
			System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

			dr = setCurrentRowItemDetails(dr, Details);

			ComputeSubTotal();
			mclsTransactionStream.AddItem(Details, mclsSalesTransactionDetails.TransactionDate);

			Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);

			//mclsSalesTransactionDetails.SubTotal = mclsSalesTransactionDetails.SubTotal;
			/*******Added: January 18, 2008***********************
			 * update purchase amount everytime there a change in 
			 *  Quantity
			 *  Price
			 *  Amount *********************************/
			Details.PurchaseAmount = Details.Quantity * Details.PurchasePrice;

			Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(pvtConnection, pvtTransaction);
			clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, Details);
		}
		private void ReturnItem()
		{
			if (mboIsRefund)
				return;

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ReturnItem);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.ReturnItem;
				login.Header = "Return Item Access";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
				clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
				clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
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
					TransactionReturnItemSelectWnd ItemWnd = new TransactionReturnItemSelectWnd();

					ItemWnd.TransactionNo = strTransactionNo;
					ItemWnd.TerminalNo = strTerminalNo;
					ItemWnd.ShowDialog(this);
					if (ItemWnd.Result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						if (!mboIsInTransaction)
						{
							lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
							if (!this.CreateTransaction()) return;
						}

						Data.SalesTransactionItemDetails details = new Data.SalesTransactionItemDetails();
						details = ItemWnd.Details;

						details.TransactionItemStatus = TransactionItemStatus.Return;

						System.Data.DataRow dr = ItemDataTable.NewRow();

						details.TransactionItemStatus = TransactionItemStatus.Return;
						details.ItemNo = Convert.ToString(ItemDataTable.Rows.Count + 1);

						dr = setCurrentRowItemDetails(dr, details);

						mclsTransactionStream.AddItem(details, mclsSalesTransactionDetails.TransactionDate);

						Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
						clsSalesTransactions.GetConnection();

						details.TransactionItemsID = AddItemToDB(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, details);
						dr["TransactionItemsID"] = details.TransactionItemsID.ToString();
						
						// Added May 7, 2011 to Cater Reserved and Commit functionality    
						ReservedAndCommitItem(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, details, details.TransactionItemStatus);

						ItemDataTable.Rows.Add(dr);

						dgItems.CurrentRowIndex = ItemDataTable.Rows.Count;
						dgItems.Select(dgItems.CurrentRowIndex);
						ComputeSubTotal();

						clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks);
						
						InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.RefundTransaction, "Return Item " + details.ProductCode + ".");
						
						clsSalesTransactions.CommitAndDispose();
						try
						{
							DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
							DisplayItemToTurretDel.BeginInvoke("RET-" + details.ProductCode, details.ProductUnitCode, details.Quantity, details.Price, details.Discount, details.PromoApplied, details.Amount, details.VAT, details.EVAT, null, null);
						}
						catch { }
						if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
						{
							PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
							string strProductCode = details.ProductCode;
							if (details.MatrixDescription != string.Empty && details.MatrixDescription != null) strProductCode += "-" + details.MatrixDescription;
							PrintItemDel.BeginInvoke(details.ItemNo, strProductCode + " - RET ", details.ProductUnitCode, details.Quantity, details.Price, details.Discount, details.PromoApplied, details.Amount, details.VAT, details.EVAT, null, null);
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
					DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyItemDiscount);

					if (loginresult == DialogResult.None)
					{
						LogInWnd login = new LogInWnd();

						login.AccessType = AccessTypes.ApplyItemDiscount;
						login.Header = "Apply Item Discount";
						login.ShowDialog(this);
						loginresult = login.Result;
						login.Close();
						login.Dispose();
					}
					if (loginresult == DialogResult.OK)
					{
						try
						{
							Data.SalesTransactionItemDetails clsItemDetails;
							clsItemDetails = getCurrentRowItemDetails();
							clsEvent.AddEvent("[" + lblCashier.Text + "] Applying item discount for item. no. [" + clsItemDetails.TransactionItemsID + "]" + clsItemDetails.ProductCode);

						Back:
							DiscountWnd discount = new DiscountWnd();
							discount.BalanceAmount = clsItemDetails.Amount;
							discount.DiscountType = clsItemDetails.ItemDiscountType;
							discount.DiscountAmount = clsItemDetails.ItemDiscount;
							discount.DiscountCode = clsItemDetails.DiscountCode;
							discount.Remarks = clsItemDetails.DiscountRemarks;
							discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
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
								Cursor.Current = Cursors.WaitCursor;
								clsItemDetails.ItemDiscount = DiscountAmount;
								clsItemDetails.Discount = clsItemDetails.ItemDiscount;
								clsItemDetails.ItemDiscountType = itemDiscountType;
								clsItemDetails.DiscountCode = ItemDiscountCode;
								clsItemDetails.DiscountRemarks = ItemDiscountRemarks;

								if (clsItemDetails.ItemDiscountType == DiscountTypes.Percentage)
								{
									clsItemDetails.Discount = (clsItemDetails.Amount * (clsItemDetails.ItemDiscount / 100));
								}

								if (clsItemDetails.Discount >= clsItemDetails.Amount)
								{
									MessageBox.Show("Sorry the input discount will yield a less than ZERO amount. Please type another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
									goto Back;
								}

								clsEvent.AddEventLn("discount=" + clsItemDetails.Discount.ToString("#,###.#0"));

								clsItemDetails.Amount = clsItemDetails.Amount - clsItemDetails.Discount - clsItemDetails.PromoApplied;
								clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
								System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

								dr = setCurrentRowItemDetails(dr, clsItemDetails);

								ComputeSubTotal();

								if (mclsSalesTransactionDetails.DiscountableAmount==0)
								{
									mclsSalesTransactionDetails.TransDiscountType = DiscountTypes.NotApplicable;
									mclsSalesTransactionDetails.TransDiscount = 0;
									ComputeSubTotal();
								}

								Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
								mclsTransactionStream.AddItem(clsItemDetails, mclsSalesTransactionDetails.TransactionDate);

								/*******Added: April 12, 2010***********************
								 * update purchase amount everytime there a change in 
								 *  
								 *  discount *********************************/
								clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
								dr["PurchaseAmount"] = clsItemDetails.PurchaseAmount;

								clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, clsItemDetails);

								InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.Discounts, "Apply item discount for " + clsItemDetails.ProductCode + ". discount=" + clsItemDetails.Discount.ToString("#,###.#0"));

								clsSalesTransactions.CommitAndDispose();

								clsEvent.AddEventLn("Done applying item discount...", true);

								DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
								DisplayItemToTurretDel.BeginInvoke(clsItemDetails.Description, clsItemDetails.ProductUnitCode, clsItemDetails.Quantity, clsItemDetails.Price, clsItemDetails.Discount, clsItemDetails.PromoApplied, clsItemDetails.Amount, clsItemDetails.VAT, clsItemDetails.EVAT, null, null);

							}
							else { clsEvent.AddEventLn("Cancelled!"); }
						}
						catch (Exception ex)
						{ clsEvent.AddErrorEventLn(ex); }

						Cursor.Current = Cursors.Default;
					}
				}
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

				DialogResult result; Data.ContactDetails details;
				TableSelectWnd clsTableSelectWnd = new TableSelectWnd();
				clsTableSelectWnd.TerminalDetails = mclsTerminalDetails;
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
							LoadContact(ContactGroupCategory.CUSTOMER, details);
							return;
						}
						else if (mclsSalesTransactionDetails.CustomerID == details.ContactID)
						{ return; }
						else if (mclsSalesTransactionDetails.CustomerID != details.ContactID)
						{ this.SuspendTransaction(false); }
					}

					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					string stTransactionNo = clsSalesTransactions.getSuspendedTransactionNo(details.ContactID, mclsTerminalDetails.TerminalNo, Constants.TerminalBranchID);

					if (stTransactionNo != string.Empty)
					{
						LoadTransaction(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, stTransactionNo, mclsTerminalDetails.TerminalNo);
						clsSalesTransactions.CommitAndDispose();
					}
					else
					{
						clsSalesTransactions.CommitAndDispose();

						this.LoadOptions();
						LoadContact(ContactGroupCategory.CUSTOMER, details);
					}
				}
				else { 
					//if (strTransactionNo != string.Empty)
					//{
					//    Data.SalesTransactions clsSalesTransactions1 = new Data.SalesTransactions();
					//    LoadTransaction(clsSalesTransactions1.Connection, clsSalesTransactions1.Transaction, strTransactionNo, mclsTerminalDetails.TerminalNo);
					//    clsSalesTransactions1.CommitAndDispose();
					//}
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
				clsTableSelectWnd.ShowAvailableTableOnly = mboIsInTransaction;
				clsTableSelectWnd.ShowDialog(this);
				details = clsTableSelectWnd.Details;
				result = clsTableSelectWnd.Result;
				clsTableSelectWnd.Close();
				clsTableSelectWnd.Dispose();

				if (result == DialogResult.OK)
				{
					LoadContact(enumContactGroupCategory, details);
					
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
				if (mclsTerminalDetails.ShowCustomerSelection)
					mclsContactDetails = pContactDetails;
				else
				{
					string strContactCardNo = txtBarCode.Text.Replace(Constants.SWIPE_REWARD_CARD, "").Trim();

					Data.Contacts clsContact = new Data.Contacts();
					// check using reward card info
					mclsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
					if (mclsContactDetails.ContactID == 0) 
					{
						// check using credit card info
						mclsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
						if (mclsContactDetails.ContactID == 0) 
						{
							strContactCardNo = strContactCardNo.Remove(strContactCardNo.Length - 1);
							// check using reward card info
							mclsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
							if (mclsContactDetails.ContactID == 0)
							{
								// check using credit card info
								mclsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
								if (mclsContactDetails.ContactID == 0)
								{ clsContact.CommitAndDispose(); SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER); return;}
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
							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
							clsSalesTransactions.UpdateContact(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsContactDetails);
							clsSalesTransactions.CommitAndDispose();

							mclsSalesTransactionDetails.CustomerID = mclsContactDetails.ContactID;
							mclsSalesTransactionDetails.CustomerName = mclsContactDetails.ContactName;
							mclsSalesTransactionDetails.RewardCardActive = mclsContactDetails.RewardDetails.RewardActive;
							mclsSalesTransactionDetails.RewardCardNo = mclsContactDetails.RewardDetails.RewardCardNo;
							mclsSalesTransactionDetails.RewardPreviousPoints = mclsContactDetails.RewardDetails.RewardPoints;
							mclsSalesTransactionDetails.RewardCurrentPoints = mclsSalesTransactionDetails.RewardPreviousPoints;
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
							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
							clsSalesTransactions.UpdateAgent(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsContactDetails);
							clsSalesTransactions.CommitAndDispose();

							mclsSalesTransactionDetails.AgentID = mclsContactDetails.ContactID;
							mclsSalesTransactionDetails.AgentName = mclsContactDetails.ContactName;
						}
						break;
				}
			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); }
		}
		private bool SuspendTransaction(bool ShowNotificationWindow)
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.SuspendTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.SuspendTransaction;
				login.Header = "Suspend Transaction No. " + lblTransNo.Text;
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				{
					try
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Suspending transaction no. " + lblTransNo.Text);

						DialogResult result; Data.ContactDetails details;
						TableSelectWnd clsTableSelectWnd = new TableSelectWnd();
						clsTableSelectWnd.TerminalDetails = mclsTerminalDetails;
						clsTableSelectWnd.ShowAvailableTableOnly = mboIsInTransaction;
						clsTableSelectWnd.ShowDialog(this);
						details = clsTableSelectWnd.Details;
						result = clsTableSelectWnd.Result;
						clsTableSelectWnd.Close();
						clsTableSelectWnd.Dispose();

						if (result == DialogResult.OK)
						{
							if (details.ContactID == Constants.C_RETAILPLUS_CUSTOMERID) return false;

							lblCustomer.Text = details.ContactName;
							lblCustomer.Tag = details.ContactID.ToString();

							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
							clsSalesTransactions.Suspend(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, details);

							InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.SuspendTransaction, "Suspend transaction #: " + lblTransNo.Text);

							clsSalesTransactions.CommitAndDispose();

							mclsTransactionStream.WriteLine("The above transaction is void. REASON: transaction has been suspended.", mclsSalesTransactionDetails.TransactionDate);
							mclsTransactionStream.CommitAndDispose(mclsSalesTransactionDetails.TransactionDate);

							if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
								PrintReportFooterSection(true, TransactionStatus.Suspended, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

							this.PrintOrderSlip(false);
							
							clsEvent.AddEventLn("Done!");

							if (ShowNotificationWindow) MessageBox.Show("Transaction has been SUSPENDED. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK);
							LoadOptions();
							boRetValue = true;
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

						Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
						clsSalesTransactions.Suspend(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal);

						InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.SuspendTransaction, "Suspend transaction #: " + lblTransNo.Text);

						clsSalesTransactions.CommitAndDispose();

						mclsTransactionStream.WriteLine("The above transaction is void. REASON: transaction has been suspended.", mclsSalesTransactionDetails.TransactionDate);
						mclsTransactionStream.CommitAndDispose(mclsSalesTransactionDetails.TransactionDate);

						if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
							PrintReportFooterSection(true, TransactionStatus.Suspended, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

						clsEvent.AddEventLn("Done!");

						if (ShowNotificationWindow) MessageBox.Show("Transaction has been SUSPENDED. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
						LoadOptions();
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

				WaiterSelectWnd WaiterWnd = new WaiterSelectWnd();
				WaiterWnd.ShowDialog(this);
				long iWaiterID = WaiterWnd.getWaiterID;
				string stWaiterName = WaiterWnd.getWaiterName;
				DialogResult result = WaiterWnd.Result;
				WaiterWnd.Close();
				WaiterWnd.Dispose();

				if (result == DialogResult.OK)
				{
					lblServedBy.Text = "Served by: " + stWaiterName;
					lblServedBy.Tag = iWaiterID.ToString();
					clsEvent.AddEventLn("Done! Selected Waiter: " + stWaiterName);

					if (mboIsInTransaction)
					{
						mclsSalesTransactionDetails.WaiterID = iWaiterID;
						mclsSalesTransactionDetails.WaiterName = stWaiterName;
						Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
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
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			if (mclsTerminalDetails.ShowOneTerminalSuspendedTransactions)
			{
				Data.SalesTransactions clsSales = new Data.SalesTransactions();
				int count = clsSales.CountSuspended(mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID);
				clsSales.CommitAndDispose();

				if (count == 0)
				{
					MessageBox.Show("No suspended transaction found for this day.", "RetailPlus", MessageBoxButtons.OK);
					return;
				}
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ResumeTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.ResumeTransaction;
				login.Header = "Resume Suspended Transaction";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				try
				{
					ResumeTransactionWnd ResumeWnd = new ResumeTransactionWnd();

					ResumeWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					ResumeWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
					ResumeWnd.ShowOnlyPackedTransactions = mclsTerminalDetails.ShowOnlyPackedTransactions;
					ResumeWnd.ShowOneTerminalSuspendedTransactions = mclsTerminalDetails.ShowOneTerminalSuspendedTransactions;
					ResumeWnd.ShowDialog(this);
					DialogResult result = ResumeWnd.Result;
					Data.SalesTransactionDetails details = ResumeWnd.Details;
					ResumeWnd.Close();
					ResumeWnd.Dispose();

					if (result == DialogResult.OK)
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Resuming transaction no. " + details.TransactionNo);

						mclsSalesTransactionDetails = details;

						if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Refund)
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

						lblTransDiscount.Tag = mclsSalesTransactionDetails.TransDiscountType.ToString("d");
						
						if (mclsSalesTransactionDetails.ChargeAmount == 0)
							lblTransCharge.Tag = ChargeTypes.NotApplicable.ToString("d");
						else
						{
							Data.ChargeType clsChargeType = new Data.ChargeType();
							byte bytelInPercent = clsChargeType.Details(mclsSalesTransactionDetails.ChargeCode).InPercent;
							clsChargeType.CommitAndDispose();

							if (bytelInPercent == 1)
							{
								lblTransCharge.Tag = ChargeTypes.Percentage.ToString("d");
							}
							else
							{
								lblTransCharge.Tag = ChargeTypes.FixedValue.ToString("d");
							}
						}

						// Aug 6, 2011 : Lemu
						// Put here from CloseTransaction
						try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
						catch { }
						mclsSalesTransactionDetails.CashierName = lblCashier.Text;

						//insert to logfile
						mclsTransactionStream.AddTransactionHeader(details, mclsSalesTransactionDetails.TransactionDate);

						LoadResumedItems(details.TransactionItems, false);

						mboIsInTransaction = true;

						InsertAuditLog(AccessTypes.ResumeTransaction, "Resume transaction #: " + lblTransNo.Text);
						clsEvent.AddEventLn("Done!");
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.VoidTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.VoidTransaction;
				login.Header = "Void Transaction No. " + lblTransNo.Text;
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Voiding transaction no. " + lblTransNo.Text, true);

					if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
						if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
							if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
								PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
						}
					}

					Cursor.Current = Cursors.WaitCursor;

					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					clsSalesTransactions.GetConnection();

					// Added May 7, 2011 to Cater Reserved and Commit functionality    
					#region Reserved And Commit
					if (mclsTerminalDetails.ReservedAndCommit == true)
					{
						System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
						for (int x = 0; x < dt.Rows.Count; x++)
						{
							dgItems.CurrentRowIndex = x;
							Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
							Details.TransactionItemStatus = TransactionItemStatus.Void;
							ReservedAndCommitItem(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, Details, Details.TransactionItemStatus);
						}
					}
					#endregion

					//UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
					UpdateTerminalReport(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, TransactionStatus.Void, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

					//UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
					UpdateCashierReport(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, TransactionStatus.Void, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

					clsSalesTransactions.Void(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);
					
					clsSalesTransactions.UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

					InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.VoidTransaction, "VOID transaction #:" + lblTransNo.Text);

					clsSalesTransactions.CommitAndDispose();

					mclsTransactionStream.WriteLine("The above transaction is void. REASON: transaction has been void.", mclsSalesTransactionDetails.TransactionDate);
					mclsTransactionStream.CommitAndDispose(mclsSalesTransactionDetails.TransactionDate);

					clsEvent.AddEventLn("Done transaction no. " + lblTransNo.Text + " has been void.", true);

					if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
					{
						PrintReportFooterSection(true, TransactionStatus.Void, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);
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
					
					MessageBox.Show("Transaction has been VOID. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.LoadOptions();
					Cursor.Current = Cursors.Default;
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
			}
		}
		private void WithHold()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Withhold);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.Withhold;
				login.Header = "WithHold Amount";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] WithHolding amount.");

					WithholdWnd frmWithHoldWnd = new WithholdWnd();
					frmWithHoldWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmWithHoldWnd.ShowDialog(this);
					DialogResult result = frmWithHoldWnd.Result;
					Data.WithHoldDetails clsWithHoldDetails = frmWithHoldWnd.WithholdDetails;
					frmWithHoldWnd.Close();
					frmWithHoldWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;

						Data.WithHold clsWithHold = new Data.WithHold();
						clsWithHold.Insert(clsWithHoldDetails);

						InsertAuditLog(clsWithHold.Connection, clsWithHold.Transaction, AccessTypes.Withhold, "WithHold payment: type='" + clsWithHoldDetails.PaymentType.ToString("G") + "' amount='" + clsWithHoldDetails.Amount.ToString(",##0.#0") + "'");

						clsWithHold.CommitAndDispose();

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
				{ clsEvent.AddErrorEventLn(ex); }
			}
		}
		private void Disburse()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Disburse);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.Disburse;
				login.Header = "Disburse Amount";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Disbursing amount.");

					DisburseWnd frmDisburseWnd = new DisburseWnd();
					frmDisburseWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmDisburseWnd.ShowDialog(this);
					DialogResult result = frmDisburseWnd.Result;
					Data.DisburseDetails clsDisburseDetails = frmDisburseWnd.DisburseDetails;
					frmDisburseWnd.Close();
					frmDisburseWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						
						Data.Disburse clsDisburse = new Data.Disburse();
						clsDisburse.Insert(clsDisburseDetails);

						InsertAuditLog(clsDisburse.Connection, clsDisburse.Transaction, AccessTypes.Disburse, "Disburse: type='" + clsDisburseDetails.PaymentType.ToString("G") + "' amount='" + clsDisburseDetails.Amount.ToString(",##0.#0") + "'");

						clsDisburse.CommitAndDispose();

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
				{ clsEvent.AddErrorEventLn(ex); }
			}
		}
		private void PaidOut()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PaidOut);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PaidOut;
				login.Header = "Paid-Out Amount";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing paid-out amount.");

					PaidOutWnd frmPaidOutWnd = new PaidOutWnd();
					frmPaidOutWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmPaidOutWnd.ShowDialog(this);
					DialogResult result = frmPaidOutWnd.Result;
					Data.PaidOutDetails clsPaidOutDetails = frmPaidOutWnd.PaidOutDetails;
					frmPaidOutWnd.Close();
					frmPaidOutWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						
						Data.PaidOut clsPaidOut = new Data.PaidOut();
						clsPaidOut.Insert(clsPaidOutDetails);

						InsertAuditLog(clsPaidOut.Connection, clsPaidOut.Transaction, AccessTypes.PaidOut, "Paid-out: type='" + clsPaidOutDetails.PaymentType.ToString("G") + "' amount='" + clsPaidOutDetails.Amount.ToString(",##0.#0") + "'");

						clsPaidOut.CommitAndDispose();

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
				{ clsEvent.AddErrorEventLn(ex); }
			}
		}
		private void Deposit()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.Deposit);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.Deposit;
				login.Header = "Disburse Amount";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Depositing amount.");

					DepositWnd frmDepositWnd = new DepositWnd();
					frmDepositWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmDepositWnd.ShowDialog(this);
					DialogResult result = frmDepositWnd.Result;
					Data.DepositDetails clsDepositDetails = frmDepositWnd.DepositDetails;
					frmDepositWnd.Close();
					frmDepositWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						
						Data.Deposit clsDeposit = new Data.Deposit();
						clsDeposit.Insert(clsDepositDetails);

						Data.Contacts clsContact = new Data.Contacts(clsDeposit.Connection, clsDeposit.Transaction);
						clsContact.AddDebit(clsDepositDetails.ContactID, clsDepositDetails.Amount);

						InsertAuditLog(clsDeposit.Connection, clsDeposit.Transaction, AccessTypes.Deposit, "Deposit: type='" + clsDepositDetails.PaymentType.ToString("G") + "' amount='" + clsDepositDetails.Amount.ToString(",##0.#0") + "'");

						clsDeposit.CommitAndDispose();

						PrintDepositDelegate printdepositDel = new PrintDepositDelegate(PrintDeposit);
						printdepositDel.BeginInvoke(clsDepositDetails, null, null);

						clsEvent.AddEventLn("Done! type=" + clsDepositDetails.PaymentType.ToString("G") + " amount=" + clsDepositDetails.Amount.ToString("#,###.#0"));

						Cursor.Current = Cursors.Default;
					}
					else { clsEvent.AddEventLn("Cancelled!"); }

				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
			}
		}
		private void ShowOtherCommands()
		{
 
		}
		private void ShowPrintWindow()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			// Dec 01, 2008      Lemuel E. Aceron
			// added the IsCashCountInitialized for 1 time 
			// Cash count every printing of report.
			if (mclsTerminalDetails.CashCountBeforeReport)
			{ if (!mboIsCashCountInitialized) { if (!CashCount()) return; } }

			ReportsWnd reports = new ReportsWnd();
			reports.CashierID = mclsSalesTransactionDetails.CashierID;
			reports.ShowDialog(this);
			DialogResult reportsresult = reports.Result;
			Keys KeyData = reports.KeyData;
			reports.Close();
			reports.Dispose();

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
					ReprintZRead();
					break;

				case Keys.F10:
					PrintPLUPerOrderSlipPrinter();
					break;

				case Keys.F11:
					ReprintDeliveryReceipt();
					break;
			}
		}
		private void ShowMallForwarder()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.MallForwarder);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.MallForwarder;
				login.Header = "Mall Data Forwarder";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("Mall Forwarder showing window to [" + lblCashier.Text + "]...", true);

					ForwarderWnd clsForwarderWnd = new ForwarderWnd();
					clsForwarderWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
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
						clsRLC.CreateAndTransferFile(pvtDateToProcess);
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
					DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.VoidItem);

					if (loginresult == DialogResult.None)
					{
						LogInWnd login = new LogInWnd();

						login.AccessType = AccessTypes.VoidItem;
						login.Header = "Void Item ";
						login.ShowDialog(this);
						loginresult = login.Result;
						login.Close();
						login.Dispose();
					}
					if (loginresult == DialogResult.OK)
					{
						Details = getCurrentRowItemDetails();

						if (mclsTerminalDetails.ItemVoidConfirmation)
						{
							if (MessageBox.Show("Are you sure you want to void item number: " + Details.ItemNo + " (" + Details.ProductCode +")?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
							{
								return;
							}
						}

						try
						{
							clsEvent.AddEvent("[" + lblCashier.Text + "] Voiding item no. " + Details.ItemNo + " :" + Details.Description + ".");

							// override the transaction item status
							TransactionItemStatus _previousTransactionItemStatus = Details.TransactionItemStatus;

							Details.TransactionItemStatus = TransactionItemStatus.Void;
							mclsTransactionStream.AddItem(Details, mclsSalesTransactionDetails.TransactionDate);

							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
							clsSalesTransactions.VoidItem(Details.TransactionItemsID, mclsSalesTransactionDetails.TransactionDate);

							// Added May 7, 2011 to Cater Reserved and Commit functionality    
							ReservedAndCommitItem(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, Details, _previousTransactionItemStatus);

							InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.VoidItem, "Voiding item #: " + Details.ItemNo + " :" + Details.Description + ".");

							clsSalesTransactions.CommitAndDispose();

							dgItems[iRow, 8] = "VOID";
							dgItems[iRow, 9] = "0.00";
							dgItems[iRow, 10] = "0.00";
							dgItems[iRow, 11] = "0.00";
							dgItems[iRow, 13] = "0.00";

							clsEvent.AddEventLn("Done!");
							
							ComputeSubTotal();

							try
							{
								DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
								DisplayItemToTurretDel.BeginInvoke("VOID-" + Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
							}
							catch { }
							if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
							{
								PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
								PrintItemDel.BeginInvoke(Details.ItemNo, Details.ProductCode + " - VOID ", Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
								//PrintItemDel.BeginInvoke(Details.ProductCode + " - VOID ", Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
							}

						}
						catch (Exception ex)
						{ clsEvent.AddErrorEventLn(ex); }
					}
				}
			}
		}
		private bool CashCount()
		{
			bool boRetValue = false;

			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return boRetValue; }
				else
					return boRetValue;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CashCount);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CashCount;
				login.Header = "Issue Cash Count";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing cash count.");

					CashCountWnd frmCashCountWnd = new CashCountWnd();
					frmCashCountWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmCashCountWnd.CashierName = lblCashier.Text;
					frmCashCountWnd.IsTouchScreen = mclsTerminalDetails.IsTouchScreen;
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
						InsertAuditLog(AccessTypes.CashCount, "Issue cash count. amount=" + Amount.ToString("#,###.#0"));
						clsEvent.AddEventLn("Done! amount=" + Amount.ToString("#,###.#0"));

						boRetValue = true;
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
			}
			return boRetValue;
		}
		private void Float()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterFloat);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.EnterFloat;
				login.Header = "Enter Float or Beginning Balance";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Entering beginning balance...", true);

					BalanceWnd clsBalanceWnd = new BalanceWnd();
					clsBalanceWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					clsBalanceWnd.ShowDialog(this);
					DialogResult balanceResult = clsBalanceWnd.Result;
					decimal Amount = clsBalanceWnd.Amount;
					clsBalanceWnd.Close();
					clsBalanceWnd.Dispose();

					if (balanceResult == DialogResult.OK)
					{
						OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
						Invoke(opendrawerDel);
						InsertAuditLog(AccessTypes.EnterFloat, "Issue beginning balance. amount=" + Amount.ToString("#,###.#0"));
						clsEvent.AddEventLn("Entering beginning balance Done! amount=" + Amount.ToString("#,###.#0"), true);
					}
					else { clsEvent.AddEventLn("Entering beginning balance cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
			}
		}
		private void InitializeZRead()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.InitializeZRead);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.InitializeZRead;
				login.Header = "Initialize Z-Read";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				Data.SalesTransactions clsSales = new Data.SalesTransactions();
				int count = clsSales.CountSuspended(mclsTerminalDetails.TerminalNo, 0, Constants.TerminalBranchID);
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
				if (MessageBox.Show("Warning!!! Z-Read will be initialized...Press OK to continue.", "RetailPlus", MessageBoxButtons.OKCancel , MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{
					Cursor.Current = Cursors.WaitCursor;

					try
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Initializing Beginning balance.",true);
						PrintZRead(true);

						Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
						clsTerminalReport.GetConnection();

						// Dec 01, 2008      Lemuel E. Aceron
						// added the IsCashCountInitialized for
						// 1 time Cash count every printing of report.
						Data.Terminal clsTerminal = new Data.Terminal(clsTerminalReport.Connection, clsTerminalReport.Transaction);
						clsTerminal.UpdateIsCashCountInitialized(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, false);

						//initialize Z-Read
						clsTerminalReport.InitializeZRead(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName);

						InsertAuditLog(clsTerminalReport.Connection, clsTerminalReport.Transaction, AccessTypes.InitializeZRead, "Initialize Z-Read.");

						DateTime dteMAXDateLastInitialized = DateTime.MinValue;

						// May 21, 2009      Lemuel E. Aceron
						// added the for auto FTP of file for RLC
						// get the maxdatelastinitialized
						if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
						{
							Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory(clsTerminalReport.Connection, clsTerminalReport.Transaction);
							dteMAXDateLastInitialized = clsTerminalReportHistory.MINDateLastInitialized(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo, DateTime.Now);
						}

						clsTerminalReport.CommitAndDispose();

						clsEvent.AddEventLn("Done!");

						MessageBox.Show("Z-Read has been initialized...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						// May 21, 2009      Lemuel E. Aceron
						// added the for auto FTP of file for RLC
						// send the data to RLC
						if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
							ProcessMallForwarder(dteMAXDateLastInitialized, true);

						LoggedOutCashier(false);
					}
					catch (Exception ex)
					{ clsEvent.AddErrorEventLn(ex); }

					Cursor.Current = Cursors.Default;
				}
			}
		}
		private void ReadBarCode()
		{
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CreateTransaction);
			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CreateTransaction;
				login.Header = "Create Transaction Access";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				//mbodgItemRowClick = false;
				if (txtBarCode.Text.Trim() != "" && txtBarCode.Text.Trim() != null)
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
					Data.Products clsProduct = new Data.Products();
					Data.ProductDetails clsProductDetails = clsProduct.Details(stBarcode);
					txtBarCode.Text = "";

					if (clsProductDetails.ProductID != 0)
					{
						if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
						{
							if (mboIsRefund == false)
							{
								if (clsProductDetails.Quantity < decQuantity && mclsTerminalDetails.ShowItemMoreThanZeroQty == true)
								{
									MessageBox.Show("Sorry the quantity you entered is greater than the current stock. " + Environment.NewLine + "Current Stock: " + clsProductDetails.Quantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									return;
								}
							}
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
						clsItemDetails.OrderSlipPrinter = clsProductDetails.OrderSlipPrinter;
						clsItemDetails.OrderSlipPrinted = false;
						clsItemDetails.PercentageCommision = clsProductDetails.PercentageCommision;
						clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);

						Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(clsProduct.Connection, clsProduct.Transaction);
						int variationsCtr = clsProductVariationsMatrix.CountVariations(clsItemDetails.ProductID);

						#region variationsCtr = 0
						if (variationsCtr == 0)
						{
							Data.ProductPackage clsProductPackage = new Data.ProductPackage(clsProduct.Connection, clsProduct.Transaction);
							Int32 ItemPackageCount = clsProductPackage.CountPackage(clsItemDetails.ProductID);

							if (ItemPackageCount == 0)
							{
								clsProduct.CommitAndDispose();
								MessageBox.Show("Sorry there is no Product Package for this item. " + Environment.NewLine +
												"Please advise you Inventory Officer to create at least one package." + Environment.NewLine +
													"Details are as follows: " + Environment.NewLine +
													"Product:" + clsItemDetails.Description, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							else if (ItemPackageCount == 1)
							{
								if (!mboIsInTransaction)
								{
									lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
									if (!this.CreateTransaction()) { clsProduct.CommitAndDispose(); return; }
								}

								Data.ProductPackageDetails clsPackageDetails = clsProductPackage.DetailsByProductID(clsItemDetails.ProductID);

								clsItemDetails.ProductPackageID = clsPackageDetails.PackageID;
								clsItemDetails.ProductUnitID = clsPackageDetails.UnitID;
								clsItemDetails.ProductUnitCode = clsPackageDetails.UnitCode;
								clsItemDetails.Price = clsPackageDetails.Price;
								clsItemDetails.PackageQuantity = clsPackageDetails.Quantity;
								clsItemDetails.VAT = clsPackageDetails.VAT;
								clsItemDetails.LocalTax = clsPackageDetails.LocalTax;
								clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
								clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
								clsItemDetails.PurchasePrice = clsPackageDetails.PurchasePrice;
								clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
							}
							else if (ItemPackageCount > 1)
							{
								DialogResult result = DialogResult.Cancel;
								Data.SalesTransactionItemDetails clsSalesTransactionItemDetails = clsItemDetails;

								if (mclsTerminalDetails.IsTouchScreen)
								{
									ItemPackageRestoWnd ItemPackgWnd = new ItemPackageRestoWnd();
									ItemPackgWnd.IsPriceInq = false;
									ItemPackgWnd.Details = clsItemDetails;
									ItemPackgWnd.ShowDialog(this);
									result = ItemPackgWnd.Result;
									clsSalesTransactionItemDetails = ItemPackgWnd.Details;
									ItemPackgWnd.Close();
									ItemPackgWnd.Dispose();
								}
								else
								{
									ItemPackageWnd ItemPackgWnd = new ItemPackageWnd();
									ItemPackgWnd.Details = clsItemDetails;
									ItemPackgWnd.ShowDialog(this);
									result = ItemPackgWnd.Result;
									clsSalesTransactionItemDetails = ItemPackgWnd.Details;
									ItemPackgWnd.Close();
									ItemPackgWnd.Dispose();
								}

								if (result == DialogResult.OK)
								{
									clsItemDetails = clsSalesTransactionItemDetails;

									if (!mboIsInTransaction)
									{
										lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
										if (!this.CreateTransaction()) { clsProduct.CommitAndDispose(); return; }
									}
								}
								else
								{
									clsProduct.CommitAndDispose();
									return;
								}
							}
						}
						#endregion
						#region variationsCtr = 1
						else if (variationsCtr == 1)
						{
							Data.ProductVariationsMatrix clsVariation = new Data.ProductVariationsMatrix(clsProduct.Connection, clsProduct.Transaction);
							Data.ProductBaseMatrixDetails clsProductBaseMatrixDetails = clsVariation.BaseDetails(clsItemDetails.ProductID);

							decimal decProductVariationMatrixCurrentQuantity = clsProductBaseMatrixDetails.Quantity;
							if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
							{
								if (decProductVariationMatrixCurrentQuantity < clsItemDetails.Quantity && mclsTerminalDetails.ShowItemMoreThanZeroQty == true)
								{
									clsProduct.CommitAndDispose();
									MessageBox.Show("Sorry the quantity you entered is greater than the current stock of Variation: " + clsItemDetails.MatrixDescription + Environment.NewLine + "Current Stock: " + decProductVariationMatrixCurrentQuantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									return;
								}
							}

							clsItemDetails.VariationsMatrixID = clsProductBaseMatrixDetails.MatrixID;
							clsItemDetails.MatrixDescription = clsProductBaseMatrixDetails.Description;
							clsItemDetails.Price = clsProductBaseMatrixDetails.Price;
							clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
							clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
							clsItemDetails.VAT = clsProductBaseMatrixDetails.VAT;
							clsItemDetails.LocalTax = clsProductBaseMatrixDetails.LocalTax;
							clsItemDetails.PurchasePrice = clsProductBaseMatrixDetails.PurchasePrice;
							clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
							clsItemDetails.IncludeInSubtotalDiscount = clsProductBaseMatrixDetails.IncludeInSubtotalDiscount;

							Data.MatrixPackage clsMatrixPackage = new Data.MatrixPackage(clsProduct.Connection, clsProduct.Transaction);
							Int32 MatrixPackageCount = clsMatrixPackage.CountPackage(clsItemDetails.VariationsMatrixID);
							if (MatrixPackageCount == 0)
							{
								clsProduct.CommitAndDispose();
								MessageBox.Show("Sorry there is no Product Variation Package for this item. " + Environment.NewLine +
												"Please advise you Inventory Officer to create at least one package." + Environment.NewLine +
													"Details are as follows: " + Environment.NewLine +
													"Product:" + clsItemDetails.Description + Environment.NewLine +
													"Variation: " + clsItemDetails.MatrixDescription, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							else if (MatrixPackageCount == 1)
							{
								if (!mboIsInTransaction)
								{
									lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
									if (!this.CreateTransaction()) { clsProduct.CommitAndDispose(); return; }
								}

								Data.MatrixPackageDetails clsMatrixPackageDetails = clsMatrixPackage.DetailsByMatrixID(clsItemDetails.VariationsMatrixID);

								clsItemDetails.MatrixPackageID = clsMatrixPackageDetails.PackageID;
								clsItemDetails.ProductUnitID = clsMatrixPackageDetails.UnitID;
								clsItemDetails.ProductUnitCode = clsMatrixPackageDetails.UnitCode;
								clsItemDetails.Price = clsMatrixPackageDetails.Price;
								clsItemDetails.PackageQuantity = clsMatrixPackageDetails.Quantity;
								clsItemDetails.VAT = clsMatrixPackageDetails.VAT;
								clsItemDetails.EVAT = clsMatrixPackageDetails.EVAT;
								clsItemDetails.LocalTax = clsMatrixPackageDetails.LocalTax;
								clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
								clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
								clsItemDetails.PurchasePrice = clsMatrixPackageDetails.PurchasePrice;
								clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
							}
							else if (MatrixPackageCount > 1)
							{
								DialogResult result = DialogResult.Cancel;
								Data.SalesTransactionItemDetails clsSalesTransactionItemDetails = clsItemDetails;

								if (mclsTerminalDetails.IsTouchScreen)
								{
									ItemMatrixPackageRestoWnd ItemMatrixPackgWnd = new ItemMatrixPackageRestoWnd();
									ItemMatrixPackgWnd.IsPriceInq = false;
									ItemMatrixPackgWnd.Details = clsItemDetails;
									ItemMatrixPackgWnd.ShowDialog(this);
									result = ItemMatrixPackgWnd.Result;
									clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
									ItemMatrixPackgWnd.Close();
									ItemMatrixPackgWnd.Dispose();
								}
								else
								{
									ItemMatrixPackageWnd ItemMatrixPackgWnd = new ItemMatrixPackageWnd();
									ItemMatrixPackgWnd.Details = clsItemDetails;
									ItemMatrixPackgWnd.ShowDialog(this);
									result = ItemMatrixPackgWnd.Result;
									clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
									ItemMatrixPackgWnd.Close();
									ItemMatrixPackgWnd.Dispose();
								}

								if (result == DialogResult.OK)
								{
									clsItemDetails = clsSalesTransactionItemDetails;

									if (!mboIsInTransaction)
									{
										lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
										if (!this.CreateTransaction()) { clsProduct.CommitAndDispose(); return; }
									}
								}
								else
								{
									clsProduct.CommitAndDispose();
									return;
								}
							}

						}
						#endregion
						#region variationsCtr > 1
						else if (variationsCtr > 1)
						{
						BackToVariation:
							DialogResult result = DialogResult.Cancel;
							Data.SalesTransactionItemDetails clsSalesTransactionItemDetails = clsItemDetails;

							if (mclsTerminalDetails.IsTouchScreen)
							{
								ItemVariationsRestoWnd ItemVarWnd = new ItemVariationsRestoWnd();
								ItemVarWnd.IsPriceInq = false;
								ItemVarWnd.Details = clsItemDetails;
								if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
									ItemVarWnd.ShowItemMoreThanZeroQty = false;
								else
									ItemVarWnd.ShowItemMoreThanZeroQty = mclsTerminalDetails.ShowItemMoreThanZeroQty;
								ItemVarWnd.ShowDialog(this);
								result = ItemVarWnd.Result;
								clsSalesTransactionItemDetails = ItemVarWnd.Details;
								ItemVarWnd.Close();
								ItemVarWnd.Dispose();
							}
							else
							{
								ItemVariationsWnd ItemVarWnd = new ItemVariationsWnd();
								ItemVarWnd.IsPriceInq = false;
								ItemVarWnd.Details = clsItemDetails;
								if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
									ItemVarWnd.ShowItemMoreThanZeroQty = false;
								else
									ItemVarWnd.ShowItemMoreThanZeroQty = mclsTerminalDetails.ShowItemMoreThanZeroQty;
								ItemVarWnd.ShowDialog(this);
								result = ItemVarWnd.Result;
								clsSalesTransactionItemDetails = ItemVarWnd.Details;
								ItemVarWnd.Close();
								ItemVarWnd.Dispose();
							}

							if (result == DialogResult.OK)
							{
								clsItemDetails = clsSalesTransactionItemDetails;

								decimal decProductVariationMatrixCurrentQuantity = clsProductVariationsMatrix.BaseDetailsByMatrixID(clsItemDetails.VariationsMatrixID).Quantity;
								if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
								{
									if (decProductVariationMatrixCurrentQuantity < clsItemDetails.Quantity && mclsTerminalDetails.ShowItemMoreThanZeroQty == true)
									{
										clsProduct.CommitAndDispose();
										MessageBox.Show("Sorry the quantity you entered is greater than the current stock of Variation: " + clsItemDetails.MatrixDescription + Environment.NewLine + "Current Stock: " + decProductVariationMatrixCurrentQuantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
										return;
									}
								}

								Data.MatrixPackage clsMatrixPackage = new Data.MatrixPackage(clsProduct.Connection, clsProduct.Transaction);
								Int32 MatrixPackageCount = clsMatrixPackage.CountPackage(clsItemDetails.VariationsMatrixID);
								if (MatrixPackageCount == 0)
								{
									clsProduct.CommitAndDispose();
									MessageBox.Show("Sorry there is no Product Variation Package for this item. " + Environment.NewLine + 
													"Please advise you Inventory Officer to create at least one package." + Environment.NewLine + 
														"Details are as follows: " + Environment.NewLine +
														"Product:" + clsItemDetails.Description + Environment.NewLine +
														"Variation: " + clsItemDetails.MatrixDescription, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									return;
								}
								else if (MatrixPackageCount == 1)
								{
									if (!mboIsInTransaction)
									{
										lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
										if (!this.CreateTransaction()) { clsProduct.CommitAndDispose(); return; }
									}

									Data.MatrixPackageDetails clsMatrixPackageDetails = clsMatrixPackage.DetailsByMatrixID(clsItemDetails.VariationsMatrixID);
									clsItemDetails.MatrixPackageID = clsMatrixPackageDetails.PackageID;
									clsItemDetails.ProductUnitID = clsMatrixPackageDetails.UnitID;
									clsItemDetails.ProductUnitCode = clsMatrixPackageDetails.UnitCode;
									clsItemDetails.Price = clsMatrixPackageDetails.Price;
									clsItemDetails.PackageQuantity = clsMatrixPackageDetails.Quantity;
									clsItemDetails.VAT = clsMatrixPackageDetails.VAT;
									clsItemDetails.EVAT = clsMatrixPackageDetails.EVAT;
									clsItemDetails.LocalTax = clsMatrixPackageDetails.LocalTax;
									clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
									clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
									clsItemDetails.PurchasePrice = clsMatrixPackageDetails.PurchasePrice;
									clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsMatrixPackageDetails.PurchasePrice;
								}
								else if (MatrixPackageCount > 1)
								{
									if (mclsTerminalDetails.IsTouchScreen)
									{
										ItemMatrixPackageRestoWnd ItemMatrixPackgWnd = new ItemMatrixPackageRestoWnd();
										ItemMatrixPackgWnd.IsPriceInq = false;
										ItemMatrixPackgWnd.Details = clsItemDetails;
										ItemMatrixPackgWnd.ShowDialog(this);
										result = ItemMatrixPackgWnd.Result;
										clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
										ItemMatrixPackgWnd.Close();
										ItemMatrixPackgWnd.Dispose();
									}
									else
									{
										ItemMatrixPackageWnd ItemMatrixPackgWnd = new ItemMatrixPackageWnd();
										ItemMatrixPackgWnd.Details = clsItemDetails;
										ItemMatrixPackgWnd.ShowDialog(this);
										result = ItemMatrixPackgWnd.Result;
										clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
										ItemMatrixPackgWnd.Close();
										ItemMatrixPackgWnd.Dispose();
									}

									if (result == DialogResult.OK)
									{
										clsItemDetails = clsSalesTransactionItemDetails;

										if (!mboIsInTransaction)
										{
											lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
											if (!this.CreateTransaction()) { clsProduct.CommitAndDispose(); return; }
										}
									}
									else
									{
										clsProduct.CommitAndDispose();
										return;
									}
								}

							}
							else
							{
								clsProduct.CommitAndDispose();
								return;
							}
							// Added June 1, 2010
							// Reload Variation Selection Window if WillContinueSelectionVariation
							if (mclsTerminalDetails.WillContinueSelectionVariation == true)
							{
								AddItem(clsProduct.Connection, clsProduct.Transaction, clsItemDetails);
								goto BackToVariation;
							}
						}
						#endregion
						AddItem(clsProduct.Connection, clsProduct.Transaction, clsItemDetails);
					}
					else
					{
						MessageBox.Show("Sorry the item is not yet entered in the system. Please select the generic code for this item.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					clsProduct.CommitAndDispose();
				}
			}
		}
		private void ShowProductPrice()
		{
			if (txtBarCode.Text.Trim() != "" && txtBarCode.Text.Trim() != null)
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
				Data.Products clsProduct = new Data.Products();
				Data.ProductDetails details = clsProduct.Details(stBarcode);
				clsProduct.CommitAndDispose();
				if (details.ProductID != 0)
				{
					Data.SalesTransactionItemDetails clsItemDetails = new Data.SalesTransactionItemDetails();

					clsItemDetails.ProductID = details.ProductID;
					clsItemDetails.ProductCode = details.ProductCode;
					clsItemDetails.BarCode = details.BarCode;
					clsItemDetails.Description = details.ProductDesc;
					clsItemDetails.ProductGroup = details.ProductGroupName;
					clsItemDetails.ProductSubGroup = details.ProductSubGroupName;
					clsItemDetails.TransactionItemStatus = TransactionItemStatus.Valid;
					clsItemDetails.ProductUnitID = details.BaseUnitID;
					clsItemDetails.ProductUnitCode = details.BaseUnitCode;
					clsItemDetails.Quantity = decQuantity;
					clsItemDetails.Price = details.Price;
					clsItemDetails.Discount = 0;
					clsItemDetails.ItemDiscount = 0;
					clsItemDetails.ItemDiscountType = DiscountTypes.NotApplicable;
					clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
					clsItemDetails.VAT = details.VAT;
					clsItemDetails.EVAT = details.EVAT;
					clsItemDetails.LocalTax = details.LocalTax;
					clsItemDetails.TransactionItemStatus = TransactionItemStatus.Valid;
					clsItemDetails.PurchasePrice = details.PurchasePrice;
					clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
					clsItemDetails.IncludeInSubtotalDiscount = details.IncludeInSubtotalDiscount;
					clsItemDetails.OrderSlipPrinter = details.OrderSlipPrinter;
					clsItemDetails.OrderSlipPrinted = false;
					clsItemDetails.PercentageCommision = details.PercentageCommision;
					clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);

					Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix();
					int variationsCtr = clsProductVariationsMatrix.CountVariations(clsItemDetails.ProductID);
					clsProductVariationsMatrix.CommitAndDispose();

					if (variationsCtr == 0)
					{
						Data.ProductPackage clsProductPackage = new Data.ProductPackage();
						Int32 ItemPackageCount = clsProductPackage.CountPackage(clsItemDetails.ProductID);

						if (ItemPackageCount == 1)
						{
							Data.ProductPackageDetails PackageDetails = clsProductPackage.DetailsByProductID(clsItemDetails.ProductID);
							clsProductPackage.CommitAndDispose();

							clsItemDetails.ProductPackageID = PackageDetails.PackageID;
							clsItemDetails.ProductUnitID = PackageDetails.UnitID;
							clsItemDetails.ProductUnitCode = PackageDetails.UnitCode;
							clsItemDetails.Price = PackageDetails.Price;
							clsItemDetails.PackageQuantity = PackageDetails.Quantity;
							clsItemDetails.VAT = PackageDetails.VAT;
							clsItemDetails.LocalTax = PackageDetails.LocalTax;
							clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
							clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
							clsItemDetails.PurchasePrice = PackageDetails.PurchasePrice;
							clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
						}
						else if (ItemPackageCount > 1)
						{
							clsProductPackage.CommitAndDispose();
							DialogResult result = DialogResult.Cancel;
							Data.SalesTransactionItemDetails clsSalesTransactionItemDetails = clsItemDetails;

							if (mclsTerminalDetails.IsTouchScreen)
							{
								ItemPackageRestoWnd ItemPackgWnd = new ItemPackageRestoWnd();
								ItemPackgWnd.IsPriceInq = true;
								ItemPackgWnd.Details = clsItemDetails;
								ItemPackgWnd.ShowDialog(this);
								result = ItemPackgWnd.Result;
								clsSalesTransactionItemDetails = ItemPackgWnd.Details;
								ItemPackgWnd.Close();
								ItemPackgWnd.Dispose();
							}
							else
							{
								ItemPackageWnd ItemPackgWnd = new ItemPackageWnd();
								ItemPackgWnd.Details = clsItemDetails;
								ItemPackgWnd.ShowDialog(this);
								result = ItemPackgWnd.Result;
								clsSalesTransactionItemDetails = ItemPackgWnd.Details;
								ItemPackgWnd.Close();
								ItemPackgWnd.Dispose();
							}

							if (result == DialogResult.OK)
							{
								clsItemDetails = clsSalesTransactionItemDetails;
							}
							else
							{
								txtBarCode.Text = "";
								return;
							}
						}
					}
					else if (variationsCtr == 1)
					{
						Data.ProductVariationsMatrix clsVariation = new Data.ProductVariationsMatrix();
						Data.ProductBaseMatrixDetails clsDetails = clsVariation.BaseDetails(clsItemDetails.ProductID);
						clsVariation.CommitAndDispose();

						clsItemDetails.VariationsMatrixID = clsDetails.MatrixID;
						clsItemDetails.MatrixDescription = clsDetails.Description;
						clsItemDetails.Price = Convert.ToDecimal(clsDetails.Price);
						clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
						clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
						clsItemDetails.VAT = details.VAT;
						clsItemDetails.LocalTax = details.LocalTax;
						clsItemDetails.PurchasePrice = clsDetails.PurchasePrice;
						clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;

						Data.MatrixPackage clsMatrixPackage = new Data.MatrixPackage();
						Int32 MatrixPackageCount = clsMatrixPackage.CountPackage(clsItemDetails.VariationsMatrixID);
						if (MatrixPackageCount == 1)
						{
							Data.MatrixPackageDetails clsMatrixPackageDetails = clsMatrixPackage.DetailsByMatrixID(clsItemDetails.VariationsMatrixID);
							clsMatrixPackage.CommitAndDispose();

							clsItemDetails.MatrixPackageID = clsMatrixPackageDetails.PackageID;
							clsItemDetails.ProductUnitID = clsMatrixPackageDetails.UnitID;
							clsItemDetails.ProductUnitCode = clsMatrixPackageDetails.UnitCode;
							clsItemDetails.Price = clsMatrixPackageDetails.Price;
							clsItemDetails.PackageQuantity = clsMatrixPackageDetails.Quantity;
							clsItemDetails.VAT = clsMatrixPackageDetails.VAT;
							clsItemDetails.EVAT = clsMatrixPackageDetails.EVAT;
							clsItemDetails.LocalTax = clsMatrixPackageDetails.LocalTax;
							clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
							clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
							//clsItemDetails.PurchasePrice = clsItemDetails.PurchasePrice;
							clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
						}
						else if (MatrixPackageCount > 1)
						{
							clsMatrixPackage.CommitAndDispose();
							DialogResult result = DialogResult.Cancel;
							Data.SalesTransactionItemDetails clsSalesTransactionItemDetails = clsItemDetails;

							if (mclsTerminalDetails.IsTouchScreen)
							{
								ItemMatrixPackageRestoWnd ItemMatrixPackgWnd = new ItemMatrixPackageRestoWnd();
								ItemMatrixPackgWnd.IsPriceInq = true;
								ItemMatrixPackgWnd.Details = clsItemDetails;
								ItemMatrixPackgWnd.ShowDialog(this);
								result = ItemMatrixPackgWnd.Result;
								clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
								ItemMatrixPackgWnd.Close();
								ItemMatrixPackgWnd.Dispose();
							}
							else
							{
								ItemMatrixPackageWnd ItemMatrixPackgWnd = new ItemMatrixPackageWnd();
								//ItemMatrixPackgWnd.IsPriceInq = true;
								ItemMatrixPackgWnd.Details = clsItemDetails;
								ItemMatrixPackgWnd.ShowDialog(this);
								result = ItemMatrixPackgWnd.Result;
								clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
								ItemMatrixPackgWnd.Close();
								ItemMatrixPackgWnd.Dispose();
							}

							if (result == DialogResult.OK)
							{
								clsItemDetails = clsSalesTransactionItemDetails;
							}
							else
							{
								txtBarCode.Text = "";
								return;
							}
						}

					}
					else if (variationsCtr > 1)
					{
						DialogResult result = DialogResult.Cancel;
						Data.SalesTransactionItemDetails clsSalesTransactionItemDetails = clsItemDetails;

						if (mclsTerminalDetails.IsTouchScreen)
						{
							ItemVariationsRestoWnd ItemVarWnd = new ItemVariationsRestoWnd();
							ItemVarWnd.IsPriceInq = true;
							ItemVarWnd.Details = clsItemDetails;
							ItemVarWnd.ShowItemMoreThanZeroQty = mclsTerminalDetails.ShowItemMoreThanZeroQty;
							ItemVarWnd.ShowDialog(this);
							result = ItemVarWnd.Result;
							clsSalesTransactionItemDetails = ItemVarWnd.Details;
							ItemVarWnd.Close();
							ItemVarWnd.Dispose();
						}
						else
						{
							ItemVariationsWnd ItemVarWnd = new ItemVariationsWnd();
							ItemVarWnd.IsPriceInq = false;
							ItemVarWnd.Details = clsItemDetails;
							ItemVarWnd.ShowItemMoreThanZeroQty = mclsTerminalDetails.ShowItemMoreThanZeroQty;
							ItemVarWnd.ShowDialog(this);
							result = ItemVarWnd.Result;
							clsSalesTransactionItemDetails = ItemVarWnd.Details;
							ItemVarWnd.Close();
							ItemVarWnd.Dispose();
						}

						if (result == DialogResult.OK)
						{
							clsItemDetails = clsSalesTransactionItemDetails;

							Data.MatrixPackage clsMatrixPackage = new Data.MatrixPackage();
							Int32 MatrixPackageCount = clsMatrixPackage.CountPackage(clsItemDetails.VariationsMatrixID);
							if (MatrixPackageCount == 1)
							{
								Data.MatrixPackageDetails clsMatrixPackageDetails = clsMatrixPackage.DetailsByMatrixID(clsItemDetails.VariationsMatrixID);
								clsItemDetails.MatrixPackageID = clsMatrixPackageDetails.PackageID;
								clsItemDetails.ProductUnitID = clsMatrixPackageDetails.UnitID;
								clsItemDetails.ProductUnitCode = clsMatrixPackageDetails.UnitCode;
								clsItemDetails.Price = clsMatrixPackageDetails.Price;
								clsItemDetails.PackageQuantity = clsMatrixPackageDetails.Quantity;
								clsItemDetails.VAT = clsMatrixPackageDetails.VAT;
								clsItemDetails.EVAT = clsMatrixPackageDetails.EVAT;
								clsItemDetails.LocalTax = clsMatrixPackageDetails.LocalTax;
								clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
								clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
								clsItemDetails.PurchasePrice = clsMatrixPackageDetails.PurchasePrice;
								clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;

								clsMatrixPackage.CommitAndDispose();
							}
							else if (MatrixPackageCount > 1)
							{
								clsMatrixPackage.CommitAndDispose();

								if (mclsTerminalDetails.IsTouchScreen)
								{
									ItemMatrixPackageRestoWnd ItemMatrixPackgWnd = new ItemMatrixPackageRestoWnd();
									ItemMatrixPackgWnd.IsPriceInq = true;
									ItemMatrixPackgWnd.Details = clsItemDetails;
									ItemMatrixPackgWnd.ShowDialog(this);
									result = ItemMatrixPackgWnd.Result;
									clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
									ItemMatrixPackgWnd.Close();
									ItemMatrixPackgWnd.Dispose();
								}
								else
								{
									ItemMatrixPackageWnd ItemMatrixPackgWnd = new ItemMatrixPackageWnd();
									ItemMatrixPackgWnd.Details = clsItemDetails;
									ItemMatrixPackgWnd.ShowDialog(this);
									result = ItemMatrixPackgWnd.Result;
									clsSalesTransactionItemDetails = ItemMatrixPackgWnd.Details;
									ItemMatrixPackgWnd.Close();
									ItemMatrixPackgWnd.Dispose();
								}

								if (result == DialogResult.OK)
								{
									clsItemDetails = clsSalesTransactionItemDetails;
								}
								else
								{
									txtBarCode.Text = "";
									return;
								}
							}
						}
						else
						{
							txtBarCode.Text = "";
							return;
						}
					}
				}
				txtBarCode.Text = "";
			}
		}
		private void SplitTransaction()
		{
 
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
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CloseTransaction;
				login.Header = "Close Transaction";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				if (mclsSalesTransactionDetails.SubTotal == 0)
				{
					if (MessageBox.Show("Are you sure you want to close this  ZERO amount transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
						return;
				}

				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Closing transaction no. " + lblTransNo.Text, true);

					//insert payment details
					PaymentsWnd payment = new PaymentsWnd();
					payment.SalesTransactionDetails = mclsSalesTransactionDetails;
					payment.CustomerDetails = mclsContactDetails;
					payment.Discount = mclsSalesTransactionDetails.Discount;
					payment.Charge = mclsSalesTransactionDetails.Charge;
					payment.SubTotal = mclsSalesTransactionDetails.SubTotal;
					payment.IsRefund = mboIsRefund;
					payment.CONFIG_TURRET_NAME = mclsTerminalDetails.TurretName;
					payment.TerminalDetails = mclsTerminalDetails;

					payment.ShowDialog(this);

					DialogResult paymentResult = payment.Result;

					decimal AmountPaid = payment.AmountPaid;
					decimal CashPayment = payment.CashPayment;
					decimal ChequePayment = payment.ChequePayment;
					decimal CreditCardPayment = payment.CreditCardPayment;
					decimal CreditPayment = payment.CreditPayment;
					decimal DebitPayment = payment.DebitPayment;
					
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

					payment.Close();
					payment.Dispose();

					if (paymentResult == DialogResult.OK)
					{
						/**
						 * Nov 04, 2011 : for payments using reward points
						 * */
						mclsSalesTransactionDetails.RewardPointsPayment = RewardPointsPayment;
						mclsSalesTransactionDetails.RewardConvertedPayment = RewardConvertedPayment;

						/**
						 * Oct 17, 2011 : Move this code here.
						 * check if will print transaction or not before opening any connection to database.
						 * */
						if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
						{
							if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
								mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;
						}

						// Mar 17, 2009
						// open drawer first before printing.
						OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
						Invoke(opendrawerDel);

						// start a connection for the database.
						//update the transaction table 
						Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
						clsSalesTransactions.GetConnection();

						mclsSalesTransactionDetails.AmountPaid = AmountPaid;
						mclsSalesTransactionDetails.ChangeAmount = ChangeAmount;

						Cursor.Current = Cursors.WaitCursor;
						SavePayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails, clsSalesTransactions.Connection, clsSalesTransactions.Transaction);

						//insert to log file
						ItemFooterDetails clsItemFooterDetails = new ItemFooterDetails();
						clsItemFooterDetails.Quantity = ItemDataTable.Rows.Count;
						clsItemFooterDetails.Price = mclsSalesTransactionDetails.SubTotal + mclsSalesTransactionDetails.Discount;
						clsItemFooterDetails.Discount = mclsSalesTransactionDetails.Discount;
						clsItemFooterDetails.ItemDiscount = mclsSalesTransactionDetails.ItemsDiscount;
						clsItemFooterDetails.Amount = mclsSalesTransactionDetails.SubTotal;
						mclsTransactionStream.AddItemFooter(clsItemFooterDetails, mclsSalesTransactionDetails.TransactionDate);

						FooterDetails clsFooterDetails = new FooterDetails();
						clsFooterDetails.SubTotal = mclsSalesTransactionDetails.SubTotal;
						clsFooterDetails.Discount = mclsSalesTransactionDetails.Discount;
						clsFooterDetails.VAT = mclsSalesTransactionDetails.VAT;
						clsFooterDetails.VatableAmount = mclsSalesTransactionDetails.VatableAmount;
						clsFooterDetails.EVAT = mclsSalesTransactionDetails.EVAT;
						clsFooterDetails.EVatableAmount = mclsSalesTransactionDetails.EVatableAmount;
						clsFooterDetails.LocalTax = mclsSalesTransactionDetails.LocalTax;
						clsFooterDetails.AmountPaid = AmountPaid;
						clsFooterDetails.BalanceAmount = BalanceAmount;
						clsFooterDetails.ChangeAmount = ChangeAmount;
						mclsTransactionStream.AddTransactionFooter(clsFooterDetails, mclsSalesTransactionDetails.TransactionDate);

						mclsTransactionStream.CommitAndDispose(mclsSalesTransactionDetails.TransactionDate);

						if (mboIsRefund)
						{
							clsSalesTransactions.UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);
							clsSalesTransactions.Refund(mclsSalesTransactionDetails.TransactionID, -mclsSalesTransactionDetails.SubTotal, -mclsSalesTransactionDetails.ItemsDiscount, -mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, -mclsSalesTransactionDetails.VAT, -mclsSalesTransactionDetails.VatableAmount, -mclsSalesTransactionDetails.EVAT, -mclsSalesTransactionDetails.EVatableAmount, -mclsSalesTransactionDetails.LocalTax, -mclsSalesTransactionDetails.AmountPaid, -CashPayment, -ChequePayment, -CreditCardPayment, -CreditPayment, -DebitPayment, -RewardPointsPayment, -RewardConvertedPayment, -BalanceAmount, -ChangeAmount, PaymentType, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, -mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.CashierID, lblCashier.Text);

							//UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
							UpdateTerminalReport(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, TransactionStatus.Refund, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

							//UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
							UpdateCashierReport(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, TransactionStatus.Refund, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

							if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
							{
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

										if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
											PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
									}
								}

								if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
									PrintReportFooterSection(true, TransactionStatus.Refund, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails );
							}

							Data.Products clsProduct = new Data.Products(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
							Data.ProductUnit clsProductUnit = new Data.ProductUnit(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
							Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);

							foreach (System.Data.DataRow dr in ItemDataTable.Rows)
							{
								Int64 lProductID = Convert.ToInt64(dr["ProductID"]);
								Int64 lVariationsMatrixID = Convert.ToInt64(dr["VariationsMatrixID"]);
								Int32 iProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
								decimal decQuantity = 0;
								decimal decPackageQuantity = 0;
								decimal decNewQuantity = 0;
								
								if (dr["Quantity"].ToString() != "VOID")
								{
									decQuantity = Convert.ToDecimal(dr["Quantity"]);
									decPackageQuantity = Convert.ToDecimal(dr["PackageQuantity"]);
									decNewQuantity = clsProductUnit.GetBaseUnitValue(lProductID, iProductUnitID, decQuantity * decPackageQuantity);

									// Sep 24, 2011      Lemuel E. Aceron
									// Added order slip wherein all punch items will not change sales and inventory
									// a customer named ORDER SLIP should be defined in contacts
									if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
									{
										// Added May 7, 2011 to Cater Reserved and Commit functionality    
										if (mclsTerminalDetails.ReservedAndCommit == false)
										{
											// Jul 26, 2011
											clsProduct.AddQuantity(Constants.TerminalBranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_REFUND_ITEM), mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);

											// remove the ff codes for a change in Jul 26, 2011
											// clsProduct.AddQuantity(lProductID, decNewQuantity);
											// if (lVariationsMatrixID != 0)
											//    clsProductVariationsMatrix.AddQuantity(lVariationsMatrixID, decNewQuantity);
											//
										}
									}
								}
							}
						}
						else
						{
							clsSalesTransactions.UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);
							clsSalesTransactions.Close(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, BalanceAmount, ChangeAmount, PaymentType, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);

							//UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
							UpdateTerminalReport(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, TransactionStatus.Closed, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

							//UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
							UpdateCashierReport(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, TransactionStatus.Closed, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

							Data.Products clsProduct = new Data.Products(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
							Data.ProductUnit clsProductUnit = new Data.ProductUnit(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
							Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);

							foreach (System.Data.DataRow dr in ItemDataTable.Rows)
							{
								long lProductID = Convert.ToInt64(dr["ProductID"]);
								long lVariationsMatrixID = Convert.ToInt64(dr["VariationsMatrixID"]);
								int iProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
								decimal decQuantity = 0;
								decimal decPackageQuantity = 0;
								decimal decNewQuantity = 0;
								decimal decPrice = Convert.ToDecimal(dr["Price"]);
								decimal decPurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]);

								if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
								{
									decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
									decPackageQuantity = Convert.ToDecimal(dr["PackageQuantity"]);
									decNewQuantity = clsProductUnit.GetBaseUnitValue(lProductID, iProductUnitID, decQuantity * decPackageQuantity);

									// Sep 24, 2011      Lemuel E. Aceron
									// Added order slip wherein all punch items will not change sales and inventory
									// a customer named ORDER SLIP should be defined in contacts
									if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
									{
										// Added May 7, 2011 to Cater Reserved and Commit functionality    
										if (mclsTerminalDetails.ReservedAndCommit == false)
										{
											// Jul 26, 2011
											clsProduct.AddQuantity(Constants.TerminalBranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_RETURN_ITEM), mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);

											// remove the ff codes for a change in Jul 26, 2011
											// clsProduct.AddQuantity(lProductID, decNewQuantity);
											// 
											// if (lVariationsMatrixID != 0)
											//     clsProductVariationsMatrix.AddQuantity(lVariationsMatrixID, decNewQuantity);
										}
									}
								}
								else if (dr["Quantity"].ToString() != "VOID")
								{
									decQuantity = Convert.ToDecimal(dr["Quantity"]);
									decPackageQuantity = Convert.ToDecimal(dr["PackageQuantity"]);
									decNewQuantity = clsProductUnit.GetBaseUnitValue(lProductID, iProductUnitID, decQuantity * decPackageQuantity);

									// Sep 24, 2011      Lemuel E. Aceron
									// Added order slip wherein all punch items will not change sales and inventory
									// a customer named ORDER SLIP should be defined in contacts
									if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
									{
										// Added May 7, 2011 to Cater Reserved and Commit functionality    
										if (mclsTerminalDetails.ReservedAndCommit == false)
										{
											// Jul 26, 2011
											clsProduct.SubtractQuantity(Constants.TerminalBranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_RETAIL) + " @ " + decPrice.ToString("#,##0.#0") + " Buying: " + decPurchasePrice.ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
											// remove the ff codes for a change in Jul 26, 2011
											// clsProduct.SubtractQuantity(lProductID, decNewQuantity);
											// 
											// if (lVariationsMatrixID != 0)
											//     clsProductVariationsMatrix.SubtractQuantity(lVariationsMatrixID, decNewQuantity);
										}
									}

									// Nov 1, 2011 : Lemu - disabled reward points if product is exempted 
									if (mclsSalesTransactionDetails.RewardCardActive)
									{
										if (mclsTerminalDetails.RewardPointsDetails.EnableRewardPoints == true)
										{
											if (dr["Barcode"].ToString() == Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE ||
												dr["Barcode"].ToString() == Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE ||
												dr["Barcode"].ToString() == Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE ||
												dr["Barcode"].ToString() == Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE ||
												dr["Barcode"].ToString() == Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE ||
												dr["Barcode"].ToString() == Data.Products.DEFAULT_SUPER_CARD_MEMBERSHIP_FEE_BARCODE ||
												dr["Barcode"].ToString() == Data.Products.DEFAULT_SUPER_CARD_RENEWAL_FEE_BARCODE ||
												dr["Barcode"].ToString() == Data.Products.DEFAULT_SUPER_CARD_REPLACEMENT_FEE_BARCODE)
												mclsTerminalDetails.RewardPointsDetails.EnableRewardPoints = false;
										}
									}
								}
							}

							// Oct 23, 2011 : Lemu - Added Reward Points
							
							if (mclsTerminalDetails.RewardPointsDetails.EnableRewardPoints && mclsSalesTransactionDetails.RewardCardActive && mclsTerminalDetails.RewardPointsDetails.RewardPointsMinimum <= mclsSalesTransactionDetails.AmountDue)
							{
								decimal decRewardPoints = 0;
								try
								{
									decRewardPoints = (mclsSalesTransactionDetails.AmountDue - mclsSalesTransactionDetails.RewardConvertedPayment) / mclsTerminalDetails.RewardPointsDetails.RewardPointsEvery * mclsTerminalDetails.RewardPointsDetails.RewardPoints;

									// round down points if RoundDown is enabled
									if (mclsTerminalDetails.RewardPointsDetails.RoundDownRewardPoints) decRewardPoints = decimal.Floor(decRewardPoints);
								}
								catch (System.DivideByZeroException ex) { }

								mclsSalesTransactionDetails.RewardEarnedPoints = decRewardPoints;
								mclsSalesTransactionDetails.RewardCurrentPoints = mclsSalesTransactionDetails.RewardPreviousPoints + mclsSalesTransactionDetails.RewardEarnedPoints;

								Data.ContactReward clsContactReward = new Data.ContactReward(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);    
								clsContactReward.AddPoints(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.RewardEarnedPoints);
								clsContactReward.AddPurchase(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.AmountDue);
								string strReason = "Purchase " + mclsSalesTransactionDetails.AmountDue.ToString("#,##0.#0") + " using Reward Card #: " + mclsSalesTransactionDetails.RewardCardNo;
								clsContactReward.AddMovement(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.RewardPreviousPoints, mclsSalesTransactionDetails.RewardEarnedPoints, mclsSalesTransactionDetails.RewardCurrentPoints, mclsSalesTransactionDetails.RewardCardExpiry, strReason, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName, mclsSalesTransactionDetails.TransactionNo);
							}

							if (mclsSalesTransactionDetails.RewardPointsPayment != 0)
							{
								// this should comes before earning of points otherwise this will be wrong.
								Data.ContactReward clsContactReward = new Data.ContactReward(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
								clsContactReward.DeductPoints(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.RewardPointsPayment);
								string strReason = "Redeemed " + mclsSalesTransactionDetails.RewardPointsPayment + " using Reward Card #: " + mclsSalesTransactionDetails.RewardCardNo;
								clsContactReward.AddMovement(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.RewardCurrentPoints, -mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardCurrentPoints - mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardCardExpiry, strReason, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName, mclsSalesTransactionDetails.TransactionNo);

								mclsSalesTransactionDetails.RewardPreviousPoints = mclsSalesTransactionDetails.RewardCurrentPoints;
								mclsSalesTransactionDetails.RewardCurrentPoints -= mclsSalesTransactionDetails.RewardPointsPayment;
								mclsSalesTransactionDetails.RewardEarnedPoints = 0;

								PrintRewardsRedemptionSlip();
							}

							/**
							 * print the transaction
							 * */
							
							if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
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
							//Added February 10, 2010
							else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
							{
								PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
							}
							//Added May 11, 2010
							else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
							{
								if (mclsSalesTransactionDetails.CashPayment != 0 || mclsSalesTransactionDetails.CreditCardPayment != 0)
									PrintSalesInvoice();
								if (mclsSalesTransactionDetails.ChequePayment != 0 || mclsSalesTransactionDetails.CreditPayment != 0)
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
								PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon);
							}
							else
							{
								if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
								{
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

										if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
											PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
									}
									if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
									{
										PrintReportFooterSection(true, TransactionStatus.Closed, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

										// Nov 05, 2011 : Print Charge Slip
										PrintChargeSlip(ChargeSlipType.Guarantor);
										PrintChargeSlip(ChargeSlipType.Original);
										
									}
								}
							}
						}

						InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.CloseTransaction, "Close transaction #: " + lblTransNo.Text + "... Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + CashPayment.ToString("#,###.#0") + " ChequePayment: " + ChequePayment.ToString("#,###.#0") + " CreditCardPayment: " + CreditCardPayment + " CreditPayment: " + CreditPayment.ToString("#,###.#0") + " DebitPayment: " + DebitPayment.ToString("#,###.#0") + " ChangeAmount: " + ChangeAmount.ToString("#,###.#0"));

						// commit the transactions
						clsSalesTransactions.CommitAndDispose();

						clsEvent.AddEventLn("Done! Transaction no. " + lblTransNo.Text + " has been closed. Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + CashPayment.ToString("#,###.#0") + " ChequePayment: " + ChequePayment.ToString("#,###.#0") + " CreditCardPayment: " + CreditCardPayment + " CreditPayment: " + CreditPayment.ToString("#,###.#0") + " DebitPayment: " + DebitPayment.ToString("#,###.#0") + " ChangeAmount: " + ChangeAmount.ToString("#,###.#0"), true);

						this.LoadOptions();
					}

				}
				catch (Exception ex)
				{
					clsEvent.AddErrorEventLn(ex);
				}
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
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CloseTransaction;
				login.Header = "Close Transaction";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Closing as order slip transaction no. " + lblTransNo.Text, true);


					// start a connection for the database.
					//update the transaction table 
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					clsSalesTransactions.CloseAsOrderSlip(mclsSalesTransactionDetails.TransactionID);

					InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.CloseTransaction, "Close transaction #: " + lblTransNo.Text + "... Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + 0.ToString("#,###.#0") + " ChequePayment: " + 0.ToString("#,###.#0") + " CreditCardPayment: " + 0 + " CreditPayment: " + 0.ToString("#,###.#0") + " DebitPayment: " + 0.ToString("#,###.#0") + " ChangeAmount: " + 0.ToString("#,###.#0"));

					// commit the transactions
					clsSalesTransactions.CommitAndDispose();

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
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CreateTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CreateTransaction;
				login.Header = "Create Transaction Access";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{

			BackToSelectionProduct:
				try
				{
					DialogResult result = DialogResult.Cancel;
					string strSelectedBarCode = string.Empty;

					if (mclsTerminalDetails.IsTouchScreen)
					{
						ItemSelectRestoWnd ItemRestoWnd = new ItemSelectRestoWnd();

						ItemRestoWnd.IsPriceInq = isPriceInq;
						ItemRestoWnd.ShowItemMoreThanZeroQty = mclsTerminalDetails.ShowItemMoreThanZeroQty;
						// Aug 6, 2011 : Lemu
						// Include InAvtive Products during REFUND
						if (mboIsRefund == true) ItemRestoWnd.ShowInActiveProducts = true;

						ItemRestoWnd.ShowDialog(this);
						result = ItemRestoWnd.Result;
						strSelectedBarCode = ItemRestoWnd.SelectedBarCode;
						ItemRestoWnd.Close();
						ItemRestoWnd.Dispose();
					}
					else
					{
						ItemSelectWnd ItemWnd = new ItemSelectWnd();

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
					}

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
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ReleaseItems);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.ReleaseItems;
				login.Header = "Release Items";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Releasing items...");

					ItemReleaseWnd _ItemReleaseWnd = new ItemReleaseWnd();
					_ItemReleaseWnd.ReleaserID = long.Parse(lblCashier.Tag.ToString());
					_ItemReleaseWnd.ReleaserName = lblCashier.Text;
					_ItemReleaseWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
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
				if (mboIsInTransaction)
				{
					if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
					{ if (!SuspendTransaction(true)) return DialogResult.Cancel; }
					else
						return DialogResult.Cancel;
				}

				DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.LogoutFE);

				if (loginresult == DialogResult.None)
				{
					LogInWnd login = new LogInWnd();

					login.AccessType = AccessTypes.LogoutFE;
					login.Header = "Logout";
					login.ShowDialog(this);
					loginresult = login.Result;
					login.Close();
					login.Dispose();
				}
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

						CashierLogs clsCashierLogs = new CashierLogs();
						clsCashierLogs.Logout(Convert.ToInt64(lblCashierName.Tag));

						InsertAuditLog(clsCashierLogs.Connection, clsCashierLogs.Transaction, AccessTypes.LogoutFE, "User logout.");

						clsCashierLogs.CommitAndDispose();
						
						clsEvent.AddEventLn("Done!");

						this.Lock();

						clsEvent.AddEventLn("System is now closed for any transaction!", true);
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
		private void ApplyTransDiscount()
		{
			int iRow = dgItems.CurrentRowIndex;
			if (iRow < 0) return;

			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot apply a transaction discount if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyTransDiscount);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.ApplyTransDiscount;
				login.Header = "Apply Transaction Discount";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Applying transaction discount for trans. no. " + lblTransNo.Text);

				Back:
					DiscountTypes TransDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), lblTransDiscount.Tag.ToString());
					DiscountWnd discount = new DiscountWnd();
					discount.BalanceAmount = mclsSalesTransactionDetails.SubTotal;
					discount.DiscountType = TransDiscountType;
					discount.DiscountAmount = mclsSalesTransactionDetails.TransDiscount;
					discount.DiscountCode = mclsSalesTransactionDetails.DiscountCode;
					discount.Remarks = mclsSalesTransactionDetails.DiscountRemarks;
					discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
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
							lblTransDiscount.Text = "";
						}
						lblTransDiscount.Tag = TransDiscountType.ToString("d");
						mclsSalesTransactionDetails.TransDiscountType = TransDiscountType;
						mclsSalesTransactionDetails.TransDiscount = DiscountAmount;
						mclsSalesTransactionDetails.DiscountCode = TransDiscountCode;
						mclsSalesTransactionDetails.DiscountRemarks = TransDiscountRemarks;

						ComputeSubTotal();

						if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
						{
							ChargeTypes TransChargeType = (ChargeTypes)Enum.Parse(typeof(ChargeTypes), lblTransCharge.Tag.ToString());
							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
							clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks);

							InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.Discounts, "Apply transaction discount for " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + ". Tran. #:" + lblTransNo.Text);

							clsSalesTransactions.CommitAndDispose();
						}
						else
						{
							MessageBox.Show("Sorry the input discount will yield a less than ZERO amount. Please type another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
							mclsSalesTransactionDetails.TransDiscount = OldDiscount;
							lblTransDiscount.Tag = OldTransDiscountType;

							ComputeSubTotal();
							goto Back;
						}
						
						clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Discount.ToString("#,###.#0"));
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ChargeType);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.ChargeType;
				login.Header = "Apply Transaction Charge";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Applying transaction Charge for trans. no. " + lblTransNo.Text);

				BackToCharge:
					ChargeTypes TransChargeType = (ChargeTypes)Enum.Parse(typeof(ChargeTypes), lblTransCharge.Tag.ToString());
					ChargeWnd charge = new ChargeWnd();
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
						string OldTransChargeType = lblTransCharge.Tag.ToString();

						if (TransChargeType == ChargeTypes.NotApplicable)
						{
							lblTransCharge.Text = "";
						}
						mclsSalesTransactionDetails.ChargeAmount = ChargeAmount;
						lblTransCharge.Tag = TransChargeType.ToString("d");
						mclsSalesTransactionDetails.ChargeCode = TransChargeCode;
						mclsSalesTransactionDetails.ChargeRemarks = TransChargeRemarks;

						ComputeSubTotal();

						if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
						{
							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
							clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks);

							InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.ChargeType, "Apply transaction Charge for " + mclsSalesTransactionDetails.Charge.ToString("#,###.#0") + ". Tran. #:" + lblTransNo.Text);

							clsSalesTransactions.CommitAndDispose();
						}
						else
						{
							MessageBox.Show("Sorry the input Charge will yield a less than ZERO amount. Please type another Charge.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
							mclsSalesTransactionDetails.ChargeAmount = OldCharge;
							lblTransCharge.Tag = OldTransChargeType;

							ComputeSubTotal();
							goto BackToCharge;
						}
						
						clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Charge.ToString("#,###.#0"));
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
				Cursor.Current = Cursors.Default;
			}
		}
		private void ChangeOrderType()
		{
			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot change Order Type if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CloseTransaction;
				login.Header = "Change Order Type";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Changing order type of trans. no. " + lblTransNo.Text);

					OrderTypeWnd clsOrderTypeWnd = new OrderTypeWnd();
					clsOrderTypeWnd.ShowDialog(this);
					DialogResult result = clsOrderTypeWnd.Result;
					OrderTypes pvtOrderType = clsOrderTypeWnd.orderType;
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

						mclsSalesTransactionDetails.OrderType = pvtOrderType;

						Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
						clsSalesTransactions.UpdateOrderType(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.OrderType);
						InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.ChargeType, "Change order type to " + mclsSalesTransactionDetails.OrderType.ToString("G") + ". Tran. #:" + lblTransNo.Text);
						clsSalesTransactions.CommitAndDispose();

						clsEvent.AddEventLn("Done!");

						//if (pvtOrderType != OrderTypes.DineIn && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID) 
						//    SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER);

						//if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
						//{
						//    mclsSalesTransactionDetails.OrderType = OrderTypes.DineIn;

						//    clsSalesTransactions = new Data.SalesTransactions();
						//    clsSalesTransactions.UpdateOrderType(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.OrderType);
						//    InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.ChargeType, "System override order type to " + mclsSalesTransactionDetails.OrderType.ToString("G") + ". Tran. #:" + lblTransNo.Text);
						//    clsSalesTransactions.CommitAndDispose();
						//}

						lblOrderType.Text = mclsSalesTransactionDetails.OrderType.ToString("G").ToUpper();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
				Cursor.Current = Cursors.Default;
			}
		}
		private void OpenTransactionDrawer()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.OpenDrawer);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.OpenDrawer;
				login.Header = "Open Drawer";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
				Invoke(opendrawerDel);
			}
		}
		private void EnterCreditPayment()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterCreditPayment);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.EnterCreditPayment;
				login.Header = "Enter Credit Payment";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{

				ContactSelectWnd ContactWnd = new ContactSelectWnd();
				ContactWnd.HasCreditOnly = true;
				ContactWnd.ShowDialog(this);
				DialogResult result = ContactWnd.Result;
				Data.ContactDetails details = ContactWnd.Details;
				ContactWnd.Close();
				ContactWnd.Dispose();

				if (result == DialogResult.OK)
				{
					CreditsWnd creditWnd = new CreditsWnd();
					creditWnd.CustomerDetails = details;
					creditWnd.ShowDialog(this);

					decimal AmountPaid = creditWnd.AmountPayment;
					decimal CashPayment = creditWnd.CashPayment;
					decimal ChequePayment = creditWnd.ChequePayment;
					decimal CreditCardPayment = creditWnd.CreditCardPayment;
					decimal DebitPayment = creditWnd.DebitPayment;
					decimal BalanceAmount = creditWnd.BalanceAmount;
					decimal ChangeAmount = creditWnd.ChangeAmount;
					PaymentTypes PaymentType = creditWnd.PaymentType;
					ArrayList arrCashPaymentDetails = creditWnd.CashPaymentDetails;
					ArrayList arrChequePaymentDetails = creditWnd.ChequePaymentDetails;
					ArrayList arrCreditCardPaymentDetails = creditWnd.CreditCardPaymentDetails;
					ArrayList arrDebitPaymentDetails = creditWnd.DebitPaymentDetails;
					result = creditWnd.Result;

					creditWnd.Close();
					creditWnd.Dispose();

					if (result == DialogResult.OK)
					{

						Cursor.Current = Cursors.WaitCursor;
						try
						{
							/************** April 21, 2006: added transaction no. ***************/
							lblCustomer.Tag = details.ContactID;
							lblCustomer.Text = details.ContactName;

							LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, details);

							if (!this.CreateTransaction()) return;

							txtBarCode.Text = Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE;
							ReadBarCode();
							int iRow = dgItems.CurrentRowIndex;

							Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
							Details.Price = AmountPaid;
							Details.Amount = AmountPaid;

							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
							clsSalesTransactions.GetConnection();

							ApplyChangeQuantityPriceAmountDetails(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, iRow, Details);

							SavePayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails, clsSalesTransactions.Connection, clsSalesTransactions.Transaction);

							OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
							Invoke(opendrawerDel);

							//insert to log file
							ItemFooterDetails clsItemFooterDetails = new ItemFooterDetails();
							clsItemFooterDetails.Quantity = ItemDataTable.Rows.Count;
							clsItemFooterDetails.Price = AmountPaid;
							clsItemFooterDetails.Discount = 0;
							clsItemFooterDetails.ItemDiscount = 0;
							clsItemFooterDetails.Amount = AmountPaid;
							mclsTransactionStream.AddItemFooter(clsItemFooterDetails, mclsSalesTransactionDetails.TransactionDate);

							FooterDetails clsFooterDetails = new FooterDetails();
							clsFooterDetails.SubTotal = AmountPaid;
							clsFooterDetails.Discount = 0;
							clsFooterDetails.VAT = 0;
							clsFooterDetails.VatableAmount = 0;
							clsFooterDetails.EVAT = 0;
							clsFooterDetails.EVatableAmount = 0;
							clsFooterDetails.LocalTax = 0;
							clsFooterDetails.AmountPaid = AmountPaid;
							clsFooterDetails.BalanceAmount = BalanceAmount;
							clsFooterDetails.ChangeAmount = ChangeAmount;
							mclsTransactionStream.AddTransactionFooter(clsFooterDetails, mclsSalesTransactionDetails.TransactionDate);

							mclsTransactionStream.CommitAndDispose(mclsSalesTransactionDetails.TransactionDate);

							//update the transaction table 
							Int64 iTransactionID = Convert.ToInt64(lblTransNo.Tag);
							clsSalesTransactions.Close(mclsSalesTransactionDetails.TransactionID, AmountPaid, 0, 0, 0, DiscountTypes.NotApplicable, 0, 0, 0, 0, 0, AmountPaid, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, 0, 0, PaymentType, null, null, 0, 0, null, null, mclsSalesTransactionDetails.CashierID, lblCashier.Text);

							//UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
							UpdateTerminalReport(clsSalesTransactions.Connection,clsSalesTransactions.Transaction, TransactionStatus.CreditPayment, AmountPaid, 0, 0, 0, 0, 0, 0, 0, 0, 0, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, PaymentType);

							//UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
							UpdateCashierReport(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, TransactionStatus.CreditPayment, AmountPaid, 0, 0, 0, 0, 0, 0, 0, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, PaymentType);

							InsertAuditLog(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, AccessTypes.CreditPayment, "Pay credit for " + details.ContactName + ".");

							clsSalesTransactions.CommitAndDispose();

							if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
								if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
									mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

							if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
							{
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

										PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
									}
								}
							}

							PrintReportFooterSection(true, TransactionStatus.CreditPayment, 0, 0, AmountPaid, 0, 0, AmountPaid, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails);

							this.LoadOptions();
						}
						catch (Exception ex)
						{
							Event clsEvent = new Event();
							clsEvent.AddEventLn("ERROR!!! Credit payment procedure. Err Description: " + ex.Message);
						}
						Cursor.Current = Cursors.Default;
					}
				}
			}
		}
		private void RefundTransaction()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.RefundTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.RefundTransaction;
				login.Header = "Refund Transaction";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				if (MessageBox.Show("Press OK to issue REFUND transaction or CANCEL to disregard.", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
					return;

				Cursor.Current = Cursors.WaitCursor;

				try
				{
					clsEvent.AddEventLn("Issuing REFUND Transaction!");
					LoadOptions();
					if (!this.CreateTransaction()) return;
					mboIsRefund = true;
					// Aug 6, 2011 : Lemu
					// Include items with zero quantity for REFUND
					mclsTerminalDetails.ShowItemMoreThanZeroQty = false;

					lblSubtotalName.Text = "SUBTOTAL: REFUND";
					lblOrderType.Visible = false;
					InsertAuditLog(AccessTypes.RefundTransaction, "Initialize refund transaction defaults. Tran. #:" + lblTransNo.Text);
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardIssuance);
			
			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.RewardCardIssuance;
				login.Header = "Issue new Reward Card";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					Data.Contacts clsContact = new Data.Contacts();
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
						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was issued to " + clsContactDetails.ContactName + "." + 
										Environment.NewLine + "Please collect the payment then close the transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was issued to " + clsContactDetails.ContactName + ".", true);

						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;
						
						txtBarCode.Text = "";
						CloseTransaction();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.RewardCardChange;
				login.Header = "Reward Card Replacement";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					Data.Contacts clsContact = new Data.Contacts();
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
						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been renewed with new expiry date " + clsContactDetails.RewardDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been renewed with new expiry date " + clsContactDetails.RewardDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", true);

						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.RewardCardChange;
				login.Header = "Reward Card Replacement";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					Data.Contacts clsContact = new Data.Contacts();
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
						MessageBox.Show("Reward Card No: " + strOldRewardCardNo + " has been replaced with new card #: " + clsContactDetails.RewardDetails.RewardCardNo + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + strOldRewardCardNo + " has been replaced with new card #: " + clsContactDetails.RewardDetails.RewardCardNo + ".", true);

						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.RewardCardChange;
				login.Header = "LOST Reward Card Reactivation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					Data.Contacts clsContact = new Data.Contacts();
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
					clsContactRewardWnd.Caption = "Reward Card Reactivation of LOST CARD ";
					clsContactRewardWnd.ContactDetails = clsContactDetails;
					clsContactRewardWnd.RewardCardStatus = RewardCardStatus.Reactivated_Lost;
					clsContactRewardWnd.ShowDialog(this);
					result = clsContactRewardWnd.Result;
					clsContactDetails = clsContactRewardWnd.ContactDetails;
					clsContactRewardWnd.Close();
					clsContactRewardWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been reactivated...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been reactivated...", true);
						this.LoadOptions();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.RewardCardChange;
				login.Header = "Reward Card Declaration as LOST";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					Data.Contacts clsContact = new Data.Contacts();
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
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardIssuance);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CreditCardIssuance;
				login.Header = "Issue new Credit Card";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; CreditType clsCreditType = CreditType.Individual; 
					Data.ContactDetails clsGuarantorDetails = new AceSoft.RetailPlus.Data.ContactDetails();
					ContactCreditTypeSelectWnd clsContactCreditTypeSelectWnd = new ContactCreditTypeSelectWnd();
					clsContactCreditTypeSelectWnd.ShowDialog(this);
					clsCreditType = clsContactCreditTypeSelectWnd.CreditType;
					result = clsContactCreditTypeSelectWnd.Result;
					clsContactCreditTypeSelectWnd.Close();
					clsContactCreditTypeSelectWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();

					if (clsCreditType == CreditType.Group)
					{
						MessageBox.Show("Please select a GUARANTOR to issue Credit Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
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
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					if (clsCreditType == CreditType.Individual) clsGuarantorDetails = clsContactDetails;

					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing credit card no to " + clsContactDetails.ContactName);

					Data.Contacts clsContact = new Data.Contacts();
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
					clsContactCreditWnd.Caption = "Credit Card Issuance";
					clsContactCreditWnd.CreditType = clsCreditType;
					clsContactCreditWnd.GuarantorID = clsGuarantorDetails.ContactID;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.New;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was issued to " + clsContactDetails.ContactName + "." +
										Environment.NewLine + "Please collect the payment then close the transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was issued to " + clsContactDetails.ContactName + ".", true);

						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CreditCardChange;
				login.Header = "Credit Card Replacement";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing credit card.");

					Data.Contacts clsContact = new Data.Contacts();
					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing credit card #: " + clsContactDetails.CreditDetails.CreditCardNo + " of " + clsContactDetails.ContactName + ".");

					ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
					clsContactCreditWnd.Caption = "Credit Card Renewal";
					clsContactCreditWnd.CreditType = clsContactDetails.CreditDetails.CreditType;
					clsContactCreditWnd.GuarantorID = clsContactDetails.CreditDetails.GuarantorID;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.ReNew;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been renewed with new expiry date " + clsContactDetails.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been renewed with new expiry date " + clsContactDetails.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", true);

						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CreditCardChange;
				login.Header = "Credit Card Replacement";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing credit card...");

					Data.Contacts clsContact = new Data.Contacts();
					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
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
						clsContactCreditWnd.Caption = "Credit Card Replacement of LOST CARD ";
					else if (pvtCreditCardStatus == CreditCardStatus.Replaced_Expired)
						clsContactCreditWnd.Caption = "Credit Card Replacement of EXPIRED CARD ";
					clsContactCreditWnd.CreditType = clsContactDetails.CreditDetails.CreditType;
					clsContactCreditWnd.GuarantorID = clsContactDetails.CreditDetails.GuarantorID;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = pvtCreditCardStatus;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Credit Card No: " + strOldCreditCardNo + " has been replaced with new card #: " + clsContactDetails.CreditDetails.CreditCardNo + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + strOldCreditCardNo + " has been replaced with new card #: " + clsContactDetails.CreditDetails.CreditCardNo + ".", true);

						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_CREDIT_CARD_REPLACEMENT_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CreditCardChange;
				login.Header = "Credit Card Reactivation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					clsEvent.AddEvent("[" + lblCashier.Text + "] Reactivating lost credit card...");

					Data.Contacts clsContact = new Data.Contacts();
					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
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
					clsContactCreditWnd.Caption = "Credit Card Reactivation of LOST CARD ";
					clsContactCreditWnd.CreditType = clsContactDetails.CreditDetails.CreditType;
					clsContactCreditWnd.GuarantorID = clsContactDetails.CreditDetails.GuarantorID;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.Reactivated_Lost;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been reactivated...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been reactivated...", true);
						this.LoadOptions();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
				Cursor.Current = Cursors.Default;
			}
		}
		private void CreditCardDeclareAsLost()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot deaclare a Credit Card as lost while there is an ongoing transaction." + Environment.NewLine + "Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CreditCardChange;
				login.Header = "Credit Card Declaration as Lost";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
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

					clsEvent.AddEvent("[" + lblCashier.Text + "] Declaring credit card as lost.");

					Data.Contacts clsContact = new Data.Contacts();
					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
					clsContactCreditWnd.Caption = "Credit Card Declaration as Lost";
					clsContactCreditWnd.CreditType = clsContactDetails.CreditDetails.CreditType;
					clsContactCreditWnd.GuarantorID = clsContactDetails.CreditDetails.GuarantorID;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.Lost;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been declared as LOST.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been declared as LOST.", true);
						this.LoadOptions();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ clsEvent.AddErrorEventLn(ex); }
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion

		#region Private Modifiers

		private void InitializeTransaction(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, Int64 UID)
		{
			try
			{
				clsEvent.AddEvent("Checking for pending transaction.");

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(pvtConnection, pvtTransaction);
				string stTransactionNo = null;
				bool HasPendingTransaction = clsSalesTransactions.HasPendingTransaction(UID, mclsTerminalDetails.TerminalNo, out stTransactionNo);

				if (HasPendingTransaction)
				{ clsEvent.AddEventLn(stTransactionNo + " found pending."); LoadTransaction(pvtConnection, pvtTransaction, stTransactionNo, mclsTerminalDetails.TerminalNo); }
				else
				{ clsEvent.AddEventLn("None."); LoadOptions(); }
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
					clsLogDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
					clsLogDetails.IPAddress = System.Net.Dns.GetHostName();
					clsLogDetails.LogoutDate = clsLogDetails.LoginDate;
					clsLogDetails.Status = CashierLogStatus.LoggedIn;
					
					this.UnLock(UserID);

					// for testing purposes only...remove this once ok.
					LoadOptions();

					CashierLogs clsCashierLogs = new CashierLogs();
					lblCashierName.Tag = clsCashierLogs.Insert(clsLogDetails).ToString();

					this.InitializeTransaction(clsCashierLogs.Connection, clsCashierLogs.Transaction, UserID);

					InsertAuditLog(clsCashierLogs.Connection, clsCashierLogs.Transaction, AccessTypes.LoginFE, "System login at terminal no. " + mclsTerminalDetails.TerminalNo);

					clsCashierLogs.CommitAndDispose();

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
				Details.PromoInPercent = Convert.ToInt16(dgItems[iRow, 29].ToString());
				Details.PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dgItems[iRow, 30].ToString());
				Details.PromoApplied = Convert.ToDecimal(dgItems[iRow, 31].ToString());
				Details.PurchasePrice = Convert.ToDecimal(dgItems[iRow, 32].ToString());
				Details.PurchaseAmount = Convert.ToDecimal(dgItems[iRow, 33].ToString());
				Details.IncludeInSubtotalDiscount = Convert.ToBoolean(dgItems[iRow, 34].ToString());
				Details.OrderSlipPrinter = (OrderSlipPrinter) Enum.Parse(typeof(OrderSlipPrinter), dgItems[iRow, 35].ToString());
				Details.OrderSlipPrinted = Convert.ToBoolean(dgItems[iRow, 36].ToString());
				Details.PercentageCommision = Convert.ToDecimal(dgItems[iRow, 37].ToString());
				Details.Commision = Convert.ToDecimal(dgItems[iRow, 38].ToString());
				Details.PaxNo = Convert.ToInt32(dgItems[iRow, 39].ToString());

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
				dr["Quantity"] = Details.Quantity;	//8
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
				dr["OrderSlipPrinter"] = Details.OrderSlipPrinter;
				dr["OrderSlipPrinted"] = Details.OrderSlipPrinted.ToString();
				dr["PercentageCommision"] = Details.PercentageCommision;
				dr["Commision"] = Details.Amount * (Details.PercentageCommision / 100);
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

				Data.CashierReport clsCashierReport = new Data.CashierReport();
				bool IsBeginningBalanceInitialized = clsCashierReport.IsBeginningBalanceInitialized(Constants.TerminalBranchID, CashierID, mclsTerminalDetails.TerminalNo);
				clsCashierReport.CommitAndDispose();

				if (!IsBeginningBalanceInitialized)
				{
					BalanceWnd clsBalanceWnd = new BalanceWnd();
					clsBalanceWnd.CashierID = CashierID;
					clsBalanceWnd.ShowDialog(this);

					DialogResult balanceResult = clsBalanceWnd.Result;

					clsBalanceWnd.Close();
					clsBalanceWnd.Dispose();

					if (balanceResult == DialogResult.OK)
					{
						boRetValue = true;
						OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
						Invoke(opendrawerDel);
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
		private Data.SalesTransactionItemDetails ApplyPromo(Data.SalesTransactionItemDetails Details)
		{
			try
			{
				Details.Amount = (Details.Price * Details.Quantity);

				decimal AppliedQuantity = 0;
				foreach (System.Data.DataRow dr in ItemDataTable.Rows)
				{
					if (dr["TransactionItemsID"].ToString() != Details.TransactionItemsID.ToString())
					{
						if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
						{
							AppliedQuantity += Convert.ToDecimal(dr["Quantity"]);
						}
					}
					if (Details.ItemNo == dr["ItemNo"].ToString())
					{
						break;
					}
				}

				PromoTypes PromoType = PromoTypes.NotApplicable;
				decimal PromoQuantity = 0;
				decimal PromoValue = 0;
				bool PromoInPercent = false;

				Data.PromoItems clsPromoItems = new Data.PromoItems();
				bool IsPromoApplied = clsPromoItems.ApplyPromoValue(Convert.ToInt64(lblCustomer.Tag), Details.ProductID, Details.VariationsMatrixID, out PromoType, out PromoQuantity, out PromoValue, out PromoInPercent);
				clsPromoItems.CommitAndDispose();

				Details.PromoValue = PromoValue;
				Details.PromoQuantity = PromoQuantity;
				Details.PromoInPercent = Convert.ToInt16(PromoInPercent);
				Details.PromoType = PromoType;
				Details.PromoApplied = 0;

				if (IsPromoApplied && PromoType != PromoTypes.NotApplicable)
				{
					Details.PromoApplied = GetPromoApplied(PromoType, Details.Price, Details.Quantity, PromoQuantity, PromoValue, PromoInPercent, AppliedQuantity);
				}
				Details.Amount = Details.Amount - Details.Discount - Details.PromoApplied;
				Details.Commision = Details.Amount * (Details.PercentageCommision / 100);

				return Details;
			}
			catch (Exception ex)
			{
				clsEvent.AddEventLn("ERROR! Applying promo. TRACE: ");
				clsEvent.AddErrorEventLn(ex);
				throw ex;
			}
		}
		private Data.SalesTransactionItemDetails ApplyPromo(Data.SalesTransactionItemDetails Details, MySqlConnection pvtConnection, MySqlTransaction pvtTransaction)
		{
			try
			{
				Details.Amount = (Details.Price * Details.Quantity);

				decimal AppliedQuantity = 0;
				foreach (System.Data.DataRow dr in ItemDataTable.Rows)
				{
					if (dr["TransactionItemsID"].ToString() != Details.TransactionItemsID.ToString())
					{
						if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
						{
							AppliedQuantity += Convert.ToDecimal(dr["Quantity"]);
						}
					}
					if (Details.ItemNo == dr["ItemNo"].ToString())
					{
						break;
					}
				}

				PromoTypes PromoType = PromoTypes.NotApplicable;
				decimal PromoQuantity = 0;
				decimal PromoValue = 0;
				bool PromoInPercent = false;

				Data.PromoItems clsPromoItems = new Data.PromoItems(pvtConnection, pvtTransaction);
				bool IsPromoApplied = clsPromoItems.ApplyPromoValue(Convert.ToInt64(lblCustomer.Tag), Details.ProductID, Details.VariationsMatrixID, out PromoType, out PromoQuantity, out PromoValue, out PromoInPercent);

				Details.PromoValue = PromoValue;
				Details.PromoQuantity = PromoQuantity;
				Details.PromoInPercent = Convert.ToInt16(PromoInPercent);
				Details.PromoType = PromoType;
				Details.PromoApplied = 0;

				if (IsPromoApplied && PromoType != PromoTypes.NotApplicable)
				{
					Details.PromoApplied = GetPromoApplied(PromoType, Details.Price, Details.Quantity, PromoQuantity, PromoValue, PromoInPercent, AppliedQuantity);
				}
				Details.Amount = Details.Amount - Details.Discount - Details.PromoApplied;
				Details.Commision = Details.Amount * (Details.PercentageCommision / 100);

				return Details;
			}
			catch (Exception ex)
			{
				clsEvent.AddEventLn("ERROR! Applying promo. TRACE: ");
				clsEvent.AddErrorEventLn(ex);
				throw ex;
			}
		}
		private decimal GetPromoApplied(PromoTypes PromoType, decimal Price, decimal Quantity, decimal PromoQuantity, decimal PromoValue, bool InPercent, decimal AppliedQuantity)
		{
			try
			{
				// This is the PromoApplied
				decimal decRetValue = 0;

				int ApplicableQuantity = (int)((Quantity + (AppliedQuantity % PromoQuantity)) / PromoQuantity);

				switch (PromoType)
				{
					case PromoTypes.ValueOffAfterQtyReached:
						if (!InPercent)
						{   decRetValue = ApplicableQuantity * PromoValue;  }
						break;
					case PromoTypes.PercentOffAfterQtyReached:
						if (InPercent)
						{   decRetValue = ApplicableQuantity * Price * PromoQuantity * (PromoValue / 100);  }
						break;
				}

				return decRetValue;
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR! Computing applicable promo. TRACE: " + ex.Message);
				throw ex;
			}
		}
		private void AddItem(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, Data.SalesTransactionItemDetails Details)
		{
			try
			{
				Details.ItemNo = Convert.ToString(ItemDataTable.Rows.Count + 1);
				Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;
				Details.PaxNo = 1;

				Details = ApplyPromo(Details, pvtConnection, pvtTransaction);

				System.Data.DataRow dr = ItemDataTable.NewRow();
				dr = setCurrentRowItemDetails(dr, Details);

				mclsTransactionStream.AddItem(Details, mclsSalesTransactionDetails.TransactionDate);

				Details.TransactionItemsID = AddItemToDB(pvtConnection, pvtTransaction, Details);
				dr["TransactionItemsID"] = Details.TransactionItemsID;

				// Added May 7, 2011 to Cater Reserved and Commit functionality    
				ReservedAndCommitItem(pvtConnection, pvtTransaction, Details, Details.TransactionItemStatus);

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

				ComputeSubTotal();

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(pvtConnection, pvtTransaction);
				clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VatableAmount, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVatableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks);

				try
				{
					DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
					DisplayItemToTurretDel.BeginInvoke(Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
				}
				catch { }
				if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
				{
					PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
					PrintItemDel.BeginInvoke(Details.ItemNo, Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
				}
			}
			
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR! Adding sales item. TRACE: " + ex.Message, true);
				throw ex;
			}
		}
		private long AddItemToDB(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, Data.SalesTransactionItemDetails Details)
		{
			try
			{
				Details.TransactionID = mclsSalesTransactionDetails.TransactionID;
				Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(pvtConnection, pvtTransaction);
				long TransactionItemsID = clsSalesTransactions.AddItem(Details);

				return TransactionItemsID;
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR! Adding sales item to database. TRACE: " + ex.Message);
				throw ex;
			}
		}

		private void ReservedAndCommitItem(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, Data.SalesTransactionItemDetails Details, TransactionItemStatus _previousTransactionItemStatus)
		{
			// Sep 24, 2011      Lemuel E. Aceron
			// Added order slip wherein all punch items will not change sales and inventory
			// a customer named ORDER SLIP should be defined in contacts
			if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
			{
				// Added May 7, 2011 to Cater Reserved and Commit functionality
				if (mclsTerminalDetails.ReservedAndCommit == true)
				{
					Data.Products clsProduct = new Data.Products(pvtConnection, pvtTransaction);
					Data.ProductUnit clsProductUnit = new Data.ProductUnit(pvtConnection, pvtTransaction);
					Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(pvtConnection, pvtTransaction);
					decimal decNewQuantity = 0;

					// both refund and normal transaction
					if (mboIsRefund)
					{
						if (Details.TransactionItemStatus == TransactionItemStatus.Void)
						{
							decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);

							clsProduct.SubtractQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_QTY_RESERVE_AND_COMMIT_VOID_ITEM) + " @ " + Details.Price.ToString("#,##0.#0") + " Buying:" + Details.PurchasePrice.ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
							// remove the ff codes for a change in Jul 26, 2011
							// clsProduct.SubtractQuantity(Details.ProductID, decNewQuantity);
							// 
							// if (Details.VariationsMatrixID != 0)
							//     clsProductVariationsMatrix.SubtractQuantity(Details.VariationsMatrixID, decNewQuantity);
						}
						else if (Details.TransactionItemStatus != TransactionItemStatus.Void)
						{
							decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);

							clsProduct.AddQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_REFUND_ITEM), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
							// remove the ff codes for a change in Jul 26, 2011
							// clsProduct.AddQuantity(Details.ProductID, decNewQuantity);
							// 
							// if (Details.VariationsMatrixID != 0)
							//     clsProductVariationsMatrix.AddQuantity(Details.VariationsMatrixID, decNewQuantity);
						}
					}
					else
					{
						// RETURN items
						if (Details.TransactionItemStatus == TransactionItemStatus.Return)
						{
							decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);

							clsProduct.AddQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_RETURN_ITEM), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
							// remove the ff codes for a change in Jul 26, 2011
							// clsProduct.AddQuantity(Details.ProductID, decNewQuantity);
							// 
							// if (Details.VariationsMatrixID != 0)
							//     clsProductVariationsMatrix.AddQuantity(Details.VariationsMatrixID, decNewQuantity);
						}
						// RETURN items that are VOID
						else if (Details.TransactionItemStatus == TransactionItemStatus.Void && _previousTransactionItemStatus == TransactionItemStatus.Return)
						{
							decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);

							clsProduct.SubtractQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_RESERVE_AND_COMMIT_VOID_ITEM) + " @ " + Details.Price.ToString("#,##0.#0") + " Buying:" + Details.PurchasePrice.ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
							// remove the ff codes for a change in Jul 26, 2011
							// clsProduct.AddQuantity(Details.ProductID, decNewQuantity);
							// 
							// if (Details.VariationsMatrixID != 0)
							//     clsProductVariationsMatrix.AddQuantity(Details.VariationsMatrixID, decNewQuantity);
						}
						// SOLD items that are VOID
						else if (Details.TransactionItemStatus == TransactionItemStatus.Void && _previousTransactionItemStatus != TransactionItemStatus.Return)
						{
							decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);

							clsProduct.AddQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_RESERVE_AND_COMMIT_VOID_ITEM), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
							// remove the ff codes for a change in Jul 26, 2011
							// clsProduct.AddQuantity(Details.ProductID, decNewQuantity);
							// 
							// if (Details.VariationsMatrixID != 0)
							//     clsProductVariationsMatrix.AddQuantity(Details.VariationsMatrixID, decNewQuantity);
						}
						// SOLD items
						else if (Details.TransactionItemStatus != TransactionItemStatus.Void)
						{
							decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);

							clsProduct.SubtractQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_RETAIL) + " @ " + Details.Price.ToString("#,##0.#0") + " Buying:" + Details.PurchasePrice.ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
							// remove the ff codes for a change in Jul 26, 2011
							// clsProduct.SubtractQuantity(Details.ProductID, decNewQuantity);
							// 
							// if (Details.VariationsMatrixID != 0)
							//     clsProductVariationsMatrix.SubtractQuantity(Details.VariationsMatrixID, decNewQuantity);
						}
					}
				}
			}
		}

		private void ComputeSubTotal()
		{
			try
			{
				clsEvent.AddEvent("Computing Amounts...");

				decimal decSubTotalDiscount = 0;
				decimal decTransDiscountApplied = mclsSalesTransactionDetails.TransDiscount;
				decimal decTransChargeApplied = mclsSalesTransactionDetails.ChargeAmount;
				DiscountTypes DiscountType = mclsSalesTransactionDetails.TransDiscountType;
				ChargeTypes ChargeType = (ChargeTypes)Enum.Parse(typeof(ChargeTypes), lblTransCharge.Tag.ToString());
				decimal decSubTotal = 0;
				decimal decSubTotalDiscountableAmount = 0;
				decimal decItemAmount = 0; //use for the amount with discount applied
				decimal decItemDiscount = 0;
				decimal decVAT = 0;
				decimal decVATableAmount = 0;
				decimal decNONVATableAmount = 0;
				decimal decEVAT = 0;
				decimal decEVATableAmount = 0;
				decimal decNONEVATableAmount = 0;
				decimal decLocalTax = 0;
				decimal decTotalItemSold = 0;
				decimal decTotalQuantitySold = 0;

				foreach (System.Data.DataRow dr in ItemDataTable.Rows)
				{
					DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString());
					decimal itemTrueAmt = Convert.ToDecimal(dr["Amount"]);
					decimal itemVAT = Convert.ToDecimal(dr["VAT"]);

					decItemAmount = itemTrueAmt;

					decItemDiscount += Convert.ToDecimal(dr["Discount"]);

					if (dr["Quantity"].ToString().IndexOf("RETURN") != -1) //2. check if the item is return
					{
						decItemAmount = decItemAmount * -1;
						decItemDiscount = decItemDiscount * -1;
						itemTrueAmt = itemTrueAmt * -1;
					}
					else if (dr["Quantity"].ToString().IndexOf("VOID") == -1)
					{
						decTotalItemSold += 1;
						decTotalQuantitySold += Convert.ToDecimal(dr["Quantity"]);
					}

					if (DiscountType != DiscountTypes.NotApplicable)
					{
						if (ItemDiscountType == DiscountTypes.NotApplicable && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["IncludeInSubtotalDiscount"].ToString() != "0")
						{
							decSubTotalDiscountableAmount += itemTrueAmt;
						}
					}

					decSubTotal += itemTrueAmt;

					// compute the vat, evat and local tax
					// march 31, 2006 remove "&& dr["Quantity"].ToString().IndexOf("RETURN") == -1)
					// june 8, 2006 added && dr["Quantity"].ToString().IndexOf("RETURN") == -1) again.
					// june 27, 2007 remove "&& dr["Quantity"].ToString().IndexOf("RETURN") == -1) again
					if (Convert.ToDecimal(dr["VAT"]) != 0)
					{
						// Sep 2, 2010 added to include Senior Citizen Discount as nonvatable
						if (mclsSalesTransactionDetails.DiscountCode == Constants.SeniorCitizenDiscountCode || dr["DiscountCode"].ToString() == Constants.SeniorCitizenDiscountCode)
						{
							if (ItemDiscountType != DiscountTypes.NotApplicable && dr["DiscountCode"].ToString() != Constants.SeniorCitizenDiscountCode)
								decVATableAmount += decItemAmount;    
							else
								decNONVATableAmount += decItemAmount;                
						}
						else
							decVATableAmount += decItemAmount;
					}
					else if (Convert.ToDecimal(dr["VAT"]) == 0 && dr["Quantity"].ToString().IndexOf("VOID") == -1)
					{
						decNONVATableAmount += decItemAmount;
					}

					if (Convert.ToDecimal(dr["EVAT"]) != 0 && mclsTerminalDetails.EnableEVAT)
					{
						// Sep 2, 2010 added to include Senior Citizen Discount as nonvatable
						if (mclsSalesTransactionDetails.DiscountCode == Constants.SeniorCitizenDiscountCode || dr["DiscountCode"].ToString() == Constants.SeniorCitizenDiscountCode)
						{
							if (ItemDiscountType != DiscountTypes.NotApplicable && dr["DiscountCode"].ToString() != Constants.SeniorCitizenDiscountCode)
								decEVATableAmount += decItemAmount;
							else
								decNONEVATableAmount += decItemAmount;
						}
						else
							decEVATableAmount += decItemAmount;
					}
					else if (Convert.ToDecimal(dr["EVAT"]) == 0 && dr["Quantity"].ToString().IndexOf("VOID") == -1)
					{
						decNONEVATableAmount += decItemAmount;
					}

					if (Convert.ToDecimal(dr["LocalTax"]) != 0)
						decLocalTax += decItemAmount;

				}

				if (DiscountType == DiscountTypes.NotApplicable)
				{
					decSubTotalDiscount = 0;
					lblTransDiscount.Text = "";
				}
				else if (DiscountType == DiscountTypes.FixedValue)
				{
					decSubTotalDiscount = decTransDiscountApplied;
					lblTransDiscount.Text = "Less " + decTransDiscountApplied.ToString("#,##0") + " / " + decSubTotalDiscount.ToString("#,##0.#0");
				}
				else if (DiscountType == DiscountTypes.Percentage)
				{
					decSubTotalDiscount = (decSubTotalDiscountableAmount * (decTransDiscountApplied / 100));
					lblTransDiscount.Text = "Less " + decTransDiscountApplied.ToString("#,##0") + " % / " + decSubTotalDiscount.ToString("#,##0.#0");
				}
				//	gross sales = net sales + discount;
				//	net sales = gross sales - vat - discount

				if (!mclsTerminalDetails.IsVATInclusive)
				{
					if (decVATableAmount >= decSubTotalDiscount) decVATableAmount = decVATableAmount - decSubTotalDiscount; else decVATableAmount = 0;
					if (decEVATableAmount >= decSubTotalDiscount) decEVATableAmount = decEVATableAmount - decSubTotalDiscount; else decEVATableAmount = 0;
					if (decLocalTax >= decSubTotalDiscount) decLocalTax = decLocalTax - decSubTotalDiscount; else decLocalTax = 0;
				}
				else
				{
					if (decVATableAmount >= decSubTotalDiscount) decVATableAmount = (decVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decVATableAmount = 0;
					if (decEVATableAmount >= decSubTotalDiscount) decEVATableAmount = (decEVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decEVATableAmount = 0;
					if (decLocalTax >= decSubTotalDiscount) decLocalTax = (decLocalTax - decSubTotalDiscount) / (1 + (mclsTerminalDetails.LocalTax / 100)); else decLocalTax = 0;

					//// Sep 2, 2010 added to include Senior Citizen Discount as nonvatable
					//if (decNONVATableAmount >= decSubTotalDiscount) decNONVATableAmount = (decNONVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decNONVATableAmount = 0;
					//if (decNONEVATableAmount >= decSubTotalDiscount) decNONEVATableAmount = (decNONEVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decNONEVATableAmount = 0;
				}

				// Sep 2, 2010 added to include Senior Citizen Discount as nonvatable
				if (decNONVATableAmount >= decSubTotalDiscount) decNONVATableAmount = decNONVATableAmount - decSubTotalDiscount; else decNONVATableAmount = 0;
				if (decNONEVATableAmount >= decSubTotalDiscount) decNONEVATableAmount = decNONEVATableAmount - decSubTotalDiscount; else decNONEVATableAmount = 0;

				decVAT = decVATableAmount * (mclsTerminalDetails.VAT / 100);
				decEVAT = decEVATableAmount * (mclsTerminalDetails.EVAT / 100);
				decLocalTax = decLocalTax * (mclsTerminalDetails.LocalTax / 100);

				if (!mclsTerminalDetails.IsVATInclusive) decSubTotal += decVAT + decLocalTax;
				if (!mclsTerminalDetails.EnableEVAT) decSubTotal += decEVAT;

				
				// ****** BYPASS ****//
				long lngCount = 7;
				try { lngCount = long.Parse(System.Configuration.ConfigurationManager.AppSettings["CustomerCount"]); }
				catch { }

				if (ItemDataTable.Rows.Count >= lngCount)
				{
					try 
					{ 
						string[] strCustomerIDs = System.Configuration.ConfigurationManager.AppSettings["CustomerIDs"].ToLower().Split(':');
						foreach(string strCustomerID in strCustomerIDs)
						{
							decimal decDiscount = decimal.Parse("0.30");

							try { decDiscount = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["CustomerIDsX"]); }
							catch { }

							if (strCustomerID == lblCustomer.Tag.ToString()) 
							{
								decSubTotal = decSubTotal * (decimal.Parse("1") -decDiscount);
								decSubTotalDiscount = decSubTotalDiscount * (decimal.Parse("1") - decDiscount);
								decSubTotalDiscountableAmount = decSubTotalDiscountableAmount * (decimal.Parse("1") - decDiscount);
								decItemDiscount = decItemDiscount * (decimal.Parse("1") - decDiscount);
								decVAT = decVAT * (decimal.Parse("1") - decDiscount);
								decVATableAmount = decVATableAmount * (decimal.Parse("1") - decDiscount);
								decNONVATableAmount = decNONVATableAmount * (decimal.Parse("1") - decDiscount);
								decEVAT = decEVAT * (decimal.Parse("1") - decDiscount);
								decEVATableAmount = decEVATableAmount * (decimal.Parse("1") - decDiscount);
								decNONEVATableAmount = decNONEVATableAmount * (decimal.Parse("1") - decDiscount);
								decLocalTax = decLocalTax * (decimal.Parse("1") - decDiscount);

								break; 
							}
						}
					}
					catch { }
				}

				lblSubTotal.Text = decSubTotal.ToString("###,##0.#0");

				if (ChargeType == ChargeTypes.NotApplicable)
				{
					mclsSalesTransactionDetails.Charge = 0;
					mclsSalesTransactionDetails.ChargeAmount = 0;
					lblTransCharge.Text = "";
				}
				else if (ChargeType == ChargeTypes.FixedValue)
				{
					mclsSalesTransactionDetails.Charge = decTransChargeApplied;
					lblTransCharge.Text = "Plus " + decTransChargeApplied.ToString("#,##0") + " / " + mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
				}
				else if (ChargeType == ChargeTypes.Percentage)
				{
					mclsSalesTransactionDetails.Charge = decSubTotal * (decTransChargeApplied / 100);
					lblTransCharge.Text = "Plus " + decTransChargeApplied.ToString("#,##0") + " % / " + mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
				}

				mclsSalesTransactionDetails.AmountDue = decSubTotal + mclsSalesTransactionDetails.Charge - decSubTotalDiscount;
				mclsSalesTransactionDetails.SubTotal = decSubTotal;
				mclsSalesTransactionDetails.Discount = decSubTotalDiscount;
				mclsSalesTransactionDetails.DiscountableAmount = decSubTotalDiscountableAmount;
				mclsSalesTransactionDetails.ItemsDiscount = decItemDiscount;
				mclsSalesTransactionDetails.VAT = decVAT;
				mclsSalesTransactionDetails.VatableAmount = decVATableAmount;
				mclsSalesTransactionDetails.NonVATableAmount = decNONVATableAmount;
				mclsSalesTransactionDetails.EVAT = decEVAT;
				mclsSalesTransactionDetails.EVatableAmount = decEVATableAmount;
				mclsSalesTransactionDetails.NonEVATableAmount = decNONEVATableAmount;
				mclsSalesTransactionDetails.LocalTax = decLocalTax;
				mclsSalesTransactionDetails.TotalItemSold = decTotalItemSold;
				mclsSalesTransactionDetails.TotalQuantitySold = decTotalQuantitySold;
				
				clsEvent.AddEventLn("Done computing amount for transaction #:" + mclsSalesTransactionDetails.TransactionNo);
			}
			catch (Exception ex)
			{
				clsEvent.AddEventLn("ERROR! Computing sales subtotal. TRACE: " + ex.Message + " " + ex.ToString());
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PackUnpackTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PackUnpackTransaction;
				login.Header = "Pack/Unpack Transaction Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
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

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PackUnpackTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PackUnpackTransaction;
				login.Header = "Pack/Unpack Transaction Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				try
				{
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
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
		private void ReprintTransaction()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ReprintTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.ReprintTransaction;
				login.Header = "Reprint Transaction Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
				clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
				clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
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
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					clsSalesTransactions.GetConnection();
					LoadTransaction(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, strTransactionNo, strTerminalNo);
					clsSalesTransactions.CommitAndDispose();

					//insert to logfile
					mclsTransactionStream.AddTransactionHeader(mclsSalesTransactionDetails, mclsSalesTransactionDetails.TransactionDate);

					ArrayList arrChequePaymentDetails = null;
					ArrayList arrCreditCardPaymentDetails = null;
					ArrayList arrCreditPaymentDetails = null;
					ArrayList arrDebitPaymentDetails = null;

					//print transactionfooter
					if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
						PrintReportFooterSection(true, TransactionStatus.Reprinted, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, mclsSalesTransactionDetails.CashPayment, mclsSalesTransactionDetails.ChequePayment, mclsSalesTransactionDetails.CreditCardPayment, mclsSalesTransactionDetails.CreditPayment, mclsSalesTransactionDetails.DebitPayment, mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardConvertedPayment, mclsSalesTransactionDetails.ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

					//add to item footer details event
					ItemFooterDetails clsItemFooterDetails = new ItemFooterDetails();
					clsItemFooterDetails.Quantity = ItemDataTable.Rows.Count;
					clsItemFooterDetails.Price = mclsSalesTransactionDetails.SubTotal + mclsSalesTransactionDetails.Discount;
					clsItemFooterDetails.Discount = mclsSalesTransactionDetails.Discount;
					clsItemFooterDetails.ItemDiscount = mclsSalesTransactionDetails.ItemsDiscount;
					clsItemFooterDetails.Amount = mclsSalesTransactionDetails.SubTotal;
					mclsTransactionStream.AddItemFooter(clsItemFooterDetails, mclsSalesTransactionDetails.TransactionDate);

					//add transaction footer event
					FooterDetails clsFooterDetails = new FooterDetails();
					clsFooterDetails.SubTotal = mclsSalesTransactionDetails.SubTotal;
					clsFooterDetails.Discount = mclsSalesTransactionDetails.Discount;
					clsFooterDetails.VAT = mclsSalesTransactionDetails.VAT;
					clsFooterDetails.EVAT = mclsSalesTransactionDetails.EVAT;
					clsFooterDetails.LocalTax = mclsSalesTransactionDetails.LocalTax;
					clsFooterDetails.AmountPaid = mclsSalesTransactionDetails.AmountPaid;
					clsFooterDetails.BalanceAmount = mclsSalesTransactionDetails.BalanceAmount;
					clsFooterDetails.ChangeAmount = mclsSalesTransactionDetails.ChangeAmount;
					mclsTransactionStream.AddTransactionFooter(clsFooterDetails, mclsSalesTransactionDetails.TransactionDate);

					//Close transaction footer
					mclsTransactionStream.CommitAndDispose(mclsSalesTransactionDetails.TransactionDate);

					if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
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
					//Added February 10, 2010
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
					{
						PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
					}
					//Added May 11, 2010
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
					{
						if (mclsSalesTransactionDetails.CashPayment != 0 || mclsSalesTransactionDetails.CreditCardPayment != 0)
							PrintSalesInvoice();
						if (mclsSalesTransactionDetails.ChequePayment != 0 || mclsSalesTransactionDetails.CreditPayment != 0)
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
						PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon);
					}
					clsEvent.AddEventLn("Done reprinting transaction #:" + strTransactionNo, true);

					this.LoadOptions();
				}
			}
		}
		private void ReprintDeliveryReceipt()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.ReprintTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.ReprintTransaction;
				login.Header = "Reprint Transaction Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
				clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
				clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
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
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					clsSalesTransactions.GetConnection();
					LoadTransaction(clsSalesTransactions.Connection, clsSalesTransactions.Transaction, strTransactionNo, strTerminalNo);
					clsSalesTransactions.CommitAndDispose();

					//insert to logfile
					mclsTransactionStream.AddTransactionHeader(mclsSalesTransactionDetails, mclsSalesTransactionDetails.TransactionDate);

					ArrayList arrChequePaymentDetails = null;
					ArrayList arrCreditCardPaymentDetails = null;
					ArrayList arrCreditPaymentDetails = null;
					ArrayList arrDebitPaymentDetails = null;

					//print transactionfooter
					if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
						PrintReportFooterSection(true, TransactionStatus.Reprinted, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, mclsSalesTransactionDetails.CashPayment, mclsSalesTransactionDetails.ChequePayment, mclsSalesTransactionDetails.CreditCardPayment, mclsSalesTransactionDetails.CreditPayment, mclsSalesTransactionDetails.DebitPayment, mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardConvertedPayment, mclsSalesTransactionDetails.ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

					//add to item footer details event
					ItemFooterDetails clsItemFooterDetails = new ItemFooterDetails();
					clsItemFooterDetails.Quantity = ItemDataTable.Rows.Count;
					clsItemFooterDetails.Price = mclsSalesTransactionDetails.SubTotal + mclsSalesTransactionDetails.Discount;
					clsItemFooterDetails.Discount = mclsSalesTransactionDetails.Discount;
					clsItemFooterDetails.ItemDiscount = mclsSalesTransactionDetails.ItemsDiscount;
					clsItemFooterDetails.Amount = mclsSalesTransactionDetails.SubTotal;
					mclsTransactionStream.AddItemFooter(clsItemFooterDetails, mclsSalesTransactionDetails.TransactionDate);

					//add transaction footer event
					FooterDetails clsFooterDetails = new FooterDetails();
					clsFooterDetails.SubTotal = mclsSalesTransactionDetails.SubTotal;
					clsFooterDetails.Discount = mclsSalesTransactionDetails.Discount;
					clsFooterDetails.VAT = mclsSalesTransactionDetails.VAT;
					clsFooterDetails.EVAT = mclsSalesTransactionDetails.EVAT;
					clsFooterDetails.LocalTax = mclsSalesTransactionDetails.LocalTax;
					clsFooterDetails.AmountPaid = mclsSalesTransactionDetails.AmountPaid;
					clsFooterDetails.BalanceAmount = mclsSalesTransactionDetails.BalanceAmount;
					clsFooterDetails.ChangeAmount = mclsSalesTransactionDetails.ChangeAmount;
					mclsTransactionStream.AddTransactionFooter(clsFooterDetails, mclsSalesTransactionDetails.TransactionDate);

					//Close transaction footer
					mclsTransactionStream.CommitAndDispose(mclsSalesTransactionDetails.TransactionDate);

					PrintDeliveryReceipt();
					
					clsEvent.AddEventLn("Done reprinting delivery receipt transaction #:" + strTransactionNo, true);

					this.LoadOptions();
				}
			}
		}
		private void PrintCheckOutBill()
		{
			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
			{
				MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				return;
			}
			if (!mboIsInTransaction)
			{
				MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CloseTransaction;
				login.Header = "Print Check-Out Bill Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				/*****
				 * Put a separate ReportHeader for Check-OutBill
				 * Due to security reasons that check-out bill is use as a receipt.
				 * January 14, 2008
				 * ***/
				PrintCheckOutBillHeader();

				/*** end of Jan 14 ***/

				foreach (System.Data.DataRow dr in ItemDataTable.Rows)
				{
					if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
					{
						string stItemNo = "" + dr["ItemNo"].ToString();
						string stProductCode = "" + dr["ProductCode"].ToString() + "-RET";
						string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
						if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
						decimal decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
						decimal decPrice = Convert.ToDecimal(dr["Price"]);
						decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
						decimal decAmount = Convert.ToDecimal(dr["Amount"]) * -1;
						decimal decVAT = Convert.ToDecimal(dr["VAT"]);
						decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
						decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);

						PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
					}
					else if (dr["Quantity"].ToString() != "VOID")
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

						PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
					}
				}

				PrintReportFooterSection(true, TransactionStatus.Closed, mclsSalesTransactionDetails.TotalItemSold, mclsSalesTransactionDetails.TotalQuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

				mboIsItemHeaderPrinted = false;
				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
			}
		}
		private void PrintCheckOutBillHeader()
		{
			PrintReportHeaderSection(true);
			mboIsItemHeaderPrinted = true;

			if (mclsTerminalDetails.IsPrinterDotMatrix)
			{
				mstrToPrint += CenterString("-/- CHECK-OUT BILL -/-", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += CenterString("NOT VALID AS RECEIPT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
			}else{
				mstrToPrint += RawPrinterHelper.escBoldOn + CenterString("-/- CHECK-OUT BILL -/-", mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escBoldOff + Environment.NewLine;
				mstrToPrint += RawPrinterHelper.escBoldOn + CenterString("NOT VALID AS RECEIPT", mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escBoldOff + Environment.NewLine;
			}
			PrintReportPageHeaderSection(true);
		}
		private void PrintCheckOutBillFooter()
		{
			if (mclsSalesTransactionDetails.OrderType == OrderTypes.Delivery)
			{
				Data.Contacts clsContact = new Data.Contacts();
				Data.ContactDetails clsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
				clsContact.CommitAndDispose();

				if (clsContactDetails.BusinessName != string.Empty)
					mstrToPrint += "Delivered to".PadRight(13) + ":" + clsContactDetails.BusinessName.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14) + Environment.NewLine;
				if (clsContactDetails.TelephoneNo != string.Empty)
					mstrToPrint += "Tel #".PadRight(13) + ":" + clsContactDetails.TelephoneNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14) + Environment.NewLine;
				if (clsContactDetails.Address != string.Empty)
					mstrToPrint += "Address".PadRight(13) + ":" + clsContactDetails.Address.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14) + Environment.NewLine;

			}

		}
		private void PrintOrderSlip(bool WillReprintAll)
		{
			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
			{
				MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				return;
			}
			if (!mboIsInTransaction)
			{
				MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				return;
			}
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.CloseTransaction;
				login.Header = "Print Order Slip Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}
			if (loginresult == DialogResult.OK)
			{
				/*************************
				 * Check if will reprint all items for ALT + S
				 * December 18, 2008
				 * **********************/

				bool bolRetailPlusOSPrinter1HeaderPrinted = false;
				bool bolRetailPlusOSPrinter2HeaderPrinted = false;
				bool bolRetailPlusOSPrinter3HeaderPrinted = false;
				bool bolRetailPlusOSPrinter4HeaderPrinted = false;
				bool bolRetailPlusOSPrinter5HeaderPrinted = false;

				Data.ProductComposition clsProductComposition = new Data.ProductComposition();
				clsProductComposition.GetConnection();
				Data.Products clsProduct = new Data.Products(clsProductComposition.Connection, clsProductComposition.Transaction);
				Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(clsProductComposition.Connection, clsProductComposition.Transaction);

				// print order slip items in each printer
				foreach (System.Data.DataRow dr in ItemDataTable.Rows)
				{
					bool OrderSlipPrinted = Convert.ToBoolean(dr["OrderSlipPrinted"]);
					if (!OrderSlipPrinted || WillReprintAll)
					{
						/****************************************
						 * Update items that are already printed
						 *  December 18, 2008
						****************************************/
						long TransactionItemsID = Convert.ToInt64(dr["TransactionItemsID"]);
						clsSalesTransactionItems.UpdateOrderSlipPrinted(true, TransactionItemsID, mclsSalesTransactionDetails.TransactionDate);

						if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1)
						{
							//string stItemNo = "" + dr["ItemNo"].ToString();
							long lProductID = Convert.ToInt64(dr["ProductID"]);
							//clsProduct = new Data.Product(clsProductComposition.Connection, clsProductComposition.Transaction);
							bool bolWillPrintProductComposition = clsProduct.WillPrintProductComposition(lProductID);

							string stProductCode = "" + dr["ProductCode"].ToString();
							string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
							decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);

							AceSoft.RetailPlus.OrderSlipPrinter orderSlipPrinter = (OrderSlipPrinter) Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
							string stPrinterName = orderSlipPrinter.ToString("G");

							if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1 && !bolRetailPlusOSPrinter1HeaderPrinted)
							{ bolRetailPlusOSPrinter1HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1.ToString("G")); }
							if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2 && !bolRetailPlusOSPrinter2HeaderPrinted)
							{ bolRetailPlusOSPrinter2HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2.ToString("G")); }
							if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3 && !bolRetailPlusOSPrinter3HeaderPrinted)
							{ bolRetailPlusOSPrinter3HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3.ToString("G")); }
							if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4 && !bolRetailPlusOSPrinter4HeaderPrinted)
							{ bolRetailPlusOSPrinter4HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4.ToString("G")); }
							if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5 && !bolRetailPlusOSPrinter5HeaderPrinted)
							{ bolRetailPlusOSPrinter5HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5.ToString("G")); }

							// print product composition first: 
							// if there are product compositions no need to print product

							if (bolWillPrintProductComposition)
							{
								if (!PrintOrderSlipComposition(lProductID, stProductCode, stProductUnitCode, decQuantity, clsProductComposition.Connection, clsProductComposition.Transaction, bolWillPrintProductComposition))
								{
									// if there are no product composition
									// print the product only
									PrintItemForKitchen(stProductCode, stProductUnitCode, decQuantity, stPrinterName, true);
								}
							}
							else {
								// if there are no product composition
								// print the product only
								PrintItemForKitchen(stProductCode, stProductUnitCode, decQuantity, stPrinterName, true);
							}
						}

						/****************************************
						 * Update items that are already printed
						 *  December 18, 2008
						****************************************/
						dr["OrderSlipPrinted"] = true.ToString();
						
					}
				}
				clsProductComposition.CommitAndDispose();
				// print order slip footer in each printer
				if (bolRetailPlusOSPrinter1HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1.ToString("G")); }
				if (bolRetailPlusOSPrinter2HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2.ToString("G")); }
				if (bolRetailPlusOSPrinter3HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3.ToString("G")); }
				if (bolRetailPlusOSPrinter4HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4.ToString("G")); }
				if (bolRetailPlusOSPrinter5HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5.ToString("G")); }

			}
		}
		private bool PrintOrderSlipCountCompositionHeader(long ProductID, MySqlConnection cn, MySqlTransaction tran, int iRetailPlusOSPrinter1Ctr, int iRetailPlusOSPrinter2Ctr, int iRetailPlusOSPrinter3Ctr, int iRetailPlusOSPrinter4Ctr, int iRetailPlusOSPrinter5Ctr, out int RetailPlusOSPrinter1Ctr, out int RetailPlusOSPrinter2Ctr, out int RetailPlusOSPrinter3Ctr, out int RetailPlusOSPrinter4Ctr, out int RetailPlusOSPrinter5Ctr)
		{
			// returns 
			//  false if no product composition
			//  true if with product composition

			bool boRetValue = false;
			Data.ProductComposition clsProductComposition = new Data.ProductComposition(cn, tran);
			System.Data.DataTable dt = clsProductComposition.dtList(ProductID, string.Empty, SortOption.Ascending);
			foreach (System.Data.DataRow dr in dt.Rows)
			{
				AceSoft.RetailPlus.OrderSlipPrinter orderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
				switch (orderSlipPrinter)
				{
					case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1: { iRetailPlusOSPrinter1Ctr++; break; }
					case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2: { iRetailPlusOSPrinter2Ctr++; break; }
					case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3: { iRetailPlusOSPrinter3Ctr++; break; }
					case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4: { iRetailPlusOSPrinter4Ctr++; break; }
					case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5: { iRetailPlusOSPrinter5Ctr++; break; }
				}

				long lProductID = Convert.ToInt64(dr["ProductID"]);
				PrintOrderSlipCountCompositionHeader(lProductID, cn, tran, iRetailPlusOSPrinter1Ctr, iRetailPlusOSPrinter2Ctr, iRetailPlusOSPrinter3Ctr, iRetailPlusOSPrinter4Ctr, iRetailPlusOSPrinter5Ctr, out RetailPlusOSPrinter1Ctr, out RetailPlusOSPrinter2Ctr, out RetailPlusOSPrinter3Ctr, out RetailPlusOSPrinter4Ctr, out RetailPlusOSPrinter5Ctr);

				boRetValue = true;
			}

			RetailPlusOSPrinter1Ctr = iRetailPlusOSPrinter1Ctr;
			RetailPlusOSPrinter2Ctr = iRetailPlusOSPrinter2Ctr;
			RetailPlusOSPrinter3Ctr = iRetailPlusOSPrinter3Ctr;
			RetailPlusOSPrinter4Ctr = iRetailPlusOSPrinter4Ctr;
			RetailPlusOSPrinter5Ctr = iRetailPlusOSPrinter5Ctr;

			return boRetValue;
		}
		private bool PrintOrderSlipComposition(long ProductID, string ProductCode, string ProductUnitCode, decimal Quantity, MySqlConnection cn, MySqlTransaction tran, bool bolWillPrintProductComposition)
		{
			bool boRetValue = false;

			bool bolRetailPlusOSPrinter1ItemHeaderPrinted = false;
			bool bolRetailPlusOSPrinter2ItemHeaderPrinted = false;
			bool bolRetailPlusOSPrinter3ItemHeaderPrinted = false;
			bool bolRetailPlusOSPrinter4ItemHeaderPrinted = false;
			bool bolRetailPlusOSPrinter5ItemHeaderPrinted = false;


			Data.ProductComposition clsProductComposition = new Data.ProductComposition(cn, tran);
			System.Data.DataTable dt = clsProductComposition.dtList(ProductID, string.Empty, SortOption.Ascending);

			foreach (System.Data.DataRow dr in dt.Rows)
			{
                AceSoft.RetailPlus.OrderSlipPrinter orderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
				string stPrinterName = orderSlipPrinter.ToString("G");

				long lProductID = Convert.ToInt64(dr["ProductID"]);
				string stProductCode = "" + dr["ProductCode"].ToString();
				string stProductUnitCode = "" + dr["UnitCode"].ToString();
				decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);

				if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1 && !bolRetailPlusOSPrinter1ItemHeaderPrinted)
				{ bolRetailPlusOSPrinter1ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1.ToString("G"), bolWillPrintProductComposition); }
				if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2 && !bolRetailPlusOSPrinter2ItemHeaderPrinted)
				{ bolRetailPlusOSPrinter2ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2.ToString("G"), bolWillPrintProductComposition); }
				if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3 && !bolRetailPlusOSPrinter3ItemHeaderPrinted)
				{ bolRetailPlusOSPrinter3ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3.ToString("G"), bolWillPrintProductComposition); }
				if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4 && !bolRetailPlusOSPrinter4ItemHeaderPrinted)
				{ bolRetailPlusOSPrinter4ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4.ToString("G"), bolWillPrintProductComposition); }
				if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5 && !bolRetailPlusOSPrinter5ItemHeaderPrinted)
				{ bolRetailPlusOSPrinter5ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5.ToString("G"), bolWillPrintProductComposition); }

				if (!PrintOrderSlipComposition(lProductID, stProductCode, stProductUnitCode, decQuantity, cn, tran, bolWillPrintProductComposition))
				{
					// if there are no product composition
					// print the product only
					PrintItemForKitchen("   " + stProductCode, stProductUnitCode, decQuantity, stPrinterName);
				}

				boRetValue = true;
			}

			return boRetValue;
		}
		private void PrintOrderSlipHeader(string PrinterName)
		{
			// print page header spacer
			for (int i = 0; i < 3; i++)
			{ SendOrderSlipToPrinter(Environment.NewLine, PrinterName); }

			SendOrderSlipToPrinter(CenterString("Trx #: " + mclsSalesTransactionDetails.TransactionNo, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine, PrinterName);
			if (mclsTerminalDetails.IsPrinterDotMatrix)
			{   SendOrderSlipToPrinter(CenterString("ORDER SLIP", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine, PrinterName); }
			else{SendOrderSlipToPrinter(RawPrinterHelper.esc8CpiOn + CenterString("ORDER SLIP", mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.esc8CpiOff + Environment.NewLine, PrinterName);}
			SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
			SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
			SendOrderSlipToPrinter("Customer : " + mclsSalesTransactionDetails.CustomerName + Environment.NewLine, PrinterName);
			SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
			SendOrderSlipToPrinter("DESC".PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
			SendOrderSlipToPrinter("UNIT".PadRight(6), PrinterName);
			SendOrderSlipToPrinter("QTY".PadLeft(6), PrinterName);
			SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
			SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
		}
		private void PrintOrderSlipFooter(string PrinterName)
		{

			SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
			SendOrderSlipToPrinter("Served by: " + mclsSalesTransactionDetails.WaiterName + Environment.NewLine, PrinterName);
			SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);

			// print page footer spacer
			for (int i = 0; i < 6; i++)
			{ SendOrderSlipToPrinter(Environment.NewLine, PrinterName); }

			if (mclsTerminalDetails.IsPrinterAutoCutter) CutPrinterPaper();
		}
		private delegate void PrintSalesInvoiceDelegate();
		private void PrintSalesInvoice()
		{
			try
			{
				if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
				{
					MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					return;
				}
				if (!mboIsInTransaction)
				{
					MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					return;
				}
				DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

				if (loginresult == DialogResult.None)
				{
					LogInWnd login = new LogInWnd();

					login.AccessType = AccessTypes.CloseTransaction;
					login.Header = "Print Sales Invoice Validation";
					login.ShowDialog(this);
					loginresult = login.Result;
					login.Close();
					login.Dispose();
				}
				if (loginresult == DialogResult.OK)
				{
					CRSReports.OR rpt = new CRSReports.OR();

					AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
					System.Data.DataRow drNew;

					/****************************report logo *****************************/
					try
					{
						System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/images/ReportLogo.jpg", System.IO.FileMode.Open, System.IO.FileAccess.Read);
						System.IO.FileInfo fi = new System.IO.FileInfo(Application.StartupPath + "/images/ReportLogo.jpg");

						byte[] propimg = new byte[fi.Length];
						fs.Read(propimg, 0, Convert.ToInt32(fs.Length));
						fs.Close();

						drNew = rptds.CompanyLogo.NewRow(); drNew["Picture"] = propimg;
						rptds.CompanyLogo.Rows.Add(drNew);
					}
					catch { }

					/****************************sales transaction *****************************/
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					Data.SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(lblTransNo.Text, mclsTerminalDetails.TerminalNo, Constants.TerminalBranchID);

					Data.Contacts clsContact = new Data.Contacts(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
					Data.ContactDetails clsContactDetails = clsContact.Details(clsSalesTransactionDetails.CustomerID);

					if (clsSalesTransactionDetails.isExist)
					{
						/****************************sales transaction details*****************************/
						drNew = rptds.Transactions.NewRow();

						drNew["TransactionID"] = clsSalesTransactionDetails.TransactionID;
						drNew["TransactionNo"] = clsSalesTransactionDetails.TransactionNo;
						drNew["CustomerName"] = clsSalesTransactionDetails.CustomerName;
						drNew["CustomerAddress"] = clsContactDetails.Address;
						drNew["CustomerTerms"] = clsContactDetails.Terms;
						drNew["CustomerModeOfterms"] = clsContactDetails.ModeOfTerms;
						drNew["CustomerBusinessName"] = clsContactDetails.BusinessName;
						drNew["CustomerTelNo"] = clsContactDetails.TelephoneNo;
						drNew["CashierName"] = clsSalesTransactionDetails.CashierName;
						drNew["TerminalNo"] = clsSalesTransactionDetails.TerminalNo;
						drNew["TransactionDate"] = clsSalesTransactionDetails.TransactionDate;
						drNew["DateSuspended"] = clsSalesTransactionDetails.DateSuspended.ToString();
						drNew["DateResumed"] = clsSalesTransactionDetails.DateResumed;
						drNew["TransactionStatus"] = clsSalesTransactionDetails.TransactionStatus;
						drNew["SubTotal"] = clsSalesTransactionDetails.SubTotal;
						drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
						drNew["Discount"] = clsSalesTransactionDetails.Discount;
						drNew["VAT"] = clsSalesTransactionDetails.VAT;
						drNew["VatableAmount"] = clsSalesTransactionDetails.VatableAmount;
						drNew["LocalTax"] = clsSalesTransactionDetails.LocalTax;
						drNew["AmountPaid"] = clsSalesTransactionDetails.AmountPaid;
						drNew["CashPayment"] = clsSalesTransactionDetails.CashPayment;
						drNew["ChequePayment"] = clsSalesTransactionDetails.ChequePayment;
						drNew["CreditCardPayment"] = clsSalesTransactionDetails.CreditCardPayment;
						drNew["BalanceAmount"] = clsSalesTransactionDetails.BalanceAmount;
						drNew["ChangeAmount"] = clsSalesTransactionDetails.ChangeAmount;
						drNew["DateClosed"] = clsSalesTransactionDetails.DateClosed;
						drNew["PaymentType"] = clsSalesTransactionDetails.PaymentType.ToString("d");
						drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
						drNew["Charge"] = clsSalesTransactionDetails.Charge;

						rptds.Transactions.Rows.Add(drNew);

						/****************************sales transaction items*****************************/
						Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
						System.Data.DataTable dt = clsSalesTransactionItems.List(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, "TransactionItemsID", SortOption.Ascending);

						foreach(System.Data.DataRow dr in dt.Rows)
						{
							drNew = rptds.SalesTransactionItems.NewRow();

							foreach (System.Data.DataColumn dc in rptds.SalesTransactionItems.Columns)
								drNew[dc] = dr[dc.ColumnName];

							rptds.SalesTransactionItems.Rows.Add(drNew);
						}
					}

					clsSalesTransactions.CommitAndDispose();

					rpt.SetDataSource(rptds);

					CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
					CrystalDecisions.Shared.ParameterValues currentValues;
					CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

					paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = CompanyDetails.CompanyName;
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = lblCashier.Text;
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = lblServedBy.Text.Remove(0, 11);
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = CompanyDetails.Address1 +
											Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
											Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #:" + CompanyDetails.FaxPhone +
											Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg.";
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					//foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
					//{
					//    if (objPic.Name.ToUpper() == "PICLOGO1")
					//    {
					//        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
					//    }
					//}

					//CRViewer.Visible = true;
					//CRViewer.ReportSource = rpt;
					//CRViewer.Show();

					try
					{
						DateTime logdate = DateTime.Now;
						string logsdir = System.Configuration.ConfigurationManager.AppSettings["logsdir"].ToString();

						if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
						{
							Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
						}
						string logFile = logsdir + logdate.ToString("MMM") + "/OR_" + logdate.ToString("yyyyMMddhhmmss") + ".doc";

						rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, logFile);
					}
					catch { }

				   
					rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
					rpt.PrintToPrinter(1, false, 0, 0);
					
					rpt.Close();
					rpt.Dispose();
					
				}
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddErrorEventLn(ex);
				MessageBox.Show("Sorry an error was encountered during printing, please reprint again." + Environment.NewLine + "Details: " + ex.Message, "RetailPlus");
			}
		}
		private void PrintSalesInvoiceToLX(TerminalReceiptType pclsTerminalReceiptType)
		{
			try
			{
				if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
				{
					MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					return;
				}
				if (!mboIsInTransaction)
				{
					MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					return;
				}
				DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

				if (loginresult == DialogResult.None)
				{
					LogInWnd login = new LogInWnd();

					login.AccessType = AccessTypes.CloseTransaction;
					login.Header = "Print LX 300+ Sales Invoice Validation";
					login.ShowDialog(this);
					loginresult = login.Result;
					login.Close();
					login.Dispose();
				}
				if (loginresult == DialogResult.OK)
				{
					CrystalDecisions.CrystalReports.Engine.ReportClass rpt = null;
					if (pclsTerminalReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
						rpt = new CRSReports.ORLX300PlusAmazon();
					else if (pclsTerminalReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
						rpt = new CRSReports.ORLX300Plus();
					else
						rpt = new CRSReports.ORLX();

					AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
					System.Data.DataRow drNew;

					/****************************sales transaction *****************************/
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					Data.SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(lblTransNo.Text, mclsTerminalDetails.TerminalNo, Constants.TerminalBranchID);

					Data.Contacts clsContact = new Data.Contacts(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
					Data.ContactDetails clsContactDetails = clsContact.Details(clsSalesTransactionDetails.CustomerID);

					if (clsSalesTransactionDetails.isExist)
					{
						drNew = rptds.Transactions.NewRow();

						drNew["TransactionID"] = clsSalesTransactionDetails.TransactionID;
						drNew["TransactionNo"] = clsSalesTransactionDetails.TransactionNo;
						drNew["CustomerName"] = clsSalesTransactionDetails.CustomerName;
						drNew["CustomerAddress"] = clsContactDetails.Address;
						drNew["CustomerTerms"] = clsContactDetails.Terms;
						drNew["CustomerModeOfterms"] = clsContactDetails.ModeOfTerms;
						drNew["CustomerBusinessName"] = clsContactDetails.BusinessName;
						drNew["CustomerTelNo"] = clsContactDetails.TelephoneNo;
						drNew["CashierName"] = clsSalesTransactionDetails.CashierName;
						drNew["TerminalNo"] = clsSalesTransactionDetails.TerminalNo;
						drNew["TransactionDate"] = clsSalesTransactionDetails.TransactionDate;
						drNew["DateSuspended"] = clsSalesTransactionDetails.DateSuspended.ToString();
						drNew["DateResumed"] = clsSalesTransactionDetails.DateResumed;
						drNew["TransactionStatus"] = clsSalesTransactionDetails.TransactionStatus;
						drNew["SubTotal"] = clsSalesTransactionDetails.SubTotal;
						drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
						drNew["Discount"] = clsSalesTransactionDetails.Discount;
						drNew["VAT"] = clsSalesTransactionDetails.VAT;
						drNew["VatableAmount"] = clsSalesTransactionDetails.VatableAmount;
						drNew["LocalTax"] = clsSalesTransactionDetails.LocalTax;
						drNew["AmountPaid"] = clsSalesTransactionDetails.AmountPaid;
						drNew["CashPayment"] = clsSalesTransactionDetails.CashPayment;
						drNew["ChequePayment"] = clsSalesTransactionDetails.ChequePayment;
						drNew["CreditCardPayment"] = clsSalesTransactionDetails.CreditCardPayment;
						drNew["BalanceAmount"] = clsSalesTransactionDetails.BalanceAmount;
						drNew["ChangeAmount"] = clsSalesTransactionDetails.ChangeAmount;
						drNew["DateClosed"] = clsSalesTransactionDetails.DateClosed;
						drNew["PaymentType"] = clsSalesTransactionDetails.PaymentType.ToString("d");
						drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
						drNew["Charge"] = clsSalesTransactionDetails.Charge;

						rptds.Transactions.Rows.Add(drNew);

						/****************************sales transaction items*****************************/
						Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
						System.Data.DataTable dt = clsSalesTransactionItems.List(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, "TransactionItemsID", SortOption.Ascending);

                        foreach (System.Data.DataRow dr in dt.Rows)
						{
							drNew = rptds.SalesTransactionItems.NewRow();

							foreach (System.Data.DataColumn dc in rptds.SalesTransactionItems.Columns)
								drNew[dc] = dr[dc.ColumnName];

							rptds.SalesTransactionItems.Rows.Add(drNew);
						}
					}

					clsSalesTransactions.CommitAndDispose();

					rpt.SetDataSource(rptds);

					CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
					CrystalDecisions.Shared.ParameterValues currentValues;
					CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

					paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = CompanyDetails.CompanyName;
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = lblCashier.Text;
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = lblServedBy.Text.Remove(0, 11);
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = CompanyDetails.Address1 +
											Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
											Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #:" + CompanyDetails.FaxPhone +
											Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg.";
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					//foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
					//{
					//    if (objPic.Name.ToUpper() == "PICLOGO1")
					//    {
					//        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
					//    }
					//}

					//CRViewer.Visible = true;
					//CRViewer.ReportSource = rpt;
					//CRViewer.Show();

					System.Drawing.Printing.PrintDocument printDoc = new  System.Drawing.Printing.PrintDocument();
					int i;
					int rawKind = 0;

					for (i=0;i<printDoc.PrinterSettings.PaperSizes.Count;i++)
					{
						if (printDoc.PrinterSettings.PaperSizes[i].PaperName == "RetailPlusLXHalfSize")
						{
							rawKind = (int)GetField(printDoc.PrinterSettings.PaperSizes[i], "kind");
							break;
						}
					}
					rpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawKind;
					rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
					rpt.PrintToPrinter(1, false, 0, 0);

					rpt.Close();
					rpt.Dispose();

				}
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddErrorEventLn(ex);
			}
		}
		private object GetField(Object obj, String fieldName)
		{
			System.Reflection.FieldInfo fi = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
			return fi.GetValue(obj);
		}

		private delegate void PrintDeliveryReceiptDelegate();
		private void PrintDeliveryReceipt()
		{
			try
			{
				if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
				{
					MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					return;
				}
				if (!mboIsInTransaction)
				{
					MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					return;
				}
				DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

				if (loginresult == DialogResult.None)
				{
					LogInWnd login = new LogInWnd();

					login.AccessType = AccessTypes.CloseTransaction;
					login.Header = "Print Delivery Receipt";
					login.ShowDialog(this);
					loginresult = login.Result;
					login.Close();
					login.Dispose();
				}
				if (loginresult == DialogResult.OK)
				{
					CRSReports.DR rpt = new CRSReports.DR();

					AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
					System.Data.DataRow drNew;

					/****************************sales transaction *****************************/
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
					Data.SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(lblTransNo.Text, mclsTerminalDetails.TerminalNo, Constants.TerminalBranchID);

					Data.Contacts clsContact = new Data.Contacts(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
					Data.ContactDetails clsContactDetails = clsContact.Details(clsSalesTransactionDetails.CustomerID);

					if (clsSalesTransactionDetails.isExist)
					{
						drNew = rptds.Transactions.NewRow();

						drNew["TransactionID"] = clsSalesTransactionDetails.TransactionID;
						drNew["TransactionNo"] = clsSalesTransactionDetails.TransactionNo;
						drNew["CustomerName"] = clsSalesTransactionDetails.CustomerName;
						drNew["CustomerAddress"] = clsContactDetails.Address;
						drNew["CustomerTerms"] = clsContactDetails.Terms;
						drNew["CustomerModeOfterms"] = clsContactDetails.ModeOfTerms;
						drNew["CustomerBusinessName"] = clsContactDetails.BusinessName;
						drNew["CustomerTelNo"] = clsContactDetails.TelephoneNo;
						drNew["CashierName"] = clsSalesTransactionDetails.CashierName;
						drNew["TerminalNo"] = clsSalesTransactionDetails.TerminalNo;
						drNew["TransactionDate"] = clsSalesTransactionDetails.TransactionDate;
						drNew["DateSuspended"] = clsSalesTransactionDetails.DateSuspended.ToString();
						drNew["DateResumed"] = clsSalesTransactionDetails.DateResumed;
						drNew["TransactionStatus"] = clsSalesTransactionDetails.TransactionStatus;
						drNew["SubTotal"] = clsSalesTransactionDetails.SubTotal;
						drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
						drNew["Discount"] = clsSalesTransactionDetails.Discount;
						drNew["VAT"] = clsSalesTransactionDetails.VAT;
						drNew["VatableAmount"] = clsSalesTransactionDetails.VatableAmount;
						drNew["LocalTax"] = clsSalesTransactionDetails.LocalTax;
						drNew["AmountPaid"] = clsSalesTransactionDetails.AmountPaid;
						drNew["CashPayment"] = clsSalesTransactionDetails.CashPayment;
						drNew["ChequePayment"] = clsSalesTransactionDetails.ChequePayment;
						drNew["CreditCardPayment"] = clsSalesTransactionDetails.CreditCardPayment;
						drNew["BalanceAmount"] = clsSalesTransactionDetails.BalanceAmount;
						drNew["ChangeAmount"] = clsSalesTransactionDetails.ChangeAmount;
						drNew["DateClosed"] = clsSalesTransactionDetails.DateClosed;
						drNew["PaymentType"] = clsSalesTransactionDetails.PaymentType.ToString("d");
						drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
						drNew["Charge"] = clsSalesTransactionDetails.Charge;

						rptds.Transactions.Rows.Add(drNew);

						/****************************sales transaction items*****************************/
						Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(clsSalesTransactions.Connection, clsSalesTransactions.Transaction);
						System.Data.DataTable dt = clsSalesTransactionItems.List(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, "TransactionItemsID", SortOption.Ascending);

                        foreach (System.Data.DataRow dr in dt.Rows)
						{
							drNew = rptds.SalesTransactionItems.NewRow();

							foreach (System.Data.DataColumn dc in rptds.SalesTransactionItems.Columns)
								drNew[dc] = dr[dc.ColumnName];

							rptds.SalesTransactionItems.Rows.Add(drNew);
						}
					}

					clsSalesTransactions.CommitAndDispose();

					rpt.SetDataSource(rptds);

					CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
					CrystalDecisions.Shared.ParameterValues currentValues;
					CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

					paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = CompanyDetails.CompanyName;
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = lblCashier.Text;
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = lblServedBy.Text.Remove(0, 11);
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
					discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
					discreteParam.Value = CompanyDetails.Address1 +
											Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
											Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #:" + CompanyDetails.FaxPhone +
											Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg.";
					currentValues = new CrystalDecisions.Shared.ParameterValues();
					currentValues.Add(discreteParam);
					paramField.ApplyCurrentValues(currentValues);

					//foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
					//{
					//    if (objPic.Name.ToUpper() == "PICLOGO1")
					//    {
					//        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
					//    }
					//}

					//CRViewer.Visible = true;
					//CRViewer.ReportSource = rpt;
					//CRViewer.Show();

					rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
					rpt.PrintToPrinter(1, false, 0, 0);

					rpt.Close();
					rpt.Dispose();

				}
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddErrorEventLn(ex);
			}
		}
		private void PrintTerminalZRead()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintZRead);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintZRead;
				login.Header = "Print ZRead Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				PrintZRead(true);
			}
		}
		private void PrintTerminalXRead()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintXRead);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintXRead;
				login.Header = "Print XRead Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				PrintXRead();
			}
		}
		private void PrintHourly()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintXRead);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintXRead;
				login.Header = "Print XRead Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				PrintHourlyReport();
			}
		}
		private void PrintGroup()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintGroupReport);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintGroupReport;
				login.Header = "Print Dept. Report Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				PrintGroupReport();
			}
		}
		private void PrintPLU()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintPLUReport;
				login.Header = "Print PLU Report Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				PrintPLUReport();
			}
		}
		private void PrintEJournal()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintElectronicJournal);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintElectronicJournal;
				login.Header = "Print EJournal Report Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				PrintEJournalReport();
			}
		}
		private void PrintPLUPerOrderSlipPrinter()
		{
			if (mboIsInTransaction)
			{
				if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SuspendTransaction(true)) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintPLUReport;
				login.Header = "Print PLU Report Access Validation";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				PrintPLUReportPerOrderSlipPrinter();
			}
		}
		private bool IsDateLastInitializationOK()
		{
			Event clsEvent = new Event();
			try
			{
				clsEvent.AddEvent("Checking last initialization date");

				Data.Database clsDatabase = new Data.Database();
				DateTime dtDateLastInitialized = clsDatabase.DateLastInitialized();

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

				Data.TerminalReport clsTerminalReport = new Data.TerminalReport(clsDatabase.Connection, clsDatabase.Transaction);
				DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

				DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime);
				DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime);

				// if StartCutOffTime is greater than EndCutOffTime
				// this means that EndCutOffTime is in the morning.
				// Add 1 more day.
				if (dteStartCutOffTime >= dteEndCutOffTime)
					dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

				DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
				if (dteTransactionDate < dteEndCutOffTime)
				{
					dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
					dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime);
				}
				else
				{
					dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
					dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
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

				DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.TerminalNo);

				clsEvent.AddEventLn("Checking if MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("MM/dd/yyyy HH:mm") + " is already initialized.", true);
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
					else if(mclsTerminalDetails.CheckCutOffTime == true)
					{
						clsEvent.AddEventLn("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE.", true);
						MessageBox.Show("Transaction is not allowed, trasnsaction date is 2Days delayed. Please restart FE." +
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

				boRetValue = true;

				return boRetValue;
			}
			catch (Exception ex)
			{
				clsEvent.AddErrorEventLn(ex);
				return false;
			}
		}
		private bool CreateTransaction()
		{
			Boolean boRetValue = true;
			try
			{
				Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
				DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

				// Added checking of Cutofftime
				if (mclsTerminalDetails.CheckCutOffTime)
				{
					DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime);
					DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime);

					// if StartCutOffTime is greater than EndCutOffTime
					// this means that EndCutOffTime is in the morning.
					// Add 1 more day.
					if (dteStartCutOffTime >= dteEndCutOffTime)
						dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

					DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
					if (dteTransactionDate < dteEndCutOffTime)
					{
						dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
						dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime);
					}
					else
					{
						dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
						dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
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

					clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if not between cutoff: " + dteStartCutOffTime.ToString("MM/dd/yyyy HH:mm") + " & " + dteEndCutOffTime.ToString("MM/dd/yyyy HH:mm"), true);
					if (dteTransactionDate >= dteStartCutOffTime && dteTransactionDate <= dteEndCutOffTime)
					{
						clsEvent.AddEventLn("Transaction is not allowed, transaction date is within the cutofftime.", true);
						MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtBarCode.Text = "";
						return false;
					}
					clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

					clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if between selling time: " + dteAllowedStartDateTime.ToString("MM/dd/yyyy HH:mm") + " & " + dteAllowedEndDateTime.ToString("MM/dd/yyyy HH:mm"), true);
					if (dteTransactionDate < dteAllowedStartDateTime && dteTransactionDate > dteAllowedEndDateTime)
					{
						clsEvent.AddEventLn("Transaction is not allowed, transaction date is not within the allowable transaction date.", true);
						MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtBarCode.Text = "";
						return false;
					}
					clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

					clsTerminalReport.GetConnection();
					DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.TerminalNo);

					clsEvent.AddEventLn("PreviousStartCutOff       :    " + dtePreviousStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("PreviousEndCutOff         :    " + dtePreviousEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("PrevAllowedStartDateTime  :    " + dtePreviousAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("PrevAllowedEndDateTime    :    " + dtePreviousAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);

					clsEvent.AddEventLn("StartCutOff               :    " + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("EndCutOff                 :    " + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("AllowedStartDateTime      :    " + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("AllowedEndDateTime        :    " + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("MAXDateLastInitialized    :    " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"), true);

					clsEvent.AddEventLn("Checking if MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("MM/dd/yyyy HH:mm") + " is already initialized.", true);
					if (dteMAXDateLastInitialized >= dteAllowedStartDateTime && dteMAXDateLastInitialized <= dteAllowedEndDateTime)
					{
						clsTerminalReport.CommitAndDispose();
						clsEvent.AddEventLn("Transaction is not allowed, ZRead is already initialized for this date.", true);
						MessageBox.Show("Sorry selling is not permitted this time, ZRead is already initialized for this date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtBarCode.Text = "";
						return false;
					}
					clsEvent.AddEventLn("OK. MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is for previous zreading.", true);
					clsEvent.AddEventLn("Checking Transactiondate: '" + dteMAXDateLastInitialized.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "' < " + dteAllowedStartDateTime.ToString("MM/dd/yyyy HH:mm"), true);
					if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
					{
						if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
						{
							clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
						}
						else
						{
							clsEvent.AddEventLn("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE.", true);
							MessageBox.Show("Transaction is not allowed, trasnsaction date is 2Days delayed. Please restart FE." +
								Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
							txtBarCode.Text = "";
							return false;
						}
					}
					if (dteMAXDateLastInitialized > dteTransactionDate)
					{
						clsEvent.AddEventLn("Transaction is not allowed, transaction date is delayed. Please restart FE.", true);
						MessageBox.Show("Transaction is not allowed, transaction date is delayed. Please restart FE." +
							Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtBarCode.Text = "";
						return false;
					}
					clsEvent.AddEventLn("OK to sell...", true);
				}

				clsEvent.AddEventLn("[" + lblCashier.Text + "] Creating new transaction.", true);

				mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
				try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
				catch { }

				mclsSalesTransactionDetails.CustomerID = Convert.ToInt64(lblCustomer.Tag);
				mclsSalesTransactionDetails.CustomerName = lblCustomer.Text;
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
				mclsSalesTransactionDetails.BranchID = Constants.TerminalBranchID;
				mclsSalesTransactionDetails.BranchCode = mclsTerminalDetails.BranchDetails.BranchCode;
				mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Open;
				
				mclsSalesTransactionDetails.TransactionNo = mclsTransactionStream.Create(mclsSalesTransactionDetails);
				lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;

				//insert to logfile
				mclsTransactionStream.AddTransactionHeader(mclsSalesTransactionDetails, mclsSalesTransactionDetails.TransactionDate);

				//insert to transaction table 
				clsTerminalReport.GetConnection();
				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsTerminalReport.Connection, clsTerminalReport.Transaction);
				mclsSalesTransactionDetails.TransactionID = clsSalesTransactions.Insert(mclsSalesTransactionDetails);

				Data.Contacts clsContact = new Data.Contacts(clsTerminalReport.Connection, clsTerminalReport.Transaction);
				Data.ContactDetails clsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
				mclsSalesTransactionDetails.RewardCardActive = clsContactDetails.RewardDetails.RewardActive;
				mclsSalesTransactionDetails.RewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
				mclsSalesTransactionDetails.RewardCardExpiry = clsContactDetails.RewardDetails.ExpiryDate;
				mclsSalesTransactionDetails.RewardPreviousPoints = clsContactDetails.RewardDetails.RewardPoints;

				clsTerminalReport.CommitAndDispose();

				lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();

				cmdPaxAdd.Visible = true;
				cmdPaxDeduct.Visible = true;

				mboIsInTransaction = true;

				InsertAuditLog(AccessTypes.CreateTransaction, "Create transaction #:" + lblTransNo.Text);
				clsEvent.AddEventLn("Done! Trans #: " + lblTransNo.Text + " has been created.", true);
			}
			catch (Exception ex)
			{ clsEvent.AddErrorEventLn(ex); boRetValue = false; }

			return boRetValue;
		}
		private void LoadTransaction(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, string stTransactionNo, string pstrTerminalNo)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				clsEvent.AddEvent("Loading transaction : " + stTransactionNo);

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(pvtConnection, pvtTransaction);
				mclsSalesTransactionDetails = clsSalesTransactions.Details(stTransactionNo, pstrTerminalNo, Constants.TerminalBranchID);

				Data.Contacts clsContact = new Data.Contacts(pvtConnection, pvtTransaction);
				Data.ContactDetails clsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
				mclsSalesTransactionDetails.RewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
				mclsSalesTransactionDetails.RewardCardExpiry = clsContactDetails.RewardDetails.ExpiryDate;
				mclsSalesTransactionDetails.RewardPreviousPoints = clsContactDetails.RewardDetails.RewardPoints;

				if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Refund)
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

				lblTransDiscount.Tag = mclsSalesTransactionDetails.TransDiscountType.ToString("d");
				cmdPaxAdd.Visible = true; cmdPaxDeduct.Visible = true;
				lblOrders.Text = Constants.C_RESTOPLUS_CUSTOMER_ORDERS + ": " + mclsSalesTransactionDetails.PaxNo.ToString() + " PAX";
				lblOrders.Tag = mclsSalesTransactionDetails.PaxNo.ToString();
				
				//mclsSalesTransactionDetails.ChargeAmount = mclsSalesTransactionDetails.ChargeAmount;
				if (mclsSalesTransactionDetails.ChargeAmount == 0)
					lblTransCharge.Tag = ChargeTypes.NotApplicable.ToString("d"); //details.TransDiscountType.ToString("d");
				else
					lblTransCharge.Tag = ChargeTypes.Percentage.ToString("d"); //details.TransDiscountType.ToString("d");

				//insert to logfile
				mclsTransactionStream.AddTransactionHeader(mclsSalesTransactionDetails, mclsSalesTransactionDetails.TransactionDate);

				Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems(pvtConnection, pvtTransaction);
				Data.SalesTransactionItemDetails[] TransactionItems = clsItems.Details(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate);

				clsEvent.AddEventLn("Done loading transaction : " + stTransactionNo, true);

				if (mclsTerminalDetails.AutoPrint != PrintingPreference.Auto)
					LoadResumedItems(TransactionItems, true);
				else
					LoadResumedItems(TransactionItems, false);

				

				mboIsInTransaction = true;
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

					mclsTransactionStream.AddItem(item, mclsSalesTransactionDetails.TransactionDate);

					if (WillPrintItem)
					{
						if (item.TransactionItemStatus == TransactionItemStatus.Return)
						{
							if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
								PrintItem(item.ItemNo, item.ProductCode + "-RET", item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
						}
						else if (item.TransactionItemStatus != TransactionItemStatus.Void)
						{
							if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
								PrintItem(item.ItemNo, item.ProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
						}
					}
					else
					{
						if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
						{
							if (item.TransactionItemStatus == TransactionItemStatus.Return)
							{
								if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
									PrintItem(item.ItemNo, item.ProductCode + "-RET", item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
							}
							else if (item.TransactionItemStatus != TransactionItemStatus.Void)
							{
								if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
									PrintItem(item.ItemNo, item.ProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
							}
						}
					}
				}
				if (ItemDataTable.Rows.Count != 0)
				{
					dgItems.CurrentRowIndex = 0;  //ItemDataTable.Rows.Count;
					dgItems.Select(dgItems.CurrentRowIndex);
				}

				clsEvent.AddEventLn("Done loading transaction items.", true);

				ComputeSubTotal();
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR! Loading transaction items. TRACE: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#region UpdateTerminalReport
		
		//private delegate void UpdateTerminalReportDelegate(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, TransactionStatus TransStatus, decimal SubTotal, decimal Discount, decimal Charge, decimal VAT, decimal VatableAmount, decimal NonVatableAmount, decimal EVAT, decimal EVatableAmount, decimal NonEVatableAmount, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, PaymentTypes PaymentType);
		private void UpdateTerminalReport(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, TransactionStatus TransStatus, decimal SubTotal, decimal Discount, decimal Charge, decimal VAT, decimal VatableAmount, decimal NonVatableAmount, decimal EVAT, decimal EVatableAmount, decimal NonEVatableAmount, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, PaymentTypes PaymentType)
		{
			decimal TotalNoOfItems = 0;
			Int32 intNoOfCashTransactions = 0;
			Int32 intNoOfChequeTransactions = 0;
			Int32 intNoOfCreditCardTransactions = 0;
			Int32 intNoOfCreditTransactions = 0;
			Int32 intNoOfDebitTransactions = 0;
			Int32 intNoOfCombinationPaymentTransactions = 0;
			Int32 intNoOfDiscountedTransactions = 0;
			Int32 intNoOfRewardPointsPayment = 0;
			decimal ItemsDiscount = 0;
			decimal decPromotionalItems = 0;
			decimal decSeniorCitizenDiscount = 0;

			foreach (System.Data.DataRow dr in ItemDataTable.Rows)
			{

				decimal ItemQuantity = 0;
				try { ItemQuantity = Convert.ToDecimal(dr["Quantity"]); }
				catch
				{
					try { ItemQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace("RETURN", "").Trim()); }
					catch { }
				}

				decPromotionalItems += Convert.ToDecimal(dr["PromoApplied"]);

				DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString().ToString());

				if (ItemDiscountType != DiscountTypes.NotApplicable)
				{ ItemsDiscount += Convert.ToDecimal(dr["Discount"]); }

				if (dr["DiscountCode"].ToString() == Constants.SeniorCitizenDiscountCode)
					decSeniorCitizenDiscount += ItemsDiscount;
				
				TotalNoOfItems += ItemQuantity;
			}

			switch (PaymentType)
			{
				case PaymentTypes.Cash: { intNoOfCashTransactions = 1; break; }
				case PaymentTypes.Cheque: { intNoOfChequeTransactions = 1; break; }
				case PaymentTypes.CreditCard: { intNoOfCreditCardTransactions = 1; break; }
				case PaymentTypes.Credit: { intNoOfCreditTransactions = 1; break; }
				case PaymentTypes.Debit: { intNoOfDebitTransactions=1; break; }
				case PaymentTypes.RewardPoints: { intNoOfRewardPointsPayment = 1; break; }
				case PaymentTypes.Combination: { intNoOfCombinationPaymentTransactions =1; break; }
			}
			if (Discount != 0)
			{   
				intNoOfDiscountedTransactions = 1;
				if (mclsSalesTransactionDetails.DiscountCode == Constants.SeniorCitizenDiscountCode)
					decSeniorCitizenDiscount += Discount;
			}
			
			Data.TerminalReportDetails clsTerminalReportDetails = new Data.TerminalReportDetails();

			if (TransStatus == TransactionStatus.Closed)
			{
				clsTerminalReportDetails.QuantitySold = TotalNoOfItems;
				clsTerminalReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
				clsTerminalReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
				clsTerminalReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
				clsTerminalReportDetails.NoOfCreditTransactions = intNoOfCreditTransactions;
				clsTerminalReportDetails.NoOfDebitPaymentTransactions = intNoOfDebitTransactions;
				clsTerminalReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
				clsTerminalReportDetails.NoOfRewardPointsPayment = intNoOfRewardPointsPayment;
				clsTerminalReportDetails.NoOfClosedTransactions = 1;
				clsTerminalReportDetails.CashSales = CashPayment;
				clsTerminalReportDetails.ChequeSales = ChequePayment;
				clsTerminalReportDetails.CreditCardSales = CreditCardPayment;
				clsTerminalReportDetails.CreditSales = CreditPayment;
				clsTerminalReportDetails.DebitPayment = DebitPayment;
				clsTerminalReportDetails.RewardPointsPayment = RewardPointsPayment;
				clsTerminalReportDetails.RewardConvertedPayment = RewardConvertedPayment;

				clsTerminalReportDetails.ItemsDiscount = ItemsDiscount;
				clsTerminalReportDetails.SubTotalDiscount = Discount;
				clsTerminalReportDetails.DailySales = VatableAmount + NonVatableAmount;
				clsTerminalReportDetails.GrossSales = VatableAmount + NonVatableAmount + ItemsDiscount + Discount ;
				clsTerminalReportDetails.GroupSales = VatableAmount + NonVatableAmount ;
				clsTerminalReportDetails.TotalDiscount = ItemsDiscount + Discount;
				clsTerminalReportDetails.TotalCharge = Charge;
				clsTerminalReportDetails.VAT = VAT;
				clsTerminalReportDetails.VATableAmount = VatableAmount;
				clsTerminalReportDetails.NonVaTableAmount = NonVatableAmount;
				clsTerminalReportDetails.EVAT = EVAT;
				clsTerminalReportDetails.EVATableAmount = EVatableAmount;
				clsTerminalReportDetails.NonEVaTableAmount = NonEVatableAmount;
				clsTerminalReportDetails.LocalTax = LocalTax;

				// march 19, 2009
				clsTerminalReportDetails.NoOfDiscountedTransactions = intNoOfDiscountedTransactions;
				clsTerminalReportDetails.CreditSalesTax = clsTerminalReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100);
				clsTerminalReportDetails.PromotionalItems = decPromotionalItems;
			}
			else if (TransStatus == TransactionStatus.Void)
			{
				clsTerminalReportDetails.NoOfVoidTransactions = 1;
				clsTerminalReportDetails.VoidSales = VatableAmount + NonVatableAmount + VAT + EVAT + LocalTax + Charge ;

				//				****************************************remove on may 31, 2006 no use*****************************	
				//				clsTerminalReportDetails.GrossSales = SubTotal + ItemsDiscount;
			}
			else if (TransStatus == TransactionStatus.Refund)
			{
				clsTerminalReportDetails.QuantitySold = -TotalNoOfItems;
				clsTerminalReportDetails.NoOfRefundTransactions = 1;
				clsTerminalReportDetails.RefundSales = VatableAmount + NonVatableAmount + Charge ;
				clsTerminalReportDetails.CashSales = -CashPayment;
				clsTerminalReportDetails.ChequeSales = -ChequePayment;
				clsTerminalReportDetails.CreditCardSales = -CreditCardPayment;
				clsTerminalReportDetails.CreditSales = -CreditPayment;
				clsTerminalReportDetails.DebitPayment = -DebitPayment;

				clsTerminalReportDetails.ItemsDiscount = -ItemsDiscount;
				clsTerminalReportDetails.SubTotalDiscount = -Discount;
				clsTerminalReportDetails.DailySales = -(VatableAmount + NonVatableAmount );
				clsTerminalReportDetails.GrossSales = -(VatableAmount + NonVatableAmount + ItemsDiscount + Discount );
				clsTerminalReportDetails.GroupSales = -(VatableAmount + NonVatableAmount );
				clsTerminalReportDetails.TotalDiscount = -(ItemsDiscount + Discount);
				clsTerminalReportDetails.TotalCharge = -Charge;
				clsTerminalReportDetails.VAT = -VAT;
				clsTerminalReportDetails.VATableAmount = -VatableAmount;
				clsTerminalReportDetails.NonVaTableAmount = -(NonVatableAmount);
				clsTerminalReportDetails.EVAT = -EVAT;
				clsTerminalReportDetails.EVATableAmount = -EVatableAmount;
				clsTerminalReportDetails.NonEVaTableAmount = -(NonEVatableAmount);
				clsTerminalReportDetails.LocalTax = -LocalTax;

				// march 19, 2009
				clsTerminalReportDetails.CreditSalesTax = -(clsTerminalReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100));
			}
			else if (TransStatus == TransactionStatus.CreditPayment)
			{
				clsTerminalReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
				clsTerminalReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
				clsTerminalReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
				clsTerminalReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
				clsTerminalReportDetails.NoOfCreditPaymentTransactions = 1;
				clsTerminalReportDetails.CashSales = CashPayment;
				clsTerminalReportDetails.ChequeSales = ChequePayment;
				clsTerminalReportDetails.CreditCardSales = CreditCardPayment;
				clsTerminalReportDetails.DebitPayment = DebitPayment;
				clsTerminalReportDetails.CreditPayment = SubTotal + Charge;

				// march 19, 2009
				clsTerminalReportDetails.CreditSalesTax = clsTerminalReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100);
				clsTerminalReportDetails.PromotionalItems = decPromotionalItems;
				/// may 28, 2006 remove from daily sales and gross sales 
				/// since it was already declared during the credit payment of transaction
				//				clsTerminalReportDetails.DailySales = SubTotal;
				//				clsTerminalReportDetails.GrossSales = SubTotal + ItemsDiscount;
			}
			clsTerminalReportDetails.NoOfTotalTransactions = 1;

			clsTerminalReportDetails.TerminalNo = mclsTerminalDetails.TerminalNo;

			Data.TerminalReport clsTerminalReport = new Data.TerminalReport(pvtConnection, pvtTransaction);
			clsTerminalReport.UpdateTransactionSales(clsTerminalReportDetails);
		}

		#endregion

		#region UpdateCashierReport
		
		//private delegate void UpdateCashierReportDelegate(TransactionStatus TransStatus, decimal SubTotal, decimal Discount, decimal Charge, decimal VAT, decimal VatableAmount, decimal NonVatableAmount, decimal EVAT, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, PaymentTypes PaymentType);
		private void UpdateCashierReport(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, TransactionStatus TransStatus, decimal SubTotal, decimal Discount, decimal Charge, decimal VAT, decimal VatableAmount, decimal NonVatableAmount, decimal EVAT, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, PaymentTypes PaymentType)
		{
			//			decimal   NoOfItemsDiscounted = 0;
			//			decimal   NoOfItemsSold = 0;
			//			decimal   NoOfItemsVoid = 0;
			//			decimal   NoOfItemsReturned = 0;
			//			decimal   NoOfItemsSuspended = 0;
			//			decimal   NoOfItemsRefunded = 0;
			decimal TotalNoOfItems = 0;
			Int32 intNoOfCashTransactions = 0;
			Int32 intNoOfChequeTransactions = 0;
			Int32 intNoOfCreditCardTransactions = 0;
			Int32 intNoOfCreditTransactions = 0;
			Int32 intNoOfDebitTransactions = 0;
			Int32 intNoOfCombinationPaymentTransactions = 0;
			Int32 intNoOfDiscountedTransactions = 0;
			Int32 intNoOfRewardPointsPayment = 0;
			decimal ItemsDiscount = 0;
			decimal decPromotionalItems = 0;
			decimal decSeniorCitizenDiscount = 0;

			foreach (System.Data.DataRow dr in ItemDataTable.Rows)
			{

				decimal ItemQuantity = 0;
				try { ItemQuantity = Convert.ToDecimal(dr["Quantity"]); }
				catch
				{
					try { ItemQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace("RETURN", "").Trim()); }
					catch { }
				}

				decPromotionalItems += Convert.ToDecimal(dr["PromoApplied"]);

				DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString().ToString());

				if (ItemDiscountType != DiscountTypes.NotApplicable)
				{
					ItemsDiscount += Convert.ToDecimal(dr["Discount"]);
				}

				if (dr["DiscountCode"].ToString() == Constants.SeniorCitizenDiscountCode)
					decSeniorCitizenDiscount += ItemsDiscount;

				TotalNoOfItems += ItemQuantity;
			}
			switch (PaymentType)
			{
				case PaymentTypes.Cash: { intNoOfCashTransactions = 1; break; }
				case PaymentTypes.Cheque: { intNoOfChequeTransactions = 1; break; }
				case PaymentTypes.CreditCard: { intNoOfCreditCardTransactions = 1; break; }
				case PaymentTypes.Credit: { intNoOfCreditTransactions = 1; break; }
				case PaymentTypes.Debit: { intNoOfDebitTransactions = 1; break; }
				case PaymentTypes.RewardPoints: { intNoOfRewardPointsPayment = 1; break; }
				case PaymentTypes.Combination: { intNoOfCombinationPaymentTransactions = 1; break; }
			}
			if (Discount != 0)
			{
				intNoOfDiscountedTransactions = 1;
				if (mclsSalesTransactionDetails.DiscountCode == Constants.SeniorCitizenDiscountCode)
					decSeniorCitizenDiscount += Discount;
			}

			Data.CashierReportDetails clsCashierReportDetails = new Data.CashierReportDetails();

			if (TransStatus == TransactionStatus.Closed)
			{
				clsCashierReportDetails.QuantitySold = TotalNoOfItems;
				clsCashierReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
				clsCashierReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
				clsCashierReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
				clsCashierReportDetails.NoOfCreditTransactions = intNoOfCreditTransactions;
				clsCashierReportDetails.NoOfDebitPaymentTransactions = intNoOfDebitTransactions;
				clsCashierReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
				clsCashierReportDetails.NoOfRewardPointsPayment = intNoOfRewardPointsPayment;
				clsCashierReportDetails.NoOfClosedTransactions = 1;
				clsCashierReportDetails.CashSales = CashPayment;
				clsCashierReportDetails.ChequeSales = ChequePayment;
				clsCashierReportDetails.CreditCardSales = CreditCardPayment;
				clsCashierReportDetails.CreditSales = CreditPayment;
				clsCashierReportDetails.DebitPayment = DebitPayment;
				clsCashierReportDetails.RewardPointsPayment = RewardPointsPayment;
				clsCashierReportDetails.RewardConvertedPayment = RewardConvertedPayment;

				clsCashierReportDetails.ItemsDiscount = ItemsDiscount;
				clsCashierReportDetails.SubTotalDiscount = Discount;
				clsCashierReportDetails.DailySales = VatableAmount + NonVatableAmount ;
				clsCashierReportDetails.GrossSales = VatableAmount + NonVatableAmount + ItemsDiscount + Discount ;
				clsCashierReportDetails.GroupSales = VatableAmount + NonVatableAmount ;
				clsCashierReportDetails.TotalDiscount = ItemsDiscount + Discount;
				clsCashierReportDetails.TotalCharge = Charge;
				clsCashierReportDetails.VAT = VAT;
				clsCashierReportDetails.EVAT = EVAT;
				clsCashierReportDetails.LocalTax = LocalTax;

				// march 19, 2009
				clsCashierReportDetails.NoOfDiscountedTransactions = intNoOfDiscountedTransactions;
				clsCashierReportDetails.CreditSalesTax = clsCashierReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100);
				clsCashierReportDetails.PromotionalItems = decPromotionalItems;
			}
			else if (TransStatus == TransactionStatus.Void)
			{
				clsCashierReportDetails.NoOfVoidTransactions = 1;
				clsCashierReportDetails.VoidSales = VatableAmount + NonVatableAmount + VAT + EVAT + LocalTax + Charge ;
				//				****************************************remove on may 31, 2006 no use*****************************	
				//				clsCashierReportDetails.GrossSales = SubTotal + ItemsDiscount;
			}
			else if (TransStatus == TransactionStatus.Refund)
			{
				clsCashierReportDetails.QuantitySold = -TotalNoOfItems;
				clsCashierReportDetails.NoOfRefundTransactions = 1;
				clsCashierReportDetails.RefundSales = VatableAmount + NonVatableAmount + Charge ;
				clsCashierReportDetails.CashSales = -CashPayment;
				clsCashierReportDetails.ChequeSales = -ChequePayment;
				clsCashierReportDetails.CreditCardSales = -CreditCardPayment;
				clsCashierReportDetails.CreditSales = -CreditPayment;
				clsCashierReportDetails.DebitPayment = -DebitPayment;

				clsCashierReportDetails.ItemsDiscount = -ItemsDiscount;
				clsCashierReportDetails.SubTotalDiscount = -Discount;
				clsCashierReportDetails.DailySales = -(VatableAmount + NonVatableAmount );
				clsCashierReportDetails.GrossSales = -(VatableAmount + NonVatableAmount + ItemsDiscount + Discount );
				clsCashierReportDetails.GroupSales = -(VatableAmount + NonVatableAmount );
				clsCashierReportDetails.TotalDiscount = -(ItemsDiscount + Discount);
				clsCashierReportDetails.TotalCharge = -Charge;
				clsCashierReportDetails.VAT = -VAT;
				clsCashierReportDetails.EVAT = -EVAT;
				clsCashierReportDetails.LocalTax = -LocalTax;
			}
			else if (TransStatus == TransactionStatus.CreditPayment)
			{
				clsCashierReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
				clsCashierReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
				clsCashierReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
				clsCashierReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
				clsCashierReportDetails.NoOfCreditPaymentTransactions = 1;
				clsCashierReportDetails.CashSales = CashPayment;
				clsCashierReportDetails.ChequeSales = ChequePayment;
				clsCashierReportDetails.CreditCardSales = CreditCardPayment;
				clsCashierReportDetails.DebitPayment = DebitPayment;
				clsCashierReportDetails.CreditPayment = SubTotal + Charge;
				
				/// may 28, 2006 remove from daily and gross sales 
				/// since it was already declared during the credit payment of transaction
				//	clsCashierReportDetails.DailySales = SubTotal;
				//	clsCashierReportDetails.GrossSales = SubTotal + ItemsDiscount;

				// march 19, 2009
				clsCashierReportDetails.CreditSalesTax = clsCashierReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100);
				clsCashierReportDetails.PromotionalItems = decPromotionalItems;
			}
			clsCashierReportDetails.NoOfTotalTransactions = 1;

			clsCashierReportDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
			clsCashierReportDetails.CashierID = mclsSalesTransactionDetails.CashierID;

			Data.CashierReport clsCashierReport = new Data.CashierReport(pvtConnection, pvtTransaction);
			clsCashierReport.UpdateTransactionSales(clsCashierReportDetails);
		}

		#endregion

		private DialogResult GetWriteAccess(long UID, AccessTypes accesstype)
		{
			DialogResult resRetValue = DialogResult.None;

			AccessRights clsAccessRights = new AccessRights();
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID, (Int16)accesstype);

			if (clsDetails.Write)
			{
				resRetValue = DialogResult.OK;
			}

			clsAccessRights.CommitAndDispose();

			return resRetValue;
		}
		private void SavePayments(ArrayList arrCashPaymentDetails, ArrayList arrChequePaymentDetails, ArrayList arrCreditCardPaymentDetails, ArrayList arrCreditPaymentDetails, ArrayList arrDebitPaymentDetails, MySqlConnection pvtConnection, MySqlTransaction pvtTransaction)
		{
			Data.CashPaymentDetails[] CashPaymentDetails = new Data.CashPaymentDetails[0];
			if (arrCashPaymentDetails != null)
			{
				CashPaymentDetails = new Data.CashPaymentDetails[arrCashPaymentDetails.Count];
				arrCashPaymentDetails.CopyTo(CashPaymentDetails);
			}

			Data.ChequePaymentDetails[] ChequePaymentDetails = new Data.ChequePaymentDetails[0];
			if (arrChequePaymentDetails != null)
			{
				ChequePaymentDetails = new Data.ChequePaymentDetails[arrChequePaymentDetails.Count];
				arrChequePaymentDetails.CopyTo(ChequePaymentDetails);
			}

			Data.CreditCardPaymentDetails[] CreditCardPaymentDetails = new Data.CreditCardPaymentDetails[0];
			if (arrCreditCardPaymentDetails != null)
			{
				CreditCardPaymentDetails = new Data.CreditCardPaymentDetails[arrCreditCardPaymentDetails.Count];
				arrCreditCardPaymentDetails.CopyTo(CreditCardPaymentDetails);
			}

			Data.CreditPaymentDetails[] CreditPaymentDetails = new Data.CreditPaymentDetails[0];
			if (arrCreditPaymentDetails != null)
			{
				CreditPaymentDetails = new Data.CreditPaymentDetails[arrCreditPaymentDetails.Count];
				arrCreditPaymentDetails.CopyTo(CreditPaymentDetails);
			}

			Data.DebitPaymentDetails[] DebitPaymentDetails = new Data.DebitPaymentDetails[0];
			if (arrDebitPaymentDetails != null)
			{
				DebitPaymentDetails = new Data.DebitPaymentDetails[arrDebitPaymentDetails.Count];
				arrDebitPaymentDetails.CopyTo(DebitPaymentDetails);
			}
			if (mboIsRefund)
			{
				// Lemu 2011-06-09 : Added saving of debit payments as deposit if refund. Requested by Frank.
				Data.Deposit clsDeposit = new Data.Deposit(pvtConnection, pvtTransaction);
				Data.DepositDetails clsDepositDetails = new Data.DepositDetails();
				foreach(Data.DebitPaymentDetails clsDebitPaymentDetails in DebitPaymentDetails)
				{
					clsDepositDetails = new Data.DepositDetails();
					clsDepositDetails.Amount = clsDebitPaymentDetails.Amount;
					clsDepositDetails.PaymentType = PaymentTypes.Debit;
					clsDepositDetails.DateCreated = mclsSalesTransactionDetails.TransactionDate;
					clsDepositDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
					clsDepositDetails.CashierID = mclsSalesTransactionDetails.CashierID;
					clsDepositDetails.ContactID = mclsSalesTransactionDetails.CustomerID;
					clsDepositDetails.ContactName = mclsSalesTransactionDetails.CustomerName;
					clsDepositDetails.Remarks = "Added during refund of transaction #: " + mclsSalesTransactionDetails.TransactionNo;
					
					clsDeposit.Insert(clsDepositDetails);
					Data.Contacts clsContact = new Data.Contacts(clsDeposit.Connection, clsDeposit.Transaction);
					clsContact.AddDebit(clsDepositDetails.ContactID, clsDepositDetails.Amount);
				}

				InsertAuditLog(clsDeposit.Connection, clsDeposit.Transaction, AccessTypes.Deposit, "Deposit: type='" + clsDepositDetails.PaymentType.ToString("G") + "' amount='" + clsDepositDetails.Amount.ToString(",##0.#0") + "'");

				// Remove Debit Payments so that it wont be saved in the debit payment table
				DebitPaymentDetails = new Data.DebitPaymentDetails[0];
			}

			Data.PaymentDetails Details = new Data.PaymentDetails();
			Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);
			Details.arrCashPaymentDetails = CashPaymentDetails;
			Details.arrChequePaymentDetails = ChequePaymentDetails;
			Details.arrCardPaymentDetails = CreditCardPaymentDetails;
			Details.arrCreditPaymentDetails = CreditPaymentDetails;
			Details.arrDebitPaymentDetails = DebitPaymentDetails;

			Data.Payment clsPayment = new Data.Payment(pvtConnection, pvtTransaction);
			clsPayment.Insert(Details);
		}
		private bool IsStartCutOffTimeOK()
		{
			bool bolRetValue = false;
			try
			{
				// check if with Cutofftime
				if (mclsTerminalDetails.CheckCutOffTime)
				{
					clsEvent.AddEventLn("Checking StartCutOffTime vs MAXDateLastInitialized...", true);

					Data.TerminalReportDetails clsTerminalReportDetails;
					Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
					DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

					DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.TerminalNo);
					DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime);
					DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime);

					// if StartCutOffTime is greater than EndCutOffTime
					// this means that EndCutOffTime is in the morning.
					// Add 1 more day.
					if (dteStartCutOffTime >= dteEndCutOffTime)
						dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

					DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
					if (dteTransactionDate < dteEndCutOffTime)
					{
						dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
						dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime);
					}
					else
					{
						dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
						dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("MM/dd/yyyy") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
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
								MessageBox.Show("Today's EOD was not performed", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

								clsEvent.AddEventLn("Today's EOD was not performed: System found AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] > MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
								clsEvent.AddEventLn("System is Auto-Initializing ZREAD", true);

								clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.TerminalNo);
								PrintZRead(false, clsTerminalReportDetails);

								dteMAXDateLastInitialized = Convert.ToDateTime(dteMAXDateLastInitialized.AddDays(1));
								clsTerminalReport.InitializeZRead(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo, dteMAXDateLastInitialized, "SYSTEM Auto Z-Read");
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
							MessageBox.Show("Previous' day's EOD was not performed", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

							clsEvent.AddEventLn("Previous' day's EOD was not performed: System found AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] > MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
							clsEvent.AddEventLn("System is Auto-Initializing ZREAD", true);

							clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.TerminalNo);
							PrintZRead(false, clsTerminalReportDetails);

							dteMAXDateLastInitialized = Convert.ToDateTime(dteMAXDateLastInitialized.AddDays(1));
							clsTerminalReport.InitializeZRead(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo, dteMAXDateLastInitialized, "SYSTEM Auto Z-Read");
							clsEvent.AddEventLn("Done.",true);
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

				clsEvent.AddErrorEventLn(ex);
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
				DateTime dteDateToProcess = clsTerminalReportHistory.getRLCDateLastInitialized(mclsTerminalDetails.TerminalNo);
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
						bool bolCreateAndTransferFile = clsRLC.CreateAndTransferFile(dteDateToProcess);

						if (bolCreateAndTransferFile)
						{ tmrRLC.Enabled = false; lblMallForwarderStatus.Text = "Success in sending file(s) to RLC server. Press [Ctrl+C] to close this notification."; }
						else
						{ tmrRLC.Enabled = true; lblMallForwarderStatus.Text = "Error sending file(s) to RLC server. Press [Ctrl+C] to close this notification."; }

						clsTerminalReportHistory = new Data.TerminalReportHistory();
						dteDateToProcess = clsTerminalReportHistory.getRLCDateLastInitialized(mclsTerminalDetails.TerminalNo);
						clsTerminalReportHistory.CommitAndDispose();

						if (dteDateToProcess != DateTime.MinValue)
						{ goto Back; }
					}
					catch (Exception ex) { tmrRLC.Enabled = true; lblMallForwarderStatus.Text = "Error sending file(s) to RLC server. Press [Ctrl+C] to close this notification."; }
				}
			}
			catch { }
		}

		#endregion

		#region Printing

		/***********************************************
		* Printing
		* 
		* PrintReportValue
		* PrintReportHeadersSection
		* 
		***********************************************/

		private void PrintReportValue(Reports.ReceiptDetails clsReceiptDetails, bool IsReceipt)
		{
			if (clsReceiptDetails.Value == ReceiptFieldFormats.CustomerName && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.WaiterName && mclsSalesTransactionDetails.WaiterID == Constants.C_RETAILPLUS_WAITERID)
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.DiscountCode && (mclsSalesTransactionDetails.DiscountCode == null || mclsSalesTransactionDetails.DiscountCode == string.Empty))
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.DiscountRemarks && (mclsSalesTransactionDetails.DiscountRemarks == null || mclsSalesTransactionDetails.DiscountRemarks == string.Empty))
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.ChargeCode && (mclsSalesTransactionDetails.ChargeCode == null || mclsSalesTransactionDetails.ChargeCode == string.Empty))
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.ChargeRemarks && (mclsSalesTransactionDetails.ChargeRemarks == null || mclsSalesTransactionDetails.ChargeRemarks == string.Empty))
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardCardNo && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardPreviousPoints && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardEarnedPoints && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				return;
			else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardCurrentPoints && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				return;

			if ((clsReceiptDetails.Text != "" && clsReceiptDetails.Text != null) || (clsReceiptDetails.Value != "" && clsReceiptDetails.Value != null))
			{
				switch (clsReceiptDetails.Orientation)
				{
					case ReportFormatOrientation.Justify:
						if (clsReceiptDetails.Text == "" || clsReceiptDetails.Text == null)
							mstrToPrint +=GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt) + Environment.NewLine; 
						else
						{
							if (clsReceiptDetails.Value == ReceiptFieldFormats.AmountDue && !mclsTerminalDetails.IsPrinterDotMatrix)
								mstrToPrint +=RawPrinterHelper.esc8CpiOn + RawPrinterHelper.escBoldOn + clsReceiptDetails.Text.PadRight(10) + RawPrinterHelper.escAlignRight + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 10) + RawPrinterHelper.esc8CpiOff + RawPrinterHelper.escBoldOff + Environment.NewLine; 
							else if (clsReceiptDetails.Value == ReceiptFieldFormats.Change && !mclsTerminalDetails.IsPrinterDotMatrix)
								mstrToPrint +=RawPrinterHelper.escBoldOn + clsReceiptDetails.Text.PadRight(10) + RawPrinterHelper.escAlignRight + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 10) + RawPrinterHelper.escBoldOff + Environment.NewLine; 
							else
								mstrToPrint +=clsReceiptDetails.Text.PadRight(13) + ":" + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14) + Environment.NewLine; 
						}
						break;
					case ReportFormatOrientation.Center:
						if (clsReceiptDetails.Text == "" || clsReceiptDetails.Text == null)
							mstrToPrint +=CenterString(GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
						else
							if (clsReceiptDetails.Value == ReceiptFieldFormats.AmountDue && !mclsTerminalDetails.IsPrinterDotMatrix)
								mstrToPrint +=RawPrinterHelper.esc8CpiOn + RawPrinterHelper.escBoldOn + RawPrinterHelper.escAlignCenter + clsReceiptDetails.Text + " : " + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt) + RawPrinterHelper.escAlignLeft + RawPrinterHelper.esc8CpiOff + RawPrinterHelper.escBoldOff + Environment.NewLine;
							else if (clsReceiptDetails.Value == ReceiptFieldFormats.Change && !mclsTerminalDetails.IsPrinterDotMatrix)
								mstrToPrint +=RawPrinterHelper.escBoldOn + RawPrinterHelper.escAlignCenter + clsReceiptDetails.Text + " : " + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt) + RawPrinterHelper.escAlignLeft + RawPrinterHelper.escBoldOff + Environment.NewLine;
							else
								mstrToPrint +=CenterString(clsReceiptDetails.Text + " : " + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
						
						break;
				}
			}
		}
		private void PrintReportPageHeaderSection(bool IsReceipt)
		{
			int iCtr = 0;
			string stModule = "";
			Receipt clsReceipt = new Receipt();
			ReceiptDetails clsReceiptDetails;

			// print Page Header
			for (iCtr = 1; iCtr <= 10; iCtr++)
			{
				stModule = "PageHeader" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);

				PrintReportValue(clsReceiptDetails, IsReceipt);
			}

			PrintItemHeader();

			clsReceipt.CommitAndDispose();
		}
		private void PrintReportHeadersSection(bool IsReceipt)
		{
			PrintReportHeaderSection(IsReceipt);
			PrintReportPageHeaderSectionChecked(IsReceipt);
		}
		private void PrintReportHeaderSection(bool IsReceipt)
		{
			//PosExplorer posExplorer = new PosExplorer();
			//DeviceInfo deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, mclsTerminalDetails.PrinterName);
			//m_Printer = (PosPrinter) posExplorer.CreateInstance(deviceInfo);

			//m_Printer.Open();
			////Then the device is disable from other application
			//m_Printer.Claim(1000);
			////Enable the device.
			//m_Printer.DeviceEnabled = true;

			////'Output by the high quality mode

			//m_Printer.RecLetterQuality = true;

			////'Release the exclusive control right

			//m_Printer.Release();

			////m_Printer.SetBitmap(1, PrinterStation.Receipt, strFilePath, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter);

			int iCtr = 0;
			string stModule = "";
			mstrToPrint = string.Empty; // reset the transaction to print in POSPrinter

			Reports.Receipt clsReceipt = new Reports.Receipt();
			Reports.ReceiptDetails clsReceiptDetails;

			clsReceiptDetails = clsReceipt.Details(ReportFormatModule.ReportHeaderSpacer);

			// print Report Header Spacer
			for (int i = 0; i < Convert.ToInt32(clsReceiptDetails.Value); i++)
			{ mstrToPrint += Environment.NewLine; }

			mclsFilePrinter = new FilePrinter();
			if (lblTransNo.Text == string.Empty || lblTransNo.Text == "" || lblTransNo.Text == "READY..." ||  lblTransNo.Text == mclsFilePrinter.FileName)
				mclsFilePrinter.FileName = lblTransNo.Text.Replace("READY...","") + DateTime.Now.ToString("MMddyyyyhhmmss");
			else
				mclsFilePrinter.FileName = lblTransNo.Text;

			if (mclsTerminalDetails.IsPrinterDotMatrix) mstrToPrint += CenterString(CompanyDetails.CompanyCode, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
			else mstrToPrint += RawPrinterHelper.esc8CpiOn + RawPrinterHelper.escBoldOn + RawPrinterHelper.escAlignCenter + CompanyDetails.CompanyCode + RawPrinterHelper.esc8CpiOff + RawPrinterHelper.escBoldOff + RawPrinterHelper.escAlignRight + Environment.NewLine;

			// print Report Header
			for (iCtr = 1; iCtr <= 10; iCtr++)
			{
				stModule = "ReportHeader" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);

				PrintReportValue(clsReceiptDetails, IsReceipt);
			}
			clsReceipt.CommitAndDispose();
		}

		/************************
		 * use for printing the PageHeader 
		 * if the configuration is Autocutter
		 * **********************/
		private void PrintReportPageHeaderSectionChecked(bool IsReceipt)
		{
			if (IsReceipt)
			{
				// print page header <-- second transaction header
				int iCtr = 0;
				string stModule = "";

				Receipt clsReceipt = new Receipt();
				ReceiptDetails clsReceiptDetails;

				for (iCtr = 1; iCtr <= 10; iCtr++)
				{
					stModule = "PageHeader" + iCtr;
					clsReceiptDetails = clsReceipt.Details(stModule);

					PrintReportValue(clsReceiptDetails, IsReceipt);
				}

				PrintItemHeader();

				clsReceipt.CommitAndDispose();
			}
		}

		private void PrintItemHeader()
		{
			mstrToPrint += Environment.NewLine;
			mstrToPrint += "DESC".PadRight(mclsTerminalDetails.MaxReceiptWidth - 29);
			mstrToPrint += "QTY".PadLeft(6);
			mstrToPrint += "PRICE".PadLeft(10);
			mstrToPrint += "AMOUNT".PadLeft(13);
			mstrToPrint += Environment.NewLine;
			mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
		}
		private void PrintItemForKitchen(string Description, string stProductUnitCode, decimal Quantity, string PrinterName)
		{
			// description
			string stDescription = Description;
			try
			{ stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
			catch { }
			try
			{ stDescription = stDescription.Substring(0, mclsTerminalDetails.MaxReceiptWidth); }
			catch { }

			SendOrderSlipToPrinter(stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
			SendOrderSlipToPrinter(stProductUnitCode.PadRight(6), PrinterName);

			string stQuantity = Quantity.ToString("#,##0.#0");
			if (Quantity == 1 || Quantity == -1)
				stQuantity = "1";
			else if (Decimal.Compare(Quantity, Decimal.Floor(Quantity)) == 0)
			{ stQuantity = Quantity.ToString("#,##0"); }

			SendOrderSlipToPrinter(stQuantity.PadLeft(6), PrinterName);
			SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
		}
		private void PrintItemForKitchen(string Description, string stProductUnitCode, decimal Quantity, string PrinterName, bool bolBIG)
		{
			// description
			string stDescription = Description;
			try
			{ stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
			catch { }
			try
			{ stDescription = stDescription.Substring(0, mclsTerminalDetails.MaxReceiptWidth); }
			catch { }

			string stQuantity = Quantity.ToString("#,##0.#0");
			if (Quantity == 1 || Quantity == -1)
				stQuantity = "1";
			else if (Decimal.Compare(Quantity, Decimal.Floor(Quantity)) == 0)
			{ stQuantity = Quantity.ToString("#,##0"); }

			if (bolBIG)
			{
				if (mclsTerminalDetails.IsPrinterDotMatrix) SendOrderSlipToPrinter(stQuantity + "x" + stProductUnitCode + " " + stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
				else SendOrderSlipToPrinter(RawPrinterHelper.esc8CpiOn + stQuantity + "x" + stProductUnitCode + " " + stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12) + RawPrinterHelper.esc8CpiOff, PrinterName);
			}
			else
			{
				SendOrderSlipToPrinter(stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
				SendOrderSlipToPrinter(stProductUnitCode.PadRight(6), PrinterName);
				SendOrderSlipToPrinter(stQuantity.PadLeft(6), PrinterName);
			}

			SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
		}

		private delegate void PrintItemDelegate(string ItemNo, string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT);
		private void PrintItem(string ItemNo, string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT)
		{
			if (!mboIsItemHeaderPrinted)
			{
				//if (mclsTerminalDetails.IsPrinterAutoCutter)
				//    PrintReportPageHeaderSectionChecked(true);
				//else
					PrintReportHeadersSection(true);

				mboIsItemHeaderPrinted = true;
			}

			// description
			string stDescription = Description;
			try
			{ stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
			catch { }
			try
			{ stDescription = stDescription.Substring(0, mclsTerminalDetails.MaxReceiptWidth); }
			catch { }

			// discount and promo
			string AddedString = "";
			if (Discount != 0)
			{ AddedString += "@" + Discount.ToString("#,##0.#0") + "disc "; }
			if (PromoApplied != 0)
			{ AddedString += "@" + PromoApplied.ToString("#,##0.#0") + "promo "; }

			// price
			string stPrice = Price.ToString("#,##0.#0");
			if (Price == 1 || Price == -1)
				stPrice = "1";
			else if (Decimal.Compare(Price, Decimal.Floor(Price)) == 0)
			{ stPrice = Price.ToString("#,##0"); }

			// quantity
			string stQuantity = Quantity.ToString("#,##0.#0");
			if (Quantity == 1 || Quantity == -1)
				stQuantity = "1";
			else if (Decimal.Compare(Quantity, Decimal.Floor(Quantity)) == 0)
			{ stQuantity = Quantity.ToString("#,##0"); }

			// evat and vat
			bool isVATable = false;
			if (VAT > 0)
			{ isVATable = true; }

			string stAmount = Amount.ToString("#,##0.#0");
			//			if (Decimal.Compare(Amount, Decimal.Floor(Amount)) == 0)
			//			{	stAmount = Amount.ToString("#,##0");	}

			if (mclsSalesTransactionDetails.DiscountCode != Constants.SeniorCitizenDiscountCode)
			{
				if (isVATable)
				{ stAmount += "V "; }
				else
				{ stAmount += "NV"; }
			}

			if (stDescription.Length <=14 && AddedString.Length==0)
				mstrToPrint += stDescription.PadRight(14);
			else
				mstrToPrint += stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;

			if (AddedString.Length <= 11 && AddedString.Length != 0)
				mstrToPrint += AddedString.PadRight(11);
			else if (AddedString.Length > 11)
				mstrToPrint += AddedString.PadRight(mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;

			if (stQuantity.Length <= 4 && stDescription.Length <=14 && AddedString.Length == 0)
				mstrToPrint += stQuantity.PadLeft(3);
			else if (stQuantity.Length <= 6 && AddedString.Length == 0)
				mstrToPrint += stQuantity.PadLeft(17);
			else if (stQuantity.Length <= 6 && AddedString.Length > 11)
				mstrToPrint += stQuantity.PadLeft(17);
			else if (stQuantity.Length <= 6 && AddedString.Length != 0)
				mstrToPrint += stQuantity.PadLeft(6);
			else if (stQuantity.Length <= 17 && AddedString.Length == 0)
				mstrToPrint += stQuantity.PadLeft(17);
			else if (stQuantity.Length <= 17 && AddedString.Length != 0)
				mstrToPrint += stQuantity.PadLeft(17);
			else
				mstrToPrint += stQuantity.PadLeft(stQuantity.Length);

			if (stPrice.Length <= 10)
				mstrToPrint += stPrice.PadLeft(10);
			else
				mstrToPrint += Environment.NewLine + stPrice.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 13);

			if (stAmount.Length <= 13)
				mstrToPrint += stAmount.PadLeft(13);
			else
				mstrToPrint += Environment.NewLine + stAmount.PadLeft(mclsTerminalDetails.MaxReceiptWidth);

			mstrToPrint += Environment.NewLine;
		}

		private void PrintPageFooterASection()
		{
			int iCtr = 0;
			string stModule = "";
			Receipt clsReceipt = new Receipt();
			ReceiptDetails clsReceiptDetails;

			// print page footer
			for (iCtr = 1; iCtr <= 20; iCtr++)
			{
				stModule = "PageFooterA" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);

				PrintReportValue(clsReceiptDetails, false);
			}

			clsReceipt.CommitAndDispose();
		}
		private void PrintPageFooterBSection()
		{
			int iCtr = 0;
			string stModule = "";
			Receipt clsReceipt = new Receipt();
			ReceiptDetails clsReceiptDetails;

			// print page footer
			for (iCtr = 1; iCtr <= 5; iCtr++)
			{
				stModule = "PageFooterB" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);

				PrintReportValue(clsReceiptDetails, false);
			}

			clsReceipt.CommitAndDispose();
		}
		private void PrintReportFooter(bool IsReceipt)
		{
			int iCtr = 0;
			string stModule = "";
			Receipt clsReceipt = new Receipt();
			ReceiptDetails clsReceiptDetails;

			// print report footer
			for (iCtr = 1; iCtr <= 5; iCtr++)
			{
				stModule = "ReportFooter" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);

				PrintReportValue(clsReceiptDetails, IsReceipt);
			}

			// print report footer Spacer
			clsReceiptDetails = clsReceipt.Details(ReportFormatModule.ReportFooterSpacer);
			for (int i = 0; i < Convert.ToInt32(clsReceiptDetails.Value); i++)
			{ mstrToPrint += Environment.NewLine; }
			clsReceipt.CommitAndDispose();

			// do the actual print
			mclsFilePrinter.Write(mstrToPrint);
			RawPrinterHelper.SendFileToPrinter(mclsTerminalDetails.PrinterName, mclsFilePrinter.FileName, "RetailPlus " + mclsFilePrinter.FileName);
			mclsFilePrinter.DeleteFile();

		}
		private void PrintReportFooterSection(bool IsReceipt)
		{
			if (mclsTerminalDetails.AutoPrint != PrintingPreference.AskFirst)
			{
				int iCtr = 0;
				string stModule = "";
				Receipt clsReceipt = new Receipt();
				ReceiptDetails clsReceiptDetails;

				if (IsReceipt)
				{
					// print page footer
					for (iCtr = 1; iCtr <= 20; iCtr++)
					{
						stModule = "PageFooterA" + iCtr;
						clsReceiptDetails = clsReceipt.Details(stModule);

						PrintReportValue(clsReceiptDetails, IsReceipt);
					}
					// print page footer
					for (iCtr = 1; iCtr <= 5; iCtr++)
					{
						stModule = "PageFooterB" + iCtr;
						clsReceiptDetails = clsReceipt.Details(stModule);

						PrintReportValue(clsReceiptDetails, IsReceipt);
					}
				}

				// print report footer
				for (iCtr = 1; iCtr <= 5; iCtr++)
				{
					stModule = "ReportFooter" + iCtr;
					clsReceiptDetails = clsReceipt.Details(stModule);

					PrintReportValue(clsReceiptDetails, IsReceipt);
				}

				// print report footer Spacer
				clsReceiptDetails = clsReceipt.Details(ReportFormatModule.ReportFooterSpacer);
				for (int i = 0; i < Convert.ToInt32(clsReceiptDetails.Value); i++)
				{ mstrToPrint += Environment.NewLine; }

				clsReceipt.CommitAndDispose();

				mclsFilePrinter.Write(mstrToPrint);
				RawPrinterHelper.SendFileToPrinter(mclsTerminalDetails.PrinterName, mclsFilePrinter.FileName, "RetailPlus " + mclsFilePrinter.FileName);
				mclsFilePrinter.DeleteFile();

				//print the first part of transaction header if autocutter
				if (mclsTerminalDetails.IsPrinterAutoCutter)
				{
					//cut the paper if printer is auto cutter
					CutPrinterPaper();

					//PrintReportHeaderSection(IsReceipt);
				}
			}
		}
		private void PrintReportFooterSection(bool IsReceipt, TransactionStatus status, decimal TotalItemSold, decimal TotalQuantitySold, decimal SubTotal, decimal Discount, decimal Charge, decimal AmountPaid, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, decimal ChangeAmount, ArrayList arrChequePaymentDetails, ArrayList arrCreditCardPaymentDetails, ArrayList arrCreditPaymentDetails, ArrayList arrDebitPaymentDetails)
		{
			if (mclsTerminalDetails.AutoPrint != PrintingPreference.AskFirst)
			{
				PrintPageFooterASection();

				if (status == TransactionStatus.Refund)
				{
					if (CashPayment != 0)
						mstrToPrint += "Cash Refund :       " + CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

					if (ChequePayment != 0)
					{
						mstrToPrint += "Cheque Refund  :    " + ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

						if (arrChequePaymentDetails != null)
						{
							foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
							{
								//print cheque details
								mstrToPrint += "Cheque No.     :    " + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Amount         :    " + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Validity Date  :    " + chequepaymentdet.ValidityDate.ToString("MM/dd/yyyy").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += Environment.NewLine;
							}
						}
					}

					if (CreditCardPayment != 0)
					{
						mstrToPrint += "Credit Card Refund: " + CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

						if (arrCreditCardPaymentDetails != null)
						{
							foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
							{
								//print credit card details
								mstrToPrint += "Card Type      :    " + cardpaymentdet.CardTypeCode.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Card No.       :    " + cardpaymentdet.CardNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Member Name    :    " + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Amount         :    " + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Validity Date  :    " + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += Environment.NewLine;
							}
						}
					}
				}
				else if (status == TransactionStatus.Closed || status == TransactionStatus.Reprinted || status == TransactionStatus.Open || status == TransactionStatus.CreditPayment)
				{
					if (CashPayment != 0)
						mstrToPrint += "Cash Payment :      " + CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

					if (ChequePayment != 0)
					{
						mstrToPrint += "Cheque Paymnt:      " + ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

						if (arrChequePaymentDetails != null)
						{
							foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
							{
								//print checque details
								mstrToPrint += "Cheque No.   :      " + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Amount       :      " + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Validity Date:      " + chequepaymentdet.ValidityDate.ToString("MM/dd/yyyy").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += Environment.NewLine;
							}
						}
					}

					if (CreditCardPayment != 0)
					{
						mstrToPrint += "C.Card Paymnt:      " + CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
						if (arrCreditCardPaymentDetails != null)
						{
							foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
							{
								//print credit card details
								mstrToPrint += "Card Type    :      " + cardpaymentdet.CardTypeCode.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Card No.     :      " + cardpaymentdet.CardNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Member Name  :      " + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Amount       :      " + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Validity Date:      " + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += Environment.NewLine;
							}
						}
					}
					if (CreditPayment != 0)
					{
						mstrToPrint += "Credit Paymnt:      " + CreditPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
						if (arrCreditPaymentDetails != null)
						{
							foreach (Data.CreditPaymentDetails creditpaymentdet in arrCreditPaymentDetails)
							{
								//print credit details
								mstrToPrint += "Amount       :      " + creditpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Remarks      :      " + creditpaymentdet.Remarks + Environment.NewLine;
								mstrToPrint += Environment.NewLine;
							}
						}


					}
					if (DebitPayment != 0)
					{
						mstrToPrint += "Debit  Paymnt:      " + DebitPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
						if (arrDebitPaymentDetails != null)
						{
							foreach (Data.DebitPaymentDetails debitpaymentdet in arrDebitPaymentDetails)
							{
								//print credit details
								mstrToPrint += "Amount       :      " + debitpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
								mstrToPrint += "Remarks      :      " + debitpaymentdet.Remarks + Environment.NewLine;
								mstrToPrint += Environment.NewLine;
							}
						}
					}
					if (RewardConvertedPayment != 0)
					{
						mstrToPrint += "Reward Paymnt:      " + RewardConvertedPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					}
				}

				if (status == TransactionStatus.Suspended)
				{
					mstrToPrint += CenterString("*****THIS TRANSACTION IS SUSPENDED*****", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				}
				else if (status == TransactionStatus.Void)
				{
					mstrToPrint += CenterString("*******THIS TRANSACTION IS VOID*******", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				}
				else if (status == TransactionStatus.Reprinted)
				{
					mstrToPrint += CenterString("**THIS TRANSACTION IS REPRINTED AS OF**", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += CenterString(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				}
				else if (status == TransactionStatus.Refund)
				{
					mstrToPrint += CenterString("*****THIS TRANSACTION IS A REFUND*****", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				}
				else if (status == TransactionStatus.CreditPayment)
				{
					mstrToPrint += CenterString("------CREDIT PAYMENT--------", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				}

				PrintPageFooterBSection();
				PrintReportFooter(IsReceipt);

				//print the first part of transaction header if autocutter
				if (mclsTerminalDetails.IsPrinterAutoCutter)
				{
					//cut the paper if printer is auto cutter
					CutPrinterPaper();
				}
			}

		}

		private void PrintChargeSlip(ChargeSlipType clsChargeSlipType)
		{
			try 
			{
				if (mclsSalesTransactionDetails.CreditPayment != 0)
				{
					PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
					mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

					PrintReportHeaderSection(false);
					if (mclsContactDetails.CreditDetails.GuarantorID != mclsContactDetails.ContactID)
					{ if (GetReceiptFormatParameter(ReceiptFieldFormats.InHouseGroupCreditPermitNo, false) != string.Empty) mstrToPrint += CenterString("BIR Permit No." + GetReceiptFormatParameter(ReceiptFieldFormats.InHouseGroupCreditPermitNo, false), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine; }
					else
					{ if (GetReceiptFormatParameter(ReceiptFieldFormats.InHouseIndividualCreditPermitNo, false) != string.Empty) mstrToPrint += CenterString("BIR Permit No." + GetReceiptFormatParameter(ReceiptFieldFormats.InHouseIndividualCreditPermitNo, false), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine; }

					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "Transaction Date   :" + mclsSalesTransactionDetails.TransactionDate.ToString("MM/dd/yyyy").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					mstrToPrint += "Transaction #      :" + mclsSalesTransactionDetails.TransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					if (mclsContactDetails.CreditDetails.GuarantorID != mclsContactDetails.ContactID)
					{
						Data.Contacts clsContact = new Data.Contacts();
						Data.ContactDetails clsGuarantorContactDetails = clsContact.Details(mclsContactDetails.CreditDetails.GuarantorID);
						clsContact.CommitAndDispose();
						mstrToPrint += CenterString(clsGuarantorContactDetails.ContactCode, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
						mstrToPrint += "-".PadLeft(mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
						mstrToPrint += CenterString("Guarantor", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					}
					mstrToPrint += Environment.NewLine;
					mstrToPrint += CenterString("CHARGE SLIP", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "Amount of Purchase :" + mclsSalesTransactionDetails.CreditPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "I hereby agree  to pay the total  amount" + Environment.NewLine;
					mstrToPrint += "stated herein including any charges  due" + Environment.NewLine;
					mstrToPrint += "thereon  subject   to    the   pertinent" + Environment.NewLine;
					mstrToPrint += "contract   governing  the use of    this" + Environment.NewLine;
					mstrToPrint += "Credit Card." + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString(mclsSalesTransactionDetails.CustomerName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString(mclsSalesTransactionDetails.CashierName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += CenterString("Cashier", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					switch (clsChargeSlipType)
					{
						case ChargeSlipType.Original:
							mstrToPrint += CenterString("Original Copy", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
							break;
						case ChargeSlipType.Guarantor:
							mstrToPrint += CenterString("Guarantor's Copy", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
							break;
					}

					PrintReportFooterSection(false);

					mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

					InsertAuditLog(AccessTypes.PrintTransactionHeader, "Print Charge Slip: " + clsChargeSlipType.ToString("G") + " TerminalNo=" + mclsTerminalDetails.TerminalNo);
				}
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing Charge Slip: " + clsChargeSlipType .ToString("G") + ". Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}
		private void PrintRewardsRedemptionSlip()
		{
			// this should comes before earning of points otherwise this will be wrong.
			try
			{
				if (mclsSalesTransactionDetails.RewardPointsPayment != 0)
				{
					PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
					mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

					PrintReportHeaderSection(false);
					if (GetReceiptFormatParameter(ReceiptFieldFormats.RewardsPermitNo, false) != string.Empty) mstrToPrint += CenterString("BIR Permit No." + GetReceiptFormatParameter(ReceiptFieldFormats.RewardsPermitNo, false), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;

					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "Transaction Date   :" + mclsSalesTransactionDetails.TransactionDate.ToString("MM/dd/yyyy").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += CenterString("R E D E M P T I O N   S L I P", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "Rewards Card No.   :" + mclsContactDetails.RewardDetails.RewardCardNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					mstrToPrint += "Total Points       :" + mclsSalesTransactionDetails.RewardPreviousPoints.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					mstrToPrint += "Redeemed Points    :" + mclsSalesTransactionDetails.RewardPointsPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					mstrToPrint += "                    ".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += "Balance Points     :" + mclsSalesTransactionDetails.RewardCurrentPoints.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString(mclsSalesTransactionDetails.CustomerName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += "-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString(mclsSalesTransactionDetails.CashierName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += CenterString("Cashier", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += Environment.NewLine;
					mstrToPrint += CenterString("Original Copy", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;

					PrintReportFooterSection(false);

					mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

					InsertAuditLog(AccessTypes.PrintTransactionHeader, "Print Rewards Redeemption Slip: TerminalNo=" + mclsTerminalDetails.TerminalNo);
				}
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing Rewards Redeemption Slip. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		//private void SendStringToPrinter(string szString)
		//{
		//    //Console.Write(szString);
		//    clsEvent.AddEventReceipt(szString);
		//    //m_Printer.PrintNormal(PrinterStation.Receipt, szString + "\f");
		//    RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.PrinterName, szString + "\f", "RetailPlus Trx. No: " + lblTransNo.Text);
		//}
		private void SendOrderSlipToPrinter(string szString, string PrinterName)
		{
			//Console.Write(szString);
			clsEvent.AddEventReceipt(szString);
			RawPrinterHelper.SendStringToPrinter(PrinterName, szString + "\f", "RetailPlus Trx. No: " + lblTransNo.Text);
		}
		private void SendStringToTurret(string szString)
		{
			RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.TurretName, "\f" + szString, "RetailPlus Turret Disp: " + lblTransNo.Text);
		}

		private string GetReceiptFormatParameter(string stReceiptFormat, bool IsReceipt)
		{
			string stRetValue = "";

			if (stReceiptFormat == ReceiptFieldFormats.Blank)
			{
				stRetValue = "";
			}
			else if (stReceiptFormat == ReceiptFieldFormats.Spacer)
			{
				stRetValue = " ";
			}
			else if (stReceiptFormat == ReceiptFieldFormats.InvoiceNo)
			{
				if (!IsReceipt)
					stRetValue = "";
				else
					stRetValue = mclsSalesTransactionDetails.TransactionNo;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.DateNow)
			{
				stRetValue = DateTime.Now.ToLocalTime().ToString("MMM. dd, yyyy hh:mm:ss tt");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.TransactionDate)
			{
				stRetValue = mclsSalesTransactionDetails.TransactionDate.ToLocalTime().ToString("MMM. dd, yyyy hh:mm:ss tt");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.Cashier)
			{
				stRetValue = mclsSalesTransactionDetails.CashierName;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.TerminalNo)
			{
				stRetValue = mclsTerminalDetails.TerminalNo;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.MachineSerialNo)
			{
				stRetValue = CONFIG.MachineSerialNo;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.AccreditationNo)
			{
				stRetValue = CONFIG.AccreditationNo;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.SubTotal)
			{
				stRetValue = mclsSalesTransactionDetails.SubTotal.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.OtherCharges)
			{
				stRetValue = mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.Discount)
			{
				stRetValue = mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.AmountDue)
			{
				stRetValue = mclsSalesTransactionDetails.AmountDue.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.AmountTender)
			{
				stRetValue = mclsSalesTransactionDetails.AmountPaid.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.Change)
			{
				stRetValue = mclsSalesTransactionDetails.ChangeAmount.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.NonVatableAmount)
			{
				stRetValue = mclsSalesTransactionDetails.NonVATableAmount.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.VatableAmount)
			{
				stRetValue = mclsSalesTransactionDetails.VatableAmount.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.VAT)
			{
				stRetValue = mclsSalesTransactionDetails.VAT.ToString("#,##0.#0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.TotalItemSold)
			{
				stRetValue = mclsSalesTransactionDetails.TotalItemSold.ToString("#,##0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.TotalQtySold)
			{
				stRetValue = mclsSalesTransactionDetails.TotalQuantitySold.ToString("#,##0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.CustomerName)
			{
				stRetValue = mclsSalesTransactionDetails.CustomerName;// lblCustomer.Text;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.WaiterName)
			{
				stRetValue = mclsSalesTransactionDetails.WaiterName; // lblServedBy.Text.Remove(0,11).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14);
			}
			else if (stReceiptFormat == ReceiptFieldFormats.BaggerName)
			{
				stRetValue = mclsSalesTransactionDetails.WaiterName; // lblServedBy.Text.Remove(0,11).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14);
			}
			else if (stReceiptFormat == ReceiptFieldFormats.OrderType)
			{
				stRetValue = mclsSalesTransactionDetails.OrderType.ToString("G").ToUpper();
			}
			else if (stReceiptFormat == ReceiptFieldFormats.CheckOutBillFooter)
			{
				PrintCheckOutBillFooter();
				stRetValue = "";
			}
			else if (stReceiptFormat == ReceiptFieldFormats.DiscountCode)
			{
				stRetValue = mclsSalesTransactionDetails.DiscountCode;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.DiscountRemarks)
			{
				stRetValue = mclsSalesTransactionDetails.DiscountRemarks;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.ChargeCode)
			{
				stRetValue = mclsSalesTransactionDetails.ChargeCode;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.ChargeRemarks)
			{
				stRetValue = mclsSalesTransactionDetails.ChargeRemarks;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.RewardCardNo)
			{
				stRetValue = mclsSalesTransactionDetails.RewardCardNo;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.RewardPreviousPoints)
			{
				stRetValue = mclsSalesTransactionDetails.RewardPreviousPoints.ToString("#,##0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.RewardEarnedPoints)
			{
				stRetValue = mclsSalesTransactionDetails.RewardEarnedPoints.ToString("#,##0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.RewardCurrentPoints)
			{
				stRetValue = mclsSalesTransactionDetails.RewardCurrentPoints.ToString("#,##0");
			}
			else if (stReceiptFormat == ReceiptFieldFormats.RewardsPermitNo)
			{
				stRetValue = mclsTerminalDetails.RewardPointsDetails.RewardsPermitNo;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.InHouseIndividualCreditPermitNo)
			{
				stRetValue = mclsTerminalDetails.InHouseIndividualCreditPermitNo;
			}
			else if (stReceiptFormat == ReceiptFieldFormats.InHouseGroupCreditPermitNo)
			{
				stRetValue = mclsTerminalDetails.InHouseGroupCreditPermitNo;
			}
			else
			{
				stRetValue = stReceiptFormat;
			}

			if (stRetValue == null) stRetValue = "";

			return stRetValue;
		}


		#region PrintZRead
		private void PrintZRead(bool pvtWillPreviewReport)
		{
			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
			Data.TerminalReportDetails Details = clsTerminalReport.Details(mclsTerminalDetails.TerminalNo);
			clsTerminalReport.CommitAndDispose();

			DialogResult result = DialogResult.OK;

			if (pvtWillPreviewReport)
			{
				XZReadReportWnd clsXZReadReportWnd = new XZReadReportWnd();
				clsXZReadReportWnd.Details = Details;
				clsXZReadReportWnd.CashierName = lblCashier.Text;
				clsXZReadReportWnd.TrustFund = 0;
				clsXZReadReportWnd.TerminalReportType = TerminalReportType.ZRead;
				clsXZReadReportWnd.ShowDialog(this);
				result = clsXZReadReportWnd.Result;
				clsXZReadReportWnd.Close();
				clsXZReadReportWnd.Dispose();
			}

			if (result == DialogResult.OK)
			{
				PrintZReadDelegate printzreadDel = new PrintZReadDelegate(PrintZRead);
				printzreadDel.BeginInvoke(true, Details, null, null);
			}
		}
		private delegate void PrintZReadDelegate(bool pvtWillOpenDrawer, Data.TerminalReportDetails Details);
		private void PrintZRead(bool pvtWillOpenDrawer, Data.TerminalReportDetails Details)
		{
			if (pvtWillOpenDrawer)
			{
				Cursor.Current = Cursors.WaitCursor;
				OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
				Invoke(opendrawerDel);
			}

			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);
				
				DateTime dteEffectiveDate = Convert.ToDateTime(Details.DateLastInitializedToDisplay.ToString("MMM. dd, yyyy") + " " + Details.DateLastInitialized.ToString("hh:mm:ss tt"));
				mstrToPrint += CenterString(dteEffectiveDate.ToString("MMM. dd, yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("ZRead Report : " + Details.ZReadCount.ToString(), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				mstrToPrint += "Beginning Tran. No. :" + Details.BeginningTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Ending Tran. No.    :" + Details.EndingTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Gross Sales         :" + Convert.ToDecimal(Details.GrossSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Total Discount (-)  :" + Convert.ToDecimal(Details.TotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				decimal TotalAmount = Details.DailySales + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge;

				mstrToPrint += "Net Sales           :" + Convert.ToDecimal(Details.DailySales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Service Charge      :" + Convert.ToDecimal(Details.TotalCharge).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Amount        :" + Convert.ToDecimal(TotalAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				if (mclsTerminalDetails.WillPrintGrandTotal == true)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += "OLD GRAND TOTAL     :" + Convert.ToDecimal(Details.OldGrandTotal).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "This Total Amount   :" + Convert.ToDecimal(TotalAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
					mstrToPrint += "NEW GRAND TOTAL     :" + Convert.ToDecimal(Details.NewGrandTotal).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "VATable Amount      :" + Convert.ToDecimal(Details.VATableAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Non-Vatable Amt.    :" + Convert.ToDecimal(Details.NonVaTableAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Local Tax           :" + Convert.ToDecimal(Details.LocalTax).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Sales          :" + Convert.ToDecimal(Details.CashSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Sales        :" + Convert.ToDecimal(Details.ChequeSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Sales   :" + Convert.ToDecimal(Details.CreditCardSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit (Charge)     :" + Convert.ToDecimal(Details.CreditSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment      :" + Convert.ToDecimal(Details.CreditPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit  Sales        :" + Convert.ToDecimal(Details.DebitPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "RewardPoints Redeemd:" + Convert.ToDecimal(Details.RewardPointsPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "     Cash Equivalent:" + Convert.ToDecimal(Details.RewardConvertedPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Employee Acct.      :" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Sales          :" + Convert.ToDecimal(Details.VoidSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Sales        :" + Convert.ToDecimal(Details.RefundSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Items Discount      :" + Convert.ToDecimal(Details.ItemsDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Subtotal Discount   :" + Convert.ToDecimal(Details.SubTotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Discounts     :" + Convert.ToDecimal(Details.TotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
				System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.TerminalNo, Details.BeginningTransactionNo, Details.EndingTransactionNo);
				clsSalesTransactions.CommitAndDispose();
				if (dt.Rows.Count > 0)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					foreach (System.Data.DataRow dr in dt.Rows)
					{ mstrToPrint += dr["DiscountCode"].ToString().PadRight(20, ' ') + ":" + Convert.ToDecimal(Convert.ToDecimal(dr["Discount"])).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine; }
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Beginning Balance   :" + Convert.ToDecimal(Details.BeginningBalance).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cash-In-Drawer      :" + Convert.ToDecimal(Details.CashInDrawer).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Paid Out            :" + Convert.ToDecimal(Details.TotalPaidOut).ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Transactions   :" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Trans.  :" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Transactions :" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Transactions :" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit Payment Trans.:" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Transactions :" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Transactions   :" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Combination Tran    :" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment Trans:" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Reward Points  Trans:" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				//				mstrToPrint += "Employees Acct Trans:" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Transactions  :" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintZRead, "Print ZRead report: TerminalNo=" + mclsTerminalDetails.TerminalNo);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing ZRead report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		private delegate void RePrintZReadDelegate(Data.TerminalReportDetails Details);
		private void RePrintZRead(Data.TerminalReportDetails Details)
		{
			Cursor.Current = Cursors.WaitCursor;
			OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
			Invoke(opendrawerDel);

			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;
				
				PrintReportHeadersSection(false);

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("ZRead Report : " + Details.ZReadCount.ToString(), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				mstrToPrint += "Beginning Tran. No. :" + Details.BeginningTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Ending Tran. No.    :" + Details.EndingTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Gross Sales         :" + Convert.ToDecimal(Details.GrossSales - (Details.GrossSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Total Discount (-)  :" + Convert.ToDecimal(Details.TotalDiscount - (Details.TotalDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				decimal TotalAmount = Details.DailySales + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge;

				mstrToPrint += "Net Sales           :" + Convert.ToDecimal(Details.DailySales - (Details.DailySales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT - (Details.VAT * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Service Charge      :" + Convert.ToDecimal(Details.TotalCharge - (Details.TotalCharge * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Amount        :" + Convert.ToDecimal(TotalAmount - (Details.GrossSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				if (mclsTerminalDetails.WillPrintGrandTotal == true)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += "OLD GRAND TOTAL     :" + Convert.ToDecimal(Details.OldGrandTotal - (Details.OldGrandTotal * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "This Total Amount   :" + Convert.ToDecimal(TotalAmount - (TotalAmount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
					mstrToPrint += "NEW GRAND TOTAL     :" + Convert.ToDecimal(Details.NewGrandTotal - (Details.NewGrandTotal * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "VATable Amount      :" + Convert.ToDecimal(Details.VATableAmount - (Details.VATableAmount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Non-Vatable Amt.    :" + Convert.ToDecimal(Details.NonVaTableAmount - (Details.NonVaTableAmount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT - (Details.VAT * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Local Tax           :" + Convert.ToDecimal(Details.LocalTax - (Details.LocalTax * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Sales          :" + Convert.ToDecimal(Details.CashSales - (Details.CashSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Sales        :" + Convert.ToDecimal(Details.ChequeSales - (Details.ChequeSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Sales   :" + Convert.ToDecimal(Details.CreditCardSales - (Details.CreditCardSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit (Charge)     :" + Convert.ToDecimal(Details.CreditSales - (Details.CreditSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment      :" + Convert.ToDecimal(Details.CreditPayment - (Details.CreditPayment * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit  Sales        :" + Convert.ToDecimal(Details.DebitPayment - (Details.DebitPayment * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "RewardPoints Redeemd:" + Convert.ToDecimal(Details.RewardPointsPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "     Cash Equivalent:" + Convert.ToDecimal(Details.RewardConvertedPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Employee Acct.      :" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Sales          :" + Convert.ToDecimal(Details.VoidSales - (Details.VoidSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Sales        :" + Convert.ToDecimal(Details.RefundSales - (Details.RefundSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Items Discount      :" + Convert.ToDecimal(Details.ItemsDiscount - (Details.ItemsDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Subtotal Discount   :" + Convert.ToDecimal(Details.SubTotalDiscount - (Details.SubTotalDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Discounts     :" + Convert.ToDecimal(Details.TotalDiscount - (Details.TotalDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
				System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.TerminalNo, Details.BeginningTransactionNo, Details.EndingTransactionNo);
				clsSalesTransactions.CommitAndDispose();
				if (dt.Rows.Count > 0)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					foreach (System.Data.DataRow dr in dt.Rows)
					{ mstrToPrint += dr["DiscountCode"].ToString().PadRight(20, ' ') + ":" + Convert.ToDecimal(Convert.ToDecimal(dr["Discount"]) - (Convert.ToDecimal(dr["Discount"]) * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine; }
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Beginning Balance   :" + Convert.ToDecimal(Details.BeginningBalance - (Details.BeginningBalance * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cash-In-Drawer      :" + Convert.ToDecimal(Details.CashInDrawer - (Details.CashInDrawer * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Paid Out            :" + Convert.ToDecimal(Details.TotalPaidOut - (Details.TotalPaidOut * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDisburse - (Details.CashDisburse * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDisburse - (Details.ChequeDisburse * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDisburse - (Details.CreditCardDisburse * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashWithHold - (Details.CashWithHold * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeWithHold - (Details.ChequeWithHold * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardWithHold - (Details.CreditCardWithHold * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDeposit - (Details.CashDeposit * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDeposit - (Details.ChequeDeposit * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDeposit - (Details.CreditCardDeposit * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Transactions   :" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Trans.  :" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Transactions :" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Transactions :" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit Payment Trans.:" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Transactions :" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Transactions   :" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Combination Tran    :" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment Trans:" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Reward Points  Trans:" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				//				mstrToPrint += "Employees Acct Trans:" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Transactions  :" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintZRead, "Print ZRead report: TerminalNo=" + mclsTerminalDetails.TerminalNo);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing ZRead report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region ReprintZRead
		private void ReprintZRead()
		{
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTerminalReport);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintZRead;
				login.Header = "Re-Print ZREAD Report";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				TerminalHistoryDateWnd clsTerminalHistoryDateWnd = new TerminalHistoryDateWnd();
				clsTerminalHistoryDateWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
				clsTerminalHistoryDateWnd.ShowDialog(this);
				DialogResult clsTerminalHistoryDateWndresult = clsTerminalHistoryDateWnd.Result;
				DateTime dtDateLastInitialized = clsTerminalHistoryDateWnd.DateLastInitialized;
				clsTerminalHistoryDateWnd.Close();
				clsTerminalHistoryDateWnd.Dispose();

				Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
				Data.TerminalReportDetails Details = clsTerminalReportHistory.Details(mclsTerminalDetails.TerminalNo, dtDateLastInitialized);
				//Data.TerminalReportDetails NextDetails = clsTerminalReportHistory.NextDetails(mclsTerminalDetails.TerminalNo, dtDateLastInitialized);
				clsTerminalReportHistory.CommitAndDispose();

				decimal OldTrustFund = mclsTerminalDetails.TrustFund;
				mclsTerminalDetails.TrustFund = Details.TrustFund;

				AceSoft.Common.SYSTEMTIME st = new AceSoft.Common.SYSTEMTIME();
				// set the sytem date to NextDetails DateLastInitialized
				st = AceSoft.Common.ConvertToSystemTime(Details.DateLastInitialized.AddSeconds(-2).ToUniversalTime());
				mdtCurrentDateTime = DateTime.Now;
				tmr.Enabled = true;
				tmr.Start();
				AceSoft.Common.SetSystemTime(ref st);

				XZReadReportWnd clsXZReadReportWnd = new XZReadReportWnd();
				clsXZReadReportWnd.Details = Details;
				clsXZReadReportWnd.CashierName = lblCashier.Text;
				clsXZReadReportWnd.TrustFund = mclsTerminalDetails.TrustFund;
				clsXZReadReportWnd.TerminalReportType = TerminalReportType.ZRead;
				clsXZReadReportWnd.ShowDialog(this);
				DialogResult result = clsXZReadReportWnd.Result;
				clsXZReadReportWnd.Close();
				clsXZReadReportWnd.Dispose();

				if (result == DialogResult.OK)
				{
					//PrintZReadDelegate printzreadDel = new PrintZReadDelegate(PrintZRead);
					//printzreadDel.BeginInvoke(Details, null, null);
					RePrintZRead(Details);
					InsertAuditLog(AccessTypes.ReprintZRead, "Re-Print ZRead report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm"));
				}

				// set the sytem date to NextDetails DateLastInitialized
				st = AceSoft.Common.ConvertToSystemTime(mdtCurrentDateTime.ToUniversalTime());
				AceSoft.Common.SetSystemTime(ref st);
				tmr.Stop();
				tmr.Enabled = false;

				mclsTerminalDetails.TrustFund = OldTrustFund;
			}
		}

		#endregion

		#region PrintXRead
		private void PrintXRead()
		{
			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
			Data.TerminalReportDetails Details = clsTerminalReport.Details(mclsTerminalDetails.TerminalNo);
			clsTerminalReport.CommitAndDispose();

			DialogResult result = DialogResult.OK;

			if (mclsTerminalDetails.PreviewTerminalReport)
			{
				XZReadReportWnd clsXZReadReportWnd = new XZReadReportWnd();
				clsXZReadReportWnd.Details = Details;
				clsXZReadReportWnd.CashierName = lblCashier.Text;
				clsXZReadReportWnd.TrustFund = 0; // mclsTerminalDetails.TrustFund;
				clsXZReadReportWnd.TerminalReportType = TerminalReportType.XRead;
				clsXZReadReportWnd.ShowDialog(this);
				result = clsXZReadReportWnd.Result;
				clsXZReadReportWnd.Close();
				clsXZReadReportWnd.Dispose();
			}

			if (result == DialogResult.OK)
			{
				PrintXReadDelegate printxreadDel = new PrintXReadDelegate(PrintXRead);
				printxreadDel.BeginInvoke(Details, null, null);
			}
		}
		private delegate void PrintXReadDelegate(Data.TerminalReportDetails Details);
		private void PrintXRead(Data.TerminalReportDetails Details)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);
				
				DateTime dteEffectiveDate = Convert.ToDateTime(Details.DateLastInitializedToDisplay.ToString("MMM. dd, yyyy") + " " + Details.DateLastInitialized.ToString("hh:mm:ss tt"));
				mstrToPrint += CenterString(dteEffectiveDate.ToString("MMM. dd, yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				//mstrToPrint += CenterString(Details.DateLastInitialized.ToString("MMM. dd, yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("XRead Report : " + Details.XReadCount.ToString(), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				mstrToPrint += "Beginning Tran. No. :" + Details.BeginningTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Ending Tran. No.    :" + Details.EndingTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Gross Sales         :" + Convert.ToDecimal(Details.GrossSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Total Discount (-)  :" + Convert.ToDecimal(Details.TotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				decimal TotalAmount = Details.DailySales + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge;

				mstrToPrint += "Net Sales           :" + Convert.ToDecimal(Details.DailySales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Service Charge      :" + Convert.ToDecimal(Details.TotalCharge).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Amount        :" + Convert.ToDecimal(TotalAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				if (mclsTerminalDetails.WillPrintGrandTotal == true)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += "OLD GRAND TOTAL     :" + Convert.ToDecimal(Details.OldGrandTotal).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "This Total Amount   :" + Convert.ToDecimal(TotalAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
					mstrToPrint += "NEW GRAND TOTAL     :" + Convert.ToDecimal(Details.NewGrandTotal).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "VATable Amount      :" + Convert.ToDecimal(Details.VATableAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Non-Vatable Amt.    :" + Convert.ToDecimal(Details.NonVaTableAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Local Tax           :" + Convert.ToDecimal(Details.LocalTax).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Sales          :" + Convert.ToDecimal(Details.CashSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Sales        :" + Convert.ToDecimal(Details.ChequeSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Sales   :" + Convert.ToDecimal(Details.CreditCardSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit (Charge)     :" + Convert.ToDecimal(Details.CreditSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment      :" + Convert.ToDecimal(Details.CreditPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit  Sales        :" + Convert.ToDecimal(Details.DebitPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "RewardPoints Redeemd:" + Convert.ToDecimal(Details.RewardPointsPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "     Cash Equivalent:" + Convert.ToDecimal(Details.RewardConvertedPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Employee Acct.      :" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Sales          :" + Convert.ToDecimal(Details.VoidSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Sales        :" + Convert.ToDecimal(Details.RefundSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Items Discount      :" + Convert.ToDecimal(Details.ItemsDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Subtotal Discount   :" + Convert.ToDecimal(Details.SubTotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Discounts     :" + Convert.ToDecimal(Details.TotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
				System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.TerminalNo, Details.BeginningTransactionNo, Details.EndingTransactionNo);
				clsSalesTransactions.CommitAndDispose();
				if (dt.Rows.Count > 0)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					foreach (System.Data.DataRow dr in dt.Rows)
					{ mstrToPrint += dr["DiscountCode"].ToString().PadRight(20, ' ') + ":" + Convert.ToDecimal(Convert.ToDecimal(dr["Discount"])).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine; }
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Beginning Balance   :" + Convert.ToDecimal(Details.BeginningBalance).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cash-In-Drawer      :" + Convert.ToDecimal(Details.CashInDrawer).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Paid Out            :" + Convert.ToDecimal(Details.TotalPaidOut).ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Transactions   :" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Trans.  :" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Transactions :" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Transactions :" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit Payment Trans.:" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Transactions :" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Transactions   :" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Combination Tran    :" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment Trans:" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Reward Points  Trans:" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				//				mstrToPrint += "Employees Acct Trans:" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Transactions  :" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
				clsTerminalReport.UpdateXReadCount();
				clsTerminalReport.CommitAndDispose();

				InsertAuditLog(AccessTypes.PrintXRead, "Print XREAD report: TerminalNo=" + mclsTerminalDetails.TerminalNo);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing XREAD report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintHourlyReport
		private void PrintHourlyReport()
		{
			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
			System.Data.DataTable dtHourlyReport = clsTerminalReport.HourlyReport(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
			clsTerminalReport.CommitAndDispose();

			DialogResult result = DialogResult.OK;

			if (mclsTerminalDetails.PreviewTerminalReport)
			{
				HourlyReportWnd clsHourlyReportWnd = new HourlyReportWnd();
				clsHourlyReportWnd.CashierName = lblCashier.Text;
				clsHourlyReportWnd.dtHourlyReport = dtHourlyReport;
				clsHourlyReportWnd.ShowDialog(this);
				result = clsHourlyReportWnd.Result;
				clsHourlyReportWnd.Close();
				clsHourlyReportWnd.Dispose();
			}

			if (result == DialogResult.OK)
			{
				PrintHourlyReportDelegate hourlyreportDel = new PrintHourlyReportDelegate(PrintHourlyReport);
				hourlyreportDel.BeginInvoke(dtHourlyReport, null, null);
			}
		}
		private delegate void PrintHourlyReportDelegate(System.Data.DataTable dtHourlyReport);
		private void PrintHourlyReport(System.Data.DataTable dtHourlyReport)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				decimal TotalTranCount = 0;
				decimal TotalAmount = 0;
				decimal TotalDiscount = 0;

				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("HOURLY REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "TIME  TranCnt           Amount  Discount" + Environment.NewLine;

				foreach (System.Data.DataRow dr in dtHourlyReport.Rows)
				{
					string Time = dr["Time"].ToString();
					mstrToPrint += Time.PadRight(6);

					string TranCount = Convert.ToDecimal(dr["TranCount"].ToString()).ToString("##0");
					mstrToPrint += TranCount.PadLeft(7);

					string Amount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");
					mstrToPrint += Amount.PadLeft(17);

					string Discount = Convert.ToDecimal(dr["Discount"].ToString()).ToString("#,##0.#0");
					mstrToPrint += Discount.PadLeft(10);
					mstrToPrint += Environment.NewLine;

					TotalTranCount += Convert.ToDecimal(dr["TranCount"]);
					TotalAmount += Convert.ToDecimal(dr["Amount"]);
					TotalDiscount += Convert.ToDecimal(dr["Discount"]);
				}
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Total:" + TotalTranCount.ToString("##0").PadLeft(7);
				mstrToPrint += TotalAmount.ToString("#,##0.#0").PadLeft(17);
				mstrToPrint += TotalDiscount.ToString("#,##0.#0").PadLeft(10) + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintHourlyReport, "Print hourly report: TerminalNo=" + mclsTerminalDetails.TerminalNo);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing hourly report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintGroupReport
		private void PrintGroupReport()
		{
			Reports.ReceiptFormat clsReceiptFormat = new Reports.ReceiptFormat();
			ReceiptFormatDetails clsReceiptFormatDetails = clsReceiptFormat.Details();
			clsReceiptFormat.CommitAndDispose();

			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
			Data.TerminalReportDetails clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.TerminalNo);
			System.Data.DataTable dtGroupReport = clsTerminalReport.GroupReport(Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
			clsTerminalReport.CommitAndDispose();

			DialogResult result = DialogResult.OK;

			if (mclsTerminalDetails.PreviewTerminalReport)
			{
				GroupReportWnd clsGroupReportWnd = new GroupReportWnd();
				clsGroupReportWnd.IsVATInclusive = mclsTerminalDetails.IsVATInclusive;
				clsGroupReportWnd.CashierName = lblCashier.Text;
				clsGroupReportWnd.dtGroupReport = dtGroupReport;
				clsGroupReportWnd.TerminalReportDetail = clsTerminalReportDetails;
				clsGroupReportWnd.ReceiptFormatDetails = clsReceiptFormatDetails;
				clsGroupReportWnd.ShowDialog(this);
				result = clsGroupReportWnd.Result;
				clsGroupReportWnd.Close();
				clsGroupReportWnd.Dispose();
			}

			if (result == DialogResult.OK)
			{
				PrintGroupReportDelegate groupreportDel = new PrintGroupReportDelegate(PrintGroupReport);
				groupreportDel.BeginInvoke(dtGroupReport, clsTerminalReportDetails, null, null);
			}
		}

		private delegate void PrintGroupReportDelegate(System.Data.DataTable dtGroupReport, Data.TerminalReportDetails clsTerminalReportDetails);
		private void PrintGroupReport(System.Data.DataTable dtGroupReport, Data.TerminalReportDetails clsTerminalReportDetails)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				decimal TotalTranCount = 0;
				decimal TotalAmount = 0;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("GROUP REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "GROUP       QTY           Amount Prcntg." + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				foreach (System.Data.DataRow dr in dtGroupReport.Rows)
				{
					string ProductGroup = dr["ProductGroup"].ToString();
					string TranCount = Convert.ToDecimal(dr["TranCount"].ToString()).ToString("#,##0");
					string Amount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");
					string Percentage = dr["Percentage"].ToString();

					mstrToPrint += ProductGroup + Environment.NewLine;
					mstrToPrint += TranCount.PadLeft(15);
					mstrToPrint += Amount.PadLeft(17);
					mstrToPrint += Percentage.PadLeft(8);
					mstrToPrint += Environment.NewLine;
					//					}

					TotalTranCount += Convert.ToDecimal(dr["TranCount"]);
					TotalAmount += Convert.ToDecimal(dr["Amount"]);
				}
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += TotalTranCount.ToString("#,##0").PadLeft(15);
				mstrToPrint += TotalAmount.ToString("#,##0.#0").PadLeft(17);
				mstrToPrint += "100%".PadLeft(8);
				mstrToPrint += Environment.NewLine;
				mstrToPrint += "Less Discount :".PadRight(15);
				mstrToPrint += clsTerminalReportDetails.SubTotalDiscount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15);
				mstrToPrint += Environment.NewLine;
				mstrToPrint += "Plus Charges  :".PadRight(15);
				mstrToPrint += clsTerminalReportDetails.TotalCharge.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15);
				mstrToPrint += Environment.NewLine;
				if (!mclsTerminalDetails.IsVATInclusive)
				{
					mstrToPrint += "Plus 12% VAT  :".PadRight(15);
					mstrToPrint += clsTerminalReportDetails.VAT.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15);
					mstrToPrint += Environment.NewLine;
				}

				decimal GrandTotal = clsTerminalReportDetails.DailySales + clsTerminalReportDetails.VAT + clsTerminalReportDetails.TotalCharge;
				mstrToPrint += "Grand Total   :".PadRight(15);
				mstrToPrint += GrandTotal.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15);
				mstrToPrint += Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintGroupReport, "Print group report: TerminalNo=" + mclsTerminalDetails.TerminalNo);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing group report. Err Description: " + ex.Message, true);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintPLUReport
		private void PrintPLUReport()
		{
			Reports.ReceiptFormat clsReceiptFormat = new Reports.ReceiptFormat();
			ReceiptFormatDetails clsReceiptFormatDetails = clsReceiptFormat.Details();
			clsReceiptFormat.CommitAndDispose();

			Data.CashierReport clsCashierReport = new Data.CashierReport();
			Data.CashierReportDetails clsCashierReportDetails = clsCashierReport.Details(mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
			clsCashierReport.GeneratePLUReport(Constants.TerminalBranchID, lblCashier.Text, mclsTerminalDetails.TerminalNo);

			Data.PLUReport clsPLUReport = new Data.PLUReport(clsCashierReport.Connection, clsCashierReport.Transaction);
			System.Data.DataTable dtpluReport = clsPLUReport.dtList(mclsTerminalDetails.TerminalNo, "ProductCode", SortOption.Ascending);

			clsCashierReport.CommitAndDispose();

			DateTime StartDate = clsCashierReportDetails.LastLoginDate;
			DateTime EndDate = DateTime.Now;

			DialogResult result = DialogResult.OK;

			if (mclsTerminalDetails.PreviewTerminalReport)
			{
				PLUReportWnd clsPLUReportWnd = new PLUReportWnd();
				clsPLUReportWnd.IsVATInclusive = mclsTerminalDetails.IsVATInclusive;
				clsPLUReportWnd.CashierName = lblCashier.Text;
				clsPLUReportWnd.dtPLUReport = dtpluReport;
				clsPLUReportWnd.CashierReportDetail = clsCashierReportDetails;
				clsPLUReportWnd.ReceiptFormatDetails = clsReceiptFormatDetails;
				clsPLUReportWnd.StartDate = StartDate;
				clsPLUReportWnd.EndDate = EndDate;
				clsPLUReportWnd.ShowDialog(this);
				result = clsPLUReportWnd.Result;
				clsPLUReportWnd.Close();
				clsPLUReportWnd.Dispose();
			}

			if (result == DialogResult.OK)
			{
				PrintPLUReportDelegate plureportDel = new PrintPLUReportDelegate(PrintPLUReport);
				plureportDel.BeginInvoke(dtpluReport, clsCashierReportDetails, clsReceiptFormatDetails, StartDate, EndDate, null, null);
			}
		}

		private delegate void PrintPLUReportDelegate(System.Data.DataTable dtPLUReport, Data.CashierReportDetails clsCashierReportDetails, Reports.ReceiptFormatDetails clsDetails, DateTime StartDate, DateTime EndDate);
		private void PrintPLUReport(System.Data.DataTable dtPLUReport, Data.CashierReportDetails clsCashierReportDetails, Reports.ReceiptFormatDetails clsDetails, DateTime StartDate, DateTime EndDate)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				decimal TotalQuantity = 0;
				decimal TotalAmount = 0;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PLU REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Start Date: " + StartDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += CenterString("End Date  : " + EndDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine + Environment.NewLine;
				mstrToPrint += "Item           Quantity           Amount" + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				foreach (System.Data.DataRow dr in dtPLUReport.Rows)
				{
					string stProductCode = dr["ProductCode"].ToString();
					string stQuantity = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");
					string stAmount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

					if (stProductCode.Length <= 11)
					{
						mstrToPrint += stProductCode.PadRight(11);
						mstrToPrint += stQuantity.PadLeft(12);
						mstrToPrint += stAmount.PadLeft(17);
						mstrToPrint += Environment.NewLine;
					}
					else
					{
						mstrToPrint += stProductCode + Environment.NewLine;
						mstrToPrint += stQuantity.PadLeft(23);
						mstrToPrint += stAmount.PadLeft(17);
						mstrToPrint += Environment.NewLine;
					}

					TotalQuantity += Convert.ToDecimal(dr["Quantity"]);
					TotalAmount += Convert.ToDecimal(dr["Amount"]);
				}
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Total:".PadRight(6);
				mstrToPrint += TotalQuantity.ToString("#,##0.#0").PadLeft(17);
				mstrToPrint += TotalAmount.ToString("#,##0.#0").PadLeft(17);
				mstrToPrint += Environment.NewLine;
				mstrToPrint += "Less Discount :".PadRight(16);
				mstrToPrint += clsCashierReportDetails.SubTotalDiscount.ToString("#,##0.#0").PadLeft(24);
				mstrToPrint += Environment.NewLine;
				mstrToPrint += "Plus Charges  :".PadRight(16);
				mstrToPrint += clsCashierReportDetails.TotalCharge.ToString("#,##0.#0").PadLeft(24);
				mstrToPrint += Environment.NewLine;
				if (!mclsTerminalDetails.IsVATInclusive)
				{
					mstrToPrint += "Plus 12% VAT  :".PadRight(16);
					mstrToPrint += clsCashierReportDetails.VAT.ToString("#,##0.#0").PadLeft(24);
					mstrToPrint += Environment.NewLine;
				}

				decimal GrandTotal = clsCashierReportDetails.DailySales + clsCashierReportDetails.VAT + clsCashierReportDetails.TotalCharge;
				mstrToPrint += "Grand Total   :".PadRight(16);
				mstrToPrint += GrandTotal.ToString("#,##0.#0").PadLeft(24);
				mstrToPrint += Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintPLUReport, "Print PLU report: TerminalNo=" + mclsTerminalDetails.TerminalNo);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing PLU report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintEJournalReport
		private void PrintEJournalReport()
		{
			//			Reports.ReceiptFormat clsReceiptFormat = new Reports.ReceiptFormat();
			//			ReceiptFormatDetails clsReceiptFormatDetails = clsReceiptFormat.Details();
			//			clsReceiptFormat.CommitAndDispose();

			Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
			Data.SalesTransactionDetails[] salesDetails = clsTerminalReport.EJournalReport(Constants.TerminalBranchID, lblCashier.Text, mclsTerminalDetails.TerminalNo);
			clsTerminalReport.CommitAndDispose();

			DialogResult result = DialogResult.OK;

			if (mclsTerminalDetails.PreviewTerminalReport)
			{
				EJournalReportWnd clsEJournalReportWnd = new EJournalReportWnd();
				clsEJournalReportWnd.CashierName = lblCashier.Text;
				clsEJournalReportWnd.SalesDetails = salesDetails;
				clsEJournalReportWnd.CONFIG_MAX_RECEIPT_WIDTH = mclsTerminalDetails.MaxReceiptWidth;
				clsEJournalReportWnd.CONFIG_ENABLEEVAT = mclsTerminalDetails.EnableEVAT;
				clsEJournalReportWnd.ShowDialog(this);
				result = clsEJournalReportWnd.Result;
				clsEJournalReportWnd.Close();
				clsEJournalReportWnd.Dispose();
			}

			if (result == DialogResult.OK)
			{
				PrintEJournalReportDelegate ejournalreportDel = new PrintEJournalReportDelegate(PrintEJournalReport);
				ejournalreportDel.BeginInvoke(salesDetails, null, null);
			}
		}

		private delegate void PrintEJournalReportDelegate(Data.SalesTransactionDetails[] salesDetails);
		private void PrintEJournalReport(Data.SalesTransactionDetails[] salesDetails)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				mboIsItemHeaderPrinted = true;

				PrintReportHeaderSection(false);

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("ELECTRONIC JOURNAL REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				Data.ChequePaymentDetails[] arrChequePaymentDetails;
				Data.CreditCardPaymentDetails[] arrCreditCardPaymentDetails;
				Data.CreditPaymentDetails[] arrCreditPaymentDetails;
				Data.DebitPaymentDetails[] arrDebitPaymentDetails;
				Data.Payment clspayment = new Data.Payment();

				foreach (Data.SalesTransactionDetails trandetails in salesDetails)
				{
					// set the details
					mclsSalesTransactionDetails = trandetails;
					/*** 
					 * Print the Headers
					 * OFFICIAL RECEIPT #:
					 * Transaction Date 
					 * Item Header
					 ***/

					PrintReportPageHeaderSection(true);

					/*** 
					 * Print the Items
					 ***/
					int itemno = 0;
					decimal TotalItemSold = 0;
					decimal iTotalQuantitySold = 0;
					foreach (Data.SalesTransactionItemDetails item in trandetails.TransactionItems)
					{
						itemno++;
						TotalItemSold++;
						iTotalQuantitySold += item.Quantity;
						string stProductCode = item.Description;
						if (item.MatrixDescription != string.Empty && item.MatrixDescription != null) stProductCode += "-" + item.MatrixDescription;
						PrintItem(itemno.ToString(), stProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
					}

					/*** 
					 * Print the Footer
					 ***/
					/*********************************************************************************/
					PrintPageFooterASection();

					if (trandetails.TransactionStatus == TransactionStatus.Refund)
					{
						if (trandetails.CashPayment != 0)
							mstrToPrint += "Cash Refund :       " + trandetails.CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

						if (trandetails.ChequePayment != 0)
						{
							mstrToPrint += "Cheque Refund  :    " + trandetails.ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

							arrChequePaymentDetails = clspayment.arrChequePaymentDetails(trandetails.TransactionID);

							if (arrChequePaymentDetails != null)
							{
								foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
								{
									//print cheque details
									mstrToPrint += "Cheque No.     :    " + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Amount         :    " + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Validity Date  :    " + chequepaymentdet.ValidityDate.ToString("MM/dd/yyyy").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += Environment.NewLine;
								}
							}
						}

						if (trandetails.CreditCardPayment != 0)
						{
							mstrToPrint += "Credit Card Refund: " + trandetails.CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

							arrCreditCardPaymentDetails = clspayment.arrCreditCardPaymentDetails(trandetails.TransactionID);

							if (arrCreditCardPaymentDetails != null)
							{
								foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
								{
									//print credit card details
									mstrToPrint += "Member Name    :    " + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Amount         :    " + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Validity Date  :    " + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += Environment.NewLine;
								}
							}
						}
					}
					else if (trandetails.TransactionStatus == TransactionStatus.Closed || trandetails.TransactionStatus == TransactionStatus.Reprinted || trandetails.TransactionStatus == TransactionStatus.Open || trandetails.TransactionStatus == TransactionStatus.CreditPayment)
					{
						mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

						if (trandetails.CashPayment != 0)
							mstrToPrint += "Cash Payment :      " + trandetails.CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

						if (trandetails.ChequePayment != 0)
						{
							mstrToPrint += "Cheque Payment :    " + trandetails.ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

							arrChequePaymentDetails = clspayment.arrChequePaymentDetails(trandetails.TransactionID);
							if (arrChequePaymentDetails != null)
							{
								foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
								{
									//print checque details
									mstrToPrint += "Cheque No.     :    " + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Amount         :    " + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Validity Date  :    " + chequepaymentdet.ValidityDate.ToString("MM/dd/yyyy").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += Environment.NewLine;
								}
							}
						}

						if (trandetails.CreditCardPayment != 0)
						{
							mstrToPrint += "Credit Card Payment:" + trandetails.CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;

							arrCreditCardPaymentDetails = clspayment.arrCreditCardPaymentDetails(trandetails.TransactionID);
							if (arrCreditCardPaymentDetails != null)
							{
								foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
								{
									//print credit card details
									mstrToPrint += "Member Name    :    " + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Amount         :    " + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += "Validity Date  :    " + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += Environment.NewLine;
								}
							}
						}
						if (trandetails.CreditPayment != 0)
						{
							mstrToPrint += "Credit Payment:" + trandetails.CreditPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15) + Environment.NewLine;

							arrCreditPaymentDetails = clspayment.arrCreditPaymentDetails(trandetails.TransactionID);
							if (arrCreditPaymentDetails != null)
							{
								foreach (Data.CreditPaymentDetails creditpaymentdet in arrCreditPaymentDetails)
								{
									//print credit details
									mstrToPrint += "Amount         :    " + creditpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += Environment.NewLine;
								}
							}

						}
						if (trandetails.DebitPayment != 0)
						{
							mstrToPrint += "Debit  Payment:" + trandetails.DebitPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15) + Environment.NewLine;

							arrDebitPaymentDetails = clspayment.arrDebitPaymentDetails(trandetails.TransactionID);
							if (arrDebitPaymentDetails != null)
							{
								foreach (Data.DebitPaymentDetails debitpaymentdet in arrDebitPaymentDetails)
								{
									//print debit details
									mstrToPrint += "Amount         :    " + debitpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine;
									mstrToPrint += Environment.NewLine;
								}
							}
						}
						if (trandetails.RewardConvertedPayment != 0)
						{
							mstrToPrint += "Reward Paymnt:      " + trandetails.RewardConvertedPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 20) + Environment.NewLine; 
						}
					}

					if (trandetails.TransactionStatus == TransactionStatus.Suspended)
					{
						mstrToPrint += CenterString("This transaction is suspended", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					}
					else if (trandetails.TransactionStatus == TransactionStatus.Void)
					{
						mstrToPrint += CenterString("This transaction is VOID", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					}
					else if (trandetails.TransactionStatus == TransactionStatus.Reprinted)
					{
						mstrToPrint += CenterString("This transaction is reprinted as of ", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
						mstrToPrint += CenterString(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					}
					else if (trandetails.TransactionStatus == TransactionStatus.Refund)
					{
						mstrToPrint += CenterString("This transaction is a REFUND", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					}
					else if (trandetails.TransactionStatus == TransactionStatus.CreditPayment)
					{
						mstrToPrint += CenterString("------CREDIT PAYMENT--------", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					}

					PrintPageFooterBSection();
					/*********************************************************************************/
					mstrToPrint += Environment.NewLine + "=".PadRight(mclsTerminalDetails.MaxReceiptWidth, '=') + Environment.NewLine;
				}

				clspayment.CommitAndDispose();

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintElectronicJournal, "Print Electronic Journal report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " Cashier:" + lblCashier.Text);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing Electronic Journal report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintPLUReportPerOrderSlipPrinter
		private void PrintPLUReportPerOrderSlipPrinter()
		{
			Reports.ReceiptFormat clsReceiptFormat = new Reports.ReceiptFormat();
			ReceiptFormatDetails clsReceiptFormatDetails = clsReceiptFormat.Details();
			clsReceiptFormat.CommitAndDispose();

			Data.CashierReport clsCashierReport = new Data.CashierReport();
			Data.CashierReportDetails clsCashierReportDetails = clsCashierReport.Details(mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
			clsCashierReport.GeneratePLUReport(Constants.TerminalBranchID, lblCashier.Text, mclsTerminalDetails.TerminalNo);

			Data.PLUReport clsPLUReport = new Data.PLUReport(clsCashierReport.Connection, clsCashierReport.Transaction);
			System.Data.DataTable dtpluReport = clsPLUReport.dtList(mclsTerminalDetails.TerminalNo, "ProductCode", SortOption.Ascending);

			clsCashierReport.CommitAndDispose();

			DateTime StartDate = clsCashierReportDetails.LastLoginDate;
			DateTime EndDate = DateTime.Now;

			DialogResult result = DialogResult.OK;

			if (mclsTerminalDetails.PreviewTerminalReport)
			{
				PLUReportWnd clsPLUReportWnd = new PLUReportWnd();
				clsPLUReportWnd.IsVATInclusive = mclsTerminalDetails.IsVATInclusive;
				clsPLUReportWnd.CashierName = lblCashier.Text;
				clsPLUReportWnd.dtPLUReport = dtpluReport;
				clsPLUReportWnd.CashierReportDetail = clsCashierReportDetails;
				clsPLUReportWnd.ReceiptFormatDetails = clsReceiptFormatDetails;
				clsPLUReportWnd.StartDate = StartDate;
				clsPLUReportWnd.EndDate = EndDate;
				clsPLUReportWnd.ShowDialog(this);
				result = clsPLUReportWnd.Result;
				clsPLUReportWnd.Close();
				clsPLUReportWnd.Dispose();
			}

			if (result == DialogResult.OK)
			{
				// put variables in which printer to print
				int RetailPlusOSPrinter1Ctr = 0;int RetailPlusOSPrinter2Ctr = 0;int RetailPlusOSPrinter3Ctr = 0;int RetailPlusOSPrinter4Ctr = 0;int RetailPlusOSPrinter5Ctr = 0;

				foreach (System.Data.DataRow dr in dtpluReport.Rows)
				{
					AceSoft.RetailPlus.OrderSlipPrinter locOrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
					switch (locOrderSlipPrinter)
					{
						case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1: { RetailPlusOSPrinter1Ctr++; break; }
						case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2: { RetailPlusOSPrinter2Ctr++; break; }
						case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3: { RetailPlusOSPrinter3Ctr++; break; }
						case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4: { RetailPlusOSPrinter4Ctr++; break; }
						case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5: { RetailPlusOSPrinter5Ctr++; break; }
					}
				}

				if (RetailPlusOSPrinter1Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, clsReceiptFormatDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1);
				if (RetailPlusOSPrinter2Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, clsReceiptFormatDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2);
				if (RetailPlusOSPrinter3Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, clsReceiptFormatDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3);
				if (RetailPlusOSPrinter4Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, clsReceiptFormatDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4);
				if (RetailPlusOSPrinter5Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, clsReceiptFormatDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5);
			}
		}

		private void PrintPLUReportPerOrderSlipPrinter(System.Data.DataTable dtPLUReport, Data.CashierReportDetails clsCashierReportDetails, Reports.ReceiptFormatDetails clsDetails, DateTime StartDate, DateTime EndDate, OrderSlipPrinter blockOrderSlipPrinter)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				decimal TotalQuantity = 0;
				decimal TotalAmount = 0;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PLU REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += CenterString(blockOrderSlipPrinter.ToString("G"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Start Date: " + StartDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += CenterString("End Date  : " + EndDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine + Environment.NewLine;
				mstrToPrint += "Item           Quantity           Amount" + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				foreach (System.Data.DataRow dr in dtPLUReport.Rows)
				{
					OrderSlipPrinter enumOrderSlipPrinter = (OrderSlipPrinter) Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());

					if (blockOrderSlipPrinter == enumOrderSlipPrinter)
					{
						string stProductCode = dr["ProductCode"].ToString();
						string stQuantity = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");
						string stAmount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

						if (stProductCode.Length <= 11)
						{
							mstrToPrint += stProductCode.PadRight(11);
							mstrToPrint += stQuantity.PadLeft(12);
							mstrToPrint += stAmount.PadLeft(17);
							mstrToPrint += Environment.NewLine;
						}
						else
						{
							mstrToPrint += stProductCode + Environment.NewLine;
							mstrToPrint += stQuantity.PadLeft(23);
							mstrToPrint += stAmount.PadLeft(17);
							mstrToPrint += Environment.NewLine;
						}

						TotalQuantity += Convert.ToDecimal(dr["Quantity"]);
						TotalAmount += Convert.ToDecimal(dr["Amount"]);
					}
				}
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Total:".PadRight(6);
				mstrToPrint += TotalQuantity.ToString("#,##0.#0").PadLeft(17);
				mstrToPrint += TotalAmount.ToString("#,##0.#0").PadLeft(17);
				mstrToPrint += Environment.NewLine;
				mstrToPrint += "Less Discount :".PadRight(16);
				mstrToPrint += clsCashierReportDetails.SubTotalDiscount.ToString("#,##0.#0").PadLeft(24);
				mstrToPrint += Environment.NewLine;
				mstrToPrint += "Plus Charges  :".PadRight(16);
				mstrToPrint += clsCashierReportDetails.TotalCharge.ToString("#,##0.#0").PadLeft(24);
				mstrToPrint += Environment.NewLine;
				if (!mclsTerminalDetails.IsVATInclusive)
				{
					mstrToPrint += "Plus 12% VAT  :".PadRight(16);
					mstrToPrint += clsCashierReportDetails.VAT.ToString("#,##0.#0").PadLeft(24);
					mstrToPrint += Environment.NewLine;
				}

				decimal GrandTotal = clsCashierReportDetails.DailySales + clsCashierReportDetails.VAT + clsCashierReportDetails.TotalCharge;
				mstrToPrint += "Grand Total   :".PadRight(16);
				mstrToPrint += GrandTotal.ToString("#,##0.#0").PadLeft(24);
				mstrToPrint += Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintPLUReport, "Print PLU report per OrderSlipprinter: " + blockOrderSlipPrinter.ToString("G") + " TerminalNo=" + mclsTerminalDetails.TerminalNo);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing PLU report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintTerminalReport
		private void PrintTerminalReport()
		{
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTerminalReport);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintTerminalReport;
				login.Header = "Print Terminal Report";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
				Data.TerminalReportDetails Details = clsTerminalReport.Details(mclsTerminalDetails.TerminalNo);
				clsTerminalReport.CommitAndDispose();

				DialogResult result = DialogResult.OK;

				if (mclsTerminalDetails.PreviewTerminalReport)
				{
					TerminalReportWnd clsTerminalReportWnd = new TerminalReportWnd();
					clsTerminalReportWnd.Details = Details;
					clsTerminalReportWnd.CashierName = lblCashier.Text;
					clsTerminalReportWnd.TrustFund = 0; // mclsTerminalDetails.TrustFund;
					clsTerminalReportWnd.ShowDialog(this);
					result = clsTerminalReportWnd.Result;
					clsTerminalReportWnd.Close();
					clsTerminalReportWnd.Dispose();

				}
				if (result == DialogResult.OK)
				{
					PrintTerminalReportDelegate terminalreportDel = new PrintTerminalReportDelegate(PrintTerminalReport);
					terminalreportDel.BeginInvoke(Details, null, null);
				}
			}
		}
		private delegate void PrintTerminalReportDelegate(Data.TerminalReportDetails Details);
		private void PrintTerminalReport(Data.TerminalReportDetails Details)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;
	
				PrintReportHeadersSection(false);

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Terminal Report", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				mstrToPrint += "Beginning Tran. No. :" + Details.BeginningTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Ending Tran. No.    :" + Details.EndingTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Gross Sales         :" + Convert.ToDecimal(Details.GrossSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Total Discount (-)  :" + Convert.ToDecimal(Details.TotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				decimal TotalAmount = Details.DailySales + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge;

				mstrToPrint += "Net Sales           :" + Convert.ToDecimal(Details.DailySales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Service Charge      :" + Convert.ToDecimal(Details.TotalCharge).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Amount        :" + Convert.ToDecimal(TotalAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				if (mclsTerminalDetails.WillPrintGrandTotal == true)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += "OLD GRAND TOTAL     :" + Convert.ToDecimal(Details.OldGrandTotal).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "This Total Amount   :" + Convert.ToDecimal(TotalAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
					mstrToPrint += "NEW GRAND TOTAL     :" + Convert.ToDecimal(Details.NewGrandTotal).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "VATable Amount      :" + Convert.ToDecimal(Details.VATableAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Non-Vatable Amt.    :" + Convert.ToDecimal(Details.NonVaTableAmount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Local Tax           :" + Convert.ToDecimal(Details.LocalTax).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Sales          :" + Convert.ToDecimal(Details.CashSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Sales        :" + Convert.ToDecimal(Details.ChequeSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Sales   :" + Convert.ToDecimal(Details.CreditCardSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit (Charge)     :" + Convert.ToDecimal(Details.CreditSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment      :" + Convert.ToDecimal(Details.CreditPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit  Sales        :" + Convert.ToDecimal(Details.DebitPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "RewardPoints Redeemd:" + Convert.ToDecimal(Details.RewardPointsPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "     Cash Equivalent:" + Convert.ToDecimal(Details.RewardConvertedPayment).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Employee Acct.      :" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Sales          :" + Convert.ToDecimal(Details.VoidSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Sales        :" + Convert.ToDecimal(Details.RefundSales).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Items Discount      :" + Convert.ToDecimal(Details.ItemsDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Subtotal Discount   :" + Convert.ToDecimal(Details.SubTotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Discounts     :" + Convert.ToDecimal(Details.TotalDiscount).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
				System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.TerminalNo, Details.BeginningTransactionNo, Details.EndingTransactionNo);
				clsSalesTransactions.CommitAndDispose();
				if (dt.Rows.Count > 0)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					foreach (System.Data.DataRow dr in dt.Rows)
					{ mstrToPrint += dr["DiscountCode"].ToString().PadRight(20, ' ') + ":" + Convert.ToDecimal(Convert.ToDecimal(dr["Discount"])).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine; }
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Beginning Balance   :" + Convert.ToDecimal(Details.BeginningBalance).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cash-In-Drawer      :" + Convert.ToDecimal(Details.CashInDrawer).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Paid Out            :" + Convert.ToDecimal(Details.TotalPaidOut).ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDisburse).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardWithHold).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDeposit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Transactions   :" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Trans.  :" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Transactions :" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Transactions :" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit Payment Trans.:" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Transactions :" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Transactions   :" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Combination Tran    :" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment Trans:" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Reward Points  Trans:" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				//				mstrToPrint += "Employees Acct Trans:" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Transactions  :" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintTerminalReport, "Print Terminal report: TerminalNo=" + Details.TerminalNo + " CashInDrawer=" + Details.CashInDrawer.ToString("#,##0.#0"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing terminal report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		private delegate void PrintTerminalReportWithTrustFundDelegate(Data.TerminalReportDetails Details);
		private void PrintTerminalReportWithTrustFund(Data.TerminalReportDetails Details)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Terminal Report", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				mstrToPrint += "Beginning Tran. No. :" + Details.BeginningTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Ending Tran. No.    :" + Details.EndingTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Gross Sales         :" + Convert.ToDecimal(Details.GrossSales - (Details.GrossSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Total Discount (-)  :" + Convert.ToDecimal(Details.TotalDiscount - (Details.TotalDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				decimal TotalAmount = Details.DailySales + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge;

				mstrToPrint += "Net Sales           :" + Convert.ToDecimal(Details.DailySales - (Details.DailySales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT - (Details.VAT * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Service Charge      :" + Convert.ToDecimal(Details.TotalCharge - (Details.TotalCharge * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Amount        :" + Convert.ToDecimal(TotalAmount - (TotalAmount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				if (mclsTerminalDetails.WillPrintGrandTotal == true)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += "OLD GRAND TOTAL     :" + Convert.ToDecimal(Details.OldGrandTotal - (Details.OldGrandTotal * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "This Total Amount   :" + Convert.ToDecimal(TotalAmount - (TotalAmount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
					mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
					mstrToPrint += "NEW GRAND TOTAL     :" + Convert.ToDecimal(Details.NewGrandTotal - (Details.NewGrandTotal * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "VATable Amount      :" + Convert.ToDecimal(Details.VATableAmount - (Details.VATableAmount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Non-Vatable Amt.    :" + Convert.ToDecimal(Details.NonVaTableAmount - (Details.NonVaTableAmount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Convert.ToDecimal(Details.VAT - (Details.VAT * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Local Tax           :" + Convert.ToDecimal(Details.LocalTax - (Details.LocalTax * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Sales          :" + Convert.ToDecimal(Details.CashSales - (Details.CashSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Sales        :" + Convert.ToDecimal(Details.ChequeSales - (Details.ChequeSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Sales   :" + Convert.ToDecimal(Details.CreditCardSales - (Details.CreditCardSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit (Charge)     :" + Convert.ToDecimal(Details.CreditSales - (Details.CreditSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment      :" + Convert.ToDecimal(Details.CreditPayment - (Details.CreditPayment * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit  Sales        :" + Convert.ToDecimal(Details.DebitPayment - (Details.DebitPayment * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "RewardPoints Redeemd:" + Convert.ToDecimal(Details.RewardPointsPayment - (Details.RewardPointsPayment * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "     Cash Equivalent:" + Convert.ToDecimal(Details.RewardConvertedPayment - (Details.RewardConvertedPayment * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Employee Acct.      :" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Sales          :" + Convert.ToDecimal(Details.VoidSales - (Details.VoidSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Sales        :" + Convert.ToDecimal(Details.RefundSales - (Details.RefundSales * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Items Discount      :" + Convert.ToDecimal(Details.ItemsDiscount - (Details.ItemsDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Subtotal Discount   :" + Convert.ToDecimal(Details.SubTotalDiscount - (Details.SubTotalDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Discounts     :" + Convert.ToDecimal(Details.TotalDiscount - (Details.TotalDiscount * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
				System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.TerminalNo, Details.BeginningTransactionNo, Details.EndingTransactionNo);
				clsSalesTransactions.CommitAndDispose();
				if (dt.Rows.Count > 0)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					foreach (System.Data.DataRow dr in dt.Rows)
					{ mstrToPrint += dr["DiscountCode"].ToString().PadRight(20, ' ') + ":" + Convert.ToDecimal(Convert.ToDecimal(dr["Discount"]) - (Convert.ToDecimal(dr["Discount"]) * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine; }
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Beginning Balance   :" + Convert.ToDecimal(Details.BeginningBalance - (Details.BeginningBalance * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cash-In-Drawer      :" + Convert.ToDecimal(Details.CashInDrawer - (Details.CashInDrawer * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Paid Out            :" + Convert.ToDecimal(Details.TotalPaidOut - (Details.TotalPaidOut * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDisburse - (Details.CashDisburse * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDisburse - (Details.ChequeDisburse * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDisburse - (Details.CreditCardDisburse * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashWithHold - (Details.CashWithHold * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeWithHold - (Details.ChequeWithHold * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardWithHold - (Details.CreditCardWithHold * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Convert.ToDecimal(Details.CashDeposit - (Details.CashDeposit * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Convert.ToDecimal(Details.ChequeDeposit - (Details.ChequeDeposit * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Convert.ToDecimal(Details.CreditCardDeposit - (Details.CreditCardDeposit * (mclsTerminalDetails.TrustFund / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Transactions   :" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Trans.  :" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Transactions :" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Transactions :" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit Payment Trans.:" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Transactions :" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Transactions   :" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Combination Tran    :" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment Trans:" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Reward Points  Trans:" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				//				mstrToPrint += "Employees Acct Trans:" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Transactions  :" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintTerminalReport, "Print Terminal report: TerminalNo=" + Details.TerminalNo + " CashInDrawer=" + Details.CashInDrawer.ToString("#,##0.#0"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing terminal report. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintCashiersReport
		private void PrintCashiersReport()
		{
			DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintCashierReport);

			if (loginresult == DialogResult.None)
			{
				LogInWnd login = new LogInWnd();

				login.AccessType = AccessTypes.PrintCashierReport;
				login.Header = "Print Cashier Report";
				login.ShowDialog(this);
				loginresult = login.Result;
				login.Close();
				login.Dispose();
			}

			if (loginresult == DialogResult.OK)
			{
				DateTime dte = DateTime.Now;

				Data.CashierReport clsCashierReport = new Data.CashierReport();
				Data.CashierReportDetails Details = clsCashierReport.Details(mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
				clsCashierReport.CommitAndDispose();

				DialogResult result = DialogResult.OK;

				if (mclsTerminalDetails.PreviewTerminalReport)
				{
					CashierReportWnd clsCashierReportWnd = new CashierReportWnd();
					clsCashierReportWnd.Details = Details;
					clsCashierReportWnd.CashierName = lblCashier.Text;
					clsCashierReportWnd.ShowDialog(this);
					result = clsCashierReportWnd.Result;
					clsCashierReportWnd.Close();
					clsCashierReportWnd.Dispose();
				}

				if (result == DialogResult.OK)
				{
					PrintCashiersReportDelegate cashierreportDel = new PrintCashiersReportDelegate(PrintCashiersReport);
					cashierreportDel.BeginInvoke(Details, null, null);
				}
			}

		}
		private delegate void PrintCashiersReportDelegate(Data.CashierReportDetails Details);
		private void PrintCashiersReport(Data.CashierReportDetails Details)
		{
			Cursor.Current = Cursors.WaitCursor;
			OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
			Invoke(opendrawerDel);

			try
			{
				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Cashier's Report : " + lblCashier.Text, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				//				mstrToPrint += "Beginning Tran. No. :" + Details.BeginningTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				//				mstrToPrint += "Ending Tran. No.    :" + Details.EndingTransactionNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "Gross Sales         :" + Details.GrossSales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Total Discount (-)  :" + Details.TotalDiscount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				decimal TotalAmount = Details.DailySales + Details.VAT + Details.EVAT + Details.LocalTax + Details.TotalCharge;

				mstrToPrint += "Net Sales           :" + Details.DailySales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Details.VAT.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Service Charge      :" + Details.TotalCharge.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "This Total Amount   :" + TotalAmount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				//				mstrToPrint += "VATable Amount      :" + Details.VATableAmount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				//				mstrToPrint += "Non-Vatable Amt.    :" + Details.NonVaTableAmount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "VAT                 :" + Details.VAT.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Local Tax           :" + Details.LocalTax.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Sales          :" + Details.CashSales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Sales        :" + Details.ChequeSales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Sales   :" + Details.CreditCardSales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Sales        :" + Details.CreditPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit  Sales        :" + Details.DebitPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "RewardPoints Redeemd:" + Details.RewardPointsPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "     Cash Equivalent:" + Details.RewardConvertedPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Employee Acct.      :" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Sales          :" + Details.VoidSales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Sales        :" + Details.RefundSales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Items Discount      :" + Details.ItemsDiscount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Subtotal Discount   :" + Details.SubTotalDiscount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Discounts     :" + Details.TotalDiscount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
				Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
				Data.TerminalReportDetails clsTerminalReportDetails = clsTerminalReport.Details(Details.TerminalNo);
				System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.TerminalNo, clsTerminalReportDetails.BeginningTransactionNo, clsTerminalReportDetails.EndingTransactionNo, Details.CashierID);
				clsSalesTransactions.CommitAndDispose();
				clsTerminalReport.CommitAndDispose();
				if (dt.Rows.Count > 0)
				{
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					mstrToPrint += CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
					mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
					foreach (System.Data.DataRow dr in dt.Rows)
					{ mstrToPrint += dr["DiscountCode"].ToString().PadRight(20, ' ') + ":" + Convert.ToDecimal(dr["Discount"]).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine; }
				}

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Beginning Balance   :" + Details.BeginningBalance.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cash-In-Drawer      :" + Details.CashInDrawer.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Paid Out            :" + Details.TotalPaidOut.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Details.CashDisburse.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Details.ChequeDisburse.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Details.CreditCardDisburse.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Details.CashWithHold.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Details.ChequeWithHold.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Details.CreditCardWithHold.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash                :" + Details.CashDeposit.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque              :" + Details.ChequeDeposit.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card         :" + Details.CreditCardDeposit.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Cash Transactions   :" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Card Trans.  :" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Cheque Transactions :" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Transactions :" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Debit Payment Trans.:" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Refund Transactions :" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Void Transactions   :" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Combination Tran    :" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Credit Payment Trans:" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "Reward Points  Trans:" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				//				mstrToPrint += "Employees Acct Trans:" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21)  + Environment.NewLine;
				mstrToPrint += "                    :" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21, ' ') + Environment.NewLine;
				mstrToPrint += "Total Transactions  :" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("Cash Count", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				if (Details.CashInDrawer > Details.CashCount)
				{
					decimal decShort = Details.CashInDrawer - Details.CashCount;
					mstrToPrint += "Short               :" + decShort.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				}
				else if (Details.CashCount > Details.CashInDrawer)
				{
					decimal decOver = Details.CashCount - Details.CashInDrawer;
					mstrToPrint += "Over                :" + decOver.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 21) + Environment.NewLine;
				}
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.PrintCashierReport, "Print cashier report: CashInDrawer=" + Details.CashInDrawer.ToString("#,##0.#0"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing cashier report data. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintCashCount

		private delegate void PrintCashCountDelegate(Data.CashCountDetails[] arrDetails);
		private void PrintCashCount(Data.CashCountDetails[] arrDetails)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
				Invoke(opendrawerDel);

				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("CASH COUNT DECLARATION", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				decimal decTotalAmount = 0;
				foreach (Data.CashCountDetails Details in arrDetails)
				{
					mstrToPrint += Details.DenominationValue.ToString("#,##0.#0").PadLeft(8);
					mstrToPrint += " X ";
					mstrToPrint += Details.DenominationCount.ToString("#,##0").PadRight(10);
					mstrToPrint += " = ";
					mstrToPrint += Details.DenominationAmount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 26) + Environment.NewLine;
					decTotalAmount += Details.DenominationAmount;
				}
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "TOTAL                     " + decTotalAmount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 28) + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.CashCount, "Print CASHCOUNT: " + decTotalAmount.ToString("#,##0.#0"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing cash count data. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintWithHold

		private delegate void PrintWithHoldDelegate(Data.WithHoldDetails details);
		private void PrintWithHold(Data.WithHoldDetails pvtWithHoldDetails)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				lblTransDate.Tag = lblTransDate.Text;
				lblTransDate.Text = pvtWithHoldDetails.DateCreated.ToString("MMM. dd, yyyy hh:mm:ss tt");

				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				//do not remove it here: this will revert the transaction date.
				lblTransDate.Text = lblTransDate.Tag.ToString();

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("WITHHOLD / RCV-ON-ACCOUNT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Amount	 : " + pvtWithHoldDetails.Amount.ToString("#,##0.#0") + Environment.NewLine;
				mstrToPrint += "Wit. Type: " + pvtWithHoldDetails.PaymentType.ToString("G") + Environment.NewLine;
				if (pvtWithHoldDetails.Remarks != string.Empty && pvtWithHoldDetails.Remarks != null)
					mstrToPrint += "Remarks  : " + pvtWithHoldDetails.Remarks + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.Disburse, "Print WITHHOLD: " + pvtWithHoldDetails.Amount.ToString("#,##0.#0") + " " + pvtWithHoldDetails.PaymentType.ToString("G"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing wihhold data. Err Description: " + ex.Message);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintDisbursement

		private delegate void PrintDisbursementDelegate(Data.DisburseDetails pvtDisburseDetails);
		private void PrintDisbursement(Data.DisburseDetails pvtDisburseDetails)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				lblTransDate.Tag = lblTransDate.Text;
				lblTransDate.Text = pvtDisburseDetails.DateCreated.ToString("MMM. dd, yyyy hh:mm:ss tt");

				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				//do not remove it here: this will revert the transaction date.
				lblTransDate.Text = lblTransDate.Tag.ToString();

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("DISBURSEMENT / PICK-UP", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Amount	  : " + pvtDisburseDetails.Amount.ToString("#,##0.#0") + Environment.NewLine;
				mstrToPrint += "Dis. Type : " + pvtDisburseDetails.PaymentType.ToString("G") + Environment.NewLine;
				if (pvtDisburseDetails.Remarks != string.Empty && pvtDisburseDetails.Remarks != null)
					mstrToPrint += "Remarks   : " + pvtDisburseDetails.Remarks + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

				InsertAuditLog(AccessTypes.Disburse, "Print DISBURSEMENT: " + pvtDisburseDetails.Amount.ToString("#,##0.#0") + " " + pvtDisburseDetails.PaymentType.ToString("G"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing disbursement data. Err Description: " + ex.Message);
				Cursor.Current = Cursors.Default;
				throw ex;
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintPaidOut

		private delegate void PrintPaidOutDelegate(Data.PaidOutDetails details);
		private void PrintPaidOut(Data.PaidOutDetails pvtPaidOutDetails)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				lblTransDate.Tag = lblTransDate.Text;
				lblTransDate.Text = pvtPaidOutDetails.DateCreated.ToString("MMM. dd, yyyy hh:mm:ss tt");

				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				//do not remove it here: this will revert the transaction date.
				lblTransDate.Text = lblTransDate.Tag.ToString();

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("PAID-OUT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Amount	   : " + pvtPaidOutDetails.Amount.ToString("#,##0.#0") + Environment.NewLine;
				mstrToPrint += "P-Out Type : " + pvtPaidOutDetails.PaymentType.ToString("G") + Environment.NewLine;
				if (pvtPaidOutDetails.Remarks != string.Empty && pvtPaidOutDetails.Remarks != null)
					mstrToPrint += "Remarks    : " + pvtPaidOutDetails.Remarks + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
				InsertAuditLog(AccessTypes.PaidOut, "Print PAID-OUT: " + pvtPaidOutDetails.Amount.ToString("#,##0.#0") + " " + pvtPaidOutDetails.PaymentType.ToString("G"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing paid-out data. Err Description: " + ex.Message);
				Cursor.Current = Cursors.Default;
				throw ex;
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region PrintDeposit

		private delegate void PrintDepositDelegate(Data.DepositDetails pvtDepositDetails);
		private void PrintDeposit(Data.DepositDetails pvtDepositDetails)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				lblTransDate.Tag = lblTransDate.Text;
				lblTransDate.Text = pvtDepositDetails.DateCreated.ToString("MMM. dd, yyyy hh:mm:ss tt");

				PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
				mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

				PrintReportHeadersSection(false);

				//do not remove it here: this will revert the transaction date.
				lblTransDate.Text = lblTransDate.Tag.ToString();

				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += CenterString("DEPOSIT AMOUNT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine;
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;
				mstrToPrint += "Amount	     :" + pvtDepositDetails.Amount.ToString("#,##0.#0") + Environment.NewLine;
				mstrToPrint += "Dis. Type    :" + pvtDepositDetails.PaymentType.ToString("G") + Environment.NewLine;
				if (pvtDepositDetails.Remarks == string.Empty || pvtDepositDetails.Remarks == null)
					mstrToPrint += "Customer     :" + pvtDepositDetails.ContactName.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14) + Environment.NewLine + Environment.NewLine;
				else
				{
					mstrToPrint += "Customer     :" + pvtDepositDetails.ContactName.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14) + Environment.NewLine;
					mstrToPrint += "Remarks      :" + pvtDepositDetails.Remarks.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 14) + Environment.NewLine + Environment.NewLine;
				}
				mstrToPrint += "-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine;

				PrintReportFooterSection(false);

				mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
				InsertAuditLog(AccessTypes.Deposit, "Print DEPOSIT: " + pvtDepositDetails.Amount.ToString("#,##0.#0") + " " + pvtDepositDetails.PaymentType.ToString("G"));
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("ERROR!!! Printing deposit data. Err Description: " + ex.Message);
				Cursor.Current = Cursors.Default;
				throw ex;
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region CenterString, CutPrinterPaper

		private string CenterString(string Value, int TotalLengthOfCenter)
		{
			string stRetValue = Value;
			Int32 lenvalue = Value.Length;

			if (lenvalue <= TotalLengthOfCenter)
			{
				Int32 padding = (int)(TotalLengthOfCenter - lenvalue) / 2;

				for (int i = 0; i < padding; i++)
				{ stRetValue = " " + stRetValue + " "; }

				if (((TotalLengthOfCenter - lenvalue) % 2) != 0)
					stRetValue += " ";
			}
			else
			{
				stRetValue = Value.Substring(0, TotalLengthOfCenter);
			}
			return stRetValue;
		}

		private void CutPrinterPaper()
		{
			try
			{
				//string command = Convert.ToChar(29).ToString() + Convert.ToChar(86).ToString() + Convert.ToChar(49).ToString();   // cut the paper  Chr$(86)
				RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.PrinterName, RawPrinterHelper.escCut, "RetailPlus Paper Cutter.");
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("Error in auto-cutting the paper from printer. Err Description: " + ex.Message);
			}
		}

		#endregion

		#endregion

		#region Turret

		private delegate void DisplayItemToTurretDelegate(string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT);
		private void DisplayItemToTurret(string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT)
		{
			try
			{
				// clsEvent.AddEventLn("Displaying to turret...", true);

				string stDescription = Description;
				try
				{ stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
				catch { }
				if (stDescription.Length > 20)
					stDescription = stDescription.Substring(0, 20);
				else
					stDescription = stDescription.PadRight(20);

				string stAmount = Amount.ToString("#,##0.#0").PadLeft(20);
				SendStringToTurret(stDescription + stAmount + Environment.NewLine);

				//clsEvent.AddEventLn("Done!");
			}
			catch (Exception ex)
			{
				clsEvent.AddErrorEventLn(ex);
			}


		}

		#endregion

		#region Open Drawer

		private delegate void OpenDrawerDelegate();
		private void OpenDrawer()
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				Event clsEvent = new Event();
				clsEvent.AddEventLn("Opening cash drawer...", true);

				//Chr$(&H1B) + Chr$(&H70) + Chr$(&H0) + Chr$(&H2F) + Chr$(&H3F) '//drawer open
				//				string command = Convert.ToChar("&H1B").ToString() + Convert.ToChar("&H70").ToString() + Convert.ToChar("&H0").ToString() + Convert.ToChar("&H2F").ToString() + Convert.ToChar("&H3F").ToString();   // cut the paper  Chr$(86)
				string command = Convert.ToChar(27).ToString() + Convert.ToChar(112).ToString() + Convert.ToChar(0).ToString() + Convert.ToChar(47).ToString() + Convert.ToChar(63).ToString();   // cut the paper  Chr$(86)
				RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.CashDrawerName, command + "\f", "RetailPlus Drawer.");

				InsertAuditLog(AccessTypes.OpenDrawer, "Open cash drawer.");
				clsEvent.AddEventLn("Done opening cash drawer!", true);
			}
			catch (Exception ex)
			{
				Event clsEvent = new Event();
				clsEvent.AddErrorEventLn(ex);
			}
			Cursor.Current = Cursors.Default;
		}

		#endregion

		#region Audit Logs

		private void InsertAuditLog(AccessTypes AccessType, string Remarks)
		{
			Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();
			clsAuditDetails.ActivityDate = DateTime.Now;
			clsAuditDetails.User = lblCashier.Text;
			clsAuditDetails.IPAddress = System.Net.Dns.GetHostName();
			clsAuditDetails.Activity = AccessType.ToString("G");
			clsAuditDetails.Remarks = "FE:" + Remarks;

			Security.AuditTrail clsAuditTrail = new Security.AuditTrail();
			clsAuditTrail.Insert(clsAuditDetails);
			clsAuditTrail.CommitAndDispose();
		}
		private void InsertAuditLog(MySqlConnection pvtConnection, MySqlTransaction pvtTransaction, AccessTypes AccessType, string Remarks)
		{
			Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();
			clsAuditDetails.ActivityDate = DateTime.Now;
			clsAuditDetails.User = lblCashier.Text;
			clsAuditDetails.IPAddress = System.Net.Dns.GetHostName();
			clsAuditDetails.Activity = AccessType.ToString("G");
			clsAuditDetails.Remarks = "FE:" + Remarks;

			Security.AuditTrail clsAuditTrail = new Security.AuditTrail(pvtConnection, pvtTransaction);
			clsAuditTrail.Insert(clsAuditDetails);
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
					case 5: { this.ShowOtherCommands(); break; }

					//	
					case 6: { this.ShowPrintWindow(); break; }

					//	
					case 7: { this.PrintCheckOutBill(); break; }

					//	
					case 8: this.ApplyTransDiscount(); break;

					//	
					case 9: this.ApplyTransCharge(); break;

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
							DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTransactionHeader);

							if (loginresult == DialogResult.None)
							{
								LogInWnd login = new LogInWnd();

								login.AccessType = AccessTypes.PrintTransactionHeader;
								login.Header = "Print Transaction Header";
								login.ShowDialog(this);
								loginresult = login.Result;
								login.Close();
								login.Dispose();
							}
							if (loginresult == DialogResult.OK)
							{
								PrintReportHeaderSection(true);
								MessageBox.Show("Transaction header has been printed.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							break;
						}

					//	Open Drawer
					case 27: this.OpenDrawer(); break;

					//	Z-read
					case 28: { this.InitializeZRead(); break; }

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
			catch { }
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
					if (dgCell.ColumnNumber == 13) this.ChangeAmount();
					if (dgCell.ColumnNumber == 39) this.ChangePaxNo();

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
			dgItems_CurrentCellChanged(sender, e);
			try { txtBarCode.Focus(); }
			catch { }
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
			try { dgItems.Select(dgItems.CurrentRowIndex); }
			catch { }
		}
		private void lblTransNo_Click(object sender, System.EventArgs e)
		{
			// ShowMainMenu(02);
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

				long lngSequenceNoStart = 0;

				if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
					try { lngSequenceNoStart = long.Parse(cmdSubGroupLeft.Tag.ToString()); }
					catch { }
				else
					try { lngSequenceNoStart = long.Parse(cmdSubGroupRight.Tag.ToString()); }
					catch { }

				ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
				clsProductSubGroupColumns.ProductSubGroupCode = true;
				clsProductSubGroupColumns.SequenceNo = true;

				ProductSubGroup clsProductSubGroup = new ProductSubGroup();
				System.Data.DataTable dtProductSubGroup = clsProductSubGroup.ListAsDataTable(clsProductSubGroupColumns, new ProductSubGroupDetails(), lngSequenceNoStart, SequenceSortOrder, Constants.C_RESTOPLUS_MAX_SUB_GROUP + 1, "SequenceNo", SequenceSortOrder);
				clsProductSubGroup.CommitAndDispose();

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

					if (iCtr == Constants.C_RESTOPLUS_MAX_SUB_GROUP && dtProductSubGroup.Rows.Count > Constants.C_RESTOPLUS_MAX_SUB_GROUP)
					{
						if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
							try { if (iCtr==1) cmdSubGroupLeft.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
						else
							try { cmdSubGroupRight.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
					}
					else if (dtProductSubGroup.Rows.Count > 0 && dtProductSubGroup.Rows.Count <= Constants.C_RESTOPLUS_MAX_SUB_GROUP)
					{
						if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
						{
							try { if (iCtr == 1) cmdSubGroupRight.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
							cmdSubGroupLeft.Tag = "0".ToString();
						}
						else
						{
							try { if (iCtr == 1) cmdSubGroupLeft.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
							// cmdSubGroupRight.Tag = cmdSubGroupRight.Tag; // do not reset
						}
					}

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

					try { cmdSubGroup.Image = new Bitmap(Application.StartupPath + "/images/subgroups/subgroup" + iCtr.ToString() + ".gif"); }
					catch { }

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

				long lngSequenceNoStart = 0;

				if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
					try { lngSequenceNoStart = long.Parse(cmdProductLeft.Tag.ToString()); }
					catch { }
				else
					try { lngSequenceNoStart = long.Parse(cmdProductRight.Tag.ToString()); }
					catch { }

				ProductColumns clsProductColumns = new ProductColumns();
				clsProductColumns.BarCode = true;
				clsProductColumns.ProductCode = true;
				clsProductColumns.SequenceNo = true;

				ProductColumns clsSearchColumns = new ProductColumns();

				long lngProductSubGroupID = 0;
				try { lngProductSubGroupID = long.Parse(lblItems.Tag.ToString()); }
				catch { }
				Products clsProduct = new Products();
				System.Data.DataTable dtProduct = clsProduct.ListAsDataTable(clsProductColumns, Constants.TerminalBranchID, ProductListFilterType.ShowActiveOnly, lngSequenceNoStart, SequenceSortOrder,
					clsSearchColumns, string.Empty, Constants.ZERO, Constants.ZERO, string.Empty, lngProductSubGroupID, string.Empty, Constants.C_RESTOPLUS_MAX_PRODUCTS + 1, mclsTerminalDetails.ShowItemMoreThanZeroQty, true, "SequenceNo", SortOption.Ascending);
				clsProduct.CommitAndDispose();

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

					if (iCtr == Constants.C_RESTOPLUS_MAX_PRODUCTS && dtProduct.Rows.Count > Constants.C_RESTOPLUS_MAX_PRODUCTS)
					{
						if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
							try { if (iCtr == 1) cmdProductLeft.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
						else
							try { cmdProductRight.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
					}
					else if (dtProduct.Rows.Count > 0 && dtProduct.Rows.Count <= Constants.C_RESTOPLUS_MAX_PRODUCTS)
					{
						if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
						{
							try { if (iCtr == 1) cmdProductRight.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
							cmdProductLeft.Tag = "0".ToString();
						}
						else
						{
							try { if (iCtr == 1) cmdProductLeft.Tag = dr[Data.ProductSubGroupColumnNames.SequenceNo].ToString(); }
							catch { }
							// cmdProductRight.Tag = cmdProductRight.Tag; // do not reset
						}
					}

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
						string strTempProductCode = "";
						string[] strCodes = strProductCode.Split(' ');
						foreach (string strCode in strCodes)
						{
							if ((strTempProductCode.Length + strCode.Length) < 12)
							{
								strTempProductCode += (strTempProductCode.Length == 0) ? strCode : " " + strCode;
							}
							else
							{
								cmdProduct.Text += (cmdProduct.Text.Length == 0) ? strTempProductCode : "\r\n" + strTempProductCode;
								strTempProductCode = strCode;
							}
							if ((strProductCode.LastIndexOf(strCode) + strCode.Length) == strProductCode.Length) //mean this is the last
								cmdProduct.Text += "\r\n" + strTempProductCode;
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
	}
}