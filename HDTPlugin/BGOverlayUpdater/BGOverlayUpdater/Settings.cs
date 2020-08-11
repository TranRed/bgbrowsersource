using System.IO;
using Newtonsoft.Json;

namespace BGOverlayUpdater
{
    public class Settings
    {
        public static readonly string _configLocation = Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\BGOVerlayUpdater\BGOverlayUpdater.config";

        public string jsFileLocation = Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\BGOVerlayUpdater\scripts\main.js";

        public void save()
        {
            File.WriteAllText(_configLocation, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

    }
}
