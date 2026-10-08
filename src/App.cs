using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DbD_Settings_Changer
{
    public partial class Main : Form
    {
        private static readonly HttpClient httpClient = new();

        [GeneratedRegex(@"\d+")]
        private static partial Regex DigitsRegex();

        private static void CheckDate(PictureBox imgPumpkin, PictureBox imgXmasTree, PictureBox imgFireworks)
        {
            // halloween
            DateTime date = DateTime.Now;
            if (date.Month == 10 && date.Day is >= 16 and <= 31)
            {
                imgPumpkin.Show();
            }
            else
            {
                imgPumpkin.Hide();
            }

            // christmas
            if (date.Month == 12 && date.Day is >= 1 and <= 28)
            {
                imgXmasTree.Show();
            }
            else
            {
                imgXmasTree.Hide();
            }

            // new Year's Eve
            if (date.Month == 12 && date.Day is >= 29 and <= 31)
            {
                imgFireworks.Show();
            }
            else
            {
                imgFireworks.Hide();
            }
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
            menuGraphics.BackColor = Color.Crimson;
            menuGraphics.Location = new Point(menuGraphics.Location.X, 14);
            menuGraphics.Size = new Size(menuGraphics.Size.Width, 40);
            MenuColors("menuGraphics");

            panelGraphics.Show();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSensitivity.Hide();
            panelGame.Hide();
        }

        private void ChangeLanguageInFile()
        {
            string langFile = ApplicationLanguageData + "lang.ini";
            if (!File.Exists(langFile))
            {
                CreateLanguageConfig();
            }

            string[] lang = File.ReadAllLines(langFile);
            string[] words = ["en", "pl", "ru", "de", "fr", "jp", "ch", "tu", "esp", "it"];

            bool found = false;
            string word = "";
            for (int a = 0; a < lang.Length; a++)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (lang[a] == words[i])
                    {
                        word = words[i];
                        found = true;
                        word = word switch
                        {
                            "en" => "English",
                            "pl" => "Polski",
                            "ru" => "русский",
                            "de" => "Deutsch",
                            "fr" => "Français",
                            "jp" => "日本",
                            "ch" => "中国人",
                            "tu" => "Türkçe",
                            "esp" => "Español",
                            "it" => "Italiano",
                            _ => word
                        };
                    }
                }
            }

            if (!found)
            {
                word = "English";
                File.WriteAllText(langFile, "en");
            }
            cbLanguage.SelectedIndex = cbLanguage.Items.IndexOf(word);
            ChangeAppLanguage(word);
        }

        private static void CheckForUpdates()
        {
            try
            {
                string textFromFile = httpClient.GetStringAsync("https://pastebin.com/yjMGKd4u").GetAwaiter().GetResult();
                string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
                if (!textFromFile.Contains(version, StringComparison.Ordinal))
                {
                    DialogResult result = MessageBox.Show(errors["new-version"], errors["new-version-title"],
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        var psi = new System.Diagnostics.ProcessStartInfo
                        {
                            UseShellExecute = true,
                            FileName = "http://dbdconfigeditor.epizy.com/"
                        };
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

                if (string.IsNullOrEmpty(ReadConfig))
                {
                    MessageBox.Show("It seems like your Dead by Daylight configuration files are empty!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.WriteAllText(SettingsPath, UsersSettingsContent);
                }

                if (string.IsNullOrEmpty(ReadEngine))
                {
                    File.WriteAllText(EnginePath, UsersEngineContent);
                }

                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);

                if (!string.IsNullOrEmpty(ReadConfig))
                {
                    ReadAndFixAllValues();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ResetColors()
        {
            foreach (Control panels in panel10.Controls)
            {
                if (panels.Name.Contains("panel", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (Control control in panels.Controls)
                    {
                        if (control.Name.Contains("btn", StringComparison.OrdinalIgnoreCase) && !control.Name.Contains("Discord", StringComparison.OrdinalIgnoreCase) && !control.Name.Contains("UsersSettings", StringComparison.OrdinalIgnoreCase) && !control.Name.Contains("Delete", StringComparison.OrdinalIgnoreCase))
                        {
                            control.BackColor = Color.FromArgb(255, 255, 255);
                            control.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        public void ReadAndFixAllValues()
        {
            ResetColors();

            string[] settingsLines = File.ReadAllLines(SettingsPath);
            string[] engineLines = File.ReadAllLines(EnginePath);

            for (int i = 0; i < settingsLines.Length; i++)
            {
                if (settingsLines[i].Contains("FPSLimitMode=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);
                    if (result == 0)
                    {
                        for (int a = 0; a < engineLines.Length; a++)
                        {
                            if (engineLines[a].Contains("MaxSmoothedFrameRate=", StringComparison.Ordinal))
                            {
                                string str = DigitsRegex().Match(engineLines[a]).Value;
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
                if (settingsLines[i].Contains("sg.ResolutionQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);
                    if (result < tbRes.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", tbRes.Minimum.ToString() + ".000000");
                        result = tbRes.Minimum;
                    }

                    if (result > tbRes.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", tbRes.Maximum.ToString() + ".000000");
                        result = tbRes.Maximum;
                    }
                    tbRes.Value = result;
                    lblRes.Text = tbRes.Value + "%";
                }
                if (settingsLines[i].Contains("sg.ViewDistanceQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("sg.AntiAliasingQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnAaLow.BackColor = Color.Crimson;
                        btnAaLow.ForeColor = Color.White;
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
                if (settingsLines[i].Contains("AntiAliasingMode=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result == 0)
                    {
                        btnAaDisable.BackColor = Color.Crimson;
                        btnAaDisable.ForeColor = Color.White;

                        btnAaLow.BackColor = Color.FromArgb(255, 255, 255);
                        btnAaLow.ForeColor = Color.Black;

                        btnAaMedium.BackColor = Color.FromArgb(255, 255, 255);
                        btnAaMedium.ForeColor = Color.Black;

                        btnAaHigh.BackColor = Color.FromArgb(255, 255, 255);
                        btnAaHigh.ForeColor = Color.Black;

                        btnAaUltra.BackColor = Color.FromArgb(255, 255, 255);
                        btnAaUltra.ForeColor = Color.Black;

                        btnAaEpic.BackColor = Color.FromArgb(255, 255, 255);
                        btnAaEpic.ForeColor = Color.Black;

                        btnAaAwesome.BackColor = Color.FromArgb(255, 255, 255);
                        btnAaAwesome.ForeColor = Color.Black;
                    }
                }
                if (settingsLines[i].Contains("sg.ShadowQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("sg.PostProcessQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("sg.TextureQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("sg.EffectsQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("sg.FoliageQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("sg.ShadingQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("sg.AnimationQuality=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("AudioQualityLevel=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
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
                if (settingsLines[i].Contains("MainVolume=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result < tbVolume.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "MainVolume=", tbVolume.Minimum.ToString());
                        result = tbVolume.Minimum;
                    }

                    if (result > tbVolume.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "MainVolume=", tbVolume.Maximum.ToString());
                        result = tbVolume.Maximum;
                    }
                    tbVolume.Value = result;
                    lblMainVolume.Text = tbVolume.Value + "%";
                }
                if (settingsLines[i].Contains("MenuMusicVolume=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result < tbMusic.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "MenuMusicVolume=", tbMusic.Minimum.ToString());
                        result = tbMusic.Minimum;
                    }

                    if (result > tbMusic.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "MenuMusicVolume=", tbMusic.Maximum.ToString());
                        result = tbMusic.Maximum;
                    }

                    tbMusic.Value = result;
                    lblMenu.Text = tbMusic.Value + "%";
                }
                if (settingsLines[i].Contains("KillerMouseSensitivity=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result < tbKillerMouse.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "KillerMouseSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbKillerMouse.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "KillerMouseSensitivity=", 100.ToString());
                        result = 100;
                    }

                    tbKillerMouse.Value = result;
                    lblKillerMouse.Text = tbKillerMouse.Value + "%";
                }
                if (settingsLines[i].Contains("SurvivorMouseSensitivity=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result < tbSurvMouse.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "SurvivorMouseSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbSurvMouse.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "SurvivorMouseSensitivity=", 100.ToString());
                        result = 100;
                    }
                    tbSurvMouse.Value = result;
                    lblSurvMouse.Text = tbSurvMouse.Value + "%";
                }
                if (settingsLines[i].Contains("KillerControllerSensitivity=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);
                    if (result < tbKillerController.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "KillerControllerSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbKillerController.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "KillerControllerSensitivity=", 100.ToString());
                        result = 100;
                    }
                    tbKillerController.Value = result;
                    lblKillerController.Text = tbKillerController.Value + "%";
                }
                if (settingsLines[i].Contains("FieldOfView=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result < tbSurvController.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "FieldOfView=", 87.ToString());
                        result = 90;
                    }

                    if (result > tbSurvController.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "FieldOfView=", 103.ToString());
                        result = 103;
                    }
                    tbFOV.Value = result;
                    lblFOV.Text = tbFOV.Value + "°";
                }
                if (settingsLines[i].Contains("SurvivorControllerSensitivity=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result < tbSurvController.Minimum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "SurvivorControllerSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbSurvController.Maximum)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "SurvivorControllerSensitivity=", 100.ToString());
                        result = 100;
                    }
                    tbSurvController.Value = result;
                    lblSurvController.Text = tbSurvController.Value + "%";
                }
                if (settingsLines[i].Contains("ResolutionSizeX=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result > numWidth.Maximum || result < numWidth.Minimum)
                    {
                        result = 1920;
                        ChangeConfig.GameUserSettings(SettingsPath, "ResolutionSizeX=", result.ToString());
                    }

                    numWidth.Value = result;
                }
                if (settingsLines[i].Contains("ResolutionSizeY=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);

                    if (result > numHeight.Maximum || result < numHeight.Minimum)
                    {
                        result = 1080;
                        ChangeConfig.GameUserSettings(SettingsPath, "ResolutionSizeY=", result.ToString());
                    }

                    numHeight.Value = result;
                }
                if (settingsLines[i].Contains("bUseVSync=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbVsync.Checked = false;
                    }
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbVsync.Checked = true;
                    }
                }

                if (settingsLines[i].Contains("UseHeadphones=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbHeadphones.Checked = false;
                    }
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbHeadphones.Checked = true;
                    }
                }

                if (settingsLines[i].Contains("AllowCopyrightedMusic=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbCopyright.Checked = false;
                    }
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbCopyright.Checked = true;
                    }
                }

                if (settingsLines[i].Contains("MuteOnFocusLost=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbMuteFocusLost.Checked = false;
                    }
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbMuteFocusLost.Checked = true;
                    }
                }
                if (settingsLines[i].Contains("LegacyPrestigePortraits=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbLegacyPrestige.Checked = false;
                    }
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbLegacyPrestige.Checked = true;
                    }
                }
                if (settingsLines[i].Contains("HapticsVibrationPS5=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbHapticsVibration.Checked = false;
                    }
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbHapticsVibration.Checked = true;
                    }
                }
                if (settingsLines[i].Contains("SprintToCancel=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbSprintToCancel.Checked = false;
                    }
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbSprintToCancel.Checked = true;
                    }
                }

                if (settingsLines[i].Contains("Language=", StringComparison.Ordinal))
                {
                    string word = settingsLines[i] switch
                    {
                        var text when text.Contains("=du", StringComparison.Ordinal) => "Deutsch",
                        var text when text.Contains("=en", StringComparison.Ordinal) => "English",
                        var text when text.Contains("=es-MX", StringComparison.Ordinal) => "Español (México)",
                        var text when text.Contains("=es", StringComparison.Ordinal) => "Español",
                        var text when text.Contains("=fr", StringComparison.Ordinal) => "Français",
                        var text when text.Contains("=it", StringComparison.Ordinal) => "Italiano",
                        var text when text.Contains("=pl", StringComparison.Ordinal) => "Polski",
                        var text when text.Contains("=pt-BR", StringComparison.Ordinal) => "Português (Brasil)",
                        var text when text.Contains("=tr", StringComparison.Ordinal) => "Türkçe",
                        var text when text.Contains("=ru", StringComparison.Ordinal) => "русский",
                        var text when text.Contains("=th", StringComparison.Ordinal) => "ไทย",
                        var text when text.Contains("=zh-Hans", StringComparison.Ordinal) => "中文 (简体)",
                        var text when text.Contains("=zh-Hant", StringComparison.Ordinal) => "中文 (繁體)",
                        var text when text.Contains("=ja", StringComparison.Ordinal) => "日本語",
                        var text when text.Contains("=ko", StringComparison.Ordinal) => "한국어",
                        _ => null
                    };

                    if (word == null)
                    {
                        ChangeConfig.GameUserSettings(SettingsPath, "Language=", "en");
                        word = "English";
                    }
                    cbInGameLangauge.SelectedIndex = cbInGameLangauge.Items.IndexOf(word);
                }

                if (settingsLines[i].Contains("ScreenRenderSize=", StringComparison.Ordinal))
                {
                    string resultString = DigitsRegex().Match(settingsLines[i]).Value;
                    int result = int.Parse(resultString);
                    if (result < tbScRes.Minimum)
                    {
                        result = tbScRes.Minimum;
                        ChangeConfig.GameUserSettings(SettingsPath, "ScreenRenderSize=", result.ToString());
                    }

                    if (result > tbScRes.Maximum)
                    {
                        result = tbScRes.Maximum;
                        ChangeConfig.GameUserSettings(SettingsPath, "ScreenRenderSize=", result.ToString());
                    }
                    tbScRes.Value = result;
                    lblScRes.Text = tbScRes.Value + "%";
                }
                if (settingsLines[i].Contains("KillerToggleInteractions=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbKillerToggle.Checked = true;
                    }
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbKillerToggle.Checked = false;
                    }
                }
                if (settingsLines[i].Contains("SurvivorToggleInteractions=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbSurvivorToggle.Checked = true;
                    }
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbSurvivorToggle.Checked = false;
                    }
                }
                if (settingsLines[i].Contains("TerrorRadiusVisualFeedback=", StringComparison.Ordinal))
                {
                    if (settingsLines[i].Contains("True", StringComparison.OrdinalIgnoreCase))
                    {
                        cbHeartIcon.Checked = true;
                    }
                    if (settingsLines[i].Contains("False", StringComparison.OrdinalIgnoreCase))
                    {
                        cbHeartIcon.Checked = false;
                    }
                }
            }

            ReadConfig = File.ReadAllText(SettingsPath);
            ReadEngine = File.ReadAllText(EnginePath);

            File.WriteAllText(SettingsPath, ReadConfig);
            File.WriteAllText(EnginePath, ReadEngine);
        }
    }
}