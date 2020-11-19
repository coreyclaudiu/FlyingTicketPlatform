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
    public partial class TaxiForm : Form
    {
        private MySqlCommand cmd;

        public TaxiForm()
        {
            InitializeComponent();
        }

        private void TaxiForm_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            String aux = CustomerInfo.id_cab;
            String proc = "SELECT company_name FROM callcab WHERE id_callCab = '" + aux + "';";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                label3.Text = Convert.ToString(queryResult);
            }

            proc = "SELECT phone_num FROM callcab WHERE id_callCab = '" + aux + "';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                label4.Text = Convert.ToString(queryResult);
            }

            proc = "SELECT city FROM callcab WHERE id_callCab = '" + aux + "';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                label5.Text = Convert.ToString(queryResult);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
