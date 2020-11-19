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
    public partial class BuyTicket1 : Form
    {
        public static String id_flight;
        public static String first_no, second_no, economic_no;
        public static String check_in_date;
        public static int first_price;
        public static int second_price;
        public static int economic_price;
        public static String id_customer;
        public static int discount;
        private MySqlCommand my_proc;
        private MySqlCommand cmd;
        private MySqlConnection conn=new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
        

        public BuyTicket1()
        {
            InitializeComponent();
        }

        private void BuyTicket1_Load(object sender, EventArgs e)
        {
            
            conn.Open();
            dataGridView1.Hide();
            dataGridView2.Hide();
            dataGridView3.Hide();
            label13.Hide();
            textBoxTEST.Hide();

            List<int> sits1 = new List<int>();
            List<int> sits2 = new List<int>();
            List<int> sits3 = new List<int>();
            for (int i = 0; i < 11; i++)
            {
                sits1.Add(i);
                sits2.Add(i);
                sits3.Add(i);
            }
            comboBox5.DataSource = sits1;
            comboBox6.DataSource = sits2;
            comboBox7.DataSource = sits3;
            

            String proc = "SELECT * FROM customer;";
            my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView3.DataSource = datatable;
            // dataGridView1.Hide();

            id_customer = (dataGridView3.RowCount - 1).ToString();


            proc = "SELECT id_flight, departure_city, arrival_city FROM flight;";
            my_proc = new MySqlCommand(proc, conn);

            MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;

            List<String> routes = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                String aux = "";
                DataGridViewRow dg = dataGridView1.Rows[i];
                aux += dg.Cells[0].Value.ToString();
                aux += " " + dg.Cells[1].Value.ToString();
                aux += "-" + dg.Cells[2].Value.ToString();
                routes.Add(aux);
            }
            comboBox4.DataSource = routes;

            if (CustomerInfo.FLAG_EDIT == 0)
            {
               
                List<int> day_dob = new List<int>();
                List<int> month_dob = new List<int>();
                List<int> year_dob = new List<int>();

                for (int i = 1950; i <= 2020; i++)
                {
                    year_dob.Add(i);
                }

                for (int i = 1; i <= 31; i++)
                {
                    day_dob.Add(i);
                    if (i < 13)
                    {
                        month_dob.Add(i);
                    }
                }
                comboBox1.DataSource = day_dob;
                comboBox2.DataSource = month_dob;
                comboBox3.DataSource = year_dob;
                

            }
            else
            {
                textBox1.Text = CustomerInfo.email_cus;
                textBox2.Text = CustomerInfo.password;
                textBox4.Text = CustomerInfo.first_name_cus;
                textBox5.Text = CustomerInfo.last_name_cus;
                textBox6.Text = CustomerInfo.nationality_cus;
                
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            first_no = comboBox5.Text;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            second_no = comboBox6.Text;
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            economic_no = comboBox7.Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            conn.Open();
            String proc = "SELECT price_first_class_per_seat, price_second_class_per_seat, price_economic_class_per_seat FROM flight WHERE id_flight=" + id_flight + ";";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView2.DataSource = datatable;
            DataGridViewRow dg = dataGridView2.Rows[0];
            first_price = Convert.ToInt32(dg.Cells[0].Value.ToString());
            second_price = Convert.ToInt32(dg.Cells[1].Value.ToString());
            economic_price = Convert.ToInt32(dg.Cells[2].Value.ToString());
            conn.Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            int price_total = first_price * Convert.ToInt32(comboBox5.Text) + second_price * Convert.ToInt32(comboBox6.Text) + economic_price * Convert.ToInt32(comboBox7.Text);
            
            if (CustomerInfo.FLAG_EDIT == 1)
            {
                price_total = (6 * price_total) / 10;
            }

            int check_discount = 0;
            

            String proc = "SELECT id_customer FROM customer WHERE first_name = '" + textBox4.Text + "' AND last_name='"+textBox5.Text+"';";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                check_discount = Convert.ToInt32(queryResult.ToString());
            }
            else
            {
                check_discount = 0;
            }
            if (check_discount != 0)
            {
                discount = price_total / 10;
                price_total =price_total - discount; //REDUCEREA DE 10%
            }
            else
            {
                discount = 0;
            }

            int day = dateTimePicker1.Value.Day;
            int month = dateTimePicker1.Value.Month;
            int year = dateTimePicker1.Value.Year;
            String date_pick = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
            check_in_date= year.ToString() + "-" + month.ToString() + "-" + (day-1).ToString();
            try
            {
                String aux1 = "VALUES (" + id_customer + ", " + id_flight + ", '" + date_pick + "', " + first_no + ", " + second_no + ", " + economic_no + ", 0 , " + price_total + ", '" + check_in_date + "', " + discount + ");";
                proc = "INSERT INTO booking(id_customer, id_flight, departure_date, seats_first_class, seats_second_class, seats_economic_class, under18_persons, price_total, check_in, fidelity_discount) " + aux1;

                label13.Text = proc;
                MySqlCommand my_proc = new MySqlCommand(proc, conn);
                MySqlDataReader MyReader2 = my_proc.ExecuteReader();

                conn.Close();

                conn.Open();
                proc = "INSERT INTO services(id_booking) VALUES (" + id_customer + ");";



                label13.Text = proc;
                my_proc = new MySqlCommand(proc, conn);
                MyReader2 = my_proc.ExecuteReader();

                MessageBox.Show("You are registered now!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            //PAY THE COMPANY
            String company_to_pay;
            proc = "SELECT flight.id_company FROM flight INNER JOIN booking ON booking.id_booking =" + id_customer + " AND booking.id_flight = flight.id_flight";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);
            queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
            // If we have result, then convert it from object to string.
            {
                company_to_pay = Convert.ToString(queryResult);
            }
            else
            {
                company_to_pay = "Unknown";
            }

            switch (company_to_pay)
            {
                case "1": WizzForm.sum_wizz += price_total; break;
                case "2": EmiratesForm.sum_emirates += price_total; break;
                case "3": QatarForm.sum_qatar += price_total; break;
                case "4": TaromForm.sum_tarom += price_total; break;
                case "5": BlueAirForm.sum_blue += price_total; break;
            }
            conn.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {


            conn.Open();
            String proc;

            String flag_taxi = "NULL";
            String flag_rent = "NULL";
            String flag_med = "NULL";

            if (checkBox1.Checked)
            {
                int random = Convert.ToInt32(id_customer) % 10 + 1;

                flag_rent = Convert.ToString(random);
            }
            if (checkBox2.Checked)
            {
                int random = Convert.ToInt32(id_customer) % 10 + 1;

                flag_taxi = Convert.ToString(random);
            }
            if (checkBox3.Checked)
            {
                int random = Convert.ToInt32(id_customer) % 10 + 1;

                flag_med = Convert.ToString(random);
            }
            try
            {
                //String flag = "34";
                proc = "UPDATE services SET id_rentCar=" + flag_rent + ", id_callCab=" + flag_taxi + ", id_medicalServices=" + flag_med + " WHERE id_booking=" + id_customer + ";";

                label13.Text = proc;
                MySqlCommand my_proc = new MySqlCommand(proc, conn);
                MySqlDataReader MyReader2 = my_proc.ExecuteReader();
                MessageBox.Show("Services added!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            conn.Open();
            
            String proc;
            try
            {
                String dob_string = comboBox3.Text + "-" + comboBox2.Text + "-" + comboBox1.Text;

                String aux1 = "VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + dob_string + "');";
                proc = "INSERT INTO customer(username, password_c, ID, first_name, last_name, nationality, date_birth) " + aux1;

                textBoxTEST.Text = proc;
                MySqlCommand my_proc = new MySqlCommand(proc, conn);
                MySqlDataReader MyReader2 = my_proc.ExecuteReader();
                conn.Close();
                conn.Open();

                //aux1 = "'VISA', '" + textBox4.Text + " " + textBox5.Text + "', '4443617108374545', '123', 10000, " + id_customer + ");";
                //proc = "INSERT INTO payment(type_card, name_on_card, card_no, cvv, money_on_card, id_booking) VALUES ("+aux1;

                //my_proc = new MySqlCommand(proc, conn);
                //MySqlDataReader MyReader1 = my_proc.ExecuteReader();

                MessageBox.Show("You are registered now!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            String aux = comboBox4.Text;
            String[] arr = new string[4];
            String[] words = aux.Split(' ');
            foreach (String word in words)
            {
                arr[0] = word;
                break;
            }
            id_flight = arr[0];

        }
    }
}
