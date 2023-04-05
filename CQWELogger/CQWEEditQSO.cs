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
    public partial class CQWEEditQSO : Form
    {
        public CQWEContact contact { get; set; }
        public List<CQWEContact> contacts { get; set; }
        public bool Dirty { get; set; }
        public bool CanceledOperation { get; set; }

        CQWEUtilities util = new CQWEUtilities();
        private bool canceling = false;

        public CQWEEditQSO()
        {
            InitializeComponent();
        }

        private void CQWEEditQSO_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Edit QSO information and click OK to save";

            if (contact != null)
            {
                GetDropdowns();

                txtCall.Text = contact.QSOStation;
                cboBands.Text = contact.QSOBand;
                cboModes.Text = contact.QSOMode;
                txtName.Text = contact.QSOName;
                cboLocations.SelectedText = contact.QSOLocation;
                numYears.Value = contact.QSOYearsOfService;
                dteQSO.Value = contact.QSODate;
                this.Text = String.Format("Edit QSO {0}", contact.QSONumber);
            }
        }
        private void GetDropdowns()
        {
            try
            {
                util.GetLocationsShort(cboLocations);
                util.GetBands(cboBands);
                util.GetModes(cboModes);
            }
            catch (Exception e)
            {
                // ?
            }
        }
        private void ChangeBand()
        {
            Dirty = true;
            try
            {
                cboBands.SelectedIndex++;
            }
            catch (Exception e)
            {
                cboBands.SelectedIndex = 0;
            }
        }

        private void ChangeMode()
        {
            Dirty = true;
            try
            {
                cboModes.SelectedIndex++;
            }
            catch (Exception e)
            {
                cboModes.SelectedIndex = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canceling = true;
            contact = null;
            Close();
        }

        private CQWEContact GetContactInfo()
        {
            CQWEContact returnContact = new CQWEContact();
            try
            {
                returnContact.QSOMode = cboModes.Text;
                returnContact.QSOBand = cboBands.Text;
                returnContact.QSOStation = txtCall.Text.ToUpper();
                returnContact.QSOName = txtName.Text.ToUpper();
                returnContact.QSOLocation = cboLocations.Text.ToUpper();
                returnContact.QSOYearsOfService = Convert.ToInt32(numYears.Value);
                returnContact.QSODate = dteQSO.Value;
                returnContact.QSONumber = contact.QSONumber;
                return returnContact;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void UpdateContactInfo()
        {
            try
            {
                contact.QSOMode = cboModes.Text;
                contact.QSOBand = cboBands.Text;
                contact.QSOStation = txtCall.Text.ToUpper();
                contact.QSOName = txtName.Text.ToUpper();
                contact.QSOLocation = cboLocations.Text.ToUpper();
                contact.QSOYearsOfService = Convert.ToInt32(numYears.Value);
                contact.QSODate = dteQSO.Value;

            }
            catch (Exception e)
            {
                // nothing
            }
        }


        private void DupeQSOMessage(string message)
        {
            MessageBox.Show(message,
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void MissingInfoMessage()
        {
            MessageBox.Show("One or more fields are blank." + Environment.NewLine + Environment.NewLine + "This contact will not be updated.",
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void MissingLocationMessage()
        {
            MessageBox.Show("Missing or illegal location." + Environment.NewLine + Environment.NewLine + "This contact will not be updated.",
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Media.SystemSounds.Exclamation.Play();
        }
        private void BadZZYearsMessage()
        {
            MessageBox.Show("Years of Service too high for ZZ." + Environment.NewLine + Environment.NewLine + "This contact will not be updated.", 
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Media.SystemSounds.Exclamation.Play();
        }
        private void BadYOSMessage()
        {
            MessageBox.Show(String.Format("Years of service cannot be greater than {0}." + Environment.NewLine + Environment.NewLine + "This contact will not be updated.", Properties.Settings.Default.MaximumYOS),
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Media.SystemSounds.Exclamation.Play();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Dirty)
            {
                CQWEContact editContact = GetContactInfo();

                if (util.CheckForDupe(editContact, contacts))
                {
                    DupeQSOMessage(String.Format("Dupe! This QSO will not be updated because it would create a duplicate." + Environment.NewLine + Environment.NewLine + 
                                    "Original QSO Date:{0}" + Environment.NewLine + 
                                    "QSO #{1}: {2} in {3} with {4} years",
                                    editContact.QSODate.ToString("u"), editContact.QSONumber.ToString(),
                                    editContact.QSOName, editContact.QSOLocation, editContact.QSOYearsOfService));
                    
                    contact = null;
                    return;
                }

                if (!util.LegalLocation(editContact.QSOLocation))
                {
                    MissingLocationMessage();
                    //System.Media.SystemSounds.Exclamation.Play();
                    contact = null; 
                    return;
                }

                if (util.MissingData(editContact))
                {
                    MissingInfoMessage();
                    //System.Media.SystemSounds.Exclamation.Play();
                    contact = null; 
                    return;
                }

                // ZZ max value
                if (!util.ZZYearsOK(editContact))
                {
                    BadZZYearsMessage();
                    //System.Media.SystemSounds.Exclamation.Play();
                    contact = null;
                    return;
                }

                if (editContact.QSOYearsOfService  > Properties.Settings.Default.MaximumYOS)
                {
                    BadYOSMessage();
                    contact = null;
                    return;
                }
            }

            UpdateContactInfo();
            Close();
        }

        private bool CheckYOS(int yearsOfService)
        {
            try
            {
                if (yearsOfService > Properties.Settings.Default.MaximumYOS)
                {
                    MessageBox.Show(String.Format("Years of service cannot be greater than {0}." + Environment.NewLine + Environment.NewLine + 
                        "QSO not saved.", Properties.Settings.Default.MaximumYOS),
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                    return false;
                }
                else
                    return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }

        private void txtCall_Leave(object sender, EventArgs e)
        {
            txtCall.Text = txtCall.Text.ToUpper().Trim();
            // Check for dupe!
            if (contacts != null)
            {

            }
        }

        private void cboBands_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        private void cboModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        private void txtCall_TextChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }


        private void CheckYears()
        {
            if (canceling) return;
            CQWEContact contact = GetContactInfo();
            // ZZ max value
            if (!util.ZZYearsOK(contact))
            {
                MessageBox.Show(String.Format("Years of service cannot be greater than {0}", util.GetMaxZZ()),
                  Application.ProductName,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Asterisk);
                numYears.Select(0, numYears.ToString().Length);
            }
        }
        private void cboLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dirty = true;
            //CheckYears();
        }


        private void numYears_ValueChanged(object sender, EventArgs e)
        {
            //CheckYears();
            //util.CheckYOS(Convert.ToInt32(numYears.Value));
        }

        private void numYears_Enter(object sender, EventArgs e)
        {
            numYears.Select(0, numYears.ToString().Length);
        }

        private void logginToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeBandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeBand();
        }

        private void changeModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMode();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper().Trim();
        }

        private void dteQSO_ValueChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        private void numYears_Leave(object sender, EventArgs e)
        {
            CheckYears();
        }

        private void numYears_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //util.CheckYOS(Convert.ToInt32(numYears.Value));
        }


        private void numYears_KeyDown(object sender, KeyEventArgs e)
        {
            //util.CheckYOS(Convert.ToInt32(numYears.Value));
        }

        private void CQWEEditQSO_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void CQWEEditQSO_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode  == Keys.F2)
                    ChangeBand();
                if (e.KeyCode == Keys.F3)
                    ChangeMode();
            }

            catch (Exception ex)
            {

            }
        }


        //private void ChangeBand()
        //{
        //    try
        //    {
        //        cboBands.SelectedIndex++;
        //        txtCall.Focus();
        //    }
        //    catch (Exception e)
        //    {
        //        cboBands.SelectedIndex = 0;
        //        txtCall.Focus();
        //    }
        //}

        //private void ChangeMode()
        //{
        //    try
        //    {
        //        cboModes.SelectedIndex++;
        //        txtCall.Focus();
        //    }
        //    catch (Exception e)
        //    {
        //        cboModes.SelectedIndex = 0;
        //        txtCall.Focus();
        //    }
        //}

    }
}
