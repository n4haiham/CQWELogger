using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using HrdWrapper;

namespace CQWELogger
{
    public partial class HRDSettings : Form
    {
        public HRDSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void SaveSettings()        
        {
            Properties.Settings.Default.HRDServer = txtServer.Text;
            Properties.Settings.Default.HRDPort = txtPort.Text;
            //Properties.Settings.Default.HRDConfigured = true;
            Properties.Settings.Default.Save();
        }

        private void GetSettings()
        {
            txtServer.Text = Properties.Settings.Default.HRDServer;
            txtPort.Text = Properties.Settings.Default.HRDPort;
        }

        private void HRDSettings_Load(object sender, EventArgs e)
        {
            GetSettings();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            CQWEUtilities util = new CQWEUtilities();

            string portValue = txtPort.Text.Trim();
            string serverValue = txtServer.Text.Trim();

            if (portValue.Length == 0 || serverValue.Length == 0)
            {
                MessageBox.Show("Both server and port are required.", 
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                ushort port = ushort.Parse(portValue);
                string server = serverValue;

                bool bConnect = HrdInterface.Connect(Properties.Settings.Default.HRDServer, port);

                if (bConnect)
                {
                    string radio = HrdInterface.SendMessage("Get Radio");
                    string context = HrdInterface.SendMessage("Get Context");
                    string buttons = HrdInterface.SendMessage("Get Buttons");
                    string dropdowns = HrdInterface.SendMessage("Get Dropdowns");
                    string sliders = HrdInterface.SendMessage("Get Sliders");
                    string freq = HrdInterface.SendMessage("Get Frequency", context);
                    double dFreq = Convert.ToDouble(freq);
                    
                    string mode = HrdInterface.SendMessage("get dropdown-text {{Mode}}", context);
                    if (mode.Length == 0)
                        mode = HrdInterface.SendMessage("get dropdown-text {{Main Mode}}", context);
                    if (mode.Length == 0)
                        mode = HrdInterface.SendMessage("get dropdown-text {{Mode A}}", context);
                    if (mode.Length == 0)
                        mode = HrdInterface.SendMessage("get dropdown-text {{Mode B}}", context);

                    this.Cursor = Cursors.Default;

                    HRDInfo info = new HRDInfo();
                    info.radio = radio;
                    info.context = context;
                    info.buttons = buttons;
                    info.dropdowns = dropdowns;
                    info.sliders = sliders;
                    info.frequency = freq;
                    info.mode = mode;
                    
                    info.ShowDialog();
                    
                    HrdInterface.Disconnect();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Could not connect to HRD", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }

            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error while testing HRD interface." + Environment.NewLine + Environment.NewLine +
                    "Error: " + err.ToString() + Environment.NewLine + Environment.NewLine + "Application path: " + Application.StartupPath,
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

    }
}
