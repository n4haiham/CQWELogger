using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Xml;
using System.IO;
using HrdWrapper;




namespace CQWELogger
{
    public partial class CQWELogMain : Form
    {
        CQWEUtilities util = new CQWEUtilities();
        List<CQWEContact> contacts = new List<CQWEContact>();
        const string ENTER_QSO_INFO = "Enter QSO Information";
        const string LOOKING_UP_CALL = "Looking up call sign...";
        CQWEContact wipedContact;

        public CQWELogMain()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            ShowHRDStatus();        //2012.0
        }


        private bool ConfirmLogFile()
        {
            try
            {
                if (Properties.Settings.Default.LogFile == String.Empty)
                {
                    string fileName = util.NewLogFile();

                    if (fileName == String.Empty)
                    {
                        lblLogFile.Text = "No log file";
                        return false;
                    }
                    
                    UpdateScore();
                   
                    Properties.Settings.Default.LogFile = fileName;
                    Properties.Settings.Default.Save();
                   
                    //UpdateLogFile(fileName);
                    UpdateLogFile(Properties.Settings.Default.LogFile);
                    
                    return true;
                }
                else
                {
                    string fileName = Properties.Settings.Default.LogFile; 
                    if (File.Exists(fileName))
                    {
                        UpdateLogFile(fileName);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Can't find log file \"{0}\"", Properties.Settings.Default.LogFile),
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);

                        // one more chance to create a log file
                        fileName = util.NewLogFile();
                        UpdateLogFile(fileName);
                        return (fileName.Length > 0);
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error opening log file: {0}", Environment.NewLine + Environment.NewLine + e.ToString()), 
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return false;
            }
        }

        private void UpdateLogFile(string fileName)
        {
            try
            {
                if (fileName.Length > 0)
                {
                    //lblLogFile.Text = fileName;
                    lblLogFile.Text = System.IO.Path.GetFileName(fileName);
                    lblLogFile.Tag = fileName;
                    lblLogFile.ForeColor = SystemColors.ControlText;
                }
                else
                {
                    lblLogFile.Text = "NO LOG FILE";
                    lblLogFile.ForeColor = Color.Red;
                }
            }
            catch (Exception e)
            {
                lblLogFile.Text = "Error: double click for more information";
                lblLogFile.Tag = String.Format("There is something wrong with the log file." + Environment.NewLine + Environment.NewLine + "Error:" + Environment.NewLine + "{0}", e.ToString());
                lblLogFile.ForeColor = Color.Red;
            }

        }

        private void SetLastUsedBandMode()
        {
            try
            {
                if (cboBands.Items.Count > 0)
                    cboBands.SelectedIndex = 0;
                if (cboModes.Items.Count > 0)
                    cboModes.SelectedIndex = 0;
                cboBands.Focus();


                cboBands.SelectedIndex = Properties.Settings.Default.LastBand;
                cboBands.SelectedItem = cboBands.SelectedIndex;

                cboModes.SelectedIndex = Properties.Settings.Default.LastMode;
            }
            catch (Exception e)
            {
                // do nothing
            }
        }

        private void LoggerForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            if (ConfirmLogFile())
            {
                util.GetLocationsShort(cboLocations);
                util.GetBands(cboBands);
                util.GetModes(cboModes);
                util.GetModeInformation();
                

                LoadLogFileInGrid();
                UpdateScore();

                Text = Application.ProductName + " - " + Properties.Settings.Default.Callsign;
                txtExchange.Text = util.ComputeExchange();
                ResetQSOMessage();
            }
            else
            {
                txtQSOMessage.ForeColor = Color.Red;
                txtQSOMessage.Text = "Logging disabled";
                txtQSOMessage.Tag = "Disabled";
 
                btnEnterContact.Enabled = false;
            }
            //2011.7
            dteQSO.Value = System.DateTime.UtcNow;

            // 2012.0 - warn about contest year if necessary!
            if (Properties.Settings.Default.ContestYear < System.DateTime.UtcNow.Year)
                MessageBox.Show(String.Format("You may need to change the contest year in your registration." + Environment.NewLine + Environment.NewLine +
                    "Contest Year: {0}" + Environment.NewLine + Environment.NewLine +
                    "This Year: {1}" + Environment.NewLine + Environment.NewLine +   
                    "To change the Conest Year, open the Registration window by selecting the File Menu and then selecting Registration or pressing Ctrl-R after closing this message. " + 
                    "In the Registration window, change the Contest Year if you intend to submit a log for this year's contest.  If you are logging a previous year " + 
                    "constest, you do not need to change the contest year.",Properties.Settings.Default.ContestYear, System.DateTime.UtcNow.Year),
                    ProductName, MessageBoxButtons.OK,  MessageBoxIcon.Information);
        }

        private void ResetQSOMessage()
        {
            txtQSOMessage.Text = ENTER_QSO_INFO;
            txtQSOMessage.ForeColor = SystemColors.ControlText;
            txtQSOMessage.Tag = "";
        }

        private void ClearFields(bool AskFirst, bool SaveWiped)
        {
            try
            {
                // 2012.7.28 - ask first?  only ask if there's something entered
                if ((txtCall.Text.Trim().Length > 0) || (txtName.Text.Trim().Length > 0) || (cboLocations.Text.Trim().Length > 0) || (numYears.Value > 0))
                {
                    bool ClearOK = true;

                    if (AskFirst)
                        if (MessageBox.Show("Are you sure that you want to clear this QSO?", "Confirm Clear",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                            ClearOK = false;

                    if (ClearOK)
                    {
                        CQWEContact thisWipedContact = null;

                        if (SaveWiped)
                            thisWipedContact = GetContactInfo();

                        txtCall.Text = String.Empty;
                        txtName.Text = String.Empty;
                        cboLocations.Text = String.Empty;
                        cboLocations.SelectedItem = null;
                        numYears.Value = 0;
                        numYears.Text = "0";

                        if (SaveWiped)
                            wipedContact = thisWipedContact;

                        ResetQSOMessage();
                    }
                }
                txtCall.Focus();

            }

            catch (Exception e)
            {
                // nothing                
            }

        }

        private CQWEContact GetContactInfo()
        {
            try
            {
                CQWEContact contact = new CQWEContact();

                if (cboModes.SelectedItem != null)
                    contact.QSOMode = cboModes.SelectedItem.ToString();

                if (cboBands.SelectedItem != null)
                    contact.QSOBand = cboBands.SelectedItem.ToString();

                contact.QSOStation = txtCall.Text.ToUpper();
                contact.QSOName = txtName.Text.ToUpper();
                contact.QSOLocation = cboLocations.Text.ToUpper();
                contact.QSOYearsOfService = Convert.ToInt32(numYears.Value);

                //if (dteQSO.Enabled)
                //2012.0 - now a menu
                //if (chkUseSystemTime.Checked)
                if (useSystemTimeWhenEnteringQSOToolStripMenuItem.Checked)
                    contact.QSODate = System.DateTime.UtcNow;
                else
                    contact.QSODate = dteQSO.Value;

                return contact;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void CheckYears()
        {
            CQWEContact contact = GetContactInfo();

            if (contact.QSOLocation == "ZZ")
            {   
                int maxZZYears = util.GetMaxZZ();
                if (contact.QSOYearsOfService > maxZZYears)
                    MissingInfoMessage(String.Format("Years of service cannot be greater than {0}", maxZZYears));
            }
            else
            {
                if (contact.QSOYearsOfService > Properties.Settings.Default.MaximumYOS)
                {
                    //2012
                    MissingInfoMessage(String.Format("Years of service cannot be greater than {0}", Properties.Settings.Default.MaximumYOS));
                }
            }

        }

        private void EnterContact()
        {

            //2012.7.28 - populate fields first, before checking for dupe
            PopulateFieldsBasedOnCall(txtCall, txtName, cboLocations, numYears);

            CQWEContact contact = GetContactInfo();

            if (CheckForDupe())
            {
                txtCall.Focus();
                return;
            }

            if (contact.QSOStation.Trim().Length == 0)
            {
                MissingInfoMessage("Missing Call Sign");
                txtCall.Focus();
                return;
            }

            if (contact.QSOName.Trim().Length == 0)
            {
                MissingInfoMessage("Missing Operator Name");
                txtName.Focus();
                return;
            }

            if (!util.LegalLocation(contact.QSOLocation))
            {
                MissingInfoMessage("Missing or Illegal Location");
                cboLocations.Focus();
                return;
            }

            if (contact.QSOYearsOfService == 0)
            {
                MissingInfoMessage("Missing Years Of Service");
                numYears.Focus();
                return;
            }
            
            // catch all, but should never get here
            if (util.MissingData(contact))
            {
                MissingInfoMessage("Missing Contact Information");
                return;
            }    
            
            // can't work yourself
            if (contact.QSOStation == Properties.Settings.Default.Callsign)
            {
                MissingInfoMessage("You cannot work yourself for points");
                txtCall.Focus();
                return;
            }

            // ZZ can't work another ZZ
            if (contact.QSOLocation == "ZZ" && util.GetLocationCode(Properties.Settings.Default.StationLocation) == "ZZ")
            {
                MissingInfoMessage("ZZ stations cannot work other ZZ stations");
                txtCall.Focus();
                return;
            }

            // ZZ max YOS value
            if (!util.ZZYearsOK(contact))
            {
                MissingInfoMessage(String.Format("Years cannot be greater than {0} for ZZ", util.GetMaxZZ()));
                numYears.Select(0, numYears.ToString().Length); 
                numYears.Focus();
                return;
            }

            // max years
            if (contact.QSOYearsOfService > Properties.Settings.Default.MaximumYOS)
            {
                MissingInfoMessage(String.Format("Years of service cannot be greater than {0}", Properties.Settings.Default.MaximumYOS));
                numYears.Select(0, numYears.ToString().Length); 
                numYears.Focus(); 
                return;
            }

            // QSO is OK to add
            contact.QSONumber = util.GetNextQSONumber(contacts);

            if (contacts == null)
                contacts = new List<CQWEContact>();

            // make sure all required info for contact exists before adding
            if (ValidateContact(contact))
                contacts.Add(contact);
            else
            {
                MissingInfoMessage("Missing or contact information");
               
                System.Media.SystemSounds.Exclamation.Play();
                return;
            }

            //2012.0 lblMessage.ForeColor = SystemColors.ControlText;
            //2012.0 lblMessage.Text = String.Format("{0} contact(s) logged", contacts.Count);
            
            UpdateList(contact);
            UpdateScore();
            SaveLogFile(false);
            ClearFields(false, false);
            ResetQSOMessage();
        
            txtCall.Focus();
        }


        private void MissingInfoMessage(string message)
        {
            try
            {
                txtQSOMessage.Text = message;
                txtQSOMessage.ForeColor = Color.Red;
                txtQSOMessage.Tag = "Missing";
                System.Media.SystemSounds.Exclamation.Play();
            }

            catch (Exception e)
            {

            }
        }

        //MissingLocationMessage


        private bool ValidateContact(CQWEContact contact)
        {
            if (contact.QSOBand.Length == 0)
                return false;
            if (contact.QSODate.ToString().Length == 0)
                return false;
            if (contact.QSOLocation.Length == 0)
                return false;
            if (contact.QSOMode.Length == 0)
                return false;
            if (contact.QSOName.Length == 0)
                return false;
            if (contact.QSOStation.Length == 0)
                return false;
            return true;
        }
        private void UpdateList(CQWEContact contact)
        {
            int row = dataGridLog.Rows.Add();
            dataGridLog.Rows[row].Cells[0].Value = contact.QSONumber;
            dataGridLog.Rows[row].Cells[1].Value = contact.QSOStation;
            dataGridLog.Rows[row].Cells[2].Value = contact.QSOBand;
            dataGridLog.Rows[row].Cells[3].Value = contact.QSOMode;
            dataGridLog.Rows[row].Cells[4].Value = contact.QSOName;
            dataGridLog.Rows[row].Cells[5].Value = contact.QSOLocation;
            dataGridLog.Rows[row].Cells[6].Value = contact.QSOYearsOfService;
            dataGridLog.Rows[row].Cells[7].ValueType = typeof(DateTime);
            dataGridLog.Rows[row].Cells[7].Value = contact.QSODate.ToString("u");
            DataGridViewColumn datGridColumnToSort = dataGridLog.Columns[7];

            dataGridLog.Sort(datGridColumnToSort, ListSortDirection.Descending);
            dataGridLog.Rows[0].Selected = true;
            
        }

        private void ClearDataGrid()
        {
            dataGridLog.Rows.Clear();
        }
        private void LoadLogFileInGrid()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ClearDataGrid();
                contacts = util.LoadLogFile();
                if (contacts != null)
                {
                    foreach (CQWEContact contact in contacts)
                    {
                        UpdateList(contact);
                    }

                    //2012.0 lblMessage.ForeColor = SystemColors.ControlText;
                    //2012.0 lblMessage.Text = "Log file opened";
                }
                else
                {
                    contacts = null;
                    //2012.0 lblMessage.ForeColor = Color.Red;
                    //2012.0 lblMessage.Text = "Log file is empty";
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SaveLogFile(bool showMessage)
        {
            if (util.SaveLogFile(contacts, Properties.Settings.Default.LogFile))
            {
                //2012.0 lblMessage.ForeColor = SystemColors.ControlText;
                //2012.0 lblMessage.Text = "Log file saved";
                if (showMessage)
                    MessageBox.Show(String.Format("Log saved as \"{0}\"", Properties.Settings.Default.LogFile), 
                        Application.ProductName, 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
            }
            else
            {
                //2012.0 lblMessage.ForeColor = Color.Red;
                //2012.0 lblMessage.Text = "Error saving log file";
                if (showMessage)
                    MessageBox.Show(String.Format("Log file \"{0}\" not saved", Properties.Settings.Default.LogFile),
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

 
        private void UpdateScore()
        {
            if (contacts == null)
                return;
            
            // make into dictionary
            long cw = util.ComputeScore("CW", contacts);
            long phone = util.ComputeScore("PH", contacts);
            long digi = util.ComputeScore("DIG", contacts);

            long cwLoc = util.ComputeLocations("CW", contacts).Count;
            long phoneLoc = util.ComputeLocations("PH", contacts).Count;
            long digiLoc = util.ComputeLocations("DIG", contacts).Count;


            lblCW.Text = "CW = " + cwLoc.ToString();
            lblPhone.Text = "PH = " + phoneLoc.ToString();
            lblDigital.Text = "DIG = " + digiLoc.ToString();

            lblCW.Text = string.Format("CW Locs:{0}", cwLoc, cw);
            lblPhone.Text = string.Format("PH Locs:{0}", phoneLoc, phone);
            lblDigital.Text = string.Format("DIG Locs:{0}", digiLoc, digi);

            lblScore.Text = "SCORE = " + (cw + phone + digi);
        }

        private void lblTime_Click(object sender, EventArgs e)
        {
            CQWETimeInfoForm stf = new CQWETimeInfoForm();
            stf.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CQWEAbout aboutForm = new CQWEAbout();
            aboutForm.ShowDialog();
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string userManualPath = String.Format("{0}\\{1}", Path.GetDirectoryName(Application.ExecutablePath), Properties.Settings.Default.UserManual);
                System.Diagnostics.Process.Start(userManualPath);
            }
            catch (Exception ex)
            { }
        }

        private void showTimeInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CQWETimeInfoForm stf = new CQWETimeInfoForm();
            stf.Show();
        }

        private void lblDigital_Click(object sender, EventArgs e)
        {

        }

        private void clearCurrentContactInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //2012.7.28 - don't ask for confirmation when using menu
            ClearFields(false, true);
        }

        private void enterContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnterContact();
        }

        private void btnEnterContact_Click(object sender, EventArgs e)
        {
            EnterContact();
        }


        private void PopulateFieldsBasedOnCall(TextBox txtCall, TextBox txtName, ComboBox cboLocations, NumericUpDown numYears)
        {
            //AutoPopulating = true;
            try
            {
                if (txtCall.Text.Length > 0 && txtName.Text.Length == 0 && cboLocations.Text.Length == 0 && numYears.Value == 0)
                {
                    
                    //2012.0 lblMessage.ForeColor = SystemColors.ControlText;
                    //2012.0 lblMessage.Text = LOOKING_UP_CALL;

                    foreach (CQWEContact contact in contacts)
                    {
                        if (txtCall.Text.ToUpper() == contact.QSOStation.ToUpper())
                        {
                            txtName.Text = contact.QSOName;
                            cboLocations.Text = contact.QSOLocation;
                            numYears.Value = Convert.ToInt32(contact.QSOYearsOfService);
                            // don't break; keep looking for contacts - may find a later one that has "correct" info 
                            // break;
                        }
                    }
                }
                else
                { 
                    // don't update
                }

                //2012.0 lblMessage.Text = String.Empty;
                //AutoPopulating = false;
            }
            catch (Exception e)
            {
                //AutoPopulating = false;
            }
        }

        private bool CheckForDupe()
        {
            try
            {
                if (contacts != null)
                {
                    // create a contact object based on currently logged in info
                    CQWEContact contact = GetContactInfo();

                    if (util.CheckForDupe(contact, contacts) == true)
                    {
                        System.Media.SystemSounds.Exclamation.Play(); Console.WriteLine("Dupe!");
                        //MissingInfoMessage("Dupe!");
                        //2012.0
                        MissingInfoMessage(String.Format("Dupe!       Date:{0} QSO #{1}: {2} in {3} with {4} years",
                            contact.QSODate.ToString("u"), contact.QSONumber.ToString(), contact.QSOName, contact.QSOLocation, contact.QSOYearsOfService));

                        //2012 
                        txtCall.Focus();
                        return true;
                    }
                }

                PopulateFieldsBasedOnCall(txtCall, txtName, cboLocations, numYears);

                //ResetQSOMessage();
                return false;
            }

            catch (Exception e)
            {
                MissingInfoMessage("Dupe");
                return false;
            }
            
        }

        private void txtCall_Leave(object sender, EventArgs e)
        {
            txtCall.Text = txtCall.Text.ToUpper().Trim();
            txtCall.Text = txtCall.Text.Replace(" ", String.Empty);

            // check for dupe only if there's no errors
            if (txtQSOMessage.Tag.ToString().Length == 0)
                CheckForDupe();            
        }


        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLogFileInGrid();
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLogFile(true);      // only place where we show status message
        }

        private void LoggerForm_Shown(object sender, EventArgs e)
        {

            //2012.0
            SetLastUsedBandMode();

        }

        private void txtCall_Validating(object sender, CancelEventArgs e)
        {
            txtCall.Text = txtCall.Text.ToUpper();
        }

        private void ChangeBand()
        {
            try
            {
                cboBands.SelectedIndex++;
                txtCall.Focus();
            }
            catch (Exception e)
            {
                cboBands.SelectedIndex = 0;
                txtCall.Focus();
            }
        }

        private void ChangeMode()
        {
            try
            {
                cboModes.SelectedIndex++;
                txtCall.Focus();
            }
            catch (Exception e)
            {
                cboModes.SelectedIndex = 0;
                txtCall.Focus();
            }
        }

        private void changeBandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeBand();
            //SetQSOTime();
        }

        private void cboBands_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetQSOMessage();
            //SetQSOTime();
            wipedContact = null;
        }

        private void cboModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetQSOMessage();
            //SetQSOTime();
            wipedContact = null;
        }

        private void deleteSelectedContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedContact();
        }

        private void LoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show(
                "Are you sure that you want to exit?",
                 Application.ProductName,
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question,
                 MessageBoxDefaultButton.Button2))
            {
                e.Cancel = true;
            }
            else
            {
                SaveLogFile(false);     // don't show success/error message since we're closing
                
                //2012.0 - save last used band/mod
                Properties.Settings.Default.LastBand = cboBands.SelectedIndex;
                Properties.Settings.Default.LastMode = cboModes.SelectedIndex;
                
                Properties.Settings.Default.HRDConfigured = hRDConnectionToolStripMenuItem.Checked;
                
                Properties.Settings.Default.Save();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedContact();
        }

        private void DeleteSelectedContact()
        {
            if (dataGridLog.SelectedRows.Count == 0)
                return;
            
            Int32 contactNumber = Convert.ToInt32(dataGridLog.SelectedRows[0].Cells[0].Value.ToString());

            if (contactNumber == 0)
                return;

            if (DialogResult.Yes == MessageBox.Show(
                String.Format("Are you sure that you want to delete contact {0}?" + Environment.NewLine + Environment.NewLine + "This cannot be undone.", contactNumber),
                "Delete",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question,
                 MessageBoxDefaultButton.Button2))
            {
                //2012.0 lblMessage.Text = "Deleting selected contact...";
                //2012.0 lblMessage.ForeColor = SystemColors.ControlText;

                CQWEContact contactToRemove = util.FindContactByNumber(contactNumber, contacts);
                if (contactToRemove != null)
                {
                    contacts.Remove(contactToRemove);
                    SaveLogFile(false);     // don't show success/error message since we're just doing a save after logging QSO
                    LoadLogFileInGrid();
                    UpdateScore();
                }
                else
                {
                    MessageBox.Show("Can't find contact to delete from database.  Reload the log", 
                        "Error", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation);
                }
            }
        }

        private CQWEContact FindContact(CQWEContact contactToFind)
        {
            foreach (CQWEContact contact in contacts)
            {
                if (contact.QSOStation == contactToFind.QSOStation &&
                    contact.QSOBand == contactToFind.QSOBand &&
                    contact.QSOMode == contactToFind.QSOMode)
                {
                    return contact;
                }
            }
            return null;
        }

        private CQWEContact FindDupe(CQWEContact contactToFind)
        {
            foreach (CQWEContact contact in contacts)
            {
                if ((contact.QSOStation == contactToFind.QSOStation &&
                    contact.QSOBand == contactToFind.QSOBand &&
                    contact.QSOMode == contactToFind.QSOMode) && contact.QSONumber != contactToFind.QSONumber)
                {
                    return contact;
                }
            }
            return null;
        }


        private void EditSelectedContact()
        {
            Int32 contactNumber = 0;

            try
            {

                if (dataGridLog.SelectedRows.Count == 0)
                    return;

                //2012.0 lblMessage.Text = "Editing selected contact...";
                //2012.0 lblMessage.ForeColor = SystemColors.ControlText;

                //CQWEContact contactFound = null;
                contactNumber = Convert.ToInt32(dataGridLog.SelectedRows[0].Cells[0].Value.ToString());
                CQWEContact contactToEdit = util.FindContactByNumber(contactNumber, contacts);

                CQWEEditQSO editForm = new CQWEEditQSO();
                editForm.contact = contactToEdit;
                editForm.contacts = contacts;
                editForm.ShowDialog();

                if (editForm.contact == null)
                {
                    //2012.0 lblMessage.Text = String.Format("Changes not made to QSO {0}", contactNumber);
                    //2012.0 lblMessage.ForeColor = Color.Red;
                }
                else
                {
                    if (editForm.Dirty == true)
                    {
                        SaveLogFile(false);
                        LoadLogFileInGrid();
                        UpdateScore();
                     }
 
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Error while editing:" + Environment.NewLine + Environment.NewLine + "{0}", e.ToString()),
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                //2012.0 lblMessage.Text = String.Format("Changes not made to QSO {0}", contactNumber);
                //2012.0 lblMessage.ForeColor = Color.Red;
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedContact();
            txtCall.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void cboLocations_Leave(object sender, EventArgs e)
        {
            cboLocations.Text = cboLocations.Text.ToUpper();
            CheckYears();
        }


        private void numYears_Enter(object sender, EventArgs e)
        {
            numYears.Select(0, numYears.ToString().Length);
        }


        private void ExportADIF()
        {
            //http://www.hosenose.com/adif/adif.html
            //http://adif.org/adif227.htm
            //SSB, CW, RTTY, TOR=AMTOR, PKT, AM, FM, SSTV, ATV, PAC=PACTOR,CLO=CLOVER

            if (contacts == null)
                MessageBox.Show("Nothing to export", "ADIF Export", MessageBoxButtons.OK,
                   MessageBoxIcon.Asterisk);
            else
            {
                try
                {
                    // get file name
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "ADI|*.adi";
                    dialog.Title = "Export ADIF File for this year's contest";
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    dialog.FileName = util.BuildADIFileName();
                    DialogResult result = dialog.ShowDialog();
                    string adiFileName = dialog.FileName;

                    if (adiFileName.Length > 0 && result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        long outOfRange = 0;
                        StreamWriter outFile = new StreamWriter(dialog.FileName);
                        long linesWritten = 0;

                        //2012.0
                        string headerLine = String.Format("<STATION_CALLSIGN:{0}>", Properties.Settings.Default.Callsign.Length) + Properties.Settings.Default.Callsign + Environment.NewLine;
                        headerLine += String.Format("<MY_NAME:{0}>", Properties.Settings.Default.FullName.Length) + Properties.Settings.Default.FullName + Environment.NewLine;
                        headerLine += String.Format("<MY_STREET:{0}>", Properties.Settings.Default.HomeAddress.Length) + Properties.Settings.Default.HomeAddress + Environment.NewLine;
                        headerLine += String.Format("<MY_CITY:{0}>", Properties.Settings.Default.HomeCity.Length) + Properties.Settings.Default.HomeCity + Environment.NewLine;
                        headerLine += String.Format("<MY_STATE:{0}>", Properties.Settings.Default.HomeState.Length) + Properties.Settings.Default.HomeState + Environment.NewLine;
                        headerLine += String.Format("<MY_POSTAL_CODE:{0}>", Properties.Settings.Default.HomeZIP.Length) + Properties.Settings.Default.HomeZIP + Environment.NewLine;
                        headerLine += String.Format("<APP_CQWE_VERSION:{0}>", Application.ProductVersion.Length) + Application.ProductVersion + Environment.NewLine;

                        outFile.WriteLine(headerLine);

                        foreach (CQWEContact contact in contacts)
                        {
                            if (util.QSODateInRange(contact.QSODate))
                            {
                                //<call:6>WN4AZY<band:3>20M<mode:4>RTTY<qso_date:8>19960513<time_on:4>1305<eor>
                                string logLine = "<call:" + contact.QSOStation.Length + ">" + contact.QSOStation;
                                logLine += "<band:" + contact.QSOBand.Length + ">" + contact.QSOBand;
                                logLine += "<mode:" + contact.QSOMode.Length + ">" + contact.QSOMode;
                                logLine += "<qso_date:8>" + contact.QSODate.ToString("yyyyMMdd");
                                logLine += "<time_on:4>" + contact.QSODate.ToString("HHmm");
                                
                                //2012.0
                                logLine += String.Format("<APP_CQWE_NAME:{0}>", contact.QSOName.Length) + contact.QSOName;
                                logLine += String.Format("<APP_CQWE_LOCATION:{0}>", contact.QSOLocation.Length) + contact.QSOLocation;
                                logLine += String.Format("<APP_CQWE_YEARS:{0}>",contact.QSOYearsOfService.ToString().Length)  + contact.QSOYearsOfService;
                                

                                logLine += "<eor>";
                                outFile.WriteLine(logLine);
                                linesWritten++;
                            }
                            else
                            {
                                outOfRange++;
                            }
                        }
                        outFile.Close();

                        MessageBox.Show(String.Format("Wrote {0} QSO(s) to ADIF file \"{1}\"", linesWritten.ToString(), adiFileName),
                             "Finished ADIF export",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (outOfRange > 0)
                            MessageBox.Show(String.Format("{0} log entries are not from contest year {1} and were not written to the ADIF file.", outOfRange, Properties.Settings.Default.ContestYear),
                              Application.ProductName,
                              MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    }
                    // no message if user canceled
                }
                catch (Exception e)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(String.Format("Error while saving ADIF file" + Environment.NewLine + Environment.NewLine + "{0}", e.ToString()), Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void exportADIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportADIF();
        }

        private void mnuDigiStats_Click(object sender, EventArgs e)
        {
           ShowSummary("DIG");
        }

        private void mnuCWStats_Click(object sender, EventArgs e)
        {
            ShowSummary("CW");
        }

        private void mnuPhoneStats_Click(object sender, EventArgs e)
        {
            ShowSummary("PH");
        }

        private void submitLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void changeModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMode();
        }

        private void SaveLogAs()
        {

            string retFile = util.BackupLogFile(contacts);

            if (retFile.Length > 0)
            {
                //2012.0 lblMessage.Text = "Log backed up";  //Path.GetFileName(retFile);
                //2012.0 lblMessage.ForeColor = SystemColors.ControlText;
                MessageBox.Show(String.Format("Log backed up as:" + Environment.NewLine + Environment.NewLine + "{0}", retFile), 
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                //2012.0 lblMessage.Text = "Error backing up log!";
                //2012.0 lblMessage.ForeColor = Color.Red;
            }
        }

        private void backupLogAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLogAs();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CQWERegistration regForm = new CQWERegistration();
            regForm.ShowDialog();
            
            Text = Application.ProductName + " - " + Properties.Settings.Default.Callsign;
            txtExchange.Text = util.ComputeExchange();
        }

        private void editSelectedContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedContact();
        }

 

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper().Trim();
            txtName.Text = txtName.Text.Replace(" ", String.Empty);
        }


        private void overallStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string summaryInfo = util.CreateSummary(contacts);
                CQWESummary form = new CQWESummary();
                form.caption = Properties.Settings.Default.SummaryInfo;
                form.summaryInfo = summaryInfo;
                form.score = util.ComputeLocations("", contacts).Count;
                form.ShowDialog();
            }

            catch (Exception ex)
            {

            }
        }

        private void hRDConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hRDConnectionToolStripMenuItem.Checked = !hRDConnectionToolStripMenuItem.Checked;

            bool enableBandMode = !hRDConnectionToolStripMenuItem.Checked;

            hRDConnectionSetupToolStripMenuItem.Enabled = enableBandMode;
            changeBandToolStripMenuItem.Enabled = enableBandMode;
            changeModeToolStripMenuItem.Enabled = enableBandMode;
            cboBands.Enabled = enableBandMode;
            cboModes.Enabled = enableBandMode;
            lblBand.Enabled = enableBandMode;
            lblMode.Enabled = enableBandMode;
            //grpBandMode.Enabled = enableBandMode; 2011.7

            ShowHRDStatus(enableBandMode);

        }

        private void ShowHRDStatus()
        {
            ShowHRDStatus(!hRDConnectionToolStripMenuItem.Checked);
        }

        private void ShowHRDStatus(bool enableBandMode)
        {
            // 2012.0 - warn if HRD is not turned on
            if (Properties.Settings.Default.HRDConfigured)
            {

                // 2011.7
                if (enableBandMode)
                {
                    txtHRDInterfaceStatus.Text = "HRD Interface Off";
                    txtHRDInterfaceStatus.ForeColor = Color.Red;
                }
                else
                {
                    txtHRDInterfaceStatus.Text = "HRD Interface On";
                    txtHRDInterfaceStatus.ForeColor = Color.Black;
                }
            }
            else
            {
                txtHRDInterfaceStatus.Text = String.Empty;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.UtcNow;

            lblTime.Text = String.Format("{0}:{1} GMT", now.Hour.ToString("00"), 
                                                        now.Minute.ToString("00"));
            if (hRDConnectionToolStripMenuItem.Checked)
                UpdateFreqMode();

            // 2011.7 - keep on updating date/time unless we're letting the user put in date/time
            //if (chkUseSystemTime.Checked)
            // 2012.0 - now a menu
            if (useSystemTimeWhenEnteringQSOToolStripMenuItem.Checked)
                dteQSO.Value = now;
        }


        private void UpdateFreqMode()
        {
            try
            {
                ushort port = ushort.Parse(Properties.Settings.Default.HRDPort);
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
                    // 2013.3 - this is also in the HRD settings window 
                    if (mode.Length == 0)
                        mode = HrdInterface.SendMessage("get dropdown-text {{Mode A}}", context);
                    if (mode.Length == 0)
                        mode = HrdInterface.SendMessage("get dropdown-text {{Mode B}}", context);


                    if (dFreq > 0)
                    {
                        string thisBand = util.GetCQWEBand(dFreq);
                        int bandPosition = cboBands.FindStringExact(thisBand);
                        if (bandPosition >= 0)
                        {
                            cboBands.SelectedIndex = bandPosition;
                            lblMessage.Text = "Reading HRD \"band\" and \"mode\"";
                            lblMessage.ForeColor = Color.Black;
                        }
                        else
                        {
                            lblMessage.Text = "HRD \"band\" doesn't make sense";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }

                    if (mode.Length > 0)
                    {
                        string thisMode = util.GetCQWEMode(mode);
                        int modePosition = cboModes.FindStringExact(thisMode);
                        if (modePosition  >= 0)
                            cboModes.SelectedIndex = modePosition;
                        else
                        {
                            lblMessage.Text = "HRD \"mode\" doesn't make sense";
                            lblMessage.ForeColor = Color.Red; 
                        }
                    }

                    HrdInterface.Disconnect();
                }
            }
            catch (Exception e)
            {

            }
        }

        
        private void ShowSummary(string mode)
        {
            CQWESummary form = new CQWESummary();
            form.caption = String.Format("Locations Contacted - {0}", mode);
            form.mode = mode;
            form.score = util.ComputeScore(mode, contacts);
            form.locations = util.ComputeLocations(mode, contacts);
            form.QSOCount = util.ComputeQSO(mode, contacts);
            form.summaryInfo = util.LocationSummary(mode, contacts, false);
            form.ShowDialog();
        }

        private void ShowLocationSummary()
        {
            try
            {
                CQWESummary form = new CQWESummary();
                string allLocations = "";

                Dictionary<string, string> locations = util.GetLocations();

                // locations sent as string - will never be *that* long!
                foreach (KeyValuePair<string, string> location in locations)
                {
                    allLocations += location.Key + " - " + location.Value + Environment.NewLine;
                }
                form.summaryInfo = allLocations;
                form.score = locations.Count;
                form.caption = "Valid CQ-WE locations";

                form.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error while showing summary: {0}", e.ToString()), 
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void showLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLocationSummary();
        }

        private void hRDConnectionSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HRDSettings form = new HRDSettings();
            form.ShowDialog();
            ShowHRDStatus();        //2012.0
        }

        private void cboLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            wipedContact = null;
            //SetQSOTime();
            //2012.7.28 CheckForDupe();
            //2012.7.28 CheckYears();
        }

        private void numYears_ValueChanged(object sender, EventArgs e)
        {
            wipedContact = null;
            //SetQSOTime();
            //2012.7.28 CheckForDupe();
            //2012.7.28 CheckYears();
        }


        private void submitScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string instructions = util.GetLogSubmissionInstructions();
            
            if (instructions.Length == 0)
                instructions = "No instructions found";

            MessageBox.Show(instructions, Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void txtCall_KeyPress(object sender, KeyPressEventArgs e)
        {
            ResetQSOMessage();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ResetQSOMessage();
        }

        private void cboLocations_KeyPress(object sender, KeyPressEventArgs e)
        {
            ResetQSOMessage();
        }

        private void numYears_KeyPress(object sender, KeyPressEventArgs e)
        {
            ResetQSOMessage();
        }

        private void dteQSO_KeyPress(object sender, KeyPressEventArgs e)
        {
            ResetQSOMessage();
        }

        private void dteQSO_ValueChanged(object sender, EventArgs e)
        {
            //CheckForDupe(); 
        }

        private void numYears_Leave(object sender, EventArgs e)
        {
            //CheckForDupe();
            CheckYears();
        }


        private void lblLogFile_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("The complete path to the {0} log is: " + Environment.NewLine + Environment.NewLine + "{1}", Application.ProductName, lblLogFile.Tag.ToString()), 
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridLog_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedContact();
            //txtCall.Focus();
        }

        private void newLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (util.NewLogFile().Length > 0)
                {
                    contacts = null;        // clear out log!
                    SaveLogFile(false);
                    ConfirmLogFile();
                    ClearDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error creating new log: {0}", ex.ToString()), Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void openLogToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                util.OpenLogFile();
                ConfirmLogFile();
                LoadLogFileInGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error opening log: {0}", ex.ToString()), Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        //2012.0 - changed to menu
        private void chkUseSystemTime_CheckedChanged(object sender, EventArgs e)
        {
          //  dteQSO.Enabled = !(chkUseSystemTime.Checked);

        }

        private void lblPhone_Click(object sender, EventArgs e)
        {

        }

        private void txtCall_Enter(object sender, EventArgs e)
        {
            try
            {
                txtCall.SelectAll();
            }
            catch (Exception ex)
            {
            
            }
        }

        private void numYears_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void cboLocations_Enter(object sender, EventArgs e)
        {
            cboLocations.SelectAll();//(0, cboLocations.Text.Length);
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            try
            {
                txtName.SelectAll();
            }
            catch (Exception ex)
            { }
        }


        //2012.0
        private void useSystemTimeWhenEnteringQSOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useSystemTimeWhenEnteringQSOToolStripMenuItem.Checked = !useSystemTimeWhenEnteringQSOToolStripMenuItem.Checked;
        }

        private void loggingToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
           // dteQSO.Enabled = !(chkUseSystemTime.Checked);

        }
        //2012.0
        private void useSystemTimeWhenEnteringQSOToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            dteQSO.Enabled = !(useSystemTimeWhenEnteringQSOToolStripMenuItem.Checked);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields(true, true);      // ask first
        }

        private void txtHRDInterfaceStatus_Enter(object sender, EventArgs e)
        {
            //txtCall.Focus();
        }

        private void txtCall_TextChanged_2(object sender, EventArgs e)
        {
            try
            {
                wipedContact = null;
                if (txtCall.Text.Length > 6)
                    toolTip1.SetToolTip(txtCall, txtCall.Text);
                else
                    toolTip1.SetToolTip(txtCall, "Contacted station call sign");
            }
            catch (Exception ex)
            { }

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                wipedContact = null;
                if (txtName.Text.Length > 15)
                    toolTip1.SetToolTip(txtName, txtName.Text);
                else
                    toolTip1.SetToolTip(txtCall, "Contacted station Operator Name");
            }
            catch (Exception ex)
            { }

        
        }

        private void undoWipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wipedContact != null)
            {
                CQWEContact thisWipedContact = wipedContact;

                txtCall.Text = thisWipedContact.QSOStation;
                cboBands.Text = thisWipedContact.QSOBand;
                cboModes.Text = thisWipedContact.QSOMode;
                txtName.Text = thisWipedContact.QSOName;
                cboLocations.SelectedText = thisWipedContact.QSOLocation;
                numYears.Value = thisWipedContact.QSOYearsOfService;
            }
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            ClearFields(false,true);    // do NOT ask first
        }

        private void btnEnterContact_Leave_1(object sender, EventArgs e)
        {
            // put cursor back to call sign since we've logged this one and cleared out 
            txtCall.Focus();
        }

    }
}
