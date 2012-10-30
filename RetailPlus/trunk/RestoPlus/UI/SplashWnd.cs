using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for SplashWnd.
	/// </summary>
	public class SplashWnd : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Label lblStatus;
		public System.Windows.Forms.ProgressBar prgBar;
		private System.ComponentModel.IContainer components = null;

		public SplashWnd()
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
			this.lblStatus = new System.Windows.Forms.Label();
			this.prgBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.BackColor = System.Drawing.Color.White;
			this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblStatus.Location = new System.Drawing.Point(134, 74);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(184, 14);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "Checking connections to database...";
			// 
			// prgBar
			// 
			this.prgBar.Location = new System.Drawing.Point(134, 90);
			this.prgBar.Name = "prgBar";
			this.prgBar.Size = new System.Drawing.Size(256, 16);
			this.prgBar.TabIndex = 1;
			// 
			// SplashWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(425, 223);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.prgBar,
																		  this.lblStatus});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "SplashWnd";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SplashWnd_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public delegate void ChangeStatusDelegate(string MessageStatus);
		public void ChangeStatus(string MessageStatus) 
		{
			lblStatus.Text = MessageStatus;
		}

		private void SplashWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/splash.jpg");	}
			catch{}
		}
	}
}
