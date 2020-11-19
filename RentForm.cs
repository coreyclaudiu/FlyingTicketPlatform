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
    public partial class RentForm : Form
    {
        private MySqlCommand cmd;

        public RentForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RentForm_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            String aux = CustomerInfo.id_rent;
            String proc = "SELECT car_name FROM rentcar WHERE id_rentCar = '" + aux + "';";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                label1.Text = Convert.ToString(queryResult);
            }

            proc = "SELECT car_model FROM rentcar WHERE id_rentCar = '" + aux + "';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                label2.Text = Convert.ToString(queryResult);
            }

            proc = "SELECT price_per_day FROM rentcar WHERE id_rentCar = '" + aux + "';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                label3.Text = Convert.ToString(queryResult);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
