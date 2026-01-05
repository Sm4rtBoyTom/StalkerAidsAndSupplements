using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;
using StalkerAidsAndSupplementsMod;
using UniversalTweaks.Utilities;

namespace StalkerAidsAndSupplements;

internal class HeadachePatches
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    private static class RemoveHeadacheComponents
    {
        private static void Postfix(GearItem __instance)
        {
            if (Settings.instance.Headache)
            {
                ComponentUtilities.RemoveComponent<CausesHeadacheDebuff>("GEAR_BottleCaffeine");
            }
            else
            {
                ComponentUtilities.RestoreComponent<CausesHeadacheDebuff>("GEAR_BottleCaffeine");
            }
        }
    }
}
