using System;
using System.Collections.Generic;
using System.Threading;
using System.Configuration;
using DataTransfer;
using System.Net;
using System.Xml;

namespace KellControlServer
{
    static class Program
    {
        static Client client;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.WriteLog("Main()", "Program Start...", Log.Level.Info);
            DB.SkipAllCmds(Const.module);
            client = new Client();
            client.Receiving += new EventHandler<ReceiveArgs>(client_Receiving);
            if (Const.Interval >= 1000)
               client.HeartBeatInterval = (uint)(Const.Interval / 1000);
            //IPAddress ip = new GetIP().GetLocalIPv4();
            //client.Connect(ip, Const.port);
            IPAddress ip;
            int port;
            Common.GetServerSocket(out ip, out port);
            client.Connect(ip, port);
            Console.WriteLine("终端控制服务已启动...");
            //Console.WriteLine("退出服务请键入[Q]:");
            //string input = Console.ReadLine().ToUpper();
            bool running = false, stopping = false;
            int last_status = GetRunStatus();
            bool control = true;
            while (true)
            {
                //if (input == "Q")
                //    break;
                if (last_status == 0)
                {
                    if (!stopping)
                    {
                        try
                        {
                            client.Stopping();
                        }
                        catch (Exception e)
                        {
                            Log.WriteLog("client.Stopping", e.ToString(), Log.Level.Error);
                        }
                        finally
                        {
                            stopping = true;
                            running = false;
                        }
                    }
                }
                else if (last_status == 1)
                {
                    if (!running)
                    {
                        try
                        {
                            client.Running();
                        }
                        catch (Exception e)
                        {
                            Log.WriteLog("client.Running", e.ToString(), Log.Level.Error);
                        }
                        finally
                        {
                            running = true;
                            stopping = false;
                        }
                    }
                }
                Dictionary<int, string> cmds = DB.GetNewCmds(Const.module);
                foreach (int id in cmds.Keys)
                {
                    bool status;
                    bool[] allStatus;
                    bool flag = false;
                    string cmd = cmds[id];
                    switch (cmd.ToLower())
                    {
                        case "setk1":
                            flag = Action.SetK1();
                            break;
                        case "setk2":
                            flag = Action.SetK2();
                            break;
                        case "setk3":
                            flag = Action.SetK3();
                            break;
                        case "setk4":
                            flag = Action.SetK4();
                            break;
                        case "setall":
                            flag = Action.SetAll();
                            break;
                        case "resetk1":
                            flag = Action.ResetK1();
                            break;
                        case "resetk2":
                            flag = Action.ResetK2();
                            break;
                        case "resetk3":
                            flag = Action.ResetK3();
                            break;
                        case "resetk4":
                            flag = Action.ResetK4();
                            break;
                        case "resetall":
                            flag = Action.ResetAll();
                            break;
                        case "getk1":
                            flag = Action.GetK1(out status);
                            break;
                        case "getk2":
                            flag = Action.GetK2(out status);
                            break;
                        case "getk3":
                            flag = Action.GetK3(out status);
                            break;
                        case "getk4":
                            flag = Action.GetK4(out status);
                            break;
                        case "getall":
                            flag = Action.GetAll(out allStatus);
                            break;
                        default:
                            break;
                    }
                    if (flag)
                    {
                        flag = DB.AfterCmdActionSuccess(id);
                        Console.WriteLine(cmd + "命令执行成功！");
                        control = true;
                    }
                    else
                    {
                        if (control)
                        {
                            Console.WriteLine(cmd + "命令执行失败！");
                            control = false;
                        }
                    }
                }
                Thread.Sleep(Const.Interval);//休息一下，再重新检测有没新来的命令
                int new_status = GetRunStatus();
                if (new_status != last_status)
                {
                    running = false;
                    stopping = false;
                    last_status = new_status;
                }
            }
        }

        static void client_Receiving(object sender, ReceiveArgs e)
        {
            Exception ex = null;
            if (e.Msg == Common.RunMsg)
            {
                ex = SetRunStatus(1);
            }
            else if (e.Msg == Common.StopMsg)
            {
                ex = SetRunStatus(0);
            }
            if (ex != null)
                Console.WriteLine("执行SetRunStatus更新配置文档失败：" + ex.Message);
        }

        public static Exception SetRunStatus(int status)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "KellControlServer.exe.config");
                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='RunStatus']");
                xElem.SetAttribute("value", status.ToString());
                xDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "KellControlServer.exe.config");
            }
            catch (Exception e)
            {
                Log.WriteLog("SetRunStatus", e.ToString(), Log.Level.Error);
                //return e;
            }
            return null;
        }

        public static int GetRunStatus()
        {
            int status = 1;
            string stat = ConfigurationManager.AppSettings["RunStatus"];
            int r;
            if (!string.IsNullOrEmpty(stat) && int.TryParse(stat, out r))
                status = r;
            return status;
        }
    }
}