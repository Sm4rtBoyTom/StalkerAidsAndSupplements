
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
                FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam);
                    food.m_CaloriesTotal = Settings.instance.JamCalories;
                    food.m_CaloriesRemaining = Settings.instance.JamCalories;
                }
                Cookable cook = __instance.gameObject.GetComponent<Cookable>();
                if (cook != null) 
                {
                    cook.m_CookingPotMaterialsList = new Material[1] { StalkerAidsAndSupplementsUtils.LiquidMaterial() };
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
                FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
                if (food != null)
                {
                    food.m_CaloriesTotal = Settings.instance.SleepingCalories;
                    food.m_CaloriesRemaining = Settings.instance.SleepingCalories;
                }
                FatigueBuff Fatigue = __instance.gameObject.GetComponent<FatigueBuff>();
                if (Fatigue != null)
                {
                    Fatigue.m_InitialPercentDecrease = Settings.instance.SleepingIncrease;
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
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class FirstAidKit
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_FirstAidKitPainKiller"))
            {
                FirstAidItem FirstAid = __instance.gameObject.GetComponent<FirstAidItem>();
                if (FirstAid != null)
                {
                    FirstAid.m_AppliesBandage = false;
                    FirstAid.m_HPIncrease = Settings.instance.FirstAidKit;
                }
                ConditionOverTimeBuff Condition = __instance.gameObject.AddComponent<ConditionOverTimeBuff>();
                if (Condition != null)
                {
                    Condition.m_ConditionIncreasePerHour = Settings.instance.FirstAidAmount;
                    Condition.m_NumHours = Settings.instance.FirstAidTime;
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
                FirstAidItem FirstAid = __instance.gameObject.GetComponent<FirstAidItem>();
                if (FirstAid != null)
                {
                    FirstAid.m_StabalizesSprains = true;
                 //   FirstAid.m_AppliesBandage = true;
                }
                ConditionOverTimeBuff Condition = __instance.gameObject.AddComponent<ConditionOverTimeBuff>();
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
            if (__instance.name.Contains("GEAR_RosehipJam"))
            {
                FoodItem Food = __instance.gameObject.GetComponent<FoodItem>();
                if (Food != null)
                {
                    Food.m_CaloriesTotal = Settings.instance.JamCalories;
                    Food.m_CaloriesRemaining = Settings.instance.JamCalories;
                    Food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam);
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class Ibuprofen
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_Ibuprofen"))
            {
                ConditionOverTimeBuff Condition = __instance.gameObject.AddComponent<ConditionOverTimeBuff>();
                if (Condition != null)
                {
                    Condition.m_ConditionIncreasePerHour = Settings.instance.IbuprofenAmount;
                    Condition.m_NumHours = Settings.instance.IbuprofenTime;
                }
            }
        }
    }
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class Morphine
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_MorphineVial"))
            {
                ConditionOverTimeBuff Condition = __instance.gameObject.AddComponent<ConditionOverTimeBuff>();
                if (Condition != null)
                {
                    Condition.m_ConditionIncreasePerHour = 2f;
                    Condition.m_NumHours = 3f;
                }
            }
        }
    }
}




