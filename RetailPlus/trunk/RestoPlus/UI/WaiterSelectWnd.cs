using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for WaiterSelectWnd.
	/// </summary>
	public class WaiterSelectWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.PictureBox imgIcon;
		private System.Windows.Forms.DataGrid dgWaiter;
		private System.Windows.Forms.DataGridTableStyle dgStyle;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.DataGridTextBoxColumn WaiterName;


		private Int64 miWaiterID;
		private string mstWaiterName;
		private System.Windows.Forms.DataGridTextBoxColumn WaiterCode;
		private System.Windows.Forms.DataGridTextBoxColumn WaiterID;
        private AceSoft.KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;
		private DialogResult dialog;

		public Int64 getWaiterID
		{
			get { return miWaiterID; }
		}

		public string getWaiterName
		{
			get { return mstWaiterName; }
		}

		public DialogResult Result
		{
			get {	return dialog;	}
		}


		#region Constructors and Destructors

		public WaiterSelectWnd()
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.dgWaiter = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.WaiterID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.WaiterCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.WaiterName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgWaiter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // dgWaiter
            // 
            this.dgWaiter.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgWaiter.BackColor = System.Drawing.Color.White;
            this.dgWaiter.BackgroundColor = System.Drawing.Color.White;
            this.dgWaiter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgWaiter.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgWaiter.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgWaiter.CaptionVisible = false;
            this.dgWaiter.DataMember = "";
            this.dgWaiter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgWaiter.FlatMode = true;
            this.dgWaiter.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgWaiter.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgWaiter.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgWaiter.HeaderForeColor = System.Drawing.Color.White;
            this.dgWaiter.Location = new System.Drawing.Point(0, 196);
            this.dgWaiter.Name = "dgWaiter";
            this.dgWaiter.PreferredRowHeight = 50;
            this.dgWaiter.ReadOnly = true;
            this.dgWaiter.RowHeadersVisible = false;
            this.dgWaiter.RowHeaderWidth = 5;
            this.dgWaiter.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgWaiter.SelectionForeColor = System.Drawing.Color.White;
            this.dgWaiter.Size = new System.Drawing.Size(1022, 570);
            this.dgWaiter.TabIndex = 9;
            this.dgWaiter.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgWaiter.TabStop = false;
            this.dgWaiter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgWaiter_MouseDown);
            // 
            // dgStyle
            // 
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.BackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgWaiter;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.WaiterID,
            this.WaiterCode,
            this.WaiterName});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblWaiters";
            this.dgStyle.PreferredColumnWidth = 180;
            this.dgStyle.PreferredRowHeight = 40;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.RowHeaderWidth = 5;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.Green;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // WaiterID
            // 
            this.WaiterID.Format = "";
            this.WaiterID.FormatInfo = null;
            this.WaiterID.HeaderText = "ID";
            this.WaiterID.MappingName = "WaiterID";
            this.WaiterID.NullText = "";
            this.WaiterID.ReadOnly = true;
            this.WaiterID.Width = 0;
            // 
            // WaiterCode
            // 
            this.WaiterCode.Format = "";
            this.WaiterCode.FormatInfo = null;
            this.WaiterCode.HeaderText = "Code";
            this.WaiterCode.MappingName = "WaiterCode";
            this.WaiterCode.NullText = "";
            this.WaiterCode.ReadOnly = true;
            this.WaiterCode.Width = 200;
            // 
            // WaiterName
            // 
            this.WaiterName.Format = "";
            this.WaiterName.FormatInfo = null;
            this.WaiterName.HeaderText = "Name";
            this.WaiterName.MappingName = "WaiterName";
            this.WaiterName.NullText = "";
            this.WaiterName.ReadOnly = true;
            this.WaiterName.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "<- Press the icon to close the window or Enter search criteria.";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(67, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(298, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 7;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // keyboardSearchControl1
            // 
            this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
            this.keyboardSearchControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keyboardSearchControl1.Location = new System.Drawing.Point(0, 62);
            this.keyboardSearchControl1.Name = "keyboardSearchControl1";
            this.keyboardSearchControl1.Size = new System.Drawing.Size(1022, 134);
            this.keyboardSearchControl1.TabIndex = 1;
            this.keyboardSearchControl1.TabStop = false;
            this.keyboardSearchControl1.Tag = "";
            this.keyboardSearchControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardSearchControl1_UserKeyPressed);
            // 
            // WaiterSelectWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardSearchControl1);
            this.Controls.Add(this.dgWaiter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WaiterSelectWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.WaiterSelectWnd_Load);
            this.Resize += new System.EventHandler(this.WaiterSelectWnd_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WaiterSelectWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgWaiter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

		#region Private Methods

        private void CreateDetails(int iRow)
		{
			miWaiterID = Convert.ToInt64(dgWaiter[iRow, 0].ToString());
			mstWaiterName = dgWaiter[iRow, 2].ToString();
		}
		
		private void LoadOptions()
		{
			
		}

		private void LoadWaiterData()
		{
            Security.AccessUser clsAccessUser = new Security.AccessUser();

			try
			{
				string searchkey = "" + txtSearch.Text;
                System.Data.DataTable dt = clsAccessUser.Waiters(searchkey, 100, "Name", SortOption.Ascending);
                clsAccessUser.CommitAndDispose();
				
				dgWaiter.DataSource = dt;
				dgWaiter.Select(0);
				dgWaiter.CurrentRowIndex=0;
			}
			catch (IndexOutOfRangeException){}
			catch (Exception ex)
			{
                clsAccessUser.CommitAndDispose();
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}


		#endregion

		#region Windows Control Methods

		private void txtSearch_TextChanged(object sender, System.EventArgs e)
		{
			LoadWaiterData();
		}

		#endregion

		#region Windows Form Methods

		private void WaiterSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Data.DataTable dt;
			int index;

			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					if (dgWaiter.CurrentRowIndex < 0)
					{
						dialog = DialogResult.Cancel;
					} 
					else 
					{
                        CreateDetails(dgWaiter.CurrentRowIndex);
						dialog = DialogResult.OK;
					}
					this.Hide(); 
					break;
				
				case Keys.Up:
					dt = (System.Data.DataTable) dgWaiter.DataSource;
					if (dgWaiter.CurrentRowIndex > 0) 
					{
						index = dgWaiter.CurrentRowIndex;				
						dgWaiter.CurrentRowIndex -= 1;
						dgWaiter.Select(dgWaiter.CurrentRowIndex);
						dgWaiter.UnSelect(index);
					}
					break;

				case Keys.Down:
					dt = (System.Data.DataTable) dgWaiter.DataSource;
					if (dgWaiter.CurrentRowIndex < dt.Rows.Count-1) 
					{
						index = dgWaiter.CurrentRowIndex;				

						dgWaiter.CurrentRowIndex += 1;
						dgWaiter.Select(dgWaiter.CurrentRowIndex);
						dgWaiter.UnSelect(index);
					}
					break;
			}
		}

		private void WaiterSelectWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/WaiterSelect.jpg");	}
			catch{}

			LoadOptions();
			LoadWaiterData();
            txtSearch.Focus();
		}
		private void WaiterSelectWnd_Resize(object sender, System.EventArgs e)
		{
			dgStyle.GridColumnStyles["WaiterCode"].Width = 300;
			dgStyle.GridColumnStyles["WaiterName"].Width = this.Width-310;
		}

        private void dgWaiter_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGrid dgWaiter = (DataGrid)sender;
            System.Windows.Forms.DataGrid.HitTestInfo hti = dgWaiter.HitTest(e.X, e.Y);

            switch (hti.Type)
            {
                case System.Windows.Forms.DataGrid.HitTestType.Cell:
                    dgWaiter.Select(hti.Row);
                    CreateDetails(hti.Row);
                    dialog = DialogResult.OK;
                    this.Hide();
                    break;
            }
        }

		#endregion

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            txtSearch.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }
	}
}
