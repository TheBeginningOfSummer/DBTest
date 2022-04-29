using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        SQLServerTool sqlServerTool;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)//连接
        {
            if (panel2.Controls.OfType<TextBox>().All(a => a.Text.Length > 0))
            {
                sqlServerTool = new SQLServerTool(textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text);
                sqlServerTool.Connection();
            }
            else
            {
                MessageBox.Show("信息不完整");
            }
        }

        private void button2_Click(object sender, EventArgs e)//查表
        {
            DataTable dataTable = new DataTable();
            dataGridView1.DataSource = dataTable;
            if (textBox8.Text.Length == 0)
            {
                MessageBox.Show("输入表名");
            }
            else
            {
                try
                {
                    sqlServerTool.SelectExeCute(dataTable, textBox8.Text);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void button3_Click(object sender, EventArgs e)//写入数据
        {
            if (panel1.Controls.OfType<TextBox>().All(a => a.Text.Length > 0))
            {
                string command = "insert into Map_Detector(Station_ID,LOT_ID,Mag_ID,Tray_ID,Layer_ID,Seat_ID,Detector_ID) " +
                 "values(" + string.Format("'{0}',{1},{2},{3},{4},{5},'{6}'", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text) + ");";
                try
                {
                    sqlServerTool.CommandExeCute(command);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            else
            {
                MessageBox.Show("信息不完整");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox14.Text.Length == 0)
            {
                MessageBox.Show("输入要删除的id号");
            }
            else
            {
                string command = "delete from Map_Detector where id = " + textBox14.Text;
                try
                {
                    sqlServerTool.CommandExeCute(command);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable table = sqlServerTool.sqlConnection.GetSchema("Tables");
            dataGridView1.DataSource = table;
        }
    }
}
