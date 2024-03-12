using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbD_Settings_Changer
{
    public partial class Main : Form
    {
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
            menuGraphics.Location = new Point(menuGraphics.Location.X, 14);
            menuGraphics.Size = new Size(menuGraphics.Size.Width, 40);
            menuColors("menuGraphics");

            panelGraphics.Show();
            panelAudio.Hide();
            panelFPS.Hide();
            panelPresets.Hide();
            panelSensitivity.Hide();
            panelGame.Hide();
        }

        private void ChangeLanguageInFile()
        {
            int numLines = File.ReadLines(ApplicationLanguageData + "lang.ini").Count();
            string[] lang = File.ReadAllLines(ApplicationLanguageData + "lang.ini");
            string[] words = new string[] { "en", "pl", "ru", "de", "fr", "jp", "ch", "tu", "esp", "it" };

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
                            case "de":
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
                    ReadAndFixAllValues();
                }
            }
            catch (Exception)
            {

            }
        }

        private void ResetColors()
        {
            foreach (Control panels in panel10.Controls)
            {
                if (panels.Name.Contains("panel"))
                {
                    foreach (Control control in panels.Controls)
                    {
                        if (control.Name.Contains("btn") && !control.Name.Contains("Discord") && !control.Name.Contains("UsersSettings") && !control.Name.Contains("Delete"))
                        {
                            control.BackColor = Color.FromArgb(224, 224, 224);
                            control.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        public void ReadAndFixAllValues()
        {
            int SuperLow = 0;
            int Balanced = 0;
            int Awesome = 0;

            ResetColors();
            int numSettingsLines = File.ReadLines(SettingsPath).Count();
            int numEngineLines = File.ReadLines(EnginePath).Count();

            string[] settingsLines = File.ReadAllLines(SettingsPath);

            string[] engineLines = File.ReadAllLines(EnginePath);

            for (int i = 0; i < numSettingsLines; i++)
            {
                if (settingsLines[i].Contains("FPSLimitMode="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result == 0)
                    {
                        for (int a = 0; a < numEngineLines; a++)
                        {
                            if (engineLines[a].Contains("MaxSmoothedFrameRate="))
                            {
                                string str = Regex.Match(engineLines[a], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.ResolutionQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result < tbRes.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", tbRes.Minimum.ToString() + ".000000");
                        result = tbRes.Minimum;
                    }

                    if (result > tbRes.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", tbRes.Maximum.ToString() + ".000000");
                        result = tbRes.Maximum;
                    }
                    tbRes.Value = result;
                    lblRes.Text = tbRes.Value + "%";
                }
                if (settingsLines[i].Contains("sg.ViewDistanceQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.AntiAliasingQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.ShadowQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.PostProcessQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.TextureQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.EffectsQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.FoliageQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.ShadingQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("sg.AnimationQuality="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("AudioQualityLevel="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
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
                if (settingsLines[i].Contains("MainVolume="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < tbVolume.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "MainVolume=", tbVolume.Minimum.ToString());
                        result = tbVolume.Minimum;
                    }

                    if (result > tbVolume.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "MainVolume=", tbVolume.Maximum.ToString());
                        result = tbVolume.Maximum;
                    }
                    tbVolume.Value = result;
                    lblMainVolume.Text = tbVolume.Value + "%";
                }
                if (settingsLines[i].Contains("MenuMusicVolume="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < tbMusic.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "MenuMusicVolume=", tbMusic.Minimum.ToString());
                        result = tbMusic.Minimum;
                    }

                    if (result > tbMusic.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "MenuMusicVolume=", tbMusic.Maximum.ToString());
                        result = tbMusic.Maximum;
                    }

                    tbMusic.Value = result;
                    lblMenu.Text = tbMusic.Value + "%";
                }
                if (settingsLines[i].Contains("KillerMouseSensitivity="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < tbKillerMouse.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "KillerMouseSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbKillerMouse.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "KillerMouseSensitivity=", 100.ToString());
                        result = 100;
                    }

                    tbKillerMouse.Value = result;
                    lblKillerMouse.Text = tbKillerMouse.Value + "%";
                }
                if (settingsLines[i].Contains("SurvivorMouseSensitivity="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < tbSurvMouse.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "SurvivorMouseSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbSurvMouse.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "SurvivorMouseSensitivity=", 100.ToString());
                        result = 100;
                    }
                    tbSurvMouse.Value = result;
                    lblSurvMouse.Text = tbSurvMouse.Value + "%";
                }
                if (settingsLines[i].Contains("KillerControllerSensitivity="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result < tbKillerController.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "KillerControllerSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbKillerController.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "KillerControllerSensitivity=", 100.ToString());
                        result = 100;
                    }
                    tbKillerController.Value = result;
                    lblKillerController.Text = tbKillerController.Value + "%";
                }
                if (settingsLines[i].Contains("FieldOfView="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < tbSurvController.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "FieldOfView=", 87.ToString());
                        result = 90;
                    }

                    if (result > tbSurvController.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "FieldOfView=", 103.ToString());
                        result = 103;
                    }
                    tbFOV.Value = result;
                    lblFOV.Text = tbFOV.Value + "°";
                }
                if (settingsLines[i].Contains("SurvivorControllerSensitivity="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < tbSurvController.Minimum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "SurvivorControllerSensitivity=", 1.ToString());
                        result = 1;
                    }

                    if (result > tbSurvController.Maximum)
                    {
                        EditConfig.GameUserSettings(SettingsPath, "SurvivorControllerSensitivity=", 100.ToString());
                        result = 100;
                    }
                    tbSurvController.Value = result;
                    lblSurvController.Text = tbSurvController.Value + "%";
                }
                if (settingsLines[i].Contains("ResolutionSizeX="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result > numWidth.Maximum)
                    {
                        result = 1920;
                        EditConfig.GameUserSettings(SettingsPath, "ResolutionSizeX=", result.ToString());
                    }

                    if (result < numWidth.Minimum)
                    {
                        result = 1920;
                        EditConfig.GameUserSettings(SettingsPath, "ResolutionSizeX=", result.ToString());
                    }

                    numWidth.Value = result;
                }
                if (settingsLines[i].Contains("ResolutionSizeY="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result > numHeight.Maximum)
                    {
                        result = 1080;
                        EditConfig.GameUserSettings(SettingsPath, "ResolutionSizeY=", result.ToString());
                    }

                    if (result < numHeight.Minimum)
                    {
                        result = 1080;
                        EditConfig.GameUserSettings(SettingsPath, "ResolutionSizeY=", result.ToString());
                    }

                    numHeight.Value = result;
                }
                if (settingsLines[i].Contains("bUseVSync="))
                {
                    if (settingsLines[i].Contains("False"))
                    {
                        cbVsync.Checked = false;
                    }
                    if (settingsLines[i].Contains("True"))
                    {
                        cbVsync.Checked = true;
                    }
                }

                if (settingsLines[i].Contains("UseHeadphones="))
                {
                    if (settingsLines[i].Contains("False"))
                    {
                        cbHeadphones.Checked = false;
                    }
                    if (settingsLines[i].Contains("True"))
                    {
                        cbHeadphones.Checked = true;
                    }
                }

                if (settingsLines[i].Contains("MuteOnFocusLost="))
                {
                    if (settingsLines[i].Contains("False"))
                    {
                        cbMuteFocusLost.Checked = false;
                    }
                    if (settingsLines[i].Contains("True"))
                    {
                        cbMuteFocusLost.Checked = true;
                    }
                }
                if (settingsLines[i].Contains("LegacyPrestigePortraits="))
                {
                    if (settingsLines[i].Contains("False"))
                    {
                        cbLegacyPrestige.Checked = false;
                    }
                    if (settingsLines[i].Contains("True"))
                    {
                        cbLegacyPrestige.Checked = true;
                    }
                }
                if (settingsLines[i].Contains("HapticsVibrationPS5="))
                {
                    if (settingsLines[i].Contains("False"))
                    {
                        cbHapticsVibration.Checked = false;
                    }
                    if (settingsLines[i].Contains("True"))
                    {
                        cbHapticsVibration.Checked = true;
                    }
                }
                if (settingsLines[i].Contains("SprintToCancel="))
                {
                    if (settingsLines[i].Contains("False"))
                    {
                        cbSprintToCancel.Checked = false;
                    }
                    if (settingsLines[i].Contains("True"))
                    {
                        cbSprintToCancel.Checked = true;
                    }
                }

                if (settingsLines[i].Contains("Language="))
                {
                    string word = "";
                    switch (word)
                    {
                        case var text when settingsLines[i].Contains("=du"):
                            word = "Deutsch";
                            break;
                        case var text when settingsLines[i].Contains("=en"):
                            word = "English";
                            break;
                        case var text when settingsLines[i].Contains("=es"):
                            word = "Español";
                            break;
                        case var text when settingsLines[i].Contains("=es-MX"):
                            word = "Español (México)";
                            break;
                        case var text when settingsLines[i].Contains("=fr"):
                            word = "Français";
                            break;
                        case var text when settingsLines[i].Contains("=it"):
                            word = "Italiano";
                            break;
                        case var text when settingsLines[i].Contains("=pl"):
                            word = "Polski";
                            break;
                        case var text when settingsLines[i].Contains("=pt-BR"):
                            word = "Português (Brasil)";
                            break;
                        case var text when settingsLines[i].Contains("=tr"):
                            word = "Türkçe";
                            break;
                        case var text when settingsLines[i].Contains("=ru"):
                            word = "русский";
                            break;
                        case var text when settingsLines[i].Contains("=th"):
                            word = "ไทย";
                            break;
                        case var text when settingsLines[i].Contains("=zh-Hans"):
                            word = "中文 (简体)";
                            break;
                        case var text when settingsLines[i].Contains("=zh-Hant"):
                            word = "中文 (繁體)";
                            break;
                        case var text when settingsLines[i].Contains("=ja"):
                            word = "日本語";
                            break;
                        case var text when settingsLines[i].Contains("=ko"):
                            word = "한국어";
                            break;
                        default:
                            var changeConfig = new ChangeConfig();
                            changeConfig.GameUserSettings(SettingsPath, "Language=", "en");
                            word = "English";
                            break;
                    }
                    cbInGameLangauge.SelectedIndex = cbInGameLangauge.Items.IndexOf(word);
                }
                
                if (settingsLines[i].Contains("ScreenResolution="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result < tbScRes.Minimum)
                    {
                        result = tbScRes.Minimum;
                        EditConfig.GameUserSettings(SettingsPath, "ScreenResolution=", result.ToString());
                    }

                    if (result > tbScRes.Maximum)
                    {
                        result = tbScRes.Maximum;
                        EditConfig.GameUserSettings(SettingsPath, "ScreenResolution=", result.ToString());
                    }
                    tbScRes.Value = result;
                    lblScRes.Text = tbScRes.Value + "%";
                }
                if (settingsLines[i].Contains("KillerToggleInteractions="))
                {
                    if (settingsLines[i].Contains("True"))
                    {
                        cbKillerToggle.Checked = true;
                    }
                    if (settingsLines[i].Contains("False"))
                    {
                        cbKillerToggle.Checked = false;
                    }
                }
                if (settingsLines[i].Contains("SurvivorToggleInteractions="))
                {
                    if (settingsLines[i].Contains("True"))
                    {
                        cbSurvivorToggle.Checked = true;
                    }
                    if (settingsLines[i].Contains("False"))
                    {
                        cbSurvivorToggle.Checked = false;
                    }
                }
                if (settingsLines[i].Contains("TerrorRadiusVisualFeedback="))
                {
                    if (settingsLines[i].Contains("True"))
                    {
                        cbHeartIcon.Checked = true;
                    }
                    if (settingsLines[i].Contains("False"))
                    {
                        cbHeartIcon.Checked = false;
                    }
                }
                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);

                File.WriteAllText(SettingsPath, ReadConfig);
                File.WriteAllText(EnginePath, ReadEngine);
            }
        }
    }
}
