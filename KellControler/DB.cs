using System;
using System.Collections.Generic;
using System.Text;
using DBHelper;

namespace KellControler
{
    public static class DB
    {
        internal static bool SendCmd(string host, string module, string action)
        {
            int r = 0;
            SQLDBHelper sqlHelper = new SQLDBHelper();
            object o = sqlHelper.GetSingle("select ID from MyControl where Lower(Host)='" + host.ToLower() + "' and Lower(MyModule)='" + module.ToLower() + "' and Lower(MyAction)='" + action.ToLower() + "'");
            if (o != null && o != DBNull.Value)
            {
                int id = Convert.ToInt32(o);
                r = sqlHelper.ExecuteSql("update MyControl set ThisTime=getdate() where ID=" + id);
            }
            else
            {
                r = sqlHelper.ExecuteSql("insert into MyControl(Host,MyModule,MyAction) values ('" + host + "','" + module + "','" + action + "')");
            }
            return r > 0;
        }
    }
}
