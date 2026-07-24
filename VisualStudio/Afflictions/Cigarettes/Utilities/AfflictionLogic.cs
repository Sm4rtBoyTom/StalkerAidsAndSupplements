using AfflictionComponent.Components;
using StalkerAidsAndSupplementsMod.Afflictions;
using StalkerAidsAndSupplementsMod.Afflictions.Buffs;

namespace StalkerAidsAndSupplementsMod
{
    internal static class AfflictionLogic
    {
        internal static float GetCraftingSpeedMultiplierUI()
        {
            AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();

            if (manager == null)
                return 1f;

            bool hasBuff = manager.HasAfflictionOfType(typeof(CigaretteBuff));
            bool hasRisk = manager.HasAfflictionOfType(typeof(AddictionRisk));
            bool hasAddiction = manager.HasAfflictionOfType(typeof(NicotineAddiction));

            if (hasBuff && hasRisk)
                return 0.65f;

            if (hasBuff)
                return 0.75f;

            if (hasAddiction)
                return 1.5f;

            return 1f;
        }
        internal static float GetCraftingSpeedMultiplier()
        {
            AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();

            if (manager == null)
                return 1f;

            bool hasBuff = manager.HasAfflictionOfType(typeof(CigaretteBuff));
            bool hasRisk = manager.HasAfflictionOfType(typeof(AddictionRisk));
            bool hasAddiction = manager.HasAfflictionOfType(typeof(NicotineAddiction));

            if (hasBuff && hasRisk)
                return 1.35f;

            if (hasBuff)
                return 1.25f;

            if (hasAddiction)
                return 0.5f;

            return 1f;
        }
        internal static bool IsInventoryRepairContext(Panel_Inventory_Examine panel)
        {
            if (panel == null) return false;
            if (panel.m_RepairInProgress) return true;
            if (panel.m_RepairPanel != null && panel.m_RepairPanel.activeInHierarchy) return true;
            if (panel.m_SafehouseCustomizationRepairPanel != null && panel.m_SafehouseCustomizationRepairPanel.activeInHierarchy) return true;

            return false;
        }
        internal static void ApplyActionTimetoMinutes(ref int durationMinutes, string effectName)
        {
            float multiplier = GetCraftingSpeedMultiplierUI();

            durationMinutes = Mathf.CeilToInt(durationMinutes * multiplier);
        }
    }
}
