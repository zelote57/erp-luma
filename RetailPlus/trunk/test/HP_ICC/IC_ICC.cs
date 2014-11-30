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
    /// Summary description for IC_ICC.
    /// </summary>
    public class IC_ICC
    {
        public static void Main(string[] args)
        {
            try
            {
                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
                clsLocalConnection.GetConnection();

                Console.Write("Connected to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
                Console.ReadLine();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT DISTINCT CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0)) AS ContactCode, ICDESC AS ContactName," +
                                    "CONCAT(ICADR1,ICADR2) AS Address, ICLINE AS BusinessName, ICPONE AS TelephoneNo, " +
                                    "CONCAT('gurkey:',GURKEY,'; status:',ICSTAT,'; ICBEGB:',ICBEGB,'; ICPURC',ICPURC) AS Remarks, " +
                                    "ICLINE CreditLimit, ICSALE Credit  " +
                             "FROM IC_ICC;";

                cmd.CommandText = SQL;

                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);

                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

                Int64 iCount = dt.Rows.Count;
                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    string ContactCode = dr["ContactCode"].ToString();
                    string ContactName = dr["ContactName"].ToString();
                    string Address = dr["Address"].ToString();
                    string BusinessName = dr["BusinessName"].ToString();
                    string TelephoneNo = dr["TelephoneNo"].ToString();
                    string Remarks = dr["Remarks"].ToString();
                    decimal Credit = decimal.Parse(dr["Credit"].ToString());
                    decimal CreditLimit = decimal.Parse(dr["CreditLimit"].ToString());

                    ContactDetails Member = clsContacts.Details(ContactCode);

                    if (Member.ContactID != 0)
                    {
                        SQL = "UPDATE tblContacts SET " +
                                    "Credit = @Credit, CreditLimit = @CreditLimit " +
                                    "WHERE ContactID = @ContactID;";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
                        cmd.Parameters.AddWithValue("@Credit", Credit);
                        cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);

                        cmd.CommandText = SQL;
                        clsLocalConnection.ExecuteNonQuery(cmd);

                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]ContactCode: " + ContactCode + " already in the database.");
                        iTheSame++;
                    }
                    else
                    {
                        SQL = "INSERT INTO tblContacts (ContactCode ,ContactName ,ContactGroupID ,ModeOfTerms ,Terms " +
                                    ",Address ,BusinessName ,TelephoneNo ,Remarks ,Debit ,Credit " +
                                    ",CreditLimit ,IsCreditAllowed ,DateCreated ,DepartmentID ,PositionID)VALUES(" +
                                    "@ContactCode ,@ContactName ,@ContactGroupID ,@ModeOfTerms ,@Terms " +
                                    ",@Address ,@BusinessName ,@TelephoneNo ,@Remarks ,@Debit ,@Credit  " +
                                    ",@CreditLimit ,@IsCreditAllowed ,NOW() ,@DepartmentID ,@PositionID)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
                        cmd.Parameters.AddWithValue("@ContactName", ContactName);
                        cmd.Parameters.AddWithValue("@ContactGroupID", 4); // 'IC Members'
                        cmd.Parameters.AddWithValue("@ModeOfTerms", 0);
                        cmd.Parameters.AddWithValue("@Terms", 0);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@BusinessName", BusinessName);
                        cmd.Parameters.AddWithValue("@TelephoneNo", TelephoneNo);
                        cmd.Parameters.AddWithValue("@Remarks", Remarks);
                        cmd.Parameters.AddWithValue("@Debit", 0);
                        cmd.Parameters.AddWithValue("@Credit", Credit);
                        cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
                        cmd.Parameters.AddWithValue("@IsCreditAllowed", 1);
                        cmd.Parameters.AddWithValue("@DepartmentID", 1);
                        cmd.Parameters.AddWithValue("@PositionID", 1);

                        cmd.CommandText = SQL;
                        clsLocalConnection.ExecuteNonQuery(cmd);

                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]ContactCode: " + ContactCode + " has been inserted.");
                        iNotTheSame++;
                    }

                    //insert into CreditBills;

                    iCtr++;
                }
                clsLocalConnection.CommitAndDispose();
                Console.WriteLine("done and committed");
                Console.WriteLine("Total: " + iCount.ToString());
                Console.WriteLine("Already in db: " + iTheSame.ToString());
                Console.WriteLine("Inserted: " + iNotTheSame.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
