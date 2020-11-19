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
    public partial class CustomerInfo : Form
    {
        public static String username;
        public static String password;
        public static String id_user;
        public static String id_rent;
        public static String id_cab;
        public static String id_medical;
        public static String medicalMessage;

        public static String first_name_cus;
        public static String last_name_cus;
        public static String nationality_cus;
        public static String email_cus;
        public static String phone_cus;
        public static String dob_cus;
        public static int FLAG_EDIT;
        private MySqlCommand my_proc;

        public MySqlDataReader MyReader2 { get; private set; }

        private MySqlCommand cmd;
        private string varCity;

        public CustomerInfo()
        {
            InitializeComponent();
        }

        private void CustomerInfo_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            testHelper.Hide();
            labelFN.Text = "";
            labelLN.Text = "";
            labelNAT.Text = "";
            labelDOB.Text = "";

            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            id_user =Login1Customer.toSELECTid;
            username = Login1Customer.toSELECTusername;
            password = Login1Customer.toSELECTpassword;


            pictureBox1.Hide();
            pictureBox2.Hide();
            buttonCAB.Hide();
            buttonMED.Hide();
            buttonRENT.Hide();
            label16.Hide();
            label17.Hide();
            label18.Hide();


            String proc = "SELECT * FROM customer WHERE username = '" + username + "' AND password_c='" + password + "';";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;

            DataGridViewRow dgvr = dataGridView1.Rows[0];
            labelFN.Text = dgvr.Cells[4].Value.ToString();
            first_name_cus = labelFN.Text;
            labelLN.Text= dgvr.Cells[5].Value.ToString();
            last_name_cus = labelLN.Text;
            labelNAT.Text = dgvr.Cells[6].Value.ToString();
            nationality_cus = labelNAT.Text;
            labelDOB.Text = dgvr.Cells[7].Value.ToString().Remove(10);
            dob_cus = labelDOB.Text;

            labelNAME.Text = labelFN.Text + " " + labelLN.Text;

             proc = "SELECT username FROM customer WHERE first_name = '" + labelFN.Text + "' AND last_name='" + labelLN.Text + "';";
             my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);


            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                labelEMAIL.Text = Convert.ToString(queryResult);
                email_cus = labelEMAIL.Text;
            }
            else
                // Else make id = "" so you can later check it.
                labelEMAIL.Text = "STOP";

            proc = "SELECT phone FROM phone WHERE id_phone = (SELECT id_customer FROM customer WHERE first_name = '" + labelFN.Text + "' AND last_name = '" + labelLN.Text + "'); ";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                labelPHONE.Text = Convert.ToString(queryResult);
                phone_cus = labelPHONE.Text;
            }
            else
                // Else make id = "" so you can later check it.
                labelPHONE.Text = "STOP";

          
            String cond2HOME = " AND type_phone= 'Home';";
            proc = "SELECT phone FROM phone WHERE id_customer = (SELECT id_customer FROM customer WHERE first_name = '" + labelFN.Text + "' AND last_name = '" + labelLN.Text + "')" + cond2HOME;
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                labelHOME.Text = Convert.ToString(queryResult);
            else
            // Else make id = "" so you can later check it.
            {
                labelHOME.Text = "";
                label15.Hide();
            }

           
            proc = "SELECT type_card FROM payment WHERE id_payment = '"+ id_user + "';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                String type_card = Convert.ToString(queryResult);
                if (type_card == "Master Card")
                {
                    pictureBox1.Hide();
                    pictureBox2.Show();

                }
                else if (type_card == "VISA")
                {
                    pictureBox1.Show();
                    pictureBox2.Hide();

                }
            }
            else
            {
                pictureBox1.Hide();
                pictureBox2.Hide();
            }
            testHelper.Text = id_user; // AICI VERIFIC ORICE INFORMATIE NESIGURA


            //PENTRU DECOLARE!!!!!!!!!!!!!!!!
            varCity = "";
            proc = "SELECT id_flight FROM booking WHERE id_booking = '" + id_user + "';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                varCity = Convert.ToString(queryResult);
            }

            
            proc = "SELECT departure_city FROM flight WHERE id_flight= " + varCity + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                labelFROM.Text = Convert.ToString(queryResult);
            }


            //PENTRU DESTINATIE!!!!!!!!!!!!!
            proc = "SELECT id_flight FROM booking WHERE id_booking = '" + id_user + "';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                varCity = Convert.ToString(queryResult);
            }


            proc = "SELECT arrival_city FROM flight WHERE id_flight= " + varCity + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                labelTO.Text = Convert.ToString(queryResult);
            }



            int flag = 0;
            //SELECTAM MAI INTAI TABELA CU SERVICII 
            String id_service = "NULL";
            proc = "SELECT id_service FROM services WHERE id_booking= " + id_user + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                id_service = Convert.ToString(queryResult);
                flag = 1;
            }

            proc = "SELECT id_rentCar FROM services WHERE id_service= " + id_service + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                String aux = Convert.ToString(queryResult);
                if (aux != "")
                {
                    buttonRENT.Show();
                    label17.Show();
                    id_rent = aux;
                }
            }


            proc = "SELECT id_callCab FROM services WHERE id_service= " + id_service + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                String aux = Convert.ToString(queryResult);
                if (aux != "")
                {
                    buttonCAB.Show();
                    label16.Show();
                    id_cab = aux;
                }
            }

            proc = "SELECT id_medicalServices FROM services WHERE id_service= " + id_service + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                String aux = Convert.ToString(queryResult);
                if (aux != "")
                {
                    buttonMED.Show();
                    label18.Show();
                    id_medical = aux;
                }
            }
            if (flag == 0)
            {
                labelEXTRA.Text = "No extras selected";
            }

            proc = "SELECT type_service FROM medicalservices WHERE id_medicalServices= '" + id_medical + "';";
            my_proc = new MySqlCommand(proc, conn);


            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                medicalMessage = Convert.ToString(queryResult);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelFN_Click(object sender, EventArgs e)
        {

        }

        private void labelNAME_Click(object sender, EventArgs e)
        {

            
        }

        private void labelEMAIL_Click(object sender, EventArgs e)
        {
            
        }

        private void labelLN_Click(object sender, EventArgs e)
        {

        }

        private void buttonCAB_Click(object sender, EventArgs e)
        {
            this.Hide();
            new TaxiForm().ShowDialog();
            this.Show();
        }

        private void buttonRENT_Click(object sender, EventArgs e)
        {
            this.Hide();
            new RentForm().ShowDialog();
            this.Show();
        }

        private void buttonMED_Click(object sender, EventArgs e)
        {
            MessageBox.Show(medicalMessage);
        }

        private void labelDOB_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FLAG_EDIT = 1;
            this.Hide();
            new BuyTicket1().ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            String day = CurrentTimeAndDate.currentDay;
            String month = CurrentTimeAndDate.currentMonth1.ToString();

            String id_flight = varCity;
            String flight_date = "";

            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            String proc = "SELECT check_in FROM booking WHERE id_flight= " + id_flight + ";";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                flight_date = Convert.ToString(queryResult).Remove(10);
            }

            DateTime dt = Convert.ToDateTime(flight_date);

            int current_day = Convert.ToInt32(day);
            int current_month = Convert.ToInt32(month);

            int check_day = dt.Day;
            int check_month = dt.Month;
            proc = "SELECT id_company FROM flight WHERE id_flight= " + id_flight + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            int id_company = 0;
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                id_company = Convert.ToInt32(queryResult);
            }


            int sum = 5;
            if (check_month - current_month >= 3)
            {
                sum = 5;
            }
            else if (check_month - current_month >= 1)
            {
                sum *= 2;
            }
            else if (check_month - current_month == 0)
            {
                if (check_day - current_day == 0)
                {
                    sum *= 4;
                }
                else if(check_day - current_day <0)
                {
                    sum *= 0;
                }
            }
            if (sum == 0)
                MessageBox.Show("Too late for check-in");
            
            switch (id_company)
            {
                case 1: WizzForm.sum_wizz += sum;break;
                case 2: EmiratesForm.sum_emirates += sum; break;
                case 3: QatarForm.sum_qatar += sum; break;
                case 4: TaromForm.sum_tarom += sum; break;
                case 5: BlueAirForm.sum_blue += sum; break;
            }
            MessageBox.Show("Checked in for " + sum + " USD!");
            

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void buttonQUIT_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            String proc = "SELECT id_company FROM flight WHERE id_flight= " + varCity + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            int id_company = 0;
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                id_company = Convert.ToInt32(queryResult);
            }

            int sum = 0;
            proc = "SELECT price_total FROM booking WHERE id_flight= " + id_user + ";";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                sum = Convert.ToInt32(queryResult);
            }

            switch (id_company)
            {
                case 1: WizzForm.sum_wizz -= (sum/4); break;
                case 2: EmiratesForm.sum_emirates -= (sum/4); break;
                case 3: QatarForm.sum_qatar -= (sum/4); break;
                case 4: TaromForm.sum_tarom -= (sum/4); break;
                case 5: BlueAirForm.sum_blue -= (sum/4); break;
            }


            proc = "UPDATE booking SET id_flight= NULL WHERE id_booking= "+ id_user+";";

            my_proc = new MySqlCommand(proc, conn);
            MyReader2 = my_proc.ExecuteReader();
            MessageBox.Show("Your flight have been removed successfully!");

        }
    }
}
