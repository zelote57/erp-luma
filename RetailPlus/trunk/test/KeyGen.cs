//using System;
//using System.Management;
//using AceSoft.RetailPlus;

//namespace Test
//{
//    /******************************************************************************
//    **		Auth: Lemuel E. Aceron
//    **		Date: May 21, 2006
//    *******************************************************************************
//    **		Change History
//    *******************************************************************************
//    **		Date:		Author:				Description:
//    **		--------		--------				-------------------------------------------
//    **    
//    *******************************************************************************/

//    /// <summary>
//    /// Summary description for KeyGen.
//    /// </summary>
//    public class KeyGen
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                string SerialNumber = args[0].ToString();

//                string plainText = CompanyDetails.CompanyCode + SerialNumber;    // original plaintext
//                string cipherText = "";                 // encrypted text
//                string passPhrase = CompanyDetails.TIN; // can be any string
//                string initVector = "%@skmelaT3rsh1t!"; // must be 16 bytes

//                // Before encrypting data, we will append plain text to a random
//                // salt value, which will be between 4 and 8 bytes long (implicitly
//                // used defaults).
//                AceSoft.Cryptor clsCryptor = new AceSoft.Cryptor(passPhrase, initVector);

//                Console.WriteLine(String.Format("Plaintext   : {0}\n", plainText));

//                // Encrypt the same plain text data 10 time (using the same key,
//                // initialization vector, etc) and see the resulting cipher text;
//                // encrypted values will be different.
//                for (int i = 0; i < 10; i++)
//                {
//                    cipherText = clsCryptor.Encrypt(plainText);
//                    Console.WriteLine(
//                        String.Format("Encrypted #{0}: {1}", i, cipherText));
//                    plainText = clsCryptor.Decrypt(cipherText);
//                }

//                // Make sure we got decryption working correctly.
//                Console.WriteLine(String.Format("\nDecrypted   :{0}", plainText));
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        //public static void Main(string[] args)
//        //{
//        //    int[] ArrayX = { 1, 2, 3, 4, 5, 6, 7, 8 };

//        //    //using loop to get index
//        //    int IndexOf5 = -1;
//        //    foreach(int iCtr in ArrayX)
//        //    {
//        //        if (iCtr == 5) IndexOf5 = Array.IndexOf(ArrayX, 5);
//        //    }

//        //    //best way is to simply get the index w/out looping
//        //    IndexOf5 = Array.IndexOf(ArrayX, 5);
//        //    Console.WriteLine(IndexOf5.ToString());
//        //}
//    }
//}
