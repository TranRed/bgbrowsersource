using System.Windows;
using System.Windows.Controls;

namespace BGOverlayUpdater
{
    public partial class SettingsControl : UserControl
    {

        public SettingsControl(Settings settings)
        {
            InitializeComponent();
            UpdateSettings(settings);
        }

        public void UpdateSettings(Settings settings)
        {
            jsFileLocation.Text = settings.jsFileLocation;
        }

        void resetClicked(object sender, RoutedEventArgs e)
        {
            overlayUpdater.resetSession();
        }

    }
}