namespace Valkyrie.Loader
{
    partial class HotKeyTeleport
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
            this.okBtn = new MetroFramework.Controls.MetroButton();
            this.cancelBtn = new MetroFramework.Controls.MetroButton();
            this.zoneBox = new MetroFramework.Controls.MetroComboBox();
            this.dungeonRadioBtn = new MetroFramework.Controls.MetroRadioButton();
            this.fieldRadioBtn = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.mapBox = new MetroFramework.Controls.MetroComboBox();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroStyleExtender = new MetroFramework.Components.MetroStyleExtender(this.components);
            this.placeBox = new MetroFramework.Controls.MetroComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Enabled = false;
            this.okBtn.Location = new System.Drawing.Point(12, 203);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(97, 29);
            this.okBtn.TabIndex = 16;
            this.okBtn.Text = "OK";
            this.okBtn.UseSelectable = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(115, 203);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(97, 29);
            this.cancelBtn.TabIndex = 17;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseSelectable = true;
            // 
            // zoneBox
            // 
            this.zoneBox.FormattingEnabled = true;
            this.zoneBox.IntegralHeight = false;
            this.zoneBox.ItemHeight = 23;
            this.zoneBox.Items.AddRange(new object[] {
            "Mt. Mjolnir Zone",
            "Prontera Zone",
            "Alberta Zone",
            "Morroc Zone",
            "Special Area",
            "Niflheim Zone"});
            this.zoneBox.Location = new System.Drawing.Point(12, 98);
            this.zoneBox.Name = "zoneBox";
            this.zoneBox.PromptText = "Select zone";
            this.zoneBox.Size = new System.Drawing.Size(200, 29);
            this.zoneBox.TabIndex = 38;
            this.zoneBox.UseSelectable = true;
            this.zoneBox.SelectedIndexChanged += new System.EventHandler(this.ZoneBox_SelectedIndexChanged);
            // 
            // dungeonRadioBtn
            // 
            this.dungeonRadioBtn.AutoSize = true;
            this.dungeonRadioBtn.Location = new System.Drawing.Point(140, 75);
            this.dungeonRadioBtn.Name = "dungeonRadioBtn";
            this.dungeonRadioBtn.Size = new System.Drawing.Size(72, 15);
            this.dungeonRadioBtn.TabIndex = 37;
            this.dungeonRadioBtn.Text = "Dungeon";
            this.dungeonRadioBtn.UseSelectable = true;
            // 
            // fieldRadioBtn
            // 
            this.fieldRadioBtn.AutoSize = true;
            this.fieldRadioBtn.Checked = true;
            this.fieldRadioBtn.Location = new System.Drawing.Point(86, 75);
            this.fieldRadioBtn.Name = "fieldRadioBtn";
            this.fieldRadioBtn.Size = new System.Drawing.Size(48, 15);
            this.fieldRadioBtn.TabIndex = 36;
            this.fieldRadioBtn.TabStop = true;
            this.fieldRadioBtn.Text = "Field";
            this.fieldRadioBtn.UseSelectable = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(12, 71);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(39, 19);
            this.metroLabel5.TabIndex = 35;
            this.metroLabel5.Text = "Filter";
            // 
            // mapBox
            // 
            this.mapBox.FormattingEnabled = true;
            this.mapBox.IntegralHeight = false;
            this.mapBox.ItemHeight = 23;
            this.mapBox.Location = new System.Drawing.Point(12, 133);
            this.mapBox.Name = "mapBox";
            this.mapBox.PromptText = "Select map";
            this.mapBox.Size = new System.Drawing.Size(200, 29);
            this.mapBox.TabIndex = 39;
            this.mapBox.UseSelectable = true;
            this.mapBox.SelectedIndexChanged += new System.EventHandler(this.ManageMapBox_SelectedIndexChanged);
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            this.metroStyleManager.Style = MetroFramework.MetroColorStyle.Pink;
            this.metroStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // placeBox
            // 
            this.placeBox.FormattingEnabled = true;
            this.placeBox.ItemHeight = 23;
            this.placeBox.Location = new System.Drawing.Point(12, 168);
            this.placeBox.Name = "placeBox";
            this.placeBox.PromptText = "Select place";
            this.placeBox.Size = new System.Drawing.Size(200, 29);
            this.placeBox.Sorted = true;
            this.placeBox.TabIndex = 40;
            this.placeBox.UseSelectable = true;
            this.placeBox.SelectedIndexChanged += new System.EventHandler(this.PlaceBox_SelectedIndexChanged);
            // 
            // HotKeyTeleport
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(228, 254);
            this.Controls.Add(this.placeBox);
            this.Controls.Add(this.zoneBox);
            this.Controls.Add(this.dungeonRadioBtn);
            this.Controls.Add(this.fieldRadioBtn);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotKeyTeleport";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Teleport Hotkey";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton okBtn;
        private MetroFramework.Controls.MetroButton cancelBtn;
        private MetroFramework.Controls.MetroComboBox zoneBox;
        private MetroFramework.Controls.MetroRadioButton dungeonRadioBtn;
        private MetroFramework.Controls.MetroRadioButton fieldRadioBtn;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox mapBox;
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
        private MetroFramework.Components.MetroStyleExtender metroStyleExtender;
        private MetroFramework.Controls.MetroComboBox placeBox;
    }
}