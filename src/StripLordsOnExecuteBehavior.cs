using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace StripLordsOnExecute
{
    internal class StripLordsOnExecuteBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.BeforeHeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(
                (victim, killer, killCharacterActionDetail, showNotif) =>
                {
                    try
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            if (!victim.BattleEquipment[i].IsEmpty)
                            {
                                killer.PartyBelongedTo.ItemRoster.Add(new ItemRosterElement(victim.BattleEquipment[i], 1));
                                victim.BattleEquipment[i].Clear();
                            }
                        }

                        InformationMessage msg = new InformationMessage(
                            victim.FirstName.ToString() + "'s equipment is now yours."    
                        );
                        InformationManager.DisplayMessage(msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        InformationManager.DisplayMessage(new InformationMessage(ex.Message));
                    }
                }
            ));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
