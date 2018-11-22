using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PWMS_SYSTEM_V1._0.ModuleClass
{
    class MyModule
    {
        DataClass.MyMeans MyDataClass = new DataClass.MyMeans();
        public static string ADDs = "";
        public static string FindValue = "";
        public static string Address_ID = "";
        public static string User_ID = "";
        public static string User_Name = "";

        //创建show——form方法来调用相应的子窗体
        public void Show_Form(string FrmName, int n)
        {
            if (n == 1)
            {
                if (FrmName == "人事档案浏览")
                {
                    Perform.F_ManFile FrmManFile = new Perform.F_ManFile();
                    FrmManFile.Text = "人事档案浏览";
                    FrmManFile.ShowDialog();
                    FrmManFile.Dispose();

                }
                if (FrmName == "人事资料查询")
                {
                    Perform.F_Find FrmFind = new Perform.F_Find();
                    FrmFind.Text = "人事资料查询";
                    FrmFind.ShowDialog();
                    FrmFind.Dispose();
                }
                if (FrmName == "人事资料统计")
                {
                    Perform.F_Stat FrmStat = new Perform.F_Stat();
                    FrmStat.Text = "人事资料统计";
                    FrmStat.ShowDialog();
                    FrmStat.Dispose();
                }
                if (FrmName == "员工生日提示")
                {
                    InfoAddForm.F_ClewSet FrmClewSet = new InfoAddForm.F_ClewSet();
                    FrmClewSet.Text = "员工生日提示";
                    FrmClewSet.Tag = 1;
                    FrmClewSet.ShowDialog();
                    FrmClewSet.Dispose();
                }
                if (FrmName == "员工合同提示")
                {
                    InfoAddForm.F_ClewSet FrmClewSet = new InfoAddForm.F_ClewSet();
                    FrmClewSet.Text = "员工合同提示";
                    FrmClewSet.Tag = 2;
                    FrmClewSet.ShowDialog();
                    FrmClewSet.Dispose();
                }
                if (FrmName == "日常记事")
                {
                    Perform.F_WordPad FrmWordPad = new Perform.F_WordPad();
                    FrmWordPad.Text = "日常记事";
                    FrmWordPad.ShowDialog();
                    FrmWordPad.Dispose();
                }
                if (FrmName == "通讯录")
                {
                    Perform.F_AddressList FrmAddressList = new Perform.F_AddressList();
                    FrmAddressList.Text = "通讯录";
                    FrmAddressList.ShowDialog();
                    FrmAddressList.Dispose();
                }
                if(FrmName=="备份/还原数据库")
                {
                    Perform.F_HaveBack FrmHaveBack = new Perform.F_HaveBack();
                    FrmHaveBack.Text = "备份/还原数据库";
                    FrmHaveBack.ShowDialog();
                    FrmHaveBack.Dispose();
                }
                if (FrmName == "清空数据库")
                {
                    Perform.F_ClearData FrmClearData = new Perform.F_ClearData();
                    FrmClearData.Text = "清空数据库";
                    FrmClearData.ShowDialog();
                    FrmClearData.Dispose();

                }
                if (FrmName == "重新登录")
                {
                    F_LogIn FrmLogIn = new F_LogIn();
                    FrmLogIn.Tag = 2;
                    FrmLogIn.ShowDialog();
                    FrmLogIn.Dispose();
                }
                if (FrmName == "用户设置")
                {
                    Perform.F_User FrmUser = new Perform.F_User();
                    FrmUser.Text = "用户设置";
                    FrmUser.ShowDialog();
                    FrmUser.Dispose();
                }
                if (FrmName == "计算器")
                {
                    System.Diagnostics.Process.Start("calc.exe");

                }
                if (FrmName == "记事本")
                {
                    System.Diagnostics.Process.Start("notepad.exe");

                }
            }
            if (n == 2)
            {
                string FrmStr = "";//记录窗体名称
                if (FrmName == "民族类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_Folk";
                    DataClass.MyMeans.Mean_Table = "tb_Folk";
                    DataClass.MyMeans.Mean_Field = "FolkName";
                    FrmStr = FrmName;

                }
                if (FrmName == "职工类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_EmployeeGenre";
                    DataClass.MyMeans.Mean_Table = "tb_EmployeeGenre";
                    DataClass.MyMeans.Mean_Field = "EmployeeName";
                    FrmStr = FrmName;
 
                }
                if (FrmName == "文化程度设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_Kultur";
                    DataClass.MyMeans.Mean_Table = "tb_Kultur";
                    DataClass.MyMeans.Mean_Field = "KulturName";
                    FrmStr = FrmName;
                }
                if (FrmName == "政治面貌设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_Visage";
                    DataClass.MyMeans.Mean_Table = "tb_Visage";
                    DataClass.MyMeans.Mean_Field = "VisageName";
                    FrmStr = FrmName;
                }
                if (FrmName == "部门类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_Branch";
                    DataClass.MyMeans.Mean_Table = "tb_Branch";
                    DataClass.MyMeans.Mean_Field = "BranchName";
                    FrmStr = FrmName;
                }
                if (FrmName == "工资类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_Laborage";
                    DataClass.MyMeans.Mean_Table = "tb_Laborage";
                    DataClass.MyMeans.Mean_Field = "LaborageName";
                    FrmStr = FrmName;
                }
                if (FrmName == "职务类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_Business";
                    DataClass.MyMeans.Mean_Table = "tb_Business";
                    DataClass.MyMeans.Mean_Field = "BusinessName";
                    FrmStr = FrmName;
                }
                if (FrmName == "职称类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_Duthcall";
                    DataClass.MyMeans.Mean_Table = "tb_Duthcall";
                    DataClass.MyMeans.Mean_Field = "DuthcallName";
                    FrmStr = FrmName;
                }
                if (FrmName == "奖惩类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_RPKind";
                    DataClass.MyMeans.Mean_Table = "tb_RPKind";
                    DataClass.MyMeans.Mean_Field = "RPKind";
                    FrmStr = FrmName;
                }
                if (FrmName == "记事本类别设置")
                {
                    DataClass.MyMeans.Mean_SQL = "select * from tb_WordPad";
                    DataClass.MyMeans.Mean_Table = "tb_WordPad";
                    DataClass.MyMeans.Mean_Field = "WordPad";
                    FrmStr = FrmName;
                }
                InfoAddForm.F_Basic FrmBasic = new InfoAddForm.F_Basic();
                FrmBasic.Text = FrmStr;
                FrmBasic.ShowDialog();
                FrmBasic.Dispose();
            }
        }

        //GetMenu方法是将MenuStrip菜单中的菜单项动态的添加到TreeView控件相应的节点中
        public void GetMenu(TreeView treeV, MenuStrip MenuS)
        {
            for (int i = 0; i < MenuS.Items.Count; i++)
            {
                //将一级菜单中的项目名称添加到treeView中
                TreeNode newNode1 = treeV.Nodes.Add(MenuS.Items[i].Text);
                //将当前菜单项的所有相关信息存入到ToolStripDropDownItem中
                ToolStripDropDownItem newmenu = (ToolStripDropDownItem)MenuS.Items[i];
                //判断是否有二级菜单
                if (newmenu.HasDropDownItems && newmenu.DropDownItems.Count > 0)
                    for (int j = 0; j < newmenu.DropDownItems.Count; j++)
                    {
                        //将二级菜单添加到treeView组件的子节点上
                        TreeNode newnode2 = newNode1.Nodes.Add(newmenu.DropDownItems[j].Text);
                        //将当前所有菜单项的信息存入到tollstripdropdownitems中
                        ToolStripDropDownItem newmenu2 = (ToolStripDropDownItem)newmenu.DropDownItems[j];

                        //判断二级菜单中是否有三级菜单
                        if (newmenu2.HasDropDownItems && newmenu2.DropDownItems.Count > 0)
                            for (int p = 0; p < newmenu2.DropDownItems.Count; p++)
                                //将三级菜单中的名称添加到子节点NODE2中
                                newnode2.Nodes.Add(newmenu2.DropDownItems[p].Text);

                    }
            }
                    
        }

        //清空可视化控件中的文本信息以及图片
        public void Clear_Control(Control.ControlCollection Con)
        {
            //使用遍历方法将所有所有可视化控件清空
            foreach (Control C in Con)
            {
                if (C.GetType().Name == "TextBox")
                    if (((TextBox)C).Visible == true)
                        ((TextBox)C).Clear();
                if (C.GetType().Name == "MaskedTextBox")
                    if (((MaskedTextBox)C).Visible == true)
                        ((MaskedTextBox)C).Clear();
                if (C.GetType().Name == "ComboBox")
                    if (((ComboBox)C).Visible == true)
                        ((ComboBox)C).Text = "";
                if (C.GetType().Name == "PictureBox")
                    if (((PictureBox)C).Visible == true)
                        ((PictureBox)C).Image = null;


            }
        }

        //gwtautocoding 添加数据时自动获取数据的编号
        public String GetAutoCoding(string TableName, string ID)
        {
            SqlDataReader MyDataReader = MyDataClass.getCommand("select max(" + ID + ") NID from " + TableName);
            int Num = 0;
            if (MyDataReader.HasRows)
            {
                MyDataReader.Read();
                if (MyDataReader[0].ToString() == "")
                    return "0001";
                Num = Convert.ToInt32(MyDataReader[0].ToString());
                ++Num;
                string s = string.Format("{0:0000}", Num);
                return s;
            }
            else
            {
                return "0001";
            }
        }

        //TreeMenuF是单击TreeView控件的节点时被调用的，通过节点的文本名称遍历查找
        //控件找到则通过show——form方法来打开窗体
        public void TreeMenuF(MenuStrip MenuS, TreeNodeMouseClickEventArgs e)
        {
            string Men = "";
            for (int i = 0; i < MenuS.Items.Count; i++)
            {
                Men = ((ToolStripDropDownItem)MenuS.Items[i]).Name;
                if (Men.IndexOf("Menu") == -1)
                {
                    //当节点名称和菜单名称相等时
                    if (((ToolStripDropDownItem)MenuS.Items[i]).Text == e.Node.Text)
                        if (((ToolStripDropDownItem)MenuS.Items[i]).Enabled == false)
                        {
                            MessageBox.Show("当前用户无权调用" + "\"" + e.Node.Text + "\"" + "窗体");
                            break;
                        }
                        else
                            Show_Form(((ToolStripDropDownItem)MenuS.Items[i]).Text.Trim(), 1);

                }
                ToolStripDropDownItem newmenu = (ToolStripDropDownItem)MenuS.Items[i];

                //进行判断和遍历二级菜单
                if(newmenu.HasDropDownItems&&newmenu.DropDownItems.Count>0)
                    for (int j = 0; j < newmenu.DropDownItems.Count; j++)
                    {
                        Men = newmenu.DropDownItems[j].Name;
                        if (Men.IndexOf("Menu") == -1)
                        {
                            if ((newmenu.DropDownItems[j]).Text == e.Node.Text)
                                if (newmenu.DropDownItems[j].Enabled == false)
                                {
                                    MessageBox.Show("当前用户无权调用" + "\"" + e.Node.Text + "\"" + "窗体");
                                    break;

                                }
                                else
                                    Show_Form((newmenu.DropDownItems[j]).Text.Trim(), 1);
                        }
                        //遍历三级下单
                        ToolStripDropDownItem newmenu2 = (ToolStripDropDownItem)newmenu.DropDownItems[j];
                        if(newmenu2.HasDropDownItems&&newmenu2.DropDownItems.Count>0)
                            for (int p = 0; p < newmenu2.DropDownItems.Count; p++)
                            {
                                if (newmenu2.DropDownItems[p].Text == e.Node.Text)
                                    if ((newmenu2.DropDownItems[p]).Enabled == false)
                                    {
                                        MessageBox.Show("当前用户无权调用" + "\"" + e.Node.Text + "\"" + "窗体");
                                        break;
                                    }
                                    else
                                        if (newmenu2.DropDownItems[p].Text.Trim() == "员工生日提示" || newmenu2.DropDownItems[p].Text.Trim() == "员工合同提示")
                                            Show_Form(newmenu2.DropDownItems[p].Text.Trim(), 1);
                                        else
                                            Show_Form(newmenu2.DropDownItems[p].Text.Trim(), 2);
                            }
                    }
            }

        }

        //AccessNodeRecursive
        private void AccessNodeRecursive(TreeNode treeNode, List<TreeNode> nodes)
        {
            //处理每一个节点
            if (treeNode.Checked)
                nodes.Add(treeNode);
            //对每一个子节点进行递归
            foreach (TreeNode tn in treeNode.Nodes)
            {
                AccessNodeRecursive(tn, nodes);
            }
        }

        //Show_Pope主要通过当前用户的登陆姓名在权限列表中查询此用户的所有权限
        //并根据权限设置菜单的可用状态
        public void Show_Pope(Control.ControlCollection GBox, string TID)
        {
            string sID = "";
            string CheckName = "";
            bool t = false;
            DataSet dataSet = MyDataClass.GetDataSet("select ID,PopeName,Pope from tb_UserPope where ID='" + TID + "'", "tb_UserPope");
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                sID = Convert.ToString(dataSet.Tables[0].Rows[i][1]);
                if ((int)(dataSet.Tables[0].Rows[i][2]) == 1)
                    t = true;
                else
                    t = false;
                foreach (Control C in GBox)
                {
                    if (C.GetType().Name == "CheckBox")
                    {
                        CheckName = C.Name;
                        if (CheckName.IndexOf(sID) > -1)
                        {
                            ((CheckBox)C).Checked = t;
                        }
 
                    }
                        

                }
            }
        }

        //设置主窗体菜单不可用
        public void MainMenuF(MenuStrip MenuS)
        {
            string Men = "";
            for (int i = 0; i < MenuS.Items.Count; i++)
            {
                Men = ((ToolStripDropDownItem)MenuS.Items[i]).Name;
                if (Men.IndexOf("Menu") == -1)
                    ((ToolStripDropDownItem)MenuS.Items[i]).Enabled = false;
                ToolStripDropDownItem newmenu = (ToolStripDropDownItem)MenuS.Items[i];
                if(newmenu.HasDropDownItems&&newmenu.DropDownItems.Count>0)
                    for (int j = 0; j < newmenu.DropDownItems.Count; j++)
                    {
                        Men = newmenu.DropDownItems[j].Name;
                        if (Men.IndexOf("Menu") == -1)
                            newmenu.DropDownItems[j].Enabled = false;
                        ToolStripDropDownItem newmenu2 = (ToolStripDropDownItem)newmenu.DropDownItems[j];
                        if (newmenu2.HasDropDownItems && newmenu2.DropDownItems.Count > 0)
                            for (int p = 0; p < newmenu2.DropDownItems.Count; p++)
                                newmenu2.DropDownItems[p].Enabled = false;
                    }
            }
        }

        //根据用户权限设置主窗体菜单
        public void MainPope(MenuStrip MenuS, string UName)
        {
            string Str = "";
            string MenuName = "";
            DataSet Dset = MyDataClass.GetDataSet("select ID from tb_LogIn where Name='" + UName + "'", "tb_Login");
            string UID = Convert.ToString(Dset.Tables[0].Rows[0][0]);
            Dset = MyDataClass.GetDataSet("select ID,PopeName,Pope from tb_UserPope where ID='" + UID + "'", "tb_UserPope");
            bool bo = false;
            for (int k = 0; k < Dset.Tables[0].Rows.Count; k++)
            {
                Str = Convert.ToString(Dset.Tables[0].Rows[k][1]);
                if (Convert.ToInt32(Dset.Tables[0].Rows[k][2]) == 1)
                    bo = true;
                else
                    bo = false;
                for (int i = 0; i < MenuS.Items.Count; i++)
                {
                    ToolStripDropDownItem newmenu = (ToolStripDropDownItem)MenuS.Items[i];
                    if (newmenu.HasDropDownItems && newmenu.DropDownItems.Count > 0)
                    for (int j = 0; j < newmenu.DropDownItems.Count; j++)
                    {
                        MenuName = newmenu.DropDownItems[j].Name;
                        if (MenuName.IndexOf(Str) > -1)
                            newmenu.DropDownItems[j].Enabled = bo;
                        ToolStripDropDownItem newmenu2 = (ToolStripDropDownItem)newmenu.DropDownItems[j];
                        if(newmenu2.HasDropDownItems&&newmenu2.DropDownItems.Count>0)
                            for (int p = 0; p < newmenu2.DropDownItems.Count; p++)
                            {
                                MenuName = newmenu2.DropDownItems[p].Name;
                                if (MenuName.IndexOf(Str) > -1)
                                    newmenu2.DropDownItems[p].Enabled = bo;
                            }
                    }
 
                }
            }
        }

        //查询指定范围内生日与合同到期的职工
        public void PactDay(int i)
        {
            DataSet DSet = MyDataClass.GetDataSet("select * from tb_Clew where kind=" + i + " and unlock=1", "tb_clew");
            if (DSet.Tables[0].Rows.Count > 0)
            {
                string Vfield = "";
                string dSQL = "";
                int sday = Convert.ToInt32(DSet.Tables[0].Rows[0][1]);
                if (i == 1)
                {
                    Vfield = "Birthday";
                    dSQL = "select * from tb_Stuffbusic where (datediff(day,getdate(),convert(Nvarchar(12),cast(cast(year(getdate()) as char(4))+'-'+cast(month(" + Vfield + ") as char(2))+'-'+cast(day(" + Vfield + ")as char(2)) as datetime),110))<=" + sday + ") and (datediff(day,getdate(),convert(Nvarchar(12),cast(cast(year(getdate()) as char(4))+'-'+cast(month(" + Vfield + ") as char(2))+'-'+cast(day(" + Vfield + ")as char(2)) as datetime),110))>=0)";
                }
                else
                {
                    Vfield = "Pact_E";
                    dSQL = "select * from tb_Stuffbusic where ((getdate()-convert(Nvarchar(12)," + Vfield + ",110))>=-" + sday + " and (getdate()-convert(Nvarchar(12)," + Vfield + ",110))<=0)";

                }
                DSet = MyDataClass.GetDataSet(dSQL, "tb_Stuffbusic");
                if (DSet.Tables[0].Rows.Count > 0)
                {
                    if (i == 1)
                        Vfield = "是否查看" + sday.ToString() + "天内过生日的职工信息？";
                    else
                        Vfield = "是否查看" + sday.ToString() + "天内合同到期的职工信息？";
                    if (MessageBox.Show(Vfield, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        DataClass.MyMeans.AllSql = dSQL;
                        Show_Form("人事档案浏览", 1);
                    }
                }
            }
        }

        //动态的向combox控件中传递数据表中的数据
        public void CoPassData(ComboBox cobox, string TableName)
        {
            cobox.Items.Clear();
            DataClass.MyMeans MyDataClssa = new DataClass.MyMeans();
            SqlDataReader MyDataReader = MyDataClssa.getCommand("select * from " + TableName);
            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    if (MyDataReader[1].ToString() != "" && MyDataReader[1].ToString() != null)
                        cobox.Items.Add(MyDataReader[1].ToString());

 
                }
            }
        }

        //向combox控件中传递个省市的名称
        public void CityInfo(ComboBox cobox, string SQLstr, int n)
        {
            cobox.Items.Clear();
            DataClass.MyMeans MyDataClssa = new DataClass.MyMeans();
            SqlDataReader MyDR = MyDataClssa.getCommand(SQLstr);
            if (MyDR.HasRows)
            {
                while (MyDR.Read())
                {
                    if (MyDR[n].ToString() != "" && MyDR[n].ToString() != null)
                        cobox.Items.Add(MyDR[n].ToString());
 
                }
            }
        }

        //将日期转换到指定的格式
        public string Date_Format(string NDate)
        {
            string sm, sd;
            int y, m, d;
            try
            {
                y = Convert.ToDateTime(NDate).Year;
                m = Convert.ToDateTime(NDate).Month;
                d = Convert.ToDateTime(NDate).Day;

            }
            catch
            {
                return "";

            }
            if (y == 1900)
                return "";
            if (m < 10)
            {
                sm = "0" + Convert.ToString(m);

            }
            else
                sm = Convert.ToString(m);
            if (d < 10)
            {
                sd = "0" + Convert.ToString(d);
            }
            else
                sd = Convert.ToString(d);

            return Convert.ToString(y) + "-" + sm + "-" + sd;

        }

        //将时间转换到指定格式
        public string Time_Format(string nDate)
        {
            string sh, sm, se;
            int hh, mm, ss;
            try
            {
                hh = Convert.ToDateTime(nDate).Hour;
                mm = Convert.ToDateTime(nDate).Minute;
                ss = Convert.ToDateTime(nDate).Second;
            }
            catch
            {
                return "";
 
            }
            sh = Convert.ToString(hh);
            if (sh.Length < 2)
                sh = "0" + sh;
            sm = Convert.ToString(mm);
            if (sm.Length < 2)
                sm = "0" + sm;
            se = Convert.ToString(ss);
            if (se.Length < 2)
                se = "0" + se;
            return sh + sm + se;

        }

        //设置MaskTextBox控件的格式，按照格式来显示时间
        public void MaskTextBox_Format(MaskedTextBox MTBox)
        {
            MTBox.Mask = "0000-00-00";
            MTBox.ValidatingType = typeof(System.DateTime);
        }

        //用按钮控制数据记录移动时，改变按钮的可用状态
        public void Ena_Button(Button B1, Button B2, Button B3, Button B4, int n1, int n2, int n3, int n4)
        {
            B1.Enabled = Convert.ToBoolean(n1);
            B2.Enabled = Convert.ToBoolean(n2);
            B3.Enabled = Convert.ToBoolean(n3);
            B4.Enabled = Convert.ToBoolean(n4);
        }

        //保存添加或修改信息
        public void Part_SaveClass(string Sarr, string ID1, string ID2, Control.ControlCollection Contr, string BoxName, string TableName, int n, int m)
        {
            string tem_Field = "", tem_Value = "";
            int p = 2;
            if (m == 1)
            {
                //当m=1时代表添加信息
                if (ID1 != "" && ID2 != "")
                {
                    tem_Field = "ID";
                    tem_Value = "'" + ID1 + "'";
                    p = 1;
                }
                else
                {
                    tem_Field = "Sut_id,ID";
                    tem_Value = "'" + ID1 + "','" + ID2 + "'";

                }
            }
            else
            {
                if (m == 2)
                {
                    if (ID1 != "" && ID2 != "")
                        tem_Value = "ID='" + ID1 + "',ID='" + ID2 + "'";
                    p = 1;

                }
                else
                    tem_Value = "Sut_ID='" + ID1 + "',ID='" + ID2 + "'";        
 
            }

            if (m > 0)
            {
                //生成部份添加、修改语句
                string[] Parr = Sarr.Split(Convert.ToChar(','));
                for (int i = p; i < n; i++)
                {
                    string sID = BoxName + i.ToString();
                    foreach (Control C in Contr)
                    {
                        if(C.GetType().Name=="TextBox"|C.GetType().Name=="MaskedTextBox"|C.GetType().Name == "ComboBox")
                            if (C.Name == sID)
                            {
                                string Ctext = C.Text;
                                if (C.GetType().Name == "MaskedTextBox")
                                    Ctext = Date_Format(C.Text);
                                if (m == 1)
                                {
                                    tem_Field = tem_Field + "," + Parr[i];
                                    if (Ctext == "")
                                        tem_Value = tem_Value + "," + "NULL";
                                    else
                                        tem_Value = tem_Value + "," + "'" + Ctext + "'";

                                }
                                if (m == 2)
                                {
                                    if (Ctext == "")
                                        tem_Value = tem_Value + "," + Parr[i] + "=NULL";
                                    else
                                        tem_Value = tem_Value + "," + Parr[i] + "='" + Ctext + "'";

                                }
                            }

                    }
                }
                ADDs = "";
                if (m == 1)
                    ADDs = "insert into " + TableName + "(" + tem_Field + ") values(" + tem_Value + ")";
                if (m == 2)
                    if (ID2 == "")
                        ADDs = "update " + TableName + " set " + tem_Value + " where ID='" + ID1 + "'";
                    else
                        ADDs = "update " + TableName + " set " + tem_Value + " where ID='" + ID2 + "'";

            
            }
           
 
        }

        //将当前表的数据信息显示在指定的控件上
        public void Show_DGrid(DataGridView DGrid, Control.ControlCollection GBox, string TName)
        {
            string sID = "";
            if (DGrid.RowCount > 0)
            {
                for (int i = 2; i < DGrid.ColumnCount; i++)
                {
                    sID = TName + i.ToString();
                    foreach (Control C in GBox)
                    {
                        if(C.GetType().Name == "TextBox" | C.GetType().Name == "MaskedTextBox" | C.GetType().Name == "ComboBox")
                            if (C.Name == sID)
                            {
                                if (C.GetType().Name != "MaskedTextBox")
                                    C.Text = DGrid[i, DGrid.CurrentCell.RowIndex].Value.ToString();
                                else
                                    C.Text = Date_Format(Convert.ToString(DGrid[i, DGrid.CurrentCell.RowIndex].Value).Trim());

                            }
 
                    }
                }
            }
 
        }

        //清空控件上的控件信息
        public void Clear_Grids(int n, Control.ControlCollection GBox, string TName)
        {
            string sID = "";
            for (int i = 2; i < n; i++)
            {
                sID = TName + i.ToString();
                foreach (Control C in GBox)
                {
                    if(C.GetType().Name=="TextBox"|C.GetType().Name=="MaskedTextBox"|C.GetType().Name == "ComboBox")
                        if (C.Name == sID)
                        {
                            C.Text = "";
                        }

 
                }
            }
        }

        //控制数据表的显示字段
        public void Correlation_Table(DataSet DSet, DataGridView DGrid)
        {
            DGrid.DataSource = DSet.Tables[0];
            DGrid.Columns[0].Visible = false;
            DGrid.Columns[1].Visible = false;
            DGrid.RowHeadersVisible = false;
            DGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //组合查询条件
        public void Find_Grids(Control.ControlCollection GBox, string TName, string ANDSign)
        {
            string sID = "";
            if (FindValue.Length > 0)
                FindValue = FindValue + ANDSign;
            foreach (Control C in GBox)
            {
                if(C.GetType().Name=="TextBox" | C.GetType().Name == "ComboBox")
                {
                    if (C.GetType().Name == "ComboBox" && C.Text != "")
                    {
                        sID = C.Name;
                        if (sID.IndexOf(TName) > -1)
                        {
                            string[] Astr = sID.Split(Convert.ToChar('_'));
                            FindValue = FindValue + "(" + Astr[1] + "='" + C.Text + "')" + ANDSign;

                        }
                    }
                    if (C.GetType().Name == "TextBox" && C.Text != "")
                    {
                        sID = C.Text;
                        if (sID.IndexOf(TName) > -1)
                        {
                            string[] Astr = sID.Split(Convert.ToChar('_'));
                            string m_Sign = "";
                            string mID = "";
                            if (Astr.Length > 2)
                                mID = Astr[1] + "_" + Astr[2];
                            else
                                mID = Astr[1];
                            foreach (Control C1 in GBox)
                            {
                                if (C1.GetType().Name == "ComboBox")
                                    if ((C1.Name).IndexOf(mID) > -1)
                                    {
                                        if (C1.Text == "")
                                            break;
                                        else
                                        {
                                            m_Sign = C1.Text;
                                            break;
                                        }


                                    }

                            }
                            if (m_Sign != "")
                                FindValue = FindValue + "(" + mID + m_Sign + C.Text + ")" + ANDSign;


                        }
                    }
                }
            }

            if (FindValue.Length > 0)
            {
                if (FindValue.IndexOf("AND") > -1)
                    FindValue = FindValue.Substring(0, FindValue.Length - 4);
                if (FindValue.IndexOf("OR") > -1)
                    FindValue = FindValue.Substring(0, FindValue.Length - 3);

            }
            else
                FindValue = "";

 
        }

        //判断字符型日期是否正确
        public bool Estimate_Date(MaskedTextBox MTBox)
        {
            try
            {
                DateTime DT = DateTime.Parse(MTBox.Text.Trim());
                return true;
            }
            catch
            {
                MTBox.Text = "";
                MessageBox.Show("日期输入错误，请重新输入！");
                return false;

            }
        }


        //设置文本框只能输入数字型的字符串
        public void Estimate_Key(KeyPressEventArgs e, string s, int n)
        {
            if(n==0)
                if (!(e.KeyChar <= '9' && e.KeyChar >= '0') && e.KeyChar != '\r' && e.KeyChar != '\b')
                {
                    e.Handled = true;

                }
                else
                {
                    if (e.KeyChar == '.')
                        if (s == "")
                            e.Handled = true;
                        else
                        {
                            if (s.Length > 0)
                            {
                                if (s.IndexOf(".") > -1)
                                    e.Handled = true;
                            }
                        }
                }
        }

        //添加用户权限
        public void ADD_Pope(string ID, int n)
        {
            DataSet DSet;
            DSet = MyDataClass.GetDataSet("select PopeName from tb_PopeModel", "tb_PopeModel");
            for (int i = 0; i < DSet.Tables[0].Rows.Count; i++)
            {
                MyDataClass.GetA_M_D_command("insert into tb_UserPope (ID,PopeName,Pope) values('" + ID + "','" + Convert.ToString(DSet.Tables[0].Rows[i][0]) + "'," + n + ")");

            }
        }

        //清空所有数据表
        public void Clear_Table(Control.ControlCollection GBox, string TName)
        {
            string sID = "";
            foreach (Control C in GBox)
            {
                if (C.GetType().Name == "CheckBox")
                {
                    sID = C.Name;
                    if (sID.IndexOf(TName) > -1)
                    {
                        if (((CheckBox)C).Checked == true)
                        {
                            string TableName = "";
                            string[] Astr = sID.Split(Convert.ToChar('_'));
                            TableName = "tb_" + Astr[1];
                            if (Astr[1].ToUpper() == ("Clew").ToUpper())
                            {
                                MyDataClass.GetA_M_D_command("update " + TableName + " set Fate=0,Unlock=0 where ID>0");
                            }
                            else
                            {
                                MyDataClass.GetA_M_D_command("Delete" + TableName);
                                if (Astr[1].ToUpper() == ("Login").ToUpper())
                                {
                                    MyDataClass.GetA_M_D_command("Delete tb_UserPope");
                                    MyDataClass.GetA_M_D_command("insert into " + TableName + " (ID,Name,Pass) values('0001','TSoft','111')");
                                    ADD_Pope("0001", 1);
                                }
                            
                            }
                        }
 
                    }
                }
            }
        }


        //修改用户权限
        public void Amend_Pope(Control.ControlCollection GBox, string TID)
        {
            string CheckName = "";
            int tt = 0;
            foreach (Control C in GBox)
            {
                if (C.GetType().Name == "CheckBox")
                {
                    if (((CheckBox)C).Checked)
                        tt = 1;
                    else
                        tt = 0;
                    CheckName = C.Name;
                    string[] Astr = CheckName.Split(Convert.ToChar('_'));
                    MyDataClass.GetA_M_D_command("update tb_UserPope set Pope=" + tt + " where (ID='" + TID + "') and (PopeName='" + Astr[1].Trim() + "')");

                }
            }
        }

        //将图片以二进制的形式来保存在数据库中
        public void SaveImage(string MID, byte[] p)
        {
            MyDataClass.con_open();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Stuffbusic Set Photo=@Photo where ID=" + MID);
            SqlCommand cmd = new SqlCommand(strSql.ToString(), DataClass.MyMeans.My_Con);
            cmd.Parameters.Add("@Photo", SqlDbType.Binary).Value = p;
            cmd.ExecuteNonQuery();
            DataClass.MyMeans.My_Con.Close();
        }




    }
}
