using Random = UnityEngine.Random;

namespace StalkerAidsAndSupplementsMod
{
    internal class InfectionPatches
    {
        internal static int InfectionChance;

        [HarmonyPatch(typeof(InfectionRisk), nameof(InfectionRisk.Start))] //Tweaks to both Infection risk and Infection Affliction to make them more deadly
        private static class InfectionRiskPatch
        {
            private static void Postfix(InfectionRisk __instance)
            {
                if (!Settings.instance.EnableInfection) return;
                if (__instance == null) return;

                InfectionChance = Random.Range(25, 90);

                __instance.m_InfectionBaseChance = InfectionChance;
                __instance.m_InfectionChanceIncreasePerHour = 25;
                __instance.m_InfectionMaxChance = 100;
                __instance.m_InfectionChanceAntisepticReduction = 80;
                __instance.m_InfectionRollMaxHours = 16;
                __instance.m_InfectionRollMinHours = 4;
            }
        }
        [HarmonyPatch(typeof(Infection), nameof(Infection.Start))] 
        private static class InfectionPatch
        {
            private static void Postfix(Infection __instance)
            {
                if (!Settings.instance.EnableInfection) return;
                if (__instance == null) return;

                __instance.m_HPDrainPerHour = 3.5f;
                __instance.m_FatigueIncreasePerHour = 15;
                __instance.m_NumHoursRestForCure = 36;
            }
        }
    }
}