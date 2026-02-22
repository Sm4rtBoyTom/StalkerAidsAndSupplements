
using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace StalkerAidsAndSupplementsMod
{
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class VitaminCBottle
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_BottleVitaminC"))
            {
                FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.m_CaloriesTotal = Settings.instance.VitaminCalories;
                    food.m_CaloriesRemaining = Settings.instance.VitaminCalories;
                    food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCSmall);
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class UncookedJamPatches
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_UncookedRosehipJam"))
            {
                                if (__instance == null) return;

                FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(10);
                    food.m_CaloriesTotal = 15;
                    food.m_CaloriesRemaining = 15;
                }
                Cookable cook = __instance.gameObject.GetComponent<Cookable>();
                if (cook != null) 
                {
                    cook.m_CookingPotMaterialsList = new Material[1] { StalkerAidsAndSupplementsUtils.JamLiquidMaterial() };
                    cook.m_CanBePickedUpWhileCooking = false;
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class SleepingPills
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_SleepingPills"))
            {
                if (__instance == null) return;

                FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.m_CaloriesTotal = Settings.instance.SleepingCalories;
                    food.m_CaloriesRemaining = Settings.instance.SleepingCalories;
                    food.m_MustConsumeAll = true;
                }
                FoodStatEffect Fatigue = __instance.gameObject.GetComponent<FoodStatEffect>();
                if (Fatigue != null)
                {
                    Fatigue.m_Effect = Settings.instance.SleepingIncrease;
                    Fatigue.m_Stat = FoodStatEffect.StatType.Fatigue;
                }
                ConditionRestBuff Rest = __instance.gameObject.GetComponent<ConditionRestBuff>();
                if (Rest != null)
                {
                    Rest.m_ConditionRestBonus = Settings.instance.SleepingHP;
                    Rest.m_NumHoursRestAffected = Settings.instance.SleepingBonusDuration;
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class BottleCaffeine
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_BottleCaffeine"))
            {
                if (__instance == null) return;

                FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.m_CaloriesTotal = Settings.instance.CaffeineCalories;
                    food.m_CaloriesRemaining = Settings.instance.CaffeineCalories;
                }
                FatigueBuff Fatigue = __instance.gameObject.GetComponent<FatigueBuff>();
                if (Fatigue != null)
                {
                    Fatigue.m_InitialPercentDecrease = Settings.instance.CaffeineDecrease;
                    Fatigue.m_DurationHours = Settings.instance.CaffeineTime;
                }
                IngestedCarryCapacityBuff carryBuff = __instance.gameObject.GetComponent<IngestedCarryCapacityBuff>();
                if (carryBuff == null)
                {
                    carryBuff = __instance.gameObject.GetComponent<IngestedCarryCapacityBuff>() ?? __instance.gameObject.AddComponent<IngestedCarryCapacityBuff>(); //This should prevent Component Duplication
                }

                if (carryBuff != null)
                {
                    carryBuff.m_BuffIcon = "ico_carry";
                    carryBuff.m_BuffAppliedText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CarryBuffApplied" };
                    carryBuff.m_BuffTypeText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CarryBuffType" };
                    carryBuff.m_CarryCapacityBuffDurationInHours = Settings.instance.CaffeineCarryTime;
                    carryBuff.m_CarryCapacityChange = ItemWeight.FromKilograms(Settings.instance.CaffeineCarry);
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class FirstAidKit
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_FirstAidKitPainkiller"))
            {
                if (__instance == null) return;

                FirstAidItem FirstAid = __instance.gameObject.GetComponent<FirstAidItem>();
                if (FirstAid != null)
                {
                    FirstAid.m_LocalizedRemedyText = new LocalizedString { m_LocalizationID = "GAMEPLAY_ApplyBandage" };
                    FirstAid.m_LocalizedProgressBarMessage = new LocalizedString { m_LocalizationID = "GAMEPLAY_ApplyingBandage" };
                    FirstAid.m_LocalizedInspectModeUseText = new LocalizedString { m_LocalizationID = "GAMEPLAY_APPLY" };
                    FirstAid.m_AppliesBandage = false;
                }
                ConditionOverTimeBuff Condition = __instance.gameObject.GetComponent<ConditionOverTimeBuff>() ?? __instance.gameObject.AddComponent<ConditionOverTimeBuff>();
                if (Condition != null)
                {
                    Condition.m_ConditionIncreasePerHour = Settings.instance.FirstAidAmount;
                    Condition.m_NumHours = Settings.instance.FirstAidTime;
                }
                FoodMaxStatBuff FoodBuff = __instance.gameObject.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();
                if (FoodBuff != null)
                {
                    FoodBuff.m_Buff = 15;
                    FoodBuff.m_CanStack = false;
                    FoodBuff.m_Stat = FoodMaxStatBuff.StatType.Condition;
                    FoodBuff.m_TimeActiveHours = 6;
                }
                FoodStatEffect foodeffect = __instance.gameObject.GetComponent<FoodStatEffect>() ?? __instance.gameObject.AddComponent<FoodStatEffect>();
                if (foodeffect != null)
                {
                    foodeffect.m_Stat = FoodStatEffect.StatType.Hunger;
                    foodeffect.m_Effect = -35;
                }
                if (__instance != null) //Makes FirstAidKit even rarer
                {
                    __instance.m_SpawnChance = 50;
                }
                
            }
        }
    }
        [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class NaturalBandage
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_NaturalBandage"))
            {
                if (__instance == null) return;

                FirstAidItem FirstAid = __instance.gameObject.GetComponent<FirstAidItem>();
                if (FirstAid != null)
                {
                    FirstAid.m_LocalizedRemedyText = new LocalizedString { m_LocalizationID = "GAMEPLAY_ApplyBandage" };
                    FirstAid.m_LocalizedProgressBarMessage = new LocalizedString { m_LocalizationID = "GAMEPLAY_ApplyingBandage" };
                    FirstAid.m_LocalizedInspectModeUseText = new LocalizedString { m_LocalizationID = "GAMEPLAY_APPLY" };
                    FirstAid.m_StabalizesSprains = true;
                    FirstAid.m_AppliesBandage = true;
                }
                ConditionOverTimeBuff Condition = __instance.gameObject.GetComponent<ConditionOverTimeBuff>() ?? __instance.gameObject.AddComponent<ConditionOverTimeBuff>();
                if (Condition != null)
                {
                    Condition.m_ConditionIncreasePerHour = Settings.instance.BandageAmount;
                    Condition.m_NumHours = Settings.instance.BandageTime;
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class RosehipJam
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;

            if (__instance.name.Contains("GEAR_RosehipJam"))
            {
                FoodItem Food = __instance.gameObject.GetComponent<FoodItem>();
                if (Food != null)
                {
                    Food.m_CaloriesTotal = Settings.instance.JamCalories;
                    Food.m_CaloriesRemaining = Settings.instance.JamCalories;
                    Food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam);
                    Food.m_HeatedWhenCooked = true;
                    Food.m_PercentHeatLossPerMinuteIndoors = 1;
                    Food.m_PercentHeatLossPerMinuteOutdoors = 2;
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class Ibuprofen
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;

            if (__instance.name.Contains("GEAR_PainkillerIbuprofen"))
            {
                FirstAidItem firstaid = __instance.gameObject.GetComponent<FirstAidItem>();
                if (firstaid != null)
                {
                    firstaid.m_HPIncrease = 2.5f;
                    firstaid.m_AppliesSutures = true;
                    firstaid.m_LocalizedInspectModeUseText = new LocalizedString { m_LocalizationID = "GAMEPLAY_USE" };
                    firstaid.m_LocalizedProgressBarMessage = new LocalizedString { m_LocalizationID = "GAMEPLAY_TakingPainkillers" };
                    firstaid.m_LocalizedRemedyText = new LocalizedString { m_LocalizationID = "GAMEPLAY_TakePainkillers" };
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class EnergyDrink
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;

            if (__instance.name.Contains("GEAR_SodaEnergy")) //Some buff and tweaks to energy drink so it's not that useless
            {
                FatigueBuff fatigue = __instance.gameObject.GetComponent<FatigueBuff>();
                if (fatigue != null)
                {
                    fatigue.m_DurationHours = 1.5f;
                    fatigue.m_InitialPercentDecrease = 35;
                    fatigue.m_RateOfIncreaseScale = 0.5f;
                }
                EnergyBoostItem energyBoost = __instance.gameObject.GetComponent<EnergyBoostItem>();
                if (energyBoost != null)
                {
                    energyBoost.m_BoostDurationHours = 1.25f;
                    energyBoost.m_FatigueEndingIncrease = 40;
                    energyBoost.m_FatigueInitialDecrease = 60;
                    energyBoost.m_StaminaEndingDecrease = 80;
                    energyBoost.m_StaminaInitialIncrease = 75;
                }
                FoodMaxStatBuff statBuff = __instance.gameObject.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();
                if (statBuff != null)
                {
                    statBuff.m_Stat = FoodMaxStatBuff.StatType.Stamina;
                    statBuff.m_CanStack = true;
                    statBuff.m_TimeActiveHours = 1.5f;
                }
                IngestedCarryCapacityBuff carryCapacityBuff = __instance.gameObject.GetComponent<IngestedCarryCapacityBuff>() ?? __instance.gameObject.AddComponent<IngestedCarryCapacityBuff>();
                if (carryCapacityBuff != null)
                {
                    carryCapacityBuff.m_BuffIcon = "ico_carry";
                    carryCapacityBuff.m_CarryCapacityBuffDurationInHours = 1.5f;
                    carryCapacityBuff.m_CarryCapacityChange = ItemWeight.FromKilograms(3f);
                    carryCapacityBuff.m_BuffAppliedText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CarryBuffApplied" };
                    carryCapacityBuff.m_BuffTypeText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CarryBuffType" };
                }
            }
        }
    }
}










