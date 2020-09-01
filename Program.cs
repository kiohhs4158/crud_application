using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Write("サーバ名を入力してください：");
            string datasource = Console.ReadLine();
            Console.Write("ユーザ名を入力してください：");
            string user = Console.ReadLine();
            Console.Write("パスワードを入力してください：");
            string password = Console.ReadLine();
            
            string db_name = "test_db";
            string tbl_name = "test_tbl";
            string col_name = "name";
            string id = "0001";
            string name = "test";

            SQLOperate sqlope = new SQLOperate(datasource, user, password);
            //SQLOperate sqlope = new SQLOperate();
            sqlope.Setcntstr();
            sqlope.CreateDB(db_name);
            sqlope.CreateTbl(db_name, tbl_name);
            sqlope.Insert(db_name, tbl_name, id, name);
            sqlope.ReadTbl(db_name, tbl_name);
            sqlope.Update(db_name, tbl_name, col_name);
            sqlope.ReadTbl(db_name, tbl_name);
            sqlope.Delete(db_name, tbl_name);
            sqlope.DropTbl(db_name, tbl_name);
            sqlope.DropDB(db_name);
        }
    }
}
