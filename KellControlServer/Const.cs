using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace KellControlServer
{
    public static class Const
    {
        public static string host
        {
            get
            {
                string h = "127.0.0.1";
                string hst = ConfigurationManager.AppSettings["host"];
                if (!string.IsNullOrEmpty(hst))
                    h = hst;
                return h;
            }
        }
        public static int port
        {
            get
            {
                int p = 8888;
                string prt = ConfigurationManager.AppSettings["port"];
                int r;
                if (!string.IsNullOrEmpty(prt) && int.TryParse(prt, out r))
                    p = r;
                return p;
            }
        }
        public static string module
        {
            get
            {
                return ConfigurationManager.AppSettings["module"];
            }
        }

        public static string sHead
        {
            get
            {
                return ConfigurationManager.AppSettings["sHead"];
            }
        }
        public static string rHead
        {
            get
            {
                return ConfigurationManager.AppSettings["rHead"];
            }
        }
        public static string tail
        {
            get
            {
                return ConfigurationManager.AppSettings["tail"];
            }
        }
        public static string sCmd
        {
            get
            {
                return ConfigurationManager.AppSettings["sCmd"];
            }
        }
        public static string gCmd
        {
            get
            {
                return ConfigurationManager.AppSettings["gCmd"];
            }
        }

        public static string setK1
        {
            get
            {
                return ConfigurationManager.AppSettings["setK1"];
            }
        }
        public static string setK2
        {
            get
            {
                return ConfigurationManager.AppSettings["setK2"];
            }
        }
        public static string setK3
        {
            get
            {
                return ConfigurationManager.AppSettings["setK3"];
            }
        }
        public static string setK4
        {
            get
            {
                return ConfigurationManager.AppSettings["setK4"];
            }
        }
        public static string setAll
        {
            get
            {
                return ConfigurationManager.AppSettings["setAll"];
            }
        }
        public static string resetK1
        {
            get
            {
                return ConfigurationManager.AppSettings["resetK1"];
            }
        }
        public static string resetK2
        {
            get
            {
                return ConfigurationManager.AppSettings["resetK2"];
            }
        }
        public static string resetK3
        {
            get
            {
                return ConfigurationManager.AppSettings["resetK3"];
            }
        }
        public static string resetK4
        {
            get
            {
                return ConfigurationManager.AppSettings["resetK4"];
            }
        }
        public static string resetAll
        {
            get
            {
                return ConfigurationManager.AppSettings["resetAll"];
            }
        }
        public static string getK1
        {
            get
            {
                return ConfigurationManager.AppSettings["getK1"];
            }
        }
        public static string getK2
        {
            get
            {
                return ConfigurationManager.AppSettings["getK2"];
            }
        }
        public static string getK3
        {
            get
            {
                return ConfigurationManager.AppSettings["getK3"];
            }
        }
        public static string getK4
        {
            get
            {
                return ConfigurationManager.AppSettings["getK4"];
            }
        }
        public static string getAll
        {
            get
            {
                return ConfigurationManager.AppSettings["getAll"];
            }
        }

        public static string ComNum
        {
            get
            {
                return ConfigurationManager.AppSettings["ComNum"];
            }
        }
        public static string BaudRate
        {
            get
            {
                return ConfigurationManager.AppSettings["BaudRate"];
            }
        }
        public static string DataBits
        {
            get
            {
                return ConfigurationManager.AppSettings["DataBits"];
            }
        }
        public static string StopBits
        {
            get
            {
                return ConfigurationManager.AppSettings["StopBits"];
            }
        }
        public static string Parity
        {
            get
            {
                return ConfigurationManager.AppSettings["Parity"];
            }
        }

        public static int Interval
        {
            get
            {
                int interval = 1000;//默认为一秒钟
                string inv = ConfigurationManager.AppSettings["Interval"];
                if (!string.IsNullOrEmpty(inv))
                {
                    int r;
                    if (int.TryParse(inv, out r))
                    {
                        interval = r;
                    }
                }
                return interval;
            }
        }

        public static int RunStatus
        {
            get
            {
                int status = 1;//默认为运行状态
                string stat = ConfigurationManager.AppSettings["RunStatus"];
                if (!string.IsNullOrEmpty(stat))
                {
                    int r;
                    if (int.TryParse(stat, out r))
                    {
                        status = r;
                    }
                }
                return status;
            }
        }
    }
}
