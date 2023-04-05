using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Drawing;
//using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CQWELogger
{
    public partial class CQWETimeInfo : UserControl
    {
        public CQWETimeInfo()
        {
            InitializeComponent();
            StartGMTTimer();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetTimeInformation();
        }

        private void StartGMTTimer()
        {
            GetTimeInformation();
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void GetTimeInformation()
        {
            DateTime dt = DateTime.Now;
            DateTime dtUTC = DateTime.UtcNow;

            if (dt.IsDaylightSavingTime())
                txtTimeZone.Text = TimeZone.CurrentTimeZone.DaylightName.ToString();
            else
                txtTimeZone.Text = TimeZone.CurrentTimeZone.StandardName.ToString();

            txtCurrentTZ.Text = dt.ToString("");       // was s
            txtCurrentGMT.Text = dtUTC.ToString("u");
        }

        private void CQWETimeInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
