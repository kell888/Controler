using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KellControlServer
{
    public static class Log
    {
        public enum Level
        {
            Info,
            Trace,
            Error
        }
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        public static void WriteLog(string module, string msg, Level level)
        {
            DateTime now = DateTime.Now;
            string p = path + level.ToString();
            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);
            string m = "[" + now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + module + Environment.NewLine + msg + Environment.NewLine + Environment.NewLine;
            File.AppendAllText(p + "\\" + now.ToShortDateString() + ".log", m);
        }
    }
}
