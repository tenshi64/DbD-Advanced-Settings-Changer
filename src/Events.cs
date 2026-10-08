using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DbD_Settings_Changer
{
    public partial class Main : Form
    {
        private void MenuColors(string avoidedName)
        {
            foreach (Control control in panel10.Controls)
            {
                if (control.Name.Contains("panel", StringComparison.OrdinalIgnoreCase))
                {
                    if (!control.Name.Contains(avoidedName, StringComparison.OrdinalIgnoreCase) && !control.Name.Contains("Discord", StringComparison.OrdinalIgnoreCase) && !control.Name.Contains("User", StringComparison.OrdinalIgnoreCase))
                    {
                        control.Hide();
                    }
                    if (control.Name.Contains(avoidedName, StringComparison.OrdinalIgnoreCase) && !control.Name.Contains("Discord", StringComparison.OrdinalIgnoreCase) && !control.Name.Contains("User", StringComparison.OrdinalIgnoreCase))
                    {
                        control.Show();
                    }
                }
                if (control.Name == "Nav")
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is Label label)
                        {
                            if (!label.Name.Contains(avoidedName, StringComparison.OrdinalIgnoreCase) && label.Name.Contains("menu", StringComparison.OrdinalIgnoreCase))
                            {
                                label.BackColor = Color.FromArgb(68, 80, 102);
                                label.Location = new Point(label.Location.X, 13);
                                label.Size = new Size(label.Size.Width, 40);
                            }
                            if (label.Name.Contains(avoidedName, StringComparison.OrdinalIgnoreCase) && label.Name.Contains("menu", StringComparison.OrdinalIgnoreCase))
                            {
                                label.BackColor = Color.Crimson;
                                label.Location = new Point(label.Location.X, 6);
                                label.Size = new Size(label.Size.Width, 44);
                            }
                        }
                    }
                }
            }
        }

        private void MenuClick(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                if (label.Name.Contains("Audio", StringComparison.Ordinal) ||
                    label.Name.Contains("Graphics", StringComparison.Ordinal) ||
                    label.Name.Contains("FPS", StringComparison.Ordinal) ||
                    label.Name.Contains("Presets", StringComparison.Ordinal) ||
                    label.Name.Contains("Sensitivity", StringComparison.Ordinal) ||
                    label.Name.Contains("Game", StringComparison.Ordinal))
                {
                    MenuColors(label.Name[4..]);
                }
            }
        }

        private static int GetValue(object sender) =>
            (sender as Button)?.Name switch
            {
                string name when name.Contains("Low", StringComparison.Ordinal) => 0,
                string name when name.Contains("Medium", StringComparison.Ordinal) => 1,
                string name when name.Contains("High", StringComparison.Ordinal) => 2,
                string name when name.Contains("Ultra", StringComparison.Ordinal) => 3,
                string name when name.Contains("Epic", StringComparison.Ordinal) => 4,
                string name when name.Contains("Awesome", StringComparison.Ordinal) => 5,
                _ => 0
            };

        private static void ChangeButtonColor(object sender, string name, string avoidedName)
        {
            if (sender is Button currentBtn && currentBtn.Parent != null)
            {
                foreach (Button btn in currentBtn.Parent.Controls.OfType<Button>())
                {
                    if (!btn.Name.Contains(avoidedName, StringComparison.Ordinal) && btn.Name.Contains(name, StringComparison.Ordinal) && btn != currentBtn)
                    {
                        btn.BackColor = Color.FromArgb(255, 255, 255);
                        btn.ForeColor = Color.Black;
                    }
                    if (btn == currentBtn)
                    {
                        btn.BackColor = Color.Crimson;
                        btn.ForeColor = Color.White;
                    }
                }
            }
        }

        private void CbCheckedChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if (sender is not CheckBox cb) return;

            if (cb.Name.Contains("SurvivorToggle", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "SurvivorToggleInteractions=", cbSurvivorToggle.Checked.ToString());
            }
            if (cb.Name.Contains("KillerToggle", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "KillerToggleInteractions=", cbKillerToggle.Checked.ToString());
            }
            if (cb.Name.Contains("Vsync", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "bUseVSync=", cbVsync.Checked.ToString());
            }
            if (cb.Name.Contains("Headphones", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "UseHeadphones=", cbHeadphones.Checked.ToString());
            }
            if (cb.Name.Contains("MuteFocusLost", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "MuteOnFocusLost=", cbMuteFocusLost.Checked.ToString());
            }
            if (cb.Name.Contains("LegacyPrestige", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "LegacyPrestigePortraits=", cbLegacyPrestige.Checked.ToString());
            }
            if (cb.Name.Contains("HapticsVibration", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "HapticsVibrationPS5=", cbHapticsVibration.Checked.ToString());
            }
            if (cb.Name.Contains("SprintToCancel", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "SprintToCancel=", cbSprintToCancel.Checked.ToString());
            }
            if (cb.Name.Contains("HeartIcon", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "TerrorRadiusVisualFeedback=", cbHeartIcon.Checked.ToString());
            }
            if (cb.Name.Contains("Copyright", StringComparison.Ordinal))
            {
                ChangeConfig.GameUserSettings(SettingsPath, "AllowCopyrightedMusic=", cbCopyright.Checked.ToString());
            }
        }

        private void ChangeSensitivityOrResolutionOrAudio(TrackBar tb, Label lbl, string line)
        {
            if (tb.Name == "tbRes")
            {
                int value = tb.Value;
                lbl.Text = value.ToString() + "%";
                string stringValue = value.ToString() + ".000000";

                ChangeConfig.GameUserSettings(SettingsPath, line, stringValue);
            }
            else
            {
                int value = tb.Value;
                if (lbl.Name.Contains("FOV", StringComparison.Ordinal))
                {
                    lbl.Text = value.ToString() + "°";
                }
                else
                {
                    lbl.Text = value.ToString() + "%";
                }

                ChangeConfig.GameUserSettings(SettingsPath, line, value.ToString());
            }
        }

        private void TbScroll(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if (sender is not TrackBar tb) return;

            if (tb.Name.Contains("Res", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbRes, lblRes, "sg.ResolutionQuality=");
            }
            if (tb.Name.Contains("KillerMouse", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbKillerMouse, lblKillerMouse, "KillerMouseSensitivity=");
            }
            if (tb.Name.Contains("KillerController", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbKillerController, lblKillerController, "KillerControllerSensitivity=");
            }
            if (tb.Name.Contains("SurvMouse", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbSurvMouse, lblSurvMouse, "SurvivorMouseSensitivity=");
            }
            if (tb.Name.Contains("SurvController", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbSurvController, lblSurvController, "SurvivorControllerSensitivity=");
            }
            if (tb.Name.Contains("ScRes", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbScRes, lblScRes, "ScreenRenderSize=");
            }
            if (tb.Name.Contains("tbVolume", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbVolume, lblMainVolume, "MainVolume=");
            }
            if (tb.Name.Contains("tbMusic", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbMusic, lblMenu, "MenuMusicVolume=");
            }
            if (tb.Name.Contains("tbFOV", StringComparison.Ordinal))
            {
                ChangeSensitivityOrResolutionOrAudio(tbFOV, lblFOV, "FieldOfView=");
            }
        }

        private void BtnClick(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if (sender is not Button btn) return;

            if (btn.Name.Contains("Aa", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Aa", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", GetValue(btn).ToString());
                if (!btn.Name.Contains("Disable", StringComparison.Ordinal))
                {
                    ChangeConfig.GameUserSettings(SettingsPath, "AntiAliasingMode=", 2.ToString());
                }
                else
                {
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "AntiAliasingMode=", 0.ToString());
                }
            }
            if (btn.Name.Contains("Pp", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Pp", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Vw", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Vw", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Shad", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Shad", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Txt", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Txt", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Eff", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Eff", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Fol", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Fol", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", GetValue(btn).ToString());
            }
            if (!btn.Name.Contains("Shad", StringComparison.Ordinal) && btn.Name.Contains("Sh", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Sh", "Shad");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Anim", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Anim", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Audio", StringComparison.Ordinal))
            {
                ChangeButtonColor(btn, "Audio", " ");
                ChangeConfig.GameUserSettings(SettingsPath, "AudioQualityLevel=", GetValue(btn).ToString());
            }
            if (btn.Name.Contains("Preset", StringComparison.Ordinal))
            {
                if (btn.Name.Contains("Low", StringComparison.Ordinal))
                {
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", 1.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 0.ToString());
                    ChangeConfig.EnableAntiAliasing(EnginePath);
                    ChangeConfig.DisableAntiAliasing(EnginePath);
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "ScreenRenderSize=", 1.ToString());

                    ReadAndFixAllValues();
                }
                if (btn.Name.Contains("Balanced", StringComparison.Ordinal))
                {
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", 100.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", 3.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", 3.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", 2.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", 2.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", 0.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", 2.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "ScreenRenderSize=", 100.ToString());

                    ReadAndFixAllValues();
                }
                if (btn.Name.Contains("Epic", StringComparison.Ordinal))
                {
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ResolutionQuality=", 200.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ViewDistanceQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.AntiAliasingQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadowQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.PostProcessQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.TextureQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.EffectsQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.FoliageQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.ShadingQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "sg.AnimationQuality=", 5.ToString());
                    ChangeConfig.GameUserSettings(SettingsPath, "ScreenRenderSize=", 200.ToString());

                    ReadAndFixAllValues();
                }
            }
            if (btn.Name.Contains("Res", StringComparison.Ordinal) && !btn.Name.Contains("FPS", StringComparison.Ordinal))
            {
                if (btn.Name.Contains("Set", StringComparison.Ordinal))
                {
                    ChangeConfig.SetResolution(SettingsPath, (int)numWidth.Value, (int)numHeight.Value);
                }
                if (btn.Name.Contains("Reset", StringComparison.Ordinal))
                {
                    int screenWidth = Screen.PrimaryScreen?.Bounds.Width ?? 1920;
                    int screenHeight = Screen.PrimaryScreen?.Bounds.Height ?? 1080;

                    numWidth.Value = screenWidth;
                    numHeight.Value = screenHeight;
                    lblHideFocus.Focus();

                    ChangeConfig.ResetResolution(SettingsPath);
                }
            }
            if (btn.Name.Contains("FPS", StringComparison.Ordinal))
            {
                if (btn.Name.Contains("Set", StringComparison.Ordinal))
                {
                    cbVsync.Checked = false;

                    ChangeConfig.ResetFPS(EnginePath);
                    if (numFPS.Value == 120)
                    {
                        ChangeConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                        ChangeConfig.SetFPS(SettingsPath, "FPSLimitMode=", (int)numFPS.Value);
                    }
                    if (numFPS.Value > 120)
                    {
                        MessageBox.Show(errors["max-fps"] + (int)numFPS.Value + errors["max-fps-continuation"], errors["information"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ChangeConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                        ChangeConfig.SetFPS(SettingsPath, "FPSLimitMode=", (int)numFPS.Value);
                    }
                    if (numFPS.Value < 120)
                    {
                        ChangeConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                        ChangeConfig.SetFPS(SettingsPath, "FPSLimitMode=", (int)numFPS.Value);
                    }

                    if (numFPS.Value <= 5)
                    {
                        ChangeConfig.SetFPS(SettingsPath, "FrameRateLimit=", (int)numFPS.Value);
                        ChangeConfig.SetFPS(SettingsPath, "FPSLimitMode=", (int)numFPS.Value);
                        ChangeConfig.EngineFPS(EnginePath, (int)numFPS.Value);
                    }

                    if (numFPS.Value >= 63 && numFPS.Value >= 120)
                    {
                        ChangeConfig.EngineFPS(EnginePath, (int)numFPS.Value);
                    }
                }
                if (btn.Name.Contains("Reset", StringComparison.Ordinal))
                {
                    cbVsync.Checked = true;
                    numFPS.Value = 62;

                    ChangeConfig.ResetFPS(EnginePath);
                    ChangeConfig.SetFPS(SettingsPath, "FrameRateLimit=", 60);
                    ChangeConfig.SetFPS(SettingsPath, "FPSLimitMode=", 60);
                }
            }
        }

        private void BtnUsersSettings_Click(object sender, EventArgs e)
        {
            try
            {
                lblHideFocus.Focus();
                File.WriteAllText(SettingsPath, UsersSettingsContent);
                File.WriteAllText(EnginePath, UsersEngineContent);

                ReadAndFixAllValues();
            }
            catch (Exception)
            {
            }
        }

        private void BtnUsersSettings_MouseLeave(object sender, EventArgs e)
        {
            btnUsersSettings.BackColor = Color.Transparent;
            tip.Hide(btnUsersSettings);
        }

        private void BtnUsersSettings_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip1"], btnUsersSettings);
        }

        private void BtnResetFPS_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip2"], btnResetFPS);
        }

        private void BtnResetFPS_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResetFPS);
        }

        private void BtnSetFPS_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip3"], btnSetFPS);
        }

        private void BtnSetFPS_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnSetFPS);
        }

        private void BtnResSet_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip4"], btnResSet);
        }

        private void BtnResSet_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResSet);
        }

        private void BtnResReset_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip5"], btnResReset);
        }

        private void BtnResReset_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnResReset);
        }

        private void BtnDelete_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip6"], btnDelete);
        }

        private void BtnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.Transparent;
            tip.Hide(btnDelete);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(errors["delete-confs"], errors["delete-confs-title"], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (File.Exists(ApplicationData + "UsEng.ini"))
                {
                    File.Delete(ApplicationData + "UsEng.ini");
                }
                if (File.Exists(ApplicationData + "UsSet.ini"))
                {
                    File.Delete(ApplicationData + "UsSet.ini");
                }
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Autosave"))
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Autosave");
                }
                Application.Restart();
            }
        }

        private void BtnDiscord_Click(object sender, EventArgs e)
        {
            try
            {
                var uri = "https://discord.com/invite/EY9uaqTS7Z";
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = uri
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception)
            {
                MessageBox.Show(errors["discord-link-fail"], errors["error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDiscord_MouseHover(object sender, EventArgs e)
        {
            tip.Show(errors["tip7"], btnDiscord);
        }

        private void BtnDiscord_MouseLeave(object sender, EventArgs e)
        {
            tip.Hide(btnDiscord);
            btnDiscord.BackColor = Color.Transparent;
        }

        private void BtnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.Crimson;
        }

        private void BtnUsersSettings_MouseEnter(object sender, EventArgs e)
        {
            btnUsersSettings.BackColor = Color.Crimson;
        }

        private void BtnDiscord_MouseEnter(object sender, EventArgs e)
        {
            btnDiscord.BackColor = Color.Crimson;
        }

        private void CbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            if ((string)cbVersion.SelectedItem == "Steam")
            {
                EnginePath = SteamEnginePath;
                SettingsPath = SteamSettingsPath;
                ReadAndFixAllValues();
            }
            if ((string)cbVersion.SelectedItem == "Epic Games Store")
            {
                EnginePath = EGSEnginePath;
                SettingsPath = EGSSettingsPath;
                ReadAndFixAllValues();
            }
            if ((string)cbVersion.SelectedItem == "Microsoft Store")
            {
                EnginePath = MSEnginePath;
                SettingsPath = MSSettingsPath;
                ReadAndFixAllValues();
            }
        }

        private void CbInGameLangauge_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            string word = cbInGameLangauge.SelectedItem?.ToString() switch
            {
                "Deutsch" => "de",
                "English" => "en",
                "Español" => "es",
                "Español (México)" => "es-MX",
                "Français" => "fr",
                "Italiano" => "it",
                "Polski" => "pl",
                "Português (Brasil)" => "pt-BR",
                "Türkçe" => "tr",
                "русский" => "ru",
                "ไทย" => "th",
                "中文 (简体)" => "zh-Hans",
                "中文 (繁體)" => "zh-Hant",
                "日本語" => "ja",
                "한국어" => "ko",
                _ => "en"
            };

            ChangeConfig.GameUserSettings(SettingsPath, "Language=", word);
        }

        private void CbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHideFocus.Focus();
            ChangeAppLanguage(cbLanguage.SelectedItem?.ToString() ?? "English");

            string word = cbLanguage.SelectedItem?.ToString() switch
            {
                "English" => "en",
                "Polski" => "pl",
                "русский" => "ru",
                "Deutsch" => "de",
                "Français" => "fr",
                "日本" => "jp",
                "中国人" => "ch",
                "Türkçe" => "tu",
                "Español" => "esp",
                "Italiano" => "it",
                _ => "en"
            };

            string filePath = ApplicationLanguageData + "lang.ini";
            if (!File.Exists(filePath)) return;

            string file = File.ReadAllText(filePath);
            string[] lang = File.ReadAllLines(filePath);
            string[] words = ["en", "pl", "ru", "de", "fr", "jp", "ch", "tu", "esp", "it"];

            foreach (string l in lang)
            {
                foreach (string w in words)
                {
                    if (l.Contains(w, StringComparison.Ordinal))
                    {
                        file = file.Replace(l, word, StringComparison.Ordinal);
                        File.WriteAllText(filePath, file);
                    }
                }
            }
        }
    }
}