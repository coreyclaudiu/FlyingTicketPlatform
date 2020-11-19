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
    public partial class StaffAuthentification : Form
    {

        public static int sum_pilot;
        public static int sum_copilot;
        public static int sum_steward;
        public static List<int> id_aircraft;
        public StaffAuthentification()
        {
            InitializeComponent();
        }

        private void StaffAuthentification_Load(object sender, EventArgs e)
        {
            button2.Hide();
            dataGridView1.Hide();
            label7.Text = LoginForm.id_staff;
            label8.Text = LoginForm.first_name_staff;
            label9.Text = LoginForm.last_name_staff;
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            String proc = "SELECT * FROM staff WHERE id_staff=" + label7.Text + ";";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;

            DataGridViewRow dg = dataGridView1.Rows[0];
            label10.Text = dg.Cells[3].Value.ToString();
            label11.Text = dg.Cells[4].Value.ToString().Remove(10);
            label12.Text = dg.Cells[5].Value.ToString();
            label13.Text = dg.Cells[6].Value.ToString();
            label14.Text = dg.Cells[7].Value.ToString();
            label15.Text = dg.Cells[8].Value.ToString().Remove(10);
            label16.Text = dg.Cells[9].Value.ToString().Remove(10);
            label17.Text = dg.Cells[10].Value.ToString().Remove(10);

   
            proc = "SELECT flight.id_flight, departure_city, arrival_city, departure_time, income_for_salary, flight.id_aircraft FROM flight INNER JOIN flightstaff ON flight.id_flight = flightstaff.id_flight AND flightstaff.id_staff = "+ label7.Text+ ";";
            my_proc = new MySqlCommand(proc, conn);

            MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView2.DataSource = datatable;

            if (label14.Text == "Pilot")
            {
                List<int> list1 = new List<int>();
                try
                {
                    button2.Show();
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        dg = dataGridView2.Rows[i];
                        sum_pilot += Convert.ToInt32(dg.Cells[4].Value.ToString());
                        list1.Add(Convert.ToInt32(dg.Cells[5].Value.ToString()));
                    }
                    id_aircraft = list1;
                    label2.Text = sum_pilot.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (label14.Text == "Copilot")
            {
                try
                {
                   
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        dg = dataGridView2.Rows[i];
                        sum_copilot += Convert.ToInt32(dg.Cells[4].Value.ToString());
                    }
                    sum_copilot = (sum_copilot * 3) / 4;
                    label2.Text = sum_copilot.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (label14.Text == "Stewardess")
            {
                try
                {
                    
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        dg = dataGridView2.Rows[i];
                        sum_steward += Convert.ToInt32(dg.Cells[4].Value.ToString());
                    }
                    sum_steward = sum_steward / 2;
                    label2.Text = sum_steward.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FillReportPilot().ShowDialog();
            this.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
