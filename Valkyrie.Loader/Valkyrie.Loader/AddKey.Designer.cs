namespace Valkyrie.Loader
{
    partial class AddKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddKey));
            this.title1Label = new MetroFramework.Controls.MetroLabel();
            this.okBtn = new MetroFramework.Controls.MetroButton();
            this.cancelBtn = new MetroFramework.Controls.MetroButton();
            this.unbindLabel = new MetroFramework.Controls.MetroLink();
            this.keyLabel = new MetroFramework.Controls.MetroLabel();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // title1Label
            // 
            this.title1Label.AutoSize = true;
            this.title1Label.Location = new System.Drawing.Point(43, 60);
            this.title1Label.Name = "title1Label";
            this.title1Label.Size = new System.Drawing.Size(119, 19);
            this.title1Label.TabIndex = 15;
            this.title1Label.Text = "Press a key to bind";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(23, 167);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 16;
            this.okBtn.Text = "OK";
            this.okBtn.UseSelectable = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(105, 167);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 17;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseSelectable = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // unbindLabel
            // 
            this.unbindLabel.Location = new System.Drawing.Point(60, 138);
            this.unbindLabel.Name = "unbindLabel";
            this.unbindLabel.Size = new System.Drawing.Size(75, 23);
            this.unbindLabel.TabIndex = 18;
            this.unbindLabel.Text = "Unbind";
            this.unbindLabel.UseSelectable = true;
            this.unbindLabel.Click += new System.EventHandler(this.UnbindLabel_Click);
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Location = new System.Drawing.Point(79, 99);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(41, 19);
            this.keyLabel.TabIndex = 19;
            this.keyLabel.Text = "None";
            this.keyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            // 
            // AddKey
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(203, 216);
            this.Controls.Add(this.keyLabel);
            this.Controls.Add(this.unbindLabel);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.title1Label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddKey";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Add Key";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel title1Label;
        private MetroFramework.Controls.MetroButton okBtn;
        private MetroFramework.Controls.MetroButton cancelBtn;
        private MetroFramework.Controls.MetroLink unbindLabel;
        private MetroFramework.Controls.MetroLabel keyLabel;
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
    }
}