using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace StripLordsOnExecute
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            if (gameStarterObject == null)
                return;

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = gameStarterObject as CampaignGameStarter;
                campaignGameStarter.AddBehavior(new StripLordsOnExecuteBehavior());
            }
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Harmony h = new Harmony("StripLordsOnExecute");
            h.PatchAll();
        }
    }
}