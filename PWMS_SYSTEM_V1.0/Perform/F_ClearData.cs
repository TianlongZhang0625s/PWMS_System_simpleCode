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
    public partial class F_ClearData : Form
    {
        public F_ClearData()
        {
            InitializeComponent();
        }

        ModuleClass.MyModule MyMC = new ModuleClass.MyModule();

        private void but_clear_Click(object sender, EventArgs e)
        {
            MyMC.Clear_Table(groupBox1.Controls, "Table_");
        }

        private void but_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ALL_Table_MouseDown(object sender, MouseEventArgs e)
        {
            //设置一个状态变量
            bool tt = false;
            if (((CheckBox)sender).Checked == true)
                tt = false;
            else
                tt = true;
            //遍历groupBox1中所有控件，并修改对应状态
            foreach (Control C in groupBox1.Controls)
            {
                string sID = C.Name;
                if (sID.IndexOf("Table_") > -1)
                {
                    ((CheckBox)C).Checked = tt;
                }
            }
        }

        private void Table_Branch_MouseUp(object sender, MouseEventArgs e)
        {
            if (((CheckBox)sender).Checked == false)
            {
                ALL_Table.Checked = false;
            }
        }


    }
}
