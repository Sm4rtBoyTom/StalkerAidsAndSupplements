using AfflictionComponent.Components;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    internal class AfflictionPatches
    {
        [HarmonyPatch(typeof(Panel_Crafting), nameof(Panel_Crafting.GetModifiedCraftingDuration))]
        private static class CraftingSpeedPatch
        {
            private static void Postfix(ref int __result)
            {
                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(CigaretteBuff)) && manager.HasAfflictionOfType(typeof(AddictionRisk)))
                {
                    __result = (int)(__result * 0.65f);
                }
                else if (manager.HasAfflictionOfType(typeof(CigaretteBuff)) && manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    __result = (int)(__result * 1f);
                }
                else if (manager.HasAfflictionOfType(typeof(CigaretteBuff)))
                {
                    __result = (int)(__result * 0.75f);
                }
                else if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    __result = (int)(__result * 1.5f);
                }
            }
        }
        [HarmonyPatch(typeof(GunItem), nameof(GunItem.Update))]
        private static class RecoilPatch
        {
            private static void Postfix(GunItem __instance)
            {
                if (__instance == null) return;

                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(CigaretteBuff)) && manager.HasAfflictionOfType(typeof(AddictionRisk)))
                {
                    __instance.m_PitchRecoilMax = 15;
                    __instance.m_PitchRecoilMin = 7;
                    __instance.m_YawRecoilMax = 5;
                    __instance.m_YawRecoilMin = -5;
                }
                else if(manager.HasAfflictionOfType(typeof(CigaretteBuff)))
                {
                    __instance.m_PitchRecoilMax = 17;
                    __instance.m_PitchRecoilMin = 8;
                    __instance.m_YawRecoilMax = 6;
                    __instance.m_YawRecoilMin = -6;
                }
                else
                {
                    __instance.m_PitchRecoilMax = 21;
                    __instance.m_PitchRecoilMin = 10;
                    __instance.m_YawRecoilMax = 8;
                    __instance.m_YawRecoilMin = -8;
                }
            }
        }
        [HarmonyPatch(typeof(vp_FPSWeapon), nameof(vp_FPSWeapon.Update))]
        private static class AimShakePatch
        {
            private static void Postfix(vp_FPSWeapon __instance)
            {
                if (__instance == null) return;

                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(CigaretteBuff)) && manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    vp_FPSWeapon.m_DisableAimShake = false;
                }
                else if (manager.HasAfflictionOfType(typeof(CigaretteBuff))) 
                {
                    vp_FPSWeapon.m_DisableAimShake = true;
                }
                else
                {
                    vp_FPSWeapon.m_DisableAimShake = false;
                }
            }
        }
        [HarmonyPatch(typeof(Fatigue), nameof(Fatigue.CalculateFatigueIncrease))]
        internal static class IncreasedFatigueGainPatch
        {
            private static void Postfix(Fatigue __instance, float realtimeSeconds, ref float __result)
            {
                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(CigaretteBuff)) && manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    return;
                }
                else if (manager.HasAfflictionOfType(typeof(CigaretteBuff)) && manager.HasAfflictionOfType(typeof(AddictionRisk)))
                {
                    __result *= 0.5f;
                }
                else if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    __result *= 1.5f;
                }
                else if (manager.HasAfflictionOfType(typeof(CigaretteBuff)))
                {
                    __result *= 0.75f;
                }
            }
        }
        [HarmonyPatch(typeof(Panel_Rest), nameof(Panel_Rest.OnSelectRest))]
        internal static class LimitSleepAndPassTime
        {
            private static void Postfix(Panel_Rest __instance)
            {
                if (__instance == null) return;

                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                else if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    __instance.m_MaxSleepHours = 6;
                }
            }
        }
        [HarmonyPatch(typeof(PlayerMovement), nameof(PlayerMovement.GetMaxSprintStaminaModifier))]
        internal static class ReducedStaminaPatch
        {
            private static void Postfix(ref float __result)
            {
                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                   __result = __result - 33f;
                }
                else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
                {
                    __result = __result - 20f;
                }
            }
        }
        [HarmonyPatch(typeof(GunItem), nameof(GunItem.GetStaminaDropThresholdPercent))]
        internal static class ReducedAimDurationPatch
        {
            private static void Postfix(ref float __result)
            {
                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                    __result *= 1.5f;
                }
                else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
                {
                    __result *= 1.33f;
                }
            }
        }
        [HarmonyPatch(typeof(BowItem), nameof(BowItem.GetStaminaDropThresholdPercent))]
        internal static class ReducedAimDurationPatchBow
        {
            private static void Postfix(ref float __result)
            {
                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                    __result *= 1.5f;
                }
                else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
                {
                    __result *= 1.33f;
                }
            }
        }
        [HarmonyPatch(typeof(GunItem), nameof(GunItem.Update))]
        internal static class IncreasedSwayPatch
        {
            private static readonly float BASE_INCREASE = 0.1f;
            private static readonly float BASE_DECREASE = 0.15f;
            private static void Postfix(BowItem __instance)
            {
                if (__instance == null) return;

                float increase = BASE_INCREASE;
                float decrease = BASE_DECREASE;

                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                    increase *= 1.33f;
                    decrease *= 0.66f;
                }
                else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
                {
                    increase *= 1.15f;
                    decrease *= 0.85f;
                }
                else if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    increase *= 1.5f;
                    decrease *= 0.5f;
                }

                if (__instance.m_SwayIncreasePerSecond != increase) __instance.m_SwayIncreasePerSecond = increase;
                if (__instance.m_SwayDecreasePerSecond != decrease) __instance.m_SwayDecreasePerSecond = decrease;
            }
        }
        [HarmonyPatch(typeof(BowItem), nameof(BowItem.Update))]
        internal static class IncreasedSwayPatchBow
        {
            private static readonly float BASE_INCREASE = 0.1f;
            private static readonly float BASE_DECREASE = 0.15f;
            private static void Postfix(BowItem __instance)
            {
                if (__instance == null) return;

                float increase = BASE_INCREASE;
                float decrease = BASE_DECREASE;

                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                    increase *= 1.33f;
                    decrease *= 0.66f;
                }
                else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
                {
                    increase *= 1.15f;
                    decrease *= 0.85f;
                }
                else if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    increase *= 1.5f;
                    decrease *= 0.5f;
                }

                if (__instance.m_SwayIncreasePerSecond != increase) __instance.m_SwayIncreasePerSecond = increase;
                if (__instance.m_SwayDecreasePerSecond != decrease) __instance.m_SwayDecreasePerSecond = decrease;
            }
        }
        [HarmonyPatch(typeof(Rest), nameof(Rest.UpdateWhenSleeping))]
        internal static class SlowerConditionRecoveryPatch
        {
            private static void Postfix(Bed __instance)
            {
                if (__instance == null) return;

                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null) 
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                    __instance.m_ConditionPercentGainPerHour = __instance.m_ConditionPercentGainPerHour * 0.75f;
                    __instance.m_UinterruptedRestPercentGainPerHour = __instance.m_UinterruptedRestPercentGainPerHour * 0.75f;
                }
                else if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    __instance.m_ConditionPercentGainPerHour = __instance.m_ConditionPercentGainPerHour * 0.85f;
                    __instance.m_UinterruptedRestPercentGainPerHour = __instance.m_UinterruptedRestPercentGainPerHour * 0.85f;
                }
            }
        }
        [HarmonyPatch(typeof(PlayerClimbRope), nameof(PlayerClimbRope.UpdateStamina))]
        internal static class WorseStaminaWhileClimbing
        {
            private static void Postfix(PlayerClimbRope __instance)
            {
                if (__instance == null) return;

                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                    __instance.m_StaminaDrainPerSecondClimbingDown = 1.33f;
                    __instance.m_StaminaDrainPerSecondClimbingHolding = 0.7f;
                    __instance.m_StaminaDrainPerSecondClimbingUp = 3.75f;
                }
                else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
                {
                    __instance.m_StaminaDrainPerSecondClimbingDown = 1.2f;
                    __instance.m_StaminaDrainPerSecondClimbingHolding = 0.6f;
                    __instance.m_StaminaDrainPerSecondClimbingUp = 3.25f;
                }
            }
        }
        [HarmonyPatch(typeof(Freezing), nameof(Freezing.CalculateBodyTemperature))]
        internal static class ExtraTemperatureBuff
        {
            private static void Postfix(ref float __result)
            {
                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(CigaretteBuff)) && manager.HasAfflictionOfType(typeof(AddictionRisk)))
                {
                    __result += 3f;
                }
                else if (manager.HasAfflictionOfType(typeof(CigaretteBuff)))
                {
                    __result += 2f;
                }
                else if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                {
                    __result -= 3f;
                }
            }
        }
        [HarmonyPatch(typeof(Condition), nameof(Condition.GetAdjustedMaxHPModifier))]
        internal static class HPReductionPatch
        {
            private static void Postfix(ref float __result)
            {
                AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
                if (manager == null)
                    return;

                if (manager.HasAfflictionOfType(typeof(LungDamage)))
                {
                    __result -= 15f;
                }
                else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
                {
                    __result -= 10f;
                }
            }
        }
    }
}
       


