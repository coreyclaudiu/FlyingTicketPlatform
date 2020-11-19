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
    public partial class DiscountFlight : Form
    {
        private MySqlConnection conn;

        public DiscountFlight()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                try
                {
                    conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                    conn.Open();

                    String proc = "UPDATE flight SET discount_under18=" + textBox2.Text + " WHERE id_flight= " + textBox1.Text + ";";
                    MySqlCommand my_proc = new MySqlCommand(proc, conn);

                    MySqlDataReader MyReader2 = my_proc.ExecuteReader();
                    MessageBox.Show("Discount changed!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Show();
        }

        private void DiscountFlight_Load(object sender, EventArgs e)
        {
            textBox2.Hide();
        }
    }
}
