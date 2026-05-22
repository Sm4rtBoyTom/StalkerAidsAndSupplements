using AfflictionComponent.Components;
using AfflictionComponent.Enums;
using AfflictionComponent.Interfaces;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    internal class AddictionRisk : CustomAffliction, IInstance, IRiskPercentage, IRemedies
    {
        public AddictionRisk() : base(
            name: "GAMEPLAY_NicotineRisk",
            causeText: "GAMEPLAY_NicotineRiskCause",
            description: "GAMEPLAY_NicotineRiskDescription",
            descriptionNoHeal: "",
            spriteName: "ico_injury_eventEntity1",
            location: AfflictionBodyArea.Head)
        {
            SmokingDataManager.Instance.ScheduleCureEvents("AddictionRisk");
            GameManager.SaveGame();
        }
        public InstanceType Type { get; set; } = InstanceType.Single;
        public bool Risk { get; set; } = true;
        public bool InstantHeal { get; set; } = true;
        public Tuple<string, int, int>[] RemedyItems { get; set; } = Array.Empty<Tuple<string, int, int>>();
        public Tuple<string, int, int>[] AltRemedyItems { get; set; } = Array.Empty<Tuple<string, int, int>>();

        private float riskPercent = 5f;
        public void OnFoundExistingInstance(CustomAffliction existingAffliction) {}
        public void CureSymptoms() {}
        public void OnCure() {}
        public float GetRiskValue() => riskPercent;
        public override void OnUpdate()
        {
            UpdateRiskValue();
        }
        public void UpdateRiskValue()
        {
            riskPercent = SmokingDataManager.cigarettesWhileAtRisk * 5f;
            riskPercent = Mathf.Clamp(riskPercent, 5f, 100f);
        }
    }
}
