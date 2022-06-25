using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace DbD_Settings_Changer
{
    public partial class Form1 : Form
    {
        private void resetColors()
        {
            foreach(Control panels in panel10.Controls)
            {
                if(panels.Name.Contains("panel"))
                {
                    foreach (Control control in panels.Controls)
                    {
                        if(control.Name.Contains("btn") && !control.Name.Contains("Discord") && !control.Name.Contains("UsersSettings") && !control.Name.Contains("Delete"))
                        {
                            control.BackColor = Color.FromArgb(224, 224, 224);
                            control.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private void readValues(string SettingsPath, string EnginePath)
        {
            resetColors();
            string ReadConfig = File.ReadAllText(SettingsPath);

            int numSettingsLines = File.ReadLines(SettingsPath).Count();
            int numEngineLines = File.ReadLines(EnginePath).Count();

            string[] settingsLines = File.ReadAllLines(SettingsPath);

            string[] engineLines = File.ReadAllLines(EnginePath);

            for (int i = 0; i < numSettingsLines; i++)
            {
                if (settingsLines[i].Contains("FrameRateLimit="))
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "sg.ResolutionQuality=" + 100 + ".000000");
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbRes.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "sg.ResolutionQuality=" + 999 + ".000000");
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 999;
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "MainVolume=" + 0);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 0;
                    }

                    if (result > tbVolume.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "MainVolume=" + 200);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 200;
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "MenuMusicVolume=" + 0);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 0;
                    }

                    if (result > tbMusic.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "MenuMusicVolume=" + 200);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 200;
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "KillerMouseSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbKillerMouse.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "KillerMouseSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "SurvivorMouseSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbSurvMouse.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "SurvivorMouseSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "KillerControllerSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbKillerController.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "KillerControllerSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
                    }
                    tbKillerController.Value = result;
                    lblKillerController.Text = tbKillerController.Value + "%";
                }
                if (settingsLines[i].Contains("SurvivorControllerSensitivity="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);

                    if (result < tbSurvController.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "SurvivorControllerSensitivity=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbSurvController.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "SurvivorControllerSensitivity=" + 1000);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 1000;
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "ResolutionSizeX=" + 1920);
                        File.WriteAllText(SettingsPath, ReadConfig);
                    }

                    if (result < numWidth.Minimum)
                    {
                        result = 1920;
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "ResolutionSizeX=" + 1920);
                        File.WriteAllText(SettingsPath, ReadConfig);
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
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "ResolutionSizeY=" + 1080);
                        File.WriteAllText(SettingsPath, ReadConfig);
                    }

                    if (result < numHeight.Minimum)
                    {
                        result = 1080;
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "ResolutionSizeY=" + 1080);
                        File.WriteAllText(SettingsPath, ReadConfig);
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

                if (settingsLines[i].Contains("ScreenResolution="))
                {
                    string resultString = Regex.Match(settingsLines[i], @"\d+").Value;
                    int result = int.Parse(resultString);
                    if (result < tbScRes.Minimum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "ScreenResolution=" + 100);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 100;
                    }

                    if (result > tbScRes.Maximum)
                    {
                        ReadConfig = ReadConfig.Replace(settingsLines[i], "ScreenResolution=" + 300);
                        File.WriteAllText(SettingsPath, ReadConfig);
                        result = 300;
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
                ReadConfig = File.ReadAllText(SettingsPath);
                ReadEngine = File.ReadAllText(EnginePath);



                File.WriteAllText(SettingsPath, ReadConfig);
                File.WriteAllText(EnginePath, ReadEngine);
            }
        }
    }
}
