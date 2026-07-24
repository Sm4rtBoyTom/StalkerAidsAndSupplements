namespace StalkerAidsAndSupplementsMod
{
    internal class HeadachePatches
    {
        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.FirstAidConsumed))] //If player has Headache affliction and takes Ibuprofen,the headache is cured
        private static class IbuprofenCuresHeadache
        {
            private static void Postfix(PlayerManager __instance, GearItem gi)
            {
                if (__instance == null || gi == null) return;

                FirstAidItem fia = GameManager.GetPlayerManagerComponent().m_FirstAidItemUsed;

                if (fia != null && fia.name == "GEAR_PainkillerIbuprofen")
                {
                    if (GameManager.GetHeadacheComponent().HasHeadache())
                    {
                        GameManager.GetHeadacheComponent().Cure();
                    }
                    GameManager.GetConditionComponent().AddHealth(Settings.instance.IbuprofenHP, DamageSource.FirstAid);
                }
                else if (fia != null && fia.name == "GEAR_FirstAidKitPainkiller")
                {
                    GameManager.GetConditionComponent().AddHealth(10f, DamageSource.FirstAid);
                }
            }
        }
    }
}


