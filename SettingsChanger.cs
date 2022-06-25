using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Reflection;

namespace DbD_Settings_Changer
{
    public partial class Form1 : Form
    {

        public ChangeSettings EditConfig = new ChangeSettings();

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

        private void CreateLanguageConfig()
        {
            if (!Directory.Exists(ApplicationLanguageData))
            {
                Directory.CreateDirectory(ApplicationLanguageData);
            }
            if (!File.Exists(ApplicationLanguageData + @"lang.ini"))
            {
                File.WriteAllText(ApplicationLanguageData + @"lang.ini", "en");
            }
        }

        private void DefaultTab()
        {
            this.Size = new Size(1145, 1022);
            menuGraphics.BackColor = Color.Crimson;
            menuGraphics.Location = new Point(menuGraphics.Location.X, 2);
            menuGraphics.Size = new Size(menuGraphics.Size.Width, 40);

            panelGraphics.Show();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSensitivity.Hide();
            panelScreen.Hide();
        }

        private void ChangeLanguage()
        {
            int numLines = File.ReadLines(ApplicationLanguageData + "lang.ini").Count();
            string[] lang = File.ReadAllLines(ApplicationLanguageData + "lang.ini");
            string[] words = new string[] { "en", "pl", "ru", "du", "fr", "jp", "ch", "tu", "esp", "it" };

            bool found = false;
            string word = "";
            for (int a = 0; a < numLines; a++)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (lang[a] == words[i])
                    {
                        word = words[i];
                        found = true;
                        switch (word)
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

            if (!found)
            {
                word = "English";
                File.WriteAllText(ApplicationLanguageData + "lang.ini", "en");
            }
            cbLanguage.SelectedIndex = cbLanguage.Items.IndexOf(word);
            changeAppLanguage(word);
        }

        private void CheckForUpdates()
        {
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
        }

        private void CheckWhichVersion()
        {
            if (!File.Exists(SteamSettingsPath) && !File.Exists(SteamEnginePath) && !File.Exists(EGSSettingsPath) && !File.Exists(EGSEnginePath) && !File.Exists(MSSettingsPath) && !File.Exists(MSEnginePath))
            {
                MessageBox.Show(errors["confs-dont-exist"], errors["error"],
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


            if (File.Exists(SteamSettingsPath) && File.Exists(SteamEnginePath))
            {
                cbVersion.Items.Add("Steam");
            }
            if (File.Exists(EGSSettingsPath) && File.Exists(EGSEnginePath))
            {
                cbVersion.Items.Add("Epic Games Store");
            }
            if (File.Exists(MSSettingsPath) && File.Exists(MSEnginePath))
            {
                cbVersion.Items.Add("Microsoft Store");
            }
            cbVersion.SelectedIndex = 0;


            if (cbVersion.Items[0].ToString() == "Steam")
            {
                EnginePath = SteamEnginePath;
                SettingsPath = SteamSettingsPath;
            }
            else if (cbVersion.Items[0].ToString() == "Epic Games Store")
            {
                EnginePath = EGSEnginePath;
                SettingsPath = EGSSettingsPath;
            }
            else if (cbVersion.Items[0].ToString() == "Microsoft Store")
            {
                EnginePath = MSEnginePath;
                SettingsPath = MSSettingsPath;
            }
        }

        private void CheckIfCongifsExist()
        {
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
                    readValues(SettingsPath, EnginePath);
                }
            }
            catch (Exception)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckDate(imgPumpkin, imgXmasTree, imgFireworks);

            CreateLanguageConfig();
            DefaultTab();
            ChangeLanguage();
            CheckWhichVersion();
            CheckForUpdates();
            CheckIfCongifsExist();

            lblHideFocus.Focus();

            foreach (Control control in panel10.Controls)
            {
                if (control.Name.Contains("panel"))
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is Button)
                        {
                            (ctrl as Button).Click += new EventHandler(this.btnClick);
                        }
                        if (ctrl is TrackBar)
                        {
                            (ctrl as TrackBar).Scroll += new EventHandler(this.tbScroll);
                        }
                        if (ctrl is CheckBox)
                        {
                            (ctrl as CheckBox).CheckedChanged += new EventHandler(this.cbCheckedChanged);
                        }
                    }
                }
                if (control is Label)
                {
                    if (control.Name.Contains("menu"))
                    {
                        (control as Label).Click += new EventHandler(this.menuClick);
                    }
                }
            }
        }

        private void menuColors(string avoidedName)
        {
            foreach (Control control in panel10.Controls)
            {
                if(control.Name.Contains("panel"))
                {
                    if (!control.Name.Contains(avoidedName) && !control.Name.Contains("Discord") && !control.Name.Contains("User"))
                    {
                        control.Hide();
                    }
                    if (control.Name.Contains(avoidedName) && !control.Name.Contains("Discord") && !control.Name.Contains("User"))
                    {
                        control.Show();
                    }
                }
                if (control is Label)
                {
                    if (!control.Name.Contains(avoidedName) && control.Name.Contains("menu"))
                    {
                        control.BackColor = Color.FromArgb(57, 59, 57);
                        control.Location = new Point(control.Location.X, 4);
                        control.Size = new Size(control.Size.Width, 40);
                    }
                    if (control.Name.Contains(avoidedName) && control.Name.Contains("menu"))
                    {
                        control.BackColor = Color.Crimson;
                        control.Location = new Point(control.Location.X, 2);
                        control.Size = new Size(control.Size.Width, 40);
                    }
                }
            }
        }

        private void menuClick(object sender, EventArgs e)
        {
            if ((sender as Label).Name.Contains("Audio"))
            {
                menuColors((sender as Label).Name.Substring(4, (sender as Label).Name.Length - 4));
                this.Size = new Size(1145, 566);
            }
            if ((sender as Label).Name.Contains("Graphics"))
            {
                menuColors((sender as Label).Name.Substring(4, (sender as Label).Name.Length - 4));
                this.Size = new Size(1145, 1022);
            }
            if ((sender as Label).Name.Contains("FPS"))
            {
                menuColors((sender as Label).Name.Substring(4, (sender as Label).Name.Length - 4));
                this.Size = new Size(1145, 408);
            }
            if ((sender as Label).Name.Contains("Presets"))
            {
                menuColors((sender as Label).Name.Substring(4, (sender as Label).Name.Length - 4));
                this.Size = new Size(1145, 448);
            }
            if ((sender as Label).Name.Contains("Sensitivity"))
            {
                menuColors((sender as Label).Name.Substring(4, (sender as Label).Name.Length - 4));
                this.Size = new Size(1145, 665);
            }
            if ((sender as Label).Name.Contains("Screen"))
            {
                menuColors((sender as Label).Name.Substring(4, (sender as Label).Name.Length - 4));
                this.Size = new Size(1145, 620);
            }
        }

        private int getValue(object sender)
        {
            switch ((sender as Button).Name)
            {
                case string value0 when value0.Contains("Low"): return 0;
                case string value1 when value1.Contains("Medium"): return 1;
                case string value2 when value2.Contains("High"): return 2;
                case string value3 when value3.Contains("Ultra"): return 3;
                case string value4 when value4.Contains("Epic"): return 4;
                case string value5 when value5.Contains("Awesome"): return 5;
            }
            return 0;
        }

        private void changeButtonColor(object sender, string name, string avoidedName)
        {
            foreach (Button btn in (sender as Button).Parent.Controls.OfType<Button>())
            {
                if (!btn.Name.Contains(avoidedName) && btn.Name.Contains(name) && btn != (sender as Button))
                {
                    btn.BackColor = Color.FromArgb(224, 224, 224);
                    btn.ForeColor = Color.Black;
                }
                if (btn == (sender as Button))
                {
                    btn.BackColor = Color.Crimson;
                    btn.ForeColor = Color.White;
                }
            }

        }

        private void cbCheckedChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if ((sender as CheckBox).Name.Contains("SurvivorToggle"))
            {
                EditConfig.GameUserSettings(SettingsPath, "SurvivorToggleInteractions=", cbSurvivorToggle.Checked.ToString());
            }
            if ((sender as CheckBox).Name.Contains("KillerToggle"))
            {
                EditConfig.GameUserSettings(SettingsPath, "KillerToggleInteractions=", cbKillerToggle.Checked.ToString());
            }
            if ((sender as CheckBox).Name.Contains("Vsync"))
            {
                EditConfig.GameUserSettings(SettingsPath, "bUseVSync=", cbVsync.Checked.ToString());
            }
            if ((sender as CheckBox).Name.Contains("Headphones"))
            {
                EditConfig.GameUserSettings(SettingsPath, "UseHeadphones=", cbHeadphones.Checked.ToString());
            }
        }

        private void changeSensitivityOrResolutionOrAudio(TrackBar tb, Label lbl, string line)
        {
            if (tb.Name == "tbRes")
            {
                int value = tb.Value;
                lbl.Text = value.ToString() + "%";
                string stringValue = value.ToString() + ".000000";

                EditConfig.GameUserSettings(SettingsPath, line, stringValue);
            }
            else
            {
                int value = tb.Value;
                lbl.Text = value.ToString() + "%";

                EditConfig.GameUserSettings(SettingsPath, line, value.ToString());
            }
        }

        private void tbScroll(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if ((sender as TrackBar).Name.Contains("Res"))
            {
                changeSensitivityOrResolutionOrAudio(tbRes, lblRes, "sg.ResolutionQuality=");
            }
            if ((sender as TrackBar).Name.Contains("KillerMouse"))
            {
                changeSensitivityOrResolutionOrAudio(tbKillerMouse, lblKillerMouse, "KillerMouseSensitivity=");
            }
            if ((sender as TrackBar).Name.Contains("KillerController"))
            {
                changeSensitivityOrResolutionOrAudio(tbKillerController, lblKillerController, "KillerControllerSensitivity=");
            }
            if ((sender as TrackBar).Name.Contains("SurvMouse"))
            {
                changeSensitivityOrResolutionOrAudio(tbSurvMouse, lblSurvMouse, "SurvivorMouseSensitivity=");
            }
            if ((sender as TrackBar).Name.Contains("SurvController"))
            {
                changeSensitivityOrResolutionOrAudio(tbSurvController, lblSurvController, "SurvivorControllerSensitivity=");
            }
            if ((sender as TrackBar).Name.Contains("ScRes"))
            {
                changeSensitivityOrResolutionOrAudio(tbScRes, lblScRes, "ScreenResolution=");
            }
            if ((sender as TrackBar).Name.Contains("tbVolume"))
            {
                changeSensitivityOrResolutionOrAudio(tbVolume, lblMainVolume, "MainVolume=");
            }
            if ((sender as TrackBar).Name.Contains("tbMusic"))
            {
                changeSensitivityOrResolutionOrAudio(tbMusic, lblMenu, "MenuMusicVolume=");
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if ((sender as Button).Name.Contains("Aa"))
            {
                changeButtonColor(sender, "Aa", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", getValue(sender).ToString());
                if (!(sender as Button).Name.Contains("Disable"))
                {
                    EditConfig.EnableAntiAliasing(EnginePath);
                }
                else
                {
                    EditConfig.EnableAntiAliasing(EnginePath);
                    EditConfig.DisableAntiAliasing(EnginePath);
                    EditConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 0.ToString());
                }
            }
            if ((sender as Button).Name.Contains("Pp"))
            {
                changeButtonColor(sender, "Pp", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Vw"))
            {
                changeButtonColor(sender, "Vw", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Shad"))
            {
                changeButtonColor(sender, "Shad", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Txt"))
            {
                changeButtonColor(sender, "Txt", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Eff"))
            {
                changeButtonColor(sender, "Eff", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Fol"))
            {
                changeButtonColor(sender, "Fol", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", getValue(sender).ToString());
            }
            if (!(sender as Button).Name.Contains("Shad") && (sender as Button).Name.Contains("Sh"))
            {
                changeButtonColor(sender, "Sh", "Shad");
                EditConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Anim"))
            {
                changeButtonColor(sender, "Anim", " ");
                EditConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Audio"))
            {
                changeButtonColor(sender, "Audio", " ");
                EditConfig.GameUserSettings(SettingsPath, "AudioQualityLevel=", getValue(sender).ToString());
            }
            if ((sender as Button).Name.Contains("Preset"))
            {
                if ((sender as Button).Name.Contains("Low"))
                {
                    EditConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", 1.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 0.ToString());
                    EditConfig.EnableAntiAliasing(EnginePath);
                    EditConfig.DisableAntiAliasing(EnginePath);
                    EditConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "ScreenResolution=", 1.ToString());

                    readValues(SettingsPath, EnginePath);
                }
                if ((sender as Button).Name.Contains("Medium"))
                {
                    EditConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", 100.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", 3.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", 3.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", 2.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", 2.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", 0.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", 2.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "ScreenResolution=", 100.ToString());

                    readValues(SettingsPath, EnginePath);
                }
                if ((sender as Button).Name.Contains("Epic"))
                {
                    EditConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", 200.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", 5.ToString());
                    EditConfig.GameUserSettings(SettingsPath, "ScreenResolution=", 200.ToString());

                    readValues(SettingsPath, EnginePath);
                }
            }
            if ((sender as Button).Name.Contains("Res") && !(sender as Button).Name.Contains("FPS"))
            {
                if ((sender as Button).Name.Contains("Set"))
                {
                    EditConfig.SetResolution(SettingsPath, (int)numWidth.Value, (int)numHeight.Value);
                }
                if ((sender as Button).Name.Contains("Reset"))
                {
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                    numWidth.Value = screenWidth;
                    numHeight.Value = screenHeight;
                    lblHideFocus.Focus();

                    EditConfig.ResetResolution(SettingsPath);
                }
            }
            if ((sender as Button).Name.Contains("FPS"))
            {
                if ((sender as Button).Name.Contains("Set"))
                {
                    cbVsync.Checked = false;

                    EditConfig.ResetFPS(EnginePath);
                    if (numFPS.Value == 120)
                    {
                        EditConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                    }
                    if (numFPS.Value > 120)
                    {
                        MessageBox.Show(errors["max-fps"] + (int)numFPS.Value + errors["max-fps-continuation"], errors["information"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EditConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                    }
                    if (numFPS.Value < 120)
                    {
                        EditConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                    }

                    if (numFPS.Value <= 5)
                    {
                        EditConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                        EditConfig.EngineFPS(EnginePath, (int)numFPS.Value);
                    }

                    if (numFPS.Value >= 63 && numFPS.Value >= 120)
                    {
                        EditConfig.EngineFPS(EnginePath, (int)numFPS.Value);
                    }


                }
                if ((sender as Button).Name.Contains("Reset"))
                {
                    cbVsync.Checked = true;
                    numFPS.Value = 62;

                    EditConfig.ResetFPS(EnginePath);
                    EditConfig.SetFPS(SettingsPath, "FrameRateLimit=", 60);
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

                readValues(SettingsPath, EnginePath);
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
            DialogResult result = MessageBox.Show(errors["delete-confs"], errors["delete-confs-title"], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if ((string)cbVersion.SelectedItem == "Steam")
            {
                EnginePath = SteamEnginePath;
                SettingsPath = SteamSettingsPath;
                readValues(SettingsPath, EnginePath);
            }
            if ((string)cbVersion.SelectedItem == "Epic Games Store")
            {
                EnginePath = EGSEnginePath;
                SettingsPath = EGSSettingsPath;
                readValues(SettingsPath, EnginePath);
            }
            if ((string)cbVersion.SelectedItem == "Microsoft Store")
            {
                EnginePath = MSEnginePath;
                SettingsPath = MSSettingsPath;
                readValues(SettingsPath, EnginePath);
            }
        }

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            changeAppLanguage(cbLanguage.SelectedItem.ToString());
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

        private void CheckDate(PictureBox imgPumpkin, PictureBox imgXmasTree, PictureBox imgFireworks)
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
}