using System;
using System.Windows.Controls;
using System.IO;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.API;
using Newtonsoft.Json;
using MahApps.Metro.Controls;


namespace BGOverlayUpdater
{
    public class OverlayUpdaterPlugin : IPlugin
    {
        private Settings settings;
        private Flyout _settingsFlyout;
        private SettingsControl _settingsControl;

        public void OnLoad()
        {
            // Triggered upon startup and when the user ticks the plugin on
            GameEvents.OnInMenu.Add(OverlayUpdater.InMenu);

            try
            {
                settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Settings._configLocation));
            }
            catch
            {
                settings = new Settings();
                settings.save();
            }

            OverlayUpdater.OnLoad(settings);
            createSettingsFlyout(settings);

        }

        public void OnUnload()
        {
            // Triggered when the user unticks the plugin, however, HDT does not completely unload the plugin.
            // see https://git.io/vxEcH
        }

        public void OnButtonPress()
        {
            // Triggered when the user clicks your button in the plugin list
            _settingsFlyout.IsOpen = true;
        }

        public void OnUpdate()
        {
            //called every ~100ms
            OverlayUpdater.OnUpdate();
        }

        private void createSettingsFlyout(Settings settings)
        {
            _settingsFlyout = new Flyout();
            _settingsFlyout.Name = "BgSettingsFlyout";
            _settingsFlyout.Position = Position.Left;
            Panel.SetZIndex(_settingsFlyout, 100);
            _settingsFlyout.Header = "BG Overlay Update Settings";
            _settingsControl = new SettingsControl(settings);
            _settingsFlyout.Content = _settingsControl;

            _settingsFlyout.ClosingFinished += (sender, args) =>
            {
                settings.jsFileLocation = _settingsControl.jsFileLocation.Text;
                settings.save();
            };

            Core.MainWindow.Flyouts.Items.Add(_settingsFlyout);
        }

        public string Name => "OBS Overlay Updater";

        public string Description => "Updates my lidl BG overlay";

        public string ButtonText => "Settings";

        public string Author => "TranRed";

        public Version Version => new Version(0, 7, 0);
        public MenuItem MenuItem => null;
    }
}
