using AfflictionComponent.Components;
using AfflictionComponent.Enums;
using AfflictionComponent.Interfaces;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    public class NicotineAddiction : CustomAffliction, IInstance, IRemedies
    {
        public NicotineAddiction() : base(
            name: "GAMEPLAY_NicotineAddiction",
            causeText: "GAMEPLAY_NicotineAddictionCause",
            description: "GAMEPLAY_NicotineAddictionDescription",
            descriptionNoHeal: "",
            spriteName: "ico_injury_Insomnia",
            location: AfflictionBodyArea.Head)
        {
            SmokingDataManager.Instance.ScheduleCureEvents("NicotineAddiction");
        }
        public InstanceType Type { get; set; } = InstanceType.Single;
        public Tuple<string, int, int>[] RemedyItems { get; set; } = Array.Empty<Tuple<string, int, int>>();
        public Tuple<string, int, int>[] AltRemedyItems { get; set; } = Array.Empty<Tuple<string, int, int>>();
        public bool InstantHeal { get; set; } = true;
        public override void OnUpdate() {}
        public void OnFoundExistingInstance(CustomAffliction existingAffliction) {}
        public void CureSymptoms() {}
        public void OnCure() {}
    }
}
