﻿using MySql.Data.MySqlClient;
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
    public partial class BlueAirForm : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        public static int sum_blue;
        public static int tax_blue;
        public static int final_blue;

        public BlueAirForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BlueAirForm_Load(object sender, EventArgs e)
        {
            dataGridView2.Hide();
            conn = new MySqlConnection("datasource=localhost;port=3306;database=flightsmanagement;username=root;password=problema");
            conn.Open();

            String proc = "SELECT departure_city,arrival_city, departure_time, arrival_time, first_class_seats_no, second_class_seats_no, economic_class_seats_no FROM flight WHERE id_company=5;";
            MySqlCommand my_proc = new MySqlCommand(proc, conn);

            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            DataTable datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;

            proc = "SELECT tax FROM company WHERE id_company=5;";
            my_proc = new MySqlCommand(proc, conn);

            cmd = new MySql.Data.MySqlClient.MySqlCommand(proc, conn);

            var queryResult = cmd.ExecuteScalar();//Return an object so first check for null
            if (queryResult != null)
                // If we have result, then convert it from object to string.
                tax_blue = Convert.ToInt32(queryResult);

            proc = "SELECT booking.price_total FROM booking INNER JOIN flight ON  booking.id_flight = flight.id_flight AND flight.id_company = 5;";
            my_proc = new MySqlCommand(proc, conn);

            MyAdapter = new MySqlDataAdapter();
            MyAdapter.SelectCommand = my_proc;
            datatable = new DataTable();
            MyAdapter.Fill(datatable);
            dataGridView2.DataSource = datatable;

            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                DataGridViewRow dg = dataGridView2.Rows[i];
                sum_blue += Convert.ToInt32(dg.Cells[0].Value);
            }


            final_blue = sum_blue - (sum_blue * tax_blue) / 100;

            labelTEST.Text = sum_blue.ToString() + " USD";
            labelTEST2.Text = tax_blue.ToString() + "%";
            labelTEST3.Text = final_blue.ToString() + " USD";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.BlueAir.ro");
        }

        private void labelTEST_Click(object sender, EventArgs e)
        {

        }
    }
}
