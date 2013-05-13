﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using GT.UI;

namespace SAEHaiku
{
    static class Program
    {
        static public Form1 mainForm;

        static public bool isDebug = false;

        public static int tableWidth = 1280;
        public static int tableHeight = 960;
                
        public static Point user1Origin = new Point(0, 0);
        public static Point user2Origin = new Point(0, 0);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Win32.AllocConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InputDialog d = new InputDialog("Connection details", "Which server:port ?", "localhost:9999");
            if (d.ShowDialog() != DialogResult.OK)
            {
                throw new InvalidOperationException();
            }
            string[] parts = d.Input.Split(':');
            string host = parts[0];
            string port = parts.Length > 1 ? parts[1] : "9999";

            PolhemusController polhemus = null;
            PhidgetController phidget = null;
            if (isDebug == false)
            {
                polhemus = new PolhemusController(false);
                phidget = new PhidgetController(polhemus);
            }
            
            mainForm = new Form1(polhemus, phidget, host, port);
            Application.Run(mainForm);
        }
    }
}
