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
    public partial class F_Main : Form
    {
        DataClass.MyMeans MyClass = new DataClass.MyMeans();
        ModuleClass.MyModule MyMenu = new ModuleClass.MyModule();

        public F_Main()
        {
            InitializeComponent();
        }

        private void Preen_Main()
        {
            //在状态栏中显式登录名
            statusStrip1.Items[2].Text = DataClass.MyMeans.LogIn_Name;
            treeView1.Nodes.Clear();
            //调用MyModiule中的getMenu方法，将menustrip1中的菜单添加到treeView1控件中
            MyMenu.GetMenu(treeView1, menuStrip1);
            MyMenu.MainMenuF(menuStrip1);
            //根据权限设置菜单的可用状态
            MyMenu.MainPope(menuStrip1, DataClass.MyMeans.LogIn_Name);

        }


        private void F_Main_Load(object sender, EventArgs e)
        {
            F_LogIn FrmLogIn = new F_LogIn();//声明登陆窗体，进行调用
            //设置与login窗体有关的数据对象设置，应为int型
            FrmLogIn.Tag = 1;
            //打开窗体
            FrmLogIn.ShowDialog();
            FrmLogIn.Dispose();
            //当调用登陆窗体时：
            if (DataClass.MyMeans.LogIn_n == 1)
            {
                //初始化窗体内的各项菜单
                Preen_Main();
                //当i取值为1时，设置为有关生日的信息的显示及初始化，不为1时，
                //则为员工合同的信息级初始化，此时表格已经加载在内存中
                MyMenu.PactDay(1);
                MyMenu.PactDay(2);

            }
            DataClass.MyMeans.LogIn_n = 3;

        }

        private void F_Main_Activated(object sender, EventArgs e)
        {
            if (DataClass.MyMeans.LogIn_n == 2)
                Preen_Main();
            DataClass.MyMeans.LogIn_n = 3;
        }


        private void Tool_Folk_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 2);
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tool_Stuffbusic_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_ClewBirthday_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_ClewBargain_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_Stufind_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_Stusum_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_DayWordPad_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_AddressBook_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);

        }

        private void Tool_Back_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_Clear_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void Tool_NewLogon_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);

        }

        private void Tool_Setup_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.Trim() == "系统退出")
            {
                Application.Exit();
            }
            MyMenu.TreeMenuF(menuStrip1, e);
        }

        private void Button_Stuffbusic_Click(object sender, EventArgs e)
        {
            if (Tool_Stuffbusic.Enabled == true)
                Tool_Stuffbusic_Click(sender, e);
            else
                MessageBox.Show("当前用户无权调用" + "\"" + ((ToolStripButton)sender).Text + "\"" + "窗体");

        }

        private void Button_Stufind_Click(object sender, EventArgs e)
        {
            if (Tool_Stufind.Enabled == true)
                Tool_Stufind_Click(sender,e);
            else
                MessageBox.Show("当前用户无权调用" + "\"" + ((ToolStripButton)sender).Text + "\"" + "窗体");

        }

        private void Button_ClewBargain_Click(object sender, EventArgs e)
        {
            if (Tool_ClewBargain.Enabled == true)
                Tool_ClewBargain_Click(sender, e);
            else
                MessageBox.Show("当前用户无权调用" + "\"" + ((ToolStripButton)sender).Text + "\"" + "窗体");
        }

        private void Botton_AddressBook_Click(object sender, EventArgs e)
        {
            if (Tool_AddressBook.Enabled == true)
                Tool_AddressBook_Click(sender, e);
            else
                MessageBox.Show("当前用户无权调用" + "\"" + ((ToolStripButton)sender).Text + "\"" + "窗体");
        }

        private void Botton_DayWordPad_Click(object sender, EventArgs e)
        {
            if (Tool_DayWordPad.Enabled == true)
                Tool_DayWordPad_Click(sender, e);
            else
                MessageBox.Show("当前用户无权调用" + "\"" + ((ToolStripButton)sender).Text + "\"" + "窗体");
        }

        private void Tool_Counter_Click(object sender, EventArgs e)
        {
            MyMenu.Show_Form(sender.ToString().Trim(), 1);
        }





        
    }
}
