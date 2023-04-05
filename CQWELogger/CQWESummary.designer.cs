namespace CQWELogger
{
    partial class CQWESummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CQWESummary));
            this.btnOK = new System.Windows.Forms.Button();
            this.txtModeLocationSummary = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLocationsStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblQSOCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScoreStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblModeStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblLocationsContacted = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(567, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(101, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtModeLocationSummary
            // 
            this.txtModeLocationSummary.BackColor = System.Drawing.SystemColors.Window;
            this.txtModeLocationSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModeLocationSummary.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModeLocationSummary.Location = new System.Drawing.Point(0, 0);
            this.txtModeLocationSummary.Multiline = true;
            this.txtModeLocationSummary.Name = "txtModeLocationSummary";
            this.txtModeLocationSummary.ReadOnly = true;
            this.txtModeLocationSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtModeLocationSummary.Size = new System.Drawing.Size(668, 310);
            this.txtModeLocationSummary.TabIndex = 2;
            this.txtModeLocationSummary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModeLocationSummary_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLocationsStrip,
            this.lblQSOCount,
            this.lblScoreStrip,
            this.lblModeStrip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 404);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(668, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLocationsStrip
            // 
            this.lblLocationsStrip.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblLocationsStrip.Name = "lblLocationsStrip";
            this.lblLocationsStrip.Size = new System.Drawing.Size(137, 17);
            this.lblLocationsStrip.Text = "toolStripStatusLabel1";
            // 
            // lblQSOCount
            // 
            this.lblQSOCount.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblQSOCount.Name = "lblQSOCount";
            this.lblQSOCount.Size = new System.Drawing.Size(137, 17);
            this.lblQSOCount.Text = "toolStripStatusLabel1";
            // 
            // lblScoreStrip
            // 
            this.lblScoreStrip.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblScoreStrip.Name = "lblScoreStrip";
            this.lblScoreStrip.Size = new System.Drawing.Size(137, 17);
            this.lblScoreStrip.Text = "toolStripStatusLabel1";
            // 
            // lblModeStrip
            // 
            this.lblModeStrip.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblModeStrip.Name = "lblModeStrip";
            this.lblModeStrip.Size = new System.Drawing.Size(137, 17);
            this.lblModeStrip.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtModeLocationSummary);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Size = new System.Drawing.Size(668, 344);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.TabIndex = 11;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblLocationsContacted);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(668, 404);
            this.splitContainer2.SplitterDistance = 56;
            this.splitContainer2.TabIndex = 12;
            // 
            // lblLocationsContacted
            // 
            this.lblLocationsContacted.BackColor = System.Drawing.SystemColors.Control;
            this.lblLocationsContacted.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblLocationsContacted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocationsContacted.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationsContacted.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLocationsContacted.Location = new System.Drawing.Point(0, 0);
            this.lblLocationsContacted.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.lblLocationsContacted.Multiline = true;
            this.lblLocationsContacted.Name = "lblLocationsContacted";
            this.lblLocationsContacted.ReadOnly = true;
            this.lblLocationsContacted.Size = new System.Drawing.Size(668, 56);
            this.lblLocationsContacted.TabIndex = 1;
            this.lblLocationsContacted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lblLocationsContacted.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lblLocationsContacted_KeyPress);
            // 
            // CQWESummary
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(668, 426);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "CQWESummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Current Statistics";
            this.Load += new System.EventHandler(this.SummaryForm_Load);
            this.Shown += new System.EventHandler(this.CQWESummary_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtModeLocationSummary;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblModeStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblScoreStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblLocationsStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblQSOCount;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox lblLocationsContacted;
    }
}