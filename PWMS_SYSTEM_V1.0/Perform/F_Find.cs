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
    public partial class F_Find : Form
    {
        public F_Find()
        {
            InitializeComponent();
        }

        ModuleClass.MyModule MyMC = new ModuleClass.MyModule();
        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        public static DataSet MyDS_Grid;
        //逻辑操作符标志 AND 和 OR ，初始化为 AND
        public string ARsign = " AND ";
        //设置查询条件
        public static string Sut_SQL = "select ID as 编号,StuffName as 职工姓名,Folk as 民族类别,Birthday as 出生日期,Age as 年龄,Kultur as 文化程度,Marriage as 婚姻,Sex as 性别,Visage as 政治面貌,IDCard as 身份证号,Workdate as 单位工作时间,WorkLength as 工龄,Employee as 职工类别,Business as 职务类别,Laborage as 工资类别,Branch as 部门类别,Duthcall as 职称类别,Phone as 电话,Handset as 手机,School as 毕业学校,Speciality as 主修专业,GraduateDate as 毕业时间,M_Pay as 月工资,Bank as 银行帐号,Pact_B as 合同开始时间,Pact_E as 合同结束时间,Pact_Y as 合同年限,BeAware as 籍贯所在省,City as 籍贯所在市 from tb_Stuffbusic";


        //清空GrupBox上的控件的信息
        private void Clear_Box(int n, Control.ControlCollection GBox, string TName)
        {
            for (int i = 0; i < n; i++)
            {
                foreach (Control C in GBox)
                {
                    if(C.GetType().Name== "TextBox" | C.GetType().Name == "MaskedTextBox" | C.GetType().Name == "ComboBox")
                        if (C.Name.IndexOf(TName) > -1)
                        {
                            C.Text = "";

                        }
                }
            }
        }
        //窗体加载
        private void F_Find_Load(object sender, EventArgs e)
        {
            MyMC.CoPassData(Find_Folk, "tb_Folk");
            MyMC.CoPassData(Find_Kultur, "tb_Kultur");
            MyMC.CoPassData(Find_Visage, "tb_Visage");  //向"正治面貌”列表框中添加信息
            MyMC.CoPassData(Find_Employee, "tb_EmployeeGenre");  //向"职工类别”列表框中添加信息
            MyMC.CoPassData(Find_Business, "tb_Business");  //向"职务类别”列表框中添加信息
            MyMC.CoPassData(Find_Laborage, "tb_Laborage");  //向"工资类别”列表框中添加信息
            MyMC.CoPassData(Find_Branch, "tb_Branch");  //向"部门类别”列表框中添加信息
            MyMC.CoPassData(Find_Duthcall, "tb_Duthcall");  //向"职称类别”列表框中添加信息
            //向下拉列表中添加省名
            MyMC.CityInfo(Find_BeAware, "select distinct beaware from tb_City", 0);
            //向下拉列表中添加市名
            MyMC.CityInfo(Find_School, "select distinct School from tb_Stuffbusic", 0);
            //向下拉列表中添加主修专业
            MyMC.CityInfo(Find_Speciality, "select distinct Speciality from tb_Stuffbusic", 0);
            MyMC.MaskTextBox_Format(Find1_WorkDate);  //指定MaskedTextBox控件的格式
            MyMC.MaskTextBox_Format(Find2_WorkDate);
            
            //得到查询的结果存放在MyDS_Grid中
            MyDS_Grid = MyDataClass.GetDataSet (Sut_SQL, "tb_Stuffbusic");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
            
        }

        private void Find_BeAware_TextChanged(object sender, EventArgs e)
        {
            Find_City.Items.Clear();
            MyMC.CityInfo(Find_City, "select beaware,city from tb_City where beaware='" + Find_BeAware.Text.Trim() + "'", 1);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ARsign = " AND ";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ARsign = " OR ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModuleClass.MyModule.FindValue = "";
            string Find_SQL = Sut_SQL;
            MyMC.Find_Grids(groupBox1.Controls, "Find",ARsign);
            MyMC.Find_Grids(groupBox2.Controls, "Find", ARsign);

            //当合同的起始日期和技术日期不为空
            if (MyMC.Date_Format(Find1_WorkDate.Text) != "" && MyMC.Date_Format(Find2_WorkDate.Text) != "")
            {
                if (ModuleClass.MyModule.FindValue != "")
                    //用ARsign变量连接查询
                    ModuleClass.MyModule.FindValue = ModuleClass.MyModule.FindValue + " (" + "workdate>='" + Find1_WorkDate.Text + "' AND workdate<='" + Find2_WorkDate.Text + "')";

            }
            if (ModuleClass.MyModule.FindValue != "")   //如果FindValue字段不为空
                //将查询条件添加到SQL语句的尾部
                Find_SQL = Find_SQL + " where " + ModuleClass.MyModule.FindValue;
            //按照指定的条件进行查询
            MyDS_Grid = MyDataClass.GetDataSet (Find_SQL, "tb_Stuffbusic");
            //在dataGridView1控件是显示查询的结果
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
            checkBox1.Checked = false;

        }

        private void Find1_WorkDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void Find1_WorkDate_Leave(object sender, EventArgs e)
        {
            MyMC.Estimate_Date((MaskedTextBox)sender);
        }

        private void Find2_WorkDate_Leave(object sender, EventArgs e)
        {
            bool TDate = MyMC.Estimate_Date((MaskedTextBox)sender);
            if(TDate==true)
                if(TDate==true)
                    if (MyMC.Date_Format(Find1_WorkDate.Text) != "" && MyMC.Date_Format(Find2_WorkDate.Text) != "")
                    {
                        if(Convert.ToDateTime(Find2_WorkDate.Text)<=Convert.ToDateTime(Find1_WorkDate.Text))
                            MessageBox.Show("当前日期必须大于它前一个日期。");
                    }
        }

        private void Find2_WorkDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void Find_Age_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void Find_M_Pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, ((TextBox)sender).Text, 1);
        }

        private void Find_WorkLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void Find_Pact_Y_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            MyDS_Grid = MyDataClass.GetDataSet(Sut_SQL, "tb_Stuffbusic");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear_Box(7, groupBox1.Controls, "Find");
            Clear_Box(12, groupBox2.Controls, "Find");
            Clear_Box(4, groupBox2.Controls, "Sign");
        }




    }
}
