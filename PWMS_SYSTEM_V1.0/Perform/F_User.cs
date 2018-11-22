using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PWMS_SYSTEM_V1._0.Perform
{
    public partial class F_User : Form
    {
        public F_User()
        {
            InitializeComponent();
        }

        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        public static DataSet MyDS_Grid;

        //调用添加用户窗体
        private void tool_UserAdd_Click(object sender, EventArgs e)
        {
            //初始化窗体
            Perform.F_UserAdd FrmUserAdd = new F_UserAdd();
            //设置tag
            FrmUserAdd.Tag = 1;
            FrmUserAdd.Text = tool_UserAdd.Text + "用户";
            FrmUserAdd.ShowDialog(this);
        }

        //实现修改操作
        private void tool_UserAmend_Click(object sender, EventArgs e)
        {
            //判断是否是超级用户
            if (ModuleClass.MyModule.User_ID.Trim() == "0001")
            {
                MessageBox.Show("不能修改超级用户。");
                return;
            }
            Perform.F_UserAdd FrmUserAdd = new F_UserAdd();
            FrmUserAdd.Tag = 2;
            FrmUserAdd.Text = tool_UserAmend.Text + "用户";
            FrmUserAdd.ShowDialog(this);
        }

        private void F_User_Load(object sender, EventArgs e)
        {
            //从指定表进行查询
            MyDS_Grid = MyDataClass.GetDataSet("select ID as 编号,Name as 用户名 from tb_Login", "tb_Login");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                //设置GridView控件中的显示内容
                ModuleClass.MyModule.User_ID = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                ModuleClass.MyModule.User_Name = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                //对菜单栏控件的可用性进行设置
                tool_UserAmend.Enabled = true;
                tool_UserDelete.Enabled = true;
                tool_UserPopedom.Enabled = true;
            }
            else
            {
                ModuleClass.MyModule.User_ID = "";
                ModuleClass.MyModule.User_Name = "";
                tool_UserAmend.Enabled = false;
                tool_UserDelete.Enabled = false;
                tool_UserPopedom.Enabled = false;
            }
        }

        //权限设置
        private void tool_UserPopedom_Click(object sender, EventArgs e)
        {
            //判断是否为超级用户
            if (ModuleClass.MyModule.User_ID.Trim() == "0001")
            {
                MessageBox.Show("不能修改超级用户权限。");
                return;
            }
            F_UserPope FrmUserPope = new F_UserPope();
            FrmUserPope.Text = "用户权限设置";
            FrmUserPope.ShowDialog(this);
        }

        private void tool_UserDelete_Click(object sender, EventArgs e)
        {
            //判断是否尾插即用户，如是则不能进行删除操作
            if (ModuleClass.MyModule.User_ID != "")
            {
                if (ModuleClass.MyModule.User_ID.Trim() == "0001")
                {
                    MessageBox.Show("不能删除超级用户。");
                    return;
                }
                MyDataClass.GetA_M_D_command("Delete tb_Login where ID='" + ModuleClass.MyModule.User_ID.Trim() + "'");
                MyDataClass.GetA_M_D_command("Delete tb_UserPope where ID='" + ModuleClass.MyModule.User_ID.Trim() + "'");
                MyDS_Grid = MyDataClass.GetDataSet("select ID as 编号,Name as 用户名 from tb_Login", "tb_Login");
                dataGridView1.DataSource = MyDS_Grid.Tables[0];
            }
            else
                MessageBox.Show("无法删除空数据表。");
        }

        private void F_User_Activated(object sender, EventArgs e)
        {
            MyDS_Grid = MyDataClass.GetDataSet("select ID as 编号,Name as 用户名 from tb_Login", "tb_Login");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
        }

        private void tool_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
