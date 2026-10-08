using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DbD_Settings_Changer
{
    public partial class Main : Form
    {
        private string SettingsPath = string.Empty;
        private string EnginePath = string.Empty;

        private readonly string SteamSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsClient\GameUserSettings.ini";
        private readonly string SteamEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WindowsClient\Engine.ini";

        private readonly string EGSSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\EGSClient\GameUserSettings.ini";
        private readonly string EGSEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\EGSClient\Engine.ini";

        private readonly string MSSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WinGDKClient\GameUserSettings.ini";
        private readonly string MSEnginePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DeadByDaylight\Saved\Config\WinGDKClient\Engine.ini";

        private readonly string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Autosave\";

        private readonly string ApplicationLanguageData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DbD Settings Changer\Data\Configs\Language\";

        private readonly ToolTip tip = new();

        private string UsersSettingsContent = string.Empty;
        private string UsersEngineContent = string.Empty;

        private string ReadConfig = string.Empty;
        private string ReadEngine = string.Empty;

        public static readonly IDictionary<string, string> errors = new Dictionary<string, string>();

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
                if (control is Panel panel && control.Name.Contains("panel", StringComparison.OrdinalIgnoreCase))
                {
                    panel.HorizontalScroll.Maximum = 0;
                    panel.AutoScroll = false;
                    panel.VerticalScroll.Visible = false;
                    panel.AutoScroll = true;

                    foreach (Control ctrl in panel.Controls)
                    {
                        if (ctrl is Button button)
                        {
                            button.Click += BtnClick;
                        }
                        if (ctrl is TrackBar trackBar)
                        {
                            trackBar.Scroll += TbScroll;
                        }
                        if (ctrl is CheckBox checkBox)
                        {
                            checkBox.CheckedChanged += CbCheckedChanged;
                        }
                    }
                }
                if (control.Name == "Nav")
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is Label label && label.Name.Contains("menu", StringComparison.OrdinalIgnoreCase))
                        {
                            label.Click += MenuClick;
                        }
                    }
                }
            }
        }
    }
}