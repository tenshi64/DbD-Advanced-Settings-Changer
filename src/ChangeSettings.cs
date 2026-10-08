using System;
using System.IO;
using System.Windows.Forms;

namespace DbD_Settings_Changer
{
    public static class ChangeConfig
    {
        public static void GameUserSettings(string path, string option, string valueInt)
        {
            string config = File.ReadAllText(path);
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (line.Contains(option, StringComparison.Ordinal))
                {
                    config = config.Replace(line, option + valueInt, StringComparison.Ordinal);
                    File.WriteAllText(path, config);
                }
            }
        }

        public static void SetFPS(string path, string option, int valueInt)
        {
            string read = File.ReadAllText(path);
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (line.Contains(option, StringComparison.Ordinal))
                {
                    if (valueInt is 60 or 61 or 62)
                    {
                        string text = !option.Contains("FPSLimitMode", StringComparison.Ordinal) ? "0.000000" : string.Empty;
                        read = read.Replace(line, option + text, StringComparison.Ordinal);
                        File.WriteAllText(path, read);
                    }
                    else
                    {
                        string text = !option.Contains("FPSLimitMode", StringComparison.Ordinal) ? ".000000" : string.Empty;
                        read = read.Replace(line, option + valueInt + text, StringComparison.Ordinal);
                        File.WriteAllText(path, read);
                    }
                }
            }

            if (option.Contains("FPSLimitMode", StringComparison.Ordinal))
            {
                if (valueInt is not (30 or 59 or 60 or 90 or 120))
                {
                    MessageBox.Show(Main.errors["fps-error"], Main.errors["information"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public static void EngineFPS(string enginePath, int value)
        {
            string readEngine = File.ReadAllText(enginePath);
            readEngine += "\n\n[/script/ReadEngine.ReadEngine]\nbSmoothFrameRate=False\nMinSmoothedFrameRate=1\nMaxSmoothedFrameRate=" + value;
            File.WriteAllText(enginePath, readEngine);
        }

        public static void ResetFPS(string path)
        {
            string readEngine = File.ReadAllText(path);
            string[] eng = File.ReadAllLines(path);

            foreach (string line in eng)
            {
                if (line.Contains("[/script/ReadEngine.ReadEngine]", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(path, readEngine);
                }
                if (line.Contains("[/script/engine.engine]", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(path, readEngine);
                }
                if (line.Contains("bSmoothFrameRate", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(path, readEngine);
                }
                if (line.Contains("MinSmoothedFrameRate", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(path, readEngine);
                }
                if (line.Contains("MaxSmoothedFrameRate", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(path, readEngine);
                }
                if (line.Contains("bUseVSync", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(path, readEngine);
                }
            }
        }

        public static void SetResolution(string path, int widthValue, int heightValue)
        {
            string readConfig = File.ReadAllText(path);
            int screenWidth = Screen.PrimaryScreen?.Bounds.Width ?? 1920;
            int screenHeight = Screen.PrimaryScreen?.Bounds.Height ?? 1080;

            string[] lines = File.ReadAllLines(path);

            if (heightValue != screenHeight || widthValue != screenWidth)
            {
                foreach (string line in lines)
                {
                    if (line.Contains("ResolutionSizeX", StringComparison.Ordinal) && !line.Contains("LastUser", StringComparison.Ordinal))
                    {
                        readConfig = readConfig.Replace(line, "ResolutionSizeX=" + widthValue, StringComparison.Ordinal);
                        File.WriteAllText(path, readConfig);
                    }
                    if (line.Contains("ResolutionSizeY", StringComparison.Ordinal) && !line.Contains("LastUser", StringComparison.Ordinal))
                    {
                        readConfig = readConfig.Replace(line, "ResolutionSizeY=" + heightValue, StringComparison.Ordinal);
                        File.WriteAllText(path, readConfig);
                    }
                    if (line.Contains("LastUserConfirmedResolutionSizeX", StringComparison.Ordinal))
                    {
                        readConfig = readConfig.Replace(line, "LastUserConfirmedResolutionSizeX=" + widthValue, StringComparison.Ordinal);
                        File.WriteAllText(path, readConfig);
                    }
                    if (line.Contains("LastUserConfirmedResolutionSizeY", StringComparison.Ordinal))
                    {
                        readConfig = readConfig.Replace(line, "LastUserConfirmedResolutionSizeY=" + heightValue, StringComparison.Ordinal);
                        File.WriteAllText(path, readConfig);
                    }
                    if (line.Contains("FullscreenMode", StringComparison.Ordinal) && !line.Contains("LastConfirmed", StringComparison.Ordinal) && !line.Contains("Preferred", StringComparison.Ordinal))
                    {
                        readConfig = readConfig.Replace(line, "FullscreenMode=0", StringComparison.Ordinal);
                        File.WriteAllText(path, readConfig);
                    }
                    if (line.Contains("LastConfirmedFullscreenMode", StringComparison.Ordinal))
                    {
                        readConfig = readConfig.Replace(line, "LastConfirmedFullscreenMode=0", StringComparison.Ordinal);
                        File.WriteAllText(path, readConfig);
                    }
                    if (line.Contains("PreferredFullscreenMode", StringComparison.Ordinal))
                    {
                        readConfig = readConfig.Replace(line, "PreferredFullscreenMode=0", StringComparison.Ordinal);
                        File.WriteAllText(path, readConfig);
                    }
                }
            }
            else if (heightValue == screenHeight && widthValue == screenWidth)
            {
                ResetResolution(path);
            }
        }

        public static void ResetResolution(string path)
        {
            string readConfig = File.ReadAllText(path);
            string[] lines = File.ReadAllLines(path);

            int screenWidth = Screen.PrimaryScreen?.Bounds.Width ?? 1920;
            int screenHeight = Screen.PrimaryScreen?.Bounds.Height ?? 1080;

            foreach (string line in lines)
            {
                if (line.Contains("ResolutionSizeX=", StringComparison.Ordinal) && !line.Contains("LastUser", StringComparison.Ordinal))
                {
                    readConfig = readConfig.Replace(line, "ResolutionSizeX=" + screenWidth, StringComparison.Ordinal);
                    File.WriteAllText(path, readConfig);
                }
                if (line.Contains("ResolutionSizeY=", StringComparison.Ordinal) && !line.Contains("LastUser", StringComparison.Ordinal))
                {
                    readConfig = readConfig.Replace(line, "ResolutionSizeY=" + screenHeight, StringComparison.Ordinal);
                    File.WriteAllText(path, readConfig);
                }
                if (line.Contains("LastUserConfirmedResolutionSizeX=", StringComparison.Ordinal))
                {
                    readConfig = readConfig.Replace(line, "LastUserConfirmedResolutionSizeX=" + screenWidth, StringComparison.Ordinal);
                    File.WriteAllText(path, readConfig);
                }
                if (line.Contains("LastUserConfirmedResolutionSizeY=", StringComparison.Ordinal))
                {
                    readConfig = readConfig.Replace(line, "LastUserConfirmedResolutionSizeY=" + screenHeight, StringComparison.Ordinal);
                    File.WriteAllText(path, readConfig);
                }
                if (line.Contains("FullscreenMode=", StringComparison.Ordinal) && !line.Contains("LastConfirmed", StringComparison.Ordinal) && !line.Contains("Preferred", StringComparison.Ordinal))
                {
                    readConfig = readConfig.Replace(line, "FullscreenMode=1", StringComparison.Ordinal);
                    File.WriteAllText(path, readConfig);
                }
                if (line.Contains("LastConfirmedFullscreenMode=", StringComparison.Ordinal) && !line.Contains("Preferred", StringComparison.Ordinal))
                {
                    readConfig = readConfig.Replace(line, "LastConfirmedFullscreenMode=1", StringComparison.Ordinal);
                    File.WriteAllText(path, readConfig);
                }
                if (line.Contains("PreferredFullscreenMode=", StringComparison.Ordinal) && !line.Contains("LastConfirmed", StringComparison.Ordinal))
                {
                    readConfig = readConfig.Replace(line, "PreferredFullscreenMode=1", StringComparison.Ordinal);
                    File.WriteAllText(path, readConfig);
                }
            }
        }

        public static void DisableAntiAliasing(string enginePath)
        {
            string readEngine = File.ReadAllText(enginePath);
            readEngine += "\n\n\n[/Script/Engine.GarbageCollectionSettings]\nr.DefaultFeature.AntiAliasing=0";
            File.WriteAllText(enginePath, readEngine);
        }

        public static void EnableAntiAliasing(string enginePath)
        {
            string readEngine = File.ReadAllText(enginePath);
            File.WriteAllText(enginePath, readEngine);
            string[] eng = File.ReadAllLines(enginePath);

            foreach (string line in eng)
            {
                if (line.Contains("[/Script/Engine.GarbageCollectionSettings]", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(enginePath, readEngine);
                }
                if (line.Contains("r.DefaultFeature.AntiAliasing=0", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(enginePath, readEngine);
                }
                if (line.Contains("[/Script/Engine.RendererOverrideSettings]", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(enginePath, readEngine);
                }
                if (line.Contains("[/ script / engine.streamingsettings]", StringComparison.OrdinalIgnoreCase))
                {
                    readEngine = readEngine.Replace(line, string.Empty, StringComparison.Ordinal);
                    File.WriteAllText(enginePath, readEngine);
                }
            }
        }
    }
}