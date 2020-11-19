using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const String SERVER = "127.0.0.1";
        private const String DATABASE = "flightsmanagement";
        private const String UID = "root";
        private const String PASSWORD = "problema";
        public static String global_date;
        private MySqlConnection dbconn;

        public Form1()
        {       
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            new LoginForm().ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string dbDetails = "datasource=localhost;port=3306;database=" + DATABASE + ";username="+UID+";password=" + PASSWORD;
                dbconn = new MySqlConnection(dbDetails);

                string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                string proc = "SELECT * FROM " + selectedItem + ";";
                
                MySqlCommand command=dbconn.CreateCommand();
                MySqlCommand my_proc = new MySqlCommand(proc, dbconn);

                dbconn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = my_proc;
                DataTable datatable = new DataTable();
                MyAdapter.Fill(datatable);
                dataGridView1.DataSource = datatable;
                
                dbconn.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            textBox1.Text = selectedItem;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            comboBox1.Hide();
            textBox1.Hide();
            button3.Hide();
            List<string> list = new List<string>();
            list.Add("");
            list.Add("Flight");
            list.Add("Customer");
            list.Add("Aircraft");
            comboBox1.DataSource = list;
            labelTIME.Text += CurrentTimeAndDate.currentDay + " " + CurrentTimeAndDate.currentMonth + " " + CurrentTimeAndDate.currentYear;
            global_date = CurrentTimeAndDate.currentDay + "-" + CurrentTimeAndDate.currentMonth + "-" + CurrentTimeAndDate.currentYear;
            textBox1.Text = global_date;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login1Customer().ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BuyTicket1().ShowDialog();
            this.Show();
        }

        private void buttonACC_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AccountantLogin().ShowDialog();
            this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelTIME_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
