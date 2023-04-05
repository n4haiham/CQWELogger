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
    public partial class CQWESummary : Form
    {
        public List<string> locations { get; set; }
        public string summaryInfo { get; set; }
        public string mode { get; set; }
        public long score { get; set; }
        public long QSOCount { get; set; }
        public string caption { get; set; }

        public CQWESummary()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SummaryForm_Load(object sender, EventArgs e)
        {

            if (mode == null)
            {
                lblLocationsContacted.Text = caption; 
                lblModeStrip.Text = string.Empty;
                lblScoreStrip.Text = string.Empty;
                lblLocationsStrip.Text = string.Empty;
                lblQSOCount.Text = string.Empty; 
            }
            else
            {
                lblLocationsContacted.Text = caption;  //String.Format("Locations Contacted - {0}", mode);
                this.Text = String.Format("Current Statistics - {0}", mode);
                lblModeStrip.Text = String.Format("Mode: {0}", mode);
                lblScoreStrip.Text = String.Format("Score: {0}", score.ToString());
                lblLocationsStrip.Text = String.Format("Locations: {0}", locations.Count.ToString());
                lblQSOCount.Text = String.Format("QSO Count: {0}",  QSOCount.ToString());
            }
            
            lblLocationsContacted.SelectionStart = 0;
            lblLocationsContacted.SelectionLength = 0;

            txtModeLocationSummary.Text = summaryInfo;
            txtModeLocationSummary.SelectionStart = 0;
            txtModeLocationSummary.SelectionLength = 0;
            
        }



        private void lblLocationsContacted_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ignore typing into "multiline caption" (which is really a text box!)
            e.Handled = true;
        }

        private void CQWESummary_Shown(object sender, EventArgs e)
        {
            btnOK.Focus();
        }

        private void txtModeLocationSummary_KeyDown(object sender, KeyEventArgs e)
        {

            if (!(e.Control && e.KeyCode == Keys.C) || !(e.Control && e.KeyCode == Keys.A))
                e.Handled = true;
        }
    }
}
