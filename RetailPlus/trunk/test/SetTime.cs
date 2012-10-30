using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class SetTime
    {
        [System.Runtime.InteropServices.DllImport("kernel32 ", SetLastError = true)]
        private static extern bool GetSystemTime(out SYSTEMTIME systemTime);
        [System.Runtime.InteropServices.DllImport("kernel32 ", SetLastError = true)]
        private static extern bool SetSystemTime(ref SYSTEMTIME systemTime);
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        //static void Main()
        //{
        //    SYSTEMTIME st;
        //    if(GetSystemTime(out st))
        //    {
        //        st.wHour = 10; //Beware SYSTEMTIME is in UTC time format!!!!!
        //        st.wYear = 2009;
        //        if(SetSystemTime(ref st))
        //            Console.WriteLine("success");
        //        else
        //            Console.WriteLine(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
        //    }
        //    else
        //     Console.WriteLine("GetSystemTime failed: {0}", System.Runtime.InteropServices.Marshal.GetLastWin32Error());
        //}
    }
}
