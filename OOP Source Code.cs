using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DbD_Settings_Changer
{
    public partial class Form1 : Form
    {

        public string SettingsPath = "";
        public string EnginePath = "";



        public string SteamSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsNoEditor\GameUserSettings.ini";
        public string SteamEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsNoEditor\Engine.ini";


        public string EGSSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\EGS\GameUserSettings.ini";
        public string EGSEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\EGS\Engine.ini";


        public string MSSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\BehaviourInteractive.DeadbyDaylightWindows_b1gz2xhdanwfm\LocalCache\Local\DeadByDaylight\Saved\Config\GRDKNoEditor\GameUserSettings.ini";
        public string MSEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"Packages\BehaviourInteractive.DeadbyDaylightWindows_b1gz2xhdanwfm\LocalCache\Local\DeadByDaylight\Saved\Config\GRDKNoEditor\Engine.ini";


        public string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Autosave\";

        ToolTip tip = new ToolTip();

        public string UsersSettingsContent;
        public string UsersEngineContent;

        string ReadConfig;
        string ReadEngine;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var CheckDate = new CheckDate(imgPumpkin, imgXmasTree, imgFireworks);

            btnPresetLow.BackColor = Color.FromArgb(224, 224, 224);
            btnPresetLow.ForeColor = Color.Black;
            btnPresetMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnPresetMedium.ForeColor = Color.Black;
            btnPresetEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnPresetEpic.ForeColor = Color.Black;

            this.Size = new Size(1145, 1022);
            label2.BackColor = Color.Crimson;
            label2.Location = new Point(label2.Location.X, 2);
            label2.Size = new Size(label2.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 2);
            label12.Size = new Size(label12.Size.Width, 40);

            label14.Location = new Point(label14.Location.X, 2);
            label14.Size = new Size(label14.Size.Width, 40);

            label16.Location = new Point(label16.Location.X, 2);
            label16.Size = new Size(label16.Size.Width, 40);

            label19.Location = new Point(label19.Location.X, 2);
            label19.Size = new Size(label19.Size.Width, 40);


            label15.Location = new Point(label15.Location.X, 2);
            label15.Size = new Size(label15.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 4);
            label14.Location = new Point(label14.Location.X, 4);
            label16.Location = new Point(label16.Location.X, 4);
            label19.Location = new Point(label19.Location.X, 4);
            label15.Location = new Point(label15.Location.X, 4);

            label12.BackColor = Color.FromArgb(57, 59, 57);
            label14.BackColor = Color.FromArgb(57, 59, 57);
            label16.BackColor = Color.FromArgb(57, 59, 57);
            label19.BackColor = Color.FromArgb(57, 59, 57);
            label15.BackColor = Color.FromArgb(57, 59, 57);
            panelGraphics.Show();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSens.Hide();
            panelRes.Hide();

            if (!File.Exists(SteamSettingsPath) && !File.Exists(SteamEnginePath) && !File.Exists(EGSSettingsPath) && !File.Exists(EGSEnginePath) && !File.Exists(MSSettingsPath) && !File.Exists(MSEnginePath))
            {
                MessageBox.Show("Dead by Daylight confiuration files do not exist! Please, launch the game.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


            if (File.Exists(SteamSettingsPath) && File.Exists(SteamEnginePath))
            {
                EnginePath = SteamEnginePath;
                SettingsPath = SteamSettingsPath;
                cbVersion.Items.Add("Steam");
                cbVersion.SelectedIndex = 0;
            }
            if (File.Exists(EGSSettingsPath) && File.Exists(EGSEnginePath))
            {
                EnginePath = EGSEnginePath;
                SettingsPath = EGSSettingsPath;
                cbVersion.Items.Add("Epic Games Store");
                cbVersion.SelectedIndex = 0;
            }
            if (File.Exists(MSSettingsPath) && File.Exists(MSEnginePath))
            {
                EnginePath = MSEnginePath;
                SettingsPath = MSSettingsPath;
                cbVersion.Items.Add("Microsoft Store");
                cbVersion.SelectedIndex = 0;
            }


            try
            {
                WebClient wc = new WebClient();
                string textFromFile = wc.DownloadString("link to update");
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (!textFromFile.Contains(version))
                {
                    DialogResult result = MessageBox.Show("A new version of the program has been found.\n\nDo you want to download it?", "Check for updates",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        var uri = "http://dbdconfigeditor.epizy.com/";
                        var psi = new System.Diagnostics.ProcessStartInfo();
                        psi.UseShellExecute = true;
                        psi.FileName = uri;
                        System.Diagnostics.Process.Start(psi);
                        Application.Exit();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Checking newer program versions failed!", "Check for updates",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblHideFocus.Focus();

            try
            {
                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);

                ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");
                ReadEngine = Regex.Replace(ReadEngine, Environment.NewLine + Environment.NewLine, "\r");
                File.WriteAllText(EnginePath, ReadEngine);
                File.WriteAllText(SettingsPath, ReadConfig);

                if (!Directory.Exists(ApplicationData))
                {
                    Directory.CreateDirectory(ApplicationData);
                }

                if (!File.Exists(ApplicationData + "UsSet.ini"))
                {
                    File.Copy(SettingsPath, ApplicationData + "UsSet.ini");
                }

                if (!File.Exists(ApplicationData + "UsEng.ini"))
                {
                    File.Copy(EnginePath, ApplicationData + "UsEng.ini");
                }

                UsersEngineContent = File.ReadAllText(ApplicationData + "UsEng.ini");
                UsersSettingsContent = File.ReadAllText(ApplicationData + "UsSet.ini");

                if (ReadConfig == null || ReadConfig.Length == 0)
                {
                    File.WriteAllText(SettingsPath, UsersSettingsContent);
                    if (ReadEngine == null || ReadEngine.Length == 0)
                    {
                        File.WriteAllText(EnginePath, UsersEngineContent);
                    }
                    Application.Restart();
                }

                if (ReadEngine == null || ReadEngine.Length == 0)
                {
                    File.WriteAllText(EnginePath, UsersEngineContent);
                    if (ReadConfig == null || ReadConfig.Length == 0)
                    {
                        File.WriteAllText(SettingsPath, UsersSettingsContent);
                    }
                    Application.Restart();
                }

                if (ReadConfig != null || ReadConfig.Length != 0)
                {
                    CheckValues(SettingsPath, EnginePath);
                }
            }
            catch (Exception)
            {

            }
        }

        private void tbRes_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string res = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int resolution = tbRes.Value;
            lblRes.Text = resolution.ToString() + "%";

            int numLines = File.ReadLines(SettingsPath).Count();
            string[] lines = File.ReadAllLines(SettingsPath);
            

            for (int i = 0; i < numLines; i++)
            {

                if (lines[i].Contains("ResolutionQuality"))
                {
                    res = res.Replace(lines[i], "sg.ResolutionQuality=" + resolution + ".000000");
                    File.WriteAllText(SettingsPath, res);
                }
            }
        }

        private void btnUsersSettings_Click(object sender, EventArgs e)
        {
            try
            {
                lblHideFocus.Focus();
                File.WriteAllText(SettingsPath, UsersSettingsContent);
                File.WriteAllText(EnginePath, UsersEngineContent);

                CheckValues(SettingsPath, EnginePath);
            }
            catch (Exception)
            {

            }
        }

        private void btnUsersSettings_MouseLeave(object sender, EventArgs e)
        {
            btnUsersSettings.BackColor = Color.Transparent;
            tip.Hide(btnUsersSettings);
        }

        private void btnUsersSettings_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Reset the settings to those saved in the backup.", btnUsersSettings);
        }

        private void btnVwLow_Click(object sender, EventArgs e)
        {
            btnVwMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnVwMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnVwHigh.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnVwUltra.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnVwEpic.ForeColor = Color.Black;
            btnVwAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnVwAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwLow.BackColor = Color.Crimson;
            btnVwLow.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", graphic);
        }

        private void btnVwMedium_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.FromArgb(224, 224, 224);
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnVwHigh.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnVwUltra.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnVwEpic.ForeColor = Color.Black;
            btnVwAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnVwAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwMedium.BackColor = Color.Crimson;
            btnVwMedium.ForeColor = Color.White;

            lblHideFocus.Focus();

            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", graphic);
        }

        private void btnVwHigh_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.FromArgb(224, 224, 224);
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnVwMedium.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnVwUltra.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnVwEpic.ForeColor = Color.Black;
            btnVwAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnVwAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwHigh.BackColor = Color.Crimson;
            btnVwHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", graphic);
        }

        private void btnVwUltra_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.FromArgb(224, 224, 224);
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnVwMedium.ForeColor = Color.Black;
            btnVwHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnVwHigh.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnVwEpic.ForeColor = Color.Black;
            btnVwAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnVwAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwUltra.BackColor = Color.Crimson;
            btnVwUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", graphic);
        }

        private void btnVwEpic_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.FromArgb(224, 224, 224);
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnVwMedium.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnVwUltra.ForeColor = Color.Black;
            btnVwHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnVwHigh.ForeColor = Color.Black;
            btnVwAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnVwAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwEpic.BackColor = Color.Crimson;
            btnVwEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", graphic);
        }

        private void btnAaLow_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAaMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAaHigh.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAaEpic.ForeColor = Color.Black;
            btnAaAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaLow.BackColor = Color.Crimson;
            btnAaLow.ForeColor = Color.White;
            btnAaDisable.BackColor = Color.FromArgb(224, 224, 224);
            btnAaDisable.ForeColor = Color.Black;

            var Delete = new DeleteAntiAliasing(EnginePath);

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", graphic);
        }

        private void btnAaMedium_Click(object sender, EventArgs e)
        {
            btnAaLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAaLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAaHigh.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAaEpic.ForeColor = Color.Black;
            btnAaAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaMedium.BackColor = Color.Crimson;
            btnAaMedium.ForeColor = Color.White;
            btnAaDisable.BackColor = Color.FromArgb(224, 224, 224);
            btnAaDisable.ForeColor = Color.Black;

            var Delete = new DeleteAntiAliasing(EnginePath);

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", graphic);
        }

        private void btnAaHigh_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAaMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAaLow.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAaEpic.ForeColor = Color.Black;
            btnAaAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaHigh.BackColor = Color.Crimson;
            btnAaHigh.ForeColor = Color.White;
            btnAaDisable.BackColor = Color.FromArgb(224, 224, 224);
            btnAaDisable.ForeColor = Color.Black;

            var Delete = new DeleteAntiAliasing(EnginePath);

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", graphic);
        }

        private void btnAaUltra_Click(object sender, EventArgs e)
        {
            btnAaHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAaHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAaMedium.ForeColor = Color.Black;
            btnAaLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAaLow.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAaEpic.ForeColor = Color.Black;
            btnAaAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaUltra.BackColor = Color.Crimson;
            btnAaUltra.ForeColor = Color.White;
            btnAaDisable.BackColor = Color.FromArgb(224, 224, 224);
            btnAaDisable.ForeColor = Color.Black;

            var Delete = new DeleteAntiAliasing(EnginePath);

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", graphic);
        }

        private void btnAaEpic_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAaMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAaHigh.ForeColor = Color.Black;
            btnAaLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAaLow.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAaUltra.ForeColor = Color.Black;
            btnAaAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaEpic.BackColor = Color.Crimson;
            btnAaEpic.ForeColor = Color.White;
            btnAaDisable.BackColor = Color.FromArgb(224, 224, 224);
            btnAaDisable.ForeColor = Color.Black;

            var Delete = new DeleteAntiAliasing(EnginePath);

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", graphic);
        }

        private void btnShadLow_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShadHigh.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShadMedium.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShadUltra.ForeColor = Color.Black;
            btnShadAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShadAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadLow.BackColor = Color.Crimson;
            btnShadLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", graphic);
        }

        private void btnShadMedium_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShadHigh.ForeColor = Color.Black;
            btnShadLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShadLow.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShadUltra.ForeColor = Color.Black;
            btnShadAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShadAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadMedium.BackColor = Color.Crimson;
            btnShadMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", graphic);
        }

        private void btnShadHigh_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShadLow.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShadMedium.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShadUltra.ForeColor = Color.Black;
            btnShadAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShadAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadHigh.BackColor = Color.Crimson;
            btnShadHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", graphic);
        }

        private void btnShadUltra_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShadHigh.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShadMedium.ForeColor = Color.Black;
            btnShadLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShadLow.ForeColor = Color.Black;
            btnShadAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShadAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadUltra.BackColor = Color.Crimson;
            btnShadUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", graphic);
        }

        private void btnShadEpic_Click(object sender, EventArgs e)
        {
            btnShadLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShadLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShadHigh.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShadMedium.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShadUltra.ForeColor = Color.Black;
            btnShadAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShadAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadEpic.BackColor = Color.Crimson;
            btnShadEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", graphic);
        }

        private void btnPpLow_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnPpHigh.ForeColor = Color.Black;
            btnPpMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnPpMedium.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnPpUltra.ForeColor = Color.Black;
            btnPpAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnPpAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpLow.BackColor = Color.Crimson;
            btnPpLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", graphic);
        }

        private void btnPpMedium_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnPpHigh.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.FromArgb(224, 224, 224);
            btnPpLow.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnPpUltra.ForeColor = Color.Black;
            btnPpAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnPpAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpMedium.BackColor = Color.Crimson;
            btnPpMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", graphic);
        }

        private void btnPpHigh_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnPpMedium.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.FromArgb(224, 224, 224);
            btnPpLow.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnPpUltra.ForeColor = Color.Black;
            btnPpAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnPpAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpHigh.BackColor = Color.Crimson;
            btnPpHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", graphic);
        }

        private void btnPpUltra_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnPpHigh.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.FromArgb(224, 224, 224);
            btnPpLow.ForeColor = Color.Black;
            btnPpMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnPpMedium.ForeColor = Color.Black;
            btnPpAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnPpAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpUltra.BackColor = Color.Crimson;
            btnPpUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", graphic);
        }

        private void btnPpEpic_Click(object sender, EventArgs e)
        {
            btnPpMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnPpMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnPpHigh.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.FromArgb(224, 224, 224);
            btnPpLow.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnPpUltra.ForeColor = Color.Black;
            btnPpAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnPpAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpEpic.BackColor = Color.Crimson;
            btnPpEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", graphic);
        }

        private void btnTxtLow_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtUltra.ForeColor = Color.Black;
            btnTxtAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtLow.BackColor = Color.Crimson;
            btnTxtLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.TextureQuality=", graphic);
        }

        private void btnTxtMedium_Click(object sender, EventArgs e)
        {
            btnTxtLow.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtUltra.ForeColor = Color.Black;
            btnTxtAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtMedium.BackColor = Color.Crimson;
            btnTxtMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.TextureQuality=", graphic);
        }

        private void btnTxtHigh_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtLow.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtLow.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtUltra.ForeColor = Color.Black;
            btnTxtAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtHigh.BackColor = Color.Crimson;
            btnTxtHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.TextureQuality=", graphic);
        }

        private void btnTxtUltra_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtLow.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtLow.ForeColor = Color.Black;
            btnTxtAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtUltra.BackColor = Color.Crimson;
            btnTxtUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.TextureQuality=", graphic);
        }

        private void btnTxtEpic_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtLow.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtLow.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtUltra.ForeColor = Color.Black;
            btnTxtAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtEpic.BackColor = Color.Crimson;
            btnTxtEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.TextureQuality=", graphic);
        }

        private void btnEffLow_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnEffHigh.ForeColor = Color.Black;
            btnEffMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnEffMedium.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnEffUltra.ForeColor = Color.Black;
            btnEffAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnEffAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffLow.BackColor = Color.Crimson;
            btnEffLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", graphic);
        }

        private void btnEffMedium_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnEffHigh.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.FromArgb(224, 224, 224);
            btnEffLow.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnEffUltra.ForeColor = Color.Black;
            btnEffAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnEffAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffMedium.BackColor = Color.Crimson;
            btnEffMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", graphic);
        }

        private void btnEffHigh_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnEffMedium.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.FromArgb(224, 224, 224);
            btnEffLow.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnEffUltra.ForeColor = Color.Black;
            btnEffAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnEffAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffHigh.BackColor = Color.Crimson;
            btnEffHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", graphic);
        }

        private void btnEffUltra_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnEffMedium.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.FromArgb(224, 224, 224);
            btnEffLow.ForeColor = Color.Black;
            btnEffHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnEffHigh.ForeColor = Color.Black;
            btnEffAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnEffAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffUltra.BackColor = Color.Crimson;
            btnEffUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", graphic);
        }

        private void btnEffEpic_Click(object sender, EventArgs e)
        {
            btnEffUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnEffUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnEffMedium.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.FromArgb(224, 224, 224);
            btnEffLow.ForeColor = Color.Black;
            btnEffHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnEffHigh.ForeColor = Color.Black;
            btnEffAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnEffAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffEpic.BackColor = Color.Crimson;
            btnEffEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", graphic);
        }

        private void btnFolLow_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnFolMedium.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnFolEpic.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnFolHigh.ForeColor = Color.Black;
            btnFolAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnFolAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolLow.BackColor = Color.Crimson;
            btnFolLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", graphic);
        }

        private void btnFolMedium_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.FromArgb(224, 224, 224);
            btnFolLow.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnFolEpic.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnFolHigh.ForeColor = Color.Black;
            btnFolAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnFolAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolMedium.BackColor = Color.Crimson;
            btnFolMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", graphic);
        }

        private void btnFolHigh_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.FromArgb(224, 224, 224);
            btnFolLow.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnFolEpic.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnFolMedium.ForeColor = Color.Black;
            btnFolAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnFolAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolHigh.BackColor = Color.Crimson;
            btnFolHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", graphic);
        }

        private void btnFolUltra_Click(object sender, EventArgs e)
        {
            btnFolHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnFolHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.FromArgb(224, 224, 224);
            btnFolLow.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnFolEpic.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnFolMedium.ForeColor = Color.Black;
            btnFolAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnFolAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolUltra.BackColor = Color.Crimson;
            btnFolUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", graphic);
        }

        private void btnFolEpic_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.FromArgb(224, 224, 224);
            btnFolLow.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnFolHigh.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnFolMedium.ForeColor = Color.Black;
            btnFolAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnFolAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolEpic.BackColor = Color.Crimson;
            btnFolEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", graphic);
        }

        private void btnShLow_Click(object sender, EventArgs e)
        {
            btnShHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShEpic.ForeColor = Color.Black;
            btnShMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShMedium.ForeColor = Color.Black;
            btnShAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShLow.BackColor = Color.Crimson;
            btnShLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", graphic);
        }

        private void btnShMedium_Click(object sender, EventArgs e)
        {
            btnShHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShEpic.ForeColor = Color.Black;
            btnShLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShLow.ForeColor = Color.Black;
            btnShAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShMedium.BackColor = Color.Crimson;
            btnShMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", graphic);
        }

        private void btnShHigh_Click(object sender, EventArgs e)
        {
            btnShMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShEpic.ForeColor = Color.Black;
            btnShLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShLow.ForeColor = Color.Black;
            btnShAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShHigh.BackColor = Color.Crimson;
            btnShHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", graphic);
        }

        private void btnShUltra_Click(object sender, EventArgs e)
        {
            btnShMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShHigh.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShEpic.ForeColor = Color.Black;
            btnShLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShLow.ForeColor = Color.Black;
            btnShAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShUltra.BackColor = Color.Crimson;
            btnShUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", graphic);
        }

        private void btnShEpic_Click(object sender, EventArgs e)
        {
            btnShMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShHigh.ForeColor = Color.Black;
            btnShUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShUltra.ForeColor = Color.Black;
            btnShLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShLow.ForeColor = Color.Black;
            btnShAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShEpic.BackColor = Color.Crimson;
            btnShEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", graphic);
        }

        private void btnAnimLow_Click(object sender, EventArgs e)
        {
            btnAnimMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimEpic.ForeColor = Color.Black;
            btnAnimAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimLow.BackColor = Color.Crimson;
            btnAnimLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", graphic);
        }

        private void btnAnimMedium_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimEpic.ForeColor = Color.Black;
            btnAnimAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimMedium.BackColor = Color.Crimson;
            btnAnimMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", graphic);
        }

        private void btnAnimHigh_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimEpic.ForeColor = Color.Black;
            btnAnimAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimHigh.BackColor = Color.Crimson;
            btnAnimHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", graphic);
        }

        private void btnAnimUltra_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimEpic.ForeColor = Color.Black;
            btnAnimAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimUltra.BackColor = Color.Crimson;
            btnAnimUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", graphic);
        }

        private void btnAnimEpic_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimEpic.BackColor = Color.Crimson;
            btnAnimEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", graphic);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int aud = trackBar1.Value;
            lblMainVolume.Text = aud.ToString() + "%";

            var ChangeSettings = new ChangeSettings(SettingsPath, "MainVolume=", aud);
        }

        private void btnAudioLow_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioUltra.ForeColor = Color.Black;
            btnAudioAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioLow.BackColor = Color.Crimson;
            btnAudioLow.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings1 = new ChangeSettings(SettingsPath, "AudioQualityLevel=", graphic);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "LastConfirmedAudioQualityLevel=", graphic);
        }

        private void btnAudioMedium_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioLow.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioUltra.ForeColor = Color.Black;
            btnAudioAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioMedium.BackColor = Color.Crimson;
            btnAudioMedium.ForeColor = Color.White;
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 1;

            var ChangeSettings1 = new ChangeSettings(SettingsPath, "AudioQualityLevel=", graphic);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "LastConfirmedAudioQualityLevel=", graphic);
        }

        private void btnAudioHigh_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioLow.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioUltra.ForeColor = Color.Black;
            btnAudioAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioHigh.BackColor = Color.Crimson;
            btnAudioHigh.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 2;

            var ChangeSettings1 = new ChangeSettings(SettingsPath, "AudioQualityLevel=", graphic);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "LastConfirmedAudioQualityLevel=", graphic);
        }

        private void btnAudioUltra_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioLow.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioUltra.BackColor = Color.Crimson;
            btnAudioUltra.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 3;

            var ChangeSettings1 = new ChangeSettings(SettingsPath, "AudioQualityLevel=", graphic);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "LastConfirmedAudioQualityLevel=", graphic);
        }

        private void btnAudioEpic_Click(object sender, EventArgs e)
        {
            btnAudioUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioLow.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioEpic.BackColor = Color.Crimson;
            btnAudioEpic.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 4;

            var ChangeSettings1 = new ChangeSettings(SettingsPath, "AudioQualityLevel=", graphic);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "LastConfirmedAudioQualityLevel=", graphic);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int aud = trackBar2.Value;
            lblMenu.Text = aud.ToString() + "%";

            var ChangeSettings1 = new ChangeSettings(SettingsPath, "MenuMusicVolume=", aud);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            bool check;
            if (checkBox1.Checked)
            {
                lblHideFocus.Focus();
                check = true;

                var ChangeSettings1 = new SettingsTrueOrFalse(SettingsPath, "UseHeadphones=", check);
            }
            else
            {
                lblHideFocus.Focus();
                check = false;
                var ChangeSettings1 = new SettingsTrueOrFalse(SettingsPath, "UseHeadphones=", check);
            }
        }

        private void btnSetFPS_Click(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            string read = File.ReadAllText(EnginePath);
            checkBox2.Checked = false;

            var ResetFPS = new ResetFPS(EnginePath);
            if (numFPS.Value == 120)
            {
                var SetFPS = new SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
            }
            if (numFPS.Value > 120)
            {
                MessageBox.Show($"The maximum frame rate in Dead by Daylight is 120. The game will be displayed at 120 frames per second anyway. But the program will set you a limit of {(int)numFPS.Value} FPS anyway ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var SetFPS = new SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
            }
            if (numFPS.Value < 120)
            {
                var SetFPS = new SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
            }

            if (numFPS.Value <= 5)
            {
                var SetFPS = new SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                var EngineSetFPS = new EngineSetFPS(EnginePath, (int)numFPS.Value);
            }

            if (numFPS.Value >= 63 && numFPS.Value >= 120)
            {
                var EngineSetFPS = new EngineSetFPS(EnginePath, (int)numFPS.Value);
            }
        }

        private void btnResetFPS_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            lblHideFocus.Focus();
            numFPS.Value = 62;

            var ResetFPS = new ResetFPS(EnginePath);
            var SetFPS = new SetFPS(SettingsPath, "FrameRateLimit=", 60);
        }

        private void btnResetFPS_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Reset FPS cap.", btnResetFPS);
        }

        private void btnResetFPS_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResetFPS);
        }

        private void btnSetFPS_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Set your own FPS cap.", btnSetFPS);
        }

        private void btnSetFPS_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnSetFPS);
        }

        private void btnResSet_Click(object sender, EventArgs e)
        {
            var SetResolution = new SetResolution(SettingsPath, (int)numWidth.Value, (int)numHeight.Value);
            lblHideFocus.Focus();
        }

        private void btnResReset_Click(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            numWidth.Value = screenWidth;
            numHeight.Value = screenHeight;
            lblHideFocus.Focus();

            var ResetResolution = new ResetResolution(SettingsPath);
        }

        private void btnPresetLow_Click(object sender, EventArgs e)
        {
            lblHideFocus.Focus();

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ResolutionQuality=", 1);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", 0);
            var ChangeSettings3 = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", 0);
            var Delete = new DeleteAntiAliasing(EnginePath);
            var Disable = new DisableAntiAliasing(EnginePath);
            var ChangeSettings4 = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", 0);
            var ChangeSettings5 = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", 0);
            var ChangeSettings6 = new ChangeSettings(SettingsPath, "sg.TextureQuality=", 0);
            var ChangeSettings7 = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", 0);
            var ChangeSettings8 = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", 0);
            var ChangeSettings9 = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", 0);
            var ChangeSettings10 = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", 0);
            var ChangeSettings11 = new ChangeSettings(SettingsPath, "ScreenResolution=", 1);

            CheckValues(SettingsPath, EnginePath);
        }

        private void btnPresetEpic_Click(object sender, EventArgs e)
        {
            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ResolutionQuality=", 200);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", 5);
            var ChangeSettings3 = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", 5);
            var ChangeSettings4 = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", 5);
            var ChangeSettings5 = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", 5);
            var ChangeSettings6 = new ChangeSettings(SettingsPath, "sg.TextureQuality=", 5);
            var ChangeSettings7 = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", 5);
            var ChangeSettings8 = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", 5);
            var ChangeSettings9 = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", 5);
            var ChangeSettings10 = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", 5);
            var ChangeSettings11 = new ChangeSettings(SettingsPath, "ScreenResolution=", 200);

            CheckValues(SettingsPath, EnginePath);
        }

        private void btnResSet_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Set your own game resolution.", btnResSet);
        }

        private void btnResSet_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResSet);
        }

        private void btnResReset_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Reset to system resolution.", btnResReset);
        }

        private void btnResReset_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResReset);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            bool check;
            if (checkBox2.Checked)
            {
                lblHideFocus.Focus();
                string view = File.ReadAllText(SettingsPath);
                string config = File.ReadAllText(SettingsPath);
                check = true;

                int numLines = File.ReadLines(SettingsPath).Count();
                string[] lines = File.ReadAllLines(SettingsPath);
                

                for (int i = 0; i < numLines; i++)
                {

                    if (lines[i].Contains("bUseVSync"))
                    {
                        //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                        view = view.Replace(lines[i], "bUseVSync=" + check);
                        File.WriteAllText(SettingsPath, view);
                    }
                }
            }
            else
            {
                lblHideFocus.Focus();
                string view = File.ReadAllText(SettingsPath);
                string config = File.ReadAllText(SettingsPath);
                check = false;

                int numLines = File.ReadLines(SettingsPath).Count();
                string[] lines = File.ReadAllLines(SettingsPath);
                

                for (int i = 0; i < numLines; i++)
                {

                    if (lines[i].Contains("bUseVSync"))
                    {
                        //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                        view = view.Replace(lines[i], "bUseVSync=" + check);
                        File.WriteAllText(SettingsPath, view);
                    }
                }
            }
        }

        private void KillerMouse_Scroll(object sender, EventArgs e)
        {
            int sensitivity = KillerMouse.Value;
            lblKillerMouse.Text = sensitivity.ToString() + "%";

            var ChangeSettings = new ChangeSettings(SettingsPath, "KillerMouseSensitivity=", sensitivity);
        }

        private void KillerController_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            int sensitivity = KillerController.Value;
            lblKillerController.Text = sensitivity.ToString() + "%";
            var ChangeSettings = new ChangeSettings(SettingsPath, "KillerControllerSensitivity=", sensitivity);
        }

        private void SurvMouse_Scroll(object sender, EventArgs e)
        {
            int sensitivity = SurvMouse.Value;
            lblSurvMouse.Text = sensitivity.ToString() + "%";

            var ChangeSettings = new ChangeSettings(SettingsPath, "SurvivorMouseSensitivity=", sensitivity);
        }

        private void SurvController_Scroll(object sender, EventArgs e)
        {
            int sensitivity = SurvController.Value;
            lblSurvController.Text = sensitivity.ToString() + "%";

            var ChangeSettings = new ChangeSettings(SettingsPath, "SurvivorControllerSensitivity=", sensitivity);
        }

        private void tbScRes_Scroll(object sender, EventArgs e)
        {
            int resolution = tbScRes.Value;
            lblScRes.Text = resolution.ToString() + "%";

            var ChangeSettings = new ChangeSettings(SettingsPath, "ScreenResolution=", resolution);
        }

        private void btnDelete_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Delete the backup configuration files and create a new ones.", btnDelete);
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.Transparent;
            tip.Hide(btnDelete);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the configuration file and create a new one?", "Delete backup config",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.Delete(ApplicationData + "UsEng.ini");
                File.Delete(ApplicationData + "UsSet.ini");
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Autosave");
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs");
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data");
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer");
                Application.Restart();
            }
        }

        private void btnDiscord_Click(object sender, EventArgs e)
        {
            try
            {
                var uri = "https://discord.com/invite/EY9uaqTS7Z";
                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = uri;
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem opening the Discord link!", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDiscord_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Official Discord Server", btnDiscord);
        }

        private void btnDiscord_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnDiscord);
            btnDiscord.BackColor = Color.Transparent;
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.Crimson;
        }

        private void btnUsersSettings_MouseEnter(object sender, EventArgs e)
        {
            btnUsersSettings.BackColor = Color.Crimson;
        }

        private void btnDiscord_MouseEnter(object sender, EventArgs e)
        {
            btnDiscord.BackColor = Color.Crimson;
        }

        private void btnPresetMedium_Click(object sender, EventArgs e)
        {
            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ResolutionQuality=", 100);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", 3);
            var ChangeSettings3 = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", 0);
            var ChangeSettings4 = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", 0);
            var ChangeSettings5 = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", 3);
            var ChangeSettings6 = new ChangeSettings(SettingsPath, "sg.TextureQuality=", 2);
            var ChangeSettings7 = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", 2);
            var ChangeSettings8 = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", 0);
            var ChangeSettings9 = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", 0);
            var ChangeSettings10 = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", 2);
            var ChangeSettings11 = new ChangeSettings(SettingsPath, "ScreenResolution=", 100);

            CheckValues(SettingsPath, EnginePath);
        }

        private void btnVwAwesome_Click(object sender, EventArgs e)
        {
            btnVwMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnVwMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnVwHigh.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnVwUltra.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnVwEpic.ForeColor = Color.Black;
            btnVwLow.BackColor = Color.FromArgb(224, 224, 224);
            btnVwLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwAwesome.BackColor = Color.Crimson;
            btnVwAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ViewDistanceQuality=", graphic);
        }

        private void btnAaAwesome_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAaMedium.ForeColor = Color.Black;
            btnAaDisable.BackColor = Color.FromArgb(224, 224, 224);
            btnAaDisable.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAaHigh.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAaEpic.ForeColor = Color.Black;
            btnAaLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAaLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaAwesome.BackColor = Color.Crimson;
            btnAaAwesome.ForeColor = Color.White;
            var Delete = new DeleteAntiAliasing(EnginePath);

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", graphic);
        }

        private void btnShadAwesome_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShadHigh.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShadMedium.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShadUltra.ForeColor = Color.Black;
            btnShadLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShadLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadAwesome.BackColor = Color.Crimson;
            btnShadAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadowQuality=", graphic);
        }

        private void btnPpAwesome_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnPpHigh.ForeColor = Color.Black;
            btnPpMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnPpMedium.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnPpUltra.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.FromArgb(224, 224, 224);
            btnPpLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpAwesome.BackColor = Color.Crimson;
            btnPpAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.PostProcessQuality=", graphic);
        }

        private void btnTxtAwesome_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtUltra.ForeColor = Color.Black;
            btnTxtLow.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtAwesome.BackColor = Color.Crimson;
            btnTxtAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.TextureQuality=", graphic);
        }

        private void btnEffAwesome_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnEffHigh.ForeColor = Color.Black;
            btnEffMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnEffMedium.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnEffUltra.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.FromArgb(224, 224, 224);
            btnEffLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffAwesome.BackColor = Color.Crimson;
            btnEffAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.EffectsQuality=", graphic);
        }

        private void btnFolAwesome_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnFolMedium.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnFolEpic.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnFolHigh.ForeColor = Color.Black;
            btnFolLow.BackColor = Color.FromArgb(224, 224, 224);
            btnFolLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolAwesome.BackColor = Color.Crimson;
            btnFolAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.FoliageQuality=", graphic);
        }

        private void btnShAwesome_Click(object sender, EventArgs e)
        {
            btnShHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShEpic.ForeColor = Color.Black;
            btnShMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShMedium.ForeColor = Color.Black;
            btnShLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShAwesome.BackColor = Color.Crimson;
            btnShAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.ShadingQuality=", graphic);
        }

        private void btnAnimAwesome_Click(object sender, EventArgs e)
        {
            btnAnimMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimEpic.ForeColor = Color.Black;
            btnAnimLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimAwesome.BackColor = Color.Crimson;
            btnAnimAwesome.ForeColor = Color.White;

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AnimationQuality=", graphic);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1145, 1022);
            label2.BackColor = Color.Crimson;
            label2.Location = new Point(label2.Location.X, 2);
            label2.Size = new Size(label2.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 2);
            label12.Size = new Size(label12.Size.Width, 40);

            label14.Location = new Point(label14.Location.X, 2);
            label14.Size = new Size(label14.Size.Width, 40);

            label16.Location = new Point(label16.Location.X, 2);
            label16.Size = new Size(label16.Size.Width, 40);

            label19.Location = new Point(label19.Location.X, 2);
            label19.Size = new Size(label19.Size.Width, 40);


            label15.Location = new Point(label15.Location.X, 2);
            label15.Size = new Size(label15.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 4);
            label14.Location = new Point(label14.Location.X, 4);
            label16.Location = new Point(label16.Location.X, 4);
            label19.Location = new Point(label19.Location.X, 4);
            label15.Location = new Point(label15.Location.X, 4);

            label12.BackColor = Color.FromArgb(57, 59, 57);
            label14.BackColor = Color.FromArgb(57, 59, 57);
            label16.BackColor = Color.FromArgb(57, 59, 57);
            label19.BackColor = Color.FromArgb(57, 59, 57);
            label15.BackColor = Color.FromArgb(57, 59, 57);
            panelGraphics.Show();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSens.Hide();
            panelRes.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1145, 566);
            label12.BackColor = Color.Crimson;
            label12.Location = new Point(label12.Location.X, 2);
            label12.Size = new Size(label12.Size.Width, 40);

            label2.Location = new Point(label2.Location.X, 2);
            label2.Size = new Size(label2.Size.Width, 40);

            label14.Location = new Point(label14.Location.X, 2);
            label14.Size = new Size(label14.Size.Width, 40);

            label16.Location = new Point(label16.Location.X, 2);
            label16.Size = new Size(label16.Size.Width, 40);

            label19.Location = new Point(label19.Location.X, 2);
            label19.Size = new Size(label19.Size.Width, 40);

            label15.Location = new Point(label15.Location.X, 2);
            label15.Size = new Size(label15.Size.Width, 40);

            label2.Location = new Point(label2.Location.X, 4);
            label14.Location = new Point(label14.Location.X, 4);
            label16.Location = new Point(label16.Location.X, 4);
            label19.Location = new Point(label19.Location.X, 4);
            label15.Location = new Point(label15.Location.X, 4);

            label2.BackColor = Color.FromArgb(57, 59, 57);
            label14.BackColor = Color.FromArgb(57, 59, 57);
            label16.BackColor = Color.FromArgb(57, 59, 57);
            label19.BackColor = Color.FromArgb(57, 59, 57);
            label15.BackColor = Color.FromArgb(57, 59, 57);
            panelGraphics.Hide();
            panelAudio.Show();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSens.Hide();
            panelRes.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1145, 408);
            label14.BackColor = Color.Crimson;
            label14.Location = new Point(label14.Location.X, 2);
            label14.Size = new Size(label14.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 2);
            label12.Size = new Size(label12.Size.Width, 40);

            label2.Location = new Point(label2.Location.X, 2);
            label2.Size = new Size(label2.Size.Width, 40);

            label16.Location = new Point(label16.Location.X, 2);
            label16.Size = new Size(label16.Size.Width, 40);

            label19.Location = new Point(label19.Location.X, 2);
            label19.Size = new Size(label19.Size.Width, 40);

            label15.Location = new Point(label15.Location.X, 2);
            label15.Size = new Size(label15.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 4);
            label2.Location = new Point(label2.Location.X, 4);
            label16.Location = new Point(label16.Location.X, 4);
            label19.Location = new Point(label19.Location.X, 4);
            label15.Location = new Point(label15.Location.X, 4);

            label12.BackColor = Color.FromArgb(57, 59, 57);
            label2.BackColor = Color.FromArgb(57, 59, 57);
            label16.BackColor = Color.FromArgb(57, 59, 57);
            label19.BackColor = Color.FromArgb(57, 59, 57);
            label15.BackColor = Color.FromArgb(57, 59, 57);
            panelGraphics.Hide();
            panelAudio.Hide();
            panelFPS.Show();
            panelPresets.Hide();
            panelSens.Hide();
            panelRes.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1145, 448);
            label16.BackColor = Color.Crimson;
            label16.Location = new Point(label16.Location.X, 2);
            label16.Size = new Size(label16.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 2);
            label12.Size = new Size(label12.Size.Width, 40);

            label14.Location = new Point(label14.Location.X, 2);
            label14.Size = new Size(label14.Size.Width, 40);

            label2.Location = new Point(label2.Location.X, 2);
            label2.Size = new Size(label2.Size.Width, 40);

            label19.Location = new Point(label19.Location.X, 2);
            label19.Size = new Size(label19.Size.Width, 40);

            label15.Location = new Point(label15.Location.X, 2);
            label15.Size = new Size(label15.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 4);
            label14.Location = new Point(label14.Location.X, 4);
            label2.Location = new Point(label2.Location.X, 4);
            label19.Location = new Point(label19.Location.X, 4);
            label15.Location = new Point(label15.Location.X, 4);

            label12.BackColor = Color.FromArgb(57, 59, 57);
            label14.BackColor = Color.FromArgb(57, 59, 57);
            label2.BackColor = Color.FromArgb(57, 59, 57);
            label19.BackColor = Color.FromArgb(57, 59, 57);
            label15.BackColor = Color.FromArgb(57, 59, 57);
            panelGraphics.Hide();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Show();
            panelSens.Hide();
            panelRes.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1145, 530);
            label19.BackColor = Color.Crimson;
            label19.Location = new Point(label19.Location.X, 2);
            label19.Size = new Size(label19.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 2);
            label12.Size = new Size(label12.Size.Width, 40);

            label14.Location = new Point(label14.Location.X, 2);
            label14.Size = new Size(label14.Size.Width, 40);

            label16.Location = new Point(label16.Location.X, 2);
            label16.Size = new Size(label16.Size.Width, 40);

            label2.Location = new Point(label2.Location.X, 2);
            label2.Size = new Size(label2.Size.Width, 40);

            label15.Location = new Point(label15.Location.X, 2);
            label15.Size = new Size(label15.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 4);
            label14.Location = new Point(label14.Location.X, 4);
            label16.Location = new Point(label16.Location.X, 4);
            label2.Location = new Point(label2.Location.X, 4);
            label15.Location = new Point(label15.Location.X, 4);

            label12.BackColor = Color.FromArgb(57, 59, 57);
            label14.BackColor = Color.FromArgb(57, 59, 57);
            label16.BackColor = Color.FromArgb(57, 59, 57);
            label2.BackColor = Color.FromArgb(57, 59, 57);
            label15.BackColor = Color.FromArgb(57, 59, 57);
            panelGraphics.Hide();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSens.Hide();
            panelRes.Show();
        }


        private void label15_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1145, 665);
            label15.BackColor = Color.Crimson;
            label15.Location = new Point(label15.Location.X, 2);
            label15.Size = new Size(label15.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 2);
            label12.Size = new Size(label12.Size.Width, 40);

            label14.Location = new Point(label14.Location.X, 2);
            label14.Size = new Size(label14.Size.Width, 40);

            label16.Location = new Point(label16.Location.X, 2);
            label16.Size = new Size(label16.Size.Width, 40);

            label19.Location = new Point(label19.Location.X, 2);
            label19.Size = new Size(label19.Size.Width, 40);


            label2.Location = new Point(label2.Location.X, 2);
            label2.Size = new Size(label2.Size.Width, 40);

            label12.Location = new Point(label12.Location.X, 4);
            label14.Location = new Point(label14.Location.X, 4);
            label16.Location = new Point(label16.Location.X, 4);
            label19.Location = new Point(label19.Location.X, 4);
            label2.Location = new Point(label2.Location.X, 4);

            label12.BackColor = Color.FromArgb(57, 59, 57);
            label14.BackColor = Color.FromArgb(57, 59, 57);
            label16.BackColor = Color.FromArgb(57, 59, 57);
            label19.BackColor = Color.FromArgb(57, 59, 57);
            label2.BackColor = Color.FromArgb(57, 59, 57);
            panelGraphics.Hide();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSens.Show();
            panelRes.Hide();
        }

        private void btnAudioAwesome_Click(object sender, EventArgs e)
        {
            btnAudioUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioLow.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioAwesome.BackColor = Color.Crimson;
            btnAudioAwesome.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            int graphic = 5;

            var ChangeSettings1 = new ChangeSettings(SettingsPath, "AudioQualityLevel=", graphic);
            var ChangeSettings2 = new ChangeSettings(SettingsPath, "LastConfirmedAudioQualityLevel=", graphic);
        }

        private void btnAaDisable_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAaMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAaHigh.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAaEpic.ForeColor = Color.Black;
            btnAaAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaAwesome.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAaLow.ForeColor = Color.Black;
            btnAaDisable.ForeColor = Color.White;
            btnAaDisable.BackColor = Color.Crimson;

            var Delete = new DeleteAntiAliasing(EnginePath);
            var Disable = new DisableAntiAliasing(EnginePath);

            lblHideFocus.Focus();
            int graphic = 0;

            var ChangeSettings = new ChangeSettings(SettingsPath, "sg.AntiAliasingQuality=", graphic);
        }

        public void CheckValues(string SettingsPath, string EnginePath)
        {
            btnVwLow.ForeColor = Color.Black;
            btnVwLow.BackColor = Color.FromArgb(224, 224, 224);
            btnVwMedium.ForeColor = Color.Black;
            btnVwMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnVwHigh.ForeColor = Color.Black;
            btnVwHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnVwUltra.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnVwEpic.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnVwAwesome.ForeColor = Color.Black;
            btnVwAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaLow.ForeColor = Color.Black;
            btnAaLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAaMedium.ForeColor = Color.Black;
            btnAaMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAaHigh.ForeColor = Color.Black;
            btnAaHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAaUltra.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAaEpic.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAaAwesome.ForeColor = Color.Black;
            btnAaAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShadLow.ForeColor = Color.Black;
            btnShadLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShadMedium.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShadHigh.ForeColor = Color.Black;
            btnShadHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShadUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShadUltra.ForeColor = Color.Black;
            btnShadEpic.ForeColor = Color.Black;
            btnShadEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShadAwesome.ForeColor = Color.Black;
            btnShadAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnPpLow.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.FromArgb(224, 224, 224);
            btnPpMedium.ForeColor = Color.Black;
            btnPpMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnPpHigh.ForeColor = Color.Black;
            btnPpHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnPpUltra.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnPpEpic.ForeColor = Color.Black;
            btnPpEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnPpAwesome.ForeColor = Color.Black;
            btnPpAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtLow.ForeColor = Color.Black;
            btnTxtLow.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtMedium.ForeColor = Color.Black;
            btnTxtMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtUltra.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnTxtAwesome.ForeColor = Color.Black;
            btnTxtAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnEffLow.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.FromArgb(224, 224, 224);
            btnEffMedium.ForeColor = Color.Black;
            btnEffMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnEffHigh.ForeColor = Color.Black;
            btnEffHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnEffUltra.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnEffEpic.ForeColor = Color.Black;
            btnEffEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnEffAwesome.ForeColor = Color.Black;
            btnEffAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnFolLow.ForeColor = Color.Black;
            btnFolLow.BackColor = Color.FromArgb(224, 224, 224);
            btnFolMedium.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnFolHigh.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnFolUltra.ForeColor = Color.Black;
            btnFolUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnFolEpic.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnFolAwesome.ForeColor = Color.Black;
            btnFolAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnShLow.BackColor = Color.FromArgb(224, 224, 224);
            btnShLow.ForeColor = Color.Black;
            btnShMedium.ForeColor = Color.Black;
            btnShMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnShHigh.ForeColor = Color.Black;
            btnShHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnShUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnShAwesome.ForeColor = Color.Black;
            btnShAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimLow.ForeColor = Color.Black;
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimEpic.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAnimAwesome.ForeColor = Color.Black;
            btnAnimAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioLow.ForeColor = Color.Black;
            btnAudioLow.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioUltra.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioEpic.ForeColor = Color.Black;
            btnAudioEpic.BackColor = Color.FromArgb(224, 224, 224);
            btnAudioAwesome.ForeColor = Color.Black;
            btnAudioAwesome.BackColor = Color.FromArgb(224, 224, 224);
            btnAaDisable.ForeColor = Color.Black;
            btnAaDisable.BackColor = Color.FromArgb(224, 224, 224);

            string ReadConfig = File.ReadAllText(SettingsPath);
            string ReadEngine = File.ReadAllText(EnginePath);

            int numLines = File.ReadLines(SettingsPath).Count();
            int numLines2 = File.ReadLines(EnginePath).Count();

            string[] lines = File.ReadAllLines(SettingsPath);
            
            string[] lines2 = File.ReadAllLines(EnginePath);

            for (int i = 0; i < numLines; i++)
            {
                if (lines[i].Contains("FrameRateLimit="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result == 0)
                    {
                        for (int a = 0; a < numLines2; a++)
                        {
                            if (lines2[a].Contains("MaxSmoothedFrameRate="))
                            {
                                string str = Regex.Match(lines2[a], @"\d+").Value;
                                int checkStr = int.Parse(str);

                                numFPS.Value = checkStr;
                            }
                            else
                            {
                                numFPS.Value = 62;
                            }
                        }
                    }
                    else
                    {
                        numFPS.Value = result;
                    }
                }
                if (lines[i].Contains("sg.ResolutionQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result < tbRes.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "sg.ResolutionQuality=" + 100 + ".000000");
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbRes.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "sg.ResolutionQuality=" + 999 + ".000000");
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 999;
                    }
                    tbRes.Value = result;
                    lblRes.Text = tbRes.Value + "%";
                }
                if (lines[i].Contains("sg.ViewDistanceQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnVwLow.BackColor = Color.Crimson;
                        btnVwLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnVwMedium.BackColor = Color.Crimson;
                        btnVwMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnVwHigh.BackColor = Color.Crimson;
                        btnVwHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnVwUltra.BackColor = Color.Crimson;
                        btnVwUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnVwEpic.BackColor = Color.Crimson;
                        btnVwEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnVwAwesome.BackColor = Color.Crimson;
                        btnVwAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AntiAliasingQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        int check = 0;
                        ReadEngine = File.ReadAllText(EnginePath);
                        int numLines3 = File.ReadLines(EnginePath).Count();
                        string[] eng = File.ReadAllLines(EnginePath);

                        for (int a = 0; a <= numLines3 - 1; a++)
                        {
                            if (eng[a] == "[/Script/Engine.GarbageCollectionSettings]")
                            {
                                check++;
                            }
                            if (eng[a] == "r.DefaultFeature.AntiAliasing=0")
                            {
                                check++;
                            }
                        }

                        if (check == 2)
                        {
                            btnAaDisable.BackColor = Color.Crimson;
                            btnAaDisable.ForeColor = Color.White;
                        }
                        else
                        {
                            btnAaLow.BackColor = Color.Crimson;
                            btnAaLow.ForeColor = Color.White;
                        }

                    }
                    if (result == 1)
                    {
                        btnAaMedium.BackColor = Color.Crimson;
                        btnAaMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnAaHigh.BackColor = Color.Crimson;
                        btnAaHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnAaUltra.BackColor = Color.Crimson;
                        btnAaUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnAaEpic.BackColor = Color.Crimson;
                        btnAaEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnAaAwesome.BackColor = Color.Crimson;
                        btnAaAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadowQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnShadLow.BackColor = Color.Crimson;
                        btnShadLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnShadMedium.BackColor = Color.Crimson;
                        btnShadMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnShadHigh.BackColor = Color.Crimson;
                        btnShadHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnShadUltra.BackColor = Color.Crimson;
                        btnShadUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnShadEpic.BackColor = Color.Crimson;
                        btnShadEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnShadAwesome.BackColor = Color.Crimson;
                        btnShadAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.PostProcessQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnPpLow.BackColor = Color.Crimson;
                        btnPpLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnPpMedium.BackColor = Color.Crimson;
                        btnPpMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnPpHigh.BackColor = Color.Crimson;
                        btnPpHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnPpUltra.BackColor = Color.Crimson;
                        btnPpUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnPpEpic.BackColor = Color.Crimson;
                        btnPpEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnPpAwesome.BackColor = Color.Crimson;
                        btnPpAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.TextureQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnTxtLow.BackColor = Color.Crimson;
                        btnTxtLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnTxtMedium.BackColor = Color.Crimson;
                        btnTxtMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnTxtHigh.BackColor = Color.Crimson;
                        btnTxtHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnTxtUltra.BackColor = Color.Crimson;
                        btnTxtUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnTxtEpic.BackColor = Color.Crimson;
                        btnTxtEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnTxtAwesome.BackColor = Color.Crimson;
                        btnTxtAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.EffectsQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnEffLow.BackColor = Color.Crimson;
                        btnEffLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnEffMedium.BackColor = Color.Crimson;
                        btnEffMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnEffHigh.BackColor = Color.Crimson;
                        btnEffHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnEffUltra.BackColor = Color.Crimson;
                        btnEffUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnEffEpic.BackColor = Color.Crimson;
                        btnEffEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnEffAwesome.BackColor = Color.Crimson;
                        btnEffAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.FoliageQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnFolLow.BackColor = Color.Crimson;
                        btnFolLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnFolMedium.BackColor = Color.Crimson;
                        btnFolMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnFolHigh.BackColor = Color.Crimson;
                        btnFolHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnFolUltra.BackColor = Color.Crimson;
                        btnFolUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnFolEpic.BackColor = Color.Crimson;
                        btnFolEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnFolAwesome.BackColor = Color.Crimson;
                        btnFolAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadingQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnShLow.BackColor = Color.Crimson;
                        btnShLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnShMedium.BackColor = Color.Crimson;
                        btnShMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnShHigh.BackColor = Color.Crimson;
                        btnShHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnShUltra.BackColor = Color.Crimson;
                        btnShUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnShEpic.BackColor = Color.Crimson;
                        btnShEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnShAwesome.BackColor = Color.Crimson;
                        btnShAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AnimationQuality="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnAnimLow.BackColor = Color.Crimson;
                        btnAnimLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnAnimMedium.BackColor = Color.Crimson;
                        btnAnimMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnAnimHigh.BackColor = Color.Crimson;
                        btnAnimHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnAnimUltra.BackColor = Color.Crimson;
                        btnAnimUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnAnimEpic.BackColor = Color.Crimson;
                        btnAnimEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnAnimAwesome.BackColor = Color.Crimson;
                        btnAnimAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("AudioQualityLevel="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnAudioLow.BackColor = Color.Crimson;
                        btnAudioLow.ForeColor = Color.White;
                    }
                    if (result == 1)
                    {
                        btnAudioMedium.BackColor = Color.Crimson;
                        btnAudioMedium.ForeColor = Color.White;
                    }
                    if (result == 2)
                    {
                        btnAudioHigh.BackColor = Color.Crimson;
                        btnAudioHigh.ForeColor = Color.White;
                    }
                    if (result == 3)
                    {
                        btnAudioUltra.BackColor = Color.Crimson;
                        btnAudioUltra.ForeColor = Color.White;
                    }
                    if (result == 4)
                    {
                        btnAudioEpic.BackColor = Color.Crimson;
                        btnAudioEpic.ForeColor = Color.White;
                    }
                    if (result == 5)
                    {
                        btnAudioAwesome.BackColor = Color.Crimson;
                        btnAudioAwesome.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("MainVolume="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < trackBar1.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "MainVolume=" + 0);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 0;
                    }

                    if (result > trackBar1.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "MainVolume=" + 200);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 200;
                    }
                    trackBar1.Value = result;
                    lblMainVolume.Text = trackBar1.Value + "%";
                }
                if (lines[i].Contains("MenuMusicVolume="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < trackBar2.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "MenuMusicVolume=" + 0);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 0;
                    }

                    if (result > trackBar2.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "MenuMusicVolume=" + 200);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 200;
                    }

                    trackBar2.Value = result;
                    lblMenu.Text = trackBar2.Value + "%";
                }
                if (lines[i].Contains("KillerMouseSensitivity="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < KillerMouse.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "KillerMouseSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > KillerMouse.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "KillerMouseSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
                    }

                    KillerMouse.Value = result;
                    lblKillerMouse.Text = KillerMouse.Value + "%";
                }
                if (lines[i].Contains("SurvivorMouseSensitivity="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < SurvMouse.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "SurvivorMouseSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > SurvMouse.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "SurvivorMouseSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
                    }
                    SurvMouse.Value = result;
                    lblSurvMouse.Text = SurvMouse.Value + "%";
                }
                if (lines[i].Contains("KillerControllerSensitivity="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result < KillerController.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "KillerControllerSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > KillerController.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "KillerControllerSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
                    }
                    KillerController.Value = result;
                    lblKillerController.Text = KillerController.Value + "%";
                }
                if (lines[i].Contains("SurvivorControllerSensitivity="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < SurvController.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "SurvivorControllerSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > SurvController.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "SurvivorControllerSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
                    }
                    SurvController.Value = result;
                    lblSurvController.Text = SurvController.Value + "%";
                }
                if (lines[i].Contains("ResolutionSizeX="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result > numWidth.Maximum)
                    {
                        result = 1920;
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeX=" + 1920);
                        File.WriteAllText(SettingsPath, ReadConfig);
                    }

                    if (result < numWidth.Minimum)
                    {
                        result = 1920;
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeX=" + 1920);
                        File.WriteAllText(SettingsPath, ReadConfig);
                    }

                    numWidth.Value = result;
                }
                if (lines[i].Contains("ResolutionSizeY="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result > numHeight.Maximum)
                    {
                        result = 1080;
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeY=" + 1080);
                        File.WriteAllText(SettingsPath, ReadConfig);
                    }

                    if (result < numHeight.Minimum)
                    {
                        result = 1080;
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeY=" + 1080);
                        File.WriteAllText(SettingsPath, ReadConfig);
                    }

                    numHeight.Value = result;
                }
                if (lines[i].Contains("bUseVSync="))
                {
                    if (lines[i].Contains("False"))
                    {
                        checkBox2.Checked = false;
                    }
                    if (lines[i].Contains("True"))
                    {
                        checkBox2.Checked = true;
                    }
                }

                if (lines[i].Contains("UseHeadphones="))
                {
                    if (lines[i].Contains("False"))
                    {
                        checkBox1.Checked = false;
                    }
                    if (lines[i].Contains("True"))
                    {
                        checkBox1.Checked = true;
                    }
                }

                if (lines[i].Contains("ScreenResolution="))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result < tbScRes.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ScreenResolution=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbScRes.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ScreenResolution=" + 300);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 300;
                    }
                    tbScRes.Value = result;
                    lblScRes.Text = tbScRes.Value + "%";
                }
                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);
                ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");
                ReadEngine = Regex.Replace(ReadEngine, Environment.NewLine + Environment.NewLine, "\r");

                File.WriteAllText(SettingsPath, ReadConfig);
                File.WriteAllText(EnginePath, ReadEngine);
            }
        }

        private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if((string)cbVersion.SelectedItem == "Steam")
            {
                EnginePath = SteamEnginePath;
                SettingsPath = SteamSettingsPath;
                CheckValues(SettingsPath, EnginePath);
            }
            if((string)cbVersion.SelectedItem == "Epic Games Store")
            {
                EnginePath = EGSEnginePath;
                SettingsPath = EGSSettingsPath;
                CheckValues(SettingsPath, EnginePath);
            }
            if ((string)cbVersion.SelectedItem == "Microsoft Store")
            {
                EnginePath = MSEnginePath;
                SettingsPath = MSSettingsPath;
                CheckValues(SettingsPath, EnginePath);
            }
        }
    }

    public class ChangeSettings
    {
        public ChangeSettings(string path, string option, int valueInt)
        {
            string read = File.ReadAllText(path);

            int numLines = File.ReadLines(path).Count();
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < numLines; i++)
            {
                if (lines[i].Contains(option))
                {
                    read = read.Replace(lines[i], option + valueInt);
                    File.WriteAllText(path, read);
                }
            }
        }
    }


    public class SetFPS
    {
        public SetFPS(string path, string option, int valueInt)
        {
            string read = File.ReadAllText(path);
            int numLines = File.ReadLines(path).Count();
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < numLines; i++)
            {
                if (lines[i].Contains(option))
                {
                    if (valueInt == 60 || valueInt == 61 || valueInt == 62)
                    {
                        read = read.Replace(lines[i], option + "0.000000");
                        read = Regex.Replace(read, Environment.NewLine + Environment.NewLine, "\r");
                        File.WriteAllText(path, read);
                    }
                    else
                    {
                        read = read.Replace(lines[i], option + valueInt + ".000000");
                        read = Regex.Replace(read, Environment.NewLine + Environment.NewLine, "\r");
                        File.WriteAllText(path, read);
                    }
                }
            }
        }
    }

    public class ResetFPS
    {
        public ResetFPS(string path)
        {
            string ReadEngine = File.ReadAllText(path);
            int numLines2 = File.ReadLines(path).Count();
            string[] eng = File.ReadAllLines(path);

            for (int a = 0; a < numLines2; a++)
            {
                if (eng[a].Contains("script/ReadEngine.ReadEngine"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].Contains("bSmoothFrameRate"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].Contains("MinSmoothedFrameRate"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].Contains("MaxSmoothedFrameRate"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].Contains("bUseVSync"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
            }
        }
    }

    public class EngineSetFPS
    {
        public EngineSetFPS(string EnginePath, int value)
        {
            string ReadEngine = File.ReadAllText(EnginePath);
            ReadEngine += "\n\n[/script/ReadEngine.ReadEngine]\nbSmoothFrameRate=False\nMinSmoothedFrameRate=1\nMaxSmoothedFrameRate=" + value;
            File.WriteAllText(EnginePath, ReadEngine);
        }
    }

    public class SetResolution
    {
        public SetResolution(string path, int WidthValue, int HeightValue)
        {
            string ReadConfig = File.ReadAllText(path);
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int numLines = File.ReadLines(path).Count();
            string[] lines = File.ReadAllLines(path);

            if (HeightValue != screenHeight || WidthValue != screenWidth)
            {
                for (int i = 0; i < numLines; i++)
                {
                    if (lines[i].Contains("ResolutionSizeX") && !lines[i].Contains("LastUser"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeX=" + WidthValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("ResolutionSizeY") && !lines[i].Contains("LastUser"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeY=" + HeightValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastUserConfirmedResolutionSizeX"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeX=" + WidthValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastUserConfirmedResolutionSizeY"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeY=" + HeightValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("FullscreenMode") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Prefered"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "FullscreenMode=" + 0);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastConfirmedFullscreenMode") && !lines[i].Contains("Prefered"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastConfirmedFullscreenMode=" + 0);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("PreferredFullscreenMode") && !lines[i].Contains("LastConfirmed"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "PreferredFullscreenMode=" + 0);
                        File.WriteAllText(path, ReadConfig);
                    }
                }
            }

            if (HeightValue == screenHeight && WidthValue == screenWidth)
            {
                for (int i = 0; i < numLines; i++)
                {
                    if (lines[i].Contains("ResolutionSizeX") && !lines[i].Contains("LastUser"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeX=" + WidthValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("ResolutionSizeY") && !lines[i].Contains("LastUser"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeY=" + HeightValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastUserConfirmedResolutionSizeX"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeX=" + WidthValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastUserConfirmedResolutionSizeY"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeY=" + HeightValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("FullscreenMode") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Prefered"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "FullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastConfirmedFullscreenMode") && !lines[i].Contains("Prefered"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastConfirmedFullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("PreferredFullscreenMode") && !lines[i].Contains("LastConfirmed"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "PreferredFullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                }
            }
        }
    }

    public class ResetResolution
    {
        public ResetResolution(string path)
        {
            string ReadConfig = File.ReadAllText(path);

            int numLines = File.ReadLines(path).Count();
            string[] lines = File.ReadAllLines(path);

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            for (int i = 0; i < numLines; i++)
            {

                if (lines[i].Contains("ResolutionSizeX") && !lines[i].Contains("LastUser"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeX=" + screenWidth);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("ResolutionSizeY") && !lines[i].Contains("LastUser"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeY=" + screenHeight);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeX"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeX=" + screenWidth);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeY"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeY=" + screenHeight);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("FullscreenMode") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Prefered"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "FullscreenMode=" + 1);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("LastConfirmedFullscreenMode") && !lines[i].Contains("Prefered"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "LastConfirmedFullscreenMode=" + 1);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("PreferredFullscreenMode") && !lines[i].Contains("LastConfirmed"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "PreferredFullscreenMode=" + 1);
                    File.WriteAllText(path, ReadConfig);
                }
            }
        }
    }

    public class SettingsTrueOrFalse
    {
        public SettingsTrueOrFalse(string path, string option, bool value)
        {
            string read = File.ReadAllText(path);

            int numLines = File.ReadLines(path).Count();
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < numLines; i++)
            {
                if (lines[i].Contains(option))
                {
                    read = read.Replace(lines[i], option + value);
                    File.WriteAllText(path, read);
                }
            }
        }
    }

    public class CheckDate
    {
        public CheckDate(PictureBox imgPumpkin, PictureBox imgXmasTree, PictureBox imgFireworks)
        {
            //halloween
            DateTime date = DateTime.Now;
            if (date.Month == 10 && date.Day >= 16 && date.Day <= 31)
            {
                imgPumpkin.Show();
            }
            else
            {
                imgPumpkin.Hide();
            }

            //christmas
            if (date.Month == 12 && date.Day >= 1 && date.Day <= 28)
            {
                imgXmasTree.Show();
            }
            else
            {
                imgXmasTree.Hide();
            }
            //new Year's Eve
            if (date.Month == 12 && date.Day >= 29 && date.Day <= 31)
            {
                imgFireworks.Show();
            }
            else
            {
                imgFireworks.Hide();
            }
        }
    }

    public class DisableAntiAliasing
    {
        public DisableAntiAliasing(string EnginePath)
        {
            string ReadEngine = File.ReadAllText(EnginePath);
            ReadEngine += "\n\n\n[/Script/Engine.GarbageCollectionSettings]\nr.DefaultFeature.AntiAliasing=0";
            ReadEngine = Regex.Replace(ReadEngine, Environment.NewLine + Environment.NewLine, "\r");
            File.WriteAllText(EnginePath, ReadEngine);
        }
    }

    public class DeleteAntiAliasing
    {
        public DeleteAntiAliasing(string EnginePath)
        {
            string ReadEngine = File.ReadAllText(EnginePath);

            File.WriteAllText(EnginePath, ReadEngine);
            int numLines2 = File.ReadLines(EnginePath).Count();
            string[] eng = File.ReadAllLines(EnginePath);

            for (int a = 0; a < numLines2; a++)
            {
                if (eng[a].Contains("[/Script/Engine.GarbageCollectionSettings]"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(EnginePath, ReadEngine);
                }
                if (eng[a].Contains("r.DefaultFeature.AntiAliasing=0"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(EnginePath, ReadEngine);
                }
            }
        }
    }
}
