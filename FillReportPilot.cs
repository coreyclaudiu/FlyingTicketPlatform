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
    public partial class FillReportPilot : Form
    {
        public FillReportPilot()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillReportPilot_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = StaffAuthentification.id_aircraft;
            textBox1.Text = LoginForm.first_name_staff;
            textBox2.Text = LoginForm.last_name_staff;

            List<int> days = new List<int>();
            List<int> months = new List<int>();
            List<int> year = new List<int>();
            List<int> available = new List<int>();
            List<String> comps = new List<String>();
            comps.Add("Wizz Air");
            comps.Add("Emirates");
            comps.Add("Qatar");
            comps.Add("Tarom");
            comps.Add("Blue Air");
            comboBox9.DataSource = comps;
            available.Add(1);
            available.Add(0);
            year.Add(2020);
            year.Add(2021);
            for(int i=1; i<=31; i++)
            {
                days.Add(i);
                if (i < 13)
                {
                    months.Add(i);
                }
            }
            comboBox2.DataSource = days;
            comboBox7.DataSource = days;
            comboBox3.DataSource = months;
            comboBox6.DataSource = months;
            comboBox5.DataSource = year;
            comboBox4.DataSource = year;
            comboBox8.DataSource = available;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            String dep_time = comboBox4.Text.ToString() + "-" + comboBox3.Text.ToString() + "-" + comboBox2.Text.ToString();
            String arr_time= comboBox5.Text.ToString() + "-" + comboBox6.Text.ToString() + "-" + comboBox7.Text.ToString();
            String aircraft = comboBox1.Text.ToString();
            String av = comboBox8.Text.ToString();
            String comp;
            switch (comboBox9.Text)
            {
                case "Wizz Air": comp = "1"; break;
                case "Emirates": comp = "2"; break;
                case "Qatar": comp = "3"; break;
                case "Tarom": comp = "4"; break;
                case "Blue Air": comp = "5"; break;
                default: comp = ""; break;
            }

            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                String aux1 = "VALUES (" + comboBox1.Text + ", '" + textBox1.Text + "', '" + textBox2.Text + "', '" + dep_time + "', '" + arr_time + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox7.Text + "', " + av + ", " + comp + ");";
                String proc = "INSERT INTO report(id_aircraft, pilot_first_name, pilot_last_name, departure_date, arrival_date, departure_city, arrival_city, tehnical_problems, availability, id_company) " + aux1;
                MySqlCommand my_proc = new MySqlCommand(proc, conn);
                conn.Open();
                MySqlDataReader MyReader2 = my_proc.ExecuteReader();
                MessageBox.Show("Data Updated");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
