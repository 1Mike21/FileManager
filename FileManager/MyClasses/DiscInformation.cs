using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.MyClasses
{
    public partial class DiscInformation
    {
        public delegate void Disc();
        #region Переменные
        public static string DiscInfo { get; set; }="";
        public static long DiscSize { get; set; }
        public static long TotalDiscSize { get; set; } 
        public static long FreeSpace { get; set; }
        #endregion
    }
}
