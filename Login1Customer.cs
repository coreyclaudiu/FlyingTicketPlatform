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
    public partial class Login1Customer : Form
    {
        private MySqlCommand my_proc;
        private MySqlCommand cmd;
        public static String toSELECTusername;
        public static String toSELECTpassword;
        public static String toSELECTid;

        public Login1Customer()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                String username = textBox1.Text;
                String pass = textBox2.Text;  

                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                conn.Open();

                String queryUsername = "SELECT username FROM customer WHERE username = '"+ username +"';";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryUsername, conn);

                
                var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
                if (queryResult != null)
                    // If we have result, then convert it from object to string.
                    toSELECTusername = Convert.ToString(queryResult);
                else
                    // Else make id = "" so you can later check it.
                    toSELECTusername = "STOP";

                String queryPassword = "SELECT password_c from customer where password_c LIKE '" + pass + "';";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryPassword, conn);

                queryResult = cmd.ExecuteScalar();//Return an object so first check for null
                if (queryResult != null)
                    // If we have result, then convert it from object to string.
                    toSELECTpassword = Convert.ToString(queryResult);
                else
                    // Else make id = "" so you can later check it.
                    toSELECTpassword = "STOP";

                MySqlCommand command = conn.CreateCommand();
                String proc = "SELECT id_customer FROM customer WHERE username='" + username + "' and password_c='" + pass + "';";
                my_proc = new MySqlCommand(proc, conn);

                cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);


                queryResult = cmd.ExecuteScalar();//Return an object so first check for null
                if (queryResult != null)
                    // If we have result, then convert it from object to string.
                    toSELECTid = Convert.ToString(queryResult);
                else
                {
                    toSELECTid = "00";
                }


                if (toSELECTusername=="STOP" || toSELECTpassword == "STOP")
                {
  
                    label3.ForeColor = Color.DarkSalmon;
                    label3.Text = "Unfound client";
                }
                else if(username==toSELECTusername && pass==toSELECTpassword)
                {
                    String queryCustomer = "SELECT * FROM CUSTOMER WHERE USERNAME LIKE '" + username + "';";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(queryUsername, conn);
                    queryResult = cmd.ExecuteReader();

                    label3.ForeColor = Color.Black;
                    label3.Text = "CORRECT";
                    this.Hide();
                    new CustomerInfo().ShowDialog();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    label3.Text = "---------";
                    this.Show();
                }

                

                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
   
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Login1Customer_Load(object sender, EventArgs e)
        {
            textBox3.Text = "GabrielVoicu@gmail.com";
            textBox4.Text = "sXLXdkXJAxkE";
            label3.Hide();
        }
    }
}
