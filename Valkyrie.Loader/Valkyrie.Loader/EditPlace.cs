using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Valkyrie.Core;

namespace Valkyrie.Loader
{
    public partial class EditPlace : MetroForm
    {
        private Main _parentForm;
        private List<Map> _maps;

        private int _mapId;
        private float _corX, _corY, _corZ;
        private float[] _placeCoordinates;

        private string _placeName;

        public EditPlace(Main parentForm, Settings settings, MetroThemeStyle theme, MetroColorStyle color, 
            List<Map> maps, int mapId, string placeName, float[] placeCoordinates, float corX, float corY, float corZ)
        {
            InitializeComponent();

            _parentForm = parentForm;
            _maps = maps;

            _mapId = mapId;
            _corX = corX;
            _corY = corY;
            _corZ = corZ;

            _placeName = placeName;
            _placeCoordinates = placeCoordinates;

            Theme = theme;
            Style = color;

            metroStyleManager.Theme = theme;
            metroStyleManager.Style = color;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            nameBox.Text = _placeName;

            corXBox.Value = (decimal)_placeCoordinates[0];
            corYBox.Value = (decimal)_placeCoordinates[1];
            corZBox.Value = (decimal)_placeCoordinates[2];

            if (metroStyleManager.Theme == MetroThemeStyle.Light)
            {
                corXBox.BackColor = Color.White;
                corXBox.ForeColor = Color.Black;

                corYBox.BackColor = Color.White;
                corYBox.ForeColor = Color.Black;

                corZBox.BackColor = Color.White;
                corZBox.ForeColor = Color.Black;

                return;
            }

            corXBox.BackColor = Color.FromArgb(27, 27, 27);
            corXBox.ForeColor = Color.Gainsboro;

            corYBox.BackColor = Color.FromArgb(27, 27, 27);
            corYBox.ForeColor = Color.Gainsboro;

            corZBox.BackColor = Color.FromArgb(27, 27, 27);
            corZBox.ForeColor = Color.Gainsboro;
        }

        private void FillCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!fillCheckBox.Checked)
            {
                corXBox.Enabled = true;
                corYBox.Enabled = true;
                corZBox.Enabled = true;

                return;
            }

            corXBox.Value = (decimal)_corX;
            corYBox.Value = (decimal)_corY;
            corZBox.Value = (decimal)_corZ;

            corXBox.Enabled = false;
            corYBox.Enabled = false;
            corZBox.Enabled = false;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (nameBox.Text == "")
            {
                MessageBox.Show("Please enter a valid place name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float[] coordinates = { (float)corXBox.Value, (float)corYBox.Value, (float)corZBox.Value };
            var map = _maps.FirstOrDefault(x => x.Id == _mapId);
            var place = map.Places.Find(p => p.Name == _placeName);

            if (map.ToString() == null || place.ToString() == null)
            {
                MessageBox.Show("Something wrong happened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Place newPlace = new Place()
            {
                Name = nameBox.Text,
                Coordinates = coordinates
            };

            var index = map.Places.IndexOf(place);

            if (index == -1)
            {
                MessageBox.Show("Something wrong happened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            map.Places[index] = newPlace;

            _parentForm.Maps = _maps;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
