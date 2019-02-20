using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Valkyrie.Core;

namespace Valkyrie.Loader
{
    public partial class AddPlace : Form
    {
        private JsonStorage storage = new JsonStorage();
        private List<Map> maps = new List<Map>();

        private int _mapId;
        private float _corX, _corY, _corZ;

        private string _placeName;

        public AddPlace(int mapId, string placeName, float corX, float corY, float corZ)
        {
            InitializeComponent();

            maps = storage.RestoreObject<List<Map>>("Resources/data");

            _mapId = mapId;
            _corX = corX;
            _corY = corY;
            _corZ = corZ;

            _placeName = placeName;
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

            if (map.ToString() == null)
            {
                MessageBox.Show("Something wrong happened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (map.Places.FindIndex(i => i.name == nameBox.Text) > -1)
            {
                if (MessageBox.Show("A place with the same name already exists, do you want to replace it?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    return;
                }

                var place = map.Places.Find(p => p.name == nameBox.Text);       
                var index = map.Places.IndexOf(place);

                if (index == -1)
                {
                    MessageBox.Show("Something wrong happened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Place newPlace = new Place()
                {
                    name = nameBox.Text,
                    coordinates = coordinates
                };

                map.Places[index] = newPlace;
            }
            else
            {
                // Add the new place to the map list
                map.Places.Add(new Place
                {
                    name = nameBox.Text,
                    coordinates = coordinates
                });
            }

            storage.StoreObject(maps, "Resources/data");
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
