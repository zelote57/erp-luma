using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace AceSoft.KeyBoardHook
{
	/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: August 4, 2005
	*******************************************************************************
	**		Change History
	*******************************************************************************
	**		Date:		Author:				Description:
	**		--------		--------				-------------------------------------------
	**    
	*******************************************************************************/

	/// <summary>
	/// This class allows you to tap keyboard and mouse and / or to detect their activity even when an 
	/// application runes in background or does not have any user interface at all. This class raises 
	/// common .NET events with KeyEventArgs so you can easily retrive any information you need.
	/// </summary>
	public class KeyBoardHook : object
	{
		public KeyBoardHook()
		{
			Start();
		}

		~KeyBoardHook() 
		{ 
			Stop();
		} 

		public event KeyEventHandler KeyDown;
		public event KeyPressEventHandler KeyPress;
		public event KeyEventHandler KeyUp;

		public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        static IntPtr hKeyboardHook = IntPtr.Zero; //Declare keyboard hook handle as int.

		//values from Winuser.h in Microsoft SDK.
		public const int WH_KEYBOARD_LL = 13;	//keyboard hook constant	

		HookProc KeyboardHookProcedure; //Declare KeyboardHookProcedure as HookProc type.

		#region DLL Imports

		//Import for SetWindowsHookEx function.
		//Use this function to install a hook.
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

		//Import for UnhookWindowsHookEx.
		//Call this function to uninstall the hook.
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        //public static extern bool UnhookWindowsHookEx(int idHook);

		//Import for CallNextHookEx.
		//Use this function to pass the hook information to next hook procedure in chain.
		[DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
        public static extern int CallNextHookEx(IntPtr hInstance, int nCode, Int32 wParam, IntPtr lParam);
        //public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam); 

		//The ToAscii function translates the specified virtual-key code and keyboard state to the corresponding character or characters. The function translates the code using the input language and physical keyboard layout identified by the keyboard layout handle.
		[DllImport("user32")] 
		public static extern int ToAscii(int uVirtKey, //[in] Specifies the virtual-key code to be translated. 
			int uScanCode, // [in] Specifies the hardware scan code of the key to be translated. The high-order bit of this value is set if the key is up (not pressed). 
			byte[] lpbKeyState, // [in] Pointer to a 256-byte array that contains the current keyboard state. Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down (pressed). The low bit, if set, indicates that the key is toggled on. In this function, only the toggle bit of the CAPS LOCK key is relevant. The toggle state of the NUM LOCK and SCROLL LOCK keys is ignored.
			byte[] lpwTransKey, // [out] Pointer to the buffer that receives the translated character or characters. 
			int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise. 

		//The GetKeyboardState function copies the status of the 256 virtual keys to the specified buffer. 
		[DllImport("user32")] 
		public static extern int GetKeyboardState(byte[] pbKeyState);

		#endregion
		
		public void Start()
		{
			// install Keyboard hook 
            if (hKeyboardHook == IntPtr.Zero)
			{
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL,
                    KeyboardHookProcedure,
                    GetCurrentModule().BaseAddress,
                    0);
                //hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL,
                //    KeyboardHookProcedure,
                //    Marshal.GetHINSTANCE(
                //    Assembly.GetExecutingAssembly().GetModules()[0]),
                //    0);

				//If SetWindowsHookEx fails.
                if (hKeyboardHook == IntPtr.Zero)	
				{
					Stop();
					throw new Exception("SetWindowsHookEx is failed.");
				}
			}
		}

		public void Stop()
		{
			bool retKeyboard = true;

            if (hKeyboardHook != IntPtr.Zero)
			{
				retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = IntPtr.Zero;
			} 
			
			//If UnhookWindowsHookEx fails.
			if (!(retKeyboard)) throw new Exception("UnhookWindowsHookEx failed.");
		}

		
		private const int WM_KEYDOWN 		= 0x100;
		private const int WM_KEYUP 			= 0x101;
		private const int WM_SYSKEYDOWN 	= 0x104;
		private const int WM_SYSKEYUP 		= 0x105;

		private const int VK_TAB = 0x9;
		private const int VK_CONTROL = 0x11;
		private const int VK_ESCAPE = 0x1B;
		private const int VK_DELETE = 0x2E;
		private const int VK_LWIN = 0x5B;
		private const int VK_RWIN = 0x5C;

		private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
		{
			// it was ok and someone listens to events
			if ((nCode >= 0) && (KeyDown!=null || KeyUp!=null || KeyPress !=null))
			{
				KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

				// raise KeyUp
				if ( KeyDown!=null && MyKeyboardHookStruct.vkCode ==VK_ESCAPE && Control.ModifierKeys == Keys.Control)
				{
					if (BlockControlEscape())
						return 1;
				}
				else if ( KeyDown!=null && MyKeyboardHookStruct.vkCode ==VK_TAB && Control.ModifierKeys == Keys.Alt)
				{
					if (BlockAltTab())
						return 1;
				}
				else if ( KeyDown!=null && MyKeyboardHookStruct.vkCode ==VK_ESCAPE && Control.ModifierKeys == Keys.Alt)
				{
					if (BlockAltEsc())
						return 1;
				}
				else if ( KeyDown!=null && (MyKeyboardHookStruct.vkCode == VK_LWIN || MyKeyboardHookStruct.vkCode == VK_RWIN))
				{
					if (BlockControlWindow())
						return 1;
				}
				else if ( KeyDown!=null && (MyKeyboardHookStruct.vkCode == VK_LWIN || MyKeyboardHookStruct.vkCode == VK_RWIN))
				{
					if (BlockControlWindow())
						return 1;
				}
				else if( KeyDown!=null && ( wParam ==WM_KEYUP || wParam==WM_SYSKEYUP ))
				{
					Keys keyData=(Keys)MyKeyboardHookStruct.vkCode;
					KeyEventArgs e = new KeyEventArgs(keyData);
					KeyDown(this, e);
				}
			}
			return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam); 
		}

		private bool BlockControlEscape()
		{
			return true;
		}
		private bool BlockAltTab()
		{
			return true;
		}
		private bool BlockAltEsc()
		{
			return true;
		}
		private bool BlockControlWindow()
		{
			return true;
		}

        private static ProcessModule GetCurrentModule()         
        {             // need instance handle to module to create a system-wide hook             
            Module[] list = System.Reflection.Assembly.GetExecutingAssembly().GetModules();             
            System.Diagnostics.Debug.Assert(list != null && list.Length > 0);
            var currentProcess = Process.GetCurrentProcess(); 
            var modules = currentProcess.Modules; 
            ProcessModule mod = null;
            foreach (ProcessModule m in modules)                             
                //for .net 2 we will find module here                 
                if (m.ModuleName == list[0].Name)                 
                {                     
                    mod = m;                     
                    break;                 
                }                      
            //for .net 4 take current module             
            if (mod == null)                 
                mod = Process.GetCurrentProcess().MainModule; 

            return mod;
        } 
    }
}
