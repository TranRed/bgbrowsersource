using System.Windows.Controls;

namespace BGOverlayUpdater
{
    public partial class SettingsControl : UserControl
    {

        private Settings _settings;

        public SettingsControl(Settings settings)
        {
            InitializeComponent();
            UpdateSettings(settings);
        }

        public void UpdateSettings(Settings settings)
        {
            jsFileLocation.Text = settings.jsFileLocation;
        }

    }
}