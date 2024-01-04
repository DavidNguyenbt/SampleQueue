using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleQueue
{
    class AppConfig
    {
        public static Color New { get; set; } = Color.LightBlue;
        public static Color Incomplete { get; set; } = Color.MediumSlateBlue;
        public static Color InDecoration { get; set; } = Color.Orange;
        public static Color InSewing { get; set; } = Color.Yellow;
        public static Color FinishOnTime { get; set; } = Color.Green;
        public static Color FinishDelay { get; set; } = Color.Red;
        public static Color InQueue { get; set; } = Color.LightBlue;
        public static string User { get; set; } = "";
        public static Color CFTPassed { get; set; } = Color.Lime;
        public static string FilterColumn1 { get; set; } = "Style";
        public static string FilterColumn2 { get; set; } = "Style";
        public static bool Notification { get; set; } = true;
        public static bool ShowNews { get; set; } = true;
        public static bool CapacityTip { get; set; } = true;
        public static bool UrgentTip { get; set; } = true;
        public static bool CommentTip { get; set; } = true;
        public static int X { get; set; } = 10;
        public static int Y { get; set; } = 50;
    }
}
