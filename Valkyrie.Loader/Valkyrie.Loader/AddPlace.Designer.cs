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
            this.components = new System.ComponentModel.Container();
            this.nameBox = new MetroFramework.Controls.MetroTextBox();
            this.OkBtn = new MetroFramework.Controls.MetroButton();
            this.CancelBtn = new MetroFramework.Controls.MetroButton();
            this.corZBox = new System.Windows.Forms.NumericUpDown();
            this.corYBox = new System.Windows.Forms.NumericUpDown();
            this.corXBox = new System.Windows.Forms.NumericUpDown();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.fillCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.corZBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corXBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            // 
            // 
            // 
            this.nameBox.CustomButton.Image = null;
            this.nameBox.CustomButton.Location = new System.Drawing.Point(152, 1);
            this.nameBox.CustomButton.Name = "";
            this.nameBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.nameBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.nameBox.CustomButton.TabIndex = 1;
            this.nameBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.nameBox.CustomButton.UseSelectable = true;
            this.nameBox.CustomButton.Visible = false;
            this.nameBox.Lines = new string[0];
            this.nameBox.Location = new System.Drawing.Point(23, 63);
            this.nameBox.MaxLength = 32767;
            this.nameBox.Name = "nameBox";
            this.nameBox.PasswordChar = '\0';
            this.nameBox.PromptText = "Name";
            this.nameBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nameBox.SelectedText = "";
            this.nameBox.SelectionLength = 0;
            this.nameBox.SelectionStart = 0;
            this.nameBox.ShortcutsEnabled = true;
            this.nameBox.Size = new System.Drawing.Size(174, 23);
            this.nameBox.TabIndex = 3;
            this.nameBox.UseSelectable = true;
            this.nameBox.WaterMark = "Name";
            this.nameBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.nameBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(23, 248);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 4;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseSelectable = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(122, 248);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseSelectable = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // corZBox
            // 
            this.corZBox.DecimalPlaces = 6;
            this.corZBox.Location = new System.Drawing.Point(23, 201);
            this.corZBox.Name = "corZBox";
            this.corZBox.Size = new System.Drawing.Size(174, 20);
            this.corZBox.TabIndex = 28;
            // 
            // corYBox
            // 
            this.corYBox.DecimalPlaces = 6;
            this.corYBox.Location = new System.Drawing.Point(23, 156);
            this.corYBox.Name = "corYBox";
            this.corYBox.Size = new System.Drawing.Size(174, 20);
            this.corYBox.TabIndex = 27;
            // 
            // corXBox
            // 
            this.corXBox.DecimalPlaces = 6;
            this.corXBox.Location = new System.Drawing.Point(23, 111);
            this.corXBox.Name = "corXBox";
            this.corXBox.Size = new System.Drawing.Size(174, 20);
            this.corXBox.TabIndex = 26;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 89);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(90, 19);
            this.metroLabel1.TabIndex = 29;
            this.metroLabel1.Text = "Coordinate X:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 134);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(90, 19);
            this.metroLabel2.TabIndex = 30;
            this.metroLabel2.Text = "Coordinate Y:";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(23, 179);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(90, 19);
            this.metroLabel3.TabIndex = 31;
            this.metroLabel3.Text = "Coordinate Z:";
            // 
            // fillCheckBox
            // 
            this.fillCheckBox.AutoSize = true;
            this.fillCheckBox.Location = new System.Drawing.Point(23, 227);
            this.fillCheckBox.Name = "fillCheckBox";
            this.fillCheckBox.Size = new System.Drawing.Size(151, 15);
            this.fillCheckBox.TabIndex = 32;
            this.fillCheckBox.Text = "Fill with current position";
            this.fillCheckBox.UseSelectable = true;
            this.fillCheckBox.CheckedChanged += new System.EventHandler(this.FillCheckBox_CheckedChanged);
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            // 
            // AddPlace
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(220, 294);
            this.Controls.Add(this.fillCheckBox);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.corZBox);
            this.Controls.Add(this.corYBox);
            this.Controls.Add(this.corXBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.nameBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPlace";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "AddPlace";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            ((System.ComponentModel.ISupportInitialize)(this.corZBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corXBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox nameBox;
        private MetroFramework.Controls.MetroButton OkBtn;
        private MetroFramework.Controls.MetroButton CancelBtn;
        private System.Windows.Forms.NumericUpDown corZBox;
        private System.Windows.Forms.NumericUpDown corYBox;
        private System.Windows.Forms.NumericUpDown corXBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroCheckBox fillCheckBox;
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
    }
}