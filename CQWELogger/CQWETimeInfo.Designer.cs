namespace CQWELogger
{
    partial class CQWETimeInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtCurrentTZ = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCurrentGMT = new System.Windows.Forms.TextBox();
            this.txtTimeZone = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtCurrentTZ
            // 
            this.txtCurrentTZ.CausesValidation = false;
            this.txtCurrentTZ.Location = new System.Drawing.Point(118, 9);
            this.txtCurrentTZ.MaxLength = 2;
            this.txtCurrentTZ.Name = "txtCurrentTZ";
            this.txtCurrentTZ.ReadOnly = true;
            this.txtCurrentTZ.Size = new System.Drawing.Size(214, 20);
            this.txtCurrentTZ.TabIndex = 67;
            this.txtCurrentTZ.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.CausesValidation = false;
            this.label23.Location = new System.Drawing.Point(12, 12);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(92, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "Your Current Time";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.CausesValidation = false;
            this.label22.Location = new System.Drawing.Point(12, 43);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(83, 13);
            this.label22.TabIndex = 71;
            this.label22.Text = "Your Time Zone";
            // 
            // txtCurrentGMT
            // 
            this.txtCurrentGMT.CausesValidation = false;
            this.txtCurrentGMT.Location = new System.Drawing.Point(118, 72);
            this.txtCurrentGMT.MaxLength = 2;
            this.txtCurrentGMT.Name = "txtCurrentGMT";
            this.txtCurrentGMT.ReadOnly = true;
            this.txtCurrentGMT.Size = new System.Drawing.Size(214, 20);
            this.txtCurrentGMT.TabIndex = 70;
            this.txtCurrentGMT.TabStop = false;
            // 
            // txtTimeZone
            // 
            this.txtTimeZone.CausesValidation = false;
            this.txtTimeZone.Location = new System.Drawing.Point(119, 40);
            this.txtTimeZone.MaxLength = 2;
            this.txtTimeZone.Name = "txtTimeZone";
            this.txtTimeZone.ReadOnly = true;
            this.txtTimeZone.Size = new System.Drawing.Size(214, 20);
            this.txtTimeZone.TabIndex = 68;
            this.txtTimeZone.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.CausesValidation = false;
            this.label20.Location = new System.Drawing.Point(12, 75);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 69;
            this.label20.Text = "GMT";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CQWETimeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCurrentTZ);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtCurrentGMT);
            this.Controls.Add(this.txtTimeZone);
            this.Controls.Add(this.label20);
            this.Name = "CQWETimeInfo";
            this.Size = new System.Drawing.Size(347, 113);
            this.Load += new System.EventHandler(this.CQWETimeInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCurrentTZ;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCurrentGMT;
        private System.Windows.Forms.TextBox txtTimeZone;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Timer timer1;
    }
}
