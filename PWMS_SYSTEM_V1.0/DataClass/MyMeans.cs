using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PWMS_SYSTEM_V1._0.DataClass
{
    class MyMeans
    {
        #region the property
        public static string LogIn_ID = "";
        public static string LogIn_Name = "";
        public static string Mean_Table = "", Mean_SQL = "", Mean_Field = "";
        public static SqlConnection My_Con;
        public static string M_str_sqlcon = "Server=DESKTOP-AIE7LD0\\NAME;Database=db_PWMS;Integrated Security=true";
        public static int LogIn_n = 0;
        public static string AllSql = "Select * from tb_Stuffbusic";
        #endregion

        //建立与数据库的连接，并打开，返回sqlconnection的对象信息
        public static SqlConnection getcon()
        {
            My_Con = new SqlConnection(M_str_sqlcon);
            My_Con.Open();
            return My_Con;

        }
        //测试打开数据库
        public void con_open()
        {
            getcon();
            //con_close();
        }
        //如果这个连接状态为连接时，则关闭数据库连接，释放所有空间
        public void con_close()
        {
            if (My_Con.State == ConnectionState.Open)
            {
                My_Con.Close();
                My_Con.Dispose();
            }
        }
        //利用sqldatareader来对数据进行只读
        public SqlDataReader getCommand(string SQLstring)
        {
            getcon();
            SqlCommand My_Command = My_Con.CreateCommand();
            My_Command.CommandText = SQLstring;
            SqlDataReader My_reader = My_Command.ExecuteReader();
            return My_reader;

        }
        //利用ExecuteNonQuery来进行对数据的添加修改和删除等操作
        public void GetA_M_D_command(string A_M_Dstring)
        {
            getcon();
            SqlCommand SQLcommand = new SqlCommand(A_M_Dstring, My_Con);
            SQLcommand.ExecuteNonQuery();
            SQLcommand.Dispose();
            con_close();
                

        }
        //执行数据库中的添加修改和删除操作，使用dataset现象
        public DataSet GetDataSet(string sqlStr, string tableName)
        {
            getcon();
            SqlDataAdapter sqlDataAdappter = new SqlDataAdapter(sqlStr, My_Con);
            DataSet My_DataSet = new DataSet();
            sqlDataAdappter.Fill(My_DataSet, tableName);
            con_close();
            return My_DataSet;
        }
    }

    
}
