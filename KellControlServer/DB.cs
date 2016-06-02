using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DBHelper;
using System.Net;

namespace KellControlServer
{
    public static class DB
    {
        internal static Dictionary<int, string> GetNewCmds(string module)
        {
            Dictionary<int, string> cmds = new Dictionary<int, string>();
            string host = Const.host;
            SQLDBHelper sqlHelper = new SQLDBHelper();
            try
            {
                DataTable dt = sqlHelper.Query("select ID,MyAction from MyControl where IsEnable=1 and Lower(Host)='" + host.ToLower() + "' and Lower(MyModule)='" + module.ToLower() + "' and (LastTime is NULL or ThisTime>LastTime) order by ThisTime asc");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int id = 0;
                        string cmd = string.Empty;
                        if (dt.Rows[i][0] != null && dt.Rows[i][0] != DBNull.Value)
                            id = Convert.ToInt32(dt.Rows[i][0].ToString());
                        if (dt.Rows[i][1] != null && dt.Rows[i][1] != DBNull.Value)
                            cmd = dt.Rows[i][1].ToString();
                        cmds.Add(id, cmd);
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("GetNewCmds", e.ToString(), Log.Level.Error);
            }
            return cmds;
        }

        internal static bool AfterCmdActionSuccess(int id)
        {
            int r = 0;
            SQLDBHelper sqlHelper = new SQLDBHelper();
            try
            {
                object o = sqlHelper.GetSingle("select ThisTime from MyControl where ID=" + id);
                if (o != null && o != DBNull.Value)
                {
                    DateTime ret;
                    if (DateTime.TryParse(o.ToString(), out ret))
                    {

                    }
                    else
                    {
                        r = sqlHelper.ExecuteSql("update MyControl set ThisTime=getdate() where ID=" + id);
                    }
                }
                else
                {
                    r = sqlHelper.ExecuteSql("update MyControl set ThisTime=getdate() where ID=" + id);
                }
                r = sqlHelper.ExecuteSql("update MyControl set LastTime=ThisTime where ID=" + id);
            }
            catch (Exception e)
            {
                Log.WriteLog("AfterCmdActionSuccess", e.ToString(), Log.Level.Error);
            }
            return r > 0;
        }

        internal static void SkipAllCmds(string module)
        {
            string host = Const.host;
            SQLDBHelper sqlHelper = new SQLDBHelper();
            try
            {
                DataTable dt = sqlHelper.Query("select ID from MyControl where IsEnable=1 and Lower(Host)='" + host.ToLower() + "' and Lower(MyModule)='" + module.ToLower() + "' and (LastTime is NULL or ThisTime>LastTime) order by ThisTime asc");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int id = 0;
                        if (dt.Rows[i][0] != null && dt.Rows[i][0] != DBNull.Value)
                            id = Convert.ToInt32(dt.Rows[i][0].ToString());
                        if (id > 0)
                            AfterCmdActionSuccess(id);
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("SkipAllCmds", e.ToString(), Log.Level.Error);
            }
        }
    }
}