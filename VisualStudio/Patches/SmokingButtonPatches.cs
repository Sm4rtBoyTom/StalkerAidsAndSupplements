

using Il2CppTLD.IntBackedUnit;
using StalkerAidsAndSupplementsMod;
using UnityEngine;
using HarmonyLib;
using Il2Cpp;
using Random = UnityEngine.Random;



namespace StalkerAidsAndSupplementsMod
{
    //Basically the same principle as Sweetening Button but for cigarettes
    internal class SmokeButton
    {
        private static GameObject smokeButton;
        internal static GearItem Cigarette;

        internal static void InitializeItemDescriptionPage(ItemDescriptionPage itemDescriptionPage)
        {
            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            smokeButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            smokeButton.transform.Translate(0f, 0f, 0f);  
            Utils.GetComponentInChildren<UILabel>(smokeButton).text = Localization.Get("GAMEPLAY_SmokeButton");

            AddAction(smokeButton, new System.Action(OnSmoke));

            SetSmokeActive(false);
        }

        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }
        internal static void SetSmokeActive(bool active)
        {
            NGUITools.SetActive(smokeButton, active);
        }
        internal static bool IsCigarette(string gearName)
        {
                if (gearName.Contains("Cigarette"))
                {
                    return true;
                }
            return false;
        }
        private static bool HasMatches()
        {
            bool hasFirestriker = GameManager.GetInventoryComponent().GearInInventory("GEAR_Firestriker", 1);
            bool hasWoodMatches = GameManager.GetInventoryComponent().GearInInventory("GEAR_WoodMatches", 1);
            bool hasPackMatches = GameManager.GetInventoryComponent().GearInInventory("GEAR_PackMatches", 1);

            return hasFirestriker || hasWoodMatches || hasPackMatches;
        }
        private static void UseMatch()
        {
            if (GameManager.GetInventoryComponent().GearInInventory("GEAR_Firestriker", 1))
            {
                GearItem firestriker = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_Firestriker");
                if (firestriker != null)
                {
                    firestriker.DegradeOnUse();
                }
            }
            else if (GameManager.GetInventoryComponent().GearInInventory("GEAR_WoodMatches", 1))
            {
                GameManager.GetInventoryComponent().RemoveGearFromInventory("GEAR_WoodMatches", 1);
            }
            else if (GameManager.GetInventoryComponent().GearInInventory("GEAR_PackMatches", 1))
            {
                GameManager.GetInventoryComponent().RemoveGearFromInventory("GEAR_PackMatches", 1);
            }
        }
        private static void OnSmoke()
        {
            var cigaretteItem = Cigarette;

            if (cigaretteItem == null) return;

            if (!HasMatches())
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NoMatches"));
                GameAudioManager.PlayGUIError();
                return;
            }

            GameAudioManager.PlayGuiConfirm();

            string lightSound = GetLightSound();

            UseMatch();

            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_SmokingProgressBar"), 5f, 0f, 0f, lightSound, null, true, true, new System.Action<bool, bool, float>(OnSmokeFinished));

            tempCigarette = cigaretteItem;
        }

        private static string GetLightSound()
        {
            if (GameManager.GetInventoryComponent().GearInInventory("GEAR_Firestriker", 1))
            {
                return "Play_SndActionFireStrikerLight1"; 
            }
            else if (GameManager.GetInventoryComponent().GearInInventory("GEAR_WoodMatches", 1) ||
                     GameManager.GetInventoryComponent().GearInInventory("GEAR_PackMatches", 1))
            {
                return "PLAY_SNDACTIONFIREMATCHLIGHT1";  
            }

            return "Play_MatchStrike";
        }
        private static GearItem tempCigarette;

        private static void OnSmokeFinished(bool success, bool playerCancel, float progress)
        {
            if (!success || playerCancel) return;

            if (tempCigarette != null && tempCigarette.m_StackableItem != null)
            {
                tempCigarette.m_StackableItem.m_Units--;
            }

            ApplyCigaretteEffects();
        }

        private static void ApplyCigaretteEffects() //Determines what happens after cigarette is smoked
        {
            GameManager.GetPlayerMovementComponent().AddSprintStamina(-15);
            GameManager.GetCabinFeverComponent().ApplyCabinFeverReductionBuff(Random.Range(1,4), Random.Range(12, 24));
            GameManager.GetThirstComponent().AddThirst(15);
            GameManager.GetFatigueComponent().AddFatigue(-7.5f);
            GameManager.GetHungerComponent().AddReserveCalories(-5);
            GameManager.GetConditionComponent().AddHealthWithNoHudNotification(-3.5f, DamageSource.Unspecified);
            GameManager.GetFreezingComponent().AddFreezing(10);
            GameManager.GetCameraEffects().PainPulse(0.35f);
        }
    }
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
    internal class SmokeButtonInitialization
    {
        private static void Postfix(Panel_Inventory __instance)
        {
            SmokeButton.InitializeItemDescriptionPage(__instance.m_ItemDescriptionPage);
        }
    }

    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
    internal class UpdateSmokeButton
    {
        private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
        {
            if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;

            SmokeButton.Cigarette = gi;

            if (gi != null && SmokeButton.IsCigarette(gi.name))
            {
                SmokeButton.SetSmokeActive(true);
            }
            else
            {
                SmokeButton.SetSmokeActive(false);
            }
        }
    }
}
