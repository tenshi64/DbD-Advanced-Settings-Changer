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

        public string ApplicationLanguageData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Language\";

        ToolTip tip = new ToolTip();

        public string UsersSettingsContent;
        public string UsersEngineContent;

        string ReadConfig;
        string ReadEngine;
        IDictionary<string, string> errors = new Dictionary<string, string>();

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
            if(!Directory.Exists(ApplicationLanguageData))
            {
                Directory.CreateDirectory(ApplicationLanguageData);
            }
            if (!File.Exists(ApplicationLanguageData + @"lang.ini"))
            {
                File.WriteAllText(ApplicationLanguageData + @"lang.ini", "en");
            }

            string path = File.ReadAllText(ApplicationLanguageData + "lang.ini");
            int numLines = File.ReadLines(ApplicationLanguageData + "lang.ini").Count();
            string[] lang = File.ReadAllLines(ApplicationLanguageData + "lang.ini");
            string[] words = new string[] { "en", "pl", "ru", "du", "fr", "jp", "ch", "tu", "esp", "it"};

            bool found = false;
            string word = "";
            for (int a = 0; a < numLines; a++)
            {
                for(int i = 0; i < words.Length; i++)
                {
                    if (lang[a] == words[i])
                    {
                        word = words[i];
                        found = true;
                        switch(word)
                        {
                            case "en":
                                word = "English";
                                break;
                            case "pl":
                                word = "Polski";
                                break;
                            case "ru":
                                word = "русский";
                                break;
                            case "du":
                                word = "Deutsch";
                                break;
                            case "fr":
                                word = "Français";
                                break;
                            case "jp":
                                word = "日本";
                                break;
                            case "ch":
                                word = "中国人";
                                break;
                            case "tu":
                                word = "Türkçe";
                                break;
                            case "esp":
                                word = "Español";
                                break;
                            case "it":
                                word = "Italiano";
                                break;
                        }
                    }
                }
            }

            if(!found)
            {
                word = "English";
                File.WriteAllText(ApplicationLanguageData + "lang.ini", "en");
            }
            cbLanguage.SelectedIndex = cbLanguage.Items.IndexOf(word);
            ChangeAppLanguage(word);

            if (!File.Exists(SteamSettingsPath) && !File.Exists(SteamEnginePath) && !File.Exists(EGSSettingsPath) && !File.Exists(EGSEnginePath) && !File.Exists(MSSettingsPath) && !File.Exists(MSEnginePath))
            {
                MessageBox.Show(errors["confs-dont-exist"], errors["error"],
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
                string textFromFile = wc.DownloadString("https://pastebin.com/yjMGKd4u");
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (!textFromFile.Contains(version))
                {
                    DialogResult result = MessageBox.Show(errors["new-version"], errors["new-version-title"],
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
                MessageBox.Show(errors["new-version-fail"], errors["new-version-title"],
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblHideFocus.Focus();

            try
            {
                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);
                         
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
            int resolution = tbRes.Value;
            lblRes.Text = resolution.ToString() + "%";

            var ChangeResolution = new ChangeResolution(resolution, SettingsPath);
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
            tip.Show(errors["tip1"], btnUsersSettings);
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
            

            bool check = true;
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
                MessageBox.Show(errors["max-fps"] +  (int)numFPS.Value + errors["max-fps-continuation"], errors["information"], MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            tip.Show(errors["tip2"], btnResetFPS);
        }

        private void btnResetFPS_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResetFPS);
        }

        private void btnSetFPS_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip3"], btnSetFPS);
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
            tip.Show(errors["tip4"], btnResSet);
        }

        private void btnResSet_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResSet);
        }

        private void btnResReset_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip5"], btnResReset);
        }

        private void btnResReset_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResReset);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //

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
            tip.Show(errors["tip6"], btnDelete);
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.Transparent;
            tip.Hide(btnDelete);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(errors["delete-confs"], errors["delete-confs-title"],
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                File.Delete(ApplicationData + "UsEng.ini");
                File.Delete(ApplicationData + "UsSet.ini");
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Autosave");
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
                MessageBox.Show(errors["discord-link-fail"], errors["error"],
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDiscord_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip7"], btnDiscord);
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
            this.Size = new Size(1145, 520);
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
                if (lines[i].Contains("HUDConstrainedAspectRatio="))
                {
                    if (lines[i].Contains("True"))
                    {
                        int check = 0;
                        int numLines3 = File.ReadLines(EnginePath).Count();
                        string[] eng = File.ReadAllLines(EnginePath);

                        for (int a = 0; a <= numLines3 - 1; a++)
                        {
                            if (eng[a] == "[/Script/Engine.LocalPlayer]")
                            {
                                check++;
                            }
                            if (eng[a] == "AspectRatioAxisConstraint=AspectRatio_MAX")
                            {
                                check++;
                            }
                        }

                        if (check == 2)
                        {
                            checkBox3.Checked = true;
                        }
                        else
                        {
                            checkBox3.Checked = false;
                        }
                    }
                }
                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);
                
                

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

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            ChangeAppLanguage(cbLanguage.SelectedItem.ToString());
            string word = "";
            switch (cbLanguage.SelectedItem.ToString())
            {
                case "English":
                    word = "en";
                    break;
                case "Polski":
                    word = "pl";
                    break;
                case "русский":
                    word = "ru";
                    break;
                case "Deutsch":
                    word = "du";
                    break;
                case "Français":
                    word = "fr";
                    break;
                case "日本":
                    word = "jp";
                    break;
                case "中国人":
                    word = "ch";
                    break;
                case "Türkçe":
                    word = "tu";
                    break;
                case "Español":
                    word = "esp";
                    break;
                case "Italiano":
                    word = "it";
                    break;
            }

            string path = File.ReadAllText(ApplicationLanguageData + "lang.ini");
            int numLines = File.ReadLines(ApplicationLanguageData + "lang.ini").Count();
            string[] lang = File.ReadAllLines(ApplicationLanguageData + "lang.ini");
            string[] words = new string[] { "en", "pl", "ru", "du", "fr", "jp", "ch", "tu", "esp", "it" };

            for (int a = 0; a < numLines; a++)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (lang[a].Contains(words[i]))
                    {
                        path = path.Replace(lang[a], word);
                        File.WriteAllText(ApplicationLanguageData + "lang.ini", path);
                    }
                }    
            }
        }

        public void ChangeAppLanguage(string Language)
        {
            if (Language == "English")
            {
                errors["confs-dont-exist"] = "Dead by Daylight confiuration files do not exist! Please, launch the game.";
                errors["new-version"] = "A new version of the program has been found.\n\nDo you want to download it?";
                errors["new-version-fail"] = "Checking newer program versions failed!";
                errors["max-fps"] = "The maximum frame rate in Dead by Daylight is 120. The game will be displayed at 120 frames per second anyway. Program will set you a limit of ";
                errors["max-fps-continuation"] = " FPS anyway.";
                errors["delete-confs"] = "Are you sure you want to delete the configuration file and create a new one?";
                errors["delete-confs-title"] = "Delete backup config";
                errors["discord-link-fail"] = "There was a problem opening the Discord link!";
                errors["new-version-title"] = "Check for updates";
                errors["information"] = "Information";
                errors["error"] = "Error";

                errors["tip1"] = "Reset settings to those saved in the backup.";
                errors["tip2"] = "Reset FPS cap.";
                errors["tip3"] = "Set your own FPS cap.";
                errors["tip4"] = "Set your own game resolution.";
                errors["tip5"] = "Reset to system resolution.";
                errors["tip6"] = "Delete the backup configuration file and create a new one.";
                errors["tip7"] = "Official Discord Server.";


                checkBox3.Text = "Remove black bars";
                lblInAppLang.Text = "In-app language:";
                lblChangeSettings.Text = "Game version:";
                label12.Text = "Audio";
                label2.Text = "Graphics";
                label14.Text = "FPS Cap";
                label16.Text = "Graphic presets";
                label15.Text = "Mouse sensitivity";
                label19.Text = "Screen";
                lblWidth.Text = "Width";
                lblHeight.Text = "Height";
                label35.Text = "2D Resolution";
                btnResSet.Text = "Set";
                btnResReset.Text = "Reset";
                label1.Text = "3D Resolution";
                label3.Text = "View Distance Quality";
                lblAaQuality.Text = "Anti-Aliasing Quality";
                label5.Text = "Shadow Quality";
                label6.Text = "Post-Processing Quality";
                label7.Text = "Textures Quality";
                label8.Text = "Effects Quality";
                label9.Text = "Foliage Quality";
                label10.Text = "Shading Quality";
                label11.Text = "Animations Quality";

                btnVwLow.Text = "Low";
                btnVwMedium.Text = "Medium";
                btnVwHigh.Text = "High";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epic";
                btnVwAwesome.Text = "Awesome";

                btnAaLow.Text = "Low";
                btnAaMedium.Text = "Medium";
                btnAaHigh.Text = "High";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epic";
                btnAaAwesome.Text = "Awesome";

                btnShadLow.Text = "Low";
                btnShadMedium.Text = "Medium";
                btnShadHigh.Text = "High";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epic";
                btnShadAwesome.Text = "Awesome";

                btnPpLow.Text = "Low";
                btnPpMedium.Text = "Medium";
                btnPpHigh.Text = "High";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epic";
                btnPpAwesome.Text = "Awesome";

                btnTxtLow.Text = "Low";
                btnTxtMedium.Text = "Medium";
                btnTxtHigh.Text = "High";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epic";
                btnTxtAwesome.Text = "Awesome";

                btnEffLow.Text = "Low";
                btnEffMedium.Text = "Medium";
                btnEffHigh.Text = "High";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epic";
                btnEffAwesome.Text = "Awesome";

                btnFolLow.Text = "Low";
                btnFolMedium.Text = "Medium";
                btnFolHigh.Text = "High";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epic";
                btnFolAwesome.Text = "Awesome";

                btnShLow.Text = "Low";
                btnShMedium.Text = "Medium";
                btnShHigh.Text = "High";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epic";
                btnShAwesome.Text = "Awesome";

                btnAnimLow.Text = "Low";
                btnAnimMedium.Text = "Medium";
                btnAnimHigh.Text = "High";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epic";
                btnAnimAwesome.Text = "Awesome";
                btnAaDisable.Text = "Disable";
                checkBox2.Text = "V-Sync";
                btnSetFPS.Text = "Set";
                btnResetFPS.Text = "Reset";
                label18.Text = "Set your own FPS cap";
                label22.Text = "Main Volume";
                label13.Text = "Menu Music";
                label21.Text = "Audio Quality";
                checkBox1.Text = "Headphones";

                btnAudioLow.Text = "Low";
                btnAudioMedium.Text = "Medium";
                btnAudioHigh.Text = "High";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epic";
                btnAudioAwesome.Text = "Awesome";
                
                label17.Text = "Choose one of the graphic presets.";
                btnPresetLow.Text = "Super low";
                btnPresetMedium.Text = "Medium";
                btnPresetEpic.Text = "Awesome";

                label24.Text = "Killer (mouse)";
                label25.Text = "Killer (controller)";
                label29.Text = "Survivor (mouse)";
                label27.Text = "Survivor (controller)";
            }
            if (Language == "Polski")
            {
                errors["confs-dont-exist"] = "Pliki konfiguracyjne Dead by Daylight nie istnieją! Uruchom grę.";
                errors["new-version"] = "Znaleziono nową wersję programu.\n\nCzy chcesz ją pobrać?";
                errors["new-version-fail"] = "Sprawdzanie nowszych wersji programu nie powiodło się!";
                errors["max-fps"] = "Maksymalna liczba klatek na sekundę w Dead by Daylight to 120. Gra i tak będzie wyświetlana z szybkością 120 klatek na sekundę. Program ustawi ci limit na ";
                errors["max-fps-continuation"] = " FPS mimo to.";
                errors["delete-confs"] = "Czy na pewno chcesz usunąć plik konfiguracyjny i utworzyć nowy?";
                errors["delete-confs-title"] = "Usuń konfigurację kopii zapasowej";
                errors["discord-link-fail"] = "Wystąpił problem podczas otwierania linku Discord!";
                errors["new-version-title"] = "Sprawdź aktualizacje";
                errors["information"] = "Informacje";
                errors["error"] = "Błąd";

                errors["tip1"] = "Zresetuj ustawienia do tych zapisanych w kopii zapasowej.";
                errors["tip2"] = "Zresetuj limit FPS.";
                errors["tip3"] = "Ustaw własny limit FPS.";
                errors["tip4"] = "Ustaw własną rozdzielczość gry.";
                errors["tip5"] = "Zresetuj do rozdzielczości systemu.";
                errors["tip6"] = "Usuń plik konfiguracyjny kopii zapasowej i utwórz nowy.";
                errors["tip7"] = "Oficjalny serwer Discord.";

                checkBox3.Text = "Usuń czarne paski";
                lblInAppLang.Text = "Język w aplikacji:";
                lblChangeSettings.Text = "Wersja gry:";
                label12.Text = "Dźwięk";
                label2.Text = "Grafika";
                label14.Text = "Limit FPS";
                label16.Text = "Graficz. presety";
                label15.Text = "Czułość myszy";
                label19.Text = "Ekran";
                lblWidth.Text = "Szerokość";
                lblHeight.Text = "Wysokość";
                label35.Text = "Rozdzielczość 2D";
                btnResSet.Text = "Ustaw";
                btnResReset.Text = "Zresetuj";
                label1.Text = "Rozdzielczość 3D";
                label3.Text = "Jakość odgległości renderowania";
                lblAaQuality.Text = "Jakość Anty-Aliasingu";
                label5.Text = "Jakość cieni";
                label6.Text = "Jakość Post-Processingu";
                label7.Text = "Jakość tekstur";
                label8.Text = "Jakość efektów";
                label9.Text = "Jakość trawy/natury";
                label10.Text = "Jakość cieniowania";
                label11.Text = "Jakość animacji";

                btnVwLow.Text = "Niska";
                btnVwMedium.Text = "Średnia";
                btnVwHigh.Text = "Wysoka";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epicka";
                btnVwAwesome.Text = "Świetna";

                btnAaLow.Text = "Niska";
                btnAaMedium.Text = "Średnia";
                btnAaHigh.Text = "Wysoka";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epicka";
                btnAaAwesome.Text = "Świetna";

                btnShadLow.Text = "Niska";
                btnShadMedium.Text = "Średnia";
                btnShadHigh.Text = "Wysoka";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epicka";
                btnShadAwesome.Text = "Świetna";

                btnPpLow.Text = "Niska";
                btnPpMedium.Text = "Średnia";
                btnPpHigh.Text = "Wysoka";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epicka";
                btnPpAwesome.Text = "Świetna";

                btnTxtLow.Text = "Niska";
                btnTxtMedium.Text = "Średnia";
                btnTxtHigh.Text = "Wysoka";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epicka";
                btnTxtAwesome.Text = "Świetna";

                btnEffLow.Text = "Niska";
                btnEffMedium.Text = "Średnia";
                btnEffHigh.Text = "Wysoka";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epicka";
                btnEffAwesome.Text = "Świetna";

                btnFolLow.Text = "Niska";
                btnFolMedium.Text = "Średnia";
                btnFolHigh.Text = "Wysoka";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epicka";
                btnFolAwesome.Text = "Świetna";

                btnShLow.Text = "Niska";
                btnShMedium.Text = "Średnia";
                btnShHigh.Text = "Wysoka";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epicka";
                btnShAwesome.Text = "Świetna";

                btnAnimLow.Text = "Niska";
                btnAnimMedium.Text = "Średnia";
                btnAnimHigh.Text = "Wysoka";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epicka";
                btnAnimAwesome.Text = "Świetna";
                btnAaDisable.Text = "Wyłącz";

                checkBox2.Text = "Synchronizacja pionowa";
                btnSetFPS.Text = "Ustaw";
                btnResetFPS.Text = "Zresetuj";
                label18.Text = "Ustaw własny limit FPS";

                label22.Text = "Główna głośność";
                label13.Text = "Muzyka w menu";
                label21.Text = "Jakość dźwięku";
                checkBox1.Text = "Słuchawki";

                btnAudioLow.Text = "Niska";
                btnAudioMedium.Text = "Średnia";
                btnAudioHigh.Text = "Wysoka";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epicka";
                btnAudioAwesome.Text = "Świetna";

                label17.Text = "Wybierz jeden z graficznych presetów";
                btnPresetLow.Text = "Bardzo niskie";
                btnPresetMedium.Text = "Średnie";
                btnPresetEpic.Text = "Świetne";

                label24.Text = "Zabójca (mysz)";
                label25.Text = "Zabójca (kontroler)";
                label29.Text = "Ocalały (mysz)";
                label27.Text = "Ocalały (kontroler)";
            }
            if (Language == "русский")
            {
                errors["confs-dont-exist"] = "Файлы конфигурации Dead by Daylight не существуют! Пожалуйста, запустите игру.";
                errors["new-version"] = "Обнаружена новая версия программы.\n\nВы хотите ее скачать?";
                errors["new-version-fail"] = "Проверка новых версий программы не удалась!";
                errors["max-fps"] = "Максимальная частота кадров в Dead by Daylight - 120. Игра будет отображаться со скоростью 120 кадров в секунду в любом случае. Программа установит вам ограничение ";
                errors["max-fps-continuation"] = " Все равно FPS.";
                errors["delete-confs"] = "Вы уверены, что хотите удалить файл конфигурации и создать новый?";
                errors["delete-confs-title"] = "Удалить резервную копию конфигурации";
                errors["discord-link-fail"] = "Не удалось открыть ссылку Discord!";
                errors["new-version-title"] = "Проверить наличие обновлений";
                errors["information"] = "Информация";
                errors["error"] = "Ошибка";

                errors["tip1"] = "Сбросить настройки до сохраненных в резервной копии.";
                errors["tip2"] = "Сбросить ограничение FPS.";
                errors["tip3"] = "Установите собственное ограничение FPS.";
                errors["tip4"] = "Установите собственное разрешение игры.";
                errors["tip5"] = "Сбросить системное разрешение.";
                errors["tip6"] = "Удалите резервный файл конфигурации и создайте новый.";
                errors["tip7"] = "Официальный сервер Discord.";

                checkBox3.Text = "Удалить черные полосы";
                lblInAppLang.Text = "Язык приложения:";
                lblChangeSettings.Text = "Версия игры:";
                label12.Text = "Аудио";
                label2.Text = "Графика";
                label14.Text = "FPS";
                label16.Text = "пресеты";
                label15.Text = "чувствительность";
                label19.Text = "Экран";
                lblWidth.Text = "Ширина";
                lblHeight.Text = "Высота";
                label35.Text = "2D-разрешение";
                btnResSet.Text = "Набор";
                btnResReset.Text = "Перезагрузить";
                label1.Text = "3D-разрешение";
                label3.Text = "Просмотр качества расстояния";
                lblAaQuality.Text = "Качество сглаживания";
                label5.Text = "Качество теней";
                label6.Text = "Качество постобработки";
                label7.Text = "Качество текстур";
                label8.Text = "Качество эффектов";
                label9.Text = "Качество листвы";
                label10.Text = "Качество затенения";
                label11.Text = "Качество анимации";

                btnVwLow.Text = "Низкий";
                btnVwMedium.Text = "Середина";
                btnVwHigh.Text = "Высокий";
                btnVwUltra.Text = "Ультра";
                btnVwEpic.Text = "Эпический";
                btnVwAwesome.Text = "Потрясающий";

                btnAaLow.Text = "Низкий";
                btnAaMedium.Text = "Середина";
                btnAaHigh.Text = "Высокий";
                btnAaUltra.Text = "Ультра";
                btnAaEpic.Text = "Эпический";
                btnAaAwesome.Text = "Потрясающий";

                btnShadLow.Text = "Низкий";
                btnShadMedium.Text = "Середина";
                btnShadHigh.Text = "Высокий";
                btnShadUltra.Text = "Ультра";
                btnShadEpic.Text = "Эпический";
                btnShadAwesome.Text = "Потрясающий";

                btnPpLow.Text = "Низкий";
                btnPpMedium.Text = "Середина";
                btnPpHigh.Text = "Высокий";
                btnPpUltra.Text = "Ультра";
                btnPpEpic.Text = "Эпический";
                btnPpAwesome.Text = "Потрясающий";

                btnTxtLow.Text = "Низкий";
                btnTxtMedium.Text = "Середина";
                btnTxtHigh.Text = "Высокий";
                btnTxtUltra.Text = "Ультра";
                btnTxtEpic.Text = "Эпический";
                btnTxtAwesome.Text = "Потрясающий";

                btnEffLow.Text = "Низкий";
                btnEffMedium.Text = "Середина";
                btnEffHigh.Text = "Высокий";
                btnEffUltra.Text = "Ультра";
                btnEffEpic.Text = "Эпический";
                btnEffAwesome.Text = "Потрясающий";

                btnFolLow.Text = "Низкий";
                btnFolMedium.Text = "Середина";
                btnFolHigh.Text = "Высокий";
                btnFolUltra.Text = "Ультра";
                btnFolEpic.Text = "Эпический";
                btnFolAwesome.Text = "Потрясающий";

                btnShLow.Text = "Низкий";
                btnShMedium.Text = "Середина";
                btnShHigh.Text = "Высокий";
                btnShUltra.Text = "Ультра";
                btnShEpic.Text = "Эпический";
                btnShAwesome.Text = "Потрясающий";

                btnAnimLow.Text = "Низкий";
                btnAnimMedium.Text = "Середина";
                btnAnimHigh.Text = "Высокий";
                btnAnimUltra.Text = "Ультра";
                btnAnimEpic.Text = "Эпический";
                btnAnimAwesome.Text = "Потрясающий";
                btnAaDisable.Text = "Запрещать";
                checkBox2.Text = "вертикальная синхронизация";
                btnSetFPS.Text = "Набор";
                btnResetFPS.Text = "Перезагрузить";
                label18.Text = "Установите собственное ограничение FPS";
                label22.Text = "Основной объем";
                label13.Text = "Меню Музыка";
                label21.Text = "Качество звука";
                checkBox1.Text = "Наушники";

                btnAudioLow.Text = "Низкий";
                btnAudioMedium.Text = "Середина";
                btnAudioHigh.Text = "Высокий";
                btnAudioUltra.Text = "Ультра";
                btnAudioEpic.Text = "Эпический";
                btnAudioAwesome.Text = "Потрясающий";

                label17.Text = "Выберите один из графических пресетов.";
                btnPresetLow.Text = "Супер низкий";
                btnPresetMedium.Text = "Середина";
                btnPresetEpic.Text = "Потрясающий";

                label24.Text = "Убийца (мышь)";
                label25.Text = "Киллер (контролер)";
                label29.Text = "Выживший (мышь)";
                label27.Text = "Выживший (контроллер)";
            }
            if (Language == "Deutsch")
            {
                errors["confs-dont-exist"] = "Dead by Daylight Konfigurationsdateien existieren nicht! Bitte starten Sie das Spiel.";
                errors["new-version"] = "Eine neue Version des Programms wurde gefunden.\n\nMöchten Sie sie herunterladen?";
                errors["new-version-fail"] = "Überprüfung neuerer Programmversionen fehlgeschlagen!";
                errors["max-fps"] = "Die maximale Bildrate in Dead by Daylight beträgt 120. Das Spiel wird trotzdem mit 120 Bildern pro Sekunde angezeigt. Das Programm setzt Ihnen ein Limit von ";
                errors["max-fps-continuation"] = " FPS sowieso.";
                errors["delete-confs"] = "Möchten Sie die Konfigurationsdatei wirklich löschen und eine neue erstellen?";
                errors["delete-confs-title"] = "Backup-Konfiguration löschen";
                errors["discord-link-fail"] = "Beim Öffnen des Discord-Links ist ein Problem aufgetreten!";
                errors["new-version-title"] = "Nach Updates suchen";
                errors["information"] = "Informationen";
                errors["error"] = "Fehler";

                errors["tip1"] = "Einstellungen auf die im Backup gespeicherten zurücksetzen.";
                errors["tip2"] = "FPS-Obergrenze zurücksetzen.";
                errors["tip3"] = "Legen Sie Ihre eigene FPS-Obergrenze fest.";
                errors["tip4"] = "Stellen Sie Ihre eigene Spielauflösung ein.";
                errors["tip5"] = "Auf Systemauflösung zurücksetzen.";
                errors["tip6"] = "Löschen Sie die Backup-Konfigurationsdatei und erstellen Sie eine neue.";
                errors["tip7"] = "Offizieller Discord-Server.";

                checkBox3.Text = "Schwarze Balken entfernen";
                lblInAppLang.Text = "In-App-Sprache:";
                lblChangeSettings.Text = "Spielversion:";
                label12.Text = "Audio";
                label2.Text = "Grafik";
                label14.Text = "FPS";
                label16.Text = "Voreinstellungen";
                label15.Text = "Empfindlichkeit";
                label19.Text = "Schirm";
                lblWidth.Text = "Breite";
                lblHeight.Text = "Höhe";
                label35.Text = "2D-Auflösung";
                btnResSet.Text = "Satz";
                btnResReset.Text = "Zurücksetzen";
                label1.Text = "3D-Auflösung";
                label3.Text = "Entfernungsqualität anzeigen";
                lblAaQuality.Text = "Anti-Aliasing-Qualität";
                label5.Text = "Schattenqualität";
                label6.Text = "Qualität der Nachbearbeitung";
                label7.Text = "Qualität der Texturen";
                label8.Text = "Effektqualität";
                label9.Text = "Laubqualität";
                label10.Text = "Schattierungsqualität";
                label11.Text = "Animationsqualität";

                btnVwLow.Text = "Niedrig";
                btnVwMedium.Text = "Mittel";
                btnVwHigh.Text = "Hoch";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Episch";
                btnVwAwesome.Text = "Toll";

                btnAaLow.Text = "Niedrig";
                btnAaMedium.Text = "Mittel";
                btnAaHigh.Text = "Hoch";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Episch";
                btnAaAwesome.Text = "Toll";

                btnShadLow.Text = "Niedrig";
                btnShadMedium.Text = "Mittel";
                btnShadHigh.Text = "Hoch";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Episch";
                btnShadAwesome.Text = "Toll";

                btnPpLow.Text = "Niedrig";
                btnPpMedium.Text = "Mittel";
                btnPpHigh.Text = "Hoch";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Episch";
                btnPpAwesome.Text = "Toll";

                btnTxtLow.Text = "Niedrig";
                btnTxtMedium.Text = "Mittel";
                btnTxtHigh.Text = "Hoch";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Episch";
                btnTxtAwesome.Text = "Toll";

                btnEffLow.Text = "Niedrig";
                btnEffMedium.Text = "Mittel";
                btnEffHigh.Text = "Hoch";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Episch";
                btnEffAwesome.Text = "Toll";

                btnFolLow.Text = "Niedrig";
                btnFolMedium.Text = "Mittel";
                btnFolHigh.Text = "Hoch";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Episch";
                btnFolAwesome.Text = "Toll";

                btnShLow.Text = "Niedrig";
                btnShMedium.Text = "Mittel";
                btnShHigh.Text = "Hoch";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Episch";
                btnShAwesome.Text = "Toll";

                btnAnimLow.Text = "Niedrig";
                btnAnimMedium.Text = "Mittel";
                btnAnimHigh.Text = "Hoch";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Episch";
                btnAnimAwesome.Text = "Toll";
                btnAaDisable.Text = "Deaktivieren";
                checkBox2.Text = "Vertikale Synchronisation";
                btnSetFPS.Text = "Satz";
                btnResetFPS.Text = "Zurücksetzen";
                label18.Text = "Legen Sie Ihre eigene FPS-Obergrenze fest";
                label22.Text = "Hauptvolumen";
                label13.Text = "Menü Musik";
                label21.Text = "Audio Qualität";
                checkBox1.Text = "Kopfhörer";

                btnAudioLow.Text = "Niedrig";
                btnAudioMedium.Text = "Mittel";
                btnAudioHigh.Text = "Hoch";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Episch";
                btnAudioAwesome.Text = "Toll";

                label17.Text = "Wählen Sie eine der Grafikvorgaben aus.";
                btnPresetLow.Text = "Sehr niedrig";
                btnPresetMedium.Text = "Mittel";
                btnPresetEpic.Text = "Toll";

                label24.Text = "Mörder (Maus)";
                label25.Text = "Mörder (Controller)";
                label29.Text = "Überlebender (Maus)";
                label27.Text = "Überlebender (Controller)";
            }
            if (Language == "Français")
            {
                errors["confs-dont-exist"] = "Les fichiers de configuration de Dead by Daylight n'existent pas ! Veuillez lancer le jeu.";
                errors["new-version"] = "Une nouvelle version du programme a été trouvée.\n\nVoulez-vous la télécharger ?";
                errors["new-version-fail"] = "La vérification des nouvelles versions du programme a échoué !";
                errors["max-fps"] = "La fréquence d'images maximale dans Dead by Daylight est de 120. Le jeu sera affiché à 120 images par seconde de toute façon. Le programme vous fixera une limite de ";
                errors["max-fps-continuation"] = " FPS quand même.";
                errors["delete-confs"] = "Êtes-vous sûr de vouloir supprimer le fichier de configuration et en créer un nouveau ?";
                errors["delete-confs-title"] = "Supprimer la configuration de sauvegarde";
                errors["discord-link-fail"] = "Un problème est survenu lors de l'ouverture du lien Discord !";
                errors["new-version-title"] = "Vérifier les mises à jour";
                errors["information"] = "Informations";
                errors["error"] = "Erreur";

                errors["tip1"] = "Réinitialisez les paramètres à ceux enregistrés dans la sauvegarde.";
                errors["tip2"] = "Réinitialiser le plafond FPS.";
                errors["tip3"] = "Définissez votre propre plafond FPS.";
                errors["tip4"] = "Définissez votre propre résolution de jeu.";
                errors["tip5"] = "Réinitialiser à la résolution du système.";
                errors["tip6"] = "Supprimez le fichier de configuration de sauvegarde et créez-en un nouveau.";
                errors["tip7"] = "Serveur Discord officiel.";

                checkBox3.Text = "Supprimer les barres noires";
                lblInAppLang.Text = "Langue:";
                lblChangeSettings.Text = "Version du jeu:";
                label12.Text = "Audio";
                label2.Text = "Graphiques";
                label14.Text = "FPS";
                label16.Text = "Préréglages";
                label15.Text = "Sensibilité";
                label19.Text = "Écran";
                lblWidth.Text = "Largeur";
                lblHeight.Text = "Hauteur";
                label35.Text = "Résolution 2D";
                btnResSet.Text = "Définir";
                btnResReset.Text = "Réinitialiser";
                label1.Text = "Résolution 3D";
                label3.Text = "Afficher la qualité de la distance";
                lblAaQuality.Text = "Qualité anticrénelage";
                label5.Text = "Qualité de l'ombre";
                label6.Text = "Qualité post-traitement";
                label7.Text = "Qualité des textures";
                label8.Text = "Qualité des effets";
                label9.Text = "Qualité du feuillage";
                label10.Text = "Qualité de l'ombrage";
                label11.Text = "Qualité des animations";

                btnVwLow.Text = "Bas";
                btnVwMedium.Text = "Moyen";
                btnVwHigh.Text = "Élevé";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epique";
                btnVwAwesome.Text = "Génial";

                btnAaLow.Text = "Bas";
                btnAaMedium.Text = "Moyen";
                btnAaHigh.Text = "Élevé";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epique";
                btnAaAwesome.Text = "Génial";

                btnShadLow.Text = "Bas";
                btnShadMedium.Text = "Moyen";
                btnShadHigh.Text = "Élevé";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epique";
                btnShadAwesome.Text = "Génial";

                btnPpLow.Text = "Bas";
                btnPpMedium.Text = "Moyen";
                btnPpHigh.Text = "Élevé";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epique";
                btnPpAwesome.Text = "Génial";

                btnTxtLow.Text = "Bas";
                btnTxtMedium.Text = "Moyen";
                btnTxtHigh.Text = "Élevé";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epique";
                btnTxtAwesome.Text = "Génial";

                btnEffLow.Text = "Bas";
                btnEffMedium.Text = "Moyen";
                btnEffHigh.Text = "Élevé";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epique";
                btnEffAwesome.Text = "Génial";

                btnFolLow.Text = "Bas";
                btnFolMedium.Text = "Moyen";
                btnFolHigh.Text = "Élevé";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epique";
                btnFolAwesome.Text = "Génial";

                btnShLow.Text = "Bas";
                btnShMedium.Text = "Moyen";
                btnShHigh.Text = "Élevé";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epique";
                btnShAwesome.Text = "Génial";

                btnAnimLow.Text = "Bas";
                btnAnimMedium.Text = "Moyen";
                btnAnimHigh.Text = "Élevé";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epique";
                btnAnimAwesome.Text = "Génial";
                btnAaDisable.Text = "Désactiver";
                checkBox2.Text = "Synchronisation verticale";
                btnSetFPS.Text = "Définir";
                btnResetFPS.Text = "Réinitialiser";
                label18.Text = "Définissez votre propre plafond FPS";
                label22.Text = "Volume principal";
                label13.Text = "Menu Musique";
                label21.Text = "Qualité audio";
                checkBox1.Text = "Écouteurs";

                btnAudioLow.Text = "Bas";
                btnAudioMedium.Text = "Moyen";
                btnAudioHigh.Text = "Élevé";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epique";
                btnAudioAwesome.Text = "Génial";

                label17.Text = "Choisissez l'un des préréglages graphiques.";
                btnPresetLow.Text = "Super bas";
                btnPresetMedium.Text = "Moyen";
                btnPresetEpic.Text = "Génial";

                label24.Text = "Tueur (souris)";
                label25.Text = "Tueur (contrôleur)";
                label29.Text = "Survivant (souris)";
                label27.Text = "Survivant (contrôleur)";
            }
            if (Language == "日本")
            {
                errors["confs-dont-exist"] = "Dead by Daylightの設定ファイルが存在しません！ゲームを起動してください。";
                errors["new-version"] = "プログラムの新しいバージョンが見つかりました\n\nダウンロードしますか？";
                errors["new-version-fail"] = "新しいプログラムバージョンのチェックに失敗しました！";
                errors["max-fps"] = "Dead by Daylightの最大フレームレートは120です。ゲームはとにかく毎秒120フレームで表示されます。プログラムは の制限を設定します ";
                errors["max-fps-continuation"] = " とにかくFPS";
                errors["delete-confs"] = "構成ファイルを削除して、新しいファイルを作成してもよろしいですか？";
                errors["delete-confs-title"] = "バックアップ構成の削除";
                errors["discord-link-fail"] = "Discordリンクを開くときに問題が発生しました！";
                errors["new-version-title"] = "更新を確認してください";
                errors["information"] = "情報";
                errors["error"] = "エラー";

                errors["tip1"] = "バックアップに保存されている設定にリセットします。";
                errors["tip2"] = "FPSキャップをリセットします。";
                errors["tip3"] = "独自のFPSキャップを設定します。";
                errors["tip4"] = "独自のゲーム解像度を設定してください。";
                errors["tip5"] = "システム解像度にリセットします。";
                errors["tip6"] = "バックアップ構成ファイルを削除し、新しいファイルを作成します。";
                errors["tip7"] = "公式Discordサーバー。";

                checkBox3.Text = "黒いバーを削除します";
                lblInAppLang.Text = "アプリ内言語：";
                lblChangeSettings.Text = "ゲームバージョン：";
                label12.Text = "オーディオ";
                label2.Text = "グラフィックス";
                label14.Text = "FPSキャップ";
                label16.Text = "グラフィックプリセット";
                label15.Text = "マウスの感度";
                label19.Text = "画面";
                lblWidth.Text = "幅";
                lblHeight.Text = "高さ";
                label35.Text = "2D解像度";
                btnResSet.Text = "設定";
                btnResReset.Text = "リセット";
                label1.Text = "3D解像度";
                label3.Text = "距離の品質を表示";
                lblAaQuality.Text = "アンチエイリアシング品質";
                label5.Text = "シャドウ品質";
                label6.Text = "後処理品質";
                label7.Text = "テクスチャ品質";
                label8.Text = "効果の品質";
                label9.Text = "葉の品質";
                label10.Text = "シェーディング品質";
                label11.Text = "アニメーションの品質";

                btnVwLow.Text = "低";
                btnVwMedium.Text = "中";
                btnVwHigh.Text = "高";
                btnVwUltra.Text = "ウルトラ";
                btnVwEpic.Text = "大作";
                btnVwAwesome.Text = "素晴らしい";

                btnAaLow.Text = "低";
                btnAaMedium.Text = "中";
                btnAaHigh.Text = "高";
                btnAaUltra.Text = "ウルトラ";
                btnAaEpic.Text = "大作";
                btnAaAwesome.Text = "素晴らしい";

                btnShadLow.Text = "低";
                btnShadMedium.Text = "中";
                btnShadHigh.Text = "高";
                btnShadUltra.Text = "ウルトラ";
                btnShadEpic.Text = "大作";
                btnShadAwesome.Text = "素晴らしい";

                btnPpLow.Text = "低";
                btnPpMedium.Text = "中";
                btnPpHigh.Text = "高";
                btnPpUltra.Text = "ウルトラ";
                btnPpEpic.Text = "大作";
                btnPpAwesome.Text = "素晴らしい";

                btnTxtLow.Text = "低";
                btnTxtMedium.Text = "中";
                btnTxtHigh.Text = "高";
                btnTxtUltra.Text = "ウルトラ";
                btnTxtEpic.Text = "大作";
                btnTxtAwesome.Text = "素晴らしい";

                btnEffLow.Text = "低";
                btnEffMedium.Text = "中";
                btnEffHigh.Text = "高";
                btnEffUltra.Text = "ウルトラ";
                btnEffEpic.Text = "大作";
                btnEffAwesome.Text = "素晴らしい";

                btnFolLow.Text = "低";
                btnFolMedium.Text = "中";
                btnFolHigh.Text = "高";
                btnFolUltra.Text = "ウルトラ";
                btnFolEpic.Text = "大作";
                btnFolAwesome.Text = "素晴らしい";

                btnShLow.Text = "低";
                btnShMedium.Text = "中";
                btnShHigh.Text = "高";
                btnShUltra.Text = "ウルトラ";
                btnShEpic.Text = "大作";
                btnShAwesome.Text = "素晴らしい";

                btnAnimLow.Text = "低";
                btnAnimMedium.Text = "中";
                btnAnimHigh.Text = "高";
                btnAnimUltra.Text = "ウルトラ";
                btnAnimEpic.Text = "大作";
                btnAnimAwesome.Text = "素晴らしい";
                btnAaDisable.Text = "無効";
                checkBox2.Text = "垂直同期";
                btnSetFPS.Text = "設定";
                btnResetFPS.Text = "リセット";
                label18.Text = "独自のFPSキャップを設定";
                label22.Text = "メインボリューム";
                label13.Text = "メニュー音楽";
                label21.Text = "オーディオ品質";
                checkBox1.Text = "ヘッドフォン";

                btnAudioLow.Text = "低";
                btnAudioMedium.Text = "中";
                btnAudioHigh.Text = "高";
                btnAudioUltra.Text = "ウルトラ";
                btnAudioEpic.Text = "大作";
                btnAudioAwesome.Text = "素晴らしい";

                label17.Text = "グラフィックプリセットの1つを選択してください。";
                btnPresetLow.Text = "超低";
                btnPresetMedium.Text = "中";
                btnPresetEpic.Text = "素晴らしい";

                label24.Text = "キラー（マウス）";
                label25.Text = "キラー（コントローラー）";
                label29.Text = "サバイバー（マウス）";
                label27.Text = "サバイバー（コントローラー）";
            }
            if (Language == "中国人")
            {
                errors["confs-dont-exist"] = "Dead by Daylight 配置文件不存在！请启动游戏。";
                errors["new-version"] = "已找到该程序的新版本。\n\n您要下载吗？";
                errors["new-version-fail"] = "检查较新的程序版本失败！";
                errors["max-fps"] = "Dead by Daylight 中的最大帧速率为 120。游戏将以每秒 120 帧的速度显示。程序将为您设置一个限制 ";
                errors["max-fps-continuation"] = " FPS 无论如何。";
                errors["delete-confs"] = "您确定要删除配置文件并新建一个吗？";
                errors["delete-confs-title"] = "删除备份配置";
                errors["discord-link-fail"] = "打开 Discord 链接时出现问题！";
                errors["new-version-title"] = "检查更新";
                errors["information"] = "信息";
                errors["error"] = "错误";

                errors["tip1"] = "将设置重置为备份中保存的设置。";
                errors["tip2"] = "重置 FPS 上限。";
                errors["tip3"] = "设置你自己的 FPS 上限。";
                errors["tip4"] = "设置你自己的游戏分辨率。";
                errors["tip5"] = "重置为系统分辨率。";
                errors["tip6"] = "删除备份配置文件并新建一个。";
                errors["tip7"] = "官方 Discord 服务器。";

                checkBox3.Text = "去除黑条";
                lblInAppLang.Text = "应用内语言：";
                lblChangeSettings.Text = "游戏版本：";
                label12.Text = "音频";
                label2.Text = "图形";
                label14.Text = "帧数限制";
                label16.Text = "图形预设";
                label15.Text = "鼠标灵敏度";
                label19.Text = "屏幕";
                lblWidth.Text = "宽度";
                lblHeight.Text = "高度";
                label35.Text = "二维分辨率";
                btnResSet.Text = "设置";
                btnResReset.Text = "重置";
                label1.Text = "3D 分辨率";
                label3.Text = "查看距离质量";
                lblAaQuality.Text = "抗锯齿质量";
                label5.Text = "阴影质量";
                label6.Text = "后处理质量";
                label7.Text = "纹理质量";
                label8.Text = "影响质量";
                label9.Text = "树叶质量";
                label10.Text = "着色质量";
                label11.Text = "动画质量";

                btnVwLow.Text = "低";
                btnVwMedium.Text = "中等";
                btnVwHigh.Text = "高";
                btnVwUltra.Text = "超";
                btnVwEpic.Text = "史诗";
                btnVwAwesome.Text = "真棒";

                btnAaLow.Text = "低";
                btnAaMedium.Text = "中等";
                btnAaHigh.Text = "高";
                btnAaUltra.Text = "超";
                btnAaEpic.Text = "史诗";
                btnAaAwesome.Text = "真棒";

                btnShadLow.Text = "低";
                btnShadMedium.Text = "中等";
                btnShadHigh.Text = "高";
                btnShadUltra.Text = "超";
                btnShadEpic.Text = "史诗";
                btnShadAwesome.Text = "真棒";

                btnPpLow.Text = "低";
                btnPpMedium.Text = "中等";
                btnPpHigh.Text = "高";
                btnPpUltra.Text = "超";
                btnPpEpic.Text = "史诗";
                btnPpAwesome.Text = "真棒";

                btnTxtLow.Text = "低";
                btnTxtMedium.Text = "中等";
                btnTxtHigh.Text = "高";
                btnTxtUltra.Text = "超";
                btnTxtEpic.Text = "史诗";
                btnTxtAwesome.Text = "真棒";

                btnEffLow.Text = "低";
                btnEffMedium.Text = "中等";
                btnEffHigh.Text = "高";
                btnEffUltra.Text = "超";
                btnEffEpic.Text = "史诗";
                btnEffAwesome.Text = "真棒";

                btnFolLow.Text = "低";
                btnFolMedium.Text = "中等";
                btnFolHigh.Text = "高";
                btnFolUltra.Text = "超";
                btnFolEpic.Text = "史诗";
                btnFolAwesome.Text = "真棒";

                btnShLow.Text = "低";
                btnShMedium.Text = "中等";
                btnShHigh.Text = "高";
                btnShUltra.Text = "超";
                btnShEpic.Text = "史诗";
                btnShAwesome.Text = "真棒";

                btnAnimLow.Text = "低";
                btnAnimMedium.Text = "中等";
                btnAnimHigh.Text = "高";
                btnAnimUltra.Text = "超";
                btnAnimEpic.Text = "史诗";
                btnAnimAwesome.Text = "真棒";
                btnAaDisable.Text = "禁用";
                checkBox2.Text = "垂直同步";
                btnSetFPS.Text = "设置";
                btnResetFPS.Text = "重置";
                label18.Text = "设置你自己的 FPS 上限";
                label22.Text = "主卷";
                label13.Text = "菜单音乐";
                label21.Text = "音频质量";
                checkBox1.Text = "耳机";

                btnAudioLow.Text = "低";
                btnAudioMedium.Text = "中等";
                btnAudioHigh.Text = "高";
                btnAudioUltra.Text = "超";
                btnAudioEpic.Text = "史诗";
                btnAudioAwesome.Text = "真棒";

                label17.Text = "选择图形预设之一。";
                btnPresetLow.Text = "超低";
                btnPresetMedium.Text = "中";
                btnPresetEpic.Text = "真棒";

                label24.Text = "杀手（鼠标）";
                label25.Text = "杀手（控制器）";
                label29.Text = "幸存者（鼠标）";
                label27.Text = "幸存者（控制器）";
            }
            if (Language == "Türkçe")
            {
                errors["confs-dont-exist"] = "Dead by Daylight yapılandırma dosyaları mevcut değil! Lütfen oyunu başlatın.";
                errors["new-version"] = "Programın yeni bir sürümü bulundu.\n\nİndirmek istiyor musunuz?";
                errors["new-version-fail"] = "Daha yeni program sürümleri kontrol edilemedi!";
                errors["max-fps"] = "Dead by Daylight'ta maksimum kare hızı 120'dir. Oyun zaten 120 kare/saniye olarak gösterilecektir. Program size bir limit belirleyecektir ";
                errors["max-fps-continuation"] = " FPS yine de.";
                errors["delete-confs"] = "Yapılandırma dosyasını silip yeni bir tane oluşturmak istediğinizden emin misiniz?";
                errors["delete-confs-title"] = "Yedekleme yapılandırmasını sil";
                errors["discord-link-fail"] = "Discord bağlantısı açılırken bir sorun oluştu!";
                errors["new-version-title"] = "Güncellemeleri kontrol et";
                errors["information"] = "Bilgi";
                errors["error"] = "Hata";

                errors["tip1"] = "Ayarları yedekte kayıtlı olanlara sıfırlayın.";
                errors["tip2"] = "FPS sınırını sıfırla.";
                errors["tip3"] = "Kendi FPS sınırınızı belirleyin.";
                errors["tip4"] = "Kendi oyun çözünürlüğünüzü ayarlayın.";
                errors["tip5"] = "Sistem çözünürlüğüne sıfırla.";
                errors["tip6"] = "Yedek yapılandırma dosyasını silin ve yeni bir tane oluşturun.";
                errors["tip7"] = "Resmi Discord Sunucusu.";

                checkBox3.Text = "Siyah çubukları kaldır";
                lblInAppLang.Text = "Uygulama içi dil:";
                lblChangeSettings.Text = "Oyun sürümü:";
                label12.Text = "Ses";
                label2.Text = "Grafikler";
                label14.Text = "FPS Sınırı";
                label16.Text = "Grafik ön ayarları";
                label15.Text = "Fare duyarlılığı";
                label19.Text = "Ekran";
                lblWidth.Text = "Genişlik";
                lblHeight.Text = "Yükseklik";
                label35.Text = "2D Çözünürlük";
                btnResSet.Text = "Ayarla";
                btnResReset.Text = "Sıfırla";
                label1.Text = "3D Çözünürlük";
                label3.Text = "Mesafe Kalitesini Görüntüle";
                lblAaQuality.Text = "Örtüşme Önleme Kalitesi";
                label5.Text = "Gölge Kalitesi";
                label6.Text = "İşlem Sonrası Kalite";
                label7.Text = "Doku Kalitesi";
                label8.Text = "Etki Kalitesi";
                label9.Text = "Yaprak Kalitesi";
                label10.Text = "Gölgeleme Kalitesi";
                label11.Text = "Animasyon Kalitesi";

                btnVwLow.Text = "Düşük";
                btnVwMedium.Text = "Orta";
                btnVwHigh.Text = "Yüksek";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epik";
                btnVwAwesome.Text = "Harika";

                btnAaLow.Text = "Düşük";
                btnAaMedium.Text = "Orta";
                btnAaHigh.Text = "Yüksek";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epik";
                btnAaAwesome.Text = "Harika";

                btnShadLow.Text = "Düşük";
                btnShadMedium.Text = "Orta";
                btnShadHigh.Text = "Yüksek";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epik";
                btnShadAwesome.Text = "Harika";

                btnPpLow.Text = "Düşük";
                btnPpMedium.Text = "Orta";
                btnPpHigh.Text = "Yüksek";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epik";
                btnPpAwesome.Text = "Harika";

                btnTxtLow.Text = "Düşük";
                btnTxtMedium.Text = "Orta";
                btnTxtHigh.Text = "Yüksek";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epik";
                btnTxtAwesome.Text = "Harika";

                btnEffLow.Text = "Düşük";
                btnEffMedium.Text = "Orta";
                btnEffHigh.Text = "Yüksek";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epik";
                btnEffAwesome.Text = "Harika";

                btnFolLow.Text = "Düşük";
                btnFolMedium.Text = "Orta";
                btnFolHigh.Text = "Yüksek";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epik";
                btnFolAwesome.Text = "Harika";

                btnShLow.Text = "Düşük";
                btnShMedium.Text = "Orta";
                btnShHigh.Text = "Yüksek";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epik";
                btnShAwesome.Text = "Harika";

                btnAnimLow.Text = "Düşük";
                btnAnimMedium.Text = "Orta";
                btnAnimHigh.Text = "Yüksek";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epik";
                btnAnimAwesome.Text = "Harika";
                btnAaDisable.Text = "Devre Dışı Bırak";
                checkBox2.Text = "Dikey senkronizasyon";
                btnSetFPS.Text = "Ayarla";
                btnResetFPS.Text = "Sıfırla";
                label18.Text = "Kendi FPS sınırınızı belirleyin";
                label22.Text = "Ana Cilt";
                label13.Text = "Menü Müziği";
                label21.Text = "Ses Kalitesi";
                checkBox1.Text = "Kulaklıklar";

                btnAudioLow.Text = "Düşük";
                btnAudioMedium.Text = "Orta";
                btnAudioHigh.Text = "Yüksek";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epik";
                btnAudioAwesome.Text = "Harika";

                label17.Text = "Grafik ön ayarlarından birini seçin.";
                btnPresetLow.Text = "Süper düşük";
                btnPresetMedium.Text = "Orta";
                btnPresetEpic.Text = "Harika";

                label24.Text = "Katil (fare)";
                label25.Text = "Killer (denetleyici)";
                label29.Text = "Hayatta kalan (fare)";
                label27.Text = "Hayatta kalan (kontrolör)";
            }
            if (Language == "Español")
            {
                errors["confs-dont-exist"] = "¡Los archivos de configuración de Dead by Daylight no existen! Por favor, inicia el juego.";
                errors["new-version"] = "Se ha encontrado una nueva versión del programa.\n\n¿Desea descargarla?";
                errors["new-version-fail"] = "¡Error al comprobar las versiones más recientes del programa!";
                errors["max-fps"] = "La velocidad de fotogramas máxima en Dead by Daylight es 120. El juego se mostrará a 120 fotogramas por segundo de todos modos. El programa establecerá un límite de ";
                errors["max-fps-continuation"] = " FPS de todos modos.";
                errors["delete-confs"] = "¿Está seguro de que desea eliminar el archivo de configuración y crear uno nuevo?";
                errors["delete-confs-title"] = "Eliminar configuración de copia de seguridad";
                errors["discord-link-fail"] = "¡Hubo un problema al abrir el enlace de Discord!";
                errors["new-version-title"] = "Buscar actualizaciones";
                errors["information"] = "Información";
                errors["error"] = "Error";

                errors["tip1"] = "Restablecer la configuración a la guardada en la copia de seguridad.";
                errors["tip2"] = "Restablecer límite de FPS.";
                errors["tip3"] = "Establece tu propio límite de FPS.";
                errors["tip4"] = "Establece tu propia resolución de juego.";
                errors["tip5"] = "Restablecer a la resolución del sistema.";
                errors["tip6"] = "Elimine el archivo de configuración de la copia de seguridad y cree uno nuevo.";
                errors["tip7"] = "Servidor oficial de Discord.";

                checkBox3.Text = "Eliminar barras negras";
                lblInAppLang.Text = "Idioma:";
                lblChangeSettings.Text = "Versión del juego:";
                label12.Text = "Audio";
                label2.Text = "Gráficos";
                label14.Text = "FPS";
                label16.Text = "Presets gráficos";
                label15.Text = "Sensibilidad";
                label19.Text = "Pantalla";
                lblWidth.Text = "Ancho";
                lblHeight.Text = "Altura";
                label35.Text = "Resolución 2D";
                btnResSet.Text = "Establecer";
                btnResReset.Text = "Reiniciar";
                label1.Text = "Resolución 3D";
                label3.Text = "Ver calidad de distancia";
                lblAaQuality.Text = "Calidad de suavizado";
                label5.Text = "Calidad de la sombra";
                label6.Text = "Calidad de posprocesamiento";
                label7.Text = "Calidad de las texturas";
                label8.Text = "Calidad de los efectos";
                label9.Text = "Calidad del follaje";
                label10.Text = "Calidad de sombreado";
                label11.Text = "Calidad de las animaciones";

                btnVwLow.Text = "Bajo";
                btnVwMedium.Text = "Medio";
                btnVwHigh.Text = "Alto";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Épico";
                btnVwAwesome.Text = "Impresionante";

                btnAaLow.Text = "Bajo";
                btnAaMedium.Text = "Medio";
                btnAaHigh.Text = "Alto";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Épico";
                btnAaAwesome.Text = "Impresionante";

                btnShadLow.Text = "Bajo";
                btnShadMedium.Text = "Medio";
                btnShadHigh.Text = "Alto";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Épico";
                btnShadAwesome.Text = "Impresionante";

                btnPpLow.Text = "Bajo";
                btnPpMedium.Text = "Medio";
                btnPpHigh.Text = "Alto";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Épico";
                btnPpAwesome.Text = "Impresionante";

                btnTxtLow.Text = "Bajo";
                btnTxtMedium.Text = "Medio";
                btnTxtHigh.Text = "Alto";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Épico";
                btnTxtAwesome.Text = "Impresionante";

                btnEffLow.Text = "Bajo";
                btnEffMedium.Text = "Medio";
                btnEffHigh.Text = "Alto";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Épico";
                btnEffAwesome.Text = "Impresionante";

                btnFolLow.Text = "Bajo";
                btnFolMedium.Text = "Medio";
                btnFolHigh.Text = "Alto";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Épico";
                btnFolAwesome.Text = "Impresionante";

                btnShLow.Text = "Bajo";
                btnShMedium.Text = "Medio";
                btnShHigh.Text = "Alto";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Épico";
                btnShAwesome.Text = "Impresionante";

                btnAnimLow.Text = "Bajo";
                btnAnimMedium.Text = "Medio";
                btnAnimHigh.Text = "Alto";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Épico";
                btnAnimAwesome.Text = "Impresionante";
                btnAaDisable.Text = "Deshabilitar";
                checkBox2.Text = "Sincronización vertical";
                btnSetFPS.Text = "Establecer";
                btnResetFPS.Text = "Reiniciar";
                label18.Text = "Establece tu propio límite de FPS";
                label22.Text = "Volumen principal";
                label13.Text = "Menú Música";
                label21.Text = "Calidad de audio";
                checkBox1.Text = "Auriculares";

                btnAudioLow.Text = "Bajo";
                btnAudioMedium.Text = "Medio";
                btnAudioHigh.Text = "Alto";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Épico";
                btnAudioAwesome.Text = "Impresionante";

                label17.Text = "Elija uno de los ajustes preestablecidos gráficos.";
                btnPresetLow.Text = "Muy bajo";
                btnPresetMedium.Text = "Medio";
                btnPresetEpic.Text = "Impresionante";

                label24.Text = "Asesino (ratón)";
                label25.Text = "Asesino (controlador)";
                label29.Text = "Superviviente (ratón)";
                label27.Text = "Superviviente (controlador)";
            }
            if (Language == "Italiano")
            {
                errors["confs-dont-exist"] = "I file di configurazione di Dead by Daylight non esistono! Per favore, avvia il gioco.";
                errors["new-version"] = "È stata trovata una nuova versione del programma.\n\nVuoi scaricarla?";
                errors["new-version-fail"] = "Controllo delle versioni più recenti del programma fallito!";
                errors["max-fps"] = "Il frame rate massimo in Dead by Daylight è 120. Il gioco verrà comunque visualizzato a 120 frame al secondo. Il programma ti imposterà un limite di ";
                errors["max-fps-continuation"] = " FPS comunque.";
                errors["delete-confs"] = "Sei sicuro di voler eliminare il file di configurazione e crearne uno nuovo?";
                errors["delete-confs-title"] = "Elimina configurazione di backup";
                errors["discord-link-fail"] = "Si è verificato un problema durante l'apertura del collegamento Discord!";
                errors["new-version-title"] = "Verifica aggiornamenti";
                errors["information"] = "Informazioni";
                errors["error"] = "Errore";

                errors["tip1"] = "Ripristina le impostazioni su quelle salvate nel backup.";
                errors["tip2"] = "Reimposta limite FPS.";
                errors["tip3"] = "Imposta il tuo limite FPS.";
                errors["tip4"] = "Imposta la tua risoluzione di gioco.";
                errors["tip5"] = "Ripristina risoluzione di sistema.";
                errors["tip6"] = "Elimina il file di configurazione del backup e creane uno nuovo.";
                errors["tip7"] = "Server Discord ufficiale.";

                checkBox3.Text = "Rimuovere le barre nere";
                lblInAppLang.Text = "Lingua nell'app:";
                lblChangeSettings.Text = "Versione del gioco:";
                label12.Text = "Audio";
                label2.Text = "Grafica";
                label14.Text = "Tappo FPS";
                label16.Text = "Preimpostazioni grafiche";
                label15.Text = "Sensibilità del mouse";
                label19.Text = "Schermo";
                lblWidth.Text = "Larghezza";
                lblHeight.Text = "Altezza";
                label35.Text = "Risoluzione 2D";
                btnResSet.Text = "Imposta";
                btnResReset.Text = "Ripristina";
                label1.Text = "Risoluzione 3D";
                label3.Text = "Visualizza la qualità della distanza";
                lblAaQuality.Text = "Qualità anti-aliasing";
                label5.Text = "Qualità ombra";
                label6.Text = "Qualità post-elaborazione";
                label7.Text = "Qualità delle trame";
                label8.Text = "Qualità degli effetti";
                label9.Text = "Qualità fogliame";
                label10.Text = "Qualità dell'ombreggiatura";
                label11.Text = "Qualità animazioni";

                btnVwLow.Text = "Basso";
                btnVwMedium.Text = "Medio";
                btnVwHigh.Text = "Alto";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epico";
                btnVwAwesome.Text = "Fantastico";

                btnAaLow.Text = "Basso";
                btnAaMedium.Text = "Medio";
                btnAaHigh.Text = "Alto";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epico";
                btnAaAwesome.Text = "Fantastico";

                btnShadLow.Text = "Basso";
                btnShadMedium.Text = "Medio";
                btnShadHigh.Text = "Alto";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epico";
                btnShadAwesome.Text = "Fantastico";

                btnPpLow.Text = "Basso";
                btnPpMedium.Text = "Medio";
                btnPpHigh.Text = "Alto";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epico";
                btnPpAwesome.Text = "Fantastico";

                btnTxtLow.Text = "Basso";
                btnTxtMedium.Text = "Medio";
                btnTxtHigh.Text = "Alto";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epico";
                btnTxtAwesome.Text = "Fantastico";

                btnEffLow.Text = "Basso";
                btnEffMedium.Text = "Medio";
                btnEffHigh.Text = "Alto";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epico";
                btnEffAwesome.Text = "Fantastico";

                btnFolLow.Text = "Basso";
                btnFolMedium.Text = "Medio";
                btnFolHigh.Text = "Alto";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epico";
                btnFolAwesome.Text = "Fantastico";

                btnShLow.Text = "Basso";
                btnShMedium.Text = "Medio";
                btnShHigh.Text = "Alto";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epico";
                btnShAwesome.Text = "Fantastico";

                btnAnimLow.Text = "Basso";
                btnAnimMedium.Text = "Medio";
                btnAnimHigh.Text = "Alto";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epico";
                btnAnimAwesome.Text = "Fantastico";
                btnAaDisable.Text = "Disabilita";
                checkBox2.Text = "Sincronizzazione verticale";
                btnSetFPS.Text = "Imposta";
                btnResetFPS.Text = "Ripristina";
                label18.Text = "Imposta il tuo limite FPS";
                label22.Text = "Volume principale";
                label13.Text = "Menu Musica";
                label21.Text = "Qualità audio";
                checkBox1.Text = "Cuffie";

                btnAudioLow.Text = "Basso";
                btnAudioMedium.Text = "Medio";
                btnAudioHigh.Text = "Alto";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epico";
                btnAudioAwesome.Text = "Fantastico";

                label17.Text = "Scegli uno dei predefiniti grafici.";
                btnPresetLow.Text = "Super basso";
                btnPresetMedium.Text = "Medio";
                btnPresetEpic.Text = "Fantastico";

                label24.Text = "Killer (mouse)";
                label25.Text = "Killer (controllore)";
                label29.Text = "Sopravvissuto (mouse)";
                label27.Text = "Sopravvissuto (controllore)";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            var changeHUDRatio = new SettingsTrueOrFalse(SettingsPath, "HUDConstrainedAspectRatio=", !checkBox3.Checked);
            if(checkBox3.Checked)
            {
                var remove = new RemoveBlackBars(EnginePath);
            }
            else
            {
                var reset = new ResetBlackBars(EnginePath);
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
                        
                        File.WriteAllText(path, read);
                    }
                    else
                    {
                        read = read.Replace(lines[i], option + valueInt + ".000000");
                        
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
                    if (lines[i].Contains("ResolutionSizeX=") && !lines[i].Contains("LastUser"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeX=" + WidthValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("ResolutionSizeY=") && !lines[i].Contains("LastUser"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeY=" + HeightValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastUserConfirmedResolutionSizeX="))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeX=" + WidthValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastUserConfirmedResolutionSizeY="))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeY=" + HeightValue);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("FullscreenMode=") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Prefered"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "FullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastConfirmedFullscreenMode=") && !lines[i].Contains("Prefered"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastConfirmedFullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("PreferredFullscreenMode=") && !lines[i].Contains("LastConfirmed"))
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

                if (lines[i].Contains("ResolutionSizeX=") && !lines[i].Contains("LastUser"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeX=" + screenWidth);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("ResolutionSizeY=") && !lines[i].Contains("LastUser"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "ResolutionSizeY=" + screenHeight);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeX="))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeX=" + screenWidth);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeY="))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "LastUserConfirmedResolutionSizeY=" + screenHeight);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("FullscreenMode=") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Preferred"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "FullscreenMode=" + 1);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("LastConfirmedFullscreenMode=") && !lines[i].Contains("Preferred"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "LastConfirmedFullscreenMode=" + 1);
                    File.WriteAllText(path, ReadConfig);
                }
                if (lines[i].Contains("PreferredFullscreenMode=") && !lines[i].Contains("LastConfirmed"))
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
            
            File.WriteAllText(EnginePath, ReadEngine);
        }
    }

    public class RemoveBlackBars
    {
        public RemoveBlackBars(string EnginePath)
        {
            string ReadEngine = File.ReadAllText(EnginePath);
            ReadEngine += "\n\n\n[/Script/Engine.LocalPlayer]\nAspectRatioAxisConstraint=AspectRatio_MAX";

            File.WriteAllText(EnginePath, ReadEngine);
        }
    }

    public class ResetBlackBars
    {
        public ResetBlackBars(string path)
        {
            string ReadEngine = File.ReadAllText(path);
            int numLines2 = File.ReadLines(path).Count();
            string[] eng = File.ReadAllLines(path);

            for (int a = 0; a < numLines2; a++)
            {
                if (eng[a].Contains("[/Script/Engine.LocalPlayer]"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].Contains("AspectRatioAxisConstraint=AspectRatio_MAX"))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
            }
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

    public class ChangeResolution
    {
        public ChangeResolution(int resolution, string SettingsPath)
        {
            string ReadConfig = File.ReadAllText(SettingsPath);

            int numLines = File.ReadLines(SettingsPath).Count();
            string[] lines = File.ReadAllLines(SettingsPath);

            for (int i = 0; i < numLines; i++)
            {
                if (lines[i].Contains("ResolutionQuality="))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ResolutionQuality=" + resolution + ".000000");
                    File.WriteAllText(SettingsPath, ReadConfig);
                }
            }
        }
    }
}
