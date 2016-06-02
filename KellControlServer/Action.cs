using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace KellControlServer
{
    public static class Action
    {
        static byte sHead = Convert.ToByte(Const.sHead, 16);
        static byte rHead = Convert.ToByte(Const.rHead, 16);
        static byte[] tail = ComUtility.StrHexToBin(Const.tail);
        static byte sCmd = Convert.ToByte(Const.sCmd, 16);
        static byte gCmd = Convert.ToByte(Const.gCmd, 16);
        static byte[] setK1 = ComUtility.StrHexToBin(Const.setK1);
        static byte[] setK2 = ComUtility.StrHexToBin(Const.setK2);
        static byte[] setK3 = ComUtility.StrHexToBin(Const.setK3);
        static byte[] setK4 = ComUtility.StrHexToBin(Const.setK4);
        static byte[] setAll = ComUtility.StrHexToBin(Const.setAll);
        static byte[] resetK1 = ComUtility.StrHexToBin(Const.resetK1);
        static byte[] resetK2 = ComUtility.StrHexToBin(Const.resetK2);
        static byte[] resetK3 = ComUtility.StrHexToBin(Const.resetK3);
        static byte[] resetK4 = ComUtility.StrHexToBin(Const.resetK4);
        static byte[] resetAll = ComUtility.StrHexToBin(Const.resetAll);
        static byte[] getK1 = ComUtility.StrHexToBin(Const.getK1);
        static byte[] getK2 = ComUtility.StrHexToBin(Const.getK2);
        static byte[] getK3 = ComUtility.StrHexToBin(Const.getK3);
        static byte[] getK4 = ComUtility.StrHexToBin(Const.getK4);
        static byte[] getAll = ComUtility.StrHexToBin(Const.getAll);

        static bool Send(int k, bool set, bool read = false)
        {
            bool flag = false;
            System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();
            sp.PortName = "COM" + Const.ComNum;
            sp.BaudRate = Convert.ToInt32(Const.BaudRate);
            sp.DataBits = Convert.ToInt32(Const.DataBits);
            sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Const.StopBits);
            sp.Parity = (Parity)Enum.Parse(typeof(Parity), Const.Parity);
            byte[] package = null;
            try
            {
                sp.Open();
                if (sp.IsOpen)
                {
                    byte[] realData = null;
                    switch (k)
                    {
                        case 1:
                            if (read)
                                realData = getK1;
                            else
                            {
                                if (set)
                                    realData = setK1;
                                else
                                    realData = resetK1;
                            }
                            break;
                        case 2:
                            if (read)
                                realData = getK2;
                            else
                            {
                                if (set)
                                    realData = setK2;
                                else
                                    realData = resetK2;
                            }
                            break;
                        case 3:
                            if (read)
                                realData = getK3;
                            else
                            {
                                if (set)
                                    realData = setK3;
                                else
                                    realData = resetK3;
                            }
                            break;
                        case 4:
                            if (read)
                                realData = getK4;
                            else
                            {
                                if (set)
                                    realData = setK4;
                                else
                                    realData = resetK4;
                            }
                            break;
                        case 0:
                            if (read)
                                realData = getAll;
                            else
                            {
                                if (set)
                                    realData = setAll;
                                else
                                    realData = resetAll;
                            }
                            break;
                    }
                    byte cmd = sCmd;
                    if (read) cmd = gCmd;
                    int dataIndex = 2;
                    int tailLen = tail.Length;//帧尾
                    int needLen = tailLen + dataIndex;
                    int length = (byte)realData.Length;
                    package = new byte[length + needLen];
                    package[0] = sHead;//帧头
                    package[1] = cmd;//命令号
                    for (int i = 0; i < realData.Length; i++)
                    {
                        package[dataIndex + i] = realData[i];
                    }
                    for (int i = 0; i < tailLen; i++)
                    {
                        package[dataIndex + i + length] = tail[i];
                    }
                    if (package != null)
                    {
                        sp.Write(package, 0, package.Length);
                        flag = true;
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("Send", e.ToString(), Log.Level.Error);
                //throw e;
            }
            finally
            {
                sp.Close();
                System.Threading.Thread.Sleep(200);
            }
            return flag;
        }

        internal static bool SetK1()
        {
            try
            {
                return Send(1, true);
            }
            catch { return false; }
        }

        internal static bool SetK2()
        {
            try
            {
                return Send(2, true);
            }
            catch { return false; }
        }

        internal static bool SetK3()
        {
            try
            {
                return Send(3, true);
            }
            catch { return false; }
        }

        internal static bool SetK4()
        {
            try
            {
                return Send(4, true);
            }
            catch { return false; }
        }

        internal static bool SetAll()
        {
            try
            {
                return Send(0, true);
            }
            catch { return false; }
        }

        internal static bool ResetK1()
        {
            try
            {
                return Send(1, false);
            }
            catch { return false; }
        }

        internal static bool ResetK2()
        {
            try
            {
                return Send(2, false);
            }
            catch { return false; }
        }

        internal static bool ResetK3()
        {
            try
            {
                return Send(3, false);
            }
            catch { return false; }
        }

        internal static bool ResetK4()
        {
            try
            {
                return Send(4, false);
            }
            catch { return false; }
        }

        internal static bool ResetAll()
        {
            try
            {
                return Send(0, false);
            }
            catch { return false; }
        }

        internal static bool GetK1(out bool status)
        {
            status = false;
            try
            {
                return Send(1, false, true);
            }
            catch { return false; }
        }

        internal static bool GetK2(out bool status)
        {
            status = false;
            try
            {
                return Send(2, false, true);
            }
            catch { return false; }
        }

        internal static bool GetK3(out bool status)
        {
            status = false;
            try
            {
                return Send(3, false, true);
            }
            catch { return false; }
        }

        internal static bool GetK4(out bool status)
        {
            status = false;
            try
            {
                return Send(4, false, true);
            }
            catch { return false; }
        }

        internal static bool GetAll(out bool[] allStatus)
        {
            allStatus = (bool[])Array.CreateInstance(typeof(bool), 4);
            try
            {
                return Send(0, false, true);
            }
            catch { return false; }
        }
    }
}