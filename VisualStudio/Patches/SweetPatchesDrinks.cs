using Il2CppTLD.Gear;

namespace StalkerAidsAndSupplementsMod
{
    [HarmonyPatch(typeof(GearItem), "Awake")]

    //Patches for sweetened drinks to behave like vanilla counterparts but with extra features
    internal class SweetenedDrinks
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
            if (lower.Contains("bannock") || lower.Contains("porridge"))
            {
                return;
            }

            FoodItem food = __instance.gameObject.GetComponent<FoodItem>();
            if (food != null)
            {
                food.m_HeatedWhenCooked = true;
                food.m_PercentHeatLossPerMinuteIndoors = 0.75f;
                food.m_PercentHeatLossPerMinuteOutdoors = 1.5f;
            }

            FreezingBuff FB = __instance.gameObject.GetComponent<FreezingBuff>() ?? __instance.gameObject.AddComponent<FreezingBuff>();
            if (FB != null)
            {
                FB.m_DurationHours = 1;
                FB.m_InitialPercentDecrease = 25;
                FB.m_RateOfIncreaseScale = 0.45f;
            }

            if (lower.Contains("sugar"))
            {
                FoodMaxStatBuff foodMaxStat = __instance.gameObject.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();

                if (foodMaxStat != null)
                {
                    foodMaxStat.m_Buff = 10;
                    foodMaxStat.m_CanStack = false;
                    foodMaxStat.m_TimeActiveHours = 0.5f;
                    foodMaxStat.m_Stat = FoodMaxStatBuff.StatType.Stamina;
                }
            }

            // Calories

            if (food != null && lower.Contains("jam"))
            {
                food.m_CaloriesTotal = Settings.instance.JamCalories + 100;
                food.m_CaloriesRemaining = Settings.instance.JamCalories + 100;
            }

            if (food != null && lower.Contains("sugar"))
            {
                food.m_CaloriesTotal = 135;
                food.m_CaloriesRemaining = 135;
            }

            // Vitamin C

            //Burdock Tea
            if (food != null && name.Contains("GEAR_BurdockTeaJam"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam + 7);
            }
            else if (food != null && name.Contains("GEAR_BurdockTeaSugar"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(7);
            }
            //Birchbark Tea
            if (food != null && name.Contains("GEAR_BirchbarkTeaJam"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam + 6);
            }
            else if (food != null && name.Contains("GEAR_BirchbarkTeaSugar"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(6);
            }
            //Green Tea
            if (food != null && name.Contains("GEAR_GreenTeaCupJam"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam + 11);
            }
            else if (food != null && name.Contains("GEAR_GreenTeaCupSugar"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(11);
            }
            //Reishi Tea
            if (food != null && name.Contains("ReishiTeaJam"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam + 5);
            }
            else if (food != null && name.Contains("GEAR_ReishiTeaSugar"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(5);
            }
            //RoseHip Tea
            if (food != null && name.Contains("RoseHipTeaJam"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(Settings.instance.VitaminCJam + 13);
            }
            else if (food != null && name.Contains("GEAR_RoseHipTeaSugar"))
            {
                food.m_Nutrients = StalkerAidsAndSupplementsUtils.VitC(13);
            }

            //Coffees

            if (lower.Contains("coffeecup"))
            {
                FatigueBuff fatigueBuff = __instance.gameObject.GetComponent<FatigueBuff>() ?? __instance.gameObject.AddComponent<FatigueBuff>();
                if (fatigueBuff != null)
                {
                    if (name.Contains("GEAR_AcornCoffeeCupSugar"))
                    {
                        fatigueBuff.m_DurationHours = 0.75f;
                        fatigueBuff.m_InitialPercentDecrease = 7.5f;
                        fatigueBuff.m_RateOfIncreaseScale = 0.2f;
                    }
                    else if (name.Contains("GEAR_CoffeeCupSugar"))
                    {
                        fatigueBuff.m_DurationHours = 1.25f;
                        fatigueBuff.m_InitialPercentDecrease = 12.5f;
                        fatigueBuff.m_RateOfIncreaseScale = 0.4f;
                    }
                }
            }

            //Green Tea
            if (lower.Contains("greenteacup"))
            {
                ConditionRestBuff conditionRest = __instance.gameObject.GetComponent<ConditionRestBuff>() ?? __instance.gameObject.AddComponent<ConditionRestBuff>();

                if (conditionRest != null)
                {
                    if (name.Contains("GEAR_GreenTeaCupJam"))
                    {
                        conditionRest.m_ConditionRestBonus = 2.5f;
                        conditionRest.m_NumHoursRestAffected = 7;
                    }
                    else if (name.Contains("GEAR_GreenTeaCupSugar"))
                    {
                        conditionRest.m_ConditionRestBonus = 2;
                        conditionRest.m_NumHoursRestAffected = 6;
                    }
                }
            }

            // Birchbark Tea

            if (lower.Contains("birchbarktea"))
            {
                ConditionOverTimeBuff conditionOverTime = __instance.gameObject.GetComponent<ConditionOverTimeBuff>() ?? __instance.gameObject.AddComponent<ConditionOverTimeBuff>();

                if (conditionOverTime != null)
                {
                    if (name.Contains("GEAR_BirchbarkTeaJam"))
                    {
                        conditionOverTime.m_ConditionIncreasePerHour = 3;
                        conditionOverTime.m_NumHours = 2.5f;
                    }
                    else if (name.Contains("GEAR_BirchbarkTeaSugar"))
                    {
                        conditionOverTime.m_ConditionIncreasePerHour = 2.5f;
                        conditionOverTime.m_NumHours = 2;
                    }
                }
            }

            // FirstAid Teas

            if (name.Contains("GEAR_BurdockTea") || name.Contains("GEAR_ReishiTea") || name.Contains("GEAR_RoseHipTea"))
            {
                FirstAidItem fia = __instance.gameObject.GetComponent<FirstAidItem>() ?? __instance.gameObject.AddComponent<FirstAidItem>();

                if (fia == null)
                {
                    return;
                }
                //For all
                if (fia != null)
                {
                    fia.m_LocalizedInspectModeUseText = new LocalizedString { m_LocalizationID = "GAMEPLAY_DRINK" };
                    fia.m_LocalizedProgressBarMessage = new LocalizedString { m_LocalizationID = "GAMEPLAY_DrinkingProgress" };
                    fia.m_LocalizedRemedyText = new LocalizedString { m_LocalizationID = "GAMEPLAY_Drink" };
                    fia.m_UseAudio = "Play_Slurping1";
                    fia.m_TimeToUseSeconds = 2;
                    fia.m_UnitsPerUse = 1;

                    if (name.Contains("GEAR_BurdockTea") || name.Contains("GEAR_ReishiTea"))
                    {
                        fia.m_ProvidesAntibiotics = true;
                    }
                    else if (name.Contains("GEAR_RoseHipTea"))
                    {
                        fia.m_KillsPain = true;
                    }
                }
            }

            //CookingPot Materials

            Cookable cookable = __instance.gameObject.GetComponent<Cookable>() ?? __instance.gameObject.AddComponent<Cookable>();

            if (cookable != null)
            {
                if (name.Contains("GEAR_AcornCoffeeCup"))
                {
                    cookable.m_CookingPotMaterialsList = new Material[1] { StalkerAidsAndSupplementsUtils.AcornCoffeeLiquidMaterial() };
                }
                else if (name.Contains("GEAR_CoffeeCup"))
                {
                    cookable.m_CookingPotMaterialsList = new Material[1] { StalkerAidsAndSupplementsUtils.CoffeeLiquidMaterial() };
                }
                else if (name.Contains("GEAR_GreenTeaCup"))
                {
                    cookable.m_CookingPotMaterialsList = new Material[1] { StalkerAidsAndSupplementsUtils.HerbalTeaLiquidMaterial() };
                }
                else if (name.Contains("GEAR_BirchbarkTea") || name.Contains("GEAR_ReishiTea"))
                {
                    cookable.m_CookingPotMaterialsList = new Material[1] { StalkerAidsAndSupplementsUtils.ReishiTeaLiquidMaterial() };
                }
                else if (name.Contains("GEAR_BurdockTea") || name.Contains("GEAR_RoseHipTea"))
                {
                    cookable.m_CookingPotMaterialsList = new Material[1] { StalkerAidsAndSupplementsUtils.RoseHipTeaLiquidMaterial() };
                }
            }
        }
    }
}