namespace CQWELogger
{
    partial class HRDInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HRDInfo));
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProcessedMode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtProcessedBand = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFreq = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSliders = new System.Windows.Forms.TextBox();
            this.txtButtons = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRadio = new System.Windows.Forms.TextBox();
            this.txtDropdowns = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContext = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(307, 245);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 227);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtProcessedMode);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtProcessedBand);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 201);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Processed";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(17, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(318, 97);
            this.label8.TabIndex = 27;
            this.label8.Text = resources.GetString("label8.Text");
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProcessedMode
            // 
            this.txtProcessedMode.Location = new System.Drawing.Point(132, 15);
            this.txtProcessedMode.Name = "txtProcessedMode";
            this.txtProcessedMode.Size = new System.Drawing.Size(203, 20);
            this.txtProcessedMode.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 69;
            this.label13.Text = "Mode";
            // 
            // txtProcessedBand
            // 
            this.txtProcessedBand.Location = new System.Drawing.Point(132, 41);
            this.txtProcessedBand.Name = "txtProcessedBand";
            this.txtProcessedBand.Size = new System.Drawing.Size(203, 20);
            this.txtProcessedBand.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 13);
            this.label15.TabIndex = 70;
            this.label15.Text = "Band";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtMode);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtFreq);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtSliders);
            this.tabPage2.Controls.Add(this.txtButtons);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtRadio);
            this.tabPage2.Controls.Add(this.txtDropdowns);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtContext);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(362, 201);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Raw";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 61;
            this.label7.Text = "Mode";
            // 
            // txtMode
            // 
            this.txtMode.Location = new System.Drawing.Point(135, 65);
            this.txtMode.Name = "txtMode";
            this.txtMode.Size = new System.Drawing.Size(203, 20);
            this.txtMode.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "Frequency";
            // 
            // txtFreq
            // 
            this.txtFreq.Location = new System.Drawing.Point(135, 91);
            this.txtFreq.Name = "txtFreq";
            this.txtFreq.Size = new System.Drawing.Size(203, 20);
            this.txtFreq.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 59;
            this.label5.Text = "Sliders";
            // 
            // txtSliders
            // 
            this.txtSliders.Location = new System.Drawing.Point(135, 169);
            this.txtSliders.Name = "txtSliders";
            this.txtSliders.Size = new System.Drawing.Size(203, 20);
            this.txtSliders.TabIndex = 9;
            // 
            // txtButtons
            // 
            this.txtButtons.Location = new System.Drawing.Point(135, 117);
            this.txtButtons.Name = "txtButtons";
            this.txtButtons.Size = new System.Drawing.Size(203, 20);
            this.txtButtons.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Dropdowns";
            // 
            // txtRadio
            // 
            this.txtRadio.Location = new System.Drawing.Point(135, 13);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new System.Drawing.Size(203, 20);
            this.txtRadio.TabIndex = 3;
            // 
            // txtDropdowns
            // 
            this.txtDropdowns.Location = new System.Drawing.Point(135, 143);
            this.txtDropdowns.Name = "txtDropdowns";
            this.txtDropdowns.Size = new System.Drawing.Size(203, 20);
            this.txtDropdowns.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Radio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Buttons";
            // 
            // txtContext
            // 
            this.txtContext.Location = new System.Drawing.Point(135, 39);
            this.txtContext.Name = "txtContext";
            this.txtContext.Size = new System.Drawing.Size(203, 20);
            this.txtContext.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Context";
            // 
            // HRDInfo
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 273);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HRDInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HRD Information";
            this.Load += new System.EventHandler(this.HRDInfo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtProcessedMode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtProcessedBand;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFreq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSliders;
        private System.Windows.Forms.TextBox txtButtons;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRadio;
        private System.Windows.Forms.TextBox txtDropdowns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
    }
}