using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUD_console
{
    class SQLOperate
    {
        private string datasource;
        private string user;
        private string password;
        private string catalog;
        private string cntstr;
        public SQLOperate(string catalog = "master")//.gitignore設定要
        {
            this.catalog    = catalog;
            cntstr = ConfigurationManager.ConnectionStrings["sqlsvr"].ConnectionString;
        }
        public SQLOperate(string datasource, string user, string password, string catalog = "master")
        {
            this.datasource = datasource;
            this.user       = user;
            this.password   = password;
            this.catalog    = catalog;
        }
        public void Setcntstr()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = datasource;
            builder.UserID = user;
            builder.Password = password;
            builder.InitialCatalog = catalog;
            cntstr = builder.ConnectionString;
        }
        private void Execute(string sql)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cntstr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void Reader(string sql)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cntstr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetString(0)}{reader.GetString(1)}");
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

            }
        }
        public void CreateDB(string db_name)
        {
            //Console.WriteLine($"CREATE DATABASE {db_name}を実行します");
            string sql = $"CREATE DATABASE {db_name};";
            Execute(sql);
            //Console.WriteLine("データベースを作成しました。");
        }
        public void CreateTbl(string db_name, string tbl_name)
        {
            //Console.WriteLine($"USE {db_name}を実行します");
            //Console.WriteLine($"CREATE TABLE {tbl_name}を実行します");
            StringBuilder sb = new StringBuilder();
            sb.Append($"USE {db_name};");
            sb.Append($"CREATE TABLE {tbl_name} (id CHAR(4), name VARCHAR(100));");
            string sql = sb.ToString();
            Execute(sql);
            //Console.WriteLine("テーブルを作成しました。");
        }
        public void Insert(string db_name, string tbl_name, string id, string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"USE {db_name};");
            sb.Append($"INSERT INTO {tbl_name} VALUES ({id}, '{name}');");
            string sql = sb.ToString();
            Execute(sql);
            //Console.WriteLine("データを登録しました。");
        }
        public void ReadTbl(string db_name, string tbl_name, string col_name = "id", string col_name2 = "name")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"USE {db_name};");
            sb.Append($"SELECT {col_name}, {col_name2} FROM {tbl_name};");
            string sql = sb.ToString();
            Reader(sql);
        }
        public void Update(string db_name, string tbl_name, string col_name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"USE {db_name};");
            sb.Append($"UPDATE {tbl_name} SET {col_name} = 'update_test'");
            string sql = sb.ToString();
            Execute(sql);
            //Console.WriteLine($"データを更新しました。");
        }
        public void Delete(string db_name, string tbl_name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"USE {db_name};");
            sb.Append($"DELETE FROM {tbl_name}");
            string sql = sb.ToString();
            Execute(sql);
            //Console.WriteLine("データを削除しました。");
        }
        public void DropDB(string db_name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"USE {catalog};");
            sb.Append($"DROP DATABASE {db_name};");
            string sql = sb.ToString();
            Execute(sql);
        }
        public void DropTbl(string db_name, string tbl_name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"USE {db_name};");
            sb.Append($"DROP TABLE {tbl_name};");
            string sql = sb.ToString();
            Execute(sql);
        }
    }
}
