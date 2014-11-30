using System;
using System.Runtime.InteropServices;

namespace AceSoft.RetailPlus.Client
{
    /******************************************************************************
    **		Auth: Lemuel E. Aceron
    **		Date: August 17, 2005
    *******************************************************************************
    **		Change History
    *******************************************************************************
    **		Date:		Author:				Description:
    **		--------		--------				-------------------------------------------
    **    
    *******************************************************************************/

    /// <summary>
    /// Hide or show the TaskBar
    /// </summary>
    public class TaskBarWnd
    {
        [DllImport("user32.dll")]
        static extern int FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool IsWindowVisible(int hwnd);

        [DllImport("user32.dll")]
        static extern int ShowWindow(int hwnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        #region Constructors and Destructors

        public TaskBarWnd()
        {

        }

        #endregion

        #region Public Modifiers

        public void Show()
        {
            int Shell_TrayWnd = FindWindow("Shell_TrayWnd", null);

            if (!IsWindowVisible(Shell_TrayWnd))
                ShowWindow(Shell_TrayWnd, SW_SHOW);

            int hwndOrb = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
            ShowWindow(hwndOrb, SW_SHOW);
        }

        public void Hide()
        {
            int Shell_TrayWnd = FindWindow("Shell_TrayWnd", null);

            if (IsWindowVisible(Shell_TrayWnd))
                ShowWindow(Shell_TrayWnd, SW_HIDE);

            int hwndOrb = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
            ShowWindow(hwndOrb, SW_HIDE);
        }

        #endregion

    }
}
