using System;
using System.Collections.Generic;   
using System.Linq;
using System.Windows.Forms;
using Valkyrie.Core;

namespace Valkyrie.Loader
{
    public partial class EditPlace : Form
    {
        private JsonStorage storage = new JsonStorage();
        private List<Map> maps = new List<Map>();

        private int _mapId;
        private float _corX, _corY, _corZ;
        private float[] _placeCoordinates;

        private string _placeName;

        public EditPlace(int mapId, string placeName, float[] placeCoordinates, float corX, float corY, float corZ)
        {
            InitializeComponent();

            maps = storage.RestoreObject<List<Map>>("Resources/data");

            _mapId = mapId;
            _corX = corX;
            _corY = corY;
            _corZ = corZ;

            _placeName = placeName;
            _placeCoordinates = placeCoordinates;
        }


        private void EditPlace_Load(object sender, EventArgs e)
        {
            nameBox.Text = _placeName;

            corXBox.Value = (decimal)_placeCoordinates[0];
            corYBox.Value = (decimal)_placeCoordinates[1];
            corZBox.Value = (decimal)_placeCoordinates[2];
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
            var map = maps.FirstOrDefault(x => x.Id == _mapId);
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

            storage.StoreObject(maps, "Resources/data");
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
