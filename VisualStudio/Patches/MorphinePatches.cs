using System;
using Il2CppTLD.Gear;

namespace StalkerAidsAndSupplements.Morphine.Patch
{
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class Morphine
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_PainkillerMorphineVial")) //Regular GearItem Patches
            {
                if (__instance == null) return;

                FirstAidItem firstAid = __instance.gameObject.GetComponent<FirstAidItem>() ?? __instance.gameObject.AddComponent<FirstAidItem>();
                if (firstAid != null)
                {
                    firstAid.m_AppliesSutures = true;
                    firstAid.m_KillsPain = true;
                    firstAid.m_TimeToUseSeconds = 4;
                    firstAid.m_UnitsPerUse = 1;
                    firstAid.m_UseAudio = "Play_FirstAidAntiseptic";
                    firstAid.m_LocalizedInspectModeUseText = new LocalizedString { m_LocalizationID = "GAMEPLAY_USE" }; //Remedy Localization for progress bar text - same principle for every patch
                    firstAid.m_LocalizedProgressBarMessage = new LocalizedString { m_LocalizationID = "GAMEPLAY_TakingPainkillers" };
                    firstAid.m_LocalizedRemedyText = new LocalizedString { m_LocalizationID = "GAMEPLAY_TakePainkillers" };
                }
                FoodMaxStatBuff FoodBuff = __instance.gameObject.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();
                if (FoodBuff != null)
                {
                    FoodBuff.m_Buff = 10;
                    FoodBuff.m_CanStack = false;
                    FoodBuff.m_Stat = FoodMaxStatBuff.StatType.Condition;
                    FoodBuff.m_TimeActiveHours = 4;
                }
            }
        }
    }
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseFirstAidItem))] //This hides the HUD message saying: "Treatment did not do anything." and suppresses Voiceline when Treatment "fails"
    internal class MorphineHUDMessageAndVODisable
    {
        internal static void Postfix(PlayerManager __instance, GearItem gi)
        {
            if (__instance == null || gi == null) return;

            FirstAidItem fia = GameManager.GetPlayerManagerComponent().m_FirstAidItemUsed;

            if (fia != null && fia.name.Contains("GEAR_PainkillerMorphineVial"))
            {
                InterfaceManager.GetPanel<Panel_HUD>().m_Label_Message.fontSize = 0;
                GameManager.GetPlayerManagerComponent().m_TreatmentFailedAudio = "Play_VOCatchBreath"; 
            }
            else if (fia != null && fia.name.Contains("GEAR_PainkillerIbuprofen"))
            {
                InterfaceManager.GetPanel<Panel_HUD>().m_Label_Message.fontSize = 0;
                GameManager.GetPlayerManagerComponent().m_TreatmentFailedAudio = "";
            }
            else if (fia != null && fia.name.Contains("GEAR_FirstAidKitPainkiller"))
            {
                InterfaceManager.GetPanel<Panel_HUD>().m_Label_Message.fontSize = 0;
                GameManager.GetPlayerManagerComponent().m_TreatmentFailedAudio = "Play_VOCatchBreath";
            }
            else if (fia != null && fia.name.Contains("GEAR_NaturalBandage"))
            {
                InterfaceManager.GetPanel<Panel_HUD>().m_Label_Message.fontSize = 0;
                GameManager.GetPlayerManagerComponent().m_TreatmentFailedAudio = "Play_VOCatchBreath";
            }
            else if (fia != null && fia.name.ToLowerInvariant().Contains("Sugar") || fia != null && fia.name.ToLowerInvariant().Contains("Jam")) //For Sweetened Teas
            {
                InterfaceManager.GetPanel<Panel_HUD>().m_Label_Message.fontSize = 0;
                GameManager.GetPlayerManagerComponent().m_TreatmentFailedAudio = "";
            }
            else
            {
                InterfaceManager.GetPanel<Panel_HUD>().m_Label_Message.fontSize = 14;
                GameManager.GetPlayerManagerComponent().m_TreatmentFailedAudio = "Play_FailGeneralSwitch";
            }

        }
    }
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateButtons))] //Replaces Button label with GAMEPLAY_Inject instead of GAMEPLAY_USE
    internal class MorphineEquipButtonLocalization
    {
        internal static void Postfix(ItemDescriptionPage __instance, GearItem gi)
        {
            if (__instance == null || gi == null) return;

            if (gi.name.Contains("GEAR_MorphineVial"))
            {
                __instance.m_Label_MouseButtonEquip.text = Localization.Get("GAMEPLAY_Inject");
            }
        }
    }
    
}
