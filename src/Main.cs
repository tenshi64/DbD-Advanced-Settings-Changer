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
    public partial class Main : Form
    {
        ChangeConfig EditConfig = new ChangeConfig();

        private string SettingsPath = "";
        private string EnginePath = "";

        private string SteamSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsClient\GameUserSettings.ini";
        private string SteamEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsClient\Engine.ini";


        private string EGSSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\EGS\GameUserSettings.ini";
        private string EGSEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\EGS\Engine.ini";


        private string MSSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WinGDK\GameUserSettings.ini";
        private string MSEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WinGDK\Engine.ini";


        private string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Autosave\";

        private string ApplicationLanguageData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Language\";

        ToolTip tip = new ToolTip();

        private string UsersSettingsContent;
        private string UsersEngineContent;

        private string ReadConfig;
        private string ReadEngine;

        protected IDictionary<string, string> errors = new Dictionary<string, string>();

        public Main()
        {
            InitializeComponent();
        }

        public void Main_Load(object sender, EventArgs e)
        {
            CreateLanguageConfig();
            DefaultTab();
            ChangeLanguageInFile();
            CheckWhichVersion();
            CheckForUpdates();
            CheckIfCongifsExist();
            ReadAndFixAllValues();
            CheckDate(imgPumpkin, imgXmasTree, imgFireworks);

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
                if(control.Name == "Nav")
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is Label)
                        {
                            if (ctrl.Name.Contains("menu"))
                            {
                                (ctrl as Label).Click += new EventHandler(this.menuClick);
                            }
                        }
                    }
                }
            }
        }
    }
}