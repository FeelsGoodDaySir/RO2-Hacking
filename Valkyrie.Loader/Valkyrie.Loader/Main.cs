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
using Valkyrie.Loader.Utils;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;

namespace Valkyrie.Loader
{
    public partial class Main : MetroForm
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys key);

        private readonly string _LOADER_VERSION = "2.0.0 [PUBLIC]";

        private readonly string configPath = "Resources/config.json";
        private readonly string dataPath = "Resources/data.json";

        private Engine engine = new Engine();
        private Storage storage = new Storage();

        public List<Map> Maps { get; set; }
        public Settings Settings { get; set; }
  
        private Process currentProcess;
        bool openProc = false;

        private float movementSpeed;
        private float savedX, savedY, savedZ;

        private int nextStyle;

        public Main()
        {
            InitializeComponent();
            InitializeMainForm();
        }

        private void InitializeMainForm()
        {
            if (!File.Exists(configPath))
            {
                Utilities.GenerateConfigFile();
            }

            if (!File.Exists(dataPath))
            {
                Utilities.GenerateDataFile();
            }

            Maps = storage.RestoreObject<List<Map>>("Resources/data");
            Settings = storage.RestoreObject<Settings>("Resources/config");

            // [24-02-2019] This Loader update needs to reset the previous 
            // version of the config file to enable the hotkeys feature.
            if (Settings.KeyBinding == null)
            {
                Utilities.GenerateConfigFile();
                Settings = storage.RestoreObject<Settings>("Resources/config");
            }

            Theme = (MetroThemeStyle)Settings.MetroStyle.Theme;
            Style = (MetroColorStyle)Settings.MetroStyle.Color;

            metroStyleManager.Theme = (MetroThemeStyle)Settings.MetroStyle.Theme;
            metroStyleManager.Style = (MetroColorStyle)Settings.MetroStyle.Color;
        }

        protected override void OnLoad(EventArgs e)
        {
            RefreshTheme();
            RefreshHotKeys();

            coreVersionLabel.Text = engine.GetCoreVersion();
            gameVersionLabel.Text = engine.GetCompatibleVersion();
            loaderVersionLabel.Text = _LOADER_VERSION;

            base.OnLoad(e);

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/madgwee/RO2-Hacking/master/Valkyrie.System/changelog.txt", "Changelog.txt");
                    StreamReader sr = new StreamReader("Changelog.txt");

                    string changelog = "";
                    while (!sr.EndOfStream)
                    {
                        changelog += sr.ReadLine() + Environment.NewLine;
                    }
                    changelogBox.Text = changelog;
                }
            }
            catch
            {
                changelogBox.Text = "Couldn't fetch the data.";
            }

            if (!BackgroundWorker1.IsBusy)
            {
                BackgroundWorker1.RunWorkerAsync();
            }
        }

        private bool ImAlive()
        {
            if (engine.GetPlayerInfo().MaxHp > 0)
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
                    engine.Update();

                    procBox.Invoke(new MethodInvoker(delegate
                    {
                        currentProcess = Process.GetProcessById(Convert.ToInt32(procBox.SelectedItem));
                    }));

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
                        // Handle our Hotkey teleports
                        IntPtr activatedHandle = GetForegroundWindow();
                        if (activatedHandle == currentProcess.MainWindowHandle)
                        {
                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[0].Key)) && Settings.KeyBinding[0].Key != Keys.None && Settings.KeyBinding[0].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[0].Coordinates[0], Settings.KeyBinding[0].Coordinates[1], Settings.KeyBinding[0].Coordinates[2]);

                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[1].Key)) && Settings.KeyBinding[1].Key != Keys.None && Settings.KeyBinding[1].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[1].Coordinates[0], Settings.KeyBinding[1].Coordinates[1], Settings.KeyBinding[1].Coordinates[2]);

                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[2].Key)) && Settings.KeyBinding[2].Key != Keys.None && Settings.KeyBinding[2].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[2].Coordinates[0], Settings.KeyBinding[2].Coordinates[1], Settings.KeyBinding[2].Coordinates[2]);

                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[3].Key)) && Settings.KeyBinding[3].Key != Keys.None && Settings.KeyBinding[3].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[3].Coordinates[0], Settings.KeyBinding[3].Coordinates[1], Settings.KeyBinding[3].Coordinates[2]);

                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[4].Key)) && Settings.KeyBinding[4].Key != Keys.None && Settings.KeyBinding[4].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[4].Coordinates[0], Settings.KeyBinding[4].Coordinates[1], Settings.KeyBinding[4].Coordinates[2]);

                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[5].Key)) && Settings.KeyBinding[5].Key != Keys.None && Settings.KeyBinding[5].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[5].Coordinates[0], Settings.KeyBinding[5].Coordinates[1], Settings.KeyBinding[5].Coordinates[2]);

                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[6].Key)) && Settings.KeyBinding[6].Key != Keys.None && Settings.KeyBinding[6].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[6].Coordinates[0], Settings.KeyBinding[6].Coordinates[1], Settings.KeyBinding[6].Coordinates[2]);

                            if (Convert.ToBoolean(GetAsyncKeyState(Settings.KeyBinding[7].Key)) && Settings.KeyBinding[7].Key != Keys.None && Settings.KeyBinding[7].Alias != "None")
                                engine.TeleportHack(Settings.KeyBinding[7].Coordinates[0], Settings.KeyBinding[7].Coordinates[1], Settings.KeyBinding[7].Coordinates[2]);
                        }

                        // Update main tab labels
                        procStatusLabel.Invoke((MethodInvoker)delegate
                        {
                            injectionLoading.Visible = false;
                            procStatusLabel.Text = "Successfully injected";
                            procStatusLabel.ForeColor = Color.Green;
                        });

                        hpLabel.Invoke((MethodInvoker)delegate
                        {
                            hpLabel.Visible = true;
                            hpLabel.Text = engine.GetPlayerInfo().Hp + " / " + engine.GetPlayerInfo().MaxHp;
                        });

                        movSpeedLabel.Invoke((MethodInvoker)delegate
                        {
                            movSpeedLabel.Visible = true;
                            movSpeedLabel.Text = engine.GetPlayerInfo().MovementSpeed.ToString();
                        });

                        mainMapLabel.Invoke((MethodInvoker)delegate
                        {
                            mainMapLabel.Visible = true;
                            var nameToShow = Maps.Where(m => m.Id == engine.GetMapInfo().Id).ToList();
                            mainMapLabel.Text = engine.GetPlayerInfo().MovementSpeed == 0 ? "Loading..." : !nameToShow.Any() ? "Unknown map" : nameToShow[0].Name;

                        });

                        mapID.Invoke((MethodInvoker)delegate
                        {
                            mapID.Text = engine.GetPlayerInfo().MovementSpeed == 0 ? "Loading..." : engine.GetMapInfo().Id.ToString();
                        });

                        positionBox.Invoke(new MethodInvoker(delegate
                        {
                            corXLabel.Text = engine.GetPlayerInfo().PosX.ToString();
                            corYLabel.Text = engine.GetPlayerInfo().PosY.ToString();
                            corZLabel.Text = engine.GetPlayerInfo().PosZ.ToString();
                        }));

                        // Handle speed hack checkbox
                        if (movSpeedToggle.Checked)
                        {
                            // Enable the speed hack box
                            movSpeedBox.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedBox.Enabled = true;
                            }));

                            // Handle speed hack
                            bool speedInputValidation = float.TryParse(movSpeedBox.Text, out float speedHackTest);
                            if (speedInputValidation)
                            {
                                // Start the speed hack
                                movementSpeed = float.Parse(movSpeedBox.Text);
                                engine.Speedhack(movementSpeed);

                                // Show a success message to the user through a hidden label
                                movSpeedStatusLabel.Invoke(new MethodInvoker(delegate
                                {
                                    movSpeedStatusLabel.Visible = true;
                                }));
                            }
                        }
                        else
                        {
                            // Disable the speed hack box
                            movSpeedBox.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedBox.Enabled = false;
                            }));

                            // Stop the speed hack and fill the box with the original player movement speed value
                            movSpeedBox.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedBox.Text = engine.GetPlayerInfo().MovementSpeed.ToString();
                            }));

                            // Hide the success message
                            movSpeedStatusLabel.Invoke(new MethodInvoker(delegate
                            {
                                movSpeedStatusLabel.Visible = false;
                            }));
                        }

                        // Handle wall hack checkbox
                        if (wallToggle.Checked)
                        {
                            // Start the wall friction hack
                            engine.WallfrictionHack(0);

                            // Show a success message to the user through a hidden label
                            wallFrictionLabel.Invoke(new MethodInvoker(delegate
                            {
                                wallFrictionLabel.Visible = true;
                            }));
                        }
                        else
                        {
                            // Stop the wall friction hack
                            engine.WallfrictionHack(1);

                            // Hide the success message
                            wallFrictionLabel.Invoke(new MethodInvoker(delegate
                            {
                                wallFrictionLabel.Visible = false;
                            }));
                        }

                        mapLabel.Invoke((MethodInvoker)delegate
                        {
                            var nameToShow = Maps.Where(m => m.Id == engine.GetMapInfo().Id).ToList();

                            if (engine.GetPlayerInfo().MovementSpeed == 0)
                            {
                                mapLabel.Text = "Loading...";
                                mapIdLoading.Visible = true;
                                mapNameLoading.Visible = true;
                                mapLabel.ForeColor = Color.DarkGray;
                            }
                            else if (!nameToShow.Any() && engine.GetPlayerInfo().MovementSpeed > 0)
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
                engine.Inject(selectedProc);

                // Check if the Game version is compatible with the Core
                if (engine.GetCompatibleVersion() != engine.GetGameVersion())
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
            // Refresh Place box

            placeBox.Items.Clear();
            placeBox.ResetText();
            placeBox.Refresh();

            foreach (var map in Maps.Where(m => m.Id == engine.GetMapInfo().Id))
            {
                if (map.Places == null)
                {
                    return;
                }

                foreach (var place in map.Places)
                {
                    placeBox.Items.Add(place.Name);
                }
            }

            // Refresh manageplacebox

            if (managePlacesBox.Text != null)
            {
                managePlacesBox.Items.Clear();

                foreach (var map in Maps.Where(m => m.Name == manageMapBox.Text))
                {
                    if (map.Places == null)
                    {
                        return;
                    }

                    foreach (var element in map.Places)
                    {
                        managePlacesBox.Items.Add(element.Name);
                    }
                }
            }

            // Reset buttons

            savedTeleportBtn.Enabled = false;
            savedRollback.Enabled = false;
            deletePlaceBtn.Enabled = false;
        }

        private void RefreshHotKeys()
        {
            teleportKey1Box.Text = Settings.KeyBinding[0].Key.ToString();
            teleportKey2Box.Text = Settings.KeyBinding[1].Key.ToString();
            teleportKey3Box.Text = Settings.KeyBinding[2].Key.ToString();
            teleportKey4Box.Text = Settings.KeyBinding[3].Key.ToString();
            teleportKey5Box.Text = Settings.KeyBinding[4].Key.ToString();
            teleportKey6Box.Text = Settings.KeyBinding[5].Key.ToString();
            teleportKey7Box.Text = Settings.KeyBinding[6].Key.ToString();
            teleportKey8Box.Text = Settings.KeyBinding[7].Key.ToString();

            teleportCors1Box.Text = Settings.KeyBinding[0].Alias;
            teleportCors2Box.Text = Settings.KeyBinding[1].Alias;
            teleportCors3Box.Text = Settings.KeyBinding[2].Alias;
            teleportCors4Box.Text = Settings.KeyBinding[3].Alias;
            teleportCors5Box.Text = Settings.KeyBinding[4].Alias;
            teleportCors6Box.Text = Settings.KeyBinding[5].Alias;
            teleportCors7Box.Text = Settings.KeyBinding[6].Alias;
            teleportCors8Box.Text = Settings.KeyBinding[7].Alias;
        }

        private void RefreshTheme()
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

                title1.ForeColor = Color.Black;
                title2.ForeColor = Color.Black;
                title3.ForeColor = Color.Black;
                title4.ForeColor = Color.Black;

                teleportKey1Box.BackColor = Color.White;
                teleportKey1Box.ForeColor = Color.Black;

                teleportKey2Box.BackColor = Color.White;
                teleportKey2Box.ForeColor = Color.Black;

                teleportKey3Box.BackColor = Color.White;
                teleportKey3Box.ForeColor = Color.Black;

                teleportKey4Box.BackColor = Color.White;
                teleportKey4Box.ForeColor = Color.Black;

                teleportKey5Box.BackColor = Color.White;
                teleportKey5Box.ForeColor = Color.Black;

                teleportKey6Box.BackColor = Color.White;
                teleportKey6Box.ForeColor = Color.Black;

                teleportKey7Box.BackColor = Color.White;
                teleportKey7Box.ForeColor = Color.Black;

                teleportKey8Box.BackColor = Color.White;
                teleportKey8Box.ForeColor = Color.Black;

                teleportCors1Box.BackColor = Color.White;
                teleportCors1Box.ForeColor = Color.Black;

                teleportCors2Box.BackColor = Color.White;
                teleportCors2Box.ForeColor = Color.Black;

                teleportCors3Box.BackColor = Color.White;
                teleportCors3Box.ForeColor = Color.Black;

                teleportCors4Box.BackColor = Color.White;
                teleportCors4Box.ForeColor = Color.Black;

                teleportCors5Box.BackColor = Color.White;
                teleportCors5Box.ForeColor = Color.Black;

                teleportCors6Box.BackColor = Color.White;
                teleportCors6Box.ForeColor = Color.Black;

                teleportCors7Box.BackColor = Color.White;
                teleportCors7Box.ForeColor = Color.Black;

                teleportCors8Box.BackColor = Color.White;
                teleportCors8Box.ForeColor = Color.Black;

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

            teleportKey1Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey1Box.ForeColor = Color.Gainsboro;

            teleportKey2Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey2Box.ForeColor = Color.Gainsboro;

            teleportKey3Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey3Box.ForeColor = Color.Gainsboro;

            teleportKey4Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey4Box.ForeColor = Color.Gainsboro;

            teleportKey5Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey5Box.ForeColor = Color.Gainsboro;

            teleportKey6Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey6Box.ForeColor = Color.Gainsboro;

            teleportKey7Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey7Box.ForeColor = Color.Gainsboro;

            teleportKey8Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportKey8Box.ForeColor = Color.Gainsboro;

            teleportCors1Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors1Box.ForeColor = Color.Gainsboro;

            teleportCors2Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors2Box.ForeColor = Color.Gainsboro;

            teleportCors3Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors3Box.ForeColor = Color.Gainsboro;

            teleportCors4Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors4Box.ForeColor = Color.Gainsboro;

            teleportCors5Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors5Box.ForeColor = Color.Gainsboro;

            teleportCors6Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors6Box.ForeColor = Color.Gainsboro;

            teleportCors7Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors7Box.ForeColor = Color.Gainsboro;

            teleportCors8Box.BackColor = Color.FromArgb(27, 27, 27);
            teleportCors8Box.ForeColor = Color.Gainsboro;

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
            savedX = engine.GetPlayerInfo().PosX;
            savedY = engine.GetPlayerInfo().PosY;
            savedZ = engine.GetPlayerInfo().PosZ;

            // Move to the new coordinates
            engine.TeleportHack((float)corXBox.Value, (float)corYBox.Value, (float)corZBox.Value);
        }

        private void RollbackBtn_Click(object sender, EventArgs e)
        {
            if (!ImAlive())
            {
                return;
            }

            // Move to the backup coordinates
            engine.TeleportHack(savedX, savedY, savedZ);
        }

        private void SaveCurrentCorsBtn_Click(object sender, EventArgs e)
        {
            if (!ImAlive())
            {
                return;
            }

            var addForm = new SaveCurrent(this, Settings, metroStyleManager.Theme, metroStyleManager.Style, Maps, engine.GetMapInfo().Id, engine.GetPlayerInfo().PosX, engine.GetPlayerInfo().PosY, engine.GetPlayerInfo().PosZ);
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

                Console.WriteLine(engine.GetMapInfo().Name);

                var map = Maps.Find(m => m.Id == engine.GetMapInfo().Id);

                if (map.Places == null)
                {
                    return;
                }

                foreach (var place in map.Places)
                {
                    placeBox.Items.Add(place.Name);
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

            engine.TeleportHack(savedX, savedY, savedZ);

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

            foreach (var maps in Maps.Where(m => m.Id == engine.GetMapInfo().Id).ToList())
            {
                foreach (var element in maps.Places.Where(p => p.Name == placeBox.Text))
                {
                    coordinates = element.Coordinates;
                }
            }

            // Move to the new coordinates
            if (placeBox.SelectedItem != null)
            {
                // Save the actual coordinates for emergency backup
                savedX = engine.GetPlayerInfo().PosX;
                savedY = engine.GetPlayerInfo().PosY;
                savedZ = engine.GetPlayerInfo().PosZ;

                engine.TeleportHack(coordinates[0], coordinates[1], coordinates[2]);

                // Output
                outputBox.AppendText("Teleported to: [ x: " + coordinates[0].ToString() + " y: " + coordinates[1].ToString() + " z: " + coordinates[2] + " ]");
                outputBox.AppendText(Environment.NewLine);

                savedRollback.Enabled = true;
            }
        }

        #endregion

        #region Manage

        private void ZoneBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fieldRadioBtn.Checked)
            {
                manageMapBox.Items.Clear();

                foreach (var map in Maps.Where(m => m.Zone == zoneBox.SelectedItem.ToString() && m.Category == "Field"))
                {
                    manageMapBox.Items.Add(map.Name);
                }
            }

            if (dungeonRadioBtn.Checked)
            {
                manageMapBox.Items.Clear();

                foreach (var map in Maps.Where(m => m.Zone == zoneBox.SelectedItem.ToString() && m.Category == "Dungeon"))
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

                foreach (var map in Maps.Where(m => m.Category == "Field" && m.Zone == zoneBox.SelectedItem.ToString()))
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

                foreach (var map in Maps.Where(m => m.Category == "Field" && m.Zone == zoneBox.SelectedItem.ToString()))
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

                foreach (var map in Maps.Where(m => m.Category == "Dungeon" && m.Zone == zoneBox.SelectedItem.ToString()))
                {
                    manageMapBox.Items.Add(map.Name);
                }
            }
        }

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
            var map = Maps.Find(m => m.Name == manageMapBox.Text);

            if (map.Id == 0)
            {
                return;
            }

            var addplaceForm = new AddPlace(this, Settings, metroStyleManager.Theme, metroStyleManager.Style, Maps, map.Id, "", engine.GetPlayerInfo().PosX, engine.GetPlayerInfo().PosY, engine.GetPlayerInfo().PosZ);
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

            var map = Maps.Find(m => m.Name == manageMapBox.Text);

            var place = map.Places.Find(p => p.Name == managePlacesBox.SelectedItem.ToString());

            coordiantes = place.Coordinates;

            var editForm = new EditPlace(this, Settings, metroStyleManager.Theme, metroStyleManager.Style, Maps, map.Id, managePlacesBox.SelectedItem.ToString(), coordiantes, engine.GetPlayerInfo().PosX, engine.GetPlayerInfo().PosY, engine.GetPlayerInfo().PosZ);
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

            var map = Maps.Find(m => m.Name == manageMapBox.Text);

            var place = map.Places.Find(p => p.Name == managePlacesBox.SelectedItem.ToString());

            coordiantes = place.Coordinates;

            var editForm = new EditPlace(this, Settings, metroStyleManager.Theme, metroStyleManager.Style, Maps, map.Id, managePlacesBox.SelectedItem.ToString(), coordiantes, engine.GetPlayerInfo().PosX, engine.GetPlayerInfo().PosY, engine.GetPlayerInfo().PosZ);
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

            var map = Maps.FirstOrDefault(x => x.Name == manageMapBox.Text);

            if (map.ToString() == null)
            {
                MessageBox.Show("Something wrong happened", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            map.Places.RemoveAll(p => p.Name == managePlacesBox.SelectedItem.ToString());

            RefreshPlaces();
        }

        #endregion

        #region Hotkeys

        private void SaveHotKey(int id)
        {
            AddKey addKey = new AddKey(this, Settings, metroStyleManager.Theme, metroStyleManager.Style, id);
            addKey.ShowDialog();
            RefreshHotKeys();
        }

        private void SaveTeleportHotKey(int id)
        {
            HotKeyTeleport hotKeyTeleport = new HotKeyTeleport(this, Settings, Maps, id, metroStyleManager.Theme, metroStyleManager.Style);
            hotKeyTeleport.ShowDialog();
            RefreshHotKeys();
        }

        private void TeleportKey1Box_DoubleClick(object sender, EventArgs e)
        {
            SaveHotKey(1);
        }

        private void TeleportKey2Box_DoubleClick(object sender, EventArgs e)
        {   
            SaveHotKey(2);
        }

        private void TeleportKey3Box_DoubleClick(object sender, EventArgs e)
        {
            SaveHotKey(3);
        }

        private void TeleportKey4Box_DoubleClick(object sender, EventArgs e)
        {
            SaveHotKey(4);
        }

        private void TeleportKey5Box_DoubleClick(object sender, EventArgs e)
        {
            SaveHotKey(5);
        }

        private void TeleportKey6Box_DoubleClick(object sender, EventArgs e)
        {
            SaveHotKey(6);
        }

        private void TeleportKey7Box_DoubleClick(object sender, EventArgs e)
        {
            SaveHotKey(7);
        }

        private void TeleportKey8Box_DoubleClick(object sender, EventArgs e)
        {
            SaveHotKey(8);
        }

        private void TeleportCors1Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(1);
        }

        private void TeleportCors2Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(2);
        }

        private void TeleportCors3Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(3);
        }

        private void TeleportCors4Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(4);
        }

        private void TeleportCors5Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(5);
        }

        private void TeleportCors6Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(6);
        }

        private void TeleportCors7Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(7);
        }

        private void TeleportCors8Box_DoubleClick(object sender, EventArgs e)
        {
            SaveTeleportHotKey(8);
        }

        #endregion

        #region About

        private void MetroTile1_Click(object sender, EventArgs e)
        {
            Theme = metroStyleManager.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
            metroStyleManager.Theme = Theme;

            RefreshTheme();
        }

        private void MetroTileSwitch_Click(object sender, EventArgs e)
        {
            var m = new Random();
            nextStyle = m.Next(0, 13);
            Style = (MetroColorStyle)nextStyle;
            metroStyleManager.Style = Style;

            RefreshTheme();
        }

        private void GithubLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/madgwee");
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            MetroStyle metroStyle = new MetroStyle
            {
                Theme = (int)Theme,
                Color = (int)Style
            };

            Settings.MetroStyle = metroStyle;

            storage.StoreObject(Settings, "Resources/config");
            storage.StoreObject(Maps, "Resources/data");

            engine.Close();
            Environment.Exit(0);
        }
    }
}
