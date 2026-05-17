using AfflictionComponent.Components;
using AfflictionComponent.Enums;
using AfflictionComponent.Interfaces;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    internal class LungDamage : CustomAffliction, IInstance
    {
        public LungDamage() : base(
            name: "GAMEPLAY_LungDamageSevere",
            causeText: "GAMEPLAY_LungDamageSevereCause",
            description: "GAMEPLAY_LungDamageSevereDescription",
            descriptionNoHeal: "",
            spriteName: "ico_injury_suffocation",
            location: AfflictionBodyArea.Chest)
        {
        }
        public InstanceType Type { get; set; } = InstanceType.Single;
        public override void OnUpdate() {}
        public void OnCure() {}
        public void OnFoundExistingInstance(CustomAffliction existingAffliction) {}
        public void CureSymptoms() {}
    }
}
