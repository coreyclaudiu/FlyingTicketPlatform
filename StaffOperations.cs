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
    public partial class StaffOperations : Form
    {
        private MySqlCommand cmd;
        private MySqlConnection conn;
        private string proc;

        public StaffOperations()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            proc = "SELECT id_staff, first_name, last_name, gender, date_of_birth, nationality FROM staff WHERE job='Pilot';";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            proc = "SELECT id_staff, first_name, last_name, gender, date_of_birth, nationality FROM staff WHERE job='Copilot';";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            proc = "SELECT id_staff, first_name, last_name, gender, date_of_birth, nationality FROM staff WHERE job='Stewardess';";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AccessEditAccountant().ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DiscountFlight().ShowDialog();
            this.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
