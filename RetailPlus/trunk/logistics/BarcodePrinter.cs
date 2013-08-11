using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceSoft
{
    public enum printerModel
    {
        Epson,
        Tally,
        EpsonTest
    }

    public enum barcodeType
    {
        Code2of5,
        Code11,
        Code39,
        Codabar,
        EAN8,
        EAN13,
        UPCA,
        UPCE,
        IBM,
        Code128,
        EAN128,
        POSTNET
    }

    public class BarcodePrinter
    {
        public string GenerateBarCode(string barcode, printerModel model, barcodeType codeType)
        {
            string barcodeString = "";

            switch (model)
            {
                case printerModel.EpsonTest:

                    barcodeString = RawPrinterHelper.escReset + RawPrinterHelper.escEmphasizedOn + RawPrinterHelper.escBarcodeEan13 + barcode + RawPrinterHelper.escEmphasizedOff + RawPrinterHelper.escReset;

                    break;
                case printerModel.Epson:

                    barcodeString = RawPrinterHelper.escReset + RawPrinterHelper.escBarcodeEan13 + barcode + RawPrinterHelper.escReset;

                    //barcodeString = "\x1B@\x0a" +                                       //Initialize printer
                    //                "\x1B\x28\x42" +                                    //Start barcode header
                    //                Convert.ToChar(6 + barcode.Length).ToString() +     //Barcode length
                    //                "\x00" +
                    //                "{barCodeType}" +                                   //Barcode type
                    //                "\x02" +                                            //Module width
                    //                "\x00" +                                            //Space adjustment
                    //                "\x24" +                                            //Bar lenght(Height)
                    //                "\x00" +                                            //Add check digit 1=ON 0=Off
                    //                "\x03" +                                            //Human readable charachters 3=ON 2=OFF
                    //                "{barCodeData}\x0a" +                               //Data
                    //                "\x1B@\x0a";                                        //Reset Printer

                    //switch (codeType)
                    //{
                    //    case barcodeType.EAN13:
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x00");
                    //        break;

                    //    case barcodeType.EAN8:
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x01");
                    //        break;

                    //    case barcodeType.Code2of5:
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x02");
                    //        break;

                    //    case barcodeType.UPCA:
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x03");
                    //        break;

                    //    case barcodeType.UPCE:
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x04");
                    //        break;

                    //    case barcodeType.Code39:
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x05");
                    //        break;

                    //    case barcodeType.Code128:
                    //        barcode = "A" + barcode;
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x06");
                    //        break;

                    //    case barcodeType.POSTNET:
                    //        barcodeString = barcodeString.Replace("{barCodeType}", "\x07");
                    //        break;
                    //}

                    //barcodeString = barcodeString.Replace("{barCodeData}", barcode);

                    break;

                case printerModel.Tally:

                    barcodeString = "\x1B[?11~" +                       //Barcode interpreter ON
                                    "\x1A" +                            //Start barcode
                                    "\x22" +                            //Print feature
                                    "{barCodeType}" +                   //Barcode type
                                    "3" +                               //Height (n/6 inch)
                                    ";" +
                                    "000" +                             //xyz params; x = Width of the narrow bar; y = Width of the narrow space; z = Ratio of wide to narrow 
                                    "\x19" +                            //End barcode header
                                    "\x14" +
                                    "{startChar}" +                     //Start barcode
                                    "{barCodeData}" +                   //Data
                                    "{stopChar}" +                      //Stop barcode
                                    "\x14";
                    //"\x1B[?10~";                      //Barcode interpreter OFF (seems to be for all barcodes untill printer is turned off or interpreter is switched on)

                    switch (codeType)
                    {
                        case barcodeType.Code2of5:
                            barcodeString = barcodeString.Replace("{startChar}", ":").Replace("{stopChar}", ":");
                            barcodeString = barcodeString.Replace("{barCodeType}", "A");
                            break;

                        case barcodeType.Code11:
                            barcodeString = barcodeString.Replace("{startChar}", ":").Replace("{stopChar}", ":");
                            barcodeString = barcodeString.Replace("{barCodeType}", "D");
                            break;

                        case barcodeType.Code39:
                            barcodeString = barcodeString.Replace("{startChar}", "*").Replace("{stopChar}", "*");
                            barcodeString = barcodeString.Replace("{barCodeType}", "F");
                            break;

                        case barcodeType.Codabar:
                            barcodeString = barcodeString.Replace("{startChar}", "*").Replace("{stopChar}", "*");
                            barcodeString = barcodeString.Replace("{barCodeType}", "G");
                            break;

                        case barcodeType.EAN8:
                            barcodeString = barcodeString.Replace("{startChar}", ":").Replace("{stopChar}", ":");
                            barcodeString = barcodeString.Replace("{barCodeType}", "H");
                            break;

                        case barcodeType.EAN13:
                            barcodeString = barcodeString.Replace("{startChar}", ":").Replace("{stopChar}", ":");
                            barcodeString = barcodeString.Replace("{barCodeType}", "K");
                            break;

                        case barcodeType.UPCA:
                            barcodeString = barcodeString.Replace("{startChar}", ":").Replace("{stopChar}", ":");
                            barcodeString = barcodeString.Replace("{barCodeType}", "N");
                            break;

                        case barcodeType.UPCE:
                            barcodeString = barcodeString.Replace("{startChar}", ":").Replace("{stopChar}", ":");
                            barcodeString = barcodeString.Replace("{barCodeType}", "P");
                            break;

                        case barcodeType.IBM:
                            barcodeString = barcodeString.Replace("{startChar}", "F").Replace("{stopChar}", "D");
                            barcodeString = barcodeString.Replace("{barCodeType}", "R");
                            break;

                        case barcodeType.Code128:
                            barcodeString = barcodeString.Replace("{startChar}", "").Replace("{stopChar}", "");
                            barcodeString = barcodeString.Replace("{barCodeType}", "S");
                            break;

                        case barcodeType.EAN128:
                            barcodeString = barcodeString.Replace("{startChar}", "").Replace("{stopChar}", "");
                            barcodeString = barcodeString.Replace("{barCodeType}", "T");
                            break;
                    }

                    barcodeString = barcodeString.Replace("{barCodeData}", barcode);

                    break;
            }

            return barcodeString;
        }
    }
}
