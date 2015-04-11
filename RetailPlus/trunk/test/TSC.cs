using System;
using System.Management;
using AceSoft.RetailPlus;
using MySql.Data.MySqlClient;
using System.Data;
using AceSoft.RetailPlus.Data;
using AceSoft.RetailPlus.Reports;
using AceSoft.RetailPlus.Security;

namespace Test
{
    /******************************************************************************
    **		Auth: Lemuel E. Aceron
    **		Date: May 21, 2006
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
    public class TSC
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length < 3)
                {
                    Console.WriteLine("Syntax: test.exe {prdcode} {barcode} {price}");
                }

                string prdcode = "BECH YCX0041WH3 WHITEPIECE (S)";
                string barcode = "4000003476631";
                string price = "349.750";

                if (args.Length >= 1) prdcode = args[0].ToString();
                if (args.Length >= 2) barcode = args[1].ToString();
                if (args.Length >= 3) price = args[2].ToString();

                //AceSoft.ThermalBarCodePrinter clsThermalBarCodePrinter3 = new AceSoft.ThermalBarCodePrinter();
                //clsThermalBarCodePrinter3.PrintTagPrice(prdcode, barcode, Convert.ToDecimal(price).ToString("#,##0.#0"));

                AceSoft.ThermalBarCodePrinter clsThermalBarCodePrinter3 = new AceSoft.ThermalBarCodePrinter();
                clsThermalBarCodePrinter3.PrintShelvesTag(prdcode, barcode, Convert.ToDecimal(price).ToString("#,##0.#0"));

                //AceSoft.ThermalBarCodePrinter clsThermalBarCodePrinter3 = new AceSoft.ThermalBarCodePrinter();
                //clsThermalBarCodePrinter3.PrintUserAccess("Lemuel E. Aceron", "lemuel|80");

                Console.WriteLine("printed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
