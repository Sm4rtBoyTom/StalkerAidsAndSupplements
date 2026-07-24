namespace StalkerAidsAndSupplementsMod
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Section("Vitamin C Settings")]

        [Name("Vitamin C per Dose")]
        [Description("Adjust how much Vitamin C a single dose of Vitamin C Pills provides. Default: 20. [Requires scene reload.]")]
        [Slider(10, 40, 7)]
        public int VitaminCSmall = 20;

        [Name("Calories per Dose")]
        [Description("Adjust how many calories a single dose of Vitamin C Pills provides. Default: 5. [Requires scene reload.]")]
        [Slider(5, 25, 5)]
        public int VitaminCalories = 5;

        [Section("Sleeping pills Settings")]

        [Name("Fatigue Change per Dose")]
        [Description("Adjust the fatigue change from one dose of Sleeping Pills. Default: -50%. [Requires scene reload.]")]
        [Slider(50, 100, 11)]
        public int SleepingIncrease = 75;

        [Name("Condition Bonus per Hour Slept")]
        [Description("Adjust the additional condition gained per hour slept while the bonus is active. Default: +1% condition/hour. [Requires scene reload.]")]
        [Slider(0, 4, 8)]
        public float SleepingHP = 2.5f;

        [Name("Condition Bonus Duration")]
        [Description("Adjust how long the condition rest bonus lasts per dose. Default: 6 hours. [Requires scene reload.]")]
        [Slider(4, 10, 7)]
        public float SleepingBonusDuration = 8;

        [Section("Caffeine pills Settings")]

        [Name("Fatigue Reduction per Dose")]
        [Description("Adjust the fatigue reduction from one dose of Caffeine Pills. Default: 25%. [Requires scene reload.]")]
        [Slider(25, 50 ,6)]
        public int CaffeineDecrease = 25;

        [Name("Calories per Dose")]
        [Description("Adjust how many calories a single dose of Caffeine Pills provides. Default: 5. [Requires scene reload.]")]
        [Slider(5, 25, 5)]
        public int CaffeineCalories = 5;

        [Name("Fatigue Reduction Duration")]
        [Description("Adjust how long the fatigue reduction lasts per dose. Default: 1 hour. [Requires scene reload.]")]
        [Slider(0, 3, 7)]
        public float CaffeineTime = 1;

        [Name("Carry Weight Bonus")]
        [Description("Adjust the carry weight bonus from one dose of Caffeine Pills. Default: 1.5 kilograms. [Requires scene reload.]")]
        [Slider(1, 3, 5)]
        public float CaffeineCarry = 1.5f;

        [Name("Carry Weight Bonus Duration")]
        [Description("Adjust how long the carry weight bonus lasts per dose. Default: 1.5 hours. [Requires scene reload.]")]
        [Slider(1, 2.5f, 4)]
        public float CaffeineCarryTime = 1.5f;

        [Section("Jam Settings Settings")]
        
        [Name("Calories per Serving")]
        [Description("Adjust how many calories Rosehip Jam provides per serving. Default: 150. [Requires scene reload.]")]
        [Slider(50, 250, 9)]
        public int JamCalories = 150;

        [Name("Vitamin C per Serving")]
        [Description("Adjust how much Vitamin C Rosehip Jam provides per serving. Default: 24. [Requires scene reload.]")]
        [Slider(10, 50, 9)]
        public int VitaminCJam = 25;

        [Section("First Aid Kit Settings")]

        [Name("Condition Restoration per Hour")]
        [Description("Adjust how much condition the First Aid Kit restores per hour. Default: 5% per hour. [Requires scene reload.]")]
        [Slider(1, 7.5f, 14)]
        public float FirstAidAmount = 5;

        [Name("Condition Restoration Duration")]
        [Description("Adjust how long the condition-over-time effect lasts. Default: 6 hours. [Requires scene reload.]")]
        [Slider(2, 8, 13)]
        public float FirstAidTime = 6;

        [Section("Burdock Dressing Settings")]

        [Name("Condition Restoration per Hour")]
        [Description("Adjust how much condition Burdock Dressing restores per hour. Default: 1.5% per hour. [Requires scene reload.]")]
        [Slider(2f, 4f, 5)]
        public float BandageAmount = 3f;

        [Name("Condition Restoration Duration")]
        [Description("Adjust how long the condition-over-time effect lasts. Default: 2 hours. [Requires scene reload.]")]
        [Slider(1, 4, 7)]
        public float BandageTime = 2;

        [Section("Anabiotics Settings")]

        [Name("Condition Restoration per Hour Slept")]
        [Description("Adjust how much condition Anabiotics restore per hour slept. Default: 6% per hour. [Requires scene reload.]")]
        [Slider(3, 8, 11)]
        public float SleepHP = 6;

        [Name("Cabin Fever Reduction (Hours)")]
        [Description("Adjust how many cabin fever hours Anabiotics reduce. Default: 12 hours. [Requires scene reload.]")]
        [Slider(2, 16, 15)]
        public int CabinFeverHours = 12;

        [Section("Misc Settings")]

        [Name("Ibuprofen Immediate Condition Restoration")]
        [Description("Adjust how much condition Ibuprofen increases. Default: 1.5%. [Requires scene reload.]")]
        [Slider(1, 3, 5)]
        public float IbuprofenHP = 2f;

        [Name("Nicotine Boost Duration")]
        [Description("Adjust the duration of Nicotine Boost Buff. Default: 4 Hours.")]
        [Slider(2, 8, 13)]
        public float BuffDuration = 4f;

        [Name("Enable Infection risk changes")]
        [Description("Mostly for mod compatibility, reverts back to vanilla Infection risk/Affliction behaviour. Enabled by default. [Requires scene reload.]")]
        public bool EnableInfection = true;

        [Name("Extra Quest")]
        [Description("Enables/Disables bonus quest from the trader. Enabled by default. TEMPORARILY DISABLED!")]
        public bool TraderExtras = true;

        [Section("Reset Settings")]

        [Name("Reset to Default Settings")]
        [Description("Resets all settings to default. (Confirmation and a scene reload/transition required.)")]
        public bool ResetSettings = false;
        protected override void OnConfirm()
        {
            ApplyReset();
            instance.ResetSettings = false;
            base.OnConfirm();
            instance.RefreshGUI();
        }
        public static void ApplyReset()
        {
            if(instance.ResetSettings == true) 
            {
                instance.VitaminCSmall = 20;
                instance.VitaminCalories = 5;
                instance.SleepingIncrease = 50;
                instance.SleepingBonusDuration = 6;
                instance.SleepingHP = 2.5f;
                instance.CaffeineCalories = 5;
                instance.CaffeineDecrease = 25;
                instance.CaffeineTime = 1;
                instance.JamCalories = 150;
                instance.VitaminCJam = 24;
                instance.FirstAidTime = 6;
                instance.FirstAidAmount = 5;
                instance.BandageTime = 2;
                instance.BandageAmount = 3f;
                instance.SleepHP = 6;
                instance.CaffeineCarry = 1.5f;
                instance.CaffeineCarryTime = 1.5f;
                instance.CabinFeverHours = 12;
                instance.EnableInfection = true;
                instance.IbuprofenHP = 2f;
                instance.BuffDuration = 4f;
                instance.RefreshGUI();
            }
        }
    }
}