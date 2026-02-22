using Il2CppTLD.Gear;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace StalkerAidsAndSupplementsMod
{
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class Anabiotics
    {
        internal static void Postfix(GearItem __instance) // Regular GearItem Patches
        {

            if (__instance.name.Contains("GEAR_Anabiotics"))
            {
                FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.m_MustConsumeAll = true;
                }
                FoodStatEffect foodeffect = __instance.gameObject.GetComponent<FoodStatEffect>() ?? __instance.gameObject.AddComponent<FoodStatEffect>();
                if (foodeffect != null)
                {
                    foodeffect.m_Effect = -25;
                    foodeffect.m_Stat = FoodStatEffect.StatType.Hunger;
                }
                CabinFeverReductionBuff cabinfever = __instance.gameObject.GetComponent<CabinFeverReductionBuff>() ?? __instance.gameObject.AddComponent<CabinFeverReductionBuff>();
                if (cabinfever != null)
                {
                    cabinfever.m_BuffAppliedText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CabinFeverReductionApplied" };
                    cabinfever.m_BuffTypeText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CabinFeverReductionType" };
                    cabinfever.m_BuffCooldownInHours = 36;
                    cabinfever.m_BuffIcon = "ico_injury_cabinFever";
                    cabinfever.m_FeverReductionInHours = Settings.instance.CabinFeverHours;
                    cabinfever.m_RiskReductionInHours = Settings.instance.CabinFeverHours * 2;
                }
            }
        }
    }
    [HarmonyPatch(typeof(FoodItem), "ApplyNutrition")] //This patch check if the GEAR_name has been eaten
    internal class AnabioticsBlackOutPatch
    {
        private static GearItem tempBedroll = null;

        internal static void Postfix(FoodItem __instance)
        {
            if (__instance == null) return;

            string foodName = __instance.name;

            if (foodName.Contains("GEAR_Anabiotics") || foodName == "GEAR_Anabiotics") //if the GearItem being eaten is GEAR_Anabiotics
            {
                GameManager.GetPlayerManagerComponent().m_FreezeMovement = true; //On finish eating freeze movement
                MelonCoroutines.Start(FadeAfterSeconds(3.5f)); //Starts a 3.5 second countdown
            }
        }
        static System.Collections.IEnumerator FadeAfterSeconds(float delay)
        {
            yield return new WaitForSeconds(delay);

            InterfaceManager.GetPanel<Panel_HUD>().Enable(false); 

            float FadeAlpha = 0f;
            float fadeEndAlpha = 1f;
            float fadeDuration = 3.5f;

            CameraFade.Fade(FadeAlpha, fadeEndAlpha, fadeDuration);

            yield return new WaitForSeconds(fadeDuration);

            // After screen fades create a temporary bedroll to sleep in

            GearItem bedrollPrefab = GearItem.LoadGearItemPrefab("GEAR_BedRoll");

            if (bedrollPrefab != null)
            {
                GameObject bedrollObject = GearItem.InstantiateDepletedGearPrefab(bedrollPrefab.gameObject);
                tempBedroll = bedrollObject.GetComponent<GearItem>();

                if (tempBedroll != null)
                {
                    tempBedroll.m_CurrentHP = Mathf.Max(2f, tempBedroll.m_CurrentHP);
                    tempBedroll.m_WornOut = false;
                    tempBedroll.m_InPlayerInventory = false;
                    tempBedroll.gameObject.transform.position = GameManager.GetPlayerTransform().position;
                    tempBedroll.gameObject.SetActive(true);

                    Bed bedComponent = tempBedroll.GetComponent<Bed>();
                    if (bedComponent != null)
                    {
                        bedComponent.m_OpenAudio = ""; //Empty, putting anything besides bedroll sounds doesn't seem to work
                        bedComponent.m_CloseAudio = "";
                        bedComponent.SetState(BedRollState.Placed);

                        Rest rest = GameManager.GetRestComponent();
                        int sleepHours = Random.Range(6, 12); //Roll random num of hours sleeping 
                        int maxHours = 12;

                        rest.BeginSleeping(bedComponent, sleepHours, maxHours); //Forces sleep
                        CameraFade.Fade(0, 1, 0);

                        GameManager.GetConditionComponent().AddHealthWithNoHudNotification(sleepHours * Settings.instance.SleepHP, DamageSource.Sleeping); //Add health based on time spent sleeping

                        MelonCoroutines.Start(WakeUp());
                    }
                }
            }
        }
        // Player wakes up and everything is re-enabled
        static System.Collections.IEnumerator WakeUp()
        {
            while (GameManager.GetRestComponent().IsSleeping())
            {
                yield return new WaitForSeconds(3f);

                GameManager.GetCameraEffects().HeadachePulse(3f);
                GameManager.GetPlayerManagerComponent().m_FreezeMovement = false;

                InterfaceManager.GetPanel<Panel_HUD>().Enable(true);

                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_AnabioticsMessage"), 5f, true, true); //Message that appears after player wakes up

                //This destroys temporary bedroll right after player wakes up

                if (tempBedroll != null) 
                {
                    GearManager.DestroyGearObject(tempBedroll.gameObject);
                    tempBedroll = null;
                }
            }
        }
    }
}

