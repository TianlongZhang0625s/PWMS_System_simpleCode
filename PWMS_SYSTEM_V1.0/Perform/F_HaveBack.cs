using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PWMS_SYSTEM_V1._0.Perform
{
    public partial class F_HaveBack : Form
    {
        public F_HaveBack()
        {
            InitializeComponent();
        }

        //定义使用的全局变量
        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        ModuleClass.MyModule MyMC = new ModuleClass.MyModule();

        private void button1_Click(object sender, EventArgs e)
        {
            //button1_Click事件为备份数据库
            string Str_dar = "";
            //选择并设置默认路径
            if (radioButton1.Checked == true)
            {
                Str_dar = System.Environment.CurrentDirectory + "\\bar\\";
                
            }
            //选择其他路径
            if (radioButton2.Checked == true)
            {
                Str_dar = textBox2.Text + "\\";

            }
            if (textBox2.Text == "" & radioButton2.Checked == true)
            {
                MessageBox.Show("请选择备份数据库文件的路径。");
                return;
            }

            //尝试数据库的备份
            try
            {
                //定义SQL语句实现到指定时间点的还原
                Str_dar = "backup database db_PWMS to disk='" + Str_dar + (System.DateTime.Now.ToShortDateString()).ToString() + MyMC.Time_Format(System.DateTime.Now.ToString()) + ".bak" + "'";
                //将指令在MyClass类中的GetA_M_D_command进行对数据库的操作执行
                MyDataClass.GetA_M_D_command(Str_dar);
                //弹出提示信息的对话框
                MessageBox.Show("数据备份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                //提示返回的错误信息
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //浏览路径
        private void button2_Click(object sender, EventArgs e)
        {
            //调用文件路径对话框
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        //还原数据库操作
        private void button5_Click(object sender, EventArgs e)
        {
            //当文件路径为空时
            if (textBox3.Text == "")
            {
                MessageBox.Show("请选择备份数据库文件的路径。");
                return;
            }
            //尝试还原工作
            try
            {

                //判断数据库的连接状态，若连接则断开连接
                if (DataClass.MyMeans.My_Con.State == ConnectionState.Open)
                {
                    DataClass.MyMeans.My_Con.Close();
                }
                //数据库连接语句，指向master数据库
                string DateStr = "Data Source=MRDEL\\MRDEL;Database=master;User id=sa;PWD=";
                //同时建立一个新的SqlConnection对象
                SqlConnection conn = new SqlConnection(DateStr);
                //打开数据库连接
                conn.Open();

                //-------------------杀掉所有连接 db_PWMS 数据库的进程--------------
                //定义sql语句，以及所使用的变量
                string strSQL = "select spid from master..sysprocesses where dbid=db_id( 'db_PWMS') ";
                SqlDataAdapter Da = new SqlDataAdapter(strSQL, conn);

                DataTable spidTable = new DataTable();
                Da.Fill(spidTable);

                //向SqlCommand对象中传递Text指令
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.Connection = conn;

                //通过循环构成强行关闭用户进程的命令指令
                for (int iRow = 0; iRow <= spidTable.Rows.Count - 1; iRow++)
                {
                    Cmd.CommandText = "kill " + spidTable.Rows[iRow][0].ToString();   //强行关闭用户进程 
                    Cmd.ExecuteNonQuery();
                }
                //关闭并释放资源
                conn.Close();
                conn.Dispose();
                //--------------------------------------------------------------------

                //建立新的SqlConnection对象
                SqlConnection Tem_con = new SqlConnection(DateStr);
                //连接数据库
                Tem_con.Open();
                //第一个参数为一个完整的SQL语句，用于查找对应的还原文件
                SqlCommand SQLcom = new SqlCommand("backup log db_PWMS to disk='" + textBox3.Text.Trim() + "' restore database db_PWMS from disk='" + textBox3.Text.Trim() + "'", Tem_con);
                //进行查询
                SQLcom.ExecuteNonQuery();
                SQLcom.Dispose();
                Tem_con.Close();
                Tem_con.Dispose();
                MessageBox.Show("数据还原成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyDataClass.con_open();
                MyDataClass.con_close();
                //为保证数据的安全性，强制关闭整个系统
                MessageBox.Show("为了避免数据丢失，在数据库原还后将关闭整个系统。");
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.bak|*.bak";
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
