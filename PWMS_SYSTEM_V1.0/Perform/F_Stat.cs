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
    public partial class F_Stat : Form
    {
        public F_Stat()
        {
            InitializeComponent();
        }
        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();

        public static string Term_Field = "Folk,Age,Kultur,Marriage,Sex,Visage,WorkLength,Employee,Business,Laborage,Branch,Duthcall,School,Speciality,Pact_Y,BeAware,City";
        public static string Term_Value = "民族类别,年龄,文化程度,婚姻,性别,政治面貌,工龄,职工类别,职务类别,工资类别,部门类别,职称类别,毕业学校,主修专业,合同年限,籍贯所在省,籍贯所在市";
        public static string[] A_Field = Term_Field.Split(Convert.ToChar(','));
        public static string[] A_Value = Term_Value.Split(Convert.ToChar(','));
        public static DataSet MyDS_Grid;

        public void Stat_Class(int n)
        {
            MyDS_Grid = MyDataClass.GetDataSet("select " + A_Field[n] + " as '" + A_Value[n] + "', count(" + A_Field[n] + ")  as '人数' from tb_stuffbusic group by " + A_Field[n], "tb_Stuffbusic");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 55;
        }

        private void F_Stat_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < A_Value.Length; i++)
                listBox1.Items.Add("按" + A_Value[i] + "统计");
            Stat_Class(0);

        }


        private void listBox1_Click_1(object sender, EventArgs e)
        {
            Stat_Class(listBox1.SelectedIndex);
        }
    }
}
