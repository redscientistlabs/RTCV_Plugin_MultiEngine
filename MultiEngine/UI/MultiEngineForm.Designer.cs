
namespace MultiEngine.UI
{
    partial class MultiEngineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiEngineForm));
            this.btnCorrupt = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.lbEngines = new System.Windows.Forms.ListBox();
            this.bSave = new System.Windows.Forms.Button();
            this.bLoad = new System.Windows.Forms.Button();
            this.bUp = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.imgWarning = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCorrupt
            // 
            this.btnCorrupt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnCorrupt.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCorrupt.FlatAppearance.BorderSize = 0;
            this.btnCorrupt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorrupt.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnCorrupt.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnCorrupt.Image = ((System.Drawing.Image)(resources.GetObject("btnCorrupt.Image")));
            this.btnCorrupt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCorrupt.Location = new System.Drawing.Point(341, 12);
            this.btnCorrupt.Margin = new System.Windows.Forms.Padding(4);
            this.btnCorrupt.Name = "btnCorrupt";
            this.btnCorrupt.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCorrupt.Size = new System.Drawing.Size(35, 16);
            this.btnCorrupt.TabIndex = 152;
            this.btnCorrupt.TabStop = false;
            this.btnCorrupt.Tag = "color:dark2";
            this.btnCorrupt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCorrupt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCorrupt.UseVisualStyleBackColor = false;
            this.btnCorrupt.Visible = false;
            this.btnCorrupt.Click += new System.EventHandler(this.btnCorrupt_Click);
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAdd.BackColor = System.Drawing.Color.Gray;
            this.bAdd.FlatAppearance.BorderSize = 0;
            this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bAdd.ForeColor = System.Drawing.Color.White;
            this.bAdd.Location = new System.Drawing.Point(12, 36);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(122, 59);
            this.bAdd.TabIndex = 158;
            this.bAdd.TabStop = false;
            this.bAdd.Tag = "color:light1";
            this.bAdd.Text = "Add Setting";
            this.bAdd.UseVisualStyleBackColor = false;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // lbEngines
            // 
            this.lbEngines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEngines.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbEngines.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEngines.ForeColor = System.Drawing.Color.White;
            this.lbEngines.FormattingEnabled = true;
            this.lbEngines.HorizontalScrollbar = true;
            this.lbEngines.IntegralHeight = false;
            this.lbEngines.Location = new System.Drawing.Point(140, 36);
            this.lbEngines.Name = "lbEngines";
            this.lbEngines.Size = new System.Drawing.Size(236, 100);
            this.lbEngines.TabIndex = 160;
            this.lbEngines.Tag = "color:dark2";
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.BackColor = System.Drawing.Color.Gray;
            this.bSave.FlatAppearance.BorderSize = 0;
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bSave.ForeColor = System.Drawing.Color.White;
            this.bSave.Location = new System.Drawing.Point(12, 101);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(58, 35);
            this.bSave.TabIndex = 163;
            this.bSave.TabStop = false;
            this.bSave.Tag = "color:light1";
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bLoad.BackColor = System.Drawing.Color.Gray;
            this.bLoad.FlatAppearance.BorderSize = 0;
            this.bLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLoad.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bLoad.ForeColor = System.Drawing.Color.White;
            this.bLoad.Location = new System.Drawing.Point(76, 101);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(58, 35);
            this.bLoad.TabIndex = 164;
            this.bLoad.TabStop = false;
            this.bLoad.Tag = "color:light1";
            this.bLoad.Text = "Load";
            this.bLoad.UseVisualStyleBackColor = false;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bUp
            // 
            this.bUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bUp.BackColor = System.Drawing.Color.Gray;
            this.bUp.FlatAppearance.BorderSize = 0;
            this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bUp.ForeColor = System.Drawing.Color.White;
            this.bUp.Location = new System.Drawing.Point(382, 36);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(25, 44);
            this.bUp.TabIndex = 165;
            this.bUp.TabStop = false;
            this.bUp.Tag = "color:light1";
            this.bUp.Text = "↑";
            this.bUp.UseVisualStyleBackColor = false;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDown.BackColor = System.Drawing.Color.Gray;
            this.bDown.FlatAppearance.BorderSize = 0;
            this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDown.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDown.ForeColor = System.Drawing.Color.White;
            this.bDown.Location = new System.Drawing.Point(382, 86);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(26, 50);
            this.bDown.TabIndex = 166;
            this.bDown.TabStop = false;
            this.bDown.Tag = "color:light1";
            this.bDown.Text = "↓";
            this.bDown.UseVisualStyleBackColor = false;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imgWarning);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.bAdd);
            this.groupBox1.Controls.Add(this.btnCorrupt);
            this.groupBox1.Controls.Add(this.bDown);
            this.groupBox1.Controls.Add(this.bSave);
            this.groupBox1.Controls.Add(this.bUp);
            this.groupBox1.Controls.Add(this.bLoad);
            this.groupBox1.Controls.Add(this.lbEngines);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 148);
            this.groupBox1.TabIndex = 167;
            this.groupBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(168, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(172, 13);
            this.label14.TabIndex = 167;
            this.label14.Text = "Combine multiple engines together";
            // 
            // imgWarning
            // 
            this.imgWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgWarning.Location = new System.Drawing.Point(382, 9);
            this.imgWarning.Name = "imgWarning";
            this.imgWarning.Size = new System.Drawing.Size(23, 21);
            this.imgWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgWarning.TabIndex = 168;
            this.imgWarning.TabStop = false;
            // 
            // MultiEngineForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(420, 151);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 151);
            this.Name = "MultiEngineForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "color:dark1";
            this.Text = "Multi Engine";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnCorrupt;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.ListBox lbEngines;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox imgWarning;
    }
}