using Il2CppTLD.Gear;

namespace StalkerAidsAndSupplementsMod
{
    [HarmonyPatch(typeof(GearItem), "Awake")]

    //Patches for sweetened food to behave like vanilla counterparts but with extra features (litle more powerful than drinks)
    internal class SweetenedFood
    {
        internal static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;

            string name = __instance.name;
            string lower = name.ToLowerInvariant();

            if (name.Contains("GEAR_RosehipJam") || name.Contains("GEAR_SugarA"))
            {
                return;
            }
            if (!lower.Contains("jam") && !lower.Contains("sugar"))
            {
                return;
            }
            if (!lower.Contains("bannock") && !lower.Contains("porridge"))
            {
                return;
            }

            FoodItem food = __instance.gameObject.GetComponent<FoodItem>();

            if (food != null)
            {
                food.m_ChanceFoodPoisoningLowCondition = 5;
                food.m_ChanceFoodPoisoningRuined = 10;

                if (name.Contains("GEAR_CookedBannockAcornJam"))
                {
                    food.m_CaloriesTotal = Settings.instance.JamCalories + 250;
                    food.m_CaloriesRemaining = Settings.instance.JamCalories + 250;
                    food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam);
                }
                if (name.Contains("GEAR_CookedBannockAcornSugar"))
                {
                    food.m_CaloriesTotal = 300;
                    food.m_CaloriesRemaining = 300;
                }
                if (name.Contains("GEAR_CookedBannockJam"))
                {
                    food.m_CaloriesTotal = Settings.instance.JamCalories + 200;
                    food.m_CaloriesRemaining = Settings.instance.JamCalories + 200;
                    food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam);
                }
                if (name.Contains("GEAR_CookedBannockSugar"))
                {
                    food.m_CaloriesTotal = 250;
                    food.m_CaloriesRemaining = 250;
                }
                if (name.Contains("GEAR_CookedPorridgeJam"))
                {
                    food.m_CaloriesTotal = Settings.instance.JamCalories + 350;
                    food.m_CaloriesRemaining = Settings.instance.JamCalories + 350;
                    food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam);
                }
                if (name.Contains("GEAR_CookedPorridgeSugar"))
                {
                    food.m_CaloriesTotal = 420;
                    food.m_CaloriesRemaining = 420;
                }
            }
            if (lower.Contains("sugar"))
            {
                FoodMaxStatBuff foodbuff = __instance.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();
                FatigueBuff fatigue = __instance.GetComponent<FatigueBuff>() ?? __instance.gameObject.AddComponent<FatigueBuff>();

                if (foodbuff != null)
                {
                    foodbuff.m_Stat = FoodMaxStatBuff.StatType.Stamina;
                    foodbuff.m_CanStack = false;
                    foodbuff.m_Buff = 20;
                    foodbuff.m_TimeActiveHours = 0.75f;
                }
                if (fatigue != null)
                {
                    fatigue.m_InitialPercentDecrease = 7.5f;
                    fatigue.m_RateOfIncreaseScale = 0.75f;
                    fatigue.m_DurationHours = 0.75f;
                }
            }
        }
    }
}
