namespace Valkyrie.Loader
{
    partial class AddPlace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPlace));
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.corXLabel = new System.Windows.Forms.Label();
            this.corYLabel = new System.Windows.Forms.Label();
            this.corZLabel = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.fillCheckBox = new System.Windows.Forms.CheckBox();
            this.corXBox = new System.Windows.Forms.NumericUpDown();
            this.corYBox = new System.Windows.Forms.NumericUpDown();
            this.corZBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.corXBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corZBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name:";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(12, 25);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(170, 20);
            this.nameBox.TabIndex = 2;
            // 
            // corXLabel
            // 
            this.corXLabel.AutoSize = true;
            this.corXLabel.Location = new System.Drawing.Point(12, 48);
            this.corXLabel.Name = "corXLabel";
            this.corXLabel.Size = new System.Drawing.Size(71, 13);
            this.corXLabel.TabIndex = 3;
            this.corXLabel.Text = "Coordinate X:";
            // 
            // corYLabel
            // 
            this.corYLabel.AutoSize = true;
            this.corYLabel.Location = new System.Drawing.Point(12, 87);
            this.corYLabel.Name = "corYLabel";
            this.corYLabel.Size = new System.Drawing.Size(71, 13);
            this.corYLabel.TabIndex = 5;
            this.corYLabel.Text = "Coordinate Y:";
            // 
            // corZLabel
            // 
            this.corZLabel.AutoSize = true;
            this.corZLabel.Location = new System.Drawing.Point(12, 126);
            this.corZLabel.Name = "corZLabel";
            this.corZLabel.Size = new System.Drawing.Size(71, 13);
            this.corZLabel.TabIndex = 7;
            this.corZLabel.Text = "Coordinate Z:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(107, 191);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 10;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(12, 191);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 9;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // fillCheckBox
            // 
            this.fillCheckBox.AutoSize = true;
            this.fillCheckBox.Location = new System.Drawing.Point(12, 168);
            this.fillCheckBox.Name = "fillCheckBox";
            this.fillCheckBox.Size = new System.Drawing.Size(135, 17);
            this.fillCheckBox.TabIndex = 11;
            this.fillCheckBox.Text = "Fill with current position";
            this.fillCheckBox.UseVisualStyleBackColor = true;
            this.fillCheckBox.CheckedChanged += new System.EventHandler(this.FillCheckBox_CheckedChanged);
            // 
            // corXBox
            // 
            this.corXBox.DecimalPlaces = 6;
            this.corXBox.Location = new System.Drawing.Point(12, 64);
            this.corXBox.Name = "corXBox";
            this.corXBox.Size = new System.Drawing.Size(170, 20);
            this.corXBox.TabIndex = 12;
            // 
            // corYBox
            // 
            this.corYBox.DecimalPlaces = 6;
            this.corYBox.Location = new System.Drawing.Point(12, 103);
            this.corYBox.Name = "corYBox";
            this.corYBox.Size = new System.Drawing.Size(170, 20);
            this.corYBox.TabIndex = 13;
            // 
            // corZBox
            // 
            this.corZBox.DecimalPlaces = 6;
            this.corZBox.Location = new System.Drawing.Point(12, 142);
            this.corZBox.Name = "corZBox";
            this.corZBox.Size = new System.Drawing.Size(170, 20);
            this.corZBox.TabIndex = 14;
            // 
            // AddPlace
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(194, 231);
            this.Controls.Add(this.corZBox);
            this.Controls.Add(this.corYBox);
            this.Controls.Add(this.corXBox);
            this.Controls.Add(this.fillCheckBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.corZLabel);
            this.Controls.Add(this.corYLabel);
            this.Controls.Add(this.corXLabel);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPlace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddPlace";
            ((System.ComponentModel.ISupportInitialize)(this.corXBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corZBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label corXLabel;
        private System.Windows.Forms.Label corYLabel;
        private System.Windows.Forms.Label corZLabel;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.CheckBox fillCheckBox;
        private System.Windows.Forms.NumericUpDown corXBox;
        private System.Windows.Forms.NumericUpDown corYBox;
        private System.Windows.Forms.NumericUpDown corZBox;
    }
}