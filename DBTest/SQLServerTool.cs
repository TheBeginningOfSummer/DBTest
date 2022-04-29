using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DBTest
{
    class SQLServerTool
    {
        public  string DataSource { get; private set; }
        public  string UserID { get; private set; }
        public string Password { get; private set; }
        public string InitialCatalog { get; private set; }
        /// <summary> sql连接参数</summary>
        SqlConnectionStringBuilder conString;
        /// <summary> sql命令</summary>
        SqlCommand sqlCommand;
        /// <summary> 连接服务器</summary>
        public  SqlConnection sqlConnection;
        /// <summary> 建立数据库和dataGridView组建的桥梁-----》填充DataTable（表示数据库中一个库中的一个表）或者DataSet（表示数据库的一个库）类型</summary>
        SqlDataAdapter sqlDataAdapter;

        public SQLServerTool(string dataSource,string userID,string password,string initialCatalog)
        {
            conString = new SqlConnectionStringBuilder();
            conString.DataSource = dataSource;
            conString.UserID = userID;
            conString.Password = password;
            conString.InitialCatalog = initialCatalog;
            conString.IntegratedSecurity = false;

            sqlConnection = new SqlConnection(conString.ToString());//SQL连接实例
            sqlCommand = new SqlCommand();//SQL命令实例
            sqlCommand.Connection = sqlConnection;//命令实例所属连接
            sqlDataAdapter = new SqlDataAdapter();
        }

        public void Connection()
        {
            try
            {
                sqlConnection.Open();
                //sqlConnection.OpenAsync(cancellationToken);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.ToString());
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void CommandExeCute(string command)
        {
            try
            {
                sqlCommand.CommandText = command;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public DataTable SelectExeCute(DataTable dataTable, string tableName)
        {
            //DataTable table = sqlConnection.GetSchema("Tables");
            //bool bExist = false;
            //foreach (DataRow row in table.Rows)
            //{
            //    if (string.Equals(tableName, row[2].ToString()))
            //    {
            //        bExist = true;
            //        break;
            //    }
            //}
            //if (bExist)
            //{
            //    sqlCommand.CommandText = "SELECT * FROM " + tableName;
            //    sqlDataAdapter.SelectCommand = sqlCommand;
            //    sqlDataAdapter.Fill(dataTable);
            //}
            //else
            //{
            //    MessageBox.Show("不存在的表");
            //}
            try
            {
                sqlCommand.CommandText = "SELECT * FROM " + tableName;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return dataTable;
        }

        
    }
}
