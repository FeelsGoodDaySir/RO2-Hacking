using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace Valkyrie.Loader
{
    public partial class AddKey : MetroForm
    {
        private int _keyId;
        private Main _parentForm;
        private Settings _settings;
        private KeyBinding keyBinding = new KeyBinding();

        public AddKey(Main parentForm, Settings settings, MetroThemeStyle theme, MetroColorStyle color, int keyId)
        {
            InitializeComponent();

            _parentForm = parentForm;
            _settings = settings;
            _keyId = keyId;

            Theme = theme;
            Style = color;

            metroStyleManager.Theme = theme;
            metroStyleManager.Style = color;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            keyLabel.Text = _settings.KeyBinding[_settings.KeyBinding.FindIndex(k => k.Id == _keyId)].Key.ToString();

            title1Label.Left = (ClientSize.Width - title1Label.Width) / 2;
            keyLabel.Left = (ClientSize.Width - keyLabel.Width) / 2;
            unbindLabel.Left = (ClientSize.Width - unbindLabel.Width) / 2;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                var savedKeyBinding = _settings.KeyBinding.Find(k => k.Id == _keyId);
                return base.ProcessCmdKey(ref msg, savedKeyBinding.Key);
            }

            keyBinding.Key = keyData;

            keyLabel.Text = keyData.ToString();
            keyLabel.Left = (ClientSize.Width - keyLabel.Width) / 2;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (_settings.KeyBinding.FindAll(k => k.Key == keyBinding.Key).Count > 0)
            {
                if (keyBinding.Key != _settings.KeyBinding[_settings.KeyBinding.FindIndex(k => k.Id == _keyId)].Key)
                {
                    MessageBox.Show("This hotkey already exist! Please re-bind to another key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (_settings.KeyBinding.FindIndex(k => k.Id == _keyId) > -1 && keyBinding.Key != Keys.None)
            {
                var savedKeyBinding = _settings.KeyBinding.Find(k => k.Id == _keyId);
                var index = _settings.KeyBinding.IndexOf(savedKeyBinding);

                keyBinding.Id = savedKeyBinding.Id;
                keyBinding.Alias = savedKeyBinding.Alias;
                keyBinding.Coordinates = savedKeyBinding.Coordinates;

                _settings.KeyBinding[index] = keyBinding;
            }

            _parentForm.Settings = _settings;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UnbindLabel_Click(object sender, EventArgs e)
        {
            keyBinding.Key = Keys.None;
            keyLabel.Text = Keys.None.ToString();
            keyLabel.Left = (ClientSize.Width - keyLabel.Width) / 2;
        }
    }
}
