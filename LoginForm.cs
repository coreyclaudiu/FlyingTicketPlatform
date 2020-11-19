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
    public partial class LoginForm : Form
    {
        private MySqlCommand cmd;
        public static String first_name_staff;
        public static String last_name_staff;
        public static String id_staff;



        public LoginForm()
        {

            InitializeComponent();

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                first_name_staff = textBox1.Text;
      
                String queryStr = "SELECT first_name from staff where first_name='" + first_name_staff + "';";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);
                conn.Open();
                var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
                if (queryResult != null)
                    first_name_staff = Convert.ToString(queryResult);
                else
                    first_name_staff = "";

                last_name_staff = textBox2.Text;
                queryStr = "SELECT last_name FROM staff WHERE last_name='" + last_name_staff + "';";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);
                queryResult = cmd.ExecuteScalar();
                if (queryResult != null)
                {
                    last_name_staff = Convert.ToString(queryResult);
                }
                else last_name_staff = "";

                id_staff = textBox3.Text;
                queryStr = "SELECT id_staff FROM staff WHERE id_staff='" + id_staff + "';";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, conn);
                queryResult = cmd.ExecuteScalar();
                if (queryResult != null)
                {
                    id_staff = Convert.ToString(queryResult);
                }
                else id_staff = "";

                if (first_name_staff!= "" && last_name_staff != "" && id_staff!="")
                {
                    this.Hide();
                    new StaffAuthentification().ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Invalid data inserted!");
                }
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
