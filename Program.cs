﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CurrentTimeAndDate());
            //Application.Run(new StaffOperations());
            //Application.Run(new WizzForm());
            //Application.Run(new CompaniesButtons());
            //Application.Run(new LoginForm());
            //Application.Run(new BuyTicket1());
        }
    }
}
