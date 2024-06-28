using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SampleQueue
{
    class Temp
    {
        public static string ch = "Data Source=192.168.1.245;Initial Catalog=DtradeProduction;Persist Security Info=True;User ID=prog4;Password=DeS";
        public static string ch2 = "Data Source=192.168.1.245;Initial Catalog=SecurityReport;Persist Security Info=True;User ID=prog4;Password=DeS";
        public static string erp = "Data Source=192.168.70.115;Initial Catalog=AXDB;Persist Security Info=True;User ID=iplan;Password=Ipla246!";
        public static string path = @"\\192.168.1.248\Shared File\SQData";//VN
        //public static string ch = "Data Source=192.168.50.253;Initial Catalog=DtradeProduction;Persist Security Info=True;User ID=sa;Password=Sql4116!";
        //public static string ch2 = "Data Source=192.168.50.253;Initial Catalog=SecurityReport;Persist Security Info=True;User ID=sa;Password=Sql4116!";
        //public static string erp = "Data Source=192.168.43.20;Initial Catalog=AXDB;Persist Security Info=True;User ID=iplan;Password=mQ$IvJ9YATCJ";
        //public static string path = @"\\192.168.50.253\Software From A1A\Shared File\SQData";//THAI
        public static string User = "KHANH-MERA", Dept = "", DeptDesc = "MERA", Profile = "", version = "V1.2.0";
        public static List<string> BoPhan = new List<string> { "All", "MER - Merchandiser", "SMP - Sample Room", "PTT - Pattern", "MK - Marker", "CUT - Cutting", "EMB - Decoration", "SEW - Sewing", "QC - QC", "CFT - CFT" };
        public static string url = "http://192.168.1.100/", myconfig = "";
        public static Color New = Color.LightBlue;
        public static Color Incomplete = Color.MediumSlateBlue;
        public static Color InDecoration = Color.Orange;
        public static Color InSewing = Color.Yellow;
        public static Color InQueue = Color.LightGray;
        public static Color FinishOnTime = Color.Green;
        public static Color FinishDelay = Color.Red;
        public static Color CFTPassed = Color.Lime;
        public static List<PrintingItem> Printers = new List<PrintingItem>();
        public static string CapacityTip = "";
        public static string UrgentTip = "      Sample Urgent is a way of Merchandiser that book samples as priority to keep up with the shipment date of samples that Merchandiser cannot book samples in the usual way.\r\n" +
                                        "       With this way, the sample that Mer booked must be accepted by the Sample Room Manager before being placed in the queue.\r\n" +
                                        @"       For more details, please click the ""See More"" below";
        public static string CommentTip = "      Comment Management System (CMS) is a system for managing internal comments or comments from customers about samples.\r\n" +
                                          "      Each comment will be taken and followed up by relevant departments when required to resolve sample issues.\r\n" +
                                          "      Through the system we will track whether the comment is resolved or not or who has tracked these comments.\r\n" +
                                          "      The system also links with DDT system to store related documents about the sample (this means that the sample documents that you update on the system will automatically be pushed through the DDT system for storage).\r\n" +
                                        @"       For more details, please click the ""See More"" below";

        public static System.Globalization.CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        public static void SaveConfig()
        {
            StreamWriter wr = new StreamWriter(myconfig);

            string config = "";
            config += "New:" + AppConfig.New.Name + "\n";
            config += "Incomplete:" + AppConfig.Incomplete.Name + "\n";
            config += "InDecoration:" + AppConfig.InDecoration.Name + "\n";
            config += "InSewing:" + AppConfig.InSewing.Name + "\n";
            config += "FinishOnTime:" + AppConfig.FinishOnTime.Name + "\n";
            config += "FinishDelay:" + AppConfig.FinishDelay.Name + "\n";
            config += "InQueue:" + AppConfig.InQueue.Name + "\n";
            config += "User:" + AppConfig.User + "\n";
            config += "CFTPassed:" + AppConfig.CFTPassed.Name + "\n";
            config += "FilterColumn1:" + AppConfig.FilterColumn1 + "\n";
            config += "FilterColumn2:" + AppConfig.FilterColumn2 + "\n";
            config += "Notification:" + AppConfig.Notification + "\n";
            config += "ShowNews:" + AppConfig.ShowNews + "\n";
            config += "CapacityTip:" + AppConfig.CapacityTip + "\n";
            config += "UrgentTip:" + AppConfig.UrgentTip + "\n";
            config += "CommentTip:" + AppConfig.CommentTip + "\n";
            config += "PrinterX:" + AppConfig.X + "\n";
            config += "PrinterY:" + AppConfig.Y;

            if (config != "")
            {
                wr.Write(config);
                wr.Close();
            }
        }

        public static bool IsValidPassword(string password)
        {
            // Regex kiểm tra mật khẩu
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
    class PrintingItem
    {
        public string SampleID { get; set; }
        public string Style { get; set; }
        public string Season { get; set; }
        public string Smptype { get; set; }
        public string Qty { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}
