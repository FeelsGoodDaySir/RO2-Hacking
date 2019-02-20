using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using MetroFramework;
using Valkyrie.Core;

namespace Valkyrie.Loader
{
    public partial class Main : MetroForm
    {
        private Core.Core core = new Core.Core();
        private JsonStorage storage = new JsonStorage();

        private List<Map> maps = new List<Map>();

        private MetroStyle metroStyle = new MetroStyle();

        bool openProc = false;

        private float savedX, savedY, savedZ;

        int nextStyle;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                maps = storage.RestoreObject<List<Map>>("Resources/data");
                metroStyle = storage.RestoreObject<MetroStyle>("Resources/config");

                Theme = (MetroThemeStyle)metroStyle.Theme;
                Style = (MetroColorStyle)metroStyle.Color;
            }
            catch (Exception)
            {
                maps = storage.RestoreObject<List<Map>>("Resources/template");
                storage.StoreObject(maps, "Resources/data");

                Theme = MetroThemeStyle.Dark;
                Style = MetroColorStyle.Pink;
            }

            coreVersionLabel.Text = core.GetCoreVersion();
            gameVersionLabel.Text = core.GetCompatibleVersion();

            metroStyleManager.Theme = Theme;
            metroStyleManager.Style = Style;

            UpdateControlsToTheme();

            if (!BackgroundWorker1.IsBusy)
                BackgroundWorker1.RunWorkerAsync();
        }

        private bool ImAlive()
        {
            if (core.GetPlayerInfo().MaxHp > 0)
            {
                return true;
            }

            return false;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true) // infinite loop
            {
                Process[] processlist = Process.GetProcesses();

                // Check for new processes to add to our list
                foreach (Process theprocess in processlist)
                {
                    // Only add processes that are not currently in the list
                    if (theprocess.ProcessName == "Rag2" && !procBox.Items.Contains(theprocess.Id.ToString()))
                    {
                        procBox.Invoke(new MethodInvoker(delegate
                        {
                            procBox.Items.Add(theprocess.Id.ToString()); // _SelectedIndexChanged will do the rest
                        }));

                        Debug.WriteLine("procID " + theprocess.Id.ToString() + " has been added to the list.");
                    }
                }

                // Store our procBox list in another list prior to removing the item from the procBox list to prevent an error
                List<string> procBoxList = new List<string>();
                foreach (string listItem in procBox.Items)
                {
                    procBoxList.Add(listItem);
                }

                // Check for dead processes and remove them
                foreach (string listItem in procBoxList)
                {
                    try
                    {
                        Process.GetProcessById(Convert.ToInt32(listItem));
                    }
                    catch
                    {
                        procBox.Invoke(new MethodInvoker(delegate
                        {
                            procBox.Items.RemoveAt(procBox.FindString(listItem));
                        }));
                        Debug.WriteLine("procID " + listItem + " has been removed from the list.");
                    }
                }

                if (openProc)
                {
                    core.Update();

                    hpTitleLabel.Invoke((MethodInvoker)delegate
                    {
                        hpTitleLabel.Visible = true;
                    });

                    speedTitleLabel.Invoke((MethodInvoker)delegate
                    {
                        speedTitleLabel.Visible = true;
                    });

                    mapTitleLabel.Invoke((MethodInvoker)delegate
                    {
                        mapTitleLabel.Visible = true;
                    });

                    if (ImAlive())
                    {
                        procStatusLabel.Invoke((MethodInvoker)delegate
                        {
                            injectionLoading.Visible = false;
                            procStatusLabel.Text = "Successfully injected";
                            procStatusLabel.ForeColor = Color.Green;
                        });

                        hpLabel.Invoke((MethodInvoker)delegate
                        {
                            hpLabel.Visible = true;
                            hpLabel.Text = core.GetPlayerInfo().Hp + " / " + core.GetPlayerInfo().MaxHp;
                        });

                        movSpeedLabel.Invoke((MethodInvoker)delegate
                        {
                            movSpeedLabel.Visible = true;
                            movSpeedLabel.Text = core.GetPlayerInfo().MovementSpeed.ToString();
                        });

                        mainMapLabel.Invoke((MethodInvoker)delegate
                        {
                            mainMapLabel.Visible = true;
                            var nameToShow = maps.Where(m => m.Id == core.GetMapInfo().Id).ToList();
                            mainMapLabel.Text = core.GetPlayerInfo().MovementSpeed == 0 ? "Loading..." : !nameToShow.Any() ? "Unknown map" : nameToShow[0].Name;

                        });

                        mapID.Invoke((MethodInvoker)delegate
                        {
                            mapID.Text = core.GetPlayerInfo().MovementSpeed == 0 ? "Loading..." : core.GetMapInfo().Id.ToString();
                        });

                        positionBox.Invoke(new MethodInvoker(delegate
                        {
                            corXLabel.Text = core.GetPlayerInfo().PosX.ToString();
                            corYLabel.Text = core.GetPlayerInfo().PosY.ToString();
                            corZLabel.Text = core.GetPlayerInfo().PosZ.ToString();
                        }));

                        if (movSpeedToggle.Checked)
                        {
                            movSpeedBox.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedBox.Enabled = true;
                            }));

                            movSpeedStatusLabel.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedStatusLabel.Visible = true;
                            }));

                            if (movSpeedBox.Text == "")
                            {
                                movSpeedBox.Invoke(new MethodInvoker(delegate
                                {
                                    movSpeedBox.Text = "0";
                                }));
                            }

                            core.Speedhack(float.Parse(movSpeedBox.Text));
                        }
                        else
                        {
                            movSpeedBox.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedBox.Enabled = false;
                            }));

                            movSpeedBox.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedBox.Text = core.GetPlayerInfo().MovementSpeed.ToString();
                            }));

                            movSpeedStatusLabel.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedStatusLabel.Visible = false;
                            }));
                        }

                        if (wallToggle.Checked)
                        {
                            wallFrictionLabel.Invoke(new MethodInvoker(delegate
                            {
                                wallFrictionLabel.Visible = true;
                            }));

                            core.WallfrictionHack(0);
                        }
                        else
                        {
                            wallFrictionLabel.Invoke(new MethodInvoker(delegate
                            {
                                wallFrictionLabel.Visible = false;
                            }));

                            core.WallfrictionHack(1);
                        }

                        mapLabel.Invoke((MethodInvoker)delegate
                        {
                            var nameToShow = maps.Where(m => m.Id == core.GetMapInfo().Id).ToList();

                            if (core.GetPlayerInfo().MovementSpeed == 0)
                            {
                                mapLabel.Text = "Loading...";
                                mapIdLoading.Visible = true;
                                mapNameLoading.Visible = true;
                                mapLabel.ForeColor = Color.DarkGray;
                            }
                            else if (!nameToShow.Any() && core.GetPlayerInfo().MovementSpeed > 0)
                            {
                                mapLabel.Text = "Unknown map";
                                mapIdLoading.Visible = false;
                                mapNameLoading.Visible = false;
                                mapLabel.ForeColor = Color.Yellow;
                            }
                            else
                            {
                                mapLabel.Text = nameToShow[0].Name;
                                mapIdLoading.Visible = false;
                                mapNameLoading.Visible = false;
                                mapLabel.ForeColor = Color.DarkGray;
                            }
                        });
                    }
                    else
                    {
                        procStatusLabel.Invoke((MethodInvoker)delegate
                        {
                            procStatusLabel.Text = "Waiting to be ingame";
                            procStatusLabel.ForeColor = Color.Magenta;
                        });

                        hpLabel.Invoke((MethodInvoker)delegate
                        {
                            hpLabel.Visible = true;
                        });

                        movSpeedLabel.Invoke((MethodInvoker)delegate
                        {
                            movSpeedLabel.Visible = true;
                        });

                        mainMapLabel.Invoke((MethodInvoker)delegate
                        {
                            mainMapLabel.Visible = true;
                        });

                        savedTeleportBtn.Invoke((MethodInvoker)delegate
                        {
                            savedTeleportBtn.Enabled = false;
                        });

                        savedRollback.Invoke((MethodInvoker)delegate
                        {
                            savedRollback.Enabled = false;
                        });
                    }
                }
                else
                {
                    hpTitleLabel.Invoke((MethodInvoker)delegate
                    {
                        hpTitleLabel.Visible = false;
                    });

                    speedTitleLabel.Invoke((MethodInvoker)delegate
                    {
                        speedTitleLabel.Visible = false;
                    });

                    mapTitleLabel.Invoke((MethodInvoker)delegate
                    {
                        mapTitleLabel.Visible = false;
                    });

                    hpLabel.Invoke((MethodInvoker)delegate
                    {
                        hpLabel.Visible = false;
                    });

                    movSpeedLabel.Invoke((MethodInvoker)delegate
                    {
                        movSpeedLabel.Visible = false;
                    });

                    mainMapLabel.Invoke((MethodInvoker)delegate
                    {
                        mainMapLabel.Visible = false;
                    });
                }

                System.Threading.Thread.Sleep(200);
            }
        }

        private void ProcBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedProc = Convert.ToInt32(procBox.SelectedItem);
            if (selectedProc > 0)
            {
                Debug.WriteLine("Now changing process to " + selectedProc);
                core.Inject(selectedProc);

                // Check if the Game version is compatible with the Core
                if (core.GetCompatibleVersion() != core.GetGameVersion())
                {
                    procStatusLabel.Invoke((MethodInvoker)delegate
                    {
                        injectionLoading.Visible = false;
                        procStatusLabel.Text = "Incompatible game version";
                        procStatusLabel.ForeColor = Color.Red;
                    });

                    return;
                }

                // We have now successfully swapped to another game process!
                openProc = true;
            }
        }

        private void RefreshPlaces()
        {
            maps = storage.RestoreObject<List<Map>>("Resources/data");

            // Refresh Place box

            placeBox.Items.Clear();
            placeBox.ResetText();
            placeBox.Refresh();

            foreach (var map in maps.Where(m => m.Id == core.GetMapInfo().Id))
            {
                if (map.Places == null)
                {
                    return;
                }

                foreach (var place in map.Places)
                {
                    placeBox.Items.Add(place.name);
                }
            }

            // Refresh manageplacebox

            if (managePlacesBox.Text != null)
            {
                managePlacesBox.Items.Clear();

                foreach (var map in maps.Where(m => m.Name == manageMapBox.Text))
                {
                    if (map.Places == null)
                    {
                        return;
                    }

                    foreach (var element in map.Places)
                    {
                        managePlacesBox.Items.Add(element.name);
                    }
                }
            }

            // Reset buttons

            savedTeleportBtn.Enabled = false;
            savedRollback.Enabled = false;
            deletePlaceBtn.Enabled = false;
        }

        private void UpdateControlsToTheme()
        {
            if (metroStyleManager.Theme == MetroThemeStyle.Light)
            {
                managePlacesBox.BackColor = Color.White;
                managePlacesBox.ForeColor = Color.Black;

                corXBox.BackColor = Color.White;
                corXBox.ForeColor = Color.Black;

                corYBox.BackColor = Color.White;
                corYBox.ForeColor = Color.Black;

                corZBox.BackColor = Color.White;
                corZBox.ForeColor = Color.Black;

                movSpeedBox.BackColor = Color.White;
                movSpeedBox.ForeColor = Color.Black;

                outputBox.ForeColor = Color.DimGray;

                title1.ForeColor = Color.DimGray;
                title2.ForeColor = Color.DimGray;
                title3.ForeColor = Color.DimGray;
                title4.ForeColor = Color.DimGray;

                return;
            }

            managePlacesBox.BackColor = Color.FromArgb(27, 27, 27);
            managePlacesBox.ForeColor = Color.Gainsboro;

            corXBox.BackColor = Color.FromArgb(27, 27, 27);
            corXBox.ForeColor = Color.Gainsboro;

            corYBox.BackColor = Color.FromArgb(27, 27, 27);
            corYBox.ForeColor = Color.Gainsboro;

            corZBox.BackColor = Color.FromArgb(27, 27, 27);
            corZBox.ForeColor = Color.Gainsboro;

            movSpeedBox.BackColor = Color.FromArgb(27, 27, 27);
            movSpeedBox.ForeColor = Color.Gainsboro;

            title1.ForeColor = Color.Gainsboro;
            title2.ForeColor = Color.Gainsboro;
            title3.ForeColor = Color.Gainsboro;
            title4.ForeColor = Color.Gainsboro;

            outputBox.ForeColor = Color.Silver;

            if (nextStyle != 2)
            {
                metroTileSwitch.ForeColor = Color.White;
                metroTile1.ForeColor = Color.White;
                return;
            }

            metroTileSwitch.ForeColor = Color.Black;
            metroTile1.ForeColor = Color.Black;
        }

        #region Manual

        private void CorXLabel_DoubleClick(object sender, EventArgs e)
        {
            corXBox.Value = decimal.Parse(corXLabel.Text);
        }

        private void CorYLabel_DoubleClick(object sender, EventArgs e)
        {
            corYBox.Value = decimal.Parse(corYLabel.Text);
        }

        private void CorZLabel_DoubleClick(object sender, EventArgs e)
        {
            corZBox.Value = decimal.Parse(corZLabel.Text);
        }

        private void ManualTeleportBtn_Click(object sender, EventArgs e)
        {
            if (!ImAlive())
            {
                return;
            }

            rollbackBtn.Enabled = true;

            // Save the actual coordinates for emergency backup
            savedX = core.GetPlayerInfo().PosX;
            savedY = core.GetPlayerInfo().PosY;
            savedZ = core.GetPlayerInfo().PosZ;

            // Move to the new coordinates
            core.TeleportHack((float)corXBox.Value, (float)corYBox.Value, (float)corZBox.Value);
        }

        private void RollbackBtn_Click(object sender, EventArgs e)
        {
            if (!ImAlive())
            {
                return;
            }

            // Move to the backup coordinates
            core.TeleportHack(savedX, savedY, savedZ);
        }

        private void SaveCurrentCorsBtn_Click(object sender, EventArgs e)
        {
            if (!ImAlive())
            {
                return;
            }

            var addForm = new SaveCurrent(core.GetMapInfo().Id, core.GetPlayerInfo().PosX, core.GetPlayerInfo().PosY, core.GetPlayerInfo().PosZ);
            addForm.ShowDialog();
            RefreshPlaces();
        }

        #endregion

        #region Saved

        private void MapID_TextChanged(object sender, EventArgs e)
        {
            if (ImAlive())
            {
                // Place box
                placeBox.Items.Clear();
                placeBox.ResetText();
                placeBox.Refresh();

                Console.WriteLine(core.GetMapInfo().Name);

                var map = maps.Find(m => m.Id == core.GetMapInfo().Id);

                if (map.Places == null)
                {
                    return;
                }

                foreach (var place in map.Places)
                {
                    placeBox.Items.Add(place.name);
                }
            }
        }

        private void PlaceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (placeBox.SelectedItem.ToString() == "")
            {
                savedTeleportBtn.Enabled = false;
                return;
            }

            savedTeleportBtn.Enabled = true;
        }

        private void SavedRollback_Click(object sender, EventArgs e)
        {
            if (!ImAlive())
            {
                return;
            }

            core.TeleportHack(savedX, savedY, savedZ);

            // Output
            outputBox.AppendText("Rolled back to: [ x: " + savedX + " y: " + savedY + " z: " + savedZ + " ]");
            outputBox.AppendText(Environment.NewLine);
        }

        private void SavedTeleportBtn_Click(object sender, EventArgs e)
        {
            if (!ImAlive())
            {
                return;
            }

            if (placeBox.SelectedItem.ToString() == "")
            {
                return;
            }

            float[] coordinates = { };

            foreach (var maps in maps.Where(m => m.Id == core.GetMapInfo().Id).ToList())
            {
                foreach (var element in maps.Places.Where(p => p.name == placeBox.Text))
                {
                    coordinates = element.coordinates;
                }
            }

            // Move to the new coordinates
            if (placeBox.SelectedItem != null)
            {
                // Save the actual coordinates for emergency backup
                savedX = core.GetPlayerInfo().PosX;
                savedY = core.GetPlayerInfo().PosY;
                savedZ = core.GetPlayerInfo().PosZ;

                core.TeleportHack(coordinates[0], coordinates[1], coordinates[2]);

                // Output
                outputBox.AppendText("Teleported to: [ x: " + coordinates[0].ToString() + " y: " + coordinates[1].ToString() + " z: " + coordinates[2] + " ]");
                outputBox.AppendText(Environment.NewLine);

                savedRollback.Enabled = true;
            }
        }

        #endregion

        #region Manage

        private void ManagePlacesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (managePlacesBox.SelectedIndex == -1)
            {
                deletePlaceBtn.Enabled = false;
                return;
            }

            deletePlaceBtn.Enabled = true;
        }

        private void ManageMapBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manageMapBox.SelectedItem.ToString() == "")
            {
                addPlaceBtn.Enabled = false;
                return;
            }

            addPlaceBtn.Enabled = true;
        }

        private void ManageMapBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (managePlacesBox.Text != null)
            {
                RefreshPlaces();
            }
        }

        private void AddPlaceBtn_Click(object sender, EventArgs e)
        {
            var map = maps.Find(m => m.Name == manageMapBox.Text);

            if (map.Id == 0)
            {
                return;
            }

            var addplaceForm = new AddPlace(map.Id, "", core.GetPlayerInfo().PosX, core.GetPlayerInfo().PosY, core.GetPlayerInfo().PosZ);
            addplaceForm.ShowDialog();
            RefreshPlaces();
        }

        private void ManagePlacesBox_DoubleClick(object sender, EventArgs e)
        {
            if (managePlacesBox.SelectedIndex == -1)
            {
                return;
            }

            float[] coordiantes;

            var map = maps.Find(m => m.Name == manageMapBox.Text);

            var place = map.Places.Find(p => p.name == managePlacesBox.SelectedItem.ToString());

            coordiantes = place.coordinates;

            var editForm = new EditPlace(map.Id, managePlacesBox.SelectedItem.ToString(), coordiantes, core.GetPlayerInfo().PosX, core.GetPlayerInfo().PosY, core.GetPlayerInfo().PosZ);
            editForm.ShowDialog();
            RefreshPlaces();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (managePlacesBox.SelectedIndex == -1)
            {
                return;
            }

            float[] coordiantes;

            var map = maps.Find(m => m.Name == manageMapBox.Text);

            var place = map.Places.Find(p => p.name == managePlacesBox.SelectedItem.ToString());

            coordiantes = place.coordinates;

            var editForm = new EditPlace(map.Id, managePlacesBox.SelectedItem.ToString(), coordiantes, core.GetPlayerInfo().PosX, core.GetPlayerInfo().PosY, core.GetPlayerInfo().PosZ);
            editForm.ShowDialog();
            RefreshPlaces();
        }

        private void DeletePlaceBtn_Click(object sender, EventArgs e)
        {
            if (managePlacesBox.SelectedIndex == -1)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to permanently delete this place?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }

            var map = maps.FirstOrDefault(x => x.Name == manageMapBox.Text);

            if (map.ToString() == null)
            {
                MessageBox.Show("Something wrong happened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            map.Places.RemoveAll(p => p.name == managePlacesBox.SelectedItem.ToString());

            storage.StoreObject(maps, "Resources/data");
            RefreshPlaces();
        }

        #endregion

        #region About

        private void MetroTile1_Click(object sender, EventArgs e)
        {
            Theme = metroStyleManager.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
            metroStyleManager.Theme = Theme;

            UpdateControlsToTheme();
        }

        private void MetroTileSwitch_Click(object sender, EventArgs e)
        {
            var m = new Random();
            nextStyle = m.Next(0, 13);
            Style = (MetroColorStyle)nextStyle;
            metroStyleManager.Style = Style;

            UpdateControlsToTheme();
        }

        private void ZoneBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fieldRadioBtn.Checked)
            {
                manageMapBox.Items.Clear();

                foreach (var map in maps.Where(m => m.Zone == zoneBox.SelectedItem.ToString() && m.Category == "Field"))
                {
                    manageMapBox.Items.Add(map.Name);
                }
            }

            if (dungeonRadioBtn.Checked)
            {
                manageMapBox.Items.Clear();

                foreach (var map in maps.Where(m => m.Zone == zoneBox.SelectedItem.ToString() && m.Category == "Dungeon"))
                {
                    manageMapBox.Items.Add(map.Name);
                }
            }
        }

        private void CityRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (fieldRadioBtn.Checked)
            {
                manageMapBox.Items.Clear();

                if (zoneBox.SelectedItem == null)
                {
                    return;
                }

                foreach (var map in maps.Where(m => m.Category == "Field" && m.Zone == zoneBox.SelectedItem.ToString()))
                {
                    manageMapBox.Items.Add(map.Name);
                }
            }
        }

        private void ZoneRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (fieldRadioBtn.Checked)
            {
                manageMapBox.Items.Clear();

                if (zoneBox.SelectedItem == null)
                {
                    return;
                }

                foreach (var map in maps.Where(m => m.Category == "Field" && m.Zone == zoneBox.SelectedItem.ToString()))
                {
                    manageMapBox.Items.Add(map.Name);
                }
            }
        }

        private void DungeonRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (dungeonRadioBtn.Checked)
            {
                manageMapBox.Items.Clear();

                if (zoneBox.SelectedItem == null)
                {
                    return;
                }

                foreach (var map in maps.Where(m => m.Category == "Dungeon" && m.Zone == zoneBox.SelectedItem.ToString()))
                {
                    manageMapBox.Items.Add(map.Name);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void GithubLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/madgwee");
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            metroStyle.Theme = (int)Theme;
            metroStyle.Color = (int)Style;

            storage.StoreObject(metroStyle, "Resources/config");

            core.Close();
            Environment.Exit(0);
        }
    }
}
