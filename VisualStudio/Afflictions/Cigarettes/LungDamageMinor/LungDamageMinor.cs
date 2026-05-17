using AfflictionComponent.Components;
using AfflictionComponent.Enums;
using AfflictionComponent.Interfaces;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    internal class MinorLungDamage : CustomAffliction, IInstance
    {
        public MinorLungDamage() : base(
            name: "GAMEPLAY_LungDamageMinor",
            causeText: "GAMEPLAY_LungDamageMinorCause",
            description: "GAMEPLAY_LungDamageMinorDescription",
            descriptionNoHeal: "",
            spriteName: "ico_injury_suffocation",
            location: AfflictionBodyArea.Chest)
        {
        }
        public InstanceType Type { get; set; } = InstanceType.Single;
        public override void OnUpdate() { }
        public void OnCure() { }
        public void OnFoundExistingInstance(CustomAffliction existingAffliction) { }
        public void CureSymptoms() { }
    }
}
