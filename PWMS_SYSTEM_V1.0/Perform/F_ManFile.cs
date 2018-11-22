using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Office.Interop;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace PWMS_SYSTEM_V1._0.Perform
{
    public partial class F_ManFile : Form
    {
        public F_ManFile()
        {
            InitializeComponent();
        }

        #region 当前窗体的所有共公变量
        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        ModuleClass.MyModule MyMC = new ModuleClass.MyModule();
        public static DataSet MyDS_Grid;
        public static string tem_Field = "";
        public static string tem_Value = "";
        public static string tem_ID = "";
        public static int hold_n = 0;
        public static byte[] imgBytesIn;  //用来存储图片的二进制数
        public static int Ima_n = 0;  //判断是否对图片进行了操作
        public static string Part_ID = "";  //存储数据表的ID信息
        #endregion


        //显示数据库图片
        public void ShowData_Image(byte[] DI, PictureBox Ima)
        {
            byte[] buffer = DI;
            MemoryStream ms = new MemoryStream(buffer);
            Ima.Image = Image.FromStream(ms);  
        }



        #region  显示“职工基本信息”表中的指定记录
        /// <summary>
        /// 动态读取指定的记录行，并进行显示.
        /// </summary>
        /// <param name="DGrid">DataGridView控件</param>
        /// <returns>返回string对象</returns> 
        public string Grid_Inof(DataGridView DGrid)
        {
            byte[] pic; //定义一个字节数组
            //当DataGridView控件的记录>1时，将当前行中信息显示在相应的控件上
            if (DGrid.RowCount > 1)
            {
                //向空间中指定位置添加职员的信息
                S_0.Text = DGrid[0, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_1.Text = DGrid[1, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_2.Text = Convert.ToString(DGrid[2, DGrid.CurrentCell.RowIndex].Value).Trim();
                S_3.Text = MyMC.Date_Format(Convert.ToString(DGrid[3, DGrid.CurrentCell.RowIndex].Value).Trim());
                S_4.Text = Convert.ToString(DGrid[4, DGrid.CurrentCell.RowIndex].Value).Trim();
                S_5.Text = DGrid[5, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_6.Text = DGrid[6, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_7.Text = DGrid[7, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_8.Text = DGrid[8, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_9.Text = DGrid[9, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_10.Text = MyMC.Date_Format(Convert.ToString(DGrid[10, DGrid.CurrentCell.RowIndex].Value).Trim());
                S_11.Text = Convert.ToString(DGrid[11, DGrid.CurrentCell.RowIndex].Value).Trim();
                S_12.Text = DGrid[12, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_13.Text = DGrid[13, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_14.Text = DGrid[14, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_15.Text = DGrid[15, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_16.Text = DGrid[16, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_17.Text = DGrid[17, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_18.Text = DGrid[18, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_19.Text = DGrid[19, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_20.Text = DGrid[20, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_21.Text = MyMC.Date_Format(Convert.ToString(DGrid[21, DGrid.CurrentCell.RowIndex].Value).Trim());
                S_22.Text = DGrid[22, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_23.Text = DGrid[24, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_24.Text = DGrid[25, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_25.Text = Convert.ToString(DGrid[26, DGrid.CurrentCell.RowIndex].Value).Trim();
                S_26.Text = DGrid[27, DGrid.CurrentCell.RowIndex].Value.ToString();
                S_27.Text = MyMC.Date_Format(Convert.ToString(DGrid[28, DGrid.CurrentCell.RowIndex].Value).Trim());
                S_28.Text = MyMC.Date_Format(Convert.ToString(DGrid[29, DGrid.CurrentCell.RowIndex].Value).Trim());
                S_29.Text = Convert.ToString(DGrid[30, DGrid.CurrentCell.RowIndex].Value).Trim();
                try
                {
                    //将数据库中的图片存入到字节数组中
                    pic = (byte[])(MyDS_Grid.Tables[0].Rows[DGrid.CurrentCell.RowIndex][23]);
                    MemoryStream ms = new MemoryStream(pic);			//将字节数组存入到二进制流中
                    S_Photo.Image = Image.FromStream(ms);   //二进制流Image控件中显示
                }
                catch { S_Photo.Image = null; } //当出现错误时，将Image控件清空
                tem_ID = S_0.Text.Trim();   //获取当前职伯编号
                return DGrid[1, DGrid.CurrentCell.RowIndex].Value.ToString();   //返回当前职工的姓名
            }
            else
            {
                //使用MyMeans公共类中的Clear_Control()方法清空指定控件集中的相应控件
                MyMC.Clear_Control(tabControl1.TabPages[0].Controls);
                tem_ID = "";
                return "";
            }
        }
        #endregion

        #region  按条件显示“职工基本信息”表的内容
        /// <summary>
        /// 通过公共变量动态进行查询.
        /// </summary>
        /// <param name="C_Value">条件值</param>
        public void Condition_Lookup(string C_Value)
        {
            MyDS_Grid = MyDataClass.GetDataSet("Select * from tb_Stuffbusic where " + tem_Field + "='" + tem_Value + "'", "tb_Stuffbusic");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            textBox1.Text = Grid_Inof(dataGridView1);  //显示职工信息表的当前记录            
        }
        #endregion

        #region  将图片转换成字节数组
        public void Read_Image(OpenFileDialog openF, PictureBox MyImage)  //
        {
            openF.Filter = "*.jpg|*.jpg|*.bmp|*.bmp";   //指定OpenFileDialog控件打开的文件格式
            if (openF.ShowDialog(this) == DialogResult.OK)  //如果打开了图片文件
            {
                try
                {
                    MyImage.Image = System.Drawing.Image.FromFile(openF.FileName);  //将图片文件存入到PictureBox控件中
                    string strimg = openF.FileName.ToString();  //记录图片的所在路径
                    FileStream fs = new FileStream(strimg, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
                    BinaryReader br = new BinaryReader(fs);
                    imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中
                }
                catch
                {
                    MessageBox.Show("您选择的图片不能被读取或文件类型不对！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    S_Photo.Image = null;
                }
            }
        }
        #endregion


        private void F_ManFile_Load(object sender, EventArgs e)
        {
            //用dataGridView1控件显示职工的名称
            MyDS_Grid = MyDataClass.GetDataSet(DataClass.MyMeans.AllSql, "tb_Stuffbusic");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            dataGridView1.AutoGenerateColumns = true;  //是否自动创建列
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 80;

            for (int i = 2; i < dataGridView1.ColumnCount; i++)  //隐藏dataGridView1控件中不需要的列字段
            {
                dataGridView1.Columns[i].Visible = false;
            }

            MyMC.MaskTextBox_Format(S_3);  //指定MaskedTextBox控件的格式
            MyMC.MaskTextBox_Format(S_10);
            MyMC.MaskTextBox_Format(S_21);
            MyMC.MaskTextBox_Format(S_27);
            MyMC.MaskTextBox_Format(S_28);

            MyMC.CoPassData(S_2, "tb_Folk");  //向“民族类别”列表框中添加信息
            MyMC.CoPassData(S_5, "tb_Kultur");  //向"文化程度”列表框中添加信息
            MyMC.CoPassData(S_8, "tb_Visage");  //向"正治面貌”列表框中添加信息
            MyMC.CoPassData(S_12, "tb_EmployeeGenre");  //向"职工类别”列表框中添加信息
            MyMC.CoPassData(S_13, "tb_Business");  //向"职务类别”列表框中添加信息
            MyMC.CoPassData(S_14, "tb_Laborage");  //向"工资类别”列表框中添加信息
            MyMC.CoPassData(S_15, "tb_Branch");  //向"部门类别”列表框中添加信息
            MyMC.CoPassData(S_16, "tb_Duthcall");  //向"职称类别”列表框中添加信息
            MyMC.CityInfo(S_23, "select distinct beaware from tb_City", 0);

            S_23.AutoCompleteMode = AutoCompleteMode.SuggestAppend;  //使S_BeAware控件具有查询功能
            S_23.AutoCompleteSource = AutoCompleteSource.ListItems;

            textBox1.Text = Grid_Inof(dataGridView1);  //显示职工信息表的首记录
            DataClass.MyMeans.AllSql = "Select * from tb_Stuffbusic";

        }

        private void Sut_Add_Click(object sender, EventArgs e)
        {
            MyMC.Clear_Control(tabControl1.TabPages[0].Controls);   //清空职工基本信息的相应文本框
            S_0.Text = MyMC.GetAutoCoding("tb_Stuffbusic", "ID");  //自动添加编号
            hold_n = 1;  //用于记录添加操作的标识
            MyMC.Ena_Button(Sut_Add, Sut_Amend, Sut_Cancel, Sut_Save, 0, 0, 1, 1);
            groupBox5.Text = "当前正在添加信息";
            Img_Clear.Enabled = true;   //使图片选择按钮为可用状态
            Img_Save.Enabled = true;
        }

        private void S_BeAware_TextChanged(object sender, EventArgs e)
        {
            S_24.Items.Clear();
            MyMC.CityInfo(S_24, "select beaware,city from tb_City where beaware='" + S_23.Text.Trim() + "'", 1);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            groupBox5.Enabled = true;
            Sut_Delete.Enabled = true;
            MyMC.Ena_Button(Sut_Add, Sut_Amend, Sut_Cancel, Sut_Save, 1, 1, 0, 0);
            if (tabControl1.SelectedTab.Name == "tabPage1") //如果选择的是“职工基本信息”选项卡
            {
                hold_n = 0;  //恢复原始标识
                MyMC.Ena_Button(Sut_Add, Sut_Amend, Sut_Cancel, Sut_Save, 1, 1, 0, 0);  //
                groupBox5.Text = "";
                Ima_n = 0;//标识是否选择了职工照片
                Img_Clear.Enabled = false;  //使按钮为不可用状态
                Img_Save.Enabled = false;
                Sub_Table.Enabled = true;
            }
            //如果选择的是“工作简历”、“家庭关系”、“培训记录”和“奖惩记录”选项卡
            if (tabControl1.SelectedTab.Name == "tabPage2" | tabControl1.SelectedTab.Name == "tabPage3" | tabControl1.SelectedTab.Name == "tabPage4" | tabControl1.SelectedTab.Name == "tabPage5")
            {
                groupBox5.Enabled = false;  //使窗体中的操作按钮为不可用状态
                Sub_Table.Enabled = false;
                if (tabControl1.SelectedTab.Name == "tabPage2") //“工作简历”选项卡
                {
                    groupBox6.Parent = (TabPage)tabPage2;
                    MyMC.MaskTextBox_Format(Word_2);  //指定MaskedTextBox控件的格式
                    MyMC.MaskTextBox_Format(Word_3);
                }
                if (tabControl1.SelectedTab.Name == "tabPage3") //“家庭关系”选项卡
                {
                    groupBox6.Parent = (TabPage)tabPage3;
                    MyMC.MaskTextBox_Format(Famity_4);

                }
                if (tabControl1.SelectedTab.Name == "tabPage4") //“培训记录”选项卡
                {
                    groupBox6.Parent = (TabPage)tabPage4;
                    MyMC.MaskTextBox_Format(TrainNote_3);
                    MyMC.MaskTextBox_Format(TrainNote_4);
                }
                if (tabControl1.SelectedTab.Name == "tabPage5") //“奖惩记录”选项卡
                {
                    groupBox6.Parent = (TabPage)tabPage5;
                    MyMC.MaskTextBox_Format(RANDP_3);
                    MyMC.MaskTextBox_Format(RANDP_5);
                    MyMC.CoPassData(RANDP_2, "tb_RPKind");  //向“奖惩类别”列表框中添加信息
                }
                MyMC.Ena_Button(Part_Add, Part_Amend, Part_Cancel, Part_Save, 1, 1, 0, 0);
            }
            if (tabControl1.SelectedTab.Name == "tabPage6") //“个人简历”选项卡
            {
                MyMC.Ena_Button(Sut_Add, Sut_Amend, Sut_Cancel, Sut_Delete, 0, 0, 0, 0);    //使窗体中的操作按钮为不可用
                Sut_Save.Enabled = true;    //将窗体中的“保存”按钮设为可用状态
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)  //向comboBox2控件中添加相应的查询条件
            {
                case 0:
                    {
                        MyMC.CityInfo(comboBox2, "select distinct StuffName from tb_Stuffbusic", 0);  //职工姓名
                        tem_Field = "StuffName";
                        break;
                    }
                case 1:  //性别
                    {
                        comboBox2.Items.Clear();
                        comboBox2.Items.Add("男");
                        comboBox2.Items.Add("女");
                        tem_Field = "Sex";
                        break;

                    }
                case 2:
                    {
                        MyMC.CoPassData(comboBox2, "tb_Folk");  //民族类别
                        tem_Field = "Folk";
                        break;
                    }
                case 3:
                    {
                        MyMC.CoPassData(comboBox2, "tb_Kultur");  //文化程度
                        tem_Field = "Kultur";
                        break;
                    }
                case 4:
                    {
                        MyMC.CoPassData(comboBox2, "tb_Visage");  //正治面貌
                        tem_Field = "Visage";
                        break;
                    }
                case 5:
                    {
                        MyMC.CoPassData(comboBox2, "tb_EmployeeGenre");  //职工类别
                        tem_Field = "Employee";
                        break;
                    }
                case 6:
                    {
                        MyMC.CoPassData(comboBox2, "tb_Business");  //职务类别
                        tem_Field = "Business";
                        break;
                    }
                case 7:
                    {
                        MyMC.CoPassData(comboBox2, "tb_Branch");  //部门类别
                        tem_Field = "Branch";
                        break;
                    }
                case 8:
                    {
                        MyMC.CoPassData(comboBox2, "tb_Duthcall");  //职称类别
                        tem_Field = "Duthcall";
                        break;
                    }
                case 9:
                    {
                        MyMC.CoPassData(comboBox2, "tb_Laborage");  //工资类别
                        tem_Field = "Laborage";
                        break;
                    }
            }

        }

        private void N_First_Click(object sender, EventArgs e)
        {
            try
            {
                int ColInd = 0;
                if (dataGridView1.CurrentCell.ColumnIndex == -1 || dataGridView1.CurrentCell.ColumnIndex > 1)
                    ColInd = 0;
                else
                    ColInd = dataGridView1.CurrentCell.ColumnIndex;
                if ((((Button)sender).Name) == "N_First")
                {
                    dataGridView1.CurrentCell = this.dataGridView1[ColInd, 0];
                    MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 0, 0, 1, 1);
                }
                if ((((Button)sender).Name) == "N_Previous")
                {
                    if (dataGridView1.CurrentCell.RowIndex == 0)
                    {
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 0, 0, 1, 1);
                    }
                    else
                    {
                        dataGridView1.CurrentCell = this.dataGridView1[ColInd, dataGridView1.CurrentCell.RowIndex - 1];
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 1, 1);
                    }
                }
                if ((((Button)sender).Name) == "N_Next")
                {
                    if (dataGridView1.CurrentCell.RowIndex == dataGridView1.RowCount - 2)
                    {
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 0, 0);
                    }
                    else
                    {
                        dataGridView1.CurrentCell = this.dataGridView1[ColInd, dataGridView1.CurrentCell.RowIndex + 1];
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 1, 1);
                    }
                }
                if ((((Button)sender).Name) == "N_Cauda")
                {
                    dataGridView1.CurrentCell = this.dataGridView1[ColInd, dataGridView1.RowCount - 2];
                    MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 0, 0);
                }
            }
            catch { }

        }

        private void N_Previous_Click(object sender, EventArgs e)
        {
            N_First_Click(sender, e);
        }

        private void N_Cauda_Click(object sender, EventArgs e)
        {
            N_First_Click(sender, e);
        }
        private void N_Next_Click(object sender, EventArgs e)
        {
            N_First_Click(sender, e);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.RowIndex > -1)
                {
                    textBox1.Text = Grid_Inof(dataGridView1);  //显示职工信息表的当前记录
                    MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 1, 1);  //使窗体中的编辑按钮可用
                    //获取工作简历表中的信息
                    DataSet WDset = MyDataClass.GetDataSet("select Sut_ID,ID,BeginDate as 开始时间,EndDate as 结束时间, Branch as 部门, Business as 职务, WordUnit as 工作单位 from tb_WordResume where Sut_ID='" + tem_ID + "'", "tb_WordResume");
                    MyMC.Correlation_Table(WDset, dataGridView2);   //将WDset存储的信息显示在dataGridView2控件中
                    if (WDset.Tables[0].Rows.Count < 1) //当WDset中没有信息时
                        //清空相应的控件
                        MyMC.Clear_Grids(WDset.Tables[0].Columns.Count, groupBox7.Controls, "Word_");
                    //获取家庭关系表中的信息
                    DataSet FDset = MyDataClass.GetDataSet("select Sut_ID,ID,LeaguerName as 家庭成员名称,Nexus as 与本人的关系, BirthDate as 出生日期, WordUnit as 工作单位, Business as 职务, Visage as 政治面貌, Phone as 电话 from tb_Family where Sut_ID='" + tem_ID + "'", "tb_Family");
                    MyMC.Correlation_Table(FDset, dataGridView3);
                    if (FDset.Tables[0].Rows.Count < 1)
                        MyMC.Clear_Grids(FDset.Tables[0].Columns.Count, groupBox10.Controls, "Famity_");
                    //获取工作简历表中的信息
                    DataSet TDset = MyDataClass.GetDataSet("select Sut_ID,ID,TrainFashion as 培训方式,BeginDate as 培训开始时间, EndDate as 培训结束时间, Speciality as 培训专业, TrainUnit as 培训单位, KulturMemo as 培训内容, Charge as 费用, Effect as 效果 from tb_TrainNote where Sut_ID='" + tem_ID + "'", "tb_TrainNote");
                    MyMC.Correlation_Table(TDset, dataGridView4);
                    if (TDset.Tables[0].Rows.Count < 1)
                        MyMC.Clear_Grids(TDset.Tables[0].Columns.Count, groupBox12.Controls, "TrainNote_");
                    //获取奖惩记录表中的信息
                    DataSet RDset = MyDataClass.GetDataSet("select Sut_ID,ID,RPKind as 奖惩种类,RPDate as 奖惩时间, SealMan as 批准人, QuashDate as 撤消时间, QuashWhys as 撤消原因 from tb_RANDP where Sut_ID='" + tem_ID + "'", "tb_RANDP");
                    MyMC.Correlation_Table(RDset, dataGridView5);
                    if (RDset.Tables[0].Rows.Count < 1)
                        MyMC.Clear_Grids(RDset.Tables[0].Columns.Count, groupBox14.Controls, "RANDP_");
                    //获取个人简历表中的信息
                    SqlDataReader Read_Memo = MyDataClass.getCommand("Select * from tb_Individual where ID='" + tem_ID + "'");
                    if (Read_Memo.Read())
                        Ind_Mome.Text = Read_Memo[1].ToString();
                    else
                        Ind_Mome.Clear();

                    //MyMC.Show_DGrid(dataGridView2, groupBox7.Controls, "Word_");
                }
            }
            catch { }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tem_Value = comboBox2.SelectedItem.ToString();
                Condition_Lookup(tem_Value);
            }
            catch
            {
                comboBox2.Text = "";
                MessageBox.Show("只能以选择方式查询。");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tem_Field = "";
            MyDS_Grid = MyDataClass.GetDataSet(DataClass.MyMeans.AllSql, "tb_Stuffbusic");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            textBox1.Text = Grid_Inof(dataGridView1);  //显示职工信息表的当前记录
        }

        private void Sut_Amend_Click(object sender, EventArgs e)
        {
            hold_n = 2;  //用于记录修改操作的标识
            MyMC.Ena_Button(Sut_Add, Sut_Amend, Sut_Cancel, Sut_Save, 0, 0, 1, 1);
            groupBox5.Text = "当前正在修改信息";
            Img_Clear.Enabled = true;   //使图片选择按钮为可用状态
            Img_Save.Enabled = true;
        }

        private void Sut_Cancel_Click(object sender, EventArgs e)
        {
            hold_n = 0;  //恢复原始标识
            MyMC.Ena_Button(Sut_Add, Sut_Amend, Sut_Cancel, Sut_Save, 1, 1, 0, 0);
            groupBox5.Text = "";
            Ima_n = 0;
            if (tem_Field == "")
                button1_Click(sender, e);
            else
                Condition_Lookup(tem_Value);
            Img_Clear.Enabled = false;
            Img_Save.Enabled = false;
        }

        private void Sut_Save_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage6") //如果当前是“个人简历”选项卡
            {
                //通过MyMeans公共类中的getcom()方法查询当前职工是否添加了个人简历
                SqlDataReader Read_Memo = MyDataClass.getCommand("Select * from tb_Individual where ID='" + tem_ID + "'");
                if (Read_Memo.Read())   //如果有记录
                    //将当前设置的个人简历进行修改
                    MyDataClass.GetA_M_D_command("update tb_Individual set Memo='" + Ind_Mome.Text + "' where ID='" + tem_ID + "'");
                else
                    //如果没有记录，则进行添加操作
                    MyDataClass.GetA_M_D_command("insert into tb_Individual (ID,Memo) values('" + tem_ID + "','" + Ind_Mome.Text + "')");
            }
            else //如果当前是“职工基本信息”选项卡
            {
                //定义字符串变量，并存储将“职工基本信息表”中的所有字段
                string All_Field = "ID,StuffName,Folk,Birthday,Age,Kultur,Marriage,Sex,Visage,IDCard,Workdate,WorkLength,Employee,Business,Laborage,Branch,Duthcall,Phone,Handset,School,Speciality,GraduateDate,Address,BeAware,City,M_Pay,Bank,Pact_B,Pact_E,Pact_Y";

                if (hold_n == 1 || hold_n == 2) //判断当前是添加，还是修改操作
                {
                    ModuleClass.MyModule.ADDs = ""; //清空MyModule公共类中的ADDs变量
                    //用MyModule公共类中的Part_SaveClass()方法组合添加或修改的SQL语句
                    MyMC.Part_SaveClass(All_Field, S_0.Text.Trim(), "", tabControl1.TabPages[0].Controls, "S_", "tb_Stuffbusic", 30, hold_n);
                    //如果ADDs变量不为空，则通过MyMeans公共类中的getsqlcom()方法执行添加、修改操作
                    if (ModuleClass.MyModule.ADDs != "")
                        MyDataClass.GetA_M_D_command(ModuleClass.MyModule.ADDs);
                }
                if (Ima_n > 0)  //如果图片标识大于0
                {
                    //通过MyModule公共类中r的SaveImage()方法将图片存入数据库中
                    MyMC.SaveImage(S_0.Text.Trim(), imgBytesIn);
                }
                Sut_Cancel_Click(sender, e);    //调用“取消”按钮的单击事件
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Read_Image(openFileDialog1, S_Photo);
            Ima_n = 1;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            S_Photo.Image = null;
            imgBytesIn = new byte[65536];
            Ima_n = 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hold_n = 1;
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                MyMC.Clear_Control(this.groupBox7.Controls);
                Part_ID = MyMC.GetAutoCoding("tb_WordResume", "ID");  //自动添加编号;
            }
            if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                MyMC.Clear_Control(this.groupBox10.Controls);
                Part_ID = MyMC.GetAutoCoding("tb_Family", "ID");  //自动添加编号;
            }
            if (tabControl1.SelectedTab.Name == "tabPage4")
            {
                MyMC.Clear_Control(this.groupBox12.Controls);
                Part_ID = MyMC.GetAutoCoding("tb_TrainNote", "ID");  //自动添加编号;
            }
            if (tabControl1.SelectedTab.Name == "tabPage5")
            {
                MyMC.Clear_Control(this.groupBox14.Controls);
                Part_ID = MyMC.GetAutoCoding("tb_RANDP", "ID");  //自动添加编号;
            }
            MyMC.Ena_Button(Part_Add, Part_Amend, Part_Cancel, Part_Save, 1, 0, 1, 1);
        }

        private void Part_Save_Click(object sender, EventArgs e)
        {
            string s = "";
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                s = "ID,Sut_ID,BeginDate,EndDate,Branch,Business,WordUnit";
                //"select Sut_ID,ID,BeginDate as 开始时间,EndDate as 结束时间, Branch as 部门, Business as 职务, WordUnit as 工作单位 from tb_WordResume
                ModuleClass.MyModule.ADDs = "";
                if (hold_n == 2)
                {
                    if (dataGridView2.RowCount < 2)
                    {
                        MessageBox.Show("数据表为空，不可以修改");
                    }
                    else
                        Part_ID = dataGridView2[1, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                }
                MyMC.Part_SaveClass(s, tem_ID, Part_ID, this.groupBox7.Controls, "Word_", "tb_WordResume", 7, hold_n);
            }
            if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                s = "ID,Sut_ID,LeaguerName,Nexus,BirthDate,WordUnit,Business,Visage,Phone";
                ModuleClass.MyModule.ADDs = "";
                if (hold_n == 2)
                {
                    if (dataGridView3.RowCount < 2)
                    {
                        MessageBox.Show("数据表为空，不可以修改");
                    }
                    else
                        Part_ID = dataGridView3[1, dataGridView3.CurrentCell.RowIndex].Value.ToString();
                }
                MyMC.Part_SaveClass(s, tem_ID, Part_ID, this.groupBox10.Controls, "Famity_", "tb_Family", 9, hold_n);
            }
            if (tabControl1.SelectedTab.Name == "tabPage4")
            {
                s = "ID,Sut_ID,TrainFashion,BeginDate,EndDate,Speciality,TrainUnit,KulturMemo,Charge,Effect";
                ModuleClass.MyModule.ADDs = "";
                if (hold_n == 2)
                {
                    if (dataGridView4.RowCount < 2)
                    {
                        MessageBox.Show("数据表为空，不可以修改");
                    }
                    else
                        Part_ID = dataGridView4[1, dataGridView4.CurrentCell.RowIndex].Value.ToString();
                }
                MyMC.Part_SaveClass(s, tem_ID, Part_ID, this.groupBox12.Controls, "TrainNote_", "tb_TrainNote", 10, hold_n);
            }
            if (tabControl1.SelectedTab.Name == "tabPage5")
            {
                s = "ID,Sut_ID,RPKind,RPDate,SealMan,QuashDate,QuashWhys";
                ModuleClass.MyModule.ADDs = "";
                if (hold_n == 2)
                {
                    if (dataGridView5.RowCount < 2)
                    {
                        MessageBox.Show("数据表为空，不可以修改");
                    }
                    else
                        Part_ID = dataGridView5[1, dataGridView5.CurrentCell.RowIndex].Value.ToString();
                }
                MyMC.Part_SaveClass(s, tem_ID, Part_ID, this.groupBox14.Controls, "RANDP_", "tb_RANDP", 7, hold_n);
            }
            if (ModuleClass.MyModule.ADDs != "")
                MyDataClass.GetA_M_D_command(ModuleClass.MyModule.ADDs);
            Part_Cancel_Click(sender, e);
        }

        private void Part_Amend_Click(object sender, EventArgs e)
        {
            hold_n = 2;
            MyMC.Ena_Button(Part_Add, Part_Amend, Part_Cancel, Part_Save, 0, 1, 1, 1);
        }

        private void Part_Cancel_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                DataSet WDset = MyDataClass.GetDataSet("select Sut_ID,ID,BeginDate as 开始时间,EndDate as 结束时间, Branch as 部门, Business as 职务, WordUnit as 工作单位 from tb_WordResume where Sut_ID='" + tem_ID + "'", "tb_WordResume");
                MyMC.Correlation_Table(WDset, dataGridView2);
            }
            if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                DataSet FDset = MyDataClass.GetDataSet("select Sut_ID,ID,LeaguerName as 家庭成员名称,Nexus as 与本人的关系, BirthDate as 出生日期, WordUnit as 工作单位, Business as 职务, Visage as 政治面貌, Phone as 电话 from tb_Family where Sut_ID='" + tem_ID + "'", "tb_Family");
                MyMC.Correlation_Table(FDset, dataGridView3);
            }
            if (tabControl1.SelectedTab.Name == "tabPage4")
            {
                DataSet TDset = MyDataClass.GetDataSet("select Sut_ID,ID,TrainFashion as 培训方式,BeginDate as 培训开始时间, EndDate as 培训结束时间, Speciality as 培训专业, TrainUnit as 培训单位, KulturMemo as 培训内容, Charge as 费用, Effect as 效果 from tb_TrainNote where Sut_ID='" + tem_ID + "'", "tb_TrainNote");
                MyMC.Correlation_Table(TDset, dataGridView4);
            }
            if (tabControl1.SelectedTab.Name == "tabPage5")
            {
                DataSet RDset = MyDataClass.GetDataSet("select Sut_ID,ID,RPKind as 奖惩种类,RPDate as 奖惩时间, SealMan as 批准人, QuashDate as 撤消时间, QuashWhys as 撤消原因 from tb_RANDP where Sut_ID='" + tem_ID + "'", "tb_RANDP");
                MyMC.Correlation_Table(RDset, dataGridView5);
            }
            hold_n = 0;  //恢复原始标识
            MyMC.Ena_Button(Part_Add, Part_Amend, Part_Cancel, Part_Save, 1, 1, 0, 0);
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            MyMC.Show_DGrid(dataGridView2, groupBox7.Controls, "Word_");
        }
       
        private void S_Pact_B_Leave(object sender, EventArgs e)
        {
            MyMC.Estimate_Date((MaskedTextBox)sender);
        }

        private void S_Pact_B_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void S_Pact_E_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

       

        private void S_Pact_E_Leave(object sender, EventArgs e)
        {
            bool TDate = MyMC.Estimate_Date((MaskedTextBox)sender);
            if (TDate == true)
                if (MyMC.Date_Format(S_27.Text) != "" && MyMC.Date_Format(S_28.Text) != "")
                {
                    if (Convert.ToDateTime(S_28.Text) <= Convert.ToDateTime(S_27.Text))
                        MessageBox.Show("当前日期必须大于它前一个日期。");
                }
        }

        private void S_GraduateDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void S_GraduateDate_Leave(object sender, EventArgs e)
        {
            MyMC.Estimate_Date((MaskedTextBox)sender);
        }

        private void S_Workdate_Leave(object sender, EventArgs e)
        {
            MyMC.Estimate_Date((MaskedTextBox)sender);
        }

        private void S_Workdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void S_M_Pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, ((TextBox)sender).Text, 1);
        }

        private void S_Pact_Y_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void S_Age_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void S_WorkLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, "", 0);
        }

        private void Part_Delete_Click(object sender, EventArgs e)
        {
            string Delete_Table = "";
            string Delete_ID = "";
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                if (dataGridView2.RowCount < 2)
                {
                    MessageBox.Show("数据表为空，不可以删除。");
                    return;
                }
                MyMC.Clear_Control(this.groupBox7.Controls);
                Delete_ID = dataGridView2[1, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                Delete_Table = "tb_WordResume";
            }
            if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                if (dataGridView3.RowCount < 2)
                {
                    MessageBox.Show("数据表为空，不可以删除。");
                    return;
                }
                MyMC.Clear_Control(this.groupBox10.Controls);
                Delete_ID = dataGridView3[1, dataGridView3.CurrentCell.RowIndex].Value.ToString();
                Delete_Table = "tb_Family";
            }
            if (tabControl1.SelectedTab.Name == "tabPage4")
            {
                if (dataGridView4.RowCount < 2)
                {
                    MessageBox.Show("数据表为空，不可以删除。");
                    return;
                }
                MyMC.Clear_Control(this.groupBox12.Controls);
                Delete_ID = dataGridView4[1, dataGridView4.CurrentCell.RowIndex].Value.ToString();
                Delete_Table = "tb_TrainNote";
            }
            if (tabControl1.SelectedTab.Name == "tabPage5")
            {
                if (dataGridView5.RowCount < 2)
                {
                    MessageBox.Show("数据表为空，不可以删除。");
                    return;
                }
                MyMC.Clear_Control(this.groupBox14.Controls);
                Delete_ID = dataGridView5[1, dataGridView5.CurrentCell.RowIndex].Value.ToString();
                Delete_Table = "tb_RANDP";
            }
            if ((Delete_ID.Trim()).Length > 0)
            {
                MyDataClass.GetA_M_D_command("Delete " + Delete_Table + " where ID='" + Delete_ID + "'");
                Part_Cancel_Click(sender, e);
            }
        }

        private void Sut_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 2) //判断dataGridView1控件中是否有记录
            {
                MessageBox.Show("数据表为空，不可以删除。");
                return;
            }
            //删除职工信息表中的当前记录，及其他相关表中的信息
            MyDataClass.GetA_M_D_command("Delete tb_Stuffbusic where ID='" + S_0.Text.Trim() + "'");
            MyDataClass.GetA_M_D_command("Delete tb_WordResume where Sut_ID='" + S_0.Text.Trim() + "'");
            MyDataClass.GetA_M_D_command("Delete tb_Family where Sut_ID='" + S_0.Text.Trim() + "'");
            MyDataClass.GetA_M_D_command("Delete tb_TrainNote where Sut_ID='" + S_0.Text.Trim() + "'");
            MyDataClass.GetA_M_D_command("Delete tb_RANDP where Sut_ID='" + S_0.Text.Trim() + "'");
            MyDataClass.GetA_M_D_command("Delete tb_WordResume where Sut_ID='" + S_0.Text.Trim() + "'");
            MyDataClass.GetA_M_D_command("Delete tb_Individual where ID='" + S_0.Text.Trim() + "'");
            Sut_Cancel_Click(sender, e);    //调用“取消”按钮的单击事件
        }


        //word编程部分

        public void but_Table_Click(object sender, EventArgs e)
        {
            object Nothing = System.Reflection.Missing.Value;
            object missing = System.Reflection.Missing.Value;
            //创建Word文档
            Word.Application wordApp = new Word.ApplicationClass();
            Word.Document wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            wordApp.Visible = true;

            //设置文档宽度
            wordApp.Selection.PageSetup.LeftMargin = wordApp.CentimetersToPoints(float.Parse("2"));
            wordApp.ActiveWindow.ActivePane.HorizontalPercentScrolled = 11;
            wordApp.Selection.PageSetup.RightMargin = wordApp.CentimetersToPoints(float.Parse("2"));

            Object strat = Type.Missing;
            object end = Type.Missing;

            PictureBox pp = new PictureBox();
            int p1 = 0;

            for (int i = 0; i < MyDS_Grid.Tables[0].Rows.Count; i++)
            {
                try
                {
                    byte[] pic = (byte[])(MyDS_Grid.Tables[0].Rows[i][23]);
                    MemoryStream ms = new MemoryStream();
                    pp.Image = Image.FromStream(ms);
                    pp.Image.Save(@"C:\22.bmp");

                }
                catch
                {
                    p1 = 1;
                }

                object rng = Type.Missing;
                string strInfo = "职工基本信息表" + "(" + MyDS_Grid.Tables[0].Rows[i][1].ToString() + ")";
                strat = 0;
                end = 0;
                wordDoc.Range(ref strat, ref end).InsertBefore(strInfo);
                wordDoc.Range(ref strat, ref end).Font.Name = "Verdana";
                wordDoc.Range(ref strat, ref end).ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                strat = strInfo.Length;
                end = strInfo.Length;
                wordDoc.Range(ref strat, ref end).InsertParagraphAfter();

                object missingValue = Type.Missing;
                object location = strInfo.Length;
                Word.Range rng2 = wordDoc.Range(ref location, ref location);


                wordDoc.Tables.Add(rng2, 14, 6, ref missingValue, ref missingValue);

                wordDoc.Tables[1].Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightAtLeast;
                wordDoc.Tables[1].Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));
                wordDoc.Tables[1].Range.Font.Size = 10;
                wordDoc.Tables[1].Range.Font.Name = "宋体";
                

                //设置表格样式
                wordDoc.Tables[1].Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                wordDoc.Tables[1].Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth050pt;
                wordDoc.Tables[1].Borders.InsideColor = Word.WdColor.wdColorAutomatic;

                wordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;//设置右对齐

                //第5行显示
                wordDoc.Tables[1].Cell(1, 5).Merge(wordDoc.Tables[1].Cell(5, 6));
                //第6行显示
                wordDoc.Tables[1].Cell(6, 5).Merge(wordDoc.Tables[1].Cell(6, 6));
                //第9行显示
                wordDoc.Tables[1].Cell(9, 4).Merge(wordDoc.Tables[1].Cell(9, 6));
                //第12行显示
                wordDoc.Tables[1].Cell(12, 2).Merge(wordDoc.Tables[1].Cell(12, 6));
                //第13行显示
                wordDoc.Tables[1].Cell(13, 2).Merge(wordDoc.Tables[1].Cell(13, 6));
                //第14行显示
                wordDoc.Tables[1].Cell(14, 2).Merge(wordDoc.Tables[1].Cell(14, 6));

                //第1行赋值
                wordDoc.Tables[1].Cell(1, 1).Range.Text = "职工编号：";
                wordDoc.Tables[1].Cell(1, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][0].ToString();
                wordDoc.Tables[1].Cell(1, 3).Range.Text = "职工姓名：";
                wordDoc.Tables[1].Cell(1, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][1].ToString();

                //第1行赋值
                wordDoc.Tables[1].Cell(1, 1).Range.Text = "职工编号：";
                wordDoc.Tables[1].Cell(1, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][0].ToString();
                wordDoc.Tables[1].Cell(1, 3).Range.Text = "职工姓名：";
                wordDoc.Tables[1].Cell(1, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][1].ToString();

                //插入图片

                if (p1 == 0)
                {
                    string FileName = @"C:\22.bmp";//图片所在路径
                    object LinkToFile = false;
                    object SaveWithDocument = true;
                    object Anchor = wordDoc.Tables[1].Cell(1, 5).Range;    //指定图片插入的区域
                    //将图片插入到单元格中
                    wordDoc.Tables[1].Cell(1, 5).Range.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
                }
                p1 = 0;

                //第2行赋值
                wordDoc.Tables[1].Cell(2, 1).Range.Text = "民族类别：";
                wordDoc.Tables[1].Cell(2, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][2].ToString();
                wordDoc.Tables[1].Cell(2, 3).Range.Text = "出生日期：";
                try
                {
                    wordDoc.Tables[1].Cell(2, 4).Range.Text = Convert.ToString(Convert.ToDateTime(MyDS_Grid.Tables[0].Rows[i][3]).ToShortDateString());
                }
                catch { wordDoc.Tables[1].Cell(2, 4).Range.Text = ""; }
                //Convert.ToString(MyDS_Grid.Tables[0].Rows[i][3]);
                //第3行赋值
                wordDoc.Tables[1].Cell(3, 1).Range.Text = "年龄：";
                wordDoc.Tables[1].Cell(3, 2).Range.Text = Convert.ToString(MyDS_Grid.Tables[0].Rows[i][4]);
                wordDoc.Tables[1].Cell(3, 3).Range.Text = "文化程序：";
                wordDoc.Tables[1].Cell(3, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][5].ToString();
                //第4行赋值
                wordDoc.Tables[1].Cell(4, 1).Range.Text = "婚姻：";
                wordDoc.Tables[1].Cell(4, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][6].ToString();
                wordDoc.Tables[1].Cell(4, 3).Range.Text = "性别：";
                wordDoc.Tables[1].Cell(4, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][7].ToString();
                //第5行赋值
                wordDoc.Tables[1].Cell(5, 1).Range.Text = "政治面貌：";
                wordDoc.Tables[1].Cell(5, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][8].ToString();
                wordDoc.Tables[1].Cell(5, 3).Range.Text = "单位工作时间：";
                try
                {
                    wordDoc.Tables[1].Cell(5, 4).Range.Text = Convert.ToString(Convert.ToDateTime(MyDS_Grid.Tables[0].Rows[0][10]).ToShortDateString());
                }
                catch { wordDoc.Tables[1].Cell(5, 4).Range.Text = ""; }
                //第6行赋值
                wordDoc.Tables[1].Cell(6, 1).Range.Text = "籍贯：";
                wordDoc.Tables[1].Cell(6, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][24].ToString();
                wordDoc.Tables[1].Cell(6, 3).Range.Text = MyDS_Grid.Tables[0].Rows[i][25].ToString();
                wordDoc.Tables[1].Cell(6, 4).Range.Text = "身份证：";
                wordDoc.Tables[1].Cell(6, 5).Range.Text = MyDS_Grid.Tables[0].Rows[i][9].ToString();
                //第7行赋值
                wordDoc.Tables[1].Cell(7, 1).Range.Text = "工龄：";
                wordDoc.Tables[1].Cell(7, 2).Range.Text = Convert.ToString(MyDS_Grid.Tables[0].Rows[i][11]);
                wordDoc.Tables[1].Cell(7, 3).Range.Text = "职工类别：";
                wordDoc.Tables[1].Cell(7, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][12].ToString();
                wordDoc.Tables[1].Cell(7, 5).Range.Text = "职务类别：";
                wordDoc.Tables[1].Cell(7, 6).Range.Text = MyDS_Grid.Tables[0].Rows[i][13].ToString();
                //第8行赋值
                wordDoc.Tables[1].Cell(8, 1).Range.Text = "工资类别：";
                wordDoc.Tables[1].Cell(8, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][14].ToString();
                wordDoc.Tables[1].Cell(8, 3).Range.Text = "部门类别：";
                wordDoc.Tables[1].Cell(8, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][15].ToString();
                wordDoc.Tables[1].Cell(8, 5).Range.Text = "职称类别：";
                wordDoc.Tables[1].Cell(8, 6).Range.Text = MyDS_Grid.Tables[0].Rows[i][16].ToString();
                //第9行赋值
                wordDoc.Tables[1].Cell(9, 1).Range.Text = "月工资：";
                wordDoc.Tables[1].Cell(9, 2).Range.Text = Convert.ToString(MyDS_Grid.Tables[0].Rows[i][26]);
                wordDoc.Tables[1].Cell(9, 3).Range.Text = "银行帐号：";
                wordDoc.Tables[1].Cell(9, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][27].ToString();
                //第10行赋值
                wordDoc.Tables[1].Cell(10, 1).Range.Text = "合同起始日期：";
                try
                {
                    wordDoc.Tables[1].Cell(10, 2).Range.Text = Convert.ToString(Convert.ToDateTime(MyDS_Grid.Tables[0].Rows[i][28]).ToShortDateString());
                }
                catch { wordDoc.Tables[1].Cell(10, 2).Range.Text = ""; }
                //Convert.ToString(MyDS_Grid.Tables[0].Rows[i][28]);
                wordDoc.Tables[1].Cell(10, 3).Range.Text = "合同结束日期：";
                try
                {
                    wordDoc.Tables[1].Cell(10, 4).Range.Text = Convert.ToString(Convert.ToDateTime(MyDS_Grid.Tables[0].Rows[i][29]).ToShortDateString());
                }
                catch { wordDoc.Tables[1].Cell(10, 4).Range.Text = ""; }
                //Convert.ToString(MyDS_Grid.Tables[0].Rows[i][29]);
                wordDoc.Tables[1].Cell(10, 5).Range.Text = "合同年限：";
                wordDoc.Tables[1].Cell(10, 6).Range.Text = Convert.ToString(MyDS_Grid.Tables[0].Rows[i][30]);
                //第11行赋值
                wordDoc.Tables[1].Cell(11, 1).Range.Text = "电话：";
                wordDoc.Tables[1].Cell(11, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][17].ToString();
                wordDoc.Tables[1].Cell(11, 3).Range.Text = "手机：";
                wordDoc.Tables[1].Cell(11, 4).Range.Text = MyDS_Grid.Tables[0].Rows[i][18].ToString();
                wordDoc.Tables[1].Cell(11, 5).Range.Text = "毕业时间：";
                try
                {
                    wordDoc.Tables[1].Cell(11, 6).Range.Text = Convert.ToString(Convert.ToDateTime(MyDS_Grid.Tables[0].Rows[i][21]).ToShortDateString());
                }
                catch { wordDoc.Tables[1].Cell(11, 6).Range.Text = ""; }
                //Convert.ToString(MyDS_Grid.Tables[0].Rows[i][21]);
                //第12行赋值
                wordDoc.Tables[1].Cell(12, 1).Range.Text = "毕业学校：";
                wordDoc.Tables[1].Cell(12, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][19].ToString();
                //第13行赋值
                wordDoc.Tables[1].Cell(13, 1).Range.Text = "主修专业：";
                wordDoc.Tables[1].Cell(13, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][20].ToString();
                //第14行赋值
                wordDoc.Tables[1].Cell(14, 1).Range.Text = "家庭地址：";
                wordDoc.Tables[1].Cell(14, 2).Range.Text = MyDS_Grid.Tables[0].Rows[i][22].ToString();

                wordDoc.Range(ref strat, ref end).InsertParagraphAfter();//插入回车
                wordDoc.Range(ref strat, ref end).ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter; //设置字体局中


            }


        }




        private void dataGridView3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            MyMC.Show_DGrid(dataGridView3, groupBox10.Controls, "Famity_");
        }

        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            MyMC.Show_DGrid(dataGridView4, groupBox12.Controls, "TrainNote_");
        }

        private void dataGridView5_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            MyMC.Show_DGrid(dataGridView5, groupBox14.Controls, "RANDP_");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //用dataGridView1控件显示职工的名称
            MyDS_Grid = MyDataClass.GetDataSet(DataClass.MyMeans.AllSql, "tb_Stuffbusic");
            dataGridView1.DataSource = MyDS_Grid.Tables[0];
            dataGridView1.AutoGenerateColumns = true;  //是否自动创建列
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 80;

            for (int i = 2; i < dataGridView1.ColumnCount; i++)  //隐藏dataGridView1控件中不需要的列字段
            {
                dataGridView1.Columns[i].Visible = false;
            }
        }

        private void TrainNote_8_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyMC.Estimate_Key(e, ((TextBox)sender).Text, 1);
        }

    }
}
