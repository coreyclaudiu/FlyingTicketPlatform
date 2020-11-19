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
    public partial class AccountantLogin : Form
    {
        public AccountantLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="accountant" && textBox2.Text == "accountant")
            { 
                if (CurrentTimeAndDate.currentHour >= 8 && CurrentTimeAndDate.currentHour < 20)
                {
                       this.Hide();
                       new CompaniesButtons().ShowDialog();
                       this.Show();
                }
                else
                {
                    MessageBox.Show("You have no acces at this hour of night/morning!\nGet back to bed!");
                }
                
            }
            else
            {
                MessageBox.Show("Something went wrong with the authentification!\nPlease try again later!");
                //textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AccountantLogin_Load(object sender, EventArgs e)
        {
            textBox3.Hide();
            textBox1.Text = "accountant";
            textBox3.Text = CurrentTimeAndDate.currentHour.ToString() + " " + CurrentTimeAndDate.currentMinute.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
