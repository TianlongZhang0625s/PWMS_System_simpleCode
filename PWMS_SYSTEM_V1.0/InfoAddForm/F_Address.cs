using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PWMS_SYSTEM_V1._0.InfoAddForm
{
    public partial class F_Address : Form
    {

        //定义全局变量,并实例化MyMeans，MyModule
        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        ModuleClass.MyModule MyModule = new ModuleClass.MyModule();
        public static DataSet MyDS_Grid;
        public static string Address_ID = "";


        public F_Address()
        {
            InitializeComponent();
        }

        private void F_Address_Load(object sender, EventArgs e)
        {
            if ((int)(this.Tag) == 1)
            {
                //自动编号
                Address_ID = MyModule.GetAutoCoding("tb_AddressBook", "ID");

            }
            if ((int)this.Tag == 2)
            {
                //获取tb_AddressBook表中所有的内容
                MyDS_Grid = MyDataClass.GetDataSet("select ID,Name,Sex,Phone,Handset,WordPhone,QQ,E_Mail from tb_AddressBook where ID='" + ModuleClass.MyModule.Address_ID + "'", "tb_AddressBook");
                //设置控件和表中相关的内容进行绑定
                Address_ID = MyDS_Grid.Tables[0].Rows[0][0].ToString();
                this.Address_1.Text = MyDS_Grid.Tables[0].Rows[0][1].ToString();
                this.Address_2.Text = MyDS_Grid.Tables[0].Rows[0][2].ToString();
                this.Address_3.Text = MyDS_Grid.Tables[0].Rows[0][3].ToString();
                this.Address_4.Text = MyDS_Grid.Tables[0].Rows[0][4].ToString();
                this.Address_5.Text = MyDS_Grid.Tables[0].Rows[0][5].ToString();
                this.Address_6.Text = MyDS_Grid.Tables[0].Rows[0][6].ToString();
                this.Address_7.Text = MyDS_Grid.Tables[0].Rows[0][7].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Address_1.Text != "")
            {
                MyModule.Part_SaveClass("ID,Name,Sex,Phone,Handset,WordPhone,QQ,E_Mail", Address_ID, "", this.groupBox1.Controls, "Address_", "tb_AddressBook", 8, (int)this.Tag);
                MyDataClass.GetA_M_D_command(ModuleClass.MyModule.ADDs);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
