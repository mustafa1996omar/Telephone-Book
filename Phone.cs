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

namespace Telephone
{
    public partial class Phone : Form
    {
        SqlConnection dbcon = new SqlConnection("Data Source=MUSTAFA-PC\\MYSQLSERVER;Initial Catalog=Phone;Integrated Security=True");
        public Phone()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Phone_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox2;
            textBox2.Focus();
            Display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbcon.Open();
            SqlCommand insert = new SqlCommand(@"INSERT INTO Phones (FirstName, LastName, Mobile, Email, Category) VALUES ('" + textBox2.Text + "' , '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + comboBox1.Text + "')", dbcon);
            insert.ExecuteNonQuery();
            dbcon.Close();
            MessageBox.Show("Contact was successfully saved!");
            Display();
        }

        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from Phones", dbcon);
            DataTable dt = new DataTable();
            Console.Write(dt);
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["FirstName"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["LastName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Mobile"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Email"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Category"].ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dbcon.Open();
            SqlCommand delete = new SqlCommand(@"Delete FROM Phones Where (Mobile = '"+ textBox4.Text + "' )", dbcon);
            delete.ExecuteNonQuery();
            dbcon.Close();
            MessageBox.Show("Contact was successfully deleted!");
            Display();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dbcon.Open();
            SqlCommand delete = new SqlCommand(@"Update Phones SET FirstName = '" + textBox2.Text + "' , LastName = '" + textBox3.Text + "' , Mobile = '" + textBox4.Text + "' , Email = '" + textBox5.Text + "' , Category = '" + comboBox1.Text + "' Where (Mobile = '" + textBox4.Text + "')", dbcon);
            delete.ExecuteNonQuery();
            dbcon.Close();
            MessageBox.Show("Contact was successfully updated!");
            Display();
        }
    }
}
