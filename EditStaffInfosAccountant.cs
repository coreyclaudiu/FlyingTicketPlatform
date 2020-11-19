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
    public partial class EditStaffInfosAccountant : Form
    {
        public static int salary_flag=0;
        public static int begin_flag = 0;
        public static int end_flag = 0;
        public static int job_flag = 0;
        private MySqlCommand cmd;
        public static String kind_of_job;
        public static String fn;
        public static String ln;
        public static String staff_key;
        private int sc_year_begin;
        private int sc_month_begin;
        private int sc_day_begin;
        private int sc_year_end;
        private int sc_month_end;
        private int sc_day_end;

        public EditStaffInfosAccountant()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditStaffInfosAccountant_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            label4.Hide();
            label3.Hide();
            button3.Hide();
            button1.Hide();
            textBox1.Hide();
            dataGridView1.Hide();
            dataGridView2.Hide();

            List<String> jobs = new List<String>();
            jobs.Add("Pilot");
            jobs.Add("Copilot");
            jobs.Add("Stewardess");
            comboBox1.DataSource = jobs;

            List<String> to_edit = new List<string>();
            to_edit.Add("");
            to_edit.Add("salary");
            to_edit.Add("date_start_vacation");
            to_edit.Add("date_end_vacation");
            to_edit.Add("job");
            comboBox2.DataSource = to_edit;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(begin_flag==0 && end_flag==0)
            {
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                
                String proc;
                if (salary_flag == 1)
                {
                    try
                    {
                        int aux = Convert.ToInt32(textBox1.Text);
                        String aux1 = " AND first_name='" + fn + "' AND last_name='" + ln + "';";
                        proc = "UPDATE staff SET " + comboBox2.Text + "= " + aux.ToString() + " WHERE job='" + comboBox1.Text + "' AND id_staff="+staff_key+aux1;
                        //labelTEST.Text = proc;
                        MySqlCommand my_proc = new MySqlCommand(proc, conn);
                        conn.Open();
                        MySqlDataReader MyReader2 = my_proc.ExecuteReader();
                        MessageBox.Show("Data Updated");
                        textBox1.Text = "";
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (job_flag == 1)
                {
                    try
                    {
                        String aux = textBox1.Text;
                        if (aux == "Pilot" || aux == "Copilot" || aux == "Stewardess") {
                            String aux1 = " AND first_name='" + fn + "' AND last_name='" + ln + "';";
                            proc = "UPDATE staff SET " + comboBox2.Text + "='" + aux + "' WHERE job='" + comboBox1.Text + "' AND id_staff=" + staff_key + aux1;
                            //labelTEST.Text = proc;
                            MySqlCommand my_proc = new MySqlCommand(proc, conn);
                            conn.Open();
                            MySqlDataReader MyReader2 = my_proc.ExecuteReader();
                            MessageBox.Show("Data Updated");
                            textBox1.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("This job doesn't exist!");
                            textBox1.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vacation selected! Take care!");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "salary")
            {
                button3.Hide();
                dateTimePicker1.Hide();
                dateTimePicker2.Hide();
                label3.Hide();
                label4.Hide();
                button1.Show();
                textBox1.Show();
                salary_flag = 1;
                job_flag = begin_flag = end_flag = 0;
            }
            else if (comboBox2.Text == "job")
            {
                button3.Hide();
                dateTimePicker1.Hide();
                dateTimePicker2.Hide();
                label3.Hide();
                label4.Hide();
                button1.Show();
                textBox1.Show();
                job_flag = 1;
                salary_flag = begin_flag = end_flag = 0;
            }
            else if (comboBox2.Text == "date_start_vacation")
            {
                button3.Show();
                dateTimePicker1.Show();
                dateTimePicker2.Hide();
                label3.Show();
                label4.Hide();
                button1.Hide();
                textBox1.Hide();
                begin_flag = 1;
                salary_flag = job_flag = end_flag = 0;
            }
            else if (comboBox2.Text == "date_end_vacation")
            {
                button3.Show();
                dateTimePicker1.Hide();
                dateTimePicker2.Show();
                label3.Hide();
                label4.Show();
                button1.Hide();
                textBox1.Hide();
                end_flag = 1;
                salary_flag = job_flag = begin_flag = 0;
            }
            else
            {
                dateTimePicker1.Hide();
                dateTimePicker2.Hide();
                label4.Hide();
                label3.Hide();
                button3.Hide();
                button1.Hide();
                textBox1.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (begin_flag == 1)
            {
                try
                {
                    String aux = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    sc_year_begin = Convert.ToInt32(dateTimePicker1.Value.Year) - Convert.ToInt32(CurrentTimeAndDate.currentYear1);
                    sc_month_begin = Convert.ToInt32(dateTimePicker1.Value.Month) - Convert.ToInt32(CurrentTimeAndDate.currentMonth1);
                    sc_day_begin = Convert.ToInt32(dateTimePicker1.Value.Day) - Convert.ToInt32(CurrentTimeAndDate.currentDay1);
                    int flag = 0;
                    if (sc_year_begin > 0)
                    {
                        flag = 1;
                    }
                    else if (sc_year_begin == 0)
                    {
                        if (sc_month_begin > 0)
                        {
                            flag = 1;
                        }
                        else if (sc_month_begin == 0)
                        {
                            if (sc_day_begin > 0)
                            {
                                flag = 1;
                            }
                        }
                    }
                    if (flag == 0)
                    {
                        MessageBox.Show("You're date is too late!");
                    }
                    else
                    {
                        String proc = "UPDATE staff SET date_start_vacation='" + aux + "' WHERE id_staff=" + staff_key + ";";

                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                        MySqlCommand my_proc = new MySqlCommand(proc, conn);
                        conn.Open();
                        MySqlDataReader MyReader2 = my_proc.ExecuteReader();

                        MessageBox.Show("Succes!");
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (end_flag == 1)
            {
                try
                {
                    String aux = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                    sc_year_end = Convert.ToInt32(dateTimePicker2.Value.Year) - sc_year_begin;
                    sc_month_end = Convert.ToInt32(dateTimePicker2.Value.Month) - sc_month_begin;
                    sc_day_end = Convert.ToInt32(dateTimePicker2.Value.Day) - sc_day_begin;
                    int flag = 0;
                    if (sc_year_end > 0)
                    {
                        flag = 1;
                    }
                    else if (sc_year_end == 0)
                    {
                        if (sc_month_end > 0)
                        {
                            flag = 1;
                        }
                        else if (sc_month_end == 0)
                        {
                            if (sc_day_end > 0)
                            {
                                flag = 1;
                            }
                        }
                    }
                    if (flag == 0)
                    {
                        MessageBox.Show("You're date is too late!");
                    }
                    else
                    {
                        String proc = "UPDATE staff SET date_end_vacation='" + aux + "' WHERE id_staff=" + staff_key + ";";

                        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                        MySqlCommand my_proc = new MySqlCommand(proc, conn);
                        conn.Open();
                        MySqlDataReader MyReader2 = my_proc.ExecuteReader();

                        MessageBox.Show("Succes!");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            String aux = comboBox3.Text;
            String[] arr = new string[4];
            String[] words = aux.Split(' ');
            int i = 0;
            foreach (String word in words)
            {
                arr[i] = word;
                i++;
            }
            staff_key = arr[0];
            fn = arr[1];
            ln = arr[2];

            try
            {
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                conn.Open();
                string selectedItem = comboBox3.Items[comboBox3.SelectedIndex].ToString();

                String proc = "SELECT * FROM staff WHERE id_staff="+staff_key+";";
                MySqlCommand my_proc = new MySqlCommand(proc, conn);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = my_proc;
                DataTable datatable = new DataTable();
                MyAdapter.Fill(datatable);
                dataGridView2.DataSource = datatable;

                DataGridViewRow dg = dataGridView2.Rows[0];
                label7.Text = dg.Cells[0].Value.ToString();
                label8.Text = dg.Cells[1].Value.ToString();
                label9.Text = dg.Cells[2].Value.ToString();
                label10.Text = dg.Cells[3].Value.ToString();
                label11.Text = dg.Cells[4].Value.ToString().Remove(10);
                label12.Text = dg.Cells[5].Value.ToString();
                label13.Text = dg.Cells[6].Value.ToString();
                label14.Text = dg.Cells[7].Value.ToString();
                label15.Text = dg.Cells[8].Value.ToString().Remove(10);
                label16.Text = dg.Cells[9].Value.ToString().Remove(10);
                label17.Text = dg.Cells[10].Value.ToString().Remove(10);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            kind_of_job = comboBox1.Text;
           // labelTEST.Text = comboBox1.Text;
            try
            {


                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
                conn.Open();

                String proc = "SELECT id_staff, first_name, last_name FROM staff WHERE job='" + kind_of_job + "';";
                MySqlCommand my_proc = new MySqlCommand(proc, conn);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = my_proc;
                DataTable datatable = new DataTable();
                MyAdapter.Fill(datatable);
                dataGridView1.DataSource = datatable;
                List<String> list_staff = new List<String>();
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    String aux = "";
                    DataGridViewRow dg = dataGridView1.Rows[i];
                    aux = dg.Cells[0].Value.ToString();
                    aux += " " + dg.Cells[1].Value;
                    aux += " " + dg.Cells[2].Value;
                    list_staff.Add(aux);
                }
                comboBox3.DataSource = list_staff;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid tables!");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            


        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
