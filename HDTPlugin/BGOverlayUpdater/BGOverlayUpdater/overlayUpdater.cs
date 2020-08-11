using System;
using System.Linq;
using System.Windows.Controls;
using System.IO;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using HearthDb.Enums;
using Newtonsoft.Json;
using MahApps.Metro.Controls;
using MahApps.Metro;

namespace BGOverlayUpdater
{
    public class overlayUpdater
    {
        static int first = 0;
        static int second = 0;
        static int third = 0;
        static int fourth = 0;
        static int fifth = 0;
        static int sixth = 0;
        static int seventh = 0;
        static int eigth = 0;
        static string mmrStart = "";
        static string mmrNow ="";
        private static Settings _settings;

        internal static void InMenu()
        {

            //update rating in Menu after battlegrounds game

            if (mmrStart == "")
            {
                //starting MMR should be updated from menu already, but let's be safe in case I forget to start the tracker on time
                int ratingStart = Core.Game.CurrentGameStats.BattlegroundsRating;
                mmrStart = ratingStart.ToString();
                mmrStart = mmrStart.Substring(0, mmrStart.Length - 3) + "," + mmrStart.Substring(mmrStart.Length - 3, 3);
                mmrNow = mmrStart;
            }

            //MMR after the game
            int rating = Core.Game.CurrentGameStats.BattlegroundsRatingAfter;
            string ratingStr = rating.ToString();
            ratingStr = ratingStr.Substring(0, ratingStr.Length - 3) + "," + ratingStr.Substring(ratingStr.Length - 3, 3);
            mmrNow = ratingStr;

            //snippet "borrowed" from boonwin: https://github.com/boonwin/BoonwinsBattlegroundsTracker 
            int playerId = Core.Game.Player.Id;
            Entity hero = Core.Game.Entities.Values.Where(x => x.IsHero && x.GetTag(GameTag.PLAYER_ID) == playerId).First();
            var placement = hero.GetTag(GameTag.PLAYER_LEADERBOARD_PLACE);

            switch (placement)
            {
                case 1:
                    first = first + 1;
                    break;

                case 2:
                    second = second + 1;
                    break;

                case 3:
                    third = third + 1;
                    break;

                case 4:
                    fourth = fourth + 1;
                    break;

                case 5:
                    fifth = fifth + 1;
                    break;

                case 6:
                    sixth = sixth + 1;
                    break;

                case 7:
                    seventh = seventh + 1;
                    break;

                case 8:
                    eigth = eigth + 1;
                    break;
            }
  
            _updateOverlay();
        }

        internal static void OnLoad(Settings settings)
        {
            //do an initial reset of stats (in case I load up HDT after starting the first game)
            _settings = settings;
            _updateOverlay();
        }

            internal static void OnUpdate()
        {
            //only try to update MMR in Battlegrounds menu
            if (Core.Game.CurrentMode == Hearthstone_Deck_Tracker.Enums.Hearthstone.Mode.BACON)
            {
                if (mmrStart == "")
                {
                    int ratingStart = Core.Game.BattlegroundsRatingInfo.Rating;
                    mmrStart = ratingStart.ToString();
                    mmrStart = mmrStart.Substring(0, mmrStart.Length - 3) + "," + mmrStart.Substring(mmrStart.Length - 3, 3);
                    mmrNow = mmrStart;
                    _updateOverlay();
                }
            }
        }

        private static void _updateOverlay()
        {
            var path = _settings.jsFileLocation;
            
            if (path != "")
            {
                string[] lines = { "document.getElementById(\"MMRstart\").innerHTML = \""+ mmrStart +"\";",
                                   "document.getElementById(\"MMRnow\").innerHTML = \""+ mmrNow +"\";",
                                   "document.getElementById(\"first\").innerHTML = \""+ first.ToString() +"\";",
                                   "document.getElementById(\"second\").innerHTML = \""+ second.ToString() +"\";",
                                   "document.getElementById(\"third\").innerHTML = \""+ third.ToString() +"\";",
                                   "document.getElementById(\"fourth\").innerHTML = \""+ fourth.ToString() +"\";",
                                   "document.getElementById(\"fifth\").innerHTML = \""+ fifth.ToString() +"\";",
                                   "document.getElementById(\"sixth\").innerHTML = \""+ sixth.ToString() +"\";",
                                   "document.getElementById(\"seventh\").innerHTML = \""+ seventh.ToString() +"\";",
                                   "document.getElementById(\"eigth\").innerHTML = \""+ eigth.ToString() +"\";",};
                File.WriteAllLines(path, lines);
            }
        }


    }
    public class overlayUpdaterPlugin : IPlugin
    {
        private Settings settings;
        private Flyout _settingsFlyout;
        private SettingsControl _settingsControl;
        public void OnLoad()
        {
            // Triggered upon startup and when the user ticks the plugin on
            GameEvents.OnInMenu.Add(overlayUpdater.InMenu);

            try
            {
                settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Settings._configLocation));
            }
            catch
            {
                settings = new Settings();
                settings.save();
            }

            overlayUpdater.OnLoad(settings);

            // create settings flyout
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
            overlayUpdater.OnUpdate();
        }


        public string Name => "OBS Overlay Updater";

        public string Description => "Updates my lidl BG overlay";

        public string ButtonText => "Settings";

        public string Author => "TranRed";

        public Version Version => new Version(0, 7, 0);

        public MenuItem MenuItem => null;
    }

    public class Settings
    {
        public static readonly string _configLocation = Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\BGOVerlayUpdater\BGOverlayUpdater.config";

        //filepath for .js file
        //leave empty for now (until I have a good idea about possible distribution)
        //must be set to work
        public string jsFileLocation = Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\BGOVerlayUpdater\scripts\main.js";

        public void save()
        {
            File.WriteAllText(_configLocation, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

    }
}
