using Il2CppTLD.Gear;

namespace StalkerAidsAndSupplementsMod;

internal class HeadachePatches
{
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseFirstAidItem))] //If player has Headache affliction and takes Ibuprofen,the headache is cured
    private static class IbuprofenCuresHeadache
    {
        private static void Postfix(PlayerManager __instance, GearItem gi)
        {
            if (__instance == null || gi == null) return;

            FirstAidItem fia = GameManager.GetPlayerManagerComponent().m_FirstAidItemUsed;

            if (fia != null && fia.name.Contains("GEAR_PainkillerIbuprofen"))
            {
                if (GameManager.GetHeadacheComponent().HasHeadache())
                    GameManager.GetHeadacheComponent().Cure();
            }
        }
    }

    [HarmonyPatch(typeof(InfectionRisk), nameof(InfectionRisk.Update))] 
    private static class InfectionRiskPatches
    {
        private static void Postfix(InfectionRisk __instance)
        {
            if (__instance == null) return;

            if (__instance != null)
            {
                __instance.m_InfectionBaseChance = 70;
                __instance.m_InfectionChanceIncreasePerHour = 10;
            }

            
        }
    }
    [HarmonyPatch(typeof(Infection), nameof(Infection.Update))]
    private static class InfectionPatches
    {
        private static void Postfix(Infection __instance)
        {
            if (__instance == null) return;

            if (__instance != null)
            {
                __instance.m_FatigueIncreasePerHour = 15;
                __instance.m_HPDrainPerHour = 7.5f;
                __instance.m_NumHoursRestForCure = 18;
            }


        }
    }
}
