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
    public partial class CurrentTimeAndDate : Form
    {

        public static String currentDay;
        public static String currentMonth;
        public static String currentYear;
        public static int currentHour;
        public static int currentMinute;
        public static int currentDay1;
        public static int currentMonth1;
        public static int currentYear1;
        private string proc;
        private MySqlCommand my_proc;
        private MySqlCommand cmd;
        private MySqlConnection conn;

        public CurrentTimeAndDate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                currentHour = Convert.ToInt32(textBox1.Text);
                currentMinute = Convert.ToInt32(textBox2.Text);
                if (currentHour < 0 || currentHour > 23 || currentMinute < 0 || currentMinute > 59)
                {
                    MessageBox.Show("Invalid values. Try again!");
                }
                else
                {
                    this.Hide();
                    new Form1().ShowDialog();
                    this.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            currentDay1 = Convert.ToInt32(dateTimePicker1.Value.Day);
            currentMonth1 = Convert.ToInt32(dateTimePicker1.Value.Month);
            currentYear1 = Convert.ToInt32(dateTimePicker1.Value.Year);


            currentDay = dateTimePicker1.Value.Day.ToString();
            currentYear = dateTimePicker1.Value.Year.ToString();

            switch (dateTimePicker1.Value.Month.ToString())
            {
                case "1":
                    currentMonth = "Jan";
                    break;
                case "2":
                    currentMonth = "Feb";
                    break;
                case "3":
                    currentMonth = "Mar";
                    break;
                case "4":
                    currentMonth = "Apr";
                    break;
                case "5":
                    currentMonth = "May";
                    break;
                case "6":
                    currentMonth = "Jun";
                    break;
                case "7":
                    currentMonth = "Jul";
                    break;
                case "8":
                    currentMonth = "Aug";
                    break;
                case "9":
                    currentMonth = "Sep";
                    break;
                case "10":
                    currentMonth = "Oct";
                    break;
                case "11":
                    currentMonth = "Nov";
                    break;
                case "12":
                    currentMonth = "Dec";
                    break;
            }
            
        }

        private void CurrentTimeAndDate_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            proc = "SELECT income FROM company WHERE id_company=1;";
            my_proc = new MySqlCommand(proc, conn);
            conn.Open();

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);

            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                WizzForm.sum_wizz = Convert.ToInt32(queryResult);


            proc = "SELECT income FROM company WHERE id_company=2;";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);

            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                EmiratesForm.sum_emirates = Convert.ToInt32(queryResult);

            proc = "SELECT income FROM company WHERE id_company=3;";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);

            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                QatarForm.sum_qatar = Convert.ToInt32(queryResult);

            proc = "SELECT income FROM company WHERE id_company=4;";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);

            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                TaromForm.sum_tarom = Convert.ToInt32(queryResult);

            proc = "SELECT income FROM company WHERE id_company=5;";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);

            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                BlueAirForm.sum_blue = Convert.ToInt32(queryResult);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
