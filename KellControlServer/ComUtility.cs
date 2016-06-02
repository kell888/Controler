using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net;

namespace KellControlServer
{
    public static class ComUtility
    {
        public static string GetHex(byte data, bool format = false)
        {
            string fo = string.Empty;
            if (format)
                fo = "0x";
            return fo + data.ToString("X2");
        }

        public static string Format(string hex, bool format)
        {
            string fo = string.Empty;
            string[] fs = hex.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (fs.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string f in fs)
                {
                    if (format)
                    {
                        if (!f.StartsWith("0x"))
                            sb.Append("0x" + f);
                        else
                            sb.Append(f);
                    }
                    else
                    {
                        if (f.StartsWith("0x"))
                            sb.Append(f.Substring(2));
                        else
                            sb.Append(f);
                    }
                    sb.Append(" ");
                }
                fo = sb.ToString().TrimEnd(' ');
            }
            else
            {
                fo = hex;
                if (format)
                {
                    if (!hex.StartsWith("0x"))
                        fo = "0x" + hex;
                }
                else
                {
                    if (hex.StartsWith("0x"))
                        fo = hex.Substring(2);
                }
            }
            return fo;
        }

        public static string GetHex(byte[] data, bool format = false)
        {
            string fo = string.Empty;
            if (format)
                fo = "0x";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(fo + data[i].ToString("X2") + " ");
            }
            return sb.ToString();
        }

        public static byte[] StrHexToBin(string StrHex)
        {
            StrHex = StrHex.Trim();
            string[] temp = StrHex.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            byte[] buf = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                buf[i] = System.Convert.ToByte(temp[i], 16);
            }
            return buf;
        }

        public static int[] GetDefaultIndexs(int length)
        {
            int[] indexs = new int[length];
            for (int i = 0; i < length; i++)
            {
                indexs[i] = i;
            }
            return indexs;
        }

        public static byte[] MergeData(byte[] data1, byte[] data2)
        {
            int len1 = 0, len2 = 0;
            if (data1 != null) len1 = data1.Length;
            if (data2 != null) len2 = data2.Length;
            byte[] data = new byte[len1 + len2];
            if (data.Length > 0)
            {
                for (int i = 0; i < len1; i++)
                {
                    data[i] = data1[i];
                }
                for (int i = 0; i < len2; i++)
                {
                    data[len1 + i] = data2[i];
                }
            }
            return data;
        }

        public static object[] Convert(byte[] data)
        {
            object[] result = null;
            if (data != null)
            {
                result = new object[data.Length];
                data.CopyTo(result, 0);
            }
            return result;
        }

        public static void GetBytes(ushort pari, out byte pari1, out byte pari2)
        {
            byte[] data = System.BitConverter.GetBytes(pari);
            pari1 = data[0];
            pari2 = data[1];
        }

        /// <summary>
        /// 要保证配置文档中存在ServerSocket配置项，格式为：122.114.39.219:8888
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public static void GetServerSocket(out IPAddress ip, out int port)
        {
            ip = IPAddress.Loopback;
            port = 8888;
            string server = ConfigurationManager.AppSettings["ServerSocket"];
            if (!string.IsNullOrEmpty(server))
            {
                string[] ipport = server.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (ipport.Length == 2)
                {
                    IPAddress i;
                    if (IPAddress.TryParse(ipport[0], out i))
                    {
                        ip = i;
                    }
                    int p;
                    if (int.TryParse(ipport[1], out p))
                    {
                        port = p;
                    }
                }
            }
        }
    }
}
