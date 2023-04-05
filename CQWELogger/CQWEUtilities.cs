using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace CQWELogger
{
    class CQWEUtilities
    {
        const string MSG_CANT_USE_SETTINGS = "The Contest Settings file is used to store this year's contest particulars.  " +
                            "You cannot use the contest settings file as your log file.  ";

        List<string> modesCW = null;
        List<string> modesPH = null;
        List<string> modesDIG = null;
        Dictionary<string, string> locationList = null;

        public long GetNextQSONumber(List<CQWEContact> contacts)
        {
            long maximumQSONumber = 0;

            if (contacts == null)
                return 1;
            else
            {
                foreach (CQWEContact contact in contacts)
                {
                    if (contact.QSONumber > maximumQSONumber)
                        maximumQSONumber = contact.QSONumber;
                }
                return ++maximumQSONumber;
            }
        }


        public bool CheckForDupe(CQWEContact qso, List<CQWEContact> contacts)
        {
            if (contacts == null || qso == null)
                return false;

            bool foundDupe = false;

            // brute force method of finding dupes, but the logs will never be THAT big.
            foreach (CQWEContact contact in contacts)
            {
                // 2012: only look at this year's log - don't look at previous year's contacts
                //if (contact.QSODate.Year == DateTime.Now.Year)
                    if (contact.QSODate.Year == Properties.Settings.Default.ContestYear)
                    if (contact.QSONumber != qso.QSONumber)
                        if (qso.QSODate.Year == contact.QSODate.Year &&        // checking year in case there are older contacts in the log.
                            qso.QSOStation == contact.QSOStation &&
                            qso.QSOBand == contact.QSOBand)
                            // now see if the band is in the same group; little optimization here, but not much
                            if (GetModeGroup(qso.QSOMode) == GetModeGroup(contact.QSOMode))
                            {
                                //2012.0
                                qso.QSOName = contact.QSOName;
                                qso.QSONumber = contact.QSONumber;
                                qso.QSOLocation = contact.QSOLocation;
                                qso.QSOYearsOfService = contact.QSOYearsOfService;
                                qso.QSODate = contact.QSODate;
                                qso.QSOBand = contact.QSOBand;
                                qso.QSOMode = contact.QSOMode;
                                qso.QSOStation = contact.QSOStation;
                                qso.QSOValidated = contact.QSOValidated;
                            
                                foundDupe = true;
                                break;
                            }
            }
            return foundDupe;

        }

        public bool CheckSettingsFile()
        {
            try
            {
                string destFile = Properties.Settings.Default.ContestSettingsFile;

                // see if we have the setting for the contest settings file *AND* the file exists 
                if (destFile.Length > 0)
                    if (File.Exists(destFile))
                    {
                        // check settings file date for *existing* file
                        return CheckSettingsFileDate();
                    }

                // either: file name is not populated OR file does not exist; compute the settings file name
                destFile =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + 
                    Properties.Settings.Default.ContestSettingsFileDefault;

                // get the contents of the resource XML file and save to the computed file name
                byte[] b1 = System.Text.Encoding.UTF8.GetBytes(Properties.Resources.CQWEContestSettings);
                File.WriteAllBytes(destFile, b1);
                
                // Save the path to the contest settings file
                Properties.Settings.Default.ContestSettingsFile = destFile;
                Properties.Settings.Default.Save();

                // check settings file date for file that was just saved
                return CheckSettingsFileDate();
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Problem while checking the contest settings file: \r\n\r\n{0}", e.ToString(), Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Stop));
                return false;
            }
        }

        public string GetLocationCode(string location)
        {
            try 
            {
                return location.Substring(0, location.IndexOf(" "));
            }
            catch (Exception e)
            {
                return "";
            }
        }
 
        public bool MissingData(CQWEContact contact)
        {
            try
            {
                if (contact.QSOBand.Length == 0)    // should check to be sure its in the list?
                    return true;
                if (contact.QSOMode.Length == 0)
                    return true;

                if (contact.QSOStation.Length == 0)
                    return true;
                if (contact.QSOName.Length == 0)
                    return true;
                if (!LegalLocation(contact.QSOLocation))
                    return true;
                if (contact.QSOYearsOfService == 0)
                    return true;
                if (contact.QSODate == null)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ZZYearsOK(CQWEContact contact)
        {
            try
            {
                if (contact != null)
                    if (contact.QSOLocation == "ZZ")
                        return contact.QSOYearsOfService <= GetMaxZZ();

                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
            
        }

        public bool CheckSettingsFileDate()
        {
            try
            {
                string settingsFileDate = "";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList settingsNode = xmlDoc.SelectNodes("Settings");
                foreach(XmlNode node in settingsNode)
                {
                    settingsFileDate = node.Attributes["date"].Value;       // loops through all Settings; uses last date found; shouldn't be more than 1 though
                }
                
                //DateTime dt = Convert.ToDateTime(settingsFileDate);
                //if (dt.Year != DateTime.Now.Year)
                //{
                //    MessageBox.Show("Settings file is out of date.  Download the latest program or CQWEContestSettings.xml file before running this program.", 
                //        Application.ProductName,
                //        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Problem while checking the date of contest settings file.  " + 
                    "The file must be up to date before this program will run. \r\n\r\n{0}", e.ToString(), 
                    Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Stop));

                return false;
            }
        }

        public string ComputeExchange()
        {
            string missingInfo = Properties.Settings.Default.MissingInfoMessage;

            try
            {
                string firstName = Properties.Settings.Default.FirstName;
                string location = GetLocationCode(Properties.Settings.Default.StationLocation);
                int yearsOfService = Properties.Settings.Default.Years;
                return (ComputeExchange(firstName, location, yearsOfService));
            }
            catch (Exception e)
            {
                return missingInfo;
            }

        }

        public string ComputeExchange(string firstName, string location, decimal yearsOfService)
        {
            string missingInfo = Properties.Settings.Default.MissingInfoMessage;

            try
            {
                if (firstName.Length > 0 && yearsOfService > 0 && location.Length > 0)
                    return String.Format("{0} {1} {2}", firstName, location, yearsOfService);
            }
            catch (Exception e)
            {
                return missingInfo;
            }

            return missingInfo;

        }

        public string GetModeGroup(string mode)
        {
            if (modesCW == null || modesPH == null || modesDIG == null)
                GetModeInformation();

            if (modesCW.IndexOf(mode) >= 0)
                return "CW";

            if (modesPH.IndexOf(mode) >= 0)
                return "PH";

            if (modesDIG.IndexOf(mode) >= 0)
                return "DIG";

            return String.Empty;
        }

        public void GetModeInformation()
        {
            modesCW = GetModesForGroup("CW");
            modesPH = GetModesForGroup("PH");
            modesDIG = GetModesForGroup("DIG");
        }

        public bool LegalLocation(string qsoLocation)
        {
            try
            {
                string qsoLocationValue = null;
                return GetLocations().TryGetValue(qsoLocation, out qsoLocationValue); 
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public Dictionary<string, string> GetLocations()
        {
            //Dictionary<string, string> locationList = new Dictionary<string, string>();
            try
            {
                if (locationList != null)
                    return locationList;
                else
                {
                    locationList = new Dictionary<string, string>();
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);
                    XmlNodeList locations = xmlDoc.SelectNodes("Settings/Locations/Location");
                    foreach (XmlNode node in locations)
                    {
                        XmlNode locationName = node.SelectSingleNode("LocationName");
                        locationList.Add(node.Attributes["id"].Value, locationName.InnerText);
                    }
                    return locationList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public string GetLogSubmissionInstructions()
        {
            string ret = "";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList instructions = xmlDoc.SelectNodes("Settings/Instructions/LogSubmission");
                foreach (XmlNode node in instructions)
                {
                    XmlNode instruction = node.SelectSingleNode("instructions");
                    ret += node.Attributes["instructions"].Value + Environment.NewLine;
                }
                return string.Format(ret, Properties.Settings.Default.LogFile);
                //return ret;
            }
            catch (Exception e)
            {
                return "Error";
            }
        }

        public void GetLocations(ComboBox cboLocations)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList locations = xmlDoc.SelectNodes("Settings/Locations/Location");
                foreach (XmlNode node in locations)
                {
                    XmlNode locationName = node.SelectSingleNode("LocationName");
                    cboLocations.Items.Add(node.Attributes["id"].Value + " - " + locationName.InnerText);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Problem loading location codes: {0}", e.ToString()),
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void GetLocationsShort(ComboBox cboLocations)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList locations = xmlDoc.SelectNodes("Settings/Locations/Location");
                foreach (XmlNode node in locations)
                {
                    cboLocations.Items.Add(node.Attributes["id"].Value);
                }

            }
            catch (Exception e)
            {
                cboLocations.Items.Add("Error");
            }
        }
        public void GetBands(ComboBox cboBands)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList locations = xmlDoc.SelectNodes("Settings/Bands/Band");
                foreach (XmlNode node in locations)
                {
                    cboBands.Items.Add(node.Attributes["id"].Value);
                    // default?
                }

            }
            catch (Exception e)
            {
                cboBands.Items.Add("Error");

            }
        }

        public void GetLicenseClasses(ComboBox cboLicenseClasses)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList licenseClasses = xmlDoc.SelectNodes("Settings/LicenseClasses/LicenseClass");
                foreach (XmlNode licenseClass in licenseClasses)
                {
                    cboLicenseClasses.Items.Add(licenseClass.Attributes["id"].Value);
                }

            }
            catch (Exception e)
            {
                cboLicenseClasses.Items.Add("Error");
            }
        }

        public void GetModes(ComboBox cboModes)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList modegroups = xmlDoc.SelectNodes("Settings/ModeGroups/ModeGroup");
                foreach (XmlNode mode in modegroups)
                {
                    foreach (XmlNode modenode in mode)
                    {
                        cboModes.Items.Add(modenode.Attributes["id"].Value);
                    }
                }

            }
            catch (Exception e)
            {
                cboModes.Items.Add("Error");
            }
        }

        public List<string> GetModesForGroup(string groupName)
        {
            List<string> modeList = new List<string>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);

                XmlNodeList modegroups = xmlDoc.SelectNodes("Settings/ModeGroups/ModeGroup");

                foreach (XmlNode mode in modegroups)
                {
                    if (mode.Attributes["id"].Value == groupName)
                    {
                        foreach (XmlNode modenode in mode)
                        {
                            modeList.Add(modenode.Attributes["id"].Value);
                        }
                    }
                }
                return modeList;
            }
            catch (Exception e)
            {
                //
            }


            return modeList;
        }



        public bool ValidateSettingsFile(out string validation)
        {
            try
            {
                string validationFileDate = String.Empty;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Properties.Settings.Default.ContestSettingsFile);
                XmlNodeList reg = xmlDoc.GetElementsByTagName("Settings");
                validationFileDate = reg[0].Attributes["date"].Value;
                DateTime dt = Convert.ToDateTime(validationFileDate);
                string formattedDate = String.Format("{0:MM/dd/yyyy}", dt);

                //if (dt.Year == DateTime.Now.Year)
                //{
                //    //validation = "Setting file is up to date: " + formattedDate;  
                //    validation = String.Format("Location data is up to date for the {0} contest", dt.Year); 
                validation = String.Format("Settings file is dated {0}", dt.Year);
                return true;
                //}
                //else
                //{
                //    validation = String.Format("Setting file is old: {0}", formattedDate);
                //    return false;
                //}
            }
            catch (Exception e)
            {
                validation = String.Format("Error reading settings file: {0}", Properties.Settings.Default.ContestSettingsFile);
                return false;
            }

        }

        public List<CQWEContact> LoadLogFile()
        {
            if (Properties.Settings.Default.LogFile == String.Empty)
            {
                return null;
            }

            List<CQWEContact> contacts = new List<CQWEContact>();
            int iContact = 0;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                if (File.Exists(Properties.Settings.Default.LogFile))
                {
                    xmlDoc.Load(Properties.Settings.Default.LogFile);

                    XmlNodeList nodelist = xmlDoc.SelectNodes("CQ-WELog/QSO");
                    if (nodelist.Count > 0)
                    {
                        foreach (XmlNode node in nodelist)
                        {
                            if (node.ChildNodes.Count > 0)
                            {
                                CQWEContact contact = new CQWEContact();

                                XmlNode entry = node.SelectSingleNode("Entry");
                                contact.QSONumber = Convert.ToInt32(node.SelectSingleNode("QSONumber").ChildNodes[0].Value);
                                contact.QSOStation = node.SelectSingleNode("Callsign").ChildNodes[0].Value;
                                contact.QSOBand = node.SelectSingleNode("Band").ChildNodes[0].Value;
                                contact.QSOMode = node.SelectSingleNode("Mode").ChildNodes[0].Value;
                                DateTime dt = Convert.ToDateTime(node.SelectSingleNode("Date").ChildNodes[0].Value);
                                contact.QSODate = dt.ToUniversalTime();
                                contact.QSOName = node.SelectSingleNode("Name").ChildNodes[0].Value;
                                contact.QSOLocation = node.SelectSingleNode("Location").ChildNodes[0].Value;
                                contact.QSOYearsOfService = Convert.ToInt32(node.SelectSingleNode("YearsOfService").ChildNodes[0].Value);
                                contact.QSOValidated = Convert.ToBoolean(node.SelectSingleNode("Validated").ChildNodes[0].Value);
                                contacts.Add(contact);
                                iContact++;
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error while loading log: \r\n\r\n{0}", e), 
                    "Read " + iContact + " contacts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return contacts;

        }

        public bool OpenNewSettingsFile()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "CQ-WE Settings File (*.xml)|*.xml";
                dialog.Title = String.Format("Select the CQ-WE Settings file for {0}", DateTime.Now.Year);
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.FileName = Properties.Settings.Default.ContestSettingsFile;
                string fileName = String.Empty;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    Properties.Settings.Default.ContestSettingsFile = fileName;
                    Properties.Settings.Default.Save();
                    return true;
                }

                if (fileName == String.Empty)
                {
                    string message = String.Format("You must select the {0}" +
                                    " CQ-WE Settings file supplied by the CQ-WE contest committee." + Environment.NewLine + Environment.NewLine + 
                                    "Run the program again and select the settings file.", DateTime.Now.Year);

                    MessageBox.Show(message,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                return false;
            }

            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error while accessing settings file" + Environment.NewLine + Environment.NewLine + "{0}", e.ToString()),
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }


        //public bool WriteDefaultSettings(string settingsFile)
        //{
        //    try
        //    {
        //        Assembly thisAssembly = Assembly.GetExecutingAssembly();
        //        Stream defaultXMLfile = thisAssembly.GetManifestResourceStream("CQWELogger.Resources.CQWESettings.xml");
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(defaultXMLfile);
        //        doc.PreserveWhitespace = true;
        //        doc.Save(settingsFile);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error while writing default settings file \"" + settingsFile + 
        //                        "\"\r\n\r\n" + e.ToString(), Application.ProductName,
        //            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return false;
        //    }
        //}

        //public bool ConfirmSettingsFile()
        //{
        //    try
        //    {
        //        Properties.Resources.ResourceManager.

        //        // Compiled code: C:\Users\Tom\AppData\Local\N4HAI\\CQWESettings.xml
        //        // Running in VS: C:\Users\Tom\AppData\Local\N4HAI\CQWELogger.vshost.exe_StrongName_hp4lz4tqp5v5acl3uxl2ki5pt2p2symk\1.0.0.0
        //        //MessageBox.Show(Application.LocalUserAppDataPath);

        //        if (Properties.Settings.Default.SettingsFile.Length > 0)
        //            // already saved a settings file, check to see if it's there
        //            if (File.Exists(Properties.Settings.Default.SettingsFile))
        //                return true;

        //        // properties file does not exist
        //        string settingsFileDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.CompanyName;

        //        // create settings file directory - don't rely on saving properties to create this directory; not necessary in Windows 7, needed on XP
        //        if (!Directory.Exists(settingsFileDir))
        //            Directory.CreateDirectory(settingsFileDir);

        //        Properties.Settings.Default.SettingsFile = settingsFileDir + "\\" + Properties.Settings.Default.DefaultSettingsFile;
        //        Properties.Settings.Default.Save();

        //        //if (!WriteDefaultSettings(Properties.Settings.Default.SettingsFile))
        //        //    return OpenSettingsFile();

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error while accessing settings file\r\n\r\n" + e.ToString(),
        //            Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        //        return false;
        //    }
        //}

        public CQWEContact CopyContact(CQWEContact contact)
        {
            CQWEContact contactFound = new CQWEContact();
            contactFound.QSONumber = contact.QSONumber;
            contactFound.QSOBand = contact.QSOBand;
            contactFound.QSODate = contact.QSODate;
            contactFound.QSOLocation = contact.QSOLocation;
            contactFound.QSOMode = contact.QSOMode;
            contactFound.QSOName = contact.QSOName;
            contactFound.QSONumber = contact.QSONumber;
            contactFound.QSOStation = contact.QSOStation;
            contactFound.QSOYearsOfService = contact.QSOYearsOfService;
            // don't copy validated value!
            contactFound.QSOValidated = false;
            return contactFound;
        }

        public CQWEContact FindContactByNumber(long QSONumber, List<CQWEContact> contacts)
        {
            CQWEContact contactFound = new CQWEContact();
            // there's a better way for this, too...
            foreach (CQWEContact contact in contacts)
            {
                if (contact.QSONumber == QSONumber)
                {
                    return contact;
                }
            }
            return null;
        }

        public List<string> ComputeLocations(string modeGroup, List<CQWEContact> contacts)
        {
            if (contacts != null)
            {
                List<string> locations = new List<string>();

                foreach (CQWEContact contact in contacts)
                {
                    if (ModeInModeGroup(contact.QSOMode, modeGroup) && QSODateInRange(contact.QSODate))
                    {
                        if (locations.IndexOf(contact.QSOLocation) == -1)
                        {
                            locations.Add(contact.QSOLocation);
                        }
                    }
                }
                locations.Sort();
                return locations;
            }
            else
                return null;
        }


        public bool ModeInModeGroup(string mode, string modeGroup)
        {
            if (modeGroup.Length == 0)
            {
                return true;
            }
            else
            {
                List<string> modeListToSearch = null;
                switch (modeGroup)
                {
                    case "CW":
                        modeListToSearch = modesCW;
                        break;
                    case "PH":
                        modeListToSearch = modesPH;
                        break;
                    case "DIG":
                        modeListToSearch = modesDIG;
                        break;
                    default:
                        return false;
                }

                if (modeListToSearch == null)
                    return false;
                else
                    return modeListToSearch.IndexOf(mode) >= 0;
            }
        }

        public bool QSODateInRange(DateTime QSODate)
        {
            // one day add a contest start / end?
            return QSODate.Year == Convert.ToInt32(Properties.Settings.Default.ContestYear);
        }

        public long ComputeScore(string modeGroup, List<CQWEContact> contacts)
        {
            List<string> locations = new List<string>();
            long score = 0;
            foreach (CQWEContact contact in contacts)
            {
                if (ModeInModeGroup(contact.QSOMode, modeGroup))
                {
                    if (QSODateInRange(contact.QSODate))
                    {
                        if (locations.IndexOf(contact.QSOLocation) == -1)
                        {
                            locations.Add(contact.QSOLocation);
                        }
                        score += contact.QSOYearsOfService;
                    }
                }
            }
            return locations.Count * score;
        }

        public long ComputeQSO(string modeGroup, List<CQWEContact> contacts)
        {
            long score = 0;
            foreach (CQWEContact contact in contacts)
            {
                if (ModeInModeGroup(contact.QSOMode, modeGroup))
                {
                    if (QSODateInRange(contact.QSODate))
                    {
                        score++;
                    }
                }
            }
            return score;
        }

        private string GetCallSignBasedOnLocation(List<CQWEContact> contacts, string location, string mode)
        {
            List<string> modesForThisGroup = null;
            foreach (CQWEContact contact in contacts)
            {
                switch (mode)
                {
                    case "CW": modesForThisGroup = modesCW; break;
                    case "PH": modesForThisGroup = modesPH; break;
                    case "DIG": modesForThisGroup = modesDIG; break;
                }

                if (location == contact.QSOLocation && modesForThisGroup.IndexOf(contact.QSOMode) >= 0)
                    return contact.QSOStation;
            }
            return null;
        }

        public string LocationSummary(string mode, List<CQWEContact> contacts, bool showFormatting)
        {
            Dictionary<string, string> locationList = GetLocations();
            int locationCount = 0;

            string ret = String.Empty;

            if (showFormatting)
                ret += mode + " Locations" + Environment.NewLine;

            foreach (string location in ComputeLocations(mode, contacts))
            {
                ret += location + " - ";
                if (locationList.ContainsKey(location))
                    ret += locationList[location];
                else
                    ret += "??";

                ret += " (" + GetCallSignBasedOnLocation(contacts, location, mode) + ")";
                ret += Environment.NewLine;

                locationCount++;
            }

            if (showFormatting)
                ret += "Total " + mode + " locations: " + locationCount + Environment.NewLine;

            return ret;
        }


        public string CreateSummary(List<CQWEContact> contacts)
        {
            try
            {
                long scoreCW = ComputeScore("CW", contacts);
                long scorePH = ComputeScore("PH", contacts);
                long scoreDIG = ComputeScore("DIG", contacts);
                long scoreTotal = scoreCW + scorePH + scoreDIG;

                long countCW = ComputeQSO("CW", contacts);
                long countPH = ComputeQSO("PH", contacts);
                long countDIG = ComputeQSO("DIG", contacts);
                long countTotal = countCW + countPH + countDIG;

                List<string> locCW = ComputeLocations("CW", contacts);
                List<string> locPH = ComputeLocations("PH", contacts);
                List<string> locDIG = ComputeLocations("DIG", contacts);
                List<string> locAll = ComputeLocations("", contacts);

                List<string> retVal = new List<string>();

                retVal.Add(String.Format("{0} CQ-WE Contest Summary", Properties.Settings.Default.ContestYear));
                retVal.Add("");
                //retVal.Add("Full Name          : " + Properties.Settings.Default.FullName);
                //retVal.Add("Callsign           : " + Properties.Settings.Default.Callsign);
                //retVal.Add("License Class      : " + Properties.Settings.Default.LicenseClass);
                //retVal.Add("Retired            : " + Properties.Settings.Default.Retired.ToString());
                //retVal.Add("Certificate Wanted : " + Properties.Settings.Default.CertificateWanted.ToString());
                //retVal.Add("QRP                : " + Properties.Settings.Default.QRP.ToString());
                //retVal.Add("");
                //retVal.Add("Exchange           : " + ComputeExchange(Properties.Settings.Default.FirstName,
                //                    Properties.Settings.Default.StationLocation, Properties.Settings.Default.Years));
                //retVal.Add("First Name         : " + Properties.Settings.Default.FirstName);
                //retVal.Add("Location           : " + Properties.Settings.Default.StationLocation);
                //retVal.Add("Years Of Service   : " + Properties.Settings.Default.Years.ToString());
                //retVal.Add("");
                //retVal.Add("Home Address       : " + Properties.Settings.Default.HomeAddress);
                //retVal.Add("Home City          : " + Properties.Settings.Default.HomeCity);
                //retVal.Add("Home State         : " + Properties.Settings.Default.HomeState);
                //retVal.Add("Home ZIP           : " + Properties.Settings.Default.HomeZIP);
                //retVal.Add("Home Phone         : " + Properties.Settings.Default.HomePhone);
                //retVal.Add("Home Email         : " + Properties.Settings.Default.HomeEmail);
                //retVal.Add("");

                //retVal.Add("Work Address       : " + Properties.Settings.Default.WorkAddress);
                //retVal.Add("Work Room          : " + Properties.Settings.Default.WorkRoom);
                //retVal.Add("Work City          : " + Properties.Settings.Default.WorkCity);
                //retVal.Add("Work State         : " + Properties.Settings.Default.WorkState);
                //retVal.Add("Work ZIP           : " + Properties.Settings.Default.WorkZIP);
                //retVal.Add("Work Phone         : " + Properties.Settings.Default.WorkPhone);
                //retVal.Add("");

                retVal.Add(String.Format("CW  Score  : {0}", scoreCW));
                retVal.Add(String.Format("PH  Score  : {0}", scorePH));
                retVal.Add(String.Format("DIG Score  : {0}", scoreDIG));
                retVal.Add("");
                retVal.Add(String.Format("Total Score: >>> {0} <<<", scoreTotal));
                retVal.Add("");

                retVal.Add(String.Format("CW  QSOs   : {0}", countCW));
                retVal.Add(String.Format("CW  Locs   : {0}", locCW.Count));
                retVal.Add("");

                retVal.Add(String.Format("PH  QSOs   : {0}", countPH));
                retVal.Add(String.Format("PH  Locs   : {0}", locPH.Count));
                retVal.Add("");

                retVal.Add(String.Format("DIG QSOs   : {0}", countDIG));
                retVal.Add(String.Format("DIG Locs   : {0}", locDIG.Count));
                retVal.Add("");

                retVal.Add("");
                retVal.Add(String.Format("Total QSOs : {0}", countTotal));
                retVal.Add("");
                retVal.Add(String.Format("Total Locs : {0}", locAll.Count));
                retVal.Add("");

                retVal.Add(LocationSummary("CW", contacts, true));
                retVal.Add(LocationSummary("PH", contacts, true));
                retVal.Add(LocationSummary("DIG", contacts, true));
                retVal.Add("");

                long outOfRange = 0;

                retVal.Add("QSONumber,Station,Band,Mode,Location,YearsOfService,Date");
                foreach (CQWEContact contact in contacts)
                {
                    if (QSODateInRange(contact.QSODate))
                    {
                        retVal.Add(String.Format("{0},{1},{2},{3},{4},{5},{6}",
                                contact.QSONumber, contact.QSOStation,
                                contact.QSOBand, contact.QSOMode,
                                contact.QSOLocation, contact.QSOYearsOfService,
                                contact.QSODate.ToString("u")));
                    }
                    else
                        outOfRange++;
                }


                //MessageBox.Show("Contest Summary written to: " + fileName, Application.ProductName,
                //    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (outOfRange > 0)
                {
                    string msg = String.Format("{0} log entries are not from contest year {1} and were not used in the summary",
                        outOfRange, Properties.Settings.Default.ContestYear);
                    MessageBox.Show(msg, Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                string returnString = Environment.NewLine;
                foreach (string line in retVal)
                {
                    returnString += line + Environment.NewLine;
                }
                return returnString;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error while submitting log information", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        public string BuildADIFileName()
        {
            return     Properties.Settings.Default.DefaultLogFileName + "_" +
                       Properties.Settings.Default.ContestYear + "_" +
                       Properties.Settings.Default.Callsign + 
                       ".adi" ;

        }

        public string BuildLogFileName(bool isBackup)
        {
            if (isBackup)
                return Properties.Settings.Default.DefaultLogFileName + "_" +
                       Properties.Settings.Default.ContestYear + "_" +
                       Properties.Settings.Default.Callsign + 
                       "_" + String.Format("{0:s}", DateTime.UtcNow).Replace(":", ".") + 
                       "Z.xml" ;
            else
                return Properties.Settings.Default.DefaultLogFileName + "_" +
                       Properties.Settings.Default.ContestYear + "_" +
                       Properties.Settings.Default.Callsign + 
                       ".xml";

        }

        public string BackupLogFile(List<CQWEContact> contacts)
        {
            // must have a call sign to make a log file
            if (Properties.Settings.Default.Callsign.Length == 0)
            {
                MessageBox.Show("Can't back up log data in XML because no callsign is registered.  " +
                    "Go back to the Registration page and fill in your call sign.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
            }


            string logFileName = Properties.Settings.Default.LogFile;
            string logDir = Directory.GetParent(logFileName).FullName;
            string backupLog = String.Empty;

            if (File.Exists(logFileName))
            {
                try
                {

                    if (logDir != null)
                    {
                        // create backup file with date/time
                        backupLog = logDir + "\\" + BuildLogFileName(true);
                        File.Copy(logFileName, backupLog);
                    }
                    return backupLog;
                }

                catch (Exception e)
                {
                    MessageBox.Show(String.Format("Error backing up log from \"{0}\" to backup file \"{1}\"; Error = {2}",
                                    logFileName, backupLog, e.ToString()),
                                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return String.Empty;
                }
            }
            else
            {
                MessageBox.Show(String.Format("Can't find log file \"{0}\".  Did you save any QSOs?", logFileName), 
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return String.Empty;
            }
        }

        public bool SaveLogFile(List<CQWEContact> contacts, string fileName)
        {
            try
            {
                string logFile = fileName;

                if (fileName.Length == 0)
                    logFile = Properties.Settings.Default.LogFile;

                XmlTextWriter writer = new XmlTextWriter(logFile, null);

                writer.WriteStartDocument();

                // Comments - application, date, call, clamed score
                writer.WriteComment(String.Format("{0} {1} Contest Summary File", 
                    Properties.Settings.Default.ContestYear,
                    Application.ProductName, " Contest Summary File"));
                writer.WriteWhitespace(Environment.NewLine);

                writer.WriteComment(String.Format("Created {0}", DateTime.Now));
                writer.WriteWhitespace(Environment.NewLine);

                writer.WriteComment(String.Format("Callsign: {0}; Total Claimed Points: {1}", 
                    Properties.Settings.Default.Callsign, ComputeTotalScore(contacts)));
                writer.WriteWhitespace(Environment.NewLine);

                writer.WriteStartElement("CQ-WELog", "");
                    writer.WriteAttributeString("SavedOn", System.DateTime.UtcNow.ToString("u"));
                writer.WriteWhitespace(Environment.NewLine);

                LogSummary(writer, contacts);
                LogContacts(writer, contacts);

                writer.WriteEndDocument();
                writer.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error while writing log file: {0}", e.ToString()), 
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private long ComputeTotalScore(List<CQWEContact> contacts)
        {
            if (contacts != null)
                return ComputeScore("CW", contacts) + ComputeScore("PH", contacts) + ComputeScore("DIG", contacts);
            else
                return 0;
        }

        private void LogSummary(XmlTextWriter writer, List<CQWEContact> contacts)
        {
            if (contacts == null)
                return;

            try
            {
                long scoreCW = ComputeScore("CW", contacts);
                long scorePH = ComputeScore("PH", contacts);
                long scoreDIG = ComputeScore("DIG", contacts);
                long scoreTotal = scoreCW + scorePH + scoreDIG;

                long countCW = ComputeQSO("CW", contacts);
                long countPH = ComputeQSO("PH", contacts);
                long countDIG = ComputeQSO("DIG", contacts);
                long countTotal = countCW + countPH + countDIG;

                List<string> locCW = ComputeLocations("CW", contacts);
                List<string> locPH = ComputeLocations("PH", contacts);
                List<string> locDIG = ComputeLocations("DIG", contacts);


                writer.WriteElementString("Callsign", Properties.Settings.Default.Callsign); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("FullName", Properties.Settings.Default.FullName); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("ClaimedTotalScore", scoreTotal.ToString()); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("ClaimedCWScore", scoreCW.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("ClaimedPHScore", scorePH.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("ClaimedDIGScore", scoreDIG.ToString()); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("ClaimedTotalCount", countTotal.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("ClaimedCWCount", countCW.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("ClaimedCWLocations", locCW.Count.ToString()); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("ClaimedPHCount", countPH.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("ClaimedPHLocations", locPH.Count.ToString()); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("ClaimedDIGCount", countDIG.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("ClaimedDIGLocations", locDIG.Count.ToString()); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("LicenseClass", Properties.Settings.Default.LicenseClass); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("Retired", Properties.Settings.Default.Retired.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("CertificateWanted", Properties.Settings.Default.CertificateWanted.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("QRP", Properties.Settings.Default.QRP.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("FirstName", Properties.Settings.Default.FirstName); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("Location", Properties.Settings.Default.StationLocation); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("YearsOfService", Properties.Settings.Default.Years.ToString()); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("Exchange", ComputeExchange()); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("HomeAddress", Properties.Settings.Default.HomeAddress); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("HomeCity", Properties.Settings.Default.HomeCity); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("HomeState", Properties.Settings.Default.HomeState); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("HomeZIP", Properties.Settings.Default.HomeZIP); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("HomeEmail", Properties.Settings.Default.HomeEmail); writer.WriteWhitespace(Environment.NewLine);

                writer.WriteElementString("WorkAddress", Properties.Settings.Default.WorkAddress); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("WorkRoom", Properties.Settings.Default.WorkRoom); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("WorkCity", Properties.Settings.Default.WorkCity); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("WorkState", Properties.Settings.Default.WorkState); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("WorkZIP", Properties.Settings.Default.WorkZIP); writer.WriteWhitespace(Environment.NewLine);
                writer.WriteElementString("WorkPhone", Properties.Settings.Default.WorkPhone); writer.WriteWhitespace(Environment.NewLine);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error while writing log summary: {0}", e.ToString()), "Error");
            }
        }

        private void LogContacts(XmlTextWriter writer, List<CQWEContact> contacts)
        {
            int iContact = 0;

            try
            {
                if (contacts == null)
                    return;
                writer.WriteWhitespace(Environment.NewLine);
                foreach (CQWEContact contact in contacts)
                {
                    if (LegalContact(contact))
                    {
                        writer.WriteStartElement("QSO"); writer.WriteWhitespace(Environment.NewLine);


                        writer.WriteWhitespace("  "); writer.WriteElementString("QSONumber", contact.QSONumber.ToString()); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("Callsign", contact.QSOStation); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("Band", contact.QSOBand); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("Mode", contact.QSOMode); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("Date", contact.QSODate.ToString("u")); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("Name", contact.QSOName); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("Location", contact.QSOLocation); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("YearsOfService", contact.QSOYearsOfService.ToString()); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteWhitespace("  "); writer.WriteElementString("Validated", contact.QSOValidated.ToString()); writer.WriteWhitespace(Environment.NewLine);
                        writer.WriteEndElement();
                        writer.WriteWhitespace(Environment.NewLine);
                    }
                    iContact++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Error while writing log: {0}", e.ToString()),
                        "Wrote " + iContact + " contacts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

        }

        private bool LegalContact(CQWEContact contact)
        {
            if (contact.QSONumber <= 0 || 
                contact.QSOStation.Length == 0 || 
                contact.QSOBand.Length == 0 || 
                contact.QSOMode.Length == 0 || 
                contact.QSODate == null || 
                contact.QSOName.Length == 0 || 
                contact.QSOLocation.Length == 0)
                return false;
            else
                return true;
        }

        public string GetCQWEMode(string modeSearch)
        {
            if (modeSearch.IndexOf("LSB") > 0  || modeSearch.IndexOf("USB") > 0)
                return "SSB";

            if (modeSearch.IndexOf("RTTY") > 0)
                return "RTTY";
            
            if (modeSearch.IndexOf("CW") > 0)
                return "CW";

            if (modeSearch.IndexOf("FM") > 0)
                return "FM";

            if (modeSearch.IndexOf("AM") > 0)
                return "AM";
     
            if (modeSearch.IndexOf("PKT") > 0)
                return "PKT";

            return "";
        }

        public string GetCQWEBand(double freqHz)
        {
            double freq = freqHz / 1000000;

            if (freq >= 1.8 && freq <= 2.0)
                return "160M";
            if (freq >= 3.5 && freq <= 4.0)
                return "80M";
            if (freq >= 5.0 && freq <= 5.5)
                return "60M";
            if (freq >= 7.0 && freq <= 7.3)
                return "40M";
            if (freq >= 10.1 && freq <= 10.15)
                return "30M";
            if (freq >= 14.0 && freq <= 14.35)
                return "20M";
            if (freq >= 18.068 && freq <= 18.168)
                return "17M";
            if (freq >= 21 && freq <= 21.45)
                return "15M";
            if (freq >= 24.89 && freq <= 24.990)
                return "12M";
            if (freq >= 28 && freq <= 29.7)
                return "10M";
            if (freq >= 50 && freq <= 54)
                return "6M";
            if (freq >= 144 && freq <= 148)
                return "2M";
            if (freq >= 219 && freq <= 225)
                return "1.25M";
            if (freq >= 420 && freq <= 450)
                return "70cm";
            return "??";
        }

        public int GetMaxZZ()
        {
            try
            {
                // compute max ZZ for this year
                // 2011 allows for 5 years of participation
                return (int)DateTime.Now.Year - 2011 + 5;
            }
            catch (Exception e)
            {
                return 99;
            }
        }

        public bool CheckRegisteredDate()
        {
            try
            {
                if (Properties.Settings.Default.RegisteredDate == null)
                {
                    MessageBox.Show("You must save your registration information before opening the log file.",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);

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

        public string OpenLogFile()
        {
            if (!CheckRegisteredDate())
                return String.Empty;

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Filter = "CQ-WE Log files (*.xml)|*.xml";
                dialog.Title = "Open a CQ-WE Log file";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Default log file = My Documents \ CQWELog_Callsign_Year.xml
                dialog.FileName = BuildLogFileName(false);
                string fileName = String.Empty;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.FileName == Properties.Settings.Default.ContestSettingsFile)
                    {
                        MessageBox.Show(MSG_CANT_USE_SETTINGS,
                            Application.ProductName, MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        return String.Empty;
                    }
                    else
                    {
                        // save file name
                        Properties.Settings.Default.LogFile = dialog.FileName;
                        Properties.Settings.Default.Save();
                        LoadLogFile();
                        return dialog.FileName;
                    }
                }
                else
                    return String.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error opening log file" + Environment.NewLine + Environment.NewLine +
                    "Log File: " + Properties.Settings.Default.LogFile + Environment.NewLine +
                    "Error: " + e.ToString(), Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return String.Empty;
            }
        }


        public string NewLogFile()
        {
            if (!CheckRegisteredDate())
                return String.Empty;

            try
            {
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "CQ-WE Log files (*.xml)|*.xml";
                dialog.Title = "Create a new CQ-WE Log file";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Default log file = My Documents \ CQWELog_Callsign_Year.xml
                dialog.FileName = BuildLogFileName(false);
                string fileName = String.Empty;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.FileName == Properties.Settings.Default.ContestSettingsFile)
                    {
                        MessageBox.Show(MSG_CANT_USE_SETTINGS,
                            Application.ProductName, MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                        return String.Empty;
                    }
                    else
                    {
                        // save file name
                        Properties.Settings.Default.LogFile = dialog.FileName;
                        Properties.Settings.Default.Save();

                        //2012 - starting w/ new file, so delete old one, if it exists
                        DeleteFile(Properties.Settings.Default.LogFile);
                        
                        LoadLogFile();
                        return dialog.FileName;
                    }
                }
                else
                    return String.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error opening log file" + Environment.NewLine + Environment.NewLine +
                    "Log File: " + Properties.Settings.Default.LogFile + Environment.NewLine +
                    "Error: " + e.ToString(), Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return String.Empty;
            }
        }

        private void DeleteFile(String fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }

            catch (Exception e)
            {
                MessageBox.Show("Error deleting file" + Environment.NewLine + Environment.NewLine +
                    "File: " + fileName + Environment.NewLine +
                    "Error: " + e.ToString(), Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            
            }
        }


    }

    
}

