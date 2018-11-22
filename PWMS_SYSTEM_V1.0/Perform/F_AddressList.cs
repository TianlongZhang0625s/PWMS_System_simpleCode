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
    public partial class F_AddressList : Form
    {
        public F_AddressList()
        {
            InitializeComponent();
        }
        //实例化MyMeans类对象
        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        //实例化MyModule类对象
        ModuleClass.MyModule MyMC = new ModuleClass.MyModule();
        //声明一个DataSET数据容器,用来通过SqlDataReader类查询出来的数据
        public static DataSet MyDS_Grid;
        //查询得到tb_AddressBook表中所有项,实现对信息的添加等操作
        public static string AllSql = "Select ID,Name as 姓名, Sex as 性别 , Phone as电话,WordPhone as 工作电话,Handset as 手机, QQ as QQ号,E_Mail as 邮箱地址 from tb_AddressBook";
        //Find_Field-->存放查询字段
        public static string Find_Field = "";

        public void ShowAll()
        {
            ModuleClass.MyModule.Address_ID = "";
            //用dataGridView控件显式职工的名称
            MyDS_Grid = MyDataClass.GetDataSet(AllSql, "tb_AddressBook");
            //设置控件的数据来源，datasoursce
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            //设置第一列不可见
            dataGridView1.Columns[0].Visible = false;

            //根据查询结果实现对Button控制
            if (dataGridView1.RowCount > 1)
            {
                Address_Amend.Enabled = true;
                Address_Delete.Enabled = false;

            }
            else
            {
                Address_Amend.Enabled = false;
                Address_Delete.Enabled = false;

            }
        }

        private void F_AddressList_Load(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void Address_Add_Click(object sender, EventArgs e)
        {
            //此时需要调用另一个窗体
            InfoAddForm.F_Address FrmAddress = new InfoAddForm.F_Address();
            FrmAddress.Text = "通讯录添加操作";
            FrmAddress.Tag = 1;
            FrmAddress.ShowDialog(this);
            ShowAll();

            
        }

        private void Address_Amend_Click(object sender, EventArgs e)
        {
            //调用F_Address窗体添加通讯录信息
            InfoAddForm.F_Address FrmAddress = new InfoAddForm.F_Address();
            FrmAddress.Text = "通讯录修改操作";
            FrmAddress.Tag = 2;
            FrmAddress.ShowDialog(this);
            ShowAll();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                ModuleClass.MyModule.Address_ID = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                Address_Amend.Enabled = true;
                Address_Delete.Enabled = true;
            }
            else
            {
                Address_Amend.Enabled = false;
                Address_Delete.Enabled = false;
            }
        }

        private void Address_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                MyDataClass.GetA_M_D_command("Delete tb_AddressBook where ID='" + ModuleClass.MyModule.Address_ID + "'");
                ShowAll();
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //设置查询类型条件的ComBox控件来实现按照条件来进行查询
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    {
                        Find_Field = "Name";
                        break;
                    }
                case 1:
                    {
                        Find_Field = "Sex";
                        break;
 
                    }
                case 2:
                    {
                        Find_Field = "E_Mail";
                        break;
                    }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //判断查询条件是否为空
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入查询条件。");
                return;
            }
            ModuleClass.MyModule.Address_ID = "";
            //使用datagridview来显示职工的名称
            MyDS_Grid = MyDataClass.GetDataSet(AllSql + " where " + Find_Field + " like '%" + textBox1.Text.Trim() + "%'", "tb_AddressBook");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            dataGridView1.Columns[0].Visible = false;
            //当查询结果存在
            if (dataGridView1.RowCount > 1)
            {
                Address_Amend.Enabled = true;
                Address_Delete.Enabled = true;
            }
            else
            {
                Address_Amend.Enabled = false;
                Address_Delete.Enabled = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void Address_Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
