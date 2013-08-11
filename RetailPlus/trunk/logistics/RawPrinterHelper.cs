using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Permissions;

namespace AceSoft
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	/// <summary>
	/// From microsoft
	/// </summary>
	public class RawPrinterHelper
	{
        //http://content.epson.de/fileadmin/content/files/RSD/downloads/escpos.pdf

        public static string escESC = Convert.ToChar(27).ToString();  // ESC char
        public static string escNewLine = Convert.ToChar(10).ToString();  // New line (LF line feed)

        public static string escFontSmall = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(1).ToString(); // Font B - Small Font
        public static string escFontNormal = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(0).ToString(); // Font A  -  Normal Font
        public static string escFontEmpOn = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(8).ToString(); // Font Emphasized On
        public static string escFontEmpOff = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(0).ToString(); // Font Emphasized Off
        public static string escFontHeightX2On = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(16).ToString(); // Font Size Height x2 On
        public static string escFontHeightX2Off = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(0).ToString();  // Font Size Height x2 Off
        public static string escFontWidthX2On = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(32).ToString(); // Font Size Width x2 On
        public static string escFontWidthX2Off = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(0).ToString();  // Font Size Width x2 Off
        public static string escFontUnderlineOn = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(128).ToString(); // Font Underline x2 On
        public static string escFontUnderlineOff = escESC + Convert.ToChar(33).ToString() + Convert.ToChar(0).ToString();  // Font Underline x2 Off

        public static string escUnderlineOn = escESC + Convert.ToChar(45).ToString() + Convert.ToChar(1).ToString();  // Unerline On
        public static string escUnderlineOnx2 = escESC + Convert.ToChar(45).ToString() + Convert.ToChar(2).ToString();  // Unerline On 2-dots
        public static string escUnderlineOff = escESC + Convert.ToChar(45).ToString() + Convert.ToChar(0).ToString();  // Unerline Off

        public static string escReset = escESC + Convert.ToChar(64).ToString(); // Reset Printer or Initialize printer
        public static string escEmphasizedOn = escESC + Convert.ToChar(69).ToString() + Convert.ToChar(1).ToString(); // 1, emphasized mode is turned on
        public static string escEmphasizedOff = escESC + Convert.ToChar(69).ToString() + Convert.ToChar(0).ToString(); // 0, emphasized mode is turned off

        public static string escDoubleStrikeOn = escESC + Convert.ToChar(71).ToString() + Convert.ToChar(1).ToString(); // 1, double-strike is turned on
        public static string escDoubleStrikeOff = escESC + Convert.ToChar(71).ToString() + Convert.ToChar(0).ToString(); // 0, double-strike is turned off

        public static string escFontA = escESC + Convert.ToChar(77).ToString() + Convert.ToChar(0).ToString(); // Font A
        public static string escFontB = escESC + Convert.ToChar(77).ToString() + Convert.ToChar(1).ToString(); // Font B
        public static string escFontC = escESC + Convert.ToChar(77).ToString() + Convert.ToChar(2).ToString(); // Font C

        public static string escAlignDef = escESC + Convert.ToChar(97).ToString() + Convert.ToChar(0).ToString(); // Align Text to the Left
        public static string escAlignLeft = escESC + Convert.ToChar(97).ToString() + Convert.ToChar(0).ToString(); // Align Text to the Left
        public static string escAlignCenter = escESC + Convert.ToChar(97).ToString() + Convert.ToChar(1).ToString(); // Align Text to the Center
        public static string escAlignRight = escESC + Convert.ToChar(97).ToString() + Convert.ToChar(2).ToString(); // Align Text to the Right

        public static string escPrintAndFeedLines(int nlines) 
        {
            return escESC + Convert.ToChar(100).ToString() + Convert.ToChar(100).ToString() + Convert.ToChar(nlines).ToString();  // Print and feedlines
        }

        public static string escPrintAndFeedLinesReverse(int nlines)
        {
            return escESC + Convert.ToChar(100).ToString() + Convert.ToChar(101).ToString() + Convert.ToChar(nlines).ToString();  // Print and feedlines in reverse
        }

        public static string escColor1 = escESC + Convert.ToChar(114).ToString() + Convert.ToChar(1).ToString(); // color 1 is selected 
        public static string escColorDef = escESC + Convert.ToChar(114).ToString() + Convert.ToChar(0).ToString(); // color 0 is selected -default

        public static string escNegativeOn = Convert.ToChar(29).ToString() + Convert.ToChar(66).ToString() + Convert.ToChar(1).ToString();  // White On Black On'
        public static string escNegativeOff = Convert.ToChar(29).ToString() + Convert.ToChar(66).ToString() + Convert.ToChar(0).ToString();  // White On Black Off

        public static string escCutFull = Convert.ToChar(29).ToString() + Convert.ToChar(86).ToString() + Convert.ToChar(48).ToString(); // Full cut
        public static string escCutPartial = Convert.ToChar(29).ToString() + Convert.ToChar(86).ToString() + Convert.ToChar(49).ToString(); // Partial cut
        public static string escCutAndFeed = Convert.ToChar(29).ToString() + Convert.ToChar(86).ToString() + Convert.ToChar(65).ToString(); // Full Cut and feed
        public static string escCutPartialAndFeed = Convert.ToChar(29).ToString() + Convert.ToChar(86).ToString() + Convert.ToChar(66).ToString(); // Partial Cut and feed

        public static string escBarcodeHeight(int ndots = 162)
        {
            return Convert.ToChar(29).ToString() + Convert.ToChar(104).ToString() + Convert.ToChar(ndots).ToString(); // set the height of barcode; 162 default per documentation; max=255
        }

        public static string escBarcodeEan13 = Convert.ToChar(29).ToString() + Convert.ToChar(107).ToString() + Convert.ToChar(2).ToString(); // For EAN13
        public static string escBarcodeCODE39 = Convert.ToChar(29).ToString() + Convert.ToChar(107).ToString() + Convert.ToChar(4).ToString(); // For CODE39

        public static string escBoldHOn = escFontHeightX2On + escFontEmpOn;
        public static string escBoldHOff = escFontHeightX2Off + escFontEmpOff;
        public static string escBoldHWOn = escFontWidthX2On + escFontHeightX2On + escFontEmpOn;
        public static string escBoldHWOff = escFontWidthX2Off + escFontHeightX2Off + escFontEmpOff;
        

		public RawPrinterHelper()
		{

		}
		// Structure and API declarions:
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
			public class DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)] public string pDocName;
			[MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
			[MarshalAs(UnmanagedType.LPStr)] public string pDataType;
		}
		[DllImport("winspool.Drv", EntryPoint="OpenPrinterA", SetLastError=true, CharSet=CharSet.Ansi, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, int pd);
        //public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, long pd);

        //[2/23] LEA comment
        //A Long value in C++ has the same capacity as .NET's current Int32, int (C#), and Integer (VB.NET) values, which is -2,147,483,648 through 2,147,483,647. 
        //A long value in .NET is -9,223,372,036,854,775,808 through 9,223,372,036,854,775,807 (9.2...E+18 ?), which is a considerably larger value. 
        //(Edited: Added a reference to what used to be declared as a long value)
        //The original kb article on how to print directly to a printer declared the OpenPrinter function as 
        //Public Shared Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Long) As Boolean
        //By changing this to 
        //Public Shared Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Integer) As Boolean, I no longer had a stack issue.



		[DllImport("winspool.Drv", EntryPoint="ClosePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="StartDocPrinterA", SetLastError=true, CharSet=CharSet.Ansi, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool StartDocPrinter( IntPtr hPrinter, Int32 level,  [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

		[DllImport("winspool.Drv", EntryPoint="EndDocPrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="StartPagePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="EndPagePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="WritePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten );

		// SendBytesToPrinter()
		// When the function is given a printer name and an unmanaged array
		// of bytes, the function sends those bytes to the print queue.
		// Returns true on success, false on failure.
		public static bool SendBytesToPrinter( string szPrinterName, IntPtr pBytes, Int32 dwCount, string DocumentName)
		{
			Int32    dwError = 0, dwWritten = 0;
			IntPtr    hPrinter = new IntPtr(0);
			DOCINFOA    di = new DOCINFOA();
			bool    bSuccess = false; // Assume failure unless you specifically succeed.

			di.pDocName = DocumentName;
			di.pDataType = "RAW";

			// Open the printer.
			if( OpenPrinter( szPrinterName, out hPrinter, 0 ) )
			{
				// Start a document.
				if( StartDocPrinter(hPrinter, 1, di) )
				{
					// Start a page.
					if( StartPagePrinter(hPrinter) )
					{
						// Write your bytes.
						bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
						EndPagePrinter(hPrinter);
					}
					EndDocPrinter(hPrinter);
				}
				ClosePrinter(hPrinter);
			}
			// If you did not succeed, GetLastError may give more information
			// about why not.
			if( bSuccess == false )
			{
				dwError = Marshal.GetLastWin32Error();
			}
			return bSuccess;
		}

		public static bool SendFileToPrinter( string szPrinterName, string szFileName, string DocumentName )
		{
			// Open the file.
			FileStream fs = new FileStream(szFileName, FileMode.Open);
			// Create a BinaryReader on the file.
			BinaryReader br = new BinaryReader(fs);
			// Dim an array of bytes big enough to hold the file's contents.
			Byte []bytes = new Byte[fs.Length];
			bool bSuccess = false;
			// Your unmanaged pointer.
			IntPtr pUnmanagedBytes = new IntPtr(0);
			int nLength;

			nLength = Convert.ToInt32(fs.Length);
			// Read the contents of the file into the array.
			bytes = br.ReadBytes( nLength );
			// Allocate some unmanaged memory for those bytes.
			pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
			// Copy the managed byte array into the unmanaged array.
			Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
			// Send the unmanaged bytes to the printer.
			bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength, DocumentName);
			// Free the unmanaged memory that you allocated earlier.
			Marshal.FreeCoTaskMem(pUnmanagedBytes);
			return bSuccess;
		}
		public static bool SendStringToPrinter( string szPrinterName, string szString , string DocumentName)
		{
			// Send a form feed character to the printer \f

			IntPtr pBytes = new IntPtr(0);
			Int32 dwCount;
			// How many characters are in the string?
			dwCount = szString.Length;
			// Assume that the printer is expecting ANSI text, and then convert
			// the string to ANSI text.
			pBytes = Marshal.StringToCoTaskMemAnsi(szString);
			// Send the converted ANSI string to the printer.
			SendBytesToPrinter(szPrinterName, pBytes, dwCount, DocumentName);
			Marshal.FreeCoTaskMem(pBytes);
			return true;
		}


	}
}
