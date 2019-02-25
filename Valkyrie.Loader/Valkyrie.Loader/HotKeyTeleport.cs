using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Valkyrie.Core;

namespace Valkyrie.Loader
{
    public partial class HotKeyTeleport : MetroForm
    {
        private KeyBinding keyBinding = new KeyBinding();
        private Main _parentForm;
        private Settings _settings;
        private List<Map> _maps;

        public HotKeyTeleport(Main parentForm, Settings settings, List<Map> maps, int id, MetroThemeStyle theme, MetroColorStyle color)
        {
            InitializeComponent();

            _parentForm = parentForm;
            _settings = settings;
            _maps = maps;

            Theme = theme;
            Style = color;

            metroStyleManager.Theme = theme;
            metroStyleManager.Style = color;

            keyBinding.Id = id;
        }

        private void ZoneBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fieldRadioBtn.Checked)
            {
                mapBox.Items.Clear();

                foreach (var map in _maps.Where(m => m.Zone == zoneBox.SelectedItem.ToString() && m.Category == "Field"))
                {
                    mapBox.Items.Add(map.Name);
                }
            }

            if (dungeonRadioBtn.Checked)
            {
                mapBox.Items.Clear();

                foreach (var map in _maps.Where(m => m.Zone == zoneBox.SelectedItem.ToString() && m.Category == "Dungeon"))
                {
                    mapBox.Items.Add(map.Name);
                }
            }
        }

        private void ManageMapBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            placeBox.Items.Clear();
            var map = _maps.Find(m => m.Name == mapBox.Text);
            foreach (var place in map.Places)
            {
                placeBox.Items.Add(place.Name);
            }
        }

        private void PlaceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mapBox.SelectedItem.ToString() == "")
            {
                okBtn.Enabled = false;
                return;
            }

            okBtn.Enabled = true;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            var map = _maps.Find(m => m.Name == mapBox.Text);
            var place = map.Places.Find(p => p.Name == placeBox.Text);

            if (_settings.KeyBinding.FindIndex(k => k.Id == keyBinding.Id) > -1)
            {
                var savedKeyBinding = _settings.KeyBinding.Find(k => k.Id == keyBinding.Id);
                var index = _settings.KeyBinding.IndexOf(savedKeyBinding);

                keyBinding.Key = savedKeyBinding.Key;
                keyBinding.Alias = place.Name;
                keyBinding.Coordinates = place.Coordinates;
                _settings.KeyBinding[index] = keyBinding;
            }
            else
            {
                keyBinding.Key = 0;
                keyBinding.Alias = place.Name;
                keyBinding.Coordinates = place.Coordinates;
                _settings.KeyBinding.Add(keyBinding);
            }

            _parentForm.Settings = _settings;
            Close();
        }
    }
}
