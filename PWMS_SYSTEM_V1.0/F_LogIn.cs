using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PWMS_SYSTEM_V1._0
{
    public partial class F_LogIn : Form
    {
        DataClass.MyMeans MyClass = new DataClass.MyMeans();
        public F_LogIn()
        {
            InitializeComponent();
        }

        private void butLogIn_Click(object sender, EventArgs e)
        {
            if (textName.Text != "" & textPass.Text != "")
            {
                //获取登陆表中的用户名和密码的组合
                SqlDataReader temDataReader = MyClass.getCommand("select * from tb_Login where Name='" + textName.Text.Trim() + "' and Pass= '" + textPass.Text.Trim() + "'");
                //read()得到的返回值bool型，利用这个特性
                bool ifcom = temDataReader.Read();
                if (ifcom)
                {
                    //获取登录名
                    DataClass.MyMeans.LogIn_Name = textName.Text.Trim();
                    //获取登陆的ID地址
                    DataClass.MyMeans.LogIn_ID = temDataReader.GetString(0);
                    //断开连接
                    DataClass.MyMeans.My_Con.Close();
                    //释放占用的资源
                    DataClass.MyMeans.My_Con.Dispose();
                    //获取对象的引用
                    DataClass.MyMeans.LogIn_n = (int)(this.Tag);
                    //窗口关闭，进入主界面
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //重新设置为空
                    textName.Text = "";
                    textPass.Text = "";
                }
                //调用MyMeans类中定义的Close方法
                MyClass.con_close();
            }
            else
                MessageBox.Show("请将登录信息添写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            //假如引用的对象为1
            if ((int)(this.Tag) == 1)
            {
                DataClass.MyMeans.LogIn_n = 3;
                Application.Exit();
            }
            else
            {
                //
                if ((int)(this.Tag) == 2)
                    this.Close();
            }
        }

        private void F_LogIn_Load(object sender, EventArgs e)
        {
            try
            {
                MyClass.con_open();
                MyClass.con_close();
                textName.Text = "";
                textPass.Text = "";
            }
            catch
            {
                MessageBox.Show("数据库连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
                
        }

        private void textName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //当按下回车键时,焦点转移到密码textBox控件上
            if (e.KeyChar == '\r')
                textPass.Focus();
        }
        private void textPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            //当按下回车键时,焦点转移到密码LogIn按钮
            if (e.KeyChar == '\r')
                butLogIn.Focus();
        }


        private void F_LogIn_Activated(object sender, EventArgs e)
        {
            //当处于活动窗体时,焦点在name的TextBox控件上
            textName.Focus();
        
        }
    }
}


