using Il2CppTLD.Gear;

namespace StalkerAidsAndSupplementsMod;
internal class BlueprintPatches
{
    [HarmonyPatch(typeof(Il2CppTLD.Gear.BlueprintManager), nameof(Il2CppTLD.Gear.BlueprintManager.LoadAddressableBlueprints))]
    internal static class BlueprintLock
    {
        private static void Postfix(Il2CppTLD.Gear.BlueprintManager __instance)
        {
            foreach (BlueprintData blueprint in __instance.m_AllBlueprints) //This Hides/Locks blueprint
            {
                if (blueprint.name == "BP_BLUEPRINT_NaturalBandage")
                {
                    blueprint.m_Locked = true;
                    blueprint.m_IgnoreLockInSurvival = false;
                    blueprint.m_AppearsInStoryOnly = false;
                    blueprint.m_AppearsInSurvivalOnly = true;
                    break;
                }
            }
        }
    }
    [HarmonyPatch(typeof(Inventory), nameof(Inventory.AddGear))] //If player has Gear_NoteNaturalBandage in inventory blueprint gets unlock without notification
    internal static class UnlockOnNotePickUp
    {
        private static void Postfix(GearItem gi)
        {
            if (gi == null || !gi.name.Contains("GEAR_NoteNaturalBandage")) return;

            Il2CppTLD.Gear.BlueprintManager __instance = Il2CppTLD.Gear.BlueprintManager.Instance;
            if (__instance == null) return;

            BlueprintData targetBlueprint = null;
            foreach (BlueprintData blueprint in __instance.m_AllBlueprints)
            {
                if (blueprint.name == "BP_BLUEPRINT_NaturalBandage")
                {
                    targetBlueprint = blueprint;
                    break;
                }
            }
            if (targetBlueprint == null) return;
            if (!targetBlueprint.m_Locked) return;

            __instance.UnlockSilent(targetBlueprint); //if Unlock with notification - Notification appears on every scene load
        }
    }
}

