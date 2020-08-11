using System.IO;
using Newtonsoft.Json;

namespace BGOverlayUpdater
{
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
