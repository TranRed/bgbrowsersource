using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Utility.Logging;
using HearthDb.Enums;
using System.Runtime.ExceptionServices;

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

        internal static void InMenu()
        {

            //update rating in Menu after battlegrounds game
            if (mmrStart == "")
            {
                int ratingStart = Core.Game.CurrentGameStats.BattlegroundsRating;
                mmrStart = ratingStart.ToString();
                mmrStart = mmrStart.Substring(0, mmrStart.Length - 3) + "," + mmrStart.Substring(mmrStart.Length - 3, 3);
            }

            int rating = Core.Game.CurrentGameStats.BattlegroundsRatingAfter;
            string ratingStr = rating.ToString();
            ratingStr = ratingStr.Substring(0, ratingStr.Length - 3) + "," + ratingStr.Substring(ratingStr.Length - 3, 3);

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
            string placementStr = placement.ToString();

            //experimental code to update the js
            //path should be set in settings (somehow) and placements and MMR need to be read from result
            //starting MMR to be set on startup
            var path = @"C:\Users\kupfe\OneDrive\Dokumente\GitHub\bgbrowsersource\scripts\main.js";
            string[] lines = { "document.getElementById(\"MMRstart\").innerHTML = \""+ mmrStart +"\";",
                               "document.getElementById(\"MMRnow\").innerHTML = \""+ ratingStr +"\";",
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
    public class overlayUpdaterPlugin : IPlugin
    {

        public void OnLoad()
        {
            // Triggered upon startup and when the user ticks the plugin on
            GameEvents.OnInMenu.Add(overlayUpdater.InMenu);


            var path = @"C:\Users\kupfe\OneDrive\Dokumente\GitHub\bgbrowsersource\scripts\main.js";
            string[] lines = { "document.getElementById(\"MMRstart\").innerHTML = \"\";",
                               "document.getElementById(\"MMRnow\").innerHTML = \"\";",
                               "document.getElementById(\"first\").innerHTML = \"0\";",
                               "document.getElementById(\"second\").innerHTML = \"0\";",
                               "document.getElementById(\"third\").innerHTML = \"0\";",
                               "document.getElementById(\"fourth\").innerHTML =\"0\";",
                               "document.getElementById(\"fifth\").innerHTML = \"0\";",
                               "document.getElementById(\"sixth\").innerHTML = \"0\";",
                               "document.getElementById(\"seventh\").innerHTML = \"0\";",
                               "document.getElementById(\"eigth\").innerHTML = \"0\";",};
            File.WriteAllLines(path, lines);
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

        public Version Version => new Version(0, 5, 0);

        public MenuItem MenuItem => null;
    }
}
