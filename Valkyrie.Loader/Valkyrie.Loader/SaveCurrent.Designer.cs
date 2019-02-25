namespace Valkyrie.Loader
{
    partial class SaveCurrent
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
            this.CancelBtn = new MetroFramework.Controls.MetroButton();
            this.OkBtn = new MetroFramework.Controls.MetroButton();
            this.nameBox = new MetroFramework.Controls.MetroTextBox();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(122, 92);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 8;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseSelectable = true;
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(23, 92);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 7;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseSelectable = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
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
            this.nameBox.WaterMark = "Name";
            this.nameBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nameBox.SelectedText = "";
            this.nameBox.SelectionLength = 0;
            this.nameBox.SelectionStart = 0;
            this.nameBox.ShortcutsEnabled = true;
            this.nameBox.Size = new System.Drawing.Size(174, 23);
            this.nameBox.TabIndex = 6;
            this.nameBox.UseSelectable = true;
            this.nameBox.WaterMark = "Name";
            this.nameBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.nameBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            // 
            // SaveCurrent
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(220, 150);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.nameBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveCurrent";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "SaveCurrent";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton CancelBtn;
        private MetroFramework.Controls.MetroButton OkBtn;
        private MetroFramework.Controls.MetroTextBox nameBox;
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
    }
}