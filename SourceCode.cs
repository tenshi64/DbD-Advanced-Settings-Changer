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

namespace DbD_Settings_Changer
{
    public partial class Form1 : Form
    {

        public string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsNoEditor\GameUserSettings.ini";
        public string EnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsNoEditor\Engine.ini";
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
            if (!File.Exists(EnginePath) || !File.Exists(SettingsPath))
            {
                MessageBox.Show("Dead by Daylight confiuration files do not exist! Please, launch the game.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            try
            {
                WebClient wc = new WebClient();
                string textFromFile = wc.DownloadString("Link with version (I won't show you!)");
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (!textFromFile.Contains(version))
                { 
                    DialogResult result = MessageBox.Show("A new version of the program has been found. \nBefore installing the new version, please uninstall the old one.\n\nDo you want to download it?", "Check for updates",
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
            catch(Exception)
            {
                MessageBox.Show("Checking newer program versions failed!", "Check for updates",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      

            lblHideFocus.Focus();

            try
            {
                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);
                ReadEngine = Regex.Replace(ReadEngine, @"\n\n", String.Empty);
                ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");
                File.WriteAllText(SettingsPath, ReadConfig);
                File.WriteAllText(EnginePath, ReadEngine);

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
                //Deklaracja oraz usuwanie enterów

                if (ReadConfig != null || ReadConfig.Length != 0)
                {
                    ReadConfig = File.ReadAllText(SettingsPath);
                    ReadEngine = File.ReadAllText(EnginePath);
                    ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");
                    ReadEngine = Regex.Replace(ReadEngine, Environment.NewLine + Environment.NewLine, "\r");

                    File.WriteAllText(SettingsPath, ReadConfig);
                    File.WriteAllText(EnginePath, ReadEngine);
                    int numLines = ReadConfig.Split('\n').Length;
                    int numLines2 = ReadEngine.Split('\n').Length;

                    string[] lines = File.ReadAllLines(SettingsPath);
                    Console.WriteLine(String.Join(Environment.NewLine, lines));
                    string[] lines2 = File.ReadAllLines(EnginePath);
                    Console.WriteLine(String.Join(Environment.NewLine, lines2));

                    for (int i = 0; i <= numLines - 1; i++)
                    {
                        if (lines[i].Contains("sg.ResolutionQuality"))
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
                        if (lines[i].Contains("sg.ViewDistanceQuality"))
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
                                btnVwEpic.BackColor = Color.Crimson;
                                btnVwEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.AntiAliasingQuality"))
                        {
                            string resultString = Regex.Match(lines[i], @"\d+").Value;
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
                                btnAaEpic.BackColor = Color.Crimson;
                                btnAaEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.ShadowQuality"))
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
                                btnShadEpic.BackColor = Color.Crimson;
                                btnShadEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.PostProcessQuality"))
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
                                btnPpEpic.BackColor = Color.Crimson;
                                btnPpEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.TextureQuality"))
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
                                btnTxtEpic.BackColor = Color.Crimson;
                                btnTxtEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.EffectsQuality"))
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
                                btnEffEpic.BackColor = Color.Crimson;
                                btnEffEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.FoliageQuality"))
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
                                btnFolEpic.BackColor = Color.Crimson;
                                btnFolEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.ShadingQuality"))
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
                                btnShEpic.BackColor = Color.Crimson;
                                btnShEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("sg.AnimationQuality"))
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
                                btnAnimEpic.BackColor = Color.Crimson;
                                btnAnimEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("AudioQualityLevel"))
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
                                btnAudioEpic.BackColor = Color.Crimson;
                                btnAudioEpic.ForeColor = Color.White;
                            }
                        }
                        if (lines[i].Contains("MainVolume"))
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
                        if (lines[i].Contains("MenuMusicVolume"))
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
                        if (lines[i].Contains("KillerMouseSensitivity"))
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
                        if (lines[i].Contains("SurvivorMouseSensitivity"))
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
                        if (lines[i].Contains("KillerControllerSensitivity"))
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
                        if (lines[i].Contains("SurvivorControllerSensitivity"))
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
                        if (lines[i].Contains("ResolutionSizeX"))
                        {
                            string resultString = Regex.Match(lines[i], @"\d+").Value;
                            int result = int.Parse(resultString);
                            numWidth.Value = result;
                        }
                        if (lines[i].Contains("ResolutionSizeY"))
                        {
                            string resultString = Regex.Match(lines[i], @"\d+").Value;
                            int result = int.Parse(resultString);
                            numHeight.Value = result;
                        }
                        if (lines[i].Contains("bUseVSync"))
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

                        if (lines[i].Contains("UseHeadphones"))
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

                        if (lines[i].Contains("ScreenResolution"))
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
            }
            catch(Exception)
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

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i<=numLines-1; i++)
            {

                if (lines[i].Contains("ResolutionQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    res = res.Replace(lines[i], "sg.ResolutionQuality=" + resolution + ".000000");
                    File.WriteAllText(SettingsPath, res);
                }
            }
        }

        private void btnUsersSettings_Click(object sender, EventArgs e)
        {
            btnVwLow.ForeColor = Color.Black;
            btnVwLow.BackColor = Color.Transparent;
            btnVwMedium.ForeColor = Color.Black;
            btnVwMedium.BackColor = Color.Transparent;
            btnVwHigh.ForeColor = Color.Black;
            btnVwHigh.BackColor = Color.Transparent;
            btnVwUltra.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.Transparent;
            btnVwEpic.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.Transparent;
            btnAaLow.ForeColor = Color.Black;
            btnAaLow.BackColor = Color.Transparent;
            btnAaMedium.ForeColor = Color.Black;
            btnAaMedium.BackColor = Color.Transparent;
            btnAaHigh.ForeColor = Color.Black;
            btnAaHigh.BackColor = Color.Transparent;
            btnAaUltra.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.Transparent;
            btnAaEpic.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.Transparent;
            btnShadLow.ForeColor = Color.Black;
            btnShadLow.BackColor = Color.Transparent;
            btnShadMedium.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.Transparent;
            btnShadHigh.ForeColor = Color.Black;
            btnShadHigh.BackColor = Color.Transparent;
            btnShadUltra.BackColor = Color.Transparent;
            btnShadUltra.ForeColor = Color.Black;
            btnShadEpic.ForeColor = Color.Black;
            btnShadEpic.BackColor = Color.Transparent;
            btnPpLow.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.Transparent;
            btnPpMedium.ForeColor = Color.Black;
            btnPpMedium.BackColor = Color.Transparent;
            btnPpHigh.ForeColor = Color.Black;
            btnPpHigh.BackColor = Color.Transparent;
            btnPpUltra.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.Transparent;
            btnPpEpic.ForeColor = Color.Black;
            btnPpEpic.BackColor = Color.Transparent;
            btnTxtLow.ForeColor = Color.Black;
            btnTxtLow.BackColor = Color.Transparent;
            btnTxtMedium.ForeColor = Color.Black;
            btnTxtMedium.BackColor = Color.Transparent;
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtHigh.BackColor = Color.Transparent;
            btnTxtUltra.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.Transparent;
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.Transparent;
            btnEffLow.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.Transparent;
            btnEffMedium.ForeColor = Color.Black;
            btnEffMedium.BackColor = Color.Transparent;
            btnEffHigh.ForeColor = Color.Black;
            btnEffHigh.BackColor = Color.Transparent;
            btnEffUltra.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.Transparent;
            btnEffEpic.ForeColor = Color.Black;
            btnEffEpic.BackColor = Color.Transparent;
            btnFolLow.ForeColor = Color.Black;
            btnFolLow.BackColor = Color.Transparent;
            btnFolMedium.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.Transparent;
            btnFolHigh.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.Transparent;
            btnFolUltra.ForeColor = Color.Black;
            btnFolUltra.BackColor = Color.Transparent;
            btnFolEpic.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.Transparent; 
            btnShLow.ForeColor = Color.Black;
            btnShMedium.ForeColor = Color.Black;
            btnShHigh.ForeColor = Color.Black;
            btnShHigh.BackColor = Color.Transparent;
            btnShUltra.BackColor = Color.Transparent;
            btnShEpic.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.Transparent;
            btnShLow.ForeColor = Color.Black;
            btnAnimLow.BackColor = Color.Transparent;
            btnAnimLow.ForeColor = Color.Black;
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimMedium.BackColor = Color.Transparent;
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimHigh.BackColor = Color.Transparent;
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.Transparent;
            btnAnimEpic.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.Transparent; 
            btnAudioLow.ForeColor = Color.Black;
            btnAudioLow.BackColor = Color.Transparent;
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.Transparent;
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.Transparent;
            btnAudioUltra.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.Transparent;
            btnAudioEpic.ForeColor = Color.Black;
            btnAudioEpic.BackColor = Color.Transparent;

            lblHideFocus.Focus();
            File.WriteAllText(SettingsPath, UsersSettingsContent);
            File.WriteAllText(EnginePath, UsersEngineContent);

            string ReadConfig = File.ReadAllText(SettingsPath);
            string ReadEngine = File.ReadAllText(EnginePath);
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");
            File.WriteAllText(SettingsPath, ReadConfig);
            ReadEngine = Regex.Replace(ReadEngine, Environment.NewLine + Environment.NewLine, "\r");
            File.WriteAllText(EnginePath, ReadEngine);

            //Deklaracja oraz usuwanie enterów


            int numLines = ReadConfig.Split('\n').Length;

            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("sg.ResolutionQuality"))
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
                if (lines[i].Contains("sg.ViewDistanceQuality"))
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
                        btnVwEpic.BackColor = Color.Crimson;
                        btnVwEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AntiAliasingQuality"))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
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
                        btnAaEpic.BackColor = Color.Crimson;
                        btnAaEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadowQuality"))
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
                        btnShadEpic.BackColor = Color.Crimson;
                        btnShadEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.PostProcessQuality"))
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
                        btnPpEpic.BackColor = Color.Crimson;
                        btnPpEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.TextureQuality"))
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
                        btnTxtEpic.BackColor = Color.Crimson;
                        btnTxtEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.EffectsQuality"))
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
                        btnEffEpic.BackColor = Color.Crimson;
                        btnEffEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.FoliageQuality"))
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
                        btnFolEpic.BackColor = Color.Crimson;
                        btnFolEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadingQuality"))
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
                        btnShEpic.BackColor = Color.Crimson;
                        btnShEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AnimationQuality"))
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
                        btnAnimEpic.BackColor = Color.Crimson;
                        btnAnimEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("AudioQualityLevel"))
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
                        btnAudioEpic.BackColor = Color.Crimson;
                        btnAudioEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("MainVolume"))
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
                if (lines[i].Contains("MenuMusicVolume"))
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
                if (lines[i].Contains("KillerMouseSensitivity"))
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
                if (lines[i].Contains("SurvivorMouseSensitivity"))
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
                if (lines[i].Contains("KillerControllerSensitivity"))
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
                if (lines[i].Contains("SurvivorControllerSensitivity"))
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
                if (lines[i].Contains("ResolutionSizeX"))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    numWidth.Value = result;
                }
                if (lines[i].Contains("ResolutionSizeY"))
                {
                    string resultString = Regex.Match(lines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    numHeight.Value = result;
                }
                if (lines[i].Contains("bUseVSync"))
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

                if (lines[i].Contains("UseHeadphones"))
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

                if (lines[i].Contains("ScreenResolution"))
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
            }
        }

        private void btnUsersSettings_MouseEnter(object sender, EventArgs e)
        {
            btnUsersSettings.BackColor = Color.Crimson;
        }

        private void btnUsersSettings_MouseLeave(object sender, EventArgs e)
        {
            btnUsersSettings.BackColor = Color.Transparent;
            tip.Hide(btnUsersSettings);
        }

        private void btnUsersSettings_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Reset to your own settings before using the program.", btnUsersSettings);
        }

        private void btnVwLow_Click(object sender, EventArgs e)
        {
            btnVwMedium.BackColor = Color.Transparent;
            btnVwMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.Transparent;
            btnAaHigh.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.Transparent;
            btnVwUltra.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.Transparent;
            btnVwEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwLow.BackColor = Color.Crimson;
            btnVwLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ViewDistanceQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ViewDistanceQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnVwMedium_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.Transparent;
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwHigh.BackColor = Color.Transparent;
            btnVwHigh.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.Transparent;
            btnVwUltra.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.Transparent;
            btnVwEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwMedium.BackColor = Color.Crimson;
            btnVwMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ViewDistanceQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ViewDistanceQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnVwHigh_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.Transparent;
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwMedium.BackColor = Color.Transparent;
            btnVwMedium.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.Transparent;
            btnVwUltra.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.Transparent;
            btnVwEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwHigh.BackColor = Color.Crimson;
            btnVwHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ViewDistanceQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ViewDistanceQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnVwUltra_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.Transparent;
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwMedium.BackColor = Color.Transparent;
            btnVwMedium.ForeColor = Color.Black;
            btnVwHigh.BackColor = Color.Transparent;
            btnVwHigh.ForeColor = Color.Black;
            btnVwEpic.BackColor = Color.Transparent;
            btnVwEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwUltra.BackColor = Color.Crimson;
            btnVwUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ViewDistanceQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ViewDistanceQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnVwEpic_Click(object sender, EventArgs e)
        {
            btnVwLow.BackColor = Color.Transparent;
            btnVwLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnVwMedium.BackColor = Color.Transparent;
            btnVwMedium.ForeColor = Color.Black;
            btnVwUltra.BackColor = Color.Transparent;
            btnVwUltra.ForeColor = Color.Black;
            btnVwHigh.BackColor = Color.Transparent;
            btnVwHigh.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnVwEpic.BackColor = Color.Crimson;
            btnVwEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ViewDistanceQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ViewDistanceQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAaLow_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.Transparent;
            btnAaMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.Transparent;
            btnAaHigh.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.Transparent;
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.Transparent;
            btnAaEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaLow.BackColor = Color.Crimson;
            btnAaLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AntiAliasingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AntiAliasingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAaMedium_Click(object sender, EventArgs e)
        {
            btnAaLow.BackColor = Color.Transparent;
            btnAaLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.Transparent;
            btnAaHigh.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.Transparent;
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.Transparent;
            btnAaEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaMedium.BackColor = Color.Crimson;
            btnAaMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AntiAliasingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AntiAliasingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAaHigh_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.Transparent;
            btnAaMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaLow.BackColor = Color.Transparent;
            btnAaLow.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.Transparent;
            btnAaUltra.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.Transparent;
            btnAaEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaHigh.BackColor = Color.Crimson;
            btnAaHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AntiAliasingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AntiAliasingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAaUltra_Click(object sender, EventArgs e)
        {
            btnAaHigh.BackColor = Color.Transparent;
            btnAaHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaMedium.BackColor = Color.Transparent;
            btnAaMedium.ForeColor = Color.Black;
            btnAaLow.BackColor = Color.Transparent;
            btnAaLow.ForeColor = Color.Black;
            btnAaEpic.BackColor = Color.Transparent;
            btnAaEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaUltra.BackColor = Color.Crimson;
            btnAaUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AntiAliasingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AntiAliasingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAaEpic_Click(object sender, EventArgs e)
        {
            btnAaMedium.BackColor = Color.Transparent;
            btnAaMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAaHigh.BackColor = Color.Transparent;
            btnAaHigh.ForeColor = Color.Black;
            btnAaLow.BackColor = Color.Transparent;
            btnAaLow.ForeColor = Color.Black;
            btnAaUltra.BackColor = Color.Transparent;
            btnAaUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAaEpic.BackColor = Color.Crimson;
            btnAaEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AntiAliasingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AntiAliasingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShadLow_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.Transparent;
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.Transparent;
            btnShadHigh.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.Transparent;
            btnShadMedium.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.Transparent;
            btnShadUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadLow.BackColor = Color.Crimson;
            btnShadLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadowQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadowQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShadMedium_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.Transparent;
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.Transparent;
            btnShadHigh.ForeColor = Color.Black;
            btnShadLow.BackColor = Color.Transparent;
            btnShadLow.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.Transparent;
            btnShadUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadMedium.BackColor = Color.Crimson;
            btnShadMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadowQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadowQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShadHigh_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.Transparent;
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadLow.BackColor = Color.Transparent;
            btnShadLow.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.Transparent;
            btnShadMedium.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.Transparent;
            btnShadUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadHigh.BackColor = Color.Crimson;
            btnShadHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadowQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadowQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShadUltra_Click(object sender, EventArgs e)
        {
            btnShadEpic.BackColor = Color.Transparent;
            btnShadEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.Transparent;
            btnShadHigh.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.Transparent;
            btnShadMedium.ForeColor = Color.Black;
            btnShadLow.BackColor = Color.Transparent;
            btnShadLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadUltra.BackColor = Color.Crimson;
            btnShadUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadowQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadowQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShadEpic_Click(object sender, EventArgs e)
        {
            btnShadLow.BackColor = Color.Transparent;
            btnShadLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShadHigh.BackColor = Color.Transparent;
            btnShadHigh.ForeColor = Color.Black;
            btnShadMedium.BackColor = Color.Transparent;
            btnShadMedium.ForeColor = Color.Black;
            btnShadUltra.BackColor = Color.Transparent;
            btnShadUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShadEpic.BackColor = Color.Crimson;
            btnShadEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadowQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadowQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnPpLow_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.Transparent;
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.Transparent;
            btnPpHigh.ForeColor = Color.Black;
            btnPpMedium.BackColor = Color.Transparent;
            btnPpMedium.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.Transparent;
            btnPpUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpLow.BackColor = Color.Crimson;
            btnPpLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("PostProcessQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.PostProcessQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnPpMedium_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.Transparent;
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.Transparent;
            btnPpHigh.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.Transparent;
            btnPpLow.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.Transparent;
            btnPpUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpMedium.BackColor = Color.Crimson;
            btnPpMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("PostProcessQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.PostProcessQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnPpHigh_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.Transparent;
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpMedium.BackColor = Color.Transparent;
            btnPpMedium.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.Transparent;
            btnPpLow.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.Transparent;
            btnPpUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpHigh.BackColor = Color.Crimson;
            btnPpHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("PostProcessQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.PostProcessQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnPpUltra_Click(object sender, EventArgs e)
        {
            btnPpEpic.BackColor = Color.Transparent;
            btnPpEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.Transparent;
            btnPpHigh.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.Transparent;
            btnPpLow.ForeColor = Color.Black;
            btnPpMedium.BackColor = Color.Transparent;
            btnPpMedium.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpUltra.BackColor = Color.Crimson;
            btnPpUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("PostProcessQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.PostProcessQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnPpEpic_Click(object sender, EventArgs e)
        {
            btnPpMedium.BackColor = Color.Transparent;
            btnPpMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnPpHigh.BackColor = Color.Transparent;
            btnPpHigh.ForeColor = Color.Black;
            btnPpLow.BackColor = Color.Transparent;
            btnPpLow.ForeColor = Color.Black;
            btnPpUltra.BackColor = Color.Transparent;
            btnPpUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnPpEpic.BackColor = Color.Crimson;
            btnPpEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("PostProcessQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.PostProcessQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnTxtLow_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.Transparent;
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.Transparent;
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.Transparent;
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.Transparent;
            btnTxtUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtLow.BackColor = Color.Crimson;
            btnTxtLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("TextureQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.TextureQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnTxtMedium_Click(object sender, EventArgs e)
        {
            btnTxtLow.BackColor = Color.Transparent;
            btnTxtLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.Transparent;
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.Transparent;
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.Transparent;
            btnTxtUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtMedium.BackColor = Color.Crimson;
            btnTxtMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("TextureQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.TextureQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnTxtHigh_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.Transparent;
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtLow.BackColor = Color.Transparent;
            btnTxtLow.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.Transparent;
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.Transparent;
            btnTxtUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtHigh.BackColor = Color.Crimson;
            btnTxtHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("TextureQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.TextureQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnTxtUltra_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.Transparent;
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.Transparent;
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtEpic.BackColor = Color.Transparent;
            btnTxtEpic.ForeColor = Color.Black;
            btnTxtLow.BackColor = Color.Transparent;
            btnTxtLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtUltra.BackColor = Color.Crimson;
            btnTxtUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("TextureQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.TextureQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnTxtEpic_Click(object sender, EventArgs e)
        {
            btnTxtMedium.BackColor = Color.Transparent;
            btnTxtMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnTxtHigh.BackColor = Color.Transparent;
            btnTxtHigh.ForeColor = Color.Black;
            btnTxtLow.BackColor = Color.Transparent;
            btnTxtLow.ForeColor = Color.Black;
            btnTxtUltra.BackColor = Color.Transparent;
            btnTxtUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnTxtEpic.BackColor = Color.Crimson;
            btnTxtEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("TextureQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.TextureQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnEffLow_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.Transparent;
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffHigh.BackColor = Color.Transparent;
            btnEffHigh.ForeColor = Color.Black;
            btnEffMedium.BackColor = Color.Transparent;
            btnEffMedium.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.Transparent;
            btnEffUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffLow.BackColor = Color.Crimson;
            btnEffLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("EffectsQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.EffectsQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnEffMedium_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.Transparent;
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffHigh.BackColor = Color.Transparent;
            btnEffHigh.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.Transparent;
            btnEffLow.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.Transparent;
            btnEffUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffMedium.BackColor = Color.Crimson;
            btnEffMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("EffectsQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.EffectsQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnEffHigh_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.Transparent;
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffMedium.BackColor = Color.Transparent;
            btnEffMedium.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.Transparent;
            btnEffLow.ForeColor = Color.Black;
            btnEffUltra.BackColor = Color.Transparent;
            btnEffUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffHigh.BackColor = Color.Crimson;
            btnEffHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("EffectsQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.EffectsQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnEffUltra_Click(object sender, EventArgs e)
        {
            btnEffEpic.BackColor = Color.Transparent;
            btnEffEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffMedium.BackColor = Color.Transparent;
            btnEffMedium.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.Transparent;
            btnEffLow.ForeColor = Color.Black;
            btnEffHigh.BackColor = Color.Transparent;
            btnEffHigh.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffUltra.BackColor = Color.Crimson;
            btnEffUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("EffectsQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.EffectsQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnEffEpic_Click(object sender, EventArgs e)
        {
            btnEffUltra.BackColor = Color.Transparent;
            btnEffUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnEffMedium.BackColor = Color.Transparent;
            btnEffMedium.ForeColor = Color.Black;
            btnEffLow.BackColor = Color.Transparent;
            btnEffLow.ForeColor = Color.Black;
            btnEffHigh.BackColor = Color.Transparent;
            btnEffHigh.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnEffEpic.BackColor = Color.Crimson;
            btnEffEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("EffectsQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.EffectsQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnFolLow_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.Transparent;
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolMedium.BackColor = Color.Transparent;
            btnFolMedium.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.Transparent;
            btnFolEpic.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.Transparent;
            btnFolHigh.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolLow.BackColor = Color.Crimson;
            btnFolLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("FoliageQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.FoliageQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnFolMedium_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.Transparent;
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.Transparent;
            btnFolLow.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.Transparent;
            btnFolEpic.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.Transparent;
            btnFolHigh.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolMedium.BackColor = Color.Crimson;
            btnFolMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("FoliageQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.FoliageQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnFolHigh_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.Transparent;
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.Transparent;
            btnFolLow.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.Transparent;
            btnFolEpic.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.Transparent;
            btnFolMedium.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolHigh.BackColor = Color.Crimson;
            btnFolHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("FoliageQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.FoliageQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnFolUltra_Click(object sender, EventArgs e)
        {
            btnFolHigh.BackColor = Color.Transparent;
            btnFolHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.Transparent;
            btnFolLow.ForeColor = Color.Black;
            btnFolEpic.BackColor = Color.Transparent;
            btnFolEpic.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.Transparent;
            btnFolMedium.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolUltra.BackColor = Color.Crimson;
            btnFolUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("FoliageQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.FoliageQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnFolEpic_Click(object sender, EventArgs e)
        {
            btnFolUltra.BackColor = Color.Transparent;
            btnFolUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnFolLow.BackColor = Color.Transparent;
            btnFolLow.ForeColor = Color.Black;
            btnFolHigh.BackColor = Color.Transparent;
            btnFolHigh.ForeColor = Color.Black;
            btnFolMedium.BackColor = Color.Transparent;
            btnFolMedium.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnFolEpic.BackColor = Color.Crimson;
            btnFolEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("FoliageQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.FoliageQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShLow_Click(object sender, EventArgs e)
        {
            btnShHigh.BackColor = Color.Transparent;
            btnShHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShUltra.BackColor = Color.Transparent;
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.Transparent;
            btnShEpic.ForeColor = Color.Black;
            btnShMedium.BackColor = Color.Transparent;
            btnShMedium.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShLow.BackColor = Color.Crimson;
            btnShLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShMedium_Click(object sender, EventArgs e)
        {
            btnShHigh.BackColor = Color.Transparent;
            btnShHigh.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShUltra.BackColor = Color.Transparent;
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.Transparent;
            btnShEpic.ForeColor = Color.Black;
            btnShLow.BackColor = Color.Transparent;
            btnShLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShMedium.BackColor = Color.Crimson;
            btnShMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShHigh_Click(object sender, EventArgs e)
        {
            btnShMedium.BackColor = Color.Transparent;
            btnShMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShUltra.BackColor = Color.Transparent;
            btnShUltra.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.Transparent;
            btnShEpic.ForeColor = Color.Black;
            btnShLow.BackColor = Color.Transparent;
            btnShLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShHigh.BackColor = Color.Crimson;
            btnShHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShUltra_Click(object sender, EventArgs e)
        {
            btnShMedium.BackColor = Color.Transparent;
            btnShMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShHigh.BackColor = Color.Transparent;
            btnShHigh.ForeColor = Color.Black;
            btnShEpic.BackColor = Color.Transparent;
            btnShEpic.ForeColor = Color.Black;
            btnShLow.BackColor = Color.Transparent;
            btnShLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShUltra.BackColor = Color.Crimson;
            btnShUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnShEpic_Click(object sender, EventArgs e)
        {
            btnShMedium.BackColor = Color.Transparent;
            btnShMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnShHigh.BackColor = Color.Transparent;
            btnShHigh.ForeColor = Color.Black;
            btnShUltra.BackColor = Color.Transparent;
            btnShUltra.ForeColor = Color.Black;
            btnShLow.BackColor = Color.Transparent;
            btnShLow.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnShEpic.BackColor = Color.Crimson;
            btnShEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ShadingQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.ShadingQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAnimLow_Click(object sender, EventArgs e)
        {
            btnAnimMedium.BackColor = Color.Transparent;
            btnAnimMedium.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimHigh.BackColor = Color.Transparent;
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.Transparent;
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.Transparent;
            btnAnimEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimLow.BackColor = Color.Crimson;
            btnAnimLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AnimationQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AnimationQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAnimMedium_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.Transparent;
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimHigh.BackColor = Color.Transparent;
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.Transparent;
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.Transparent;
            btnAnimEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimMedium.BackColor = Color.Crimson;
            btnAnimMedium.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AnimationQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AnimationQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAnimHigh_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.Transparent;
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimMedium.BackColor = Color.Transparent;
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.Transparent;
            btnAnimUltra.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.Transparent;
            btnAnimEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimHigh.BackColor = Color.Crimson;
            btnAnimHigh.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AnimationQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AnimationQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAnimUltra_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.Transparent;
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimMedium.BackColor = Color.Transparent;
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimHigh.BackColor = Color.Transparent;
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimEpic.BackColor = Color.Transparent;
            btnAnimEpic.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimUltra.BackColor = Color.Crimson;
            btnAnimUltra.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AnimationQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AnimationQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAnimEpic_Click(object sender, EventArgs e)
        {
            btnAnimLow.BackColor = Color.Transparent;
            btnAnimLow.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAnimMedium.BackColor = Color.Transparent;
            btnAnimMedium.ForeColor = Color.Black;
            btnAnimHigh.BackColor = Color.Transparent;
            btnAnimHigh.ForeColor = Color.Black;
            btnAnimUltra.BackColor = Color.Transparent;
            btnAnimUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAnimEpic.BackColor = Color.Crimson;
            btnAnimEpic.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AnimationQuality"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "sg.AnimationQuality=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string audio = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int aud = trackBar1.Value;
            lblMainVolume.Text = aud.ToString() + "%";

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("MainVolume"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    audio = audio.Replace(lines[i], "MainVolume=" + aud);
                    File.WriteAllText(SettingsPath, audio);
                }
            }
        }

        private void btnAudioLow_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.Transparent;
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioMedium.BackColor = Color.Transparent;
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.Transparent;
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.Transparent;
            btnAudioUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioLow.BackColor = Color.Crimson;
            btnAudioLow.ForeColor = Color.White;

            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 0;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "AudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastConfirmedAudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "LastConfirmedAudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAudioMedium_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.Transparent;
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.Transparent;
            btnAudioLow.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.Transparent;
            btnAudioHigh.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.Transparent;
            btnAudioUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioMedium.BackColor = Color.Crimson;
            btnAudioMedium.ForeColor = Color.White;
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 1;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "AudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastConfirmedAudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "LastConfirmedAudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAudioHigh_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.Transparent;
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.Transparent;
            btnAudioLow.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.Transparent;
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioUltra.BackColor = Color.Transparent;
            btnAudioUltra.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioHigh.BackColor = Color.Crimson;
            btnAudioHigh.ForeColor = Color.White;
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 2;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "AudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastConfirmedAudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "LastConfirmedAudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAudioUltra_Click(object sender, EventArgs e)
        {
            btnAudioEpic.BackColor = Color.Transparent;
            btnAudioEpic.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.Transparent;
            btnAudioLow.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.Transparent;
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.Transparent;
            btnAudioHigh.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioUltra.BackColor = Color.Crimson;
            btnAudioUltra.ForeColor = Color.White;
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 3;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "AudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastConfirmedAudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "LastConfirmedAudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnAudioEpic_Click(object sender, EventArgs e)
        {
            btnAudioUltra.BackColor = Color.Transparent;
            btnAudioUltra.ForeColor = Color.Black;
            //nazwa wcześniejszego
            btnAudioLow.BackColor = Color.Transparent;
            btnAudioLow.ForeColor = Color.Black;
            btnAudioMedium.BackColor = Color.Transparent;
            btnAudioMedium.ForeColor = Color.Black;
            btnAudioHigh.BackColor = Color.Transparent;
            btnAudioHigh.ForeColor = Color.Black;
            //nazwa klikniętego (poniżej)
            btnAudioEpic.BackColor = Color.Crimson;
            btnAudioEpic.ForeColor = Color.White;
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int graphic = 4;

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("AudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "AudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }

                if (lines[i].Contains("LastConfirmedAudioQualityLevel"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    view = view.Replace(lines[i], "LastConfirmedAudioQualityLevel=" + graphic);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string audio = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int aud = trackBar2.Value;
            lblMenu.Text = aud.ToString() + "%";

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("MenuMusicVolume"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    audio = audio.Replace(lines[i], "MenuMusicVolume=" + aud);
                    File.WriteAllText(SettingsPath, audio);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            bool check;
            if(checkBox1.Checked)
            {
                lblHideFocus.Focus();
                string view = File.ReadAllText(SettingsPath);
                string config = File.ReadAllText(SettingsPath);
                check = true;

                int numLines = config.Split('\n').Length;
                string[] lines = File.ReadAllLines(SettingsPath);
                Console.WriteLine(String.Join(Environment.NewLine, lines));

                for (int i = 0; i <= numLines - 1; i++)
                {

                    if (lines[i].Contains("UseHeadphones"))
                    {
                        //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                        view = view.Replace(lines[i], "UseHeadphones=" + check);
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

                int numLines = config.Split('\n').Length;
                string[] lines = File.ReadAllLines(SettingsPath);
                Console.WriteLine(String.Join(Environment.NewLine, lines));

                for (int i = 0; i <= numLines - 1; i++)
                {

                    if (lines[i].Contains("UseHeadphones"))
                    {
                        //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                        view = view.Replace(lines[i], "UseHeadphones=" + check);
                        File.WriteAllText(SettingsPath, view);
                    }
                }
            }
        }

        private void btnSetFPS_Click(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            string read = File.ReadAllText(EnginePath);
            checkBox2.Checked = false;

            //File.WriteAllText(EnginePath, read);


            string engine = File.ReadAllText(EnginePath);
            engine = Regex.Replace(ReadEngine, @"\n\n", String.Empty);
            File.WriteAllText(EnginePath, engine);
            int numLines2 = engine.Split('\n').Length;
            string[] eng = File.ReadAllLines(EnginePath);
            Console.WriteLine(String.Join(Environment.NewLine, eng));

            for (int a = 0; a <= numLines2 - 1; a++)
            {
                if (eng[a].Contains("script/engine.engine"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("bSmoothFrameRate"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("MinSmoothedFrameRate"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("MaxSmoothedFrameRate"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                //Powyżej 120 fpsów
                if (eng[a].Contains("Engine/Plugins/Experimental/GeometryProcessing/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUICore/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Wwise/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUIMobile/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/2D/Paper2D/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Developer/TraceSourceFiltering/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/SentryIo/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/FX/Niagara/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Developer/AnimationSharing/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Editor/GeometryMode/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Experimental/ChaosClothEditor/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Experimental/ChaosNiagara/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeapPassableWorld/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeap/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Media/MediaCompositing/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/MovieScene/MovieRenderPipeline/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
            }

            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            if(numFPS.Value == 120)
            {
                for (int i = 0; i <= numLines - 1; i++)
                {
                    read = Regex.Replace(read, Environment.NewLine + Environment.NewLine, "\r");
                    if (lines[i].Contains("FrameRateLimit"))
                    {
                        view = view.Replace(lines[i], "FrameRateLimit=" + "150.000000");
                        File.WriteAllText(SettingsPath, view);
                    }
                }
            }
            if(numFPS.Value > 120)
            {
                for (int i = 0; i <= numLines - 1; i++)
                {            
                    read = Regex.Replace(read, Environment.NewLine + Environment.NewLine, "\r");
                    if (lines[i].Contains("FrameRateLimit"))
                    {
                        view = view.Replace(lines[i], "FrameRateLimit=" + "0.000000");
                        File.WriteAllText(SettingsPath, view);
                    }
                }
                engine += "\n\n\nPaths=../../../Engine/Plugins/Experimental/GeometryProcessing/Content\n" +
                    "Paths=../../../DeadByDaylight/Plugins/Runtime/Bhvr/DBDUICore/Content\n" +
                    "Paths=../../../DeadByDaylight/Plugins/Wwise/Content\n" +
                    "Paths=../../../DeadByDaylight/Plugins/Runtime/Bhvr/DBDUIMobile/Content\n" +
                    "Paths=../../../Engine/Plugins/2D/Paper2D/Content\n" +
                    "Paths=../../../Engine/Plugins/Developer/TraceSourceFiltering/Content\n" +
                    "Paths=../../../DeadByDaylight/Plugins/Runtime/Bhvr/SentryIo/Content\n" +
                    "Paths=../../../Engine/Plugins/FX/Niagara/Content\n" +
                    "Paths=../../../Engine/Plugins/Developer/AnimationSharing/Content\n" +
                    "Paths=../../../Engine/Plugins/Editor/GeometryMode/Content\n" +
                    "Paths=../../../Engine/Plugins/Experimental/ChaosClothEditor/Content\n" +
                    "Paths=../../../Engine/Plugins/Experimental/ChaosNiagara/Content\n" +
                    "Paths=../../../Engine/Plugins/MagicLeap/MagicLeapPassableWorld/Content\n" +
                    "Paths=../../../Engine/Plugins/MagicLeap/MagicLeap/Content\n" +
                    "Paths=../../../Engine/Plugins/Media/MediaCompositing/Content\n" +
                    "Paths=../../../Engine/Plugins/MovieScene/MovieRenderPipeline/Content\n";
                File.WriteAllText(EnginePath, engine);
            }
            if(numFPS.Value < 120)
            {
                for (int a = 0; a <= numLines2 - 1; a++)
                {
                    if (eng[a].Contains("script/engine.engine"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("bSmoothFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("MinSmoothedFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("MaxSmoothedFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    //Powyżej 120 fpsów
                    if (eng[a].Contains("Engine/Plugins/Experimental/GeometryProcessing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUICore/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Wwise/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUIMobile/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/2D/Paper2D/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Developer/TraceSourceFiltering/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/SentryIo/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/FX/Niagara/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Developer/AnimationSharing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Editor/GeometryMode/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Experimental/ChaosClothEditor/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Experimental/ChaosNiagara/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeapPassableWorld/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeap/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Media/MediaCompositing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MovieScene/MovieRenderPipeline/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                }
                for (int i = 0; i <= numLines - 1; i++)
                {
                    read = Regex.Replace(read, Environment.NewLine + Environment.NewLine, "\r");
                    if (lines[i].Contains("FrameRateLimit"))
                    {
                        //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                        if (numFPS.Value == 60 || numFPS.Value == 61 || numFPS.Value == 62)
                        {
                            view = view.Replace(lines[i], "FrameRateLimit=" + "0.000000");
                            File.WriteAllText(SettingsPath, view);
                        }
                        else
                        {
                            view = view.Replace(lines[i], "FrameRateLimit=" + numFPS.Value + ".000000");
                            File.WriteAllText(SettingsPath, view);
                        }
                    }
                }
            }

            if(numFPS.Value <= 5)
            {
                for (int a = 0; a <= numLines2 - 1; a++)
                {
                    if (eng[a].Contains("script/engine.engine"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("bSmoothFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("MinSmoothedFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("MaxSmoothedFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    //Powyżej 120 fpsów
                    if (eng[a].Contains("Engine/Plugins/Experimental/GeometryProcessing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUICore/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Wwise/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUIMobile/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/2D/Paper2D/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Developer/TraceSourceFiltering/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/SentryIo/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/FX/Niagara/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Developer/AnimationSharing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Editor/GeometryMode/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Experimental/ChaosClothEditor/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Experimental/ChaosNiagara/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeapPassableWorld/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeap/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Media/MediaCompositing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MovieScene/MovieRenderPipeline/Content"))
                    {
                        engine = engine.Replace(eng[a], "\r");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                }
                engine += "\n\n[/script/engine.engine]\nbSmoothFrameRate=False\nMinSmoothedFrameRate=1\nMaxSmoothedFrameRate=" + numFPS.Value;
                File.WriteAllText(EnginePath, engine);
            }

            if(numFPS.Value>=63 && numFPS.Value != 120)
            {
                for (int a = 0; a <= numLines2 - 1; a++)
                {
                    if (eng[a].Contains("script/engine.engine"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("bSmoothFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("MinSmoothedFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("MaxSmoothedFrameRate"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    //Powyżej 120 fpsów
                    if (eng[a].Contains("Engine/Plugins/Experimental/GeometryProcessing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUICore/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Wwise/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUIMobile/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/2D/Paper2D/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Developer/TraceSourceFiltering/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/SentryIo/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/FX/Niagara/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Developer/AnimationSharing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Editor/GeometryMode/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Experimental/ChaosClothEditor/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Experimental/ChaosNiagara/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeapPassableWorld/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeap/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/Media/MediaCompositing/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                    if (eng[a].Contains("Engine/Plugins/MovieScene/MovieRenderPipeline/Content"))
                    {
                        engine = engine.Replace(eng[a], "\n");
                        engine = Regex.Replace(engine, @"\n\n", String.Empty);
                        File.WriteAllText(EnginePath, engine);
                    }
                }
                engine += "\n\n[/script/engine.engine]\nbSmoothFrameRate=False\nMinSmoothedFrameRate=5\nMaxSmoothedFrameRate=" + numFPS.Value;
                File.WriteAllText(EnginePath, engine);
            }
        }

        private void btnResetFPS_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            lblHideFocus.Focus();
            numFPS.Value = 62;

            //Engine (poniżej)

            string engine = File.ReadAllText(EnginePath);
            engine = Regex.Replace(ReadEngine, @"\n\n", String.Empty);
            File.WriteAllText(EnginePath, engine);
            int numLines2 = engine.Split('\n').Length;
            //engine = Regex.Replace(engine, @"\n", String.Empty);
            string[] eng = File.ReadAllLines(EnginePath);
            Console.WriteLine(String.Join(Environment.NewLine, eng));

            for (int a = 0; a <= numLines2 - 1; a++)
            {
                if (eng[a].Contains("script/engine.engine"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("bSmoothFrameRate"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("MinSmoothedFrameRate"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("MaxSmoothedFrameRate"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                //Powyżej 120 fpsów
                if (eng[a].Contains("Engine/Plugins/Experimental/GeometryProcessing/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUICore/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Wwise/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/DBDUIMobile/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/2D/Paper2D/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Developer/TraceSourceFiltering/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("DeadByDaylight/Plugins/Runtime/Bhvr/SentryIo/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/FX/Niagara/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Developer/AnimationSharing/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Editor/GeometryMode/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Experimental/ChaosClothEditor/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Experimental/ChaosNiagara/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeapPassableWorld/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/MagicLeap/MagicLeap/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/Media/MediaCompositing/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
                if (eng[a].Contains("Engine/Plugins/MovieScene/MovieRenderPipeline/Content"))
                {
                    engine = engine.Replace(eng[a], "\r");
                    engine = Regex.Replace(engine, @"\n\n", String.Empty);
                    File.WriteAllText(EnginePath, engine);
                }
            }

            //GameUserSettings (poniżej)

            string config = File.ReadAllText(SettingsPath);
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));
            int numLines = config.Split('\n').Length;
            string view = File.ReadAllText(SettingsPath);

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("FrameRateLimit"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    if (numFPS.Value == 60 || numFPS.Value == 61 || numFPS.Value == 62)
                    {
                        view = view.Replace(lines[i], "FrameRateLimit=" + "0.000000");
                        view = Regex.Replace(view, Environment.NewLine + Environment.NewLine, "\r");
                        File.WriteAllText(SettingsPath, view);
                    }
                }
            }
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
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ResolutionSizeX") && !lines[i].Contains("LastUser"))
                {
                    view = view.Replace(lines[i], "ResolutionSizeX=" + numWidth.Value);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("ResolutionSizeY") && !lines[i].Contains("LastUser"))
                {
                    view = view.Replace(lines[i], "ResolutionSizeY=" + numHeight.Value);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeX"))
                {
                    view = view.Replace(lines[i], "LastUserConfirmedResolutionSizeX=" + numWidth.Value);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeY"))
                {
                    view = view.Replace(lines[i], "LastUserConfirmedResolutionSizeY=" + numHeight.Value);
                    File.WriteAllText(SettingsPath, view);
                }


                if (lines[i].Contains("FullscreenMode") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Prefered"))
                {
                    view = view.Replace(lines[i], "FullscreenMode=" + 0);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastConfirmedFullscreenMode") && !lines[i].Contains("Prefered"))
                {
                    view = view.Replace(lines[i], "LastConfirmedFullscreenMode=" + 0);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("PreferredFullscreenMode") && !lines[i].Contains("LastConfirmed"))
                {
                    view = view.Replace(lines[i], "PreferredFullscreenMode=" + 0);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnResReset_Click(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            string view = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            numWidth.Value = 1920;
            numHeight.Value = 1080;
            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ResolutionSizeX") && !lines[i].Contains("LastUser"))
                {
                    view = view.Replace(lines[i], "ResolutionSizeX=" + 1920);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("ResolutionSizeY") && !lines[i].Contains("LastUser"))
                {
                    view = view.Replace(lines[i], "ResolutionSizeY=" + 1080);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeX"))
                {
                    view = view.Replace(lines[i], "LastUserConfirmedResolutionSizeX=" + 1920);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastUserConfirmedResolutionSizeY"))
                {
                    view = view.Replace(lines[i], "LastUserConfirmedResolutionSizeY=" + 1080);
                    File.WriteAllText(SettingsPath, view);
                }


                if (lines[i].Contains("FullscreenMode") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Prefered"))
                {
                    view = view.Replace(lines[i], "FullscreenMode=" + 1);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("LastConfirmedFullscreenMode") && !lines[i].Contains("Prefered"))
                {
                    view = view.Replace(lines[i], "LastConfirmedFullscreenMode=" + 1);
                    File.WriteAllText(SettingsPath, view);
                }
                if (lines[i].Contains("PreferredFullscreenMode") && !lines[i].Contains("LastConfirmed"))
                {
                    view = view.Replace(lines[i], "PreferredFullscreenMode=" + 1);
                    File.WriteAllText(SettingsPath, view);
                }
            }
        }

        private void btnPresetLow_Click(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();

            int numLines = ReadConfig.Split('\n').Length;

            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));




            for (int i = 0; i <= numLines - 1; i++)
            {
                if (lines[i].Contains("sg.ResolutionQuality"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ResolutionQuality=1.000000");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;
                    tbRes.Value = result;
                    lblRes.Text = tbRes.Value + "%";
                }
                if (lines[i].Contains("sg.ViewDistanceQuality"))
                {
                    btnVwLow.BackColor = Color.Transparent;
                    btnVwLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnVwHigh.BackColor = Color.Transparent;
                    btnVwHigh.ForeColor = Color.Black;
                    btnVwUltra.BackColor = Color.Transparent;
                    btnVwUltra.ForeColor = Color.Black;
                    btnVwEpic.BackColor = Color.Transparent;
                    btnVwEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnVwMedium.BackColor = Color.Transparent;
                    btnVwMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ViewDistanceQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnVwEpic.BackColor = Color.Crimson;
                        btnVwEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AntiAliasingQuality"))
                {
                    btnAaLow.BackColor = Color.Transparent;
                    btnAaLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnAaHigh.BackColor = Color.Transparent;
                    btnAaHigh.ForeColor = Color.Black;
                    btnAaUltra.BackColor = Color.Transparent;
                    btnAaUltra.ForeColor = Color.Black;
                    btnAaEpic.BackColor = Color.Transparent;
                    btnAaEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnAaMedium.BackColor = Color.Transparent;
                    btnAaMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.AntiAliasingQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnAaEpic.BackColor = Color.Crimson;
                        btnAaEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadowQuality"))
                {
                    btnShadLow.BackColor = Color.Transparent;
                    btnShadLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnShadHigh.BackColor = Color.Transparent;
                    btnShadHigh.ForeColor = Color.Black;
                    btnShadUltra.BackColor = Color.Transparent;
                    btnShadUltra.ForeColor = Color.Black;
                    btnShadEpic.BackColor = Color.Transparent;
                    btnShadEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnShadMedium.BackColor = Color.Transparent;
                    btnShadMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ShadowQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnShadEpic.BackColor = Color.Crimson;
                        btnShadEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.PostProcessQuality"))
                {
                    btnPpLow.BackColor = Color.Transparent;
                    btnPpLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnPpHigh.BackColor = Color.Transparent;
                    btnPpHigh.ForeColor = Color.Black;
                    btnPpUltra.BackColor = Color.Transparent;
                    btnPpUltra.ForeColor = Color.Black;
                    btnPpEpic.BackColor = Color.Transparent;
                    btnPpEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnPpMedium.BackColor = Color.Transparent;
                    btnPpMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.PostProcessQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnPpEpic.BackColor = Color.Crimson;
                        btnPpEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.TextureQuality"))
                {
                    btnTxtLow.BackColor = Color.Transparent;
                    btnTxtLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnTxtHigh.BackColor = Color.Transparent;
                    btnTxtHigh.ForeColor = Color.Black;
                    btnTxtUltra.BackColor = Color.Transparent;
                    btnTxtUltra.ForeColor = Color.Black;
                    btnTxtEpic.BackColor = Color.Transparent;
                    btnTxtEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnTxtMedium.BackColor = Color.Transparent;
                    btnTxtMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.TextureQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnTxtEpic.BackColor = Color.Crimson;
                        btnTxtEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.EffectsQuality"))
                {
                    btnEffLow.BackColor = Color.Transparent;
                    btnEffLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnEffHigh.BackColor = Color.Transparent;
                    btnEffHigh.ForeColor = Color.Black;
                    btnEffUltra.BackColor = Color.Transparent;
                    btnEffUltra.ForeColor = Color.Black;
                    btnEffEpic.BackColor = Color.Transparent;
                    btnEffEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnEffMedium.BackColor = Color.Transparent;
                    btnEffMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.EffectsQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnEffEpic.BackColor = Color.Crimson;
                        btnEffEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.FoliageQuality"))
                {
                    btnFolLow.BackColor = Color.Transparent;
                    btnFolLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnFolHigh.BackColor = Color.Transparent;
                    btnFolHigh.ForeColor = Color.Black;
                    btnFolUltra.BackColor = Color.Transparent;
                    btnFolUltra.ForeColor = Color.Black;
                    btnFolEpic.BackColor = Color.Transparent;
                    btnFolEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnFolMedium.BackColor = Color.Transparent;
                    btnFolMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.FoliageQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnFolEpic.BackColor = Color.Crimson;
                        btnFolEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadingQuality"))
                {
                    btnShLow.BackColor = Color.Transparent;
                    btnShLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnShHigh.BackColor = Color.Transparent;
                    btnShHigh.ForeColor = Color.Black;
                    btnShUltra.BackColor = Color.Transparent;
                    btnShUltra.ForeColor = Color.Black;
                    btnShEpic.BackColor = Color.Transparent;
                    btnShEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnShMedium.BackColor = Color.Transparent;
                    btnShMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ShadingQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnShEpic.BackColor = Color.Crimson;
                        btnShEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AnimationQuality"))
                {
                    btnAnimLow.BackColor = Color.Transparent;
                    btnAnimLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnAnimHigh.BackColor = Color.Transparent;
                    btnAnimHigh.ForeColor = Color.Black;
                    btnAnimUltra.BackColor = Color.Transparent;
                    btnAnimUltra.ForeColor = Color.Black;
                    btnAnimEpic.BackColor = Color.Transparent;
                    btnAnimEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnAnimMedium.BackColor = Color.Transparent;
                    btnAnimMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.AnimationQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnAnimEpic.BackColor = Color.Crimson;
                        btnAnimEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("ScreenResolution"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "ScreenResolution=1");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;
                    tbScRes.Value = result;
                    lblScRes.Text = tbScRes.Value + "%";
                }
            }
        }

        private void btnPresetMedium_Click(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            int numLines = ReadConfig.Split('\n').Length;

            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));




            for (int i = 0; i <= numLines - 1; i++)
            {
                if (lines[i].Contains("sg.ResolutionQuality"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ResolutionQuality=67.000000");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 67;
                    tbRes.Value = result;
                    lblRes.Text = tbRes.Value + "%";
                }
                if (lines[i].Contains("sg.ViewDistanceQuality"))
                {
                    btnVwLow.BackColor = Color.Transparent;
                    btnVwLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnVwHigh.BackColor = Color.Transparent;
                    btnVwHigh.ForeColor = Color.Black;
                    btnVwUltra.BackColor = Color.Transparent;
                    btnVwUltra.ForeColor = Color.Black;
                    btnVwEpic.BackColor = Color.Transparent;
                    btnVwEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnVwMedium.BackColor = Color.Transparent;
                    btnVwMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ViewDistanceQuality=1");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;

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
                        btnVwEpic.BackColor = Color.Crimson;
                        btnVwEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AntiAliasingQuality"))
                {
                    btnAaLow.BackColor = Color.Transparent;
                    btnAaLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnAaHigh.BackColor = Color.Transparent;
                    btnAaHigh.ForeColor = Color.Black;
                    btnAaUltra.BackColor = Color.Transparent;
                    btnAaUltra.ForeColor = Color.Black;
                    btnAaEpic.BackColor = Color.Transparent;
                    btnAaEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnAaMedium.BackColor = Color.Transparent;
                    btnAaMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.AntiAliasingQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 0;

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
                        btnAaEpic.BackColor = Color.Crimson;
                        btnAaEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadowQuality"))
                {
                    btnShadLow.BackColor = Color.Transparent;
                    btnShadLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnShadHigh.BackColor = Color.Transparent;
                    btnShadHigh.ForeColor = Color.Black;
                    btnShadUltra.BackColor = Color.Transparent;
                    btnShadUltra.ForeColor = Color.Black;
                    btnShadEpic.BackColor = Color.Transparent;
                    btnShadEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnShadMedium.BackColor = Color.Transparent;
                    btnShadMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ShadowQuality=1");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;

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
                        btnShadEpic.BackColor = Color.Crimson;
                        btnShadEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.PostProcessQuality"))
                {
                    btnPpLow.BackColor = Color.Transparent;
                    btnPpLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnPpHigh.BackColor = Color.Transparent;
                    btnPpHigh.ForeColor = Color.Black;
                    btnPpUltra.BackColor = Color.Transparent;
                    btnPpUltra.ForeColor = Color.Black;
                    btnPpEpic.BackColor = Color.Transparent;
                    btnPpEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnPpMedium.BackColor = Color.Transparent;
                    btnPpMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.PostProcessQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnPpEpic.BackColor = Color.Crimson;
                        btnPpEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.TextureQuality"))
                {
                    btnTxtLow.BackColor = Color.Transparent;
                    btnTxtLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnTxtHigh.BackColor = Color.Transparent;
                    btnTxtHigh.ForeColor = Color.Black;
                    btnTxtUltra.BackColor = Color.Transparent;
                    btnTxtUltra.ForeColor = Color.Black;
                    btnTxtEpic.BackColor = Color.Transparent;
                    btnTxtEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnTxtMedium.BackColor = Color.Transparent;
                    btnTxtMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.TextureQuality=0");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;

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
                        btnTxtEpic.BackColor = Color.Crimson;
                        btnTxtEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.EffectsQuality"))
                {
                    btnEffLow.BackColor = Color.Transparent;
                    btnEffLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnEffHigh.BackColor = Color.Transparent;
                    btnEffHigh.ForeColor = Color.Black;
                    btnEffUltra.BackColor = Color.Transparent;
                    btnEffUltra.ForeColor = Color.Black;
                    btnEffEpic.BackColor = Color.Transparent;
                    btnEffEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnEffMedium.BackColor = Color.Transparent;
                    btnEffMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.EffectsQuality=1");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;

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
                        btnEffEpic.BackColor = Color.Crimson;
                        btnEffEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.FoliageQuality"))
                {
                    btnFolLow.BackColor = Color.Transparent;
                    btnFolLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnFolHigh.BackColor = Color.Transparent;
                    btnFolHigh.ForeColor = Color.Black;
                    btnFolUltra.BackColor = Color.Transparent;
                    btnFolUltra.ForeColor = Color.Black;
                    btnFolEpic.BackColor = Color.Transparent;
                    btnFolEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnFolMedium.BackColor = Color.Transparent;
                    btnFolMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.FoliageQuality=1");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;

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
                        btnFolEpic.BackColor = Color.Crimson;
                        btnFolEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadingQuality"))
                {
                    btnShLow.BackColor = Color.Transparent;
                    btnShLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnShHigh.BackColor = Color.Transparent;
                    btnShHigh.ForeColor = Color.Black;
                    btnShUltra.BackColor = Color.Transparent;
                    btnShUltra.ForeColor = Color.Black;
                    btnShEpic.BackColor = Color.Transparent;
                    btnShEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnShMedium.BackColor = Color.Transparent;
                    btnShMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ShadingQuality=1");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;

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
                        btnShEpic.BackColor = Color.Crimson;
                        btnShEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AnimationQuality"))
                {
                    btnAnimLow.BackColor = Color.Transparent;
                    btnAnimLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnAnimHigh.BackColor = Color.Transparent;
                    btnAnimHigh.ForeColor = Color.Black;
                    btnAnimUltra.BackColor = Color.Transparent;
                    btnAnimUltra.ForeColor = Color.Black;
                    btnAnimEpic.BackColor = Color.Transparent;
                    btnAnimEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnAnimMedium.BackColor = Color.Transparent;
                    btnAnimMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.AnimationQuality=1");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 1;

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
                        btnAnimEpic.BackColor = Color.Crimson;
                        btnAnimEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("ScreenResolution"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "ScreenResolution=100");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 100;
                    tbScRes.Value = result;
                    lblScRes.Text = tbScRes.Value + "%";
                }
            }
        }

        private void btnPresetEpic_Click(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            lblHideFocus.Focus();

            int numLines = ReadConfig.Split('\n').Length;

            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));




            for (int i = 0; i <= numLines - 1; i++)
            {
                if (lines[i].Contains("sg.ResolutionQuality"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ResolutionQuality=999.000000");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 999;
                    tbRes.Value = result;
                    lblRes.Text = tbRes.Value + "%";
                }
                if (lines[i].Contains("sg.ViewDistanceQuality"))
                {
                    btnVwLow.BackColor = Color.Transparent;
                    btnVwLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnVwHigh.BackColor = Color.Transparent;
                    btnVwHigh.ForeColor = Color.Black;
                    btnVwUltra.BackColor = Color.Transparent;
                    btnVwUltra.ForeColor = Color.Black;
                    btnVwEpic.BackColor = Color.Transparent;
                    btnVwEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnVwMedium.BackColor = Color.Transparent;
                    btnVwMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ViewDistanceQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnVwEpic.BackColor = Color.Crimson;
                        btnVwEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AntiAliasingQuality"))
                {
                    btnAaLow.BackColor = Color.Transparent;
                    btnAaLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnAaHigh.BackColor = Color.Transparent;
                    btnAaHigh.ForeColor = Color.Black;
                    btnAaUltra.BackColor = Color.Transparent;
                    btnAaUltra.ForeColor = Color.Black;
                    btnAaEpic.BackColor = Color.Transparent;
                    btnAaEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnAaMedium.BackColor = Color.Transparent;
                    btnAaMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.AntiAliasingQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnAaEpic.BackColor = Color.Crimson;
                        btnAaEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadowQuality"))
                {
                    btnShadLow.BackColor = Color.Transparent;
                    btnShadLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnShadHigh.BackColor = Color.Transparent;
                    btnShadHigh.ForeColor = Color.Black;
                    btnShadUltra.BackColor = Color.Transparent;
                    btnShadUltra.ForeColor = Color.Black;
                    btnShadEpic.BackColor = Color.Transparent;
                    btnShadEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnShadMedium.BackColor = Color.Transparent;
                    btnShadMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ShadowQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnShadEpic.BackColor = Color.Crimson;
                        btnShadEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.PostProcessQuality"))
                {
                    btnPpLow.BackColor = Color.Transparent;
                    btnPpLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnPpHigh.BackColor = Color.Transparent;
                    btnPpHigh.ForeColor = Color.Black;
                    btnPpUltra.BackColor = Color.Transparent;
                    btnPpUltra.ForeColor = Color.Black;
                    btnPpEpic.BackColor = Color.Transparent;
                    btnPpEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnPpMedium.BackColor = Color.Transparent;
                    btnPpMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.PostProcessQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnPpEpic.BackColor = Color.Crimson;
                        btnPpEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.TextureQuality"))
                {
                    btnTxtLow.BackColor = Color.Transparent;
                    btnTxtLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnTxtHigh.BackColor = Color.Transparent;
                    btnTxtHigh.ForeColor = Color.Black;
                    btnTxtUltra.BackColor = Color.Transparent;
                    btnTxtUltra.ForeColor = Color.Black;
                    btnTxtEpic.BackColor = Color.Transparent;
                    btnTxtEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnTxtMedium.BackColor = Color.Transparent;
                    btnTxtMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.TextureQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnTxtEpic.BackColor = Color.Crimson;
                        btnTxtEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.EffectsQuality"))
                {
                    btnEffLow.BackColor = Color.Transparent;
                    btnEffLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnEffHigh.BackColor = Color.Transparent;
                    btnEffHigh.ForeColor = Color.Black;
                    btnEffUltra.BackColor = Color.Transparent;
                    btnEffUltra.ForeColor = Color.Black;
                    btnEffEpic.BackColor = Color.Transparent;
                    btnEffEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnEffMedium.BackColor = Color.Transparent;
                    btnEffMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.EffectsQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnEffEpic.BackColor = Color.Crimson;
                        btnEffEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.FoliageQuality"))
                {
                    btnFolLow.BackColor = Color.Transparent;
                    btnFolLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnFolHigh.BackColor = Color.Transparent;
                    btnFolHigh.ForeColor = Color.Black;
                    btnFolUltra.BackColor = Color.Transparent;
                    btnFolUltra.ForeColor = Color.Black;
                    btnFolEpic.BackColor = Color.Transparent;
                    btnFolEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnFolMedium.BackColor = Color.Transparent;
                    btnFolMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.FoliageQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnFolEpic.BackColor = Color.Crimson;
                        btnFolEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.ShadingQuality"))
                {
                    btnShLow.BackColor = Color.Transparent;
                    btnShLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnShHigh.BackColor = Color.Transparent;
                    btnShHigh.ForeColor = Color.Black;
                    btnShUltra.BackColor = Color.Transparent;
                    btnShUltra.ForeColor = Color.Black;
                    btnShEpic.BackColor = Color.Transparent;
                    btnShEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnShMedium.BackColor = Color.Transparent;
                    btnShMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.ShadingQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);
                    
                    int result = 5;

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
                        btnShEpic.BackColor = Color.Crimson;
                        btnShEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("sg.AnimationQuality"))
                {
                    btnAnimLow.BackColor = Color.Transparent;
                    btnAnimLow.ForeColor = Color.Black;
                    //nazwa wcześniejszego
                    btnAnimHigh.BackColor = Color.Transparent;
                    btnAnimHigh.ForeColor = Color.Black;
                    btnAnimUltra.BackColor = Color.Transparent;
                    btnAnimUltra.ForeColor = Color.Black;
                    btnAnimEpic.BackColor = Color.Transparent;
                    btnAnimEpic.ForeColor = Color.Black;
                    //nazwa klikniętego (poniżej)
                    btnAnimMedium.BackColor = Color.Transparent;
                    btnAnimMedium.ForeColor = Color.Black;
                    ReadConfig = ReadConfig.Replace(lines[i], "sg.AnimationQuality=5");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 5;

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
                        btnAnimEpic.BackColor = Color.Crimson;
                        btnAnimEpic.ForeColor = Color.White;
                    }
                }
                if (lines[i].Contains("ScreenResolution"))
                {
                    ReadConfig = ReadConfig.Replace(lines[i], "ScreenResolution=300");
                    File.WriteAllText(SettingsPath, ReadConfig);

                    int result = 300;
                    tbScRes.Value = result;
                    lblScRes.Text = tbScRes.Value + "%";
                }
            }
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
            tip.Show("Reset game resolution to default.", btnResReset);
        }

        private void btnResReset_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResReset);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            bool check;
            if (checkBox2.Checked)
            {
                lblHideFocus.Focus();
                string view = File.ReadAllText(SettingsPath);
                string config = File.ReadAllText(SettingsPath);
                check = true;

                int numLines = config.Split('\n').Length;
                string[] lines = File.ReadAllLines(SettingsPath);
                Console.WriteLine(String.Join(Environment.NewLine, lines));

                for (int i = 0; i <= numLines - 1; i++)
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

                int numLines = config.Split('\n').Length;
                string[] lines = File.ReadAllLines(SettingsPath);
                Console.WriteLine(String.Join(Environment.NewLine, lines));

                for (int i = 0; i <= numLines - 1; i++)
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
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string audio = File.ReadAllText(SettingsPath);
            string sens = File.ReadAllText(SettingsPath);
            int sensitivity = KillerMouse.Value;
            lblKillerMouse.Text = sensitivity.ToString() + "%";

            int numLines = sens.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("KillerMouseSensitivity"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    audio = audio.Replace(lines[i], "KillerMouseSensitivity=" + sensitivity);
                    File.WriteAllText(SettingsPath, audio);
                }
            }
        }

        private void KillerController_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string audio = File.ReadAllText(SettingsPath);
            string sens = File.ReadAllText(SettingsPath);
            int sensitivity = KillerController.Value;
            lblKillerController.Text = sensitivity.ToString() + "%";

            int numLines = sens.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("KillerControllerSensitivity"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    audio = audio.Replace(lines[i], "KillerControllerSensitivity=" + sensitivity);
                    File.WriteAllText(SettingsPath, audio);
                }
            }
        }

        private void SurvMouse_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string audio = File.ReadAllText(SettingsPath);
            string sens = File.ReadAllText(SettingsPath);
            int sensitivity = SurvMouse.Value;
            lblSurvMouse.Text = sensitivity.ToString() + "%";

            int numLines = sens.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("SurvivorMouseSensitivity"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    audio = audio.Replace(lines[i], "SurvivorMouseSensitivity=" + sensitivity);
                    File.WriteAllText(SettingsPath, audio);
                }
            }
        }

        private void SurvController_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string audio = File.ReadAllText(SettingsPath);
            string sens = File.ReadAllText(SettingsPath);
            int sensitivity = SurvController.Value;
            lblSurvController.Text = sensitivity.ToString() + "%";

            int numLines = sens.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("SurvivorControllerSensitivity"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    audio = audio.Replace(lines[i], "SurvivorControllerSensitivity=" + sensitivity);
                    File.WriteAllText(SettingsPath, audio);
                }
            }
        }

        private void tbScRes_Scroll(object sender, EventArgs e)
        {
            ReadConfig = Regex.Replace(ReadConfig, Environment.NewLine + Environment.NewLine, "\r");

            string res = File.ReadAllText(SettingsPath);
            string config = File.ReadAllText(SettingsPath);
            int resolution = tbScRes.Value;
            lblScRes.Text = resolution.ToString() + "%";

            int numLines = config.Split('\n').Length;
            string[] lines = File.ReadAllLines(SettingsPath);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            for (int i = 0; i <= numLines - 1; i++)
            {

                if (lines[i].Contains("ScreenResolution"))
                {
                    //lines[i] = "sg.ResolutionQuality=" + resolution + "0.000000";
                    res = res.Replace(lines[i], "ScreenResolution=" + resolution);
                    File.WriteAllText(SettingsPath, res);
                }
            }
        }

        private void btnDelete_MouseHover(object sender, EventArgs e)
        {
            tip.Show("Delete the backup configuration file and create a new one.", btnDelete);
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.Transparent;
            tip.Hide(btnDelete);
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.Crimson;
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
    }
}
