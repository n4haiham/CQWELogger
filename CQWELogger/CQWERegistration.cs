using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using System.Xml;




namespace CQWELogger
{
    public partial class CQWERegistration : Form
    {
        private const string ENTER_INFO = "Enter registration information";
        public bool RegistrationOK { get; set; }
        private bool blnHitOK = false;

        CQWEUtilities util = null;//new CQWEUtilities();

        public CQWERegistration()
        {
            util = new CQWEUtilities();
            InitializeComponent();
            statusMessage.Text = ENTER_INFO;
            CheckSettingsFile();
            util.GetLocations(cboLocations);
            util.GetLicenseClasses(cboLicenseClass);

            PopulateSettings();
            ComputeExchange();
        }

        private bool CheckSettingsFile()
        {
            string settingsMessage = String.Empty;
            bool retVal = false;

            if (util.ValidateSettingsFile(out settingsMessage))
            {
                lblSettingsFile.ForeColor = System.Drawing.Color.Green;
                btnOK.Enabled = true;
                retVal = true;
            }
            else
            {
                lblSettingsFile.ForeColor = System.Drawing.Color.Red;
                btnOK.Enabled = false;
                retVal = false;
            }
            lblSettingsFile.Text = settingsMessage;
            return retVal;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            //blnHitOK = false;
            //if (MessageBox.Show("Are you sure that you want to close Registration?  Settings will not be saved.", ProductName, 
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
            //    MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            //    Close();                
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            numContestYear.Value = DateTime.UtcNow.Year;
            txtCall.Focus();
        }

        private void ShowAbout()
        {
            CQWEAbout aboutForm = new CQWEAbout();
            aboutForm.ShowDialog();
        }

        private bool PopulateSettings()
        {
            try
            {
                txtCall.Text =   Properties.Settings.Default.Callsign.Trim();
                txtFullName.Text = Properties.Settings.Default.FullName.Trim();
                txtFirstName.Text = Properties.Settings.Default.FirstName.Trim();
                cboLocations.SelectedItem = Properties.Settings.Default.StationLocation;
                numYears.Value = Properties.Settings.Default.Years;
                chkRetired.Checked = Properties.Settings.Default.Retired;
                chkCertificateWanted.Checked = Properties.Settings.Default.CertificateWanted;
                chkQRP.Checked = Properties.Settings.Default.QRP;
                chkDateOK.Checked = Properties.Settings.Default.TimeZoneConfirmed;
                cboLicenseClass.SelectedItem = Properties.Settings.Default.LicenseClass;
                numContestYear.Value = Properties.Settings.Default.ContestYear;
                txtWorkAddress.Text = Properties.Settings.Default.WorkAddress.Trim();
                txtWorkRoom.Text = Properties.Settings.Default.WorkRoom.Trim();
                txtWorkCity.Text = Properties.Settings.Default.WorkCity.Trim();
                txtWorkState.Text = Properties.Settings.Default.WorkState.Trim();
                txtWorkZIP.Text = Properties.Settings.Default.WorkZIP.Trim();
                txtWorkPhone.Text = Properties.Settings.Default.WorkPhone.Trim();

                txtHomeAddress.Text = Properties.Settings.Default.HomeAddress.Trim();
                txtHomeCity.Text = Properties.Settings.Default.HomeCity.Trim();
                txtHomeState.Text = Properties.Settings.Default.HomeState.Trim();
                txtHomeZIP.Text = Properties.Settings.Default.HomeZIP.Trim();
                txtHomePhone.Text = Properties.Settings.Default.HomePhone.Trim();
                txtHomeEmail.Text = Properties.Settings.Default.HomeEmail.Trim();

                txtExchange.Text = util.ComputeExchange();
                return true;
            }
            catch (Exception e)
            {
                //2012.0 - don't show
                //MessageBox.Show(String.Format("Error while reading values: {0}", e.ToString()), ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool SaveSettings()
        {
            try
            {
                Properties.Settings.Default.Callsign = txtCall.Text;
                Properties.Settings.Default.FullName = txtFullName.Text;
                Properties.Settings.Default.FirstName = txtFirstName.Text;
                Properties.Settings.Default.StationLocation = cboLocations.SelectedItem.ToString();
                Properties.Settings.Default.Years = Convert.ToInt32(numYears.Value);
                Properties.Settings.Default.Retired = chkRetired.Checked;
                Properties.Settings.Default.CertificateWanted = chkCertificateWanted.Checked;
                Properties.Settings.Default.QRP = chkQRP.Checked;
                Properties.Settings.Default.TimeZoneConfirmed = chkDateOK.Checked;
                Properties.Settings.Default.LicenseClass = cboLicenseClass.SelectedItem.ToString();
                Properties.Settings.Default.ContestYear = Convert.ToInt32(numContestYear.Value);
                Properties.Settings.Default.WorkAddress = txtWorkAddress.Text;
                Properties.Settings.Default.WorkRoom = txtWorkRoom.Text;
                Properties.Settings.Default.WorkCity = txtWorkCity.Text;
                Properties.Settings.Default.WorkState = txtWorkState.Text;
                Properties.Settings.Default.WorkZIP = txtWorkZIP.Text;
                Properties.Settings.Default.WorkPhone = txtWorkPhone.Text;
                Properties.Settings.Default.HomeAddress = txtHomeAddress.Text;
                Properties.Settings.Default.HomeCity = txtHomeCity.Text;
                Properties.Settings.Default.HomeState = txtHomeState.Text;
                Properties.Settings.Default.HomeZIP = txtHomeZIP.Text;
                Properties.Settings.Default.HomePhone = txtHomePhone.Text;
                Properties.Settings.Default.HomeEmail = txtHomeEmail.Text;

                Properties.Settings.Default.RegisteredDate = DateTime.Now;
                RegistrationOK = true;

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void txtCall_LostFocus(object sender, System.EventArgs e)
        {
            txtCall.Text = txtCall.Text.ToUpper();
        }

        private void ComputeExchange()
        {
            try
            {
                string location = cboLocations.SelectedItem.ToString();
                location = location.Substring(0, location.IndexOf(" "));

                txtExchange.Text = util.ComputeExchange(txtFirstName.Text, location, numYears.Value);
            }
            catch (Exception e)
            {
                txtExchange.Text = Properties.Settings.Default.MissingInfoMessage;
            }
        }

        private void numYears_ValueChanged(object sender, EventArgs e)
        {
            ComputeExchange();
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            ComputeExchange();
        }

        private void cboLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComputeExchange();
            try
            {
                if (cboLocations.SelectedItem.ToString().Substring(0, 2) == "ZZ")
                    numYears.Maximum = util.GetMaxZZ();
                else
                    numYears.Maximum = Properties.Settings.Default.MaximumYOS;  //2012.0 - replaced 50
            }
            catch (Exception ex)
            {

            }
        }

        private void RegistrationForm_Shown(object sender, EventArgs e)
        {
            txtCall.Focus();
        }


        private void txtFullName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtFirstName.Text.Length == 0)
                    txtFirstName.Text = txtFullName.Text.Substring(0, txtFullName.Text.IndexOf(" "));
            }
            catch (Exception err)
            {
                // nothing
            }
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void txtCall_Leave(object sender, EventArgs e)
        {
            txtCall.Text = txtCall.Text.ToUpper();
        }

        private bool ValidateForm()
        {
            bool validationError = false;

            if (!validationError && txtCall.Text.Length == 0)
            {
                statusMessage.Text = "Callsign is required";
                validationError = true;
            }
            if (!validationError && txtFullName.Text.Trim().Length == 0)
            {
                statusMessage.Text = "Name is required";
                validationError = true;
            }
            if (!validationError && txtFirstName.Text.Trim().Length == 0)
            {
                statusMessage.Text = "First Name is required";
                validationError = true;
            }
            if (!validationError && cboLocations.SelectedIndex < 0) 
            {
                statusMessage.Text = "Location is required";
                validationError = true;
            }

            if (!validationError && numYears.Value <= 0)
            {
                statusMessage.Text = "Number of years is required";
                validationError = true;
            }

            if (!validationError && cboLicenseClass.SelectedIndex < 0)
            {
                statusMessage.Text = "License Class is required";
                validationError = true;
            }

            if (!validationError && numContestYear.Value > DateTime.Now.Year)
            {
                statusMessage.Text = "Contest year cannot be in the future";
                validationError = true;
            }

            if (!validationError && chkDateOK.Checked == false)
            {
                statusMessage.Text = "Confirm your timezone and UTC";
                validationError = true;
            }

            if (validationError)
            {
                statusMessage.ForeColor = Color.Red;
                return false;
            }
            else
            {
                statusMessage.Text = ENTER_INFO;
                statusMessage.ForeColor = SystemColors.ControlText;
                return true;
            }
        }

        private void txtCall_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void txtFullName_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void cboLocations_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void numYears_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void cboLicenseClass_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void CQWERegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (blnHitOK)
            {
                bool validated = ValidateForm();
                e.Cancel = !validated;
                if (validated)
                    SaveSettings();
                    
            }
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            blnHitOK = true;
            Close();
        }

        private void numYears_Enter(object sender, EventArgs e)
        {
            numYears.Select(0, numYears.ToString().Length);

            try
            {
                if (cboLocations.SelectedItem.ToString().Substring(0, 2) == "ZZ")
                    numYears.Maximum = util.GetMaxZZ();
                else
                    //2012.0 - increased years of service max to 75
                    numYears.Maximum = Properties.Settings.Default.MaximumYOS;
            }
            catch (Exception ex)
            {
            }
        }

        // removed button, placed contest year into form load
        //private void btnNow_Click(object sender, EventArgs e)
        //{
        //    numContestYear.Value = DateTime.UtcNow.Year;
        //}




   }
}
