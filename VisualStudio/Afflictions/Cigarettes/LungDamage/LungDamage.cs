using AfflictionComponent.Components;
using AfflictionComponent.Enums;
using AfflictionComponent.Interfaces;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    public class LungDamage : CustomAffliction, IInstance
    {
        public LungDamage() : base(
            name: "GAMEPLAY_LungDamageSevere",
            causeText: "GAMEPLAY_LungDamageSevereCause",
            description: "GAMEPLAY_LungDamageSevereDescription",
            descriptionNoHeal: "",
            spriteName: "ico_injury_suffocation",
            location: AfflictionBodyArea.Chest)
        {
            AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
            if (manager == null) return;

            if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
            {
                var risk = manager.GetAfflictionsOfType(typeof(MinorLungDamage))
                                  .FirstOrDefault() as MinorLungDamage;
                risk?.Cure();
            }
        }
        public InstanceType Type { get; set; } = InstanceType.Single;
        public override void OnUpdate() {}
        public void OnCure() {}
        public void OnFoundExistingInstance(CustomAffliction existingAffliction) {}
        public void CureSymptoms() {}
    }
}
