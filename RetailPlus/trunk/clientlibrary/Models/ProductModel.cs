using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceSoft.RetailPlus.Client
{
    public static class ProductModel
    {
        public static long ProductID { get; set; }
        public static long MatrixID { get; set; }
        public static long PackageID { get; set; }
        public static string BarCode { get; set; }

        public static void Clear()
        {
            ProductID = 0;
            MatrixID = 0;
            PackageID = 0;
            BarCode = string.Empty;
        }
    }
}
