using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using SandBox;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace StripLordsOnExecute
{
    [HarmonyPatch]
    internal class HeroExecutionSceneNotificationDataPatch
    {
        [HarmonyPatch(typeof(HeroExecutionSceneNotificationData), "GetSceneNotificationCharacters")]
        static bool Prefix(ref IEnumerable<SceneNotificationData.SceneNotificationCharacter> __result, HeroExecutionSceneNotificationData __instance)
        {
            Equipment emptyEquipment = new Equipment();
            /*
            emptyEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.NumAllWeaponSlots, default(EquipmentElement));
            emptyEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, default(EquipmentElement));
            emptyEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            emptyEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            emptyEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            emptyEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            */

            ItemObject item = Items.All.FirstOrDefault((ItemObject i) => i.StringId == "execution_axe");
            Equipment equipment2 = __instance.Executer.BattleEquipment.Clone(true);
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(item, null, null, false));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon2, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon3, default(EquipmentElement));
            equipment2.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ExtraWeaponSlot, default(EquipmentElement));
            __result = new List<SceneNotificationData.SceneNotificationCharacter>
            {
                CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Victim, emptyEquipment, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false),
                CampaignSceneNotificationHelper.CreateNotificationCharacterFromHero(__instance.Executer, equipment2, false, default(BodyProperties), uint.MaxValue, uint.MaxValue, false)
            };

            return false;
        }
    }
}
