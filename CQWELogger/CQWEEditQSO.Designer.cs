namespace CQWELogger
{
    partial class CQWEEditQSO
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CQWEEditQSO));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboModes = new System.Windows.Forms.ComboBox();
            this.cboBands = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dteQSO = new System.Windows.Forms.DateTimePicker();
            this.numYears = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cboLocations = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCall = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.logginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYears)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(692, 171);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.toolTip1.SetToolTip(this.btnOK, "Updates this QSO only if it does not produce a dupe.");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(611, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Cancels edit and does not change this QSO");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboModes);
            this.groupBox2.Controls.Add(this.cboBands);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dteQSO);
            this.groupBox2.Controls.Add(this.numYears);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboLocations);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCall);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(759, 153);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "QSO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(151, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 34;
            this.label2.Text = "Mode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Band";
            // 
            // cboModes
            // 
            this.cboModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboModes.FormattingEnabled = true;
            this.cboModes.Location = new System.Drawing.Point(154, 36);
            this.cboModes.Name = "cboModes";
            this.cboModes.Size = new System.Drawing.Size(117, 33);
            this.cboModes.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cboModes, "Shortcut: F3");
            // 
            // cboBands
            // 
            this.cboBands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBands.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBands.FormattingEnabled = true;
            this.cboBands.Location = new System.Drawing.Point(10, 36);
            this.cboBands.Name = "cboBands";
            this.cboBands.Size = new System.Drawing.Size(125, 33);
            this.cboBands.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cboBands, "Shortcut: F2");
            this.cboBands.SelectedIndexChanged += new System.EventHandler(this.cboBands_SelectedIndexChanged_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(487, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "Date / Time";
            // 
            // dteQSO
            // 
            this.dteQSO.CustomFormat = "MM-dd-yyyy HH:mm:ss Z";
            this.dteQSO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteQSO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteQSO.Location = new System.Drawing.Point(490, 101);
            this.dteQSO.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dteQSO.MinDate = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            this.dteQSO.Name = "dteQSO";
            this.dteQSO.ShowUpDown = true;
            this.dteQSO.Size = new System.Drawing.Size(263, 31);
            this.dteQSO.TabIndex = 6;
            this.toolTip1.SetToolTip(this.dteQSO, "Edit QSO Date/Time if necessary");
            this.dteQSO.ValueChanged += new System.EventHandler(this.dteQSO_ValueChanged);
            // 
            // numYears
            // 
            this.numYears.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numYears.Location = new System.Drawing.Point(428, 101);
            this.numYears.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numYears.Name = "numYears";
            this.numYears.Size = new System.Drawing.Size(56, 31);
            this.numYears.TabIndex = 5;
            this.numYears.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numYears, "Edit Number of Years if necessary");
            this.numYears.ValueChanged += new System.EventHandler(this.numYears_ValueChanged);
            this.numYears.Enter += new System.EventHandler(this.numYears_Enter);
            this.numYears.Leave += new System.EventHandler(this.numYears_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(425, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "Years";
            // 
            // cboLocations
            // 
            this.cboLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLocations.FormattingEnabled = true;
            this.cboLocations.Location = new System.Drawing.Point(346, 98);
            this.cboLocations.Name = "cboLocations";
            this.cboLocations.Size = new System.Drawing.Size(73, 33);
            this.cboLocations.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cboLocations, "Edit Location if necessary");
            this.cboLocations.SelectedIndexChanged += new System.EventHandler(this.cboLocations_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(343, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "Location";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(154, 101);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 31);
            this.txtName.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtName, "Edit operator name if necessary");
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(151, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 27;
            this.label4.Text = "Name";
            // 
            // txtCall
            // 
            this.txtCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCall.Location = new System.Drawing.Point(10, 100);
            this.txtCall.MaxLength = 12;
            this.txtCall.Name = "txtCall";
            this.txtCall.Size = new System.Drawing.Size(124, 31);
            this.txtCall.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtCall, "Edit Callsign if necessary");
            this.txtCall.TextChanged += new System.EventHandler(this.txtCall_TextChanged);
            this.txtCall.Leave += new System.EventHandler(this.txtCall_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "Call";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Status";
            // 
            // logginToolStripMenuItem
            // 
            this.logginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeBandToolStripMenuItem,
            this.changeModeToolStripMenuItem});
            this.logginToolStripMenuItem.Name = "logginToolStripMenuItem";
            this.logginToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.logginToolStripMenuItem.Text = "&Logging";
            this.logginToolStripMenuItem.Click += new System.EventHandler(this.logginToolStripMenuItem_Click);
            // 
            // changeBandToolStripMenuItem
            // 
            this.changeBandToolStripMenuItem.Name = "changeBandToolStripMenuItem";
            this.changeBandToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.changeBandToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changeBandToolStripMenuItem.Text = "Change &Band";
            this.changeBandToolStripMenuItem.Click += new System.EventHandler(this.changeBandToolStripMenuItem_Click);
            // 
            // changeModeToolStripMenuItem
            // 
            this.changeModeToolStripMenuItem.Name = "changeModeToolStripMenuItem";
            this.changeModeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.changeModeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changeModeToolStripMenuItem.Text = "Change &Mode";
            this.changeModeToolStripMenuItem.Click += new System.EventHandler(this.changeModeToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(286, 149);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(256, 20);
            this.textBox1.TabIndex = 12;
            // 
            // CQWEEditQSO
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(776, 214);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CQWEEditQSO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit QSO";
            this.Load += new System.EventHandler(this.CQWEEditQSO_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CQWEEditQSO_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYears)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numYears;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboLocations;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem logginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeBandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeModeToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dteQSO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboModes;
        private System.Windows.Forms.ComboBox cboBands;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}