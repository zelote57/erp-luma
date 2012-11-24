using System;
//using System.Management;
using System.Diagnostics;

namespace AceSoft.RetailPlus.HD
{
	/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: May 29, 2006
	*******************************************************************************
	**		Change History
	*******************************************************************************
	**		Date:		Author:				Description:
	**		--------		--------				-------------------------------------------
	**    
	*******************************************************************************/

	/// <summary>
	/// Summary description for KeyGen.
	/// </summary>
	public class SerialNo
	{
		public static void Main(string[] args) 
		{
			try
			{
                string SerialNumber = Key.GetHDSerialNo();

				Console.WriteLine("SerialNumber : {0}", SerialNumber);
				Console.WriteLine("Please send this serial to your distributor. Press any key to continue.");
				Console.ReadLine();
			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
