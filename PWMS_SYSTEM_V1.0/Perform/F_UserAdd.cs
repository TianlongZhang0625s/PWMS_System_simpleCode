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
    public partial class F_UserAdd : Form
    {
        public F_UserAdd()
        {
            InitializeComponent();
        }


        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        ModuleClass.MyModule MyMC = new ModuleClass.MyModule();
        public DataSet DSet;
        public static string AutoID = "";

        private void F_UserAdd_Load(object sender, EventArgs e)
        {
            if ((int)this.Tag == 1)
            {
                text_Name.Text = "";
                text_Pass.Text = "";
            }
            else
            {
                string ID = ModuleClass.MyModule.User_ID;
                DSet = MyDataClass.GetDataSet("select Name,Pass from tb_Login where ID='" + ID + "'", "tb_Login");
                text_Name.Text = Convert.ToString(DSet.Tables[0].Rows[0][0]);
                text_Pass.Text = Convert.ToString(DSet.Tables[0].Rows[0][1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (text_Name.Text == "" && text_Pass.Text == "")
            {
                MessageBox.Show("请将用户名和密码添加完整。");
                return;
            }
            DSet = MyDataClass.GetDataSet("select Name from tb_Login where Name='" + text_Name.Text + "'", "tb_Login");
            if ((int)this.Tag == 2 && text_Name.Text == ModuleClass.MyModule.User_Name)
            {
                MyDataClass.GetA_M_D_command("update tb_Login set Name='" + text_Name.Text + "',Pass='" + text_Pass.Text + "' where ID='" + ModuleClass.MyModule.User_ID + "'");
                return;
            }
            if (DSet.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("当前用户名已存在，请重新输入。");
                text_Name.Text = "";
                text_Pass.Text = "";
                return;
            }
            if ((int)this.Tag == 1)
            {
                AutoID = MyMC.GetAutoCoding("tb_Login", "ID");
                MyDataClass.GetA_M_D_command("insert into tb_Login (ID,Name,Pass) values('" + AutoID + "','" + text_Name.Text + "','" + text_Pass.Text + "')");
                MyMC.ADD_Pope(AutoID, 0);
                MessageBox.Show("添加成功。");
            }
            else
            {
                MyDataClass.GetA_M_D_command("update tb_Login set Name='" + text_Name.Text + "',Pass='" + text_Pass.Text + "' where ID='" + ModuleClass.MyModule.User_ID + "'");
                if (ModuleClass.MyModule.User_ID == DataClass.MyMeans.LogIn_ID)
                    DataClass.MyMeans.LogIn_Name = text_Name.Text;
                MessageBox.Show("修改成功。");
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
