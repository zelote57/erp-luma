//using System;
//using System.Security.Permissions;
//using MySql.Data.MySqlClient;

//namespace AceSoft.RetailPlus.Data
//{

//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]

//    public struct SysAccessTypesDetails
//    {
//        public Int32 TypeID;
//        public string TypeName;
//        public string Remarks;
//        public bool Enabled;
//        public Int32 SequenceNo;
//        public string Category;
//        public DateTime CreatedOn;
//        public DateTime LastModified;

//    }
//    public class SysAccessTypes : POSConnection
//    {
//        #region Constructors and Destructors

//        public SysAccessTypes()
//            : base(null, null)
//        {
//        }

//        public SysAccessTypes(MySqlConnection Connection, MySqlTransaction Transaction)
//            : base(Connection, Transaction)
//        {

//        }

//        #endregion

        
//    }
//}
