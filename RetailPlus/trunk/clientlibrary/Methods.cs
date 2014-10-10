using System;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client
{
	/// <summary>
	/// Summary description for Methods.
	/// </summary>
	public class Methods
	{
		public Methods()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool AllNum(Int32 KeyAscii)
		{
			bool boRetValue = false;
			if ((KeyAscii < Convert.ToInt32(Keys.D0) || KeyAscii > Convert.ToInt32(Keys.D9)) & (KeyAscii != 8))
			{
				boRetValue = true;
			}
            return boRetValue;
		}
		public bool AllNumWithDecimal(Int32 KeyAscii)
		{
			bool boRetValue = false;
			if ((KeyAscii < Convert.ToInt32(Keys.D0) || KeyAscii > Convert.ToInt32(Keys.D9)) & (KeyAscii != 8) & (KeyAscii != 46))
			{
				boRetValue = true;
			}
			return boRetValue;
		}
		public bool AllNumWithSign(Int32 KeyAscii)
		{
			bool boRetValue = false;
			if ((KeyAscii < Convert.ToInt32(Keys.D0) || KeyAscii > Convert.ToInt32(Keys.D9)) & (KeyAscii != 8) & (KeyAscii != 45))
			{
				boRetValue = true;
			}
			return boRetValue;
		}
		public bool AllNumWithDecimalAndSign(Int32 KeyAscii)
		{
			bool boRetValue = false;
			if ((KeyAscii < Convert.ToInt32(Keys.D0) || KeyAscii > Convert.ToInt32(Keys.D9)) & (KeyAscii != 8) & (KeyAscii != 45) & (KeyAscii != 46))
			{
				boRetValue = true;
			}
			return boRetValue;
		}

        public static void InsertAuditLog(Data.TerminalDetails TerminalDetails, string Username, AccessTypes AccessType, string Remarks)
        {
            Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();
            clsAuditDetails.BranchID = TerminalDetails.BranchDetails.BranchID;
            clsAuditDetails.TerminalNo = TerminalDetails.TerminalNo;
            clsAuditDetails.ActivityDate = DateTime.Now;
            clsAuditDetails.User = Username;
            clsAuditDetails.IPAddress = System.Net.Dns.GetHostName();
            clsAuditDetails.Activity = AccessType.ToString("G");
            clsAuditDetails.Remarks = "FE:" + Remarks;

            AceSoft.RetailPlus.Client.AuditDB clsAuditConnection = new AceSoft.RetailPlus.Client.AuditDB();
            clsAuditConnection.GetConnection();

            Security.AuditTrail clsAuditTrail = new Security.AuditTrail(clsAuditConnection.Connection, clsAuditConnection.Transaction);
            clsAuditTrail.Insert(clsAuditDetails);

            clsAuditConnection.CommitAndDispose();
        }
	}
}
