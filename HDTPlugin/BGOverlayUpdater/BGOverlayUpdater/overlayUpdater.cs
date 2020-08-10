using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace BGOverlayUpdater
{
    public class overlayUpdater
    {
        internal static void GameEnd()
        {
        }

    }
    public class overlayUpdaterPlugin : IPlugin
    {

        public void OnLoad()
        {
            // Triggered upon startup and when the user ticks the plugin on
            GameEvents.OnGameEnd.Add(overlayUpdater.GameEnd);
        }

        public void OnUnload()
        {
            // Triggered when the user unticks the plugin, however, HDT does not completely unload the plugin.
            // see https://git.io/vxEcH
        }

        public void OnButtonPress()
        {
            // Triggered when the user clicks your button in the plugin list
        }

        public void OnUpdate()
        {
            // called every ~100ms
        }


        public string Name => "OBS Overlay Updater";

        public string Description => "Updates my lidl BG overlay";

        public string ButtonText => "BUTTON TEXT";

        public string Author => "TranRed";

        public Version Version => new Version(0, 0, 1);

        public MenuItem MenuItem => null;
    }
}
