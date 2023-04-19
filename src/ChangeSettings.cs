using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DbD_Settings_Changer
{
    public partial class ChangeConfig
    {
        public void GameUserSettings(string path, string option, string valueInt)
        {
            string config = File.ReadAllText(path);

            int numLines = File.ReadLines(path).Count();
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < numLines; i++)
            {
                if (lines[i].Contains(option))
                {
                    config = config.Replace(lines[i], option + valueInt);
                    File.WriteAllText(path, config);
                }
            }
        }

        public void SetFPS(string path, string option, int valueInt)
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

        public void EngineFPS(string EnginePath, int value)
        {
            string ReadEngine = File.ReadAllText(EnginePath);
            ReadEngine += "\n\n[/script/ReadEngine.ReadEngine]\nbSmoothFrameRate=False\nMinSmoothedFrameRate=1\nMaxSmoothedFrameRate=" + value;
            File.WriteAllText(EnginePath, ReadEngine);
        }

        public void ResetFPS(string path)
        {
            string ReadEngine = File.ReadAllText(path);
            int numLines2 = File.ReadLines(path).Count();
            string[] eng = File.ReadAllLines(path);

            for (int a = 0; a < numLines2; a++)
            {
                if (eng[a].ToLower().Contains("[/script/ReadEngine.ReadEngine]".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].ToLower().Contains("[/script/engine.engine]".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].ToLower().Contains("bSmoothFrameRate".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].ToLower().Contains("MinSmoothedFrameRate".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].ToLower().Contains("MaxSmoothedFrameRate".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
                if (eng[a].ToLower().Contains("bUseVSync".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(path, ReadEngine);
                }
            }
        }

        public void SetResolution(string path, int WidthValue, int HeightValue)
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
                    if (lines[i].Contains("FullscreenMode") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Preferred"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "FullscreenMode=" + 0);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastConfirmedFullscreenMode"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastConfirmedFullscreenMode=" + 0);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("PreferredFullscreenMode"))
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
                    if (lines[i].Contains("FullscreenMode=") && !lines[i].Contains("LastConfirmed") && !lines[i].Contains("Preferred"))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "FullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("LastConfirmedFullscreenMode="))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "LastConfirmedFullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                    if (lines[i].Contains("PreferredFullscreenMode="))
                    {
                        ReadConfig = ReadConfig.Replace(lines[i], "PreferredFullscreenMode=" + 1);
                        File.WriteAllText(path, ReadConfig);
                    }
                }
            }
        }

        public void ResetResolution(string path)
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

        public void DisableAntiAliasing(string EnginePath)
        {
            string ReadEngine = File.ReadAllText(EnginePath);
            ReadEngine += "\n\n\n[/Script/Engine.GarbageCollectionSettings]\nr.DefaultFeature.AntiAliasing=0";

            File.WriteAllText(EnginePath, ReadEngine);
        }

        public void EnableAntiAliasing(string EnginePath)
        {
            string ReadEngine = File.ReadAllText(EnginePath);

            File.WriteAllText(EnginePath, ReadEngine);
            int numLines2 = File.ReadLines(EnginePath).Count();
            string[] eng = File.ReadAllLines(EnginePath);

            for (int a = 0; a < numLines2; a++)
            {
                if (eng[a].ToLower().Contains("[/Script/Engine.GarbageCollectionSettings]".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(EnginePath, ReadEngine);
                }
                if (eng[a].ToLower().Contains("r.DefaultFeature.AntiAliasing=0".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(EnginePath, ReadEngine);
                }
                if (eng[a].ToLower().Contains("[/Script/Engine.RendererOverrideSettings]".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(EnginePath, ReadEngine);
                }
                if (eng[a].ToLower().Contains("[/ script / engine.streamingsettings]".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(EnginePath, ReadEngine);
                }
                if (eng[a].ToLower().Contains("[/ script / engine.streamingsettings]".ToLower()))
                {
                    ReadEngine = ReadEngine.Replace(eng[a], String.Empty);

                    File.WriteAllText(EnginePath, ReadEngine);
                }
            }
        }
    }
}
