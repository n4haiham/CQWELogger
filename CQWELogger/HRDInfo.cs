using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CQWELogger
{
    public partial class HRDInfo : Form
    {
        CQWEUtilities util = new CQWEUtilities();

        public string radio { get; set; }
        public string context { get; set; }
        public string mode { get; set; }
        public string buttons { get; set; }
        public string dropdowns { get; set; }
        public string sliders  { get; set; }
        public string frequency { get; set; }
        

        public HRDInfo()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 2011.0 - save HRD status
            Properties.Settings.Default.HRDConfigured = txtProcessedMode.Text.Length > 0 && txtProcessedBand.Text.Length > 0;
            Properties.Settings.Default.Save();

            Close();
        }

        private void HRDInfo_Load(object sender, EventArgs e)
        {
            txtRadio.Text = radio;
            txtContext.Text = context;
            txtMode.Text = mode;
            txtButtons.Text = buttons;
            txtDropdowns.Text = dropdowns;
            txtSliders.Text = sliders;
            txtFreq.Text = frequency;

            txtProcessedBand.Text = util.GetCQWEBand(double.Parse(frequency)) ;
            txtProcessedMode.Text = util.GetCQWEMode(mode);
        }

    }
}
