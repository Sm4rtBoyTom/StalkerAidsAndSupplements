using AfflictionComponent.Components;
using AfflictionComponent.Enums;
using AfflictionComponent.Interfaces;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    internal class CigaretteBuff : CustomAffliction, IDuration, IInstance, IBuff, IRemedies
    {
        public Tuple<string, int, int>[] RemedyItems { get; set; } = Array.Empty<Tuple<string, int, int>>();
        public Tuple<string, int, int>[] AltRemedyItems { get; set; } = Array.Empty<Tuple<string, int, int>>();
        public bool InstantHeal { get; set; } = true;
        public void CureSymptoms() {}
        public InstanceType Type { get; set; } = InstanceType.Single;
        public float Duration { get; set; }
        public float EndTime { get; set; }
        public bool Buff { get; set; } = true;
        public bool BuffCold { get; set; } = false;
        public bool BuffFatigue { get; set; } = true;
        public bool BuffHunger { get; set; } = false;
        public bool BuffThirst { get; set; } = false;
        public bool BuffCondition { get; set; } = false;
        public CigaretteBuff() : base(
    name: "GAMEPLAY_NicotineBuff",
    causeText: "GAMEPLAY_NicotineBuffCause",
    description: "GAMEPLAY_NicotineBuffDescription",
    descriptionNoHeal: null,
    spriteName: "ico_injury_warmingUp",
    location: AfflictionBodyArea.Head)
        {
            Duration = Settings.instance.BuffDuration;
        }
        public override void OnUpdate() { }
        public void OnCure() {}
        public void OnFoundExistingInstance(CustomAffliction existingBuff)
        {
            if (existingBuff is CigaretteBuff buff)
            {
                buff.ResetAffliction(false);
            }
        }
    }
}
