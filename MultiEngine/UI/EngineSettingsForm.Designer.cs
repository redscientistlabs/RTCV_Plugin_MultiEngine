
namespace MultiEngine.UI
{
    partial class EngineSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngineSettingsForm));
            this.pSettings = new System.Windows.Forms.Panel();
            this.bAdd = new System.Windows.Forms.Button();
            this.nmIntensity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pDomains = new System.Windows.Forms.Panel();
            this.tbNewDomain = new System.Windows.Forms.TextBox();
            this.bAddDomain = new System.Windows.Forms.Button();
            this.btnUnselectDomains = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lbMemoryDomains = new RTCV.UI.Components.Controls.ListBoxExtended();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbForceIntensity = new System.Windows.Forms.CheckBox();
            this.nmForcedIntensity = new System.Windows.Forms.NumericUpDown();
            this.imgWarning = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nmIntensity)).BeginInit();
            this.pDomains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmForcedIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // pSettings
            // 
            this.pSettings.Location = new System.Drawing.Point(12, 12);
            this.pSettings.Name = "pSettings";
            this.pSettings.Size = new System.Drawing.Size(487, 246);
            this.pSettings.TabIndex = 0;
            this.pSettings.Tag = "color:dark1";
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bAdd.BackColor = System.Drawing.Color.Gray;
            this.bAdd.FlatAppearance.BorderSize = 0;
            this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bAdd.ForeColor = System.Drawing.Color.White;
            this.bAdd.Location = new System.Drawing.Point(636, 306);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(88, 35);
            this.bAdd.TabIndex = 157;
            this.bAdd.TabStop = false;
            this.bAdd.Tag = "color:light1";
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = false;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // nmIntensity
            // 
            this.nmIntensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nmIntensity.DecimalPlaces = 2;
            this.nmIntensity.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmIntensity.ForeColor = System.Drawing.Color.White;
            this.nmIntensity.Location = new System.Drawing.Point(152, 278);
            this.nmIntensity.Margin = new System.Windows.Forms.Padding(4);
            this.nmIntensity.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nmIntensity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmIntensity.Name = "nmIntensity";
            this.nmIntensity.Size = new System.Drawing.Size(112, 22);
            this.nmIntensity.TabIndex = 160;
            this.nmIntensity.Tag = "color:normal";
            this.nmIntensity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(148, 261);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 163;
            this.label1.Text = "Intensity Percentage:";
            // 
            // pDomains
            // 
            this.pDomains.Controls.Add(this.tbNewDomain);
            this.pDomains.Controls.Add(this.bAddDomain);
            this.pDomains.Controls.Add(this.btnUnselectDomains);
            this.pDomains.Controls.Add(this.btnSelectAll);
            this.pDomains.Controls.Add(this.lbMemoryDomains);
            this.pDomains.Location = new System.Drawing.Point(520, 12);
            this.pDomains.Name = "pDomains";
            this.pDomains.Size = new System.Drawing.Size(204, 285);
            this.pDomains.TabIndex = 1;
            this.pDomains.Tag = "color:dark1";
            // 
            // tbNewDomain
            // 
            this.tbNewDomain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbNewDomain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNewDomain.ForeColor = System.Drawing.Color.White;
            this.tbNewDomain.Location = new System.Drawing.Point(7, 254);
            this.tbNewDomain.Name = "tbNewDomain";
            this.tbNewDomain.Size = new System.Drawing.Size(88, 22);
            this.tbNewDomain.TabIndex = 166;
            this.tbNewDomain.Tag = "color:normal";
            // 
            // bAddDomain
            // 
            this.bAddDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAddDomain.BackColor = System.Drawing.Color.Gray;
            this.bAddDomain.FlatAppearance.BorderSize = 0;
            this.bAddDomain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddDomain.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bAddDomain.ForeColor = System.Drawing.Color.White;
            this.bAddDomain.Location = new System.Drawing.Point(110, 252);
            this.bAddDomain.Name = "bAddDomain";
            this.bAddDomain.Size = new System.Drawing.Size(88, 24);
            this.bAddDomain.TabIndex = 165;
            this.bAddDomain.TabStop = false;
            this.bAddDomain.Tag = "color:light1";
            this.bAddDomain.Text = "Add Domain";
            this.bAddDomain.UseVisualStyleBackColor = false;
            this.bAddDomain.Click += new System.EventHandler(this.bAddDomain_Click);
            // 
            // btnUnselectDomains
            // 
            this.btnUnselectDomains.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnselectDomains.BackColor = System.Drawing.Color.Gray;
            this.btnUnselectDomains.FlatAppearance.BorderSize = 0;
            this.btnUnselectDomains.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnselectDomains.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnUnselectDomains.ForeColor = System.Drawing.Color.White;
            this.btnUnselectDomains.Location = new System.Drawing.Point(110, 219);
            this.btnUnselectDomains.Name = "btnUnselectDomains";
            this.btnUnselectDomains.Size = new System.Drawing.Size(88, 24);
            this.btnUnselectDomains.TabIndex = 17;
            this.btnUnselectDomains.TabStop = false;
            this.btnUnselectDomains.Tag = "color:light1";
            this.btnUnselectDomains.Text = "Unselect all";
            this.btnUnselectDomains.UseVisualStyleBackColor = false;
            this.btnUnselectDomains.Click += new System.EventHandler(this.btnUnselectDomains_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.BackColor = System.Drawing.Color.Gray;
            this.btnSelectAll.FlatAppearance.BorderSize = 0;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSelectAll.ForeColor = System.Drawing.Color.White;
            this.btnSelectAll.Location = new System.Drawing.Point(7, 219);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(88, 24);
            this.btnSelectAll.TabIndex = 164;
            this.btnSelectAll.TabStop = false;
            this.btnSelectAll.Tag = "color:light1";
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lbMemoryDomains
            // 
            this.lbMemoryDomains.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbMemoryDomains.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbMemoryDomains.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMemoryDomains.ForeColor = System.Drawing.Color.White;
            this.lbMemoryDomains.FormattingEnabled = true;
            this.lbMemoryDomains.IntegralHeight = false;
            this.lbMemoryDomains.Location = new System.Drawing.Point(3, 3);
            this.lbMemoryDomains.Name = "lbMemoryDomains";
            this.lbMemoryDomains.ScrollAlwaysVisible = true;
            this.lbMemoryDomains.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbMemoryDomains.Size = new System.Drawing.Size(198, 210);
            this.lbMemoryDomains.TabIndex = 0;
            this.lbMemoryDomains.Tag = "color:dark2";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.ForeColor = System.Drawing.Color.White;
            this.tbName.Location = new System.Drawing.Point(12, 278);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(126, 22);
            this.tbName.TabIndex = 167;
            this.tbName.Tag = "color:normal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 261);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 168;
            this.label2.Text = "Name:";
            // 
            // cbForceIntensity
            // 
            this.cbForceIntensity.AutoSize = true;
            this.cbForceIntensity.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbForceIntensity.ForeColor = System.Drawing.Color.White;
            this.cbForceIntensity.Location = new System.Drawing.Point(151, 307);
            this.cbForceIntensity.Name = "cbForceIntensity";
            this.cbForceIntensity.Size = new System.Drawing.Size(108, 17);
            this.cbForceIntensity.TabIndex = 169;
            this.cbForceIntensity.Text = "Forced Intensity";
            this.cbForceIntensity.UseVisualStyleBackColor = true;
            this.cbForceIntensity.CheckedChanged += new System.EventHandler(this.cbForceIntensity_CheckedChanged);
            // 
            // nmForcedIntensity
            // 
            this.nmForcedIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nmForcedIntensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nmForcedIntensity.Enabled = false;
            this.nmForcedIntensity.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmForcedIntensity.ForeColor = System.Drawing.Color.White;
            this.nmForcedIntensity.Location = new System.Drawing.Point(151, 325);
            this.nmForcedIntensity.Margin = new System.Windows.Forms.Padding(4);
            this.nmForcedIntensity.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nmForcedIntensity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmForcedIntensity.Name = "nmForcedIntensity";
            this.nmForcedIntensity.Size = new System.Drawing.Size(112, 22);
            this.nmForcedIntensity.TabIndex = 170;
            this.nmForcedIntensity.Tag = "color:normal";
            this.nmForcedIntensity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // imgWarning
            // 
            this.imgWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgWarning.Location = new System.Drawing.Point(595, 307);
            this.imgWarning.Name = "imgWarning";
            this.imgWarning.Size = new System.Drawing.Size(35, 34);
            this.imgWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgWarning.TabIndex = 171;
            this.imgWarning.TabStop = false;
            // 
            // EngineSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(736, 353);
            this.Controls.Add(this.imgWarning);
            this.Controls.Add(this.nmForcedIntensity);
            this.Controls.Add(this.cbForceIntensity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.pDomains);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmIntensity);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.pSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(752, 392);
            this.MinimumSize = new System.Drawing.Size(752, 392);
            this.Name = "EngineSettingsForm";
            this.Tag = "color:normal";
            this.Text = "Engine Settings";
            ((System.ComponentModel.ISupportInitialize)(this.nmIntensity)).EndInit();
            this.pDomains.ResumeLayout(false);
            this.pDomains.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmForcedIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pSettings;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.NumericUpDown nmIntensity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pDomains;
        private RTCV.UI.Components.Controls.ListBoxExtended lbMemoryDomains;
        private System.Windows.Forms.Button btnUnselectDomains;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button bAddDomain;
        private System.Windows.Forms.TextBox tbNewDomain;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox cbForceIntensity;
        private System.Windows.Forms.NumericUpDown nmForcedIntensity;
        private System.Windows.Forms.PictureBox imgWarning;
    }
}