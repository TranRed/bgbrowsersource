using System.Linq;
using System.IO;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using HearthDb.Enums;
using Newtonsoft.Json;


namespace BGOverlayUpdater
{
    public class OverlayUpdater
    {
        static bool gameJustEnded = false;
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
                if(mmrStart.Length > 3){
                    mmrStart = mmrStart.Substring(0, mmrStart.Length - 3) + "," + mmrStart.Substring(mmrStart.Length - 3, 3);
                }
                mmrNow = mmrStart;
            }

            if(Core.Game.Spectator == true)
            {
                return;
            }

            if (gameJustEnded) { 
                //MMR after the game
                int rating = Core.Game.CurrentGameStats.BattlegroundsRatingAfter;
                string ratingStr = rating.ToString();
                if(ratingStr.Length > 3){
                    ratingStr = ratingStr.Substring(0, ratingStr.Length - 3) + "," + ratingStr.Substring(ratingStr.Length - 3, 3);
                }
                mmrNow = ratingStr;

                //identify player and then their placement
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

                gameJustEnded = false;
            }

            _updateOverlay();
        }

        internal static void OnLoad(Settings settings)
        {
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
                    if(mmrStart.Length > 3){
                        mmrStart = mmrStart.Substring(0, mmrStart.Length - 3) + "," + mmrStart.Substring(mmrStart.Length - 3, 3);
                    }
                    mmrNow = mmrStart;
                    _updateOverlay();
                }
            }
        }

        internal static void OnGameEnd()
        {
            gameJustEnded = true;
        }

        internal static void resetSession()
        {
            //reset session stats on button click
            mmrStart = "";
            mmrNow = "";
            first = 0;
            second = 0;
            third = 0;
            fourth = 0;
            fifth = 0;
            sixth = 0;
            seventh = 0;
            eigth = 0;

            _updateOverlay();
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
}
