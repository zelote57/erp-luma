using System;
using System.Windows.Forms;

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
	}
}
