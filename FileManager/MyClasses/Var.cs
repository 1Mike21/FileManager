using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.MyClasses
{
    public partial class Var
    {
        public delegate void Command();
        #region Переменные
        public static string command { get; set; } = "";
        public static string help { get; set; } = "";
        public static string Browse { get; set; } = "";
        public static string location { get; set; } = "";
        public static string InfoFoulder { get; set; } = "";
        public static string InfoFiles { get; set; } = "";
        public static string Create { get; set; } = "";
        public static string nameFile { get; set; }
        public static string typeFile { get; set; }
        public static string Read { get; set; } = "";
        public static string Record { get; set; } = "";
        public static string Encryption { get; set; }
        public static string text { get; set; }
        public static string Exit { get; set; } = "";
        public static string answer { get; set; }
        #endregion
    }
}
