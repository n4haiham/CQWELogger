using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace CQWELogger
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
            CQWEUtilities util = new CQWEUtilities();

            if (!util.CheckSettingsFile())
            {
                MessageBox.Show(String.Format("There is a problem with the contest settings file ({0})." + Environment.NewLine + Environment.NewLine +
                    "This file is required for CQ-WE Logger to run." + Environment.NewLine + Environment.NewLine +
                    "Uninstall, and then re-install the application or locate this year's Settings file.", Properties.Settings.Default.ContestSettingsFile),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // run registration if not already registered; already registered means  "registered year = this year"
                if (Properties.Settings.Default.RegisteredDate.Year != DateTime.Now.Year)
                {
                    CQWERegistration regForm = new CQWERegistration();
                    Application.Run(regForm);
                    if (regForm.RegistrationOK)
                        Application.Run(new CQWELogMain());
                    else
                        MessageBox.Show("Registration not completed.  Exiting application.",
                            Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                    Application.Run(new CQWELogMain());
            }

        }

        /// <summary>
        /// Recursively create directory
        /// </summary>
        /// <param name="dirInfo">Folder path to create.</param>
        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists) dirInfo.Create();
        }

        private static bool IsAlreadyRegistered()
        {
            CQWEUtilities util = new CQWEUtilities();
            string validation = String.Empty;
            
            return (Properties.Settings.Default.Callsign.Length > 0) && 
                (util.ValidateSettingsFile(out validation));
        }
    }
}
